using Hl7.Fhir.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hl7.Fhir.Specification.Schema
{
    // TODO: keep them as separate entries
    // TODO: Do something with issues
    // TODO: remove duplicates
    public class Trace : IAssertion, IMergeable
    {
        private string _message;

        public Trace(string message)
        {
            _message = message;
        }

        public IMergeable Merge(IMergeable other)
            => other is Trace tt ?
                new Trace(_message + Environment.NewLine + tt._message)
                : throw Error.InvalidOperation($"Internal logic failed: tried to merge a Trace with an {other.GetType().Name}");

        public JToken ToJson() => new JProperty("trace", _message);
    }
}