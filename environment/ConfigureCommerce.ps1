#    Copyright 2020 EPAM Systems, Inc.
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

$ScriptDirectory = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent

try {
    . ("$ScriptDirectory\EnvironmentVariables.ps1") | Out-Null
}
catch {
		Write-Host "Error while loading supporting files" 
}

$ErrorActionPreference = "Stop";

########################
# Initialize Environment
########################

Import-Module (Join-Path $PSScriptRoot "$moduleName") -DisableNameChecking

Write-Host 'Initialize Environment' -fore Green

Write-Host "Try to get Authorizition Token"
$token = Get-Token -UserName $userName -Password $idPassword -IdentityServiceUri "https://$identityAlias" -Verbose

Write-Host "Bootstrap SitecoreCommerce"
Bootstrap-SitecoreCommerce -Token $token -CommerceOpsServiceUri "https://$authoringAlias" -Verbose

Write-Host "Initialize Environment $SitecoreEnvironemnt"
Initialize-Environment -Token $token -SitecoreEnvironemnt $sitecoreEnvironemnt -CommerceOpsServiceUri "https://$authoringAlias" -Verbose

########################
# Select Catalog
########################

Write-Host "Select Habitat_Master Catalog"
Select-Catalog -UserName $userName -Password $idPassword -sitecoreInstanceUri "https://$cmAlias"

#######################
# Import Habitat images
#######################

Write-Host "Import Habitat Images"
Import-Images -UserName $userName -Password $idPassword -sitecoreInstanceUri "https://$cmAlias" -ImagesZipSource "C:\hca\HCA-commerce-images-1.zip"

#######################
# Import Catalog
#######################

Write-Host "Import Catalog"
Invoke-MultipartFormDataUpload  -Token "$token" -ImportFile "$ScriptDirectory\$pathToCatalogZipFile" -SitecoreEnvironemnt $sitecoreEnvironemnt -Uri "https://$authoringAlias/api/ImportCatalogs()"

Start-Sleep -s 15

#######################
# Import InventorySet
#######################

Write-Host "Import InventorySet"
Invoke-MultipartFormDataUpload  -Token "$token" -ImportFile "$ScriptDirectory\$pathToInventoryZipFile" -SitecoreEnvironemnt $sitecoreEnvironemnt -Uri "https://$authoringAlias/api/ImportInventorySets()"

########################
# Refresh Selected Catalog
########################

Unselect-Catalog -UserName $userName -Password $idPassword -sitecoreInstanceUri "https://$cmAlias"
Select-Catalog -UserName $userName -Password $idPassword -sitecoreInstanceUri "https://$cmAlias"

#######################
# Rebuild Indexes
#######################

Write-Host "Rebuild Indexes"
Rebuild-Index -UserName $userName -Password $idPassword -SitecoreInstanceUri "https://$cmAlias"
