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


using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Hl7.Fhir.Introspection;
using System.Runtime.Serialization;

namespace Hl7.Fhir.Model
{
    [System.Diagnostics.DebuggerDisplay(@"\{{Value}}")] // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
    public partial class Code
    {
        public static bool IsValidValue(string value)
        {
            return Regex.IsMatch(value, "^" + Code.PATTERN + "$", RegexOptions.Singleline);
        }
    }

    [FhirType("codeOfT")]
    [DataContract]
    [System.Diagnostics.DebuggerDisplay(@"\{{Value}}")] // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
    public class Code<T> : Element where T : struct
    {
        // Primitive value of element
        [FhirElement("value", InSummary=true, IsPrimitiveValue=true)]
        [DataMember]
        public T? Value { get; set; }

        public Code() : this(null) {}

        public Code(T? value)
        {
#if PORTABLE45
			if (!typeof(T).GetTypeInfo().IsEnum) 
                throw new ArgumentException("T must be an enumerated type");
#else
            if (!typeof(T).IsEnum) 
                throw new ArgumentException("T must be an enumerated type");
#endif
            Value = value;
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Code<T>;

            if (dest != null)
            {
                base.CopyTo(dest);
                if (Value != null) dest.Value = Value;
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }

        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Code<T>());
        }
    }
}
