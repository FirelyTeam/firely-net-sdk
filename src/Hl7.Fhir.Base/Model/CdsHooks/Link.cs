#nullable enable

using System;

namespace Hl7.Fhir.Model.CdsHooks;

#if NET8_0_OR_GREATER
[System.Diagnostics.CodeAnalysis.Experimental(diagnosticId: "ExperimentalApi")]
#else
[System.Obsolete("This function is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.")]
#endif
[CdsHookElement]
public class Link
{
    public string? Label { get; set; }
    public Uri? Url { get; set; }
    public string? Type { get; set; }
    public string? AppContext { get; set; }
    public bool? Autolaunchable { get; set; }
}