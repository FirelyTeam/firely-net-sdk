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
using Hl7.Fhir.Specification;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using COVE = Hl7.Fhir.Validation.CodedValidationException;
using S = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.Model
{
    [Serializable]
    [FhirType("codeOfT")]
    [DataContract]
    [System.Diagnostics.DebuggerDisplay(@"\{Value={Value}}")]
    public class Code<T> : PrimitiveType, INullableValue<T>, ISystemAndCode where T : struct, Enum
    {
        static Code()
        {
            if (!typeof(T).IsEnum())
                throw new ArgumentException("T must be an enumerated type");
        }

        public override string TypeName => "code";

        public Code() : this(null) { }

        public Code(T? value)
        {
            Value = value;
        }

        // Primitive value of element
        [FhirElement("value", IsPrimitiveValue = true, XmlSerialization = XmlRepresentation.XmlAttr, InSummary = true, Order = 30)]
        [DataMember]
        public T? Value
        {
            get => TryParseObjectValue(out var value)
                    ? value
                    : throw new InvalidCastException($"Value '{ObjectValue}' cannot be cast to a member of enumeration {typeof(T).Name}.");
            set
            {
                ObjectValue = value?.GetLiteral();
                OnPropertyChanged("Value");
            }
        }

        internal bool TryParseObjectValue(out T? value)
        {
            value = default;

            if (ObjectValue is string s && EnumUtility.ParseLiteral<T>(s) is T parsed)
            {
                value = parsed;
                return true;
            }
            else return ObjectValue is null;
        }

        string ISystemAndCode.System => Value?.GetSystem();

        string ISystemAndCode.Code => Value?.GetLiteral();

        public S.Code ToSystemCode() => new(Value?.GetSystem(), Value?.GetLiteral(), display: null, version: null);

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var baseResults = base.Validate(validationContext);

            if (TryParseObjectValue(out _))
                return baseResults;
            else
            {
                var result = COVE.INVALID_CODED_VALUE.AsResult(validationContext, ObjectValue, EnumUtility.GetName<T>());
#if NET452
                return baseResults.Concat(new[] { result });
#else
                return baseResults.Append(result);
#endif
            }
        }
    }
}
