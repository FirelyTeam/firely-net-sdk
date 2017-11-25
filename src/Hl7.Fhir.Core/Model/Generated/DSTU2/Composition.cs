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
    [FhirType("Composition", IsResource=true)]
    [DataContract]
    public partial class Composition : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Composition; } }
        [NotMapped]
        public override string TypeName { get { return "Composition"; } }
    
    
        [FhirType("AttesterComponent")]
        [DataContract]
        public partial class AttesterComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "AttesterComponent"; } }
            
            /// <summary>
            /// personal | professional | legal | official
            /// </summary>
            [FhirElement("mode", InSummary=true, Order=40)]
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
            [FhirElement("time", InSummary=true, Order=50)]
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
            [FhirElement("party", InSummary=true, Order=60)]
            [CLSCompliant(false)]
            [References("Patient","Practitioner","Organization")]
            [DataMember]
            public Hl7.Fhir.Model.DSTU2.ResourceReference Party
            {
                get { return _Party; }
                set { _Party = value; OnPropertyChanged("Party"); }
            }
            
            private Hl7.Fhir.Model.DSTU2.ResourceReference _Party;
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AttesterComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ModeElement != null) dest.ModeElement = new List<Code<Hl7.Fhir.Model.CompositionAttestationMode>>(ModeElement.DeepCopy());
                    if(TimeElement != null) dest.TimeElement = (Hl7.Fhir.Model.FhirDateTime)TimeElement.DeepCopy();
                    if(Party != null) dest.Party = (Hl7.Fhir.Model.DSTU2.ResourceReference)Party.DeepCopy();
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
                    foreach (var elem in ModeElement) { if (elem != null) yield return new ElementValue("mode", true, elem); }
                    if (TimeElement != null) yield return new ElementValue("time", false, TimeElement);
                    if (Party != null) yield return new ElementValue("party", false, Party);
                }
            }
        
        
        }
    
    
        [FhirType("EventComponent")]
        [DataContract]
        public partial class EventComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "EventComponent"; } }
            
            /// <summary>
            /// Code(s) that apply to the event being documented
            /// </summary>
            [FhirElement("code", InSummary=true, Order=40)]
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
            [FhirElement("period", InSummary=true, Order=50)]
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
            [FhirElement("detail", InSummary=true, Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.DSTU2.ResourceReference> Detail
            {
                get { if(_Detail==null) _Detail = new List<Hl7.Fhir.Model.DSTU2.ResourceReference>(); return _Detail; }
                set { _Detail = value; OnPropertyChanged("Detail"); }
            }
            
            private List<Hl7.Fhir.Model.DSTU2.ResourceReference> _Detail;
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as EventComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = new List<Hl7.Fhir.Model.CodeableConcept>(Code.DeepCopy());
                    if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                    if(Detail != null) dest.Detail = new List<Hl7.Fhir.Model.DSTU2.ResourceReference>(Detail.DeepCopy());
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
                    foreach (var elem in Code) { if (elem != null) yield return new ElementValue("code", true, elem); }
                    if (Period != null) yield return new ElementValue("period", false, Period);
                    foreach (var elem in Detail) { if (elem != null) yield return new ElementValue("detail", true, elem); }
                }
            }
        
        
        }
    
    
        [FhirType("SectionComponent")]
        [DataContract]
        public partial class SectionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "SectionComponent"; } }
            
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
            [FhirElement("mode", InSummary=true, Order=70)]
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
            public List<Hl7.Fhir.Model.DSTU2.ResourceReference> Entry
            {
                get { if(_Entry==null) _Entry = new List<Hl7.Fhir.Model.DSTU2.ResourceReference>(); return _Entry; }
                set { _Entry = value; OnPropertyChanged("Entry"); }
            }
            
            private List<Hl7.Fhir.Model.DSTU2.ResourceReference> _Entry;
            
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
                    if(Entry != null) dest.Entry = new List<Hl7.Fhir.Model.DSTU2.ResourceReference>(Entry.DeepCopy());
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
                    if (TitleElement != null) yield return new ElementValue("title", false, TitleElement);
                    if (Code != null) yield return new ElementValue("code", false, Code);
                    if (Text != null) yield return new ElementValue("text", false, Text);
                    if (ModeElement != null) yield return new ElementValue("mode", false, ModeElement);
                    if (OrderedBy != null) yield return new ElementValue("orderedBy", false, OrderedBy);
                    foreach (var elem in Entry) { if (elem != null) yield return new ElementValue("entry", true, elem); }
                    if (EmptyReason != null) yield return new ElementValue("emptyReason", false, EmptyReason);
                    foreach (var elem in Section) { if (elem != null) yield return new ElementValue("section", true, elem); }
                }
            }
        
        
        }
    
        
        /// <summary>
        /// Logical identifier of composition (version-independent)
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.DSTU2.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private Hl7.Fhir.Model.DSTU2.Identifier _Identifier;
        
        /// <summary>
        /// Composition editing time
        /// </summary>
        [FhirElement("date", InSummary=true, Order=100)]
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
        [FhirElement("type", InSummary=true, Order=110)]
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
        [FhirElement("class", InSummary=true, Order=120)]
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
        [FhirElement("title", InSummary=true, Order=130)]
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
        [FhirElement("status", InSummary=true, Order=140)]
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
        [FhirElement("confidentiality", InSummary=true, Order=150)]
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
        [FhirElement("subject", InSummary=true, Order=160)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.DSTU2.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.DSTU2.ResourceReference _Subject;
        
        /// <summary>
        /// Who and/or what authored the composition
        /// </summary>
        [FhirElement("author", InSummary=true, Order=170)]
        [CLSCompliant(false)]
        [References("Practitioner","Device","Patient","RelatedPerson")]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.DSTU2.ResourceReference> Author
        {
            get { if(_Author==null) _Author = new List<Hl7.Fhir.Model.DSTU2.ResourceReference>(); return _Author; }
            set { _Author = value; OnPropertyChanged("Author"); }
        }
        
        private List<Hl7.Fhir.Model.DSTU2.ResourceReference> _Author;
        
        /// <summary>
        /// Attests to accuracy of composition
        /// </summary>
        [FhirElement("attester", InSummary=true, Order=180)]
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
        [FhirElement("custodian", InSummary=true, Order=190)]
        [CLSCompliant(false)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.DSTU2.ResourceReference Custodian
        {
            get { return _Custodian; }
            set { _Custodian = value; OnPropertyChanged("Custodian"); }
        }
        
        private Hl7.Fhir.Model.DSTU2.ResourceReference _Custodian;
        
        /// <summary>
        /// The clinical service(s) being documented
        /// </summary>
        [FhirElement("event", InSummary=true, Order=200)]
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
        [FhirElement("encounter", InSummary=true, Order=210)]
        [CLSCompliant(false)]
        [References("Encounter")]
        [DataMember]
        public Hl7.Fhir.Model.DSTU2.ResourceReference Encounter
        {
            get { return _Encounter; }
            set { _Encounter = value; OnPropertyChanged("Encounter"); }
        }
        
        private Hl7.Fhir.Model.DSTU2.ResourceReference _Encounter;
        
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
    
    
        public static ElementDefinitionConstraint Composition_CMP_2 = new ElementDefinitionConstraint
        {
            Expression = "section.all(emptyReason.empty() or entry.empty())",
            Key = "cmp-2",
            Severity = ConstraintSeverity.Warning,
            Human = "A section can only have an emptyReason if it is empty",
            Xpath = "not(exists(f:emptyReason) and exists(f:entry))"
        };
    
        public static ElementDefinitionConstraint Composition_CMP_1 = new ElementDefinitionConstraint
        {
            Expression = "section.all(text or entry or section)",
            Key = "cmp-1",
            Severity = ConstraintSeverity.Warning,
            Human = "A section must at least one of text, entries, or sub-sections",
            Xpath = "exists(f:text) or exists(f:entry) or exists(f:section)"
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
    
            InvariantConstraints.Add(Composition_CMP_2);
            InvariantConstraints.Add(Composition_CMP_1);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Composition;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.DSTU2.Identifier)Identifier.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(Class != null) dest.Class = (Hl7.Fhir.Model.CodeableConcept)Class.DeepCopy();
                if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.CompositionStatus>)StatusElement.DeepCopy();
                if(ConfidentialityElement != null) dest.ConfidentialityElement = (Code<Hl7.Fhir.Model.DSTU2.v3CodeSystemConfidentiality>)ConfidentialityElement.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.DSTU2.ResourceReference)Subject.DeepCopy();
                if(Author != null) dest.Author = new List<Hl7.Fhir.Model.DSTU2.ResourceReference>(Author.DeepCopy());
                if(Attester != null) dest.Attester = new List<AttesterComponent>(Attester.DeepCopy());
                if(Custodian != null) dest.Custodian = (Hl7.Fhir.Model.DSTU2.ResourceReference)Custodian.DeepCopy();
                if(Event != null) dest.Event = new List<EventComponent>(Event.DeepCopy());
                if(Encounter != null) dest.Encounter = (Hl7.Fhir.Model.DSTU2.ResourceReference)Encounter.DeepCopy();
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
                if (Identifier != null) yield return new ElementValue("identifier", false, Identifier);
                if (DateElement != null) yield return new ElementValue("date", false, DateElement);
                if (Type != null) yield return new ElementValue("type", false, Type);
                if (Class != null) yield return new ElementValue("class", false, Class);
                if (TitleElement != null) yield return new ElementValue("title", false, TitleElement);
                if (StatusElement != null) yield return new ElementValue("status", false, StatusElement);
                if (ConfidentialityElement != null) yield return new ElementValue("confidentiality", false, ConfidentialityElement);
                if (Subject != null) yield return new ElementValue("subject", false, Subject);
                foreach (var elem in Author) { if (elem != null) yield return new ElementValue("author", true, elem); }
                foreach (var elem in Attester) { if (elem != null) yield return new ElementValue("attester", true, elem); }
                if (Custodian != null) yield return new ElementValue("custodian", false, Custodian);
                foreach (var elem in Event) { if (elem != null) yield return new ElementValue("event", true, elem); }
                if (Encounter != null) yield return new ElementValue("encounter", false, Encounter);
                foreach (var elem in Section) { if (elem != null) yield return new ElementValue("section", true, elem); }
            }
        }
    
    }

}
