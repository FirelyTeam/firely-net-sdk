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
    /// Ordering of medication for patient or group
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.STU3, "MedicationRequest", IsResource=true)]
    [DataContract]
    public partial class MedicationRequest : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IMedicationRequest, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.MedicationRequest; } }
        [NotMapped]
        public override string TypeName { get { return "MedicationRequest"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "RequesterComponent")]
        [DataContract]
        public partial class RequesterComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "RequesterComponent"; } }
            
            /// <summary>
            /// Who ordered the initial medication(s)
            /// </summary>
            [FhirElement("agent", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [References("Practitioner","Organization","Patient","RelatedPerson","Device")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Agent
            {
                get { return _Agent; }
                set { _Agent = value; OnPropertyChanged("Agent"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Agent;
            
            /// <summary>
            /// Organization agent is acting for
            /// </summary>
            [FhirElement("onBehalfOf", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
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
                sink.BeginDataType("RequesterComponent");
                base.Serialize(sink);
                sink.Element("agent", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Agent?.Serialize(sink);
                sink.Element("onBehalfOf", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); OnBehalfOf?.Serialize(sink);
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
                    case "agent":
                        Agent = source.Get<Hl7.Fhir.Model.ResourceReference>();
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
                    case "agent":
                        Agent = source.Populate(Agent);
                        return true;
                    case "onBehalfOf":
                        OnBehalfOf = source.Populate(OnBehalfOf);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RequesterComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Agent != null) dest.Agent = (Hl7.Fhir.Model.ResourceReference)Agent.DeepCopy();
                    if(OnBehalfOf != null) dest.OnBehalfOf = (Hl7.Fhir.Model.ResourceReference)OnBehalfOf.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new RequesterComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RequesterComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Agent, otherT.Agent)) return false;
                if( !DeepComparable.Matches(OnBehalfOf, otherT.OnBehalfOf)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RequesterComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Agent, otherT.Agent)) return false;
                if( !DeepComparable.IsExactly(OnBehalfOf, otherT.OnBehalfOf)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Agent != null) yield return Agent;
                    if (OnBehalfOf != null) yield return OnBehalfOf;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Agent != null) yield return new ElementValue("agent", Agent);
                    if (OnBehalfOf != null) yield return new ElementValue("onBehalfOf", OnBehalfOf);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "DispenseRequestComponent")]
        [DataContract]
        public partial class DispenseRequestComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IMedicationRequestDispenseRequestComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "DispenseRequestComponent"; } }
            
            [NotMapped]
            Hl7.Fhir.Model.IDuration Hl7.Fhir.Model.IMedicationRequestDispenseRequestComponent.ExpectedSupplyDuration { get { return ExpectedSupplyDuration; } }
            
            /// <summary>
            /// Time period supply is authorized for
            /// </summary>
            [FhirElement("validityPeriod", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Period ValidityPeriod
            {
                get { return _ValidityPeriod; }
                set { _ValidityPeriod = value; OnPropertyChanged("ValidityPeriod"); }
            }
            
            private Hl7.Fhir.Model.Period _ValidityPeriod;
            
            /// <summary>
            /// Number of refills authorized
            /// </summary>
            [FhirElement("numberOfRepeatsAllowed", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt NumberOfRepeatsAllowedElement
            {
                get { return _NumberOfRepeatsAllowedElement; }
                set { _NumberOfRepeatsAllowedElement = value; OnPropertyChanged("NumberOfRepeatsAllowedElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _NumberOfRepeatsAllowedElement;
            
            /// <summary>
            /// Number of refills authorized
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? NumberOfRepeatsAllowed
            {
                get { return NumberOfRepeatsAllowedElement != null ? NumberOfRepeatsAllowedElement.Value : null; }
                set
                {
                    if (value == null)
                        NumberOfRepeatsAllowedElement = null;
                    else
                        NumberOfRepeatsAllowedElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("NumberOfRepeatsAllowed");
                }
            }
            
            /// <summary>
            /// Amount of medication to supply per dispense
            /// </summary>
            [FhirElement("quantity", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _Quantity;
            
            /// <summary>
            /// Number of days supply per dispense
            /// </summary>
            [FhirElement("expectedSupplyDuration", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.STU3.Duration ExpectedSupplyDuration
            {
                get { return _ExpectedSupplyDuration; }
                set { _ExpectedSupplyDuration = value; OnPropertyChanged("ExpectedSupplyDuration"); }
            }
            
            private Hl7.Fhir.Model.STU3.Duration _ExpectedSupplyDuration;
            
            /// <summary>
            /// Intended dispenser
            /// </summary>
            [FhirElement("performer", Order=80)]
            [CLSCompliant(false)]
            [References("Organization")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Performer
            {
                get { return _Performer; }
                set { _Performer = value; OnPropertyChanged("Performer"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Performer;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("DispenseRequestComponent");
                base.Serialize(sink);
                sink.Element("validityPeriod", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ValidityPeriod?.Serialize(sink);
                sink.Element("numberOfRepeatsAllowed", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); NumberOfRepeatsAllowedElement?.Serialize(sink);
                sink.Element("quantity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Quantity?.Serialize(sink);
                sink.Element("expectedSupplyDuration", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ExpectedSupplyDuration?.Serialize(sink);
                sink.Element("performer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Performer?.Serialize(sink);
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
                    case "validityPeriod":
                        ValidityPeriod = source.Get<Hl7.Fhir.Model.Period>();
                        return true;
                    case "numberOfRepeatsAllowed":
                        NumberOfRepeatsAllowedElement = source.Get<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "quantity":
                        Quantity = source.Get<Hl7.Fhir.Model.SimpleQuantity>();
                        return true;
                    case "expectedSupplyDuration":
                        ExpectedSupplyDuration = source.Get<Hl7.Fhir.Model.STU3.Duration>();
                        return true;
                    case "performer":
                        Performer = source.Get<Hl7.Fhir.Model.ResourceReference>();
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
                    case "validityPeriod":
                        ValidityPeriod = source.Populate(ValidityPeriod);
                        return true;
                    case "numberOfRepeatsAllowed":
                        NumberOfRepeatsAllowedElement = source.PopulateValue(NumberOfRepeatsAllowedElement);
                        return true;
                    case "_numberOfRepeatsAllowed":
                        NumberOfRepeatsAllowedElement = source.Populate(NumberOfRepeatsAllowedElement);
                        return true;
                    case "quantity":
                        Quantity = source.Populate(Quantity);
                        return true;
                    case "expectedSupplyDuration":
                        ExpectedSupplyDuration = source.Populate(ExpectedSupplyDuration);
                        return true;
                    case "performer":
                        Performer = source.Populate(Performer);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DispenseRequestComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ValidityPeriod != null) dest.ValidityPeriod = (Hl7.Fhir.Model.Period)ValidityPeriod.DeepCopy();
                    if(NumberOfRepeatsAllowedElement != null) dest.NumberOfRepeatsAllowedElement = (Hl7.Fhir.Model.PositiveInt)NumberOfRepeatsAllowedElement.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                    if(ExpectedSupplyDuration != null) dest.ExpectedSupplyDuration = (Hl7.Fhir.Model.STU3.Duration)ExpectedSupplyDuration.DeepCopy();
                    if(Performer != null) dest.Performer = (Hl7.Fhir.Model.ResourceReference)Performer.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new DispenseRequestComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DispenseRequestComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ValidityPeriod, otherT.ValidityPeriod)) return false;
                if( !DeepComparable.Matches(NumberOfRepeatsAllowedElement, otherT.NumberOfRepeatsAllowedElement)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(ExpectedSupplyDuration, otherT.ExpectedSupplyDuration)) return false;
                if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DispenseRequestComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ValidityPeriod, otherT.ValidityPeriod)) return false;
                if( !DeepComparable.IsExactly(NumberOfRepeatsAllowedElement, otherT.NumberOfRepeatsAllowedElement)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(ExpectedSupplyDuration, otherT.ExpectedSupplyDuration)) return false;
                if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (ValidityPeriod != null) yield return ValidityPeriod;
                    if (NumberOfRepeatsAllowedElement != null) yield return NumberOfRepeatsAllowedElement;
                    if (Quantity != null) yield return Quantity;
                    if (ExpectedSupplyDuration != null) yield return ExpectedSupplyDuration;
                    if (Performer != null) yield return Performer;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (ValidityPeriod != null) yield return new ElementValue("validityPeriod", ValidityPeriod);
                    if (NumberOfRepeatsAllowedElement != null) yield return new ElementValue("numberOfRepeatsAllowed", NumberOfRepeatsAllowedElement);
                    if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                    if (ExpectedSupplyDuration != null) yield return new ElementValue("expectedSupplyDuration", ExpectedSupplyDuration);
                    if (Performer != null) yield return new ElementValue("performer", Performer);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "SubstitutionComponent")]
        [DataContract]
        public partial class SubstitutionComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IMedicationRequestSubstitutionComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SubstitutionComponent"; } }
            
            /// <summary>
            /// Whether substitution is allowed or not
            /// </summary>
            [FhirElement("allowed", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean AllowedElement
            {
                get { return _AllowedElement; }
                set { _AllowedElement = value; OnPropertyChanged("AllowedElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _AllowedElement;
            
            /// <summary>
            /// Whether substitution is allowed or not
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Allowed
            {
                get { return AllowedElement != null ? AllowedElement.Value : null; }
                set
                {
                    if (value == null)
                        AllowedElement = null;
                    else
                        AllowedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Allowed");
                }
            }
            
            /// <summary>
            /// Why should (not) substitution be made
            /// </summary>
            [FhirElement("reason", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Reason
            {
                get { return _Reason; }
                set { _Reason = value; OnPropertyChanged("Reason"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Reason;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SubstitutionComponent");
                base.Serialize(sink);
                sink.Element("allowed", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); AllowedElement?.Serialize(sink);
                sink.Element("reason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Reason?.Serialize(sink);
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
                    case "allowed":
                        AllowedElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                        return true;
                    case "reason":
                        Reason = source.Get<Hl7.Fhir.Model.CodeableConcept>();
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
                    case "allowed":
                        AllowedElement = source.PopulateValue(AllowedElement);
                        return true;
                    case "_allowed":
                        AllowedElement = source.Populate(AllowedElement);
                        return true;
                    case "reason":
                        Reason = source.Populate(Reason);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SubstitutionComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(AllowedElement != null) dest.AllowedElement = (Hl7.Fhir.Model.FhirBoolean)AllowedElement.DeepCopy();
                    if(Reason != null) dest.Reason = (Hl7.Fhir.Model.CodeableConcept)Reason.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new SubstitutionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SubstitutionComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(AllowedElement, otherT.AllowedElement)) return false;
                if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SubstitutionComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(AllowedElement, otherT.AllowedElement)) return false;
                if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (AllowedElement != null) yield return AllowedElement;
                    if (Reason != null) yield return Reason;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (AllowedElement != null) yield return new ElementValue("allowed", AllowedElement);
                    if (Reason != null) yield return new ElementValue("reason", Reason);
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IDosage> Hl7.Fhir.Model.IMedicationRequest.DosageInstruction { get { return DosageInstruction; } }
        
        [NotMapped]
        Hl7.Fhir.Model.IMedicationRequestDispenseRequestComponent Hl7.Fhir.Model.IMedicationRequest.DispenseRequest { get { return DispenseRequest; } }
        
        [NotMapped]
        Hl7.Fhir.Model.IMedicationRequestSubstitutionComponent Hl7.Fhir.Model.IMedicationRequest.Substitution { get { return Substitution; } }
    
        
        /// <summary>
        /// External ids for this request
        /// </summary>
        [FhirElement("identifier", Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Protocol or definition
        /// </summary>
        [FhirElement("definition", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [References("ActivityDefinition","PlanDefinition")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Definition
        {
            get { if(_Definition==null) _Definition = new List<Hl7.Fhir.Model.ResourceReference>(); return _Definition; }
            set { _Definition = value; OnPropertyChanged("Definition"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Definition;
        
        /// <summary>
        /// What request fulfills
        /// </summary>
        [FhirElement("basedOn", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [References("CarePlan","MedicationRequest","ProcedureRequest","ReferralRequest")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> BasedOn
        {
            get { if(_BasedOn==null) _BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(); return _BasedOn; }
            set { _BasedOn = value; OnPropertyChanged("BasedOn"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _BasedOn;
        
        /// <summary>
        /// Composite request this is part of
        /// </summary>
        [FhirElement("groupIdentifier", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier GroupIdentifier
        {
            get { return _GroupIdentifier; }
            set { _GroupIdentifier = value; OnPropertyChanged("GroupIdentifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _GroupIdentifier;
        
        /// <summary>
        /// active | on-hold | cancelled | completed | entered-in-error | stopped | draft | unknown
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.STU3.MedicationRequestStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.STU3.MedicationRequestStatus> _StatusElement;
        
        /// <summary>
        /// active | on-hold | cancelled | completed | entered-in-error | stopped | draft | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.STU3.MedicationRequestStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.STU3.MedicationRequestStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// proposal | plan | order | instance-order
        /// </summary>
        [FhirElement("intent", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.STU3.MedicationRequestIntent> IntentElement
        {
            get { return _IntentElement; }
            set { _IntentElement = value; OnPropertyChanged("IntentElement"); }
        }
        
        private Code<Hl7.Fhir.Model.STU3.MedicationRequestIntent> _IntentElement;
        
        /// <summary>
        /// proposal | plan | order | instance-order
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.STU3.MedicationRequestIntent? Intent
        {
            get { return IntentElement != null ? IntentElement.Value : null; }
            set
            {
                if (value == null)
                    IntentElement = null;
                else
                    IntentElement = new Code<Hl7.Fhir.Model.STU3.MedicationRequestIntent>(value);
                OnPropertyChanged("Intent");
            }
        }
        
        /// <summary>
        /// Type of medication usage
        /// </summary>
        [FhirElement("category", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Category
        {
            get { return _Category; }
            set { _Category = value; OnPropertyChanged("Category"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Category;
        
        /// <summary>
        /// routine | urgent | stat | asap
        /// </summary>
        [FhirElement("priority", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.STU3.MedicationRequestPriority> PriorityElement
        {
            get { return _PriorityElement; }
            set { _PriorityElement = value; OnPropertyChanged("PriorityElement"); }
        }
        
        private Code<Hl7.Fhir.Model.STU3.MedicationRequestPriority> _PriorityElement;
        
        /// <summary>
        /// routine | urgent | stat | asap
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.STU3.MedicationRequestPriority? Priority
        {
            get { return PriorityElement != null ? PriorityElement.Value : null; }
            set
            {
                if (value == null)
                    PriorityElement = null;
                else
                    PriorityElement = new Code<Hl7.Fhir.Model.STU3.MedicationRequestPriority>(value);
                OnPropertyChanged("Priority");
            }
        }
        
        /// <summary>
        /// Medication to be taken
        /// </summary>
        [FhirElement("medication", InSummary=Hl7.Fhir.Model.Version.All, Order=170, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Element Medication
        {
            get { return _Medication; }
            set { _Medication = value; OnPropertyChanged("Medication"); }
        }
        
        private Hl7.Fhir.Model.Element _Medication;
        
        /// <summary>
        /// Who or group medication request is for
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
        /// Created during encounter/admission/stay
        /// </summary>
        [FhirElement("context", Order=190)]
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
        /// Information to support ordering of the medication
        /// </summary>
        [FhirElement("supportingInformation", Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> SupportingInformation
        {
            get { if(_SupportingInformation==null) _SupportingInformation = new List<Hl7.Fhir.Model.ResourceReference>(); return _SupportingInformation; }
            set { _SupportingInformation = value; OnPropertyChanged("SupportingInformation"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _SupportingInformation;
        
        /// <summary>
        /// When request was initially authored
        /// </summary>
        [FhirElement("authoredOn", InSummary=Hl7.Fhir.Model.Version.All, Order=210)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime AuthoredOnElement
        {
            get { return _AuthoredOnElement; }
            set { _AuthoredOnElement = value; OnPropertyChanged("AuthoredOnElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _AuthoredOnElement;
        
        /// <summary>
        /// When request was initially authored
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string AuthoredOn
        {
            get { return AuthoredOnElement != null ? AuthoredOnElement.Value : null; }
            set
            {
                if (value == null)
                    AuthoredOnElement = null;
                else
                    AuthoredOnElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("AuthoredOn");
            }
        }
        
        /// <summary>
        /// Who/What requested the Request
        /// </summary>
        [FhirElement("requester", InSummary=Hl7.Fhir.Model.Version.All, Order=220)]
        [CLSCompliant(false)]
        [DataMember]
        public RequesterComponent Requester
        {
            get { return _Requester; }
            set { _Requester = value; OnPropertyChanged("Requester"); }
        }
        
        private RequesterComponent _Requester;
        
        /// <summary>
        /// Person who entered the request
        /// </summary>
        [FhirElement("recorder", Order=230)]
        [CLSCompliant(false)]
        [References("Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Recorder
        {
            get { return _Recorder; }
            set { _Recorder = value; OnPropertyChanged("Recorder"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Recorder;
        
        /// <summary>
        /// Reason or indication for writing the prescription
        /// </summary>
        [FhirElement("reasonCode", Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ReasonCode
        {
            get { if(_ReasonCode==null) _ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ReasonCode; }
            set { _ReasonCode = value; OnPropertyChanged("ReasonCode"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ReasonCode;
        
        /// <summary>
        /// Condition or Observation that supports why the prescription is being written
        /// </summary>
        [FhirElement("reasonReference", Order=250)]
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
        /// Information about the prescription
        /// </summary>
        [FhirElement("note", Order=260)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Note;
        
        /// <summary>
        /// How the medication should be taken
        /// </summary>
        [FhirElement("dosageInstruction", Order=270)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.STU3.Dosage> DosageInstruction
        {
            get { if(_DosageInstruction==null) _DosageInstruction = new List<Hl7.Fhir.Model.STU3.Dosage>(); return _DosageInstruction; }
            set { _DosageInstruction = value; OnPropertyChanged("DosageInstruction"); }
        }
        
        private List<Hl7.Fhir.Model.STU3.Dosage> _DosageInstruction;
        
        /// <summary>
        /// Medication supply authorization
        /// </summary>
        [FhirElement("dispenseRequest", Order=280)]
        [DataMember]
        public DispenseRequestComponent DispenseRequest
        {
            get { return _DispenseRequest; }
            set { _DispenseRequest = value; OnPropertyChanged("DispenseRequest"); }
        }
        
        private DispenseRequestComponent _DispenseRequest;
        
        /// <summary>
        /// Any restrictions on medication substitution
        /// </summary>
        [FhirElement("substitution", Order=290)]
        [DataMember]
        public SubstitutionComponent Substitution
        {
            get { return _Substitution; }
            set { _Substitution = value; OnPropertyChanged("Substitution"); }
        }
        
        private SubstitutionComponent _Substitution;
        
        /// <summary>
        /// An order/prescription that is being replaced
        /// </summary>
        [FhirElement("priorPrescription", Order=300)]
        [CLSCompliant(false)]
        [References("MedicationRequest")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference PriorPrescription
        {
            get { return _PriorPrescription; }
            set { _PriorPrescription = value; OnPropertyChanged("PriorPrescription"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _PriorPrescription;
        
        /// <summary>
        /// Clinical Issue with action
        /// </summary>
        [FhirElement("detectedIssue", Order=310)]
        [CLSCompliant(false)]
        [References("DetectedIssue")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> DetectedIssue
        {
            get { if(_DetectedIssue==null) _DetectedIssue = new List<Hl7.Fhir.Model.ResourceReference>(); return _DetectedIssue; }
            set { _DetectedIssue = value; OnPropertyChanged("DetectedIssue"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _DetectedIssue;
        
        /// <summary>
        /// A list of events of interest in the lifecycle
        /// </summary>
        [FhirElement("eventHistory", Order=320)]
        [CLSCompliant(false)]
        [References("Provenance")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> EventHistory
        {
            get { if(_EventHistory==null) _EventHistory = new List<Hl7.Fhir.Model.ResourceReference>(); return _EventHistory; }
            set { _EventHistory = value; OnPropertyChanged("EventHistory"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _EventHistory;
    
    
        public static ElementDefinitionConstraint[] MedicationRequest_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "mps-1",
                severity: ConstraintSeverity.Warning,
                expression: "requester.all((agent.resolve().empty()) or (agent.resolve() is Device) or (agent.resolve() is Practitioner) or onBehalfOf.exists().not())",
                human: "onBehalfOf can only be specified if agent is practitioner or device",
                xpath: "contains(f:agent/f:reference/@value, '/Practitioner/') or contains(f:agent/f:reference/@value, '/Device/') or not(exists(f:onBehalfOf))"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(MedicationRequest_Constraints);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as MedicationRequest;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Definition != null) dest.Definition = new List<Hl7.Fhir.Model.ResourceReference>(Definition.DeepCopy());
                if(BasedOn != null) dest.BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(BasedOn.DeepCopy());
                if(GroupIdentifier != null) dest.GroupIdentifier = (Hl7.Fhir.Model.Identifier)GroupIdentifier.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.STU3.MedicationRequestStatus>)StatusElement.DeepCopy();
                if(IntentElement != null) dest.IntentElement = (Code<Hl7.Fhir.Model.STU3.MedicationRequestIntent>)IntentElement.DeepCopy();
                if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                if(PriorityElement != null) dest.PriorityElement = (Code<Hl7.Fhir.Model.STU3.MedicationRequestPriority>)PriorityElement.DeepCopy();
                if(Medication != null) dest.Medication = (Hl7.Fhir.Model.Element)Medication.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Context != null) dest.Context = (Hl7.Fhir.Model.ResourceReference)Context.DeepCopy();
                if(SupportingInformation != null) dest.SupportingInformation = new List<Hl7.Fhir.Model.ResourceReference>(SupportingInformation.DeepCopy());
                if(AuthoredOnElement != null) dest.AuthoredOnElement = (Hl7.Fhir.Model.FhirDateTime)AuthoredOnElement.DeepCopy();
                if(Requester != null) dest.Requester = (RequesterComponent)Requester.DeepCopy();
                if(Recorder != null) dest.Recorder = (Hl7.Fhir.Model.ResourceReference)Recorder.DeepCopy();
                if(ReasonCode != null) dest.ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(ReasonCode.DeepCopy());
                if(ReasonReference != null) dest.ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(ReasonReference.DeepCopy());
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                if(DosageInstruction != null) dest.DosageInstruction = new List<Hl7.Fhir.Model.STU3.Dosage>(DosageInstruction.DeepCopy());
                if(DispenseRequest != null) dest.DispenseRequest = (DispenseRequestComponent)DispenseRequest.DeepCopy();
                if(Substitution != null) dest.Substitution = (SubstitutionComponent)Substitution.DeepCopy();
                if(PriorPrescription != null) dest.PriorPrescription = (Hl7.Fhir.Model.ResourceReference)PriorPrescription.DeepCopy();
                if(DetectedIssue != null) dest.DetectedIssue = new List<Hl7.Fhir.Model.ResourceReference>(DetectedIssue.DeepCopy());
                if(EventHistory != null) dest.EventHistory = new List<Hl7.Fhir.Model.ResourceReference>(EventHistory.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new MedicationRequest());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as MedicationRequest;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Definition, otherT.Definition)) return false;
            if( !DeepComparable.Matches(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.Matches(GroupIdentifier, otherT.GroupIdentifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(IntentElement, otherT.IntentElement)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(PriorityElement, otherT.PriorityElement)) return false;
            if( !DeepComparable.Matches(Medication, otherT.Medication)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Context, otherT.Context)) return false;
            if( !DeepComparable.Matches(SupportingInformation, otherT.SupportingInformation)) return false;
            if( !DeepComparable.Matches(AuthoredOnElement, otherT.AuthoredOnElement)) return false;
            if( !DeepComparable.Matches(Requester, otherT.Requester)) return false;
            if( !DeepComparable.Matches(Recorder, otherT.Recorder)) return false;
            if( !DeepComparable.Matches(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.Matches(ReasonReference, otherT.ReasonReference)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            if( !DeepComparable.Matches(DosageInstruction, otherT.DosageInstruction)) return false;
            if( !DeepComparable.Matches(DispenseRequest, otherT.DispenseRequest)) return false;
            if( !DeepComparable.Matches(Substitution, otherT.Substitution)) return false;
            if( !DeepComparable.Matches(PriorPrescription, otherT.PriorPrescription)) return false;
            if( !DeepComparable.Matches(DetectedIssue, otherT.DetectedIssue)) return false;
            if( !DeepComparable.Matches(EventHistory, otherT.EventHistory)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as MedicationRequest;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Definition, otherT.Definition)) return false;
            if( !DeepComparable.IsExactly(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.IsExactly(GroupIdentifier, otherT.GroupIdentifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(IntentElement, otherT.IntentElement)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(PriorityElement, otherT.PriorityElement)) return false;
            if( !DeepComparable.IsExactly(Medication, otherT.Medication)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Context, otherT.Context)) return false;
            if( !DeepComparable.IsExactly(SupportingInformation, otherT.SupportingInformation)) return false;
            if( !DeepComparable.IsExactly(AuthoredOnElement, otherT.AuthoredOnElement)) return false;
            if( !DeepComparable.IsExactly(Requester, otherT.Requester)) return false;
            if( !DeepComparable.IsExactly(Recorder, otherT.Recorder)) return false;
            if( !DeepComparable.IsExactly(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.IsExactly(ReasonReference, otherT.ReasonReference)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            if( !DeepComparable.IsExactly(DosageInstruction, otherT.DosageInstruction)) return false;
            if( !DeepComparable.IsExactly(DispenseRequest, otherT.DispenseRequest)) return false;
            if( !DeepComparable.IsExactly(Substitution, otherT.Substitution)) return false;
            if( !DeepComparable.IsExactly(PriorPrescription, otherT.PriorPrescription)) return false;
            if( !DeepComparable.IsExactly(DetectedIssue, otherT.DetectedIssue)) return false;
            if( !DeepComparable.IsExactly(EventHistory, otherT.EventHistory)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("MedicationRequest");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
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
            sink.Element("groupIdentifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); GroupIdentifier?.Serialize(sink);
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); StatusElement?.Serialize(sink);
            sink.Element("intent", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); IntentElement?.Serialize(sink);
            sink.Element("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Category?.Serialize(sink);
            sink.Element("priority", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PriorityElement?.Serialize(sink);
            sink.Element("medication", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, true); Medication?.Serialize(sink);
            sink.Element("subject", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Subject?.Serialize(sink);
            sink.Element("context", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Context?.Serialize(sink);
            sink.BeginList("supportingInformation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in SupportingInformation)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("authoredOn", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); AuthoredOnElement?.Serialize(sink);
            sink.Element("requester", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Requester?.Serialize(sink);
            sink.Element("recorder", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Recorder?.Serialize(sink);
            sink.BeginList("reasonCode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in ReasonCode)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("reasonReference", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in ReasonReference)
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
            sink.BeginList("dosageInstruction", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in DosageInstruction)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("dispenseRequest", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DispenseRequest?.Serialize(sink);
            sink.Element("substitution", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Substitution?.Serialize(sink);
            sink.Element("priorPrescription", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PriorPrescription?.Serialize(sink);
            sink.BeginList("detectedIssue", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in DetectedIssue)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("eventHistory", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in EventHistory)
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
                case "groupIdentifier":
                    GroupIdentifier = source.Get<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.STU3.MedicationRequestStatus>>();
                    return true;
                case "intent":
                    IntentElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.STU3.MedicationRequestIntent>>();
                    return true;
                case "category":
                    Category = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "priority":
                    PriorityElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.STU3.MedicationRequestPriority>>();
                    return true;
                case "medicationCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Medication, "medication");
                    Medication = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "medicationReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Medication, "medication");
                    Medication = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "subject":
                    Subject = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "context":
                    Context = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "supportingInformation":
                    SupportingInformation = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "authoredOn":
                    AuthoredOnElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "requester":
                    Requester = source.Get<RequesterComponent>();
                    return true;
                case "recorder":
                    Recorder = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "reasonCode":
                    ReasonCode = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "reasonReference":
                    ReasonReference = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "note":
                    Note = source.GetList<Hl7.Fhir.Model.Annotation>();
                    return true;
                case "dosageInstruction":
                    DosageInstruction = source.GetList<Hl7.Fhir.Model.STU3.Dosage>();
                    return true;
                case "dispenseRequest":
                    DispenseRequest = source.Get<DispenseRequestComponent>();
                    return true;
                case "substitution":
                    Substitution = source.Get<SubstitutionComponent>();
                    return true;
                case "priorPrescription":
                    PriorPrescription = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "detectedIssue":
                    DetectedIssue = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "eventHistory":
                    EventHistory = source.GetList<Hl7.Fhir.Model.ResourceReference>();
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
                case "groupIdentifier":
                    GroupIdentifier = source.Populate(GroupIdentifier);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "intent":
                    IntentElement = source.PopulateValue(IntentElement);
                    return true;
                case "_intent":
                    IntentElement = source.Populate(IntentElement);
                    return true;
                case "category":
                    Category = source.Populate(Category);
                    return true;
                case "priority":
                    PriorityElement = source.PopulateValue(PriorityElement);
                    return true;
                case "_priority":
                    PriorityElement = source.Populate(PriorityElement);
                    return true;
                case "medicationCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Medication, "medication");
                    Medication = source.Populate(Medication as Hl7.Fhir.Model.CodeableConcept);
                    return true;
                case "medicationReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Medication, "medication");
                    Medication = source.Populate(Medication as Hl7.Fhir.Model.ResourceReference);
                    return true;
                case "subject":
                    Subject = source.Populate(Subject);
                    return true;
                case "context":
                    Context = source.Populate(Context);
                    return true;
                case "supportingInformation":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "authoredOn":
                    AuthoredOnElement = source.PopulateValue(AuthoredOnElement);
                    return true;
                case "_authoredOn":
                    AuthoredOnElement = source.Populate(AuthoredOnElement);
                    return true;
                case "requester":
                    Requester = source.Populate(Requester);
                    return true;
                case "recorder":
                    Recorder = source.Populate(Recorder);
                    return true;
                case "reasonCode":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "reasonReference":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "note":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "dosageInstruction":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "dispenseRequest":
                    DispenseRequest = source.Populate(DispenseRequest);
                    return true;
                case "substitution":
                    Substitution = source.Populate(Substitution);
                    return true;
                case "priorPrescription":
                    PriorPrescription = source.Populate(PriorPrescription);
                    return true;
                case "detectedIssue":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "eventHistory":
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
                case "supportingInformation":
                    source.PopulateListItem(SupportingInformation, index);
                    return true;
                case "reasonCode":
                    source.PopulateListItem(ReasonCode, index);
                    return true;
                case "reasonReference":
                    source.PopulateListItem(ReasonReference, index);
                    return true;
                case "note":
                    source.PopulateListItem(Note, index);
                    return true;
                case "dosageInstruction":
                    source.PopulateListItem(DosageInstruction, index);
                    return true;
                case "detectedIssue":
                    source.PopulateListItem(DetectedIssue, index);
                    return true;
                case "eventHistory":
                    source.PopulateListItem(EventHistory, index);
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
                if (GroupIdentifier != null) yield return GroupIdentifier;
                if (StatusElement != null) yield return StatusElement;
                if (IntentElement != null) yield return IntentElement;
                if (Category != null) yield return Category;
                if (PriorityElement != null) yield return PriorityElement;
                if (Medication != null) yield return Medication;
                if (Subject != null) yield return Subject;
                if (Context != null) yield return Context;
                foreach (var elem in SupportingInformation) { if (elem != null) yield return elem; }
                if (AuthoredOnElement != null) yield return AuthoredOnElement;
                if (Requester != null) yield return Requester;
                if (Recorder != null) yield return Recorder;
                foreach (var elem in ReasonCode) { if (elem != null) yield return elem; }
                foreach (var elem in ReasonReference) { if (elem != null) yield return elem; }
                foreach (var elem in Note) { if (elem != null) yield return elem; }
                foreach (var elem in DosageInstruction) { if (elem != null) yield return elem; }
                if (DispenseRequest != null) yield return DispenseRequest;
                if (Substitution != null) yield return Substitution;
                if (PriorPrescription != null) yield return PriorPrescription;
                foreach (var elem in DetectedIssue) { if (elem != null) yield return elem; }
                foreach (var elem in EventHistory) { if (elem != null) yield return elem; }
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
                if (GroupIdentifier != null) yield return new ElementValue("groupIdentifier", GroupIdentifier);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (IntentElement != null) yield return new ElementValue("intent", IntentElement);
                if (Category != null) yield return new ElementValue("category", Category);
                if (PriorityElement != null) yield return new ElementValue("priority", PriorityElement);
                if (Medication != null) yield return new ElementValue("medication", Medication);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Context != null) yield return new ElementValue("context", Context);
                foreach (var elem in SupportingInformation) { if (elem != null) yield return new ElementValue("supportingInformation", elem); }
                if (AuthoredOnElement != null) yield return new ElementValue("authoredOn", AuthoredOnElement);
                if (Requester != null) yield return new ElementValue("requester", Requester);
                if (Recorder != null) yield return new ElementValue("recorder", Recorder);
                foreach (var elem in ReasonCode) { if (elem != null) yield return new ElementValue("reasonCode", elem); }
                foreach (var elem in ReasonReference) { if (elem != null) yield return new ElementValue("reasonReference", elem); }
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                foreach (var elem in DosageInstruction) { if (elem != null) yield return new ElementValue("dosageInstruction", elem); }
                if (DispenseRequest != null) yield return new ElementValue("dispenseRequest", DispenseRequest);
                if (Substitution != null) yield return new ElementValue("substitution", Substitution);
                if (PriorPrescription != null) yield return new ElementValue("priorPrescription", PriorPrescription);
                foreach (var elem in DetectedIssue) { if (elem != null) yield return new ElementValue("detectedIssue", elem); }
                foreach (var elem in EventHistory) { if (elem != null) yield return new ElementValue("eventHistory", elem); }
            }
        }
    
    }

}
