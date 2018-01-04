using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification.Schema.Tags;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Schema
{
    public static class ValidationExtensions
    {
        public static Assertions Validate(this Assertion assertion, IElementNavigator input, ValidationContext vc)
        {
            if (assertion is IGroupAssertion iga)
                return iga.Validate(input, vc);
            else if (assertion is IMemberAssertion ima)
                return ima.Validate(input, vc);
            else
                return Assertions.Success;
        }

        public static Assertions Validate(this IGroupAssertion assertion, IElementNavigator input, ValidationContext vc)
            => assertion.Validate(new[] { input }, vc).Single().Item1;

        internal static JToken MakeNestedProp(this JToken t) => t is JProperty ? new JObject(t) : t;
    }
}

