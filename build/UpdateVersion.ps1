#set the next two variables or get them from an Invoke-Expression, the rest should work automatically.
Param(
  [string]$newVersion,
  [string]$suffix
)

#Go to src (version is not relevant to test projects)
Push-Location $PsScriptRoot\..\src

If ([string]::IsNullOrEmpty($newVersion)) 
{
	# newVersion is not set, so getting it from the fhir-net-api.props
	$xml = [xml](get-content fhir-net-api.props)

	#Get the version (without suffix)
	$newVersion = $xml.Project.PropertyGroup.FhirApiVersion
	
	#Remove suffix
	$newVersion = $newVersion.split('-')[-2].trim()
}

#Replacing the version and suffix
(Get-Content fhir-net-api.props) |
    Foreach-Object { $_ `
        -replace "<FhirApiVersion>.*</FhirApiVersion>", "<FhirApiVersion>$newVersion-$suffix</FhirApiVersion>" `
        -replace "<SupportApiVersion>.*</SupportApiVersion>", "<SupportApiVersion>$newVersion-$suffix</SupportApiVersion>" `
    } |
    Set-Content fhir-net-api.props

#go back to the original directory
Pop-Location
