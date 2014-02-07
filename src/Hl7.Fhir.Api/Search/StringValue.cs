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
    public class StringValue : Expression
    {
        public string Value { get; private set; }

        public StringValue(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return EscapeString(Value);
        }

        public static StringValue Parse(string text)
        {
            return new StringValue(UnescapeString(text));
        }


        internal static string EscapeString(string value)
        {
            if (value == null) return null;

            value = value.Replace(@"\", @"\\");
            value = value.Replace(@"$", @"\$");
            value = value.Replace(@",", @"\,");
            value = value.Replace(@"|", @"\|");

            return value;
        }

        internal static string UnescapeString(string value)
        {
            if (value == null) return null;

            value = value.Replace(@"\|", @"|");
            value = value.Replace(@"\,", @",");
            value = value.Replace(@"\$", @"$");
            value = value.Replace(@"\\", @"\");

            return value;
        }
    }
}