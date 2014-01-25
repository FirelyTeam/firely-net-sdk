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
    /// An action that is performed on a patient
    /// </summary>
    [FhirType("Procedure", IsResource=true)]
    [DataContract]
    public partial class Procedure : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// The nature of the relationship with this procedure
        /// </summary>
        [FhirEnumeration("ProcedureRelationshipType")]
        public enum ProcedureRelationshipType
        {
            [EnumLiteral("caused-by")]
            CausedBy, // This procedure had to be performed because of the related one.
            [EnumLiteral("because-of")]
            BecauseOf, // This procedure caused the related one to be performed.
        }
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ProcedureRelatedItemComponent")]
        [DataContract]
        public partial class ProcedureRelatedItemComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// caused-by | because-of
            /// </summary>
            [FhirElement("type", Order=40)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Procedure.ProcedureRelationshipType> TypeElement { get; set; }
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Procedure.ProcedureRelationshipType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if(value == null)
                      TypeElement = null; 
                    else
                      TypeElement = new Code<Hl7.Fhir.Model.Procedure.ProcedureRelationshipType>(value);
                }
            }
            
            /// <summary>
            /// The related item - e.g. a procedure
            /// </summary>
            [FhirElement("target", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Target { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ProcedurePerformerComponent")]
        [DataContract]
        public partial class ProcedurePerformerComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// The reference to the practitioner
            /// </summary>
            [FhirElement("person", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Person { get; set; }
            
            /// <summary>
            /// The role the person was in
            /// </summary>
            [FhirElement("role", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Role { get; set; }
            
        }
        
        
        /// <summary>
        /// External Ids for this procedure
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier { get; set; }
        
        /// <summary>
        /// Who procedure was performed on
        /// </summary>
        [FhirElement("subject", Order=80)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject { get; set; }
        
        /// <summary>
        /// Identification of the procedure
        /// </summary>
        [FhirElement("type", Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type { get; set; }
        
        /// <summary>
        /// Precise location details
        /// </summary>
        [FhirElement("bodySite", Order=100)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> BodySite { get; set; }
        
        /// <summary>
        /// Reason procedure performed
        /// </summary>
        [FhirElement("indication", Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Indication { get; set; }
        
        /// <summary>
        /// The people who performed the procedure
        /// </summary>
        [FhirElement("performer", Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Procedure.ProcedurePerformerComponent> Performer { get; set; }
        
        /// <summary>
        /// The date the procedure was performed
        /// </summary>
        [FhirElement("date", Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.Period Date { get; set; }
        
        /// <summary>
        /// The encounter when procedure performed
        /// </summary>
        [FhirElement("encounter", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Encounter { get; set; }
        
        /// <summary>
        /// What was result of procedure?
        /// </summary>
        [FhirElement("outcome", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString OutcomeElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Outcome
        {
            get { return OutcomeElement != null ? OutcomeElement.Value : null; }
            set
            {
                if(value == null)
                  OutcomeElement = null; 
                else
                  OutcomeElement = new Hl7.Fhir.Model.FhirString(value);
            }
        }
        
        /// <summary>
        /// Any report that results from the procedure
        /// </summary>
        [FhirElement("report", Order=160)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Report { get; set; }
        
        /// <summary>
        /// Complication following the procedure
        /// </summary>
        [FhirElement("complication", Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Complication { get; set; }
        
        /// <summary>
        /// Instructions for follow up
        /// </summary>
        [FhirElement("followUp", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString FollowUpElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string FollowUp
        {
            get { return FollowUpElement != null ? FollowUpElement.Value : null; }
            set
            {
                if(value == null)
                  FollowUpElement = null; 
                else
                  FollowUpElement = new Hl7.Fhir.Model.FhirString(value);
            }
        }
        
        /// <summary>
        /// A procedure that is related to this one
        /// </summary>
        [FhirElement("relatedItem", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Procedure.ProcedureRelatedItemComponent> RelatedItem { get; set; }
        
        /// <summary>
        /// Additional information about procedure
        /// </summary>
        [FhirElement("notes", Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NotesElement { get; set; }
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Notes
        {
            get { return NotesElement != null ? NotesElement.Value : null; }
            set
            {
                if(value == null)
                  NotesElement = null; 
                else
                  NotesElement = new Hl7.Fhir.Model.FhirString(value);
            }
        }
        
    }
    
}
