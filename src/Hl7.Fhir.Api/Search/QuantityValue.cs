/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using Hl7.Fhir.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Hl7.Fhir.Search
{  
    public class QuantityValue : ValueExpression
    {
        public Decimal Number { get; private set; }

        public string Namespace { get; private set; }

        public string Unit { get; private set; }

        public QuantityValue(Decimal number, string unit)
        {
            Number = number;
            Unit = unit;
        }

        public QuantityValue(Decimal number, string ns, string unit)
        {
            Number = number;
            Unit = unit;
            Namespace = ns;
        }

        public override string ToString()
        {
            var ns = Namespace ?? String.Empty;
            return Number.ConvertTo<string>() + "|" +
                StringValue.EscapeString(ns) + "|" +
                StringValue.EscapeString(Unit);
        }

        public static QuantityValue Parse(string text)
        {
            if (text == null) throw Error.ArgumentNull("text");

            string[] triple = text.SplitNotEscaped('|');

            if (triple.Length != 3)
                throw Error.Argument("text", "Quantity needs to have three parts separated by '|'");

            if(triple[0] == String.Empty) 
                throw new FormatException("Quantity needs to specify a number");
                
            var number = triple[0].ConvertTo<Decimal>();
            var ns = triple[1] != String.Empty ? StringValue.UnescapeString(triple[1]) : null;

            if (triple[2] == String.Empty)
                throw new FormatException("Quantity needs to specify a unit");

            var unit = StringValue.UnescapeString(triple[2]);
 
            return new QuantityValue(number,ns,unit);
        }     
    }
}