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

Function Install-SPE {
    param(
        [Parameter(Mandatory)]
        [string] $outDirectory,
        [Parameter(Mandatory)]
        [string] $moduleUrl
    )

    Write-Host "Installing SPE Remote module to host machine" -fore Green

    $userPath = $env:PSModulePath.split(";")[0]

    if ( -not (Test-Path -Path "$userPath\SPE") ) {
        
        if(-not (Test-Path -Path $outDirectory -PathType Leaf)){
            Write-Host 'Download SPE Remote module'
        
            Invoke-WebRequest -Uri $moduleUrl -OutFile $outDirectory
        }

        Write-Host "Extracting $name to $destination..."
        
        Expand-Archive $outDirectory -DestinationPath $userPath -Force
        Write-Host "Done" -fore Gray
    }
    else {
        Write-Host "SPE module already exists. Skipping."
    }
}

Function Confirm-VolumeFoldersExist {
    param (
        [string] $Path,
        [string[]] $VolumeName
    )
    Write-Host "Verifying Folders for Commerce Engine volumes exist in [$Path]" -ForegroundColor Green

    $VolumeName |
    ForEach-Object { 
        if (-Not (Test-Path (Join-Path $Path $_))) {
            Write-Verbose "Creating folder [$_]"
            New-Item -Path $Path -Name $_ -ItemType Directory
        }
    }
}

Function Make-Certificate {
	param(
	[string] $pathToCertFolder,
	[string[]] $certificates
	)
	
    Push-Location $pathToCertFolder
    try {
        $mkcert = ".\mkcert.exe"
        if ($null -ne (Get-Command mkcert.exe -ErrorAction SilentlyContinue)) {
            # mkcert installed in PATH
            $mkcert = "mkcert"
        } elseif (-not (Test-Path $mkcert)) {
            Write-Host "Downloading and installing mkcert certificate tool..." -ForegroundColor Green 
            Invoke-WebRequest "https://github.com/FiloSottile/mkcert/releases/download/v1.4.1/mkcert-v1.4.1-windows-amd64.exe" -UseBasicParsing -OutFile mkcert.exe
            if ((Get-FileHash mkcert.exe).Hash -ne "1BE92F598145F61CA67DD9F5C687DFEC17953548D013715FF54067B34D7C3246") {
                Remove-Item mkcert.exe -Force
                throw "Invalid mkcert.exe file"
            }
        }
        Write-Host "Generating Traefik TLS certificates..." -ForegroundColor Green
        & $mkcert -install
		$certificates | ForEach-Object { if ( -not (Test-Path $pathToCertFolder\$_.crt) ){ & $mkcert -cert-file $pathToCertFolder\$_.crt -key-file $pathToCertFolder\$_.key $_ } }
    }
    catch {
        Write-Host "An error occurred while attempting to generate TLS certificates: $_" -ForegroundColor Red
    }
    finally {
        Pop-Location
    }
}

Function Config-Traefik {
	param(
		[string] $certFolder,
		[string] $traefikConfigFolder
		)

	if (Test-Path $traefikConfigFolder -PathType Container) {
		Remove-Item  -Path $traefikConfigFolder -Force -Recurse
	}

	New-Item -ItemType Directory -Path $traefikConfigFolder | Out-Null
	New-Item -Path $traefikConfigFolder -Name "certs_config.yaml" -ItemType "file"

	If (-not(Get-InstalledModule powershell-yaml -ErrorAction silentlycontinue)) {
		Install-Module -Name powershell-yaml -Force -Repository PSGallery -Scope CurrentUser
	  }
	
	$certificates = Get-ChildItem -Path "$certFolder\*" -Include ('*.crt') -Name
	
	$certificateObjects = @()
	foreach ($certificate in $certificates) {
	 	$certificateObject = New-Object psobject -Property @{
			 certFile = "C:\etc\traefik\certs\$certificate"
			 keyFile  = "C:\etc\traefik\certs\" + [System.IO.Path]::GetFileNameWithoutExtension($certificate) + ".key"
		}

		$certificateObjects += $certificateObject
	} 

	$tlsObject = New-Object -TypeName psobject
	$tlsObject | Add-Member -MemberType NoteProperty -Name certificates -Value $certificateObjects
		
	$tls = New-Object -TypeName psobject
	$tls | Add-Member -MemberType NoteProperty -Name "tls" -Value $tlsObject

	$result = ConvertTo-Yaml -Data $tls
	Add-Content -Path "$traefikConfigFolder\certs_config.yaml" -Value $result
}

