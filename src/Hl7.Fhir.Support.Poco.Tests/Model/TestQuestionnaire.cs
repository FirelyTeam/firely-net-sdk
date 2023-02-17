// Originally generated from hl7.fhir.r3.core version: 3.0.2

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

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

namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A structured set of questions
    /// </summary>
    [Serializable]
    [DataContract]
    [FhirType("Questionnaire", "http://hl7.org/fhir/StructureDefinition/Questionnaire", IsResource = true)]
    public partial class TestQuestionnaire : Hl7.Fhir.Model.DomainResource
    {
        /// <summary>
        /// FHIR Type Name
        /// </summary>
        public override string TypeName { get { return "Questionnaire"; } }

        /// <summary>
        /// Distinguishes groups from questions and display text and indicates data type for questions
        /// (url: http://hl7.org/fhir/ValueSet/item-type)
        /// (system: http://hl7.org/fhir/item-type)
        /// </summary>
        [FhirEnumeration("QuestionnaireItemType")]
        public enum QuestionnaireItemType
        {
            /// <summary>
            /// An item with no direct answer but should have at least one child item.
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("group", "http://hl7.org/fhir/item-type"), Description("Group")]
            Group,
            /// <summary>
            /// Text for display that will not capture an answer or have child items.
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("display", "http://hl7.org/fhir/item-type"), Description("Display")]
            Display,
            /// <summary>
            /// An item that defines a specific answer to be captured, and may have child items.
            /// (the answer provided in the QuestionnaireResponse should be of the defined datatype)
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("question", "http://hl7.org/fhir/item-type"), Description("Question")]
            Question,
            /// <summary>
            /// Question with a yes/no answer (valueBoolean)
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("boolean", "http://hl7.org/fhir/item-type"), Description("Boolean")]
            Boolean,
            /// <summary>
            /// Question with is a real number answer (valueDecimal)
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("decimal", "http://hl7.org/fhir/item-type"), Description("Decimal")]
            Decimal,
            /// <summary>
            /// Question with an integer answer (valueInteger)
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("integer", "http://hl7.org/fhir/item-type"), Description("Integer")]
            Integer,
            /// <summary>
            /// Question with a date answer (valueDate)
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("date", "http://hl7.org/fhir/item-type"), Description("Date")]
            Date,
            /// <summary>
            /// Question with a date and time answer (valueDateTime)
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("dateTime", "http://hl7.org/fhir/item-type"), Description("Date Time")]
            DateTime,
            /// <summary>
            /// Question with a time (hour:minute:second) answer independent of date. (valueTime)
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("time", "http://hl7.org/fhir/item-type"), Description("Time")]
            Time,
            /// <summary>
            /// Question with a short (few words to short sentence) free-text entry answer (valueString)
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("string", "http://hl7.org/fhir/item-type"), Description("String")]
            String,
            /// <summary>
            /// Question with a long (potentially multi-paragraph) free-text entry answer (valueString)
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("text", "http://hl7.org/fhir/item-type"), Description("Text")]
            Text,
            /// <summary>
            /// Question with a URL (website, FTP site, etc.) answer (valueUri)
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("url", "http://hl7.org/fhir/item-type"), Description("Url")]
            Url,
            /// <summary>
            /// Question with a Coding drawn from a list of options (specified in either the option property, or via the valueset referenced in the options property) as an answer (valueCoding)
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("choice", "http://hl7.org/fhir/item-type"), Description("Choice")]
            Choice,
            /// <summary>
            /// Answer is a Coding drawn from a list of options (as with the choice type) or a free-text entry in a string (valueCoding or valueString)
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("open-choice", "http://hl7.org/fhir/item-type"), Description("Open Choice")]
            OpenChoice,
            /// <summary>
            /// Question with binary content such as a image, PDF, etc. as an answer (valueAttachment)
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("attachment", "http://hl7.org/fhir/item-type"), Description("Attachment")]
            Attachment,
            /// <summary>
            /// Question with a reference to another resource (practitioner, organization, etc.) as an answer (valueReference)
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("reference", "http://hl7.org/fhir/item-type"), Description("Reference")]
            Reference,
            /// <summary>
            /// Question with a combination of a numeric value and unit, potentially with a comparator (&lt;, &gt;, etc.) as an answer. (valueQuantity)
            /// There is an extension 'http://hl7.org/fhir/StructureDefinition/questionnaire-unit' that can be used to define what unit whould be captured (or the a unit that has a ucum conversion from the provided unit)
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("quantity", "http://hl7.org/fhir/item-type"), Description("Quantity")]
            Quantity,
        }

        /// <summary>
        /// Questions and sections within the Questionnaire
        /// </summary>
        [Serializable]
        [DataContract]
        [FhirType("Questionnaire#Item", IsNestedType = true)]
        public partial class ItemComponent : Hl7.Fhir.Model.BackboneElement
        {
            /// <summary>
            /// FHIR Type Name
            /// </summary>
            public override string TypeName { get { return "Questionnaire#Item"; } }

            /// <summary>
            /// Unique id for item in questionnaire
            /// </summary>
            [FhirElement("linkId", Order = 40)]
            [Cardinality(Min = 1, Max = 1)]
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
            [IgnoreDataMember]
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
            [FhirElement("definition", Order = 50)]
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
            [IgnoreDataMember]
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
            [FhirElement("code", InSummary = true, Order = 60)]
            [Cardinality(Min = 0, Max = -1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> Code
            {
                get { if (_Code == null) _Code = new List<Hl7.Fhir.Model.Coding>(); return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }

            private List<Hl7.Fhir.Model.Coding> _Code;

            /// <summary>
            /// E.g. "1(a)", "2.5.3"
            /// </summary>
            [FhirElement("prefix", Order = 70)]
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
            [IgnoreDataMember]
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
            [FhirElement("text", InSummary = true, Order = 80)]
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
            [IgnoreDataMember]
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
            [FhirElement("type", Order = 90)]
            [DeclaredType(Type = typeof(Code))]
            [Cardinality(Min = 1, Max = 1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.TestQuestionnaire.QuestionnaireItemType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }

            private Code<Hl7.Fhir.Model.TestQuestionnaire.QuestionnaireItemType> _TypeElement;

            /// <summary>
            /// group | display | boolean | decimal | integer | date | dateTime +
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [IgnoreDataMember]
            public Hl7.Fhir.Model.TestQuestionnaire.QuestionnaireItemType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (value == null)
                        TypeElement = null;
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.TestQuestionnaire.QuestionnaireItemType>(value);
                    OnPropertyChanged("Type");
                }
            }

            /// <summary>
            /// Only allow data when
            /// </summary>
            [FhirElement("enableWhen", InSummary = true, Order = 100)]
            [Cardinality(Min = 0, Max = -1)]
            [DataMember]
            public List<Hl7.Fhir.Model.TestQuestionnaire.EnableWhenComponent> EnableWhen
            {
                get { if (_EnableWhen == null) _EnableWhen = new List<Hl7.Fhir.Model.TestQuestionnaire.EnableWhenComponent>(); return _EnableWhen; }
                set { _EnableWhen = value; OnPropertyChanged("EnableWhen"); }
            }

            private List<Hl7.Fhir.Model.TestQuestionnaire.EnableWhenComponent> _EnableWhen;

            /// <summary>
            /// Whether the item must be included in data results
            /// </summary>
            [FhirElement("required", Order = 110)]
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
            [IgnoreDataMember]
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
            [FhirElement("repeats", Order = 120)]
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
            [IgnoreDataMember]
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
            [FhirElement("readOnly", Order = 130)]
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
            [IgnoreDataMember]
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
            [FhirElement("maxLength", Order = 140)]
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
            [IgnoreDataMember]
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
            [FhirElement("options", Order = 150)]
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
            [FhirElement("option", Order = 160)]
            [Cardinality(Min = 0, Max = -1)]
            [DataMember]
            public List<Hl7.Fhir.Model.TestQuestionnaire.OptionComponent> Option
            {
                get { if (_Option == null) _Option = new List<Hl7.Fhir.Model.TestQuestionnaire.OptionComponent>(); return _Option; }
                set { _Option = value; OnPropertyChanged("Option"); }
            }

            private List<Hl7.Fhir.Model.TestQuestionnaire.OptionComponent> _Option;

            /// <summary>
            /// Default value when item is first rendered
            /// </summary>
            [FhirElement("initial", Order = 170, Choice = ChoiceType.DatatypeChoice)]
            [References("Resource")]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean), typeof(Hl7.Fhir.Model.FhirDecimal), typeof(Hl7.Fhir.Model.Integer), typeof(Hl7.Fhir.Model.Date), typeof(Hl7.Fhir.Model.FhirDateTime), typeof(Hl7.Fhir.Model.Time), typeof(Hl7.Fhir.Model.FhirString), typeof(Hl7.Fhir.Model.FhirUri), typeof(Hl7.Fhir.Model.TestAttachment), typeof(Hl7.Fhir.Model.Coding), typeof(Hl7.Fhir.Model.Quantity), typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.DataType Initial
            {
                get { return _Initial; }
                set { _Initial = value; OnPropertyChanged("Initial"); }
            }

            private Hl7.Fhir.Model.DataType _Initial;

            /// <summary>
            /// Nested questionnaire items
            /// </summary>
            [FhirElement("item", Order = 180)]
            [Cardinality(Min = 0, Max = -1)]
            [DataMember]
            public List<Hl7.Fhir.Model.TestQuestionnaire.ItemComponent> Item
            {
                get { if (_Item == null) _Item = new List<Hl7.Fhir.Model.TestQuestionnaire.ItemComponent>(); return _Item; }
                set { _Item = value; OnPropertyChanged("Item"); }
            }

            private List<Hl7.Fhir.Model.TestQuestionnaire.ItemComponent> _Item;

            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ItemComponent;

                if (dest == null)
                {
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
                }

                base.CopyTo(dest);
                if (LinkIdElement != null) dest.LinkIdElement = (Hl7.Fhir.Model.FhirString)LinkIdElement.DeepCopy();
                if (DefinitionElement != null) dest.DefinitionElement = (Hl7.Fhir.Model.FhirUri)DefinitionElement.DeepCopy();
                if (Code != null) dest.Code = new List<Hl7.Fhir.Model.Coding>(Code.DeepCopy());
                if (PrefixElement != null) dest.PrefixElement = (Hl7.Fhir.Model.FhirString)PrefixElement.DeepCopy();
                if (TextElement != null) dest.TextElement = (Hl7.Fhir.Model.FhirString)TextElement.DeepCopy();
                if (TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.TestQuestionnaire.QuestionnaireItemType>)TypeElement.DeepCopy();
                if (EnableWhen != null) dest.EnableWhen = new List<Hl7.Fhir.Model.TestQuestionnaire.EnableWhenComponent>(EnableWhen.DeepCopy());
                if (RequiredElement != null) dest.RequiredElement = (Hl7.Fhir.Model.FhirBoolean)RequiredElement.DeepCopy();
                if (RepeatsElement != null) dest.RepeatsElement = (Hl7.Fhir.Model.FhirBoolean)RepeatsElement.DeepCopy();
                if (ReadOnlyElement != null) dest.ReadOnlyElement = (Hl7.Fhir.Model.FhirBoolean)ReadOnlyElement.DeepCopy();
                if (MaxLengthElement != null) dest.MaxLengthElement = (Hl7.Fhir.Model.Integer)MaxLengthElement.DeepCopy();
                if (Options != null) dest.Options = (Hl7.Fhir.Model.ResourceReference)Options.DeepCopy();
                if (Option != null) dest.Option = new List<Hl7.Fhir.Model.TestQuestionnaire.OptionComponent>(Option.DeepCopy());
                if (Initial != null) dest.Initial = (Hl7.Fhir.Model.DataType)Initial.DeepCopy();
                if (Item != null) dest.Item = new List<Hl7.Fhir.Model.TestQuestionnaire.ItemComponent>(Item.DeepCopy());
                return dest;
            }

            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ItemComponent());
            }

            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ItemComponent;
                if (otherT == null) return false;

                if (!base.Matches(otherT)) return false;
                if (!DeepComparable.Matches(LinkIdElement, otherT.LinkIdElement)) return false;
                if (!DeepComparable.Matches(DefinitionElement, otherT.DefinitionElement)) return false;
                if (!DeepComparable.Matches(Code, otherT.Code)) return false;
                if (!DeepComparable.Matches(PrefixElement, otherT.PrefixElement)) return false;
                if (!DeepComparable.Matches(TextElement, otherT.TextElement)) return false;
                if (!DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if (!DeepComparable.Matches(EnableWhen, otherT.EnableWhen)) return false;
                if (!DeepComparable.Matches(RequiredElement, otherT.RequiredElement)) return false;
                if (!DeepComparable.Matches(RepeatsElement, otherT.RepeatsElement)) return false;
                if (!DeepComparable.Matches(ReadOnlyElement, otherT.ReadOnlyElement)) return false;
                if (!DeepComparable.Matches(MaxLengthElement, otherT.MaxLengthElement)) return false;
                if (!DeepComparable.Matches(Options, otherT.Options)) return false;
                if (!DeepComparable.Matches(Option, otherT.Option)) return false;
                if (!DeepComparable.Matches(Initial, otherT.Initial)) return false;
                if (!DeepComparable.Matches(Item, otherT.Item)) return false;

                return true;
            }

            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ItemComponent;
                if (otherT == null) return false;

                if (!base.IsExactly(otherT)) return false;
                if (!DeepComparable.IsExactly(LinkIdElement, otherT.LinkIdElement)) return false;
                if (!DeepComparable.IsExactly(DefinitionElement, otherT.DefinitionElement)) return false;
                if (!DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if (!DeepComparable.IsExactly(PrefixElement, otherT.PrefixElement)) return false;
                if (!DeepComparable.IsExactly(TextElement, otherT.TextElement)) return false;
                if (!DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if (!DeepComparable.IsExactly(EnableWhen, otherT.EnableWhen)) return false;
                if (!DeepComparable.IsExactly(RequiredElement, otherT.RequiredElement)) return false;
                if (!DeepComparable.IsExactly(RepeatsElement, otherT.RepeatsElement)) return false;
                if (!DeepComparable.IsExactly(ReadOnlyElement, otherT.ReadOnlyElement)) return false;
                if (!DeepComparable.IsExactly(MaxLengthElement, otherT.MaxLengthElement)) return false;
                if (!DeepComparable.IsExactly(Options, otherT.Options)) return false;
                if (!DeepComparable.IsExactly(Option, otherT.Option)) return false;
                if (!DeepComparable.IsExactly(Initial, otherT.Initial)) return false;
                if (!DeepComparable.IsExactly(Item, otherT.Item)) return false;

                return true;
            }

            [IgnoreDataMember]
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

            [IgnoreDataMember]
            public override IEnumerable<ElementValue> NamedChildren
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

            protected override bool TryGetValue(string key, out object value)
            {
                switch (key)
                {
                    case "linkId":
                        value = LinkIdElement;
                        return LinkIdElement is not null;
                    case "definition":
                        value = DefinitionElement;
                        return DefinitionElement is not null;
                    case "code":
                        value = Code;
                        return Code?.Any() == true;
                    case "prefix":
                        value = PrefixElement;
                        return PrefixElement is not null;
                    case "text":
                        value = TextElement;
                        return TextElement is not null;
                    case "type":
                        value = TypeElement;
                        return TypeElement is not null;
                    case "enableWhen":
                        value = EnableWhen;
                        return EnableWhen?.Any() == true;
                    case "required":
                        value = RequiredElement;
                        return RequiredElement is not null;
                    case "repeats":
                        value = RepeatsElement;
                        return RepeatsElement is not null;
                    case "readOnly":
                        value = ReadOnlyElement;
                        return ReadOnlyElement is not null;
                    case "maxLength":
                        value = MaxLengthElement;
                        return MaxLengthElement is not null;
                    case "options":
                        value = Options;
                        return Options is not null;
                    case "option":
                        value = Option;
                        return Option?.Any() == true;
                    case "initial":
                        value = Initial;
                        return Initial is not null;
                    case "item":
                        value = Item;
                        return Item?.Any() == true;
                    default:
                        return base.TryGetValue(key, out value);
                };

            }

            protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
            {
                foreach (var kvp in base.GetElementPairs()) yield return kvp;
                if (LinkIdElement is not null) yield return new KeyValuePair<string, object>("linkId", LinkIdElement);
                if (DefinitionElement is not null) yield return new KeyValuePair<string, object>("definition", DefinitionElement);
                if (Code?.Any() == true) yield return new KeyValuePair<string, object>("code", Code);
                if (PrefixElement is not null) yield return new KeyValuePair<string, object>("prefix", PrefixElement);
                if (TextElement is not null) yield return new KeyValuePair<string, object>("text", TextElement);
                if (TypeElement is not null) yield return new KeyValuePair<string, object>("type", TypeElement);
                if (EnableWhen?.Any() == true) yield return new KeyValuePair<string, object>("enableWhen", EnableWhen);
                if (RequiredElement is not null) yield return new KeyValuePair<string, object>("required", RequiredElement);
                if (RepeatsElement is not null) yield return new KeyValuePair<string, object>("repeats", RepeatsElement);
                if (ReadOnlyElement is not null) yield return new KeyValuePair<string, object>("readOnly", ReadOnlyElement);
                if (MaxLengthElement is not null) yield return new KeyValuePair<string, object>("maxLength", MaxLengthElement);
                if (Options is not null) yield return new KeyValuePair<string, object>("options", Options);
                if (Option?.Any() == true) yield return new KeyValuePair<string, object>("option", Option);
                if (Initial is not null) yield return new KeyValuePair<string, object>("initial", Initial);
                if (Item?.Any() == true) yield return new KeyValuePair<string, object>("item", Item);
            }

        }

        /// <summary>
        /// Only allow data when
        /// </summary>
        [Serializable]
        [DataContract]
        [FhirType("Questionnaire#EnableWhen", IsNestedType = true)]
        public partial class EnableWhenComponent : Hl7.Fhir.Model.BackboneElement
        {
            /// <summary>
            /// FHIR Type Name
            /// </summary>
            public override string TypeName { get { return "Questionnaire#EnableWhen"; } }

            /// <summary>
            /// Question that determines whether item is enabled
            /// </summary>
            [FhirElement("question", Order = 40)]
            [Cardinality(Min = 1, Max = 1)]
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
            [IgnoreDataMember]
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
            [FhirElement("hasAnswer", Order = 50)]
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
            [IgnoreDataMember]
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
            [FhirElement("answer", Order = 60, Choice = ChoiceType.DatatypeChoice)]
            [References("Resource")]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean), typeof(Hl7.Fhir.Model.FhirDecimal), typeof(Hl7.Fhir.Model.Integer), typeof(Hl7.Fhir.Model.Date),
                typeof(Hl7.Fhir.Model.FhirDateTime), typeof(Hl7.Fhir.Model.Time), typeof(Hl7.Fhir.Model.FhirString), typeof(Hl7.Fhir.Model.FhirUri),
                typeof(Hl7.Fhir.Model.TestAttachment), typeof(Hl7.Fhir.Model.Coding), typeof(Hl7.Fhir.Model.Quantity), typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.DataType Answer
            {
                get { return _Answer; }
                set { _Answer = value; OnPropertyChanged("Answer"); }
            }

            private Hl7.Fhir.Model.DataType _Answer;

            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as EnableWhenComponent;

                if (dest == null)
                {
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
                }

                base.CopyTo(dest);
                if (QuestionElement != null) dest.QuestionElement = (Hl7.Fhir.Model.FhirString)QuestionElement.DeepCopy();
                if (HasAnswerElement != null) dest.HasAnswerElement = (Hl7.Fhir.Model.FhirBoolean)HasAnswerElement.DeepCopy();
                if (Answer != null) dest.Answer = (Hl7.Fhir.Model.DataType)Answer.DeepCopy();
                return dest;
            }

            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new EnableWhenComponent());
            }

            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as EnableWhenComponent;
                if (otherT == null) return false;

                if (!base.Matches(otherT)) return false;
                if (!DeepComparable.Matches(QuestionElement, otherT.QuestionElement)) return false;
                if (!DeepComparable.Matches(HasAnswerElement, otherT.HasAnswerElement)) return false;
                if (!DeepComparable.Matches(Answer, otherT.Answer)) return false;

                return true;
            }

            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as EnableWhenComponent;
                if (otherT == null) return false;

                if (!base.IsExactly(otherT)) return false;
                if (!DeepComparable.IsExactly(QuestionElement, otherT.QuestionElement)) return false;
                if (!DeepComparable.IsExactly(HasAnswerElement, otherT.HasAnswerElement)) return false;
                if (!DeepComparable.IsExactly(Answer, otherT.Answer)) return false;

                return true;
            }

            [IgnoreDataMember]
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

            [IgnoreDataMember]
            public override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (QuestionElement != null) yield return new ElementValue("question", QuestionElement);
                    if (HasAnswerElement != null) yield return new ElementValue("hasAnswer", HasAnswerElement);
                    if (Answer != null) yield return new ElementValue("answer", Answer);
                }
            }

            protected override bool TryGetValue(string key, out object value)
            {
                switch (key)
                {
                    case "question":
                        value = QuestionElement;
                        return QuestionElement is not null;
                    case "hasAnswer":
                        value = HasAnswerElement;
                        return HasAnswerElement is not null;
                    case "answer":
                        value = Answer;
                        return Answer is not null;
                    default:
                        return base.TryGetValue(key, out value);
                };

            }

            protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
            {
                foreach (var kvp in base.GetElementPairs()) yield return kvp;
                if (QuestionElement is not null) yield return new KeyValuePair<string, object>("question", QuestionElement);
                if (HasAnswerElement is not null) yield return new KeyValuePair<string, object>("hasAnswer", HasAnswerElement);
                if (Answer is not null) yield return new KeyValuePair<string, object>("answer", Answer);
            }

        }

        /// <summary>
        /// Permitted answer
        /// </summary>
        [Serializable]
        [DataContract]
        [FhirType("Questionnaire#Option", IsNestedType = true)]
        public partial class OptionComponent : Hl7.Fhir.Model.BackboneElement
        {
            /// <summary>
            /// FHIR Type Name
            /// </summary>
            public override string TypeName { get { return "Questionnaire#Option"; } }

            /// <summary>
            /// Answer value
            /// </summary>
            [FhirElement("value", Order = 40, Choice = ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Integer), typeof(Hl7.Fhir.Model.Date), typeof(Hl7.Fhir.Model.Time), typeof(Hl7.Fhir.Model.FhirString), typeof(Hl7.Fhir.Model.Coding))]
            [Cardinality(Min = 1, Max = 1)]
            [DataMember]
            public Hl7.Fhir.Model.DataType Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }

            private Hl7.Fhir.Model.DataType _Value;

            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as OptionComponent;

                if (dest == null)
                {
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
                }

                base.CopyTo(dest);
                if (Value != null) dest.Value = (Hl7.Fhir.Model.DataType)Value.DeepCopy();
                return dest;
            }

            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new OptionComponent());
            }

            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as OptionComponent;
                if (otherT == null) return false;

                if (!base.Matches(otherT)) return false;
                if (!DeepComparable.Matches(Value, otherT.Value)) return false;

                return true;
            }

            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as OptionComponent;
                if (otherT == null) return false;

                if (!base.IsExactly(otherT)) return false;
                if (!DeepComparable.IsExactly(Value, otherT.Value)) return false;

                return true;
            }

            [IgnoreDataMember]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Value != null) yield return Value;
                }
            }

            [IgnoreDataMember]
            public override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Value != null) yield return new ElementValue("value", Value);
                }
            }

            protected override bool TryGetValue(string key, out object value)
            {
                switch (key)
                {
                    case "value":
                        value = Value;
                        return Value is not null;
                    default:
                        return base.TryGetValue(key, out value);
                };

            }

            protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
            {
                foreach (var kvp in base.GetElementPairs()) yield return kvp;
                if (Value is not null) yield return new KeyValuePair<string, object>("value", Value);
            }

        }

        /// <summary>
        /// Logical URI to reference this questionnaire (globally unique)
        /// </summary>
        [FhirElement("url", InSummary = true, Order = 90)]
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
        [IgnoreDataMember]
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
        [FhirElement("identifier", InSummary = true, Order = 100)]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if (_Identifier == null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }

        private List<Hl7.Fhir.Model.Identifier> _Identifier;

        /// <summary>
        /// Business version of the questionnaire
        /// </summary>
        [FhirElement("version", InSummary = true, Order = 110)]
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
        [IgnoreDataMember]
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
        [FhirElement("name", InSummary = true, Order = 120)]
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
        [IgnoreDataMember]
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
        [FhirElement("title", InSummary = true, Order = 130)]
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
        [IgnoreDataMember]
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
        [FhirElement("status", InSummary = true, Order = 140)]
        [DeclaredType(Type = typeof(Code))]
        [Cardinality(Min = 1, Max = 1)]
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
        [IgnoreDataMember]
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
        [FhirElement("experimental", InSummary = true, Order = 150)]
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
        [IgnoreDataMember]
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
        [FhirElement("date", InSummary = true, Order = 160)]
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
        [IgnoreDataMember]
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
        [FhirElement("publisher", InSummary = true, Order = 170)]
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
        [IgnoreDataMember]
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
        [FhirElement("description", Order = 180)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown Description
        {
            get { return _Description; }
            set { _Description = value; OnPropertyChanged("Description"); }
        }

        private Hl7.Fhir.Model.Markdown _Description;

        /// <summary>
        /// Why this questionnaire is defined
        /// </summary>
        [FhirElement("purpose", Order = 190)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown Purpose
        {
            get { return _Purpose; }
            set { _Purpose = value; OnPropertyChanged("Purpose"); }
        }

        private Hl7.Fhir.Model.Markdown _Purpose;

        /// <summary>
        /// When the questionnaire was approved by publisher
        /// </summary>
        [FhirElement("approvalDate", Order = 200)]
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
        [IgnoreDataMember]
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
        [FhirElement("lastReviewDate", Order = 210)]
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
        [IgnoreDataMember]
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
        [FhirElement("effectivePeriod", InSummary = true, Order = 220)]
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
        [FhirElement("useContext", InSummary = true, Order = 230)]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.UsageContext> UseContext
        {
            get { if (_UseContext == null) _UseContext = new List<Hl7.Fhir.Model.UsageContext>(); return _UseContext; }
            set { _UseContext = value; OnPropertyChanged("UseContext"); }
        }

        private List<Hl7.Fhir.Model.UsageContext> _UseContext;

        /// <summary>
        /// Intended jurisdiction for questionnaire (if applicable)
        /// </summary>
        [FhirElement("jurisdiction", InSummary = true, Order = 240)]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Jurisdiction
        {
            get { if (_Jurisdiction == null) _Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Jurisdiction; }
            set { _Jurisdiction = value; OnPropertyChanged("Jurisdiction"); }
        }

        private List<Hl7.Fhir.Model.CodeableConcept> _Jurisdiction;

        /// <summary>
        /// Contact details for the publisher
        /// </summary>
        [FhirElement("contact", InSummary = true, Order = 250)]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ContactDetail> Contact
        {
            get { if (_Contact == null) _Contact = new List<Hl7.Fhir.Model.ContactDetail>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }

        private List<Hl7.Fhir.Model.ContactDetail> _Contact;

        /// <summary>
        /// Use and/or publishing restrictions
        /// </summary>
        [FhirElement("copyright", Order = 260)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown Copyright
        {
            get { return _Copyright; }
            set { _Copyright = value; OnPropertyChanged("Copyright"); }
        }

        private Hl7.Fhir.Model.Markdown _Copyright;

        /// <summary>
        /// Concept that represents the overall questionnaire
        /// </summary>
        [FhirElement("code", InSummary = true, Order = 270)]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> Code
        {
            get { if (_Code == null) _Code = new List<Hl7.Fhir.Model.Coding>(); return _Code; }
            set { _Code = value; OnPropertyChanged("Code"); }
        }

        private List<Hl7.Fhir.Model.Coding> _Code;

        /// <summary>
        /// Resource that can be subject of QuestionnaireResponse
        /// </summary>
        [FhirElement("subjectType", InSummary = true, Order = 280)]
        [DeclaredType(Type = typeof(Code))]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Code<Hl7.Fhir.Model.TestResourceType>> SubjectTypeElement
        {
            get { if (_SubjectTypeElement == null) _SubjectTypeElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.TestResourceType>>(); return _SubjectTypeElement; }
            set { _SubjectTypeElement = value; OnPropertyChanged("SubjectTypeElement"); }
        }

        private List<Code<Hl7.Fhir.Model.TestResourceType>> _SubjectTypeElement;

        /// <summary>
        /// Resource that can be subject of QuestionnaireResponse
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [IgnoreDataMember]
        public IEnumerable<Hl7.Fhir.Model.TestResourceType?> SubjectType
        {
            get { return SubjectTypeElement != null ? SubjectTypeElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    SubjectTypeElement = null;
                else
                    SubjectTypeElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.TestResourceType>>(value.Select(elem => new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.TestResourceType>(elem)));
                OnPropertyChanged("SubjectType");
            }
        }

        /// <summary>
        /// Questions and sections within the Questionnaire
        /// </summary>
        [FhirElement("item", Order = 290)]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.TestQuestionnaire.ItemComponent> Item
        {
            get { if (_Item == null) _Item = new List<Hl7.Fhir.Model.TestQuestionnaire.ItemComponent>(); return _Item; }
            set { _Item = value; OnPropertyChanged("Item"); }
        }

        private List<Hl7.Fhir.Model.TestQuestionnaire.ItemComponent> _Item;

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as TestQuestionnaire;

            if (dest == null)
            {
                throw new ArgumentException("Can only copy to an object of the same type", "other");
            }

            base.CopyTo(dest);
            if (UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
            if (Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
            if (VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
            if (NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
            if (TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
            if (StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.PublicationStatus>)StatusElement.DeepCopy();
            if (ExperimentalElement != null) dest.ExperimentalElement = (Hl7.Fhir.Model.FhirBoolean)ExperimentalElement.DeepCopy();
            if (DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
            if (PublisherElement != null) dest.PublisherElement = (Hl7.Fhir.Model.FhirString)PublisherElement.DeepCopy();
            if (Description != null) dest.Description = (Hl7.Fhir.Model.Markdown)Description.DeepCopy();
            if (Purpose != null) dest.Purpose = (Hl7.Fhir.Model.Markdown)Purpose.DeepCopy();
            if (ApprovalDateElement != null) dest.ApprovalDateElement = (Hl7.Fhir.Model.Date)ApprovalDateElement.DeepCopy();
            if (LastReviewDateElement != null) dest.LastReviewDateElement = (Hl7.Fhir.Model.Date)LastReviewDateElement.DeepCopy();
            if (EffectivePeriod != null) dest.EffectivePeriod = (Hl7.Fhir.Model.Period)EffectivePeriod.DeepCopy();
            if (UseContext != null) dest.UseContext = new List<Hl7.Fhir.Model.UsageContext>(UseContext.DeepCopy());
            if (Jurisdiction != null) dest.Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(Jurisdiction.DeepCopy());
            if (Contact != null) dest.Contact = new List<Hl7.Fhir.Model.ContactDetail>(Contact.DeepCopy());
            if (Copyright != null) dest.Copyright = (Hl7.Fhir.Model.Markdown)Copyright.DeepCopy();
            if (Code != null) dest.Code = new List<Hl7.Fhir.Model.Coding>(Code.DeepCopy());
            if (SubjectTypeElement != null) dest.SubjectTypeElement = new List<Code<Hl7.Fhir.Model.TestResourceType>>(SubjectTypeElement.DeepCopy());
            if (Item != null) dest.Item = new List<Hl7.Fhir.Model.TestQuestionnaire.ItemComponent>(Item.DeepCopy());
            return dest;
        }

        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new TestQuestionnaire());
        }

        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as TestQuestionnaire;
            if (otherT == null) return false;

            if (!base.Matches(otherT)) return false;
            if (!DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
            if (!DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if (!DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if (!DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if (!DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
            if (!DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if (!DeepComparable.Matches(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if (!DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if (!DeepComparable.Matches(PublisherElement, otherT.PublisherElement)) return false;
            if (!DeepComparable.Matches(Description, otherT.Description)) return false;
            if (!DeepComparable.Matches(Purpose, otherT.Purpose)) return false;
            if (!DeepComparable.Matches(ApprovalDateElement, otherT.ApprovalDateElement)) return false;
            if (!DeepComparable.Matches(LastReviewDateElement, otherT.LastReviewDateElement)) return false;
            if (!DeepComparable.Matches(EffectivePeriod, otherT.EffectivePeriod)) return false;
            if (!DeepComparable.Matches(UseContext, otherT.UseContext)) return false;
            if (!DeepComparable.Matches(Jurisdiction, otherT.Jurisdiction)) return false;
            if (!DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if (!DeepComparable.Matches(Copyright, otherT.Copyright)) return false;
            if (!DeepComparable.Matches(Code, otherT.Code)) return false;
            if (!DeepComparable.Matches(SubjectTypeElement, otherT.SubjectTypeElement)) return false;
            if (!DeepComparable.Matches(Item, otherT.Item)) return false;

            return true;
        }

        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as TestQuestionnaire;
            if (otherT == null) return false;

            if (!base.IsExactly(otherT)) return false;
            if (!DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
            if (!DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if (!DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if (!DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if (!DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
            if (!DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if (!DeepComparable.IsExactly(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if (!DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if (!DeepComparable.IsExactly(PublisherElement, otherT.PublisherElement)) return false;
            if (!DeepComparable.IsExactly(Description, otherT.Description)) return false;
            if (!DeepComparable.IsExactly(Purpose, otherT.Purpose)) return false;
            if (!DeepComparable.IsExactly(ApprovalDateElement, otherT.ApprovalDateElement)) return false;
            if (!DeepComparable.IsExactly(LastReviewDateElement, otherT.LastReviewDateElement)) return false;
            if (!DeepComparable.IsExactly(EffectivePeriod, otherT.EffectivePeriod)) return false;
            if (!DeepComparable.IsExactly(UseContext, otherT.UseContext)) return false;
            if (!DeepComparable.IsExactly(Jurisdiction, otherT.Jurisdiction)) return false;
            if (!DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if (!DeepComparable.IsExactly(Copyright, otherT.Copyright)) return false;
            if (!DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if (!DeepComparable.IsExactly(SubjectTypeElement, otherT.SubjectTypeElement)) return false;
            if (!DeepComparable.IsExactly(Item, otherT.Item)) return false;

            return true;
        }

        [IgnoreDataMember]
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
                if (Description != null) yield return Description;
                if (Purpose != null) yield return Purpose;
                if (ApprovalDateElement != null) yield return ApprovalDateElement;
                if (LastReviewDateElement != null) yield return LastReviewDateElement;
                if (EffectivePeriod != null) yield return EffectivePeriod;
                foreach (var elem in UseContext) { if (elem != null) yield return elem; }
                foreach (var elem in Jurisdiction) { if (elem != null) yield return elem; }
                foreach (var elem in Contact) { if (elem != null) yield return elem; }
                if (Copyright != null) yield return Copyright;
                foreach (var elem in Code) { if (elem != null) yield return elem; }
                foreach (var elem in SubjectTypeElement) { if (elem != null) yield return elem; }
                foreach (var elem in Item) { if (elem != null) yield return elem; }
            }
        }

        [IgnoreDataMember]
        public override IEnumerable<ElementValue> NamedChildren
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
                if (Description != null) yield return new ElementValue("description", Description);
                if (Purpose != null) yield return new ElementValue("purpose", Purpose);
                if (ApprovalDateElement != null) yield return new ElementValue("approvalDate", ApprovalDateElement);
                if (LastReviewDateElement != null) yield return new ElementValue("lastReviewDate", LastReviewDateElement);
                if (EffectivePeriod != null) yield return new ElementValue("effectivePeriod", EffectivePeriod);
                foreach (var elem in UseContext) { if (elem != null) yield return new ElementValue("useContext", elem); }
                foreach (var elem in Jurisdiction) { if (elem != null) yield return new ElementValue("jurisdiction", elem); }
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                if (Copyright != null) yield return new ElementValue("copyright", Copyright);
                foreach (var elem in Code) { if (elem != null) yield return new ElementValue("code", elem); }
                foreach (var elem in SubjectTypeElement) { if (elem != null) yield return new ElementValue("subjectType", elem); }
                foreach (var elem in Item) { if (elem != null) yield return new ElementValue("item", elem); }
            }
        }

        protected override bool TryGetValue(string key, out object value)
        {
            switch (key)
            {
                case "url":
                    value = UrlElement;
                    return UrlElement is not null;
                case "identifier":
                    value = Identifier;
                    return Identifier?.Any() == true;
                case "version":
                    value = VersionElement;
                    return VersionElement is not null;
                case "name":
                    value = NameElement;
                    return NameElement is not null;
                case "title":
                    value = TitleElement;
                    return TitleElement is not null;
                case "status":
                    value = StatusElement;
                    return StatusElement is not null;
                case "experimental":
                    value = ExperimentalElement;
                    return ExperimentalElement is not null;
                case "date":
                    value = DateElement;
                    return DateElement is not null;
                case "publisher":
                    value = PublisherElement;
                    return PublisherElement is not null;
                case "description":
                    value = Description;
                    return Description is not null;
                case "purpose":
                    value = Purpose;
                    return Purpose is not null;
                case "approvalDate":
                    value = ApprovalDateElement;
                    return ApprovalDateElement is not null;
                case "lastReviewDate":
                    value = LastReviewDateElement;
                    return LastReviewDateElement is not null;
                case "effectivePeriod":
                    value = EffectivePeriod;
                    return EffectivePeriod is not null;
                case "useContext":
                    value = UseContext;
                    return UseContext?.Any() == true;
                case "jurisdiction":
                    value = Jurisdiction;
                    return Jurisdiction?.Any() == true;
                case "contact":
                    value = Contact;
                    return Contact?.Any() == true;
                case "copyright":
                    value = Copyright;
                    return Copyright is not null;
                case "code":
                    value = Code;
                    return Code?.Any() == true;
                case "subjectType":
                    value = SubjectTypeElement;
                    return SubjectTypeElement?.Any() == true;
                case "item":
                    value = Item;
                    return Item?.Any() == true;
                default:
                    return base.TryGetValue(key, out value);
            };

        }

        protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
        {
            foreach (var kvp in base.GetElementPairs()) yield return kvp;
            if (UrlElement is not null) yield return new KeyValuePair<string, object>("url", UrlElement);
            if (Identifier?.Any() == true) yield return new KeyValuePair<string, object>("identifier", Identifier);
            if (VersionElement is not null) yield return new KeyValuePair<string, object>("version", VersionElement);
            if (NameElement is not null) yield return new KeyValuePair<string, object>("name", NameElement);
            if (TitleElement is not null) yield return new KeyValuePair<string, object>("title", TitleElement);
            if (StatusElement is not null) yield return new KeyValuePair<string, object>("status", StatusElement);
            if (ExperimentalElement is not null) yield return new KeyValuePair<string, object>("experimental", ExperimentalElement);
            if (DateElement is not null) yield return new KeyValuePair<string, object>("date", DateElement);
            if (PublisherElement is not null) yield return new KeyValuePair<string, object>("publisher", PublisherElement);
            if (Description is not null) yield return new KeyValuePair<string, object>("description", Description);
            if (Purpose is not null) yield return new KeyValuePair<string, object>("purpose", Purpose);
            if (ApprovalDateElement is not null) yield return new KeyValuePair<string, object>("approvalDate", ApprovalDateElement);
            if (LastReviewDateElement is not null) yield return new KeyValuePair<string, object>("lastReviewDate", LastReviewDateElement);
            if (EffectivePeriod is not null) yield return new KeyValuePair<string, object>("effectivePeriod", EffectivePeriod);
            if (UseContext?.Any() == true) yield return new KeyValuePair<string, object>("useContext", UseContext);
            if (Jurisdiction?.Any() == true) yield return new KeyValuePair<string, object>("jurisdiction", Jurisdiction);
            if (Contact?.Any() == true) yield return new KeyValuePair<string, object>("contact", Contact);
            if (Copyright is not null) yield return new KeyValuePair<string, object>("copyright", Copyright);
            if (Code?.Any() == true) yield return new KeyValuePair<string, object>("code", Code);
            if (SubjectTypeElement?.Any() == true) yield return new KeyValuePair<string, object>("subjectType", SubjectTypeElement);
            if (Item?.Any() == true) yield return new KeyValuePair<string, object>("item", Item);
        }

    }

}

// end of file
