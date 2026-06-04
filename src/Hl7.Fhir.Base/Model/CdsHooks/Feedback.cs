using System.Collections.Generic;

namespace Hl7.Fhir.Model.CdsHooks;

#nullable enable

#if NET8_0_OR_GREATER
[System.Diagnostics.CodeAnalysis.Experimental(diagnosticId: "ExperimentalApi")]
#else
[System.Obsolete("This function is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.")]
#endif
[CdsHookElement]
public class Feedback
{
    public string? Card { get; set; }
    public string? Outcome { get; set; }
    public List<Identifier>? AcceptedSuggestions { get; set; }
    public OverrideReason? OverrideReason { get; set; }
    public string? OutcomeTimeStamp { get; set; }
}