Function Import-SitecoreDockerTools {
	# Check for Sitecore Gallery
	Import-Module PowerShellGet
	
	$SitecoreGallery = Get-PSRepository | Where-Object { $_.SourceLocation -eq "https://sitecore.myget.org/F/sc-powershell/api/v2" }
	
	if (-not $SitecoreGallery) {
		Write-Host "Adding Sitecore PowerShell Gallery..." -ForegroundColor Green 
		Register-PSRepository -Name SitecoreGallery -SourceLocation https://sitecore.myget.org/F/sc-powershell/api/v2 -InstallationPolicy Trusted
		$SitecoreGallery = Get-PSRepository -Name SitecoreGallery
	}
	
	# Install and Import SitecoreDockerTools 
	$dockerToolsVersion = "10.0.5"
	
	Remove-Module SitecoreDockerTools -ErrorAction SilentlyContinue
	
	if (-not (Get-InstalledModule -Name SitecoreDockerTools -RequiredVersion $dockerToolsVersion -ErrorAction SilentlyContinue)) {
		Write-Host "Installing SitecoreDockerTools..." -ForegroundColor Green
		Install-Module SitecoreDockerTools -RequiredVersion $dockerToolsVersion -Scope CurrentUser -Repository $SitecoreGallery.Name
	}
	
	Write-Host "Importing SitecoreDockerTools..." -ForegroundColor Green
	Import-Module SitecoreDockerTools -RequiredVersion $dockerToolsVersion
}

Function Import-Images {
    [CmdletBinding()]
    PARAM(
        [string][parameter(Mandatory = $true)]$userName,
        [string][parameter(Mandatory = $true)]$password,
        [string][parameter(Mandatory = $true)]$sitecoreInstanceUri,
        [string][parameter(Mandatory = $true)]$imagesZipSource
    )

    $session = New-ScriptSession -Username $userName -Password $password -ConnectionUri $sitecoreInstanceUri
    
    try {
        $data = @{ 'path' = "$imagesZipSource"}
 
       Invoke-RemoteScript -Session $session -ScriptBlock { 
            Install-Package -Path $(($using:data).path) -InstallMode Overwrite -MergeMode Clear
        }
    }
    catch {
        $PSCmdlet.ThrowTerminatingError($_)
    }
    finally{
        Stop-ScriptSession -Session $session
    }
}

Function Rebuild-Index {
    [CmdletBinding()]
    PARAM(
        [string][parameter(Mandatory = $true)]$UserName,
        [string][parameter(Mandatory = $true)]$Password,
        [string][parameter(Mandatory = $true)]$SitecoreInstanceUri
    )

    $session = New-ScriptSession -Username $UserName -Password $Password -ConnectionUri $SitecoreInstanceUri -UseDefaultCredentials
    
    try { 
        Invoke-RemoteScript -Session $session -ScriptBlock {
            . ([scriptblock]::Create('Using namespace Sitecore.ContentSearch'))
            
            $indexes = [ContentSearchManager]::Indexes | Where-Object {$_.Name -eq 'sitecore_master_index' -or $_.Name -eq 'sitecore_web_index'}

            foreach($index in $indexes){
                Write-Host "Re-indexing started " $index.Name
                $index.Rebuild([IndexingOptions]::ForcedIndexing);
                Write-Host "Re-indexing Finished" $index.Name    
            }
        }
    }
    catch {
        $PSCmdlet.ThrowTerminatingError($_)
    }
    finally{
        Stop-ScriptSession -Session $session
    }
}

################## Rest API calls ###########################

