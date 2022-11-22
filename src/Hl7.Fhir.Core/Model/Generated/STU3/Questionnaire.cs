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
// Generated for FHIR v3.0.1
//
namespace Hl7.Fhir.Model.STU3
{
    /// <summary>
    /// A structured set of questions
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.STU3, "Questionnaire", IsResource=true)]
    [DataContract]
    public partial class Questionnaire : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IQuestionnaire, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Questionnaire; } }
        [NotMapped]
        public override string TypeName { get { return "Questionnaire"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "ItemComponent")]
        [DataContract]
        public partial class ItemComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ItemComponent"; } }
            
            /// <summary>
            /// Unique id for item in questionnaire
            /// </summary>
            [FhirElement("linkId", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString LinkIdElement
            {
                get { return _LinkIdElement; }
                set { _LinkIdElement = value; OnPropertyChanged("LinkIdElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _LinkIdElement;
            
            /// <summary>
            /// Unique id for item in questionnaire
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string LinkId
            {
                get { return LinkIdElement != null ? LinkIdElement.Value : null; }
                set
                {
                    if (value == null)
                        LinkIdElement = null;
                    else
                        LinkIdElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("LinkId");
                }
            }
            
            /// <summary>
            /// ElementDefinition - details for the item
            /// </summary>
            [FhirElement("definition", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri DefinitionElement
            {
                get { return _DefinitionElement; }
                set { _DefinitionElement = value; OnPropertyChanged("DefinitionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _DefinitionElement;
            
            /// <summary>
            /// ElementDefinition - details for the item
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Definition
            {
                get { return DefinitionElement != null ? DefinitionElement.Value : null; }
                set
                {
                    if (value == null)
                        DefinitionElement = null;
                    else
                        DefinitionElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Definition");
                }
            }
            
            /// <summary>
            /// Corresponding concept for this item in a terminology
            /// </summary>
            [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> Code
            {
                get { if(_Code==null) _Code = new List<Hl7.Fhir.Model.Coding>(); return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private List<Hl7.Fhir.Model.Coding> _Code;
            
            /// <summary>
            /// E.g. "1(a)", "2.5.3"
            /// </summary>
            [FhirElement("prefix", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PrefixElement
            {
                get { return _PrefixElement; }
                set { _PrefixElement = value; OnPropertyChanged("PrefixElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PrefixElement;
            
            /// <summary>
            /// E.g. "1(a)", "2.5.3"
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Prefix
            {
                get { return PrefixElement != null ? PrefixElement.Value : null; }
                set
                {
                    if (value == null)
                        PrefixElement = null;
                    else
                        PrefixElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Prefix");
                }
            }
            
            /// <summary>
            /// Primary text for the item
            /// </summary>
            [FhirElement("text", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TextElement
            {
                get { return _TextElement; }
                set { _TextElement = value; OnPropertyChanged("TextElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TextElement;
            
            /// <summary>
            /// Primary text for the item
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
            /// group | display | boolean | decimal | integer | date | dateTime +
            /// </summary>
            [FhirElement("type", Order=90)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.QuestionnaireItemType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.QuestionnaireItemType> _TypeElement;
            
            /// <summary>
            /// group | display | boolean | decimal | integer | date | dateTime +
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.QuestionnaireItemType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (value == null)
                        TypeElement = null;
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.QuestionnaireItemType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Only allow data when
            /// </summary>
            [FhirElement("enableWhen", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<EnableWhenComponent> EnableWhen
            {
                get { if(_EnableWhen==null) _EnableWhen = new List<EnableWhenComponent>(); return _EnableWhen; }
                set { _EnableWhen = value; OnPropertyChanged("EnableWhen"); }
            }
            
            private List<EnableWhenComponent> _EnableWhen;
            
            /// <summary>
            /// Whether the item must be included in data results
            /// </summary>
            [FhirElement("required", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean RequiredElement
            {
                get { return _RequiredElement; }
                set { _RequiredElement = value; OnPropertyChanged("RequiredElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _RequiredElement;
            
            /// <summary>
            /// Whether the item must be included in data results
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Required
            {
                get { return RequiredElement != null ? RequiredElement.Value : null; }
                set
                {
                    if (value == null)
                        RequiredElement = null;
                    else
                        RequiredElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Required");
                }
            }
            
            /// <summary>
            /// Whether the item may repeat
            /// </summary>
            [FhirElement("repeats", Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean RepeatsElement
            {
                get { return _RepeatsElement; }
                set { _RepeatsElement = value; OnPropertyChanged("RepeatsElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _RepeatsElement;
            
            /// <summary>
            /// Whether the item may repeat
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Repeats
            {
                get { return RepeatsElement != null ? RepeatsElement.Value : null; }
                set
                {
                    if (value == null)
                        RepeatsElement = null;
                    else
                        RepeatsElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Repeats");
                }
            }
            
            /// <summary>
            /// Don't allow human editing
            /// </summary>
            [FhirElement("readOnly", Order=130)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean ReadOnlyElement
            {
                get { return _ReadOnlyElement; }
                set { _ReadOnlyElement = value; OnPropertyChanged("ReadOnlyElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _ReadOnlyElement;
            
            /// <summary>
            /// Don't allow human editing
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? ReadOnly
            {
                get { return ReadOnlyElement != null ? ReadOnlyElement.Value : null; }
                set
                {
                    if (value == null)
                        ReadOnlyElement = null;
                    else
                        ReadOnlyElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("ReadOnly");
                }
            }
            
            /// <summary>
            /// No more than this many characters
            /// </summary>
            [FhirElement("maxLength", Order=140)]
            [DataMember]
            public Hl7.Fhir.Model.Integer MaxLengthElement
            {
                get { return _MaxLengthElement; }
                set { _MaxLengthElement = value; OnPropertyChanged("MaxLengthElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _MaxLengthElement;
            
            /// <summary>
            /// No more than this many characters
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? MaxLength
            {
                get { return MaxLengthElement != null ? MaxLengthElement.Value : null; }
                set
                {
                    if (value == null)
                        MaxLengthElement = null;
                    else
                        MaxLengthElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("MaxLength");
                }
            }
            
            /// <summary>
            /// Valueset containing permitted answers
            /// </summary>
            [FhirElement("options", Order=150)]
            [CLSCompliant(false)]
            [References("ValueSet")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Options
            {
                get { return _Options; }
                set { _Options = value; OnPropertyChanged("Options"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Options;
            
            /// <summary>
            /// Permitted answer
            /// </summary>
            [FhirElement("option", Order=160)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<OptionComponent> Option
            {
                get { if(_Option==null) _Option = new List<OptionComponent>(); return _Option; }
                set { _Option = value; OnPropertyChanged("Option"); }
            }
            
            private List<OptionComponent> _Option;
            
            /// <summary>
            /// Default value when item is first rendered
            /// </summary>
            [FhirElement("initial", Order=170, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.Coding),typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Initial
            {
                get { return _Initial; }
                set { _Initial = value; OnPropertyChanged("Initial"); }
            }
            
            private Hl7.Fhir.Model.Element _Initial;
            
            /// <summary>
            /// Nested questionnaire items
            /// </summary>
            [FhirElement("item", Order=180)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<ItemComponent> Item
            {
                get { if(_Item==null) _Item = new List<ItemComponent>(); return _Item; }
                set { _Item = value; OnPropertyChanged("Item"); }
            }
            
            private List<ItemComponent> _Item;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ItemComponent");
                base.Serialize(sink);
                sink.Element("linkId", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); LinkIdElement?.Serialize(sink);
                sink.Element("definition", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DefinitionElement?.Serialize(sink);
                sink.BeginList("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Code)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("prefix", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PrefixElement?.Serialize(sink);
                sink.Element("text", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TextElement?.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); TypeElement?.Serialize(sink);
                sink.BeginList("enableWhen", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in EnableWhen)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("required", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); RequiredElement?.Serialize(sink);
                sink.Element("repeats", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); RepeatsElement?.Serialize(sink);
                sink.Element("readOnly", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ReadOnlyElement?.Serialize(sink);
                sink.Element("maxLength", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); MaxLengthElement?.Serialize(sink);
                sink.Element("options", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Options?.Serialize(sink);
                sink.BeginList("option", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Option)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("initial", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Initial?.Serialize(sink);
                sink.BeginList("item", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Item)
                {
                    item?.Serialize(sink);
                }
                sink.End();
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
                    case "linkId":
                        LinkIdElement = source.PopulateValue(LinkIdElement);
                        return true;
                    case "_linkId":
                        LinkIdElement = source.Populate(LinkIdElement);
                        return true;
                    case "definition":
                        DefinitionElement = source.PopulateValue(DefinitionElement);
                        return true;
                    case "_definition":
                        DefinitionElement = source.Populate(DefinitionElement);
                        return true;
                    case "code":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "prefix":
                        PrefixElement = source.PopulateValue(PrefixElement);
                        return true;
                    case "_prefix":
                        PrefixElement = source.Populate(PrefixElement);
                        return true;
                    case "text":
                        TextElement = source.PopulateValue(TextElement);
                        return true;
                    case "_text":
                        TextElement = source.Populate(TextElement);
                        return true;
                    case "type":
                        TypeElement = source.PopulateValue(TypeElement);
                        return true;
                    case "_type":
                        TypeElement = source.Populate(TypeElement);
                        return true;
                    case "enableWhen":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "required":
                        RequiredElement = source.PopulateValue(RequiredElement);
                        return true;
                    case "_required":
                        RequiredElement = source.Populate(RequiredElement);
                        return true;
                    case "repeats":
                        RepeatsElement = source.PopulateValue(RepeatsElement);
                        return true;
                    case "_repeats":
                        RepeatsElement = source.Populate(RepeatsElement);
                        return true;
                    case "readOnly":
                        ReadOnlyElement = source.PopulateValue(ReadOnlyElement);
                        return true;
                    case "_readOnly":
                        ReadOnlyElement = source.Populate(ReadOnlyElement);
                        return true;
                    case "maxLength":
                        MaxLengthElement = source.PopulateValue(MaxLengthElement);
                        return true;
                    case "_maxLength":
                        MaxLengthElement = source.Populate(MaxLengthElement);
                        return true;
                    case "options":
                        Options = source.Populate(Options);
                        return true;
                    case "option":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "initialBoolean":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Initial, "initial");
                        Initial = source.PopulateValue(Initial as Hl7.Fhir.Model.FhirBoolean);
                        return true;
                    case "_initialBoolean":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Initial, "initial");
                        Initial = source.Populate(Initial as Hl7.Fhir.Model.FhirBoolean);
                        return true;
                    case "initialDecimal":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirDecimal>(Initial, "initial");
                        Initial = source.PopulateValue(Initial as Hl7.Fhir.Model.FhirDecimal);
                        return true;
                    case "_initialDecimal":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirDecimal>(Initial, "initial");
                        Initial = source.Populate(Initial as Hl7.Fhir.Model.FhirDecimal);
                        return true;
                    case "initialInteger":
                        source.CheckDuplicates<Hl7.Fhir.Model.Integer>(Initial, "initial");
                        Initial = source.PopulateValue(Initial as Hl7.Fhir.Model.Integer);
                        return true;
                    case "_initialInteger":
                        source.CheckDuplicates<Hl7.Fhir.Model.Integer>(Initial, "initial");
                        Initial = source.Populate(Initial as Hl7.Fhir.Model.Integer);
                        return true;
                    case "initialDate":
                        source.CheckDuplicates<Hl7.Fhir.Model.Date>(Initial, "initial");
                        Initial = source.PopulateValue(Initial as Hl7.Fhir.Model.Date);
                        return true;
                    case "_initialDate":
                        source.CheckDuplicates<Hl7.Fhir.Model.Date>(Initial, "initial");
                        Initial = source.Populate(Initial as Hl7.Fhir.Model.Date);
                        return true;
                    case "initialDateTime":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Initial, "initial");
                        Initial = source.PopulateValue(Initial as Hl7.Fhir.Model.FhirDateTime);
                        return true;
                    case "_initialDateTime":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Initial, "initial");
                        Initial = source.Populate(Initial as Hl7.Fhir.Model.FhirDateTime);
                        return true;
                    case "initialTime":
                        source.CheckDuplicates<Hl7.Fhir.Model.Time>(Initial, "initial");
                        Initial = source.PopulateValue(Initial as Hl7.Fhir.Model.Time);
                        return true;
                    case "_initialTime":
                        source.CheckDuplicates<Hl7.Fhir.Model.Time>(Initial, "initial");
                        Initial = source.Populate(Initial as Hl7.Fhir.Model.Time);
                        return true;
                    case "initialString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Initial, "initial");
                        Initial = source.PopulateValue(Initial as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "_initialString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Initial, "initial");
                        Initial = source.Populate(Initial as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "initialUri":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Initial, "initial");
                        Initial = source.PopulateValue(Initial as Hl7.Fhir.Model.FhirUri);
                        return true;
                    case "_initialUri":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Initial, "initial");
                        Initial = source.Populate(Initial as Hl7.Fhir.Model.FhirUri);
                        return true;
                    case "initialAttachment":
                        source.CheckDuplicates<Hl7.Fhir.Model.Attachment>(Initial, "initial");
                        Initial = source.Populate(Initial as Hl7.Fhir.Model.Attachment);
                        return true;
                    case "initialCoding":
                        source.CheckDuplicates<Hl7.Fhir.Model.Coding>(Initial, "initial");
                        Initial = source.Populate(Initial as Hl7.Fhir.Model.Coding);
                        return true;
                    case "initialQuantity":
                        source.CheckDuplicates<Hl7.Fhir.Model.Quantity>(Initial, "initial");
                        Initial = source.Populate(Initial as Hl7.Fhir.Model.Quantity);
                        return true;
                    case "initialReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Initial, "initial");
                        Initial = source.Populate(Initial as Hl7.Fhir.Model.ResourceReference);
                        return true;
                    case "item":
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
                    case "enableWhen":
                        source.PopulateListItem(EnableWhen, index);
                        return true;
                    case "option":
                        source.PopulateListItem(Option, index);
                        return true;
                    case "item":
                        source.PopulateListItem(Item, index);
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
                    if(LinkIdElement != null) dest.LinkIdElement = (Hl7.Fhir.Model.FhirString)LinkIdElement.DeepCopy();
                    if(DefinitionElement != null) dest.DefinitionElement = (Hl7.Fhir.Model.FhirUri)DefinitionElement.DeepCopy();
                    if(Code != null) dest.Code = new List<Hl7.Fhir.Model.Coding>(Code.DeepCopy());
                    if(PrefixElement != null) dest.PrefixElement = (Hl7.Fhir.Model.FhirString)PrefixElement.DeepCopy();
                    if(TextElement != null) dest.TextElement = (Hl7.Fhir.Model.FhirString)TextElement.DeepCopy();
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.QuestionnaireItemType>)TypeElement.DeepCopy();
                    if(EnableWhen != null) dest.EnableWhen = new List<EnableWhenComponent>(EnableWhen.DeepCopy());
                    if(RequiredElement != null) dest.RequiredElement = (Hl7.Fhir.Model.FhirBoolean)RequiredElement.DeepCopy();
                    if(RepeatsElement != null) dest.RepeatsElement = (Hl7.Fhir.Model.FhirBoolean)RepeatsElement.DeepCopy();
                    if(ReadOnlyElement != null) dest.ReadOnlyElement = (Hl7.Fhir.Model.FhirBoolean)ReadOnlyElement.DeepCopy();
                    if(MaxLengthElement != null) dest.MaxLengthElement = (Hl7.Fhir.Model.Integer)MaxLengthElement.DeepCopy();
                    if(Options != null) dest.Options = (Hl7.Fhir.Model.ResourceReference)Options.DeepCopy();
                    if(Option != null) dest.Option = new List<OptionComponent>(Option.DeepCopy());
                    if(Initial != null) dest.Initial = (Hl7.Fhir.Model.Element)Initial.DeepCopy();
                    if(Item != null) dest.Item = new List<ItemComponent>(Item.DeepCopy());
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
                if( !DeepComparable.Matches(LinkIdElement, otherT.LinkIdElement)) return false;
                if( !DeepComparable.Matches(DefinitionElement, otherT.DefinitionElement)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(PrefixElement, otherT.PrefixElement)) return false;
                if( !DeepComparable.Matches(TextElement, otherT.TextElement)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(EnableWhen, otherT.EnableWhen)) return false;
                if( !DeepComparable.Matches(RequiredElement, otherT.RequiredElement)) return false;
                if( !DeepComparable.Matches(RepeatsElement, otherT.RepeatsElement)) return false;
                if( !DeepComparable.Matches(ReadOnlyElement, otherT.ReadOnlyElement)) return false;
                if( !DeepComparable.Matches(MaxLengthElement, otherT.MaxLengthElement)) return false;
                if( !DeepComparable.Matches(Options, otherT.Options)) return false;
                if( !DeepComparable.Matches(Option, otherT.Option)) return false;
                if( !DeepComparable.Matches(Initial, otherT.Initial)) return false;
                if( !DeepComparable.Matches(Item, otherT.Item)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ItemComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(LinkIdElement, otherT.LinkIdElement)) return false;
                if( !DeepComparable.IsExactly(DefinitionElement, otherT.DefinitionElement)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(PrefixElement, otherT.PrefixElement)) return false;
                if( !DeepComparable.IsExactly(TextElement, otherT.TextElement)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(EnableWhen, otherT.EnableWhen)) return false;
                if( !DeepComparable.IsExactly(RequiredElement, otherT.RequiredElement)) return false;
                if( !DeepComparable.IsExactly(RepeatsElement, otherT.RepeatsElement)) return false;
                if( !DeepComparable.IsExactly(ReadOnlyElement, otherT.ReadOnlyElement)) return false;
                if( !DeepComparable.IsExactly(MaxLengthElement, otherT.MaxLengthElement)) return false;
                if( !DeepComparable.IsExactly(Options, otherT.Options)) return false;
                if( !DeepComparable.IsExactly(Option, otherT.Option)) return false;
                if( !DeepComparable.IsExactly(Initial, otherT.Initial)) return false;
                if( !DeepComparable.IsExactly(Item, otherT.Item)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (LinkIdElement != null) yield return LinkIdElement;
                    if (DefinitionElement != null) yield return DefinitionElement;
                    foreach (var elem in Code) { if (elem != null) yield return elem; }
                    if (PrefixElement != null) yield return PrefixElement;
                    if (TextElement != null) yield return TextElement;
                    if (TypeElement != null) yield return TypeElement;
                    foreach (var elem in EnableWhen) { if (elem != null) yield return elem; }
                    if (RequiredElement != null) yield return RequiredElement;
                    if (RepeatsElement != null) yield return RepeatsElement;
                    if (ReadOnlyElement != null) yield return ReadOnlyElement;
                    if (MaxLengthElement != null) yield return MaxLengthElement;
                    if (Options != null) yield return Options;
                    foreach (var elem in Option) { if (elem != null) yield return elem; }
                    if (Initial != null) yield return Initial;
                    foreach (var elem in Item) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (LinkIdElement != null) yield return new ElementValue("linkId", LinkIdElement);
                    if (DefinitionElement != null) yield return new ElementValue("definition", DefinitionElement);
                    foreach (var elem in Code) { if (elem != null) yield return new ElementValue("code", elem); }
                    if (PrefixElement != null) yield return new ElementValue("prefix", PrefixElement);
                    if (TextElement != null) yield return new ElementValue("text", TextElement);
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                    foreach (var elem in EnableWhen) { if (elem != null) yield return new ElementValue("enableWhen", elem); }
                    if (RequiredElement != null) yield return new ElementValue("required", RequiredElement);
                    if (RepeatsElement != null) yield return new ElementValue("repeats", RepeatsElement);
                    if (ReadOnlyElement != null) yield return new ElementValue("readOnly", ReadOnlyElement);
                    if (MaxLengthElement != null) yield return new ElementValue("maxLength", MaxLengthElement);
                    if (Options != null) yield return new ElementValue("options", Options);
                    foreach (var elem in Option) { if (elem != null) yield return new ElementValue("option", elem); }
                    if (Initial != null) yield return new ElementValue("initial", Initial);
                    foreach (var elem in Item) { if (elem != null) yield return new ElementValue("item", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "EnableWhenComponent")]
        [DataContract]
        public partial class EnableWhenComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "EnableWhenComponent"; } }
            
            /// <summary>
            /// Question that determines whether item is enabled
            /// </summary>
            [FhirElement("question", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString QuestionElement
            {
                get { return _QuestionElement; }
                set { _QuestionElement = value; OnPropertyChanged("QuestionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _QuestionElement;
            
            /// <summary>
            /// Question that determines whether item is enabled
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Question
            {
                get { return QuestionElement != null ? QuestionElement.Value : null; }
                set
                {
                    if (value == null)
                        QuestionElement = null;
                    else
                        QuestionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Question");
                }
            }
            
            /// <summary>
            /// Enable when answered or not
            /// </summary>
            [FhirElement("hasAnswer", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean HasAnswerElement
            {
                get { return _HasAnswerElement; }
                set { _HasAnswerElement = value; OnPropertyChanged("HasAnswerElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _HasAnswerElement;
            
            /// <summary>
            /// Enable when answered or not
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? HasAnswer
            {
                get { return HasAnswerElement != null ? HasAnswerElement.Value : null; }
                set
                {
                    if (value == null)
                        HasAnswerElement = null;
                    else
                        HasAnswerElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("HasAnswer");
                }
            }
            
            /// <summary>
            /// Value question must have
            /// </summary>
            [FhirElement("answer", Order=60, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.Coding),typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Answer
            {
                get { return _Answer; }
                set { _Answer = value; OnPropertyChanged("Answer"); }
            }
            
            private Hl7.Fhir.Model.Element _Answer;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("EnableWhenComponent");
                base.Serialize(sink);
                sink.Element("question", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); QuestionElement?.Serialize(sink);
                sink.Element("hasAnswer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); HasAnswerElement?.Serialize(sink);
                sink.Element("answer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); Answer?.Serialize(sink);
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
                    case "question":
                        QuestionElement = source.PopulateValue(QuestionElement);
                        return true;
                    case "_question":
                        QuestionElement = source.Populate(QuestionElement);
                        return true;
                    case "hasAnswer":
                        HasAnswerElement = source.PopulateValue(HasAnswerElement);
                        return true;
                    case "_hasAnswer":
                        HasAnswerElement = source.Populate(HasAnswerElement);
                        return true;
                    case "answerBoolean":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Answer, "answer");
                        Answer = source.PopulateValue(Answer as Hl7.Fhir.Model.FhirBoolean);
                        return true;
                    case "_answerBoolean":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(Answer, "answer");
                        Answer = source.Populate(Answer as Hl7.Fhir.Model.FhirBoolean);
                        return true;
                    case "answerDecimal":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirDecimal>(Answer, "answer");
                        Answer = source.PopulateValue(Answer as Hl7.Fhir.Model.FhirDecimal);
                        return true;
                    case "_answerDecimal":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirDecimal>(Answer, "answer");
                        Answer = source.Populate(Answer as Hl7.Fhir.Model.FhirDecimal);
                        return true;
                    case "answerInteger":
                        source.CheckDuplicates<Hl7.Fhir.Model.Integer>(Answer, "answer");
                        Answer = source.PopulateValue(Answer as Hl7.Fhir.Model.Integer);
                        return true;
                    case "_answerInteger":
                        source.CheckDuplicates<Hl7.Fhir.Model.Integer>(Answer, "answer");
                        Answer = source.Populate(Answer as Hl7.Fhir.Model.Integer);
                        return true;
                    case "answerDate":
                        source.CheckDuplicates<Hl7.Fhir.Model.Date>(Answer, "answer");
                        Answer = source.PopulateValue(Answer as Hl7.Fhir.Model.Date);
                        return true;
                    case "_answerDate":
                        source.CheckDuplicates<Hl7.Fhir.Model.Date>(Answer, "answer");
                        Answer = source.Populate(Answer as Hl7.Fhir.Model.Date);
                        return true;
                    case "answerDateTime":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Answer, "answer");
                        Answer = source.PopulateValue(Answer as Hl7.Fhir.Model.FhirDateTime);
                        return true;
                    case "_answerDateTime":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Answer, "answer");
                        Answer = source.Populate(Answer as Hl7.Fhir.Model.FhirDateTime);
                        return true;
                    case "answerTime":
                        source.CheckDuplicates<Hl7.Fhir.Model.Time>(Answer, "answer");
                        Answer = source.PopulateValue(Answer as Hl7.Fhir.Model.Time);
                        return true;
                    case "_answerTime":
                        source.CheckDuplicates<Hl7.Fhir.Model.Time>(Answer, "answer");
                        Answer = source.Populate(Answer as Hl7.Fhir.Model.Time);
                        return true;
                    case "answerString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Answer, "answer");
                        Answer = source.PopulateValue(Answer as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "_answerString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Answer, "answer");
                        Answer = source.Populate(Answer as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "answerUri":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Answer, "answer");
                        Answer = source.PopulateValue(Answer as Hl7.Fhir.Model.FhirUri);
                        return true;
                    case "_answerUri":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Answer, "answer");
                        Answer = source.Populate(Answer as Hl7.Fhir.Model.FhirUri);
                        return true;
                    case "answerAttachment":
                        source.CheckDuplicates<Hl7.Fhir.Model.Attachment>(Answer, "answer");
                        Answer = source.Populate(Answer as Hl7.Fhir.Model.Attachment);
                        return true;
                    case "answerCoding":
                        source.CheckDuplicates<Hl7.Fhir.Model.Coding>(Answer, "answer");
                        Answer = source.Populate(Answer as Hl7.Fhir.Model.Coding);
                        return true;
                    case "answerQuantity":
                        source.CheckDuplicates<Hl7.Fhir.Model.Quantity>(Answer, "answer");
                        Answer = source.Populate(Answer as Hl7.Fhir.Model.Quantity);
                        return true;
                    case "answerReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Answer, "answer");
                        Answer = source.Populate(Answer as Hl7.Fhir.Model.ResourceReference);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as EnableWhenComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(QuestionElement != null) dest.QuestionElement = (Hl7.Fhir.Model.FhirString)QuestionElement.DeepCopy();
                    if(HasAnswerElement != null) dest.HasAnswerElement = (Hl7.Fhir.Model.FhirBoolean)HasAnswerElement.DeepCopy();
                    if(Answer != null) dest.Answer = (Hl7.Fhir.Model.Element)Answer.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new EnableWhenComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as EnableWhenComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(QuestionElement, otherT.QuestionElement)) return false;
                if( !DeepComparable.Matches(HasAnswerElement, otherT.HasAnswerElement)) return false;
                if( !DeepComparable.Matches(Answer, otherT.Answer)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as EnableWhenComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(QuestionElement, otherT.QuestionElement)) return false;
                if( !DeepComparable.IsExactly(HasAnswerElement, otherT.HasAnswerElement)) return false;
                if( !DeepComparable.IsExactly(Answer, otherT.Answer)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (QuestionElement != null) yield return QuestionElement;
                    if (HasAnswerElement != null) yield return HasAnswerElement;
                    if (Answer != null) yield return Answer;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (QuestionElement != null) yield return new ElementValue("question", QuestionElement);
                    if (HasAnswerElement != null) yield return new ElementValue("hasAnswer", HasAnswerElement);
                    if (Answer != null) yield return new ElementValue("answer", Answer);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "OptionComponent")]
        [DataContract]
        public partial class OptionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "OptionComponent"; } }
            
            /// <summary>
            /// Answer value
            /// </summary>
            [FhirElement("value", Order=40, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.Coding))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.Element _Value;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("OptionComponent");
                base.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, true); Value?.Serialize(sink);
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
                    case "valueInteger":
                        source.CheckDuplicates<Hl7.Fhir.Model.Integer>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Integer);
                        return true;
                    case "_valueInteger":
                        source.CheckDuplicates<Hl7.Fhir.Model.Integer>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Integer);
                        return true;
                    case "valueDate":
                        source.CheckDuplicates<Hl7.Fhir.Model.Date>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Date);
                        return true;
                    case "_valueDate":
                        source.CheckDuplicates<Hl7.Fhir.Model.Date>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Date);
                        return true;
                    case "valueTime":
                        source.CheckDuplicates<Hl7.Fhir.Model.Time>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.Time);
                        return true;
                    case "_valueTime":
                        source.CheckDuplicates<Hl7.Fhir.Model.Time>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Time);
                        return true;
                    case "valueString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "_valueString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "valueCoding":
                        source.CheckDuplicates<Hl7.Fhir.Model.Coding>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Coding);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as OptionComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new OptionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as OptionComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as OptionComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Value != null) yield return Value;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Value != null) yield return new ElementValue("value", Value);
                }
            }
        
        
        }
    
        
        /// <summary>
        /// Logical URI to reference this questionnaire (globally unique)
        /// </summary>
        [FhirElement("url", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri UrlElement
        {
            get { return _UrlElement; }
            set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _UrlElement;
        
        /// <summary>
        /// Logical URI to reference this questionnaire (globally unique)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Url
        {
            get { return UrlElement != null ? UrlElement.Value : null; }
            set
            {
                if (value == null)
                    UrlElement = null;
                else
                    UrlElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("Url");
            }
        }
        
        /// <summary>
        /// Additional identifier for the questionnaire
        /// </summary>
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
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
        /// Business version of the questionnaire
        /// </summary>
        [FhirElement("version", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement
        {
            get { return _VersionElement; }
            set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _VersionElement;
        
        /// <summary>
        /// Business version of the questionnaire
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Version
        {
            get { return VersionElement != null ? VersionElement.Value : null; }
            set
            {
                if (value == null)
                    VersionElement = null;
                else
                    VersionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Version");
            }
        }
        
        /// <summary>
        /// Name for this questionnaire (computer friendly)
        /// </summary>
        [FhirElement("name", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// Name for this questionnaire (computer friendly)
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
        /// Name for this questionnaire (human friendly)
        /// </summary>
        [FhirElement("title", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString TitleElement
        {
            get { return _TitleElement; }
            set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _TitleElement;
        
        /// <summary>
        /// Name for this questionnaire (human friendly)
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
        /// draft | active | retired | unknown
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.PublicationStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.PublicationStatus> _StatusElement;
        
        /// <summary>
        /// draft | active | retired | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.PublicationStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.PublicationStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// For testing purposes, not real usage
        /// </summary>
        [FhirElement("experimental", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ExperimentalElement
        {
            get { return _ExperimentalElement; }
            set { _ExperimentalElement = value; OnPropertyChanged("ExperimentalElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _ExperimentalElement;
        
        /// <summary>
        /// For testing purposes, not real usage
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Experimental
        {
            get { return ExperimentalElement != null ? ExperimentalElement.Value : null; }
            set
            {
                if (value == null)
                    ExperimentalElement = null;
                else
                    ExperimentalElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Experimental");
            }
        }
        
        /// <summary>
        /// Date this was last changed
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
        /// Date this was last changed
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
        /// Name of the publisher (organization or individual)
        /// </summary>
        [FhirElement("publisher", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PublisherElement
        {
            get { return _PublisherElement; }
            set { _PublisherElement = value; OnPropertyChanged("PublisherElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _PublisherElement;
        
        /// <summary>
        /// Name of the publisher (organization or individual)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Publisher
        {
            get { return PublisherElement != null ? PublisherElement.Value : null; }
            set
            {
                if (value == null)
                    PublisherElement = null;
                else
                    PublisherElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Publisher");
            }
        }
        
        /// <summary>
        /// Natural language description of the questionnaire
        /// </summary>
        [FhirElement("description", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _DescriptionElement;
        
        /// <summary>
        /// Natural language description of the questionnaire
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
                    DescriptionElement = new Hl7.Fhir.Model.Markdown(value);
                OnPropertyChanged("Description");
            }
        }
        
        /// <summary>
        /// Why this questionnaire is defined
        /// </summary>
        [FhirElement("purpose", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown PurposeElement
        {
            get { return _PurposeElement; }
            set { _PurposeElement = value; OnPropertyChanged("PurposeElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _PurposeElement;
        
        /// <summary>
        /// Why this questionnaire is defined
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Purpose
        {
            get { return PurposeElement != null ? PurposeElement.Value : null; }
            set
            {
                if (value == null)
                    PurposeElement = null;
                else
                    PurposeElement = new Hl7.Fhir.Model.Markdown(value);
                OnPropertyChanged("Purpose");
            }
        }
        
        /// <summary>
        /// When the questionnaire was approved by publisher
        /// </summary>
        [FhirElement("approvalDate", Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.Date ApprovalDateElement
        {
            get { return _ApprovalDateElement; }
            set { _ApprovalDateElement = value; OnPropertyChanged("ApprovalDateElement"); }
        }
        
        private Hl7.Fhir.Model.Date _ApprovalDateElement;
        
        /// <summary>
        /// When the questionnaire was approved by publisher
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ApprovalDate
        {
            get { return ApprovalDateElement != null ? ApprovalDateElement.Value : null; }
            set
            {
                if (value == null)
                    ApprovalDateElement = null;
                else
                    ApprovalDateElement = new Hl7.Fhir.Model.Date(value);
                OnPropertyChanged("ApprovalDate");
            }
        }
        
        /// <summary>
        /// When the questionnaire was last reviewed
        /// </summary>
        [FhirElement("lastReviewDate", Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.Date LastReviewDateElement
        {
            get { return _LastReviewDateElement; }
            set { _LastReviewDateElement = value; OnPropertyChanged("LastReviewDateElement"); }
        }
        
        private Hl7.Fhir.Model.Date _LastReviewDateElement;
        
        /// <summary>
        /// When the questionnaire was last reviewed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string LastReviewDate
        {
            get { return LastReviewDateElement != null ? LastReviewDateElement.Value : null; }
            set
            {
                if (value == null)
                    LastReviewDateElement = null;
                else
                    LastReviewDateElement = new Hl7.Fhir.Model.Date(value);
                OnPropertyChanged("LastReviewDate");
            }
        }
        
        /// <summary>
        /// When the questionnaire is expected to be used
        /// </summary>
        [FhirElement("effectivePeriod", InSummary=Hl7.Fhir.Model.Version.All, Order=220)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Period EffectivePeriod
        {
            get { return _EffectivePeriod; }
            set { _EffectivePeriod = value; OnPropertyChanged("EffectivePeriod"); }
        }
        
        private Hl7.Fhir.Model.Period _EffectivePeriod;
        
        /// <summary>
        /// Context the content is intended to support
        /// </summary>
        [FhirElement("useContext", InSummary=Hl7.Fhir.Model.Version.All, Order=230)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.UsageContext> UseContext
        {
            get { if(_UseContext==null) _UseContext = new List<Hl7.Fhir.Model.UsageContext>(); return _UseContext; }
            set { _UseContext = value; OnPropertyChanged("UseContext"); }
        }
        
        private List<Hl7.Fhir.Model.UsageContext> _UseContext;
        
        /// <summary>
        /// Intended jurisdiction for questionnaire (if applicable)
        /// </summary>
        [FhirElement("jurisdiction", InSummary=Hl7.Fhir.Model.Version.All, Order=240)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Jurisdiction
        {
            get { if(_Jurisdiction==null) _Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Jurisdiction; }
            set { _Jurisdiction = value; OnPropertyChanged("Jurisdiction"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Jurisdiction;
        
        /// <summary>
        /// Contact details for the publisher
        /// </summary>
        [FhirElement("contact", InSummary=Hl7.Fhir.Model.Version.All, Order=250)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.STU3.ContactDetail> Contact
        {
            get { if(_Contact==null) _Contact = new List<Hl7.Fhir.Model.STU3.ContactDetail>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<Hl7.Fhir.Model.STU3.ContactDetail> _Contact;
        
        /// <summary>
        /// Use and/or publishing restrictions
        /// </summary>
        [FhirElement("copyright", Order=260)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown CopyrightElement
        {
            get { return _CopyrightElement; }
            set { _CopyrightElement = value; OnPropertyChanged("CopyrightElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _CopyrightElement;
        
        /// <summary>
        /// Use and/or publishing restrictions
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Copyright
        {
            get { return CopyrightElement != null ? CopyrightElement.Value : null; }
            set
            {
                if (value == null)
                    CopyrightElement = null;
                else
                    CopyrightElement = new Hl7.Fhir.Model.Markdown(value);
                OnPropertyChanged("Copyright");
            }
        }
        
        /// <summary>
        /// Concept that represents the overall questionnaire
        /// </summary>
        [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=270)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> Code
        {
            get { if(_Code==null) _Code = new List<Hl7.Fhir.Model.Coding>(); return _Code; }
            set { _Code = value; OnPropertyChanged("Code"); }
        }
        
        private List<Hl7.Fhir.Model.Coding> _Code;
        
        /// <summary>
        /// Resource that can be subject of QuestionnaireResponse
        /// </summary>
        [FhirElement("subjectType", InSummary=Hl7.Fhir.Model.Version.All, Order=280)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Code<Hl7.Fhir.Model.ResourceType>> SubjectTypeElement
        {
            get { if(_SubjectTypeElement==null) _SubjectTypeElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ResourceType>>(); return _SubjectTypeElement; }
            set { _SubjectTypeElement = value; OnPropertyChanged("SubjectTypeElement"); }
        }
        
        private List<Code<Hl7.Fhir.Model.ResourceType>> _SubjectTypeElement;
        
        /// <summary>
        /// Resource that can be subject of QuestionnaireResponse
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<Hl7.Fhir.Model.ResourceType?> SubjectType
        {
            get { return SubjectTypeElement != null ? SubjectTypeElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    SubjectTypeElement = null;
                else
                    SubjectTypeElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ResourceType>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ResourceType>(elem)));
                OnPropertyChanged("SubjectType");
            }
        }
        
        /// <summary>
        /// Questions and sections within the Questionnaire
        /// </summary>
        [FhirElement("item", Order=290)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ItemComponent> Item
        {
            get { if(_Item==null) _Item = new List<ItemComponent>(); return _Item; }
            set { _Item = value; OnPropertyChanged("Item"); }
        }
        
        private List<ItemComponent> _Item;
    
    
        public static ElementDefinitionConstraint[] Questionnaire_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "que-2",
                severity: ConstraintSeverity.Warning,
                expression: "descendants().linkId.isDistinct()",
                human: "The link ids for groups and questions must be unique within the questionnaire",
                xpath: "count(descendant::f:linkId/@value)=count(distinct-values(descendant::f:linkId/@value))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "que-9",
                severity: ConstraintSeverity.Warning,
                expression: "item.all(type!='display' or readOnly.empty())",
                human: "Read-only can't be specified for \"display\" items",
                xpath: "not(f:type/@value='display' and f:readOnly)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "que-8",
                severity: ConstraintSeverity.Warning,
                expression: "item.all((type!='group' and type!='display') or initial.empty())",
                human: "Default values can't be specified for groups or display items",
                xpath: "not(f:type/@value=('group', 'display') and f:*[starts-with(local-name(.), 'initial')])"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "que-6",
                severity: ConstraintSeverity.Warning,
                expression: "item.all(type!='display' or (required.empty() and repeats.empty()))",
                human: "Required and repeat aren't permitted for display items",
                xpath: "not(f:type/@value='display' and (f:required or f:repeats))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "que-5",
                severity: ConstraintSeverity.Warning,
                expression: "item.all((type ='choice' or type = 'open-choice') or (options.empty() and option.empty()))",
                human: "Only 'choice' items can have options",
                xpath: "f:type/@value=('choice','open-choice') or not(f:option or f:options)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "que-4",
                severity: ConstraintSeverity.Warning,
                expression: "item.all(option.empty() or options.empty())",
                human: "A question cannot have both option and options",
                xpath: "not(f:options and f:option)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "que-3",
                severity: ConstraintSeverity.Warning,
                expression: "item.all(type!='display' or code.empty())",
                human: "Display items cannot have a \"code\" asserted",
                xpath: "not(f:type/@value='display' and f:code)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "que-10",
                severity: ConstraintSeverity.Warning,
                expression: "item.all((type in ('boolean' | 'decimal' | 'integer' | 'string' | 'text' | 'url')) or maxLength.empty())",
                human: "Maximum length can only be declared for simple question types",
                xpath: "f:type/@value=('boolean', 'decimal', 'integer', 'open-choice', 'string', 'text', 'url') or not(f:maxLength)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "que-1",
                severity: ConstraintSeverity.Warning,
                expression: "item.all((type='group' implies item.empty().not()) and (type.trace('type')='display' implies item.trace('item').empty()))",
                human: "Group items must have nested items, display items cannot have nested items",
                xpath: "not((f:type/@value='group' and not(f:item)) or (f:type/@value='display' and f:item))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.STU3},
                key: "que-7",
                severity: ConstraintSeverity.Warning,
                expression: "item.enableWhen.all(hasAnswer.exists() xor answer.exists())",
                human: "enableWhen must contain either a 'answer' or a 'hasAnswer' element",
                xpath: "count(f:*[starts-with(local-name(.), 'answer')]|self::f:hasAnswer) = 1"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(Questionnaire_Constraints);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Questionnaire;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.PublicationStatus>)StatusElement.DeepCopy();
                if(ExperimentalElement != null) dest.ExperimentalElement = (Hl7.Fhir.Model.FhirBoolean)ExperimentalElement.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(PublisherElement != null) dest.PublisherElement = (Hl7.Fhir.Model.FhirString)PublisherElement.DeepCopy();
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.Markdown)DescriptionElement.DeepCopy();
                if(PurposeElement != null) dest.PurposeElement = (Hl7.Fhir.Model.Markdown)PurposeElement.DeepCopy();
                if(ApprovalDateElement != null) dest.ApprovalDateElement = (Hl7.Fhir.Model.Date)ApprovalDateElement.DeepCopy();
                if(LastReviewDateElement != null) dest.LastReviewDateElement = (Hl7.Fhir.Model.Date)LastReviewDateElement.DeepCopy();
                if(EffectivePeriod != null) dest.EffectivePeriod = (Hl7.Fhir.Model.Period)EffectivePeriod.DeepCopy();
                if(UseContext != null) dest.UseContext = new List<Hl7.Fhir.Model.UsageContext>(UseContext.DeepCopy());
                if(Jurisdiction != null) dest.Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(Jurisdiction.DeepCopy());
                if(Contact != null) dest.Contact = new List<Hl7.Fhir.Model.STU3.ContactDetail>(Contact.DeepCopy());
                if(CopyrightElement != null) dest.CopyrightElement = (Hl7.Fhir.Model.Markdown)CopyrightElement.DeepCopy();
                if(Code != null) dest.Code = new List<Hl7.Fhir.Model.Coding>(Code.DeepCopy());
                if(SubjectTypeElement != null) dest.SubjectTypeElement = new List<Code<Hl7.Fhir.Model.ResourceType>>(SubjectTypeElement.DeepCopy());
                if(Item != null) dest.Item = new List<ItemComponent>(Item.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new Questionnaire());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Questionnaire;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(PurposeElement, otherT.PurposeElement)) return false;
            if( !DeepComparable.Matches(ApprovalDateElement, otherT.ApprovalDateElement)) return false;
            if( !DeepComparable.Matches(LastReviewDateElement, otherT.LastReviewDateElement)) return false;
            if( !DeepComparable.Matches(EffectivePeriod, otherT.EffectivePeriod)) return false;
            if( !DeepComparable.Matches(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.Matches(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(SubjectTypeElement, otherT.SubjectTypeElement)) return false;
            if( !DeepComparable.Matches(Item, otherT.Item)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Questionnaire;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(PurposeElement, otherT.PurposeElement)) return false;
            if( !DeepComparable.IsExactly(ApprovalDateElement, otherT.ApprovalDateElement)) return false;
            if( !DeepComparable.IsExactly(LastReviewDateElement, otherT.LastReviewDateElement)) return false;
            if( !DeepComparable.IsExactly(EffectivePeriod, otherT.EffectivePeriod)) return false;
            if( !DeepComparable.IsExactly(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.IsExactly(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(SubjectTypeElement, otherT.SubjectTypeElement)) return false;
            if( !DeepComparable.IsExactly(Item, otherT.Item)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("Questionnaire");
            base.Serialize(sink);
            sink.Element("url", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); UrlElement?.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("version", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); VersionElement?.Serialize(sink);
            sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NameElement?.Serialize(sink);
            sink.Element("title", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TitleElement?.Serialize(sink);
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("experimental", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ExperimentalElement?.Serialize(sink);
            sink.Element("date", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DateElement?.Serialize(sink);
            sink.Element("publisher", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PublisherElement?.Serialize(sink);
            sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
            sink.Element("purpose", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PurposeElement?.Serialize(sink);
            sink.Element("approvalDate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ApprovalDateElement?.Serialize(sink);
            sink.Element("lastReviewDate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); LastReviewDateElement?.Serialize(sink);
            sink.Element("effectivePeriod", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); EffectivePeriod?.Serialize(sink);
            sink.BeginList("useContext", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in UseContext)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("jurisdiction", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Jurisdiction)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("contact", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Contact)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("copyright", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CopyrightElement?.Serialize(sink);
            sink.BeginList("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Code)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("subjectType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            sink.Serialize(SubjectTypeElement);
            sink.End();
            sink.BeginList("item", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Item)
            {
                item?.Serialize(sink);
            }
            sink.End();
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
                case "url":
                    UrlElement = source.PopulateValue(UrlElement);
                    return true;
                case "_url":
                    UrlElement = source.Populate(UrlElement);
                    return true;
                case "identifier":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "version":
                    VersionElement = source.PopulateValue(VersionElement);
                    return true;
                case "_version":
                    VersionElement = source.Populate(VersionElement);
                    return true;
                case "name":
                    NameElement = source.PopulateValue(NameElement);
                    return true;
                case "_name":
                    NameElement = source.Populate(NameElement);
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
                case "experimental":
                    ExperimentalElement = source.PopulateValue(ExperimentalElement);
                    return true;
                case "_experimental":
                    ExperimentalElement = source.Populate(ExperimentalElement);
                    return true;
                case "date":
                    DateElement = source.PopulateValue(DateElement);
                    return true;
                case "_date":
                    DateElement = source.Populate(DateElement);
                    return true;
                case "publisher":
                    PublisherElement = source.PopulateValue(PublisherElement);
                    return true;
                case "_publisher":
                    PublisherElement = source.Populate(PublisherElement);
                    return true;
                case "description":
                    DescriptionElement = source.PopulateValue(DescriptionElement);
                    return true;
                case "_description":
                    DescriptionElement = source.Populate(DescriptionElement);
                    return true;
                case "purpose":
                    PurposeElement = source.PopulateValue(PurposeElement);
                    return true;
                case "_purpose":
                    PurposeElement = source.Populate(PurposeElement);
                    return true;
                case "approvalDate":
                    ApprovalDateElement = source.PopulateValue(ApprovalDateElement);
                    return true;
                case "_approvalDate":
                    ApprovalDateElement = source.Populate(ApprovalDateElement);
                    return true;
                case "lastReviewDate":
                    LastReviewDateElement = source.PopulateValue(LastReviewDateElement);
                    return true;
                case "_lastReviewDate":
                    LastReviewDateElement = source.Populate(LastReviewDateElement);
                    return true;
                case "effectivePeriod":
                    EffectivePeriod = source.Populate(EffectivePeriod);
                    return true;
                case "useContext":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "jurisdiction":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "contact":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "copyright":
                    CopyrightElement = source.PopulateValue(CopyrightElement);
                    return true;
                case "_copyright":
                    CopyrightElement = source.Populate(CopyrightElement);
                    return true;
                case "code":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "subjectType":
                case "_subjectType":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "item":
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
                case "useContext":
                    source.PopulateListItem(UseContext, index);
                    return true;
                case "jurisdiction":
                    source.PopulateListItem(Jurisdiction, index);
                    return true;
                case "contact":
                    source.PopulateListItem(Contact, index);
                    return true;
                case "code":
                    source.PopulateListItem(Code, index);
                    return true;
                case "subjectType":
                    source.PopulatePrimitiveListItemValue(SubjectTypeElement, index);
                    return true;
                case "_subjectType":
                    source.PopulatePrimitiveListItem(SubjectTypeElement, index);
                    return true;
                case "item":
                    source.PopulateListItem(Item, index);
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
                if (UrlElement != null) yield return UrlElement;
                foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                if (VersionElement != null) yield return VersionElement;
                if (NameElement != null) yield return NameElement;
                if (TitleElement != null) yield return TitleElement;
                if (StatusElement != null) yield return StatusElement;
                if (ExperimentalElement != null) yield return ExperimentalElement;
                if (DateElement != null) yield return DateElement;
                if (PublisherElement != null) yield return PublisherElement;
                if (DescriptionElement != null) yield return DescriptionElement;
                if (PurposeElement != null) yield return PurposeElement;
                if (ApprovalDateElement != null) yield return ApprovalDateElement;
                if (LastReviewDateElement != null) yield return LastReviewDateElement;
                if (EffectivePeriod != null) yield return EffectivePeriod;
                foreach (var elem in UseContext) { if (elem != null) yield return elem; }
                foreach (var elem in Jurisdiction) { if (elem != null) yield return elem; }
                foreach (var elem in Contact) { if (elem != null) yield return elem; }
                if (CopyrightElement != null) yield return CopyrightElement;
                foreach (var elem in Code) { if (elem != null) yield return elem; }
                foreach (var elem in SubjectTypeElement) { if (elem != null) yield return elem; }
                foreach (var elem in Item) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (UrlElement != null) yield return new ElementValue("url", UrlElement);
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (VersionElement != null) yield return new ElementValue("version", VersionElement);
                if (NameElement != null) yield return new ElementValue("name", NameElement);
                if (TitleElement != null) yield return new ElementValue("title", TitleElement);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (ExperimentalElement != null) yield return new ElementValue("experimental", ExperimentalElement);
                if (DateElement != null) yield return new ElementValue("date", DateElement);
                if (PublisherElement != null) yield return new ElementValue("publisher", PublisherElement);
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                if (PurposeElement != null) yield return new ElementValue("purpose", PurposeElement);
                if (ApprovalDateElement != null) yield return new ElementValue("approvalDate", ApprovalDateElement);
                if (LastReviewDateElement != null) yield return new ElementValue("lastReviewDate", LastReviewDateElement);
                if (EffectivePeriod != null) yield return new ElementValue("effectivePeriod", EffectivePeriod);
                foreach (var elem in UseContext) { if (elem != null) yield return new ElementValue("useContext", elem); }
                foreach (var elem in Jurisdiction) { if (elem != null) yield return new ElementValue("jurisdiction", elem); }
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                if (CopyrightElement != null) yield return new ElementValue("copyright", CopyrightElement);
                foreach (var elem in Code) { if (elem != null) yield return new ElementValue("code", elem); }
                foreach (var elem in SubjectTypeElement) { if (elem != null) yield return new ElementValue("subjectType", elem); }
                foreach (var elem in Item) { if (elem != null) yield return new ElementValue("item", elem); }
            }
        }
    
    }

}
