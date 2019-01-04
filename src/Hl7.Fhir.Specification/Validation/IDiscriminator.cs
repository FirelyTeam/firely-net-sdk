using Hl7.Fhir.ElementModel;

namespace Hl7.Fhir.Validation
{
    internal interface IDiscriminator
    {
        bool Matches(ITypedElement candidate);
    }
}