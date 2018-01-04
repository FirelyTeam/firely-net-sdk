using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification.Schema.Tags;
using Hl7.Fhir.Utility;
using Newtonsoft.Json.Linq;

namespace Hl7.Fhir.Specification.Schema
{
    public class Undecided : Assertion, IMemberAssertion, IMergeableAssertion
    {
        public override IEnumerable<Assertions> Collect() => Assertions.Failure.Collection;

        public IMergeableAssertion Merge(IMergeableAssertion other)
            => other is Undecided f ? 
                this : throw Error.InvalidOperation($"Internal logic failed: tried to merge an Undecided with an {other.GetType().Name}");

        public Assertions Validate(IElementNavigator input, ValidationContext vc)
            => Assertions.Failure;

        public override string ToString() => $"\"raise\": \"{ValidationResult.Undecided}\"";

        public override JToken ToJson() => new JProperty("raise", ValidationResult.Undecided.ToString());
    }
}
