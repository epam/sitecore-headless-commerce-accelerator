$xml=New-Object XML

$speFilePath = "C:\inetpub\wwwroot\App_Config\Include\Spe\Spe.config"
$xml.Load($speFilePath)

$restfulv1=$xml.SelectNodes("/configuration/sitecore/powershell/services/restfulv1")
$restfulv1.SetAttribute("enabled", "true")

$restfulv2=$xml.SelectNodes("/configuration/sitecore/powershell/services/restfulv2")
$restfulv2.SetAttribute("enabled", "true")

$remoting=$xml.SelectNodes("/configuration/sitecore/powershell/services/remoting")
$remoting.SetAttribute("enabled", "true")

$remotingAutorization = $remoting.SelectNodes("./authorization")
$addAllowRemoting = $xml.CreateElement("add")
$addAllowRemoting.SetAttribute("Permission", "Allow")
$addAllowRemoting.SetAttribute("IdentityType", "User")
$addAllowRemoting.SetAttribute("Identity", "sitecore\admin")
$remotingAutorization.AppendChild($addAllowRemoting)

$fileUpload=$xml.SelectNodes("/configuration/sitecore/powershell/services/fileUpload")
$fileUpload.SetAttribute("enabled", "true")

$mediaUpload=$xml.SelectNodes("/configuration/sitecore/powershell/services/mediaUpload")
$mediaUpload.SetAttribute("enabled", "true")

$xml.Save($speFilePath)