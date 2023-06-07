dotnet publish ../src/Hl7.Fhir.InterfaceApplier.CLI/Hl7.Fhir.InterfaceApplier.CLI.csproj /p:PublishProfile=../src/Hl7.Fhir.InterfaceApplier.CLI/Properties/PublishProfiles/BuildToolsFolderProfile.pubxml

$interfaceApplierCliPath = "./tools/InterfaceApplier.exe"
$ourcesDirectoryParam = "--SourcesDirectory=../src"
$fhirVersionsParam = "--FhirVersions=R4|R4B|R5|STU3"
& $interfaceApplierCliPath $ourcesDirectoryParam $fhirVersionsParam
