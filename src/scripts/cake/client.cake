#addin "Cake.Npm"

#load ../code-generation/generateTypescript.cake

public static class Client
{
    public const string CLIENT_DIR = "CLIENT_DIR";
    public const string CLIENT_BUILD_SCRIPT = "CLIENT_BUILD_SCRIPT";
    public const string CLIENT_TESTS_SCRIPT = "CLIENT_TESTS_SCRIPT";

    private static ICakeContext _context;

    public static string ClientDir { get; private set; }
    public static string ClientBuildScript { get; private set; }
    public static string ClientTestsScript { get; private set; }

    public static CakeTaskBuilder RestoreNpmPackages { get; set; }
    public static CakeTaskBuilder GenerateCode { get; set; }
    public static CakeTaskBuilder GenerateCommerceCode { get; set; }
    public static CakeTaskBuilder BuildCode { get; set; }
    public static CakeTaskBuilder RunTests { get; set; }
    public static CakeTaskBuilder Publish { get; set; }
    public static CakeTaskBuilder GenerateModels { get; set; }

    public static void InitParams(
        ICakeContext context,
        string clientDir = null,
        string clientBuildScript = null,
        string clientTestsScript = null)
    {
        _context = context;

        ClientDir = GetAbsoluteDirPath(GetParameterValue(Client.CLIENT_DIR, clientDir ?? "./../src/client"));
        ClientBuildScript = GetParameterValue(Client.CLIENT_BUILD_SCRIPT, clientBuildScript ?? "build:Debug");
        ClientTestsScript = GetParameterValue(Client.CLIENT_TESTS_SCRIPT, clientTestsScript ?? "test-cover");
    }

    // TODO: Copied from Cake.Sitecore due to private modificator
    private static string GetAbsoluteDirPath(string path){
        return _context
            .MakeAbsolute(_context.Directory(path)).Collapse().FullPath.ToString();
    }

    // TODO: Copied from Cake.Sitecore due to private modificator
    private static string GetParameterValue(string argumentName, string defaultValue, string environmentNamePrefix = null) {
        return Sitecore.Utils.ArgumentOrEnvironmentVariable(_context, argumentName, defaultValue, environmentNamePrefix);
    }
}

Client.RestoreNpmPackages = Task("Client :: Restore NPM Packages")
    .Does(()=>
    {
        Sitecore.Utils.AssertIfNullOrEmpty(Client.ClientDir, "ClientDir", "CLIENT_DIR");

        var settings = new NpmInstallSettings();
        settings.LogLevel = NpmLogLevel.Error;
        settings.FromPath(Client.ClientDir);

        NpmInstall(settings);
    });

Client.GenerateCode = Task("Client :: Generte Code")
    .Does(() =>
    {
        Sitecore.Utils.AssertIfNullOrEmpty(Client.ClientDir, "ClientDir", "CLIENT_DIR");

        runNpmScript("sc:codegen", NpmLogLevel.Error, Client.ClientDir);
    });

Client.GenerateCommerceCode = Task("Client :: Generte Commerce Code")
    .Does(() =>
    {
        Sitecore.Utils.AssertIfNullOrEmpty(Client.ClientDir, "ClientDir", "CLIENT_DIR");

        runNpmScript("sc:codegen:commerce", NpmLogLevel.Error, Client.ClientDir);
    });

Client.BuildCode = Task("Client :: Build Code")
    .Does(() =>
    {
        Sitecore.Utils.AssertIfNullOrEmpty(Client.ClientDir, "ClientDir", "CLIENT_DIR");
        Sitecore.Utils.AssertIfNullOrEmpty(Client.ClientBuildScript, "ClientBuildScript", "CLIENT_BUILD_SCRIPT");

        runNpmScript(Client.ClientBuildScript, NpmLogLevel.Error, Client.ClientDir);
    });

Client.RunTests = Task("Client :: Run Tests")
    .Does(() =>
    {
        Sitecore.Utils.AssertIfNullOrEmpty(Client.ClientDir, "ClientDir", "CLIENT_DIR");
        Sitecore.Utils.AssertIfNullOrEmpty(Client.ClientTestsScript, "ClientTestsScript", "CLIENT_TESTS_SCRIPT");

        runNpmScript(Client.ClientTestsScript, NpmLogLevel.Error, Client.ClientDir);
    });

Client.Publish = Task("Client :: Publish")
    .Does(() => 
    {
        Sitecore.Utils.AssertIfNullOrEmpty(Sitecore.Parameters.PublishingTargetDir, "PublishingTargetDir", "PUBLISHING_TARGET_DIR");
        Sitecore.Utils.AssertIfNullOrEmpty(Sitecore.Parameters.SolutionName, "SolutionName", "SOLUTION_NAME");
        Sitecore.Utils.AssertIfNullOrEmpty(Client.ClientDir, "ClientDir", "CLIENT_DIR");

        var buildDirectoryList = GetDirectories($"{Client.ClientDir}/src/Project/*/build");

        foreach (var buildDirectory in buildDirectoryList)
        {
            var pathSegments =  buildDirectory.ToString()
              .Replace("/build", "")
              .Split('/');
            var project = pathSegments[pathSegments.Length -1];

            var targetDir = $"{Sitecore.Parameters.PublishingTargetDir}/dist/{Sitecore.Parameters.SolutionName}/Project/{project}";
            
            if (DirectoryExists(targetDir))
            {
                CleanDirectory(targetDir);
            }

            Information($"Publishing {project} client project");
            CopyDirectory(buildDirectory, targetDir);
        }
    });

Client.GenerateModels = Task("Client :: Generate Models")
    .Does(() =>
    {
        Sitecore.Utils.AssertIfNullOrEmpty(Client.ClientDir, "ClientDir", "CLIENT_DIR");
        Sitecore.Utils.AssertIfNullOrEmpty(Sitecore.Parameters.SrcDir, "SrcDir", "SRC_DIR");

        var regex = @"^.+[/\\](src)[/\\](?<layerName>[^/\\]+)[/\\](?<projectName>[^/\\]+)[/\\].+$";

        var templateFiles = GetFiles($"{Client.ClientDir}/src/**/*.tt");
        Information($"Found files: {templateFiles.Count}");

        foreach (var templateFile in templateFiles)
        {
            Information($"Running client generation in {templateFile}.");

            Regex fileRegex = new Regex(regex);
            System.Text.RegularExpressions.Match match = fileRegex.Match(templateFile.FullPath);
            var layer = match.Groups["layerName"];
            var project = match.Groups["projectName"];

            var tsFile = templateFile.GetDirectory().CombineWithFilePath(new FilePath(templateFile.GetFilenameWithoutExtension() + ".ts"));
            var dllFile = GetFiles($"{Sitecore.Parameters.SrcDir}/{layer}/{project}/website/**/*.{layer}.{project}.dll").FirstOrDefault();

            generateTypeScript(dllFile, tsFile, templateFile);
        }
    });

Action<string, NpmLogLevel, DirectoryPath> runNpmScript = (scriptName, logLevel, fromPath) =>
{
    var settings = new NpmRunScriptSettings();

    settings.ScriptName = scriptName;
    settings.LogLevel = logLevel;
    settings.FromPath(fromPath);

    NpmRunScript(settings);
};