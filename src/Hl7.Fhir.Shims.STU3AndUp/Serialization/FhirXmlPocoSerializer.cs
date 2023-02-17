#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification;
using System;

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


        /// <inheritdoc/>
        [Obsolete("Please use BaseFhirXmlPocoSerializer if you want to use multiple versions of FHIR")]
        public FhirXmlPocoSerializer(FhirRelease release) : base(release)
        {
        }
    }
}

#nullable restore
