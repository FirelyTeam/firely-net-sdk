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
// Generated on Tue, Jul 1, 2014 16:22+0200 for FHIR v0.0.81
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A resource that describes a message that is exchanged between systems
    /// </summary>
    [FhirType("MessageHeader", IsResource=true)]
    [DataContract]
    public partial class MessageHeader : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// The kind of response to a message
        /// </summary>
        [FhirEnumeration("ResponseType")]
        public enum ResponseType
        {
            /// <summary>
            /// The message was accepted and processed without error.
            /// </summary>
            [EnumLiteral("ok")]
            Ok,
            /// <summary>
            /// Some internal unexpected error occurred - wait and try again. Note - this is usually used for things like database unavailable, which may be expected to resolve, though human intervention may be required.
            /// </summary>
            [EnumLiteral("transient-error")]
            TransientError,
            /// <summary>
            /// The message was rejected because of some content in it. There is no point in re-sending without change. The response narrative SHALL describe what the issue is.
            /// </summary>
            [EnumLiteral("fatal-error")]
            FatalError,
        }
        
        [FhirType("MessageDestinationComponent")]
        [DataContract]
        public partial class MessageDestinationComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Name of system
            /// </summary>
            [FhirElement("name", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Name of system
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if(value == null)
                      NameElement = null; 
                    else
                      NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Particular delivery destination within the destination
            /// </summary>
            [FhirElement("target", InSummary=true, Order=50)]
            [References("Device")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Target
            {
                get { return _Target; }
                set { _Target = value; OnPropertyChanged("Target"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Target;
            
            /// <summary>
            /// Actual destination address or id
            /// </summary>
            [FhirElement("endpoint", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri EndpointElement
            {
                get { return _EndpointElement; }
                set { _EndpointElement = value; OnPropertyChanged("EndpointElement"); }
            }
            private Hl7.Fhir.Model.FhirUri _EndpointElement;
            
            /// <summary>
            /// Actual destination address or id
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Endpoint
            {
                get { return EndpointElement != null ? EndpointElement.Value : null; }
                set
                {
                    if(value == null)
                      EndpointElement = null; 
                    else
                      EndpointElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Endpoint");
                }
            }
            
        }
        
        
        [FhirType("MessageSourceComponent")]
        [DataContract]
        public partial class MessageSourceComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Name of system
            /// </summary>
            [FhirElement("name", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Name of system
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if(value == null)
                      NameElement = null; 
                    else
                      NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Name of software running the system
            /// </summary>
            [FhirElement("software", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString SoftwareElement
            {
                get { return _SoftwareElement; }
                set { _SoftwareElement = value; OnPropertyChanged("SoftwareElement"); }
            }
            private Hl7.Fhir.Model.FhirString _SoftwareElement;
            
            /// <summary>
            /// Name of software running the system
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Software
            {
                get { return SoftwareElement != null ? SoftwareElement.Value : null; }
                set
                {
                    if(value == null)
                      SoftwareElement = null; 
                    else
                      SoftwareElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Software");
                }
            }
            
            /// <summary>
            /// Version of software running
            /// </summary>
            [FhirElement("version", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString VersionElement
            {
                get { return _VersionElement; }
                set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
            }
            private Hl7.Fhir.Model.FhirString _VersionElement;
            
            /// <summary>
            /// Version of software running
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
            /// Human contact for problems
            /// </summary>
            [FhirElement("contact", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Contact Contact
            {
                get { return _Contact; }
                set { _Contact = value; OnPropertyChanged("Contact"); }
            }
            private Hl7.Fhir.Model.Contact _Contact;
            
            /// <summary>
            /// Actual message source address or id
            /// </summary>
            [FhirElement("endpoint", InSummary=true, Order=80)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri EndpointElement
            {
                get { return _EndpointElement; }
                set { _EndpointElement = value; OnPropertyChanged("EndpointElement"); }
            }
            private Hl7.Fhir.Model.FhirUri _EndpointElement;
            
            /// <summary>
            /// Actual message source address or id
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Endpoint
            {
                get { return EndpointElement != null ? EndpointElement.Value : null; }
                set
                {
                    if(value == null)
                      EndpointElement = null; 
                    else
                      EndpointElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Endpoint");
                }
            }
            
        }
        
        
        [FhirType("MessageHeaderResponseComponent")]
        [DataContract]
        public partial class MessageHeaderResponseComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Id of original message
            /// </summary>
            [FhirElement("identifier", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id IdentifierElement
            {
                get { return _IdentifierElement; }
                set { _IdentifierElement = value; OnPropertyChanged("IdentifierElement"); }
            }
            private Hl7.Fhir.Model.Id _IdentifierElement;
            
            /// <summary>
            /// Id of original message
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Identifier
            {
                get { return IdentifierElement != null ? IdentifierElement.Value : null; }
                set
                {
                    if(value == null)
                      IdentifierElement = null; 
                    else
                      IdentifierElement = new Hl7.Fhir.Model.Id(value);
                    OnPropertyChanged("Identifier");
                }
            }
            
            /// <summary>
            /// ok | transient-error | fatal-error
            /// </summary>
            [FhirElement("code", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.MessageHeader.ResponseType> CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            private Code<Hl7.Fhir.Model.MessageHeader.ResponseType> _CodeElement;
            
            /// <summary>
            /// ok | transient-error | fatal-error
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.MessageHeader.ResponseType? Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if(value == null)
                      CodeElement = null; 
                    else
                      CodeElement = new Code<Hl7.Fhir.Model.MessageHeader.ResponseType>(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Specific list of hints/warnings/errors
            /// </summary>
            [FhirElement("details", InSummary=true, Order=60)]
            [References("OperationOutcome")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Details
            {
                get { return _Details; }
                set { _Details = value; OnPropertyChanged("Details"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Details;
            
        }
        
        
        /// <summary>
        /// Id of this message
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Id IdentifierElement
        {
            get { return _IdentifierElement; }
            set { _IdentifierElement = value; OnPropertyChanged("IdentifierElement"); }
        }
        private Hl7.Fhir.Model.Id _IdentifierElement;
        
        /// <summary>
        /// Id of this message
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Identifier
        {
            get { return IdentifierElement != null ? IdentifierElement.Value : null; }
            set
            {
                if(value == null)
                  IdentifierElement = null; 
                else
                  IdentifierElement = new Hl7.Fhir.Model.Id(value);
                OnPropertyChanged("Identifier");
            }
        }
        
        /// <summary>
        /// Time that the message was sent
        /// </summary>
        [FhirElement("timestamp", Order=80)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Instant TimestampElement
        {
            get { return _TimestampElement; }
            set { _TimestampElement = value; OnPropertyChanged("TimestampElement"); }
        }
        private Hl7.Fhir.Model.Instant _TimestampElement;
        
        /// <summary>
        /// Time that the message was sent
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public DateTimeOffset? Timestamp
        {
            get { return TimestampElement != null ? TimestampElement.Value : null; }
            set
            {
                if(value == null)
                  TimestampElement = null; 
                else
                  TimestampElement = new Hl7.Fhir.Model.Instant(value);
                OnPropertyChanged("Timestamp");
            }
        }
        
        /// <summary>
        /// Code for the event this message represents
        /// </summary>
        [FhirElement("event", Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Coding Event
        {
            get { return _Event; }
            set { _Event = value; OnPropertyChanged("Event"); }
        }
        private Hl7.Fhir.Model.Coding _Event;
        
        /// <summary>
        /// If this is a reply to prior message
        /// </summary>
        [FhirElement("response", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.MessageHeader.MessageHeaderResponseComponent Response
        {
            get { return _Response; }
            set { _Response = value; OnPropertyChanged("Response"); }
        }
        private Hl7.Fhir.Model.MessageHeader.MessageHeaderResponseComponent _Response;
        
        /// <summary>
        /// Message Source Application
        /// </summary>
        [FhirElement("source", Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.MessageHeader.MessageSourceComponent Source
        {
            get { return _Source; }
            set { _Source = value; OnPropertyChanged("Source"); }
        }
        private Hl7.Fhir.Model.MessageHeader.MessageSourceComponent _Source;
        
        /// <summary>
        /// Message Destination Application(s)
        /// </summary>
        [FhirElement("destination", Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.MessageHeader.MessageDestinationComponent> Destination
        {
            get { return _Destination; }
            set { _Destination = value; OnPropertyChanged("Destination"); }
        }
        private List<Hl7.Fhir.Model.MessageHeader.MessageDestinationComponent> _Destination;
        
        /// <summary>
        /// The source of the data entry
        /// </summary>
        [FhirElement("enterer", Order=130)]
        [References("Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Enterer
        {
            get { return _Enterer; }
            set { _Enterer = value; OnPropertyChanged("Enterer"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Enterer;
        
        /// <summary>
        /// The source of the decision
        /// </summary>
        [FhirElement("author", Order=140)]
        [References("Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Author
        {
            get { return _Author; }
            set { _Author = value; OnPropertyChanged("Author"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Author;
        
        /// <summary>
        /// Intended "real-world" recipient for the data
        /// </summary>
        [FhirElement("receiver", Order=150)]
        [References("Practitioner","Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Receiver
        {
            get { return _Receiver; }
            set { _Receiver = value; OnPropertyChanged("Receiver"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Receiver;
        
        /// <summary>
        /// Final responsibility for event
        /// </summary>
        [FhirElement("responsible", Order=160)]
        [References("Practitioner","Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Responsible
        {
            get { return _Responsible; }
            set { _Responsible = value; OnPropertyChanged("Responsible"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Responsible;
        
        /// <summary>
        /// Cause of event
        /// </summary>
        [FhirElement("reason", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Reason
        {
            get { return _Reason; }
            set { _Reason = value; OnPropertyChanged("Reason"); }
        }
        private Hl7.Fhir.Model.CodeableConcept _Reason;
        
        /// <summary>
        /// The actual content of the message
        /// </summary>
        [FhirElement("data", Order=180)]
        [References()]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Data
        {
            get { return _Data; }
            set { _Data = value; OnPropertyChanged("Data"); }
        }
        private List<Hl7.Fhir.Model.ResourceReference> _Data;
        
    }
    
}
