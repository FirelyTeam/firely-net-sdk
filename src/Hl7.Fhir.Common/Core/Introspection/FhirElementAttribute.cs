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

using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Validation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Introspection
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class FhirElementAttribute : ValidationAttribute
    {
        readonly string name;

        public FhirElementAttribute(string name)
        {
            this.name = name;
            this.XmlSerialization = XmlRepresentation.None;
            this.Choice = ChoiceType.None;
        }

        public ChoiceType Choice { get; set; }

        public string Name
        {
            get { return name; }
        }

        public bool IsPrimitiveValue { get; set; }

        public XmlRepresentation XmlSerialization { get; set; }

        public int Order { get; set; }

        public bool InSummary { get; set; }

        public Type TypeRedirect { get; set; }

        // This attribute is a subclass of ValidationAttribute so that IsValid() is called on every 
        // FhirElement while validating. This allows us to extend validation into each FhirElement,
        // while normally, the .NET validation will only validate one level, but will not recurse
        // into each element. This is controllable by the SetValidateRecursively extension of the
        // ValidationContext
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(validationContext == null) throw new ArgumentNullException("validationContext");

            if (value == null) return ValidationResult.Success;

            // If we should not validate 'value's elements, return immediately
            if (!validationContext.ValidateRecursively()) return ValidationResult.Success;

            IEnumerable list = value as IEnumerable;
            var result = new List<ValidationResult>();

            // If value is an enumerated type, validate all elements of the list
            if (list != null)
            {
                foreach (var element in list)
                {
                    if (element != null)
                    {
                        validateElement(element, validationContext, result);
                    }
                }
            }
            else
            {
                validateElement(value, validationContext, result);
            }

            return result.FirstOrDefault();                
        }

        private static void validateElement(object value, ValidationContext validationContext, List<ValidationResult> result)
        {
            DotNetAttributeValidation.TryValidate(value, result, validationContext.ValidateRecursively());
        }
    }
}
