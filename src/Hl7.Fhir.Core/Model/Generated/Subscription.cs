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
// Generated for FHIR v1.0.2
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A server push subscription criteria
    /// </summary>
    [FhirType("Subscription", IsResource=true)]
    [DataContract]
    public partial class Subscription : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Subscription; } }
        [NotMapped]
        public override string TypeName { get { return "Subscription"; } }
        
        /// <summary>
        /// The status of a subscription.
        /// (url: http://hl7.org/fhir/ValueSet/subscription-status)
        /// </summary>
        [FhirEnumeration("SubscriptionStatus")]
        public enum SubscriptionStatus
        {
            /// <summary>
            /// The client has requested the subscription, and the server has not yet set it up.
            /// (system: http://hl7.org/fhir/subscription-status)
            /// </summary>
            [EnumLiteral("requested", "http://hl7.org/fhir/subscription-status"), Description("Requested")]
            Requested,
            /// <summary>
            /// The subscription is active.
            /// (system: http://hl7.org/fhir/subscription-status)
            /// </summary>
            [EnumLiteral("active", "http://hl7.org/fhir/subscription-status"), Description("Active")]
            Active,
            /// <summary>
            /// The server has an error executing the notification.
            /// (system: http://hl7.org/fhir/subscription-status)
            /// </summary>
            [EnumLiteral("error", "http://hl7.org/fhir/subscription-status"), Description("Error")]
            Error,
            /// <summary>
            /// Too many errors have occurred or the subscription has expired.
            /// (system: http://hl7.org/fhir/subscription-status)
            /// </summary>
            [EnumLiteral("off", "http://hl7.org/fhir/subscription-status"), Description("Off")]
            Off,
        }

        /// <summary>
        /// The type of method used to execute a subscription.
        /// (url: http://hl7.org/fhir/ValueSet/subscription-channel-type)
        /// </summary>
        [FhirEnumeration("SubscriptionChannelType")]
        public enum SubscriptionChannelType
        {
            /// <summary>
            /// The channel is executed by making a post to the URI. If a payload is included, the URL is interpreted as the service base, and an update (PUT) is made.
            /// (system: http://hl7.org/fhir/subscription-channel-type)
            /// </summary>
            [EnumLiteral("rest-hook", "http://hl7.org/fhir/subscription-channel-type"), Description("Rest Hook")]
            RestHook,
            /// <summary>
            /// The channel is executed by sending a packet across a web socket connection maintained by the client. The URL identifies the websocket, and the client binds to this URL.
            /// (system: http://hl7.org/fhir/subscription-channel-type)
            /// </summary>
            [EnumLiteral("websocket", "http://hl7.org/fhir/subscription-channel-type"), Description("Websocket")]
            Websocket,
            /// <summary>
            /// The channel is executed by sending an email to the email addressed in the URI (which must be a mailto:).
            /// (system: http://hl7.org/fhir/subscription-channel-type)
            /// </summary>
            [EnumLiteral("email", "http://hl7.org/fhir/subscription-channel-type"), Description("Email")]
            Email,
            /// <summary>
            /// The channel is executed by sending an SMS message to the phone number identified in the URL (tel:).
            /// (system: http://hl7.org/fhir/subscription-channel-type)
            /// </summary>
            [EnumLiteral("sms", "http://hl7.org/fhir/subscription-channel-type"), Description("SMS")]
            Sms,
            /// <summary>
            /// The channel is executed by sending a message (e.g. a Bundle with a MessageHeader resource etc.) to the application identified in the URI.
            /// (system: http://hl7.org/fhir/subscription-channel-type)
            /// </summary>
            [EnumLiteral("message", "http://hl7.org/fhir/subscription-channel-type"), Description("Message")]
            Message,
        }

        [FhirType("ChannelComponent")]
        [DataContract]
        public partial class ChannelComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ChannelComponent"; } }
            
            /// <summary>
            /// rest-hook | websocket | email | sms | message
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Subscription.SubscriptionChannelType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.Subscription.SubscriptionChannelType> _TypeElement;
            
            /// <summary>
            /// rest-hook | websocket | email | sms | message
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Subscription.SubscriptionChannelType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        TypeElement = null; 
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.Subscription.SubscriptionChannelType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Where the channel points to
            /// </summary>
            [FhirElement("endpoint", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri EndpointElement
            {
                get { return _EndpointElement; }
                set { _EndpointElement = value; OnPropertyChanged("EndpointElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _EndpointElement;
            
            /// <summary>
            /// Where the channel points to
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Endpoint
            {
                get { return EndpointElement != null ? EndpointElement.Value : null; }
                set
                {
                    if (value == null)
                        EndpointElement = null; 
                    else
                        EndpointElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Endpoint");
                }
            }
            
            /// <summary>
            /// Mimetype to send, or blank for no payload
            /// </summary>
            [FhirElement("payload", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PayloadElement
            {
                get { return _PayloadElement; }
                set { _PayloadElement = value; OnPropertyChanged("PayloadElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PayloadElement;
            
            /// <summary>
            /// Mimetype to send, or blank for no payload
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Payload
            {
                get { return PayloadElement != null ? PayloadElement.Value : null; }
                set
                {
                    if (value == null)
                        PayloadElement = null; 
                    else
                        PayloadElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Payload");
                }
            }
            
            /// <summary>
            /// Usage depends on the channel type
            /// </summary>
            [FhirElement("header", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString HeaderElement
            {
                get { return _HeaderElement; }
                set { _HeaderElement = value; OnPropertyChanged("HeaderElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _HeaderElement;
            
            /// <summary>
            /// Usage depends on the channel type
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Header
            {
                get { return HeaderElement != null ? HeaderElement.Value : null; }
                set
                {
                    if (value == null)
                        HeaderElement = null; 
                    else
                        HeaderElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Header");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ChannelComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.Subscription.SubscriptionChannelType>)TypeElement.DeepCopy();
                    if(EndpointElement != null) dest.EndpointElement = (Hl7.Fhir.Model.FhirUri)EndpointElement.DeepCopy();
                    if(PayloadElement != null) dest.PayloadElement = (Hl7.Fhir.Model.FhirString)PayloadElement.DeepCopy();
                    if(HeaderElement != null) dest.HeaderElement = (Hl7.Fhir.Model.FhirString)HeaderElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ChannelComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ChannelComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(EndpointElement, otherT.EndpointElement)) return false;
                if( !DeepComparable.Matches(PayloadElement, otherT.PayloadElement)) return false;
                if( !DeepComparable.Matches(HeaderElement, otherT.HeaderElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ChannelComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(EndpointElement, otherT.EndpointElement)) return false;
                if( !DeepComparable.IsExactly(PayloadElement, otherT.PayloadElement)) return false;
                if( !DeepComparable.IsExactly(HeaderElement, otherT.HeaderElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (TypeElement != null) yield return TypeElement;
                    if (EndpointElement != null) yield return EndpointElement;
                    if (PayloadElement != null) yield return PayloadElement;
                    if (HeaderElement != null) yield return HeaderElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                    if (EndpointElement != null) yield return new ElementValue("endpoint", EndpointElement);
                    if (PayloadElement != null) yield return new ElementValue("payload", PayloadElement);
                    if (HeaderElement != null) yield return new ElementValue("header", HeaderElement);
                }
            }

            
        }
        
        
        /// <summary>
        /// Rule for server push criteria
        /// </summary>
        [FhirElement("criteria", InSummary=true, Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString CriteriaElement
        {
            get { return _CriteriaElement; }
            set { _CriteriaElement = value; OnPropertyChanged("CriteriaElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _CriteriaElement;
        
        /// <summary>
        /// Rule for server push criteria
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Criteria
        {
            get { return CriteriaElement != null ? CriteriaElement.Value : null; }
            set
            {
                if (value == null)
                  CriteriaElement = null; 
                else
                  CriteriaElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Criteria");
            }
        }
        
        /// <summary>
        /// Contact details for source (e.g. troubleshooting)
        /// </summary>
        [FhirElement("contact", InSummary=true, Order=100)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ContactPoint> Contact
        {
            get { if(_Contact==null) _Contact = new List<Hl7.Fhir.Model.ContactPoint>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<Hl7.Fhir.Model.ContactPoint> _Contact;
        
        /// <summary>
        /// Description of why this subscription was created
        /// </summary>
        [FhirElement("reason", InSummary=true, Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ReasonElement
        {
            get { return _ReasonElement; }
            set { _ReasonElement = value; OnPropertyChanged("ReasonElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _ReasonElement;
        
        /// <summary>
        /// Description of why this subscription was created
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Reason
        {
            get { return ReasonElement != null ? ReasonElement.Value : null; }
            set
            {
                if (value == null)
                  ReasonElement = null; 
                else
                  ReasonElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Reason");
            }
        }
        
        /// <summary>
        /// requested | active | error | off
        /// </summary>
        [FhirElement("status", InSummary=true, Order=120)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Subscription.SubscriptionStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Subscription.SubscriptionStatus> _StatusElement;
        
        /// <summary>
        /// requested | active | error | off
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Subscription.SubscriptionStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.Subscription.SubscriptionStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Latest error note
        /// </summary>
        [FhirElement("error", InSummary=true, Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ErrorElement
        {
            get { return _ErrorElement; }
            set { _ErrorElement = value; OnPropertyChanged("ErrorElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _ErrorElement;
        
        /// <summary>
        /// Latest error note
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Error
        {
            get { return ErrorElement != null ? ErrorElement.Value : null; }
            set
            {
                if (value == null)
                  ErrorElement = null; 
                else
                  ErrorElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Error");
            }
        }
        
        /// <summary>
        /// The channel on which to report matches to the criteria
        /// </summary>
        [FhirElement("channel", InSummary=true, Order=140)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Subscription.ChannelComponent Channel
        {
            get { return _Channel; }
            set { _Channel = value; OnPropertyChanged("Channel"); }
        }
        
        private Hl7.Fhir.Model.Subscription.ChannelComponent _Channel;
        
        /// <summary>
        /// When to automatically delete the subscription
        /// </summary>
        [FhirElement("end", InSummary=true, Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.Instant EndElement
        {
            get { return _EndElement; }
            set { _EndElement = value; OnPropertyChanged("EndElement"); }
        }
        
        private Hl7.Fhir.Model.Instant _EndElement;
        
        /// <summary>
        /// When to automatically delete the subscription
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public DateTimeOffset? End
        {
            get { return EndElement != null ? EndElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  EndElement = null; 
                else
                  EndElement = new Hl7.Fhir.Model.Instant(value);
                OnPropertyChanged("End");
            }
        }
        
        /// <summary>
        /// A tag to add to matching resources
        /// </summary>
        [FhirElement("tag", InSummary=true, Order=160)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> Tag
        {
            get { if(_Tag==null) _Tag = new List<Hl7.Fhir.Model.Coding>(); return _Tag; }
            set { _Tag = value; OnPropertyChanged("Tag"); }
        }
        
        private List<Hl7.Fhir.Model.Coding> _Tag;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Subscription;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(CriteriaElement != null) dest.CriteriaElement = (Hl7.Fhir.Model.FhirString)CriteriaElement.DeepCopy();
                if(Contact != null) dest.Contact = new List<Hl7.Fhir.Model.ContactPoint>(Contact.DeepCopy());
                if(ReasonElement != null) dest.ReasonElement = (Hl7.Fhir.Model.FhirString)ReasonElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Subscription.SubscriptionStatus>)StatusElement.DeepCopy();
                if(ErrorElement != null) dest.ErrorElement = (Hl7.Fhir.Model.FhirString)ErrorElement.DeepCopy();
                if(Channel != null) dest.Channel = (Hl7.Fhir.Model.Subscription.ChannelComponent)Channel.DeepCopy();
                if(EndElement != null) dest.EndElement = (Hl7.Fhir.Model.Instant)EndElement.DeepCopy();
                if(Tag != null) dest.Tag = new List<Hl7.Fhir.Model.Coding>(Tag.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Subscription());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Subscription;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(CriteriaElement, otherT.CriteriaElement)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(ReasonElement, otherT.ReasonElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(ErrorElement, otherT.ErrorElement)) return false;
            if( !DeepComparable.Matches(Channel, otherT.Channel)) return false;
            if( !DeepComparable.Matches(EndElement, otherT.EndElement)) return false;
            if( !DeepComparable.Matches(Tag, otherT.Tag)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Subscription;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(CriteriaElement, otherT.CriteriaElement)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(ReasonElement, otherT.ReasonElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(ErrorElement, otherT.ErrorElement)) return false;
            if( !DeepComparable.IsExactly(Channel, otherT.Channel)) return false;
            if( !DeepComparable.IsExactly(EndElement, otherT.EndElement)) return false;
            if( !DeepComparable.IsExactly(Tag, otherT.Tag)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				if (CriteriaElement != null) yield return CriteriaElement;
				foreach (var elem in Contact) { if (elem != null) yield return elem; }
				if (ReasonElement != null) yield return ReasonElement;
				if (StatusElement != null) yield return StatusElement;
				if (ErrorElement != null) yield return ErrorElement;
				if (Channel != null) yield return Channel;
				if (EndElement != null) yield return EndElement;
				foreach (var elem in Tag) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (CriteriaElement != null) yield return new ElementValue("criteria", CriteriaElement);
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                if (ReasonElement != null) yield return new ElementValue("reason", ReasonElement);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (ErrorElement != null) yield return new ElementValue("error", ErrorElement);
                if (Channel != null) yield return new ElementValue("channel", Channel);
                if (EndElement != null) yield return new ElementValue("end", EndElement);
                foreach (var elem in Tag) { if (elem != null) yield return new ElementValue("tag", elem); }
            }
        }

    }
    
}
