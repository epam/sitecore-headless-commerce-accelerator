![Homepage](https://user-images.githubusercontent.com/23119997/97112726-ed10d000-16f6-11eb-9c7b-b8fdc40ec1da.png)

<details>
  <summary>Click to see other pages</summary>
  
![Registration](https://user-images.githubusercontent.com/23119997/97112722-ebdfa300-16f6-11eb-83ad-5cccb49e6134.png)
![Cart](https://user-images.githubusercontent.com/23119997/97112723-ec783980-16f6-11eb-9eae-6070e43f988d.png)
![Checkout](https://user-images.githubusercontent.com/23119997/97112725-ed10d000-16f6-11eb-8852-7450c9867e43.png)
![PDP](https://user-images.githubusercontent.com/23119997/97112727-eda96680-16f6-11eb-833c-de01693379bc.png)

</details>

## Headless Commerce Accelerator for Sitecore Experience Commerce

* Enables a JSS-based eCommerce site that is headless, meaning it is decoupled from Sitecore servers
* Empowers productivity on day one through source code that saves teams weeks of development time
* Allows front-end JavaScript developers to quickly develop and adjust eCommerce website features independently from Sitecore back-end developers
* Uses REACT to accelerate the development process with code re-use
* Provides a professionally–branded, custom storefront UI
* Offers a proven alternative to the Sitecore Experience Accelerator (SXA)-based storefront

----
## Installation Guide

### Prerequisites
* Make sure these packages are installed on your PC:
* `Solr 8.1.1` (installing the service with NSSM is recommended)
* `Node 10.15.3 x64`
* `Java jre-8u191 x64`
* `Postman 6.6.1`
* `Redis 3.0.504`
* Machine should have at least 8, and 12-16 RAM for comfort work.

### Packages
Make sure you use these packages during installation:

* Sitecore Experience Platform 9.3 Initial Release ([download](https://dev.sitecore.net/Downloads/Sitecore_Experience_Platform/93/Sitecore_Experience_Platform_93_Initial_Release.aspx))
* Install Sitecore Commerce 9.3 Initial Release ([download](https://dev.sitecore.net/Downloads/Sitecore_Commerce/93/Sitecore_Experience_Commerce_93_Initial_Release.aspx))
* Sitecore JavaScript Services 13.0.0 (Sitecore JavaScript Services Server for Sitecore 9.3 XP) ([download](https://dev.sitecore.net/Downloads/Sitecore_JavaScript_Services/130/Sitecore_JavaScript_Services_1300.aspx))
* Sitecore PowerShell Extensions-5.1

----
## Steps

### License
Acquire a Sitecore license that authorizes the use of JSS (open it in Notepad and check for "Sitecore.JSS")

### Install Prerequisites
* Install the software specified in the prerequisites

### Install Sitecore Experience Platform 9.3 Initial Release
* Download & unpack the Packages for On Premises
* Follow the Installation Guide for XP Single (XP0).
* It’s recommended to set a standard password for the admin sitecore user (password: b).
* Following a successful installation the following sites should be created in IIS:
  * {sitename}_identityserver
  * {sitename}_XConnect
  * {sitename}
* It is strongly suggested that at this point a backup copy of the Sitecore installation be created (using SIM).

### Install Sitecore Commerce 9.3 Initial Release
* Make sure you have backed up the Sitecore installation prior to installing it, if the script fails you might need to roll everything back.
* It’s important to follow the installation guide to the letter, and pay close attention to the prerequisites, making sure everything specified is installed. Otherwise the script might fail later on and without an informative error message.
* Prior to running the script it needs to be tweaked:

----
### Configuration of the installation process.
* Make sure UserAccount's username/password meets the local username/password requirements, because it will be created if it doesn't exist yet.
* Make sure names and directories for all the required sites in the commerce deployment script correspond the ones that have been installed with sitecore installation.
* Add the CommerceServicesPostfix parameter to postfix these commerce sites in IIS:
* PowerShellExtensionsModuleFullPath & MergeToolFullPath will possibly need to be modified according to their location
* CommerceConnectModuleFullPath
* Replace Sitecore.BizFX.* with the actual folder name (like Sitecore.BizFX.2.0.3) to avoid conflicts with the SDK zip of the same name (the SitecoreBizFxServicesContentPath variable).
* Replace Sitecore.Commerce.Engine.* with the actual folder name (like Sitecore.Commerce.Engine.3.0.163.zip) to avoid conflicts with the SDK zip of the same name (the SitecoreCommerceEnginePath variable).
* Make sure the following ports are available (change if needed):

> CommerceOpsServicesPort   
> CommerceShopsServicesPort     
> CommerceAuthoringServicesPort     
> CommerceMinionsServicesPort

### Advanced configuration of the installation process:
* The **Sitecore Experience Accelerator (SXA)** was not working with Headless Commerce Accelerator at the time of writing, so it’s recommended that it be removed from installation. To do it, set the
value of the parameter **$skipInstallDefaultStorefront** to **true** and **$skipDeployStorefrontPackages** to **true**.
The script predefines the list of tasks to skip.

** Important to say the **Sitecore Experience Accelerator** module is not needed so it should not be downloaded either.

* SitecoreBizFx website & app pool's names are hardcoded. To change them, edit the following configs in SIF: **.\Configuration\Commerce\SitecoreBizFx\SitecoreBizFx.json**
* The next to last step RebuildIndexes might fail due to timeout, it’s suggested increasing the timeout by editing the following file: **.\Modules\SitecoreUtilityTasks\SitecoreUtilityTasks.psm1** and setting **–TimeoutSec** to something like 100000, escecially on a slow env.
* Consecutive messages like "Unable to start the site ..." are indicative of the script waiting for the site to come back from reboot, and are OK if the script recovers from them.
* Pro tip: should one of the steps fail, you don't have to start from scratch, just edit the appropriate json file(s) and remove from it (them) the previous, successfully completed steps and run the script again after fixing the problem that prevented it running the first time.

----
### Known issues of the sitecre commerce server installation

* `The service cannot accept control messages at this time. (Exception from HRESULT: 0x80070425)`
To resolve the issue follow the link to [StackOverflow](https://sitecore.stackexchange.com/questions/12870/the-service-cannot-accept-control-messages-at-this-time-while-installing-sitec). Before you start the installation process from the beginning you have to delete the folders created by the failed commerce server installation process (i.e. sitecoreCatalogItemsScope, sitecoreCustomersScope, sitecoreOrdersScope from solr_root_folder/server/solr).

----
### Bootstrapping the sitecore commerce server

To bootstrap the Commerce Server follow these instructions:

* For Headless Commerce Accelerator use the authoring server instance of the commerce server (default directory for the instances C:\inetpub\wwwroot).
* In the instance modify the wwwroot\data\Environments\PlugIn.Payments.Braintree.PolicySet-1.0.0.json - set the MerchantId, PublicKey and PrivateKey corresponding to the account you have set up with BrainTree.
* Disable SSL verification from the File > Settings > SSL certification verification in Postman. This settings needs to be turned off.
* Navigate to \CommerceAuthoring_Sc9\wwwroot\config.json and set the value for AntiForgeryEnabled to false.
* Bootstrap the commerce server, using Postman and executing [Sitecore Commerce SDK’s commands](https://doc.sitecore.com/developers/93/sitecore-experience-commerce/en/execute-sample-api-calls-in-postman.html). Open Postman, import the required script collections and than execute the required requests:
  * First the GetToken request (from Sitecore.Commerce.Engine.SDK_folder\postman\Shops\Authentication.postman_collection.json collection).
  * Then the Bootstrap Sitecore Commerce request (from Sitecore.Commerce.Engine.SDK_folder\postman\DevOps\SitecoreCommerce_DevOps.postman_collection.json collection).

----
### Install the Sitecore JavaScript Services 13.0.0

* Download & install the package
* Make sure the following exists in your web.config and if not then add it (system.webServer/handlers section):
`<add verb="*" path="sitecorejss_media.ashx" type="Sitecore.JavaScriptServices.Media.MediaRequestHandler, Sitecore.JavaScriptServices.Media" name="Sitecore.JavaScriptServices.Media.MediaRequestHandler" />`

----
### Building & deploying Headless Commerce Accelerator      
1. Fetch **Headless Commerce Accelerator** code base.
2. Copy Sitecore license to **./src** folder.
3. Local automation is implemented on the top of the [Cake tool](https://cakebuild.net/). Check [**src/build.cake**](src/build.cake) if `Sitecore/Parameters.InitParams` are correct for your installation.
4. For Visual Studio:
   * 17: change to **msBuildToolVersion: MSBuildToolVersion.VS2017**;
   * 19: change to **msBuildToolVersion: MSBuildToolVersion.VS2019**.
5. Change default IP address to VM address (if it's different) in
- .\src\publishsettings.targets: `192.168.50.4 -> VM IP`;
- .\src\build.cake: `192.168.50.4 -> VM IP`.
6. Create a symbol link with `unicorn-hca` name inside the **Root_Sitecore_Folder\App_Data** folder to the .\src folder (Root_Sitecore_Folder is the folder where Sitecore is installed, for ex. c:\inetpub\wwwroot\xp0.sc).
7. In IIS bind **hca.local** to the site, add **hca.local** entry for localhost to the hosts list (C:\Windows\System32\drivers\etc\hosts), HCA - is internal name of the Headless Commerce Accelerator.
8. Run `.\src\build.ps1` in PowerShell with administration privileges (script will restore **NuGet** packages, install **npm** dependencies and run deployment process). Next time cake scripts can be run from Visual Studio (install [Visual Studio Extension](https://marketplace.visualstudio.com/items?itemName=MadsKristensen.CommandTaskRunner)) or from Visual Studio Code (install [Cake Build](https://marketplace.visualstudio.com/items?itemName=cake-build.cake-vscode)) or from PowerShell.
9. In sitecore content editor modify **sitecore/Commerce/Catalog Management/Catalogs** item. Select **Habitat_Master** in the **Selected Catalogs** field.
10. Publish content tree & Rebuild all the indexes in sitecore indexing manager.

----
### Troubleshooting deployment 

In case of issues with the installation process you have several diagnostic options:

  * In **src/build.cake**, set **BuildConfiguration = "Debug"**;  
  * In **src/build.cake**, set **var publishingTargetDir = artifactsBuildDir** 
  * In **src/scripts/webpack/environments/production.js** make sure **mode="development"** and **minimize: false** 

----
## Front-End

Front-end code is processed using webpack. In order to start development server run following commands.
For more details see [readme.md](src/readme.md) in the 'src' folder.
```
cd ./src
npm start
```

----
## Known issues
1. If you have errors in Unicorn sync after run `.\src\build.ps1` you should log in to the sitecore then make a GET request on **http://{website}/unicorn.aspx?verb=Sync&log=null&skipTransparentConfigs=false**
