using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification.Schema.Tags;
using Newtonsoft.Json.Linq;

namespace Hl7.Fhir.Specification.Schema
{
    public class ReferenceAssertion : Assertion, IGroupAssertion
    {
        // A symbolic reference, or a reference to another Schema?
        // Or one of the next two?

        public ReferenceAssertion(ElementSchema schema)
        {
            DirectReference = schema;
        }

        public ReferenceAssertion(Func<ElementSchema> dereference)
        {
            Dereference = dereference;
        }

        public readonly ElementSchema DirectReference;

        public readonly Func<ElementSchema> Dereference;

        private ElementSchema ReferencedSchema => DirectReference ?? Dereference();

        // TODO: Risk of loop (if a referenced schema refers back to this schema - which is nonsense, but possible)
        public override IEnumerable<Assertions> Collect()
            => ReferencedSchema.Collect();

        public List<(Assertions,IElementNavigator)> Validate(IEnumerable<IElementNavigator> input, ValidationContext vc)
            => ReferencedSchema.Validate(input, vc);

        public override JToken ToJson() => new JProperty("ref", ReferencedSchema.Id ?? "no identifier");
    }

}
