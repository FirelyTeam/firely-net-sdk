using System.Collections.Generic;
using Hl7.Fhir.ElementModel;
using Newtonsoft.Json.Linq;

namespace Hl7.Fhir.Specification.Schema
{
    public abstract class SimpleAssertion : IAssertion, ICollectable, IValidatable
    {
        public IEnumerable<Assertions> Collect() => new Assertions(this).Collection;

        public JToken ToJson() => new JProperty(Key, Value);

        public abstract Assertions Validate(IElementNavigator input, ValidationContext vc);

        protected abstract string Key { get; }
        protected abstract object Value { get; }
    }
}
