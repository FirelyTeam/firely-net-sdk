using System;
using System.Collections.Generic;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using Newtonsoft.Json.Linq;

namespace Hl7.Fhir.Specification.Schema
{
    public class ImpliesAssertion : IAssertion, IValidatable, ICollectable
    {
        public readonly IAssertion Condition;

        public readonly IAssertion Assertion;

        public ImpliesAssertion(IAssertion condition, IAssertion assertion)
        {
            Condition = condition ?? throw new ArgumentNullException(nameof(condition));
            Assertion = assertion ?? throw new ArgumentNullException(nameof(assertion));
        }

        public IEnumerable<Assertions> Collect() => Condition.Collect().Product(Assertion.Collect());

        public Assertions Validate(IElementNavigator input, ValidationContext vc)
        {
            var result = Condition.Validate(input, vc);

            switch (result.Result.Result)
            {
                case ValidationResult.Success:
                    // is a member, result depends on membership assertion
                    return result + Assertion.Validate(input, vc);
                case ValidationResult.Failure:
                    // fails membership condition -> no reason to fail
                    return Assertions.Success;
                case ValidationResult.Undecided:
                    // propagate undecided results
                    return Assertions.Undecided;
                default:
                    throw Error.InvalidOperation($"Internal logic failed: Unknown validation result '{result.Result.Result}' encountered in ConditionalAssertion.");
            }
        }

        public JToken ToJson() =>
            new JProperty("implies",
                new JObject(
                    new JProperty("condition", Condition.ToJson().MakeNestedProp()),
                    new JProperty("assertion", Assertion.ToJson().MakeNestedProp())));
    }

}
