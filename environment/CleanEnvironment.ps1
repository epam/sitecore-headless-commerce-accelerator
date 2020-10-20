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

############################################
#  Clean the environment configuration file
############################################

Write-Host "Try to clean .env file..." -ForegroundColor Green

#Find all the .env files and updated them
Get-ChildItem -Path "$ScriptDirectory\$envRootPath\" -Include '*.env' -Recurse | `
ForEach-Object {
    Write-Host "Cleaning [$_] file ..."

    $envContent = Get-Content -Path $_
    $envContent = $envContent -replace "SITECORE_LICENSE=.*", "SITECORE_LICENSE="
    $envContent = $envContent -replace "SITECORE_IDSECRET=.*", "SITECORE_IDSECRET="
    $envContent = $envContent -replace "SITECORE_ID_CERTIFICATE=.*", "SITECORE_ID_CERTIFICATE="
    $envContent = $envContent -replace "SITECORE_ID_CERTIFICATE_PASSWORD=.*", "SITECORE_ID_CERTIFICATE_PASSWORD="
    $envContent = $envContent -replace "TELERIK_ENCRYPTION_KEY=.*", "TELERIK_ENCRYPTION_KEY="
    $envContent = $envContent -replace "REPORTING_API_KEY=.*", "REPORTING_API_KEY="
    $envContent = $envContent -replace "XC_IDENTITY_COMMERCEENGINECONNECTCLIENT_CLIENTSECRET1=.*", "XC_IDENTITY_COMMERCEENGINECONNECTCLIENT_CLIENTSECRET1="
    $envContent = $envContent -replace "XC_ENGINE_BRAINTREEENVIRONMENT=.*", "XC_ENGINE_BRAINTREEENVIRONMENT="
    $envContent = $envContent -replace "XC_ENGINE_BRAINTREEMERCHANTID=.*", "XC_ENGINE_BRAINTREEMERCHANTID="
    $envContent = $envContent -replace "XC_ENGINE_BRAINTREEPUBLICKEY=.*", "XC_ENGINE_BRAINTREEPUBLICKEY="
    $envContent = $envContent -replace "XC_ENGINE_BRAINTREEPRIVATEKEY=.*", "XC_ENGINE_BRAINTREEPRIVATEKEY="
    $envContent = $envContent -replace "PATH_TO_DATA_FOLDER=.*", "PATH_TO_DATA_FOLDER="
    $envContent = $envContent -replace "PATH_TO_DEPLOY_FOLDER=.*", "PATH_TO_DEPLOY_FOLDER="
    $envContent = $envContent -replace "PROJECT_SOURCE_FOLDER=.*", "PROJECT_SOURCE_FOLDER="
    $envContent = $envContent -replace "SITECORE_ADMIN_PASSWORD=.*", "SITECORE_ADMIN_PASSWORD="
    $envContent = $envContent -replace "SQL_SA_PASSWORD=.*", "SQL_SA_PASSWORD="
    $envContent = $envContent -replace "CM_HOST=.*", "CM_HOST="
    $envContent = $envContent -replace "ID_HOST=.*", "ID_HOST="
    $envContent = $envContent -replace "AUTHORING_HOST=.*", "AUTHORING_HOST="
    $envContent = $envContent -replace "SHOPS_HOST=.*", "SHOPS_HOST="
    $envContent = $envContent -replace "MINIONS_HOST=.*", "MINIONS_HOST="
    $envContent = $envContent -replace "OPS_HOST=.*", "OPS_HOST="
    $envContent = $envContent -replace "BIZFX_HOST=.*", "BIZFX_HOST="

    Set-Content -Path $_ -Value $envContent -Force

    Write-Host ".env file [$_] has been cleaned"
}

##################################
# Clean up data folder
##################################

if (Test-Path "$pathToDataFolder" -PathType Container) {
    Write-Host "Cleaning up data folder..." -ForegroundColor Green
    Remove-Item -Path $pathToDataFolder -Force -Recurse
}

##################################
# Clean up deploy folder
##################################

if (Test-Path $pathToDeployFolder -PathType Container) {
    Write-Host "Cleaning up deploy folder..." -ForegroundColor Green
    Remove-Item -Path $pathToDeployFolder -Force -Recurse
}

###################################
# Remove Windows hosts file entries
###################################

Write-Host "Removing Windows hosts file entries..." -ForegroundColor Green
$hosts | ForEach-Object { & Remove-HostsEntry $_ }

##################################
# Remove HCA_ScriptSupport module
##################################

Write-Host "Removing $moduleName module" -fore Green

if (Get-Module -Name "$moduleName") {
    Remove-Module -Name "$moduleName"
} 
else {
    Write-Host "Module $moduleName does not exist"
}

Write-Host "Done!" -ForegroundColor Green