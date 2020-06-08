// //////////////////////////////////////////////////
// Dependencies
// //////////////////////////////////////////////////
#tool nuget:?package=Cake.Sitecore&prerelease
#load nuget:?package=Cake.Sitecore&prerelease

#load ./scripts/cake/xunit.cake
#load ./scripts/cake/coverage.cake
#load ./scripts/code-generation/generateTypescript.cake

// //////////////////////////////////////////////////
// Arguments
// //////////////////////////////////////////////////
var Target = ArgumentOrEnvironmentVariable("target", "", "Default");

// //////////////////////////////////////////////////
// Prepare
// //////////////////////////////////////////////////

Sitecore.Constants.SetNames();
Sitecore.Parameters.InitParams(
    context: Context,
    msBuildToolVersion: MSBuildToolVersion.VS2019,
    solutionName: "HCA",
    scSiteUrl: "https://sc9.local", // default URL exposed from the box
    unicornSerializationRoot: "unicorn-hca",
    publishingTargetDir: "\\\\192.168.50.4\\c$\\inetpub\\wwwroot\\sc9.local",
    xUnitTestsCoverageRegister: "Path64",
    xUnitTestsCoverageExcludeAttributeFilters: "*ExcludeFromCodeCoverage*",
    xUnitTestsCoverageExcludeFileFilters: "*.Generated.cs;*\\App_Start\\*",
    supportHelix20: "true"
);

// //////////////////////////////////////////////////
// Extensions
// //////////////////////////////////////////////////

Task("Generate-Client-Models")
    .Does(() =>
    {
        var regex = @"^.+[/\\](?<layerName>[^/\\]+)[/\\](?<projectName>[^/\\]+)[/\\](client|code)[/\\].+$";

        var templateFiles = GetFiles($"./../src/*/*/client/**/*.tt");
        Information($"Found files: {templateFiles.Count}");

        foreach (var templateFile in templateFiles)
        {
            Information($"Running client generation in {templateFile}.");

            Regex fileRegex = new Regex(regex);
            System.Text.RegularExpressions.Match match = fileRegex.Match(templateFile.FullPath);
            var layer = match.Groups["layerName"];
            var project = match.Groups["projectName"];

            var tsFile = templateFile.GetDirectory().CombineWithFilePath(new FilePath(templateFile.GetFilenameWithoutExtension() + ".ts"));
            var dllFile = GetFiles($"./../src/{layer}/{project}/website/**/*.{layer}.{project}.dll").FirstOrDefault();

            generateTypeScript(dllFile, tsFile, templateFile);
        }
    });

Task("Generate-Commerce-Code")
    .Does(() =>
    {
        Sitecore.Utils.AssertIfNullOrEmpty(Sitecore.Parameters.SrcDir, "SrcDir", "SRC_DIR");

        var settings = new NpmRunScriptSettings();

        settings.ScriptName = "sc:codegen:commerce";
        settings.LogLevel = NpmLogLevel.Error;
        settings.FromPath(Sitecore.Parameters.SrcDir);

        NpmRunScript(settings);
    });

// //////////////////////////////////////////////////
// Tasks
// //////////////////////////////////////////////////

Task("000-Clean")
    .IsDependentOn(Sitecore.Tasks.ConfigureToolsTaskName)
    .IsDependentOn(Sitecore.Tasks.CleanWildcardFoldersTaskName)
    .Does(() => {
        DeleteFiles(Sitecore.Parameters.PublishingTargetDir + @"\App_Config\Include\Unicorn\*");    
        DeleteFiles(Sitecore.Parameters.PublishingTargetDir + @"\App_Config\Include\Foundation\HCA.*");        
        DeleteFiles(Sitecore.Parameters.PublishingTargetDir + @"\App_Config\Include\Feature\HCA.*");        
        DeleteFiles(Sitecore.Parameters.PublishingTargetDir + @"\App_Config\Include\Project\HCA.*");      
        DeleteFiles(Sitecore.Parameters.PublishingTargetDir + @"\bin\HCA.*");
    });

Task("001-Restore")
    .IsDependentOn(Sitecore.Tasks.RestoreNuGetPackagesTask)
    .IsDependentOn(Sitecore.Tasks.RestoreNpmPackagesTaskName)
    ;

Task("002-Build")
    .IsDependentOn(Sitecore.Tasks.GenerateCodeTaskName)
    .IsDependentOn(Sitecore.Tasks.BuildClientCodeTaskName)
    .IsDependentOn(Sitecore.Tasks.BuildServerCodeTaskName)
    ;

Task("003-Tests")
    .IsDependentOn(XUnitRunner.RunTests)
    .IsDependentOn(Sitecore.Tasks.RunClientUnitTestsTaskName)
    .IsDependentOn(Sitecore.Tasks.MergeCoverageReportsTaskName)
    .IsDependentOn(Coverage.OutputCoverage)
    ;

Task("004-Packages")
    //.IsDependentOn(Sitecore.Tasks.CopyShipFilesTaskName)
    //.IsDependentOn(Sitecore.Tasks.CopySpeRemotingFilesTaskName)
    .IsDependentOn(Sitecore.Tasks.PrepareWebConfigTask)
    .IsDependentOn(Sitecore.Tasks.RunPackagesInstallationTask)
    ;

Task("005-Publish")
    .IsDependentOn(Sitecore.Tasks.PublishFoundationTaskName)
    .IsDependentOn(Sitecore.Tasks.PublishFeatureTaskName)
    .IsDependentOn(Sitecore.Tasks.PublishProjectTaskName)
    ;

Task("006-Sync-Content")
    .IsDependentOn(Sitecore.Tasks.SyncAllUnicornItems)
    ;

// //////////////////////////////////////////////////
// Targets
// //////////////////////////////////////////////////

Task("Default") // LocalDev
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

Task("Client-Build-and-Publish") // LocalDev
    .IsDependentOn(Sitecore.Tasks.BuildClientCodeTaskName)
    .IsDependentOn(Sitecore.Tasks.PublishProjectTaskName)
    ;



// //////////////////////////////////////////////////
// Execution
// //////////////////////////////////////////////////

RunTarget(Target);