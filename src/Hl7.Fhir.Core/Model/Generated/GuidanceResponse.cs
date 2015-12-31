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
    /// The formal response to a guidance request
    /// </summary>
    [FhirType("GuidanceResponse", IsResource=true)]
    [DataContract]
    public partial class GuidanceResponse : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.GuidanceResponse; } }
        [NotMapped]
        public override string TypeName { get { return "GuidanceResponse"; } }
        
        /// <summary>
        /// The status of a guidance response
        /// (url: http://hl7.org/fhir/ValueSet/guidance-response-status)
        /// </summary>
        [FhirEnumeration("GuidanceResponseStatus")]
        public enum GuidanceResponseStatus
        {
            /// <summary>
            /// The request was processed successfully
            /// (system: http://hl7.org/fhir/guidance-response-status)
            /// </summary>
            [EnumLiteral("success"), Description("Success")]
            Success,
            /// <summary>
            /// The request was processed successfully, but more data may result in a more complete evaluation
            /// (system: http://hl7.org/fhir/guidance-response-status)
            /// </summary>
            [EnumLiteral("data-requested"), Description("Data Requested")]
            DataRequested,
            /// <summary>
            /// The request was processed, but more data is required to complete the evaluation
            /// (system: http://hl7.org/fhir/guidance-response-status)
            /// </summary>
            [EnumLiteral("data-required"), Description("Data Required")]
            DataRequired,
            /// <summary>
            /// The request is currently being processed
            /// (system: http://hl7.org/fhir/guidance-response-status)
            /// </summary>
            [EnumLiteral("in-progress"), Description("In Progress")]
            InProgress,
            /// <summary>
            /// The request was not processed successfully
            /// (system: http://hl7.org/fhir/guidance-response-status)
            /// </summary>
            [EnumLiteral("failure"), Description("Failure")]
            Failure,
        }

        /// <summary>
        /// The type of action to be performed
        /// (url: http://hl7.org/fhir/ValueSet/guidance-response-action-type)
        /// </summary>
        [FhirEnumeration("GuidanceResponseActionType")]
        public enum GuidanceResponseActionType
        {
            /// <summary>
            /// The action is to create a new resource
            /// (system: http://hl7.org/fhir/guidance-response-action-type)
            /// </summary>
            [EnumLiteral("create"), Description("Create")]
            Create,
            /// <summary>
            /// The action is to update an existing resource
            /// (system: http://hl7.org/fhir/guidance-response-action-type)
            /// </summary>
            [EnumLiteral("update"), Description("Update")]
            Update,
            /// <summary>
            /// The action is to remove an existing resource
            /// (system: http://hl7.org/fhir/guidance-response-action-type)
            /// </summary>
            [EnumLiteral("remove"), Description("Remove")]
            Remove,
            /// <summary>
            /// The action is to fire a specific event
            /// (system: http://hl7.org/fhir/guidance-response-action-type)
            /// </summary>
            [EnumLiteral("fire-event"), Description("Fire Event")]
            FireEvent,
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
            /// 
            /// </summary>
            [FhirElement("participant", Order=80)]
            [References("Patient","Person","Practitioner","RelatedPerson")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Participant
            {
                get { if(_Participant==null) _Participant = new List<Hl7.Fhir.Model.ResourceReference>(); return _Participant; }
                set { _Participant = value; OnPropertyChanged("Participant"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Participant;
            
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
            public Code<Hl7.Fhir.Model.GuidanceResponse.GuidanceResponseActionType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.GuidanceResponse.GuidanceResponseActionType> _TypeElement;
            
            /// <summary>
            /// create | update | remove | fire-event
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.GuidanceResponse.GuidanceResponseActionType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if(value == null)
                      TypeElement = null; 
                    else
                      TypeElement = new Code<Hl7.Fhir.Model.GuidanceResponse.GuidanceResponseActionType>(value);
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
            [FhirElement("actions", Order=150)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.GuidanceResponse.ActionComponent> Actions
            {
                get { if(_Actions==null) _Actions = new List<Hl7.Fhir.Model.GuidanceResponse.ActionComponent>(); return _Actions; }
                set { _Actions = value; OnPropertyChanged("Actions"); }
            }
            
            private List<Hl7.Fhir.Model.GuidanceResponse.ActionComponent> _Actions;
            
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
                    if(Participant != null) dest.Participant = new List<Hl7.Fhir.Model.ResourceReference>(Participant.DeepCopy());
                    if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(TextEquivalentElement != null) dest.TextEquivalentElement = (Hl7.Fhir.Model.FhirString)TextEquivalentElement.DeepCopy();
                    if(Concept != null) dest.Concept = new List<Hl7.Fhir.Model.CodeableConcept>(Concept.DeepCopy());
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.GuidanceResponse.GuidanceResponseActionType>)TypeElement.DeepCopy();
                    if(Resource != null) dest.Resource = (Hl7.Fhir.Model.ResourceReference)Resource.DeepCopy();
                    if(Actions != null) dest.Actions = new List<Hl7.Fhir.Model.GuidanceResponse.ActionComponent>(Actions.DeepCopy());
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
                if( !DeepComparable.Matches(Participant, otherT.Participant)) return false;
                if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(TextEquivalentElement, otherT.TextEquivalentElement)) return false;
                if( !DeepComparable.Matches(Concept, otherT.Concept)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(Resource, otherT.Resource)) return false;
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
                if( !DeepComparable.IsExactly(Participant, otherT.Participant)) return false;
                if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(TextEquivalentElement, otherT.TextEquivalentElement)) return false;
                if( !DeepComparable.IsExactly(Concept, otherT.Concept)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(Resource, otherT.Resource)) return false;
                if( !DeepComparable.IsExactly(Actions, otherT.Actions)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// The id of the request associated with this response, if any
        /// </summary>
        [FhirElement("requestId", Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString RequestIdElement
        {
            get { return _RequestIdElement; }
            set { _RequestIdElement = value; OnPropertyChanged("RequestIdElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _RequestIdElement;
        
        /// <summary>
        /// The id of the request associated with this response, if any
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string RequestId
        {
            get { return RequestIdElement != null ? RequestIdElement.Value : null; }
            set
            {
                if(value == null)
                  RequestIdElement = null; 
                else
                  RequestIdElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("RequestId");
            }
        }
        
        /// <summary>
        /// A reference to a knowledge module
        /// </summary>
        [FhirElement("module", InSummary=true, Order=100)]
        [References("DecisionSupportServiceModule","DecisionSupportRule")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Module
        {
            get { return _Module; }
            set { _Module = value; OnPropertyChanged("Module"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Module;
        
        /// <summary>
        /// success | data-requested | data-required | in-progress | failure
        /// </summary>
        [FhirElement("status", InSummary=true, Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.GuidanceResponse.GuidanceResponseStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.GuidanceResponse.GuidanceResponseStatus> _StatusElement;
        
        /// <summary>
        /// success | data-requested | data-required | in-progress | failure
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.GuidanceResponse.GuidanceResponseStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.GuidanceResponse.GuidanceResponseStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Messages resulting from the evaluation of the artifact or artifacts
        /// </summary>
        [FhirElement("evaluationMessage", Order=120)]
        [References("OperationOutcome")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> EvaluationMessage
        {
            get { if(_EvaluationMessage==null) _EvaluationMessage = new List<Hl7.Fhir.Model.ResourceReference>(); return _EvaluationMessage; }
            set { _EvaluationMessage = value; OnPropertyChanged("EvaluationMessage"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _EvaluationMessage;
        
        /// <summary>
        /// The output parameters of the evaluation, if any
        /// </summary>
        [FhirElement("outputParameters", Order=130)]
        [References("Parameters")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference OutputParameters
        {
            get { return _OutputParameters; }
            set { _OutputParameters = value; OnPropertyChanged("OutputParameters"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _OutputParameters;
        
        /// <summary>
        /// 
        /// </summary>
        [FhirElement("action", Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.GuidanceResponse.ActionComponent> Action
        {
            get { if(_Action==null) _Action = new List<Hl7.Fhir.Model.GuidanceResponse.ActionComponent>(); return _Action; }
            set { _Action = value; OnPropertyChanged("Action"); }
        }
        
        private List<Hl7.Fhir.Model.GuidanceResponse.ActionComponent> _Action;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as GuidanceResponse;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(RequestIdElement != null) dest.RequestIdElement = (Hl7.Fhir.Model.FhirString)RequestIdElement.DeepCopy();
                if(Module != null) dest.Module = (Hl7.Fhir.Model.ResourceReference)Module.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.GuidanceResponse.GuidanceResponseStatus>)StatusElement.DeepCopy();
                if(EvaluationMessage != null) dest.EvaluationMessage = new List<Hl7.Fhir.Model.ResourceReference>(EvaluationMessage.DeepCopy());
                if(OutputParameters != null) dest.OutputParameters = (Hl7.Fhir.Model.ResourceReference)OutputParameters.DeepCopy();
                if(Action != null) dest.Action = new List<Hl7.Fhir.Model.GuidanceResponse.ActionComponent>(Action.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new GuidanceResponse());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as GuidanceResponse;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(RequestIdElement, otherT.RequestIdElement)) return false;
            if( !DeepComparable.Matches(Module, otherT.Module)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(EvaluationMessage, otherT.EvaluationMessage)) return false;
            if( !DeepComparable.Matches(OutputParameters, otherT.OutputParameters)) return false;
            if( !DeepComparable.Matches(Action, otherT.Action)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as GuidanceResponse;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(RequestIdElement, otherT.RequestIdElement)) return false;
            if( !DeepComparable.IsExactly(Module, otherT.Module)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(EvaluationMessage, otherT.EvaluationMessage)) return false;
            if( !DeepComparable.IsExactly(OutputParameters, otherT.OutputParameters)) return false;
            if( !DeepComparable.IsExactly(Action, otherT.Action)) return false;
            
            return true;
        }
        
    }
    
}
