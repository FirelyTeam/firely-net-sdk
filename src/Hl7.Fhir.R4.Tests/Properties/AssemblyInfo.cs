using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;

[assembly: FhirModelAssembly]
[assembly: CqlModelAssembly("FHIR", "4.0.1", "http://hl7.org/fhir", typeof(Patient), nameof(Patient.BirthDateElement))]
