Param(
  [Parameter(Mandatory=$true)] [string]$propFile
)

Write-Host "##[section]Retrieving version information from $propFile"

$xml = [xml](get-content $propFile)

#Get the version 
[string]$version = $xml.Project.PropertyGroup.VersionPrefix
$version = $version.Trim()

#Get the suffix
[string]$suffix = $xml.Project.PropertyGroup.VersionSuffix
$suffix = $suffix.Trim()

Write-Host "##[command]Found version: $version"
if (![string]::IsNullOrEmpty($suffix)) {
    Write-Host "##[command]Found suffix: $suffix"
} else {
    Write-Host "##[command]No version suffix found"
}

#Setting task variable $CurrentVersion (used for VSTS) 
Write-Host "##vso[task.setvariable variable=CurrentVersion]$version"

#Setting task variable $CurrentVersion (used for VSTS) 
Write-Host "##vso[task.setvariable variable=CurrentSuffix]$suffix"

Write-Host "##[section]Version retrieval completed successfully!"