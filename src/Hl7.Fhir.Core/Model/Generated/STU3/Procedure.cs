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
// Generated for FHIR v3.0.1
//
namespace Hl7.Fhir.Model.STU3
{
    /// <summary>
    /// An action that is being or was performed on a patient
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.STU3, "Procedure", IsResource=true)]
    [DataContract]
    public partial class Procedure : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IProcedure, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Procedure; } }
        [NotMapped]
        public override string TypeName { get { return "Procedure"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "PerformerComponent")]
        [DataContract]
        public partial class PerformerComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IProcedurePerformerComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "PerformerComponent"; } }
            
            /// <summary>
            /// The role the actor was in
            /// </summary>
            [FhirElement("role", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Role
            {
                get { return _Role; }
                set { _Role = value; OnPropertyChanged("Role"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Role;
            
            /// <summary>
            /// The reference to the practitioner
            /// </summary>
            [FhirElement("actor", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [References("Practitioner","Organization","Patient","RelatedPerson","Device")]
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("PerformerComponent");
                base.Serialize(sink);
                sink.Element("role", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Role?.Serialize(sink);
                sink.Element("actor", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Actor?.Serialize(sink);
                sink.Element("onBehalfOf", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); OnBehalfOf?.Serialize(sink);
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
                    case "role":
                        Role = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "actor":
                        Actor = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "onBehalfOf":
                        OnBehalfOf = source.Get<Hl7.Fhir.Model.ResourceReference>();
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
                    case "role":
                        Role = source.Populate(Role);
                        return true;
                    case "actor":
                        Actor = source.Populate(Actor);
                        return true;
                    case "onBehalfOf":
                        OnBehalfOf = source.Populate(OnBehalfOf);
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
                    if(Role != null) dest.Role = (Hl7.Fhir.Model.CodeableConcept)Role.DeepCopy();
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
                if( !DeepComparable.Matches(Role, otherT.Role)) return false;
                if( !DeepComparable.Matches(Actor, otherT.Actor)) return false;
                if( !DeepComparable.Matches(OnBehalfOf, otherT.OnBehalfOf)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PerformerComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Role, otherT.Role)) return false;
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
                    if (Role != null) yield return Role;
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
                    if (Role != null) yield return new ElementValue("role", Role);
                    if (Actor != null) yield return new ElementValue("actor", Actor);
                    if (OnBehalfOf != null) yield return new ElementValue("onBehalfOf", OnBehalfOf);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "FocalDeviceComponent")]
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
        /// Instantiates protocol or definition
        /// </summary>
        [FhirElement("definition", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [References("PlanDefinition","ActivityDefinition","HealthcareService")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Definition
        {
            get { if(_Definition==null) _Definition = new List<Hl7.Fhir.Model.ResourceReference>(); return _Definition; }
            set { _Definition = value; OnPropertyChanged("Definition"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Definition;
        
        /// <summary>
        /// A request for this procedure
        /// </summary>
        [FhirElement("basedOn", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [References("CarePlan","ProcedureRequest","ReferralRequest")]
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
        [FhirElement("partOf", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
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
        /// preparation | in-progress | suspended | aborted | completed | entered-in-error | unknown
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.STU3.EventStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.STU3.EventStatus> _StatusElement;
        
        /// <summary>
        /// preparation | in-progress | suspended | aborted | completed | entered-in-error | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.STU3.EventStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.STU3.EventStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// True if procedure was not performed as scheduled
        /// </summary>
        [FhirElement("notDone", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean NotDoneElement
        {
            get { return _NotDoneElement; }
            set { _NotDoneElement = value; OnPropertyChanged("NotDoneElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _NotDoneElement;
        
        /// <summary>
        /// True if procedure was not performed as scheduled
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? NotDone
        {
            get { return NotDoneElement != null ? NotDoneElement.Value : null; }
            set
            {
                if (value == null)
                    NotDoneElement = null;
                else
                    NotDoneElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("NotDone");
            }
        }
        
        /// <summary>
        /// Reason procedure was not performed
        /// </summary>
        [FhirElement("notDoneReason", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept NotDoneReason
        {
            get { return _NotDoneReason; }
            set { _NotDoneReason = value; OnPropertyChanged("NotDoneReason"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _NotDoneReason;
        
        /// <summary>
        /// Classification of the procedure
        /// </summary>
        [FhirElement("category", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
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
        [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
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
        [FhirElement("subject", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
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
        /// Encounter or episode associated with the procedure
        /// </summary>
        [FhirElement("context", InSummary=Hl7.Fhir.Model.Version.All, Order=190)]
        [CLSCompliant(false)]
        [References("Encounter","EpisodeOfCare")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Context
        {
            get { return _Context; }
            set { _Context = value; OnPropertyChanged("Context"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Context;
        
        /// <summary>
        /// Date/Period the procedure was performed
        /// </summary>
        [FhirElement("performed", InSummary=Hl7.Fhir.Model.Version.All, Order=200, Choice=ChoiceType.DatatypeChoice)]
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
        [FhirElement("performer", InSummary=Hl7.Fhir.Model.Version.All, Order=210)]
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
        /// Where the procedure happened
        /// </summary>
        [FhirElement("location", InSummary=Hl7.Fhir.Model.Version.All, Order=220)]
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
        [FhirElement("reasonCode", InSummary=Hl7.Fhir.Model.Version.All, Order=230)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ReasonCode
        {
            get { if(_ReasonCode==null) _ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ReasonCode; }
            set { _ReasonCode = value; OnPropertyChanged("ReasonCode"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ReasonCode;
        
        /// <summary>
        /// Condition that is the reason the procedure performed
        /// </summary>
        [FhirElement("reasonReference", InSummary=Hl7.Fhir.Model.Version.All, Order=240)]
        [CLSCompliant(false)]
        [References("Condition","Observation")]
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
        [FhirElement("bodySite", InSummary=Hl7.Fhir.Model.Version.All, Order=250)]
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
        /// The result of procedure
        /// </summary>
        [FhirElement("outcome", InSummary=Hl7.Fhir.Model.Version.All, Order=260)]
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
        [FhirElement("report", Order=270)]
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
        [FhirElement("complication", Order=280)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Complication
        {
            get { if(_Complication==null) _Complication = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Complication; }
            set { _Complication = value; OnPropertyChanged("Complication"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Complication;
        
        /// <summary>
        /// A condition that is a result of the procedure
        /// </summary>
        [FhirElement("complicationDetail", Order=290)]
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
        [FhirElement("followUp", Order=300)]
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
        [FhirElement("note", Order=310)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Note;
        
        /// <summary>
        /// Device changed in procedure
        /// </summary>
        [FhirElement("focalDevice", Order=320)]
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
        [FhirElement("usedReference", Order=330)]
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
        [FhirElement("usedCode", Order=340)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> UsedCode
        {
            get { if(_UsedCode==null) _UsedCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _UsedCode; }
            set { _UsedCode = value; OnPropertyChanged("UsedCode"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _UsedCode;
    
    
        public static ElementDefinitionConstraint[] Procedure_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "pro-1",
                severity: ConstraintSeverity.Warning,
                expression: "notDoneReason.empty() or notDone = true",
                human: "Reason not done is only permitted if notDone indicator is true",
                xpath: "not(exists(f:notDoneReason)) or f:notDone/@value=true()"
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
                if(Definition != null) dest.Definition = new List<Hl7.Fhir.Model.ResourceReference>(Definition.DeepCopy());
                if(BasedOn != null) dest.BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(BasedOn.DeepCopy());
                if(PartOf != null) dest.PartOf = new List<Hl7.Fhir.Model.ResourceReference>(PartOf.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.STU3.EventStatus>)StatusElement.DeepCopy();
                if(NotDoneElement != null) dest.NotDoneElement = (Hl7.Fhir.Model.FhirBoolean)NotDoneElement.DeepCopy();
                if(NotDoneReason != null) dest.NotDoneReason = (Hl7.Fhir.Model.CodeableConcept)NotDoneReason.DeepCopy();
                if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Context != null) dest.Context = (Hl7.Fhir.Model.ResourceReference)Context.DeepCopy();
                if(Performed != null) dest.Performed = (Hl7.Fhir.Model.Element)Performed.DeepCopy();
                if(Performer != null) dest.Performer = new List<PerformerComponent>(Performer.DeepCopy());
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
                if(FocalDevice != null) dest.FocalDevice = new List<FocalDeviceComponent>(FocalDevice.DeepCopy());
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
            if( !DeepComparable.Matches(Definition, otherT.Definition)) return false;
            if( !DeepComparable.Matches(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.Matches(PartOf, otherT.PartOf)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(NotDoneElement, otherT.NotDoneElement)) return false;
            if( !DeepComparable.Matches(NotDoneReason, otherT.NotDoneReason)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Context, otherT.Context)) return false;
            if( !DeepComparable.Matches(Performed, otherT.Performed)) return false;
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
            if( !DeepComparable.IsExactly(Definition, otherT.Definition)) return false;
            if( !DeepComparable.IsExactly(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.IsExactly(PartOf, otherT.PartOf)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(NotDoneElement, otherT.NotDoneElement)) return false;
            if( !DeepComparable.IsExactly(NotDoneReason, otherT.NotDoneReason)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Context, otherT.Context)) return false;
            if( !DeepComparable.IsExactly(Performed, otherT.Performed)) return false;
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
            sink.BeginList("definition", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Definition)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("basedOn", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in BasedOn)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("partOf", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in PartOf)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("notDone", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NotDoneElement?.Serialize(sink);
            sink.Element("notDoneReason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NotDoneReason?.Serialize(sink);
            sink.Element("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Category?.Serialize(sink);
            sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Code?.Serialize(sink);
            sink.Element("subject", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Subject?.Serialize(sink);
            sink.Element("context", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Context?.Serialize(sink);
            sink.Element("performed", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Performed?.Serialize(sink);
            sink.BeginList("performer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Performer)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("location", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Location?.Serialize(sink);
            sink.BeginList("reasonCode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in ReasonCode)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("reasonReference", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in ReasonReference)
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
            sink.BeginList("complicationDetail", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in ComplicationDetail)
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
            sink.BeginList("note", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Note)
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
            sink.BeginList("usedReference", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in UsedReference)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("usedCode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in UsedCode)
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
                case "definition":
                    Definition = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "basedOn":
                    BasedOn = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "partOf":
                    PartOf = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.STU3.EventStatus>>();
                    return true;
                case "notDone":
                    NotDoneElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                    return true;
                case "notDoneReason":
                    NotDoneReason = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "category":
                    Category = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "code":
                    Code = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "subject":
                    Subject = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "context":
                    Context = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "performedDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Performed, "performed");
                    Performed = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "performedPeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(Performed, "performed");
                    Performed = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "performer":
                    Performer = source.GetList<PerformerComponent>();
                    return true;
                case "location":
                    Location = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "reasonCode":
                    ReasonCode = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "reasonReference":
                    ReasonReference = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "bodySite":
                    BodySite = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
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
                case "complicationDetail":
                    ComplicationDetail = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "followUp":
                    FollowUp = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "note":
                    Note = source.GetList<Hl7.Fhir.Model.Annotation>();
                    return true;
                case "focalDevice":
                    FocalDevice = source.GetList<FocalDeviceComponent>();
                    return true;
                case "usedReference":
                    UsedReference = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "usedCode":
                    UsedCode = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
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
                case "definition":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "basedOn":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "partOf":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "notDone":
                    NotDoneElement = source.PopulateValue(NotDoneElement);
                    return true;
                case "_notDone":
                    NotDoneElement = source.Populate(NotDoneElement);
                    return true;
                case "notDoneReason":
                    NotDoneReason = source.Populate(NotDoneReason);
                    return true;
                case "category":
                    Category = source.Populate(Category);
                    return true;
                case "code":
                    Code = source.Populate(Code);
                    return true;
                case "subject":
                    Subject = source.Populate(Subject);
                    return true;
                case "context":
                    Context = source.Populate(Context);
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
                case "performer":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "location":
                    Location = source.Populate(Location);
                    return true;
                case "reasonCode":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "reasonReference":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "bodySite":
                    source.SetList(this, jsonPropertyName);
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
                case "complicationDetail":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "followUp":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "note":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "focalDevice":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "usedReference":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "usedCode":
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
                case "definition":
                    source.PopulateListItem(Definition, index);
                    return true;
                case "basedOn":
                    source.PopulateListItem(BasedOn, index);
                    return true;
                case "partOf":
                    source.PopulateListItem(PartOf, index);
                    return true;
                case "performer":
                    source.PopulateListItem(Performer, index);
                    return true;
                case "reasonCode":
                    source.PopulateListItem(ReasonCode, index);
                    return true;
                case "reasonReference":
                    source.PopulateListItem(ReasonReference, index);
                    return true;
                case "bodySite":
                    source.PopulateListItem(BodySite, index);
                    return true;
                case "report":
                    source.PopulateListItem(Report, index);
                    return true;
                case "complication":
                    source.PopulateListItem(Complication, index);
                    return true;
                case "complicationDetail":
                    source.PopulateListItem(ComplicationDetail, index);
                    return true;
                case "followUp":
                    source.PopulateListItem(FollowUp, index);
                    return true;
                case "note":
                    source.PopulateListItem(Note, index);
                    return true;
                case "focalDevice":
                    source.PopulateListItem(FocalDevice, index);
                    return true;
                case "usedReference":
                    source.PopulateListItem(UsedReference, index);
                    return true;
                case "usedCode":
                    source.PopulateListItem(UsedCode, index);
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
                foreach (var elem in Definition) { if (elem != null) yield return elem; }
                foreach (var elem in BasedOn) { if (elem != null) yield return elem; }
                foreach (var elem in PartOf) { if (elem != null) yield return elem; }
                if (StatusElement != null) yield return StatusElement;
                if (NotDoneElement != null) yield return NotDoneElement;
                if (NotDoneReason != null) yield return NotDoneReason;
                if (Category != null) yield return Category;
                if (Code != null) yield return Code;
                if (Subject != null) yield return Subject;
                if (Context != null) yield return Context;
                if (Performed != null) yield return Performed;
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
                foreach (var elem in Definition) { if (elem != null) yield return new ElementValue("definition", elem); }
                foreach (var elem in BasedOn) { if (elem != null) yield return new ElementValue("basedOn", elem); }
                foreach (var elem in PartOf) { if (elem != null) yield return new ElementValue("partOf", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (NotDoneElement != null) yield return new ElementValue("notDone", NotDoneElement);
                if (NotDoneReason != null) yield return new ElementValue("notDoneReason", NotDoneReason);
                if (Category != null) yield return new ElementValue("category", Category);
                if (Code != null) yield return new ElementValue("code", Code);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Context != null) yield return new ElementValue("context", Context);
                if (Performed != null) yield return new ElementValue("performed", Performed);
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
