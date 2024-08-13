#nullable enable
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Model.CdsHooks;

#if NET8_0_OR_GREATER
[System.Diagnostics.CodeAnalysis.Experimental(diagnosticId: "ExperimentalApi")]
#else
[System.Obsolete("This function is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.")]
#endif
[CdsHookElement]
public class Suggestion
{
    public string? Label { get; set; }
    public string? Uuid { get; set; }
    public bool IsRecommended { get; set; }
    public List<Action>? Actions { get; set; }
}