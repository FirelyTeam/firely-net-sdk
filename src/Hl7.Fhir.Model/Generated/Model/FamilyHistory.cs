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
    /// Information about patient's relatives, relevant for patient
    /// </summary>
    [FhirType("FamilyHistory", IsResource=true)]
    [DataContract]
    public partial class FamilyHistory : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// null
        /// </summary>
        [FhirType("FamilyHistoryRelationConditionComponent")]
        [DataContract]
        public partial class FamilyHistoryRelationConditionComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Condition suffered by relation
            /// </summary>
            [FhirElement("type", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type { get; set; }
            
            /// <summary>
            /// deceased | permanent disability | etc.
            /// </summary>
            [FhirElement("outcome", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Outcome { get; set; }
            
            /// <summary>
            /// When condition first manifested
            /// </summary>
            [FhirElement("onset", Order=60, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Age),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.FhirString))]
            [DataMember]
            public Hl7.Fhir.Model.Element Onset { get; set; }
            
            /// <summary>
            /// Extra information about condition
            /// </summary>
            [FhirElement("note", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NoteElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Note
            {
                get { return NoteElement != null ? NoteElement.Value : null; }
                set
                {
                    if(value == null)
                      NoteElement = null; 
                    else
                      NoteElement = new Hl7.Fhir.Model.FhirString(value);
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("FamilyHistoryRelationComponent")]
        [DataContract]
        public partial class FamilyHistoryRelationComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// The family member described
            /// </summary>
            [FhirElement("name", Order=40)]
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
            /// Relationship to the subject
            /// </summary>
            [FhirElement("relationship", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Relationship { get; set; }
            
            /// <summary>
            /// (approximate) date of birth
            /// </summary>
            [FhirElement("born", Order=60, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirString))]
            [DataMember]
            public Hl7.Fhir.Model.Element Born { get; set; }
            
            /// <summary>
            /// Dead? How old/when?
            /// </summary>
            [FhirElement("deceased", Order=70, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Age),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirString))]
            [DataMember]
            public Hl7.Fhir.Model.Element Deceased { get; set; }
            
            /// <summary>
            /// General note about related person
            /// </summary>
            [FhirElement("note", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NoteElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Note
            {
                get { return NoteElement != null ? NoteElement.Value : null; }
                set
                {
                    if(value == null)
                      NoteElement = null; 
                    else
                      NoteElement = new Hl7.Fhir.Model.FhirString(value);
                }
            }
            
            /// <summary>
            /// Condition that the related person had
            /// </summary>
            [FhirElement("condition", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FamilyHistory.FamilyHistoryRelationConditionComponent> Condition { get; set; }
            
        }
        
        
        /// <summary>
        /// External Id(s) for this record
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
        
        /// <summary>
        /// Patient history is about
        /// </summary>
        [FhirElement("subject", Order=80)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject { get; set; }
        
        /// <summary>
        /// Additional details not covered elsewhere
        /// </summary>
        [FhirElement("note", Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NoteElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Note
        {
            get { return NoteElement != null ? NoteElement.Value : null; }
            set
            {
                if(value == null)
                  NoteElement = null; 
                else
                  NoteElement = new Hl7.Fhir.Model.FhirString(value);
            }
        }
        
        /// <summary>
        /// Relative described by history
        /// </summary>
        [FhirElement("relation", Order=100)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FamilyHistory.FamilyHistoryRelationComponent> Relation { get; set; }
        
    }
    
}
