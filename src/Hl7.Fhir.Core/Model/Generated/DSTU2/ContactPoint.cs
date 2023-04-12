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
    /// Details of a Technology mediated contact point (phone, fax, email, etc.)
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.DSTU2, "ContactPoint")]
    [DataContract]
    public partial class ContactPoint : Hl7.Fhir.Model.Element, Hl7.Fhir.Model.IContactPoint, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "ContactPoint"; } }
    
        
        /// <summary>
        /// phone | fax | email | pager | other
        /// </summary>
        [FhirElement("system", InSummary=Hl7.Fhir.Model.Version.All, Order=30)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DSTU2.ContactPointSystem> SystemElement
        {
            get { return _SystemElement; }
            set { _SystemElement = value; OnPropertyChanged("SystemElement"); }
        }
        
        private Code<Hl7.Fhir.Model.DSTU2.ContactPointSystem> _SystemElement;
        
        /// <summary>
        /// phone | fax | email | pager | other
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DSTU2.ContactPointSystem? System
        {
            get { return SystemElement != null ? SystemElement.Value : null; }
            set
            {
                if (value == null)
                    SystemElement = null;
                else
                    SystemElement = new Code<Hl7.Fhir.Model.DSTU2.ContactPointSystem>(value);
                OnPropertyChanged("System");
            }
        }
        
        /// <summary>
        /// The actual contact point details
        /// </summary>
        [FhirElement("value", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ValueElement
        {
            get { return _ValueElement; }
            set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _ValueElement;
        
        /// <summary>
        /// The actual contact point details
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Value
        {
            get { return ValueElement != null ? ValueElement.Value : null; }
            set
            {
                if (value == null)
                    ValueElement = null;
                else
                    ValueElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Value");
            }
        }
        
        /// <summary>
        /// home | work | temp | old | mobile - purpose of this contact point
        /// </summary>
        [FhirElement("use", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.ContactPointUse> UseElement
        {
            get { return _UseElement; }
            set { _UseElement = value; OnPropertyChanged("UseElement"); }
        }
        
        private Code<Hl7.Fhir.Model.ContactPointUse> _UseElement;
        
        /// <summary>
        /// home | work | temp | old | mobile - purpose of this contact point
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.ContactPointUse? Use
        {
            get { return UseElement != null ? UseElement.Value : null; }
            set
            {
                if (value == null)
                    UseElement = null;
                else
                    UseElement = new Code<Hl7.Fhir.Model.ContactPointUse>(value);
                OnPropertyChanged("Use");
            }
        }
        
        /// <summary>
        /// Specify preferred order of use (1 = highest)
        /// </summary>
        [FhirElement("rank", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.PositiveInt RankElement
        {
            get { return _RankElement; }
            set { _RankElement = value; OnPropertyChanged("RankElement"); }
        }
        
        private Hl7.Fhir.Model.PositiveInt _RankElement;
        
        /// <summary>
        /// Specify preferred order of use (1 = highest)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? Rank
        {
            get { return RankElement != null ? RankElement.Value : null; }
            set
            {
                if (value == null)
                    RankElement = null;
                else
                    RankElement = new Hl7.Fhir.Model.PositiveInt(value);
                OnPropertyChanged("Rank");
            }
        }
        
        /// <summary>
        /// Time period when the contact point was/is in use
        /// </summary>
        [FhirElement("period", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period
        {
            get { return _Period; }
            set { _Period = value; OnPropertyChanged("Period"); }
        }
        
        private Hl7.Fhir.Model.Period _Period;
    
    
        public static ElementDefinitionConstraint[] ContactPoint_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "cpt-2",
                severity: ConstraintSeverity.Warning,
                expression: "value.empty() or system",
                human: "A system is required if a value is provided.",
                xpath: "not(exists(f:value)) or exists(f:system)"
            ),
        };
    
        // TODO: Add code to enforce the above constraints
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ContactPoint;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(SystemElement != null) dest.SystemElement = (Code<Hl7.Fhir.Model.DSTU2.ContactPointSystem>)SystemElement.DeepCopy();
                if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirString)ValueElement.DeepCopy();
                if(UseElement != null) dest.UseElement = (Code<Hl7.Fhir.Model.ContactPointUse>)UseElement.DeepCopy();
                if(RankElement != null) dest.RankElement = (Hl7.Fhir.Model.PositiveInt)RankElement.DeepCopy();
                if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new ContactPoint());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ContactPoint;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(SystemElement, otherT.SystemElement)) return false;
            if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
            if( !DeepComparable.Matches(UseElement, otherT.UseElement)) return false;
            if( !DeepComparable.Matches(RankElement, otherT.RankElement)) return false;
            if( !DeepComparable.Matches(Period, otherT.Period)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ContactPoint;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(SystemElement, otherT.SystemElement)) return false;
            if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
            if( !DeepComparable.IsExactly(UseElement, otherT.UseElement)) return false;
            if( !DeepComparable.IsExactly(RankElement, otherT.RankElement)) return false;
            if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginDataType("ContactPoint");
            base.Serialize(sink);
            sink.Element("system", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); SystemElement?.Serialize(sink);
            sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ValueElement?.Serialize(sink);
            sink.Element("use", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); UseElement?.Serialize(sink);
            sink.Element("rank", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); RankElement?.Serialize(sink);
            sink.Element("period", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Period?.Serialize(sink);
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
                case "system":
                    SystemElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DSTU2.ContactPointSystem>>();
                    return true;
                case "value":
                    ValueElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "use":
                    UseElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ContactPointUse>>();
                    return true;
                case "rank":
                    RankElement = source.Get<Hl7.Fhir.Model.PositiveInt>();
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
                case "system":
                    SystemElement = source.PopulateValue(SystemElement);
                    return true;
                case "_system":
                    SystemElement = source.Populate(SystemElement);
                    return true;
                case "value":
                    ValueElement = source.PopulateValue(ValueElement);
                    return true;
                case "_value":
                    ValueElement = source.Populate(ValueElement);
                    return true;
                case "use":
                    UseElement = source.PopulateValue(UseElement);
                    return true;
                case "_use":
                    UseElement = source.Populate(UseElement);
                    return true;
                case "rank":
                    RankElement = source.PopulateValue(RankElement);
                    return true;
                case "_rank":
                    RankElement = source.Populate(RankElement);
                    return true;
                case "period":
                    Period = source.Populate(Period);
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
                if (SystemElement != null) yield return SystemElement;
                if (ValueElement != null) yield return ValueElement;
                if (UseElement != null) yield return UseElement;
                if (RankElement != null) yield return RankElement;
                if (Period != null) yield return Period;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (SystemElement != null) yield return new ElementValue("system", SystemElement);
                if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                if (UseElement != null) yield return new ElementValue("use", UseElement);
                if (RankElement != null) yield return new ElementValue("rank", RankElement);
                if (Period != null) yield return new ElementValue("period", Period);
            }
        }
    
    }

}
