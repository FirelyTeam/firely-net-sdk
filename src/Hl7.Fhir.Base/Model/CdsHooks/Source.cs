using Hl7.Fhir.Model;
using System;

#nullable enable

namespace Hl7.Fhir.Model.CdsHooks;

#if NET8_0_OR_GREATER
[System.Diagnostics.CodeAnalysis.Experimental(diagnosticId: "ExperimentalApi")]
#else
[System.Obsolete("This function is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.")]
#endif
[CdsHookElement]
public class Source
{
    public string? Label { get; set; }
    public Uri? Url { get; set; }
    public Uri? Icon { get; set; }
    public Coding? Coding { get; set; }
}