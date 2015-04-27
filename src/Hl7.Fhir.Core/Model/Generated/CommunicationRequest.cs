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
    /// A request for information to be sent to a receiver
    /// </summary>
    [FhirType("CommunicationRequest", IsResource=true)]
    [DataContract]
    public partial class CommunicationRequest : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.CommunicationRequest; } }
        [NotMapped]
        public override string TypeName { get { return "CommunicationRequest"; } }
        
        /// <summary>
        /// The status of the communication
        /// </summary>
        [FhirEnumeration("CommunicationRequestStatus")]
        public enum CommunicationRequestStatus
        {
            /// <summary>
            /// The request has been proposed.
            /// </summary>
            [EnumLiteral("proposed")]
            Proposed,
            /// <summary>
            /// The request has been planned.
            /// </summary>
            [EnumLiteral("planned")]
            Planned,
            /// <summary>
            /// The request has been placed.
            /// </summary>
            [EnumLiteral("requested")]
            Requested,
            /// <summary>
            /// The receiving system has received the request but not yet decided whether it will be performed.
            /// </summary>
            [EnumLiteral("received")]
            Received,
            /// <summary>
            /// The receiving system has accepted the order, but work has not yet commenced.
            /// </summary>
            [EnumLiteral("accepted")]
            Accepted,
            /// <summary>
            /// The work to fulfill the order is happening.
            /// </summary>
            [EnumLiteral("in-progress")]
            InProgress,
            /// <summary>
            /// The work has been complete, the report(s) released, and no further work is planned.
            /// </summary>
            [EnumLiteral("completed")]
            Completed,
            /// <summary>
            /// The request has been held by originating system/user request.
            /// </summary>
            [EnumLiteral("suspended")]
            Suspended,
            /// <summary>
            /// The receiving system has declined to fulfill the request.
            /// </summary>
            [EnumLiteral("rejected")]
            Rejected,
            /// <summary>
            /// The communication was attempted, but due to some procedural error, it could not be completed.
            /// </summary>
            [EnumLiteral("failed")]
            Failed,
        }
        
        [FhirType("CommunicationRequestPayloadComponent")]
        [DataContract]
        public partial class CommunicationRequestPayloadComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "CommunicationRequestPayloadComponent"; } }
            
            /// <summary>
            /// Message part content
            /// </summary>
            [FhirElement("content", InSummary=true, Order=40, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.ResourceReference))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Content
            {
                get { return _Content; }
                set { _Content = value; OnPropertyChanged("Content"); }
            }
            
            private Hl7.Fhir.Model.Element _Content;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CommunicationRequestPayloadComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Content != null) dest.Content = (Hl7.Fhir.Model.Element)Content.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new CommunicationRequestPayloadComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CommunicationRequestPayloadComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Content, otherT.Content)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CommunicationRequestPayloadComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Content, otherT.Content)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// Unique identifier
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
        /// Message category
        /// </summary>
        [FhirElement("category", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Category
        {
            get { return _Category; }
            set { _Category = value; OnPropertyChanged("Category"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Category;
        
        /// <summary>
        /// Message sender
        /// </summary>
        [FhirElement("sender", Order=110)]
        [References("Device","Organization","Patient","Practitioner","RelatedPerson")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Sender
        {
            get { return _Sender; }
            set { _Sender = value; OnPropertyChanged("Sender"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Sender;
        
        /// <summary>
        /// Message recipient
        /// </summary>
        [FhirElement("recipient", Order=120)]
        [References("Device","Organization","Patient","Practitioner","RelatedPerson")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Recipient
        {
            get { if(_Recipient==null) _Recipient = new List<Hl7.Fhir.Model.ResourceReference>(); return _Recipient; }
            set { _Recipient = value; OnPropertyChanged("Recipient"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Recipient;
        
        /// <summary>
        /// Message payload
        /// </summary>
        [FhirElement("payload", Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CommunicationRequest.CommunicationRequestPayloadComponent> Payload
        {
            get { if(_Payload==null) _Payload = new List<Hl7.Fhir.Model.CommunicationRequest.CommunicationRequestPayloadComponent>(); return _Payload; }
            set { _Payload = value; OnPropertyChanged("Payload"); }
        }
        
        private List<Hl7.Fhir.Model.CommunicationRequest.CommunicationRequestPayloadComponent> _Payload;
        
        /// <summary>
        /// Communication medium
        /// </summary>
        [FhirElement("medium", Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Medium
        {
            get { if(_Medium==null) _Medium = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Medium; }
            set { _Medium = value; OnPropertyChanged("Medium"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Medium;
        
        /// <summary>
        /// Requester of communication
        /// </summary>
        [FhirElement("requester", Order=150)]
        [References("Practitioner","Patient","RelatedPerson")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Requester
        {
            get { return _Requester; }
            set { _Requester = value; OnPropertyChanged("Requester"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Requester;
        
        /// <summary>
        /// proposed | planned | requested | received | accepted | in-progress | completed | suspended | rejected | failed
        /// </summary>
        [FhirElement("status", Order=160)]
        [DataMember]
        public Code<Hl7.Fhir.Model.CommunicationRequest.CommunicationRequestStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.CommunicationRequest.CommunicationRequestStatus> _StatusElement;
        
        /// <summary>
        /// proposed | planned | requested | received | accepted | in-progress | completed | suspended | rejected | failed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.CommunicationRequest.CommunicationRequestStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.CommunicationRequest.CommunicationRequestStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Encounter leading to message
        /// </summary>
        [FhirElement("encounter", Order=170)]
        [References("Encounter")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Encounter
        {
            get { return _Encounter; }
            set { _Encounter = value; OnPropertyChanged("Encounter"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Encounter;
        
        /// <summary>
        /// When scheduled
        /// </summary>
        [FhirElement("scheduledTime", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime ScheduledTimeElement
        {
            get { return _ScheduledTimeElement; }
            set { _ScheduledTimeElement = value; OnPropertyChanged("ScheduledTimeElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _ScheduledTimeElement;
        
        /// <summary>
        /// When scheduled
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ScheduledTime
        {
            get { return ScheduledTimeElement != null ? ScheduledTimeElement.Value : null; }
            set
            {
                if(value == null)
                  ScheduledTimeElement = null; 
                else
                  ScheduledTimeElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("ScheduledTime");
            }
        }
        
        /// <summary>
        /// Indication for message
        /// </summary>
        [FhirElement("reason", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Reason
        {
            get { if(_Reason==null) _Reason = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Reason; }
            set { _Reason = value; OnPropertyChanged("Reason"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Reason;
        
        /// <summary>
        /// When ordered or proposed
        /// </summary>
        [FhirElement("orderedOn", Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime OrderedOnElement
        {
            get { return _OrderedOnElement; }
            set { _OrderedOnElement = value; OnPropertyChanged("OrderedOnElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _OrderedOnElement;
        
        /// <summary>
        /// When ordered or proposed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string OrderedOn
        {
            get { return OrderedOnElement != null ? OrderedOnElement.Value : null; }
            set
            {
                if(value == null)
                  OrderedOnElement = null; 
                else
                  OrderedOnElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("OrderedOn");
            }
        }
        
        /// <summary>
        /// Focus of message
        /// </summary>
        [FhirElement("subject", Order=210)]
        [References("Patient")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Message urgency
        /// </summary>
        [FhirElement("priority", Order=220)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Priority
        {
            get { return _Priority; }
            set { _Priority = value; OnPropertyChanged("Priority"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Priority;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as CommunicationRequest;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                if(Sender != null) dest.Sender = (Hl7.Fhir.Model.ResourceReference)Sender.DeepCopy();
                if(Recipient != null) dest.Recipient = new List<Hl7.Fhir.Model.ResourceReference>(Recipient.DeepCopy());
                if(Payload != null) dest.Payload = new List<Hl7.Fhir.Model.CommunicationRequest.CommunicationRequestPayloadComponent>(Payload.DeepCopy());
                if(Medium != null) dest.Medium = new List<Hl7.Fhir.Model.CodeableConcept>(Medium.DeepCopy());
                if(Requester != null) dest.Requester = (Hl7.Fhir.Model.ResourceReference)Requester.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.CommunicationRequest.CommunicationRequestStatus>)StatusElement.DeepCopy();
                if(Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
                if(ScheduledTimeElement != null) dest.ScheduledTimeElement = (Hl7.Fhir.Model.FhirDateTime)ScheduledTimeElement.DeepCopy();
                if(Reason != null) dest.Reason = new List<Hl7.Fhir.Model.CodeableConcept>(Reason.DeepCopy());
                if(OrderedOnElement != null) dest.OrderedOnElement = (Hl7.Fhir.Model.FhirDateTime)OrderedOnElement.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Priority != null) dest.Priority = (Hl7.Fhir.Model.CodeableConcept)Priority.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new CommunicationRequest());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as CommunicationRequest;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(Sender, otherT.Sender)) return false;
            if( !DeepComparable.Matches(Recipient, otherT.Recipient)) return false;
            if( !DeepComparable.Matches(Payload, otherT.Payload)) return false;
            if( !DeepComparable.Matches(Medium, otherT.Medium)) return false;
            if( !DeepComparable.Matches(Requester, otherT.Requester)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.Matches(ScheduledTimeElement, otherT.ScheduledTimeElement)) return false;
            if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
            if( !DeepComparable.Matches(OrderedOnElement, otherT.OrderedOnElement)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Priority, otherT.Priority)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as CommunicationRequest;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(Sender, otherT.Sender)) return false;
            if( !DeepComparable.IsExactly(Recipient, otherT.Recipient)) return false;
            if( !DeepComparable.IsExactly(Payload, otherT.Payload)) return false;
            if( !DeepComparable.IsExactly(Medium, otherT.Medium)) return false;
            if( !DeepComparable.IsExactly(Requester, otherT.Requester)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.IsExactly(ScheduledTimeElement, otherT.ScheduledTimeElement)) return false;
            if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
            if( !DeepComparable.IsExactly(OrderedOnElement, otherT.OrderedOnElement)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Priority, otherT.Priority)) return false;
            
            return true;
        }
        
    }
    
}
