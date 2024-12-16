#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification;
using System;

namespace Hl7.Fhir.Serialization
{
    public class FhirJsonPocoSerializer : BaseFhirJsonPocoSerializer
    {
        /// <summary>
        /// Construct a new FHIR Json serializer, based on the currently used FHIR version.
        /// </summary>
        public FhirJsonPocoSerializer() : base(ModelInfo.ModelInspector)
        {
        }

        /// <summary>
        /// Construct a new FHIR Json serializer, based on the currently used FHIR version.
        /// </summary>
        /// <param name="settings">Serialization settings</param>
        public FhirJsonPocoSerializer(FhirJsonPocoSerializerSettings settings) : base(ModelInfo.ModelInspector, settings)
        {
        }
    }
}

#nullable restore