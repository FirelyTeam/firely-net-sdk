#nullable enable

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using System;
using System.Reflection;

namespace Hl7.Fhir.Serialization
{
    public class FhirJsonPocoDeserializer : BaseFhirJsonPocoDeserializer
    {
        /// <summary>
        /// Construct a new FHIR Json deserializer, based on the currently used FHIR version.
        /// </summary>
        public FhirJsonPocoDeserializer() : base(ModelInfo.ModelInspector) { }

        /// <summary>
        /// Construct a new FHIR Json deserializer, based on the currently used FHIR version.
        /// </summary>
        /// <param name="settings">Deserialization settings</param>
        public FhirJsonPocoDeserializer(FhirJsonPocoDeserializerSettings settings) : base(ModelInfo.ModelInspector, settings) { }

        ///<inheritdoc/>
        [Obsolete("Please use FhirJsonPocoDeserializer() if you are using a single version of FHIR or BaseFhirJsonPocoDeserializer if you want to use multiple versions of FHIR")]
        public FhirJsonPocoDeserializer(Assembly assembly) : base(assembly)
        {
        }

        ///<inheritdoc/>
        [Obsolete("Please use FhirJsonPocoDeserializer() if you are using a single version of FHIR or BaseFhirJsonPocoDeserializer if you want to use multiple versions of FHIR")]
        public FhirJsonPocoDeserializer(ModelInspector inspector) : base(inspector)
        {
        }

        ///<inheritdoc/>
        [Obsolete("Please use FhirJsonPocoDeserializer() if you are using a single version of FHIR or BaseFhirJsonPocoDeserializer if you want to use multiple versions of FHIR")]
        public FhirJsonPocoDeserializer(Assembly assembly, FhirJsonPocoDeserializerSettings settings) : base(assembly, settings)
        {
        }

        ///<inheritdoc/>
        [Obsolete("Please use FhirJsonPocoDeserializer() if you are using a single version of FHIR or BaseFhirJsonPocoDeserializer if you want to use multiple versions of FHIR")]
        public FhirJsonPocoDeserializer(ModelInspector inspector, FhirJsonPocoDeserializerSettings settings) : base(inspector, settings)
        {
        }
    }
}

#nullable restore