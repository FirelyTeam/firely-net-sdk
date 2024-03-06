#nullable enable

using System.Text.RegularExpressions;

namespace Hl7.Fhir.Specification.Terminology
{
    /// <summary>
    /// Checks if codes are valid language types
    /// </summary>
    public class LanguageTerminologyService : CustomValueSetTerminologyService
    {
        private const string LANGUAGE_SYSTEM = "urn:ietf:bcp:47";
        public const string LANGUAGE_VALUESET = "http://hl7.org/fhir/ValueSet/all-languages";

        public LanguageTerminologyService() : base("language", LANGUAGE_SYSTEM, [LANGUAGE_VALUESET])
        {
        }

        override protected bool ValidateCodeType(string code)
        {
            var regex = new Regex("^[a-z]{2}(-[A-Z]{2})?$"); // matches either two lowercase letters OR 2 lowercase letters followed by a dash and two uppercase letters
            return regex.IsMatch(code);
        }
    }
}

#nullable restore