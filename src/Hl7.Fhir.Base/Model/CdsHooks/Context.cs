#nullable enable
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hl7.Fhir.Model.CdsHooks;

public class Context
{
    public string? UserId { get; set; }
    public string? PatientId { get; set; }
    public string? EncounterId { get; set; }
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? Fields { get; set; }
}