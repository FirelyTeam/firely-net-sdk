#nullable enable

using System.Text.Json.Serialization;

namespace Hl7.Fhir.Model.CdsHooks;

#if NET8_0_OR_GREATER
[System.Diagnostics.CodeAnalysis.Experimental(diagnosticId: "ExperimentalApi")]
#else
[System.Obsolete("This function is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.")]
#endif
[CdsHookElement]
public class Authorization
{
    [JsonPropertyName("access_token")] public string? AccessToken { get; set; } // we need this naming for the deserialization
    [JsonPropertyName("token_type")] public string? TokenType { get; set; }
    [JsonPropertyName("expires_in")] public int? ExpiresIn { get; set; }
    public string? Scope { get; set; }
    public string? Subject { get; set; }
    public string? Patient { get; set; }
}