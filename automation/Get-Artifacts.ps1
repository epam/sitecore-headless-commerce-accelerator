$ArtifactName = $args[0]
$ArtifactSource = Join-Path $env:Workspace s/src/$ArtifactName
$ArtifactDestination = Join-Path $env:Workspace s/output
$ArtifactDestinationLayer = Join-Path $ArtifactDestination $ArtifactName
$SubFoldersList = dir $ArtifactSource | Where-Object {$_.PSIsContainer} | ForEach-Object -Process {$_.FullName}

ForEach ($Folder in $SubFoldersList) {
	$ArtifactSourcePath = Join-Path $Folder code/obj/Debug/Package/PackageTmp
	robocopy $ArtifactSourcePath $ArtifactDestinationLayer /s /xo
	}
robocopy $ArtifactSource $ArtifactDestination/unicorn/$ArtifactName *.yml /s
