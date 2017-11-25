using System;

namespace Hl7.Fhir.Model
{
    public interface IBundle
    {
        Uri FirstLink { get; }
        Uri PreviousLink { get; }
        Uri NextLink { get; }
        Uri LastLink { get; }
    }
}
