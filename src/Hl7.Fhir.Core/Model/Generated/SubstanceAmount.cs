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
// Generated for FHIR v4.0.1, v1.0.2, v3.0.1
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Chemical substances are a single substance type whose primary defining element is the molecular structure. Chemical substances shall be defined on the basis of their complete covalent molecular structure; the presence of a salt (counter-ion) and/or solvates (water, alcohols) is also captured. Purity, grade, physical form or particle size are not taken into account in the definition of a chemical substance or in the assignment of a Substance ID
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.All, "SubstanceAmount")]
    [DataContract]
    public partial class SubstanceAmount : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "SubstanceAmount"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.All, "ReferenceRangeComponent")]
        [DataContract]
        public partial class ReferenceRangeComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ReferenceRangeComponent"; } }
            
            /// <summary>
            /// Lower limit possible or expected
            /// </summary>
            [FhirElement("lowLimit", Versions=Hl7.Fhir.Model.Version.R4, InSummary=Hl7.Fhir.Model.Version.R4, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity LowLimit
            {
                get { return _LowLimit; }
                set { _LowLimit = value; OnPropertyChanged("LowLimit"); }
            }
            
            private Hl7.Fhir.Model.Quantity _LowLimit;
            
            /// <summary>
            /// Upper limit possible or expected
            /// </summary>
            [FhirElement("highLimit", Versions=Hl7.Fhir.Model.Version.R4, InSummary=Hl7.Fhir.Model.Version.R4, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Quantity HighLimit
            {
                get { return _HighLimit; }
                set { _HighLimit = value; OnPropertyChanged("HighLimit"); }
            }
            
            private Hl7.Fhir.Model.Quantity _HighLimit;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ReferenceRangeComponent");
                base.Serialize(sink);
                sink.Element("lowLimit", Hl7.Fhir.Model.Version.R4, Hl7.Fhir.Model.Version.R4, false, false); LowLimit?.Serialize(sink);
                sink.Element("highLimit", Hl7.Fhir.Model.Version.R4, Hl7.Fhir.Model.Version.R4, false, false); HighLimit?.Serialize(sink);
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
                    case "lowLimit" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        LowLimit = source.Get<Hl7.Fhir.Model.Quantity>();
                        return true;
                    case "highLimit" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        HighLimit = source.Get<Hl7.Fhir.Model.Quantity>();
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
                    case "lowLimit" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        LowLimit = source.Populate(LowLimit);
                        return true;
                    case "highLimit" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                        HighLimit = source.Populate(HighLimit);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ReferenceRangeComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(LowLimit != null) dest.LowLimit = (Hl7.Fhir.Model.Quantity)LowLimit.DeepCopy();
                    if(HighLimit != null) dest.HighLimit = (Hl7.Fhir.Model.Quantity)HighLimit.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ReferenceRangeComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ReferenceRangeComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(LowLimit, otherT.LowLimit)) return false;
                if( !DeepComparable.Matches(HighLimit, otherT.HighLimit)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ReferenceRangeComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(LowLimit, otherT.LowLimit)) return false;
                if( !DeepComparable.IsExactly(HighLimit, otherT.HighLimit)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (LowLimit != null) yield return LowLimit;
                    if (HighLimit != null) yield return HighLimit;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (LowLimit != null) yield return new ElementValue("lowLimit", LowLimit);
                    if (HighLimit != null) yield return new ElementValue("highLimit", HighLimit);
                }
            }
        
        
        }
    
        
        /// <summary>
        /// Used to capture quantitative values for a variety of elements. If only limits are given, the arithmetic mean would be the average. If only a single definite value for a given element is given, it would be captured in this field
        /// </summary>
        [FhirElement("amount", Versions=Hl7.Fhir.Model.Version.R4, InSummary=Hl7.Fhir.Model.Version.R4, Order=90, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(Version=Version.R4, Types=new[]{typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.FhirString)})]
        [DataMember]
        public Hl7.Fhir.Model.Element Amount
        {
            get { return _Amount; }
            set { _Amount = value; OnPropertyChanged("Amount"); }
        }
        
        private Hl7.Fhir.Model.Element _Amount;
        
        /// <summary>
        /// Most elements that require a quantitative value will also have a field called amount type. Amount type should always be specified because the actual value of the amount is often dependent on it. EXAMPLE: In capturing the actual relative amounts of substances or molecular fragments it is essential to indicate whether the amount refers to a mole ratio or weight ratio. For any given element an effort should be made to use same the amount type for all related definitional elements
        /// </summary>
        [FhirElement("amountType", Versions=Hl7.Fhir.Model.Version.R4, InSummary=Hl7.Fhir.Model.Version.R4, Order=100)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept AmountType
        {
            get { return _AmountType; }
            set { _AmountType = value; OnPropertyChanged("AmountType"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _AmountType;
        
        /// <summary>
        /// A textual comment on a numeric value
        /// </summary>
        [FhirElement("amountText", Versions=Hl7.Fhir.Model.Version.R4, InSummary=Hl7.Fhir.Model.Version.R4, Order=110)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString AmountTextElement
        {
            get { return _AmountTextElement; }
            set { _AmountTextElement = value; OnPropertyChanged("AmountTextElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _AmountTextElement;
        
        /// <summary>
        /// A textual comment on a numeric value
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string AmountText
        {
            get { return AmountTextElement != null ? AmountTextElement.Value : null; }
            set
            {
                if (value == null)
                    AmountTextElement = null;
                else
                    AmountTextElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("AmountText");
            }
        }
        
        /// <summary>
        /// Reference range of possible or expected values
        /// </summary>
        [FhirElement("referenceRange", Versions=Hl7.Fhir.Model.Version.R4, InSummary=Hl7.Fhir.Model.Version.R4, Order=120)]
        [CLSCompliant(false)]
        [DataMember]
        public ReferenceRangeComponent ReferenceRange
        {
            get { return _ReferenceRange; }
            set { _ReferenceRange = value; OnPropertyChanged("ReferenceRange"); }
        }
        
        private ReferenceRangeComponent _ReferenceRange;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as SubstanceAmount;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Amount != null) dest.Amount = (Hl7.Fhir.Model.Element)Amount.DeepCopy();
                if(AmountType != null) dest.AmountType = (Hl7.Fhir.Model.CodeableConcept)AmountType.DeepCopy();
                if(AmountTextElement != null) dest.AmountTextElement = (Hl7.Fhir.Model.FhirString)AmountTextElement.DeepCopy();
                if(ReferenceRange != null) dest.ReferenceRange = (ReferenceRangeComponent)ReferenceRange.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new SubstanceAmount());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as SubstanceAmount;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Amount, otherT.Amount)) return false;
            if( !DeepComparable.Matches(AmountType, otherT.AmountType)) return false;
            if( !DeepComparable.Matches(AmountTextElement, otherT.AmountTextElement)) return false;
            if( !DeepComparable.Matches(ReferenceRange, otherT.ReferenceRange)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as SubstanceAmount;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Amount, otherT.Amount)) return false;
            if( !DeepComparable.IsExactly(AmountType, otherT.AmountType)) return false;
            if( !DeepComparable.IsExactly(AmountTextElement, otherT.AmountTextElement)) return false;
            if( !DeepComparable.IsExactly(ReferenceRange, otherT.ReferenceRange)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginDataType("SubstanceAmount");
            base.Serialize(sink);
            sink.Element("amount", Hl7.Fhir.Model.Version.R4, Hl7.Fhir.Model.Version.R4, false, true); Amount?.Serialize(sink);
            sink.Element("amountType", Hl7.Fhir.Model.Version.R4, Hl7.Fhir.Model.Version.R4, false, false); AmountType?.Serialize(sink);
            sink.Element("amountText", Hl7.Fhir.Model.Version.R4, Hl7.Fhir.Model.Version.R4, false, false); AmountTextElement?.Serialize(sink);
            sink.Element("referenceRange", Hl7.Fhir.Model.Version.R4, Hl7.Fhir.Model.Version.R4, false, false); ReferenceRange?.Serialize(sink);
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
                case "amountQuantity" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    source.CheckDuplicates<Hl7.Fhir.Model.Quantity>(Amount, "amount");
                    Amount = source.Get<Hl7.Fhir.Model.Quantity>();
                    return true;
                case "amountRange" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    source.CheckDuplicates<Hl7.Fhir.Model.Range>(Amount, "amount");
                    Amount = source.Get<Hl7.Fhir.Model.Range>();
                    return true;
                case "amountString" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Amount, "amount");
                    Amount = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "amountType" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    AmountType = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "amountText" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    AmountTextElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "referenceRange" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    ReferenceRange = source.Get<ReferenceRangeComponent>();
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
                case "amountQuantity" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    source.CheckDuplicates<Hl7.Fhir.Model.Quantity>(Amount, "amount");
                    Amount = source.Populate(Amount as Hl7.Fhir.Model.Quantity);
                    return true;
                case "amountRange" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    source.CheckDuplicates<Hl7.Fhir.Model.Range>(Amount, "amount");
                    Amount = source.Populate(Amount as Hl7.Fhir.Model.Range);
                    return true;
                case "amountString" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Amount, "amount");
                    Amount = source.PopulateValue(Amount as Hl7.Fhir.Model.FhirString);
                    return true;
                case "_amountString" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Amount, "amount");
                    Amount = source.Populate(Amount as Hl7.Fhir.Model.FhirString);
                    return true;
                case "amountType" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    AmountType = source.Populate(AmountType);
                    return true;
                case "amountText" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    AmountTextElement = source.PopulateValue(AmountTextElement);
                    return true;
                case "_amountText" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    AmountTextElement = source.Populate(AmountTextElement);
                    return true;
                case "referenceRange" when source.IsVersion(Hl7.Fhir.Model.Version.R4):
                    ReferenceRange = source.Populate(ReferenceRange);
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
                if (Amount != null) yield return Amount;
                if (AmountType != null) yield return AmountType;
                if (AmountTextElement != null) yield return AmountTextElement;
                if (ReferenceRange != null) yield return ReferenceRange;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (Amount != null) yield return new ElementValue("amount", Amount);
                if (AmountType != null) yield return new ElementValue("amountType", AmountType);
                if (AmountTextElement != null) yield return new ElementValue("amountText", AmountTextElement);
                if (ReferenceRange != null) yield return new ElementValue("referenceRange", ReferenceRange);
            }
        }
    
    }

}
