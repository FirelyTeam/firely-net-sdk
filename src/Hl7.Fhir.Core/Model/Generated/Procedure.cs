using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.ComponentModel;

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
// Generated for FHIR v1.8.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// An action that is being or was performed on a patient
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
        /// A code specifying the state of the procedure.
        /// (url: http://hl7.org/fhir/ValueSet/procedure-status)
        /// </summary>
        [FhirEnumeration("ProcedureStatus")]
        public enum ProcedureStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/procedure-status)
            /// </summary>
            [EnumLiteral("in-progress"), Description("In Progress")]
            InProgress,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/procedure-status)
            /// </summary>
            [EnumLiteral("aborted"), Description("Aboted")]
            Aborted,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/procedure-status)
            /// </summary>
            [EnumLiteral("completed"), Description("Completed")]
            Completed,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/procedure-status)
            /// </summary>
            [EnumLiteral("entered-in-error"), Description("Entered in Error")]
            EnteredInError,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/procedure-status)
            /// </summary>
            [EnumLiteral("unknown"), Description("Unknown")]
            Unknown,
        }

        [FhirType("PerformerComponent")]
        [DataContract]
        public partial class PerformerComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "PerformerComponent"; } }
            
            /// <summary>
            /// The reference to the practitioner
            /// </summary>
            [FhirElement("actor", InSummary=true, Order=40)]
            [CLSCompliant(false)]
			[References("Practitioner","Organization","Patient","RelatedPerson")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Actor
            {
                get { return _Actor; }
                set { _Actor = value; OnPropertyChanged("Actor"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Actor;
            
            /// <summary>
            /// The role the actor was in
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
                var dest = other as PerformerComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Actor != null) dest.Actor = (Hl7.Fhir.Model.ResourceReference)Actor.DeepCopy();
                    if(Role != null) dest.Role = (Hl7.Fhir.Model.CodeableConcept)Role.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new PerformerComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PerformerComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Actor, otherT.Actor)) return false;
                if( !DeepComparable.Matches(Role, otherT.Role)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PerformerComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Actor, otherT.Actor)) return false;
                if( !DeepComparable.IsExactly(Role, otherT.Role)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    // BackboneElement elements
                    foreach (var elem in ModifierExtension) { if (elem != null) yield return elem; }
                    // PerformerComponent elements
                    if (Actor != null) yield return Actor;
                    if (Role != null) yield return Role;
                }
            }
            
        }
        
        
        [FhirType("FocalDeviceComponent")]
        [DataContract]
        public partial class FocalDeviceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "FocalDeviceComponent"; } }
            
            /// <summary>
            /// Kind of change to device
            /// </summary>
            [FhirElement("action", Order=40)]
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
            [FhirElement("manipulated", Order=50)]
            [CLSCompliant(false)]
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
                var dest = other as FocalDeviceComponent;
                
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
                return CopyTo(new FocalDeviceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as FocalDeviceComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Action, otherT.Action)) return false;
                if( !DeepComparable.Matches(Manipulated, otherT.Manipulated)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as FocalDeviceComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Action, otherT.Action)) return false;
                if( !DeepComparable.IsExactly(Manipulated, otherT.Manipulated)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    // BackboneElement elements
                    foreach (var elem in ModifierExtension) { if (elem != null) yield return elem; }
                    // FocalDeviceComponent elements
                    if (Action != null) yield return Action;
                    if (Manipulated != null) yield return Manipulated;
                }
            }
            
        }
        
        
        /// <summary>
        /// External Identifiers for this procedure
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
        /// in-progress | aborted | completed | entered-in-error | unknown
        /// </summary>
        [FhirElement("status", InSummary=true, Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Procedure.ProcedureStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Procedure.ProcedureStatus> _StatusElement;
        
        /// <summary>
        /// in-progress | aborted | completed | entered-in-error | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Procedure.ProcedureStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.Procedure.ProcedureStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Classification of the procedure
        /// </summary>
        [FhirElement("category", InSummary=true, Order=110)]
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
        [FhirElement("code", InSummary=true, Order=120)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Code
        {
            get { return _Code; }
            set { _Code = value; OnPropertyChanged("Code"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Code;
        
        /// <summary>
        /// Who the procedure was performed on
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=130)]
        [CLSCompliant(false)]
		[References("Patient","Group")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// The encounter associated with the procedure
        /// </summary>
        [FhirElement("encounter", InSummary=true, Order=140)]
        [CLSCompliant(false)]
		[References("Encounter")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Encounter
        {
            get { return _Encounter; }
            set { _Encounter = value; OnPropertyChanged("Encounter"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Encounter;
        
        /// <summary>
        /// Date/Period the procedure was performed
        /// </summary>
        [FhirElement("performed", InSummary=true, Order=150, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
        [DataMember]
        public Hl7.Fhir.Model.Element Performed
        {
            get { return _Performed; }
            set { _Performed = value; OnPropertyChanged("Performed"); }
        }
        
        private Hl7.Fhir.Model.Element _Performed;
        
        /// <summary>
        /// The people who performed the procedure
        /// </summary>
        [FhirElement("performer", InSummary=true, Order=160)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Procedure.PerformerComponent> Performer
        {
            get { if(_Performer==null) _Performer = new List<Hl7.Fhir.Model.Procedure.PerformerComponent>(); return _Performer; }
            set { _Performer = value; OnPropertyChanged("Performer"); }
        }
        
        private List<Hl7.Fhir.Model.Procedure.PerformerComponent> _Performer;
        
        /// <summary>
        /// Where the procedure happened
        /// </summary>
        [FhirElement("location", InSummary=true, Order=170)]
        [CLSCompliant(false)]
		[References("Location")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Location
        {
            get { return _Location; }
            set { _Location = value; OnPropertyChanged("Location"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Location;
        
        /// <summary>
        /// Condition that is the reason the procedure performed
        /// </summary>
        [FhirElement("reasonReference", InSummary=true, Order=180)]
        [CLSCompliant(false)]
		[References("Condition")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> ReasonReference
        {
            get { if(_ReasonReference==null) _ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(); return _ReasonReference; }
            set { _ReasonReference = value; OnPropertyChanged("ReasonReference"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _ReasonReference;
        
        /// <summary>
        /// Coded reason procedure performed
        /// </summary>
        [FhirElement("reasonCode", InSummary=true, Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ReasonCode
        {
            get { if(_ReasonCode==null) _ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ReasonCode; }
            set { _ReasonCode = value; OnPropertyChanged("ReasonCode"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ReasonCode;
        
        /// <summary>
        /// True if procedure was not performed as scheduled
        /// </summary>
        [FhirElement("notPerformed", InSummary=true, Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean NotPerformedElement
        {
            get { return _NotPerformedElement; }
            set { _NotPerformedElement = value; OnPropertyChanged("NotPerformedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _NotPerformedElement;
        
        /// <summary>
        /// True if procedure was not performed as scheduled
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? NotPerformed
        {
            get { return NotPerformedElement != null ? NotPerformedElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  NotPerformedElement = null; 
                else
                  NotPerformedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("NotPerformed");
            }
        }
        
        /// <summary>
        /// Reason procedure was not performed
        /// </summary>
        [FhirElement("reasonNotPerformed", Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ReasonNotPerformed
        {
            get { if(_ReasonNotPerformed==null) _ReasonNotPerformed = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ReasonNotPerformed; }
            set { _ReasonNotPerformed = value; OnPropertyChanged("ReasonNotPerformed"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ReasonNotPerformed;
        
        /// <summary>
        /// Target body sites
        /// </summary>
        [FhirElement("bodySite", InSummary=true, Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> BodySite
        {
            get { if(_BodySite==null) _BodySite = new List<Hl7.Fhir.Model.CodeableConcept>(); return _BodySite; }
            set { _BodySite = value; OnPropertyChanged("BodySite"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _BodySite;
        
        /// <summary>
        /// The result of procedure
        /// </summary>
        [FhirElement("outcome", InSummary=true, Order=230)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Outcome
        {
            get { return _Outcome; }
            set { _Outcome = value; OnPropertyChanged("Outcome"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Outcome;
        
        /// <summary>
        /// Any report resulting from the procedure
        /// </summary>
        [FhirElement("report", Order=240)]
        [CLSCompliant(false)]
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
        [FhirElement("complication", Order=250)]
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
        [FhirElement("followUp", Order=260)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> FollowUp
        {
            get { if(_FollowUp==null) _FollowUp = new List<Hl7.Fhir.Model.CodeableConcept>(); return _FollowUp; }
            set { _FollowUp = value; OnPropertyChanged("FollowUp"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _FollowUp;
        
        /// <summary>
        /// A request for this procedure
        /// </summary>
        [FhirElement("request", Order=270)]
        [CLSCompliant(false)]
		[References("CarePlan","DiagnosticRequest","ProcedureRequest","ReferralRequest")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Request
        {
            get { return _Request; }
            set { _Request = value; OnPropertyChanged("Request"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Request;
        
        /// <summary>
        /// Additional information about the procedure
        /// </summary>
        [FhirElement("notes", Order=280)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Notes
        {
            get { if(_Notes==null) _Notes = new List<Hl7.Fhir.Model.Annotation>(); return _Notes; }
            set { _Notes = value; OnPropertyChanged("Notes"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Notes;
        
        /// <summary>
        /// Device changed in procedure
        /// </summary>
        [FhirElement("focalDevice", Order=290)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Procedure.FocalDeviceComponent> FocalDevice
        {
            get { if(_FocalDevice==null) _FocalDevice = new List<Hl7.Fhir.Model.Procedure.FocalDeviceComponent>(); return _FocalDevice; }
            set { _FocalDevice = value; OnPropertyChanged("FocalDevice"); }
        }
        
        private List<Hl7.Fhir.Model.Procedure.FocalDeviceComponent> _FocalDevice;
        
        /// <summary>
        /// Items used during procedure
        /// </summary>
        [FhirElement("usedReference", Order=300)]
        [CLSCompliant(false)]
		[References("Device","Medication","Substance")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> UsedReference
        {
            get { if(_UsedReference==null) _UsedReference = new List<Hl7.Fhir.Model.ResourceReference>(); return _UsedReference; }
            set { _UsedReference = value; OnPropertyChanged("UsedReference"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _UsedReference;
        
        /// <summary>
        /// Coded items used during the procedure
        /// </summary>
        [FhirElement("usedCode", Order=310)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> UsedCode
        {
            get { if(_UsedCode==null) _UsedCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _UsedCode; }
            set { _UsedCode = value; OnPropertyChanged("UsedCode"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _UsedCode;
        
        /// <summary>
        /// Events related to the procedure
        /// </summary>
        [FhirElement("component", Order=320)]
        [CLSCompliant(false)]
		[References("MedicationAdministration","Procedure","Observation")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Component
        {
            get { if(_Component==null) _Component = new List<Hl7.Fhir.Model.ResourceReference>(); return _Component; }
            set { _Component = value; OnPropertyChanged("Component"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Component;
        

        public static ElementDefinition.ConstraintComponent Procedure_PRO_1 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "reasonNotPerformed.empty() or notPerformed = true",
            Key = "pro-1",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Reason not performed is only permitted if notPerformed indicator is true",
            Xpath = "not(exists(f:reasonNotPerformed)) or f:notPerformed/@value=true()"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(Procedure_PRO_1);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Procedure;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Procedure.ProcedureStatus>)StatusElement.DeepCopy();
                if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
                if(Performed != null) dest.Performed = (Hl7.Fhir.Model.Element)Performed.DeepCopy();
                if(Performer != null) dest.Performer = new List<Hl7.Fhir.Model.Procedure.PerformerComponent>(Performer.DeepCopy());
                if(Location != null) dest.Location = (Hl7.Fhir.Model.ResourceReference)Location.DeepCopy();
                if(ReasonReference != null) dest.ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(ReasonReference.DeepCopy());
                if(ReasonCode != null) dest.ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(ReasonCode.DeepCopy());
                if(NotPerformedElement != null) dest.NotPerformedElement = (Hl7.Fhir.Model.FhirBoolean)NotPerformedElement.DeepCopy();
                if(ReasonNotPerformed != null) dest.ReasonNotPerformed = new List<Hl7.Fhir.Model.CodeableConcept>(ReasonNotPerformed.DeepCopy());
                if(BodySite != null) dest.BodySite = new List<Hl7.Fhir.Model.CodeableConcept>(BodySite.DeepCopy());
                if(Outcome != null) dest.Outcome = (Hl7.Fhir.Model.CodeableConcept)Outcome.DeepCopy();
                if(Report != null) dest.Report = new List<Hl7.Fhir.Model.ResourceReference>(Report.DeepCopy());
                if(Complication != null) dest.Complication = new List<Hl7.Fhir.Model.CodeableConcept>(Complication.DeepCopy());
                if(FollowUp != null) dest.FollowUp = new List<Hl7.Fhir.Model.CodeableConcept>(FollowUp.DeepCopy());
                if(Request != null) dest.Request = (Hl7.Fhir.Model.ResourceReference)Request.DeepCopy();
                if(Notes != null) dest.Notes = new List<Hl7.Fhir.Model.Annotation>(Notes.DeepCopy());
                if(FocalDevice != null) dest.FocalDevice = new List<Hl7.Fhir.Model.Procedure.FocalDeviceComponent>(FocalDevice.DeepCopy());
                if(UsedReference != null) dest.UsedReference = new List<Hl7.Fhir.Model.ResourceReference>(UsedReference.DeepCopy());
                if(UsedCode != null) dest.UsedCode = new List<Hl7.Fhir.Model.CodeableConcept>(UsedCode.DeepCopy());
                if(Component != null) dest.Component = new List<Hl7.Fhir.Model.ResourceReference>(Component.DeepCopy());
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
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.Matches(Performed, otherT.Performed)) return false;
            if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
            if( !DeepComparable.Matches(Location, otherT.Location)) return false;
            if( !DeepComparable.Matches(ReasonReference, otherT.ReasonReference)) return false;
            if( !DeepComparable.Matches(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.Matches(NotPerformedElement, otherT.NotPerformedElement)) return false;
            if( !DeepComparable.Matches(ReasonNotPerformed, otherT.ReasonNotPerformed)) return false;
            if( !DeepComparable.Matches(BodySite, otherT.BodySite)) return false;
            if( !DeepComparable.Matches(Outcome, otherT.Outcome)) return false;
            if( !DeepComparable.Matches(Report, otherT.Report)) return false;
            if( !DeepComparable.Matches(Complication, otherT.Complication)) return false;
            if( !DeepComparable.Matches(FollowUp, otherT.FollowUp)) return false;
            if( !DeepComparable.Matches(Request, otherT.Request)) return false;
            if( !DeepComparable.Matches(Notes, otherT.Notes)) return false;
            if( !DeepComparable.Matches(FocalDevice, otherT.FocalDevice)) return false;
            if( !DeepComparable.Matches(UsedReference, otherT.UsedReference)) return false;
            if( !DeepComparable.Matches(UsedCode, otherT.UsedCode)) return false;
            if( !DeepComparable.Matches(Component, otherT.Component)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Procedure;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.IsExactly(Performed, otherT.Performed)) return false;
            if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
            if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
            if( !DeepComparable.IsExactly(ReasonReference, otherT.ReasonReference)) return false;
            if( !DeepComparable.IsExactly(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.IsExactly(NotPerformedElement, otherT.NotPerformedElement)) return false;
            if( !DeepComparable.IsExactly(ReasonNotPerformed, otherT.ReasonNotPerformed)) return false;
            if( !DeepComparable.IsExactly(BodySite, otherT.BodySite)) return false;
            if( !DeepComparable.IsExactly(Outcome, otherT.Outcome)) return false;
            if( !DeepComparable.IsExactly(Report, otherT.Report)) return false;
            if( !DeepComparable.IsExactly(Complication, otherT.Complication)) return false;
            if( !DeepComparable.IsExactly(FollowUp, otherT.FollowUp)) return false;
            if( !DeepComparable.IsExactly(Request, otherT.Request)) return false;
            if( !DeepComparable.IsExactly(Notes, otherT.Notes)) return false;
            if( !DeepComparable.IsExactly(FocalDevice, otherT.FocalDevice)) return false;
            if( !DeepComparable.IsExactly(UsedReference, otherT.UsedReference)) return false;
            if( !DeepComparable.IsExactly(UsedCode, otherT.UsedCode)) return false;
            if( !DeepComparable.IsExactly(Component, otherT.Component)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
				// Procedure elements
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				if (StatusElement != null) yield return StatusElement;
				if (Category != null) yield return Category;
				if (Code != null) yield return Code;
				if (Subject != null) yield return Subject;
				if (Encounter != null) yield return Encounter;
				if (Performed != null) yield return Performed;
				foreach (var elem in Performer) { if (elem != null) yield return elem; }
				if (Location != null) yield return Location;
				foreach (var elem in ReasonReference) { if (elem != null) yield return elem; }
				foreach (var elem in ReasonCode) { if (elem != null) yield return elem; }
				if (NotPerformedElement != null) yield return NotPerformedElement;
				foreach (var elem in ReasonNotPerformed) { if (elem != null) yield return elem; }
				foreach (var elem in BodySite) { if (elem != null) yield return elem; }
				if (Outcome != null) yield return Outcome;
				foreach (var elem in Report) { if (elem != null) yield return elem; }
				foreach (var elem in Complication) { if (elem != null) yield return elem; }
				foreach (var elem in FollowUp) { if (elem != null) yield return elem; }
				if (Request != null) yield return Request;
				foreach (var elem in Notes) { if (elem != null) yield return elem; }
				foreach (var elem in FocalDevice) { if (elem != null) yield return elem; }
				foreach (var elem in UsedReference) { if (elem != null) yield return elem; }
				foreach (var elem in UsedCode) { if (elem != null) yield return elem; }
				foreach (var elem in Component) { if (elem != null) yield return elem; }
            }
        }
    }
    
}
