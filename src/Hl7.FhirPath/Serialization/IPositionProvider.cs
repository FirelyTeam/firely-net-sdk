using System;
using System.Linq;
using System.Collections.Generic;

namespace Hl7.Fhir.Serialization
{
    public interface IPositionProvider
    {
        int LineNumber { get; }
        int LinePosition { get; }
    }

}