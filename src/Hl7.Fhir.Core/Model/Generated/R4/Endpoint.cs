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
// Generated for FHIR v4.0.1
//
namespace Hl7.Fhir.Model.R4
{
    /// <summary>
    /// The technical details of an endpoint that can be used for electronic services
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "Endpoint", IsResource=true)]
    [DataContract]
    public partial class Endpoint : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IEndpoint, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Endpoint; } }
        [NotMapped]
        public override string TypeName { get { return "Endpoint"; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IContactPoint> Hl7.Fhir.Model.IEndpoint.Contact { get { return Contact; } }
    
        
        /// <summary>
        /// Identifies this endpoint across multiple systems
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
        /// active | suspended | error | off | entered-in-error | test
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.EndpointStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.EndpointStatus> _StatusElement;
        
        /// <summary>
        /// active | suspended | error | off | entered-in-error | test
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.EndpointStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.EndpointStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Protocol/Profile/Standard to be used with this endpoint connection
        /// </summary>
        [FhirElement("connectionType", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Coding ConnectionType
        {
            get { return _ConnectionType; }
            set { _ConnectionType = value; OnPropertyChanged("ConnectionType"); }
        }
        
        private Hl7.Fhir.Model.Coding _ConnectionType;
        
        /// <summary>
        /// A name that this endpoint can be identified by
        /// </summary>
        [FhirElement("name", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// A name that this endpoint can be identified by
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
        /// Organization that manages this endpoint (might not be the organization that exposes the endpoint)
        /// </summary>
        [FhirElement("managingOrganization", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference ManagingOrganization
        {
            get { return _ManagingOrganization; }
            set { _ManagingOrganization = value; OnPropertyChanged("ManagingOrganization"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _ManagingOrganization;
        
        /// <summary>
        /// Contact details for source (e.g. troubleshooting)
        /// </summary>
        [FhirElement("contact", Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.R4.ContactPoint> Contact
        {
            get { if(_Contact==null) _Contact = new List<Hl7.Fhir.Model.R4.ContactPoint>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<Hl7.Fhir.Model.R4.ContactPoint> _Contact;
        
        /// <summary>
        /// Interval the endpoint is expected to be operational
        /// </summary>
        [FhirElement("period", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period
        {
            get { return _Period; }
            set { _Period = value; OnPropertyChanged("Period"); }
        }
        
        private Hl7.Fhir.Model.Period _Period;
        
        /// <summary>
        /// The type of content that may be used at this endpoint (e.g. XDS Discharge summaries)
        /// </summary>
        [FhirElement("payloadType", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> PayloadType
        {
            get { if(_PayloadType==null) _PayloadType = new List<Hl7.Fhir.Model.CodeableConcept>(); return _PayloadType; }
            set { _PayloadType = value; OnPropertyChanged("PayloadType"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _PayloadType;
        
        /// <summary>
        /// Mimetype to send. If not specified, the content could be anything (including no payload, if the connectionType defined this)
        /// </summary>
        [FhirElement("payloadMimeType", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Code> PayloadMimeTypeElement
        {
            get { if(_PayloadMimeTypeElement==null) _PayloadMimeTypeElement = new List<Hl7.Fhir.Model.Code>(); return _PayloadMimeTypeElement; }
            set { _PayloadMimeTypeElement = value; OnPropertyChanged("PayloadMimeTypeElement"); }
        }
        
        private List<Hl7.Fhir.Model.Code> _PayloadMimeTypeElement;
        
        /// <summary>
        /// Mimetype to send. If not specified, the content could be anything (including no payload, if the connectionType defined this)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> PayloadMimeType
        {
            get { return PayloadMimeTypeElement != null ? PayloadMimeTypeElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    PayloadMimeTypeElement = null;
                else
                    PayloadMimeTypeElement = new List<Hl7.Fhir.Model.Code>(value.Select(elem=>new Hl7.Fhir.Model.Code(elem)));
                OnPropertyChanged("PayloadMimeType");
            }
        }
        
        /// <summary>
        /// The technical base address for connecting to this endpoint
        /// </summary>
        [FhirElement("address", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Url AddressElement
        {
            get { return _AddressElement; }
            set { _AddressElement = value; OnPropertyChanged("AddressElement"); }
        }
        
        private Hl7.Fhir.Model.Url _AddressElement;
        
        /// <summary>
        /// The technical base address for connecting to this endpoint
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Address
        {
            get { return AddressElement != null ? AddressElement.Value : null; }
            set
            {
                if (value == null)
                    AddressElement = null;
                else
                    AddressElement = new Hl7.Fhir.Model.Url(value);
                OnPropertyChanged("Address");
            }
        }
        
        /// <summary>
        /// Usage depends on the channel type
        /// </summary>
        [FhirElement("header", Order=190)]
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
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Endpoint;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.EndpointStatus>)StatusElement.DeepCopy();
                if(ConnectionType != null) dest.ConnectionType = (Hl7.Fhir.Model.Coding)ConnectionType.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(ManagingOrganization != null) dest.ManagingOrganization = (Hl7.Fhir.Model.ResourceReference)ManagingOrganization.DeepCopy();
                if(Contact != null) dest.Contact = new List<Hl7.Fhir.Model.R4.ContactPoint>(Contact.DeepCopy());
                if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                if(PayloadType != null) dest.PayloadType = new List<Hl7.Fhir.Model.CodeableConcept>(PayloadType.DeepCopy());
                if(PayloadMimeTypeElement != null) dest.PayloadMimeTypeElement = new List<Hl7.Fhir.Model.Code>(PayloadMimeTypeElement.DeepCopy());
                if(AddressElement != null) dest.AddressElement = (Hl7.Fhir.Model.Url)AddressElement.DeepCopy();
                if(HeaderElement != null) dest.HeaderElement = new List<Hl7.Fhir.Model.FhirString>(HeaderElement.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new Endpoint());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Endpoint;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(ConnectionType, otherT.ConnectionType)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(ManagingOrganization, otherT.ManagingOrganization)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(Period, otherT.Period)) return false;
            if( !DeepComparable.Matches(PayloadType, otherT.PayloadType)) return false;
            if( !DeepComparable.Matches(PayloadMimeTypeElement, otherT.PayloadMimeTypeElement)) return false;
            if( !DeepComparable.Matches(AddressElement, otherT.AddressElement)) return false;
            if( !DeepComparable.Matches(HeaderElement, otherT.HeaderElement)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Endpoint;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(ConnectionType, otherT.ConnectionType)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(ManagingOrganization, otherT.ManagingOrganization)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
            if( !DeepComparable.IsExactly(PayloadType, otherT.PayloadType)) return false;
            if( !DeepComparable.IsExactly(PayloadMimeTypeElement, otherT.PayloadMimeTypeElement)) return false;
            if( !DeepComparable.IsExactly(AddressElement, otherT.AddressElement)) return false;
            if( !DeepComparable.IsExactly(HeaderElement, otherT.HeaderElement)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("Endpoint");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("connectionType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); ConnectionType?.Serialize(sink);
            sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NameElement?.Serialize(sink);
            sink.Element("managingOrganization", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ManagingOrganization?.Serialize(sink);
            sink.BeginList("contact", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Contact)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("period", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Period?.Serialize(sink);
            sink.BeginList("payloadType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true);
            foreach(var item in PayloadType)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("payloadMimeType", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            sink.Serialize(PayloadMimeTypeElement);
            sink.End();
            sink.Element("address", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); AddressElement?.Serialize(sink);
            sink.BeginList("header", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            sink.Serialize(HeaderElement);
            sink.End();
            sink.End();
        }
    
        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                if (StatusElement != null) yield return StatusElement;
                if (ConnectionType != null) yield return ConnectionType;
                if (NameElement != null) yield return NameElement;
                if (ManagingOrganization != null) yield return ManagingOrganization;
                foreach (var elem in Contact) { if (elem != null) yield return elem; }
                if (Period != null) yield return Period;
                foreach (var elem in PayloadType) { if (elem != null) yield return elem; }
                foreach (var elem in PayloadMimeTypeElement) { if (elem != null) yield return elem; }
                if (AddressElement != null) yield return AddressElement;
                foreach (var elem in HeaderElement) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (ConnectionType != null) yield return new ElementValue("connectionType", ConnectionType);
                if (NameElement != null) yield return new ElementValue("name", NameElement);
                if (ManagingOrganization != null) yield return new ElementValue("managingOrganization", ManagingOrganization);
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                if (Period != null) yield return new ElementValue("period", Period);
                foreach (var elem in PayloadType) { if (elem != null) yield return new ElementValue("payloadType", elem); }
                foreach (var elem in PayloadMimeTypeElement) { if (elem != null) yield return new ElementValue("payloadMimeType", elem); }
                if (AddressElement != null) yield return new ElementValue("address", AddressElement);
                foreach (var elem in HeaderElement) { if (elem != null) yield return new ElementValue("header", elem); }
            }
        }
    
    }

}
