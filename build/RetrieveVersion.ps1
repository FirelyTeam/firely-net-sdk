#Go to Vonk src (version is not relevant to test projects)
Push-Location $PsScriptRoot\..\src

$xml = [xml](get-content ..\src\fhir-net-api.props)

#Get the version 
$fileVersion = $xml.Project.PropertyGroup.FhirApiVersion

# cut off the suffix
$fileVersion = $fileVersion.split('-')[-2].trim()

#Setting task variable $CurrentVersion (used for VSTS) 
Write-Host "##vso[task.setvariable variable=CurrentVersion]$fileVersion"

#go back to the original directory
Pop-Location