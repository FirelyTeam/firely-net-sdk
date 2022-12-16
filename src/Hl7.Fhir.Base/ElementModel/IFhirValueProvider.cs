using Hl7.Fhir.Model;

namespace Hl7.Fhir.ElementModel
{
    public interface IFhirValueProvider
    {
        Base FhirValue { get; }
    }
}