using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Serialization
{
    public interface IFhirReader
    {
        bool IsAtComplexObject();

        string GetResourceTypeName();

        IEnumerable<Tuple<string, IFhirReader>> GetMembers();
    }
}
