using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Validation
{
    /// <summary>
    /// Class to hold the pathname of the property that was not found
    /// (to be used in the checks for slice applicability)
    /// (to be used on the OperationOutcome.Issue property)
    /// </summary>
    public class SlicePathAnnotation
    {
        public static implicit operator SlicePathAnnotation(string value)
        {
            return new SlicePathAnnotation(value);
        }
        public static implicit operator string(SlicePathAnnotation value)
        {
            return value?.Value;
        }
        public SlicePathAnnotation(string value)
        {
            Value = value;
        }
        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}
