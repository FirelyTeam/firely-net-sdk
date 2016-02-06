# ---------------------------------------------------------------------------
# Download the published content from the FHIR specification
# ---------------------------------------------------------------------------
cls

# Script to be run from 'build' directory

$server = "http://hl7-fhir.github.io";
$baseDir = Resolve-Path ..
$srcdir = "$baseDir\src";

# Download a file from a URL and place it at the specified target path.
# This also has some basic capabilities to go through proxies without any additional configuration.
function PowerCurl($targetPath, $sourceUrl)
{
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


# Download the core profiles for code generation
PowerCurl "$srcdir\Hl7.Fhir.Core\Model\Source\expansions.xml" "$server/expansions.xml"
PowerCurl "$srcdir\Hl7.Fhir.Core\Model\Source\profiles-resources.xml" "$server/profiles-resources.xml"
PowerCurl "$srcdir\Hl7.Fhir.Core\Model\Source\profiles-types.xml" "$server/profiles-types.xml"
PowerCurl "$srcdir\Hl7.Fhir.Core\Model\Source\search-parameters.xml" "$server/search-parameters.xml"

PowerCurl "$srcdir\Hl7.Fhir.Core.Tests\TestData\careplan-example-f201-renal.xml" "$server/careplan-example-f201-renal.xml"
PowerCurl "$srcdir\Hl7.Fhir.Core.Tests\TestData\examples.zip" "$server/examples.zip"
PowerCurl "$srcdir\Hl7.Fhir.Core.Tests\TestData\examples-json.zip" "$server/examples-json.zip"
PowerCurl "$srcdir\Hl7.Fhir.Core.Tests\TestData\json-edge-cases.json" "$server/json-edge-cases.json"

PowerCurl "$srcdir\Hl7.Fhir.Specification\validation.xml.zip" "$server/validation.xml.zip"
PowerCurl "$srcdir\Hl7.Fhir.Specification.Tests\TestData\snapshot-test\profiles-others.xml" "$server/profiles-others.xml"
copy "$srcdir\Hl7.Fhir.Core\Model\Source\profiles-resources.xml" "$srcdir\Hl7.Fhir.Specification.Tests\TestData\snapshot-test"
copy "$srcdir\Hl7.Fhir.Core\Model\Source\profiles-types.xml" "$srcdir\Hl7.Fhir.Specification.Tests\TestData\snapshot-test"
