#tool nuget:?package=NuGet.CommandLine&version=5.11.0
#addin nuget:?package=Cake.Npm&version=1.0.0
#tool nuget:?package=xunit.runner.console&version=2.4.1
#tool nuget:?package=OpenCover&version=4.7.1221
#tool nuget:?package=OpenCoverToCoberturaConverter&version=0.3.4
#tool nuget:?package=ReportGenerator&version=4.8.13
#addin nuget:?package=Microsoft.Web.Xdt&version=3.1.0

#load ./solr.cake
#load ./ts.cake

using Microsoft.Web.XmlTransform;
using System.Text.RegularExpressions;

var rootDir = "./..";
var srcDir = $"{rootDir}/src";
var solutionName = "HCA";
var solution = $"{solutionName}.sln";
var buildToolVersion = MSBuildToolVersion.VS2022;
var clientDir = $"{srcDir}/client";
var outputDir = $"{rootDir}/output";
var testsOutputDir = $"{outputDir}/tests";
var testsCoverageOutputDir = $"{testsOutputDir}/coverage";
var xUnitTestsCoverageOutputDir = $"{testsCoverageOutputDir}/xUnit";
var srcConfigsDir = $"{srcDir}/configs";
var publishingTargetDir = "\\\\192.168.50.4\\c$\\inetpub\\wwwroot\\sc10_sc.dev.local";
var scNodeEnv = "local|standalone";
var unitTestsFailed = false;

var Target = Argument("target", EnvironmentVariable("target", "Default"));

Solr.InitParams(
    context: Context,
    vagrantIP: "192.168.50.4",
    solrInstance: "s10_solr-8.4.0",
    solrPort: "8983",
    solrCore: "sc10__master_index",
    recreateCoresIfExist: true,
    coresToCreate: new string[] {}
);

Action<string, string, List<string>> transform = (sourceFile, targetFile, transforms) =>
{
    var _appliedTransforms = new List<String>();
    using(var _document  = new XmlTransformableDocument { PreserveWhitespace = true })
    {
        _document.Load(sourceFile);

        foreach(var _transform in transforms)
        {
            var _transformFile = File($"{sourceFile}.{_transform}.transform");
            if (!FileExists(_transformFile))
            {
                continue;
            }
            using(var _transformation = new Microsoft.Web.XmlTransform.XmlTransformation(_transformFile))
            {
                if(!_transformation.Apply(_document))
                {
                    throw new Exception($"Failed to transform \"{sourceFile}\" using \"{_transformFile}\" to \"{targetFile}\"");
                }
                _appliedTransforms.Add(_transform);
            }
        }

        var _targetFilePath = new FilePath(targetFile);
        var _targetDirectory = _targetFilePath.GetDirectory();

        if (!DirectoryExists(_targetDirectory))
        {
            CreateDirectory(_targetDirectory);
        }

        _document.Save(targetFile);

        var _transformationsMessage = _appliedTransforms.Any()
            ? string.Join(">", _appliedTransforms)
            : "transforms not found!";

        Information($" + updated '{targetFile}': {_transformationsMessage}");
    }
};

Action<string, string, string> copyClientAssets = (srcRootDir, layer, solutionName) =>
{
    Verbose($"Executing [copyClientAssets] with params ({srcRootDir}, {layer}, {solutionName})");

    var _template = $"{srcRootDir}/{layer}/*/client/build";
    var _directoryList = GetDirectories(_template);

    // iterate over every client build folder
    foreach(var _directory in _directoryList)
    {
        Verbose($"Copy client assets from: {_directory.ToString()}");

        var _pathSegments =  _directory.ToString()
            .Replace("/client/build", "")
            .Split('/');
        var _project = _pathSegments[_pathSegments.Length -1];

        // copy client build artifacts into a project wildcard folder
        var _targetDir = $"{_directory}/../../website/dist/{solutionName}/{layer}/{_project}";
        CopyDirectory(_directory, _targetDir);
    }
};

