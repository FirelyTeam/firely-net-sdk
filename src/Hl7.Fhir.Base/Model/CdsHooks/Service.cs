using System.Collections.Generic;

#nullable enable

namespace Hl7.Fhir.Model.CdsHooks;

[CdsHookElement]
public class Service
{
    public string? Hook { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Id { get; set; }
    public Dictionary<string, string>? Prefetch { get; set; }
    public string? UsageRequirements { get; set; }
}