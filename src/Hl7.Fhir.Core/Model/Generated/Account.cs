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
namespace Hl7.Fhir.Model
{
    [FhirType("Account", IsResource=true)]
    [DataContract]
    public partial class Account : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Account; } }
        [NotMapped]
        public override string TypeName { get { return "Account"; } }
        
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
        /// Human-readable label
        /// </summary>
        [FhirElement("name", InSummary=true, Order=100)]
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
        /// active | inactive
        /// </summary>
        [FhirElement("status", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.Code StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Hl7.Fhir.Model.Code _StatusElement;
        
        /// <summary>
        /// active | inactive
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Hl7.Fhir.Model.Code(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Valid from..to
        /// </summary>
        [FhirElement("activePeriod", InSummary=true, Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.Period ActivePeriod
        {
            get { return _ActivePeriod; }
            set { _ActivePeriod = value; OnPropertyChanged("ActivePeriod"); }
        }
        
        private Hl7.Fhir.Model.Period _ActivePeriod;
        
        /// <summary>
        /// Base currency in which balance is tracked
        /// </summary>
        [FhirElement("currency", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.Coding Currency
        {
            get { return _Currency; }
            set { _Currency = value; OnPropertyChanged("Currency"); }
        }
        
        private Hl7.Fhir.Model.Coding _Currency;
        
        /// <summary>
        /// How much is in account?
        /// </summary>
        [FhirElement("balance", InSummary=true, Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.Money Balance
        {
            get { return _Balance; }
            set { _Balance = value; OnPropertyChanged("Balance"); }
        }
        
        private Hl7.Fhir.Model.Money _Balance;
        
        /// <summary>
        /// Transaction window
        /// </summary>
        [FhirElement("coveragePeriod", InSummary=true, Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.Period CoveragePeriod
        {
            get { return _CoveragePeriod; }
            set { _CoveragePeriod = value; OnPropertyChanged("CoveragePeriod"); }
        }
        
        private Hl7.Fhir.Model.Period _CoveragePeriod;
        
        /// <summary>
        /// What is account tied to?
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=170)]
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
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Hl7.Fhir.Model.Code)StatusElement.DeepCopy();
                if(ActivePeriod != null) dest.ActivePeriod = (Hl7.Fhir.Model.Period)ActivePeriod.DeepCopy();
                if(Currency != null) dest.Currency = (Hl7.Fhir.Model.Coding)Currency.DeepCopy();
                if(Balance != null) dest.Balance = (Hl7.Fhir.Model.Money)Balance.DeepCopy();
                if(CoveragePeriod != null) dest.CoveragePeriod = (Hl7.Fhir.Model.Period)CoveragePeriod.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Owner != null) dest.Owner = (Hl7.Fhir.Model.ResourceReference)Owner.DeepCopy();
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
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
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(ActivePeriod, otherT.ActivePeriod)) return false;
            if( !DeepComparable.Matches(Currency, otherT.Currency)) return false;
            if( !DeepComparable.Matches(Balance, otherT.Balance)) return false;
            if( !DeepComparable.Matches(CoveragePeriod, otherT.CoveragePeriod)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Owner, otherT.Owner)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Account;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(ActivePeriod, otherT.ActivePeriod)) return false;
            if( !DeepComparable.IsExactly(Currency, otherT.Currency)) return false;
            if( !DeepComparable.IsExactly(Balance, otherT.Balance)) return false;
            if( !DeepComparable.IsExactly(CoveragePeriod, otherT.CoveragePeriod)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Owner, otherT.Owner)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				if (NameElement != null) yield return NameElement;
				if (Type != null) yield return Type;
				if (StatusElement != null) yield return StatusElement;
				if (ActivePeriod != null) yield return ActivePeriod;
				if (Currency != null) yield return Currency;
				if (Balance != null) yield return Balance;
				if (CoveragePeriod != null) yield return CoveragePeriod;
				if (Subject != null) yield return Subject;
				if (Owner != null) yield return Owner;
				if (DescriptionElement != null) yield return DescriptionElement;
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (NameElement != null) yield return new ElementValue("name", NameElement);
                if (Type != null) yield return new ElementValue("type", Type);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (ActivePeriod != null) yield return new ElementValue("activePeriod", ActivePeriod);
                if (Currency != null) yield return new ElementValue("currency", Currency);
                if (Balance != null) yield return new ElementValue("balance", Balance);
                if (CoveragePeriod != null) yield return new ElementValue("coveragePeriod", CoveragePeriod);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Owner != null) yield return new ElementValue("owner", Owner);
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
            }
        }

    }
    
}
