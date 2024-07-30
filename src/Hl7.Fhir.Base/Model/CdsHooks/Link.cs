#nullable enable

using System;

namespace Hl7.Fhir.Model.CdsHooks;

public class Link
{
    public string? Label { get; set; }
    public Uri? Url { get; set; }
    public string? Type { get; set; }
    public string? AppContext { get; set; }
    public bool? Autolaunchable { get; set; }
}