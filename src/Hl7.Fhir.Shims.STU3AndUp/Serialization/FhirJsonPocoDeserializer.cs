#nullable enable

using Hl7.Fhir.Model;
using System;

namespace Hl7.Fhir.Serialization;

[Obsolete("Use FhirJsonDeserializer instead.")]
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
    public FhirJsonPocoDeserializer(FhirJsonConverterOptions settings) : base(ModelInfo.ModelInspector, settings) { }
}