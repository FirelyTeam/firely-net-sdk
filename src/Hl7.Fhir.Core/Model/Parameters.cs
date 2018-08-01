/*
  Copyright (c) 2011-2012, HL7, Inc
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



using Hl7.Fhir.Introspection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Hl7.Fhir.Support;
using System.Diagnostics;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Model
{
    /// <summary>
    /// This is the Parameters partial class that adds all the specific functionality of a Parameters to the model
    /// </summary>
    [System.Diagnostics.DebuggerDisplay(@"\{Count={_Parameter != null ? _Parameter.Count : 0}}")]
    public partial class Parameters
    {   
        /// <summary>
        /// Add a parameter with a given name and value.
        /// </summary>
        /// <param name="name">The name of the parameter</param>
        /// <param name="value">The value of the parameter as a FHIR datatype or Resource</param>
        /// <returns>this (Parameters), so you can chain AddParameter calls</returns>
        public Parameters Add(string name, Base value)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            if (value != null)
            {
                Parameter.Add(
                    new ParameterComponent()
                    {
                        Name = name,
                        Value = value as Element,
                        Resource = value as Resource
                    });
            }

            return this;
        }


        /// <summary>
        /// Add a parameter with a given name and tuple value.
        /// </summary>
        /// <param name="name">The name of the parameter</param>
        /// <param name="tuples">The value of the parameter as a list of tuples of (name,FHIR datatype or Resource)</param>
        /// <returns>this (Parameters), so you can chain AddParameter calls</returns>
        public Parameters Add(string name, IEnumerable<Tuple<string,Base>> tuples)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (tuples == null) throw new ArgumentNullException("tuples");

            var newParam = new ParameterComponent() { Name = name };
            
            foreach (var tuple in tuples)
            {
                var newPart = new ParameterComponent() { Name = tuple.Item1 };
                newParam.Part.Add(newPart);

                if (tuple.Item2 is Element)
                    newPart.Value = (Element)tuple.Item2;
                else
                {
                    //TODO: Due to an error in the jan2015 version of DSTU2, this is not yet possible
                    //newPart.Resource = (Resource)tuple.Item2;
                    throw Error.NotImplemented("Jan 2015 DSTU2 does not support resource values for tuples parameters");
                }
            }

            Parameter.Add(newParam);

            return this;
        }


        /// <summary>
        /// Remove a parameter with a given name.
        /// </summary>
        /// <param name="name">The name of the parameter</param>
        /// <param name="matchPrefix">If true, will remove all parameters which begin with the string given in the "name" parameter</param>
        /// <remarks>No exception is thrown when the parameters were not found and nothing was removed.</remarks>
        public void Remove(string name, bool matchPrefix = false)
        {
            if (name == null) throw new ArgumentNullException("name");

            foreach(var hit in Get(name,matchPrefix).ToList()) Parameter.Remove(hit);
        }


        /// <summary>
        /// Searches for a parameter with the given name, and returns the matching parameter(s)
        /// </summary>
        /// <param name="name">The name of the parameter</param>
        /// <param name="matchPrefix">If true, will remove all parameters which begin with the string given in the "name" parameter</param>
        public IEnumerable<ParameterComponent> Get(string name, bool matchPrefix = false)
        {
            if (name == null) throw new ArgumentNullException("name");

            if (matchPrefix)
                return Parameter.Where(p => p.Name.StartsWith(name)).ToList();
            else
                return Parameter.Where(p => p.Name == name).ToList();
        }

        /// <summary>
        /// Searches for a parameter with the given name, and returns the matching parameter(s)
        /// </summary>
        /// <param name="name">The name of the parameter</param>
        /// <param name="matchPrefix">If true, will remove all parameters which begin with the string given in the "name" parameter</param>
        public ParameterComponent GetSingle(string name, bool matchPrefix = false)
        {
            if (name == null) throw new ArgumentNullException("name");

            return Get(name, matchPrefix).SingleOrDefault();
        }

        [NotMapped]
        public ParameterComponent this[string name] => GetSingle(name);

        /// <summary>
        /// Returns the Value property of the requested parameter casted to the requested type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="matchPrefix"></param>
        /// <returns></returns>
        public T GetSingleValue<T>(string name, bool matchPrefix = false) where T : Element
        {
            if (name == null) throw new ArgumentNullException("name");
            ParameterComponent p = Get(name, matchPrefix).SingleOrDefault();
            if (p == null)
                return null;
            return p.Value as T;
        }

        [System.Diagnostics.DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")] // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
        public partial class ParameterComponent
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            [NotMapped]
            private string DebuggerDisplay
            {
                get
                {
                    return String.Format("Name=\"{0}\" Value.Type={1}", this.Name, this.Value);
                }
            }
        }

    }
}
