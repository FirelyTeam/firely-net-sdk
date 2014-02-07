using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Hl7.Fhir.Search
{
    public class UntypedValue : Expression
    {
        public string Value { get; private set; }

        public UntypedValue(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }

        public NumberValue AsNumberValue()
        {
            return NumberValue.Parse(Value);
        }

        public DateValue AsDateValue()
        {
            return DateValue.Parse(Value);
        }

        public StringValue AsStringValue()
        {
            return StringValue.Parse(Value);
        }

        public TokenValue AsTokenValue()
        {
            return TokenValue.Parse(Value);
        }

        //public ReferenceParamValue AsReferenceValue()
        //{
        //    return ReferenceParamValue.FromQueryValue(Value);
        //}
    }
}