Action<string, string, string, MSBuildToolVersion> publishProject = (projectFilePath, buildConfiguration, dest, msBuildToolVersion) =>
{
    Verbose($"Executing [publishProject] with params ({projectFilePath}, {buildConfiguration}, {dest}, {msBuildToolVersion.ToString()})");

    var _settings = new MSBuildSettings()
        .SetConfiguration(buildConfiguration)
        .SetVerbosity(Verbosity.Minimal)
        .UseToolVersion(msBuildToolVersion)
        .WithTarget("Build")
        .WithProperty("DeployOnBuild", "true")
        .WithProperty("DeployDefaultTarget", "WebPublish")
        .WithProperty("WebPublishMethod", "FileSystem")
        .WithProperty("DeleteExistingFiles", "false")
        .WithProperty("publishUrl", dest)
        .WithProperty("_FindDependencies", "false");

    MSBuild(projectFilePath, _settings);
};

Action<string, string, string, string, MSBuildToolVersion> publishLayer = (srcRootDir, layer, buildConfiguration, dest, msBuildToolVersion) =>
{
    Verbose($"Executing [publishLayer] with params ({srcRootDir}, {layer}, {buildConfiguration}, {dest}, {msBuildToolVersion.ToString()})");

    // perform cleanup layer configs directory operation for "Debug" configuration
    // Disable configs cleanup as Cake doesn't support glob patterns for UNC and CleanDirectory won't delete configs correctly in multiproject setup
    /*/if (buildConfiguration == "Debug")
    {
        var _configsDirPath = $"{dest}/App_Config/Include/{layer}";
        if (DirectoryExists(_configsDirPath)) {
            Information($"Cleaning configs directory: {_configsDirPath}");
            CleanDirectory(_configsDirPath);
        }
    }*/

    var _projectFilePathList = GetFiles($"{srcRootDir}/{layer}/**/website/*.csproj");

    foreach(var _projectFilePath in _projectFilePathList)
    {
        publishProject(_projectFilePath.ToString(), buildConfiguration, dest, msBuildToolVersion);
    }
};

Task("Clean-up :: Clean Wildcard Folders")
    .Description("Remove items from project ./dist and ./App_data folders")
    .Does(() =>
    {

        var _layerDirectories =
            GetDirectories($"{srcDir}/Foundation") +
            GetDirectories($"{srcDir}/Feature") +
            GetDirectories($"{srcDir}/Project");

        var _projectDirectories = _layerDirectories.SelectMany(_dir => GetDirectories($"{_dir}/*/website"));

        var _wildcardDirectories = _projectDirectories.SelectMany(_dir => GetDirectories($"{_dir}/dist")).ToArray();

        foreach (var directory in _wildcardDirectories)
        {
            CleanDirectory(directory);
        }
    });

Task("Restore :: Restore NuGet Packages")
    .Does(() => NuGetRestore(solution));

Task("Restore :: Restore NPM Packages")
    .Does(() => NpmInstall());

Task("Client :: Restore NPM Packages")
    .Does(()=>
    {
        var settings = new NpmInstallSettings();
        settings.LogLevel = NpmLogLevel.Error;
        settings.FromPath(clientDir);
        NpmInstall(settings);
    });

Task("Build :: Generate Code")
    .Description("Executes JS plugin to parse Unicorn files via `npm run` and generate code.")
    .Does(() =>
    {
        var settings = new NpmRunScriptSettings();
        settings.ScriptName = "sc:codegen";
        settings.LogLevel = NpmLogLevel.Error;
        settings.FromPath(srcDir);
        NpmRunScript(settings);
    });

Task("Client :: Generate Code")
    .Does(() =>
    {       
        var settings = new NpmRunScriptSettings();
        settings.ScriptName = "sc:codegen";
        settings.LogLevel = NpmLogLevel.Error;
        settings.FromPath(clientDir);
        NpmRunScript(settings);
    });

Task("Client :: Build Code")
    .Does(() =>
    {
        var settings = new NpmRunScriptSettings();
        settings.ScriptName = "build:Debug";
        settings.LogLevel = NpmLogLevel.Error;
        settings.FromPath(clientDir);
        NpmRunScript(settings);
    });

