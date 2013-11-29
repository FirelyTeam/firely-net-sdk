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
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Hl7.Fhir.Support.Search
{
    public class CombinedParamValue : SearchParamValue
    {
        public IEnumerable<SearchParamValue> Values { get; internal set; }

        public CombinedParamValue( params SearchParamValue[] parameters) : this((IEnumerable<SearchParamValue>)parameters)
        {           
        }

        public CombinedParamValue( IEnumerable<SearchParamValue> parameters)
        {
            if (parameters.Any(par => par is CombinedParamValue))
                throw new ArgumentException("Combined parameters can only combine atomic-value parameters, not nested combined parameters");

            Values = parameters;
        }


        internal static CombinedParamValue FromQueryValue(string queryValue)
        {
            var vals = queryValue.Split('$');

            var pars = vals.Select(s=> new UntypedParamValue(s));

            return new CombinedParamValue(pars);
        }

        internal override string QueryValue 
        {
            get
            {
                var result = Values.Aggregate<SearchParamValue,string>(String.Empty, (s,par) => s += par.QueryValue + "$");

                // Remove last $ we added, that's just the separator
                if (result != String.Empty) result = result.Substring(0, result.Length - 1);

                return result;
            }
        }
    }
}