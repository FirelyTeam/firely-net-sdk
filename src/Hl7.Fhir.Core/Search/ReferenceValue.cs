/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

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
    public class ReferenceValue : ValueExpression
    {
        public string Value { get; private set; }
     
        public ReferenceValue(string value)
        {
            if (!Uri.IsWellFormedUriString(value, UriKind.Absolute) &&
                !Id.IsValidValue(value))
                throw Error.Argument("text", "Reference is not a valid Id nor a valid absolute Url");

            Value = value;
        }
                              
        public override string ToString()
        {
            return StringValue.EscapeString(Value);
        }

        public static ReferenceValue Parse(string text)
        {
            var value = StringValue.UnescapeString(text);
         
            return new ReferenceValue(value);
        }
    }
}