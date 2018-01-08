using System.Collections.Generic;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using Newtonsoft.Json.Linq;

namespace Hl7.Fhir.Specification.Schema
{
    public class Undecided : IAssertion, IValidatable, IMergeable, ICollectable
    {
        public IEnumerable<Assertions> Collect() => Assertions.Failure.Collection;

        public IMergeable Merge(IMergeable other)
            => other is Undecided f ? 
                this : throw Error.InvalidOperation($"Internal logic failed: tried to merge an Undecided with an {other.GetType().Name}");

        public Assertions Validate(IElementNavigator input, ValidationContext vc)
            => Assertions.Failure;

        public JToken ToJson() => new JProperty("raise", ValidationResult.Undecided.ToString());
    }
}
