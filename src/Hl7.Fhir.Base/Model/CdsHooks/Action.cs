using Hl7.Fhir.Model;

#nullable enable

namespace Hl7.Fhir.Model.CdsHooks;

#if NET8_0_OR_GREATER
[System.Diagnostics.CodeAnalysis.Experimental(diagnosticId: "ExperimentalApi")]
#else
[System.Obsolete("This function is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.")]
#endif
[CdsHookElement]
public class Action
{
    public string? Type { get; set; }
    public string? Description { get; set; }
    public Resource? Resource { get; set; }
    public string? Label { get; set; }
}