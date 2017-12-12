# ---------------------------------------------------------------------------
# Download the published content from the FHIR specification
# ---------------------------------------------------------------------------
cls

# Script to be run from 'build' directory

$server = "http://hl7.org/fhir";
$baseDir = Resolve-Path ..
$srcdir = "$baseDir\src";

# Download a file from a URL and place it at the specified target path.
# This also has some basic capabilities to go through proxies without any additional configuration.
function PowerCurl($targetPath, $sourceUrl)
{
	Write-Host "Downloading $sourceUrl to $targetPath"
	Try
	{
        $webclient = New-Object System.Net.WebClient
        $webclient.DownloadFile($sourceUrl, $targetPath)
	} Catch
	{
		Write-Host -ForegroundColor Red "$_ occurred while downloading $sourceUrl to $targetPath"
		$_ | fl * -Force
	}
}


# Download the DSTU2 core profiles for code generation
PowerCurl "$srcdir\Hl7.Fhir.Core\Model\Source-DSTU2\expansions.xml" "$server/DSTU2/expansions.xml"
PowerCurl "$srcdir\Hl7.Fhir.Core\Model\Source-DSTU2\profiles-resources.xml" "$server/DSTU2/profiles-resources.xml"
PowerCurl "$srcdir\Hl7.Fhir.Core\Model\Source-DSTU2\profiles-types.xml" "$server/DSTU2/profiles-types.xml"
PowerCurl "$srcdir\Hl7.Fhir.Core\Model\Source-DSTU2\search-parameters.xml" "$server/DSTU2/search-parameters.xml"

# Download the STU3 core profiles for code generation
PowerCurl "$srcdir\Hl7.Fhir.Core\Model\Source-STU3\expansions.xml" "$server/STU3/expansions.xml"
PowerCurl "$srcdir\Hl7.Fhir.Core\Model\Source-STU3\profiles-resources.xml" "$server/STU3/profiles-resources.xml"
PowerCurl "$srcdir\Hl7.Fhir.Core\Model\Source-STU3\profiles-types.xml" "$server/STU3/profiles-types.xml"
PowerCurl "$srcdir\Hl7.Fhir.Core\Model\Source-STU3\search-parameters.xml" "$server/STU3/search-parameters.xml"

PowerCurl "$srcdir\Hl7.Fhir.Core.Tests\TestData\careplan-example-f201-renal.xml" "$server/DSTU2/careplan-example-f201-renal.xml"
PowerCurl "$srcdir\Hl7.Fhir.Core.Tests\TestData\examples.zip" "$server/DSTU2/examples.zip"
PowerCurl "$srcdir\Hl7.Fhir.Core.Tests\TestData\examples-json.zip" "$server/DSTU2/examples-json.zip"
PowerCurl "$srcdir\Hl7.Fhir.Core.Tests\TestData\json-edge-cases.json" "$server/DSTU2/json-edge-cases.json"

PowerCurl "$srcdir\Hl7.Fhir.Specification\validation.xml.zip" "$server/DSTU2/validation.xml.zip"
PowerCurl "$srcdir\Hl7.Fhir.Specification.Tests\TestData\snapshot-test\profiles-others.xml" "$server/DSTU2/profiles-others.xml"
copy "$srcdir\Hl7.Fhir.Core\Model\Source-DSTU2\profiles-resources.xml" "$srcdir\Hl7.Fhir.Specification.Tests\TestData\snapshot-test"
copy "$srcdir\Hl7.Fhir.Core\Model\Source-DSTU2\profiles-types.xml" "$srcdir\Hl7.Fhir.Specification.Tests\TestData\snapshot-test"
