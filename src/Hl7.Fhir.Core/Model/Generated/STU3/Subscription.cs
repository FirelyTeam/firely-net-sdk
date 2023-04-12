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
namespace Hl7.Fhir.Model.STU3
{
    /// <summary>
    /// A server push subscription criteria
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.STU3, "Subscription", IsResource=true)]
    [DataContract]
    public partial class Subscription : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.ISubscription, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Subscription; } }
        [NotMapped]
        public override string TypeName { get { return "Subscription"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "ChannelComponent")]
        [DataContract]
        public partial class ChannelComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ISubscriptionChannelComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ChannelComponent"; } }
            
            /// <summary>
            /// rest-hook | websocket | email | sms | message
            /// </summary>
            [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.SubscriptionChannelType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.SubscriptionChannelType> _TypeElement;
            
            /// <summary>
            /// rest-hook | websocket | email | sms | message
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.SubscriptionChannelType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (value == null)
                        TypeElement = null;
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.SubscriptionChannelType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Where the channel points to
            /// </summary>
            [FhirElement("endpoint", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
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
            /// Mimetype to send, or omit for no payload
            /// </summary>
            [FhirElement("payload", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PayloadElement
            {
                get { return _PayloadElement; }
                set { _PayloadElement = value; OnPropertyChanged("PayloadElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PayloadElement;
            
            /// <summary>
            /// Mimetype to send, or omit for no payload
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
            [FhirElement("header", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ChannelComponent");
                base.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); TypeElement?.Serialize(sink);
                sink.Element("endpoint", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); EndpointElement?.Serialize(sink);
                sink.Element("payload", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PayloadElement?.Serialize(sink);
                sink.BeginList("header", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                sink.Serialize(HeaderElement);
                sink.End();
                sink.End();
            }
        
            internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
            {
                if (base.SetElementFromSource(elementName, source))
                {
                    return true;
                }
                switch (elementName)
                {
                    case "type":
                        TypeElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.SubscriptionChannelType>>();
                        return true;
                    case "endpoint":
                        EndpointElement = source.Get<Hl7.Fhir.Model.FhirUri>();
                        return true;
                    case "payload":
                        PayloadElement = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "header":
                        HeaderElement = source.GetList<Hl7.Fhir.Model.FhirString>();
                        return true;
                }
                return false;
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "type":
                        TypeElement = source.PopulateValue(TypeElement);
                        return true;
                    case "_type":
                        TypeElement = source.Populate(TypeElement);
                        return true;
                    case "endpoint":
                        EndpointElement = source.PopulateValue(EndpointElement);
                        return true;
                    case "_endpoint":
                        EndpointElement = source.Populate(EndpointElement);
                        return true;
                    case "payload":
                        PayloadElement = source.PopulateValue(PayloadElement);
                        return true;
                    case "_payload":
                        PayloadElement = source.Populate(PayloadElement);
                        return true;
                    case "header":
                    case "_header":
                        source.SetList(this, jsonPropertyName);
                        return true;
                }
                return false;
            }
            
            internal override bool SetListElementFromJson(string jsonPropertyName, int index, ref Serialization.JsonSource source)
            {
                if (base.SetListElementFromJson(jsonPropertyName, index, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "header":
                        source.PopulatePrimitiveListItemValue(HeaderElement, index);
                        return true;
                    case "_header":
                        source.PopulatePrimitiveListItem(HeaderElement, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ChannelComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.SubscriptionChannelType>)TypeElement.DeepCopy();
                    if(EndpointElement != null) dest.EndpointElement = (Hl7.Fhir.Model.FhirUri)EndpointElement.DeepCopy();
                    if(PayloadElement != null) dest.PayloadElement = (Hl7.Fhir.Model.FhirString)PayloadElement.DeepCopy();
                    if(HeaderElement != null) dest.HeaderElement = new List<Hl7.Fhir.Model.FhirString>(HeaderElement.DeepCopy());
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
                    foreach (var elem in HeaderElement) { if (elem != null) yield return elem; }
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
                    foreach (var elem in HeaderElement) { if (elem != null) yield return new ElementValue("header", elem); }
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IContactPoint> Hl7.Fhir.Model.ISubscription.Contact { get { return Contact; } }
        
        [NotMapped]
        Hl7.Fhir.Model.ISubscriptionChannelComponent Hl7.Fhir.Model.ISubscription.Channel { get { return Channel; } }
    
        
        /// <summary>
        /// requested | active | error | off
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.SubscriptionStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.SubscriptionStatus> _StatusElement;
        
        /// <summary>
        /// requested | active | error | off
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.SubscriptionStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.SubscriptionStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Contact details for source (e.g. troubleshooting)
        /// </summary>
        [FhirElement("contact", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.STU3.ContactPoint> Contact
        {
            get { if(_Contact==null) _Contact = new List<Hl7.Fhir.Model.STU3.ContactPoint>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<Hl7.Fhir.Model.STU3.ContactPoint> _Contact;
        
        /// <summary>
        /// When to automatically delete the subscription
        /// </summary>
        [FhirElement("end", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
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
                if (value == null)
                    EndElement = null;
                else
                    EndElement = new Hl7.Fhir.Model.Instant(value);
                OnPropertyChanged("End");
            }
        }
        
        /// <summary>
        /// Description of why this subscription was created
        /// </summary>
        [FhirElement("reason", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
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
        /// Rule for server push criteria
        /// </summary>
        [FhirElement("criteria", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
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
        /// Latest error note
        /// </summary>
        [FhirElement("error", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
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
        [FhirElement("channel", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public ChannelComponent Channel
        {
            get { return _Channel; }
            set { _Channel = value; OnPropertyChanged("Channel"); }
        }
        
        private ChannelComponent _Channel;
        
        /// <summary>
        /// A tag to add to matching resources
        /// </summary>
        [FhirElement("tag", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Coding> Tag
        {
            get { if(_Tag==null) _Tag = new List<Hl7.Fhir.Model.Coding>(); return _Tag; }
            set { _Tag = value; OnPropertyChanged("Tag"); }
        }
        
        private List<Hl7.Fhir.Model.Coding> _Tag;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Subscription;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.SubscriptionStatus>)StatusElement.DeepCopy();
                if(Contact != null) dest.Contact = new List<Hl7.Fhir.Model.STU3.ContactPoint>(Contact.DeepCopy());
                if(EndElement != null) dest.EndElement = (Hl7.Fhir.Model.Instant)EndElement.DeepCopy();
                if(ReasonElement != null) dest.ReasonElement = (Hl7.Fhir.Model.FhirString)ReasonElement.DeepCopy();
                if(CriteriaElement != null) dest.CriteriaElement = (Hl7.Fhir.Model.FhirString)CriteriaElement.DeepCopy();
                if(ErrorElement != null) dest.ErrorElement = (Hl7.Fhir.Model.FhirString)ErrorElement.DeepCopy();
                if(Channel != null) dest.Channel = (ChannelComponent)Channel.DeepCopy();
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
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(EndElement, otherT.EndElement)) return false;
            if( !DeepComparable.Matches(ReasonElement, otherT.ReasonElement)) return false;
            if( !DeepComparable.Matches(CriteriaElement, otherT.CriteriaElement)) return false;
            if( !DeepComparable.Matches(ErrorElement, otherT.ErrorElement)) return false;
            if( !DeepComparable.Matches(Channel, otherT.Channel)) return false;
            if( !DeepComparable.Matches(Tag, otherT.Tag)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Subscription;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(EndElement, otherT.EndElement)) return false;
            if( !DeepComparable.IsExactly(ReasonElement, otherT.ReasonElement)) return false;
            if( !DeepComparable.IsExactly(CriteriaElement, otherT.CriteriaElement)) return false;
            if( !DeepComparable.IsExactly(ErrorElement, otherT.ErrorElement)) return false;
            if( !DeepComparable.IsExactly(Channel, otherT.Channel)) return false;
            if( !DeepComparable.IsExactly(Tag, otherT.Tag)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("Subscription");
            base.Serialize(sink);
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.BeginList("contact", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Contact)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("end", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); EndElement?.Serialize(sink);
            sink.Element("reason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); ReasonElement?.Serialize(sink);
            sink.Element("criteria", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); CriteriaElement?.Serialize(sink);
            sink.Element("error", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ErrorElement?.Serialize(sink);
            sink.Element("channel", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Channel?.Serialize(sink);
            sink.BeginList("tag", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Tag)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.End();
        }
    
        internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
        {
            if (base.SetElementFromSource(elementName, source))
            {
                return true;
            }
            switch (elementName)
            {
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.SubscriptionStatus>>();
                    return true;
                case "contact":
                    Contact = source.GetList<Hl7.Fhir.Model.STU3.ContactPoint>();
                    return true;
                case "end":
                    EndElement = source.Get<Hl7.Fhir.Model.Instant>();
                    return true;
                case "reason":
                    ReasonElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "criteria":
                    CriteriaElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "error":
                    ErrorElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "channel":
                    Channel = source.Get<ChannelComponent>();
                    return true;
                case "tag":
                    Tag = source.GetList<Hl7.Fhir.Model.Coding>();
                    return true;
            }
            return false;
        }
    
        internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
        {
            if (base.SetElementFromJson(jsonPropertyName, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "contact":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "end":
                    EndElement = source.PopulateValue(EndElement);
                    return true;
                case "_end":
                    EndElement = source.Populate(EndElement);
                    return true;
                case "reason":
                    ReasonElement = source.PopulateValue(ReasonElement);
                    return true;
                case "_reason":
                    ReasonElement = source.Populate(ReasonElement);
                    return true;
                case "criteria":
                    CriteriaElement = source.PopulateValue(CriteriaElement);
                    return true;
                case "_criteria":
                    CriteriaElement = source.Populate(CriteriaElement);
                    return true;
                case "error":
                    ErrorElement = source.PopulateValue(ErrorElement);
                    return true;
                case "_error":
                    ErrorElement = source.Populate(ErrorElement);
                    return true;
                case "channel":
                    Channel = source.Populate(Channel);
                    return true;
                case "tag":
                    source.SetList(this, jsonPropertyName);
                    return true;
            }
            return false;
        }
        
        internal override bool SetListElementFromJson(string jsonPropertyName, int index, ref Serialization.JsonSource source)
        {
            if (base.SetListElementFromJson(jsonPropertyName, index, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "contact":
                    source.PopulateListItem(Contact, index);
                    return true;
                case "tag":
                    source.PopulateListItem(Tag, index);
                    return true;
            }
            return false;
        }
    
        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                if (StatusElement != null) yield return StatusElement;
                foreach (var elem in Contact) { if (elem != null) yield return elem; }
                if (EndElement != null) yield return EndElement;
                if (ReasonElement != null) yield return ReasonElement;
                if (CriteriaElement != null) yield return CriteriaElement;
                if (ErrorElement != null) yield return ErrorElement;
                if (Channel != null) yield return Channel;
                foreach (var elem in Tag) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                if (EndElement != null) yield return new ElementValue("end", EndElement);
                if (ReasonElement != null) yield return new ElementValue("reason", ReasonElement);
                if (CriteriaElement != null) yield return new ElementValue("criteria", CriteriaElement);
                if (ErrorElement != null) yield return new ElementValue("error", ErrorElement);
                if (Channel != null) yield return new ElementValue("channel", Channel);
                foreach (var elem in Tag) { if (elem != null) yield return new ElementValue("tag", elem); }
            }
        }
    
    }

}
