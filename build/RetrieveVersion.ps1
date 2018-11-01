Push-Location $PsScriptRoot\..\src

$xml = [xml](get-content ..\src\fhir-net-api.props)

#Get the version 
[string]$version = $xml.Project.PropertyGroup.VersionPrefix

#Get the suffix
[string]$suffix = $xml.Project.PropertyGroup.VersionSuffix

#Setting task variable $CurrentVersion (used for VSTS) 
Write-Host "##vso[task.setvariable variable=CurrentVersion]$version"

#Setting task variable $CurrentVersion (used for VSTS) 
Write-Host "##vso[task.setvariable variable=CurrentSuffix]$suffix"

#go back to the original directory
Pop-Location