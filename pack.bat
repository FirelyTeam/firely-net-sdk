msbuild src\Hl7.Fhir.sln /t:clean /p:configuration=release /v:minimal %*
msbuild src\Hl7.Fhir.sln /t:restore /p:configuration=release /v:minimal %*
msbuild src\Hl7.Fhir.sln /t:build,pack /p:configuration=release /v:minimal %*
