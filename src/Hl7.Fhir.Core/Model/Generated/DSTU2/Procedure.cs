using System;
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
// Generated for FHIR v1.0.2
//
namespace Hl7.Fhir.Model.DSTU2
{
    /// <summary>
    /// An action that is being or was performed on a patient
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.DSTU2, "Procedure", IsResource=true)]
    [DataContract]
    public partial class Procedure : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IProcedure, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Procedure; } }
        [NotMapped]
        public override string TypeName { get { return "Procedure"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "PerformerComponent")]
        [DataContract]
        public partial class PerformerComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IProcedurePerformerComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "PerformerComponent"; } }
            
            /// <summary>
            /// The reference to the practitioner
            /// </summary>
            [FhirElement("actor", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
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
            [FhirElement("role", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Role
            {
                get { return _Role; }
                set { _Role = value; OnPropertyChanged("Role"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Role;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("PerformerComponent");
                base.Serialize(sink);
                sink.Element("actor", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Actor?.Serialize(sink);
                sink.Element("role", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Role?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "actor":
                        Actor = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "role":
                        Role = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "actor":
                        Actor = source.Populate(Actor);
                        return true;
                    case "role":
                        Role = source.Populate(Role);
                        return true;
                }
                return false;
            }
        
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
                    foreach (var item in base.Children) yield return item;
                    if (Actor != null) yield return Actor;
                    if (Role != null) yield return Role;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Actor != null) yield return new ElementValue("actor", Actor);
                    if (Role != null) yield return new ElementValue("role", Role);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "FocalDeviceComponent")]
        [DataContract]
        public partial class FocalDeviceComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IProcedureFocalDeviceComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("FocalDeviceComponent");
                base.Serialize(sink);
                sink.Element("action", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Action?.Serialize(sink);
                sink.Element("manipulated", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Manipulated?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "action":
                        Action = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "manipulated":
                        Manipulated = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "action":
                        Action = source.Populate(Action);
                        return true;
                    case "manipulated":
                        Manipulated = source.Populate(Manipulated);
                        return true;
                }
                return false;
            }
        
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
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IProcedurePerformerComponent> Hl7.Fhir.Model.IProcedure.Performer { get { return Performer; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IProcedureFocalDeviceComponent> Hl7.Fhir.Model.IProcedure.FocalDevice { get { return FocalDevice; } }
    
        
        /// <summary>
        /// External Identifiers for this procedure
        /// </summary>
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Who the procedure was performed on
        /// </summary>
        [FhirElement("subject", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
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
        /// in-progress | aborted | completed | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DSTU2.ProcedureStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.DSTU2.ProcedureStatus> _StatusElement;
        
        /// <summary>
        /// in-progress | aborted | completed | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DSTU2.ProcedureStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.DSTU2.ProcedureStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Classification of the procedure
        /// </summary>
        [FhirElement("category", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
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
        [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Code
        {
            get { return _Code; }
            set { _Code = value; OnPropertyChanged("Code"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Code;
        
        /// <summary>
        /// True if procedure was not performed as scheduled
        /// </summary>
        [FhirElement("notPerformed", Order=140)]
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
                if (value == null)
                    NotPerformedElement = null;
                else
                    NotPerformedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("NotPerformed");
            }
        }
        
        /// <summary>
        /// Reason procedure was not performed
        /// </summary>
        [FhirElement("reasonNotPerformed", Order=150)]
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
        [FhirElement("bodySite", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> BodySite
        {
            get { if(_BodySite==null) _BodySite = new List<Hl7.Fhir.Model.CodeableConcept>(); return _BodySite; }
            set { _BodySite = value; OnPropertyChanged("BodySite"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _BodySite;
        
        /// <summary>
        /// Reason procedure performed
        /// </summary>
        [FhirElement("reason", InSummary=Hl7.Fhir.Model.Version.All, Order=170, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Reason
        {
            get { return _Reason; }
            set { _Reason = value; OnPropertyChanged("Reason"); }
        }
        
        private Hl7.Fhir.Model.Element _Reason;
        
        /// <summary>
        /// The people who performed the procedure
        /// </summary>
        [FhirElement("performer", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<PerformerComponent> Performer
        {
            get { if(_Performer==null) _Performer = new List<PerformerComponent>(); return _Performer; }
            set { _Performer = value; OnPropertyChanged("Performer"); }
        }
        
        private List<PerformerComponent> _Performer;
        
        /// <summary>
        /// Date/Period the procedure was performed
        /// </summary>
        [FhirElement("performed", InSummary=Hl7.Fhir.Model.Version.All, Order=190, Choice=ChoiceType.DatatypeChoice)]
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
        /// The encounter associated with the procedure
        /// </summary>
        [FhirElement("encounter", InSummary=Hl7.Fhir.Model.Version.All, Order=200)]
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
        /// Where the procedure happened
        /// </summary>
        [FhirElement("location", InSummary=Hl7.Fhir.Model.Version.All, Order=210)]
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
        /// The result of procedure
        /// </summary>
        [FhirElement("outcome", InSummary=Hl7.Fhir.Model.Version.All, Order=220)]
        [CLSCompliant(false)]
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
        [FhirElement("report", Order=230)]
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
        [FhirElement("complication", Order=240)]
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
        [FhirElement("followUp", Order=250)]
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
        [FhirElement("request", Order=260)]
        [CLSCompliant(false)]
        [References("CarePlan","DiagnosticOrder","ProcedureRequest","ReferralRequest")]
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
        [FhirElement("notes", Order=270)]
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
        [FhirElement("focalDevice", Order=280)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<FocalDeviceComponent> FocalDevice
        {
            get { if(_FocalDevice==null) _FocalDevice = new List<FocalDeviceComponent>(); return _FocalDevice; }
            set { _FocalDevice = value; OnPropertyChanged("FocalDevice"); }
        }
        
        private List<FocalDeviceComponent> _FocalDevice;
        
        /// <summary>
        /// Items used during procedure
        /// </summary>
        [FhirElement("used", Order=290)]
        [CLSCompliant(false)]
        [References("Device","Medication","Substance")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Used
        {
            get { if(_Used==null) _Used = new List<Hl7.Fhir.Model.ResourceReference>(); return _Used; }
            set { _Used = value; OnPropertyChanged("Used"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Used;
    
    
        public static ElementDefinitionConstraint[] Procedure_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "pro-1",
                severity: ConstraintSeverity.Warning,
                expression: "reasonNotPerformed.empty() or notPerformed = 'true'",
                human: "Reason not performed is only permitted if notPerformed indicator is true",
                xpath: "not(exists(f:reasonNotPerformed)) or f:notPerformed/@value=true()"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(Procedure_Constraints);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Procedure;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.DSTU2.ProcedureStatus>)StatusElement.DeepCopy();
                if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                if(NotPerformedElement != null) dest.NotPerformedElement = (Hl7.Fhir.Model.FhirBoolean)NotPerformedElement.DeepCopy();
                if(ReasonNotPerformed != null) dest.ReasonNotPerformed = new List<Hl7.Fhir.Model.CodeableConcept>(ReasonNotPerformed.DeepCopy());
                if(BodySite != null) dest.BodySite = new List<Hl7.Fhir.Model.CodeableConcept>(BodySite.DeepCopy());
                if(Reason != null) dest.Reason = (Hl7.Fhir.Model.Element)Reason.DeepCopy();
                if(Performer != null) dest.Performer = new List<PerformerComponent>(Performer.DeepCopy());
                if(Performed != null) dest.Performed = (Hl7.Fhir.Model.Element)Performed.DeepCopy();
                if(Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
                if(Location != null) dest.Location = (Hl7.Fhir.Model.ResourceReference)Location.DeepCopy();
                if(Outcome != null) dest.Outcome = (Hl7.Fhir.Model.CodeableConcept)Outcome.DeepCopy();
                if(Report != null) dest.Report = new List<Hl7.Fhir.Model.ResourceReference>(Report.DeepCopy());
                if(Complication != null) dest.Complication = new List<Hl7.Fhir.Model.CodeableConcept>(Complication.DeepCopy());
                if(FollowUp != null) dest.FollowUp = new List<Hl7.Fhir.Model.CodeableConcept>(FollowUp.DeepCopy());
                if(Request != null) dest.Request = (Hl7.Fhir.Model.ResourceReference)Request.DeepCopy();
                if(Notes != null) dest.Notes = new List<Hl7.Fhir.Model.Annotation>(Notes.DeepCopy());
                if(FocalDevice != null) dest.FocalDevice = new List<FocalDeviceComponent>(FocalDevice.DeepCopy());
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
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(NotPerformedElement, otherT.NotPerformedElement)) return false;
            if( !DeepComparable.Matches(ReasonNotPerformed, otherT.ReasonNotPerformed)) return false;
            if( !DeepComparable.Matches(BodySite, otherT.BodySite)) return false;
            if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
            if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
            if( !DeepComparable.Matches(Performed, otherT.Performed)) return false;
            if( !DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.Matches(Location, otherT.Location)) return false;
            if( !DeepComparable.Matches(Outcome, otherT.Outcome)) return false;
            if( !DeepComparable.Matches(Report, otherT.Report)) return false;
            if( !DeepComparable.Matches(Complication, otherT.Complication)) return false;
            if( !DeepComparable.Matches(FollowUp, otherT.FollowUp)) return false;
            if( !DeepComparable.Matches(Request, otherT.Request)) return false;
            if( !DeepComparable.Matches(Notes, otherT.Notes)) return false;
            if( !DeepComparable.Matches(FocalDevice, otherT.FocalDevice)) return false;
            if( !DeepComparable.Matches(Used, otherT.Used)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Procedure;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(NotPerformedElement, otherT.NotPerformedElement)) return false;
            if( !DeepComparable.IsExactly(ReasonNotPerformed, otherT.ReasonNotPerformed)) return false;
            if( !DeepComparable.IsExactly(BodySite, otherT.BodySite)) return false;
            if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
            if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
            if( !DeepComparable.IsExactly(Performed, otherT.Performed)) return false;
            if( !DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
            if( !DeepComparable.IsExactly(Outcome, otherT.Outcome)) return false;
            if( !DeepComparable.IsExactly(Report, otherT.Report)) return false;
            if( !DeepComparable.IsExactly(Complication, otherT.Complication)) return false;
            if( !DeepComparable.IsExactly(FollowUp, otherT.FollowUp)) return false;
            if( !DeepComparable.IsExactly(Request, otherT.Request)) return false;
            if( !DeepComparable.IsExactly(Notes, otherT.Notes)) return false;
            if( !DeepComparable.IsExactly(FocalDevice, otherT.FocalDevice)) return false;
            if( !DeepComparable.IsExactly(Used, otherT.Used)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("Procedure");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("subject", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Subject?.Serialize(sink);
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Category?.Serialize(sink);
            sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Code?.Serialize(sink);
            sink.Element("notPerformed", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); NotPerformedElement?.Serialize(sink);
            sink.BeginList("reasonNotPerformed", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in ReasonNotPerformed)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("bodySite", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in BodySite)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("reason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Reason?.Serialize(sink);
            sink.BeginList("performer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Performer)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("performed", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Performed?.Serialize(sink);
            sink.Element("encounter", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Encounter?.Serialize(sink);
            sink.Element("location", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Location?.Serialize(sink);
            sink.Element("outcome", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Outcome?.Serialize(sink);
            sink.BeginList("report", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Report)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("complication", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Complication)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("followUp", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in FollowUp)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("request", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Request?.Serialize(sink);
            sink.BeginList("notes", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Notes)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("focalDevice", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in FocalDevice)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("used", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Used)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.End();
        }
    
        internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
        {
            if (base.SetElementFromSource(elementName, source))
            {
                return true;
            }
            switch (elementName)
            {
                case "identifier":
                    Identifier = source.GetList<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "subject":
                    Subject = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DSTU2.ProcedureStatus>>();
                    return true;
                case "category":
                    Category = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "code":
                    Code = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "notPerformed":
                    NotPerformedElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "reasonNotPerformed":
                    ReasonNotPerformed = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "bodySite":
                    BodySite = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "reasonCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Reason, "reason");
                    Reason = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "reasonReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Reason, "reason");
                    Reason = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "performer":
                    Performer = source.GetList<PerformerComponent>();
                    return true;
                case "performedDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Performed, "performed");
                    Performed = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "performedPeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(Performed, "performed");
                    Performed = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "encounter":
                    Encounter = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "location":
                    Location = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "outcome":
                    Outcome = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "report":
                    Report = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "complication":
                    Complication = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "followUp":
                    FollowUp = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "request":
                    Request = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "notes":
                    Notes = source.GetList<Hl7.Fhir.Model.Annotation>();
                    return true;
                case "focalDevice":
                    FocalDevice = source.GetList<FocalDeviceComponent>();
                    return true;
                case "used":
                    Used = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
            }
            return false;
        }
    
        internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
        {
            if (base.SetElementFromJson(jsonPropertyName, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "identifier":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "subject":
                    Subject = source.Populate(Subject);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "category":
                    Category = source.Populate(Category);
                    return true;
                case "code":
                    Code = source.Populate(Code);
                    return true;
                case "notPerformed":
                    NotPerformedElement = source.PopulateValue(NotPerformedElement);
                    return true;
                case "_notPerformed":
                    NotPerformedElement = source.Populate(NotPerformedElement);
                    return true;
                case "reasonNotPerformed":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "bodySite":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "reasonCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Reason, "reason");
                    Reason = source.Populate(Reason as Hl7.Fhir.Model.CodeableConcept);
                    return true;
                case "reasonReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Reason, "reason");
                    Reason = source.Populate(Reason as Hl7.Fhir.Model.ResourceReference);
                    return true;
                case "performer":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "performedDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Performed, "performed");
                    Performed = source.PopulateValue(Performed as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "_performedDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Performed, "performed");
                    Performed = source.Populate(Performed as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "performedPeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(Performed, "performed");
                    Performed = source.Populate(Performed as Hl7.Fhir.Model.Period);
                    return true;
                case "encounter":
                    Encounter = source.Populate(Encounter);
                    return true;
                case "location":
                    Location = source.Populate(Location);
                    return true;
                case "outcome":
                    Outcome = source.Populate(Outcome);
                    return true;
                case "report":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "complication":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "followUp":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "request":
                    Request = source.Populate(Request);
                    return true;
                case "notes":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "focalDevice":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "used":
                    source.SetList(this, jsonPropertyName);
                    return true;
            }
            return false;
        }
        
        internal override bool SetListElementFromJson(string jsonPropertyName, int index, ref Serialization.JsonSource source)
        {
            if (base.SetListElementFromJson(jsonPropertyName, index, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "identifier":
                    source.PopulateListItem(Identifier, index);
                    return true;
                case "reasonNotPerformed":
                    source.PopulateListItem(ReasonNotPerformed, index);
                    return true;
                case "bodySite":
                    source.PopulateListItem(BodySite, index);
                    return true;
                case "performer":
                    source.PopulateListItem(Performer, index);
                    return true;
                case "report":
                    source.PopulateListItem(Report, index);
                    return true;
                case "complication":
                    source.PopulateListItem(Complication, index);
                    return true;
                case "followUp":
                    source.PopulateListItem(FollowUp, index);
                    return true;
                case "notes":
                    source.PopulateListItem(Notes, index);
                    return true;
                case "focalDevice":
                    source.PopulateListItem(FocalDevice, index);
                    return true;
                case "used":
                    source.PopulateListItem(Used, index);
                    return true;
            }
            return false;
        }
    
        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                if (Subject != null) yield return Subject;
                if (StatusElement != null) yield return StatusElement;
                if (Category != null) yield return Category;
                if (Code != null) yield return Code;
                if (NotPerformedElement != null) yield return NotPerformedElement;
                foreach (var elem in ReasonNotPerformed) { if (elem != null) yield return elem; }
                foreach (var elem in BodySite) { if (elem != null) yield return elem; }
                if (Reason != null) yield return Reason;
                foreach (var elem in Performer) { if (elem != null) yield return elem; }
                if (Performed != null) yield return Performed;
                if (Encounter != null) yield return Encounter;
                if (Location != null) yield return Location;
                if (Outcome != null) yield return Outcome;
                foreach (var elem in Report) { if (elem != null) yield return elem; }
                foreach (var elem in Complication) { if (elem != null) yield return elem; }
                foreach (var elem in FollowUp) { if (elem != null) yield return elem; }
                if (Request != null) yield return Request;
                foreach (var elem in Notes) { if (elem != null) yield return elem; }
                foreach (var elem in FocalDevice) { if (elem != null) yield return elem; }
                foreach (var elem in Used) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (Category != null) yield return new ElementValue("category", Category);
                if (Code != null) yield return new ElementValue("code", Code);
                if (NotPerformedElement != null) yield return new ElementValue("notPerformed", NotPerformedElement);
                foreach (var elem in ReasonNotPerformed) { if (elem != null) yield return new ElementValue("reasonNotPerformed", elem); }
                foreach (var elem in BodySite) { if (elem != null) yield return new ElementValue("bodySite", elem); }
                if (Reason != null) yield return new ElementValue("reason", Reason);
                foreach (var elem in Performer) { if (elem != null) yield return new ElementValue("performer", elem); }
                if (Performed != null) yield return new ElementValue("performed", Performed);
                if (Encounter != null) yield return new ElementValue("encounter", Encounter);
                if (Location != null) yield return new ElementValue("location", Location);
                if (Outcome != null) yield return new ElementValue("outcome", Outcome);
                foreach (var elem in Report) { if (elem != null) yield return new ElementValue("report", elem); }
                foreach (var elem in Complication) { if (elem != null) yield return new ElementValue("complication", elem); }
                foreach (var elem in FollowUp) { if (elem != null) yield return new ElementValue("followUp", elem); }
                if (Request != null) yield return new ElementValue("request", Request);
                foreach (var elem in Notes) { if (elem != null) yield return new ElementValue("notes", elem); }
                foreach (var elem in FocalDevice) { if (elem != null) yield return new ElementValue("focalDevice", elem); }
                foreach (var elem in Used) { if (elem != null) yield return new ElementValue("used", elem); }
            }
        }
    
    }

}
