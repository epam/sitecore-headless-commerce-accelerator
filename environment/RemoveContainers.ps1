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

##################################
# Stop and Remove containers
##################################

Write-Host "Removing containers..." -ForegroundColor Green

Push-Location "$envRootPath"
try {
    docker-compose down
}
finally{
    Pop-Location
}

##################################
# Clean up data folder
##################################

if (Test-Path "$pathToDataFolder" -PathType Container) {
    Write-Host "Cleaning up mssql and solr folders..." -ForegroundColor Green
    Remove-Item -Path "$pathToDataFolder\mssql-data\*" -Force -Recurse
    Remove-Item -Path "$pathToDataFolder\solr-data\*" -Force -Recurse
}