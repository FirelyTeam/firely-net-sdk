using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Model
{
    [System.Diagnostics.DebuggerDisplay(@"{Value}")]
    public partial class Markdown : IStringValue
    {
        public static bool IsValidValue(string value)
        {
            return FhirString.IsValidValue(value);
        }
    }
}
