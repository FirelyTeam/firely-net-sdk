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
    /// A list is a curated collection of resources
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "List", IsResource=true)]
    [DataContract]
    public partial class List : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IList, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.List; } }
        [NotMapped]
        public override string TypeName { get { return "List"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "EntryComponent")]
        [DataContract]
        public partial class EntryComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IListEntryComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "EntryComponent"; } }
            
            /// <summary>
            /// Status/Workflow information about this item
            /// </summary>
            [FhirElement("flag", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Flag
            {
                get { return _Flag; }
                set { _Flag = value; OnPropertyChanged("Flag"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Flag;
            
            /// <summary>
            /// If this item is actually marked as deleted
            /// </summary>
            [FhirElement("deleted", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean DeletedElement
            {
                get { return _DeletedElement; }
                set { _DeletedElement = value; OnPropertyChanged("DeletedElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _DeletedElement;
            
            /// <summary>
            /// If this item is actually marked as deleted
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Deleted
            {
                get { return DeletedElement != null ? DeletedElement.Value : null; }
                set
                {
                    if (value == null)
                        DeletedElement = null;
                    else
                        DeletedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Deleted");
                }
            }
            
            /// <summary>
            /// When item added to list
            /// </summary>
            [FhirElement("date", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime DateElement
            {
                get { return _DateElement; }
                set { _DateElement = value; OnPropertyChanged("DateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _DateElement;
            
            /// <summary>
            /// When item added to list
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
            /// Actual entry
            /// </summary>
            [FhirElement("item", Order=70)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Item
            {
                get { return _Item; }
                set { _Item = value; OnPropertyChanged("Item"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Item;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("EntryComponent");
                base.Serialize(sink);
                sink.Element("flag", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Flag?.Serialize(sink);
                sink.Element("deleted", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DeletedElement?.Serialize(sink);
                sink.Element("date", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DateElement?.Serialize(sink);
                sink.Element("item", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Item?.Serialize(sink);
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
                    case "flag":
                        Flag = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                        return true;
                    case "deleted":
                        DeletedElement = source.Get<Hl7.Fhir.Model.FhirBoolean>();
                        return true;
                    case "date":
                        DateElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                        return true;
                    case "item":
                        Item = source.Get<Hl7.Fhir.Model.ResourceReference>();
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
                    case "flag":
                        Flag = source.Populate(Flag);
                        return true;
                    case "deleted":
                        DeletedElement = source.PopulateValue(DeletedElement);
                        return true;
                    case "_deleted":
                        DeletedElement = source.Populate(DeletedElement);
                        return true;
                    case "date":
                        DateElement = source.PopulateValue(DateElement);
                        return true;
                    case "_date":
                        DateElement = source.Populate(DateElement);
                        return true;
                    case "item":
                        Item = source.Populate(Item);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as EntryComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Flag != null) dest.Flag = (Hl7.Fhir.Model.CodeableConcept)Flag.DeepCopy();
                    if(DeletedElement != null) dest.DeletedElement = (Hl7.Fhir.Model.FhirBoolean)DeletedElement.DeepCopy();
                    if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                    if(Item != null) dest.Item = (Hl7.Fhir.Model.ResourceReference)Item.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new EntryComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as EntryComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Flag, otherT.Flag)) return false;
                if( !DeepComparable.Matches(DeletedElement, otherT.DeletedElement)) return false;
                if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.Matches(Item, otherT.Item)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as EntryComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Flag, otherT.Flag)) return false;
                if( !DeepComparable.IsExactly(DeletedElement, otherT.DeletedElement)) return false;
                if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.IsExactly(Item, otherT.Item)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Flag != null) yield return Flag;
                    if (DeletedElement != null) yield return DeletedElement;
                    if (DateElement != null) yield return DateElement;
                    if (Item != null) yield return Item;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Flag != null) yield return new ElementValue("flag", Flag);
                    if (DeletedElement != null) yield return new ElementValue("deleted", DeletedElement);
                    if (DateElement != null) yield return new ElementValue("date", DateElement);
                    if (Item != null) yield return new ElementValue("item", Item);
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IListEntryComponent> Hl7.Fhir.Model.IList.Entry { get { return Entry; } }
    
        
        /// <summary>
        /// Business identifier
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
        /// current | retired | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.ListStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.ListStatus> _StatusElement;
        
        /// <summary>
        /// current | retired | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.ListStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.ListStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// working | snapshot | changes
        /// </summary>
        [FhirElement("mode", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
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
        /// Descriptive name for the list
        /// </summary>
        [FhirElement("title", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString TitleElement
        {
            get { return _TitleElement; }
            set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _TitleElement;
        
        /// <summary>
        /// Descriptive name for the list
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
        /// What the purpose of this list is
        /// </summary>
        [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Code
        {
            get { return _Code; }
            set { _Code = value; OnPropertyChanged("Code"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Code;
        
        /// <summary>
        /// If all resources have the same subject
        /// </summary>
        [FhirElement("subject", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [References("Patient","Group","Device","Location")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Context in which list created
        /// </summary>
        [FhirElement("encounter", Order=150)]
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
        /// When the list was prepared
        /// </summary>
        [FhirElement("date", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// When the list was prepared
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
        /// Who and/or what defined the list contents (aka Author)
        /// </summary>
        [FhirElement("source", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [References("Practitioner","PractitionerRole","Patient","Device")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Source
        {
            get { return _Source; }
            set { _Source = value; OnPropertyChanged("Source"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Source;
        
        /// <summary>
        /// What order the list has
        /// </summary>
        [FhirElement("orderedBy", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept OrderedBy
        {
            get { return _OrderedBy; }
            set { _OrderedBy = value; OnPropertyChanged("OrderedBy"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _OrderedBy;
        
        /// <summary>
        /// Comments about the list
        /// </summary>
        [FhirElement("note", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Note;
        
        /// <summary>
        /// Entries in the list
        /// </summary>
        [FhirElement("entry", Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<EntryComponent> Entry
        {
            get { if(_Entry==null) _Entry = new List<EntryComponent>(); return _Entry; }
            set { _Entry = value; OnPropertyChanged("Entry"); }
        }
        
        private List<EntryComponent> _Entry;
        
        /// <summary>
        /// Why list is empty
        /// </summary>
        [FhirElement("emptyReason", Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept EmptyReason
        {
            get { return _EmptyReason; }
            set { _EmptyReason = value; OnPropertyChanged("EmptyReason"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _EmptyReason;
    
    
        public static ElementDefinitionConstraint[] List_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "lst-3",
                severity: ConstraintSeverity.Warning,
                expression: "mode = 'working' or entry.date.empty()",
                human: "An entry date can only be used if the mode of the list is \"working\"",
                xpath: "(f:mode/@value = 'working') or not(exists(f:entry/f:date))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "lst-2",
                severity: ConstraintSeverity.Warning,
                expression: "mode = 'changes' or entry.deleted.empty()",
                human: "The deleted flag can only be used if the mode of the list is \"changes\"",
                xpath: "(f:mode/@value = 'changes') or not(exists(f:entry/f:deleted))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "lst-1",
                severity: ConstraintSeverity.Warning,
                expression: "emptyReason.empty() or entry.empty()",
                human: "A list can only have an emptyReason if it is empty",
                xpath: "not(exists(f:emptyReason) and exists(f:entry))"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(List_Constraints);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as List;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.ListStatus>)StatusElement.DeepCopy();
                if(ModeElement != null) dest.ModeElement = (Code<Hl7.Fhir.Model.ListMode>)ModeElement.DeepCopy();
                if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(Source != null) dest.Source = (Hl7.Fhir.Model.ResourceReference)Source.DeepCopy();
                if(OrderedBy != null) dest.OrderedBy = (Hl7.Fhir.Model.CodeableConcept)OrderedBy.DeepCopy();
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                if(Entry != null) dest.Entry = new List<EntryComponent>(Entry.DeepCopy());
                if(EmptyReason != null) dest.EmptyReason = (Hl7.Fhir.Model.CodeableConcept)EmptyReason.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new List());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as List;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(ModeElement, otherT.ModeElement)) return false;
            if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(Source, otherT.Source)) return false;
            if( !DeepComparable.Matches(OrderedBy, otherT.OrderedBy)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            if( !DeepComparable.Matches(Entry, otherT.Entry)) return false;
            if( !DeepComparable.Matches(EmptyReason, otherT.EmptyReason)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as List;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(ModeElement, otherT.ModeElement)) return false;
            if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(Source, otherT.Source)) return false;
            if( !DeepComparable.IsExactly(OrderedBy, otherT.OrderedBy)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            if( !DeepComparable.IsExactly(Entry, otherT.Entry)) return false;
            if( !DeepComparable.IsExactly(EmptyReason, otherT.EmptyReason)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("List");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("mode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); ModeElement?.Serialize(sink);
            sink.Element("title", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TitleElement?.Serialize(sink);
            sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Code?.Serialize(sink);
            sink.Element("subject", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Subject?.Serialize(sink);
            sink.Element("encounter", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Encounter?.Serialize(sink);
            sink.Element("date", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DateElement?.Serialize(sink);
            sink.Element("source", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Source?.Serialize(sink);
            sink.Element("orderedBy", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); OrderedBy?.Serialize(sink);
            sink.BeginList("note", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Note)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("entry", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Entry)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("emptyReason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); EmptyReason?.Serialize(sink);
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
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ListStatus>>();
                    return true;
                case "mode":
                    ModeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ListMode>>();
                    return true;
                case "title":
                    TitleElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "code":
                    Code = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "subject":
                    Subject = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "encounter":
                    Encounter = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "date":
                    DateElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "source":
                    Source = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "orderedBy":
                    OrderedBy = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "note":
                    Note = source.GetList<Hl7.Fhir.Model.Annotation>();
                    return true;
                case "entry":
                    Entry = source.GetList<EntryComponent>();
                    return true;
                case "emptyReason":
                    EmptyReason = source.Get<Hl7.Fhir.Model.CodeableConcept>();
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
                case "mode":
                    ModeElement = source.PopulateValue(ModeElement);
                    return true;
                case "_mode":
                    ModeElement = source.Populate(ModeElement);
                    return true;
                case "title":
                    TitleElement = source.PopulateValue(TitleElement);
                    return true;
                case "_title":
                    TitleElement = source.Populate(TitleElement);
                    return true;
                case "code":
                    Code = source.Populate(Code);
                    return true;
                case "subject":
                    Subject = source.Populate(Subject);
                    return true;
                case "encounter":
                    Encounter = source.Populate(Encounter);
                    return true;
                case "date":
                    DateElement = source.PopulateValue(DateElement);
                    return true;
                case "_date":
                    DateElement = source.Populate(DateElement);
                    return true;
                case "source":
                    Source = source.Populate(Source);
                    return true;
                case "orderedBy":
                    OrderedBy = source.Populate(OrderedBy);
                    return true;
                case "note":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "entry":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "emptyReason":
                    EmptyReason = source.Populate(EmptyReason);
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
                case "note":
                    source.PopulateListItem(Note, index);
                    return true;
                case "entry":
                    source.PopulateListItem(Entry, index);
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
                if (ModeElement != null) yield return ModeElement;
                if (TitleElement != null) yield return TitleElement;
                if (Code != null) yield return Code;
                if (Subject != null) yield return Subject;
                if (Encounter != null) yield return Encounter;
                if (DateElement != null) yield return DateElement;
                if (Source != null) yield return Source;
                if (OrderedBy != null) yield return OrderedBy;
                foreach (var elem in Note) { if (elem != null) yield return elem; }
                foreach (var elem in Entry) { if (elem != null) yield return elem; }
                if (EmptyReason != null) yield return EmptyReason;
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
                if (ModeElement != null) yield return new ElementValue("mode", ModeElement);
                if (TitleElement != null) yield return new ElementValue("title", TitleElement);
                if (Code != null) yield return new ElementValue("code", Code);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Encounter != null) yield return new ElementValue("encounter", Encounter);
                if (DateElement != null) yield return new ElementValue("date", DateElement);
                if (Source != null) yield return new ElementValue("source", Source);
                if (OrderedBy != null) yield return new ElementValue("orderedBy", OrderedBy);
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                foreach (var elem in Entry) { if (elem != null) yield return new ElementValue("entry", elem); }
                if (EmptyReason != null) yield return new ElementValue("emptyReason", EmptyReason);
            }
        }
    
    }

}
