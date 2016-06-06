using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.ComponentModel;

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

//
// Generated for FHIR v1.4.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A structured set of questions
    /// </summary>
    [FhirType("Questionnaire", IsResource=true)]
    [DataContract]
    public partial class Questionnaire : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Questionnaire; } }
        [NotMapped]
        public override string TypeName { get { return "Questionnaire"; } }
        
        /// <summary>
        /// Lifecycle status of the questionnaire.
        /// (url: http://hl7.org/fhir/ValueSet/questionnaire-status)
        /// </summary>
        [FhirEnumeration("QuestionnaireStatus")]
        public enum QuestionnaireStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/questionnaire-status)
            /// </summary>
            [EnumLiteral("draft"), Description("Draft")]
            Draft,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/questionnaire-status)
            /// </summary>
            [EnumLiteral("published"), Description("Published")]
            Published,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/questionnaire-status)
            /// </summary>
            [EnumLiteral("retired"), Description("Retired")]
            Retired,
        }

        /// <summary>
        /// Distinguishes groups from questions and display text and indicates data type for questions
        /// (url: http://hl7.org/fhir/ValueSet/item-type)
        /// </summary>
        [FhirEnumeration("QuestionnaireItemType")]
        public enum QuestionnaireItemType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("group"), Description("Group")]
            Group,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("display"), Description("Display")]
            Display,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("boolean"), Description("Boolean")]
            Boolean,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("decimal"), Description("Decimal")]
            Decimal,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("integer"), Description("Integer")]
            Integer,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("date"), Description("Date")]
            Date,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("dateTime"), Description("Date Time")]
            DateTime,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("instant"), Description("Instant")]
            Instant,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("time"), Description("Time")]
            Time,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("string"), Description("String")]
            String,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("text"), Description("Text")]
            Text,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("url"), Description("Url")]
            Url,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("choice"), Description("Choice")]
            Choice,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("open-choice"), Description("Open Choice")]
            OpenChoice,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("attachment"), Description("Attachment")]
            Attachment,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("reference"), Description("Reference")]
            Reference,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/item-type)
            /// </summary>
            [EnumLiteral("quantity"), Description("Quantity")]
            Quantity,
        }

        [FhirType("ItemComponent")]
        [DataContract]
        public partial class ItemComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ItemComponent"; } }
            
            /// <summary>
            /// To link questionnaire with questionnaire response
            /// </summary>
            [FhirElement("linkId", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString LinkIdElement
            {
                get { return _LinkIdElement; }
                set { _LinkIdElement = value; OnPropertyChanged("LinkIdElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _LinkIdElement;
            
            /// <summary>
            /// To link questionnaire with questionnaire response
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string LinkId
            {
                get { return LinkIdElement != null ? LinkIdElement.Value : null; }
                set
                {
                    if(value == null)
                      LinkIdElement = null; 
                    else
                      LinkIdElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("LinkId");
                }
            }
            
            /// <summary>
            /// Concept that represents this item within in a questionnaire
            /// </summary>
            [FhirElement("concept", InSummary=true, Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> Concept
            {
                get { if(_Concept==null) _Concept = new List<Hl7.Fhir.Model.Coding>(); return _Concept; }
                set { _Concept = value; OnPropertyChanged("Concept"); }
            }
            
            private List<Hl7.Fhir.Model.Coding> _Concept;
            
            /// <summary>
            /// E.g. "1(a)", "2.5.3"
            /// </summary>
            [FhirElement("prefix", Order=60)]
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
                    if(value == null)
                      PrefixElement = null; 
                    else
                      PrefixElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Prefix");
                }
            }
            
            /// <summary>
            /// Primary text for the item
            /// </summary>
            [FhirElement("text", InSummary=true, Order=70)]
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
                    if(value == null)
                      TextElement = null; 
                    else
                      TextElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Text");
                }
            }
            
            /// <summary>
            /// group | display | boolean | decimal | integer | date | dateTime +
            /// </summary>
            [FhirElement("type", Order=80)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Questionnaire.QuestionnaireItemType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.Questionnaire.QuestionnaireItemType> _TypeElement;
            
            /// <summary>
            /// group | display | boolean | decimal | integer | date | dateTime +
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Questionnaire.QuestionnaireItemType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if(value == null)
                      TypeElement = null; 
                    else
                      TypeElement = new Code<Hl7.Fhir.Model.Questionnaire.QuestionnaireItemType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Only allow data when:
            /// </summary>
            [FhirElement("enableWhen", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Questionnaire.EnableWhenComponent> EnableWhen
            {
                get { if(_EnableWhen==null) _EnableWhen = new List<Hl7.Fhir.Model.Questionnaire.EnableWhenComponent>(); return _EnableWhen; }
                set { _EnableWhen = value; OnPropertyChanged("EnableWhen"); }
            }
            
            private List<Hl7.Fhir.Model.Questionnaire.EnableWhenComponent> _EnableWhen;
            
            /// <summary>
            /// Whether the item must be included in data results
            /// </summary>
            [FhirElement("required", Order=100)]
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
                    if(value == null)
                      RequiredElement = null; 
                    else
                      RequiredElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Required");
                }
            }
            
            /// <summary>
            /// Whether the item may repeat
            /// </summary>
            [FhirElement("repeats", Order=110)]
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
                    if(value == null)
                      RepeatsElement = null; 
                    else
                      RepeatsElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Repeats");
                }
            }
            
            /// <summary>
            /// Don't allow human editing
            /// </summary>
            [FhirElement("readOnly", Order=120)]
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
                    if(value == null)
                      ReadOnlyElement = null; 
                    else
                      ReadOnlyElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("ReadOnly");
                }
            }
            
            /// <summary>
            /// No more than this many characters
            /// </summary>
            [FhirElement("maxLength", Order=130)]
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
                    if(value == null)
                      MaxLengthElement = null; 
                    else
                      MaxLengthElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("MaxLength");
                }
            }
            
            /// <summary>
            /// Valueset containing permitted answers
            /// </summary>
            [FhirElement("options", Order=140)]
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
            [FhirElement("option", Order=150)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Questionnaire.OptionComponent> Option
            {
                get { if(_Option==null) _Option = new List<Hl7.Fhir.Model.Questionnaire.OptionComponent>(); return _Option; }
                set { _Option = value; OnPropertyChanged("Option"); }
            }
            
            private List<Hl7.Fhir.Model.Questionnaire.OptionComponent> _Option;
            
            /// <summary>
            /// Initial presumed answer for question
            /// </summary>
            [FhirElement("initial", Order=160, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Instant),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.Coding),typeof(Quantity),typeof(Hl7.Fhir.Model.ResourceReference))]
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
            [FhirElement("item", Order=170)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Questionnaire.ItemComponent> Item
            {
                get { if(_Item==null) _Item = new List<Hl7.Fhir.Model.Questionnaire.ItemComponent>(); return _Item; }
                set { _Item = value; OnPropertyChanged("Item"); }
            }
            
            private List<Hl7.Fhir.Model.Questionnaire.ItemComponent> _Item;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ItemComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(LinkIdElement != null) dest.LinkIdElement = (Hl7.Fhir.Model.FhirString)LinkIdElement.DeepCopy();
                    if(Concept != null) dest.Concept = new List<Hl7.Fhir.Model.Coding>(Concept.DeepCopy());
                    if(PrefixElement != null) dest.PrefixElement = (Hl7.Fhir.Model.FhirString)PrefixElement.DeepCopy();
                    if(TextElement != null) dest.TextElement = (Hl7.Fhir.Model.FhirString)TextElement.DeepCopy();
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.Questionnaire.QuestionnaireItemType>)TypeElement.DeepCopy();
                    if(EnableWhen != null) dest.EnableWhen = new List<Hl7.Fhir.Model.Questionnaire.EnableWhenComponent>(EnableWhen.DeepCopy());
                    if(RequiredElement != null) dest.RequiredElement = (Hl7.Fhir.Model.FhirBoolean)RequiredElement.DeepCopy();
                    if(RepeatsElement != null) dest.RepeatsElement = (Hl7.Fhir.Model.FhirBoolean)RepeatsElement.DeepCopy();
                    if(ReadOnlyElement != null) dest.ReadOnlyElement = (Hl7.Fhir.Model.FhirBoolean)ReadOnlyElement.DeepCopy();
                    if(MaxLengthElement != null) dest.MaxLengthElement = (Hl7.Fhir.Model.Integer)MaxLengthElement.DeepCopy();
                    if(Options != null) dest.Options = (Hl7.Fhir.Model.ResourceReference)Options.DeepCopy();
                    if(Option != null) dest.Option = new List<Hl7.Fhir.Model.Questionnaire.OptionComponent>(Option.DeepCopy());
                    if(Initial != null) dest.Initial = (Hl7.Fhir.Model.Element)Initial.DeepCopy();
                    if(Item != null) dest.Item = new List<Hl7.Fhir.Model.Questionnaire.ItemComponent>(Item.DeepCopy());
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
                if( !DeepComparable.Matches(Concept, otherT.Concept)) return false;
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
                if( !DeepComparable.IsExactly(Concept, otherT.Concept)) return false;
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
            
        }
        
        
        [FhirType("EnableWhenComponent")]
        [DataContract]
        public partial class EnableWhenComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
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
                    if(value == null)
                      QuestionElement = null; 
                    else
                      QuestionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Question");
                }
            }
            
            /// <summary>
            /// Enable when answered or not
            /// </summary>
            [FhirElement("answered", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean AnsweredElement
            {
                get { return _AnsweredElement; }
                set { _AnsweredElement = value; OnPropertyChanged("AnsweredElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _AnsweredElement;
            
            /// <summary>
            /// Enable when answered or not
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Answered
            {
                get { return AnsweredElement != null ? AnsweredElement.Value : null; }
                set
                {
                    if(value == null)
                      AnsweredElement = null; 
                    else
                      AnsweredElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Answered");
                }
            }
            
            /// <summary>
            /// Value question must have
            /// </summary>
            [FhirElement("answer", Order=60, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Instant),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.Coding),typeof(Quantity),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Answer
            {
                get { return _Answer; }
                set { _Answer = value; OnPropertyChanged("Answer"); }
            }
            
            private Hl7.Fhir.Model.Element _Answer;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as EnableWhenComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(QuestionElement != null) dest.QuestionElement = (Hl7.Fhir.Model.FhirString)QuestionElement.DeepCopy();
                    if(AnsweredElement != null) dest.AnsweredElement = (Hl7.Fhir.Model.FhirBoolean)AnsweredElement.DeepCopy();
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
                if( !DeepComparable.Matches(AnsweredElement, otherT.AnsweredElement)) return false;
                if( !DeepComparable.Matches(Answer, otherT.Answer)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as EnableWhenComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(QuestionElement, otherT.QuestionElement)) return false;
                if( !DeepComparable.IsExactly(AnsweredElement, otherT.AnsweredElement)) return false;
                if( !DeepComparable.IsExactly(Answer, otherT.Answer)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("OptionComponent")]
        [DataContract]
        public partial class OptionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "OptionComponent"; } }
            
            /// <summary>
            /// Answer value
            /// </summary>
            [FhirElement("value", Order=40, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.Coding))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.Element _Value;
            
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
            
        }
        
        
        /// <summary>
        /// Globally unique logical identifier for  questionnaire
        /// </summary>
        [FhirElement("url", InSummary=true, Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri UrlElement
        {
            get { return _UrlElement; }
            set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _UrlElement;
        
        /// <summary>
        /// Globally unique logical identifier for  questionnaire
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Url
        {
            get { return UrlElement != null ? UrlElement.Value : null; }
            set
            {
                if(value == null)
                  UrlElement = null; 
                else
                  UrlElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("Url");
            }
        }
        
        /// <summary>
        /// External identifiers for this questionnaire
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=100)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Logical identifier for this version of Questionnaire
        /// </summary>
        [FhirElement("version", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement
        {
            get { return _VersionElement; }
            set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _VersionElement;
        
        /// <summary>
        /// Logical identifier for this version of Questionnaire
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Version
        {
            get { return VersionElement != null ? VersionElement.Value : null; }
            set
            {
                if(value == null)
                  VersionElement = null; 
                else
                  VersionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Version");
            }
        }
        
        /// <summary>
        /// draft | published | retired
        /// </summary>
        [FhirElement("status", InSummary=true, Order=120)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Questionnaire.QuestionnaireStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Questionnaire.QuestionnaireStatus> _StatusElement;
        
        /// <summary>
        /// draft | published | retired
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Questionnaire.QuestionnaireStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.Questionnaire.QuestionnaireStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Date this version was authored
        /// </summary>
        [FhirElement("date", InSummary=true, Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// Date this version was authored
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Date
        {
            get { return DateElement != null ? DateElement.Value : null; }
            set
            {
                if(value == null)
                  DateElement = null; 
                else
                  DateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Date");
            }
        }
        
        /// <summary>
        /// Organization/individual who designed the questionnaire
        /// </summary>
        [FhirElement("publisher", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PublisherElement
        {
            get { return _PublisherElement; }
            set { _PublisherElement = value; OnPropertyChanged("PublisherElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _PublisherElement;
        
        /// <summary>
        /// Organization/individual who designed the questionnaire
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Publisher
        {
            get { return PublisherElement != null ? PublisherElement.Value : null; }
            set
            {
                if(value == null)
                  PublisherElement = null; 
                else
                  PublisherElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Publisher");
            }
        }
        
        /// <summary>
        /// Contact information of the publisher
        /// </summary>
        [FhirElement("telecom", InSummary=true, Order=150)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ContactPoint> Telecom
        {
            get { if(_Telecom==null) _Telecom = new List<Hl7.Fhir.Model.ContactPoint>(); return _Telecom; }
            set { _Telecom = value; OnPropertyChanged("Telecom"); }
        }
        
        private List<Hl7.Fhir.Model.ContactPoint> _Telecom;
        
        /// <summary>
        /// Questionnaire intends to support these contexts
        /// </summary>
        [FhirElement("useContext", InSummary=true, Order=160)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> UseContext
        {
            get { if(_UseContext==null) _UseContext = new List<Hl7.Fhir.Model.CodeableConcept>(); return _UseContext; }
            set { _UseContext = value; OnPropertyChanged("UseContext"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _UseContext;
        
        /// <summary>
        /// Name for the questionnaire
        /// </summary>
        [FhirElement("title", InSummary=true, Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString TitleElement
        {
            get { return _TitleElement; }
            set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _TitleElement;
        
        /// <summary>
        /// Name for the questionnaire
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Title
        {
            get { return TitleElement != null ? TitleElement.Value : null; }
            set
            {
                if(value == null)
                  TitleElement = null; 
                else
                  TitleElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Title");
            }
        }
        
        /// <summary>
        /// Concept that represents the overall questionnaire
        /// </summary>
        [FhirElement("concept", InSummary=true, Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> Concept
        {
            get { if(_Concept==null) _Concept = new List<Hl7.Fhir.Model.Coding>(); return _Concept; }
            set { _Concept = value; OnPropertyChanged("Concept"); }
        }
        
        private List<Hl7.Fhir.Model.Coding> _Concept;
        
        /// <summary>
        /// Resource that can be subject of QuestionnaireResponse
        /// </summary>
        [FhirElement("subjectType", InSummary=true, Order=190)]
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
                if(value == null)
                  SubjectTypeElement = null; 
                else
                  SubjectTypeElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ResourceType>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.ResourceType>(elem)));
                OnPropertyChanged("SubjectType");
            }
        }
        
        /// <summary>
        /// Questions and sections within the Questionnaire
        /// </summary>
        [FhirElement("item", Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Questionnaire.ItemComponent> Item
        {
            get { if(_Item==null) _Item = new List<Hl7.Fhir.Model.Questionnaire.ItemComponent>(); return _Item; }
            set { _Item = value; OnPropertyChanged("Item"); }
        }
        
        private List<Hl7.Fhir.Model.Questionnaire.ItemComponent> _Item;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Questionnaire;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Questionnaire.QuestionnaireStatus>)StatusElement.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(PublisherElement != null) dest.PublisherElement = (Hl7.Fhir.Model.FhirString)PublisherElement.DeepCopy();
                if(Telecom != null) dest.Telecom = new List<Hl7.Fhir.Model.ContactPoint>(Telecom.DeepCopy());
                if(UseContext != null) dest.UseContext = new List<Hl7.Fhir.Model.CodeableConcept>(UseContext.DeepCopy());
                if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                if(Concept != null) dest.Concept = new List<Hl7.Fhir.Model.Coding>(Concept.DeepCopy());
                if(SubjectTypeElement != null) dest.SubjectTypeElement = new List<Code<Hl7.Fhir.Model.ResourceType>>(SubjectTypeElement.DeepCopy());
                if(Item != null) dest.Item = new List<Hl7.Fhir.Model.Questionnaire.ItemComponent>(Item.DeepCopy());
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
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.Matches(Telecom, otherT.Telecom)) return false;
            if( !DeepComparable.Matches(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.Matches(Concept, otherT.Concept)) return false;
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
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.IsExactly(Telecom, otherT.Telecom)) return false;
            if( !DeepComparable.IsExactly(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.IsExactly(Concept, otherT.Concept)) return false;
            if( !DeepComparable.IsExactly(SubjectTypeElement, otherT.SubjectTypeElement)) return false;
            if( !DeepComparable.IsExactly(Item, otherT.Item)) return false;
            
            return true;
        }
        
    }
    
}
