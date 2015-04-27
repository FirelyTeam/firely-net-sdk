using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;

/*
  Copyright (c) 2011+, HL7, Inc.
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
// Generated on Thu, Apr 2, 2015 14:21+0200 for FHIR v0.5.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// An action that was or is currently being performed on a patient
    /// </summary>
    [FhirType("Procedure", IsResource=true)]
    [DataContract]
    public partial class Procedure : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Procedure; } }
        [NotMapped]
        public override string TypeName { get { return "Procedure"; } }
        
        /// <summary>
        /// The nature of the relationship with this procedure
        /// </summary>
        [FhirEnumeration("ProcedureRelationshipType")]
        public enum ProcedureRelationshipType
        {
            /// <summary>
            /// This procedure had to be performed because of the related one.
            /// </summary>
            [EnumLiteral("caused-by")]
            CausedBy,
            /// <summary>
            /// This procedure caused the related one to be performed.
            /// </summary>
            [EnumLiteral("because-of")]
            BecauseOf,
        }
        
        /// <summary>
        /// A code specifying the state of the procedure record
        /// </summary>
        [FhirEnumeration("ProcedureStatus")]
        public enum ProcedureStatus
        {
            /// <summary>
            /// The procedure is still occurring.
            /// </summary>
            [EnumLiteral("in-progress")]
            InProgress,
            /// <summary>
            /// The procedure was terminated without completing successfully.
            /// </summary>
            [EnumLiteral("aborted")]
            Aborted,
            /// <summary>
            /// All actions involved in the procedure have taken place.
            /// </summary>
            [EnumLiteral("completed")]
            Completed,
            /// <summary>
            /// The statement was entered in error and Is not valid.
            /// </summary>
            [EnumLiteral("entered-in-error")]
            EnteredInError,
        }
        
        [FhirType("ProcedureBodySiteComponent")]
        [DataContract]
        public partial class ProcedureBodySiteComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ProcedureBodySiteComponent"; } }
            
            /// <summary>
            /// Precise location details
            /// </summary>
            [FhirElement("site", InSummary=true, Order=40, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Site
            {
                get { return _Site; }
                set { _Site = value; OnPropertyChanged("Site"); }
            }
            
            private Hl7.Fhir.Model.Element _Site;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ProcedureBodySiteComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Site != null) dest.Site = (Hl7.Fhir.Model.Element)Site.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ProcedureBodySiteComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ProcedureBodySiteComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Site, otherT.Site)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ProcedureBodySiteComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Site, otherT.Site)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ProcedureDeviceComponent")]
        [DataContract]
        public partial class ProcedureDeviceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ProcedureDeviceComponent"; } }
            
            /// <summary>
            /// Kind of change to device
            /// </summary>
            [FhirElement("action", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Action
            {
                get { return _Action; }
                set { _Action = value; OnPropertyChanged("Action"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Action;
            
            /// <summary>
            /// Device that was changed
            /// </summary>
            [FhirElement("manipulated", InSummary=true, Order=50)]
            [References("Device")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Manipulated
            {
                get { return _Manipulated; }
                set { _Manipulated = value; OnPropertyChanged("Manipulated"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Manipulated;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ProcedureDeviceComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Action != null) dest.Action = (Hl7.Fhir.Model.CodeableConcept)Action.DeepCopy();
                    if(Manipulated != null) dest.Manipulated = (Hl7.Fhir.Model.ResourceReference)Manipulated.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ProcedureDeviceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ProcedureDeviceComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Action, otherT.Action)) return false;
                if( !DeepComparable.Matches(Manipulated, otherT.Manipulated)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ProcedureDeviceComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Action, otherT.Action)) return false;
                if( !DeepComparable.IsExactly(Manipulated, otherT.Manipulated)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ProcedureRelatedItemComponent")]
        [DataContract]
        public partial class ProcedureRelatedItemComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ProcedureRelatedItemComponent"; } }
            
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
            
            /// <summary>
            /// caused-by | because-of
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            [References("AllergyIntolerance","CarePlan","Condition","DiagnosticReport","FamilyMemberHistory","ImagingStudy","Immunization","ImmunizationRecommendation","MedicationAdministration","MedicationDispense","MedicationPrescription","MedicationStatement","Observation","Procedure")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Target
            {
                get { return _Target; }
                set { _Target = value; OnPropertyChanged("Target"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Target;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ProcedureRelatedItemComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.Procedure.ProcedureRelationshipType>)TypeElement.DeepCopy();
                    if(Target != null) dest.Target = (Hl7.Fhir.Model.ResourceReference)Target.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ProcedureRelatedItemComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ProcedureRelatedItemComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(Target, otherT.Target)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ProcedureRelatedItemComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(Target, otherT.Target)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ProcedurePerformerComponent")]
        [DataContract]
        public partial class ProcedurePerformerComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ProcedurePerformerComponent"; } }
            
            /// <summary>
            /// The reference to the practitioner
            /// </summary>
            [FhirElement("person", InSummary=true, Order=40)]
            [References("Practitioner","Patient","RelatedPerson")]
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ProcedurePerformerComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Person != null) dest.Person = (Hl7.Fhir.Model.ResourceReference)Person.DeepCopy();
                    if(Role != null) dest.Role = (Hl7.Fhir.Model.CodeableConcept)Role.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ProcedurePerformerComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ProcedurePerformerComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Person, otherT.Person)) return false;
                if( !DeepComparable.Matches(Role, otherT.Role)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ProcedurePerformerComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Person, otherT.Person)) return false;
                if( !DeepComparable.IsExactly(Role, otherT.Role)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// External Ids for this procedure
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Who procedure was performed on
        /// </summary>
        [FhirElement("patient", InSummary=true, Order=100)]
        [References("Patient")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient
        {
            get { return _Patient; }
            set { _Patient = value; OnPropertyChanged("Patient"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Patient;
        
        /// <summary>
        /// in-progress | aborted | completed | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary=true, Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Procedure.ProcedureStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Procedure.ProcedureStatus> _StatusElement;
        
        /// <summary>
        /// in-progress | aborted | completed | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Procedure.ProcedureStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.Procedure.ProcedureStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Classification of the procedure
        /// </summary>
        [FhirElement("category", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Category
        {
            get { return _Category; }
            set { _Category = value; OnPropertyChanged("Category"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Category;
        
        /// <summary>
        /// Identification of the procedure
        /// </summary>
        [FhirElement("type", InSummary=true, Order=130)]
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
        [FhirElement("bodySite", InSummary=true, Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Procedure.ProcedureBodySiteComponent> BodySite
        {
            get { if(_BodySite==null) _BodySite = new List<Hl7.Fhir.Model.Procedure.ProcedureBodySiteComponent>(); return _BodySite; }
            set { _BodySite = value; OnPropertyChanged("BodySite"); }
        }
        
        private List<Hl7.Fhir.Model.Procedure.ProcedureBodySiteComponent> _BodySite;
        
        /// <summary>
        /// Reason procedure performed
        /// </summary>
        [FhirElement("indication", InSummary=true, Order=150)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Indication
        {
            get { if(_Indication==null) _Indication = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Indication; }
            set { _Indication = value; OnPropertyChanged("Indication"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Indication;
        
        /// <summary>
        /// The people who performed the procedure
        /// </summary>
        [FhirElement("performer", InSummary=true, Order=160)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Procedure.ProcedurePerformerComponent> Performer
        {
            get { if(_Performer==null) _Performer = new List<Hl7.Fhir.Model.Procedure.ProcedurePerformerComponent>(); return _Performer; }
            set { _Performer = value; OnPropertyChanged("Performer"); }
        }
        
        private List<Hl7.Fhir.Model.Procedure.ProcedurePerformerComponent> _Performer;
        
        /// <summary>
        /// Date/Period the procedure was performed
        /// </summary>
        [FhirElement("performed", InSummary=true, Order=170, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
        [DataMember]
        public Hl7.Fhir.Model.Element Performed
        {
            get { return _Performed; }
            set { _Performed = value; OnPropertyChanged("Performed"); }
        }
        
        private Hl7.Fhir.Model.Element _Performed;
        
        /// <summary>
        /// The encounter when procedure performed
        /// </summary>
        [FhirElement("encounter", InSummary=true, Order=180)]
        [References("Encounter")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Encounter
        {
            get { return _Encounter; }
            set { _Encounter = value; OnPropertyChanged("Encounter"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Encounter;
        
        /// <summary>
        /// Where the procedure happened
        /// </summary>
        [FhirElement("location", InSummary=true, Order=190)]
        [References("Location")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Location
        {
            get { return _Location; }
            set { _Location = value; OnPropertyChanged("Location"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Location;
        
        /// <summary>
        /// What was result of procedure?
        /// </summary>
        [FhirElement("outcome", InSummary=true, Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Outcome
        {
            get { return _Outcome; }
            set { _Outcome = value; OnPropertyChanged("Outcome"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Outcome;
        
        /// <summary>
        /// Any report that results from the procedure
        /// </summary>
        [FhirElement("report", Order=210)]
        [References("DiagnosticReport")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Report
        {
            get { if(_Report==null) _Report = new List<Hl7.Fhir.Model.ResourceReference>(); return _Report; }
            set { _Report = value; OnPropertyChanged("Report"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Report;
        
        /// <summary>
        /// Complication following the procedure
        /// </summary>
        [FhirElement("complication", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Complication
        {
            get { if(_Complication==null) _Complication = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Complication; }
            set { _Complication = value; OnPropertyChanged("Complication"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Complication;
        
        /// <summary>
        /// Instructions for follow up
        /// </summary>
        [FhirElement("followUp", Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> FollowUp
        {
            get { if(_FollowUp==null) _FollowUp = new List<Hl7.Fhir.Model.CodeableConcept>(); return _FollowUp; }
            set { _FollowUp = value; OnPropertyChanged("FollowUp"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _FollowUp;
        
        /// <summary>
        /// A procedure that is related to this one
        /// </summary>
        [FhirElement("relatedItem", Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Procedure.ProcedureRelatedItemComponent> RelatedItem
        {
            get { if(_RelatedItem==null) _RelatedItem = new List<Hl7.Fhir.Model.Procedure.ProcedureRelatedItemComponent>(); return _RelatedItem; }
            set { _RelatedItem = value; OnPropertyChanged("RelatedItem"); }
        }
        
        private List<Hl7.Fhir.Model.Procedure.ProcedureRelatedItemComponent> _RelatedItem;
        
        /// <summary>
        /// Additional information about procedure
        /// </summary>
        [FhirElement("notes", Order=250)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NotesElement
        {
            get { return _NotesElement; }
            set { _NotesElement = value; OnPropertyChanged("NotesElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NotesElement;
        
        /// <summary>
        /// Additional information about procedure
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
        
        /// <summary>
        /// Device changed in procedure
        /// </summary>
        [FhirElement("device", Order=260)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Procedure.ProcedureDeviceComponent> Device
        {
            get { if(_Device==null) _Device = new List<Hl7.Fhir.Model.Procedure.ProcedureDeviceComponent>(); return _Device; }
            set { _Device = value; OnPropertyChanged("Device"); }
        }
        
        private List<Hl7.Fhir.Model.Procedure.ProcedureDeviceComponent> _Device;
        
        /// <summary>
        /// Items used during procedure
        /// </summary>
        [FhirElement("used", Order=270)]
        [References("Device","Medication","Substance")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Used
        {
            get { if(_Used==null) _Used = new List<Hl7.Fhir.Model.ResourceReference>(); return _Used; }
            set { _Used = value; OnPropertyChanged("Used"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Used;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Procedure;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Procedure.ProcedureStatus>)StatusElement.DeepCopy();
                if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(BodySite != null) dest.BodySite = new List<Hl7.Fhir.Model.Procedure.ProcedureBodySiteComponent>(BodySite.DeepCopy());
                if(Indication != null) dest.Indication = new List<Hl7.Fhir.Model.CodeableConcept>(Indication.DeepCopy());
                if(Performer != null) dest.Performer = new List<Hl7.Fhir.Model.Procedure.ProcedurePerformerComponent>(Performer.DeepCopy());
                if(Performed != null) dest.Performed = (Hl7.Fhir.Model.Element)Performed.DeepCopy();
                if(Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
                if(Location != null) dest.Location = (Hl7.Fhir.Model.ResourceReference)Location.DeepCopy();
                if(Outcome != null) dest.Outcome = (Hl7.Fhir.Model.CodeableConcept)Outcome.DeepCopy();
                if(Report != null) dest.Report = new List<Hl7.Fhir.Model.ResourceReference>(Report.DeepCopy());
                if(Complication != null) dest.Complication = new List<Hl7.Fhir.Model.CodeableConcept>(Complication.DeepCopy());
                if(FollowUp != null) dest.FollowUp = new List<Hl7.Fhir.Model.CodeableConcept>(FollowUp.DeepCopy());
                if(RelatedItem != null) dest.RelatedItem = new List<Hl7.Fhir.Model.Procedure.ProcedureRelatedItemComponent>(RelatedItem.DeepCopy());
                if(NotesElement != null) dest.NotesElement = (Hl7.Fhir.Model.FhirString)NotesElement.DeepCopy();
                if(Device != null) dest.Device = new List<Hl7.Fhir.Model.Procedure.ProcedureDeviceComponent>(Device.DeepCopy());
                if(Used != null) dest.Used = new List<Hl7.Fhir.Model.ResourceReference>(Used.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Procedure());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Procedure;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(BodySite, otherT.BodySite)) return false;
            if( !DeepComparable.Matches(Indication, otherT.Indication)) return false;
            if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
            if( !DeepComparable.Matches(Performed, otherT.Performed)) return false;
            if( !DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.Matches(Location, otherT.Location)) return false;
            if( !DeepComparable.Matches(Outcome, otherT.Outcome)) return false;
            if( !DeepComparable.Matches(Report, otherT.Report)) return false;
            if( !DeepComparable.Matches(Complication, otherT.Complication)) return false;
            if( !DeepComparable.Matches(FollowUp, otherT.FollowUp)) return false;
            if( !DeepComparable.Matches(RelatedItem, otherT.RelatedItem)) return false;
            if( !DeepComparable.Matches(NotesElement, otherT.NotesElement)) return false;
            if( !DeepComparable.Matches(Device, otherT.Device)) return false;
            if( !DeepComparable.Matches(Used, otherT.Used)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Procedure;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(BodySite, otherT.BodySite)) return false;
            if( !DeepComparable.IsExactly(Indication, otherT.Indication)) return false;
            if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
            if( !DeepComparable.IsExactly(Performed, otherT.Performed)) return false;
            if( !DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
            if( !DeepComparable.IsExactly(Outcome, otherT.Outcome)) return false;
            if( !DeepComparable.IsExactly(Report, otherT.Report)) return false;
            if( !DeepComparable.IsExactly(Complication, otherT.Complication)) return false;
            if( !DeepComparable.IsExactly(FollowUp, otherT.FollowUp)) return false;
            if( !DeepComparable.IsExactly(RelatedItem, otherT.RelatedItem)) return false;
            if( !DeepComparable.IsExactly(NotesElement, otherT.NotesElement)) return false;
            if( !DeepComparable.IsExactly(Device, otherT.Device)) return false;
            if( !DeepComparable.IsExactly(Used, otherT.Used)) return false;
            
            return true;
        }
        
    }
    
}
