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
// Generated for FHIR v4.0.1
//
namespace Hl7.Fhir.Model.R4
{
    /// <summary>
    /// Response to a claim predetermination or preauthorization
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "ClaimResponse", IsResource=true)]
    [DataContract]
    public partial class ClaimResponse : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IClaimResponse, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ClaimResponse; } }
        [NotMapped]
        public override string TypeName { get { return "ClaimResponse"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ItemComponent")]
        [DataContract]
        public partial class ItemComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ItemComponent"; } }
            
            /// <summary>
            /// Claim item instance identifier
            /// </summary>
            [FhirElement("itemSequence", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt ItemSequenceElement
            {
                get { return _ItemSequenceElement; }
                set { _ItemSequenceElement = value; OnPropertyChanged("ItemSequenceElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _ItemSequenceElement;
            
            /// <summary>
            /// Claim item instance identifier
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? ItemSequence
            {
                get { return ItemSequenceElement != null ? ItemSequenceElement.Value : null; }
                set
                {
                    if (value == null)
                        ItemSequenceElement = null;
                    else
                        ItemSequenceElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("ItemSequence");
                }
            }
            
            /// <summary>
            /// Applicable note numbers
            /// </summary>
            [FhirElement("noteNumber", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> NoteNumberElement
            {
                get { if(_NoteNumberElement==null) _NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _NoteNumberElement; }
                set { _NoteNumberElement = value; OnPropertyChanged("NoteNumberElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _NoteNumberElement;
            
            /// <summary>
            /// Applicable note numbers
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
            [FhirElement("adjudication", Order=60)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<AdjudicationComponent> Adjudication
            {
                get { if(_Adjudication==null) _Adjudication = new List<AdjudicationComponent>(); return _Adjudication; }
                set { _Adjudication = value; OnPropertyChanged("Adjudication"); }
            }
            
            private List<AdjudicationComponent> _Adjudication;
            
            /// <summary>
            /// Adjudication for claim details
            /// </summary>
            [FhirElement("detail", Order=70)]
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
                sink.BeginDataType("ItemComponent");
                base.Serialize(sink);
                sink.Element("itemSequence", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); ItemSequenceElement?.Serialize(sink);
                sink.BeginList("noteNumber", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(NoteNumberElement);
                sink.End();
                sink.BeginList("adjudication", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true);
                foreach(var item in Adjudication)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("detail", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
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
                    case "itemSequence":
                        ItemSequenceElement = source.Get<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "noteNumber":
                        NoteNumberElement = source.GetList<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "adjudication":
                        Adjudication = source.GetList<AdjudicationComponent>();
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
                    case "itemSequence":
                        ItemSequenceElement = source.PopulateValue(ItemSequenceElement);
                        return true;
                    case "_itemSequence":
                        ItemSequenceElement = source.Populate(ItemSequenceElement);
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
                var dest = other as ItemComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ItemSequenceElement != null) dest.ItemSequenceElement = (Hl7.Fhir.Model.PositiveInt)ItemSequenceElement.DeepCopy();
                    if(NoteNumberElement != null) dest.NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(NoteNumberElement.DeepCopy());
                    if(Adjudication != null) dest.Adjudication = new List<AdjudicationComponent>(Adjudication.DeepCopy());
                    if(Detail != null) dest.Detail = new List<ItemDetailComponent>(Detail.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ItemComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ItemComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ItemSequenceElement, otherT.ItemSequenceElement)) return false;
                if( !DeepComparable.Matches(NoteNumberElement, otherT.NoteNumberElement)) return false;
                if( !DeepComparable.Matches(Adjudication, otherT.Adjudication)) return false;
                if( !DeepComparable.Matches(Detail, otherT.Detail)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ItemComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ItemSequenceElement, otherT.ItemSequenceElement)) return false;
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
                    if (ItemSequenceElement != null) yield return ItemSequenceElement;
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
                    if (ItemSequenceElement != null) yield return new ElementValue("itemSequence", ItemSequenceElement);
                    foreach (var elem in NoteNumberElement) { if (elem != null) yield return new ElementValue("noteNumber", elem); }
                    foreach (var elem in Adjudication) { if (elem != null) yield return new ElementValue("adjudication", elem); }
                    foreach (var elem in Detail) { if (elem != null) yield return new ElementValue("detail", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "AdjudicationComponent")]
        [DataContract]
        public partial class AdjudicationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "AdjudicationComponent"; } }
            
            /// <summary>
            /// Type of adjudication information
            /// </summary>
            [FhirElement("category", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Category
            {
                get { return _Category; }
                set { _Category = value; OnPropertyChanged("Category"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Category;
            
            /// <summary>
            /// Explanation of adjudication outcome
            /// </summary>
            [FhirElement("reason", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Reason
            {
                get { return _Reason; }
                set { _Reason = value; OnPropertyChanged("Reason"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Reason;
            
            /// <summary>
            /// Monetary amount
            /// </summary>
            [FhirElement("amount", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.R4.Money Amount
            {
                get { return _Amount; }
                set { _Amount = value; OnPropertyChanged("Amount"); }
            }
            
            private Hl7.Fhir.Model.R4.Money _Amount;
            
            /// <summary>
            /// Non-monetary value
            /// </summary>
            [FhirElement("value", Order=70)]
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
                sink.BeginDataType("AdjudicationComponent");
                base.Serialize(sink);
                sink.Element("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Category?.Serialize(sink);
                sink.Element("reason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Reason?.Serialize(sink);
                sink.Element("amount", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Amount?.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ValueElement?.Serialize(sink);
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
                    case "category":
                        Category = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "reason":
                        Reason = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "amount":
                        Amount = source.Get<Hl7.Fhir.Model.R4.Money>();
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
                    case "category":
                        Category = source.Populate(Category);
                        return true;
                    case "reason":
                        Reason = source.Populate(Reason);
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
                var dest = other as AdjudicationComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                    if(Reason != null) dest.Reason = (Hl7.Fhir.Model.CodeableConcept)Reason.DeepCopy();
                    if(Amount != null) dest.Amount = (Hl7.Fhir.Model.R4.Money)Amount.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirDecimal)ValueElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new AdjudicationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AdjudicationComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Category, otherT.Category)) return false;
                if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
                if( !DeepComparable.Matches(Amount, otherT.Amount)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AdjudicationComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
                if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
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
                    if (Category != null) yield return Category;
                    if (Reason != null) yield return Reason;
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
                    if (Category != null) yield return new ElementValue("category", Category);
                    if (Reason != null) yield return new ElementValue("reason", Reason);
                    if (Amount != null) yield return new ElementValue("amount", Amount);
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ItemDetailComponent")]
        [DataContract]
        public partial class ItemDetailComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IClaimResponseItemDetailComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ItemDetailComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IClaimResponseSubDetailComponent> Hl7.Fhir.Model.IClaimResponseItemDetailComponent.SubDetail { get { return SubDetail; } }
            
            /// <summary>
            /// Claim detail instance identifier
            /// </summary>
            [FhirElement("detailSequence", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt DetailSequenceElement
            {
                get { return _DetailSequenceElement; }
                set { _DetailSequenceElement = value; OnPropertyChanged("DetailSequenceElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _DetailSequenceElement;
            
            /// <summary>
            /// Claim detail instance identifier
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? DetailSequence
            {
                get { return DetailSequenceElement != null ? DetailSequenceElement.Value : null; }
                set
                {
                    if (value == null)
                        DetailSequenceElement = null;
                    else
                        DetailSequenceElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("DetailSequence");
                }
            }
            
            /// <summary>
            /// Applicable note numbers
            /// </summary>
            [FhirElement("noteNumber", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> NoteNumberElement
            {
                get { if(_NoteNumberElement==null) _NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _NoteNumberElement; }
                set { _NoteNumberElement = value; OnPropertyChanged("NoteNumberElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _NoteNumberElement;
            
            /// <summary>
            /// Applicable note numbers
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
            /// Detail level adjudication details
            /// </summary>
            [FhirElement("adjudication", Order=60)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<AdjudicationComponent> Adjudication
            {
                get { if(_Adjudication==null) _Adjudication = new List<AdjudicationComponent>(); return _Adjudication; }
                set { _Adjudication = value; OnPropertyChanged("Adjudication"); }
            }
            
            private List<AdjudicationComponent> _Adjudication;
            
            /// <summary>
            /// Adjudication for claim sub-details
            /// </summary>
            [FhirElement("subDetail", Order=70)]
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
                sink.Element("detailSequence", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); DetailSequenceElement?.Serialize(sink);
                sink.BeginList("noteNumber", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(NoteNumberElement);
                sink.End();
                sink.BeginList("adjudication", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true);
                foreach(var item in Adjudication)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("subDetail", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
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
                    case "detailSequence":
                        DetailSequenceElement = source.Get<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "noteNumber":
                        NoteNumberElement = source.GetList<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "adjudication":
                        Adjudication = source.GetList<AdjudicationComponent>();
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
                    case "detailSequence":
                        DetailSequenceElement = source.PopulateValue(DetailSequenceElement);
                        return true;
                    case "_detailSequence":
                        DetailSequenceElement = source.Populate(DetailSequenceElement);
                        return true;
                    case "noteNumber":
                    case "_noteNumber":
                        source.SetList(this, jsonPropertyName);
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
                    case "noteNumber":
                        source.PopulatePrimitiveListItemValue(NoteNumberElement, index);
                        return true;
                    case "_noteNumber":
                        source.PopulatePrimitiveListItem(NoteNumberElement, index);
                        return true;
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
                    if(DetailSequenceElement != null) dest.DetailSequenceElement = (Hl7.Fhir.Model.PositiveInt)DetailSequenceElement.DeepCopy();
                    if(NoteNumberElement != null) dest.NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(NoteNumberElement.DeepCopy());
                    if(Adjudication != null) dest.Adjudication = new List<AdjudicationComponent>(Adjudication.DeepCopy());
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
                if( !DeepComparable.Matches(DetailSequenceElement, otherT.DetailSequenceElement)) return false;
                if( !DeepComparable.Matches(NoteNumberElement, otherT.NoteNumberElement)) return false;
                if( !DeepComparable.Matches(Adjudication, otherT.Adjudication)) return false;
                if( !DeepComparable.Matches(SubDetail, otherT.SubDetail)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ItemDetailComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DetailSequenceElement, otherT.DetailSequenceElement)) return false;
                if( !DeepComparable.IsExactly(NoteNumberElement, otherT.NoteNumberElement)) return false;
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
                    if (DetailSequenceElement != null) yield return DetailSequenceElement;
                    foreach (var elem in NoteNumberElement) { if (elem != null) yield return elem; }
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
                    if (DetailSequenceElement != null) yield return new ElementValue("detailSequence", DetailSequenceElement);
                    foreach (var elem in NoteNumberElement) { if (elem != null) yield return new ElementValue("noteNumber", elem); }
                    foreach (var elem in Adjudication) { if (elem != null) yield return new ElementValue("adjudication", elem); }
                    foreach (var elem in SubDetail) { if (elem != null) yield return new ElementValue("subDetail", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "SubDetailComponent")]
        [DataContract]
        public partial class SubDetailComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IClaimResponseSubDetailComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SubDetailComponent"; } }
            
            /// <summary>
            /// Claim sub-detail instance identifier
            /// </summary>
            [FhirElement("subDetailSequence", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SubDetailSequenceElement
            {
                get { return _SubDetailSequenceElement; }
                set { _SubDetailSequenceElement = value; OnPropertyChanged("SubDetailSequenceElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SubDetailSequenceElement;
            
            /// <summary>
            /// Claim sub-detail instance identifier
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? SubDetailSequence
            {
                get { return SubDetailSequenceElement != null ? SubDetailSequenceElement.Value : null; }
                set
                {
                    if (value == null)
                        SubDetailSequenceElement = null;
                    else
                        SubDetailSequenceElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("SubDetailSequence");
                }
            }
            
            /// <summary>
            /// Applicable note numbers
            /// </summary>
            [FhirElement("noteNumber", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> NoteNumberElement
            {
                get { if(_NoteNumberElement==null) _NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _NoteNumberElement; }
                set { _NoteNumberElement = value; OnPropertyChanged("NoteNumberElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _NoteNumberElement;
            
            /// <summary>
            /// Applicable note numbers
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
            /// Subdetail level adjudication details
            /// </summary>
            [FhirElement("adjudication", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<AdjudicationComponent> Adjudication
            {
                get { if(_Adjudication==null) _Adjudication = new List<AdjudicationComponent>(); return _Adjudication; }
                set { _Adjudication = value; OnPropertyChanged("Adjudication"); }
            }
            
            private List<AdjudicationComponent> _Adjudication;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SubDetailComponent");
                base.Serialize(sink);
                sink.Element("subDetailSequence", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); SubDetailSequenceElement?.Serialize(sink);
                sink.BeginList("noteNumber", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(NoteNumberElement);
                sink.End();
                sink.BeginList("adjudication", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
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
                    case "subDetailSequence":
                        SubDetailSequenceElement = source.Get<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "noteNumber":
                        NoteNumberElement = source.GetList<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "adjudication":
                        Adjudication = source.GetList<AdjudicationComponent>();
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
                    case "subDetailSequence":
                        SubDetailSequenceElement = source.PopulateValue(SubDetailSequenceElement);
                        return true;
                    case "_subDetailSequence":
                        SubDetailSequenceElement = source.Populate(SubDetailSequenceElement);
                        return true;
                    case "noteNumber":
                    case "_noteNumber":
                        source.SetList(this, jsonPropertyName);
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
                    case "noteNumber":
                        source.PopulatePrimitiveListItemValue(NoteNumberElement, index);
                        return true;
                    case "_noteNumber":
                        source.PopulatePrimitiveListItem(NoteNumberElement, index);
                        return true;
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
                    if(SubDetailSequenceElement != null) dest.SubDetailSequenceElement = (Hl7.Fhir.Model.PositiveInt)SubDetailSequenceElement.DeepCopy();
                    if(NoteNumberElement != null) dest.NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(NoteNumberElement.DeepCopy());
                    if(Adjudication != null) dest.Adjudication = new List<AdjudicationComponent>(Adjudication.DeepCopy());
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
                if( !DeepComparable.Matches(SubDetailSequenceElement, otherT.SubDetailSequenceElement)) return false;
                if( !DeepComparable.Matches(NoteNumberElement, otherT.NoteNumberElement)) return false;
                if( !DeepComparable.Matches(Adjudication, otherT.Adjudication)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SubDetailComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SubDetailSequenceElement, otherT.SubDetailSequenceElement)) return false;
                if( !DeepComparable.IsExactly(NoteNumberElement, otherT.NoteNumberElement)) return false;
                if( !DeepComparable.IsExactly(Adjudication, otherT.Adjudication)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SubDetailSequenceElement != null) yield return SubDetailSequenceElement;
                    foreach (var elem in NoteNumberElement) { if (elem != null) yield return elem; }
                    foreach (var elem in Adjudication) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SubDetailSequenceElement != null) yield return new ElementValue("subDetailSequence", SubDetailSequenceElement);
                    foreach (var elem in NoteNumberElement) { if (elem != null) yield return new ElementValue("noteNumber", elem); }
                    foreach (var elem in Adjudication) { if (elem != null) yield return new ElementValue("adjudication", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "AddedItemComponent")]
        [DataContract]
        public partial class AddedItemComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IClaimResponseAddedItemComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "AddedItemComponent"; } }
            
            /// <summary>
            /// Item sequence number
            /// </summary>
            [FhirElement("itemSequence", Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> ItemSequenceElement
            {
                get { if(_ItemSequenceElement==null) _ItemSequenceElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _ItemSequenceElement; }
                set { _ItemSequenceElement = value; OnPropertyChanged("ItemSequenceElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _ItemSequenceElement;
            
            /// <summary>
            /// Item sequence number
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> ItemSequence
            {
                get { return ItemSequenceElement != null ? ItemSequenceElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        ItemSequenceElement = null;
                    else
                        ItemSequenceElement = new List<Hl7.Fhir.Model.PositiveInt>(value.Select(elem=>new Hl7.Fhir.Model.PositiveInt(elem)));
                    OnPropertyChanged("ItemSequence");
                }
            }
            
            /// <summary>
            /// Detail sequence number
            /// </summary>
            [FhirElement("detailSequence", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> DetailSequenceElement
            {
                get { if(_DetailSequenceElement==null) _DetailSequenceElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _DetailSequenceElement; }
                set { _DetailSequenceElement = value; OnPropertyChanged("DetailSequenceElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _DetailSequenceElement;
            
            /// <summary>
            /// Detail sequence number
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> DetailSequence
            {
                get { return DetailSequenceElement != null ? DetailSequenceElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        DetailSequenceElement = null;
                    else
                        DetailSequenceElement = new List<Hl7.Fhir.Model.PositiveInt>(value.Select(elem=>new Hl7.Fhir.Model.PositiveInt(elem)));
                    OnPropertyChanged("DetailSequence");
                }
            }
            
            /// <summary>
            /// Subdetail sequence number
            /// </summary>
            [FhirElement("subdetailSequence", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> SubdetailSequenceElement
            {
                get { if(_SubdetailSequenceElement==null) _SubdetailSequenceElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _SubdetailSequenceElement; }
                set { _SubdetailSequenceElement = value; OnPropertyChanged("SubdetailSequenceElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _SubdetailSequenceElement;
            
            /// <summary>
            /// Subdetail sequence number
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> SubdetailSequence
            {
                get { return SubdetailSequenceElement != null ? SubdetailSequenceElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        SubdetailSequenceElement = null;
                    else
                        SubdetailSequenceElement = new List<Hl7.Fhir.Model.PositiveInt>(value.Select(elem=>new Hl7.Fhir.Model.PositiveInt(elem)));
                    OnPropertyChanged("SubdetailSequence");
                }
            }
            
            /// <summary>
            /// Authorized providers
            /// </summary>
            [FhirElement("provider", Order=70)]
            [CLSCompliant(false)]
            [References("Practitioner","PractitionerRole","Organization")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Provider
            {
                get { if(_Provider==null) _Provider = new List<Hl7.Fhir.Model.ResourceReference>(); return _Provider; }
                set { _Provider = value; OnPropertyChanged("Provider"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Provider;
            
            /// <summary>
            /// Billing, service, product, or drug code
            /// </summary>
            [FhirElement("productOrService", Order=80)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept ProductOrService
            {
                get { return _ProductOrService; }
                set { _ProductOrService = value; OnPropertyChanged("ProductOrService"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _ProductOrService;
            
            /// <summary>
            /// Service/Product billing modifiers
            /// </summary>
            [FhirElement("modifier", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Modifier
            {
                get { if(_Modifier==null) _Modifier = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Modifier; }
                set { _Modifier = value; OnPropertyChanged("Modifier"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Modifier;
            
            /// <summary>
            /// Program the product or service is provided under
            /// </summary>
            [FhirElement("programCode", Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> ProgramCode
            {
                get { if(_ProgramCode==null) _ProgramCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ProgramCode; }
                set { _ProgramCode = value; OnPropertyChanged("ProgramCode"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _ProgramCode;
            
            /// <summary>
            /// Date or dates of service or product delivery
            /// </summary>
            [FhirElement("serviced", Order=110, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.Period))]
            [DataMember]
            public Hl7.Fhir.Model.Element Serviced
            {
                get { return _Serviced; }
                set { _Serviced = value; OnPropertyChanged("Serviced"); }
            }
            
            private Hl7.Fhir.Model.Element _Serviced;
            
            /// <summary>
            /// Place of service or where product was supplied
            /// </summary>
            [FhirElement("location", Order=120, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.Address),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Location
            {
                get { return _Location; }
                set { _Location = value; OnPropertyChanged("Location"); }
            }
            
            private Hl7.Fhir.Model.Element _Location;
            
            /// <summary>
            /// Count of products or services
            /// </summary>
            [FhirElement("quantity", Order=130)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _Quantity;
            
            /// <summary>
            /// Fee, charge or cost per item
            /// </summary>
            [FhirElement("unitPrice", Order=140)]
            [DataMember]
            public Hl7.Fhir.Model.R4.Money UnitPrice
            {
                get { return _UnitPrice; }
                set { _UnitPrice = value; OnPropertyChanged("UnitPrice"); }
            }
            
            private Hl7.Fhir.Model.R4.Money _UnitPrice;
            
            /// <summary>
            /// Price scaling factor
            /// </summary>
            [FhirElement("factor", Order=150)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal FactorElement
            {
                get { return _FactorElement; }
                set { _FactorElement = value; OnPropertyChanged("FactorElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _FactorElement;
            
            /// <summary>
            /// Price scaling factor
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Factor
            {
                get { return FactorElement != null ? FactorElement.Value : null; }
                set
                {
                    if (value == null)
                        FactorElement = null;
                    else
                        FactorElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Factor");
                }
            }
            
            /// <summary>
            /// Total item cost
            /// </summary>
            [FhirElement("net", Order=160)]
            [DataMember]
            public Hl7.Fhir.Model.R4.Money Net
            {
                get { return _Net; }
                set { _Net = value; OnPropertyChanged("Net"); }
            }
            
            private Hl7.Fhir.Model.R4.Money _Net;
            
            /// <summary>
            /// Anatomical location
            /// </summary>
            [FhirElement("bodySite", Order=170)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept BodySite
            {
                get { return _BodySite; }
                set { _BodySite = value; OnPropertyChanged("BodySite"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _BodySite;
            
            /// <summary>
            /// Anatomical sub-location
            /// </summary>
            [FhirElement("subSite", Order=180)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> SubSite
            {
                get { if(_SubSite==null) _SubSite = new List<Hl7.Fhir.Model.CodeableConcept>(); return _SubSite; }
                set { _SubSite = value; OnPropertyChanged("SubSite"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _SubSite;
            
            /// <summary>
            /// Applicable note numbers
            /// </summary>
            [FhirElement("noteNumber", Order=190)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> NoteNumberElement
            {
                get { if(_NoteNumberElement==null) _NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _NoteNumberElement; }
                set { _NoteNumberElement = value; OnPropertyChanged("NoteNumberElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _NoteNumberElement;
            
            /// <summary>
            /// Applicable note numbers
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
            /// Added items adjudication
            /// </summary>
            [FhirElement("adjudication", Order=200)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<AdjudicationComponent> Adjudication
            {
                get { if(_Adjudication==null) _Adjudication = new List<AdjudicationComponent>(); return _Adjudication; }
                set { _Adjudication = value; OnPropertyChanged("Adjudication"); }
            }
            
            private List<AdjudicationComponent> _Adjudication;
            
            /// <summary>
            /// Insurer added line details
            /// </summary>
            [FhirElement("detail", Order=210)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<AddedItemDetailComponent> Detail
            {
                get { if(_Detail==null) _Detail = new List<AddedItemDetailComponent>(); return _Detail; }
                set { _Detail = value; OnPropertyChanged("Detail"); }
            }
            
            private List<AddedItemDetailComponent> _Detail;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("AddedItemComponent");
                base.Serialize(sink);
                sink.BeginList("itemSequence", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(ItemSequenceElement);
                sink.End();
                sink.BeginList("detailSequence", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(DetailSequenceElement);
                sink.End();
                sink.BeginList("subdetailSequence", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(SubdetailSequenceElement);
                sink.End();
                sink.BeginList("provider", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Provider)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("productOrService", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); ProductOrService?.Serialize(sink);
                sink.BeginList("modifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Modifier)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("programCode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in ProgramCode)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("serviced", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Serviced?.Serialize(sink);
                sink.Element("location", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Location?.Serialize(sink);
                sink.Element("quantity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Quantity?.Serialize(sink);
                sink.Element("unitPrice", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); UnitPrice?.Serialize(sink);
                sink.Element("factor", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); FactorElement?.Serialize(sink);
                sink.Element("net", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Net?.Serialize(sink);
                sink.Element("bodySite", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); BodySite?.Serialize(sink);
                sink.BeginList("subSite", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in SubSite)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("noteNumber", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(NoteNumberElement);
                sink.End();
                sink.BeginList("adjudication", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true);
                foreach(var item in Adjudication)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("detail", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
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
                    case "itemSequence":
                        ItemSequenceElement = source.GetList<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "detailSequence":
                        DetailSequenceElement = source.GetList<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "subdetailSequence":
                        SubdetailSequenceElement = source.GetList<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "provider":
                        Provider = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "productOrService":
                        ProductOrService = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "modifier":
                        Modifier = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "programCode":
                        ProgramCode = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "servicedDate":
                        source.CheckDuplicates<Hl7.Fhir.Model.Date>(Serviced, "serviced");
                        Serviced = source.Get<Hl7.Fhir.Model.Date>();
                        return true;
                    case "servicedPeriod":
                        source.CheckDuplicates<Hl7.Fhir.Model.Period>(Serviced, "serviced");
                        Serviced = source.Get<Hl7.Fhir.Model.Period>();
                        return true;
                    case "locationCodeableConcept":
                        source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Location, "location");
                        Location = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "locationAddress":
                        source.CheckDuplicates<Hl7.Fhir.Model.Address>(Location, "location");
                        Location = source.Get<Hl7.Fhir.Model.Address>();
                        return true;
                    case "locationReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Location, "location");
                        Location = source.Get<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "quantity":
                        Quantity = source.Get<Hl7.Fhir.Model.SimpleQuantity>();
                        return true;
                    case "unitPrice":
                        UnitPrice = source.Get<Hl7.Fhir.Model.R4.Money>();
                        return true;
                    case "factor":
                        FactorElement = source.Get<Hl7.Fhir.Model.FhirDecimal>();
                        return true;
                    case "net":
                        Net = source.Get<Hl7.Fhir.Model.R4.Money>();
                        return true;
                    case "bodySite":
                        BodySite = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "subSite":
                        SubSite = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "noteNumber":
                        NoteNumberElement = source.GetList<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "adjudication":
                        Adjudication = source.GetList<AdjudicationComponent>();
                        return true;
                    case "detail":
                        Detail = source.GetList<AddedItemDetailComponent>();
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
                    case "itemSequence":
                    case "_itemSequence":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "detailSequence":
                    case "_detailSequence":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "subdetailSequence":
                    case "_subdetailSequence":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "provider":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "productOrService":
                        ProductOrService = source.Populate(ProductOrService);
                        return true;
                    case "modifier":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "programCode":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "servicedDate":
                        source.CheckDuplicates<Hl7.Fhir.Model.Date>(Serviced, "serviced");
                        Serviced = source.PopulateValue(Serviced as Hl7.Fhir.Model.Date);
                        return true;
                    case "_servicedDate":
                        source.CheckDuplicates<Hl7.Fhir.Model.Date>(Serviced, "serviced");
                        Serviced = source.Populate(Serviced as Hl7.Fhir.Model.Date);
                        return true;
                    case "servicedPeriod":
                        source.CheckDuplicates<Hl7.Fhir.Model.Period>(Serviced, "serviced");
                        Serviced = source.Populate(Serviced as Hl7.Fhir.Model.Period);
                        return true;
                    case "locationCodeableConcept":
                        source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Location, "location");
                        Location = source.Populate(Location as Hl7.Fhir.Model.CodeableConcept);
                        return true;
                    case "locationAddress":
                        source.CheckDuplicates<Hl7.Fhir.Model.Address>(Location, "location");
                        Location = source.Populate(Location as Hl7.Fhir.Model.Address);
                        return true;
                    case "locationReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Location, "location");
                        Location = source.Populate(Location as Hl7.Fhir.Model.ResourceReference);
                        return true;
                    case "quantity":
                        Quantity = source.Populate(Quantity);
                        return true;
                    case "unitPrice":
                        UnitPrice = source.Populate(UnitPrice);
                        return true;
                    case "factor":
                        FactorElement = source.PopulateValue(FactorElement);
                        return true;
                    case "_factor":
                        FactorElement = source.Populate(FactorElement);
                        return true;
                    case "net":
                        Net = source.Populate(Net);
                        return true;
                    case "bodySite":
                        BodySite = source.Populate(BodySite);
                        return true;
                    case "subSite":
                        source.SetList(this, jsonPropertyName);
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
                    case "itemSequence":
                        source.PopulatePrimitiveListItemValue(ItemSequenceElement, index);
                        return true;
                    case "_itemSequence":
                        source.PopulatePrimitiveListItem(ItemSequenceElement, index);
                        return true;
                    case "detailSequence":
                        source.PopulatePrimitiveListItemValue(DetailSequenceElement, index);
                        return true;
                    case "_detailSequence":
                        source.PopulatePrimitiveListItem(DetailSequenceElement, index);
                        return true;
                    case "subdetailSequence":
                        source.PopulatePrimitiveListItemValue(SubdetailSequenceElement, index);
                        return true;
                    case "_subdetailSequence":
                        source.PopulatePrimitiveListItem(SubdetailSequenceElement, index);
                        return true;
                    case "provider":
                        source.PopulateListItem(Provider, index);
                        return true;
                    case "modifier":
                        source.PopulateListItem(Modifier, index);
                        return true;
                    case "programCode":
                        source.PopulateListItem(ProgramCode, index);
                        return true;
                    case "subSite":
                        source.PopulateListItem(SubSite, index);
                        return true;
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
                var dest = other as AddedItemComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ItemSequenceElement != null) dest.ItemSequenceElement = new List<Hl7.Fhir.Model.PositiveInt>(ItemSequenceElement.DeepCopy());
                    if(DetailSequenceElement != null) dest.DetailSequenceElement = new List<Hl7.Fhir.Model.PositiveInt>(DetailSequenceElement.DeepCopy());
                    if(SubdetailSequenceElement != null) dest.SubdetailSequenceElement = new List<Hl7.Fhir.Model.PositiveInt>(SubdetailSequenceElement.DeepCopy());
                    if(Provider != null) dest.Provider = new List<Hl7.Fhir.Model.ResourceReference>(Provider.DeepCopy());
                    if(ProductOrService != null) dest.ProductOrService = (Hl7.Fhir.Model.CodeableConcept)ProductOrService.DeepCopy();
                    if(Modifier != null) dest.Modifier = new List<Hl7.Fhir.Model.CodeableConcept>(Modifier.DeepCopy());
                    if(ProgramCode != null) dest.ProgramCode = new List<Hl7.Fhir.Model.CodeableConcept>(ProgramCode.DeepCopy());
                    if(Serviced != null) dest.Serviced = (Hl7.Fhir.Model.Element)Serviced.DeepCopy();
                    if(Location != null) dest.Location = (Hl7.Fhir.Model.Element)Location.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                    if(UnitPrice != null) dest.UnitPrice = (Hl7.Fhir.Model.R4.Money)UnitPrice.DeepCopy();
                    if(FactorElement != null) dest.FactorElement = (Hl7.Fhir.Model.FhirDecimal)FactorElement.DeepCopy();
                    if(Net != null) dest.Net = (Hl7.Fhir.Model.R4.Money)Net.DeepCopy();
                    if(BodySite != null) dest.BodySite = (Hl7.Fhir.Model.CodeableConcept)BodySite.DeepCopy();
                    if(SubSite != null) dest.SubSite = new List<Hl7.Fhir.Model.CodeableConcept>(SubSite.DeepCopy());
                    if(NoteNumberElement != null) dest.NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(NoteNumberElement.DeepCopy());
                    if(Adjudication != null) dest.Adjudication = new List<AdjudicationComponent>(Adjudication.DeepCopy());
                    if(Detail != null) dest.Detail = new List<AddedItemDetailComponent>(Detail.DeepCopy());
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
                if( !DeepComparable.Matches(ItemSequenceElement, otherT.ItemSequenceElement)) return false;
                if( !DeepComparable.Matches(DetailSequenceElement, otherT.DetailSequenceElement)) return false;
                if( !DeepComparable.Matches(SubdetailSequenceElement, otherT.SubdetailSequenceElement)) return false;
                if( !DeepComparable.Matches(Provider, otherT.Provider)) return false;
                if( !DeepComparable.Matches(ProductOrService, otherT.ProductOrService)) return false;
                if( !DeepComparable.Matches(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.Matches(ProgramCode, otherT.ProgramCode)) return false;
                if( !DeepComparable.Matches(Serviced, otherT.Serviced)) return false;
                if( !DeepComparable.Matches(Location, otherT.Location)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.Matches(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.Matches(Net, otherT.Net)) return false;
                if( !DeepComparable.Matches(BodySite, otherT.BodySite)) return false;
                if( !DeepComparable.Matches(SubSite, otherT.SubSite)) return false;
                if( !DeepComparable.Matches(NoteNumberElement, otherT.NoteNumberElement)) return false;
                if( !DeepComparable.Matches(Adjudication, otherT.Adjudication)) return false;
                if( !DeepComparable.Matches(Detail, otherT.Detail)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AddedItemComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ItemSequenceElement, otherT.ItemSequenceElement)) return false;
                if( !DeepComparable.IsExactly(DetailSequenceElement, otherT.DetailSequenceElement)) return false;
                if( !DeepComparable.IsExactly(SubdetailSequenceElement, otherT.SubdetailSequenceElement)) return false;
                if( !DeepComparable.IsExactly(Provider, otherT.Provider)) return false;
                if( !DeepComparable.IsExactly(ProductOrService, otherT.ProductOrService)) return false;
                if( !DeepComparable.IsExactly(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.IsExactly(ProgramCode, otherT.ProgramCode)) return false;
                if( !DeepComparable.IsExactly(Serviced, otherT.Serviced)) return false;
                if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.IsExactly(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.IsExactly(Net, otherT.Net)) return false;
                if( !DeepComparable.IsExactly(BodySite, otherT.BodySite)) return false;
                if( !DeepComparable.IsExactly(SubSite, otherT.SubSite)) return false;
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
                    foreach (var elem in ItemSequenceElement) { if (elem != null) yield return elem; }
                    foreach (var elem in DetailSequenceElement) { if (elem != null) yield return elem; }
                    foreach (var elem in SubdetailSequenceElement) { if (elem != null) yield return elem; }
                    foreach (var elem in Provider) { if (elem != null) yield return elem; }
                    if (ProductOrService != null) yield return ProductOrService;
                    foreach (var elem in Modifier) { if (elem != null) yield return elem; }
                    foreach (var elem in ProgramCode) { if (elem != null) yield return elem; }
                    if (Serviced != null) yield return Serviced;
                    if (Location != null) yield return Location;
                    if (Quantity != null) yield return Quantity;
                    if (UnitPrice != null) yield return UnitPrice;
                    if (FactorElement != null) yield return FactorElement;
                    if (Net != null) yield return Net;
                    if (BodySite != null) yield return BodySite;
                    foreach (var elem in SubSite) { if (elem != null) yield return elem; }
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
                    foreach (var elem in ItemSequenceElement) { if (elem != null) yield return new ElementValue("itemSequence", elem); }
                    foreach (var elem in DetailSequenceElement) { if (elem != null) yield return new ElementValue("detailSequence", elem); }
                    foreach (var elem in SubdetailSequenceElement) { if (elem != null) yield return new ElementValue("subdetailSequence", elem); }
                    foreach (var elem in Provider) { if (elem != null) yield return new ElementValue("provider", elem); }
                    if (ProductOrService != null) yield return new ElementValue("productOrService", ProductOrService);
                    foreach (var elem in Modifier) { if (elem != null) yield return new ElementValue("modifier", elem); }
                    foreach (var elem in ProgramCode) { if (elem != null) yield return new ElementValue("programCode", elem); }
                    if (Serviced != null) yield return new ElementValue("serviced", Serviced);
                    if (Location != null) yield return new ElementValue("location", Location);
                    if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                    if (UnitPrice != null) yield return new ElementValue("unitPrice", UnitPrice);
                    if (FactorElement != null) yield return new ElementValue("factor", FactorElement);
                    if (Net != null) yield return new ElementValue("net", Net);
                    if (BodySite != null) yield return new ElementValue("bodySite", BodySite);
                    foreach (var elem in SubSite) { if (elem != null) yield return new ElementValue("subSite", elem); }
                    foreach (var elem in NoteNumberElement) { if (elem != null) yield return new ElementValue("noteNumber", elem); }
                    foreach (var elem in Adjudication) { if (elem != null) yield return new ElementValue("adjudication", elem); }
                    foreach (var elem in Detail) { if (elem != null) yield return new ElementValue("detail", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "AddedItemDetailComponent")]
        [DataContract]
        public partial class AddedItemDetailComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "AddedItemDetailComponent"; } }
            
            /// <summary>
            /// Billing, service, product, or drug code
            /// </summary>
            [FhirElement("productOrService", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept ProductOrService
            {
                get { return _ProductOrService; }
                set { _ProductOrService = value; OnPropertyChanged("ProductOrService"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _ProductOrService;
            
            /// <summary>
            /// Service/Product billing modifiers
            /// </summary>
            [FhirElement("modifier", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Modifier
            {
                get { if(_Modifier==null) _Modifier = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Modifier; }
                set { _Modifier = value; OnPropertyChanged("Modifier"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Modifier;
            
            /// <summary>
            /// Count of products or services
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
            /// Fee, charge or cost per item
            /// </summary>
            [FhirElement("unitPrice", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.R4.Money UnitPrice
            {
                get { return _UnitPrice; }
                set { _UnitPrice = value; OnPropertyChanged("UnitPrice"); }
            }
            
            private Hl7.Fhir.Model.R4.Money _UnitPrice;
            
            /// <summary>
            /// Price scaling factor
            /// </summary>
            [FhirElement("factor", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal FactorElement
            {
                get { return _FactorElement; }
                set { _FactorElement = value; OnPropertyChanged("FactorElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _FactorElement;
            
            /// <summary>
            /// Price scaling factor
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Factor
            {
                get { return FactorElement != null ? FactorElement.Value : null; }
                set
                {
                    if (value == null)
                        FactorElement = null;
                    else
                        FactorElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Factor");
                }
            }
            
            /// <summary>
            /// Total item cost
            /// </summary>
            [FhirElement("net", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.R4.Money Net
            {
                get { return _Net; }
                set { _Net = value; OnPropertyChanged("Net"); }
            }
            
            private Hl7.Fhir.Model.R4.Money _Net;
            
            /// <summary>
            /// Applicable note numbers
            /// </summary>
            [FhirElement("noteNumber", Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> NoteNumberElement
            {
                get { if(_NoteNumberElement==null) _NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _NoteNumberElement; }
                set { _NoteNumberElement = value; OnPropertyChanged("NoteNumberElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _NoteNumberElement;
            
            /// <summary>
            /// Applicable note numbers
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
            /// Added items detail adjudication
            /// </summary>
            [FhirElement("adjudication", Order=110)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<AdjudicationComponent> Adjudication
            {
                get { if(_Adjudication==null) _Adjudication = new List<AdjudicationComponent>(); return _Adjudication; }
                set { _Adjudication = value; OnPropertyChanged("Adjudication"); }
            }
            
            private List<AdjudicationComponent> _Adjudication;
            
            /// <summary>
            /// Insurer added line items
            /// </summary>
            [FhirElement("subDetail", Order=120)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<AddedItemSubDetailComponent> SubDetail
            {
                get { if(_SubDetail==null) _SubDetail = new List<AddedItemSubDetailComponent>(); return _SubDetail; }
                set { _SubDetail = value; OnPropertyChanged("SubDetail"); }
            }
            
            private List<AddedItemSubDetailComponent> _SubDetail;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("AddedItemDetailComponent");
                base.Serialize(sink);
                sink.Element("productOrService", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); ProductOrService?.Serialize(sink);
                sink.BeginList("modifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Modifier)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("quantity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Quantity?.Serialize(sink);
                sink.Element("unitPrice", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); UnitPrice?.Serialize(sink);
                sink.Element("factor", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); FactorElement?.Serialize(sink);
                sink.Element("net", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Net?.Serialize(sink);
                sink.BeginList("noteNumber", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(NoteNumberElement);
                sink.End();
                sink.BeginList("adjudication", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true);
                foreach(var item in Adjudication)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("subDetail", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
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
                    case "productOrService":
                        ProductOrService = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "modifier":
                        Modifier = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "quantity":
                        Quantity = source.Get<Hl7.Fhir.Model.SimpleQuantity>();
                        return true;
                    case "unitPrice":
                        UnitPrice = source.Get<Hl7.Fhir.Model.R4.Money>();
                        return true;
                    case "factor":
                        FactorElement = source.Get<Hl7.Fhir.Model.FhirDecimal>();
                        return true;
                    case "net":
                        Net = source.Get<Hl7.Fhir.Model.R4.Money>();
                        return true;
                    case "noteNumber":
                        NoteNumberElement = source.GetList<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "adjudication":
                        Adjudication = source.GetList<AdjudicationComponent>();
                        return true;
                    case "subDetail":
                        SubDetail = source.GetList<AddedItemSubDetailComponent>();
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
                    case "productOrService":
                        ProductOrService = source.Populate(ProductOrService);
                        return true;
                    case "modifier":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "quantity":
                        Quantity = source.Populate(Quantity);
                        return true;
                    case "unitPrice":
                        UnitPrice = source.Populate(UnitPrice);
                        return true;
                    case "factor":
                        FactorElement = source.PopulateValue(FactorElement);
                        return true;
                    case "_factor":
                        FactorElement = source.Populate(FactorElement);
                        return true;
                    case "net":
                        Net = source.Populate(Net);
                        return true;
                    case "noteNumber":
                    case "_noteNumber":
                        source.SetList(this, jsonPropertyName);
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
                    case "modifier":
                        source.PopulateListItem(Modifier, index);
                        return true;
                    case "noteNumber":
                        source.PopulatePrimitiveListItemValue(NoteNumberElement, index);
                        return true;
                    case "_noteNumber":
                        source.PopulatePrimitiveListItem(NoteNumberElement, index);
                        return true;
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
                var dest = other as AddedItemDetailComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ProductOrService != null) dest.ProductOrService = (Hl7.Fhir.Model.CodeableConcept)ProductOrService.DeepCopy();
                    if(Modifier != null) dest.Modifier = new List<Hl7.Fhir.Model.CodeableConcept>(Modifier.DeepCopy());
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                    if(UnitPrice != null) dest.UnitPrice = (Hl7.Fhir.Model.R4.Money)UnitPrice.DeepCopy();
                    if(FactorElement != null) dest.FactorElement = (Hl7.Fhir.Model.FhirDecimal)FactorElement.DeepCopy();
                    if(Net != null) dest.Net = (Hl7.Fhir.Model.R4.Money)Net.DeepCopy();
                    if(NoteNumberElement != null) dest.NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(NoteNumberElement.DeepCopy());
                    if(Adjudication != null) dest.Adjudication = new List<AdjudicationComponent>(Adjudication.DeepCopy());
                    if(SubDetail != null) dest.SubDetail = new List<AddedItemSubDetailComponent>(SubDetail.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new AddedItemDetailComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AddedItemDetailComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ProductOrService, otherT.ProductOrService)) return false;
                if( !DeepComparable.Matches(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.Matches(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.Matches(Net, otherT.Net)) return false;
                if( !DeepComparable.Matches(NoteNumberElement, otherT.NoteNumberElement)) return false;
                if( !DeepComparable.Matches(Adjudication, otherT.Adjudication)) return false;
                if( !DeepComparable.Matches(SubDetail, otherT.SubDetail)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AddedItemDetailComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ProductOrService, otherT.ProductOrService)) return false;
                if( !DeepComparable.IsExactly(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.IsExactly(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.IsExactly(Net, otherT.Net)) return false;
                if( !DeepComparable.IsExactly(NoteNumberElement, otherT.NoteNumberElement)) return false;
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
                    if (ProductOrService != null) yield return ProductOrService;
                    foreach (var elem in Modifier) { if (elem != null) yield return elem; }
                    if (Quantity != null) yield return Quantity;
                    if (UnitPrice != null) yield return UnitPrice;
                    if (FactorElement != null) yield return FactorElement;
                    if (Net != null) yield return Net;
                    foreach (var elem in NoteNumberElement) { if (elem != null) yield return elem; }
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
                    if (ProductOrService != null) yield return new ElementValue("productOrService", ProductOrService);
                    foreach (var elem in Modifier) { if (elem != null) yield return new ElementValue("modifier", elem); }
                    if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                    if (UnitPrice != null) yield return new ElementValue("unitPrice", UnitPrice);
                    if (FactorElement != null) yield return new ElementValue("factor", FactorElement);
                    if (Net != null) yield return new ElementValue("net", Net);
                    foreach (var elem in NoteNumberElement) { if (elem != null) yield return new ElementValue("noteNumber", elem); }
                    foreach (var elem in Adjudication) { if (elem != null) yield return new ElementValue("adjudication", elem); }
                    foreach (var elem in SubDetail) { if (elem != null) yield return new ElementValue("subDetail", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "AddedItemSubDetailComponent")]
        [DataContract]
        public partial class AddedItemSubDetailComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "AddedItemSubDetailComponent"; } }
            
            /// <summary>
            /// Billing, service, product, or drug code
            /// </summary>
            [FhirElement("productOrService", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept ProductOrService
            {
                get { return _ProductOrService; }
                set { _ProductOrService = value; OnPropertyChanged("ProductOrService"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _ProductOrService;
            
            /// <summary>
            /// Service/Product billing modifiers
            /// </summary>
            [FhirElement("modifier", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Modifier
            {
                get { if(_Modifier==null) _Modifier = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Modifier; }
                set { _Modifier = value; OnPropertyChanged("Modifier"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Modifier;
            
            /// <summary>
            /// Count of products or services
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
            /// Fee, charge or cost per item
            /// </summary>
            [FhirElement("unitPrice", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.R4.Money UnitPrice
            {
                get { return _UnitPrice; }
                set { _UnitPrice = value; OnPropertyChanged("UnitPrice"); }
            }
            
            private Hl7.Fhir.Model.R4.Money _UnitPrice;
            
            /// <summary>
            /// Price scaling factor
            /// </summary>
            [FhirElement("factor", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal FactorElement
            {
                get { return _FactorElement; }
                set { _FactorElement = value; OnPropertyChanged("FactorElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _FactorElement;
            
            /// <summary>
            /// Price scaling factor
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Factor
            {
                get { return FactorElement != null ? FactorElement.Value : null; }
                set
                {
                    if (value == null)
                        FactorElement = null;
                    else
                        FactorElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Factor");
                }
            }
            
            /// <summary>
            /// Total item cost
            /// </summary>
            [FhirElement("net", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.R4.Money Net
            {
                get { return _Net; }
                set { _Net = value; OnPropertyChanged("Net"); }
            }
            
            private Hl7.Fhir.Model.R4.Money _Net;
            
            /// <summary>
            /// Applicable note numbers
            /// </summary>
            [FhirElement("noteNumber", Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> NoteNumberElement
            {
                get { if(_NoteNumberElement==null) _NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _NoteNumberElement; }
                set { _NoteNumberElement = value; OnPropertyChanged("NoteNumberElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _NoteNumberElement;
            
            /// <summary>
            /// Applicable note numbers
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
            /// Added items detail adjudication
            /// </summary>
            [FhirElement("adjudication", Order=110)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<AdjudicationComponent> Adjudication
            {
                get { if(_Adjudication==null) _Adjudication = new List<AdjudicationComponent>(); return _Adjudication; }
                set { _Adjudication = value; OnPropertyChanged("Adjudication"); }
            }
            
            private List<AdjudicationComponent> _Adjudication;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("AddedItemSubDetailComponent");
                base.Serialize(sink);
                sink.Element("productOrService", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); ProductOrService?.Serialize(sink);
                sink.BeginList("modifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Modifier)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("quantity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Quantity?.Serialize(sink);
                sink.Element("unitPrice", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); UnitPrice?.Serialize(sink);
                sink.Element("factor", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); FactorElement?.Serialize(sink);
                sink.Element("net", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Net?.Serialize(sink);
                sink.BeginList("noteNumber", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                sink.Serialize(NoteNumberElement);
                sink.End();
                sink.BeginList("adjudication", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true);
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
                    case "productOrService":
                        ProductOrService = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "modifier":
                        Modifier = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "quantity":
                        Quantity = source.Get<Hl7.Fhir.Model.SimpleQuantity>();
                        return true;
                    case "unitPrice":
                        UnitPrice = source.Get<Hl7.Fhir.Model.R4.Money>();
                        return true;
                    case "factor":
                        FactorElement = source.Get<Hl7.Fhir.Model.FhirDecimal>();
                        return true;
                    case "net":
                        Net = source.Get<Hl7.Fhir.Model.R4.Money>();
                        return true;
                    case "noteNumber":
                        NoteNumberElement = source.GetList<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "adjudication":
                        Adjudication = source.GetList<AdjudicationComponent>();
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
                    case "productOrService":
                        ProductOrService = source.Populate(ProductOrService);
                        return true;
                    case "modifier":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "quantity":
                        Quantity = source.Populate(Quantity);
                        return true;
                    case "unitPrice":
                        UnitPrice = source.Populate(UnitPrice);
                        return true;
                    case "factor":
                        FactorElement = source.PopulateValue(FactorElement);
                        return true;
                    case "_factor":
                        FactorElement = source.Populate(FactorElement);
                        return true;
                    case "net":
                        Net = source.Populate(Net);
                        return true;
                    case "noteNumber":
                    case "_noteNumber":
                        source.SetList(this, jsonPropertyName);
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
                    case "modifier":
                        source.PopulateListItem(Modifier, index);
                        return true;
                    case "noteNumber":
                        source.PopulatePrimitiveListItemValue(NoteNumberElement, index);
                        return true;
                    case "_noteNumber":
                        source.PopulatePrimitiveListItem(NoteNumberElement, index);
                        return true;
                    case "adjudication":
                        source.PopulateListItem(Adjudication, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AddedItemSubDetailComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ProductOrService != null) dest.ProductOrService = (Hl7.Fhir.Model.CodeableConcept)ProductOrService.DeepCopy();
                    if(Modifier != null) dest.Modifier = new List<Hl7.Fhir.Model.CodeableConcept>(Modifier.DeepCopy());
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                    if(UnitPrice != null) dest.UnitPrice = (Hl7.Fhir.Model.R4.Money)UnitPrice.DeepCopy();
                    if(FactorElement != null) dest.FactorElement = (Hl7.Fhir.Model.FhirDecimal)FactorElement.DeepCopy();
                    if(Net != null) dest.Net = (Hl7.Fhir.Model.R4.Money)Net.DeepCopy();
                    if(NoteNumberElement != null) dest.NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(NoteNumberElement.DeepCopy());
                    if(Adjudication != null) dest.Adjudication = new List<AdjudicationComponent>(Adjudication.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new AddedItemSubDetailComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AddedItemSubDetailComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ProductOrService, otherT.ProductOrService)) return false;
                if( !DeepComparable.Matches(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.Matches(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.Matches(Net, otherT.Net)) return false;
                if( !DeepComparable.Matches(NoteNumberElement, otherT.NoteNumberElement)) return false;
                if( !DeepComparable.Matches(Adjudication, otherT.Adjudication)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AddedItemSubDetailComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ProductOrService, otherT.ProductOrService)) return false;
                if( !DeepComparable.IsExactly(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(UnitPrice, otherT.UnitPrice)) return false;
                if( !DeepComparable.IsExactly(FactorElement, otherT.FactorElement)) return false;
                if( !DeepComparable.IsExactly(Net, otherT.Net)) return false;
                if( !DeepComparable.IsExactly(NoteNumberElement, otherT.NoteNumberElement)) return false;
                if( !DeepComparable.IsExactly(Adjudication, otherT.Adjudication)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (ProductOrService != null) yield return ProductOrService;
                    foreach (var elem in Modifier) { if (elem != null) yield return elem; }
                    if (Quantity != null) yield return Quantity;
                    if (UnitPrice != null) yield return UnitPrice;
                    if (FactorElement != null) yield return FactorElement;
                    if (Net != null) yield return Net;
                    foreach (var elem in NoteNumberElement) { if (elem != null) yield return elem; }
                    foreach (var elem in Adjudication) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (ProductOrService != null) yield return new ElementValue("productOrService", ProductOrService);
                    foreach (var elem in Modifier) { if (elem != null) yield return new ElementValue("modifier", elem); }
                    if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                    if (UnitPrice != null) yield return new ElementValue("unitPrice", UnitPrice);
                    if (FactorElement != null) yield return new ElementValue("factor", FactorElement);
                    if (Net != null) yield return new ElementValue("net", Net);
                    foreach (var elem in NoteNumberElement) { if (elem != null) yield return new ElementValue("noteNumber", elem); }
                    foreach (var elem in Adjudication) { if (elem != null) yield return new ElementValue("adjudication", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "TotalComponent")]
        [DataContract]
        public partial class TotalComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "TotalComponent"; } }
            
            /// <summary>
            /// Type of adjudication information
            /// </summary>
            [FhirElement("category", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Category
            {
                get { return _Category; }
                set { _Category = value; OnPropertyChanged("Category"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Category;
            
            /// <summary>
            /// Financial total for the category
            /// </summary>
            [FhirElement("amount", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.R4.Money Amount
            {
                get { return _Amount; }
                set { _Amount = value; OnPropertyChanged("Amount"); }
            }
            
            private Hl7.Fhir.Model.R4.Money _Amount;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("TotalComponent");
                base.Serialize(sink);
                sink.Element("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Category?.Serialize(sink);
                sink.Element("amount", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Amount?.Serialize(sink);
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
                    case "category":
                        Category = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "amount":
                        Amount = source.Get<Hl7.Fhir.Model.R4.Money>();
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
                    case "category":
                        Category = source.Populate(Category);
                        return true;
                    case "amount":
                        Amount = source.Populate(Amount);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TotalComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                    if(Amount != null) dest.Amount = (Hl7.Fhir.Model.R4.Money)Amount.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new TotalComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TotalComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Category, otherT.Category)) return false;
                if( !DeepComparable.Matches(Amount, otherT.Amount)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TotalComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
                if( !DeepComparable.IsExactly(Amount, otherT.Amount)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Category != null) yield return Category;
                    if (Amount != null) yield return Amount;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Category != null) yield return new ElementValue("category", Category);
                    if (Amount != null) yield return new ElementValue("amount", Amount);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "PaymentComponent")]
        [DataContract]
        public partial class PaymentComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "PaymentComponent"; } }
            
            /// <summary>
            /// Partial or complete payment
            /// </summary>
            [FhirElement("type", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Payment adjustment for non-claim issues
            /// </summary>
            [FhirElement("adjustment", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.R4.Money Adjustment
            {
                get { return _Adjustment; }
                set { _Adjustment = value; OnPropertyChanged("Adjustment"); }
            }
            
            private Hl7.Fhir.Model.R4.Money _Adjustment;
            
            /// <summary>
            /// Explanation for the adjustment
            /// </summary>
            [FhirElement("adjustmentReason", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept AdjustmentReason
            {
                get { return _AdjustmentReason; }
                set { _AdjustmentReason = value; OnPropertyChanged("AdjustmentReason"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _AdjustmentReason;
            
            /// <summary>
            /// Expected date of payment
            /// </summary>
            [FhirElement("date", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Date DateElement
            {
                get { return _DateElement; }
                set { _DateElement = value; OnPropertyChanged("DateElement"); }
            }
            
            private Hl7.Fhir.Model.Date _DateElement;
            
            /// <summary>
            /// Expected date of payment
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Date
            {
                get { return DateElement != null ? DateElement.Value : null; }
                set
                {
                    if (value == null)
                        DateElement = null;
                    else
                        DateElement = new Hl7.Fhir.Model.Date(value);
                    OnPropertyChanged("Date");
                }
            }
            
            /// <summary>
            /// Payable amount after adjustment
            /// </summary>
            [FhirElement("amount", Order=80)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.R4.Money Amount
            {
                get { return _Amount; }
                set { _Amount = value; OnPropertyChanged("Amount"); }
            }
            
            private Hl7.Fhir.Model.R4.Money _Amount;
            
            /// <summary>
            /// Business identifier for the payment
            /// </summary>
            [FhirElement("identifier", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("PaymentComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Type?.Serialize(sink);
                sink.Element("adjustment", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Adjustment?.Serialize(sink);
                sink.Element("adjustmentReason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); AdjustmentReason?.Serialize(sink);
                sink.Element("date", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DateElement?.Serialize(sink);
                sink.Element("amount", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Amount?.Serialize(sink);
                sink.Element("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Identifier?.Serialize(sink);
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
                    case "type":
                        Type = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "adjustment":
                        Adjustment = source.Get<Hl7.Fhir.Model.R4.Money>();
                        return true;
                    case "adjustmentReason":
                        AdjustmentReason = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "date":
                        DateElement = source.Get<Hl7.Fhir.Model.Date>();
                        return true;
                    case "amount":
                        Amount = source.Get<Hl7.Fhir.Model.R4.Money>();
                        return true;
                    case "identifier":
                        Identifier = source.Get<Hl7.Fhir.Model.Identifier>();
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
                    case "type":
                        Type = source.Populate(Type);
                        return true;
                    case "adjustment":
                        Adjustment = source.Populate(Adjustment);
                        return true;
                    case "adjustmentReason":
                        AdjustmentReason = source.Populate(AdjustmentReason);
                        return true;
                    case "date":
                        DateElement = source.PopulateValue(DateElement);
                        return true;
                    case "_date":
                        DateElement = source.Populate(DateElement);
                        return true;
                    case "amount":
                        Amount = source.Populate(Amount);
                        return true;
                    case "identifier":
                        Identifier = source.Populate(Identifier);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PaymentComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Adjustment != null) dest.Adjustment = (Hl7.Fhir.Model.R4.Money)Adjustment.DeepCopy();
                    if(AdjustmentReason != null) dest.AdjustmentReason = (Hl7.Fhir.Model.CodeableConcept)AdjustmentReason.DeepCopy();
                    if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.Date)DateElement.DeepCopy();
                    if(Amount != null) dest.Amount = (Hl7.Fhir.Model.R4.Money)Amount.DeepCopy();
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new PaymentComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PaymentComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Adjustment, otherT.Adjustment)) return false;
                if( !DeepComparable.Matches(AdjustmentReason, otherT.AdjustmentReason)) return false;
                if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.Matches(Amount, otherT.Amount)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PaymentComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Adjustment, otherT.Adjustment)) return false;
                if( !DeepComparable.IsExactly(AdjustmentReason, otherT.AdjustmentReason)) return false;
                if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.IsExactly(Amount, otherT.Amount)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    if (Adjustment != null) yield return Adjustment;
                    if (AdjustmentReason != null) yield return AdjustmentReason;
                    if (DateElement != null) yield return DateElement;
                    if (Amount != null) yield return Amount;
                    if (Identifier != null) yield return Identifier;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Adjustment != null) yield return new ElementValue("adjustment", Adjustment);
                    if (AdjustmentReason != null) yield return new ElementValue("adjustmentReason", AdjustmentReason);
                    if (DateElement != null) yield return new ElementValue("date", DateElement);
                    if (Amount != null) yield return new ElementValue("amount", Amount);
                    if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "NoteComponent")]
        [DataContract]
        public partial class NoteComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "NoteComponent"; } }
            
            /// <summary>
            /// Note instance identifier
            /// </summary>
            [FhirElement("number", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt NumberElement
            {
                get { return _NumberElement; }
                set { _NumberElement = value; OnPropertyChanged("NumberElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _NumberElement;
            
            /// <summary>
            /// Note instance identifier
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
            [FhirElement("type", Order=50)]
            [DataMember]
            public Code<Hl7.Fhir.Model.R4.NoteType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.R4.NoteType> _TypeElement;
            
            /// <summary>
            /// display | print | printoper
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.R4.NoteType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (value == null)
                        TypeElement = null;
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.R4.NoteType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Note explanatory text
            /// </summary>
            [FhirElement("text", Order=60)]
            [Cardinality(Min=1,Max=1)]
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
            
            /// <summary>
            /// Language of the text
            /// </summary>
            [FhirElement("language", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Language
            {
                get { return _Language; }
                set { _Language = value; OnPropertyChanged("Language"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Language;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("NoteComponent");
                base.Serialize(sink);
                sink.Element("number", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); NumberElement?.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); TypeElement?.Serialize(sink);
                sink.Element("text", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); TextElement?.Serialize(sink);
                sink.Element("language", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Language?.Serialize(sink);
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
                        TypeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.NoteType>>();
                        return true;
                    case "text":
                        TextElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "language":
                        Language = source.Get<Hl7.Fhir.Model.CodeableConcept>();
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
                        TypeElement = source.PopulateValue(TypeElement);
                        return true;
                    case "_type":
                        TypeElement = source.Populate(TypeElement);
                        return true;
                    case "text":
                        TextElement = source.PopulateValue(TextElement);
                        return true;
                    case "_text":
                        TextElement = source.Populate(TextElement);
                        return true;
                    case "language":
                        Language = source.Populate(Language);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as NoteComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NumberElement != null) dest.NumberElement = (Hl7.Fhir.Model.PositiveInt)NumberElement.DeepCopy();
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.R4.NoteType>)TypeElement.DeepCopy();
                    if(TextElement != null) dest.TextElement = (Hl7.Fhir.Model.FhirString)TextElement.DeepCopy();
                    if(Language != null) dest.Language = (Hl7.Fhir.Model.CodeableConcept)Language.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new NoteComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as NoteComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NumberElement, otherT.NumberElement)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(TextElement, otherT.TextElement)) return false;
                if( !DeepComparable.Matches(Language, otherT.Language)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as NoteComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NumberElement, otherT.NumberElement)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(TextElement, otherT.TextElement)) return false;
                if( !DeepComparable.IsExactly(Language, otherT.Language)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NumberElement != null) yield return NumberElement;
                    if (TypeElement != null) yield return TypeElement;
                    if (TextElement != null) yield return TextElement;
                    if (Language != null) yield return Language;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NumberElement != null) yield return new ElementValue("number", NumberElement);
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                    if (TextElement != null) yield return new ElementValue("text", TextElement);
                    if (Language != null) yield return new ElementValue("language", Language);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "InsuranceComponent")]
        [DataContract]
        public partial class InsuranceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "InsuranceComponent"; } }
            
            /// <summary>
            /// Insurance instance identifier
            /// </summary>
            [FhirElement("sequence", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceElement;
            
            /// <summary>
            /// Insurance instance identifier
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
            /// Coverage to be used for adjudication
            /// </summary>
            [FhirElement("focal", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean FocalElement
            {
                get { return _FocalElement; }
                set { _FocalElement = value; OnPropertyChanged("FocalElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _FocalElement;
            
            /// <summary>
            /// Coverage to be used for adjudication
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
            [FhirElement("coverage", Order=60)]
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
            /// Additional provider contract number
            /// </summary>
            [FhirElement("businessArrangement", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString BusinessArrangementElement
            {
                get { return _BusinessArrangementElement; }
                set { _BusinessArrangementElement = value; OnPropertyChanged("BusinessArrangementElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _BusinessArrangementElement;
            
            /// <summary>
            /// Additional provider contract number
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
            /// Adjudication results
            /// </summary>
            [FhirElement("claimResponse", Order=80)]
            [CLSCompliant(false)]
            [References("ClaimResponse")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference ClaimResponse
            {
                get { return _ClaimResponse; }
                set { _ClaimResponse = value; OnPropertyChanged("ClaimResponse"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _ClaimResponse;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("InsuranceComponent");
                base.Serialize(sink);
                sink.Element("sequence", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); SequenceElement?.Serialize(sink);
                sink.Element("focal", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); FocalElement?.Serialize(sink);
                sink.Element("coverage", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Coverage?.Serialize(sink);
                sink.Element("businessArrangement", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); BusinessArrangementElement?.Serialize(sink);
                sink.Element("claimResponse", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ClaimResponse?.Serialize(sink);
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
                    case "claimResponse":
                        ClaimResponse = source.Get<Hl7.Fhir.Model.ResourceReference>();
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
                    case "claimResponse":
                        ClaimResponse = source.Populate(ClaimResponse);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as InsuranceComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.PositiveInt)SequenceElement.DeepCopy();
                    if(FocalElement != null) dest.FocalElement = (Hl7.Fhir.Model.FhirBoolean)FocalElement.DeepCopy();
                    if(Coverage != null) dest.Coverage = (Hl7.Fhir.Model.ResourceReference)Coverage.DeepCopy();
                    if(BusinessArrangementElement != null) dest.BusinessArrangementElement = (Hl7.Fhir.Model.FhirString)BusinessArrangementElement.DeepCopy();
                    if(ClaimResponse != null) dest.ClaimResponse = (Hl7.Fhir.Model.ResourceReference)ClaimResponse.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new InsuranceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as InsuranceComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(FocalElement, otherT.FocalElement)) return false;
                if( !DeepComparable.Matches(Coverage, otherT.Coverage)) return false;
                if( !DeepComparable.Matches(BusinessArrangementElement, otherT.BusinessArrangementElement)) return false;
                if( !DeepComparable.Matches(ClaimResponse, otherT.ClaimResponse)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as InsuranceComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(FocalElement, otherT.FocalElement)) return false;
                if( !DeepComparable.IsExactly(Coverage, otherT.Coverage)) return false;
                if( !DeepComparable.IsExactly(BusinessArrangementElement, otherT.BusinessArrangementElement)) return false;
                if( !DeepComparable.IsExactly(ClaimResponse, otherT.ClaimResponse)) return false;
            
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
                    if (ClaimResponse != null) yield return ClaimResponse;
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
                    if (ClaimResponse != null) yield return new ElementValue("claimResponse", ClaimResponse);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ErrorComponent")]
        [DataContract]
        public partial class ErrorComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ErrorComponent"; } }
            
            /// <summary>
            /// Item sequence number
            /// </summary>
            [FhirElement("itemSequence", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt ItemSequenceElement
            {
                get { return _ItemSequenceElement; }
                set { _ItemSequenceElement = value; OnPropertyChanged("ItemSequenceElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _ItemSequenceElement;
            
            /// <summary>
            /// Item sequence number
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? ItemSequence
            {
                get { return ItemSequenceElement != null ? ItemSequenceElement.Value : null; }
                set
                {
                    if (value == null)
                        ItemSequenceElement = null;
                    else
                        ItemSequenceElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("ItemSequence");
                }
            }
            
            /// <summary>
            /// Detail sequence number
            /// </summary>
            [FhirElement("detailSequence", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt DetailSequenceElement
            {
                get { return _DetailSequenceElement; }
                set { _DetailSequenceElement = value; OnPropertyChanged("DetailSequenceElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _DetailSequenceElement;
            
            /// <summary>
            /// Detail sequence number
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? DetailSequence
            {
                get { return DetailSequenceElement != null ? DetailSequenceElement.Value : null; }
                set
                {
                    if (value == null)
                        DetailSequenceElement = null;
                    else
                        DetailSequenceElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("DetailSequence");
                }
            }
            
            /// <summary>
            /// Subdetail sequence number
            /// </summary>
            [FhirElement("subDetailSequence", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SubDetailSequenceElement
            {
                get { return _SubDetailSequenceElement; }
                set { _SubDetailSequenceElement = value; OnPropertyChanged("SubDetailSequenceElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SubDetailSequenceElement;
            
            /// <summary>
            /// Subdetail sequence number
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? SubDetailSequence
            {
                get { return SubDetailSequenceElement != null ? SubDetailSequenceElement.Value : null; }
                set
                {
                    if (value == null)
                        SubDetailSequenceElement = null;
                    else
                        SubDetailSequenceElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("SubDetailSequence");
                }
            }
            
            /// <summary>
            /// Error code detailing processing issues
            /// </summary>
            [FhirElement("code", Order=70)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ErrorComponent");
                base.Serialize(sink);
                sink.Element("itemSequence", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ItemSequenceElement?.Serialize(sink);
                sink.Element("detailSequence", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DetailSequenceElement?.Serialize(sink);
                sink.Element("subDetailSequence", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); SubDetailSequenceElement?.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Code?.Serialize(sink);
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
                    case "itemSequence":
                        ItemSequenceElement = source.Get<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "detailSequence":
                        DetailSequenceElement = source.Get<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "subDetailSequence":
                        SubDetailSequenceElement = source.Get<Hl7.Fhir.Model.PositiveInt>();
                        return true;
                    case "code":
                        Code = source.Get<Hl7.Fhir.Model.CodeableConcept>();
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
                    case "itemSequence":
                        ItemSequenceElement = source.PopulateValue(ItemSequenceElement);
                        return true;
                    case "_itemSequence":
                        ItemSequenceElement = source.Populate(ItemSequenceElement);
                        return true;
                    case "detailSequence":
                        DetailSequenceElement = source.PopulateValue(DetailSequenceElement);
                        return true;
                    case "_detailSequence":
                        DetailSequenceElement = source.Populate(DetailSequenceElement);
                        return true;
                    case "subDetailSequence":
                        SubDetailSequenceElement = source.PopulateValue(SubDetailSequenceElement);
                        return true;
                    case "_subDetailSequence":
                        SubDetailSequenceElement = source.Populate(SubDetailSequenceElement);
                        return true;
                    case "code":
                        Code = source.Populate(Code);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ErrorComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ItemSequenceElement != null) dest.ItemSequenceElement = (Hl7.Fhir.Model.PositiveInt)ItemSequenceElement.DeepCopy();
                    if(DetailSequenceElement != null) dest.DetailSequenceElement = (Hl7.Fhir.Model.PositiveInt)DetailSequenceElement.DeepCopy();
                    if(SubDetailSequenceElement != null) dest.SubDetailSequenceElement = (Hl7.Fhir.Model.PositiveInt)SubDetailSequenceElement.DeepCopy();
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ErrorComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ErrorComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ItemSequenceElement, otherT.ItemSequenceElement)) return false;
                if( !DeepComparable.Matches(DetailSequenceElement, otherT.DetailSequenceElement)) return false;
                if( !DeepComparable.Matches(SubDetailSequenceElement, otherT.SubDetailSequenceElement)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ErrorComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ItemSequenceElement, otherT.ItemSequenceElement)) return false;
                if( !DeepComparable.IsExactly(DetailSequenceElement, otherT.DetailSequenceElement)) return false;
                if( !DeepComparable.IsExactly(SubDetailSequenceElement, otherT.SubDetailSequenceElement)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (ItemSequenceElement != null) yield return ItemSequenceElement;
                    if (DetailSequenceElement != null) yield return DetailSequenceElement;
                    if (SubDetailSequenceElement != null) yield return SubDetailSequenceElement;
                    if (Code != null) yield return Code;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (ItemSequenceElement != null) yield return new ElementValue("itemSequence", ItemSequenceElement);
                    if (DetailSequenceElement != null) yield return new ElementValue("detailSequence", DetailSequenceElement);
                    if (SubDetailSequenceElement != null) yield return new ElementValue("subDetailSequence", SubDetailSequenceElement);
                    if (Code != null) yield return new ElementValue("code", Code);
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IClaimResponseAddedItemComponent> Hl7.Fhir.Model.IClaimResponse.AddItem { get { return AddItem; } }
    
        
        /// <summary>
        /// Business Identifier for a claim response
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
        /// active | cancelled | draft | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.FinancialResourceStatusCodes> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.FinancialResourceStatusCodes> _StatusElement;
        
        /// <summary>
        /// active | cancelled | draft | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.FinancialResourceStatusCodes? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.FinancialResourceStatusCodes>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// More granular claim type
        /// </summary>
        [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Type;
        
        /// <summary>
        /// More granular claim type
        /// </summary>
        [FhirElement("subType", Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept SubType
        {
            get { return _SubType; }
            set { _SubType = value; OnPropertyChanged("SubType"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _SubType;
        
        /// <summary>
        /// claim | preauthorization | predetermination
        /// </summary>
        [FhirElement("use", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.R4.Use> UseElement
        {
            get { return _UseElement; }
            set { _UseElement = value; OnPropertyChanged("UseElement"); }
        }
        
        private Code<Hl7.Fhir.Model.R4.Use> _UseElement;
        
        /// <summary>
        /// claim | preauthorization | predetermination
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.R4.Use? Use
        {
            get { return UseElement != null ? UseElement.Value : null; }
            set
            {
                if (value == null)
                    UseElement = null;
                else
                    UseElement = new Code<Hl7.Fhir.Model.R4.Use>(value);
                OnPropertyChanged("Use");
            }
        }
        
        /// <summary>
        /// The recipient of the products and services
        /// </summary>
        [FhirElement("patient", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [References("Patient")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient
        {
            get { return _Patient; }
            set { _Patient = value; OnPropertyChanged("Patient"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Patient;
        
        /// <summary>
        /// Response creation date
        /// </summary>
        [FhirElement("created", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime CreatedElement
        {
            get { return _CreatedElement; }
            set { _CreatedElement = value; OnPropertyChanged("CreatedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _CreatedElement;
        
        /// <summary>
        /// Response creation date
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
        /// Party responsible for reimbursement
        /// </summary>
        [FhirElement("insurer", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [References("Organization")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Insurer
        {
            get { return _Insurer; }
            set { _Insurer = value; OnPropertyChanged("Insurer"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Insurer;
        
        /// <summary>
        /// Party responsible for the claim
        /// </summary>
        [FhirElement("requestor", Order=170)]
        [CLSCompliant(false)]
        [References("Practitioner","PractitionerRole","Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Requestor
        {
            get { return _Requestor; }
            set { _Requestor = value; OnPropertyChanged("Requestor"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Requestor;
        
        /// <summary>
        /// Id of resource triggering adjudication
        /// </summary>
        [FhirElement("request", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
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
        /// queued | complete | error | partial
        /// </summary>
        [FhirElement("outcome", InSummary=Hl7.Fhir.Model.Version.All, Order=190)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.R4.ClaimProcessingCodes> OutcomeElement
        {
            get { return _OutcomeElement; }
            set { _OutcomeElement = value; OnPropertyChanged("OutcomeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.R4.ClaimProcessingCodes> _OutcomeElement;
        
        /// <summary>
        /// queued | complete | error | partial
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.R4.ClaimProcessingCodes? Outcome
        {
            get { return OutcomeElement != null ? OutcomeElement.Value : null; }
            set
            {
                if (value == null)
                    OutcomeElement = null;
                else
                    OutcomeElement = new Code<Hl7.Fhir.Model.R4.ClaimProcessingCodes>(value);
                OnPropertyChanged("Outcome");
            }
        }
        
        /// <summary>
        /// Disposition Message
        /// </summary>
        [FhirElement("disposition", Order=200)]
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
        /// Preauthorization reference
        /// </summary>
        [FhirElement("preAuthRef", Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PreAuthRefElement
        {
            get { return _PreAuthRefElement; }
            set { _PreAuthRefElement = value; OnPropertyChanged("PreAuthRefElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _PreAuthRefElement;
        
        /// <summary>
        /// Preauthorization reference
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string PreAuthRef
        {
            get { return PreAuthRefElement != null ? PreAuthRefElement.Value : null; }
            set
            {
                if (value == null)
                    PreAuthRefElement = null;
                else
                    PreAuthRefElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("PreAuthRef");
            }
        }
        
        /// <summary>
        /// Preauthorization reference effective period
        /// </summary>
        [FhirElement("preAuthPeriod", Order=220)]
        [DataMember]
        public Hl7.Fhir.Model.Period PreAuthPeriod
        {
            get { return _PreAuthPeriod; }
            set { _PreAuthPeriod = value; OnPropertyChanged("PreAuthPeriod"); }
        }
        
        private Hl7.Fhir.Model.Period _PreAuthPeriod;
        
        /// <summary>
        /// Party to be paid any benefits payable
        /// </summary>
        [FhirElement("payeeType", Order=230)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept PayeeType
        {
            get { return _PayeeType; }
            set { _PayeeType = value; OnPropertyChanged("PayeeType"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _PayeeType;
        
        /// <summary>
        /// Adjudication for claim line items
        /// </summary>
        [FhirElement("item", Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ItemComponent> Item
        {
            get { if(_Item==null) _Item = new List<ItemComponent>(); return _Item; }
            set { _Item = value; OnPropertyChanged("Item"); }
        }
        
        private List<ItemComponent> _Item;
        
        /// <summary>
        /// Insurer added line items
        /// </summary>
        [FhirElement("addItem", Order=250)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<AddedItemComponent> AddItem
        {
            get { if(_AddItem==null) _AddItem = new List<AddedItemComponent>(); return _AddItem; }
            set { _AddItem = value; OnPropertyChanged("AddItem"); }
        }
        
        private List<AddedItemComponent> _AddItem;
        
        /// <summary>
        /// Header-level adjudication
        /// </summary>
        [FhirElement("adjudication", Order=260)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<AdjudicationComponent> Adjudication
        {
            get { if(_Adjudication==null) _Adjudication = new List<AdjudicationComponent>(); return _Adjudication; }
            set { _Adjudication = value; OnPropertyChanged("Adjudication"); }
        }
        
        private List<AdjudicationComponent> _Adjudication;
        
        /// <summary>
        /// Adjudication totals
        /// </summary>
        [FhirElement("total", InSummary=Hl7.Fhir.Model.Version.All, Order=270)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<TotalComponent> Total
        {
            get { if(_Total==null) _Total = new List<TotalComponent>(); return _Total; }
            set { _Total = value; OnPropertyChanged("Total"); }
        }
        
        private List<TotalComponent> _Total;
        
        /// <summary>
        /// Payment Details
        /// </summary>
        [FhirElement("payment", Order=280)]
        [DataMember]
        public PaymentComponent Payment
        {
            get { return _Payment; }
            set { _Payment = value; OnPropertyChanged("Payment"); }
        }
        
        private PaymentComponent _Payment;
        
        /// <summary>
        /// Funds reserved status
        /// </summary>
        [FhirElement("fundsReserve", Order=290)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept FundsReserve
        {
            get { return _FundsReserve; }
            set { _FundsReserve = value; OnPropertyChanged("FundsReserve"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _FundsReserve;
        
        /// <summary>
        /// Printed form identifier
        /// </summary>
        [FhirElement("formCode", Order=300)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept FormCode
        {
            get { return _FormCode; }
            set { _FormCode = value; OnPropertyChanged("FormCode"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _FormCode;
        
        /// <summary>
        /// Printed reference or actual form
        /// </summary>
        [FhirElement("form", Order=310)]
        [DataMember]
        public Hl7.Fhir.Model.Attachment Form
        {
            get { return _Form; }
            set { _Form = value; OnPropertyChanged("Form"); }
        }
        
        private Hl7.Fhir.Model.Attachment _Form;
        
        /// <summary>
        /// Note concerning adjudication
        /// </summary>
        [FhirElement("processNote", Order=320)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<NoteComponent> ProcessNote
        {
            get { if(_ProcessNote==null) _ProcessNote = new List<NoteComponent>(); return _ProcessNote; }
            set { _ProcessNote = value; OnPropertyChanged("ProcessNote"); }
        }
        
        private List<NoteComponent> _ProcessNote;
        
        /// <summary>
        /// Request for additional information
        /// </summary>
        [FhirElement("communicationRequest", Order=330)]
        [CLSCompliant(false)]
        [References("CommunicationRequest")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> CommunicationRequest
        {
            get { if(_CommunicationRequest==null) _CommunicationRequest = new List<Hl7.Fhir.Model.ResourceReference>(); return _CommunicationRequest; }
            set { _CommunicationRequest = value; OnPropertyChanged("CommunicationRequest"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _CommunicationRequest;
        
        /// <summary>
        /// Patient insurance information
        /// </summary>
        [FhirElement("insurance", Order=340)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<InsuranceComponent> Insurance
        {
            get { if(_Insurance==null) _Insurance = new List<InsuranceComponent>(); return _Insurance; }
            set { _Insurance = value; OnPropertyChanged("Insurance"); }
        }
        
        private List<InsuranceComponent> _Insurance;
        
        /// <summary>
        /// Processing errors
        /// </summary>
        [FhirElement("error", Order=350)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ErrorComponent> Error
        {
            get { if(_Error==null) _Error = new List<ErrorComponent>(); return _Error; }
            set { _Error = value; OnPropertyChanged("Error"); }
        }
        
        private List<ErrorComponent> _Error;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ClaimResponse;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.FinancialResourceStatusCodes>)StatusElement.DeepCopy();
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(SubType != null) dest.SubType = (Hl7.Fhir.Model.CodeableConcept)SubType.DeepCopy();
                if(UseElement != null) dest.UseElement = (Code<Hl7.Fhir.Model.R4.Use>)UseElement.DeepCopy();
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(CreatedElement != null) dest.CreatedElement = (Hl7.Fhir.Model.FhirDateTime)CreatedElement.DeepCopy();
                if(Insurer != null) dest.Insurer = (Hl7.Fhir.Model.ResourceReference)Insurer.DeepCopy();
                if(Requestor != null) dest.Requestor = (Hl7.Fhir.Model.ResourceReference)Requestor.DeepCopy();
                if(Request != null) dest.Request = (Hl7.Fhir.Model.ResourceReference)Request.DeepCopy();
                if(OutcomeElement != null) dest.OutcomeElement = (Code<Hl7.Fhir.Model.R4.ClaimProcessingCodes>)OutcomeElement.DeepCopy();
                if(DispositionElement != null) dest.DispositionElement = (Hl7.Fhir.Model.FhirString)DispositionElement.DeepCopy();
                if(PreAuthRefElement != null) dest.PreAuthRefElement = (Hl7.Fhir.Model.FhirString)PreAuthRefElement.DeepCopy();
                if(PreAuthPeriod != null) dest.PreAuthPeriod = (Hl7.Fhir.Model.Period)PreAuthPeriod.DeepCopy();
                if(PayeeType != null) dest.PayeeType = (Hl7.Fhir.Model.CodeableConcept)PayeeType.DeepCopy();
                if(Item != null) dest.Item = new List<ItemComponent>(Item.DeepCopy());
                if(AddItem != null) dest.AddItem = new List<AddedItemComponent>(AddItem.DeepCopy());
                if(Adjudication != null) dest.Adjudication = new List<AdjudicationComponent>(Adjudication.DeepCopy());
                if(Total != null) dest.Total = new List<TotalComponent>(Total.DeepCopy());
                if(Payment != null) dest.Payment = (PaymentComponent)Payment.DeepCopy();
                if(FundsReserve != null) dest.FundsReserve = (Hl7.Fhir.Model.CodeableConcept)FundsReserve.DeepCopy();
                if(FormCode != null) dest.FormCode = (Hl7.Fhir.Model.CodeableConcept)FormCode.DeepCopy();
                if(Form != null) dest.Form = (Hl7.Fhir.Model.Attachment)Form.DeepCopy();
                if(ProcessNote != null) dest.ProcessNote = new List<NoteComponent>(ProcessNote.DeepCopy());
                if(CommunicationRequest != null) dest.CommunicationRequest = new List<Hl7.Fhir.Model.ResourceReference>(CommunicationRequest.DeepCopy());
                if(Insurance != null) dest.Insurance = new List<InsuranceComponent>(Insurance.DeepCopy());
                if(Error != null) dest.Error = new List<ErrorComponent>(Error.DeepCopy());
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
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(SubType, otherT.SubType)) return false;
            if( !DeepComparable.Matches(UseElement, otherT.UseElement)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.Matches(Insurer, otherT.Insurer)) return false;
            if( !DeepComparable.Matches(Requestor, otherT.Requestor)) return false;
            if( !DeepComparable.Matches(Request, otherT.Request)) return false;
            if( !DeepComparable.Matches(OutcomeElement, otherT.OutcomeElement)) return false;
            if( !DeepComparable.Matches(DispositionElement, otherT.DispositionElement)) return false;
            if( !DeepComparable.Matches(PreAuthRefElement, otherT.PreAuthRefElement)) return false;
            if( !DeepComparable.Matches(PreAuthPeriod, otherT.PreAuthPeriod)) return false;
            if( !DeepComparable.Matches(PayeeType, otherT.PayeeType)) return false;
            if( !DeepComparable.Matches(Item, otherT.Item)) return false;
            if( !DeepComparable.Matches(AddItem, otherT.AddItem)) return false;
            if( !DeepComparable.Matches(Adjudication, otherT.Adjudication)) return false;
            if( !DeepComparable.Matches(Total, otherT.Total)) return false;
            if( !DeepComparable.Matches(Payment, otherT.Payment)) return false;
            if( !DeepComparable.Matches(FundsReserve, otherT.FundsReserve)) return false;
            if( !DeepComparable.Matches(FormCode, otherT.FormCode)) return false;
            if( !DeepComparable.Matches(Form, otherT.Form)) return false;
            if( !DeepComparable.Matches(ProcessNote, otherT.ProcessNote)) return false;
            if( !DeepComparable.Matches(CommunicationRequest, otherT.CommunicationRequest)) return false;
            if( !DeepComparable.Matches(Insurance, otherT.Insurance)) return false;
            if( !DeepComparable.Matches(Error, otherT.Error)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ClaimResponse;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(SubType, otherT.SubType)) return false;
            if( !DeepComparable.IsExactly(UseElement, otherT.UseElement)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.IsExactly(Insurer, otherT.Insurer)) return false;
            if( !DeepComparable.IsExactly(Requestor, otherT.Requestor)) return false;
            if( !DeepComparable.IsExactly(Request, otherT.Request)) return false;
            if( !DeepComparable.IsExactly(OutcomeElement, otherT.OutcomeElement)) return false;
            if( !DeepComparable.IsExactly(DispositionElement, otherT.DispositionElement)) return false;
            if( !DeepComparable.IsExactly(PreAuthRefElement, otherT.PreAuthRefElement)) return false;
            if( !DeepComparable.IsExactly(PreAuthPeriod, otherT.PreAuthPeriod)) return false;
            if( !DeepComparable.IsExactly(PayeeType, otherT.PayeeType)) return false;
            if( !DeepComparable.IsExactly(Item, otherT.Item)) return false;
            if( !DeepComparable.IsExactly(AddItem, otherT.AddItem)) return false;
            if( !DeepComparable.IsExactly(Adjudication, otherT.Adjudication)) return false;
            if( !DeepComparable.IsExactly(Total, otherT.Total)) return false;
            if( !DeepComparable.IsExactly(Payment, otherT.Payment)) return false;
            if( !DeepComparable.IsExactly(FundsReserve, otherT.FundsReserve)) return false;
            if( !DeepComparable.IsExactly(FormCode, otherT.FormCode)) return false;
            if( !DeepComparable.IsExactly(Form, otherT.Form)) return false;
            if( !DeepComparable.IsExactly(ProcessNote, otherT.ProcessNote)) return false;
            if( !DeepComparable.IsExactly(CommunicationRequest, otherT.CommunicationRequest)) return false;
            if( !DeepComparable.IsExactly(Insurance, otherT.Insurance)) return false;
            if( !DeepComparable.IsExactly(Error, otherT.Error)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("ClaimResponse");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Type?.Serialize(sink);
            sink.Element("subType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); SubType?.Serialize(sink);
            sink.Element("use", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); UseElement?.Serialize(sink);
            sink.Element("patient", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Patient?.Serialize(sink);
            sink.Element("created", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); CreatedElement?.Serialize(sink);
            sink.Element("insurer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Insurer?.Serialize(sink);
            sink.Element("requestor", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Requestor?.Serialize(sink);
            sink.Element("request", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Request?.Serialize(sink);
            sink.Element("outcome", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); OutcomeElement?.Serialize(sink);
            sink.Element("disposition", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DispositionElement?.Serialize(sink);
            sink.Element("preAuthRef", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PreAuthRefElement?.Serialize(sink);
            sink.Element("preAuthPeriod", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PreAuthPeriod?.Serialize(sink);
            sink.Element("payeeType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PayeeType?.Serialize(sink);
            sink.BeginList("item", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Item)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("addItem", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in AddItem)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("adjudication", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Adjudication)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("total", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Total)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("payment", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Payment?.Serialize(sink);
            sink.Element("fundsReserve", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); FundsReserve?.Serialize(sink);
            sink.Element("formCode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); FormCode?.Serialize(sink);
            sink.Element("form", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Form?.Serialize(sink);
            sink.BeginList("processNote", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in ProcessNote)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("communicationRequest", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in CommunicationRequest)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("insurance", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Insurance)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("error", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Error)
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
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.FinancialResourceStatusCodes>>();
                    return true;
                case "type":
                    Type = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "subType":
                    SubType = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "use":
                    UseElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.Use>>();
                    return true;
                case "patient":
                    Patient = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "created":
                    CreatedElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "insurer":
                    Insurer = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "requestor":
                    Requestor = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "request":
                    Request = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "outcome":
                    OutcomeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.ClaimProcessingCodes>>();
                    return true;
                case "disposition":
                    DispositionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "preAuthRef":
                    PreAuthRefElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "preAuthPeriod":
                    PreAuthPeriod = source.Get<Hl7.Fhir.Model.Period>();
                    return true;
                case "payeeType":
                    PayeeType = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "item":
                    Item = source.GetList<ItemComponent>();
                    return true;
                case "addItem":
                    AddItem = source.GetList<AddedItemComponent>();
                    return true;
                case "adjudication":
                    Adjudication = source.GetList<AdjudicationComponent>();
                    return true;
                case "total":
                    Total = source.GetList<TotalComponent>();
                    return true;
                case "payment":
                    Payment = source.Get<PaymentComponent>();
                    return true;
                case "fundsReserve":
                    FundsReserve = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "formCode":
                    FormCode = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "form":
                    Form = source.Get<Hl7.Fhir.Model.Attachment>();
                    return true;
                case "processNote":
                    ProcessNote = source.GetList<NoteComponent>();
                    return true;
                case "communicationRequest":
                    CommunicationRequest = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "insurance":
                    Insurance = source.GetList<InsuranceComponent>();
                    return true;
                case "error":
                    Error = source.GetList<ErrorComponent>();
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
                case "subType":
                    SubType = source.Populate(SubType);
                    return true;
                case "use":
                    UseElement = source.PopulateValue(UseElement);
                    return true;
                case "_use":
                    UseElement = source.Populate(UseElement);
                    return true;
                case "patient":
                    Patient = source.Populate(Patient);
                    return true;
                case "created":
                    CreatedElement = source.PopulateValue(CreatedElement);
                    return true;
                case "_created":
                    CreatedElement = source.Populate(CreatedElement);
                    return true;
                case "insurer":
                    Insurer = source.Populate(Insurer);
                    return true;
                case "requestor":
                    Requestor = source.Populate(Requestor);
                    return true;
                case "request":
                    Request = source.Populate(Request);
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
                case "preAuthRef":
                    PreAuthRefElement = source.PopulateValue(PreAuthRefElement);
                    return true;
                case "_preAuthRef":
                    PreAuthRefElement = source.Populate(PreAuthRefElement);
                    return true;
                case "preAuthPeriod":
                    PreAuthPeriod = source.Populate(PreAuthPeriod);
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
                case "adjudication":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "total":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "payment":
                    Payment = source.Populate(Payment);
                    return true;
                case "fundsReserve":
                    FundsReserve = source.Populate(FundsReserve);
                    return true;
                case "formCode":
                    FormCode = source.Populate(FormCode);
                    return true;
                case "form":
                    Form = source.Populate(Form);
                    return true;
                case "processNote":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "communicationRequest":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "insurance":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "error":
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
                case "adjudication":
                    source.PopulateListItem(Adjudication, index);
                    return true;
                case "total":
                    source.PopulateListItem(Total, index);
                    return true;
                case "processNote":
                    source.PopulateListItem(ProcessNote, index);
                    return true;
                case "communicationRequest":
                    source.PopulateListItem(CommunicationRequest, index);
                    return true;
                case "insurance":
                    source.PopulateListItem(Insurance, index);
                    return true;
                case "error":
                    source.PopulateListItem(Error, index);
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
                if (SubType != null) yield return SubType;
                if (UseElement != null) yield return UseElement;
                if (Patient != null) yield return Patient;
                if (CreatedElement != null) yield return CreatedElement;
                if (Insurer != null) yield return Insurer;
                if (Requestor != null) yield return Requestor;
                if (Request != null) yield return Request;
                if (OutcomeElement != null) yield return OutcomeElement;
                if (DispositionElement != null) yield return DispositionElement;
                if (PreAuthRefElement != null) yield return PreAuthRefElement;
                if (PreAuthPeriod != null) yield return PreAuthPeriod;
                if (PayeeType != null) yield return PayeeType;
                foreach (var elem in Item) { if (elem != null) yield return elem; }
                foreach (var elem in AddItem) { if (elem != null) yield return elem; }
                foreach (var elem in Adjudication) { if (elem != null) yield return elem; }
                foreach (var elem in Total) { if (elem != null) yield return elem; }
                if (Payment != null) yield return Payment;
                if (FundsReserve != null) yield return FundsReserve;
                if (FormCode != null) yield return FormCode;
                if (Form != null) yield return Form;
                foreach (var elem in ProcessNote) { if (elem != null) yield return elem; }
                foreach (var elem in CommunicationRequest) { if (elem != null) yield return elem; }
                foreach (var elem in Insurance) { if (elem != null) yield return elem; }
                foreach (var elem in Error) { if (elem != null) yield return elem; }
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
                if (SubType != null) yield return new ElementValue("subType", SubType);
                if (UseElement != null) yield return new ElementValue("use", UseElement);
                if (Patient != null) yield return new ElementValue("patient", Patient);
                if (CreatedElement != null) yield return new ElementValue("created", CreatedElement);
                if (Insurer != null) yield return new ElementValue("insurer", Insurer);
                if (Requestor != null) yield return new ElementValue("requestor", Requestor);
                if (Request != null) yield return new ElementValue("request", Request);
                if (OutcomeElement != null) yield return new ElementValue("outcome", OutcomeElement);
                if (DispositionElement != null) yield return new ElementValue("disposition", DispositionElement);
                if (PreAuthRefElement != null) yield return new ElementValue("preAuthRef", PreAuthRefElement);
                if (PreAuthPeriod != null) yield return new ElementValue("preAuthPeriod", PreAuthPeriod);
                if (PayeeType != null) yield return new ElementValue("payeeType", PayeeType);
                foreach (var elem in Item) { if (elem != null) yield return new ElementValue("item", elem); }
                foreach (var elem in AddItem) { if (elem != null) yield return new ElementValue("addItem", elem); }
                foreach (var elem in Adjudication) { if (elem != null) yield return new ElementValue("adjudication", elem); }
                foreach (var elem in Total) { if (elem != null) yield return new ElementValue("total", elem); }
                if (Payment != null) yield return new ElementValue("payment", Payment);
                if (FundsReserve != null) yield return new ElementValue("fundsReserve", FundsReserve);
                if (FormCode != null) yield return new ElementValue("formCode", FormCode);
                if (Form != null) yield return new ElementValue("form", Form);
                foreach (var elem in ProcessNote) { if (elem != null) yield return new ElementValue("processNote", elem); }
                foreach (var elem in CommunicationRequest) { if (elem != null) yield return new ElementValue("communicationRequest", elem); }
                foreach (var elem in Insurance) { if (elem != null) yield return new ElementValue("insurance", elem); }
                foreach (var elem in Error) { if (elem != null) yield return new ElementValue("error", elem); }
            }
        }
    
    }

}
