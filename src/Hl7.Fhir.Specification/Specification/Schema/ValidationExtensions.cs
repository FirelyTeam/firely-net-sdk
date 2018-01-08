using Hl7.Fhir.ElementModel;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Hl7.Fhir.Specification.Schema
{
    public static class ValidationExtensions
    {
        public static Assertions Validate(this IAssertion assertion, IElementNavigator input, ValidationContext vc)
        {
            if (assertion is IGroupValidatable iga)
                return iga.Validate(input, vc);
            else if (assertion is IValidatable ima)
                return ima.Validate(input, vc);
            else
                return Assertions.Success;
        }

        public static Assertions Validate(this IGroupValidatable assertion, IElementNavigator input, ValidationContext vc)
            => assertion.Validate(new[] { input }, vc).Single().Item1;

        internal static JToken MakeNestedProp(this JToken t) => t is JProperty ? new JObject(t) : t;
    }
}

