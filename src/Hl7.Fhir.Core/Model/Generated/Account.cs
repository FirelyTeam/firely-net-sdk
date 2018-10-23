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
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Tracks balance, charges, for patient or cost center
    /// </summary>
    [FhirType("Account", IsResource=true)]
    [DataContract]
    public partial class Account : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Account; } }
        [NotMapped]
        public override string TypeName { get { return "Account"; } }
        
        /// <summary>
        /// Indicates whether the account is available to be used.
        /// (url: http://hl7.org/fhir/ValueSet/account-status)
        /// </summary>
        [FhirEnumeration("AccountStatus")]
        public enum AccountStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/account-status)
            /// </summary>
            [EnumLiteral("active", "http://hl7.org/fhir/account-status"), Description("Active")]
            Active,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/account-status)
            /// </summary>
            [EnumLiteral("inactive", "http://hl7.org/fhir/account-status"), Description("Inactive")]
            Inactive,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/account-status)
            /// </summary>
            [EnumLiteral("entered-in-error", "http://hl7.org/fhir/account-status"), Description("Entered in error")]
            EnteredInError,
        }

        [FhirType("CoverageComponent")]
        [DataContract]
        public partial class CoverageComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "CoverageComponent"; } }
            
            /// <summary>
            /// The party(s) that are responsible for covering the payment of this account
            /// </summary>
            [FhirElement("coverage", InSummary=true, Order=40)]
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
            [FhirElement("priority", InSummary=true, Order=50)]
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
                    if (!value.HasValue)
                        PriorityElement = null; 
                    else
                        PriorityElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("Priority");
                }
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
        
        
        [FhirType("GuarantorComponent")]
        [DataContract]
        public partial class GuarantorComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
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
                    if (!value.HasValue)
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
        /// active | inactive | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary=true, Order=100)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Account.AccountStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Account.AccountStatus> _StatusElement;
        
        /// <summary>
        /// active | inactive | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Account.AccountStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.Account.AccountStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// E.g. patient, expense, depreciation
        /// </summary>
        [FhirElement("type", InSummary=true, Order=110)]
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
        [FhirElement("name", InSummary=true, Order=120)]
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
        [FhirElement("subject", InSummary=true, Order=130)]
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
        [FhirElement("period", InSummary=true, Order=140)]
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
        [FhirElement("active", InSummary=true, Order=150)]
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
        public Money Balance
        {
            get { return _Balance; }
            set { _Balance = value; OnPropertyChanged("Balance"); }
        }
        
        private Money _Balance;
        
        /// <summary>
        /// The party(s) that are responsible for covering the payment of this account, and what order should they be applied to the account
        /// </summary>
        [FhirElement("coverage", InSummary=true, Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Account.CoverageComponent> Coverage
        {
            get { if(_Coverage==null) _Coverage = new List<Hl7.Fhir.Model.Account.CoverageComponent>(); return _Coverage; }
            set { _Coverage = value; OnPropertyChanged("Coverage"); }
        }
        
        private List<Hl7.Fhir.Model.Account.CoverageComponent> _Coverage;
        
        /// <summary>
        /// Who is responsible?
        /// </summary>
        [FhirElement("owner", InSummary=true, Order=180)]
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
        [FhirElement("description", InSummary=true, Order=190)]
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
        public List<Hl7.Fhir.Model.Account.GuarantorComponent> Guarantor
        {
            get { if(_Guarantor==null) _Guarantor = new List<Hl7.Fhir.Model.Account.GuarantorComponent>(); return _Guarantor; }
            set { _Guarantor = value; OnPropertyChanged("Guarantor"); }
        }
        
        private List<Hl7.Fhir.Model.Account.GuarantorComponent> _Guarantor;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Account;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Account.AccountStatus>)StatusElement.DeepCopy();
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                if(Active != null) dest.Active = (Hl7.Fhir.Model.Period)Active.DeepCopy();
                if(Balance != null) dest.Balance = (Money)Balance.DeepCopy();
                if(Coverage != null) dest.Coverage = new List<Hl7.Fhir.Model.Account.CoverageComponent>(Coverage.DeepCopy());
                if(Owner != null) dest.Owner = (Hl7.Fhir.Model.ResourceReference)Owner.DeepCopy();
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(Guarantor != null) dest.Guarantor = new List<Hl7.Fhir.Model.Account.GuarantorComponent>(Guarantor.DeepCopy());
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
