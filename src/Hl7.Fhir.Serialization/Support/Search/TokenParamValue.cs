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
   
    public class TokenParamValue : SearchParamValue
    {
        public string Namespace { get; internal set; }

        public string Value { get; internal set; }

        public bool NamespaceSensitive { get; internal set; }

        public TokenParamValue(string value, bool namespaceSensitive = false)
        {
            Value = value;
            NamespaceSensitive = namespaceSensitive;
        }

        public TokenParamValue(string value, string ns)
        {
            Value = value;
            NamespaceSensitive = true;
            Namespace = NamespaceSensitive ? ns : null;
        }

     
        internal static TokenParamValue FromQueryValue(string queryValue)
        {
            string[] pair = queryValue.Split('!');

            // Only first '!' is relevant, concatenate the rest
            if (pair.Length > 2) pair[1] = String.Join("!", pair.Skip(1));

            if(pair[0] == String.Empty && pair[1] == String.Empty)
                throw new FormatException("Token query parameters should at least specify a value, not just the separator '!'");

            bool hasNamespace = queryValue.Contains("!");
            if (hasNamespace)
            {
                if(pair[1] == String.Empty)
                    throw new FormatException("Token query parameters should at least specify a value, not just a namespace");

                if(pair[0] == String.Empty)
                    return new TokenParamValue(pair[1], namespaceSensitive:true);
                else
                    return new TokenParamValue(pair[1], pair[0]);
            }
            else
            {
                return new TokenParamValue(pair[0], namespaceSensitive:false);
            }            
        }


        internal override string QueryValue
        {
            get
            {
                if(NamespaceSensitive)
                    return (Namespace ?? String.Empty) + "!" + Value;
                else
                    return Value;
            }
        }
    }


    public enum NamespaceRelevance
    {
        /// <summary>
        /// This token matches when the value matches (namespace is ignored)
        /// </summary>
        AnyNamespace,

        /// <summary>
        /// This matches when the value matches and the namespace is explicitly empty
        /// </summary>
        NoNamespace
    }
}