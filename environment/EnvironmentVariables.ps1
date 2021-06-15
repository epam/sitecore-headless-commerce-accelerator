#    Copyright 2021 EPAM Systems, Inc.
# 
#    Licensed under the Apache License, Version 2.0 (the "License");
#    you may not use this file except in compliance with the License.
#    You may obtain a copy of the License at
# 
#      http://www.apache.org/licenses/LICENSE-2.0
# 
#    Unless required by applicable law or agreed to in writing, software
#    distributed under the License is distributed on an "AS IS" BASIS,
#    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
#    See the License for the specific language governing permissions and
#    limitations under the License.

$licenseFilePath = "C:\<your_path>\license.xml"
$braintreeEnvironment = "sandbox"
$braintreeMerchantId = #"Your merchant id"
$braintreePublicKey = #"Your public key"
$braintreePrivateKey = #"Your private key"

# Define the user credentials
$userName = 'sitecore\admin'; 

# We do not need to use [SecureString] here since the value will be stored unencrypted in .env,
# and used only for transient local example environment.
[string]
$idPassword = "Password12345"
[string]
$sqlSaPassword = "Password12345"
[string]
$sitecoreAdminPassword = "Password12345"

$cmAlias = "hca.local"
$identityAlias = "xc0id.localhost"
$authoringAlias = "authoring.localhost"
$shopsAlias = "shops.localhost"
$minionsAlias = "minions.localhost"
$opsAlias = "ops.localhost"
$bizfxAlias = "bizfx.localhost"

$hosts = @($cmAlias, $identityAlias, $authoringAlias, $shopsAlias, $opsAlias, $bizfxAlias)

$sitecoreEnvironment = 'HabitatAuthoring'
$minionsEnvironment = 'HabitatMinions'
$speRemoteModuleUrl = 'https://github.com/SitecorePowerShell/Console/releases/download/6.1.1/SPE.Remoting-6.1.1.zip'
$speTempPath = "$env:TEMP\SPE.Remoting-6.1.1.zip"

$moduleName = "HCA_ScriptSupport"

$envRootPath = "docker\xc0"
$pathToDataFolder = "$envRootPath\data"
$pathToDeployFolder = "$envRootPath\deploy"
$pathToCertFolder = "$pathToDataFolder\traefik\certs"
$pathToTraefikConfigFolder = "$pathToDataFolder\traefik\config\dynamic"
$pathToCatalogZipFile = "$envRootPath\build\cm\assets\HCA-Catalogs.zip"
$pathToInventoryZipFile = "$envRootPath\build\cm\assets\HCA-InventorySets.zip"
$pathToShopZipFile = "$envRootPath\build\cm\assets\HCA-Shop.zip"
$pathToImagesZipFile = "$envRootPath\build\cm\assets\HCA-Images.zip"