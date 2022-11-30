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
    /// Name of a human - parts and usage
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "HumanName")]
    [DataContract]
    public partial class HumanName : Hl7.Fhir.Model.Element, Hl7.Fhir.Model.IHumanName, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "HumanName"; } }
    
        
        /// <summary>
        /// usual | official | temp | nickname | anonymous | old | maiden
        /// </summary>
        [FhirElement("use", InSummary=Hl7.Fhir.Model.Version.All, Order=30)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.NameUse> UseElement
        {
            get { return _UseElement; }
            set { _UseElement = value; OnPropertyChanged("UseElement"); }
        }
        
        private Code<Hl7.Fhir.Model.NameUse> _UseElement;
        
        /// <summary>
        /// usual | official | temp | nickname | anonymous | old | maiden
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.NameUse? Use
        {
            get { return UseElement != null ? UseElement.Value : null; }
            set
            {
                if (value == null)
                    UseElement = null;
                else
                    UseElement = new Code<Hl7.Fhir.Model.NameUse>(value);
                OnPropertyChanged("Use");
            }
        }
        
        /// <summary>
        /// Text representation of the full name
        /// </summary>
        [FhirElement("text", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString TextElement
        {
            get { return _TextElement; }
            set { _TextElement = value; OnPropertyChanged("TextElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _TextElement;
        
        /// <summary>
        /// Text representation of the full name
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Text
        {
            get { return TextElement != null ? TextElement.Value : null; }
            set
            {
                if (value == null)
                    TextElement = null;
                else
                    TextElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Text");
            }
        }
        
        /// <summary>
        /// Family name (often called 'Surname')
        /// </summary>
        [FhirElement("family", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString FamilyElement
        {
            get { return _FamilyElement; }
            set { _FamilyElement = value; OnPropertyChanged("FamilyElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _FamilyElement;
        
        /// <summary>
        /// Family name (often called 'Surname')
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Family
        {
            get { return FamilyElement != null ? FamilyElement.Value : null; }
            set
            {
                if (value == null)
                    FamilyElement = null;
                else
                    FamilyElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Family");
            }
        }
        
        /// <summary>
        /// Given names (not always 'first'). Includes middle names
        /// </summary>
        [FhirElement("given", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> GivenElement
        {
            get { if(_GivenElement==null) _GivenElement = new List<Hl7.Fhir.Model.FhirString>(); return _GivenElement; }
            set { _GivenElement = value; OnPropertyChanged("GivenElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _GivenElement;
        
        /// <summary>
        /// Given names (not always 'first'). Includes middle names
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Given
        {
            get { return GivenElement != null ? GivenElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    GivenElement = null;
                else
                    GivenElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("Given");
            }
        }
        
        /// <summary>
        /// Parts that come before the name
        /// </summary>
        [FhirElement("prefix", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> PrefixElement
        {
            get { if(_PrefixElement==null) _PrefixElement = new List<Hl7.Fhir.Model.FhirString>(); return _PrefixElement; }
            set { _PrefixElement = value; OnPropertyChanged("PrefixElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _PrefixElement;
        
        /// <summary>
        /// Parts that come before the name
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Prefix
        {
            get { return PrefixElement != null ? PrefixElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    PrefixElement = null;
                else
                    PrefixElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("Prefix");
            }
        }
        
        /// <summary>
        /// Parts that come after the name
        /// </summary>
        [FhirElement("suffix", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> SuffixElement
        {
            get { if(_SuffixElement==null) _SuffixElement = new List<Hl7.Fhir.Model.FhirString>(); return _SuffixElement; }
            set { _SuffixElement = value; OnPropertyChanged("SuffixElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _SuffixElement;
        
        /// <summary>
        /// Parts that come after the name
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Suffix
        {
            get { return SuffixElement != null ? SuffixElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    SuffixElement = null;
                else
                    SuffixElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("Suffix");
            }
        }
        
        /// <summary>
        /// Time period when name was/is in use
        /// </summary>
        [FhirElement("period", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period
        {
            get { return _Period; }
            set { _Period = value; OnPropertyChanged("Period"); }
        }
        
        private Hl7.Fhir.Model.Period _Period;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as HumanName;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(UseElement != null) dest.UseElement = (Code<Hl7.Fhir.Model.NameUse>)UseElement.DeepCopy();
                if(TextElement != null) dest.TextElement = (Hl7.Fhir.Model.FhirString)TextElement.DeepCopy();
                if(FamilyElement != null) dest.FamilyElement = (Hl7.Fhir.Model.FhirString)FamilyElement.DeepCopy();
                if(GivenElement != null) dest.GivenElement = new List<Hl7.Fhir.Model.FhirString>(GivenElement.DeepCopy());
                if(PrefixElement != null) dest.PrefixElement = new List<Hl7.Fhir.Model.FhirString>(PrefixElement.DeepCopy());
                if(SuffixElement != null) dest.SuffixElement = new List<Hl7.Fhir.Model.FhirString>(SuffixElement.DeepCopy());
                if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new HumanName());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as HumanName;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(UseElement, otherT.UseElement)) return false;
            if( !DeepComparable.Matches(TextElement, otherT.TextElement)) return false;
            if( !DeepComparable.Matches(FamilyElement, otherT.FamilyElement)) return false;
            if( !DeepComparable.Matches(GivenElement, otherT.GivenElement)) return false;
            if( !DeepComparable.Matches(PrefixElement, otherT.PrefixElement)) return false;
            if( !DeepComparable.Matches(SuffixElement, otherT.SuffixElement)) return false;
            if( !DeepComparable.Matches(Period, otherT.Period)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as HumanName;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(UseElement, otherT.UseElement)) return false;
            if( !DeepComparable.IsExactly(TextElement, otherT.TextElement)) return false;
            if( !DeepComparable.IsExactly(FamilyElement, otherT.FamilyElement)) return false;
            if( !DeepComparable.IsExactly(GivenElement, otherT.GivenElement)) return false;
            if( !DeepComparable.IsExactly(PrefixElement, otherT.PrefixElement)) return false;
            if( !DeepComparable.IsExactly(SuffixElement, otherT.SuffixElement)) return false;
            if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginDataType("HumanName");
            base.Serialize(sink);
            sink.Element("use", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); UseElement?.Serialize(sink);
            sink.Element("text", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TextElement?.Serialize(sink);
            sink.Element("family", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); FamilyElement?.Serialize(sink);
            sink.BeginList("given", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            sink.Serialize(GivenElement);
            sink.End();
            sink.BeginList("prefix", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            sink.Serialize(PrefixElement);
            sink.End();
            sink.BeginList("suffix", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            sink.Serialize(SuffixElement);
            sink.End();
            sink.Element("period", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Period?.Serialize(sink);
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
                case "use":
                    UseElement = source.PopulateValue(UseElement);
                    return true;
                case "_use":
                    UseElement = source.Populate(UseElement);
                    return true;
                case "text":
                    TextElement = source.PopulateValue(TextElement);
                    return true;
                case "_text":
                    TextElement = source.Populate(TextElement);
                    return true;
                case "family":
                    FamilyElement = source.PopulateValue(FamilyElement);
                    return true;
                case "_family":
                    FamilyElement = source.Populate(FamilyElement);
                    return true;
                case "given":
                case "_given":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "prefix":
                case "_prefix":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "suffix":
                case "_suffix":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "period":
                    Period = source.Populate(Period);
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
                case "given":
                    source.PopulatePrimitiveListItemValue(GivenElement, index);
                    return true;
                case "_given":
                    source.PopulatePrimitiveListItem(GivenElement, index);
                    return true;
                case "prefix":
                    source.PopulatePrimitiveListItemValue(PrefixElement, index);
                    return true;
                case "_prefix":
                    source.PopulatePrimitiveListItem(PrefixElement, index);
                    return true;
                case "suffix":
                    source.PopulatePrimitiveListItemValue(SuffixElement, index);
                    return true;
                case "_suffix":
                    source.PopulatePrimitiveListItem(SuffixElement, index);
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
                if (UseElement != null) yield return UseElement;
                if (TextElement != null) yield return TextElement;
                if (FamilyElement != null) yield return FamilyElement;
                foreach (var elem in GivenElement) { if (elem != null) yield return elem; }
                foreach (var elem in PrefixElement) { if (elem != null) yield return elem; }
                foreach (var elem in SuffixElement) { if (elem != null) yield return elem; }
                if (Period != null) yield return Period;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (UseElement != null) yield return new ElementValue("use", UseElement);
                if (TextElement != null) yield return new ElementValue("text", TextElement);
                if (FamilyElement != null) yield return new ElementValue("family", FamilyElement);
                foreach (var elem in GivenElement) { if (elem != null) yield return new ElementValue("given", elem); }
                foreach (var elem in PrefixElement) { if (elem != null) yield return new ElementValue("prefix", elem); }
                foreach (var elem in SuffixElement) { if (elem != null) yield return new ElementValue("suffix", elem); }
                if (Period != null) yield return new ElementValue("period", Period);
            }
        }
    
    }

}
