#nullable enable

using Hl7.Fhir.Model;

namespace Hl7.Fhir.Serialization
{
    public class FhirXmlPocoSerializer : BaseFhirXmlPocoSerializer
    {

        /// <summary>
        /// Construct a new FHIR XML serializer, based on the currently used FHIR version.
        /// </summary>
        public FhirXmlPocoSerializer() : base(ModelInfo.ModelInspector.FhirRelease)
        {

        }
    }
}

#nullable restore
