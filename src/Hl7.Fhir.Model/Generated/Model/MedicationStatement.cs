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
    /// Administration of medication to a patient
    /// </summary>
    [FhirType("MedicationStatement", IsResource=true)]
    [DataContract]
    public partial class MedicationStatement : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// null
        /// </summary>
        [FhirType("MedicationStatementDosageComponent")]
        [DataContract]
        public partial class MedicationStatementDosageComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// When/how often was medication taken?
            /// </summary>
            [FhirElement("timing", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Schedule Timing { get; set; }
            
            /// <summary>
            /// Take "as needed" f(or x)
            /// </summary>
            [FhirElement("asNeeded", Order=50, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.CodeableConcept))]
            [DataMember]
            public Hl7.Fhir.Model.Element AsNeeded { get; set; }
            
            /// <summary>
            /// Where on body was medication administered?
            /// </summary>
            [FhirElement("site", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Site { get; set; }
            
            /// <summary>
            /// How did the medication enter the body?
            /// </summary>
            [FhirElement("route", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Route { get; set; }
            
            /// <summary>
            /// Technique used to administer medication
            /// </summary>
            [FhirElement("method", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Method { get; set; }
            
            /// <summary>
            /// Amount administered in one dose
            /// </summary>
            [FhirElement("quantity", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity Quantity { get; set; }
            
            /// <summary>
            /// Dose quantity per unit of time
            /// </summary>
            [FhirElement("rate", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.Ratio Rate { get; set; }
            
            /// <summary>
            /// Maximum dose that was consumed per unit of time
            /// </summary>
            [FhirElement("maxDosePerPeriod", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.Ratio MaxDosePerPeriod { get; set; }
            
        }
        
        
        /// <summary>
        /// External Identifier
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
        
        /// <summary>
        /// Who was/is taking medication
        /// </summary>
        [FhirElement("patient", Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient { get; set; }
        
        /// <summary>
        /// True if medication is/was not being taken
        /// </summary>
        [FhirElement("wasNotGiven", Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean WasNotGivenElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? WasNotGiven
        {
            get { return WasNotGivenElement != null ? WasNotGivenElement.Value : null; }
            set
            {
                if(value == null)
                  WasNotGivenElement = null; 
                else
                  WasNotGivenElement = new Hl7.Fhir.Model.FhirBoolean(value);
            }
        }
        
        /// <summary>
        /// True if asserting medication was not given
        /// </summary>
        [FhirElement("reasonNotGiven", Order=100)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ReasonNotGiven { get; set; }
        
        /// <summary>
        /// Over what period was medication consumed?
        /// </summary>
        [FhirElement("whenGiven", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.Period WhenGiven { get; set; }
        
        /// <summary>
        /// What medication was taken?
        /// </summary>
        [FhirElement("medication", Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Medication { get; set; }
        
        /// <summary>
        /// E.g. infusion pump
        /// </summary>
        [FhirElement("device", Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Device { get; set; }
        
        /// <summary>
        /// Details of how medication was taken
        /// </summary>
        [FhirElement("dosage", Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.MedicationStatement.MedicationStatementDosageComponent> Dosage { get; set; }
        
    }
    
}
