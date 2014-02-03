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
// Generated on Mon, Feb 3, 2014 11:56+0100 for FHIR v0.80
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A homogeneous material with a definite composition
    /// </summary>
    [FhirType("Substance", IsResource=true)]
    [DataContract]
    public partial class Substance : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// null
        /// </summary>
        [FhirType("SubstanceIngredientComponent")]
        [DataContract]
        public partial class SubstanceIngredientComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Optional amount (concentration)
            /// </summary>
            [FhirElement("quantity", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Ratio Quantity { get; set; }
            
            /// <summary>
            /// A component of the substance
            /// </summary>
            [FhirElement("substance", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Substance { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("SubstanceInstanceComponent")]
        [DataContract]
        public partial class SubstanceInstanceComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Identifier of the package/container
            /// </summary>
            [FhirElement("identifier", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier { get; set; }
            
            /// <summary>
            /// When no longer valid to use
            /// </summary>
            [FhirElement("expiry", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime ExpiryElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Expiry
            {
                get { return ExpiryElement != null ? ExpiryElement.Value : null; }
                set
                {
                    if(value == null)
                      ExpiryElement = null; 
                    else
                      ExpiryElement = new Hl7.Fhir.Model.FhirDateTime(value);
                }
            }
            
            /// <summary>
            /// Amount of substance in the package
            /// </summary>
            [FhirElement("quantity", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity Quantity { get; set; }
            
        }
        
        
        /// <summary>
        /// What kind of substance this is
        /// </summary>
        [FhirElement("type", Order=70)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type { get; set; }
        
        /// <summary>
        /// Textual description of the substance, comments
        /// </summary>
        [FhirElement("description", Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Description
        {
            get { return DescriptionElement != null ? DescriptionElement.Value : null; }
            set
            {
                if(value == null)
                  DescriptionElement = null; 
                else
                  DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
            }
        }
        
        /// <summary>
        /// If this describes a specific package/container of the substance
        /// </summary>
        [FhirElement("instance", Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.Substance.SubstanceInstanceComponent Instance { get; set; }
        
        /// <summary>
        /// Composition information about the substance
        /// </summary>
        [FhirElement("ingredient", Order=100)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Substance.SubstanceIngredientComponent> Ingredient { get; set; }
        
    }
    
}
