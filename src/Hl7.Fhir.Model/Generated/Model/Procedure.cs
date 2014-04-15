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
// Generated on Tue, Apr 15, 2014 17:48+0200 for FHIR v0.80
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// An action that is performed on a patient
    /// </summary>
    [FhirType("Procedure", IsResource=true)]
    [DataContract]
    public partial class Procedure : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
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
        public partial class ProcedureRelatedItemComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// caused-by | because-of
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Procedure.ProcedureRelationshipType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            private Code<Hl7.Fhir.Model.Procedure.ProcedureRelationshipType> _TypeElement;
            
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
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// The related item - e.g. a procedure
            /// </summary>
            [FhirElement("target", InSummary=true, Order=50)]
            [References("AdverseReaction","AllergyIntolerance","CarePlan","Condition","DeviceObservationReport","DiagnosticReport","FamilyHistory","ImagingStudy","Immunization","ImmunizationRecommendation","MedicationAdministration","MedicationDispense","MedicationPrescription","MedicationStatement","Observation","Procedure")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Target
            {
                get { return _Target; }
                set { _Target = value; OnPropertyChanged("Target"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Target;
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ProcedurePerformerComponent")]
        [DataContract]
        public partial class ProcedurePerformerComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// The reference to the practitioner
            /// </summary>
            [FhirElement("person", InSummary=true, Order=40)]
            [References("Practitioner")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Person
            {
                get { return _Person; }
                set { _Person = value; OnPropertyChanged("Person"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Person;
            
            /// <summary>
            /// The role the person was in
            /// </summary>
            [FhirElement("role", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Role
            {
                get { return _Role; }
                set { _Role = value; OnPropertyChanged("Role"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _Role;
            
        }
        
        
        /// <summary>
        /// External Ids for this procedure
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Who procedure was performed on
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=80)]
        [References("Patient")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Identification of the procedure
        /// </summary>
        [FhirElement("type", InSummary=true, Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        private Hl7.Fhir.Model.CodeableConcept _Type;
        
        /// <summary>
        /// Precise location details
        /// </summary>
        [FhirElement("bodySite", InSummary=true, Order=100)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> BodySite
        {
            get { return _BodySite; }
            set { _BodySite = value; OnPropertyChanged("BodySite"); }
        }
        private List<Hl7.Fhir.Model.CodeableConcept> _BodySite;
        
        /// <summary>
        /// Reason procedure performed
        /// </summary>
        [FhirElement("indication", InSummary=true, Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Indication
        {
            get { return _Indication; }
            set { _Indication = value; OnPropertyChanged("Indication"); }
        }
        private List<Hl7.Fhir.Model.CodeableConcept> _Indication;
        
        /// <summary>
        /// The people who performed the procedure
        /// </summary>
        [FhirElement("performer", InSummary=true, Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Procedure.ProcedurePerformerComponent> Performer
        {
            get { return _Performer; }
            set { _Performer = value; OnPropertyChanged("Performer"); }
        }
        private List<Hl7.Fhir.Model.Procedure.ProcedurePerformerComponent> _Performer;
        
        /// <summary>
        /// The date the procedure was performed
        /// </summary>
        [FhirElement("date", InSummary=true, Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.Period Date
        {
            get { return _Date; }
            set { _Date = value; OnPropertyChanged("Date"); }
        }
        private Hl7.Fhir.Model.Period _Date;
        
        /// <summary>
        /// The encounter when procedure performed
        /// </summary>
        [FhirElement("encounter", InSummary=true, Order=140)]
        [References("Encounter")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Encounter
        {
            get { return _Encounter; }
            set { _Encounter = value; OnPropertyChanged("Encounter"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Encounter;
        
        /// <summary>
        /// What was result of procedure?
        /// </summary>
        [FhirElement("outcome", InSummary=true, Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString OutcomeElement
        {
            get { return _OutcomeElement; }
            set { _OutcomeElement = value; OnPropertyChanged("OutcomeElement"); }
        }
        private Hl7.Fhir.Model.FhirString _OutcomeElement;
        
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
                OnPropertyChanged("Outcome");
            }
        }
        
        /// <summary>
        /// Any report that results from the procedure
        /// </summary>
        [FhirElement("report", Order=160)]
        [References("DiagnosticReport")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Report
        {
            get { return _Report; }
            set { _Report = value; OnPropertyChanged("Report"); }
        }
        private List<Hl7.Fhir.Model.ResourceReference> _Report;
        
        /// <summary>
        /// Complication following the procedure
        /// </summary>
        [FhirElement("complication", Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Complication
        {
            get { return _Complication; }
            set { _Complication = value; OnPropertyChanged("Complication"); }
        }
        private List<Hl7.Fhir.Model.CodeableConcept> _Complication;
        
        /// <summary>
        /// Instructions for follow up
        /// </summary>
        [FhirElement("followUp", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString FollowUpElement
        {
            get { return _FollowUpElement; }
            set { _FollowUpElement = value; OnPropertyChanged("FollowUpElement"); }
        }
        private Hl7.Fhir.Model.FhirString _FollowUpElement;
        
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
                OnPropertyChanged("FollowUp");
            }
        }
        
        /// <summary>
        /// A procedure that is related to this one
        /// </summary>
        [FhirElement("relatedItem", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Procedure.ProcedureRelatedItemComponent> RelatedItem
        {
            get { return _RelatedItem; }
            set { _RelatedItem = value; OnPropertyChanged("RelatedItem"); }
        }
        private List<Hl7.Fhir.Model.Procedure.ProcedureRelatedItemComponent> _RelatedItem;
        
        /// <summary>
        /// Additional information about procedure
        /// </summary>
        [FhirElement("notes", Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NotesElement
        {
            get { return _NotesElement; }
            set { _NotesElement = value; OnPropertyChanged("NotesElement"); }
        }
        private Hl7.Fhir.Model.FhirString _NotesElement;
        
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
                OnPropertyChanged("Notes");
            }
        }
        
    }
    
}
