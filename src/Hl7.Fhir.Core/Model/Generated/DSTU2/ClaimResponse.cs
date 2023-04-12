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
    /// Remittance resource
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.DSTU2, "ClaimResponse", IsResource=true)]
    [DataContract]
    public partial class ClaimResponse : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IClaimResponse, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ClaimResponse; } }
        [NotMapped]
        public override string TypeName { get { return "ClaimResponse"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "ItemsComponent")]
        [DataContract]
        public partial class ItemsComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ItemsComponent"; } }
            
            /// <summary>
            /// Service instance
            /// </summary>
            [FhirElement("sequenceLinkId", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceLinkIdElement
            {
                get { return _SequenceLinkIdElement; }
                set { _SequenceLinkIdElement = value; OnPropertyChanged("SequenceLinkIdElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceLinkIdElement;
            
            /// <summary>
            /// Service instance
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? SequenceLinkId
            {
                get { return SequenceLinkIdElement != null ? SequenceLinkIdElement.Value : null; }
                set
                {
                    if (value == null)
                        SequenceLinkIdElement = null;
                    else
                        SequenceLinkIdElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("SequenceLinkId");
                }
            }
            
            /// <summary>
            /// List of note numbers which apply
            /// </summary>
            [FhirElement("noteNumber", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> NoteNumberElement
            {
                get { if(_NoteNumberElement==null) _NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _NoteNumberElement; }
                set { _NoteNumberElement = value; OnPropertyChanged("NoteNumberElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _NoteNumberElement;
            
            /// <summary>
            /// List of note numbers which apply
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> NoteNumber
            {
                get { return NoteNumberElement != null ? NoteNumberElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        NoteNumberElement = null;
                    else
                        NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(value.Select(elem=>new Hl7.Fhir.Model.PositiveInt(elem)));
                    OnPropertyChanged("NoteNumber");
                }
            }
            
            /// <summary>
            /// Adjudication details
            /// </summary>
            [FhirElement("adjudication", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<ItemAdjudicationComponent> Adjudication
            {
                get { if(_Adjudication==null) _Adjudication = new List<ItemAdjudicationComponent>(); return _Adjudication; }
                set { _Adjudication = value; OnPropertyChanged("Adjudication"); }
            }
            
            private List<ItemAdjudicationComponent> _Adjudication;
            
            /// <summary>
            /// Detail line items
            /// </summary>
            [FhirElement("detail", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<ItemDetailComponent> Detail
            {
                get { if(_Detail==null) _Detail = new List<ItemDetailComponent>(); return _Detail; }
                set { _Detail = value; OnPropertyChanged("Detail"); }
            }
            
            private List<ItemDetailComponent> _Detail;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ItemsComponent");
                base.Serialize(sink);
                sink.Element("sequenceLinkId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); SequenceLinkIdElement?.Serialize(sink);
                sink.BeginList("noteNumber", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                sink.Serialize(NoteNumberElement);
                sink.End();
                sink.BeginList("adjudication", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Adjudication)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("detail", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Detail)
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
                    case "sequenceLinkId":
                        SequenceLinkIdElement = source.Get<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "noteNumber":
                        NoteNumberElement = source.GetList<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "adjudication":
                        Adjudication = source.GetList<ItemAdjudicationComponent>();
                        return true;
                    case "detail":
                        Detail = source.GetList<ItemDetailComponent>();
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
                    case "sequenceLinkId":
                        SequenceLinkIdElement = source.PopulateValue(SequenceLinkIdElement);
                        return true;
                    case "_sequenceLinkId":
                        SequenceLinkIdElement = source.Populate(SequenceLinkIdElement);
                        return true;
                    case "noteNumber":
                    case "_noteNumber":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "adjudication":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "detail":
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
                    case "noteNumber":
                        source.PopulatePrimitiveListItemValue(NoteNumberElement, index);
                        return true;
                    case "_noteNumber":
                        source.PopulatePrimitiveListItem(NoteNumberElement, index);
                        return true;
                    case "adjudication":
                        source.PopulateListItem(Adjudication, index);
                        return true;
                    case "detail":
                        source.PopulateListItem(Detail, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ItemsComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceLinkIdElement != null) dest.SequenceLinkIdElement = (Hl7.Fhir.Model.PositiveInt)SequenceLinkIdElement.DeepCopy();
                    if(NoteNumberElement != null) dest.NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(NoteNumberElement.DeepCopy());
                    if(Adjudication != null) dest.Adjudication = new List<ItemAdjudicationComponent>(Adjudication.DeepCopy());
                    if(Detail != null) dest.Detail = new List<ItemDetailComponent>(Detail.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ItemsComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ItemsComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceLinkIdElement, otherT.SequenceLinkIdElement)) return false;
                if( !DeepComparable.Matches(NoteNumberElement, otherT.NoteNumberElement)) return false;
                if( !DeepComparable.Matches(Adjudication, otherT.Adjudication)) return false;
                if( !DeepComparable.Matches(Detail, otherT.Detail)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ItemsComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceLinkIdElement, otherT.SequenceLinkIdElement)) return false;
                if( !DeepComparable.IsExactly(NoteNumberElement, otherT.NoteNumberElement)) return false;
                if( !DeepComparable.IsExactly(Adjudication, otherT.Adjudication)) return false;
                if( !DeepComparable.IsExactly(Detail, otherT.Detail)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SequenceLinkIdElement != null) yield return SequenceLinkIdElement;
                    foreach (var elem in NoteNumberElement) { if (elem != null) yield return elem; }
                    foreach (var elem in Adjudication) { if (elem != null) yield return elem; }
                    foreach (var elem in Detail) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SequenceLinkIdElement != null) yield return new ElementValue("sequenceLinkId", SequenceLinkIdElement);
                    foreach (var elem in NoteNumberElement) { if (elem != null) yield return new ElementValue("noteNumber", elem); }
                    foreach (var elem in Adjudication) { if (elem != null) yield return new ElementValue("adjudication", elem); }
                    foreach (var elem in Detail) { if (elem != null) yield return new ElementValue("detail", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "ItemAdjudicationComponent")]
        [DataContract]
        public partial class ItemAdjudicationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ItemAdjudicationComponent"; } }
            
            /// <summary>
            /// Adjudication category such as co-pay, eligible, benefit, etc.
            /// </summary>
            [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.Coding _Code;
            
            /// <summary>
            /// Monetary amount
            /// </summary>
            [FhirElement("amount", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.DSTU2.Money Amount
            {
                get { return _Amount; }
                set { _Amount = value; OnPropertyChanged("Amount"); }
            }
            
            private Hl7.Fhir.Model.DSTU2.Money _Amount;
            
            /// <summary>
            /// Non-monetary value
            /// </summary>
            [FhirElement("value", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _ValueElement;
            
            /// <summary>
            /// Non-monetary value
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ItemAdjudicationComponent");
                base.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Code?.Serialize(sink);
                sink.Element("amount", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Amount?.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ValueElement?.Serialize(sink);
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
                    case "code":
                        Code = source.Get<Hl7.Fhir.Model.Coding>();
                        return true;
                    case "amount":
                        Amount = source.Get<Hl7.Fhir.Model.DSTU2.Money>();
                        return true;
                    case "value":
                        ValueElement = source.Get<Hl7.Fhir.Model.FhirDecimal>();
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
                    case "code":
                        Code = source.Populate(Code);
                        return true;
                    case "amount":
                        Amount = source.Populate(Amount);
                        return true;
                    case "value":
                        ValueElement = source.PopulateValue(ValueElement);
                        return true;
                    case "_value":
                        ValueElement = source.Populate(ValueElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ItemAdjudicationComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.Coding)Code.DeepCopy();
                    if(Amount != null) dest.Amount = (Hl7.Fhir.Model.DSTU2.Money)Amount.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirDecimal)ValueElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ItemAdjudicationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ItemAdjudicationComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Amount, otherT.Amount)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ItemAdjudicationComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Amount, otherT.Amount)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Code != null) yield return Code;
                    if (Amount != null) yield return Amount;
                    if (ValueElement != null) yield return ValueElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Code != null) yield return new ElementValue("code", Code);
                    if (Amount != null) yield return new ElementValue("amount", Amount);
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "ItemDetailComponent")]
        [DataContract]
        public partial class ItemDetailComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IClaimResponseItemDetailComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ItemDetailComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IClaimResponseSubDetailComponent> Hl7.Fhir.Model.IClaimResponseItemDetailComponent.SubDetail { get { return SubDetail; } }
            
            /// <summary>
            /// Service instance
            /// </summary>
            [FhirElement("sequenceLinkId", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceLinkIdElement
            {
                get { return _SequenceLinkIdElement; }
                set { _SequenceLinkIdElement = value; OnPropertyChanged("SequenceLinkIdElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceLinkIdElement;
            
            /// <summary>
            /// Service instance
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? SequenceLinkId
            {
                get { return SequenceLinkIdElement != null ? SequenceLinkIdElement.Value : null; }
                set
                {
                    if (value == null)
                        SequenceLinkIdElement = null;
                    else
                        SequenceLinkIdElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("SequenceLinkId");
                }
            }
            
            /// <summary>
            /// Detail adjudication
            /// </summary>
            [FhirElement("adjudication", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<DetailAdjudicationComponent> Adjudication
            {
                get { if(_Adjudication==null) _Adjudication = new List<DetailAdjudicationComponent>(); return _Adjudication; }
                set { _Adjudication = value; OnPropertyChanged("Adjudication"); }
            }
            
            private List<DetailAdjudicationComponent> _Adjudication;
            
            /// <summary>
            /// Subdetail line items
            /// </summary>
            [FhirElement("subDetail", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<SubDetailComponent> SubDetail
            {
                get { if(_SubDetail==null) _SubDetail = new List<SubDetailComponent>(); return _SubDetail; }
                set { _SubDetail = value; OnPropertyChanged("SubDetail"); }
            }
            
            private List<SubDetailComponent> _SubDetail;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ItemDetailComponent");
                base.Serialize(sink);
                sink.Element("sequenceLinkId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); SequenceLinkIdElement?.Serialize(sink);
                sink.BeginList("adjudication", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Adjudication)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("subDetail", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in SubDetail)
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
                    case "sequenceLinkId":
                        SequenceLinkIdElement = source.Get<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "adjudication":
                        Adjudication = source.GetList<DetailAdjudicationComponent>();
                        return true;
                    case "subDetail":
                        SubDetail = source.GetList<SubDetailComponent>();
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
                    case "sequenceLinkId":
                        SequenceLinkIdElement = source.PopulateValue(SequenceLinkIdElement);
                        return true;
                    case "_sequenceLinkId":
                        SequenceLinkIdElement = source.Populate(SequenceLinkIdElement);
                        return true;
                    case "adjudication":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "subDetail":
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
                    case "adjudication":
                        source.PopulateListItem(Adjudication, index);
                        return true;
                    case "subDetail":
                        source.PopulateListItem(SubDetail, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ItemDetailComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceLinkIdElement != null) dest.SequenceLinkIdElement = (Hl7.Fhir.Model.PositiveInt)SequenceLinkIdElement.DeepCopy();
                    if(Adjudication != null) dest.Adjudication = new List<DetailAdjudicationComponent>(Adjudication.DeepCopy());
                    if(SubDetail != null) dest.SubDetail = new List<SubDetailComponent>(SubDetail.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ItemDetailComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ItemDetailComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceLinkIdElement, otherT.SequenceLinkIdElement)) return false;
                if( !DeepComparable.Matches(Adjudication, otherT.Adjudication)) return false;
                if( !DeepComparable.Matches(SubDetail, otherT.SubDetail)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ItemDetailComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceLinkIdElement, otherT.SequenceLinkIdElement)) return false;
                if( !DeepComparable.IsExactly(Adjudication, otherT.Adjudication)) return false;
                if( !DeepComparable.IsExactly(SubDetail, otherT.SubDetail)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SequenceLinkIdElement != null) yield return SequenceLinkIdElement;
                    foreach (var elem in Adjudication) { if (elem != null) yield return elem; }
                    foreach (var elem in SubDetail) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SequenceLinkIdElement != null) yield return new ElementValue("sequenceLinkId", SequenceLinkIdElement);
                    foreach (var elem in Adjudication) { if (elem != null) yield return new ElementValue("adjudication", elem); }
                    foreach (var elem in SubDetail) { if (elem != null) yield return new ElementValue("subDetail", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "DetailAdjudicationComponent")]
        [DataContract]
        public partial class DetailAdjudicationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "DetailAdjudicationComponent"; } }
            
            /// <summary>
            /// Adjudication category such as co-pay, eligible, benefit, etc.
            /// </summary>
            [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.Coding _Code;
            
            /// <summary>
            /// Monetary amount
            /// </summary>
            [FhirElement("amount", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.DSTU2.Money Amount
            {
                get { return _Amount; }
                set { _Amount = value; OnPropertyChanged("Amount"); }
            }
            
            private Hl7.Fhir.Model.DSTU2.Money _Amount;
            
            /// <summary>
            /// Non-monetary value
            /// </summary>
            [FhirElement("value", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _ValueElement;
            
            /// <summary>
            /// Non-monetary value
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("DetailAdjudicationComponent");
                base.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Code?.Serialize(sink);
                sink.Element("amount", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Amount?.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ValueElement?.Serialize(sink);
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
                    case "code":
                        Code = source.Get<Hl7.Fhir.Model.Coding>();
                        return true;
                    case "amount":
                        Amount = source.Get<Hl7.Fhir.Model.DSTU2.Money>();
                        return true;
                    case "value":
                        ValueElement = source.Get<Hl7.Fhir.Model.FhirDecimal>();
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
                    case "code":
                        Code = source.Populate(Code);
                        return true;
                    case "amount":
                        Amount = source.Populate(Amount);
                        return true;
                    case "value":
                        ValueElement = source.PopulateValue(ValueElement);
                        return true;
                    case "_value":
                        ValueElement = source.Populate(ValueElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DetailAdjudicationComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.Coding)Code.DeepCopy();
                    if(Amount != null) dest.Amount = (Hl7.Fhir.Model.DSTU2.Money)Amount.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirDecimal)ValueElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new DetailAdjudicationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DetailAdjudicationComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Amount, otherT.Amount)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DetailAdjudicationComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Amount, otherT.Amount)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Code != null) yield return Code;
                    if (Amount != null) yield return Amount;
                    if (ValueElement != null) yield return ValueElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Code != null) yield return new ElementValue("code", Code);
                    if (Amount != null) yield return new ElementValue("amount", Amount);
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "SubDetailComponent")]
        [DataContract]
        public partial class SubDetailComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IClaimResponseSubDetailComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SubDetailComponent"; } }
            
            /// <summary>
            /// Service instance
            /// </summary>
            [FhirElement("sequenceLinkId", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceLinkIdElement
            {
                get { return _SequenceLinkIdElement; }
                set { _SequenceLinkIdElement = value; OnPropertyChanged("SequenceLinkIdElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceLinkIdElement;
            
            /// <summary>
            /// Service instance
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? SequenceLinkId
            {
                get { return SequenceLinkIdElement != null ? SequenceLinkIdElement.Value : null; }
                set
                {
                    if (value == null)
                        SequenceLinkIdElement = null;
                    else
                        SequenceLinkIdElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("SequenceLinkId");
                }
            }
            
            /// <summary>
            /// Subdetail adjudication
            /// </summary>
            [FhirElement("adjudication", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<SubdetailAdjudicationComponent> Adjudication
            {
                get { if(_Adjudication==null) _Adjudication = new List<SubdetailAdjudicationComponent>(); return _Adjudication; }
                set { _Adjudication = value; OnPropertyChanged("Adjudication"); }
            }
            
            private List<SubdetailAdjudicationComponent> _Adjudication;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SubDetailComponent");
                base.Serialize(sink);
                sink.Element("sequenceLinkId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); SequenceLinkIdElement?.Serialize(sink);
                sink.BeginList("adjudication", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Adjudication)
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
                    case "sequenceLinkId":
                        SequenceLinkIdElement = source.Get<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "adjudication":
                        Adjudication = source.GetList<SubdetailAdjudicationComponent>();
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
                    case "sequenceLinkId":
                        SequenceLinkIdElement = source.PopulateValue(SequenceLinkIdElement);
                        return true;
                    case "_sequenceLinkId":
                        SequenceLinkIdElement = source.Populate(SequenceLinkIdElement);
                        return true;
                    case "adjudication":
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
                    case "adjudication":
                        source.PopulateListItem(Adjudication, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SubDetailComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceLinkIdElement != null) dest.SequenceLinkIdElement = (Hl7.Fhir.Model.PositiveInt)SequenceLinkIdElement.DeepCopy();
                    if(Adjudication != null) dest.Adjudication = new List<SubdetailAdjudicationComponent>(Adjudication.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new SubDetailComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SubDetailComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceLinkIdElement, otherT.SequenceLinkIdElement)) return false;
                if( !DeepComparable.Matches(Adjudication, otherT.Adjudication)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SubDetailComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceLinkIdElement, otherT.SequenceLinkIdElement)) return false;
                if( !DeepComparable.IsExactly(Adjudication, otherT.Adjudication)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SequenceLinkIdElement != null) yield return SequenceLinkIdElement;
                    foreach (var elem in Adjudication) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SequenceLinkIdElement != null) yield return new ElementValue("sequenceLinkId", SequenceLinkIdElement);
                    foreach (var elem in Adjudication) { if (elem != null) yield return new ElementValue("adjudication", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "SubdetailAdjudicationComponent")]
        [DataContract]
        public partial class SubdetailAdjudicationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SubdetailAdjudicationComponent"; } }
            
            /// <summary>
            /// Adjudication category such as co-pay, eligible, benefit, etc.
            /// </summary>
            [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.Coding _Code;
            
            /// <summary>
            /// Monetary amount
            /// </summary>
            [FhirElement("amount", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.DSTU2.Money Amount
            {
                get { return _Amount; }
                set { _Amount = value; OnPropertyChanged("Amount"); }
            }
            
            private Hl7.Fhir.Model.DSTU2.Money _Amount;
            
            /// <summary>
            /// Non-monetary value
            /// </summary>
            [FhirElement("value", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _ValueElement;
            
            /// <summary>
            /// Non-monetary value
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SubdetailAdjudicationComponent");
                base.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Code?.Serialize(sink);
                sink.Element("amount", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Amount?.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ValueElement?.Serialize(sink);
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
                    case "code":
                        Code = source.Get<Hl7.Fhir.Model.Coding>();
                        return true;
                    case "amount":
                        Amount = source.Get<Hl7.Fhir.Model.DSTU2.Money>();
                        return true;
                    case "value":
                        ValueElement = source.Get<Hl7.Fhir.Model.FhirDecimal>();
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
                    case "code":
                        Code = source.Populate(Code);
                        return true;
                    case "amount":
                        Amount = source.Populate(Amount);
                        return true;
                    case "value":
                        ValueElement = source.PopulateValue(ValueElement);
                        return true;
                    case "_value":
                        ValueElement = source.Populate(ValueElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SubdetailAdjudicationComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.Coding)Code.DeepCopy();
                    if(Amount != null) dest.Amount = (Hl7.Fhir.Model.DSTU2.Money)Amount.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirDecimal)ValueElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new SubdetailAdjudicationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SubdetailAdjudicationComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Amount, otherT.Amount)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SubdetailAdjudicationComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Amount, otherT.Amount)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Code != null) yield return Code;
                    if (Amount != null) yield return Amount;
                    if (ValueElement != null) yield return ValueElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Code != null) yield return new ElementValue("code", Code);
                    if (Amount != null) yield return new ElementValue("amount", Amount);
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "AddedItemComponent")]
        [DataContract]
        public partial class AddedItemComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IClaimResponseAddedItemComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "AddedItemComponent"; } }
            
            /// <summary>
            /// Service instances
            /// </summary>
            [FhirElement("sequenceLinkId", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> SequenceLinkIdElement
            {
                get { if(_SequenceLinkIdElement==null) _SequenceLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _SequenceLinkIdElement; }
                set { _SequenceLinkIdElement = value; OnPropertyChanged("SequenceLinkIdElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _SequenceLinkIdElement;
            
            /// <summary>
            /// Service instances
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> SequenceLinkId
            {
                get { return SequenceLinkIdElement != null ? SequenceLinkIdElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        SequenceLinkIdElement = null;
                    else
                        SequenceLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(value.Select(elem=>new Hl7.Fhir.Model.PositiveInt(elem)));
                    OnPropertyChanged("SequenceLinkId");
                }
            }
            
            /// <summary>
            /// Group, Service or Product
            /// </summary>
            [FhirElement("service", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Service
            {
                get { return _Service; }
                set { _Service = value; OnPropertyChanged("Service"); }
            }
            
            private Hl7.Fhir.Model.Coding _Service;
            
            /// <summary>
            /// Professional fee or Product charge
            /// </summary>
            [FhirElement("fee", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.DSTU2.Money Fee
            {
                get { return _Fee; }
                set { _Fee = value; OnPropertyChanged("Fee"); }
            }
            
            private Hl7.Fhir.Model.DSTU2.Money _Fee;
            
            /// <summary>
            /// List of note numbers which apply
            /// </summary>
            [FhirElement("noteNumberLinkId", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> NoteNumberLinkIdElement
            {
                get { if(_NoteNumberLinkIdElement==null) _NoteNumberLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _NoteNumberLinkIdElement; }
                set { _NoteNumberLinkIdElement = value; OnPropertyChanged("NoteNumberLinkIdElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _NoteNumberLinkIdElement;
            
            /// <summary>
            /// List of note numbers which apply
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> NoteNumberLinkId
            {
                get { return NoteNumberLinkIdElement != null ? NoteNumberLinkIdElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        NoteNumberLinkIdElement = null;
                    else
                        NoteNumberLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(value.Select(elem=>new Hl7.Fhir.Model.PositiveInt(elem)));
                    OnPropertyChanged("NoteNumberLinkId");
                }
            }
            
            /// <summary>
            /// Added items adjudication
            /// </summary>
            [FhirElement("adjudication", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<AddedItemAdjudicationComponent> Adjudication
            {
                get { if(_Adjudication==null) _Adjudication = new List<AddedItemAdjudicationComponent>(); return _Adjudication; }
                set { _Adjudication = value; OnPropertyChanged("Adjudication"); }
            }
            
            private List<AddedItemAdjudicationComponent> _Adjudication;
            
            /// <summary>
            /// Added items details
            /// </summary>
            [FhirElement("detail", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<AddedItemsDetailComponent> Detail
            {
                get { if(_Detail==null) _Detail = new List<AddedItemsDetailComponent>(); return _Detail; }
                set { _Detail = value; OnPropertyChanged("Detail"); }
            }
            
            private List<AddedItemsDetailComponent> _Detail;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("AddedItemComponent");
                base.Serialize(sink);
                sink.BeginList("sequenceLinkId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                sink.Serialize(SequenceLinkIdElement);
                sink.End();
                sink.Element("service", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Service?.Serialize(sink);
                sink.Element("fee", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Fee?.Serialize(sink);
                sink.BeginList("noteNumberLinkId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                sink.Serialize(NoteNumberLinkIdElement);
                sink.End();
                sink.BeginList("adjudication", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Adjudication)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("detail", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Detail)
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
                    case "sequenceLinkId":
                        SequenceLinkIdElement = source.GetList<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "service":
                        Service = source.Get<Hl7.Fhir.Model.Coding>();
                        return true;
                    case "fee":
                        Fee = source.Get<Hl7.Fhir.Model.DSTU2.Money>();
                        return true;
                    case "noteNumberLinkId":
                        NoteNumberLinkIdElement = source.GetList<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "adjudication":
                        Adjudication = source.GetList<AddedItemAdjudicationComponent>();
                        return true;
                    case "detail":
                        Detail = source.GetList<AddedItemsDetailComponent>();
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
                    case "sequenceLinkId":
                    case "_sequenceLinkId":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "service":
                        Service = source.Populate(Service);
                        return true;
                    case "fee":
                        Fee = source.Populate(Fee);
                        return true;
                    case "noteNumberLinkId":
                    case "_noteNumberLinkId":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "adjudication":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "detail":
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
                    case "sequenceLinkId":
                        source.PopulatePrimitiveListItemValue(SequenceLinkIdElement, index);
                        return true;
                    case "_sequenceLinkId":
                        source.PopulatePrimitiveListItem(SequenceLinkIdElement, index);
                        return true;
                    case "noteNumberLinkId":
                        source.PopulatePrimitiveListItemValue(NoteNumberLinkIdElement, index);
                        return true;
                    case "_noteNumberLinkId":
                        source.PopulatePrimitiveListItem(NoteNumberLinkIdElement, index);
                        return true;
                    case "adjudication":
                        source.PopulateListItem(Adjudication, index);
                        return true;
                    case "detail":
                        source.PopulateListItem(Detail, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AddedItemComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceLinkIdElement != null) dest.SequenceLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(SequenceLinkIdElement.DeepCopy());
                    if(Service != null) dest.Service = (Hl7.Fhir.Model.Coding)Service.DeepCopy();
                    if(Fee != null) dest.Fee = (Hl7.Fhir.Model.DSTU2.Money)Fee.DeepCopy();
                    if(NoteNumberLinkIdElement != null) dest.NoteNumberLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(NoteNumberLinkIdElement.DeepCopy());
                    if(Adjudication != null) dest.Adjudication = new List<AddedItemAdjudicationComponent>(Adjudication.DeepCopy());
                    if(Detail != null) dest.Detail = new List<AddedItemsDetailComponent>(Detail.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new AddedItemComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AddedItemComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceLinkIdElement, otherT.SequenceLinkIdElement)) return false;
                if( !DeepComparable.Matches(Service, otherT.Service)) return false;
                if( !DeepComparable.Matches(Fee, otherT.Fee)) return false;
                if( !DeepComparable.Matches(NoteNumberLinkIdElement, otherT.NoteNumberLinkIdElement)) return false;
                if( !DeepComparable.Matches(Adjudication, otherT.Adjudication)) return false;
                if( !DeepComparable.Matches(Detail, otherT.Detail)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AddedItemComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceLinkIdElement, otherT.SequenceLinkIdElement)) return false;
                if( !DeepComparable.IsExactly(Service, otherT.Service)) return false;
                if( !DeepComparable.IsExactly(Fee, otherT.Fee)) return false;
                if( !DeepComparable.IsExactly(NoteNumberLinkIdElement, otherT.NoteNumberLinkIdElement)) return false;
                if( !DeepComparable.IsExactly(Adjudication, otherT.Adjudication)) return false;
                if( !DeepComparable.IsExactly(Detail, otherT.Detail)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in SequenceLinkIdElement) { if (elem != null) yield return elem; }
                    if (Service != null) yield return Service;
                    if (Fee != null) yield return Fee;
                    foreach (var elem in NoteNumberLinkIdElement) { if (elem != null) yield return elem; }
                    foreach (var elem in Adjudication) { if (elem != null) yield return elem; }
                    foreach (var elem in Detail) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in SequenceLinkIdElement) { if (elem != null) yield return new ElementValue("sequenceLinkId", elem); }
                    if (Service != null) yield return new ElementValue("service", Service);
                    if (Fee != null) yield return new ElementValue("fee", Fee);
                    foreach (var elem in NoteNumberLinkIdElement) { if (elem != null) yield return new ElementValue("noteNumberLinkId", elem); }
                    foreach (var elem in Adjudication) { if (elem != null) yield return new ElementValue("adjudication", elem); }
                    foreach (var elem in Detail) { if (elem != null) yield return new ElementValue("detail", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "AddedItemAdjudicationComponent")]
        [DataContract]
        public partial class AddedItemAdjudicationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "AddedItemAdjudicationComponent"; } }
            
            /// <summary>
            /// Adjudication category such as co-pay, eligible, benefit, etc.
            /// </summary>
            [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.Coding _Code;
            
            /// <summary>
            /// Monetary amount
            /// </summary>
            [FhirElement("amount", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.DSTU2.Money Amount
            {
                get { return _Amount; }
                set { _Amount = value; OnPropertyChanged("Amount"); }
            }
            
            private Hl7.Fhir.Model.DSTU2.Money _Amount;
            
            /// <summary>
            /// Non-monetary value
            /// </summary>
            [FhirElement("value", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _ValueElement;
            
            /// <summary>
            /// Non-monetary value
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("AddedItemAdjudicationComponent");
                base.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Code?.Serialize(sink);
                sink.Element("amount", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Amount?.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ValueElement?.Serialize(sink);
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
                    case "code":
                        Code = source.Get<Hl7.Fhir.Model.Coding>();
                        return true;
                    case "amount":
                        Amount = source.Get<Hl7.Fhir.Model.DSTU2.Money>();
                        return true;
                    case "value":
                        ValueElement = source.Get<Hl7.Fhir.Model.FhirDecimal>();
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
                    case "code":
                        Code = source.Populate(Code);
                        return true;
                    case "amount":
                        Amount = source.Populate(Amount);
                        return true;
                    case "value":
                        ValueElement = source.PopulateValue(ValueElement);
                        return true;
                    case "_value":
                        ValueElement = source.Populate(ValueElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AddedItemAdjudicationComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.Coding)Code.DeepCopy();
                    if(Amount != null) dest.Amount = (Hl7.Fhir.Model.DSTU2.Money)Amount.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirDecimal)ValueElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new AddedItemAdjudicationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AddedItemAdjudicationComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Amount, otherT.Amount)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AddedItemAdjudicationComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Amount, otherT.Amount)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Code != null) yield return Code;
                    if (Amount != null) yield return Amount;
                    if (ValueElement != null) yield return ValueElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Code != null) yield return new ElementValue("code", Code);
                    if (Amount != null) yield return new ElementValue("amount", Amount);
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "AddedItemsDetailComponent")]
        [DataContract]
        public partial class AddedItemsDetailComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "AddedItemsDetailComponent"; } }
            
            /// <summary>
            /// Service or Product
            /// </summary>
            [FhirElement("service", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Service
            {
                get { return _Service; }
                set { _Service = value; OnPropertyChanged("Service"); }
            }
            
            private Hl7.Fhir.Model.Coding _Service;
            
            /// <summary>
            /// Professional fee or Product charge
            /// </summary>
            [FhirElement("fee", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.DSTU2.Money Fee
            {
                get { return _Fee; }
                set { _Fee = value; OnPropertyChanged("Fee"); }
            }
            
            private Hl7.Fhir.Model.DSTU2.Money _Fee;
            
            /// <summary>
            /// Added items detail adjudication
            /// </summary>
            [FhirElement("adjudication", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<AddedItemDetailAdjudicationComponent> Adjudication
            {
                get { if(_Adjudication==null) _Adjudication = new List<AddedItemDetailAdjudicationComponent>(); return _Adjudication; }
                set { _Adjudication = value; OnPropertyChanged("Adjudication"); }
            }
            
            private List<AddedItemDetailAdjudicationComponent> _Adjudication;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("AddedItemsDetailComponent");
                base.Serialize(sink);
                sink.Element("service", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Service?.Serialize(sink);
                sink.Element("fee", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Fee?.Serialize(sink);
                sink.BeginList("adjudication", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Adjudication)
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
                    case "service":
                        Service = source.Get<Hl7.Fhir.Model.Coding>();
                        return true;
                    case "fee":
                        Fee = source.Get<Hl7.Fhir.Model.DSTU2.Money>();
                        return true;
                    case "adjudication":
                        Adjudication = source.GetList<AddedItemDetailAdjudicationComponent>();
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
                    case "service":
                        Service = source.Populate(Service);
                        return true;
                    case "fee":
                        Fee = source.Populate(Fee);
                        return true;
                    case "adjudication":
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
                    case "adjudication":
                        source.PopulateListItem(Adjudication, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AddedItemsDetailComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Service != null) dest.Service = (Hl7.Fhir.Model.Coding)Service.DeepCopy();
                    if(Fee != null) dest.Fee = (Hl7.Fhir.Model.DSTU2.Money)Fee.DeepCopy();
                    if(Adjudication != null) dest.Adjudication = new List<AddedItemDetailAdjudicationComponent>(Adjudication.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new AddedItemsDetailComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AddedItemsDetailComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Service, otherT.Service)) return false;
                if( !DeepComparable.Matches(Fee, otherT.Fee)) return false;
                if( !DeepComparable.Matches(Adjudication, otherT.Adjudication)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AddedItemsDetailComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Service, otherT.Service)) return false;
                if( !DeepComparable.IsExactly(Fee, otherT.Fee)) return false;
                if( !DeepComparable.IsExactly(Adjudication, otherT.Adjudication)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Service != null) yield return Service;
                    if (Fee != null) yield return Fee;
                    foreach (var elem in Adjudication) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Service != null) yield return new ElementValue("service", Service);
                    if (Fee != null) yield return new ElementValue("fee", Fee);
                    foreach (var elem in Adjudication) { if (elem != null) yield return new ElementValue("adjudication", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "AddedItemDetailAdjudicationComponent")]
        [DataContract]
        public partial class AddedItemDetailAdjudicationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "AddedItemDetailAdjudicationComponent"; } }
            
            /// <summary>
            /// Adjudication category such as co-pay, eligible, benefit, etc.
            /// </summary>
            [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.Coding _Code;
            
            /// <summary>
            /// Monetary amount
            /// </summary>
            [FhirElement("amount", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.DSTU2.Money Amount
            {
                get { return _Amount; }
                set { _Amount = value; OnPropertyChanged("Amount"); }
            }
            
            private Hl7.Fhir.Model.DSTU2.Money _Amount;
            
            /// <summary>
            /// Non-monetary value
            /// </summary>
            [FhirElement("value", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _ValueElement;
            
            /// <summary>
            /// Non-monetary value
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("AddedItemDetailAdjudicationComponent");
                base.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Code?.Serialize(sink);
                sink.Element("amount", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Amount?.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ValueElement?.Serialize(sink);
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
                    case "code":
                        Code = source.Get<Hl7.Fhir.Model.Coding>();
                        return true;
                    case "amount":
                        Amount = source.Get<Hl7.Fhir.Model.DSTU2.Money>();
                        return true;
                    case "value":
                        ValueElement = source.Get<Hl7.Fhir.Model.FhirDecimal>();
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
                    case "code":
                        Code = source.Populate(Code);
                        return true;
                    case "amount":
                        Amount = source.Populate(Amount);
                        return true;
                    case "value":
                        ValueElement = source.PopulateValue(ValueElement);
                        return true;
                    case "_value":
                        ValueElement = source.Populate(ValueElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AddedItemDetailAdjudicationComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.Coding)Code.DeepCopy();
                    if(Amount != null) dest.Amount = (Hl7.Fhir.Model.DSTU2.Money)Amount.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirDecimal)ValueElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new AddedItemDetailAdjudicationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AddedItemDetailAdjudicationComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Amount, otherT.Amount)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AddedItemDetailAdjudicationComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Amount, otherT.Amount)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Code != null) yield return Code;
                    if (Amount != null) yield return Amount;
                    if (ValueElement != null) yield return ValueElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Code != null) yield return new ElementValue("code", Code);
                    if (Amount != null) yield return new ElementValue("amount", Amount);
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "ErrorsComponent")]
        [DataContract]
        public partial class ErrorsComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ErrorsComponent"; } }
            
            /// <summary>
            /// Item sequence number
            /// </summary>
            [FhirElement("sequenceLinkId", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceLinkIdElement
            {
                get { return _SequenceLinkIdElement; }
                set { _SequenceLinkIdElement = value; OnPropertyChanged("SequenceLinkIdElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceLinkIdElement;
            
            /// <summary>
            /// Item sequence number
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? SequenceLinkId
            {
                get { return SequenceLinkIdElement != null ? SequenceLinkIdElement.Value : null; }
                set
                {
                    if (value == null)
                        SequenceLinkIdElement = null;
                    else
                        SequenceLinkIdElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("SequenceLinkId");
                }
            }
            
            /// <summary>
            /// Detail sequence number
            /// </summary>
            [FhirElement("detailSequenceLinkId", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt DetailSequenceLinkIdElement
            {
                get { return _DetailSequenceLinkIdElement; }
                set { _DetailSequenceLinkIdElement = value; OnPropertyChanged("DetailSequenceLinkIdElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _DetailSequenceLinkIdElement;
            
            /// <summary>
            /// Detail sequence number
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? DetailSequenceLinkId
            {
                get { return DetailSequenceLinkIdElement != null ? DetailSequenceLinkIdElement.Value : null; }
                set
                {
                    if (value == null)
                        DetailSequenceLinkIdElement = null;
                    else
                        DetailSequenceLinkIdElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("DetailSequenceLinkId");
                }
            }
            
            /// <summary>
            /// Subdetail sequence number
            /// </summary>
            [FhirElement("subdetailSequenceLinkId", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SubdetailSequenceLinkIdElement
            {
                get { return _SubdetailSequenceLinkIdElement; }
                set { _SubdetailSequenceLinkIdElement = value; OnPropertyChanged("SubdetailSequenceLinkIdElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SubdetailSequenceLinkIdElement;
            
            /// <summary>
            /// Subdetail sequence number
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? SubdetailSequenceLinkId
            {
                get { return SubdetailSequenceLinkIdElement != null ? SubdetailSequenceLinkIdElement.Value : null; }
                set
                {
                    if (value == null)
                        SubdetailSequenceLinkIdElement = null;
                    else
                        SubdetailSequenceLinkIdElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("SubdetailSequenceLinkId");
                }
            }
            
            /// <summary>
            /// Error code detailing processing issues
            /// </summary>
            [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.Coding _Code;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ErrorsComponent");
                base.Serialize(sink);
                sink.Element("sequenceLinkId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); SequenceLinkIdElement?.Serialize(sink);
                sink.Element("detailSequenceLinkId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DetailSequenceLinkIdElement?.Serialize(sink);
                sink.Element("subdetailSequenceLinkId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); SubdetailSequenceLinkIdElement?.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Code?.Serialize(sink);
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
                    case "sequenceLinkId":
                        SequenceLinkIdElement = source.Get<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "detailSequenceLinkId":
                        DetailSequenceLinkIdElement = source.Get<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "subdetailSequenceLinkId":
                        SubdetailSequenceLinkIdElement = source.Get<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "code":
                        Code = source.Get<Hl7.Fhir.Model.Coding>();
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
                    case "sequenceLinkId":
                        SequenceLinkIdElement = source.PopulateValue(SequenceLinkIdElement);
                        return true;
                    case "_sequenceLinkId":
                        SequenceLinkIdElement = source.Populate(SequenceLinkIdElement);
                        return true;
                    case "detailSequenceLinkId":
                        DetailSequenceLinkIdElement = source.PopulateValue(DetailSequenceLinkIdElement);
                        return true;
                    case "_detailSequenceLinkId":
                        DetailSequenceLinkIdElement = source.Populate(DetailSequenceLinkIdElement);
                        return true;
                    case "subdetailSequenceLinkId":
                        SubdetailSequenceLinkIdElement = source.PopulateValue(SubdetailSequenceLinkIdElement);
                        return true;
                    case "_subdetailSequenceLinkId":
                        SubdetailSequenceLinkIdElement = source.Populate(SubdetailSequenceLinkIdElement);
                        return true;
                    case "code":
                        Code = source.Populate(Code);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ErrorsComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceLinkIdElement != null) dest.SequenceLinkIdElement = (Hl7.Fhir.Model.PositiveInt)SequenceLinkIdElement.DeepCopy();
                    if(DetailSequenceLinkIdElement != null) dest.DetailSequenceLinkIdElement = (Hl7.Fhir.Model.PositiveInt)DetailSequenceLinkIdElement.DeepCopy();
                    if(SubdetailSequenceLinkIdElement != null) dest.SubdetailSequenceLinkIdElement = (Hl7.Fhir.Model.PositiveInt)SubdetailSequenceLinkIdElement.DeepCopy();
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.Coding)Code.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ErrorsComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ErrorsComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceLinkIdElement, otherT.SequenceLinkIdElement)) return false;
                if( !DeepComparable.Matches(DetailSequenceLinkIdElement, otherT.DetailSequenceLinkIdElement)) return false;
                if( !DeepComparable.Matches(SubdetailSequenceLinkIdElement, otherT.SubdetailSequenceLinkIdElement)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ErrorsComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceLinkIdElement, otherT.SequenceLinkIdElement)) return false;
                if( !DeepComparable.IsExactly(DetailSequenceLinkIdElement, otherT.DetailSequenceLinkIdElement)) return false;
                if( !DeepComparable.IsExactly(SubdetailSequenceLinkIdElement, otherT.SubdetailSequenceLinkIdElement)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SequenceLinkIdElement != null) yield return SequenceLinkIdElement;
                    if (DetailSequenceLinkIdElement != null) yield return DetailSequenceLinkIdElement;
                    if (SubdetailSequenceLinkIdElement != null) yield return SubdetailSequenceLinkIdElement;
                    if (Code != null) yield return Code;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SequenceLinkIdElement != null) yield return new ElementValue("sequenceLinkId", SequenceLinkIdElement);
                    if (DetailSequenceLinkIdElement != null) yield return new ElementValue("detailSequenceLinkId", DetailSequenceLinkIdElement);
                    if (SubdetailSequenceLinkIdElement != null) yield return new ElementValue("subdetailSequenceLinkId", SubdetailSequenceLinkIdElement);
                    if (Code != null) yield return new ElementValue("code", Code);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "NotesComponent")]
        [DataContract]
        public partial class NotesComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "NotesComponent"; } }
            
            /// <summary>
            /// Note Number for this note
            /// </summary>
            [FhirElement("number", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt NumberElement
            {
                get { return _NumberElement; }
                set { _NumberElement = value; OnPropertyChanged("NumberElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _NumberElement;
            
            /// <summary>
            /// Note Number for this note
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Number
            {
                get { return NumberElement != null ? NumberElement.Value : null; }
                set
                {
                    if (value == null)
                        NumberElement = null;
                    else
                        NumberElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("Number");
                }
            }
            
            /// <summary>
            /// display | print | printoper
            /// </summary>
            [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.Coding _Type;
            
            /// <summary>
            /// Note explanatory text
            /// </summary>
            [FhirElement("text", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TextElement
            {
                get { return _TextElement; }
                set { _TextElement = value; OnPropertyChanged("TextElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TextElement;
            
            /// <summary>
            /// Note explanatory text
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("NotesComponent");
                base.Serialize(sink);
                sink.Element("number", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NumberElement?.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Type?.Serialize(sink);
                sink.Element("text", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TextElement?.Serialize(sink);
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
                    case "number":
                        NumberElement = source.Get<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "type":
                        Type = source.Get<Hl7.Fhir.Model.Coding>();
                        return true;
                    case "text":
                        TextElement = source.Get<Hl7.Fhir.Model.FhirString>();
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
                    case "number":
                        NumberElement = source.PopulateValue(NumberElement);
                        return true;
                    case "_number":
                        NumberElement = source.Populate(NumberElement);
                        return true;
                    case "type":
                        Type = source.Populate(Type);
                        return true;
                    case "text":
                        TextElement = source.PopulateValue(TextElement);
                        return true;
                    case "_text":
                        TextElement = source.Populate(TextElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as NotesComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NumberElement != null) dest.NumberElement = (Hl7.Fhir.Model.PositiveInt)NumberElement.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.Coding)Type.DeepCopy();
                    if(TextElement != null) dest.TextElement = (Hl7.Fhir.Model.FhirString)TextElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new NotesComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as NotesComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NumberElement, otherT.NumberElement)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(TextElement, otherT.TextElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as NotesComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NumberElement, otherT.NumberElement)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(TextElement, otherT.TextElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NumberElement != null) yield return NumberElement;
                    if (Type != null) yield return Type;
                    if (TextElement != null) yield return TextElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NumberElement != null) yield return new ElementValue("number", NumberElement);
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (TextElement != null) yield return new ElementValue("text", TextElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "CoverageComponent")]
        [DataContract]
        public partial class CoverageComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "CoverageComponent"; } }
            
            /// <summary>
            /// Service instance identifier
            /// </summary>
            [FhirElement("sequence", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceElement;
            
            /// <summary>
            /// Service instance identifier
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Sequence
            {
                get { return SequenceElement != null ? SequenceElement.Value : null; }
                set
                {
                    if (value == null)
                        SequenceElement = null;
                    else
                        SequenceElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("Sequence");
                }
            }
            
            /// <summary>
            /// Is the focal Coverage
            /// </summary>
            [FhirElement("focal", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean FocalElement
            {
                get { return _FocalElement; }
                set { _FocalElement = value; OnPropertyChanged("FocalElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _FocalElement;
            
            /// <summary>
            /// Is the focal Coverage
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Focal
            {
                get { return FocalElement != null ? FocalElement.Value : null; }
                set
                {
                    if (value == null)
                        FocalElement = null;
                    else
                        FocalElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Focal");
                }
            }
            
            /// <summary>
            /// Insurance information
            /// </summary>
            [FhirElement("coverage", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
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
            /// Business agreement
            /// </summary>
            [FhirElement("businessArrangement", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString BusinessArrangementElement
            {
                get { return _BusinessArrangementElement; }
                set { _BusinessArrangementElement = value; OnPropertyChanged("BusinessArrangementElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _BusinessArrangementElement;
            
            /// <summary>
            /// Business agreement
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string BusinessArrangement
            {
                get { return BusinessArrangementElement != null ? BusinessArrangementElement.Value : null; }
                set
                {
                    if (value == null)
                        BusinessArrangementElement = null;
                    else
                        BusinessArrangementElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("BusinessArrangement");
                }
            }
            
            /// <summary>
            /// Patient relationship to subscriber
            /// </summary>
            [FhirElement("relationship", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Relationship
            {
                get { return _Relationship; }
                set { _Relationship = value; OnPropertyChanged("Relationship"); }
            }
            
            private Hl7.Fhir.Model.Coding _Relationship;
            
            /// <summary>
            /// Pre-Authorization/Determination Reference
            /// </summary>
            [FhirElement("preAuthRef", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> PreAuthRefElement
            {
                get { if(_PreAuthRefElement==null) _PreAuthRefElement = new List<Hl7.Fhir.Model.FhirString>(); return _PreAuthRefElement; }
                set { _PreAuthRefElement = value; OnPropertyChanged("PreAuthRefElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _PreAuthRefElement;
            
            /// <summary>
            /// Pre-Authorization/Determination Reference
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> PreAuthRef
            {
                get { return PreAuthRefElement != null ? PreAuthRefElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        PreAuthRefElement = null;
                    else
                        PreAuthRefElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("PreAuthRef");
                }
            }
            
            /// <summary>
            /// Adjudication results
            /// </summary>
            [FhirElement("claimResponse", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
            [CLSCompliant(false)]
            [References("ClaimResponse")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference ClaimResponse
            {
                get { return _ClaimResponse; }
                set { _ClaimResponse = value; OnPropertyChanged("ClaimResponse"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _ClaimResponse;
            
            /// <summary>
            /// Original version
            /// </summary>
            [FhirElement("originalRuleset", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Coding OriginalRuleset
            {
                get { return _OriginalRuleset; }
                set { _OriginalRuleset = value; OnPropertyChanged("OriginalRuleset"); }
            }
            
            private Hl7.Fhir.Model.Coding _OriginalRuleset;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("CoverageComponent");
                base.Serialize(sink);
                sink.Element("sequence", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); SequenceElement?.Serialize(sink);
                sink.Element("focal", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); FocalElement?.Serialize(sink);
                sink.Element("coverage", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Coverage?.Serialize(sink);
                sink.Element("businessArrangement", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); BusinessArrangementElement?.Serialize(sink);
                sink.Element("relationship", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Relationship?.Serialize(sink);
                sink.BeginList("preAuthRef", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                sink.Serialize(PreAuthRefElement);
                sink.End();
                sink.Element("claimResponse", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ClaimResponse?.Serialize(sink);
                sink.Element("originalRuleset", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); OriginalRuleset?.Serialize(sink);
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
                    case "sequence":
                        SequenceElement = source.Get<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "focal":
                        FocalElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                        return true;
                    case "coverage":
                        Coverage = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "businessArrangement":
                        BusinessArrangementElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "relationship":
                        Relationship = source.Get<Hl7.Fhir.Model.Coding>();
                        return true;
                    case "preAuthRef":
                        PreAuthRefElement = source.GetList<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "claimResponse":
                        ClaimResponse = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "originalRuleset":
                        OriginalRuleset = source.Get<Hl7.Fhir.Model.Coding>();
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
                    case "sequence":
                        SequenceElement = source.PopulateValue(SequenceElement);
                        return true;
                    case "_sequence":
                        SequenceElement = source.Populate(SequenceElement);
                        return true;
                    case "focal":
                        FocalElement = source.PopulateValue(FocalElement);
                        return true;
                    case "_focal":
                        FocalElement = source.Populate(FocalElement);
                        return true;
                    case "coverage":
                        Coverage = source.Populate(Coverage);
                        return true;
                    case "businessArrangement":
                        BusinessArrangementElement = source.PopulateValue(BusinessArrangementElement);
                        return true;
                    case "_businessArrangement":
                        BusinessArrangementElement = source.Populate(BusinessArrangementElement);
                        return true;
                    case "relationship":
                        Relationship = source.Populate(Relationship);
                        return true;
                    case "preAuthRef":
                    case "_preAuthRef":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "claimResponse":
                        ClaimResponse = source.Populate(ClaimResponse);
                        return true;
                    case "originalRuleset":
                        OriginalRuleset = source.Populate(OriginalRuleset);
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
                    case "preAuthRef":
                        source.PopulatePrimitiveListItemValue(PreAuthRefElement, index);
                        return true;
                    case "_preAuthRef":
                        source.PopulatePrimitiveListItem(PreAuthRefElement, index);
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
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.PositiveInt)SequenceElement.DeepCopy();
                    if(FocalElement != null) dest.FocalElement = (Hl7.Fhir.Model.FhirBoolean)FocalElement.DeepCopy();
                    if(Coverage != null) dest.Coverage = (Hl7.Fhir.Model.ResourceReference)Coverage.DeepCopy();
                    if(BusinessArrangementElement != null) dest.BusinessArrangementElement = (Hl7.Fhir.Model.FhirString)BusinessArrangementElement.DeepCopy();
                    if(Relationship != null) dest.Relationship = (Hl7.Fhir.Model.Coding)Relationship.DeepCopy();
                    if(PreAuthRefElement != null) dest.PreAuthRefElement = new List<Hl7.Fhir.Model.FhirString>(PreAuthRefElement.DeepCopy());
                    if(ClaimResponse != null) dest.ClaimResponse = (Hl7.Fhir.Model.ResourceReference)ClaimResponse.DeepCopy();
                    if(OriginalRuleset != null) dest.OriginalRuleset = (Hl7.Fhir.Model.Coding)OriginalRuleset.DeepCopy();
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
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(FocalElement, otherT.FocalElement)) return false;
                if( !DeepComparable.Matches(Coverage, otherT.Coverage)) return false;
                if( !DeepComparable.Matches(BusinessArrangementElement, otherT.BusinessArrangementElement)) return false;
                if( !DeepComparable.Matches(Relationship, otherT.Relationship)) return false;
                if( !DeepComparable.Matches(PreAuthRefElement, otherT.PreAuthRefElement)) return false;
                if( !DeepComparable.Matches(ClaimResponse, otherT.ClaimResponse)) return false;
                if( !DeepComparable.Matches(OriginalRuleset, otherT.OriginalRuleset)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CoverageComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(FocalElement, otherT.FocalElement)) return false;
                if( !DeepComparable.IsExactly(Coverage, otherT.Coverage)) return false;
                if( !DeepComparable.IsExactly(BusinessArrangementElement, otherT.BusinessArrangementElement)) return false;
                if( !DeepComparable.IsExactly(Relationship, otherT.Relationship)) return false;
                if( !DeepComparable.IsExactly(PreAuthRefElement, otherT.PreAuthRefElement)) return false;
                if( !DeepComparable.IsExactly(ClaimResponse, otherT.ClaimResponse)) return false;
                if( !DeepComparable.IsExactly(OriginalRuleset, otherT.OriginalRuleset)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SequenceElement != null) yield return SequenceElement;
                    if (FocalElement != null) yield return FocalElement;
                    if (Coverage != null) yield return Coverage;
                    if (BusinessArrangementElement != null) yield return BusinessArrangementElement;
                    if (Relationship != null) yield return Relationship;
                    foreach (var elem in PreAuthRefElement) { if (elem != null) yield return elem; }
                    if (ClaimResponse != null) yield return ClaimResponse;
                    if (OriginalRuleset != null) yield return OriginalRuleset;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SequenceElement != null) yield return new ElementValue("sequence", SequenceElement);
                    if (FocalElement != null) yield return new ElementValue("focal", FocalElement);
                    if (Coverage != null) yield return new ElementValue("coverage", Coverage);
                    if (BusinessArrangementElement != null) yield return new ElementValue("businessArrangement", BusinessArrangementElement);
                    if (Relationship != null) yield return new ElementValue("relationship", Relationship);
                    foreach (var elem in PreAuthRefElement) { if (elem != null) yield return new ElementValue("preAuthRef", elem); }
                    if (ClaimResponse != null) yield return new ElementValue("claimResponse", ClaimResponse);
                    if (OriginalRuleset != null) yield return new ElementValue("originalRuleset", OriginalRuleset);
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IClaimResponseAddedItemComponent> Hl7.Fhir.Model.IClaimResponse.AddItem { get { return AddItem; } }
    
        
        /// <summary>
        /// Response  number
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
        /// Id of resource triggering adjudication
        /// </summary>
        [FhirElement("request", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [References("Claim")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Request
        {
            get { return _Request; }
            set { _Request = value; OnPropertyChanged("Request"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Request;
        
        /// <summary>
        /// Resource version
        /// </summary>
        [FhirElement("ruleset", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Coding Ruleset
        {
            get { return _Ruleset; }
            set { _Ruleset = value; OnPropertyChanged("Ruleset"); }
        }
        
        private Hl7.Fhir.Model.Coding _Ruleset;
        
        /// <summary>
        /// Original version
        /// </summary>
        [FhirElement("originalRuleset", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Coding OriginalRuleset
        {
            get { return _OriginalRuleset; }
            set { _OriginalRuleset = value; OnPropertyChanged("OriginalRuleset"); }
        }
        
        private Hl7.Fhir.Model.Coding _OriginalRuleset;
        
        /// <summary>
        /// Creation date
        /// </summary>
        [FhirElement("created", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime CreatedElement
        {
            get { return _CreatedElement; }
            set { _CreatedElement = value; OnPropertyChanged("CreatedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _CreatedElement;
        
        /// <summary>
        /// Creation date
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Created
        {
            get { return CreatedElement != null ? CreatedElement.Value : null; }
            set
            {
                if (value == null)
                    CreatedElement = null;
                else
                    CreatedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Created");
            }
        }
        
        /// <summary>
        /// Insurer
        /// </summary>
        [FhirElement("organization", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Organization
        {
            get { return _Organization; }
            set { _Organization = value; OnPropertyChanged("Organization"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Organization;
        
        /// <summary>
        /// Responsible practitioner
        /// </summary>
        [FhirElement("requestProvider", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [References("Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference RequestProvider
        {
            get { return _RequestProvider; }
            set { _RequestProvider = value; OnPropertyChanged("RequestProvider"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _RequestProvider;
        
        /// <summary>
        /// Responsible organization
        /// </summary>
        [FhirElement("requestOrganization", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference RequestOrganization
        {
            get { return _RequestOrganization; }
            set { _RequestOrganization = value; OnPropertyChanged("RequestOrganization"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _RequestOrganization;
        
        /// <summary>
        /// complete | error
        /// </summary>
        [FhirElement("outcome", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DSTU2.RemittanceOutcome> OutcomeElement
        {
            get { return _OutcomeElement; }
            set { _OutcomeElement = value; OnPropertyChanged("OutcomeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.DSTU2.RemittanceOutcome> _OutcomeElement;
        
        /// <summary>
        /// complete | error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DSTU2.RemittanceOutcome? Outcome
        {
            get { return OutcomeElement != null ? OutcomeElement.Value : null; }
            set
            {
                if (value == null)
                    OutcomeElement = null;
                else
                    OutcomeElement = new Code<Hl7.Fhir.Model.DSTU2.RemittanceOutcome>(value);
                OnPropertyChanged("Outcome");
            }
        }
        
        /// <summary>
        /// Disposition Message
        /// </summary>
        [FhirElement("disposition", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DispositionElement
        {
            get { return _DispositionElement; }
            set { _DispositionElement = value; OnPropertyChanged("DispositionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DispositionElement;
        
        /// <summary>
        /// Disposition Message
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Disposition
        {
            get { return DispositionElement != null ? DispositionElement.Value : null; }
            set
            {
                if (value == null)
                    DispositionElement = null;
                else
                    DispositionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Disposition");
            }
        }
        
        /// <summary>
        /// Party to be paid any benefits payable
        /// </summary>
        [FhirElement("payeeType", InSummary=Hl7.Fhir.Model.Version.All, Order=190)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Coding PayeeType
        {
            get { return _PayeeType; }
            set { _PayeeType = value; OnPropertyChanged("PayeeType"); }
        }
        
        private Hl7.Fhir.Model.Coding _PayeeType;
        
        /// <summary>
        /// Line items
        /// </summary>
        [FhirElement("item", InSummary=Hl7.Fhir.Model.Version.All, Order=200)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ItemsComponent> Item
        {
            get { if(_Item==null) _Item = new List<ItemsComponent>(); return _Item; }
            set { _Item = value; OnPropertyChanged("Item"); }
        }
        
        private List<ItemsComponent> _Item;
        
        /// <summary>
        /// Insurer added line items
        /// </summary>
        [FhirElement("addItem", InSummary=Hl7.Fhir.Model.Version.All, Order=210)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<AddedItemComponent> AddItem
        {
            get { if(_AddItem==null) _AddItem = new List<AddedItemComponent>(); return _AddItem; }
            set { _AddItem = value; OnPropertyChanged("AddItem"); }
        }
        
        private List<AddedItemComponent> _AddItem;
        
        /// <summary>
        /// Processing errors
        /// </summary>
        [FhirElement("error", InSummary=Hl7.Fhir.Model.Version.All, Order=220)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ErrorsComponent> Error
        {
            get { if(_Error==null) _Error = new List<ErrorsComponent>(); return _Error; }
            set { _Error = value; OnPropertyChanged("Error"); }
        }
        
        private List<ErrorsComponent> _Error;
        
        /// <summary>
        /// Total Cost of service from the Claim
        /// </summary>
        [FhirElement("totalCost", InSummary=Hl7.Fhir.Model.Version.All, Order=230)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.DSTU2.Money TotalCost
        {
            get { return _TotalCost; }
            set { _TotalCost = value; OnPropertyChanged("TotalCost"); }
        }
        
        private Hl7.Fhir.Model.DSTU2.Money _TotalCost;
        
        /// <summary>
        /// Unallocated deductible
        /// </summary>
        [FhirElement("unallocDeductable", InSummary=Hl7.Fhir.Model.Version.All, Order=240)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.DSTU2.Money UnallocDeductable
        {
            get { return _UnallocDeductable; }
            set { _UnallocDeductable = value; OnPropertyChanged("UnallocDeductable"); }
        }
        
        private Hl7.Fhir.Model.DSTU2.Money _UnallocDeductable;
        
        /// <summary>
        /// Total benefit payable for the Claim
        /// </summary>
        [FhirElement("totalBenefit", InSummary=Hl7.Fhir.Model.Version.All, Order=250)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.DSTU2.Money TotalBenefit
        {
            get { return _TotalBenefit; }
            set { _TotalBenefit = value; OnPropertyChanged("TotalBenefit"); }
        }
        
        private Hl7.Fhir.Model.DSTU2.Money _TotalBenefit;
        
        /// <summary>
        /// Payment adjustment for non-Claim issues
        /// </summary>
        [FhirElement("paymentAdjustment", InSummary=Hl7.Fhir.Model.Version.All, Order=260)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.DSTU2.Money PaymentAdjustment
        {
            get { return _PaymentAdjustment; }
            set { _PaymentAdjustment = value; OnPropertyChanged("PaymentAdjustment"); }
        }
        
        private Hl7.Fhir.Model.DSTU2.Money _PaymentAdjustment;
        
        /// <summary>
        /// Reason for Payment adjustment
        /// </summary>
        [FhirElement("paymentAdjustmentReason", InSummary=Hl7.Fhir.Model.Version.All, Order=270)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Coding PaymentAdjustmentReason
        {
            get { return _PaymentAdjustmentReason; }
            set { _PaymentAdjustmentReason = value; OnPropertyChanged("PaymentAdjustmentReason"); }
        }
        
        private Hl7.Fhir.Model.Coding _PaymentAdjustmentReason;
        
        /// <summary>
        /// Expected data of Payment
        /// </summary>
        [FhirElement("paymentDate", InSummary=Hl7.Fhir.Model.Version.All, Order=280)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Date PaymentDateElement
        {
            get { return _PaymentDateElement; }
            set { _PaymentDateElement = value; OnPropertyChanged("PaymentDateElement"); }
        }
        
        private Hl7.Fhir.Model.Date _PaymentDateElement;
        
        /// <summary>
        /// Expected data of Payment
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string PaymentDate
        {
            get { return PaymentDateElement != null ? PaymentDateElement.Value : null; }
            set
            {
                if (value == null)
                    PaymentDateElement = null;
                else
                    PaymentDateElement = new Hl7.Fhir.Model.Date(value);
                OnPropertyChanged("PaymentDate");
            }
        }
        
        /// <summary>
        /// Payment amount
        /// </summary>
        [FhirElement("paymentAmount", InSummary=Hl7.Fhir.Model.Version.All, Order=290)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.DSTU2.Money PaymentAmount
        {
            get { return _PaymentAmount; }
            set { _PaymentAmount = value; OnPropertyChanged("PaymentAmount"); }
        }
        
        private Hl7.Fhir.Model.DSTU2.Money _PaymentAmount;
        
        /// <summary>
        /// Payment identifier
        /// </summary>
        [FhirElement("paymentRef", InSummary=Hl7.Fhir.Model.Version.All, Order=300)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier PaymentRef
        {
            get { return _PaymentRef; }
            set { _PaymentRef = value; OnPropertyChanged("PaymentRef"); }
        }
        
        private Hl7.Fhir.Model.Identifier _PaymentRef;
        
        /// <summary>
        /// Funds reserved status
        /// </summary>
        [FhirElement("reserved", InSummary=Hl7.Fhir.Model.Version.All, Order=310)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Coding Reserved
        {
            get { return _Reserved; }
            set { _Reserved = value; OnPropertyChanged("Reserved"); }
        }
        
        private Hl7.Fhir.Model.Coding _Reserved;
        
        /// <summary>
        /// Printed Form Identifier
        /// </summary>
        [FhirElement("form", InSummary=Hl7.Fhir.Model.Version.All, Order=320)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Coding Form
        {
            get { return _Form; }
            set { _Form = value; OnPropertyChanged("Form"); }
        }
        
        private Hl7.Fhir.Model.Coding _Form;
        
        /// <summary>
        /// Processing notes
        /// </summary>
        [FhirElement("note", InSummary=Hl7.Fhir.Model.Version.All, Order=330)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<NotesComponent> Note
        {
            get { if(_Note==null) _Note = new List<NotesComponent>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<NotesComponent> _Note;
        
        /// <summary>
        /// Insurance or medical plan
        /// </summary>
        [FhirElement("coverage", InSummary=Hl7.Fhir.Model.Version.All, Order=340)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<CoverageComponent> Coverage
        {
            get { if(_Coverage==null) _Coverage = new List<CoverageComponent>(); return _Coverage; }
            set { _Coverage = value; OnPropertyChanged("Coverage"); }
        }
        
        private List<CoverageComponent> _Coverage;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ClaimResponse;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Request != null) dest.Request = (Hl7.Fhir.Model.ResourceReference)Request.DeepCopy();
                if(Ruleset != null) dest.Ruleset = (Hl7.Fhir.Model.Coding)Ruleset.DeepCopy();
                if(OriginalRuleset != null) dest.OriginalRuleset = (Hl7.Fhir.Model.Coding)OriginalRuleset.DeepCopy();
                if(CreatedElement != null) dest.CreatedElement = (Hl7.Fhir.Model.FhirDateTime)CreatedElement.DeepCopy();
                if(Organization != null) dest.Organization = (Hl7.Fhir.Model.ResourceReference)Organization.DeepCopy();
                if(RequestProvider != null) dest.RequestProvider = (Hl7.Fhir.Model.ResourceReference)RequestProvider.DeepCopy();
                if(RequestOrganization != null) dest.RequestOrganization = (Hl7.Fhir.Model.ResourceReference)RequestOrganization.DeepCopy();
                if(OutcomeElement != null) dest.OutcomeElement = (Code<Hl7.Fhir.Model.DSTU2.RemittanceOutcome>)OutcomeElement.DeepCopy();
                if(DispositionElement != null) dest.DispositionElement = (Hl7.Fhir.Model.FhirString)DispositionElement.DeepCopy();
                if(PayeeType != null) dest.PayeeType = (Hl7.Fhir.Model.Coding)PayeeType.DeepCopy();
                if(Item != null) dest.Item = new List<ItemsComponent>(Item.DeepCopy());
                if(AddItem != null) dest.AddItem = new List<AddedItemComponent>(AddItem.DeepCopy());
                if(Error != null) dest.Error = new List<ErrorsComponent>(Error.DeepCopy());
                if(TotalCost != null) dest.TotalCost = (Hl7.Fhir.Model.DSTU2.Money)TotalCost.DeepCopy();
                if(UnallocDeductable != null) dest.UnallocDeductable = (Hl7.Fhir.Model.DSTU2.Money)UnallocDeductable.DeepCopy();
                if(TotalBenefit != null) dest.TotalBenefit = (Hl7.Fhir.Model.DSTU2.Money)TotalBenefit.DeepCopy();
                if(PaymentAdjustment != null) dest.PaymentAdjustment = (Hl7.Fhir.Model.DSTU2.Money)PaymentAdjustment.DeepCopy();
                if(PaymentAdjustmentReason != null) dest.PaymentAdjustmentReason = (Hl7.Fhir.Model.Coding)PaymentAdjustmentReason.DeepCopy();
                if(PaymentDateElement != null) dest.PaymentDateElement = (Hl7.Fhir.Model.Date)PaymentDateElement.DeepCopy();
                if(PaymentAmount != null) dest.PaymentAmount = (Hl7.Fhir.Model.DSTU2.Money)PaymentAmount.DeepCopy();
                if(PaymentRef != null) dest.PaymentRef = (Hl7.Fhir.Model.Identifier)PaymentRef.DeepCopy();
                if(Reserved != null) dest.Reserved = (Hl7.Fhir.Model.Coding)Reserved.DeepCopy();
                if(Form != null) dest.Form = (Hl7.Fhir.Model.Coding)Form.DeepCopy();
                if(Note != null) dest.Note = new List<NotesComponent>(Note.DeepCopy());
                if(Coverage != null) dest.Coverage = new List<CoverageComponent>(Coverage.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new ClaimResponse());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ClaimResponse;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Request, otherT.Request)) return false;
            if( !DeepComparable.Matches(Ruleset, otherT.Ruleset)) return false;
            if( !DeepComparable.Matches(OriginalRuleset, otherT.OriginalRuleset)) return false;
            if( !DeepComparable.Matches(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.Matches(Organization, otherT.Organization)) return false;
            if( !DeepComparable.Matches(RequestProvider, otherT.RequestProvider)) return false;
            if( !DeepComparable.Matches(RequestOrganization, otherT.RequestOrganization)) return false;
            if( !DeepComparable.Matches(OutcomeElement, otherT.OutcomeElement)) return false;
            if( !DeepComparable.Matches(DispositionElement, otherT.DispositionElement)) return false;
            if( !DeepComparable.Matches(PayeeType, otherT.PayeeType)) return false;
            if( !DeepComparable.Matches(Item, otherT.Item)) return false;
            if( !DeepComparable.Matches(AddItem, otherT.AddItem)) return false;
            if( !DeepComparable.Matches(Error, otherT.Error)) return false;
            if( !DeepComparable.Matches(TotalCost, otherT.TotalCost)) return false;
            if( !DeepComparable.Matches(UnallocDeductable, otherT.UnallocDeductable)) return false;
            if( !DeepComparable.Matches(TotalBenefit, otherT.TotalBenefit)) return false;
            if( !DeepComparable.Matches(PaymentAdjustment, otherT.PaymentAdjustment)) return false;
            if( !DeepComparable.Matches(PaymentAdjustmentReason, otherT.PaymentAdjustmentReason)) return false;
            if( !DeepComparable.Matches(PaymentDateElement, otherT.PaymentDateElement)) return false;
            if( !DeepComparable.Matches(PaymentAmount, otherT.PaymentAmount)) return false;
            if( !DeepComparable.Matches(PaymentRef, otherT.PaymentRef)) return false;
            if( !DeepComparable.Matches(Reserved, otherT.Reserved)) return false;
            if( !DeepComparable.Matches(Form, otherT.Form)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            if( !DeepComparable.Matches(Coverage, otherT.Coverage)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ClaimResponse;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Request, otherT.Request)) return false;
            if( !DeepComparable.IsExactly(Ruleset, otherT.Ruleset)) return false;
            if( !DeepComparable.IsExactly(OriginalRuleset, otherT.OriginalRuleset)) return false;
            if( !DeepComparable.IsExactly(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.IsExactly(Organization, otherT.Organization)) return false;
            if( !DeepComparable.IsExactly(RequestProvider, otherT.RequestProvider)) return false;
            if( !DeepComparable.IsExactly(RequestOrganization, otherT.RequestOrganization)) return false;
            if( !DeepComparable.IsExactly(OutcomeElement, otherT.OutcomeElement)) return false;
            if( !DeepComparable.IsExactly(DispositionElement, otherT.DispositionElement)) return false;
            if( !DeepComparable.IsExactly(PayeeType, otherT.PayeeType)) return false;
            if( !DeepComparable.IsExactly(Item, otherT.Item)) return false;
            if( !DeepComparable.IsExactly(AddItem, otherT.AddItem)) return false;
            if( !DeepComparable.IsExactly(Error, otherT.Error)) return false;
            if( !DeepComparable.IsExactly(TotalCost, otherT.TotalCost)) return false;
            if( !DeepComparable.IsExactly(UnallocDeductable, otherT.UnallocDeductable)) return false;
            if( !DeepComparable.IsExactly(TotalBenefit, otherT.TotalBenefit)) return false;
            if( !DeepComparable.IsExactly(PaymentAdjustment, otherT.PaymentAdjustment)) return false;
            if( !DeepComparable.IsExactly(PaymentAdjustmentReason, otherT.PaymentAdjustmentReason)) return false;
            if( !DeepComparable.IsExactly(PaymentDateElement, otherT.PaymentDateElement)) return false;
            if( !DeepComparable.IsExactly(PaymentAmount, otherT.PaymentAmount)) return false;
            if( !DeepComparable.IsExactly(PaymentRef, otherT.PaymentRef)) return false;
            if( !DeepComparable.IsExactly(Reserved, otherT.Reserved)) return false;
            if( !DeepComparable.IsExactly(Form, otherT.Form)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            if( !DeepComparable.IsExactly(Coverage, otherT.Coverage)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("ClaimResponse");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("request", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Request?.Serialize(sink);
            sink.Element("ruleset", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Ruleset?.Serialize(sink);
            sink.Element("originalRuleset", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); OriginalRuleset?.Serialize(sink);
            sink.Element("created", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CreatedElement?.Serialize(sink);
            sink.Element("organization", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Organization?.Serialize(sink);
            sink.Element("requestProvider", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); RequestProvider?.Serialize(sink);
            sink.Element("requestOrganization", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); RequestOrganization?.Serialize(sink);
            sink.Element("outcome", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); OutcomeElement?.Serialize(sink);
            sink.Element("disposition", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DispositionElement?.Serialize(sink);
            sink.Element("payeeType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PayeeType?.Serialize(sink);
            sink.BeginList("item", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Item)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("addItem", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in AddItem)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("error", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Error)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("totalCost", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TotalCost?.Serialize(sink);
            sink.Element("unallocDeductable", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); UnallocDeductable?.Serialize(sink);
            sink.Element("totalBenefit", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TotalBenefit?.Serialize(sink);
            sink.Element("paymentAdjustment", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PaymentAdjustment?.Serialize(sink);
            sink.Element("paymentAdjustmentReason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PaymentAdjustmentReason?.Serialize(sink);
            sink.Element("paymentDate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PaymentDateElement?.Serialize(sink);
            sink.Element("paymentAmount", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PaymentAmount?.Serialize(sink);
            sink.Element("paymentRef", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PaymentRef?.Serialize(sink);
            sink.Element("reserved", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Reserved?.Serialize(sink);
            sink.Element("form", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Form?.Serialize(sink);
            sink.BeginList("note", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Note)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("coverage", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Coverage)
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
                case "request":
                    Request = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "ruleset":
                    Ruleset = source.Get<Hl7.Fhir.Model.Coding>();
                    return true;
                case "originalRuleset":
                    OriginalRuleset = source.Get<Hl7.Fhir.Model.Coding>();
                    return true;
                case "created":
                    CreatedElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "organization":
                    Organization = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "requestProvider":
                    RequestProvider = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "requestOrganization":
                    RequestOrganization = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "outcome":
                    OutcomeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DSTU2.RemittanceOutcome>>();
                    return true;
                case "disposition":
                    DispositionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "payeeType":
                    PayeeType = source.Get<Hl7.Fhir.Model.Coding>();
                    return true;
                case "item":
                    Item = source.GetList<ItemsComponent>();
                    return true;
                case "addItem":
                    AddItem = source.GetList<AddedItemComponent>();
                    return true;
                case "error":
                    Error = source.GetList<ErrorsComponent>();
                    return true;
                case "totalCost":
                    TotalCost = source.Get<Hl7.Fhir.Model.DSTU2.Money>();
                    return true;
                case "unallocDeductable":
                    UnallocDeductable = source.Get<Hl7.Fhir.Model.DSTU2.Money>();
                    return true;
                case "totalBenefit":
                    TotalBenefit = source.Get<Hl7.Fhir.Model.DSTU2.Money>();
                    return true;
                case "paymentAdjustment":
                    PaymentAdjustment = source.Get<Hl7.Fhir.Model.DSTU2.Money>();
                    return true;
                case "paymentAdjustmentReason":
                    PaymentAdjustmentReason = source.Get<Hl7.Fhir.Model.Coding>();
                    return true;
                case "paymentDate":
                    PaymentDateElement = source.Get<Hl7.Fhir.Model.Date>();
                    return true;
                case "paymentAmount":
                    PaymentAmount = source.Get<Hl7.Fhir.Model.DSTU2.Money>();
                    return true;
                case "paymentRef":
                    PaymentRef = source.Get<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "reserved":
                    Reserved = source.Get<Hl7.Fhir.Model.Coding>();
                    return true;
                case "form":
                    Form = source.Get<Hl7.Fhir.Model.Coding>();
                    return true;
                case "note":
                    Note = source.GetList<NotesComponent>();
                    return true;
                case "coverage":
                    Coverage = source.GetList<CoverageComponent>();
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
                case "request":
                    Request = source.Populate(Request);
                    return true;
                case "ruleset":
                    Ruleset = source.Populate(Ruleset);
                    return true;
                case "originalRuleset":
                    OriginalRuleset = source.Populate(OriginalRuleset);
                    return true;
                case "created":
                    CreatedElement = source.PopulateValue(CreatedElement);
                    return true;
                case "_created":
                    CreatedElement = source.Populate(CreatedElement);
                    return true;
                case "organization":
                    Organization = source.Populate(Organization);
                    return true;
                case "requestProvider":
                    RequestProvider = source.Populate(RequestProvider);
                    return true;
                case "requestOrganization":
                    RequestOrganization = source.Populate(RequestOrganization);
                    return true;
                case "outcome":
                    OutcomeElement = source.PopulateValue(OutcomeElement);
                    return true;
                case "_outcome":
                    OutcomeElement = source.Populate(OutcomeElement);
                    return true;
                case "disposition":
                    DispositionElement = source.PopulateValue(DispositionElement);
                    return true;
                case "_disposition":
                    DispositionElement = source.Populate(DispositionElement);
                    return true;
                case "payeeType":
                    PayeeType = source.Populate(PayeeType);
                    return true;
                case "item":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "addItem":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "error":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "totalCost":
                    TotalCost = source.Populate(TotalCost);
                    return true;
                case "unallocDeductable":
                    UnallocDeductable = source.Populate(UnallocDeductable);
                    return true;
                case "totalBenefit":
                    TotalBenefit = source.Populate(TotalBenefit);
                    return true;
                case "paymentAdjustment":
                    PaymentAdjustment = source.Populate(PaymentAdjustment);
                    return true;
                case "paymentAdjustmentReason":
                    PaymentAdjustmentReason = source.Populate(PaymentAdjustmentReason);
                    return true;
                case "paymentDate":
                    PaymentDateElement = source.PopulateValue(PaymentDateElement);
                    return true;
                case "_paymentDate":
                    PaymentDateElement = source.Populate(PaymentDateElement);
                    return true;
                case "paymentAmount":
                    PaymentAmount = source.Populate(PaymentAmount);
                    return true;
                case "paymentRef":
                    PaymentRef = source.Populate(PaymentRef);
                    return true;
                case "reserved":
                    Reserved = source.Populate(Reserved);
                    return true;
                case "form":
                    Form = source.Populate(Form);
                    return true;
                case "note":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "coverage":
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
                case "item":
                    source.PopulateListItem(Item, index);
                    return true;
                case "addItem":
                    source.PopulateListItem(AddItem, index);
                    return true;
                case "error":
                    source.PopulateListItem(Error, index);
                    return true;
                case "note":
                    source.PopulateListItem(Note, index);
                    return true;
                case "coverage":
                    source.PopulateListItem(Coverage, index);
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
                if (Request != null) yield return Request;
                if (Ruleset != null) yield return Ruleset;
                if (OriginalRuleset != null) yield return OriginalRuleset;
                if (CreatedElement != null) yield return CreatedElement;
                if (Organization != null) yield return Organization;
                if (RequestProvider != null) yield return RequestProvider;
                if (RequestOrganization != null) yield return RequestOrganization;
                if (OutcomeElement != null) yield return OutcomeElement;
                if (DispositionElement != null) yield return DispositionElement;
                if (PayeeType != null) yield return PayeeType;
                foreach (var elem in Item) { if (elem != null) yield return elem; }
                foreach (var elem in AddItem) { if (elem != null) yield return elem; }
                foreach (var elem in Error) { if (elem != null) yield return elem; }
                if (TotalCost != null) yield return TotalCost;
                if (UnallocDeductable != null) yield return UnallocDeductable;
                if (TotalBenefit != null) yield return TotalBenefit;
                if (PaymentAdjustment != null) yield return PaymentAdjustment;
                if (PaymentAdjustmentReason != null) yield return PaymentAdjustmentReason;
                if (PaymentDateElement != null) yield return PaymentDateElement;
                if (PaymentAmount != null) yield return PaymentAmount;
                if (PaymentRef != null) yield return PaymentRef;
                if (Reserved != null) yield return Reserved;
                if (Form != null) yield return Form;
                foreach (var elem in Note) { if (elem != null) yield return elem; }
                foreach (var elem in Coverage) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (Request != null) yield return new ElementValue("request", Request);
                if (Ruleset != null) yield return new ElementValue("ruleset", Ruleset);
                if (OriginalRuleset != null) yield return new ElementValue("originalRuleset", OriginalRuleset);
                if (CreatedElement != null) yield return new ElementValue("created", CreatedElement);
                if (Organization != null) yield return new ElementValue("organization", Organization);
                if (RequestProvider != null) yield return new ElementValue("requestProvider", RequestProvider);
                if (RequestOrganization != null) yield return new ElementValue("requestOrganization", RequestOrganization);
                if (OutcomeElement != null) yield return new ElementValue("outcome", OutcomeElement);
                if (DispositionElement != null) yield return new ElementValue("disposition", DispositionElement);
                if (PayeeType != null) yield return new ElementValue("payeeType", PayeeType);
                foreach (var elem in Item) { if (elem != null) yield return new ElementValue("item", elem); }
                foreach (var elem in AddItem) { if (elem != null) yield return new ElementValue("addItem", elem); }
                foreach (var elem in Error) { if (elem != null) yield return new ElementValue("error", elem); }
                if (TotalCost != null) yield return new ElementValue("totalCost", TotalCost);
                if (UnallocDeductable != null) yield return new ElementValue("unallocDeductable", UnallocDeductable);
                if (TotalBenefit != null) yield return new ElementValue("totalBenefit", TotalBenefit);
                if (PaymentAdjustment != null) yield return new ElementValue("paymentAdjustment", PaymentAdjustment);
                if (PaymentAdjustmentReason != null) yield return new ElementValue("paymentAdjustmentReason", PaymentAdjustmentReason);
                if (PaymentDateElement != null) yield return new ElementValue("paymentDate", PaymentDateElement);
                if (PaymentAmount != null) yield return new ElementValue("paymentAmount", PaymentAmount);
                if (PaymentRef != null) yield return new ElementValue("paymentRef", PaymentRef);
                if (Reserved != null) yield return new ElementValue("reserved", Reserved);
                if (Form != null) yield return new ElementValue("form", Form);
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                foreach (var elem in Coverage) { if (elem != null) yield return new ElementValue("coverage", elem); }
            }
        }
    
    }

}
