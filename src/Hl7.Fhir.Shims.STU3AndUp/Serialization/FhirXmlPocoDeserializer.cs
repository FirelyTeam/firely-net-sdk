#nullable enable

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using System;
using System.Reflection;

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

        /// <inheritdoc/>
        [Obsolete("Please use FhirXmlPocoDeserializer() if you are using a single version of BaseFhirXmlPocoDeserializer if you want to add custom model information")]
        public FhirXmlPocoDeserializer(Assembly assembly) : base(assembly)
        {
        }

        /// <inheritdoc/>
        [Obsolete("Please use FhirXmlPocoDeserializer() if you are using a single version of FHIR or BaseFhirXmlPocoDeserializer if you want to add custom model information")]
        public FhirXmlPocoDeserializer(ModelInspector inspector) : base(inspector)
        {
        }

        /// <inheritdoc/>
        [Obsolete("Please use FhirXmlPocoDeserializer() if you are using a single version of BaseFhirXmlPocoDeserializer if you want to add custom model information")]
        public FhirXmlPocoDeserializer(Assembly assembly, FhirXmlPocoDeserializerSettings settings) : base(assembly, settings)
        {
        }

        /// <inheritdoc/>
        [Obsolete("Please use FhirXmlPocoDeserializer() if you are using a single version of BaseFhirXmlPocoDeserializer if you want to add custom model information")]
        public FhirXmlPocoDeserializer(ModelInspector inspector, FhirXmlPocoDeserializerSettings settings) : base(inspector, settings)
        {
        }
    }
}

#nullable restore
