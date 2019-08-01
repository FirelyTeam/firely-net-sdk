using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Validation.Impl;

namespace Hl7.Fhir.Specification.Schema
{
    internal static class FixedConfigurator
    {
        public static Fixed ToValidatable(this ElementDefinition elementDefinition)
        {
            if (elementDefinition.Fixed != null)
            {
                return new Fixed(elementDefinition.Fixed.ToTypedElement());
            }
            return null;
        }
    }
}
