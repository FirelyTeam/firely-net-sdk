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

namespace Hl7.Fhir.Search
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

        //public IEnumerable<ParamValue> Values { get; internal set; }

        //public SearchParam(string name, ParamValue value)
        //    : this(name, (string)null, value)
        //{
        //}

        public SearchParam(string name, string value)
        {
            throw new NotImplementedException();
        }

        //public SearchParam(string name, string value)
        //    : this(name, (string)null, new UntypedValue(value))
        //{
        //}

        //public SearchParam(string name, string modifier, string value)
        //    : this(name, modifier, new UntypedValue(value))
        //{
        //}

        //public SearchParam(string name, string modifier, ParamValue value)
        //    : this(name, modifier, new List<ParamValue> { value })
        //{
        //}

        //public SearchParam(string name, IEnumerable<ParamValue> values)
        //    : this(name, null, values)
        //{
        //}

        //public SearchParam(string name, IEnumerable<string> values)
        //    : this(name, null, values)
        //{
        //}

        //public SearchParam(string name, string modifier, IEnumerable<ParamValue> values)
        //{
        //    Name = name;
        //    Modifier = modifier;
        //    Values = values;
        //}

        //public SearchParam(string name, string modifier, IEnumerable<string> values)
        //    : this(name, modifier)
        //{
        //    Values = values.Select(v => new UntypedValue(v));
        //}

        //public SearchParam(string name, params ParamValue[] values)
        //    : this(name, null, values)
        //{
        //}

        //public SearchParam(string name, params string[] values)
        //    : this(name, null, values)
        //{
        //}

        //public SearchParam(string name, string modifier, params ParamValue[] values)
        //    : this(name, modifier, (IEnumerable<ParamValue>)values)
        //{
        //}

        //public SearchParam(string name, string modifier, params string[] values)
        //    : this(name, modifier, (IEnumerable<string>)values)
        //{
        //}

        //public SearchParam(string name, bool isMissing)
        //    : this(name, "missing", new BoolParamValue(isMissing))
        //{
        //}


        internal string QueryKey { get { throw new NotImplementedException(); } }
        internal string QueryValue { get { throw new NotImplementedException(); } }

        //internal string QueryKey
        //{
        //    get
        //    {
        //        "OK, eigenlijk is de abstract syntax tree van een Query nog wat anders,"
        //        "een SearchParam heeft nu "values", maar het is eigenlijk dan een in operator"
        //        "net als de chain een nesting inhoudt. Even kijken hoe martijn dat kan hergebruiken"
        //        if (Modifier != null)
        //            return Name + ":" + Modifier;
        //        else
        //            return Name;
        //    }
        //}

        //internal string QueryValue
        //{
        //    get
        //    {
        //        var values = Values.Select(par => par.QueryValue);

        //        return String.Join(",", values);
        //    }
        //}

        //internal string QueryPair
        //{
        //    get
        //    {
        //        return QueryKey + "=" + QueryValue;
        //    }
        //}
    }
}