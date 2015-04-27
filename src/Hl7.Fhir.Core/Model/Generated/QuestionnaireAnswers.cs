using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
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

//
// Generated on Thu, Apr 2, 2015 14:21+0200 for FHIR v0.5.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A structured set of questions and their answers
    /// </summary>
    [FhirType("QuestionnaireAnswers", IsResource=true)]
    [DataContract]
    public partial class QuestionnaireAnswers : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.QuestionnaireAnswers; } }
        [NotMapped]
        public override string TypeName { get { return "QuestionnaireAnswers"; } }
        
        /// <summary>
        /// Lifecycle status of the questionnaire answers
        /// </summary>
        [FhirEnumeration("QuestionnaireAnswersStatus")]
        public enum QuestionnaireAnswersStatus
        {
            /// <summary>
            /// This QuestionnaireAnswers has been partially filled out with answers, but changes or additions are still expected to be made to it.
            /// </summary>
            [EnumLiteral("in-progress")]
            InProgress,
            /// <summary>
            /// This QuestionnaireAnswers has been filled out with answers, and the current content is regarded as definitive.
            /// </summary>
            [EnumLiteral("completed")]
            Completed,
            /// <summary>
            /// This QuestionnaireAnswers has been filled out with answers, then marked as complete, yet changes or additions have been made to it afterwards.
            /// </summary>
            [EnumLiteral("amended")]
            Amended,
        }
        
        [FhirType("QuestionAnswerComponent")]
        [DataContract]
        public partial class QuestionAnswerComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "QuestionAnswerComponent"; } }
            
            /// <summary>
            /// Single-valued answer to the question
            /// </summary>
            [FhirElement("value", InSummary=true, Order=40, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Instant),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.Coding),typeof(Hl7.Fhir.Model.Quantity),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.Element _Value;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as QuestionAnswerComponent;
                
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
                return CopyTo(new QuestionAnswerComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as QuestionAnswerComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as QuestionAnswerComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("QuestionComponent")]
        [DataContract]
        public partial class QuestionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "QuestionComponent"; } }
            
            /// <summary>
            /// Corresponding question within Questionnaire
            /// </summary>
            [FhirElement("linkId", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString LinkIdElement
            {
                get { return _LinkIdElement; }
                set { _LinkIdElement = value; OnPropertyChanged("LinkIdElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _LinkIdElement;
            
            /// <summary>
            /// Corresponding question within Questionnaire
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
            /// Text of the question as it is shown to the user
            /// </summary>
            [FhirElement("text", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TextElement
            {
                get { return _TextElement; }
                set { _TextElement = value; OnPropertyChanged("TextElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TextElement;
            
            /// <summary>
            /// Text of the question as it is shown to the user
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
            /// The response(s) to the question
            /// </summary>
            [FhirElement("answer", InSummary=true, Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.QuestionnaireAnswers.QuestionAnswerComponent> Answer
            {
                get { if(_Answer==null) _Answer = new List<Hl7.Fhir.Model.QuestionnaireAnswers.QuestionAnswerComponent>(); return _Answer; }
                set { _Answer = value; OnPropertyChanged("Answer"); }
            }
            
            private List<Hl7.Fhir.Model.QuestionnaireAnswers.QuestionAnswerComponent> _Answer;
            
            /// <summary>
            /// Nested questionnaire group
            /// </summary>
            [FhirElement("group", InSummary=true, Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.QuestionnaireAnswers.GroupComponent> Group
            {
                get { if(_Group==null) _Group = new List<Hl7.Fhir.Model.QuestionnaireAnswers.GroupComponent>(); return _Group; }
                set { _Group = value; OnPropertyChanged("Group"); }
            }
            
            private List<Hl7.Fhir.Model.QuestionnaireAnswers.GroupComponent> _Group;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as QuestionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(LinkIdElement != null) dest.LinkIdElement = (Hl7.Fhir.Model.FhirString)LinkIdElement.DeepCopy();
                    if(TextElement != null) dest.TextElement = (Hl7.Fhir.Model.FhirString)TextElement.DeepCopy();
                    if(Answer != null) dest.Answer = new List<Hl7.Fhir.Model.QuestionnaireAnswers.QuestionAnswerComponent>(Answer.DeepCopy());
                    if(Group != null) dest.Group = new List<Hl7.Fhir.Model.QuestionnaireAnswers.GroupComponent>(Group.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new QuestionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as QuestionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(LinkIdElement, otherT.LinkIdElement)) return false;
                if( !DeepComparable.Matches(TextElement, otherT.TextElement)) return false;
                if( !DeepComparable.Matches(Answer, otherT.Answer)) return false;
                if( !DeepComparable.Matches(Group, otherT.Group)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as QuestionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(LinkIdElement, otherT.LinkIdElement)) return false;
                if( !DeepComparable.IsExactly(TextElement, otherT.TextElement)) return false;
                if( !DeepComparable.IsExactly(Answer, otherT.Answer)) return false;
                if( !DeepComparable.IsExactly(Group, otherT.Group)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("GroupComponent")]
        [DataContract]
        public partial class GroupComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "GroupComponent"; } }
            
            /// <summary>
            /// Corresponding group within Questionnaire
            /// </summary>
            [FhirElement("linkId", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString LinkIdElement
            {
                get { return _LinkIdElement; }
                set { _LinkIdElement = value; OnPropertyChanged("LinkIdElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _LinkIdElement;
            
            /// <summary>
            /// Corresponding group within Questionnaire
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
            /// Name for this group
            /// </summary>
            [FhirElement("title", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TitleElement
            {
                get { return _TitleElement; }
                set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TitleElement;
            
            /// <summary>
            /// Name for this group
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
            /// Additional text for the group
            /// </summary>
            [FhirElement("text", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TextElement
            {
                get { return _TextElement; }
                set { _TextElement = value; OnPropertyChanged("TextElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TextElement;
            
            /// <summary>
            /// Additional text for the group
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
            /// The subject this group's answers are about
            /// </summary>
            [FhirElement("subject", InSummary=true, Order=70)]
            [References()]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Subject
            {
                get { return _Subject; }
                set { _Subject = value; OnPropertyChanged("Subject"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Subject;
            
            /// <summary>
            /// Nested questionnaire answers group
            /// </summary>
            [FhirElement("group", InSummary=true, Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.QuestionnaireAnswers.GroupComponent> Group
            {
                get { if(_Group==null) _Group = new List<Hl7.Fhir.Model.QuestionnaireAnswers.GroupComponent>(); return _Group; }
                set { _Group = value; OnPropertyChanged("Group"); }
            }
            
            private List<Hl7.Fhir.Model.QuestionnaireAnswers.GroupComponent> _Group;
            
            /// <summary>
            /// Questions in this group
            /// </summary>
            [FhirElement("question", InSummary=true, Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.QuestionnaireAnswers.QuestionComponent> Question
            {
                get { if(_Question==null) _Question = new List<Hl7.Fhir.Model.QuestionnaireAnswers.QuestionComponent>(); return _Question; }
                set { _Question = value; OnPropertyChanged("Question"); }
            }
            
            private List<Hl7.Fhir.Model.QuestionnaireAnswers.QuestionComponent> _Question;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as GroupComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(LinkIdElement != null) dest.LinkIdElement = (Hl7.Fhir.Model.FhirString)LinkIdElement.DeepCopy();
                    if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                    if(TextElement != null) dest.TextElement = (Hl7.Fhir.Model.FhirString)TextElement.DeepCopy();
                    if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                    if(Group != null) dest.Group = new List<Hl7.Fhir.Model.QuestionnaireAnswers.GroupComponent>(Group.DeepCopy());
                    if(Question != null) dest.Question = new List<Hl7.Fhir.Model.QuestionnaireAnswers.QuestionComponent>(Question.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new GroupComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as GroupComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(LinkIdElement, otherT.LinkIdElement)) return false;
                if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
                if( !DeepComparable.Matches(TextElement, otherT.TextElement)) return false;
                if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
                if( !DeepComparable.Matches(Group, otherT.Group)) return false;
                if( !DeepComparable.Matches(Question, otherT.Question)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as GroupComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(LinkIdElement, otherT.LinkIdElement)) return false;
                if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
                if( !DeepComparable.IsExactly(TextElement, otherT.TextElement)) return false;
                if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
                if( !DeepComparable.IsExactly(Group, otherT.Group)) return false;
                if( !DeepComparable.IsExactly(Question, otherT.Question)) return false;
                
                return true;
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
        /// Form being answered
        /// </summary>
        [FhirElement("questionnaire", InSummary=true, Order=100)]
        [References("Questionnaire")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Questionnaire
        {
            get { return _Questionnaire; }
            set { _Questionnaire = value; OnPropertyChanged("Questionnaire"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Questionnaire;
        
        /// <summary>
        /// in-progress | completed | amended
        /// </summary>
        [FhirElement("status", InSummary=true, Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.QuestionnaireAnswers.QuestionnaireAnswersStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.QuestionnaireAnswers.QuestionnaireAnswersStatus> _StatusElement;
        
        /// <summary>
        /// in-progress | completed | amended
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.QuestionnaireAnswers.QuestionnaireAnswersStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.QuestionnaireAnswers.QuestionnaireAnswersStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// The subject of the questions
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=120)]
        [References()]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Person who received and recorded the answers
        /// </summary>
        [FhirElement("author", InSummary=true, Order=130)]
        [References("Device","Practitioner","Patient","RelatedPerson")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Author
        {
            get { return _Author; }
            set { _Author = value; OnPropertyChanged("Author"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Author;
        
        /// <summary>
        /// Date this version was authored
        /// </summary>
        [FhirElement("authored", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime AuthoredElement
        {
            get { return _AuthoredElement; }
            set { _AuthoredElement = value; OnPropertyChanged("AuthoredElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _AuthoredElement;
        
        /// <summary>
        /// Date this version was authored
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Authored
        {
            get { return AuthoredElement != null ? AuthoredElement.Value : null; }
            set
            {
                if(value == null)
                  AuthoredElement = null; 
                else
                  AuthoredElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Authored");
            }
        }
        
        /// <summary>
        /// The person who answered the questions
        /// </summary>
        [FhirElement("source", InSummary=true, Order=150)]
        [References("Patient","Practitioner","RelatedPerson")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Source
        {
            get { return _Source; }
            set { _Source = value; OnPropertyChanged("Source"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Source;
        
        /// <summary>
        /// Primary encounter during which the answers were collected
        /// </summary>
        [FhirElement("encounter", InSummary=true, Order=160)]
        [References("Encounter")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Encounter
        {
            get { return _Encounter; }
            set { _Encounter = value; OnPropertyChanged("Encounter"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Encounter;
        
        /// <summary>
        /// Grouped questions
        /// </summary>
        [FhirElement("group", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.QuestionnaireAnswers.GroupComponent Group
        {
            get { return _Group; }
            set { _Group = value; OnPropertyChanged("Group"); }
        }
        
        private Hl7.Fhir.Model.QuestionnaireAnswers.GroupComponent _Group;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as QuestionnaireAnswers;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(Questionnaire != null) dest.Questionnaire = (Hl7.Fhir.Model.ResourceReference)Questionnaire.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.QuestionnaireAnswers.QuestionnaireAnswersStatus>)StatusElement.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Author != null) dest.Author = (Hl7.Fhir.Model.ResourceReference)Author.DeepCopy();
                if(AuthoredElement != null) dest.AuthoredElement = (Hl7.Fhir.Model.FhirDateTime)AuthoredElement.DeepCopy();
                if(Source != null) dest.Source = (Hl7.Fhir.Model.ResourceReference)Source.DeepCopy();
                if(Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
                if(Group != null) dest.Group = (Hl7.Fhir.Model.QuestionnaireAnswers.GroupComponent)Group.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new QuestionnaireAnswers());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as QuestionnaireAnswers;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Questionnaire, otherT.Questionnaire)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Author, otherT.Author)) return false;
            if( !DeepComparable.Matches(AuthoredElement, otherT.AuthoredElement)) return false;
            if( !DeepComparable.Matches(Source, otherT.Source)) return false;
            if( !DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.Matches(Group, otherT.Group)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as QuestionnaireAnswers;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Questionnaire, otherT.Questionnaire)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Author, otherT.Author)) return false;
            if( !DeepComparable.IsExactly(AuthoredElement, otherT.AuthoredElement)) return false;
            if( !DeepComparable.IsExactly(Source, otherT.Source)) return false;
            if( !DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.IsExactly(Group, otherT.Group)) return false;
            
            return true;
        }
        
    }
    
}
