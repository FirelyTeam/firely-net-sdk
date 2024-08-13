using System.Collections.Generic;

#nullable enable

namespace Hl7.Fhir.Model.CdsHooks;

#if NET8_0_OR_GREATER
[System.Diagnostics.CodeAnalysis.Experimental(diagnosticId: "ExperimentalApi")]
#else
[System.Obsolete("This function is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.")]
#endif
[CdsHookElement]
public class Service
{
    public string? Hook { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Id { get; set; }
    public Dictionary<string, string>? Prefetch { get; set; }
    public string? UsageRequirements { get; set; }
}