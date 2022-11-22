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
// Generated for FHIR v4.0.1
//
namespace Hl7.Fhir.Model.R4
{
    /// <summary>
    /// An amount of economic utility in some recognized currency
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "Money")]
    [DataContract]
    public partial class Money : Hl7.Fhir.Model.Element, Hl7.Fhir.Model.IMoney, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "Money"; } }
    
        
        /// <summary>
        /// Numerical value (with implicit precision)
        /// </summary>
        [FhirElement("value", InSummary=Hl7.Fhir.Model.Version.All, Order=30)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDecimal ValueElement
        {
            get { return _ValueElement; }
            set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDecimal _ValueElement;
        
        /// <summary>
        /// Numerical value (with implicit precision)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public decimal? Value
        {
            get { return ValueElement != null ? ValueElement.Value : null; }
            set
            {
                if (value == null)
                    ValueElement = null;
                else
                    ValueElement = new Hl7.Fhir.Model.FhirDecimal(value);
                OnPropertyChanged("Value");
            }
        }
        
        /// <summary>
        /// ISO 4217 Currency Code
        /// </summary>
        [FhirElement("currency", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.R4.Currencies> CurrencyElement
        {
            get { return _CurrencyElement; }
            set { _CurrencyElement = value; OnPropertyChanged("CurrencyElement"); }
        }
        
        private Code<Hl7.Fhir.Model.R4.Currencies> _CurrencyElement;
        
        /// <summary>
        /// ISO 4217 Currency Code
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.R4.Currencies? Currency
        {
            get { return CurrencyElement != null ? CurrencyElement.Value : null; }
            set
            {
                if (value == null)
                    CurrencyElement = null;
                else
                    CurrencyElement = new Code<Hl7.Fhir.Model.R4.Currencies>(value);
                OnPropertyChanged("Currency");
            }
        }
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Money;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirDecimal)ValueElement.DeepCopy();
                if(CurrencyElement != null) dest.CurrencyElement = (Code<Hl7.Fhir.Model.R4.Currencies>)CurrencyElement.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new Money());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Money;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
            if( !DeepComparable.Matches(CurrencyElement, otherT.CurrencyElement)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Money;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
            if( !DeepComparable.IsExactly(CurrencyElement, otherT.CurrencyElement)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginDataType("Money");
            base.Serialize(sink);
            sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ValueElement?.Serialize(sink);
            sink.Element("currency", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CurrencyElement?.Serialize(sink);
            sink.End();
        }
    
        internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
        {
            if (base.SetElementFromJson(jsonPropertyName, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "value":
                    ValueElement = source.PopulateValue(ValueElement);
                    return true;
                case "_value":
                    ValueElement = source.Populate(ValueElement);
                    return true;
                case "currency":
                    CurrencyElement = source.PopulateValue(CurrencyElement);
                    return true;
                case "_currency":
                    CurrencyElement = source.Populate(CurrencyElement);
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
                if (ValueElement != null) yield return ValueElement;
                if (CurrencyElement != null) yield return CurrencyElement;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                if (CurrencyElement != null) yield return new ElementValue("currency", CurrencyElement);
            }
        }
    
    }

}
