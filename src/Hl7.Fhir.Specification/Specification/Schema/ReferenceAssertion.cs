using System;
using System.Collections.Generic;
using Hl7.Fhir.ElementModel;
using Newtonsoft.Json.Linq;

namespace Hl7.Fhir.Specification.Schema
{
    public class ReferenceAssertion : IAssertion, IGroupValidatable, ICollectable
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
        public IEnumerable<Assertions> Collect() => ReferencedSchema.Collect();

        public List<(Assertions,IElementNavigator)> Validate(IEnumerable<IElementNavigator> input, ValidationContext vc)
            => ReferencedSchema.Validate(input, vc);

        public JToken ToJson() => new JProperty("ref", ReferencedSchema.Id ?? "no identifier");
    }

}
