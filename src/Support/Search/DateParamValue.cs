/*
  Copyright (c) 2011-2013, HL7, Inc.
  All rights reserved.
  
  Redistribution and use in source and binary forms, with or without modification, 
  are permitted provided that the following conditions are met:
  
   * Redistributions of source code must retain the above copyright notice, this 
     list of conditions and the following disclaimer.
   * Redistributions in binary form must reproduce the above copyright notice, 
     this list of conditions and the following disclaimer in the documentation 
     and/or other materials provided with the distribution.
   * Neither the name of HL7 nor the names of its contributors may be used to 
     endorse or promote products derived from this software without specific 
     prior written permission.
  
  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
  IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
  POSSIBILITY OF SUCH DAMAGE.
  

*/


using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Hl7.Fhir.Support.Search
{
    public class DateParamValue : SearchParamValue
    {
        public string Value { get; internal set; }

        public ComparisonOperator Comparison { get; internal set; }

        public DateParamValue(string value)
        {
            Value = value;
            Comparison = ComparisonOperator.EQ;
        }

        public DateParamValue(ComparisonOperator comparison, string value) : this(value)
        {
            Comparison = comparison;
        }

        public DateParamValue(DateTimeOffset value) 
            : this(PrimitiveTypeConverter.ConvertTo<string>(value))
        {             
        }

        public DateParamValue(ComparisonOperator comparison, DateTimeOffset value) 
            : this(comparison,PrimitiveTypeConverter.ConvertTo<string>(value))
        {
        }

        internal static DateParamValue FromQueryValue(string queryValue)
        {
            var compVal = findComparator(queryValue);

            var value = compVal.Item1;
            var comp = compVal.Item2;

            return new DateParamValue(comp, value);
        }

        private static Tuple<string, ComparisonOperator> findComparator(string value)
        {
            ComparisonOperator comparison = ComparisonOperator.EQ;

            if (value.StartsWith(">"))
            { comparison = ComparisonOperator.GT; value = value.Substring(1); }
            else if (value.StartsWith("<"))
            { comparison = ComparisonOperator.LT; value = value.Substring(1); }

            return Tuple.Create(value, comparison);
        }

        private static string addComparison(string value, ComparisonOperator comparison)
        {
            string result = value;

            if (comparison != ComparisonOperator.EQ)
            {
                switch (comparison)
                {
                    case ComparisonOperator.GT: result = ">" + result; break;
                    case ComparisonOperator.LT: result = "<" + result; break;
                    default: throw new InvalidOperationException();
                }
            }

            return result;
        }

        internal override string QueryValue
        {
            get
            {
                return addComparison(this.Value.ToString(), this.Comparison);
            }
        }
    }
}