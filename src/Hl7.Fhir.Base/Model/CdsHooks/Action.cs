using Hl7.Fhir.Model;

#nullable enable

namespace Hl7.Fhir.Model.CdsHooks;

[CdsHookElement]
public class Action
{
    public string? Type { get; set; }
    public string? Description { get; set; }
    public Resource? Resource { get; set; }
    public string? Label { get; set; }
}