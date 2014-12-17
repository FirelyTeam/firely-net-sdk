/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

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
    public class UntypedValue : ValueExpression
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

        public FhirDateTime AsDateTimeValue()
        {
            return new FhirDateTime(Value);
        }

        public StringValue AsStringValue()
        {
            return StringValue.Parse(Value);
        }

        public TokenValue AsTokenValue()
        {
            return TokenValue.Parse(Value);
        }

        public QuantityValue AsQuantityValue()
        {
            return QuantityValue.Parse(Value);
        }

        public ReferenceValue AsReferenceValue()
        {
            return ReferenceValue.Parse(Value);
        }
    }
}