# $token = Get-Token -UserName 'sitecore\admin' -Password 'b' -IdentityServiceUri 'https://identity'
Function Get-Token {
    [CmdletBinding()]
    PARAM(
        [string][parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]$UserName,
        [string][parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]$Password,
        [string][parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]$IdentityServiceUri
    )

    $body = @{
        username   = $UserName
        password   = $Password
        grant_type = 'password'
        client_id  = 'postman-api'
        scope      = 'openid EngineAPI postman_api'
    };

    $headers = @{
        "Cache-Control" = "no-cache"
        "Content-Type"  = "application/x-www-form-urlencoded"
        "Accept"        = "application/json"
    }

    try {
        $response = Invoke-RestMethod "$IdentityServiceUri/connect/token" -Method Post -Body $body -Headers $headers;
        $token = $response.access_token;
        return $token;
    }
    catch {
        $PSCmdlet.ThrowTerminatingError($_)
    }
}

# BootstrapSitecoreCommerce -Token $token -CommerceOpsServiceUri 'http://commerce-ops'
Function Bootstrap-SitecoreCommerce {
    [CmdletBinding()]
    PARAM
    (
        [string][parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]$Token,
        [string][parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]$CommerceOpsServiceUri
    )

    $body = @{};

    $headers = @{
        "Cache-Control"   = "no-cache"
        "Content-Type"    = "application/x-www-form-urlencoded"
        "Accept"          = "application/json"
        "Accept-Encoding" = "gzip, deflate, br"
        "Authorization"   = "Bearer $Token"
    }

    try {
        Invoke-RestMethod "$CommerceOpsServiceUri/commerceops/Bootstrap()" -Method Post -Body $body -Headers $headers
    }
    catch {
        $PSCmdlet.ThrowTerminatingError($_)
    }
}

# InitializeEnvironment -Token $token -SitecoreEnvironemnt 'HabitatAuthoring' -CommerceOpsServiceUri 'http://commerce-ops'
Function Initialize-Environment {
    [CmdletBinding()]
    PARAM
    (
        [string][parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]$token,
        [string][parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]$sitecoreEnvironemnt,
        [string][parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]$commerceOpsServiceUri
    )

    $body = @{ 
        environment = $sitecoreEnvironemnt 
        sampleData = $true }  | ConvertTo-Json;

    $headers = @{
        "Cache-Control"   = "no-cache"
        "Content-Type"    = "application/json"
        "Accept"          = "*/*"
        "Accept-Encoding" = "gzip, deflate, br"
        "Authorization"   = "Bearer $Token"
    }

    try {
        Invoke-RestMethod "$commerceOpsServiceUri/commerceops/InitializeEnvironment()" -Method Post -Body $body -Headers $headers
    }
    catch {
        $PSCmdlet.ThrowTerminatingError($_)
    }
}

Function Select-Catalog {
    [CmdletBinding()]
    PARAM(
        [string][parameter(Mandatory = $true)]$userName,
        [string][parameter(Mandatory = $true)]$password,
        [string][parameter(Mandatory = $true)]$sitecoreInstanceUri
    )

    $session = New-ScriptSession -Username $userName -Password $password -ConnectionUri $sitecoreInstanceUri -UseDefaultCredentials
    
    try {
        Invoke-RemoteScript -Session $session -ScriptBlock {
            $catalogItem = Get-Item  "master:\Commerce\Catalog Management\Catalogs"
            $catalogItem.Editing.BeginEdit()
            $catalogItem.'Selected Catalogs' = "Habitat_Master"
            $catalogItem.Editing.EndEdit()
        }
    }
    catch {
        $PSCmdlet.ThrowTerminatingError($_)
    }
    finally{
        Stop-ScriptSession -Session $session
    }
}

Function Unselect-Catalog {
    [CmdletBinding()]
    PARAM(
        [string][parameter(Mandatory = $true)]$userName,
        [string][parameter(Mandatory = $true)]$password,
        [string][parameter(Mandatory = $true)]$sitecoreInstanceUri
    )

    $session = New-ScriptSession -Username $userName -Password $password -ConnectionUri $sitecoreInstanceUri -UseDefaultCredentials
    
    try {
        Invoke-RemoteScript -Session $session -ScriptBlock {
            $catalogItem = Get-Item  "master:\Commerce\Catalog Management\Catalogs"
            $catalogItem.Editing.BeginEdit()
            $catalogItem.'Selected Catalogs' = ""
            $catalogItem.Editing.EndEdit()
        }
    }
    catch {
        $PSCmdlet.ThrowTerminatingError($_)
    }
    finally{
        Stop-ScriptSession -Session $session
    }
}

