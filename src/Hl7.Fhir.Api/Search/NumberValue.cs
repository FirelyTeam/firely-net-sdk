using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Hl7.Fhir.Search
{
    public class NumberValue : Expression
    {
        public Decimal Value { get; private set; }
     
        public NumberValue(Decimal value)
        {
            Value = value;
        }
                              
        public override string ToString()
        {
            return Value.ConvertTo<string>();
        }

        public static NumberValue Parse(string text)
        {
            return new NumberValue(text.ConvertTo<Decimal>());
        }
    }
}