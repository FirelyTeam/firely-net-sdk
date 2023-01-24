#nullable enable

using Hl7.Fhir.Model;

namespace Hl7.Fhir.Serialization
{
    /// <inheritdoc/>
    public class FhirXmlPocoDeserializer : BaseFhirXmlPocoDeserializer
    {
        /// <summary>
        /// Construct a new FHIR XML deserializer, based on the currently used FHIR version.
        /// </summary>
        public FhirXmlPocoDeserializer() : base(ModelInfo.ModelInspector)
        {
        }

        /// <summary>
        /// Construct a new FHIR XML deserializer, based on the currently used FHIR version.
        /// </summary>
        /// <param name="settings">Deserialization settings</param>
        public FhirXmlPocoDeserializer(FhirXmlPocoDeserializerSettings settings) : base(ModelInfo.ModelInspector, settings)
        {
        }
    }
}

#nullable restore
