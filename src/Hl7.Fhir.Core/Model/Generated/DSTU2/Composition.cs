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
    /// A set of resources composed into a single coherent clinical statement with clinical attestation
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.DSTU2, "Composition", IsResource=true)]
    [DataContract]
    public partial class Composition : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IComposition, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Composition; } }
        [NotMapped]
        public override string TypeName { get { return "Composition"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "AttesterComponent")]
        [DataContract]
        public partial class AttesterComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ICompositionAttesterComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "AttesterComponent"; } }
            
            /// <summary>
            /// personal | professional | legal | official
            /// </summary>
            [FhirElement("mode", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Code<Hl7.Fhir.Model.CompositionAttestationMode>> ModeElement
            {
                get { if(_ModeElement==null) _ModeElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.CompositionAttestationMode>>(); return _ModeElement; }
                set { _ModeElement = value; OnPropertyChanged("ModeElement"); }
            }
            
            private List<Code<Hl7.Fhir.Model.CompositionAttestationMode>> _ModeElement;
            
            /// <summary>
            /// personal | professional | legal | official
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<Hl7.Fhir.Model.CompositionAttestationMode?> Mode
            {
                get { return ModeElement != null ? ModeElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        ModeElement = null;
                    else
                        ModeElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.CompositionAttestationMode>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.CompositionAttestationMode>(elem)));
                    OnPropertyChanged("Mode");
                }
            }
            
            /// <summary>
            /// When composition attested
            /// </summary>
            [FhirElement("time", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime TimeElement
            {
                get { return _TimeElement; }
                set { _TimeElement = value; OnPropertyChanged("TimeElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _TimeElement;
            
            /// <summary>
            /// When composition attested
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Time
            {
                get { return TimeElement != null ? TimeElement.Value : null; }
                set
                {
                    if (value == null)
                        TimeElement = null;
                    else
                        TimeElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("Time");
                }
            }
            
            /// <summary>
            /// Who attested the composition
            /// </summary>
            [FhirElement("party", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [References("Patient","Practitioner","Organization")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Party
            {
                get { return _Party; }
                set { _Party = value; OnPropertyChanged("Party"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Party;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("AttesterComponent");
                base.Serialize(sink);
                sink.BeginList("mode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true);
                sink.Serialize(ModeElement);
                sink.End();
                sink.Element("time", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TimeElement?.Serialize(sink);
                sink.Element("party", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Party?.Serialize(sink);
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
                    case "mode":
                        ModeElement = source.GetList<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.CompositionAttestationMode>>();
                        return true;
                    case "time":
                        TimeElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                        return true;
                    case "party":
                        Party = source.Get<Hl7.Fhir.Model.ResourceReference>();
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
                    case "mode":
                    case "_mode":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "time":
                        TimeElement = source.PopulateValue(TimeElement);
                        return true;
                    case "_time":
                        TimeElement = source.Populate(TimeElement);
                        return true;
                    case "party":
                        Party = source.Populate(Party);
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
                    case "mode":
                        source.PopulatePrimitiveListItemValue(ModeElement, index);
                        return true;
                    case "_mode":
                        source.PopulatePrimitiveListItem(ModeElement, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AttesterComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ModeElement != null) dest.ModeElement = new List<Code<Hl7.Fhir.Model.CompositionAttestationMode>>(ModeElement.DeepCopy());
                    if(TimeElement != null) dest.TimeElement = (Hl7.Fhir.Model.FhirDateTime)TimeElement.DeepCopy();
                    if(Party != null) dest.Party = (Hl7.Fhir.Model.ResourceReference)Party.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new AttesterComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AttesterComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.Matches(TimeElement, otherT.TimeElement)) return false;
                if( !DeepComparable.Matches(Party, otherT.Party)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AttesterComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.IsExactly(TimeElement, otherT.TimeElement)) return false;
                if( !DeepComparable.IsExactly(Party, otherT.Party)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in ModeElement) { if (elem != null) yield return elem; }
                    if (TimeElement != null) yield return TimeElement;
                    if (Party != null) yield return Party;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in ModeElement) { if (elem != null) yield return new ElementValue("mode", elem); }
                    if (TimeElement != null) yield return new ElementValue("time", TimeElement);
                    if (Party != null) yield return new ElementValue("party", Party);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "EventComponent")]
        [DataContract]
        public partial class EventComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ICompositionEventComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "EventComponent"; } }
            
            /// <summary>
            /// Code(s) that apply to the event being documented
            /// </summary>
            [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Code
            {
                get { if(_Code==null) _Code = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Code;
            
            /// <summary>
            /// The period covered by the documentation
            /// </summary>
            [FhirElement("period", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Period Period
            {
                get { return _Period; }
                set { _Period = value; OnPropertyChanged("Period"); }
            }
            
            private Hl7.Fhir.Model.Period _Period;
            
            /// <summary>
            /// The event(s) being documented
            /// </summary>
            [FhirElement("detail", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Detail
            {
                get { if(_Detail==null) _Detail = new List<Hl7.Fhir.Model.ResourceReference>(); return _Detail; }
                set { _Detail = value; OnPropertyChanged("Detail"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Detail;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("EventComponent");
                base.Serialize(sink);
                sink.BeginList("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Code)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("period", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Period?.Serialize(sink);
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
                    case "code":
                        Code = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "period":
                        Period = source.Get<Hl7.Fhir.Model.Period>();
                        return true;
                    case "detail":
                        Detail = source.GetList<Hl7.Fhir.Model.ResourceReference>();
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
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "period":
                        Period = source.Populate(Period);
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
                    case "code":
                        source.PopulateListItem(Code, index);
                        return true;
                    case "detail":
                        source.PopulateListItem(Detail, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as EventComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = new List<Hl7.Fhir.Model.CodeableConcept>(Code.DeepCopy());
                    if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                    if(Detail != null) dest.Detail = new List<Hl7.Fhir.Model.ResourceReference>(Detail.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new EventComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as EventComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Period, otherT.Period)) return false;
                if( !DeepComparable.Matches(Detail, otherT.Detail)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as EventComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
                if( !DeepComparable.IsExactly(Detail, otherT.Detail)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Code) { if (elem != null) yield return elem; }
                    if (Period != null) yield return Period;
                    foreach (var elem in Detail) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Code) { if (elem != null) yield return new ElementValue("code", elem); }
                    if (Period != null) yield return new ElementValue("period", Period);
                    foreach (var elem in Detail) { if (elem != null) yield return new ElementValue("detail", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "SectionComponent")]
        [DataContract]
        public partial class SectionComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ICompositionSectionComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SectionComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.ICompositionSectionComponent> Hl7.Fhir.Model.ICompositionSectionComponent.Section { get { return Section; } }
            
            /// <summary>
            /// Label for section (e.g. for ToC)
            /// </summary>
            [FhirElement("title", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TitleElement
            {
                get { return _TitleElement; }
                set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TitleElement;
            
            /// <summary>
            /// Label for section (e.g. for ToC)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Title
            {
                get { return TitleElement != null ? TitleElement.Value : null; }
                set
                {
                    if (value == null)
                        TitleElement = null;
                    else
                        TitleElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Title");
                }
            }
            
            /// <summary>
            /// Classification of section (recommended)
            /// </summary>
            [FhirElement("code", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// Text summary of the section, for human interpretation
            /// </summary>
            [FhirElement("text", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Narrative Text
            {
                get { return _Text; }
                set { _Text = value; OnPropertyChanged("Text"); }
            }
            
            private Hl7.Fhir.Model.Narrative _Text;
            
            /// <summary>
            /// working | snapshot | changes
            /// </summary>
            [FhirElement("mode", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [DataMember]
            public Code<Hl7.Fhir.Model.ListMode> ModeElement
            {
                get { return _ModeElement; }
                set { _ModeElement = value; OnPropertyChanged("ModeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.ListMode> _ModeElement;
            
            /// <summary>
            /// working | snapshot | changes
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.ListMode? Mode
            {
                get { return ModeElement != null ? ModeElement.Value : null; }
                set
                {
                    if (value == null)
                        ModeElement = null;
                    else
                        ModeElement = new Code<Hl7.Fhir.Model.ListMode>(value);
                    OnPropertyChanged("Mode");
                }
            }
            
            /// <summary>
            /// Order of section entries
            /// </summary>
            [FhirElement("orderedBy", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept OrderedBy
            {
                get { return _OrderedBy; }
                set { _OrderedBy = value; OnPropertyChanged("OrderedBy"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _OrderedBy;
            
            /// <summary>
            /// A reference to data that supports this section
            /// </summary>
            [FhirElement("entry", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Entry
            {
                get { if(_Entry==null) _Entry = new List<Hl7.Fhir.Model.ResourceReference>(); return _Entry; }
                set { _Entry = value; OnPropertyChanged("Entry"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Entry;
            
            /// <summary>
            /// Why the section is empty
            /// </summary>
            [FhirElement("emptyReason", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept EmptyReason
            {
                get { return _EmptyReason; }
                set { _EmptyReason = value; OnPropertyChanged("EmptyReason"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _EmptyReason;
            
            /// <summary>
            /// Nested Section
            /// </summary>
            [FhirElement("section", Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<SectionComponent> Section
            {
                get { if(_Section==null) _Section = new List<SectionComponent>(); return _Section; }
                set { _Section = value; OnPropertyChanged("Section"); }
            }
            
            private List<SectionComponent> _Section;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SectionComponent");
                base.Serialize(sink);
                sink.Element("title", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); TitleElement?.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Code?.Serialize(sink);
                sink.Element("text", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Text?.Serialize(sink);
                sink.Element("mode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ModeElement?.Serialize(sink);
                sink.Element("orderedBy", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); OrderedBy?.Serialize(sink);
                sink.BeginList("entry", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Entry)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("emptyReason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); EmptyReason?.Serialize(sink);
                sink.BeginList("section", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Section)
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
                    case "title":
                        TitleElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "code":
                        Code = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "text":
                        Text = source.Get<Hl7.Fhir.Model.Narrative>();
                        return true;
                    case "mode":
                        ModeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ListMode>>();
                        return true;
                    case "orderedBy":
                        OrderedBy = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "entry":
                        Entry = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                        return true;
                    case "emptyReason":
                        EmptyReason = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "section":
                        Section = source.GetList<SectionComponent>();
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
                    case "title":
                        TitleElement = source.PopulateValue(TitleElement);
                        return true;
                    case "_title":
                        TitleElement = source.Populate(TitleElement);
                        return true;
                    case "code":
                        Code = source.Populate(Code);
                        return true;
                    case "text":
                        Text = source.Populate(Text);
                        return true;
                    case "mode":
                        ModeElement = source.PopulateValue(ModeElement);
                        return true;
                    case "_mode":
                        ModeElement = source.Populate(ModeElement);
                        return true;
                    case "orderedBy":
                        OrderedBy = source.Populate(OrderedBy);
                        return true;
                    case "entry":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "emptyReason":
                        EmptyReason = source.Populate(EmptyReason);
                        return true;
                    case "section":
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
                    case "entry":
                        source.PopulateListItem(Entry, index);
                        return true;
                    case "section":
                        source.PopulateListItem(Section, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SectionComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(Text != null) dest.Text = (Hl7.Fhir.Model.Narrative)Text.DeepCopy();
                    if(ModeElement != null) dest.ModeElement = (Code<Hl7.Fhir.Model.ListMode>)ModeElement.DeepCopy();
                    if(OrderedBy != null) dest.OrderedBy = (Hl7.Fhir.Model.CodeableConcept)OrderedBy.DeepCopy();
                    if(Entry != null) dest.Entry = new List<Hl7.Fhir.Model.ResourceReference>(Entry.DeepCopy());
                    if(EmptyReason != null) dest.EmptyReason = (Hl7.Fhir.Model.CodeableConcept)EmptyReason.DeepCopy();
                    if(Section != null) dest.Section = new List<SectionComponent>(Section.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new SectionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SectionComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Text, otherT.Text)) return false;
                if( !DeepComparable.Matches(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.Matches(OrderedBy, otherT.OrderedBy)) return false;
                if( !DeepComparable.Matches(Entry, otherT.Entry)) return false;
                if( !DeepComparable.Matches(EmptyReason, otherT.EmptyReason)) return false;
                if( !DeepComparable.Matches(Section, otherT.Section)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SectionComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Text, otherT.Text)) return false;
                if( !DeepComparable.IsExactly(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.IsExactly(OrderedBy, otherT.OrderedBy)) return false;
                if( !DeepComparable.IsExactly(Entry, otherT.Entry)) return false;
                if( !DeepComparable.IsExactly(EmptyReason, otherT.EmptyReason)) return false;
                if( !DeepComparable.IsExactly(Section, otherT.Section)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (TitleElement != null) yield return TitleElement;
                    if (Code != null) yield return Code;
                    if (Text != null) yield return Text;
                    if (ModeElement != null) yield return ModeElement;
                    if (OrderedBy != null) yield return OrderedBy;
                    foreach (var elem in Entry) { if (elem != null) yield return elem; }
                    if (EmptyReason != null) yield return EmptyReason;
                    foreach (var elem in Section) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (TitleElement != null) yield return new ElementValue("title", TitleElement);
                    if (Code != null) yield return new ElementValue("code", Code);
                    if (Text != null) yield return new ElementValue("text", Text);
                    if (ModeElement != null) yield return new ElementValue("mode", ModeElement);
                    if (OrderedBy != null) yield return new ElementValue("orderedBy", OrderedBy);
                    foreach (var elem in Entry) { if (elem != null) yield return new ElementValue("entry", elem); }
                    if (EmptyReason != null) yield return new ElementValue("emptyReason", EmptyReason);
                    foreach (var elem in Section) { if (elem != null) yield return new ElementValue("section", elem); }
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.ICompositionAttesterComponent> Hl7.Fhir.Model.IComposition.Attester { get { return Attester; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.ICompositionEventComponent> Hl7.Fhir.Model.IComposition.Event { get { return Event; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.ICompositionSectionComponent> Hl7.Fhir.Model.IComposition.Section { get { return Section; } }
    
        
        /// <summary>
        /// Logical identifier of composition (version-independent)
        /// </summary>
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// Composition editing time
        /// </summary>
        [FhirElement("date", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// Composition editing time
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
                    DateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Date");
            }
        }
        
        /// <summary>
        /// Kind of composition (LOINC if possible)
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
        /// Categorization of Composition
        /// </summary>
        [FhirElement("class", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Class
        {
            get { return _Class; }
            set { _Class = value; OnPropertyChanged("Class"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Class;
        
        /// <summary>
        /// Human Readable name/title
        /// </summary>
        [FhirElement("title", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString TitleElement
        {
            get { return _TitleElement; }
            set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _TitleElement;
        
        /// <summary>
        /// Human Readable name/title
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Title
        {
            get { return TitleElement != null ? TitleElement.Value : null; }
            set
            {
                if (value == null)
                    TitleElement = null;
                else
                    TitleElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Title");
            }
        }
        
        /// <summary>
        /// preliminary | final | amended | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.CompositionStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.CompositionStatus> _StatusElement;
        
        /// <summary>
        /// preliminary | final | amended | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.CompositionStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.CompositionStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// As defined by affinity domain
        /// </summary>
        [FhirElement("confidentiality", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DSTU2.v3CodeSystemConfidentiality> ConfidentialityElement
        {
            get { return _ConfidentialityElement; }
            set { _ConfidentialityElement = value; OnPropertyChanged("ConfidentialityElement"); }
        }
        
        private Code<Hl7.Fhir.Model.DSTU2.v3CodeSystemConfidentiality> _ConfidentialityElement;
        
        /// <summary>
        /// As defined by affinity domain
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DSTU2.v3CodeSystemConfidentiality? Confidentiality
        {
            get { return ConfidentialityElement != null ? ConfidentialityElement.Value : null; }
            set
            {
                if (value == null)
                    ConfidentialityElement = null;
                else
                    ConfidentialityElement = new Code<Hl7.Fhir.Model.DSTU2.v3CodeSystemConfidentiality>(value);
                OnPropertyChanged("Confidentiality");
            }
        }
        
        /// <summary>
        /// Who and/or what the composition is about
        /// </summary>
        [FhirElement("subject", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Who and/or what authored the composition
        /// </summary>
        [FhirElement("author", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [References("Practitioner","Device","Patient","RelatedPerson")]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Author
        {
            get { if(_Author==null) _Author = new List<Hl7.Fhir.Model.ResourceReference>(); return _Author; }
            set { _Author = value; OnPropertyChanged("Author"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Author;
        
        /// <summary>
        /// Attests to accuracy of composition
        /// </summary>
        [FhirElement("attester", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<AttesterComponent> Attester
        {
            get { if(_Attester==null) _Attester = new List<AttesterComponent>(); return _Attester; }
            set { _Attester = value; OnPropertyChanged("Attester"); }
        }
        
        private List<AttesterComponent> _Attester;
        
        /// <summary>
        /// Organization which maintains the composition
        /// </summary>
        [FhirElement("custodian", InSummary=Hl7.Fhir.Model.Version.All, Order=190)]
        [CLSCompliant(false)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Custodian
        {
            get { return _Custodian; }
            set { _Custodian = value; OnPropertyChanged("Custodian"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Custodian;
        
        /// <summary>
        /// The clinical service(s) being documented
        /// </summary>
        [FhirElement("event", InSummary=Hl7.Fhir.Model.Version.All, Order=200)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<EventComponent> Event
        {
            get { if(_Event==null) _Event = new List<EventComponent>(); return _Event; }
            set { _Event = value; OnPropertyChanged("Event"); }
        }
        
        private List<EventComponent> _Event;
        
        /// <summary>
        /// Context of the Composition
        /// </summary>
        [FhirElement("encounter", InSummary=Hl7.Fhir.Model.Version.All, Order=210)]
        [CLSCompliant(false)]
        [References("Encounter")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Encounter
        {
            get { return _Encounter; }
            set { _Encounter = value; OnPropertyChanged("Encounter"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Encounter;
        
        /// <summary>
        /// Composition is broken into sections
        /// </summary>
        [FhirElement("section", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<SectionComponent> Section
        {
            get { if(_Section==null) _Section = new List<SectionComponent>(); return _Section; }
            set { _Section = value; OnPropertyChanged("Section"); }
        }
        
        private List<SectionComponent> _Section;
    
    
        public static ElementDefinitionConstraint[] Composition_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "cmp-2",
                severity: ConstraintSeverity.Warning,
                expression: "section.all(emptyReason.empty() or entry.empty())",
                human: "A section can only have an emptyReason if it is empty",
                xpath: "not(exists(f:emptyReason) and exists(f:entry))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.DSTU2},
                key: "cmp-1",
                severity: ConstraintSeverity.Warning,
                expression: "section.all(text or entry or section)",
                human: "A section must at least one of text, entries, or sub-sections",
                xpath: "exists(f:text) or exists(f:entry) or exists(f:section)"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(Composition_Constraints);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Composition;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(Class != null) dest.Class = (Hl7.Fhir.Model.CodeableConcept)Class.DeepCopy();
                if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.CompositionStatus>)StatusElement.DeepCopy();
                if(ConfidentialityElement != null) dest.ConfidentialityElement = (Code<Hl7.Fhir.Model.DSTU2.v3CodeSystemConfidentiality>)ConfidentialityElement.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Author != null) dest.Author = new List<Hl7.Fhir.Model.ResourceReference>(Author.DeepCopy());
                if(Attester != null) dest.Attester = new List<AttesterComponent>(Attester.DeepCopy());
                if(Custodian != null) dest.Custodian = (Hl7.Fhir.Model.ResourceReference)Custodian.DeepCopy();
                if(Event != null) dest.Event = new List<EventComponent>(Event.DeepCopy());
                if(Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
                if(Section != null) dest.Section = new List<SectionComponent>(Section.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new Composition());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Composition;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(Class, otherT.Class)) return false;
            if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(ConfidentialityElement, otherT.ConfidentialityElement)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Author, otherT.Author)) return false;
            if( !DeepComparable.Matches(Attester, otherT.Attester)) return false;
            if( !DeepComparable.Matches(Custodian, otherT.Custodian)) return false;
            if( !DeepComparable.Matches(Event, otherT.Event)) return false;
            if( !DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.Matches(Section, otherT.Section)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Composition;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(Class, otherT.Class)) return false;
            if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(ConfidentialityElement, otherT.ConfidentialityElement)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Author, otherT.Author)) return false;
            if( !DeepComparable.IsExactly(Attester, otherT.Attester)) return false;
            if( !DeepComparable.IsExactly(Custodian, otherT.Custodian)) return false;
            if( !DeepComparable.IsExactly(Event, otherT.Event)) return false;
            if( !DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.IsExactly(Section, otherT.Section)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("Composition");
            base.Serialize(sink);
            sink.Element("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Identifier?.Serialize(sink);
            sink.Element("date", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); DateElement?.Serialize(sink);
            sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Type?.Serialize(sink);
            sink.Element("class", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Class?.Serialize(sink);
            sink.Element("title", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); TitleElement?.Serialize(sink);
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("confidentiality", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ConfidentialityElement?.Serialize(sink);
            sink.Element("subject", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Subject?.Serialize(sink);
            sink.BeginList("author", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true);
            foreach(var item in Author)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("attester", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Attester)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("custodian", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Custodian?.Serialize(sink);
            sink.BeginList("event", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Event)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("encounter", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Encounter?.Serialize(sink);
            sink.BeginList("section", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Section)
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
                    Identifier = source.Get<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "date":
                    DateElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "type":
                    Type = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "class":
                    Class = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "title":
                    TitleElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.CompositionStatus>>();
                    return true;
                case "confidentiality":
                    ConfidentialityElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DSTU2.v3CodeSystemConfidentiality>>();
                    return true;
                case "subject":
                    Subject = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "author":
                    Author = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "attester":
                    Attester = source.GetList<AttesterComponent>();
                    return true;
                case "custodian":
                    Custodian = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "event":
                    Event = source.GetList<EventComponent>();
                    return true;
                case "encounter":
                    Encounter = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "section":
                    Section = source.GetList<SectionComponent>();
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
                    Identifier = source.Populate(Identifier);
                    return true;
                case "date":
                    DateElement = source.PopulateValue(DateElement);
                    return true;
                case "_date":
                    DateElement = source.Populate(DateElement);
                    return true;
                case "type":
                    Type = source.Populate(Type);
                    return true;
                case "class":
                    Class = source.Populate(Class);
                    return true;
                case "title":
                    TitleElement = source.PopulateValue(TitleElement);
                    return true;
                case "_title":
                    TitleElement = source.Populate(TitleElement);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "confidentiality":
                    ConfidentialityElement = source.PopulateValue(ConfidentialityElement);
                    return true;
                case "_confidentiality":
                    ConfidentialityElement = source.Populate(ConfidentialityElement);
                    return true;
                case "subject":
                    Subject = source.Populate(Subject);
                    return true;
                case "author":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "attester":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "custodian":
                    Custodian = source.Populate(Custodian);
                    return true;
                case "event":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "encounter":
                    Encounter = source.Populate(Encounter);
                    return true;
                case "section":
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
                case "author":
                    source.PopulateListItem(Author, index);
                    return true;
                case "attester":
                    source.PopulateListItem(Attester, index);
                    return true;
                case "event":
                    source.PopulateListItem(Event, index);
                    return true;
                case "section":
                    source.PopulateListItem(Section, index);
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
                if (Identifier != null) yield return Identifier;
                if (DateElement != null) yield return DateElement;
                if (Type != null) yield return Type;
                if (Class != null) yield return Class;
                if (TitleElement != null) yield return TitleElement;
                if (StatusElement != null) yield return StatusElement;
                if (ConfidentialityElement != null) yield return ConfidentialityElement;
                if (Subject != null) yield return Subject;
                foreach (var elem in Author) { if (elem != null) yield return elem; }
                foreach (var elem in Attester) { if (elem != null) yield return elem; }
                if (Custodian != null) yield return Custodian;
                foreach (var elem in Event) { if (elem != null) yield return elem; }
                if (Encounter != null) yield return Encounter;
                foreach (var elem in Section) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                if (DateElement != null) yield return new ElementValue("date", DateElement);
                if (Type != null) yield return new ElementValue("type", Type);
                if (Class != null) yield return new ElementValue("class", Class);
                if (TitleElement != null) yield return new ElementValue("title", TitleElement);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (ConfidentialityElement != null) yield return new ElementValue("confidentiality", ConfidentialityElement);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                foreach (var elem in Author) { if (elem != null) yield return new ElementValue("author", elem); }
                foreach (var elem in Attester) { if (elem != null) yield return new ElementValue("attester", elem); }
                if (Custodian != null) yield return new ElementValue("custodian", Custodian);
                foreach (var elem in Event) { if (elem != null) yield return new ElementValue("event", elem); }
                if (Encounter != null) yield return new ElementValue("encounter", Encounter);
                foreach (var elem in Section) { if (elem != null) yield return new ElementValue("section", elem); }
            }
        }
    
    }

}
