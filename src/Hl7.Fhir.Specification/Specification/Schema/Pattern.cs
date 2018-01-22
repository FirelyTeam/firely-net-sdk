using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using Newtonsoft.Json.Linq;

namespace Hl7.Fhir.Specification.Schema
{
    public class Pattern : SimpleAssertion
    {
        public readonly string RegularExpression;

        public Pattern(string regex)
        {
            RegularExpression = regex ?? throw new ArgumentNullException(nameof(regex));
        }

        protected override string Key => "pattern";

        protected override object Value => RegularExpression;

        public override Assertions Validate(IElementNavigator input, ValidationContext vc)
        {
            var regex = new Regex(RegularExpression);
            var value = toStringRepresentation(input);
            var success = Regex.Match(value, "^" + regex + "$").Success;

            if (!success)
                return new Assertions(new Trace($"Value '{value}' does not match regex '{RegularExpression}'",
                    input.Location, Issue.CONTENT_ELEMENT_INVALID_PRIMITIVE_VALUE));
            else
                return Assertions.Success;

            string toStringRepresentation(IElementNavigator vp)
            {
                if (vp == null || vp.Value == null) return null;
                return PrimitiveTypeConverter.ConvertTo<string>(vp.Value);
            }

        }
    }
}
