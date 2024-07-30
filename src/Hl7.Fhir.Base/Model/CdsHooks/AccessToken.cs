#nullable enable

using System.Text.Json.Serialization;

namespace Hl7.Fhir.Model.CdsHooks;

public class Authorization
{
    [JsonPropertyName("access_token")]
    public string? AccessToken { get; set; } // we need this naming for the deserialization
    [JsonPropertyName("token_type")]
    public string? TokenType { get; set; }
    [JsonPropertyName("expires_in")]
    public int? ExpiresIn { get; set; }
    public string? Scope { get; set; }
    public string? Subject { get; set; }
    public string? Patient { get; set; }
}