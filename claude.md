# Firely .NET SDK notes

- If API compatibility checks fail, regenerate `/home/runner/work/firely-net-sdk/firely-net-sdk/src/Hl7.Fhir.Base/CompatibilitySuppressions.xml` with:
  `dotnet pack /home/runner/work/firely-net-sdk/firely-net-sdk/src/Hl7.Fhir.Base/Hl7.Fhir.Base.csproj -c Release /p:ApiCompatGenerateSuppressionFile=true`
