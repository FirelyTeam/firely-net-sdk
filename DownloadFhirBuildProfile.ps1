# ---------------------------------------------------------------------------
# Download the published content from the FHIR specification
# ---------------------------------------------------------------------------
cls

# Expect that you should run this script from a path like this one:
# C:\src\FHIR\fhir-net-api-DSTU2 - Merge\src\

$server = "http://hl7-fhir.github.io";

# Download the core profiles for code generation
curl -o ".\src\Hl7.Fhir.Core\Model\Source\expansions.xml" "$server/expansions.xml"
curl -o ".\src\Hl7.Fhir.Core\Model\Source\profiles-resources.xml" "$server/profiles-resources.xml"
curl -o ".\src\Hl7.Fhir.Core\Model\Source\profiles-types.xml" "$server/profiles-types.xml"
curl -o ".\src\Hl7.Fhir.Core\Model\Source\search-parameters.xml" "$server/search-parameters.xml"

curl -o ".\src\Hl7.Fhir.Core.Tests\TestData\careplan-example-f201-renal.xml" "$server/careplan-example-f201-renal.xml"
curl -o ".\src\Hl7.Fhir.Core.Tests\TestData\examples.zip" "$server/examples.zip"
curl -o ".\src\Hl7.Fhir.Core.Tests\TestData\examples-json.zip" "$server/examples-json.zip"
curl -o ".\src\Hl7.Fhir.Core.Tests\TestData\json-edge-cases.json" "$server/json-edge-cases.json"

curl -o ".\src\Hl7.Fhir.Specification\validation.xml.zip" "$server/validation.xml.zip"
curl -o ".\src\Hl7.Fhir.Specification.Tests\TestData\snapshot-test\profiles-others.xml" "$server/profiles-others.xml"
copy ".\src\Hl7.Fhir.Core\Model\Source\profiles-resources.xml" ".\src\Hl7.Fhir.Specification.Tests\TestData\snapshot-test"
copy ".\src\Hl7.Fhir.Core\Model\Source\profiles-types.xml" ".\src\Hl7.Fhir.Specification.Tests\TestData\snapshot-test"

#curl -o ".\Hl7.Fhir.Core.Tests\TestData\TestPatient.json" "$server/TestPatient.json"
#curl -o ".\Hl7.Fhir.Core.Tests\TestData\TestPatient.xml" "$server/TestPatient.xml"
