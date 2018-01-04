using Hl7.Fhir.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hl7.Fhir.Specification.Schema.Tags
{
    // TODO: keep them as separate entries
    // TODO: Do something with issues
    // TODO: remove duplicates
    public class Trace : Assertion, IMergeableAssertion
    {
        private string _message;

        public Trace(string message)
        {
            _message = message;
        }

        public override IEnumerable<Assertions> Collect() => Assertions.Empty.Collection;

        public IMergeableAssertion Merge(IMergeableAssertion other)
            => other is Trace tt ?
                new Trace(_message + Environment.NewLine + tt._message)
                : throw Error.InvalidOperation($"Internal logic failed: tried to merge a Trace with an {other.GetType().Name}");

        public override JToken ToJson() => new JProperty("trace", _message);
    }
}