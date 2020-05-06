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
// Generated for FHIR v4.4.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Status information about a Subscription provided during event notification
    /// </summary>
    [FhirType("SubscriptionStatus", IsResource=true)]
    [DataContract]
    public partial class SubscriptionStatus : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.SubscriptionStatus; } }
        [NotMapped]
        public override string TypeName { get { return "SubscriptionStatus"; } }
        
        /// <summary>
        /// The type of notification represented by the status message.
        /// (url: http://hl7.org/fhir/ValueSet/subscription-notification-type)
        /// </summary>
        [FhirEnumeration("SubscriptionNotificationType")]
        public enum SubscriptionNotificationType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/subscription-notification-type)
            /// </summary>
            [EnumLiteral("handshake", "http://hl7.org/fhir/subscription-notification-type"), Description("Handshake")]
            Handshake,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/subscription-notification-type)
            /// </summary>
            [EnumLiteral("heartbeat", "http://hl7.org/fhir/subscription-notification-type"), Description("Heartbeat")]
            Heartbeat,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/subscription-notification-type)
            /// </summary>
            [EnumLiteral("event-notification", "http://hl7.org/fhir/subscription-notification-type"), Description("Event Notification")]
            EventNotification,
        }

        /// <summary>
        /// handshake | heartbeat | event-notification
        /// </summary>
        [FhirElement("notificationType", InSummary=true, Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.SubscriptionStatus.SubscriptionNotificationType> NotificationTypeElement
        {
            get { return _NotificationTypeElement; }
            set { _NotificationTypeElement = value; OnPropertyChanged("NotificationTypeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.SubscriptionStatus.SubscriptionNotificationType> _NotificationTypeElement;
        
        /// <summary>
        /// handshake | heartbeat | event-notification
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.SubscriptionStatus.SubscriptionNotificationType? NotificationType
        {
            get { return NotificationTypeElement != null ? NotificationTypeElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  NotificationTypeElement = null; 
                else
                  NotificationTypeElement = new Code<Hl7.Fhir.Model.SubscriptionStatus.SubscriptionNotificationType>(value);
                OnPropertyChanged("NotificationType");
            }
        }
        
        /// <summary>
        /// Events since the Subscription was created
        /// </summary>
        [FhirElement("eventsSinceSubscriptionStart", InSummary=true, Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.Integer64 EventsSinceSubscriptionStartElement
        {
            get { return _EventsSinceSubscriptionStartElement; }
            set { _EventsSinceSubscriptionStartElement = value; OnPropertyChanged("EventsSinceSubscriptionStartElement"); }
        }
        
        private Hl7.Fhir.Model.Integer64 _EventsSinceSubscriptionStartElement;
        
        /// <summary>
        /// Events since the Subscription was created
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public long? EventsSinceSubscriptionStart
        {
            get { return EventsSinceSubscriptionStartElement != null ? EventsSinceSubscriptionStartElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  EventsSinceSubscriptionStartElement = null; 
                else
                  EventsSinceSubscriptionStartElement = new Hl7.Fhir.Model.Integer64(value);
                OnPropertyChanged("EventsSinceSubscriptionStart");
            }
        }
        
        /// <summary>
        /// Events in this notification
        /// </summary>
        [FhirElement("eventsInNotification", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.Integer EventsInNotificationElement
        {
            get { return _EventsInNotificationElement; }
            set { _EventsInNotificationElement = value; OnPropertyChanged("EventsInNotificationElement"); }
        }
        
        private Hl7.Fhir.Model.Integer _EventsInNotificationElement;
        
        /// <summary>
        /// Events in this notification
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? EventsInNotification
        {
            get { return EventsInNotificationElement != null ? EventsInNotificationElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  EventsInNotificationElement = null; 
                else
                  EventsInNotificationElement = new Hl7.Fhir.Model.Integer(value);
                OnPropertyChanged("EventsInNotification");
            }
        }
        
        /// <summary>
        /// Reference to the Subscription responsible for this notification
        /// </summary>
        [FhirElement("subscription", InSummary=true, Order=120)]
        [CLSCompliant(false)]
		[References("Subscription")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subscription
        {
            get { return _Subscription; }
            set { _Subscription = value; OnPropertyChanged("Subscription"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subscription;
        
        /// <summary>
        /// requested | active | error | off
        /// </summary>
        [FhirElement("status", InSummary=true, Order=130)]
        [DataMember]
        public Code<Hl7.Fhir.Model.SubscriptionState> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.SubscriptionState> _StatusElement;
        
        /// <summary>
        /// requested | active | error | off
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.SubscriptionState? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.SubscriptionState>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Reference to the SubscriptionTopic this notification relates to
        /// </summary>
        [FhirElement("topic", InSummary=true, Order=140)]
        [CLSCompliant(false)]
		[References("SubscriptionTopic")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Topic
        {
            get { return _Topic; }
            set { _Topic = value; OnPropertyChanged("Topic"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Topic;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as SubscriptionStatus;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(NotificationTypeElement != null) dest.NotificationTypeElement = (Code<Hl7.Fhir.Model.SubscriptionStatus.SubscriptionNotificationType>)NotificationTypeElement.DeepCopy();
                if(EventsSinceSubscriptionStartElement != null) dest.EventsSinceSubscriptionStartElement = (Hl7.Fhir.Model.Integer64)EventsSinceSubscriptionStartElement.DeepCopy();
                if(EventsInNotificationElement != null) dest.EventsInNotificationElement = (Hl7.Fhir.Model.Integer)EventsInNotificationElement.DeepCopy();
                if(Subscription != null) dest.Subscription = (Hl7.Fhir.Model.ResourceReference)Subscription.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.SubscriptionState>)StatusElement.DeepCopy();
                if(Topic != null) dest.Topic = (Hl7.Fhir.Model.ResourceReference)Topic.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new SubscriptionStatus());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as SubscriptionStatus;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(NotificationTypeElement, otherT.NotificationTypeElement)) return false;
            if( !DeepComparable.Matches(EventsSinceSubscriptionStartElement, otherT.EventsSinceSubscriptionStartElement)) return false;
            if( !DeepComparable.Matches(EventsInNotificationElement, otherT.EventsInNotificationElement)) return false;
            if( !DeepComparable.Matches(Subscription, otherT.Subscription)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Topic, otherT.Topic)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as SubscriptionStatus;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(NotificationTypeElement, otherT.NotificationTypeElement)) return false;
            if( !DeepComparable.IsExactly(EventsSinceSubscriptionStartElement, otherT.EventsSinceSubscriptionStartElement)) return false;
            if( !DeepComparable.IsExactly(EventsInNotificationElement, otherT.EventsInNotificationElement)) return false;
            if( !DeepComparable.IsExactly(Subscription, otherT.Subscription)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Topic, otherT.Topic)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				if (NotificationTypeElement != null) yield return NotificationTypeElement;
				if (EventsSinceSubscriptionStartElement != null) yield return EventsSinceSubscriptionStartElement;
				if (EventsInNotificationElement != null) yield return EventsInNotificationElement;
				if (Subscription != null) yield return Subscription;
				if (StatusElement != null) yield return StatusElement;
				if (Topic != null) yield return Topic;
            }
        }

        [NotMapped]
        public override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (NotificationTypeElement != null) yield return new ElementValue("notificationType", NotificationTypeElement);
                if (EventsSinceSubscriptionStartElement != null) yield return new ElementValue("eventsSinceSubscriptionStart", EventsSinceSubscriptionStartElement);
                if (EventsInNotificationElement != null) yield return new ElementValue("eventsInNotification", EventsInNotificationElement);
                if (Subscription != null) yield return new ElementValue("subscription", Subscription);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (Topic != null) yield return new ElementValue("topic", Topic);
            }
        }

    }
    
}
