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
    /// Item containing charge code(s) associated with the provision of healthcare provider products
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.STU3, "ChargeItem", IsResource=true)]
    [DataContract]
    public partial class ChargeItem : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IChargeItem, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ChargeItem; } }
        [NotMapped]
        public override string TypeName { get { return "ChargeItem"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "ParticipantComponent")]
        [DataContract]
        public partial class ParticipantComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ParticipantComponent"; } }
            
            /// <summary>
            /// What type of performance was done
            /// </summary>
            [FhirElement("role", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Role
            {
                get { return _Role; }
                set { _Role = value; OnPropertyChanged("Role"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Role;
            
            /// <summary>
            /// Individual who was performing
            /// </summary>
            [FhirElement("actor", Order=50)]
            [CLSCompliant(false)]
            [References("Practitioner","Organization","Patient","Device","RelatedPerson")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Actor
            {
                get { return _Actor; }
                set { _Actor = value; OnPropertyChanged("Actor"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Actor;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ParticipantComponent");
                base.Serialize(sink);
                sink.Element("role", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Role?.Serialize(sink);
                sink.Element("actor", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Actor?.Serialize(sink);
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
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ParticipantComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Role != null) dest.Role = (Hl7.Fhir.Model.CodeableConcept)Role.DeepCopy();
                    if(Actor != null) dest.Actor = (Hl7.Fhir.Model.ResourceReference)Actor.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ParticipantComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ParticipantComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Role, otherT.Role)) return false;
                if( !DeepComparable.Matches(Actor, otherT.Actor)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ParticipantComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Role, otherT.Role)) return false;
                if( !DeepComparable.IsExactly(Actor, otherT.Actor)) return false;
            
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
                }
            }
        
        
        }
        
        [NotMapped]
        Hl7.Fhir.Model.IMoney Hl7.Fhir.Model.IChargeItem.PriceOverride { get { return PriceOverride; } }
    
        
        /// <summary>
        /// Business Identifier for item
        /// </summary>
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// Defining information about the code of this charge item
        /// </summary>
        [FhirElement("definition", Order=100)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirUri> DefinitionElement
        {
            get { if(_DefinitionElement==null) _DefinitionElement = new List<Hl7.Fhir.Model.FhirUri>(); return _DefinitionElement; }
            set { _DefinitionElement = value; OnPropertyChanged("DefinitionElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirUri> _DefinitionElement;
        
        /// <summary>
        /// Defining information about the code of this charge item
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Definition
        {
            get { return DefinitionElement != null ? DefinitionElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    DefinitionElement = null;
                else
                    DefinitionElement = new List<Hl7.Fhir.Model.FhirUri>(value.Select(elem=>new Hl7.Fhir.Model.FhirUri(elem)));
                OnPropertyChanged("Definition");
            }
        }
        
        /// <summary>
        /// planned | billable | not-billable | aborted | billed | entered-in-error | unknown
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.ChargeItemStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.ChargeItemStatus> _StatusElement;
        
        /// <summary>
        /// planned | billable | not-billable | aborted | billed | entered-in-error | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.ChargeItemStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.ChargeItemStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Part of referenced ChargeItem
        /// </summary>
        [FhirElement("partOf", Order=120)]
        [CLSCompliant(false)]
        [References("ChargeItem")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> PartOf
        {
            get { if(_PartOf==null) _PartOf = new List<Hl7.Fhir.Model.ResourceReference>(); return _PartOf; }
            set { _PartOf = value; OnPropertyChanged("PartOf"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _PartOf;
        
        /// <summary>
        /// A code that identifies the charge, like a billing code
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
        /// Individual service was done for/to
        /// </summary>
        [FhirElement("subject", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
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
        /// Encounter / Episode associated with event
        /// </summary>
        [FhirElement("context", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
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
        /// When the charged service was applied
        /// </summary>
        [FhirElement("occurrence", InSummary=Hl7.Fhir.Model.Version.All, Order=160, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.STU3.Timing))]
        [DataMember]
        public Hl7.Fhir.Model.Element Occurrence
        {
            get { return _Occurrence; }
            set { _Occurrence = value; OnPropertyChanged("Occurrence"); }
        }
        
        private Hl7.Fhir.Model.Element _Occurrence;
        
        /// <summary>
        /// Who performed charged service
        /// </summary>
        [FhirElement("participant", Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ParticipantComponent> Participant
        {
            get { if(_Participant==null) _Participant = new List<ParticipantComponent>(); return _Participant; }
            set { _Participant = value; OnPropertyChanged("Participant"); }
        }
        
        private List<ParticipantComponent> _Participant;
        
        /// <summary>
        /// Organization providing the charged sevice
        /// </summary>
        [FhirElement("performingOrganization", Order=180)]
        [CLSCompliant(false)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference PerformingOrganization
        {
            get { return _PerformingOrganization; }
            set { _PerformingOrganization = value; OnPropertyChanged("PerformingOrganization"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _PerformingOrganization;
        
        /// <summary>
        /// Organization requesting the charged service
        /// </summary>
        [FhirElement("requestingOrganization", Order=190)]
        [CLSCompliant(false)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference RequestingOrganization
        {
            get { return _RequestingOrganization; }
            set { _RequestingOrganization = value; OnPropertyChanged("RequestingOrganization"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _RequestingOrganization;
        
        /// <summary>
        /// Quantity of which the charge item has been serviced
        /// </summary>
        [FhirElement("quantity", InSummary=Hl7.Fhir.Model.Version.All, Order=200)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Quantity Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; OnPropertyChanged("Quantity"); }
        }
        
        private Hl7.Fhir.Model.Quantity _Quantity;
        
        /// <summary>
        /// Anatomical location, if relevant
        /// </summary>
        [FhirElement("bodysite", InSummary=Hl7.Fhir.Model.Version.All, Order=210)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Bodysite
        {
            get { if(_Bodysite==null) _Bodysite = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Bodysite; }
            set { _Bodysite = value; OnPropertyChanged("Bodysite"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Bodysite;
        
        /// <summary>
        /// Factor overriding the associated rules
        /// </summary>
        [FhirElement("factorOverride", Order=220)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDecimal FactorOverrideElement
        {
            get { return _FactorOverrideElement; }
            set { _FactorOverrideElement = value; OnPropertyChanged("FactorOverrideElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDecimal _FactorOverrideElement;
        
        /// <summary>
        /// Factor overriding the associated rules
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public decimal? FactorOverride
        {
            get { return FactorOverrideElement != null ? FactorOverrideElement.Value : null; }
            set
            {
                if (value == null)
                    FactorOverrideElement = null;
                else
                    FactorOverrideElement = new Hl7.Fhir.Model.FhirDecimal(value);
                OnPropertyChanged("FactorOverride");
            }
        }
        
        /// <summary>
        /// Price overriding the associated rules
        /// </summary>
        [FhirElement("priceOverride", Order=230)]
        [DataMember]
        public Hl7.Fhir.Model.STU3.Money PriceOverride
        {
            get { return _PriceOverride; }
            set { _PriceOverride = value; OnPropertyChanged("PriceOverride"); }
        }
        
        private Hl7.Fhir.Model.STU3.Money _PriceOverride;
        
        /// <summary>
        /// Reason for overriding the list price/factor
        /// </summary>
        [FhirElement("overrideReason", Order=240)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString OverrideReasonElement
        {
            get { return _OverrideReasonElement; }
            set { _OverrideReasonElement = value; OnPropertyChanged("OverrideReasonElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _OverrideReasonElement;
        
        /// <summary>
        /// Reason for overriding the list price/factor
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string OverrideReason
        {
            get { return OverrideReasonElement != null ? OverrideReasonElement.Value : null; }
            set
            {
                if (value == null)
                    OverrideReasonElement = null;
                else
                    OverrideReasonElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("OverrideReason");
            }
        }
        
        /// <summary>
        /// Individual who was entering
        /// </summary>
        [FhirElement("enterer", InSummary=Hl7.Fhir.Model.Version.All, Order=250)]
        [CLSCompliant(false)]
        [References("Practitioner","Organization","Patient","Device","RelatedPerson")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Enterer
        {
            get { return _Enterer; }
            set { _Enterer = value; OnPropertyChanged("Enterer"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Enterer;
        
        /// <summary>
        /// Date the charge item was entered
        /// </summary>
        [FhirElement("enteredDate", InSummary=Hl7.Fhir.Model.Version.All, Order=260)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime EnteredDateElement
        {
            get { return _EnteredDateElement; }
            set { _EnteredDateElement = value; OnPropertyChanged("EnteredDateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _EnteredDateElement;
        
        /// <summary>
        /// Date the charge item was entered
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string EnteredDate
        {
            get { return EnteredDateElement != null ? EnteredDateElement.Value : null; }
            set
            {
                if (value == null)
                    EnteredDateElement = null;
                else
                    EnteredDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("EnteredDate");
            }
        }
        
        /// <summary>
        /// Why was the charged  service rendered?
        /// </summary>
        [FhirElement("reason", Order=270)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Reason
        {
            get { if(_Reason==null) _Reason = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Reason; }
            set { _Reason = value; OnPropertyChanged("Reason"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Reason;
        
        /// <summary>
        /// Which rendered service is being charged?
        /// </summary>
        [FhirElement("service", Order=280)]
        [CLSCompliant(false)]
        [References("DiagnosticReport","ImagingStudy","Immunization","MedicationAdministration","MedicationDispense","Observation","Procedure","SupplyDelivery")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Service
        {
            get { if(_Service==null) _Service = new List<Hl7.Fhir.Model.ResourceReference>(); return _Service; }
            set { _Service = value; OnPropertyChanged("Service"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Service;
        
        /// <summary>
        /// Account to place this charge
        /// </summary>
        [FhirElement("account", InSummary=Hl7.Fhir.Model.Version.All, Order=290)]
        [CLSCompliant(false)]
        [References("Account")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Account
        {
            get { if(_Account==null) _Account = new List<Hl7.Fhir.Model.ResourceReference>(); return _Account; }
            set { _Account = value; OnPropertyChanged("Account"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Account;
        
        /// <summary>
        /// Comments made about the ChargeItem
        /// </summary>
        [FhirElement("note", Order=300)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Note;
        
        /// <summary>
        /// Further information supporting the this charge
        /// </summary>
        [FhirElement("supportingInformation", Order=310)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> SupportingInformation
        {
            get { if(_SupportingInformation==null) _SupportingInformation = new List<Hl7.Fhir.Model.ResourceReference>(); return _SupportingInformation; }
            set { _SupportingInformation = value; OnPropertyChanged("SupportingInformation"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _SupportingInformation;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ChargeItem;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(DefinitionElement != null) dest.DefinitionElement = new List<Hl7.Fhir.Model.FhirUri>(DefinitionElement.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.ChargeItemStatus>)StatusElement.DeepCopy();
                if(PartOf != null) dest.PartOf = new List<Hl7.Fhir.Model.ResourceReference>(PartOf.DeepCopy());
                if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Context != null) dest.Context = (Hl7.Fhir.Model.ResourceReference)Context.DeepCopy();
                if(Occurrence != null) dest.Occurrence = (Hl7.Fhir.Model.Element)Occurrence.DeepCopy();
                if(Participant != null) dest.Participant = new List<ParticipantComponent>(Participant.DeepCopy());
                if(PerformingOrganization != null) dest.PerformingOrganization = (Hl7.Fhir.Model.ResourceReference)PerformingOrganization.DeepCopy();
                if(RequestingOrganization != null) dest.RequestingOrganization = (Hl7.Fhir.Model.ResourceReference)RequestingOrganization.DeepCopy();
                if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.Quantity)Quantity.DeepCopy();
                if(Bodysite != null) dest.Bodysite = new List<Hl7.Fhir.Model.CodeableConcept>(Bodysite.DeepCopy());
                if(FactorOverrideElement != null) dest.FactorOverrideElement = (Hl7.Fhir.Model.FhirDecimal)FactorOverrideElement.DeepCopy();
                if(PriceOverride != null) dest.PriceOverride = (Hl7.Fhir.Model.STU3.Money)PriceOverride.DeepCopy();
                if(OverrideReasonElement != null) dest.OverrideReasonElement = (Hl7.Fhir.Model.FhirString)OverrideReasonElement.DeepCopy();
                if(Enterer != null) dest.Enterer = (Hl7.Fhir.Model.ResourceReference)Enterer.DeepCopy();
                if(EnteredDateElement != null) dest.EnteredDateElement = (Hl7.Fhir.Model.FhirDateTime)EnteredDateElement.DeepCopy();
                if(Reason != null) dest.Reason = new List<Hl7.Fhir.Model.CodeableConcept>(Reason.DeepCopy());
                if(Service != null) dest.Service = new List<Hl7.Fhir.Model.ResourceReference>(Service.DeepCopy());
                if(Account != null) dest.Account = new List<Hl7.Fhir.Model.ResourceReference>(Account.DeepCopy());
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                if(SupportingInformation != null) dest.SupportingInformation = new List<Hl7.Fhir.Model.ResourceReference>(SupportingInformation.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new ChargeItem());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ChargeItem;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(DefinitionElement, otherT.DefinitionElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(PartOf, otherT.PartOf)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Context, otherT.Context)) return false;
            if( !DeepComparable.Matches(Occurrence, otherT.Occurrence)) return false;
            if( !DeepComparable.Matches(Participant, otherT.Participant)) return false;
            if( !DeepComparable.Matches(PerformingOrganization, otherT.PerformingOrganization)) return false;
            if( !DeepComparable.Matches(RequestingOrganization, otherT.RequestingOrganization)) return false;
            if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
            if( !DeepComparable.Matches(Bodysite, otherT.Bodysite)) return false;
            if( !DeepComparable.Matches(FactorOverrideElement, otherT.FactorOverrideElement)) return false;
            if( !DeepComparable.Matches(PriceOverride, otherT.PriceOverride)) return false;
            if( !DeepComparable.Matches(OverrideReasonElement, otherT.OverrideReasonElement)) return false;
            if( !DeepComparable.Matches(Enterer, otherT.Enterer)) return false;
            if( !DeepComparable.Matches(EnteredDateElement, otherT.EnteredDateElement)) return false;
            if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
            if( !DeepComparable.Matches(Service, otherT.Service)) return false;
            if( !DeepComparable.Matches(Account, otherT.Account)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            if( !DeepComparable.Matches(SupportingInformation, otherT.SupportingInformation)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ChargeItem;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(DefinitionElement, otherT.DefinitionElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(PartOf, otherT.PartOf)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Context, otherT.Context)) return false;
            if( !DeepComparable.IsExactly(Occurrence, otherT.Occurrence)) return false;
            if( !DeepComparable.IsExactly(Participant, otherT.Participant)) return false;
            if( !DeepComparable.IsExactly(PerformingOrganization, otherT.PerformingOrganization)) return false;
            if( !DeepComparable.IsExactly(RequestingOrganization, otherT.RequestingOrganization)) return false;
            if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
            if( !DeepComparable.IsExactly(Bodysite, otherT.Bodysite)) return false;
            if( !DeepComparable.IsExactly(FactorOverrideElement, otherT.FactorOverrideElement)) return false;
            if( !DeepComparable.IsExactly(PriceOverride, otherT.PriceOverride)) return false;
            if( !DeepComparable.IsExactly(OverrideReasonElement, otherT.OverrideReasonElement)) return false;
            if( !DeepComparable.IsExactly(Enterer, otherT.Enterer)) return false;
            if( !DeepComparable.IsExactly(EnteredDateElement, otherT.EnteredDateElement)) return false;
            if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
            if( !DeepComparable.IsExactly(Service, otherT.Service)) return false;
            if( !DeepComparable.IsExactly(Account, otherT.Account)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            if( !DeepComparable.IsExactly(SupportingInformation, otherT.SupportingInformation)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("ChargeItem");
            base.Serialize(sink);
            sink.Element("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Identifier?.Serialize(sink);
            sink.BeginList("definition", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            sink.Serialize(DefinitionElement);
            sink.End();
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.BeginList("partOf", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in PartOf)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Code?.Serialize(sink);
            sink.Element("subject", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Subject?.Serialize(sink);
            sink.Element("context", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Context?.Serialize(sink);
            sink.Element("occurrence", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Occurrence?.Serialize(sink);
            sink.BeginList("participant", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Participant)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("performingOrganization", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PerformingOrganization?.Serialize(sink);
            sink.Element("requestingOrganization", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); RequestingOrganization?.Serialize(sink);
            sink.Element("quantity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Quantity?.Serialize(sink);
            sink.BeginList("bodysite", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Bodysite)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("factorOverride", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); FactorOverrideElement?.Serialize(sink);
            sink.Element("priceOverride", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PriceOverride?.Serialize(sink);
            sink.Element("overrideReason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); OverrideReasonElement?.Serialize(sink);
            sink.Element("enterer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Enterer?.Serialize(sink);
            sink.Element("enteredDate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); EnteredDateElement?.Serialize(sink);
            sink.BeginList("reason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Reason)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("service", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Service)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("account", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Account)
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
            sink.BeginList("supportingInformation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in SupportingInformation)
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
                    Identifier = source.Get<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "definition":
                    DefinitionElement = source.GetList<Hl7.Fhir.Model.FhirUri>();
                    return true;
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ChargeItemStatus>>();
                    return true;
                case "partOf":
                    PartOf = source.GetList<Hl7.Fhir.Model.ResourceReference>();
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
                case "occurrenceDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Occurrence, "occurrence");
                    Occurrence = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "occurrencePeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(Occurrence, "occurrence");
                    Occurrence = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "occurrenceTiming":
                    source.CheckDuplicates<Hl7.Fhir.Model.STU3.Timing>(Occurrence, "occurrence");
                    Occurrence = source.Get<Hl7.Fhir.Model.STU3.Timing>();
                    return true;
                case "participant":
                    Participant = source.GetList<ParticipantComponent>();
                    return true;
                case "performingOrganization":
                    PerformingOrganization = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "requestingOrganization":
                    RequestingOrganization = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "quantity":
                    Quantity = source.Get<Hl7.Fhir.Model.Quantity>();
                    return true;
                case "bodysite":
                    Bodysite = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "factorOverride":
                    FactorOverrideElement = source.Get<Hl7.Fhir.Model.FhirDecimal>();
                    return true;
                case "priceOverride":
                    PriceOverride = source.Get<Hl7.Fhir.Model.STU3.Money>();
                    return true;
                case "overrideReason":
                    OverrideReasonElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "enterer":
                    Enterer = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "enteredDate":
                    EnteredDateElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "reason":
                    Reason = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "service":
                    Service = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "account":
                    Account = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "note":
                    Note = source.GetList<Hl7.Fhir.Model.Annotation>();
                    return true;
                case "supportingInformation":
                    SupportingInformation = source.GetList<Hl7.Fhir.Model.ResourceReference>();
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
                    Identifier = source.Populate(Identifier);
                    return true;
                case "definition":
                case "_definition":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "partOf":
                    source.SetList(this, jsonPropertyName);
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
                case "occurrenceDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Occurrence, "occurrence");
                    Occurrence = source.PopulateValue(Occurrence as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "_occurrenceDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Occurrence, "occurrence");
                    Occurrence = source.Populate(Occurrence as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "occurrencePeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(Occurrence, "occurrence");
                    Occurrence = source.Populate(Occurrence as Hl7.Fhir.Model.Period);
                    return true;
                case "occurrenceTiming":
                    source.CheckDuplicates<Hl7.Fhir.Model.STU3.Timing>(Occurrence, "occurrence");
                    Occurrence = source.Populate(Occurrence as Hl7.Fhir.Model.STU3.Timing);
                    return true;
                case "participant":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "performingOrganization":
                    PerformingOrganization = source.Populate(PerformingOrganization);
                    return true;
                case "requestingOrganization":
                    RequestingOrganization = source.Populate(RequestingOrganization);
                    return true;
                case "quantity":
                    Quantity = source.Populate(Quantity);
                    return true;
                case "bodysite":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "factorOverride":
                    FactorOverrideElement = source.PopulateValue(FactorOverrideElement);
                    return true;
                case "_factorOverride":
                    FactorOverrideElement = source.Populate(FactorOverrideElement);
                    return true;
                case "priceOverride":
                    PriceOverride = source.Populate(PriceOverride);
                    return true;
                case "overrideReason":
                    OverrideReasonElement = source.PopulateValue(OverrideReasonElement);
                    return true;
                case "_overrideReason":
                    OverrideReasonElement = source.Populate(OverrideReasonElement);
                    return true;
                case "enterer":
                    Enterer = source.Populate(Enterer);
                    return true;
                case "enteredDate":
                    EnteredDateElement = source.PopulateValue(EnteredDateElement);
                    return true;
                case "_enteredDate":
                    EnteredDateElement = source.Populate(EnteredDateElement);
                    return true;
                case "reason":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "service":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "account":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "note":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "supportingInformation":
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
                case "definition":
                    source.PopulatePrimitiveListItemValue(DefinitionElement, index);
                    return true;
                case "_definition":
                    source.PopulatePrimitiveListItem(DefinitionElement, index);
                    return true;
                case "partOf":
                    source.PopulateListItem(PartOf, index);
                    return true;
                case "participant":
                    source.PopulateListItem(Participant, index);
                    return true;
                case "bodysite":
                    source.PopulateListItem(Bodysite, index);
                    return true;
                case "reason":
                    source.PopulateListItem(Reason, index);
                    return true;
                case "service":
                    source.PopulateListItem(Service, index);
                    return true;
                case "account":
                    source.PopulateListItem(Account, index);
                    return true;
                case "note":
                    source.PopulateListItem(Note, index);
                    return true;
                case "supportingInformation":
                    source.PopulateListItem(SupportingInformation, index);
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
                if (Identifier != null) yield return Identifier;
                foreach (var elem in DefinitionElement) { if (elem != null) yield return elem; }
                if (StatusElement != null) yield return StatusElement;
                foreach (var elem in PartOf) { if (elem != null) yield return elem; }
                if (Code != null) yield return Code;
                if (Subject != null) yield return Subject;
                if (Context != null) yield return Context;
                if (Occurrence != null) yield return Occurrence;
                foreach (var elem in Participant) { if (elem != null) yield return elem; }
                if (PerformingOrganization != null) yield return PerformingOrganization;
                if (RequestingOrganization != null) yield return RequestingOrganization;
                if (Quantity != null) yield return Quantity;
                foreach (var elem in Bodysite) { if (elem != null) yield return elem; }
                if (FactorOverrideElement != null) yield return FactorOverrideElement;
                if (PriceOverride != null) yield return PriceOverride;
                if (OverrideReasonElement != null) yield return OverrideReasonElement;
                if (Enterer != null) yield return Enterer;
                if (EnteredDateElement != null) yield return EnteredDateElement;
                foreach (var elem in Reason) { if (elem != null) yield return elem; }
                foreach (var elem in Service) { if (elem != null) yield return elem; }
                foreach (var elem in Account) { if (elem != null) yield return elem; }
                foreach (var elem in Note) { if (elem != null) yield return elem; }
                foreach (var elem in SupportingInformation) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                foreach (var elem in DefinitionElement) { if (elem != null) yield return new ElementValue("definition", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                foreach (var elem in PartOf) { if (elem != null) yield return new ElementValue("partOf", elem); }
                if (Code != null) yield return new ElementValue("code", Code);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Context != null) yield return new ElementValue("context", Context);
                if (Occurrence != null) yield return new ElementValue("occurrence", Occurrence);
                foreach (var elem in Participant) { if (elem != null) yield return new ElementValue("participant", elem); }
                if (PerformingOrganization != null) yield return new ElementValue("performingOrganization", PerformingOrganization);
                if (RequestingOrganization != null) yield return new ElementValue("requestingOrganization", RequestingOrganization);
                if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                foreach (var elem in Bodysite) { if (elem != null) yield return new ElementValue("bodysite", elem); }
                if (FactorOverrideElement != null) yield return new ElementValue("factorOverride", FactorOverrideElement);
                if (PriceOverride != null) yield return new ElementValue("priceOverride", PriceOverride);
                if (OverrideReasonElement != null) yield return new ElementValue("overrideReason", OverrideReasonElement);
                if (Enterer != null) yield return new ElementValue("enterer", Enterer);
                if (EnteredDateElement != null) yield return new ElementValue("enteredDate", EnteredDateElement);
                foreach (var elem in Reason) { if (elem != null) yield return new ElementValue("reason", elem); }
                foreach (var elem in Service) { if (elem != null) yield return new ElementValue("service", elem); }
                foreach (var elem in Account) { if (elem != null) yield return new ElementValue("account", elem); }
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                foreach (var elem in SupportingInformation) { if (elem != null) yield return new ElementValue("supportingInformation", elem); }
            }
        }
    
    }

}
