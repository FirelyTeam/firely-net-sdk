#nullable enable
using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Hl7.Fhir.Model.CdsHooks;

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