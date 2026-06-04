#nullable enable
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hl7.Fhir.Model.CdsHooks;

#if NET8_0_OR_GREATER
[System.Diagnostics.CodeAnalysis.Experimental(diagnosticId: "ExperimentalApi")]
#else
[System.Obsolete("This function is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.")]
#endif
[CdsHookElement]
public class Context
{
    public string? UserId { get; set; }
    public string? PatientId { get; set; }
    public string? EncounterId { get; set; }
    [JsonExtensionData] public Dictionary<string, JsonElement>? Fields { get; set; }
}