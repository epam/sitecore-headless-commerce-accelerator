// //////////////////////////////////////////////////
// Dependencies
// //////////////////////////////////////////////////
#tool nuget:?package=Cake.Sitecore.Recipe&prerelease
#load nuget:?package=Cake.Sitecore.Recipe&prerelease

#load ./scripts/cake/xunit.cake
#load ./scripts/cake/coverage.cake
#load ./scripts/cake/client.cake
#load ./scripts/cake/solr.cake
#load ./scripts/cake/backendbuild.cake

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
    scSiteUrl: "https://sc10_sc.dev.local", // default URL exposed from the box
    unicornSerializationRoot: "unicorn-hca",
    publishingTargetDir: "\\\\192.168.50.4\\c$\\inetpub\\wwwroot\\sc10_sc.dev.local",
    xUnitTestsCoverageRegister: "Path64",
    xUnitTestsCoverageExcludeAttributeFilters: "*ExcludeFromCodeCoverage*",
    xUnitTestsCoverageExcludeFileFilters: "*.Generated.cs;*\\App_Start\\*",
    supportHelix20: "true"
);

Client.InitParams(
    context: Context,
    clientBuildScript: $"build:{Sitecore.Parameters.BuildConfiguration}"
);

Solr.InitParams(
    context: Context,
    vagrantIP: "192.168.50.4",
    solrInstance: "s10_solr-8.4.0",
    solrPort: "8983",
    solrCore: "sc10__master_index",
    recreateCoresIfExist: true,
    coresToCreate: new string[] {}
);

// //////////////////////////////////////////////////
// Extensions
// //////////////////////////////////////////////////

Task("Generate-Commerce-Code")
    .IsDependentOn(Client.GenerateCommerceCode)
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
    .IsDependentOn(Client.RestoreNpmPackages)
    ;

Task("002-Build")
    .IsDependentOn(Sitecore.Tasks.GenerateCodeTaskName)
    .IsDependentOn(Client.GenerateCode)
    .IsDependentOn(Client.BuildCode)
    .IsDependentOn(Sitecore.Tasks.BuildServerCodeTaskName)
    ;

Task("003-Tests")
    .IsDependentOn(XUnitRunner.RunTests)
    .IsDependentOn(Client.RunTests)
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
    .IsDependentOn(Client.Publish)
    ;

Task("005-Publish-Back-Only")
    .IsDependentOn(Sitecore.Tasks.PublishFoundationTaskName)
    .IsDependentOn(Sitecore.Tasks.PublishFeatureTaskName)
    .IsDependentOn(Sitecore.Tasks.PublishProjectTaskName)
    ;

Task("006-Sync-Content")
    .IsDependentOn(Sitecore.Tasks.SyncAllUnicornItems)
    ;

Task("007-Update-SolrConfig")
    .IsDependentOn(Solr.AddSuggesterComponents)
    ;

Task("008-Build-Server-Code")
    .IsDependentOn(Server.BuildCodeTask)
    ;

Task("009-Create-SolrCores")
    .IsDependentOn(Solr.CreateCores)
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

Task("Build-Backend")
    .IsDependentOn("000-Clean")
    .IsDependentOn(Sitecore.Tasks.RestoreNuGetPackagesTask)
    .IsDependentOn(Sitecore.Tasks.RestoreNpmPackagesTaskName)
    .IsDependentOn(Sitecore.Tasks.GenerateCodeTaskName)
    .IsDependentOn(Sitecore.Tasks.BuildServerCodeTaskName)
    .IsDependentOn(Sitecore.Tasks.PrepareWebConfigTask)
    .IsDependentOn(Sitecore.Tasks.RunPackagesInstallationTask)
    .IsDependentOn("005-Publish-Back-Only")
    .IsDependentOn("006-Sync-Content")
    ;

Task("Build-Backend-With-Tests")
    .IsDependentOn("000-Clean")
    .IsDependentOn(Sitecore.Tasks.RestoreNuGetPackagesTask)
    .IsDependentOn(Sitecore.Tasks.RestoreNpmPackagesTaskName)
    .IsDependentOn(Sitecore.Tasks.GenerateCodeTaskName)
    .IsDependentOn(Sitecore.Tasks.BuildServerCodeTaskName)
    .IsDependentOn(Sitecore.Tasks.PrepareWebConfigTask)
    .IsDependentOn(Sitecore.Tasks.RunPackagesInstallationTask)
    .IsDependentOn(XUnitRunner.RunTests)
    .IsDependentOn(Sitecore.Tasks.MergeCoverageReportsTaskName)
    .IsDependentOn(Coverage.OutputCoverage)
    .IsDependentOn("005-Publish-Back-Only")
    .IsDependentOn("006-Sync-Content")
    ;

Task("Generate-Client-Models")
    .IsDependentOn(Client.GenerateModels)
    ;

Task("Initial-Deploy")
    .IsDependentOn("007-Update-SolrConfig")
    .IsDependentOn("009-Create-SolrCores")
    .IsDependentOn("Default")
    ;

Task("Create-Solr-Cores")
    .IsDependentOn("009-Create-SolrCores");

// //////////////////////////////////////////////////
// Execution
// //////////////////////////////////////////////////

RunTarget(Target);