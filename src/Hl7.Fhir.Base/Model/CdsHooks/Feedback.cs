using System.Collections.Generic;

namespace Hl7.Fhir.Model.CdsHooks;

#nullable enable

[CdsHookElement]
public class Feedback
{
    public string? Card { get; set; }
    public string? Outcome { get; set; }
    public List<Identifier>? AcceptedSuggestions { get; set; }
    public OverrideReason? OverrideReason { get; set; }
    public string? OutcomeTimeStamp { get; set; }
}