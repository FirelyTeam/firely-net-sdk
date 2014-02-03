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
    /// Definition of a Medication
    /// </summary>
    [FhirType("Medication", IsResource=true)]
    [DataContract]
    public partial class Medication : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// Whether the medication is a product or a package
        /// </summary>
        [FhirEnumeration("MedicationKind")]
        public enum MedicationKind
        {
            [EnumLiteral("product")]
            Product, // The medication is a product.
            [EnumLiteral("package")]
            Package, // The medication is a package - a contained group of one of more products.
        }
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("MedicationPackageContentComponent")]
        [DataContract]
        public partial class MedicationPackageContentComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// A product in the package
            /// </summary>
            [FhirElement("item", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Item { get; set; }
            
            /// <summary>
            /// How many are in the package?
            /// </summary>
            [FhirElement("amount", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity Amount { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("MedicationPackageComponent")]
        [DataContract]
        public partial class MedicationPackageComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// E.g. box, vial, blister-pack
            /// </summary>
            [FhirElement("container", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Container { get; set; }
            
            /// <summary>
            /// What is  in the package?
            /// </summary>
            [FhirElement("content", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Medication.MedicationPackageContentComponent> Content { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("MedicationProductIngredientComponent")]
        [DataContract]
        public partial class MedicationProductIngredientComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// The product contained
            /// </summary>
            [FhirElement("item", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Item { get; set; }
            
            /// <summary>
            /// How much ingredient in product
            /// </summary>
            [FhirElement("amount", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Ratio Amount { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("MedicationProductComponent")]
        [DataContract]
        public partial class MedicationProductComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// powder | tablets | carton +
            /// </summary>
            [FhirElement("form", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Form { get; set; }
            
            /// <summary>
            /// Active or inactive ingredient
            /// </summary>
            [FhirElement("ingredient", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Medication.MedicationProductIngredientComponent> Ingredient { get; set; }
            
        }
        
        
        /// <summary>
        /// Common / Commercial name
        /// </summary>
        [FhirElement("name", Order=70)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Name
        {
            get { return NameElement != null ? NameElement.Value : null; }
            set
            {
                if(value == null)
                  NameElement = null; 
                else
                  NameElement = new Hl7.Fhir.Model.FhirString(value);
            }
        }
        
        /// <summary>
        /// Codes that identify this medication
        /// </summary>
        [FhirElement("code", Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Code { get; set; }
        
        /// <summary>
        /// True if a brand
        /// </summary>
        [FhirElement("isBrand", Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean IsBrandElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? IsBrand
        {
            get { return IsBrandElement != null ? IsBrandElement.Value : null; }
            set
            {
                if(value == null)
                  IsBrandElement = null; 
                else
                  IsBrandElement = new Hl7.Fhir.Model.FhirBoolean(value);
            }
        }
        
        /// <summary>
        /// Manufacturer of the item
        /// </summary>
        [FhirElement("manufacturer", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Manufacturer { get; set; }
        
        /// <summary>
        /// product | package
        /// </summary>
        [FhirElement("kind", Order=110)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Medication.MedicationKind> KindElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Medication.MedicationKind? Kind
        {
            get { return KindElement != null ? KindElement.Value : null; }
            set
            {
                if(value == null)
                  KindElement = null; 
                else
                  KindElement = new Code<Hl7.Fhir.Model.Medication.MedicationKind>(value);
            }
        }
        
        /// <summary>
        /// Administrable medication details
        /// </summary>
        [FhirElement("product", Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.Medication.MedicationProductComponent Product { get; set; }
        
        /// <summary>
        /// Details about packaged medications
        /// </summary>
        [FhirElement("package", Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.Medication.MedicationPackageComponent Package { get; set; }
        
    }
    
}
