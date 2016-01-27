# ---------------------------------------------------------------------------
# Download the published content from the FHIR specification
# ---------------------------------------------------------------------------
cls

# Script to be run from build directory

$server = "http://hl7-fhir.github.io";
$curl = "..\tools\curl\curl.exe";
$srcdir = "..\src";

# Download the core profiles for code generation
&$curl -o "$srcdir\Hl7.Fhir.Core\Model\Source\expansions.xml" "$server/expansions.xml"
&$curl -o "$srcdir\Hl7.Fhir.Core\Model\Source\profiles-resources.xml" "$server/profiles-resources.xml"
&$curl -o "$srcdir\Hl7.Fhir.Core\Model\Source\profiles-types.xml" "$server/profiles-types.xml"
&$curl -o "$srcdir\Hl7.Fhir.Core\Model\Source\search-parameters.xml" "$server/search-parameters.xml"

&$curl -o "$srcdir\Hl7.Fhir.Core.Tests\TestData\careplan-example-f201-renal.xml" "$server/careplan-example-f201-renal.xml"
&$curl -o "$srcdir\Hl7.Fhir.Core.Tests\TestData\examples.zip" "$server/examples.zip"
&$curl -o "$srcdir\Hl7.Fhir.Core.Tests\TestData\examples-json.zip" "$server/examples-json.zip"
&$curl -o "$srcdir\Hl7.Fhir.Core.Tests\TestData\json-edge-cases.json" "$server/json-edge-cases.json"

&$curl -o "$srcdir\Hl7.Fhir.Specification\validation.xml.zip" "$server/validation.xml.zip"
&$curl -o "$srcdir\Hl7.Fhir.Specification.Tests\TestData\snapshot-test\profiles-others.xml" "$server/profiles-others.xml"
copy "$srcdir\Hl7.Fhir.Core\Model\Source\profiles-resources.xml" "$srcdir\Hl7.Fhir.Specification.Tests\TestData\snapshot-test"
copy "$srcdir\Hl7.Fhir.Core\Model\Source\profiles-types.xml" "$srcdir\Hl7.Fhir.Specification.Tests\TestData\snapshot-test"
