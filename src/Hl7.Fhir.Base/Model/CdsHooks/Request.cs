#nullable enable
using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Hl7.Fhir.Model.CdsHooks;

#if NET8_0_OR_GREATER
[System.Diagnostics.CodeAnalysis.Experimental(diagnosticId: "ExperimentalApi")]
#else
[System.Obsolete("This function is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.")]
#endif
[CdsHookElement]
public class Request
{
    public string? HookInstance { get; set; }
    public Uri? FhirServer { get; set; }
    public string? Hook { get; set; }
    public Authorization? FhirAuthorization { get; set; }
    public Context? Context { get; set; }
    public Dictionary<string, Resource>? Prefetch { get; set; }
}