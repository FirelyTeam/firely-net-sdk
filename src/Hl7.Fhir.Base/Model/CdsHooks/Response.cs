#nullable enable
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Model.CdsHooks;

[CdsHookElement]
public class Response
{
    public List<Card>? Cards { get; set; }
    public List<Action>? SystemActions { get; set; }
}