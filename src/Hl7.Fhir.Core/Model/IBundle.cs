using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Model
{
    public partial interface IBundle
    {
        Uri FirstLink { get; }
        Uri PreviousLink { get; }
        Uri NextLink { get; }
        Uri LastLink { get; }
    }
}
