#nullable enable
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Model.CdsHooks;

public class Suggestion
{
    public string? Label { get; set; }
    public string? Uuid { get; set; }
    public bool IsRecommended { get; set; }
    public List<Action>? Actions { get; set; }
}