Task("Build :: Build Server Code")
    .Does(() =>
    {
        var msBuildConfig = new MSBuildSettings()
            .SetConfiguration("Debug")
            .SetVerbosity(Verbosity.Minimal)
            .UseToolVersion(buildToolVersion)
            .SetMaxCpuCount(new int?())
            .WithTarget("Rebuild");

        MSBuild(solution, msBuildConfig);
    });

Task("xUnit Tests :: Run Server Tests")
    .Does(() =>
    {
        var coverSettings = new OpenCoverSettings()
            .WithFilter($"+[{solutionName}.*]*")
            .WithFilter($"-[{solutionName}.*.Tests*]*");
        coverSettings.SkipAutoProps = true;
        coverSettings.Register = "Path64";
        coverSettings.MergeByHash = true;
        coverSettings.NoDefaultFilters = true;
        coverSettings.ReturnTargetCodeOffset = 0;

        void applyExclude<T>(ISet<T> filtersSet, string paramValue, Func<string, T> mapper)
        {
            if (!string.IsNullOrEmpty(paramValue))
            {
                var excludes = paramValue.Split(',').Select(mapper);
                filtersSet.UnionWith(excludes);
            }
        }

        applyExclude(coverSettings.ExcludedAttributeFilters, "*ExcludeFromCodeCoverage*", x => x);
        applyExclude(coverSettings.ExcludedFileFilters,      "*.Generated.cs;*\\App_Start\\*", x => x);
        //applyExclude(coverSettings.ExcludeDirectories,       xUnitTestsCoverageExcludeDirectories, x => Directory($"{Sitecore.Parameters.SrcDir}/{x}"));

        var directories = GetDirectories(
                $"{srcDir}/**/bin", 
                new GlobberSettings { Predicate = fileSystemInfo => !fileSystemInfo.Path.FullPath.EndsWith("node_modules", StringComparison.OrdinalIgnoreCase) }
            );
        foreach (var directory in directories)
        {
            coverSettings.SearchDirectories.Add(directory);
        }

        EnsureDirectoryExists(xUnitTestsCoverageOutputDir);

        var openCoverResultsFilePath = new FilePath($"{xUnitTestsCoverageOutputDir}/coverage.xml");

        var xUnit2Settings = new XUnit2Settings {
                XmlReport = true,
                Parallelism = ParallelismOption.None,
                NoAppDomain = false,
                OutputDirectory = testsOutputDir,
                ReportName = "xUnitTestResults",
                ShadowCopy = false
            };

        OpenCover(
            tool => { tool.XUnit2($"{srcDir}/**/tests/bin/*.Tests.dll", xUnit2Settings); }, 
            openCoverResultsFilePath, 
            coverSettings
        );

        ReportGenerator(openCoverResultsFilePath, xUnitTestsCoverageOutputDir);

        var converterExecutablePath = Context.Tools.Resolve("OpenCoverToCoberturaConverter.exe");
        StartProcess(converterExecutablePath, new ProcessSettings {
            Arguments = new ProcessArgumentBuilder()
                .Append($"-input:\"{openCoverResultsFilePath}\"")
                .Append($"-output:\"{xUnitTestsCoverageOutputDir}/cobertura-coverage.xml\"")
        });
    })
    .OnError(exception =>
    {
        //unitTestsFailed = true;
        Error(exception);
    })
    ;

Task("Client :: Run Tests")
    .Does(() =>
    {
        var settings = new NpmRunScriptSettings();
        settings.ScriptName = "test-cover";
        settings.LogLevel = NpmLogLevel.Error;
        settings.FromPath(clientDir);
        NpmRunScript(settings);
    });

Task("Packages :: Prepare web.config")
    .Does(() =>
    {

        var _sourceFilePath = $"{srcConfigsDir}/Sitecore/web.config";
        var _targetFilePath = $"{publishingTargetDir}/web.config";
        var _transforms = scNodeEnv.Split('|').ToList();

        Verbose($"Tranforming {_sourceFilePath} to {_targetFilePath}.");
        transform(_sourceFilePath, _targetFilePath, _transforms);
    });

