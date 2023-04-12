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
    /// Tracks balance, charges, for patient or cost center
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.STU3, "Account", IsResource=true)]
    [DataContract]
    public partial class Account : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IAccount, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Account; } }
        [NotMapped]
        public override string TypeName { get { return "Account"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "CoverageComponent")]
        [DataContract]
        public partial class CoverageComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "CoverageComponent"; } }
            
            /// <summary>
            /// The party(s) that are responsible for covering the payment of this account
            /// </summary>
            [FhirElement("coverage", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [References("Coverage")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Coverage
            {
                get { return _Coverage; }
                set { _Coverage = value; OnPropertyChanged("Coverage"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Coverage;
            
            /// <summary>
            /// The priority of the coverage in the context of this account
            /// </summary>
            [FhirElement("priority", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt PriorityElement
            {
                get { return _PriorityElement; }
                set { _PriorityElement = value; OnPropertyChanged("PriorityElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _PriorityElement;
            
            /// <summary>
            /// The priority of the coverage in the context of this account
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Priority
            {
                get { return PriorityElement != null ? PriorityElement.Value : null; }
                set
                {
                    if (value == null)
                        PriorityElement = null;
                    else
                        PriorityElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("Priority");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("CoverageComponent");
                base.Serialize(sink);
                sink.Element("coverage", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Coverage?.Serialize(sink);
                sink.Element("priority", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PriorityElement?.Serialize(sink);
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
                    case "coverage":
                        Coverage = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "priority":
                        PriorityElement = source.Get<Hl7.Fhir.Model.PositiveInt>();
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
                    case "coverage":
                        Coverage = source.Populate(Coverage);
                        return true;
                    case "priority":
                        PriorityElement = source.PopulateValue(PriorityElement);
                        return true;
                    case "_priority":
                        PriorityElement = source.Populate(PriorityElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CoverageComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Coverage != null) dest.Coverage = (Hl7.Fhir.Model.ResourceReference)Coverage.DeepCopy();
                    if(PriorityElement != null) dest.PriorityElement = (Hl7.Fhir.Model.PositiveInt)PriorityElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new CoverageComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CoverageComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Coverage, otherT.Coverage)) return false;
                if( !DeepComparable.Matches(PriorityElement, otherT.PriorityElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CoverageComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Coverage, otherT.Coverage)) return false;
                if( !DeepComparable.IsExactly(PriorityElement, otherT.PriorityElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Coverage != null) yield return Coverage;
                    if (PriorityElement != null) yield return PriorityElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Coverage != null) yield return new ElementValue("coverage", Coverage);
                    if (PriorityElement != null) yield return new ElementValue("priority", PriorityElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "GuarantorComponent")]
        [DataContract]
        public partial class GuarantorComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "GuarantorComponent"; } }
            
            /// <summary>
            /// Responsible entity
            /// </summary>
            [FhirElement("party", Order=40)]
            [CLSCompliant(false)]
            [References("Patient","RelatedPerson","Organization")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Party
            {
                get { return _Party; }
                set { _Party = value; OnPropertyChanged("Party"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Party;
            
            /// <summary>
            /// Credit or other hold applied
            /// </summary>
            [FhirElement("onHold", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean OnHoldElement
            {
                get { return _OnHoldElement; }
                set { _OnHoldElement = value; OnPropertyChanged("OnHoldElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _OnHoldElement;
            
            /// <summary>
            /// Credit or other hold applied
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? OnHold
            {
                get { return OnHoldElement != null ? OnHoldElement.Value : null; }
                set
                {
                    if (value == null)
                        OnHoldElement = null;
                    else
                        OnHoldElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("OnHold");
                }
            }
            
            /// <summary>
            /// Guarrantee account during
            /// </summary>
            [FhirElement("period", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Period Period
            {
                get { return _Period; }
                set { _Period = value; OnPropertyChanged("Period"); }
            }
            
            private Hl7.Fhir.Model.Period _Period;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("GuarantorComponent");
                base.Serialize(sink);
                sink.Element("party", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Party?.Serialize(sink);
                sink.Element("onHold", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); OnHoldElement?.Serialize(sink);
                sink.Element("period", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Period?.Serialize(sink);
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
                    case "party":
                        Party = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "onHold":
                        OnHoldElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                        return true;
                    case "period":
                        Period = source.Get<Hl7.Fhir.Model.Period>();
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
                    case "party":
                        Party = source.Populate(Party);
                        return true;
                    case "onHold":
                        OnHoldElement = source.PopulateValue(OnHoldElement);
                        return true;
                    case "_onHold":
                        OnHoldElement = source.Populate(OnHoldElement);
                        return true;
                    case "period":
                        Period = source.Populate(Period);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as GuarantorComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Party != null) dest.Party = (Hl7.Fhir.Model.ResourceReference)Party.DeepCopy();
                    if(OnHoldElement != null) dest.OnHoldElement = (Hl7.Fhir.Model.FhirBoolean)OnHoldElement.DeepCopy();
                    if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new GuarantorComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as GuarantorComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Party, otherT.Party)) return false;
                if( !DeepComparable.Matches(OnHoldElement, otherT.OnHoldElement)) return false;
                if( !DeepComparable.Matches(Period, otherT.Period)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as GuarantorComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Party, otherT.Party)) return false;
                if( !DeepComparable.IsExactly(OnHoldElement, otherT.OnHoldElement)) return false;
                if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Party != null) yield return Party;
                    if (OnHoldElement != null) yield return OnHoldElement;
                    if (Period != null) yield return Period;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Party != null) yield return new ElementValue("party", Party);
                    if (OnHoldElement != null) yield return new ElementValue("onHold", OnHoldElement);
                    if (Period != null) yield return new ElementValue("period", Period);
                }
            }
        
        
        }
    
        
        /// <summary>
        /// Account number
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
        /// active | inactive | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.STU3.AccountStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.STU3.AccountStatus> _StatusElement;
        
        /// <summary>
        /// active | inactive | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.STU3.AccountStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.STU3.AccountStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// E.g. patient, expense, depreciation
        /// </summary>
        [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Type;
        
        /// <summary>
        /// Human-readable label
        /// </summary>
        [FhirElement("name", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// Human-readable label
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Name
        {
            get { return NameElement != null ? NameElement.Value : null; }
            set
            {
                if (value == null)
                    NameElement = null;
                else
                    NameElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Name");
            }
        }
        
        /// <summary>
        /// What is account tied to?
        /// </summary>
        [FhirElement("subject", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [References("Patient","Device","Practitioner","Location","HealthcareService","Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Transaction window
        /// </summary>
        [FhirElement("period", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period
        {
            get { return _Period; }
            set { _Period = value; OnPropertyChanged("Period"); }
        }
        
        private Hl7.Fhir.Model.Period _Period;
        
        /// <summary>
        /// Time window that transactions may be posted to this account
        /// </summary>
        [FhirElement("active", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Period Active
        {
            get { return _Active; }
            set { _Active = value; OnPropertyChanged("Active"); }
        }
        
        private Hl7.Fhir.Model.Period _Active;
        
        /// <summary>
        /// How much is in account?
        /// </summary>
        [FhirElement("balance", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.STU3.Money Balance
        {
            get { return _Balance; }
            set { _Balance = value; OnPropertyChanged("Balance"); }
        }
        
        private Hl7.Fhir.Model.STU3.Money _Balance;
        
        /// <summary>
        /// The party(s) that are responsible for covering the payment of this account, and what order should they be applied to the account
        /// </summary>
        [FhirElement("coverage", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<CoverageComponent> Coverage
        {
            get { if(_Coverage==null) _Coverage = new List<CoverageComponent>(); return _Coverage; }
            set { _Coverage = value; OnPropertyChanged("Coverage"); }
        }
        
        private List<CoverageComponent> _Coverage;
        
        /// <summary>
        /// Who is responsible?
        /// </summary>
        [FhirElement("owner", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Owner
        {
            get { return _Owner; }
            set { _Owner = value; OnPropertyChanged("Owner"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Owner;
        
        /// <summary>
        /// Explanation of purpose/use
        /// </summary>
        [FhirElement("description", InSummary=Hl7.Fhir.Model.Version.All, Order=190)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
        /// <summary>
        /// Explanation of purpose/use
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Description
        {
            get { return DescriptionElement != null ? DescriptionElement.Value : null; }
            set
            {
                if (value == null)
                    DescriptionElement = null;
                else
                    DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Description");
            }
        }
        
        /// <summary>
        /// Responsible for the account
        /// </summary>
        [FhirElement("guarantor", Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<GuarantorComponent> Guarantor
        {
            get { if(_Guarantor==null) _Guarantor = new List<GuarantorComponent>(); return _Guarantor; }
            set { _Guarantor = value; OnPropertyChanged("Guarantor"); }
        }
        
        private List<GuarantorComponent> _Guarantor;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Account;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.STU3.AccountStatus>)StatusElement.DeepCopy();
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                if(Active != null) dest.Active = (Hl7.Fhir.Model.Period)Active.DeepCopy();
                if(Balance != null) dest.Balance = (Hl7.Fhir.Model.STU3.Money)Balance.DeepCopy();
                if(Coverage != null) dest.Coverage = new List<CoverageComponent>(Coverage.DeepCopy());
                if(Owner != null) dest.Owner = (Hl7.Fhir.Model.ResourceReference)Owner.DeepCopy();
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(Guarantor != null) dest.Guarantor = new List<GuarantorComponent>(Guarantor.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new Account());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Account;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Period, otherT.Period)) return false;
            if( !DeepComparable.Matches(Active, otherT.Active)) return false;
            if( !DeepComparable.Matches(Balance, otherT.Balance)) return false;
            if( !DeepComparable.Matches(Coverage, otherT.Coverage)) return false;
            if( !DeepComparable.Matches(Owner, otherT.Owner)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(Guarantor, otherT.Guarantor)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Account;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
            if( !DeepComparable.IsExactly(Active, otherT.Active)) return false;
            if( !DeepComparable.IsExactly(Balance, otherT.Balance)) return false;
            if( !DeepComparable.IsExactly(Coverage, otherT.Coverage)) return false;
            if( !DeepComparable.IsExactly(Owner, otherT.Owner)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(Guarantor, otherT.Guarantor)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("Account");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); StatusElement?.Serialize(sink);
            sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Type?.Serialize(sink);
            sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NameElement?.Serialize(sink);
            sink.Element("subject", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Subject?.Serialize(sink);
            sink.Element("period", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Period?.Serialize(sink);
            sink.Element("active", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Active?.Serialize(sink);
            sink.Element("balance", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Balance?.Serialize(sink);
            sink.BeginList("coverage", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Coverage)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("owner", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Owner?.Serialize(sink);
            sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DescriptionElement?.Serialize(sink);
            sink.BeginList("guarantor", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Guarantor)
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
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.STU3.AccountStatus>>();
                    return true;
                case "type":
                    Type = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "name":
                    NameElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "subject":
                    Subject = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "period":
                    Period = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "active":
                    Active = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "balance":
                    Balance = source.Get<Hl7.Fhir.Model.STU3.Money>();
                    return true;
                case "coverage":
                    Coverage = source.GetList<CoverageComponent>();
                    return true;
                case "owner":
                    Owner = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "description":
                    DescriptionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "guarantor":
                    Guarantor = source.GetList<GuarantorComponent>();
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
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "type":
                    Type = source.Populate(Type);
                    return true;
                case "name":
                    NameElement = source.PopulateValue(NameElement);
                    return true;
                case "_name":
                    NameElement = source.Populate(NameElement);
                    return true;
                case "subject":
                    Subject = source.Populate(Subject);
                    return true;
                case "period":
                    Period = source.Populate(Period);
                    return true;
                case "active":
                    Active = source.Populate(Active);
                    return true;
                case "balance":
                    Balance = source.Populate(Balance);
                    return true;
                case "coverage":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "owner":
                    Owner = source.Populate(Owner);
                    return true;
                case "description":
                    DescriptionElement = source.PopulateValue(DescriptionElement);
                    return true;
                case "_description":
                    DescriptionElement = source.Populate(DescriptionElement);
                    return true;
                case "guarantor":
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
                case "coverage":
                    source.PopulateListItem(Coverage, index);
                    return true;
                case "guarantor":
                    source.PopulateListItem(Guarantor, index);
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
                if (StatusElement != null) yield return StatusElement;
                if (Type != null) yield return Type;
                if (NameElement != null) yield return NameElement;
                if (Subject != null) yield return Subject;
                if (Period != null) yield return Period;
                if (Active != null) yield return Active;
                if (Balance != null) yield return Balance;
                foreach (var elem in Coverage) { if (elem != null) yield return elem; }
                if (Owner != null) yield return Owner;
                if (DescriptionElement != null) yield return DescriptionElement;
                foreach (var elem in Guarantor) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (Type != null) yield return new ElementValue("type", Type);
                if (NameElement != null) yield return new ElementValue("name", NameElement);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Period != null) yield return new ElementValue("period", Period);
                if (Active != null) yield return new ElementValue("active", Active);
                if (Balance != null) yield return new ElementValue("balance", Balance);
                foreach (var elem in Coverage) { if (elem != null) yield return new ElementValue("coverage", elem); }
                if (Owner != null) yield return new ElementValue("owner", Owner);
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                foreach (var elem in Guarantor) { if (elem != null) yield return new ElementValue("guarantor", elem); }
            }
        }
    
    }

}
