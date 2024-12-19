#nullable enable

using Hl7.Fhir.Model;

namespace Hl7.Fhir.Serialization;

public class FhirJsonPocoSerializer : BaseFhirJsonPocoSerializer
{
    public static readonly FhirJsonPocoSerializer Default = new();

    /// <summary>
    /// Construct a new FHIR Json serializer, based on the currently used FHIR version.
    /// </summary>
    public FhirJsonPocoSerializer() : base(ModelInfo.ModelInspector)
    {
    }
}