#Invoke-MultipartFormDataUpload  -Token "$token" -ImportFile "$ScriptPath\HCA-Catalogs.zip" -SitecoreEnvironemnt  'HabitatAuthoring' -Uri "$CommerceAuthoringService/api/ImportCatalogs()"
Function Invoke-MultipartFormDataUpload {
    [CmdletBinding()]
    PARAM
    (
        [string][parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]$ImportFile,
        [Uri][parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]$Uri,
        [string][parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]$Token,        
        [string][parameter(Mandatory = $true)][ValidateNotNullOrEmpty()]$SitecoreEnvironemnt
    )
    BEGIN {
        if (-not (Test-Path $ImportFile)) {
            $errorMessage = ("File {0} missing or unable to read." -f $ImportFile)
            $exception = New-Object System.Exception $errorMessage
            $errorRecord = New-Object System.Management.Automation.ErrorRecord $exception, 'MultipartFormDataUpload', ([System.Management.Automation.ErrorCategory]::InvalidArgument), $InFile
            $PSCmdlet.ThrowTerminatingError($errorRecord)
        }
    }
    PROCESS {
        Add-Type -AssemblyName System.Net.Http

        $httpClient = New-Object System.Net.Http.Httpclient
        $httpClient.DefaultRequestHeaders.Add('Accept', '*/*')
        $httpClient.DefaultRequestHeaders.Add('Accept-Encoding', 'gzip, deflate, br')
        $httpClient.DefaultRequestHeaders.Add('ShopName', 'CommerceEngineDefaultStorefront')
        $httpClient.DefaultRequestHeaders.Add('ShopperId', 'ShopperId')
        $httpClient.DefaultRequestHeaders.Add('Language', 'en-US')
        $httpClient.DefaultRequestHeaders.Add('Currency', 'USD')
        $httpClient.DefaultRequestHeaders.Add('Environment', "$SitecoreEnvironemnt")
        $httpClient.DefaultRequestHeaders.Add('GeoLocation', 'IpAddress=1.0.0.0')
        $httpClient.DefaultRequestHeaders.Add('CustomerId', '{{CustomerId}}')
        $httpClient.DefaultRequestHeaders.Add('PolicyKeys', 'IgnoreIndexDeletedSitecoreItem|IgnoreAddEntityToIndexList|IgnoreIndexUpdatedSitecoreItem|IgnoreLocalizeEntity')
        $httpClient.DefaultRequestHeaders.Add('Authorization', "Bearer $Token")

        $bodyContent = New-Object System.Net.Http.MultipartFormDataContent

        $fileName = (Split-Path $ImportFile -leaf)
        try {
            $packageFileStream = New-Object System.IO.FileStream @($ImportFile, [System.IO.FileMode]::Open)

            $streamContent = New-Object System.Net.Http.StreamContent $packageFileStream
            $streamContent.Headers.Add("Content-Disposition", "form-data; name=`"importFile`"; filename=`"$fileName`"")
            $streamContent.Headers.Add("Content-Type", "application/zip")
            $bodyContent.Add($streamContent, 'importFile', $fileName)

            $stringContent = New-Object System.Net.Http.StringContent "replace"
            $bodyContent.Add($stringContent, "mode")

            $response = $httpClient.PostAsync($Uri, $bodyContent).Result

            if (!$response.IsSuccessStatusCode) {
                $responseBody = $response.Content.ReadAsStringAsync().Result
                $errorMessage = "Status code {0}. Reason {1}. Server reported the following message: {2}." -f $response.StatusCode, $response.ReasonPhrase, $responseBody

                throw [System.Net.Http.HttpRequestException] $errorMessage
            }
            else {
                Write-Host "Response Status: " $response.StatusCode
            }

        }
        catch [Exception] {
            $PSCmdlet.ThrowTerminatingError($_)
        }
        finally {
            if ($null -ne $packageFileStream) {
                $packageFileStream.Close();
            }

            if ($null -ne $httpClient) {
                $httpClient.Dispose()
            }

            if ($null -ne $response) {
                $response.Dispose()
            }
        }
    }
    END { }
}
