﻿using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using Hl7.Fhir.Utility;

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

#pragma warning disable 1591 // suppress XML summary warnings 

//
// Generated for FHIR v4.0.0
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
        
        [FhirType("PerformerComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class PerformerComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "PerformerComponent"; } }
            
            /// <summary>
            /// Type of performance
            /// </summary>
            [FhirElement("function", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Function
            {
                get { return _Function; }
                set { _Function = value; OnPropertyChanged("Function"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Function;
            
            /// <summary>
            /// The reference to the practitioner
            /// </summary>
            [FhirElement("actor", InSummary=true, Order=50)]
            [CLSCompliant(false)]
			[References("Practitioner","PractitionerRole","Organization","Patient","RelatedPerson","Device")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Actor
            {
                get { return _Actor; }
                set { _Actor = value; OnPropertyChanged("Actor"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Actor;
            
            /// <summary>
            /// Organization the device or practitioner was acting for
            /// </summary>
            [FhirElement("onBehalfOf", Order=60)]
            [CLSCompliant(false)]
			[References("Organization")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference OnBehalfOf
            {
                get { return _OnBehalfOf; }
                set { _OnBehalfOf = value; OnPropertyChanged("OnBehalfOf"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _OnBehalfOf;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PerformerComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Function != null) dest.Function = (Hl7.Fhir.Model.CodeableConcept)Function.DeepCopy();
                    if(Actor != null) dest.Actor = (Hl7.Fhir.Model.ResourceReference)Actor.DeepCopy();
                    if(OnBehalfOf != null) dest.OnBehalfOf = (Hl7.Fhir.Model.ResourceReference)OnBehalfOf.DeepCopy();
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
                if( !DeepComparable.Matches(Function, otherT.Function)) return false;
                if( !DeepComparable.Matches(Actor, otherT.Actor)) return false;
                if( !DeepComparable.Matches(OnBehalfOf, otherT.OnBehalfOf)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PerformerComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Function, otherT.Function)) return false;
                if( !DeepComparable.IsExactly(Actor, otherT.Actor)) return false;
                if( !DeepComparable.IsExactly(OnBehalfOf, otherT.OnBehalfOf)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Function != null) yield return Function;
                    if (Actor != null) yield return Actor;
                    if (OnBehalfOf != null) yield return OnBehalfOf;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Function != null) yield return new ElementValue("function", Function);
                    if (Actor != null) yield return new ElementValue("actor", Actor);
                    if (OnBehalfOf != null) yield return new ElementValue("onBehalfOf", OnBehalfOf);
                }
            }

            
        }
        
        
        [FhirType("FocalDeviceComponent", NamedBackboneElement=true)]
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
                    foreach (var item in base.Children) yield return item;
                    if (Action != null) yield return Action;
                    if (Manipulated != null) yield return Manipulated;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Action != null) yield return new ElementValue("action", Action);
                    if (Manipulated != null) yield return new ElementValue("manipulated", Manipulated);
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
        /// Instantiates FHIR protocol or definition
        /// </summary>
        [FhirElement("instantiatesCanonical", InSummary=true, Order=100)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Canonical> InstantiatesCanonicalElement
        {
            get { if(_InstantiatesCanonicalElement==null) _InstantiatesCanonicalElement = new List<Hl7.Fhir.Model.Canonical>(); return _InstantiatesCanonicalElement; }
            set { _InstantiatesCanonicalElement = value; OnPropertyChanged("InstantiatesCanonicalElement"); }
        }
        
        private List<Hl7.Fhir.Model.Canonical> _InstantiatesCanonicalElement;
        
        /// <summary>
        /// Instantiates FHIR protocol or definition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> InstantiatesCanonical
        {
            get { return InstantiatesCanonicalElement != null ? InstantiatesCanonicalElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  InstantiatesCanonicalElement = null; 
                else
                  InstantiatesCanonicalElement = new List<Hl7.Fhir.Model.Canonical>(value.Select(elem=>new Hl7.Fhir.Model.Canonical(elem)));
                OnPropertyChanged("InstantiatesCanonical");
            }
        }
        
        /// <summary>
        /// Instantiates external protocol or definition
        /// </summary>
        [FhirElement("instantiatesUri", InSummary=true, Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirUri> InstantiatesUriElement
        {
            get { if(_InstantiatesUriElement==null) _InstantiatesUriElement = new List<Hl7.Fhir.Model.FhirUri>(); return _InstantiatesUriElement; }
            set { _InstantiatesUriElement = value; OnPropertyChanged("InstantiatesUriElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirUri> _InstantiatesUriElement;
        
        /// <summary>
        /// Instantiates external protocol or definition
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> InstantiatesUri
        {
            get { return InstantiatesUriElement != null ? InstantiatesUriElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  InstantiatesUriElement = null; 
                else
                  InstantiatesUriElement = new List<Hl7.Fhir.Model.FhirUri>(value.Select(elem=>new Hl7.Fhir.Model.FhirUri(elem)));
                OnPropertyChanged("InstantiatesUri");
            }
        }
        
        /// <summary>
        /// A request for this procedure
        /// </summary>
        [FhirElement("basedOn", InSummary=true, Order=120)]
        [CLSCompliant(false)]
		[References("CarePlan","ServiceRequest")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> BasedOn
        {
            get { if(_BasedOn==null) _BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(); return _BasedOn; }
            set { _BasedOn = value; OnPropertyChanged("BasedOn"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _BasedOn;
        
        /// <summary>
        /// Part of referenced event
        /// </summary>
        [FhirElement("partOf", InSummary=true, Order=130)]
        [CLSCompliant(false)]
		[References("Procedure","Observation","MedicationAdministration")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> PartOf
        {
            get { if(_PartOf==null) _PartOf = new List<Hl7.Fhir.Model.ResourceReference>(); return _PartOf; }
            set { _PartOf = value; OnPropertyChanged("PartOf"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _PartOf;
        
        /// <summary>
        /// preparation | in-progress | not-done | suspended | aborted | completed | entered-in-error | unknown
        /// </summary>
        [FhirElement("status", InSummary=true, Order=140)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.EventStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.EventStatus> _StatusElement;
        
        /// <summary>
        /// preparation | in-progress | not-done | suspended | aborted | completed | entered-in-error | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.EventStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.EventStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Reason for current status
        /// </summary>
        [FhirElement("statusReason", InSummary=true, Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept StatusReason
        {
            get { return _StatusReason; }
            set { _StatusReason = value; OnPropertyChanged("StatusReason"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _StatusReason;
        
        /// <summary>
        /// Classification of the procedure
        /// </summary>
        [FhirElement("category", InSummary=true, Order=160)]
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
        [FhirElement("code", InSummary=true, Order=170)]
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
        [FhirElement("subject", InSummary=true, Order=180)]
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
        /// Encounter created as part of
        /// </summary>
        [FhirElement("encounter", InSummary=true, Order=190)]
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
        /// When the procedure was performed
        /// </summary>
        [FhirElement("performed", InSummary=true, Order=200, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.FhirString),typeof(Age),typeof(Hl7.Fhir.Model.Range))]
        [DataMember]
        public Hl7.Fhir.Model.Element Performed
        {
            get { return _Performed; }
            set { _Performed = value; OnPropertyChanged("Performed"); }
        }
        
        private Hl7.Fhir.Model.Element _Performed;
        
        /// <summary>
        /// Who recorded the procedure
        /// </summary>
        [FhirElement("recorder", InSummary=true, Order=210)]
        [CLSCompliant(false)]
		[References("Patient","RelatedPerson","Practitioner","PractitionerRole")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Recorder
        {
            get { return _Recorder; }
            set { _Recorder = value; OnPropertyChanged("Recorder"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Recorder;
        
        /// <summary>
        /// Person who asserts this procedure
        /// </summary>
        [FhirElement("asserter", InSummary=true, Order=220)]
        [CLSCompliant(false)]
		[References("Patient","RelatedPerson","Practitioner","PractitionerRole")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Asserter
        {
            get { return _Asserter; }
            set { _Asserter = value; OnPropertyChanged("Asserter"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Asserter;
        
        /// <summary>
        /// The people who performed the procedure
        /// </summary>
        [FhirElement("performer", InSummary=true, Order=230)]
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
        [FhirElement("location", InSummary=true, Order=240)]
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
        /// Coded reason procedure performed
        /// </summary>
        [FhirElement("reasonCode", InSummary=true, Order=250)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ReasonCode
        {
            get { if(_ReasonCode==null) _ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ReasonCode; }
            set { _ReasonCode = value; OnPropertyChanged("ReasonCode"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ReasonCode;
        
        /// <summary>
        /// The justification that the procedure was performed
        /// </summary>
        [FhirElement("reasonReference", InSummary=true, Order=260)]
        [CLSCompliant(false)]
		[References("Condition","Observation","Procedure","DiagnosticReport","DocumentReference")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> ReasonReference
        {
            get { if(_ReasonReference==null) _ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(); return _ReasonReference; }
            set { _ReasonReference = value; OnPropertyChanged("ReasonReference"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _ReasonReference;
        
        /// <summary>
        /// Target body sites
        /// </summary>
        [FhirElement("bodySite", InSummary=true, Order=270)]
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
        [FhirElement("outcome", InSummary=true, Order=280)]
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
        [FhirElement("report", Order=290)]
        [CLSCompliant(false)]
		[References("DiagnosticReport","DocumentReference","Composition")]
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
        [FhirElement("complication", Order=300)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Complication
        {
            get { if(_Complication==null) _Complication = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Complication; }
            set { _Complication = value; OnPropertyChanged("Complication"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Complication;
        
        /// <summary>
        /// A condition that is a result of the procedure
        /// </summary>
        [FhirElement("complicationDetail", Order=310)]
        [CLSCompliant(false)]
		[References("Condition")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> ComplicationDetail
        {
            get { if(_ComplicationDetail==null) _ComplicationDetail = new List<Hl7.Fhir.Model.ResourceReference>(); return _ComplicationDetail; }
            set { _ComplicationDetail = value; OnPropertyChanged("ComplicationDetail"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _ComplicationDetail;
        
        /// <summary>
        /// Instructions for follow up
        /// </summary>
        [FhirElement("followUp", Order=320)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> FollowUp
        {
            get { if(_FollowUp==null) _FollowUp = new List<Hl7.Fhir.Model.CodeableConcept>(); return _FollowUp; }
            set { _FollowUp = value; OnPropertyChanged("FollowUp"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _FollowUp;
        
        /// <summary>
        /// Additional information about the procedure
        /// </summary>
        [FhirElement("note", Order=330)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Note;
        
        /// <summary>
        /// Manipulated, implanted, or removed device
        /// </summary>
        [FhirElement("focalDevice", Order=340)]
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
        [FhirElement("usedReference", Order=350)]
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
        [FhirElement("usedCode", Order=360)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> UsedCode
        {
            get { if(_UsedCode==null) _UsedCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _UsedCode; }
            set { _UsedCode = value; OnPropertyChanged("UsedCode"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _UsedCode;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Procedure;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(InstantiatesCanonicalElement != null) dest.InstantiatesCanonicalElement = new List<Hl7.Fhir.Model.Canonical>(InstantiatesCanonicalElement.DeepCopy());
                if(InstantiatesUriElement != null) dest.InstantiatesUriElement = new List<Hl7.Fhir.Model.FhirUri>(InstantiatesUriElement.DeepCopy());
                if(BasedOn != null) dest.BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(BasedOn.DeepCopy());
                if(PartOf != null) dest.PartOf = new List<Hl7.Fhir.Model.ResourceReference>(PartOf.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.EventStatus>)StatusElement.DeepCopy();
                if(StatusReason != null) dest.StatusReason = (Hl7.Fhir.Model.CodeableConcept)StatusReason.DeepCopy();
                if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
                if(Performed != null) dest.Performed = (Hl7.Fhir.Model.Element)Performed.DeepCopy();
                if(Recorder != null) dest.Recorder = (Hl7.Fhir.Model.ResourceReference)Recorder.DeepCopy();
                if(Asserter != null) dest.Asserter = (Hl7.Fhir.Model.ResourceReference)Asserter.DeepCopy();
                if(Performer != null) dest.Performer = new List<Hl7.Fhir.Model.Procedure.PerformerComponent>(Performer.DeepCopy());
                if(Location != null) dest.Location = (Hl7.Fhir.Model.ResourceReference)Location.DeepCopy();
                if(ReasonCode != null) dest.ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(ReasonCode.DeepCopy());
                if(ReasonReference != null) dest.ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(ReasonReference.DeepCopy());
                if(BodySite != null) dest.BodySite = new List<Hl7.Fhir.Model.CodeableConcept>(BodySite.DeepCopy());
                if(Outcome != null) dest.Outcome = (Hl7.Fhir.Model.CodeableConcept)Outcome.DeepCopy();
                if(Report != null) dest.Report = new List<Hl7.Fhir.Model.ResourceReference>(Report.DeepCopy());
                if(Complication != null) dest.Complication = new List<Hl7.Fhir.Model.CodeableConcept>(Complication.DeepCopy());
                if(ComplicationDetail != null) dest.ComplicationDetail = new List<Hl7.Fhir.Model.ResourceReference>(ComplicationDetail.DeepCopy());
                if(FollowUp != null) dest.FollowUp = new List<Hl7.Fhir.Model.CodeableConcept>(FollowUp.DeepCopy());
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                if(FocalDevice != null) dest.FocalDevice = new List<Hl7.Fhir.Model.Procedure.FocalDeviceComponent>(FocalDevice.DeepCopy());
                if(UsedReference != null) dest.UsedReference = new List<Hl7.Fhir.Model.ResourceReference>(UsedReference.DeepCopy());
                if(UsedCode != null) dest.UsedCode = new List<Hl7.Fhir.Model.CodeableConcept>(UsedCode.DeepCopy());
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
            if( !DeepComparable.Matches(InstantiatesCanonicalElement, otherT.InstantiatesCanonicalElement)) return false;
            if( !DeepComparable.Matches(InstantiatesUriElement, otherT.InstantiatesUriElement)) return false;
            if( !DeepComparable.Matches(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.Matches(PartOf, otherT.PartOf)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(StatusReason, otherT.StatusReason)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.Matches(Performed, otherT.Performed)) return false;
            if( !DeepComparable.Matches(Recorder, otherT.Recorder)) return false;
            if( !DeepComparable.Matches(Asserter, otherT.Asserter)) return false;
            if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
            if( !DeepComparable.Matches(Location, otherT.Location)) return false;
            if( !DeepComparable.Matches(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.Matches(ReasonReference, otherT.ReasonReference)) return false;
            if( !DeepComparable.Matches(BodySite, otherT.BodySite)) return false;
            if( !DeepComparable.Matches(Outcome, otherT.Outcome)) return false;
            if( !DeepComparable.Matches(Report, otherT.Report)) return false;
            if( !DeepComparable.Matches(Complication, otherT.Complication)) return false;
            if( !DeepComparable.Matches(ComplicationDetail, otherT.ComplicationDetail)) return false;
            if( !DeepComparable.Matches(FollowUp, otherT.FollowUp)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            if( !DeepComparable.Matches(FocalDevice, otherT.FocalDevice)) return false;
            if( !DeepComparable.Matches(UsedReference, otherT.UsedReference)) return false;
            if( !DeepComparable.Matches(UsedCode, otherT.UsedCode)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Procedure;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(InstantiatesCanonicalElement, otherT.InstantiatesCanonicalElement)) return false;
            if( !DeepComparable.IsExactly(InstantiatesUriElement, otherT.InstantiatesUriElement)) return false;
            if( !DeepComparable.IsExactly(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.IsExactly(PartOf, otherT.PartOf)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(StatusReason, otherT.StatusReason)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.IsExactly(Performed, otherT.Performed)) return false;
            if( !DeepComparable.IsExactly(Recorder, otherT.Recorder)) return false;
            if( !DeepComparable.IsExactly(Asserter, otherT.Asserter)) return false;
            if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
            if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
            if( !DeepComparable.IsExactly(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.IsExactly(ReasonReference, otherT.ReasonReference)) return false;
            if( !DeepComparable.IsExactly(BodySite, otherT.BodySite)) return false;
            if( !DeepComparable.IsExactly(Outcome, otherT.Outcome)) return false;
            if( !DeepComparable.IsExactly(Report, otherT.Report)) return false;
            if( !DeepComparable.IsExactly(Complication, otherT.Complication)) return false;
            if( !DeepComparable.IsExactly(ComplicationDetail, otherT.ComplicationDetail)) return false;
            if( !DeepComparable.IsExactly(FollowUp, otherT.FollowUp)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            if( !DeepComparable.IsExactly(FocalDevice, otherT.FocalDevice)) return false;
            if( !DeepComparable.IsExactly(UsedReference, otherT.UsedReference)) return false;
            if( !DeepComparable.IsExactly(UsedCode, otherT.UsedCode)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				foreach (var elem in InstantiatesCanonicalElement) { if (elem != null) yield return elem; }
				foreach (var elem in InstantiatesUriElement) { if (elem != null) yield return elem; }
				foreach (var elem in BasedOn) { if (elem != null) yield return elem; }
				foreach (var elem in PartOf) { if (elem != null) yield return elem; }
				if (StatusElement != null) yield return StatusElement;
				if (StatusReason != null) yield return StatusReason;
				if (Category != null) yield return Category;
				if (Code != null) yield return Code;
				if (Subject != null) yield return Subject;
				if (Encounter != null) yield return Encounter;
				if (Performed != null) yield return Performed;
				if (Recorder != null) yield return Recorder;
				if (Asserter != null) yield return Asserter;
				foreach (var elem in Performer) { if (elem != null) yield return elem; }
				if (Location != null) yield return Location;
				foreach (var elem in ReasonCode) { if (elem != null) yield return elem; }
				foreach (var elem in ReasonReference) { if (elem != null) yield return elem; }
				foreach (var elem in BodySite) { if (elem != null) yield return elem; }
				if (Outcome != null) yield return Outcome;
				foreach (var elem in Report) { if (elem != null) yield return elem; }
				foreach (var elem in Complication) { if (elem != null) yield return elem; }
				foreach (var elem in ComplicationDetail) { if (elem != null) yield return elem; }
				foreach (var elem in FollowUp) { if (elem != null) yield return elem; }
				foreach (var elem in Note) { if (elem != null) yield return elem; }
				foreach (var elem in FocalDevice) { if (elem != null) yield return elem; }
				foreach (var elem in UsedReference) { if (elem != null) yield return elem; }
				foreach (var elem in UsedCode) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                foreach (var elem in InstantiatesCanonicalElement) { if (elem != null) yield return new ElementValue("instantiatesCanonical", elem); }
                foreach (var elem in InstantiatesUriElement) { if (elem != null) yield return new ElementValue("instantiatesUri", elem); }
                foreach (var elem in BasedOn) { if (elem != null) yield return new ElementValue("basedOn", elem); }
                foreach (var elem in PartOf) { if (elem != null) yield return new ElementValue("partOf", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (StatusReason != null) yield return new ElementValue("statusReason", StatusReason);
                if (Category != null) yield return new ElementValue("category", Category);
                if (Code != null) yield return new ElementValue("code", Code);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Encounter != null) yield return new ElementValue("encounter", Encounter);
                if (Performed != null) yield return new ElementValue("performed", Performed);
                if (Recorder != null) yield return new ElementValue("recorder", Recorder);
                if (Asserter != null) yield return new ElementValue("asserter", Asserter);
                foreach (var elem in Performer) { if (elem != null) yield return new ElementValue("performer", elem); }
                if (Location != null) yield return new ElementValue("location", Location);
                foreach (var elem in ReasonCode) { if (elem != null) yield return new ElementValue("reasonCode", elem); }
                foreach (var elem in ReasonReference) { if (elem != null) yield return new ElementValue("reasonReference", elem); }
                foreach (var elem in BodySite) { if (elem != null) yield return new ElementValue("bodySite", elem); }
                if (Outcome != null) yield return new ElementValue("outcome", Outcome);
                foreach (var elem in Report) { if (elem != null) yield return new ElementValue("report", elem); }
                foreach (var elem in Complication) { if (elem != null) yield return new ElementValue("complication", elem); }
                foreach (var elem in ComplicationDetail) { if (elem != null) yield return new ElementValue("complicationDetail", elem); }
                foreach (var elem in FollowUp) { if (elem != null) yield return new ElementValue("followUp", elem); }
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                foreach (var elem in FocalDevice) { if (elem != null) yield return new ElementValue("focalDevice", elem); }
                foreach (var elem in UsedReference) { if (elem != null) yield return new ElementValue("usedReference", elem); }
                foreach (var elem in UsedCode) { if (elem != null) yield return new ElementValue("usedCode", elem); }
            }
        }

    }
    
}
