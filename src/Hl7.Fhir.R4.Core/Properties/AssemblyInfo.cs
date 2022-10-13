using Hl7.Fhir.Introspection;
using System;

// This assembly is not fully CLSCompliant, but this triggers compiler warnings to avoid the issue as described here, when mixing C# and VB 
// https://msdn.microsoft.com/en-us/library/ms235408(v=vs.90).aspx 
[assembly: CLSCompliant(true)]
[assembly: FhirModelAssembly(Hl7.Fhir.Specification.FhirRelease.R4)]