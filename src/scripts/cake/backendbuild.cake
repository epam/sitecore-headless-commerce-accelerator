#addin "Cake.FileHelpers&version=3.2.0"

public static class Server
{
    public static CakeTaskBuilder BuildCodeTask { get; set; }
}

Server.BuildCodeTask = Task("BuildCode :: BuildBackEnd Code")
    .Description("Runs MsBuild for a solution (`SOLUTION_FILE_PATH`) with a specific build configuration (`BUILD_CONFIGURATION`)")
    .Does(() =>
    {
        Sitecore.Utils.AssertIfNullOrEmpty(Sitecore.Parameters.BuildConfiguration, "BuildConfiguration", "BUILD_CONFIGURATION");
        Sitecore.Utils.AssertIfNullOrEmpty(Sitecore.Parameters.SolutionFilePath, "SolutionFilePath", "SOLUTION_FILE_PATH");

        var msBuildConfig = new MSBuildSettings()
            .SetConfiguration(Sitecore.Parameters.BuildConfiguration)
            .SetVerbosity(Verbosity.Minimal) // TODO: figure out how to get access to -Verbosity flag
            .UseToolVersion(Sitecore.Parameters.MsBuildToolVersion)
            .WithProperty("DeployOnBuild", "true")
            .SetMaxCpuCount(new int?()) // TODO: make configurable
            .WithTarget("Rebuild"); // TODO: move to configuration

        MSBuild(Sitecore.Parameters.SolutionFilePath, msBuildConfig);
    });