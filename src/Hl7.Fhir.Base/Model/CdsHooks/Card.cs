#nullable enable
using Hl7.Fhir.Model;
using System.Collections.Generic;

namespace Hl7.Fhir.Model.CdsHooks;

[CdsHookElement]
public class Card
{
    public string? Uuid { get; set; }
    public string? Summary { get; set; }
    public string? Detail { get; set; }
    public string? Indicator { get; set; }
    public Source? Source { get; set; }
    public List<Suggestion>? Suggestions { get; set; }
    public string? SelectionBehavior { get; set; }
    public List<Coding>? OverrideReasons { get; set; }
    public List<Link>? Links { get; set; }
}