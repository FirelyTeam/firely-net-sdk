#nullable enable

namespace Hl7.Fhir.Model.CdsHooks;

public class OverrideReason
{
    public Coding? Reason { get; set; }
    public string? UserComment { get; set; }
}