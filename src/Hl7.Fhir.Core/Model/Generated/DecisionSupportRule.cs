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
// Generated for FHIR v1.2.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A decision support rule
    /// </summary>
    [FhirType("DecisionSupportRule", IsResource=true)]
    [DataContract]
    public partial class DecisionSupportRule : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.DecisionSupportRule; } }
        [NotMapped]
        public override string TypeName { get { return "DecisionSupportRule"; } }
        
        /// <summary>
        /// The type of trigger
        /// (url: http://hl7.org/fhir/ValueSet/cds-rule-trigger-type)
        /// </summary>
        [FhirEnumeration("DecisionSupportRuleTriggerType")]
        public enum DecisionSupportRuleTriggerType
        {
            /// <summary>
            /// The trigger occurs in response to a specific named event
            /// (system: http://hl7.org/fhir/cds-rule-trigger-type)
            /// </summary>
            [EnumLiteral("named-event"), Description("Named Event")]
            NamedEvent,
            /// <summary>
            /// The trigger occurs at a specific time or periodically as described by a timing or schedule
            /// (system: http://hl7.org/fhir/cds-rule-trigger-type)
            /// </summary>
            [EnumLiteral("periodic"), Description("Periodic")]
            Periodic,
            /// <summary>
            /// The trigger occurs whenever data of a particular type is added
            /// (system: http://hl7.org/fhir/cds-rule-trigger-type)
            /// </summary>
            [EnumLiteral("data-added"), Description("Data Added")]
            DataAdded,
            /// <summary>
            /// The trigger occurs whenever data of a particular type is modified
            /// (system: http://hl7.org/fhir/cds-rule-trigger-type)
            /// </summary>
            [EnumLiteral("data-modified"), Description("Data Modified")]
            DataModified,
            /// <summary>
            /// The trigger occurs whenever data of a particular type is removed
            /// (system: http://hl7.org/fhir/cds-rule-trigger-type)
            /// </summary>
            [EnumLiteral("data-removed"), Description("Data Removed")]
            DataRemoved,
            /// <summary>
            /// The trigger occurs whenever data of a particular type is accessed
            /// (system: http://hl7.org/fhir/cds-rule-trigger-type)
            /// </summary>
            [EnumLiteral("data-accessed"), Description("Data Accessed")]
            DataAccessed,
            /// <summary>
            /// The trigger occurs whenever access to data of a particular type is completed
            /// (system: http://hl7.org/fhir/cds-rule-trigger-type)
            /// </summary>
            [EnumLiteral("data-access-ended"), Description("Data Access Ended")]
            DataAccessEnded,
        }

        /// <summary>
        /// The type of participant for an action in the module
        /// (url: http://hl7.org/fhir/ValueSet/cds-rule-participant)
        /// </summary>
        [FhirEnumeration("DecisionSupportRuleParticipantType")]
        public enum DecisionSupportRuleParticipantType
        {
            /// <summary>
            /// The participant is the patient under evaluation
            /// (system: http://hl7.org/fhir/cds-rule-participant)
            /// </summary>
            [EnumLiteral("patient"), Description("Patient")]
            Patient,
            /// <summary>
            /// The participant is a person
            /// (system: http://hl7.org/fhir/cds-rule-participant)
            /// </summary>
            [EnumLiteral("person"), Description("Person")]
            Person,
            /// <summary>
            /// The participant is a practitioner involved in the patient's care
            /// (system: http://hl7.org/fhir/cds-rule-participant)
            /// </summary>
            [EnumLiteral("practitioner"), Description("Practitioner")]
            Practitioner,
            /// <summary>
            /// The participant is a person related to the patient
            /// (system: http://hl7.org/fhir/cds-rule-participant)
            /// </summary>
            [EnumLiteral("related-person"), Description("Related Person")]
            RelatedPerson,
        }

        [FhirType("TriggerComponent")]
        [DataContract]
        public partial class TriggerComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "TriggerComponent"; } }
            
            /// <summary>
            /// named-event | periodic | data-added | data-modified | data-removed | data-accessed | data-access-ended
            /// </summary>
            [FhirElement("type", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.DecisionSupportRule.DecisionSupportRuleTriggerType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.DecisionSupportRule.DecisionSupportRuleTriggerType> _TypeElement;
            
            /// <summary>
            /// named-event | periodic | data-added | data-modified | data-removed | data-accessed | data-access-ended
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.DecisionSupportRule.DecisionSupportRuleTriggerType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if(value == null)
                      TypeElement = null; 
                    else
                      TypeElement = new Code<Hl7.Fhir.Model.DecisionSupportRule.DecisionSupportRuleTriggerType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("eventName", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString EventNameElement
            {
                get { return _EventNameElement; }
                set { _EventNameElement = value; OnPropertyChanged("EventNameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _EventNameElement;
            
            /// <summary>
            /// 
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string EventName
            {
                get { return EventNameElement != null ? EventNameElement.Value : null; }
                set
                {
                    if(value == null)
                      EventNameElement = null; 
                    else
                      EventNameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("EventName");
                }
            }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("eventTiming", Order=60, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Timing),typeof(Hl7.Fhir.Model.ResourceReference),typeof(Hl7.Fhir.Model.Date),typeof(Hl7.Fhir.Model.FhirDateTime))]
            [DataMember]
            public Hl7.Fhir.Model.Element EventTiming
            {
                get { return _EventTiming; }
                set { _EventTiming = value; OnPropertyChanged("EventTiming"); }
            }
            
            private Hl7.Fhir.Model.Element _EventTiming;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TriggerComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.DecisionSupportRule.DecisionSupportRuleTriggerType>)TypeElement.DeepCopy();
                    if(EventNameElement != null) dest.EventNameElement = (Hl7.Fhir.Model.FhirString)EventNameElement.DeepCopy();
                    if(EventTiming != null) dest.EventTiming = (Hl7.Fhir.Model.Element)EventTiming.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TriggerComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TriggerComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(EventNameElement, otherT.EventNameElement)) return false;
                if( !DeepComparable.Matches(EventTiming, otherT.EventTiming)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TriggerComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(EventNameElement, otherT.EventNameElement)) return false;
                if( !DeepComparable.IsExactly(EventTiming, otherT.EventTiming)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ActionComponent")]
        [DataContract]
        public partial class ActionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ActionComponent"; } }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("actionIdentifier", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier ActionIdentifier
            {
                get { return _ActionIdentifier; }
                set { _ActionIdentifier = value; OnPropertyChanged("ActionIdentifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _ActionIdentifier;
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("number", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NumberElement
            {
                get { return _NumberElement; }
                set { _NumberElement = value; OnPropertyChanged("NumberElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NumberElement;
            
            /// <summary>
            /// 
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Number
            {
                get { return NumberElement != null ? NumberElement.Value : null; }
                set
                {
                    if(value == null)
                      NumberElement = null; 
                    else
                      NumberElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Number");
                }
            }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("supportingEvidence", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Attachment> SupportingEvidence
            {
                get { if(_SupportingEvidence==null) _SupportingEvidence = new List<Hl7.Fhir.Model.Attachment>(); return _SupportingEvidence; }
                set { _SupportingEvidence = value; OnPropertyChanged("SupportingEvidence"); }
            }
            
            private List<Hl7.Fhir.Model.Attachment> _SupportingEvidence;
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("documentation", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Attachment> Documentation
            {
                get { if(_Documentation==null) _Documentation = new List<Hl7.Fhir.Model.Attachment>(); return _Documentation; }
                set { _Documentation = value; OnPropertyChanged("Documentation"); }
            }
            
            private List<Hl7.Fhir.Model.Attachment> _Documentation;
            
            /// <summary>
            /// patient | person | practitioner | related-person
            /// </summary>
            [FhirElement("participantType", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Code<Hl7.Fhir.Model.DecisionSupportRule.DecisionSupportRuleParticipantType>> ParticipantTypeElement
            {
                get { if(_ParticipantTypeElement==null) _ParticipantTypeElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DecisionSupportRule.DecisionSupportRuleParticipantType>>(); return _ParticipantTypeElement; }
                set { _ParticipantTypeElement = value; OnPropertyChanged("ParticipantTypeElement"); }
            }
            
            private List<Code<Hl7.Fhir.Model.DecisionSupportRule.DecisionSupportRuleParticipantType>> _ParticipantTypeElement;
            
            /// <summary>
            /// patient | person | practitioner | related-person
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<Hl7.Fhir.Model.DecisionSupportRule.DecisionSupportRuleParticipantType?> ParticipantType
            {
                get { return ParticipantTypeElement != null ? ParticipantTypeElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      ParticipantTypeElement = null; 
                    else
                      ParticipantTypeElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DecisionSupportRule.DecisionSupportRuleParticipantType>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DecisionSupportRule.DecisionSupportRuleParticipantType>(elem)));
                    OnPropertyChanged("ParticipantType");
                }
            }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("title", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TitleElement
            {
                get { return _TitleElement; }
                set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TitleElement;
            
            /// <summary>
            /// 
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
            /// 
            /// </summary>
            [FhirElement("description", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// 
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Description
            {
                get { return DescriptionElement != null ? DescriptionElement.Value : null; }
                set
                {
                    if(value == null)
                      DescriptionElement = null; 
                    else
                      DescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Description");
                }
            }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("textEquivalent", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TextEquivalentElement
            {
                get { return _TextEquivalentElement; }
                set { _TextEquivalentElement = value; OnPropertyChanged("TextEquivalentElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TextEquivalentElement;
            
            /// <summary>
            /// 
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string TextEquivalent
            {
                get { return TextEquivalentElement != null ? TextEquivalentElement.Value : null; }
                set
                {
                    if(value == null)
                      TextEquivalentElement = null; 
                    else
                      TextEquivalentElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("TextEquivalent");
                }
            }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("concept", Order=120)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Concept
            {
                get { if(_Concept==null) _Concept = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Concept; }
                set { _Concept = value; OnPropertyChanged("Concept"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Concept;
            
            /// <summary>
            /// create | update | remove | fire-event
            /// </summary>
            [FhirElement("type", Order=130)]
            [DataMember]
            public Hl7.Fhir.Model.Code TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Hl7.Fhir.Model.Code _TypeElement;
            
            /// <summary>
            /// create | update | remove | fire-event
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if(value == null)
                      TypeElement = null; 
                    else
                      TypeElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("resource", Order=140)]
            [References()]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Resource
            {
                get { return _Resource; }
                set { _Resource = value; OnPropertyChanged("Resource"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Resource;
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("customization", Order=150)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.DecisionSupportRule.CustomizationComponent> Customization
            {
                get { if(_Customization==null) _Customization = new List<Hl7.Fhir.Model.DecisionSupportRule.CustomizationComponent>(); return _Customization; }
                set { _Customization = value; OnPropertyChanged("Customization"); }
            }
            
            private List<Hl7.Fhir.Model.DecisionSupportRule.CustomizationComponent> _Customization;
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("actions", Order=160)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.DecisionSupportRule.ActionComponent> Actions
            {
                get { if(_Actions==null) _Actions = new List<Hl7.Fhir.Model.DecisionSupportRule.ActionComponent>(); return _Actions; }
                set { _Actions = value; OnPropertyChanged("Actions"); }
            }
            
            private List<Hl7.Fhir.Model.DecisionSupportRule.ActionComponent> _Actions;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ActionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ActionIdentifier != null) dest.ActionIdentifier = (Hl7.Fhir.Model.Identifier)ActionIdentifier.DeepCopy();
                    if(NumberElement != null) dest.NumberElement = (Hl7.Fhir.Model.FhirString)NumberElement.DeepCopy();
                    if(SupportingEvidence != null) dest.SupportingEvidence = new List<Hl7.Fhir.Model.Attachment>(SupportingEvidence.DeepCopy());
                    if(Documentation != null) dest.Documentation = new List<Hl7.Fhir.Model.Attachment>(Documentation.DeepCopy());
                    if(ParticipantTypeElement != null) dest.ParticipantTypeElement = new List<Code<Hl7.Fhir.Model.DecisionSupportRule.DecisionSupportRuleParticipantType>>(ParticipantTypeElement.DeepCopy());
                    if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(TextEquivalentElement != null) dest.TextEquivalentElement = (Hl7.Fhir.Model.FhirString)TextEquivalentElement.DeepCopy();
                    if(Concept != null) dest.Concept = new List<Hl7.Fhir.Model.CodeableConcept>(Concept.DeepCopy());
                    if(TypeElement != null) dest.TypeElement = (Hl7.Fhir.Model.Code)TypeElement.DeepCopy();
                    if(Resource != null) dest.Resource = (Hl7.Fhir.Model.ResourceReference)Resource.DeepCopy();
                    if(Customization != null) dest.Customization = new List<Hl7.Fhir.Model.DecisionSupportRule.CustomizationComponent>(Customization.DeepCopy());
                    if(Actions != null) dest.Actions = new List<Hl7.Fhir.Model.DecisionSupportRule.ActionComponent>(Actions.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ActionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ActionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ActionIdentifier, otherT.ActionIdentifier)) return false;
                if( !DeepComparable.Matches(NumberElement, otherT.NumberElement)) return false;
                if( !DeepComparable.Matches(SupportingEvidence, otherT.SupportingEvidence)) return false;
                if( !DeepComparable.Matches(Documentation, otherT.Documentation)) return false;
                if( !DeepComparable.Matches(ParticipantTypeElement, otherT.ParticipantTypeElement)) return false;
                if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(TextEquivalentElement, otherT.TextEquivalentElement)) return false;
                if( !DeepComparable.Matches(Concept, otherT.Concept)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(Resource, otherT.Resource)) return false;
                if( !DeepComparable.Matches(Customization, otherT.Customization)) return false;
                if( !DeepComparable.Matches(Actions, otherT.Actions)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ActionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ActionIdentifier, otherT.ActionIdentifier)) return false;
                if( !DeepComparable.IsExactly(NumberElement, otherT.NumberElement)) return false;
                if( !DeepComparable.IsExactly(SupportingEvidence, otherT.SupportingEvidence)) return false;
                if( !DeepComparable.IsExactly(Documentation, otherT.Documentation)) return false;
                if( !DeepComparable.IsExactly(ParticipantTypeElement, otherT.ParticipantTypeElement)) return false;
                if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(TextEquivalentElement, otherT.TextEquivalentElement)) return false;
                if( !DeepComparable.IsExactly(Concept, otherT.Concept)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(Resource, otherT.Resource)) return false;
                if( !DeepComparable.IsExactly(Customization, otherT.Customization)) return false;
                if( !DeepComparable.IsExactly(Actions, otherT.Actions)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("CustomizationComponent")]
        [DataContract]
        public partial class CustomizationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "CustomizationComponent"; } }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("path", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PathElement
            {
                get { return _PathElement; }
                set { _PathElement = value; OnPropertyChanged("PathElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PathElement;
            
            /// <summary>
            /// 
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Path
            {
                get { return PathElement != null ? PathElement.Value : null; }
                set
                {
                    if(value == null)
                      PathElement = null; 
                    else
                      PathElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Path");
                }
            }
            
            /// <summary>
            /// 
            /// </summary>
            [FhirElement("expression", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ExpressionElement
            {
                get { return _ExpressionElement; }
                set { _ExpressionElement = value; OnPropertyChanged("ExpressionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ExpressionElement;
            
            /// <summary>
            /// 
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Expression
            {
                get { return ExpressionElement != null ? ExpressionElement.Value : null; }
                set
                {
                    if(value == null)
                      ExpressionElement = null; 
                    else
                      ExpressionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Expression");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CustomizationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(PathElement != null) dest.PathElement = (Hl7.Fhir.Model.FhirString)PathElement.DeepCopy();
                    if(ExpressionElement != null) dest.ExpressionElement = (Hl7.Fhir.Model.FhirString)ExpressionElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new CustomizationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CustomizationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.Matches(ExpressionElement, otherT.ExpressionElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CustomizationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.IsExactly(ExpressionElement, otherT.ExpressionElement)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// Logical identifier
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// The version of the module, if any
        /// </summary>
        [FhirElement("version", InSummary=true, Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement
        {
            get { return _VersionElement; }
            set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _VersionElement;
        
        /// <summary>
        /// The version of the module, if any
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
        /// Module information for the rule
        /// </summary>
        [FhirElement("moduleMetadata", Order=110)]
        [References("ModuleMetadata")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference ModuleMetadata
        {
            get { return _ModuleMetadata; }
            set { _ModuleMetadata = value; OnPropertyChanged("ModuleMetadata"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _ModuleMetadata;
        
        /// <summary>
        /// A library containing logic used by the rule
        /// </summary>
        [FhirElement("library", Order=120)]
        [References("Library")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Library
        {
            get { if(_Library==null) _Library = new List<Hl7.Fhir.Model.ResourceReference>(); return _Library; }
            set { _Library = value; OnPropertyChanged("Library"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Library;
        
        /// <summary>
        /// 
        /// </summary>
        [FhirElement("trigger", Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.DecisionSupportRule.TriggerComponent> Trigger
        {
            get { if(_Trigger==null) _Trigger = new List<Hl7.Fhir.Model.DecisionSupportRule.TriggerComponent>(); return _Trigger; }
            set { _Trigger = value; OnPropertyChanged("Trigger"); }
        }
        
        private List<Hl7.Fhir.Model.DecisionSupportRule.TriggerComponent> _Trigger;
        
        /// <summary>
        /// 
        /// </summary>
        [FhirElement("condition", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ConditionElement
        {
            get { return _ConditionElement; }
            set { _ConditionElement = value; OnPropertyChanged("ConditionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _ConditionElement;
        
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Condition
        {
            get { return ConditionElement != null ? ConditionElement.Value : null; }
            set
            {
                if(value == null)
                  ConditionElement = null; 
                else
                  ConditionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Condition");
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        [FhirElement("action", Order=150)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.DecisionSupportRule.ActionComponent> Action
        {
            get { if(_Action==null) _Action = new List<Hl7.Fhir.Model.DecisionSupportRule.ActionComponent>(); return _Action; }
            set { _Action = value; OnPropertyChanged("Action"); }
        }
        
        private List<Hl7.Fhir.Model.DecisionSupportRule.ActionComponent> _Action;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as DecisionSupportRule;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(ModuleMetadata != null) dest.ModuleMetadata = (Hl7.Fhir.Model.ResourceReference)ModuleMetadata.DeepCopy();
                if(Library != null) dest.Library = new List<Hl7.Fhir.Model.ResourceReference>(Library.DeepCopy());
                if(Trigger != null) dest.Trigger = new List<Hl7.Fhir.Model.DecisionSupportRule.TriggerComponent>(Trigger.DeepCopy());
                if(ConditionElement != null) dest.ConditionElement = (Hl7.Fhir.Model.FhirString)ConditionElement.DeepCopy();
                if(Action != null) dest.Action = new List<Hl7.Fhir.Model.DecisionSupportRule.ActionComponent>(Action.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new DecisionSupportRule());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as DecisionSupportRule;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.Matches(ModuleMetadata, otherT.ModuleMetadata)) return false;
            if( !DeepComparable.Matches(Library, otherT.Library)) return false;
            if( !DeepComparable.Matches(Trigger, otherT.Trigger)) return false;
            if( !DeepComparable.Matches(ConditionElement, otherT.ConditionElement)) return false;
            if( !DeepComparable.Matches(Action, otherT.Action)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as DecisionSupportRule;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.IsExactly(ModuleMetadata, otherT.ModuleMetadata)) return false;
            if( !DeepComparable.IsExactly(Library, otherT.Library)) return false;
            if( !DeepComparable.IsExactly(Trigger, otherT.Trigger)) return false;
            if( !DeepComparable.IsExactly(ConditionElement, otherT.ConditionElement)) return false;
            if( !DeepComparable.IsExactly(Action, otherT.Action)) return false;
            
            return true;
        }
        
    }
    
}
