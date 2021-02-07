Push-Location $PsScriptRoot\..\src

if (Test-Path  ..\src\firely-net-sdk.props -PathType leaf)
{
     $propFile = "..\src\firely-net-sdk.props"
}
else
{
   $propFile = "..\src\fhir-net-api.props" # fallback to old name
}

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

#go back to the original directory
Pop-Location