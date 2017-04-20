msbuild src\Hl7.Fhir.sln /t:clean /v:minimal
msbuild src\Hl7.Fhir.sln /t:restore /v:minimal
msbuild src\Hl7.Fhir.sln /t:build /v:minimal 
msbuild src\Hl7.Fhir.sln /t:vstest /v:minimal
