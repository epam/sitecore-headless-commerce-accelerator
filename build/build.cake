// //////////////////////////////////////////////////
// Dependencies
// //////////////////////////////////////////////////
#tool nuget:?package=Cake.Sitecore&prerelease
#load nuget:?package=Cake.Sitecore&prerelease

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
    solutionName: "Wooli",
    scSiteUrl: "https://sc9.local", // default URL exposed from the box
    unicornSerializationRoot: "unicorn-wooli",
    publishingTargetDir: "\\\\192.168.0.1\\c$\\inetpub\\wwwroot\\sc9.local"
);

// //////////////////////////////////////////////////
// Tasks
// //////////////////////////////////////////////////

Task("000-Clean")
    .IsDependentOn(Sitecore.Tasks.ConfigureToolsTaskName)
    .IsDependentOn(Sitecore.Tasks.CleanWildcardFoldersTaskName)
    .Does(() => {
        DeleteFiles(Sitecore.Parameters.PublishingTargetDir + @"\App_Config\Include\Unicorn\*");
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
    .IsDependentOn(Sitecore.Tasks.RunServerUnitTestsTaskName)
    .IsDependentOn(Sitecore.Tasks.RunClientUnitTestsTaskName)
    .IsDependentOn(Sitecore.Tasks.MergeCoverageReportsTaskName)
    ;

Task("004-Packages")
    .IsDependentOn(Sitecore.Tasks.CopyShipFilesTaskName)
    .IsDependentOn(Sitecore.Tasks.CopySpeRemotingFilesTaskName)
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
    //.IsDependentOn("003-Tests") // comment this line if deploy process fails on this step
    //.IsDependentOn("004-Packages")
    .IsDependentOn("005-Publish");
    //.IsDependentOn("006-Sync-Content"); // comment this line if deploy process fails on this step

Task("Build-and-Publish") // LocalDev
    .IsDependentOn("002-Build")
    .IsDependentOn("005-Publish");
// //////////////////////////////////////////////////
// Execution
// //////////////////////////////////////////////////

RunTarget(Target);