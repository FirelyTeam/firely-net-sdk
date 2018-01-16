using System;
using System.Collections.Generic;
using Hl7.Fhir.ElementModel;
using Newtonsoft.Json.Linq;

namespace Hl7.Fhir.Specification.Schema
{
    public class ReferenceAssertion : IAssertion, IGroupValidatable, ICollectable
    {
        public ReferenceAssertion(ElementSchema schema)
        {
            _reference = new Lazy<ElementSchema>(() => schema);
        }

        public ReferenceAssertion(Func<ElementSchema> dereference)
        {
            _reference = new Lazy<ElementSchema>(dereference);
        }

        private readonly Lazy<ElementSchema> _reference;

        public ElementSchema ReferencedSchema => _reference.Value;

        // TODO: Risk of loop (if a referenced schema refers back to this schema - which is nonsense, but possible)
        public IEnumerable<Assertions> Collect() => ReferencedSchema.Collect();

        public List<(Assertions,IElementNavigator)> Validate(IEnumerable<IElementNavigator> input, ValidationContext vc)
            => ReferencedSchema.Validate(input, vc);

        public JToken ToJson() => new JProperty("ref", ReferencedSchema.Id ?? "no identifier");
    }

}
