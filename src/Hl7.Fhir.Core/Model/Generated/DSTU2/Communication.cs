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
namespace Hl7.Fhir.Model.DSTU2
{
    /// <summary>
    /// A record of information transmitted from a sender to a receiver
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.DSTU2, "Communication", IsResource=true)]
    [DataContract]
    public partial class Communication : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.ICommunication, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Communication; } }
        [NotMapped]
        public override string TypeName { get { return "Communication"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.DSTU2, "PayloadComponent")]
        [DataContract]
        public partial class PayloadComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ICommunicationPayloadComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "PayloadComponent"; } }
            
            /// <summary>
            /// Message part content
            /// </summary>
            [FhirElement("content", InSummary=Hl7.Fhir.Model.Version.All, Order=40, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.ResourceReference))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Content
            {
                get { return _Content; }
                set { _Content = value; OnPropertyChanged("Content"); }
            }
            
            private Hl7.Fhir.Model.Element _Content;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("PayloadComponent");
                base.Serialize(sink);
                sink.Element("content", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, true); Content?.Serialize(sink);
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
                    case "contentString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Content, "content");
                        Content = source.Get<Hl7.Fhir.Model.FhirString>();
                        return true;
                    case "contentAttachment":
                        source.CheckDuplicates<Hl7.Fhir.Model.Attachment>(Content, "content");
                        Content = source.Get<Hl7.Fhir.Model.Attachment>();
                        return true;
                    case "contentReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Content, "content");
                        Content = source.Get<Hl7.Fhir.Model.ResourceReference>();
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
                    case "contentString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Content, "content");
                        Content = source.PopulateValue(Content as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "_contentString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(Content, "content");
                        Content = source.Populate(Content as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "contentAttachment":
                        source.CheckDuplicates<Hl7.Fhir.Model.Attachment>(Content, "content");
                        Content = source.Populate(Content as Hl7.Fhir.Model.Attachment);
                        return true;
                    case "contentReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Content, "content");
                        Content = source.Populate(Content as Hl7.Fhir.Model.ResourceReference);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PayloadComponent;
            
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
                 return CopyTo(new PayloadComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PayloadComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Content, otherT.Content)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PayloadComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Content, otherT.Content)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Content != null) yield return Content;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Content != null) yield return new ElementValue("content", Content);
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.ICommunicationPayloadComponent> Hl7.Fhir.Model.ICommunication.Payload { get { return Payload; } }
    
        
        /// <summary>
        /// Unique identifier
        /// </summary>
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
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
        [FhirElement("category", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
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
        [FhirElement("sender", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
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
        [FhirElement("recipient", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [References("Device","Organization","Patient","Practitioner","RelatedPerson","Group")]
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
        [FhirElement("payload", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<PayloadComponent> Payload
        {
            get { if(_Payload==null) _Payload = new List<PayloadComponent>(); return _Payload; }
            set { _Payload = value; OnPropertyChanged("Payload"); }
        }
        
        private List<PayloadComponent> _Payload;
        
        /// <summary>
        /// A channel of communication
        /// </summary>
        [FhirElement("medium", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Medium
        {
            get { if(_Medium==null) _Medium = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Medium; }
            set { _Medium = value; OnPropertyChanged("Medium"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Medium;
        
        /// <summary>
        /// in-progress | completed | suspended | rejected | failed
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DSTU2.CommunicationStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.DSTU2.CommunicationStatus> _StatusElement;
        
        /// <summary>
        /// in-progress | completed | suspended | rejected | failed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DSTU2.CommunicationStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.DSTU2.CommunicationStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Encounter leading to message
        /// </summary>
        [FhirElement("encounter", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [References("Encounter")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Encounter
        {
            get { return _Encounter; }
            set { _Encounter = value; OnPropertyChanged("Encounter"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Encounter;
        
        /// <summary>
        /// When sent
        /// </summary>
        [FhirElement("sent", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime SentElement
        {
            get { return _SentElement; }
            set { _SentElement = value; OnPropertyChanged("SentElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _SentElement;
        
        /// <summary>
        /// When sent
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Sent
        {
            get { return SentElement != null ? SentElement.Value : null; }
            set
            {
                if (value == null)
                    SentElement = null;
                else
                    SentElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Sent");
            }
        }
        
        /// <summary>
        /// When received
        /// </summary>
        [FhirElement("received", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime ReceivedElement
        {
            get { return _ReceivedElement; }
            set { _ReceivedElement = value; OnPropertyChanged("ReceivedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _ReceivedElement;
        
        /// <summary>
        /// When received
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Received
        {
            get { return ReceivedElement != null ? ReceivedElement.Value : null; }
            set
            {
                if (value == null)
                    ReceivedElement = null;
                else
                    ReceivedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Received");
            }
        }
        
        /// <summary>
        /// Indication for message
        /// </summary>
        [FhirElement("reason", InSummary=Hl7.Fhir.Model.Version.All, Order=190)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Reason
        {
            get { if(_Reason==null) _Reason = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Reason; }
            set { _Reason = value; OnPropertyChanged("Reason"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Reason;
        
        /// <summary>
        /// Focus of message
        /// </summary>
        [FhirElement("subject", InSummary=Hl7.Fhir.Model.Version.All, Order=200)]
        [CLSCompliant(false)]
        [References("Patient")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// CommunicationRequest producing this message
        /// </summary>
        [FhirElement("requestDetail", InSummary=Hl7.Fhir.Model.Version.All, Order=210)]
        [CLSCompliant(false)]
        [References("CommunicationRequest")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference RequestDetail
        {
            get { return _RequestDetail; }
            set { _RequestDetail = value; OnPropertyChanged("RequestDetail"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _RequestDetail;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Communication;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                if(Sender != null) dest.Sender = (Hl7.Fhir.Model.ResourceReference)Sender.DeepCopy();
                if(Recipient != null) dest.Recipient = new List<Hl7.Fhir.Model.ResourceReference>(Recipient.DeepCopy());
                if(Payload != null) dest.Payload = new List<PayloadComponent>(Payload.DeepCopy());
                if(Medium != null) dest.Medium = new List<Hl7.Fhir.Model.CodeableConcept>(Medium.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.DSTU2.CommunicationStatus>)StatusElement.DeepCopy();
                if(Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
                if(SentElement != null) dest.SentElement = (Hl7.Fhir.Model.FhirDateTime)SentElement.DeepCopy();
                if(ReceivedElement != null) dest.ReceivedElement = (Hl7.Fhir.Model.FhirDateTime)ReceivedElement.DeepCopy();
                if(Reason != null) dest.Reason = new List<Hl7.Fhir.Model.CodeableConcept>(Reason.DeepCopy());
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(RequestDetail != null) dest.RequestDetail = (Hl7.Fhir.Model.ResourceReference)RequestDetail.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new Communication());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Communication;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(Sender, otherT.Sender)) return false;
            if( !DeepComparable.Matches(Recipient, otherT.Recipient)) return false;
            if( !DeepComparable.Matches(Payload, otherT.Payload)) return false;
            if( !DeepComparable.Matches(Medium, otherT.Medium)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.Matches(SentElement, otherT.SentElement)) return false;
            if( !DeepComparable.Matches(ReceivedElement, otherT.ReceivedElement)) return false;
            if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(RequestDetail, otherT.RequestDetail)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Communication;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(Sender, otherT.Sender)) return false;
            if( !DeepComparable.IsExactly(Recipient, otherT.Recipient)) return false;
            if( !DeepComparable.IsExactly(Payload, otherT.Payload)) return false;
            if( !DeepComparable.IsExactly(Medium, otherT.Medium)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.IsExactly(SentElement, otherT.SentElement)) return false;
            if( !DeepComparable.IsExactly(ReceivedElement, otherT.ReceivedElement)) return false;
            if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(RequestDetail, otherT.RequestDetail)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("Communication");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("category", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Category?.Serialize(sink);
            sink.Element("sender", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Sender?.Serialize(sink);
            sink.BeginList("recipient", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Recipient)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("payload", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Payload)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("medium", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Medium)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); StatusElement?.Serialize(sink);
            sink.Element("encounter", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Encounter?.Serialize(sink);
            sink.Element("sent", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); SentElement?.Serialize(sink);
            sink.Element("received", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ReceivedElement?.Serialize(sink);
            sink.BeginList("reason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Reason)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("subject", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Subject?.Serialize(sink);
            sink.Element("requestDetail", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); RequestDetail?.Serialize(sink);
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
                case "identifier":
                    Identifier = source.GetList<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "category":
                    Category = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "sender":
                    Sender = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "recipient":
                    Recipient = source.GetList<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "payload":
                    Payload = source.GetList<PayloadComponent>();
                    return true;
                case "medium":
                    Medium = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.DSTU2.CommunicationStatus>>();
                    return true;
                case "encounter":
                    Encounter = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "sent":
                    SentElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "received":
                    ReceivedElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "reason":
                    Reason = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "subject":
                    Subject = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "requestDetail":
                    RequestDetail = source.Get<Hl7.Fhir.Model.ResourceReference>();
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
                case "identifier":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "category":
                    Category = source.Populate(Category);
                    return true;
                case "sender":
                    Sender = source.Populate(Sender);
                    return true;
                case "recipient":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "payload":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "medium":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "encounter":
                    Encounter = source.Populate(Encounter);
                    return true;
                case "sent":
                    SentElement = source.PopulateValue(SentElement);
                    return true;
                case "_sent":
                    SentElement = source.Populate(SentElement);
                    return true;
                case "received":
                    ReceivedElement = source.PopulateValue(ReceivedElement);
                    return true;
                case "_received":
                    ReceivedElement = source.Populate(ReceivedElement);
                    return true;
                case "reason":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "subject":
                    Subject = source.Populate(Subject);
                    return true;
                case "requestDetail":
                    RequestDetail = source.Populate(RequestDetail);
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
                case "identifier":
                    source.PopulateListItem(Identifier, index);
                    return true;
                case "recipient":
                    source.PopulateListItem(Recipient, index);
                    return true;
                case "payload":
                    source.PopulateListItem(Payload, index);
                    return true;
                case "medium":
                    source.PopulateListItem(Medium, index);
                    return true;
                case "reason":
                    source.PopulateListItem(Reason, index);
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
                foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                if (Category != null) yield return Category;
                if (Sender != null) yield return Sender;
                foreach (var elem in Recipient) { if (elem != null) yield return elem; }
                foreach (var elem in Payload) { if (elem != null) yield return elem; }
                foreach (var elem in Medium) { if (elem != null) yield return elem; }
                if (StatusElement != null) yield return StatusElement;
                if (Encounter != null) yield return Encounter;
                if (SentElement != null) yield return SentElement;
                if (ReceivedElement != null) yield return ReceivedElement;
                foreach (var elem in Reason) { if (elem != null) yield return elem; }
                if (Subject != null) yield return Subject;
                if (RequestDetail != null) yield return RequestDetail;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (Category != null) yield return new ElementValue("category", Category);
                if (Sender != null) yield return new ElementValue("sender", Sender);
                foreach (var elem in Recipient) { if (elem != null) yield return new ElementValue("recipient", elem); }
                foreach (var elem in Payload) { if (elem != null) yield return new ElementValue("payload", elem); }
                foreach (var elem in Medium) { if (elem != null) yield return new ElementValue("medium", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (Encounter != null) yield return new ElementValue("encounter", Encounter);
                if (SentElement != null) yield return new ElementValue("sent", SentElement);
                if (ReceivedElement != null) yield return new ElementValue("received", ReceivedElement);
                foreach (var elem in Reason) { if (elem != null) yield return new ElementValue("reason", elem); }
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (RequestDetail != null) yield return new ElementValue("requestDetail", RequestDetail);
            }
        }
    
    }

}
