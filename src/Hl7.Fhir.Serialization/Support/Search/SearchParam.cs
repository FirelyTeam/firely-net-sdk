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


using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Hl7.Fhir.Support.Search
{
    //TODO: does this support chained parameters with modifiers?
    //e.g. subject:patient.organization:organization?
    public class SearchParam
    {
        /// <summary>
        /// Name of the parameter as found in the definition of the Resource
        /// </summary>
        public string Name { get; internal set; }

        public string Modifier { get; internal set; }

        public IEnumerable<SearchParamValue> Values { get; internal set; }

        public SearchParam(string name, SearchParamValue value)
            : this(name, (string)null, value)
        {
        }

        public SearchParam(string name, string value)
            : this(name, (string)null, new UntypedParamValue(value))
        {
        }

        public SearchParam(string name, string modifier, string value)
            : this(name, modifier, new UntypedParamValue(value))
        {
        }

        public SearchParam(string name, string modifier, SearchParamValue value)
            : this(name, modifier, new List<SearchParamValue> { value })
        {
        }

        public SearchParam(string name, IEnumerable<SearchParamValue> values)
            : this(name, null, values)
        {
        }

        public SearchParam(string name, IEnumerable<string> values)
            : this(name, null, values)
        {
        }

        public SearchParam(string name, string modifier, IEnumerable<SearchParamValue> values)
        {
            Name = name;
            Modifier = modifier;
            Values = values;
        }

        public SearchParam(string name, string modifier, IEnumerable<string> values) : this(name,modifier)
        {
            Values = values.Select(v => new UntypedParamValue(v));
        }

        public SearchParam(string name, params SearchParamValue[] values)
            : this(name, null, values)
        {
        }

        public SearchParam(string name, params string[] values)
            : this(name, null, values)
        {
        }

        public SearchParam(string name, string modifier, params SearchParamValue[] values) 
            : this(name,modifier, (IEnumerable<SearchParamValue>)values)
        {
        }

        public SearchParam(string name, bool isMissing) 
            : this(name, "missing", new BoolParamValue(isMissing))
        {
        }

        public static SearchParam FromQueryKeyAndValue(string qryKey, string qryValue)
        {
            string[] pair = qryKey.Split(':');

            // Only first ':' is relevant, concatenate the rest
            if (pair.Length > 2) pair[1] = String.Join(":", pair.Skip(1));

            if (pair[0] == String.Empty) throw new FormatException("A query parameter must have a non-empty name");

            string name = pair[0];
            string modifier = pair.Length == 2 ? pair[1] : null;

            //TODO: Don't split on , within strings
            var qryValues = qryValue.Split(',');

            var values = qryValues.Select(s => new UntypedParamValue(s));

            return new SearchParam(name, modifier, values);
        }


        internal string QueryKey
        {
            get
            {
                if (Modifier != null)
                    return Name + ":" + Modifier;
                else
                    return Name;
            }
        }

        internal string QueryValue
        {
            get
            {
                var values = Values.Select(par => par.QueryValue);

                return String.Join(",",values);
            }
        }

        internal string QueryPair
        {
            get
            {
                return QueryKey + "=" + QueryValue;
            }
        }

        /*
        /// <summary>
        /// Indicates whether the search is for instances where this parameter does or does not
        /// have any value. Returns null if no missing modifier found.
        /// </summary>
        public MissingOperator? Missing { get; protected set; }

        public const string MOD_MISSING = "missing";

        protected string renderMissingParam()
        {
            if (Missing != null)
            {
                string key, value;
                key = this.Name + ":missing";
                value = Missing.Value == MissingOperator.HasNoValue ? "true" : "false";

                return QueryParam.ToString(key, value);
            }
            else
                throw new InvalidOperationException();
        }


        protected static Tuple<string,MissingOperator> checkMissingParam(string key, string value)
        {
            if (key.EndsWith(":missing"))
            {
                key = key.Substring(0, key.Length - ":missing".Length);
                if (String.Equals(value,"true"))
                    return Tuple.Create(key,MissingOperator.HasNoValue);
                else if(String.Equals(value,"false"))
                    return Tuple.Create(key,MissingOperator.HasAnyValue);
                else
                    throw new ArgumentException("Search parameter uses modifier 'missing', but has no valid parameter value");
            }
            else
                return null;
        }
    }


    /// <summary>
    /// Type op missing operator applicable to any parameter, except combinations
    /// </summary>
    public enum MissingOperator
    {
        HasAnyValue,    // Has a value (missing=false)
        HasNoValue      // Does not have any value (missing=true)
    }*/
    }
}