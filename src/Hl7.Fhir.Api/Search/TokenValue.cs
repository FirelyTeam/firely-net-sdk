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
    public class TokenValue : Expression
    {
        public string Namespace { get; private set; }

        public string Value { get; private set; }

        public bool AnyNamespace { get; private set; }

        public TokenValue(string value, bool matchAnyNamespace)
        {
            Value = value;
            AnyNamespace = matchAnyNamespace;
        }

        public TokenValue(string value, string ns)
        {
            Value = value;
            AnyNamespace = false;
            Namespace = ns;
        }

        public override string ToString()
        {
            if (!AnyNamespace)
            {
                var ns = Namespace ?? String.Empty;
                return StringValue.EscapeString(ns) + "|" +
                                    StringValue.EscapeString(Value);
            }
            else
                return StringValue.EscapeString(Value);
        }

        public static TokenValue Parse(string text)
        {
            if (text == null) throw Error.ArgumentNull("text");

            string[] pair = text.SplitNotEscaped('|');

            if (pair.Length > 2)
                throw Error.Argument("text", "Token cannot have more than two parts separated by '|'");

            bool hasNamespace = pair.Length == 2;

            if (hasNamespace)
            {
                if(pair[1] == String.Empty)
                    throw new FormatException("Token query parameters should at least specify a value after the '|'");
                
                if (pair[0] == String.Empty)
                    return new TokenValue(pair[1], matchAnyNamespace: false );
                else
                    return new TokenValue(pair[1], pair[0]);
            }
            else
            {
                return new TokenValue(pair[0], matchAnyNamespace: true);
            }            
        }     
    }



}