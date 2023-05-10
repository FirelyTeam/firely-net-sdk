using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;

[assembly: FhirModelAssembly(Hl7.Fhir.Specification.FhirRelease.STU3)]
[assembly: CqlModelAssembly("FHIR", "4.0.1", "http://hl7.org/fhir", typeof(TestPatient), nameof(TestPatient.BirthDateElement))]