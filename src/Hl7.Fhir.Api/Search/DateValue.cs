using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Hl7.Fhir.Search
{
    public class DateValue : ValueExpression
    {
        public string Value { get; private set; }

        public DateValue(DateTimeOffset value)
        {
            Value = value.ConvertTo<string>();
        }

        public DateValue(string date)
        {
            if (!DateTimePatternAttribute.IsValidValue(date)) throw Error.Argument("date", "Is not a valid FHIR date/time string");

            Value = date;
        }

        public override string ToString()
        {
            return Value;
        }

        public static DateValue Parse(string text)
        {
            return new DateValue(text);
        }
    }
}