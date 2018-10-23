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
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A structured set of questions and their answers
    /// </summary>
    [FhirType("QuestionnaireResponse", IsResource=true)]
    [DataContract]
    public partial class QuestionnaireResponse : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.QuestionnaireResponse; } }
        [NotMapped]
        public override string TypeName { get { return "QuestionnaireResponse"; } }
        
        /// <summary>
        /// Lifecycle status of the questionnaire response.
        /// (url: http://hl7.org/fhir/ValueSet/questionnaire-answers-status)
        /// </summary>
        [FhirEnumeration("QuestionnaireResponseStatus")]
        public enum QuestionnaireResponseStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/questionnaire-answers-status)
            /// </summary>
            [EnumLiteral("in-progress", "http://hl7.org/fhir/questionnaire-answers-status"), Description("In Progress")]
            InProgress,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/questionnaire-answers-status)
            /// </summary>
            [EnumLiteral("completed", "http://hl7.org/fhir/questionnaire-answers-status"), Description("Completed")]
            Completed,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/questionnaire-answers-status)
            /// </summary>
            [EnumLiteral("amended", "http://hl7.org/fhir/questionnaire-answers-status"), Description("Amended")]
            Amended,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/questionnaire-answers-status)
            /// </summary>
            [EnumLiteral("entered-in-error", "http://hl7.org/fhir/questionnaire-answers-status"), Description("Entered in Error")]
            EnteredInError,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/questionnaire-answers-status)
            /// </summary>
            [EnumLiteral("stopped", "http://hl7.org/fhir/questionnaire-answers-status"), Description("Stopped")]
            Stopped,
        }

        [FhirType("ItemComponent")]
        [DataContract]
        public partial class ItemComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ItemComponent"; } }
            
            /// <summary>
            /// Pointer to specific item from Questionnaire
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
            /// Pointer to specific item from Questionnaire
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
            /// Name for group or question text
            /// </summary>
            [FhirElement("text", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TextElement
            {
                get { return _TextElement; }
                set { _TextElement = value; OnPropertyChanged("TextElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TextElement;
            
            /// <summary>
            /// Name for group or question text
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
            /// The subject this group's answers are about
            /// </summary>
            [FhirElement("subject", Order=70)]
            [CLSCompliant(false)]
			[References()]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Subject
            {
                get { return _Subject; }
                set { _Subject = value; OnPropertyChanged("Subject"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Subject;
            
            /// <summary>
            /// The response(s) to the question
            /// </summary>
            [FhirElement("answer", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.QuestionnaireResponse.AnswerComponent> Answer
            {
                get { if(_Answer==null) _Answer = new List<Hl7.Fhir.Model.QuestionnaireResponse.AnswerComponent>(); return _Answer; }
                set { _Answer = value; OnPropertyChanged("Answer"); }
            }
            
            private List<Hl7.Fhir.Model.QuestionnaireResponse.AnswerComponent> _Answer;
            
            /// <summary>
            /// Nested questionnaire response items
            /// </summary>
            [FhirElement("item", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.QuestionnaireResponse.ItemComponent> Item
            {
                get { if(_Item==null) _Item = new List<Hl7.Fhir.Model.QuestionnaireResponse.ItemComponent>(); return _Item; }
                set { _Item = value; OnPropertyChanged("Item"); }
            }
            
            private List<Hl7.Fhir.Model.QuestionnaireResponse.ItemComponent> _Item;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ItemComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(LinkIdElement != null) dest.LinkIdElement = (Hl7.Fhir.Model.FhirString)LinkIdElement.DeepCopy();
                    if(DefinitionElement != null) dest.DefinitionElement = (Hl7.Fhir.Model.FhirUri)DefinitionElement.DeepCopy();
                    if(TextElement != null) dest.TextElement = (Hl7.Fhir.Model.FhirString)TextElement.DeepCopy();
                    if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                    if(Answer != null) dest.Answer = new List<Hl7.Fhir.Model.QuestionnaireResponse.AnswerComponent>(Answer.DeepCopy());
                    if(Item != null) dest.Item = new List<Hl7.Fhir.Model.QuestionnaireResponse.ItemComponent>(Item.DeepCopy());
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
                if( !DeepComparable.Matches(TextElement, otherT.TextElement)) return false;
                if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
                if( !DeepComparable.Matches(Answer, otherT.Answer)) return false;
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
                if( !DeepComparable.IsExactly(TextElement, otherT.TextElement)) return false;
                if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
                if( !DeepComparable.IsExactly(Answer, otherT.Answer)) return false;
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
                    if (TextElement != null) yield return TextElement;
                    if (Subject != null) yield return Subject;
                    foreach (var elem in Answer) { if (elem != null) yield return elem; }
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
                    if (TextElement != null) yield return new ElementValue("text", TextElement);
                    if (Subject != null) yield return new ElementValue("subject", Subject);
                    foreach (var elem in Answer) { if (elem != null) yield return new ElementValue("answer", elem); }
                    foreach (var elem in Item) { if (elem != null) yield return new ElementValue("item", elem); }
                }
            }

            
        }
        
        
        [FhirType("AnswerComponent")]
        [DataContract]
        public partial class AnswerComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "AnswerComponent"; } }
            
            /// <summary>
            /// Single-valued answer to the question
            /// </summary>
            [FhirElement("value", Order=40, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.Coding),typeof(Quantity),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.Element _Value;
            
            /// <summary>
            /// Nested groups and questions
            /// </summary>
            [FhirElement("item", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.QuestionnaireResponse.ItemComponent> Item
            {
                get { if(_Item==null) _Item = new List<Hl7.Fhir.Model.QuestionnaireResponse.ItemComponent>(); return _Item; }
                set { _Item = value; OnPropertyChanged("Item"); }
            }
            
            private List<Hl7.Fhir.Model.QuestionnaireResponse.ItemComponent> _Item;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AnswerComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
                    if(Item != null) dest.Item = new List<Hl7.Fhir.Model.QuestionnaireResponse.ItemComponent>(Item.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new AnswerComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AnswerComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
                if( !DeepComparable.Matches(Item, otherT.Item)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AnswerComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                if( !DeepComparable.IsExactly(Item, otherT.Item)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Value != null) yield return Value;
                    foreach (var elem in Item) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Value != null) yield return new ElementValue("value", Value);
                    foreach (var elem in Item) { if (elem != null) yield return new ElementValue("item", elem); }
                }
            }

            
        }
        
        
        /// <summary>
        /// Unique id for this set of answers
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// Request fulfilled by this QuestionnaireResponse
        /// </summary>
        [FhirElement("basedOn", InSummary=true, Order=100)]
        [CLSCompliant(false)]
		[References("ReferralRequest","CarePlan","ProcedureRequest")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> BasedOn
        {
            get { if(_BasedOn==null) _BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(); return _BasedOn; }
            set { _BasedOn = value; OnPropertyChanged("BasedOn"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _BasedOn;
        
        /// <summary>
        /// Part of this action
        /// </summary>
        [FhirElement("parent", InSummary=true, Order=110)]
        [CLSCompliant(false)]
		[References("Observation","Procedure")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Parent
        {
            get { if(_Parent==null) _Parent = new List<Hl7.Fhir.Model.ResourceReference>(); return _Parent; }
            set { _Parent = value; OnPropertyChanged("Parent"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Parent;
        
        /// <summary>
        /// Form being answered
        /// </summary>
        [FhirElement("questionnaire", InSummary=true, Order=120)]
        [CLSCompliant(false)]
		[References("Questionnaire")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Questionnaire
        {
            get { return _Questionnaire; }
            set { _Questionnaire = value; OnPropertyChanged("Questionnaire"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Questionnaire;
        
        /// <summary>
        /// in-progress | completed | amended | entered-in-error | stopped
        /// </summary>
        [FhirElement("status", InSummary=true, Order=130)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.QuestionnaireResponse.QuestionnaireResponseStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.QuestionnaireResponse.QuestionnaireResponseStatus> _StatusElement;
        
        /// <summary>
        /// in-progress | completed | amended | entered-in-error | stopped
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.QuestionnaireResponse.QuestionnaireResponseStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.QuestionnaireResponse.QuestionnaireResponseStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// The subject of the questions
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=140)]
        [CLSCompliant(false)]
		[References()]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Encounter or Episode during which questionnaire was completed
        /// </summary>
        [FhirElement("context", InSummary=true, Order=150)]
        [CLSCompliant(false)]
		[References("Encounter","EpisodeOfCare")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Context
        {
            get { return _Context; }
            set { _Context = value; OnPropertyChanged("Context"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Context;
        
        /// <summary>
        /// Date the answers were gathered
        /// </summary>
        [FhirElement("authored", InSummary=true, Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime AuthoredElement
        {
            get { return _AuthoredElement; }
            set { _AuthoredElement = value; OnPropertyChanged("AuthoredElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _AuthoredElement;
        
        /// <summary>
        /// Date the answers were gathered
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Authored
        {
            get { return AuthoredElement != null ? AuthoredElement.Value : null; }
            set
            {
                if (value == null)
                  AuthoredElement = null; 
                else
                  AuthoredElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Authored");
            }
        }
        
        /// <summary>
        /// Person who received and recorded the answers
        /// </summary>
        [FhirElement("author", InSummary=true, Order=170)]
        [CLSCompliant(false)]
		[References("Device","Practitioner","Patient","RelatedPerson")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Author
        {
            get { return _Author; }
            set { _Author = value; OnPropertyChanged("Author"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Author;
        
        /// <summary>
        /// The person who answered the questions
        /// </summary>
        [FhirElement("source", InSummary=true, Order=180)]
        [CLSCompliant(false)]
		[References("Patient","Practitioner","RelatedPerson")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Source
        {
            get { return _Source; }
            set { _Source = value; OnPropertyChanged("Source"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Source;
        
        /// <summary>
        /// Groups and questions
        /// </summary>
        [FhirElement("item", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.QuestionnaireResponse.ItemComponent> Item
        {
            get { if(_Item==null) _Item = new List<Hl7.Fhir.Model.QuestionnaireResponse.ItemComponent>(); return _Item; }
            set { _Item = value; OnPropertyChanged("Item"); }
        }
        
        private List<Hl7.Fhir.Model.QuestionnaireResponse.ItemComponent> _Item;
        

        public static ElementDefinition.ConstraintComponent QuestionnaireResponse_QRS_1 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "item.all((answer.exists() and item.exists()).not())",
            Key = "qrs-1",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Nested item can't be beneath both item and answer",
            Xpath = "not(exists(f:answer) and exists(f:item))"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(QuestionnaireResponse_QRS_1);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as QuestionnaireResponse;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(BasedOn != null) dest.BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(BasedOn.DeepCopy());
                if(Parent != null) dest.Parent = new List<Hl7.Fhir.Model.ResourceReference>(Parent.DeepCopy());
                if(Questionnaire != null) dest.Questionnaire = (Hl7.Fhir.Model.ResourceReference)Questionnaire.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.QuestionnaireResponse.QuestionnaireResponseStatus>)StatusElement.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Context != null) dest.Context = (Hl7.Fhir.Model.ResourceReference)Context.DeepCopy();
                if(AuthoredElement != null) dest.AuthoredElement = (Hl7.Fhir.Model.FhirDateTime)AuthoredElement.DeepCopy();
                if(Author != null) dest.Author = (Hl7.Fhir.Model.ResourceReference)Author.DeepCopy();
                if(Source != null) dest.Source = (Hl7.Fhir.Model.ResourceReference)Source.DeepCopy();
                if(Item != null) dest.Item = new List<Hl7.Fhir.Model.QuestionnaireResponse.ItemComponent>(Item.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new QuestionnaireResponse());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as QuestionnaireResponse;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.Matches(Parent, otherT.Parent)) return false;
            if( !DeepComparable.Matches(Questionnaire, otherT.Questionnaire)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Context, otherT.Context)) return false;
            if( !DeepComparable.Matches(AuthoredElement, otherT.AuthoredElement)) return false;
            if( !DeepComparable.Matches(Author, otherT.Author)) return false;
            if( !DeepComparable.Matches(Source, otherT.Source)) return false;
            if( !DeepComparable.Matches(Item, otherT.Item)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as QuestionnaireResponse;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.IsExactly(Parent, otherT.Parent)) return false;
            if( !DeepComparable.IsExactly(Questionnaire, otherT.Questionnaire)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Context, otherT.Context)) return false;
            if( !DeepComparable.IsExactly(AuthoredElement, otherT.AuthoredElement)) return false;
            if( !DeepComparable.IsExactly(Author, otherT.Author)) return false;
            if( !DeepComparable.IsExactly(Source, otherT.Source)) return false;
            if( !DeepComparable.IsExactly(Item, otherT.Item)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				if (Identifier != null) yield return Identifier;
				foreach (var elem in BasedOn) { if (elem != null) yield return elem; }
				foreach (var elem in Parent) { if (elem != null) yield return elem; }
				if (Questionnaire != null) yield return Questionnaire;
				if (StatusElement != null) yield return StatusElement;
				if (Subject != null) yield return Subject;
				if (Context != null) yield return Context;
				if (AuthoredElement != null) yield return AuthoredElement;
				if (Author != null) yield return Author;
				if (Source != null) yield return Source;
				foreach (var elem in Item) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                foreach (var elem in BasedOn) { if (elem != null) yield return new ElementValue("basedOn", elem); }
                foreach (var elem in Parent) { if (elem != null) yield return new ElementValue("parent", elem); }
                if (Questionnaire != null) yield return new ElementValue("questionnaire", Questionnaire);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Context != null) yield return new ElementValue("context", Context);
                if (AuthoredElement != null) yield return new ElementValue("authored", AuthoredElement);
                if (Author != null) yield return new ElementValue("author", Author);
                if (Source != null) yield return new ElementValue("source", Source);
                foreach (var elem in Item) { if (elem != null) yield return new ElementValue("item", elem); }
            }
        }

    }
    
}
