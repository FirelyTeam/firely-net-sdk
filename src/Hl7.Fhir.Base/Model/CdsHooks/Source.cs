using Hl7.Fhir.Model;
using System;

#nullable enable

namespace Hl7.Fhir.Model.CdsHooks;

[CdsHookElement]
public class Source
{
    public string? Label { get; set; }
    public Uri? Url { get; set; }
    public Uri? Icon { get; set; }
    public Coding? Coding { get; set; }
}