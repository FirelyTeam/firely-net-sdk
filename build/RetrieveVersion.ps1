Param(
  [Parameter(Mandatory=$true)] [string]$propFile
)

$xml = [xml](get-content $propFile)

#Get the version 
[string]$version = $xml.Project.PropertyGroup.VersionPrefix
$version = $version.Trim()

#Get the suffix
[string]$suffix = $xml.Project.PropertyGroup.VersionSuffix
$suffix = $suffix.Trim()

#Setting task variable $CurrentVersion (used for VSTS) 
Write-Host "##vso[task.setvariable variable=CurrentVersion]$version"

#Setting task variable $CurrentVersion (used for VSTS) 
Write-Host "##vso[task.setvariable variable=CurrentSuffix]$suffix"