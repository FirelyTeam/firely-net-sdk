using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;

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

//
// Generated on Fri, Jan 24, 2014 09:44-0600 for FHIR v0.12
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// An identifier intended for computation
    /// </summary>
    [FhirType("Identifier")]
    [DataContract]
    public partial class Identifier : Hl7.Fhir.Model.Element
    {
        /// <summary>
        /// Identifies the purpose for this identifier, if known
        /// </summary>
        [FhirEnumeration("IdentifierUse")]
        public enum IdentifierUse
        {
            [EnumLiteral("usual")]
            Usual, // the identifier recommended for display and use in real-world interactions.
            [EnumLiteral("official")]
            Official, // the identifier considered to be most trusted for the identification of this item.
            [EnumLiteral("temp")]
            Temp, // A temporary identifier.
            [EnumLiteral("secondary")]
            Secondary, // An identifier that was assigned in secondary use - it serves to identify the object in a relative context, but cannot be consistently assigned to the same object again in a different context.
        }
        
        /// <summary>
        /// usual | official | temp | secondary (If known)
        /// </summary>
        [FhirElement("use", Order=40)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Identifier.IdentifierUse> UseElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Identifier.IdentifierUse? Use
        {
            get { return UseElement != null ? UseElement.Value : null; }
            set
            {
                if(value == null)
                  UseElement = null; 
                else
                  UseElement = new Code<Hl7.Fhir.Model.Identifier.IdentifierUse>(value);
            }
        }
        
        /// <summary>
        /// Description of identifier
        /// </summary>
        [FhirElement("label", Order=50)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString LabelElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Label
        {
            get { return LabelElement != null ? LabelElement.Value : null; }
            set
            {
                if(value == null)
                  LabelElement = null; 
                else
                  LabelElement = new Hl7.Fhir.Model.FhirString(value);
            }
        }
        
        /// <summary>
        /// The namespace for the identifier
        /// </summary>
        [FhirElement("system", Order=60)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri SystemElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public System.Uri System
        {
            get { return SystemElement != null ? SystemElement.Value : null; }
            set
            {
                if(value == null)
                  SystemElement = null; 
                else
                  SystemElement = new Hl7.Fhir.Model.FhirUri(value);
            }
        }
        
        /// <summary>
        /// The value that is unique
        /// </summary>
        [FhirElement("value", Order=70)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ValueElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Value
        {
            get { return ValueElement != null ? ValueElement.Value : null; }
            set
            {
                if(value == null)
                  ValueElement = null; 
                else
                  ValueElement = new Hl7.Fhir.Model.FhirString(value);
            }
        }
        
        /// <summary>
        /// Time period when id is/was valid for use
        /// </summary>
        [FhirElement("period", Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period { get; set; }
        
        /// <summary>
        /// Organization that issued id (may be just text)
        /// </summary>
        [FhirElement("assigner", Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Assigner { get; set; }
        
    }
    
}
