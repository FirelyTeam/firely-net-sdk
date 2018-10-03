$xml = [xml](get-content ..\src\fhir-net-api.props)

#Get the version 
[string]$fileVersion = $xml.Project.PropertyGroup.FhirApiVersion

# cut off the suffix
$fileVersion = $fileVersion.Split("-")[-2].Trim()

#Setting task variable $CurrentVersion (used for VSTS) 
Write-Host "##vso[task.setvariable variable=CurrentVersion]$fileVersion"