#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification;
using System;

namespace Hl7.Fhir.Serialization
{
    public class FhirJsonPocoSerializer : BaseFhirJsonPocoSerializer
    {
        /// <inheritdoc/>
        [Obsolete("Please use FhirJsonPocoSerializer() if you are using a single version of FHIR or BaseFhirJsonPocoSerializer if you want to use multiple versions of FHIR")]
        public FhirJsonPocoSerializer(FhirRelease release) : base(release)
        {
        }

        /// <inheritdoc/>
        [Obsolete("Please use FhirJsonPocoSerializer() if you are using a single version of FHIR or BaseFhirJsonPocoSerializer if you want to use multiple versions of FHIR")]
        public FhirJsonPocoSerializer(FhirRelease release, FhirJsonPocoSerializerSettings settings) : base(release, settings)
        {
        }

        /// <summary>
        /// Construct a new FHIR Json serializer, based on the currently used FHIR version.
        /// </summary>
        public FhirJsonPocoSerializer() : base(ModelInfo.ModelInspector.FhirRelease)
        {
        }

        /// <summary>
        /// Construct a new FHIR Json serializer, based on the currently used FHIR version.
        /// </summary>
        /// <param name="settings">Serialization settings</param>
        public FhirJsonPocoSerializer(FhirJsonPocoSerializerSettings settings) : base(ModelInfo.ModelInspector.FhirRelease, settings)
        {
        }

    }
}

#nullable restore
