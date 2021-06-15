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

$ScriptDirectory = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent

try {
    . ("$ScriptDirectory\EnvironmentVariables.ps1") | Out-Null
}
catch {
		Write-Host "Error while loading supporting files" 
}

$ErrorActionPreference = "Stop";

if (-not (Test-Path $licenseFilePath)) {
    throw "Did not find $licenseFilePath"
}
if (-not (Test-Path $licenseFilePath -PathType Leaf)) {
    throw "$licenseFilePath is not a file"
}

############################################
# Install HCA_ScriptSupport module
############################################

Import-Module (Join-Path $PSScriptRoot "$moduleName") -DisableNameChecking -Global

############################################
# Install and Import SitecoreDockerTools
############################################

Import-SitecoreDockerTools

############################################
# Install SPE module
############################################

Install-SPE -outDirectory $speTempPath -moduleUrl $speRemoteModuleUrl

############################################
# Update the environment configuration file
############################################

$telerikKey = Get-SitecoreRandomString 128
$idCert =  (Get-SitecoreCertificateAsBase64String -DnsName "localhost" -Password (ConvertTo-SecureString -String $idPassword -Force -AsPlainText))
$idSecret =  Get-SitecoreRandomString 64 -DisallowSpecial
$xcIdSecret = Get-SitecoreRandomString 64 -DisallowSpecial
$reportingApiKey = Get-SitecoreRandomString 32

Write-Host "Updating .env file..." -ForegroundColor Green

$license = ConvertTo-CompressedBase64String -Path "$licenseFilePath"

#Find all the .env files and updated them
Get-ChildItem -Path "$ScriptDirectory\$envRootPath\" -Include '*.env' -Recurse | `
ForEach-Object {
    Write-Host "Updating [$_] file ..."

    $envContent = Get-Content -Path $_
    $envContent = $envContent -replace "SITECORE_LICENSE=.*", "SITECORE_LICENSE=$license"
    $envContent = $envContent -replace "SITECORE_IDSECRET=.*", "SITECORE_IDSECRET=$idSecret"
    $envContent = $envContent -replace "SITECORE_ID_CERTIFICATE=.*", "SITECORE_ID_CERTIFICATE=$idCert"
    $envContent = $envContent -replace "SITECORE_ID_CERTIFICATE_PASSWORD=.*", "SITECORE_ID_CERTIFICATE_PASSWORD=$idPassword"
    $envContent = $envContent -replace "TELERIK_ENCRYPTION_KEY=.*", "TELERIK_ENCRYPTION_KEY=$telerikKey"
    $envContent = $envContent -replace "REPORTING_API_KEY=.*", "REPORTING_API_KEY=$reportingApiKey"
    $envContent = $envContent -replace "XC_IDENTITY_COMMERCEENGINECONNECTCLIENT_CLIENTSECRET1=.*", "XC_IDENTITY_COMMERCEENGINECONNECTCLIENT_CLIENTSECRET1=$xcIdSecret"
    $envContent = $envContent -replace "XC_ENGINE_BRAINTREEENVIRONMENT=.*", "XC_ENGINE_BRAINTREEENVIRONMENT=$braintreeEnvironment"
    $envContent = $envContent -replace "XC_ENGINE_BRAINTREEMERCHANTID=.*", "XC_ENGINE_BRAINTREEMERCHANTID=$braintreeMerchantId"
    $envContent = $envContent -replace "XC_ENGINE_BRAINTREEPUBLICKEY=.*", "XC_ENGINE_BRAINTREEPUBLICKEY=$braintreePublicKey"
    $envContent = $envContent -replace "XC_ENGINE_BRAINTREEPRIVATEKEY=.*", "XC_ENGINE_BRAINTREEPRIVATEKEY=$braintreePrivateKey"
    $envContent = $envContent -replace "PATH_TO_DATA_FOLDER=.*", "PATH_TO_DATA_FOLDER=$ScriptDirectory\$pathToDataFolder"
    $envContent = $envContent -replace "PATH_TO_DEPLOY_FOLDER=.*", "PATH_TO_DEPLOY_FOLDER=$ScriptDirectory\$pathToDeployFolder"
    $envContent = $envContent -replace "PROJECT_SOURCE_FOLDER=.*", "PROJECT_SOURCE_FOLDER=$ScriptDirectory\..\src"
    $envContent = $envContent -replace "SITECORE_ADMIN_PASSWORD=.*", "SITECORE_ADMIN_PASSWORD=$sitecoreAdminPassword"
    $envContent = $envContent -replace "SQL_SA_PASSWORD=.*", "SQL_SA_PASSWORD=$sqlSaPassword"
    $envContent = $envContent -replace "CM_HOST=.*", "CM_HOST=$cmAlias"
    $envContent = $envContent -replace "ID_HOST=.*", "ID_HOST=$identityAlias"
    $envContent = $envContent -replace "AUTHORING_HOST=.*", "AUTHORING_HOST=$authoringAlias"
    $envContent = $envContent -replace "SHOPS_HOST=.*", "SHOPS_HOST=$shopsAlias"
    $envContent = $envContent -replace "MINIONS_HOST=.*", "MINIONS_HOST=$minionsAlias"
    $envContent = $envContent -replace "OPS_HOST=.*", "OPS_HOST=$opsAlias"
    $envContent = $envContent -replace "BIZFX_HOST=.*", "BIZFX_HOST=$bizfxAlias"

    if($use1909) {
        $envContent = $envContent -replace "ISOLATION=.*", "ISOLATION=process"
        $envContent = $envContent -replace "TRAEFIK_ISOLATION=.*", "TRAEFIK_ISOLATION=hyperv"
    }

    Set-Content -Path $_ -Value $envContent -Force

    Write-Host ".env file [$_] has been updated"
}

#######################################################
# Generate the required TLS reverse proxy certificates
#######################################################

if (-not (Test-Path "$ScriptDirectory\$pathToCertFolder" -PathType Container)) {
    New-Item -ItemType Directory -Path "$ScriptDirectory\$pathToCertFolder" | Out-Null
}

Make-Certificate -pathToCertFolder "$ScriptDirectory\$pathToCertFolder"  -certificates $hosts

################################
# Add Windows hosts file entries
################################

Write-Host "Adding Windows hosts file entries..." -ForegroundColor Green
$hosts | ForEach-Object { Add-HostsEntry $_ }

################################
# Update traefik configuration
################################

Write-Host "Updating traefik configuration..." -ForegroundColor Green
Config-Traefik -certFolder "$ScriptDirectory\$pathToCertFolder" -traefikConfigFolder "$ScriptDirectory\$pathToTraefikConfigFolder" | Out-Null

################################
# Create volume folders
################################

Confirm-VolumeFoldersExist -Path "$pathToDataFolder" -VolumeName "cm\wwwroot", "cm\logs", "cm\domains-shared", "cd\domains-shared", "engine\catalogs", "mssql-data", "solr-data" | Out-Null

################################
# Create deploy folder
################################

if(-not (Test-Path $pathToDeployFolder -PathType Container)) {
    Write-Host "Creating deploy folder..." -ForegroundColor Green
    New-Item -ItemType Directory -Path "$pathToDeployFolder\website" | Out-Null
}

################################
# Login to Azure
################################

Write-Host "Login to Azure" -fore Green
az login
az acr login --name scengxdocker

Write-Host "Done!" -ForegroundColor Green