Task("Publish :: Foundation")
    .Does(() =>
    {
        var _layer = "Foundation";        
        copyClientAssets(srcDir, _layer, solutionName);
        publishLayer(
            srcDir,
            _layer,
            "Debug",
            publishingTargetDir,
            buildToolVersion);
    });

Task("Publish :: Features")
    .Description("Publishes all Feature-layer projects to the publishing target directory (`PUBLISHING_TARGET_DIR`) using MsBuild.")
    .Does(() =>
    {
        var _layer = "Feature";

        copyClientAssets(srcDir, _layer, solutionName);
        publishLayer(
            srcDir,
            _layer,
            "Debug",
            publishingTargetDir,
            buildToolVersion);
    });

Task("Publish :: Projects")
    .Description("Publishes all Project-layer projects to the publishing target directory (`PUBLISHING_TARGET_DIR`) using MsBuild.")
    .Does(() =>
    {
        var _layer = "Project";

        copyClientAssets(srcDir, _layer, solutionName);
        publishLayer(
            srcDir,
            _layer,
            "Debug",
            publishingTargetDir,
            buildToolVersion);
    });

Task("Client :: Publish")
    .Does(() => 
    {
        var buildDirectoryList = GetDirectories($"{clientDir}/src/bootstrap/build");

        foreach (var buildDirectory in buildDirectoryList)
        {
            var pathSegments =  buildDirectory.ToString()
              .Replace("/build", "")
              .Split('/');
            var project = pathSegments[pathSegments.Length -1];

            var targetDir = $"{publishingTargetDir}/dist/{solutionName}/Project/{solutionName}";
            
            if (DirectoryExists(targetDir))
            {
                CleanDirectory(targetDir);
            }

            Information($"Publishing {project} client project");
            CopyDirectory(buildDirectory, targetDir);
        }
    });

Task("BuildCode :: BuildBackEnd Code")
    .Does(() =>
    {
        var msBuildConfig = new MSBuildSettings()
            .SetConfiguration("Debug")
            .SetVerbosity(Verbosity.Minimal) // TODO: figure out how to get access to -Verbosity flag
            .UseToolVersion(buildToolVersion)
            .WithProperty("DeployOnBuild", "true")
            .SetMaxCpuCount(new int?()) // TODO: make configurable
            .WithTarget("Rebuild"); // TODO: move to configuration

        MSBuild(solution, msBuildConfig);
    });

Task("Client :: Generate Models")
    .Does(() =>
    {
        // pattern to extract dll name from parameter value
        var regex = new Regex("\\w*name=\"DllName\"\\s+value=\"(?<layerName>\\w+)\\.(?<projectName>\\w+)\"");

        var templateFiles = GetFiles($"{clientDir}/src/**/*.tt");
        Information($"Found files: {templateFiles.Count}");

        foreach (var templateFile in templateFiles)
        {
            Information($"Running client generation in {templateFile}.");

            string templateFileData = System.IO.File.ReadAllText(templateFile.FullPath);
            System.Text.RegularExpressions.Match match = regex.Match(templateFileData);
            
            var layer = match.Groups["layerName"];
            var project = match.Groups["projectName"];

            var tsFile = templateFile.GetDirectory().CombineWithFilePath(new FilePath(templateFile.GetFilenameWithoutExtension() + ".ts"));
            var dllFile = GetFiles($"{srcDir}/{layer}/{project}/website/**/*.{layer}.{project}.dll").FirstOrDefault();

            generateTypeScript(dllFile, tsFile, templateFile);
        }
    });


Task("000-Clean")
    .IsDependentOn("Clean-up :: Clean Wildcard Folders")
    .Does(() => {
        DeleteFiles(publishingTargetDir + @"\App_Config\Include\Glass\*");
        DeleteFiles(publishingTargetDir + @"\App_Config\Include\Foundation\HCA.*");  
        DeleteFiles(publishingTargetDir + @"\App_Config\Include\Feature\HCA.*");
        DeleteFiles(publishingTargetDir + @"\App_Config\Include\Project\HCA.*");
        DeleteFiles(publishingTargetDir + @"\bin\HCA.*");
    });

