using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Hl7.Fhir.Profiling
{
    public interface IPrimitiveValidator
    {
        bool IsValid(string value);
    }

    class RegExPrimitiveValidator : IPrimitiveValidator
    {
        string pattern;

        public static IPrimitiveValidator For(string pattern)
        {
            var v = new RegExPrimitiveValidator();
            v.pattern = pattern;
            return v;
        }

        
        public bool IsValid(string value)
        {
            return Regex.IsMatch(value, pattern);
        }
    }

    public class UriPrimitiveValidator : IPrimitiveValidator
    {
        public bool IsValid(string value)
        {
            //return Hl7.Fhir.Validation.UriPatternAttribute.IsValidValue(value);
            return Uri.IsWellFormedUriString(value, UriKind.RelativeOrAbsolute);

        }
    }
}
