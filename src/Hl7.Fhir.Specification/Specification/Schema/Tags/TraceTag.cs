using System;
using System.Text;

namespace Hl7.Fhir.Specification.Schema.Tags
{
    internal class TraceTag : SchemaTag
    {
        private string _message;

        public TraceTag(string message)
        {
            _message = message;
        }

        public override SchemaTag Merge(SchemaTag other)
        {
            return other is TraceTag tt ?
                new TraceTag(_message + Environment.NewLine + tt._message) 
                : 
                this;
        }
    }
}