Task("001-Restore")
    .IsDependentOn("Restore :: Restore NuGet Packages")
    .IsDependentOn("Restore :: Restore NPM Packages")
    .IsDependentOn("Client :: Restore NPM Packages")
    ;

Task("002-Build")
    .IsDependentOn("Build :: Generate Code")
    .IsDependentOn("Client :: Generate Code")
    .IsDependentOn("Client :: Build Code")
    .IsDependentOn("Build :: Build Server Code")
    ;

Task("003-Tests")
    .IsDependentOn("xUnit Tests :: Run Server Tests")
    .IsDependentOn("Client :: Run Tests")
    //.IsDependentOn(Sitecore.Tasks.MergeCoverageReportsTaskName)
    //.IsDependentOn(Coverage.OutputCoverage)
    ;

Task("004-Packages")
    .IsDependentOn("Packages :: Prepare web.config")
    //.IsDependentOn(Sitecore.Tasks.RunPackagesInstallationTask)
    ;

Task("005-Publish")
    .IsDependentOn("Publish :: Foundation")
    .IsDependentOn("Publish :: Features")
    .IsDependentOn("Publish :: Projects")
    .IsDependentOn("Client :: Publish")
    ;

Task("005-Publish-Back-Only")
    .IsDependentOn("Publish :: Foundation")
    .IsDependentOn("Publish :: Features")
    .IsDependentOn("Publish :: Projects")
    ;

Task("006-Sync-Content")
    .Does(() => { DotNetCoreTool("sitecore ser push"); })
    ;

Task("007-Update-SolrConfig")
    .IsDependentOn(Solr.AddSuggesterComponents)
    ;

Task("008-Build-Server-Code")
    .IsDependentOn("BuildCode :: BuildBackEnd Code")
    ;

Task("009-Create-SolrCores")
    .IsDependentOn(Solr.CreateCores)
    ;

Task("010-Update-Solr-Schema")
    .IsDependentOn(Solr.UpdateSchema)
    ;

Task("Default")
    .IsDependentOn("000-Clean")
    .IsDependentOn("001-Restore")
    .IsDependentOn("002-Build")
    .IsDependentOn("003-Tests")
    .IsDependentOn("004-Packages")
    .IsDependentOn("005-Publish")
    .IsDependentOn("006-Sync-Content")
    ;

Task("Build-and-Publish") // LocalDev
    .IsDependentOn("002-Build")
    .IsDependentOn("005-Publish")
    ;

Task("Build-Backend")
    .IsDependentOn("000-Clean")
    .IsDependentOn("Restore :: Restore NuGet Packages")
    .IsDependentOn("Restore :: Restore NPM Packages")
    .IsDependentOn("Build :: Generate Code")
    .IsDependentOn("Build :: Build Server Code")
    .IsDependentOn("Packages :: Prepare web.config")
    .IsDependentOn("005-Publish-Back-Only")
    .IsDependentOn("006-Sync-Content")
    ;

Task("Build-Backend-With-Tests")
    .IsDependentOn("000-Clean")
    .IsDependentOn("Restore :: Restore NuGet Packages")
    .IsDependentOn("Restore :: Restore NPM Packages")
    .IsDependentOn("Build :: Generate Code")
    .IsDependentOn("Build :: Build Server Code")
    .IsDependentOn("Packages :: Prepare web.config")
    .IsDependentOn("xUnit Tests :: Run Server Tests")
    .IsDependentOn("005-Publish-Back-Only")
    .IsDependentOn("006-Sync-Content")
    ;

Task("Generate-Client-Models")
    .IsDependentOn("Client :: Generate Models")
    ;

Task("Initial-Deploy")
    .IsDependentOn("007-Update-SolrConfig")
    .IsDependentOn("009-Create-SolrCores")
    .IsDependentOn("010-Update-Solr-Schema")
    .IsDependentOn("Default")
    ;

Task("Create-Solr-Cores")
    .IsDependentOn("009-Create-SolrCores");

RunTarget(Target);