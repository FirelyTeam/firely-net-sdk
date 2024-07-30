using System.Collections.Generic;

#nullable enable

namespace Hl7.Fhir.Model.CdsHooks;

public class DiscoveryResponse
{
    public List<Service>? Services { get; set; }
}