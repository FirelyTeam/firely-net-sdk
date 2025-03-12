#nullable enable

using Hl7.Fhir.Model;
using System;

namespace Hl7.Fhir.Serialization;

/// <inheritdoc/>
[Obsolete("Use FhirXmlDeserializer isntead.")]
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
    public FhirXmlPocoDeserializer(DeserializerSettings settings) : base(ModelInfo.ModelInspector, settings)
    {
    }
}