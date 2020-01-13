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
// Generated for FHIR v4.2.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Notification about a Topic
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
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/subscription-status)
            /// </summary>
            [EnumLiteral("requested", "http://hl7.org/fhir/subscription-status"), Description("Requested")]
            Requested,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/subscription-status)
            /// </summary>
            [EnumLiteral("active", "http://hl7.org/fhir/subscription-status"), Description("Active")]
            Active,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/subscription-status)
            /// </summary>
            [EnumLiteral("error", "http://hl7.org/fhir/subscription-status"), Description("Error")]
            Error,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/subscription-status)
            /// </summary>
            [EnumLiteral("off", "http://hl7.org/fhir/subscription-status"), Description("Off")]
            Off,
        }

        /// <summary>
        /// Operator to apply to filter label.
        /// (url: http://hl7.org/fhir/ValueSet/subscription-operator)
        /// </summary>
        [FhirEnumeration("SubscriptionFilterByMatchType")]
        public enum SubscriptionFilterByMatchType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/subscription-operator)
            /// </summary>
            [EnumLiteral("=", "http://hl7.org/fhir/subscription-operator"), Description("=")]
            Equal,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/subscription-operator)
            /// </summary>
            [EnumLiteral("in", "http://hl7.org/fhir/subscription-operator"), Description("in")]
            In,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/subscription-operator)
            /// </summary>
            [EnumLiteral("not-in", "http://hl7.org/fhir/subscription-operator"), Description("not-in")]
            NotIn,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/subscription-operator)
            /// </summary>
            [EnumLiteral("above", "http://hl7.org/fhir/subscription-operator"), Description("above")]
            Above,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/subscription-operator)
            /// </summary>
            [EnumLiteral("below", "http://hl7.org/fhir/subscription-operator"), Description("below")]
            Below,
        }

        /// <summary>
        /// Codes to represent how much resource content to send in the notification payload.
        /// (url: http://hl7.org/fhir/ValueSet/subscription-payload-content)
        /// </summary>
        [FhirEnumeration("SubscriptionPayloadContent")]
        public enum SubscriptionPayloadContent
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/subscription-payload-content)
            /// </summary>
            [EnumLiteral("empty", "http://hl7.org/fhir/subscription-payload-content"), Description("empty")]
            Empty,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/subscription-payload-content)
            /// </summary>
            [EnumLiteral("id-only", "http://hl7.org/fhir/subscription-payload-content"), Description("id-only")]
            IdOnly,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/subscription-payload-content)
            /// </summary>
            [EnumLiteral("full-resource", "http://hl7.org/fhir/subscription-payload-content"), Description("full-resource")]
            FullResource,
        }

        [FhirType("FilterByComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class FilterByComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "FilterByComponent"; } }
            
            /// <summary>
            /// Filter label defined in Topic
            /// </summary>
            [FhirElement("name", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Filter label defined in Topic
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
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
            /// = | in | not-in | above | below
            /// </summary>
            [FhirElement("matchType", InSummary=true, Order=50)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Subscription.SubscriptionFilterByMatchType> MatchTypeElement
            {
                get { return _MatchTypeElement; }
                set { _MatchTypeElement = value; OnPropertyChanged("MatchTypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.Subscription.SubscriptionFilterByMatchType> _MatchTypeElement;
            
            /// <summary>
            /// = | in | not-in | above | below
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Subscription.SubscriptionFilterByMatchType? MatchType
            {
                get { return MatchTypeElement != null ? MatchTypeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        MatchTypeElement = null; 
                    else
                        MatchTypeElement = new Code<Hl7.Fhir.Model.Subscription.SubscriptionFilterByMatchType>(value);
                    OnPropertyChanged("MatchType");
                }
            }
            
            /// <summary>
            /// Literal value or resource path
            /// </summary>
            [FhirElement("value", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ValueElement;
            
            /// <summary>
            /// Literal value or resource path
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Value
            {
                get { return ValueElement != null ? ValueElement.Value : null; }
                set
                {
                    if (value == null)
                        ValueElement = null; 
                    else
                        ValueElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Value");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as FilterByComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(MatchTypeElement != null) dest.MatchTypeElement = (Code<Hl7.Fhir.Model.Subscription.SubscriptionFilterByMatchType>)MatchTypeElement.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirString)ValueElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new FilterByComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as FilterByComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(MatchTypeElement, otherT.MatchTypeElement)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as FilterByComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(MatchTypeElement, otherT.MatchTypeElement)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NameElement != null) yield return NameElement;
                    if (MatchTypeElement != null) yield return MatchTypeElement;
                    if (ValueElement != null) yield return ValueElement;
                }
            }

            [NotMapped]
            public override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (MatchTypeElement != null) yield return new ElementValue("matchType", MatchTypeElement);
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                }
            }

            
        }
        
        
        [FhirType("ChannelComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ChannelComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ChannelComponent"; } }
            
            /// <summary>
            /// rest-hook | websocket | email | message
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Where the channel points to
            /// </summary>
            [FhirElement("endpoint", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUrl EndpointElement
            {
                get { return _EndpointElement; }
                set { _EndpointElement = value; OnPropertyChanged("EndpointElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUrl _EndpointElement;
            
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
                        EndpointElement = new Hl7.Fhir.Model.FhirUrl(value);
                    OnPropertyChanged("Endpoint");
                }
            }
            
            /// <summary>
            /// Usage depends on the channel type
            /// </summary>
            [FhirElement("header", InSummary=true, Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> HeaderElement
            {
                get { if(_HeaderElement==null) _HeaderElement = new List<Hl7.Fhir.Model.FhirString>(); return _HeaderElement; }
                set { _HeaderElement = value; OnPropertyChanged("HeaderElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _HeaderElement;
            
            /// <summary>
            /// Usage depends on the channel type
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Header
            {
                get { return HeaderElement != null ? HeaderElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        HeaderElement = null; 
                    else
                        HeaderElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("Header");
                }
            }
            
            /// <summary>
            /// Interval in seconds to send 'heartbeat' notification
            /// </summary>
            [FhirElement("heartbeatPeriod", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.UnsignedInt HeartbeatPeriodElement
            {
                get { return _HeartbeatPeriodElement; }
                set { _HeartbeatPeriodElement = value; OnPropertyChanged("HeartbeatPeriodElement"); }
            }
            
            private Hl7.Fhir.Model.UnsignedInt _HeartbeatPeriodElement;
            
            /// <summary>
            /// Interval in seconds to send 'heartbeat' notification
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? HeartbeatPeriod
            {
                get { return HeartbeatPeriodElement != null ? HeartbeatPeriodElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        HeartbeatPeriodElement = null; 
                    else
                        HeartbeatPeriodElement = new Hl7.Fhir.Model.UnsignedInt(value);
                    OnPropertyChanged("HeartbeatPeriod");
                }
            }
            
            /// <summary>
            /// Payload definition
            /// </summary>
            [FhirElement("payload", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Subscription.PayloadComponent Payload
            {
                get { return _Payload; }
                set { _Payload = value; OnPropertyChanged("Payload"); }
            }
            
            private Hl7.Fhir.Model.Subscription.PayloadComponent _Payload;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ChannelComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(EndpointElement != null) dest.EndpointElement = (Hl7.Fhir.Model.FhirUrl)EndpointElement.DeepCopy();
                    if(HeaderElement != null) dest.HeaderElement = new List<Hl7.Fhir.Model.FhirString>(HeaderElement.DeepCopy());
                    if(HeartbeatPeriodElement != null) dest.HeartbeatPeriodElement = (Hl7.Fhir.Model.UnsignedInt)HeartbeatPeriodElement.DeepCopy();
                    if(Payload != null) dest.Payload = (Hl7.Fhir.Model.Subscription.PayloadComponent)Payload.DeepCopy();
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
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(EndpointElement, otherT.EndpointElement)) return false;
                if( !DeepComparable.Matches(HeaderElement, otherT.HeaderElement)) return false;
                if( !DeepComparable.Matches(HeartbeatPeriodElement, otherT.HeartbeatPeriodElement)) return false;
                if( !DeepComparable.Matches(Payload, otherT.Payload)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ChannelComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(EndpointElement, otherT.EndpointElement)) return false;
                if( !DeepComparable.IsExactly(HeaderElement, otherT.HeaderElement)) return false;
                if( !DeepComparable.IsExactly(HeartbeatPeriodElement, otherT.HeartbeatPeriodElement)) return false;
                if( !DeepComparable.IsExactly(Payload, otherT.Payload)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    if (EndpointElement != null) yield return EndpointElement;
                    foreach (var elem in HeaderElement) { if (elem != null) yield return elem; }
                    if (HeartbeatPeriodElement != null) yield return HeartbeatPeriodElement;
                    if (Payload != null) yield return Payload;
                }
            }

            [NotMapped]
            public override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (EndpointElement != null) yield return new ElementValue("endpoint", EndpointElement);
                    foreach (var elem in HeaderElement) { if (elem != null) yield return new ElementValue("header", elem); }
                    if (HeartbeatPeriodElement != null) yield return new ElementValue("heartbeatPeriod", HeartbeatPeriodElement);
                    if (Payload != null) yield return new ElementValue("payload", Payload);
                }
            }

            
        }
        
        
        [FhirType("PayloadComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class PayloadComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "PayloadComponent"; } }
            
            /// <summary>
            /// MIME type to send, or omit for no payload
            /// </summary>
            [FhirElement("contentType", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Code ContentTypeElement
            {
                get { return _ContentTypeElement; }
                set { _ContentTypeElement = value; OnPropertyChanged("ContentTypeElement"); }
            }
            
            private Hl7.Fhir.Model.Code _ContentTypeElement;
            
            /// <summary>
            /// MIME type to send, or omit for no payload
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ContentType
            {
                get { return ContentTypeElement != null ? ContentTypeElement.Value : null; }
                set
                {
                    if (value == null)
                        ContentTypeElement = null; 
                    else
                        ContentTypeElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("ContentType");
                }
            }
            
            /// <summary>
            /// empty | id-only | full-resource
            /// </summary>
            [FhirElement("content", InSummary=true, Order=50)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Subscription.SubscriptionPayloadContent> ContentElement
            {
                get { return _ContentElement; }
                set { _ContentElement = value; OnPropertyChanged("ContentElement"); }
            }
            
            private Code<Hl7.Fhir.Model.Subscription.SubscriptionPayloadContent> _ContentElement;
            
            /// <summary>
            /// empty | id-only | full-resource
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Subscription.SubscriptionPayloadContent? Content
            {
                get { return ContentElement != null ? ContentElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        ContentElement = null; 
                    else
                        ContentElement = new Code<Hl7.Fhir.Model.Subscription.SubscriptionPayloadContent>(value);
                    OnPropertyChanged("Content");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PayloadComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ContentTypeElement != null) dest.ContentTypeElement = (Hl7.Fhir.Model.Code)ContentTypeElement.DeepCopy();
                    if(ContentElement != null) dest.ContentElement = (Code<Hl7.Fhir.Model.Subscription.SubscriptionPayloadContent>)ContentElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new PayloadComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PayloadComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ContentTypeElement, otherT.ContentTypeElement)) return false;
                if( !DeepComparable.Matches(ContentElement, otherT.ContentElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PayloadComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ContentTypeElement, otherT.ContentTypeElement)) return false;
                if( !DeepComparable.IsExactly(ContentElement, otherT.ContentElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (ContentTypeElement != null) yield return ContentTypeElement;
                    if (ContentElement != null) yield return ContentElement;
                }
            }

            [NotMapped]
            public override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (ContentTypeElement != null) yield return new ElementValue("contentType", ContentTypeElement);
                    if (ContentElement != null) yield return new ElementValue("content", ContentElement);
                }
            }

            
        }
        
        
        /// <summary>
        /// Additional identifiers (business identifier)
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
        /// Human readable name for this subscription
        /// </summary>
        [FhirElement("name", InSummary=true, Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// Human readable name for this subscription
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
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
        /// requested | active | error | off
        /// </summary>
        [FhirElement("status", InSummary=true, Order=110)]
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
        /// Reference to the topic being subscribed to
        /// </summary>
        [FhirElement("topic", InSummary=true, Order=120)]
        [CLSCompliant(false)]
		[References("Topic")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Topic
        {
            get { return _Topic; }
            set { _Topic = value; OnPropertyChanged("Topic"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Topic;
        
        /// <summary>
        /// Contact details for source (e.g. troubleshooting)
        /// </summary>
        [FhirElement("contact", InSummary=true, Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ContactPoint> Contact
        {
            get { if(_Contact==null) _Contact = new List<Hl7.Fhir.Model.ContactPoint>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<Hl7.Fhir.Model.ContactPoint> _Contact;
        
        /// <summary>
        /// When to automatically delete the subscription
        /// </summary>
        [FhirElement("end", InSummary=true, Order=140)]
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
        /// Description of why this subscription was created
        /// </summary>
        [FhirElement("reason", InSummary=true, Order=150)]
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
        /// Criteria for narrowing the topic stream
        /// </summary>
        [FhirElement("filterBy", InSummary=true, Order=160)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Subscription.FilterByComponent> FilterBy
        {
            get { if(_FilterBy==null) _FilterBy = new List<Hl7.Fhir.Model.Subscription.FilterByComponent>(); return _FilterBy; }
            set { _FilterBy = value; OnPropertyChanged("FilterBy"); }
        }
        
        private List<Hl7.Fhir.Model.Subscription.FilterByComponent> _FilterBy;
        
        /// <summary>
        /// Latest error code or note
        /// </summary>
        [FhirElement("error", InSummary=true, Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Error
        {
            get { if(_Error==null) _Error = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Error; }
            set { _Error = value; OnPropertyChanged("Error"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Error;
        
        /// <summary>
        /// Notification event counter
        /// </summary>
        [FhirElement("eventCount", InSummary=true, Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.UnsignedInt EventCountElement
        {
            get { return _EventCountElement; }
            set { _EventCountElement = value; OnPropertyChanged("EventCountElement"); }
        }
        
        private Hl7.Fhir.Model.UnsignedInt _EventCountElement;
        
        /// <summary>
        /// Notification event counter
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? EventCount
        {
            get { return EventCountElement != null ? EventCountElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  EventCountElement = null; 
                else
                  EventCountElement = new Hl7.Fhir.Model.UnsignedInt(value);
                OnPropertyChanged("EventCount");
            }
        }
        
        /// <summary>
        /// The channel on which to report matches to the criteria
        /// </summary>
        [FhirElement("channel", InSummary=true, Order=190)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Subscription.ChannelComponent Channel
        {
            get { return _Channel; }
            set { _Channel = value; OnPropertyChanged("Channel"); }
        }
        
        private Hl7.Fhir.Model.Subscription.ChannelComponent _Channel;
        

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
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Subscription.SubscriptionStatus>)StatusElement.DeepCopy();
                if(Topic != null) dest.Topic = (Hl7.Fhir.Model.ResourceReference)Topic.DeepCopy();
                if(Contact != null) dest.Contact = new List<Hl7.Fhir.Model.ContactPoint>(Contact.DeepCopy());
                if(EndElement != null) dest.EndElement = (Hl7.Fhir.Model.Instant)EndElement.DeepCopy();
                if(ReasonElement != null) dest.ReasonElement = (Hl7.Fhir.Model.FhirString)ReasonElement.DeepCopy();
                if(FilterBy != null) dest.FilterBy = new List<Hl7.Fhir.Model.Subscription.FilterByComponent>(FilterBy.DeepCopy());
                if(Error != null) dest.Error = new List<Hl7.Fhir.Model.CodeableConcept>(Error.DeepCopy());
                if(EventCountElement != null) dest.EventCountElement = (Hl7.Fhir.Model.UnsignedInt)EventCountElement.DeepCopy();
                if(Channel != null) dest.Channel = (Hl7.Fhir.Model.Subscription.ChannelComponent)Channel.DeepCopy();
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
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Topic, otherT.Topic)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(EndElement, otherT.EndElement)) return false;
            if( !DeepComparable.Matches(ReasonElement, otherT.ReasonElement)) return false;
            if( !DeepComparable.Matches(FilterBy, otherT.FilterBy)) return false;
            if( !DeepComparable.Matches(Error, otherT.Error)) return false;
            if( !DeepComparable.Matches(EventCountElement, otherT.EventCountElement)) return false;
            if( !DeepComparable.Matches(Channel, otherT.Channel)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Subscription;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Topic, otherT.Topic)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(EndElement, otherT.EndElement)) return false;
            if( !DeepComparable.IsExactly(ReasonElement, otherT.ReasonElement)) return false;
            if( !DeepComparable.IsExactly(FilterBy, otherT.FilterBy)) return false;
            if( !DeepComparable.IsExactly(Error, otherT.Error)) return false;
            if( !DeepComparable.IsExactly(EventCountElement, otherT.EventCountElement)) return false;
            if( !DeepComparable.IsExactly(Channel, otherT.Channel)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				if (NameElement != null) yield return NameElement;
				if (StatusElement != null) yield return StatusElement;
				if (Topic != null) yield return Topic;
				foreach (var elem in Contact) { if (elem != null) yield return elem; }
				if (EndElement != null) yield return EndElement;
				if (ReasonElement != null) yield return ReasonElement;
				foreach (var elem in FilterBy) { if (elem != null) yield return elem; }
				foreach (var elem in Error) { if (elem != null) yield return elem; }
				if (EventCountElement != null) yield return EventCountElement;
				if (Channel != null) yield return Channel;
            }
        }

        [NotMapped]
        public override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (NameElement != null) yield return new ElementValue("name", NameElement);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (Topic != null) yield return new ElementValue("topic", Topic);
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                if (EndElement != null) yield return new ElementValue("end", EndElement);
                if (ReasonElement != null) yield return new ElementValue("reason", ReasonElement);
                foreach (var elem in FilterBy) { if (elem != null) yield return new ElementValue("filterBy", elem); }
                foreach (var elem in Error) { if (elem != null) yield return new ElementValue("error", elem); }
                if (EventCountElement != null) yield return new ElementValue("eventCount", EventCountElement);
                if (Channel != null) yield return new ElementValue("channel", Channel);
            }
        }

    }
    
}
