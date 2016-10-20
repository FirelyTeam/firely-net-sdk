using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Terminology
{
    public class ValueSetExpansionTooBigException : Exception
    {
        public ValueSetExpansionTooBigException(string message) : base(message)
        {
            //
        }
    }
}
