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
// Generated on Tue, Apr 15, 2014 17:48+0200 for FHIR v0.80
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A conformance statement
    /// </summary>
    [FhirType("Conformance", IsResource=true)]
    [DataContract]
    public partial class Conformance : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Whether the application produces or consumes documents
        /// </summary>
        [FhirEnumeration("DocumentMode")]
        public enum DocumentMode
        {
            [EnumLiteral("producer")]
            Producer, // The application produces documents of the specified type.
            [EnumLiteral("consumer")]
            Consumer, // The application consumes documents of the specified type.
        }
        
        /// <summary>
        /// The mode of a RESTful conformance statement
        /// </summary>
        [FhirEnumeration("RestfulConformanceMode")]
        public enum RestfulConformanceMode
        {
            [EnumLiteral("client")]
            Client, // The application acts as a server for this resource.
            [EnumLiteral("server")]
            Server, // The application acts as a client for this resource.
        }
        
        /// <summary>
        /// The protocol used for message transport
        /// </summary>
        [FhirEnumeration("MessageTransport")]
        public enum MessageTransport
        {
            [EnumLiteral("http")]
            Http, // The application sends or receives messages using HTTP POST (may be over http or https).
            [EnumLiteral("ftp")]
            Ftp, // The application sends or receives messages using File Transfer Protocol.
            [EnumLiteral("mllp")]
            Mllp, // The application sends or receivers messages using HL7's Minimal Lower Level Protocol.
        }
        
        /// <summary>
        /// The mode of a message conformance statement
        /// </summary>
        [FhirEnumeration("ConformanceEventMode")]
        public enum ConformanceEventMode
        {
            [EnumLiteral("sender")]
            Sender, // The application sends requests and receives responses.
            [EnumLiteral("receiver")]
            Receiver, // The application receives requests and sends responses.
        }
        
        /// <summary>
        /// The impact of the content of a message
        /// </summary>
        [FhirEnumeration("MessageSignificanceCategory")]
        public enum MessageSignificanceCategory
        {
            [EnumLiteral("Consequence")]
            Consequence, // The message represents/requests a change that should not be processed more than once. E.g. Making a booking for an appointment.
            [EnumLiteral("Currency")]
            Currency, // The message represents a response to query for current information. Retrospective processing is wrong and/or wasteful.
            [EnumLiteral("Notification")]
            Notification, // The content is not necessarily intended to be current, and it can be reprocessed, though there may be version issues created by processing old notifications.
        }
        
        /// <summary>
        /// Operations supported by REST at the type or instance level
        /// </summary>
        [FhirEnumeration("RestfulOperationType")]
        public enum RestfulOperationType
        {
            [EnumLiteral("read")]
            Read,
            [EnumLiteral("vread")]
            Vread,
            [EnumLiteral("update")]
            Update,
            [EnumLiteral("delete")]
            Delete,
            [EnumLiteral("history-instance")]
            HistoryInstance,
            [EnumLiteral("validate")]
            Validate,
            [EnumLiteral("history-type")]
            HistoryType,
            [EnumLiteral("create")]
            Create,
            [EnumLiteral("search-type")]
            SearchType,
        }
        
        /// <summary>
        /// The status of this conformance statement
        /// </summary>
        [FhirEnumeration("ConformanceStatementStatus")]
        public enum ConformanceStatementStatus
        {
            [EnumLiteral("draft")]
            Draft, // This conformance statement is still under development.
            [EnumLiteral("active")]
            Active, // This conformance statement is ready for use in production systems.
            [EnumLiteral("retired")]
            Retired, // This conformance statement has been withdrawn or superceded and should no longer be used.
        }
        
        /// <summary>
        /// Operations supported by REST at the system level
        /// </summary>
        [FhirEnumeration("RestfulOperationSystem")]
        public enum RestfulOperationSystem
        {
            [EnumLiteral("transaction")]
            Transaction,
            [EnumLiteral("search-system")]
            SearchSystem,
            [EnumLiteral("history-system")]
            HistorySystem,
        }
        
        /// <summary>
        /// Data types allowed to be used for search parameters
        /// </summary>
        [FhirEnumeration("SearchParamType")]
        public enum SearchParamType
        {
            [EnumLiteral("number")]
            Number, // Search parameter SHALL be a number (a whole number, or a decimal).
            [EnumLiteral("date")]
            Date, // Search parameter is on a date/time. The date format is the standard XML format, though other formats may be supported.
            [EnumLiteral("string")]
            String, // Search parameter is a simple string, like a name part. Search is case-insensitive and accent-insensitive. May match just the start of a string. String parameters may contain spaces.
            [EnumLiteral("token")]
            Token, // Search parameter on a coded element or identifier. May be used to search through the text, displayname, code and code/codesystem (for codes) and label, system and key (for identifier). Its value is either a string or a pair of namespace and value, separated by a "|", depending on the modifier used.
            [EnumLiteral("reference")]
            Reference, // A reference to another resource.
            [EnumLiteral("composite")]
            Composite, // A composite search parameter that combines a search on two values together.
            [EnumLiteral("quantity")]
            Quantity, // A search parameter that searches on a quantity.
        }
        
        /// <summary>
        /// Types of security services used with FHIR
        /// </summary>
        [FhirEnumeration("RestfulSecurityService")]
        public enum RestfulSecurityService
        {
            [EnumLiteral("OAuth")]
            OAuth, // OAuth (see oauth.net).
            [EnumLiteral("OAuth2")]
            OAuth2, // OAuth version 2 (see oauth.net).
            [EnumLiteral("NTLM")]
            NTLM, // Microsoft NTLM Authentication.
            [EnumLiteral("Basic")]
            Basic, // Basic authentication defined in HTTP specification.
            [EnumLiteral("Kerberos")]
            Kerberos, // see http://www.ietf.org/rfc/rfc4120.txt.
        }
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ConformanceRestQueryComponent")]
        [DataContract]
        public partial class ConformanceRestQueryComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Special named queries (_query=)
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
            /// Where query is defined
            /// </summary>
            [FhirElement("definition", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri DefinitionElement
            {
                get { return _DefinitionElement; }
                set { _DefinitionElement = value; OnPropertyChanged("DefinitionElement"); }
            }
            private Hl7.Fhir.Model.FhirUri _DefinitionElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public System.Uri Definition
            {
                get { return DefinitionElement != null ? DefinitionElement.Value : null; }
                set
                {
                    if(value == null)
                      DefinitionElement = null; 
                    else
                      DefinitionElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Definition");
                }
            }
            
            /// <summary>
            /// Additional usage guidance
            /// </summary>
            [FhirElement("documentation", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DocumentationElement
            {
                get { return _DocumentationElement; }
                set { _DocumentationElement = value; OnPropertyChanged("DocumentationElement"); }
            }
            private Hl7.Fhir.Model.FhirString _DocumentationElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Documentation
            {
                get { return DocumentationElement != null ? DocumentationElement.Value : null; }
                set
                {
                    if(value == null)
                      DocumentationElement = null; 
                    else
                      DocumentationElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Documentation");
                }
            }
            
            /// <summary>
            /// Parameter for the named query
            /// </summary>
            [FhirElement("parameter", InSummary=true, Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Conformance.ConformanceRestResourceSearchParamComponent> Parameter
            {
                get { return _Parameter; }
                set { _Parameter = value; OnPropertyChanged("Parameter"); }
            }
            private List<Hl7.Fhir.Model.Conformance.ConformanceRestResourceSearchParamComponent> _Parameter;
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ConformanceRestComponent")]
        [DataContract]
        public partial class ConformanceRestComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// client | server
            /// </summary>
            [FhirElement("mode", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Conformance.RestfulConformanceMode> ModeElement
            {
                get { return _ModeElement; }
                set { _ModeElement = value; OnPropertyChanged("ModeElement"); }
            }
            private Code<Hl7.Fhir.Model.Conformance.RestfulConformanceMode> _ModeElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Conformance.RestfulConformanceMode? Mode
            {
                get { return ModeElement != null ? ModeElement.Value : null; }
                set
                {
                    if(value == null)
                      ModeElement = null; 
                    else
                      ModeElement = new Code<Hl7.Fhir.Model.Conformance.RestfulConformanceMode>(value);
                    OnPropertyChanged("Mode");
                }
            }
            
            /// <summary>
            /// General description of implementation
            /// </summary>
            [FhirElement("documentation", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DocumentationElement
            {
                get { return _DocumentationElement; }
                set { _DocumentationElement = value; OnPropertyChanged("DocumentationElement"); }
            }
            private Hl7.Fhir.Model.FhirString _DocumentationElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Documentation
            {
                get { return DocumentationElement != null ? DocumentationElement.Value : null; }
                set
                {
                    if(value == null)
                      DocumentationElement = null; 
                    else
                      DocumentationElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Documentation");
                }
            }
            
            /// <summary>
            /// Information about security of implementation
            /// </summary>
            [FhirElement("security", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Conformance.ConformanceRestSecurityComponent Security
            {
                get { return _Security; }
                set { _Security = value; OnPropertyChanged("Security"); }
            }
            private Hl7.Fhir.Model.Conformance.ConformanceRestSecurityComponent _Security;
            
            /// <summary>
            /// Resource served on the REST interface
            /// </summary>
            [FhirElement("resource", InSummary=true, Order=70)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Conformance.ConformanceRestResourceComponent> Resource
            {
                get { return _Resource; }
                set { _Resource = value; OnPropertyChanged("Resource"); }
            }
            private List<Hl7.Fhir.Model.Conformance.ConformanceRestResourceComponent> _Resource;
            
            /// <summary>
            /// What operations are supported?
            /// </summary>
            [FhirElement("operation", InSummary=true, Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Conformance.ConformanceRestOperationComponent> Operation
            {
                get { return _Operation; }
                set { _Operation = value; OnPropertyChanged("Operation"); }
            }
            private List<Hl7.Fhir.Model.Conformance.ConformanceRestOperationComponent> _Operation;
            
            /// <summary>
            /// Definition of a named query
            /// </summary>
            [FhirElement("query", InSummary=true, Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Conformance.ConformanceRestQueryComponent> Query
            {
                get { return _Query; }
                set { _Query = value; OnPropertyChanged("Query"); }
            }
            private List<Hl7.Fhir.Model.Conformance.ConformanceRestQueryComponent> _Query;
            
            /// <summary>
            /// How documents are accepted in /Mailbox
            /// </summary>
            [FhirElement("documentMailbox", InSummary=true, Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirUri> DocumentMailboxElement
            {
                get { return _DocumentMailboxElement; }
                set { _DocumentMailboxElement = value; OnPropertyChanged("DocumentMailboxElement"); }
            }
            private List<Hl7.Fhir.Model.FhirUri> _DocumentMailboxElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<System.Uri> DocumentMailbox
            {
                get { return DocumentMailboxElement != null ? DocumentMailboxElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      DocumentMailboxElement = null; 
                    else
                      DocumentMailboxElement = new List<Hl7.Fhir.Model.FhirUri>(value.Select(elem=>new Hl7.Fhir.Model.FhirUri(elem)));
                    OnPropertyChanged("DocumentMailbox");
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ConformanceSoftwareComponent")]
        [DataContract]
        public partial class ConformanceSoftwareComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// A name the software is known by
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
            /// Version covered by this statement
            /// </summary>
            [FhirElement("version", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString VersionElement
            {
                get { return _VersionElement; }
                set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
            }
            private Hl7.Fhir.Model.FhirString _VersionElement;
            
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
            /// Date this version released
            /// </summary>
            [FhirElement("releaseDate", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime ReleaseDateElement
            {
                get { return _ReleaseDateElement; }
                set { _ReleaseDateElement = value; OnPropertyChanged("ReleaseDateElement"); }
            }
            private Hl7.Fhir.Model.FhirDateTime _ReleaseDateElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ReleaseDate
            {
                get { return ReleaseDateElement != null ? ReleaseDateElement.Value : null; }
                set
                {
                    if(value == null)
                      ReleaseDateElement = null; 
                    else
                      ReleaseDateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("ReleaseDate");
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ConformanceMessagingComponent")]
        [DataContract]
        public partial class ConformanceMessagingComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Actual endpoint being described
            /// </summary>
            [FhirElement("endpoint", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri EndpointElement
            {
                get { return _EndpointElement; }
                set { _EndpointElement = value; OnPropertyChanged("EndpointElement"); }
            }
            private Hl7.Fhir.Model.FhirUri _EndpointElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public System.Uri Endpoint
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
            
            /// <summary>
            /// Reliable Message Cache Length
            /// </summary>
            [FhirElement("reliableCache", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Integer ReliableCacheElement
            {
                get { return _ReliableCacheElement; }
                set { _ReliableCacheElement = value; OnPropertyChanged("ReliableCacheElement"); }
            }
            private Hl7.Fhir.Model.Integer _ReliableCacheElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? ReliableCache
            {
                get { return ReliableCacheElement != null ? ReliableCacheElement.Value : null; }
                set
                {
                    if(value == null)
                      ReliableCacheElement = null; 
                    else
                      ReliableCacheElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("ReliableCache");
                }
            }
            
            /// <summary>
            /// Messaging interface behavior details
            /// </summary>
            [FhirElement("documentation", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DocumentationElement
            {
                get { return _DocumentationElement; }
                set { _DocumentationElement = value; OnPropertyChanged("DocumentationElement"); }
            }
            private Hl7.Fhir.Model.FhirString _DocumentationElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Documentation
            {
                get { return DocumentationElement != null ? DocumentationElement.Value : null; }
                set
                {
                    if(value == null)
                      DocumentationElement = null; 
                    else
                      DocumentationElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Documentation");
                }
            }
            
            /// <summary>
            /// Declare support for this event
            /// </summary>
            [FhirElement("event", InSummary=true, Order=70)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Conformance.ConformanceMessagingEventComponent> Event
            {
                get { return _Event; }
                set { _Event = value; OnPropertyChanged("Event"); }
            }
            private List<Hl7.Fhir.Model.Conformance.ConformanceMessagingEventComponent> _Event;
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ConformanceDocumentComponent")]
        [DataContract]
        public partial class ConformanceDocumentComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// producer | consumer
            /// </summary>
            [FhirElement("mode", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Conformance.DocumentMode> ModeElement
            {
                get { return _ModeElement; }
                set { _ModeElement = value; OnPropertyChanged("ModeElement"); }
            }
            private Code<Hl7.Fhir.Model.Conformance.DocumentMode> _ModeElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Conformance.DocumentMode? Mode
            {
                get { return ModeElement != null ? ModeElement.Value : null; }
                set
                {
                    if(value == null)
                      ModeElement = null; 
                    else
                      ModeElement = new Code<Hl7.Fhir.Model.Conformance.DocumentMode>(value);
                    OnPropertyChanged("Mode");
                }
            }
            
            /// <summary>
            /// Description of document support
            /// </summary>
            [FhirElement("documentation", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DocumentationElement
            {
                get { return _DocumentationElement; }
                set { _DocumentationElement = value; OnPropertyChanged("DocumentationElement"); }
            }
            private Hl7.Fhir.Model.FhirString _DocumentationElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Documentation
            {
                get { return DocumentationElement != null ? DocumentationElement.Value : null; }
                set
                {
                    if(value == null)
                      DocumentationElement = null; 
                    else
                      DocumentationElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Documentation");
                }
            }
            
            /// <summary>
            /// Constraint on a resource used in the document
            /// </summary>
            [FhirElement("profile", InSummary=true, Order=60)]
            [References("Profile")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Profile
            {
                get { return _Profile; }
                set { _Profile = value; OnPropertyChanged("Profile"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Profile;
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ConformanceRestResourceComponent")]
        [DataContract]
        public partial class ConformanceRestResourceComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// A resource type that is supported
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            private Hl7.Fhir.Model.Code _TypeElement;
            
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
            /// What structural features are supported
            /// </summary>
            [FhirElement("profile", InSummary=true, Order=50)]
            [References("Profile")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Profile
            {
                get { return _Profile; }
                set { _Profile = value; OnPropertyChanged("Profile"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Profile;
            
            /// <summary>
            /// What operations are supported?
            /// </summary>
            [FhirElement("operation", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Conformance.ConformanceRestResourceOperationComponent> Operation
            {
                get { return _Operation; }
                set { _Operation = value; OnPropertyChanged("Operation"); }
            }
            private List<Hl7.Fhir.Model.Conformance.ConformanceRestResourceOperationComponent> _Operation;
            
            /// <summary>
            /// Whether vRead can return past versions
            /// </summary>
            [FhirElement("readHistory", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean ReadHistoryElement
            {
                get { return _ReadHistoryElement; }
                set { _ReadHistoryElement = value; OnPropertyChanged("ReadHistoryElement"); }
            }
            private Hl7.Fhir.Model.FhirBoolean _ReadHistoryElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? ReadHistory
            {
                get { return ReadHistoryElement != null ? ReadHistoryElement.Value : null; }
                set
                {
                    if(value == null)
                      ReadHistoryElement = null; 
                    else
                      ReadHistoryElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("ReadHistory");
                }
            }
            
            /// <summary>
            /// If allows/uses update to a new location
            /// </summary>
            [FhirElement("updateCreate", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean UpdateCreateElement
            {
                get { return _UpdateCreateElement; }
                set { _UpdateCreateElement = value; OnPropertyChanged("UpdateCreateElement"); }
            }
            private Hl7.Fhir.Model.FhirBoolean _UpdateCreateElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? UpdateCreate
            {
                get { return UpdateCreateElement != null ? UpdateCreateElement.Value : null; }
                set
                {
                    if(value == null)
                      UpdateCreateElement = null; 
                    else
                      UpdateCreateElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("UpdateCreate");
                }
            }
            
            /// <summary>
            /// _include values supported by the server
            /// </summary>
            [FhirElement("searchInclude", InSummary=true, Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> SearchIncludeElement
            {
                get { return _SearchIncludeElement; }
                set { _SearchIncludeElement = value; OnPropertyChanged("SearchIncludeElement"); }
            }
            private List<Hl7.Fhir.Model.FhirString> _SearchIncludeElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> SearchInclude
            {
                get { return SearchIncludeElement != null ? SearchIncludeElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      SearchIncludeElement = null; 
                    else
                      SearchIncludeElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("SearchInclude");
                }
            }
            
            /// <summary>
            /// Additional search params defined
            /// </summary>
            [FhirElement("searchParam", InSummary=true, Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Conformance.ConformanceRestResourceSearchParamComponent> SearchParam
            {
                get { return _SearchParam; }
                set { _SearchParam = value; OnPropertyChanged("SearchParam"); }
            }
            private List<Hl7.Fhir.Model.Conformance.ConformanceRestResourceSearchParamComponent> _SearchParam;
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ConformanceImplementationComponent")]
        [DataContract]
        public partial class ConformanceImplementationComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Describes this specific instance
            /// </summary>
            [FhirElement("description", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
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
            /// Base URL for the installation
            /// </summary>
            [FhirElement("url", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri UrlElement
            {
                get { return _UrlElement; }
                set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
            }
            private Hl7.Fhir.Model.FhirUri _UrlElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public System.Uri Url
            {
                get { return UrlElement != null ? UrlElement.Value : null; }
                set
                {
                    if(value == null)
                      UrlElement = null; 
                    else
                      UrlElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Url");
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ConformanceRestResourceOperationComponent")]
        [DataContract]
        public partial class ConformanceRestResourceOperationComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// read | vread | update | delete | history-instance | validate | history-type | create | search-type
            /// </summary>
            [FhirElement("code", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Conformance.RestfulOperationType> CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            private Code<Hl7.Fhir.Model.Conformance.RestfulOperationType> _CodeElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Conformance.RestfulOperationType? Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if(value == null)
                      CodeElement = null; 
                    else
                      CodeElement = new Code<Hl7.Fhir.Model.Conformance.RestfulOperationType>(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Anything special about operation behavior
            /// </summary>
            [FhirElement("documentation", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DocumentationElement
            {
                get { return _DocumentationElement; }
                set { _DocumentationElement = value; OnPropertyChanged("DocumentationElement"); }
            }
            private Hl7.Fhir.Model.FhirString _DocumentationElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Documentation
            {
                get { return DocumentationElement != null ? DocumentationElement.Value : null; }
                set
                {
                    if(value == null)
                      DocumentationElement = null; 
                    else
                      DocumentationElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Documentation");
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ConformanceMessagingEventComponent")]
        [DataContract]
        public partial class ConformanceMessagingEventComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Event type
            /// </summary>
            [FhirElement("code", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            private Hl7.Fhir.Model.Coding _Code;
            
            /// <summary>
            /// Consequence | Currency | Notification
            /// </summary>
            [FhirElement("category", InSummary=true, Order=50)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Conformance.MessageSignificanceCategory> CategoryElement
            {
                get { return _CategoryElement; }
                set { _CategoryElement = value; OnPropertyChanged("CategoryElement"); }
            }
            private Code<Hl7.Fhir.Model.Conformance.MessageSignificanceCategory> _CategoryElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Conformance.MessageSignificanceCategory? Category
            {
                get { return CategoryElement != null ? CategoryElement.Value : null; }
                set
                {
                    if(value == null)
                      CategoryElement = null; 
                    else
                      CategoryElement = new Code<Hl7.Fhir.Model.Conformance.MessageSignificanceCategory>(value);
                    OnPropertyChanged("Category");
                }
            }
            
            /// <summary>
            /// sender | receiver
            /// </summary>
            [FhirElement("mode", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Conformance.ConformanceEventMode> ModeElement
            {
                get { return _ModeElement; }
                set { _ModeElement = value; OnPropertyChanged("ModeElement"); }
            }
            private Code<Hl7.Fhir.Model.Conformance.ConformanceEventMode> _ModeElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Conformance.ConformanceEventMode? Mode
            {
                get { return ModeElement != null ? ModeElement.Value : null; }
                set
                {
                    if(value == null)
                      ModeElement = null; 
                    else
                      ModeElement = new Code<Hl7.Fhir.Model.Conformance.ConformanceEventMode>(value);
                    OnPropertyChanged("Mode");
                }
            }
            
            /// <summary>
            /// http | ftp | mllp +
            /// </summary>
            [FhirElement("protocol", InSummary=true, Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> Protocol
            {
                get { return _Protocol; }
                set { _Protocol = value; OnPropertyChanged("Protocol"); }
            }
            private List<Hl7.Fhir.Model.Coding> _Protocol;
            
            /// <summary>
            /// Resource that's focus of message
            /// </summary>
            [FhirElement("focus", InSummary=true, Order=80)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code FocusElement
            {
                get { return _FocusElement; }
                set { _FocusElement = value; OnPropertyChanged("FocusElement"); }
            }
            private Hl7.Fhir.Model.Code _FocusElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Focus
            {
                get { return FocusElement != null ? FocusElement.Value : null; }
                set
                {
                    if(value == null)
                      FocusElement = null; 
                    else
                      FocusElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Focus");
                }
            }
            
            /// <summary>
            /// Profile that describes the request
            /// </summary>
            [FhirElement("request", InSummary=true, Order=90)]
            [References("Profile")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Request
            {
                get { return _Request; }
                set { _Request = value; OnPropertyChanged("Request"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Request;
            
            /// <summary>
            /// Profile that describes the response
            /// </summary>
            [FhirElement("response", InSummary=true, Order=100)]
            [References("Profile")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Response
            {
                get { return _Response; }
                set { _Response = value; OnPropertyChanged("Response"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Response;
            
            /// <summary>
            /// Endpoint-specific event documentation
            /// </summary>
            [FhirElement("documentation", InSummary=true, Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DocumentationElement
            {
                get { return _DocumentationElement; }
                set { _DocumentationElement = value; OnPropertyChanged("DocumentationElement"); }
            }
            private Hl7.Fhir.Model.FhirString _DocumentationElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Documentation
            {
                get { return DocumentationElement != null ? DocumentationElement.Value : null; }
                set
                {
                    if(value == null)
                      DocumentationElement = null; 
                    else
                      DocumentationElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Documentation");
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ConformanceRestSecurityComponent")]
        [DataContract]
        public partial class ConformanceRestSecurityComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Adds CORS Headers (http://enable-cors.org/)
            /// </summary>
            [FhirElement("cors", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean CorsElement
            {
                get { return _CorsElement; }
                set { _CorsElement = value; OnPropertyChanged("CorsElement"); }
            }
            private Hl7.Fhir.Model.FhirBoolean _CorsElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Cors
            {
                get { return CorsElement != null ? CorsElement.Value : null; }
                set
                {
                    if(value == null)
                      CorsElement = null; 
                    else
                      CorsElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Cors");
                }
            }
            
            /// <summary>
            /// OAuth | OAuth2 | NTLM | Basic | Kerberos
            /// </summary>
            [FhirElement("service", InSummary=true, Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Service
            {
                get { return _Service; }
                set { _Service = value; OnPropertyChanged("Service"); }
            }
            private List<Hl7.Fhir.Model.CodeableConcept> _Service;
            
            /// <summary>
            /// General description of how security works
            /// </summary>
            [FhirElement("description", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
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
            /// Certificates associated with security profiles
            /// </summary>
            [FhirElement("certificate", InSummary=true, Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Conformance.ConformanceRestSecurityCertificateComponent> Certificate
            {
                get { return _Certificate; }
                set { _Certificate = value; OnPropertyChanged("Certificate"); }
            }
            private List<Hl7.Fhir.Model.Conformance.ConformanceRestSecurityCertificateComponent> _Certificate;
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ConformanceRestSecurityCertificateComponent")]
        [DataContract]
        public partial class ConformanceRestSecurityCertificateComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Mime type for certificate
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Code TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            private Hl7.Fhir.Model.Code _TypeElement;
            
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
            /// Actual certificate
            /// </summary>
            [FhirElement("blob", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Base64Binary BlobElement
            {
                get { return _BlobElement; }
                set { _BlobElement = value; OnPropertyChanged("BlobElement"); }
            }
            private Hl7.Fhir.Model.Base64Binary _BlobElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public byte[] Blob
            {
                get { return BlobElement != null ? BlobElement.Value : null; }
                set
                {
                    if(value == null)
                      BlobElement = null; 
                    else
                      BlobElement = new Hl7.Fhir.Model.Base64Binary(value);
                    OnPropertyChanged("Blob");
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ConformanceRestOperationComponent")]
        [DataContract]
        public partial class ConformanceRestOperationComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// transaction | search-system | history-system
            /// </summary>
            [FhirElement("code", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Conformance.RestfulOperationSystem> CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            private Code<Hl7.Fhir.Model.Conformance.RestfulOperationSystem> _CodeElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Conformance.RestfulOperationSystem? Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if(value == null)
                      CodeElement = null; 
                    else
                      CodeElement = new Code<Hl7.Fhir.Model.Conformance.RestfulOperationSystem>(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Anything special about operation behavior
            /// </summary>
            [FhirElement("documentation", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DocumentationElement
            {
                get { return _DocumentationElement; }
                set { _DocumentationElement = value; OnPropertyChanged("DocumentationElement"); }
            }
            private Hl7.Fhir.Model.FhirString _DocumentationElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Documentation
            {
                get { return DocumentationElement != null ? DocumentationElement.Value : null; }
                set
                {
                    if(value == null)
                      DocumentationElement = null; 
                    else
                      DocumentationElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Documentation");
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ConformanceRestResourceSearchParamComponent")]
        [DataContract]
        public partial class ConformanceRestResourceSearchParamComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Name of search parameter
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
            /// Source of definition for parameter
            /// </summary>
            [FhirElement("definition", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri DefinitionElement
            {
                get { return _DefinitionElement; }
                set { _DefinitionElement = value; OnPropertyChanged("DefinitionElement"); }
            }
            private Hl7.Fhir.Model.FhirUri _DefinitionElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public System.Uri Definition
            {
                get { return DefinitionElement != null ? DefinitionElement.Value : null; }
                set
                {
                    if(value == null)
                      DefinitionElement = null; 
                    else
                      DefinitionElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Definition");
                }
            }
            
            /// <summary>
            /// number | date | string | token | reference | composite | quantity
            /// </summary>
            [FhirElement("type", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Conformance.SearchParamType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            private Code<Hl7.Fhir.Model.Conformance.SearchParamType> _TypeElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Conformance.SearchParamType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if(value == null)
                      TypeElement = null; 
                    else
                      TypeElement = new Code<Hl7.Fhir.Model.Conformance.SearchParamType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Server-specific usage
            /// </summary>
            [FhirElement("documentation", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DocumentationElement
            {
                get { return _DocumentationElement; }
                set { _DocumentationElement = value; OnPropertyChanged("DocumentationElement"); }
            }
            private Hl7.Fhir.Model.FhirString _DocumentationElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Documentation
            {
                get { return DocumentationElement != null ? DocumentationElement.Value : null; }
                set
                {
                    if(value == null)
                      DocumentationElement = null; 
                    else
                      DocumentationElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Documentation");
                }
            }
            
            /// <summary>
            /// Types of resource (if a resource reference)
            /// </summary>
            [FhirElement("target", InSummary=true, Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Code> TargetElement
            {
                get { return _TargetElement; }
                set { _TargetElement = value; OnPropertyChanged("TargetElement"); }
            }
            private List<Hl7.Fhir.Model.Code> _TargetElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Target
            {
                get { return TargetElement != null ? TargetElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      TargetElement = null; 
                    else
                      TargetElement = new List<Hl7.Fhir.Model.Code>(value.Select(elem=>new Hl7.Fhir.Model.Code(elem)));
                    OnPropertyChanged("Target");
                }
            }
            
            /// <summary>
            /// Chained names supported
            /// </summary>
            [FhirElement("chain", InSummary=true, Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> ChainElement
            {
                get { return _ChainElement; }
                set { _ChainElement = value; OnPropertyChanged("ChainElement"); }
            }
            private List<Hl7.Fhir.Model.FhirString> _ChainElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Chain
            {
                get { return ChainElement != null ? ChainElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      ChainElement = null; 
                    else
                      ChainElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("Chain");
                }
            }
            
        }
        
        
        /// <summary>
        /// Logical id to reference this statement
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=70)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString IdentifierElement
        {
            get { return _IdentifierElement; }
            set { _IdentifierElement = value; OnPropertyChanged("IdentifierElement"); }
        }
        private Hl7.Fhir.Model.FhirString _IdentifierElement;
        
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
                  IdentifierElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Identifier");
            }
        }
        
        /// <summary>
        /// Logical id for this version of the statement
        /// </summary>
        [FhirElement("version", InSummary=true, Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement
        {
            get { return _VersionElement; }
            set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
        }
        private Hl7.Fhir.Model.FhirString _VersionElement;
        
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
        /// Informal name for this conformance statement
        /// </summary>
        [FhirElement("name", InSummary=true, Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        private Hl7.Fhir.Model.FhirString _NameElement;
        
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
        /// Publishing Organization
        /// </summary>
        [FhirElement("publisher", InSummary=true, Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PublisherElement
        {
            get { return _PublisherElement; }
            set { _PublisherElement = value; OnPropertyChanged("PublisherElement"); }
        }
        private Hl7.Fhir.Model.FhirString _PublisherElement;
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Publisher
        {
            get { return PublisherElement != null ? PublisherElement.Value : null; }
            set
            {
                if(value == null)
                  PublisherElement = null; 
                else
                  PublisherElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Publisher");
            }
        }
        
        /// <summary>
        /// Contacts for Organization
        /// </summary>
        [FhirElement("telecom", InSummary=true, Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Contact> Telecom
        {
            get { return _Telecom; }
            set { _Telecom = value; OnPropertyChanged("Telecom"); }
        }
        private List<Hl7.Fhir.Model.Contact> _Telecom;
        
        /// <summary>
        /// Human description of the conformance statement
        /// </summary>
        [FhirElement("description", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
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
        /// draft | active | retired
        /// </summary>
        [FhirElement("status", InSummary=true, Order=130)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Conformance.ConformanceStatementStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        private Code<Hl7.Fhir.Model.Conformance.ConformanceStatementStatus> _StatusElement;
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Conformance.ConformanceStatementStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.Conformance.ConformanceStatementStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// If for testing purposes, not real usage
        /// </summary>
        [FhirElement("experimental", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ExperimentalElement
        {
            get { return _ExperimentalElement; }
            set { _ExperimentalElement = value; OnPropertyChanged("ExperimentalElement"); }
        }
        private Hl7.Fhir.Model.FhirBoolean _ExperimentalElement;
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Experimental
        {
            get { return ExperimentalElement != null ? ExperimentalElement.Value : null; }
            set
            {
                if(value == null)
                  ExperimentalElement = null; 
                else
                  ExperimentalElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Experimental");
            }
        }
        
        /// <summary>
        /// Publication Date
        /// </summary>
        [FhirElement("date", InSummary=true, Order=150)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Date
        {
            get { return DateElement != null ? DateElement.Value : null; }
            set
            {
                if(value == null)
                  DateElement = null; 
                else
                  DateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Date");
            }
        }
        
        /// <summary>
        /// Software that is covered by this conformance statement
        /// </summary>
        [FhirElement("software", InSummary=true, Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.Conformance.ConformanceSoftwareComponent Software
        {
            get { return _Software; }
            set { _Software = value; OnPropertyChanged("Software"); }
        }
        private Hl7.Fhir.Model.Conformance.ConformanceSoftwareComponent _Software;
        
        /// <summary>
        /// If this describes a specific instance
        /// </summary>
        [FhirElement("implementation", InSummary=true, Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.Conformance.ConformanceImplementationComponent Implementation
        {
            get { return _Implementation; }
            set { _Implementation = value; OnPropertyChanged("Implementation"); }
        }
        private Hl7.Fhir.Model.Conformance.ConformanceImplementationComponent _Implementation;
        
        /// <summary>
        /// FHIR Version
        /// </summary>
        [FhirElement("fhirVersion", InSummary=true, Order=180)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Id FhirVersionElement
        {
            get { return _FhirVersionElement; }
            set { _FhirVersionElement = value; OnPropertyChanged("FhirVersionElement"); }
        }
        private Hl7.Fhir.Model.Id _FhirVersionElement;
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string FhirVersion
        {
            get { return FhirVersionElement != null ? FhirVersionElement.Value : null; }
            set
            {
                if(value == null)
                  FhirVersionElement = null; 
                else
                  FhirVersionElement = new Hl7.Fhir.Model.Id(value);
                OnPropertyChanged("FhirVersion");
            }
        }
        
        /// <summary>
        /// True if application accepts unknown elements
        /// </summary>
        [FhirElement("acceptUnknown", InSummary=true, Order=190)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean AcceptUnknownElement
        {
            get { return _AcceptUnknownElement; }
            set { _AcceptUnknownElement = value; OnPropertyChanged("AcceptUnknownElement"); }
        }
        private Hl7.Fhir.Model.FhirBoolean _AcceptUnknownElement;
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? AcceptUnknown
        {
            get { return AcceptUnknownElement != null ? AcceptUnknownElement.Value : null; }
            set
            {
                if(value == null)
                  AcceptUnknownElement = null; 
                else
                  AcceptUnknownElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("AcceptUnknown");
            }
        }
        
        /// <summary>
        /// formats supported (xml | json | mime type)
        /// </summary>
        [FhirElement("format", Order=200)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Code> FormatElement
        {
            get { return _FormatElement; }
            set { _FormatElement = value; OnPropertyChanged("FormatElement"); }
        }
        private List<Hl7.Fhir.Model.Code> _FormatElement;
        
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Format
        {
            get { return FormatElement != null ? FormatElement.Select(elem => elem.Value) : null; }
            set
            {
                if(value == null)
                  FormatElement = null; 
                else
                  FormatElement = new List<Hl7.Fhir.Model.Code>(value.Select(elem=>new Hl7.Fhir.Model.Code(elem)));
                OnPropertyChanged("Format");
            }
        }
        
        /// <summary>
        /// Profiles supported by the system
        /// </summary>
        [FhirElement("profile", Order=210)]
        [References("Profile")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Profile
        {
            get { return _Profile; }
            set { _Profile = value; OnPropertyChanged("Profile"); }
        }
        private List<Hl7.Fhir.Model.ResourceReference> _Profile;
        
        /// <summary>
        /// If the endpoint is a RESTful one
        /// </summary>
        [FhirElement("rest", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Conformance.ConformanceRestComponent> Rest
        {
            get { return _Rest; }
            set { _Rest = value; OnPropertyChanged("Rest"); }
        }
        private List<Hl7.Fhir.Model.Conformance.ConformanceRestComponent> _Rest;
        
        /// <summary>
        /// If messaging is supported
        /// </summary>
        [FhirElement("messaging", Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Conformance.ConformanceMessagingComponent> Messaging
        {
            get { return _Messaging; }
            set { _Messaging = value; OnPropertyChanged("Messaging"); }
        }
        private List<Hl7.Fhir.Model.Conformance.ConformanceMessagingComponent> _Messaging;
        
        /// <summary>
        /// Document definition
        /// </summary>
        [FhirElement("document", Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Conformance.ConformanceDocumentComponent> Document
        {
            get { return _Document; }
            set { _Document = value; OnPropertyChanged("Document"); }
        }
        private List<Hl7.Fhir.Model.Conformance.ConformanceDocumentComponent> _Document;
        
    }
    
}
