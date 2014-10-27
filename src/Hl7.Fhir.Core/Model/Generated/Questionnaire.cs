using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;

/*
  Copyright (c) 2011-2013, HL7, Inc.
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
// Generated on Thu, Oct 23, 2014 14:22+0200 for FHIR v0.0.82
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A structured set of questions and their answers
    /// </summary>
    [FhirType("Questionnaire", IsResource=true)]
    [DataContract]
    public partial class Questionnaire : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Lifecycle status of the questionnaire
        /// </summary>
        [FhirEnumeration("QuestionnaireStatus")]
        public enum QuestionnaireStatus
        {
            /// <summary>
            /// This Questionnaire is used as a template but the template is not ready for use or publication.
            /// </summary>
            [EnumLiteral("draft")]
            Draft,
            /// <summary>
            /// This Questionnaire is used as a template, is published and ready for use.
            /// </summary>
            [EnumLiteral("published")]
            Published,
            /// <summary>
            /// This Questionnaire is used as a template but should no longer be used for new Questionnaires.
            /// </summary>
            [EnumLiteral("retired")]
            Retired,
            /// <summary>
            /// This Questionnaire has been filled out with answers, but changes or additions are still expected to be made to it.
            /// </summary>
            [EnumLiteral("in progress")]
            InProgress,
            /// <summary>
            /// This Questionnaire has been filled out with answers, and the current content is regarded as definitive.
            /// </summary>
            [EnumLiteral("completed")]
            Completed,
            /// <summary>
            /// This Questionnaire has been filled out with answers, then marked as complete, yet changes or additions have been made to it afterwards.
            /// </summary>
            [EnumLiteral("amended")]
            Amended,
        }
        
        [FhirType("QuestionComponent")]
        [DataContract]
        public partial class QuestionComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Code or name of the question
            /// </summary>
            [FhirElement("name", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Name
            {
                get { return _Name; }
                set { _Name = value; OnPropertyChanged("Name"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _Name;
            
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
            /// Single-valued answer to the question
            /// </summary>
            [FhirElement("answer", InSummary=true, Order=60, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDecimal),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Instant))]
            [DataMember]
            public Hl7.Fhir.Model.Element Answer
            {
                get { return _Answer; }
                set { _Answer = value; OnPropertyChanged("Answer"); }
            }
            private Hl7.Fhir.Model.Element _Answer;
            
            /// <summary>
            /// Selected options
            /// </summary>
            [FhirElement("choice", InSummary=true, Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> Choice
            {
                get { return _Choice; }
                set { _Choice = value; OnPropertyChanged("Choice"); }
            }
            private List<Hl7.Fhir.Model.Coding> _Choice;
            
            /// <summary>
            /// Valueset containing the possible options
            /// </summary>
            [FhirElement("options", InSummary=true, Order=80)]
            [References("ValueSet")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Options
            {
                get { return _Options; }
                set { _Options = value; OnPropertyChanged("Options"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Options;
            
            /// <summary>
            /// Structured answer
            /// </summary>
            [FhirElement("data", InSummary=true, Order=90, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Element))]
            [DataMember]
            public Hl7.Fhir.Model.Element Data
            {
                get { return _Data; }
                set { _Data = value; OnPropertyChanged("Data"); }
            }
            private Hl7.Fhir.Model.Element _Data;
            
            /// <summary>
            /// Remarks about the answer given
            /// </summary>
            [FhirElement("remarks", InSummary=true, Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString RemarksElement
            {
                get { return _RemarksElement; }
                set { _RemarksElement = value; OnPropertyChanged("RemarksElement"); }
            }
            private Hl7.Fhir.Model.FhirString _RemarksElement;
            
            /// <summary>
            /// Remarks about the answer given
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Remarks
            {
                get { return RemarksElement != null ? RemarksElement.Value : null; }
                set
                {
                    if(value == null)
                      RemarksElement = null; 
                    else
                      RemarksElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Remarks");
                }
            }
            
            /// <summary>
            /// Nested questionnaire group
            /// </summary>
            [FhirElement("group", InSummary=true, Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Questionnaire.GroupComponent> Group
            {
                get { return _Group; }
                set { _Group = value; OnPropertyChanged("Group"); }
            }
            private List<Hl7.Fhir.Model.Questionnaire.GroupComponent> _Group;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as QuestionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Name != null) dest.Name = (Hl7.Fhir.Model.CodeableConcept)Name.DeepCopy();
                    if(TextElement != null) dest.TextElement = (Hl7.Fhir.Model.FhirString)TextElement.DeepCopy();
                    if(Answer != null) dest.Answer = (Hl7.Fhir.Model.Element)Answer.DeepCopy();
                    if(Choice != null) dest.Choice = new List<Hl7.Fhir.Model.Coding>(Choice.DeepCopy());
                    if(Options != null) dest.Options = (Hl7.Fhir.Model.ResourceReference)Options.DeepCopy();
                    if(Data != null) dest.Data = (Hl7.Fhir.Model.Element)Data.DeepCopy();
                    if(RemarksElement != null) dest.RemarksElement = (Hl7.Fhir.Model.FhirString)RemarksElement.DeepCopy();
                    if(Group != null) dest.Group = new List<Hl7.Fhir.Model.Questionnaire.GroupComponent>(Group.DeepCopy());
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
                if( !DeepComparable.Matches(Name, otherT.Name)) return false;
                if( !DeepComparable.Matches(TextElement, otherT.TextElement)) return false;
                if( !DeepComparable.Matches(Answer, otherT.Answer)) return false;
                if( !DeepComparable.Matches(Choice, otherT.Choice)) return false;
                if( !DeepComparable.Matches(Options, otherT.Options)) return false;
                if( !DeepComparable.Matches(Data, otherT.Data)) return false;
                if( !DeepComparable.Matches(RemarksElement, otherT.RemarksElement)) return false;
                if( !DeepComparable.Matches(Group, otherT.Group)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as QuestionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Name, otherT.Name)) return false;
                if( !DeepComparable.IsExactly(TextElement, otherT.TextElement)) return false;
                if( !DeepComparable.IsExactly(Answer, otherT.Answer)) return false;
                if( !DeepComparable.IsExactly(Choice, otherT.Choice)) return false;
                if( !DeepComparable.IsExactly(Options, otherT.Options)) return false;
                if( !DeepComparable.IsExactly(Data, otherT.Data)) return false;
                if( !DeepComparable.IsExactly(RemarksElement, otherT.RemarksElement)) return false;
                if( !DeepComparable.IsExactly(Group, otherT.Group)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("GroupComponent")]
        [DataContract]
        public partial class GroupComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Code or name of the section on a questionnaire
            /// </summary>
            [FhirElement("name", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Name
            {
                get { return _Name; }
                set { _Name = value; OnPropertyChanged("Name"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _Name;
            
            /// <summary>
            /// Text that is displayed above the contents of the group
            /// </summary>
            [FhirElement("header", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString HeaderElement
            {
                get { return _HeaderElement; }
                set { _HeaderElement = value; OnPropertyChanged("HeaderElement"); }
            }
            private Hl7.Fhir.Model.FhirString _HeaderElement;
            
            /// <summary>
            /// Text that is displayed above the contents of the group
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Header
            {
                get { return HeaderElement != null ? HeaderElement.Value : null; }
                set
                {
                    if(value == null)
                      HeaderElement = null; 
                    else
                      HeaderElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Header");
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
            /// Nested questionnaire group
            /// </summary>
            [FhirElement("group", InSummary=true, Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Questionnaire.GroupComponent> Group
            {
                get { return _Group; }
                set { _Group = value; OnPropertyChanged("Group"); }
            }
            private List<Hl7.Fhir.Model.Questionnaire.GroupComponent> _Group;
            
            /// <summary>
            /// Questions in this group
            /// </summary>
            [FhirElement("question", InSummary=true, Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Questionnaire.QuestionComponent> Question
            {
                get { return _Question; }
                set { _Question = value; OnPropertyChanged("Question"); }
            }
            private List<Hl7.Fhir.Model.Questionnaire.QuestionComponent> _Question;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as GroupComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Name != null) dest.Name = (Hl7.Fhir.Model.CodeableConcept)Name.DeepCopy();
                    if(HeaderElement != null) dest.HeaderElement = (Hl7.Fhir.Model.FhirString)HeaderElement.DeepCopy();
                    if(TextElement != null) dest.TextElement = (Hl7.Fhir.Model.FhirString)TextElement.DeepCopy();
                    if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                    if(Group != null) dest.Group = new List<Hl7.Fhir.Model.Questionnaire.GroupComponent>(Group.DeepCopy());
                    if(Question != null) dest.Question = new List<Hl7.Fhir.Model.Questionnaire.QuestionComponent>(Question.DeepCopy());
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
                if( !DeepComparable.Matches(Name, otherT.Name)) return false;
                if( !DeepComparable.Matches(HeaderElement, otherT.HeaderElement)) return false;
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
                if( !DeepComparable.IsExactly(Name, otherT.Name)) return false;
                if( !DeepComparable.IsExactly(HeaderElement, otherT.HeaderElement)) return false;
                if( !DeepComparable.IsExactly(TextElement, otherT.TextElement)) return false;
                if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
                if( !DeepComparable.IsExactly(Group, otherT.Group)) return false;
                if( !DeepComparable.IsExactly(Question, otherT.Question)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// draft | published | retired | in progress | completed | amended
        /// </summary>
        [FhirElement("status", InSummary=true, Order=70)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Questionnaire.QuestionnaireStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        private Code<Hl7.Fhir.Model.Questionnaire.QuestionnaireStatus> _StatusElement;
        
        /// <summary>
        /// draft | published | retired | in progress | completed | amended
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
        [FhirElement("authored", InSummary=true, Order=80)]
        [Cardinality(Min=1,Max=1)]
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
        /// The subject of the questions
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=90)]
        [References("Patient","RelatedPerson")]
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
        [FhirElement("author", InSummary=true, Order=100)]
        [References("Practitioner","Patient","RelatedPerson")]
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
        [FhirElement("source", InSummary=true, Order=110)]
        [References("Patient","Practitioner","RelatedPerson")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Source
        {
            get { return _Source; }
            set { _Source = value; OnPropertyChanged("Source"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Source;
        
        /// <summary>
        /// Name/code for a predefined list of questions
        /// </summary>
        [FhirElement("name", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Name
        {
            get { return _Name; }
            set { _Name = value; OnPropertyChanged("Name"); }
        }
        private Hl7.Fhir.Model.CodeableConcept _Name;
        
        /// <summary>
        /// External Ids for this questionnaire
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Primary encounter during which the answers were collected
        /// </summary>
        [FhirElement("encounter", InSummary=true, Order=140)]
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
        [FhirElement("group", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.Questionnaire.GroupComponent Group
        {
            get { return _Group; }
            set { _Group = value; OnPropertyChanged("Group"); }
        }
        private Hl7.Fhir.Model.Questionnaire.GroupComponent _Group;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Questionnaire;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Questionnaire.QuestionnaireStatus>)StatusElement.DeepCopy();
                if(AuthoredElement != null) dest.AuthoredElement = (Hl7.Fhir.Model.FhirDateTime)AuthoredElement.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Author != null) dest.Author = (Hl7.Fhir.Model.ResourceReference)Author.DeepCopy();
                if(Source != null) dest.Source = (Hl7.Fhir.Model.ResourceReference)Source.DeepCopy();
                if(Name != null) dest.Name = (Hl7.Fhir.Model.CodeableConcept)Name.DeepCopy();
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
                if(Group != null) dest.Group = (Hl7.Fhir.Model.Questionnaire.GroupComponent)Group.DeepCopy();
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
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(AuthoredElement, otherT.AuthoredElement)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Author, otherT.Author)) return false;
            if( !DeepComparable.Matches(Source, otherT.Source)) return false;
            if( !DeepComparable.Matches(Name, otherT.Name)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.Matches(Group, otherT.Group)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Questionnaire;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(AuthoredElement, otherT.AuthoredElement)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Author, otherT.Author)) return false;
            if( !DeepComparable.IsExactly(Source, otherT.Source)) return false;
            if( !DeepComparable.IsExactly(Name, otherT.Name)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.IsExactly(Group, otherT.Group)) return false;
            
            return true;
        }
        
    }
    
}
