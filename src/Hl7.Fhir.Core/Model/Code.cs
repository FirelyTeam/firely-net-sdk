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
using System.Text.RegularExpressions;

using Hl7.Fhir.Introspection;
using System.Runtime.Serialization;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;

namespace Hl7.Fhir.Model
{
#if NET45
    [Serializable]
#endif
    [System.Diagnostics.DebuggerDisplay(@"\{{Value}}")] // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
    public partial class Code : IStringValue
    {
        public static bool IsValidValue(string value)
        {
            return Regex.IsMatch(value, "^" + Code.PATTERN + "$", RegexOptions.Singleline);
        }
    }

    /// <summary>
    /// Provides a way to access the system and code from a Code&lt;T&gt; derived class, without having to mess
    /// about with the generic types/additional nasty reflection
    /// </summary>
    public interface ISystemAndCode
    {
        string System { get; }
        string Code { get; }
    }

#if NET45
    [Serializable]
#endif
    [FhirType("codeOfT")]
    [DataContract]
    [System.Diagnostics.DebuggerDisplay(@"\{{Value}}")] // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
    public class Code<T> : Primitive<T>, INullableValue<T>, ISystemAndCode where T : struct
    {
        static Code()
        {
            if (!typeof(T).IsEnum())
                throw new ArgumentException("T must be an enumerated type");
        }


        // Primitive value of element
        [FhirElement("value", IsPrimitiveValue = true, XmlSerialization = XmlRepresentation.XmlAttr, InSummary = true, Order = 30)]
        [DataMember]
        public T? Value
        {
            get
            {
                if (ObjectValue != null)
                    return EnumUtility.ParseLiteral<T>((string)ObjectValue);
                else
                    return null;
            }

            set
            {
                if (value != null)
                    ObjectValue = ((Enum)(object)value).GetLiteral();
                else
                    ObjectValue = null;
            }
        }

        public Code() : this(null) {}

        public Code(T? value)
        {
            Value = value;
        }

        [NotMapped]
        public override string TypeName
        {
            get { return "code"; }
        }

        string ISystemAndCode.System => ((Enum)(object)Value).GetSystem();

        string ISystemAndCode.Code => ObjectValue as string; // this is the literal

        //public override IDeepCopyable CopyTo(IDeepCopyable other)
        //{
        //    var dest = other as Code<T>;

        //    if (dest != null)
        //    {
        //        base.CopyTo(dest);
        //        if (RawValue != null) dest.RawValue = RawValue;
        //        return dest;
        //    }
        //    else
        //        throw new ArgumentException("Can only copy to an object of the same type", "other");
        //}

        //public override IDeepCopyable DeepCopy()
        //{
        //    return CopyTo(new Code<T>());
        //}


        //public override bool Matches(IDeepComparable other)
        //{
        //    throw new NotImplementedException();
        //}

        //public override bool IsExactly(IDeepComparable other)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
