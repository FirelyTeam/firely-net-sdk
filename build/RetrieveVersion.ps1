Push-Location $PsScriptRoot\..\src

$xml = [xml](get-content ..\src\fhir-net-api.props)

#Get the version 
[string]$fileVersion = $xml.Project.PropertyGroup.VersionPrefix


#Setting task variable $CurrentVersion (used for VSTS) 
Write-Host "##vso[task.setvariable variable=CurrentVersion]$fileVersion"

#go back to the original directory
Pop-Location