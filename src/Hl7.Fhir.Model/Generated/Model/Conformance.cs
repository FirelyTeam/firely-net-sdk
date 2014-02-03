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
// Generated on Mon, Feb 3, 2014 11:56+0100 for FHIR v0.80
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A conformance statement
    /// </summary>
    [FhirType("Conformance", IsResource=true)]
    [DataContract]
    public partial class Conformance : Hl7.Fhir.Model.Resource
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
        public partial class ConformanceRestQueryComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Special named queries (_query=)
            /// </summary>
            [FhirElement("name", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Where query is defined
            /// </summary>
            [FhirElement("definition", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri DefinitionElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Additional usage guidance
            /// </summary>
            [FhirElement("documentation", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DocumentationElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Parameter for the named query
            /// </summary>
            [FhirElement("parameter", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Conformance.ConformanceRestResourceSearchParamComponent> Parameter { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ConformanceRestComponent")]
        [DataContract]
        public partial class ConformanceRestComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// client | server
            /// </summary>
            [FhirElement("mode", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Conformance.RestfulConformanceMode> ModeElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// General description of implementation
            /// </summary>
            [FhirElement("documentation", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DocumentationElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Information about security of implementation
            /// </summary>
            [FhirElement("security", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Conformance.ConformanceRestSecurityComponent Security { get; set; }
            
            /// <summary>
            /// Resource served on the REST interface
            /// </summary>
            [FhirElement("resource", Order=70)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Conformance.ConformanceRestResourceComponent> Resource { get; set; }
            
            /// <summary>
            /// What operations are supported?
            /// </summary>
            [FhirElement("operation", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Conformance.ConformanceRestOperationComponent> Operation { get; set; }
            
            /// <summary>
            /// Definition of a named query
            /// </summary>
            [FhirElement("query", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Conformance.ConformanceRestQueryComponent> Query { get; set; }
            
            /// <summary>
            /// How documents are accepted in /Mailbox
            /// </summary>
            [FhirElement("documentMailbox", Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirUri> DocumentMailboxElement { get; set; }
            
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
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ConformanceSoftwareComponent")]
        [DataContract]
        public partial class ConformanceSoftwareComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// A name the software is known by
            /// </summary>
            [FhirElement("name", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Version covered by this statement
            /// </summary>
            [FhirElement("version", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString VersionElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Date this version released
            /// </summary>
            [FhirElement("releaseDate", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime ReleaseDateElement { get; set; }
            
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
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ConformanceMessagingComponent")]
        [DataContract]
        public partial class ConformanceMessagingComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Actual endpoint being described
            /// </summary>
            [FhirElement("endpoint", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri EndpointElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Reliable Message Cache Length
            /// </summary>
            [FhirElement("reliableCache", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Integer ReliableCacheElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Messaging interface behavior details
            /// </summary>
            [FhirElement("documentation", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DocumentationElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Declare support for this event
            /// </summary>
            [FhirElement("event", Order=70)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Conformance.ConformanceMessagingEventComponent> Event { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ConformanceDocumentComponent")]
        [DataContract]
        public partial class ConformanceDocumentComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// producer | consumer
            /// </summary>
            [FhirElement("mode", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Conformance.DocumentMode> ModeElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Description of document support
            /// </summary>
            [FhirElement("documentation", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DocumentationElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Constraint on a resource used in the document
            /// </summary>
            [FhirElement("profile", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Profile { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ConformanceRestResourceComponent")]
        [DataContract]
        public partial class ConformanceRestResourceComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// A resource type that is supported
            /// </summary>
            [FhirElement("type", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code TypeElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// What structural features are supported
            /// </summary>
            [FhirElement("profile", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Profile { get; set; }
            
            /// <summary>
            /// What operations are supported?
            /// </summary>
            [FhirElement("operation", Order=60)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Conformance.ConformanceRestResourceOperationComponent> Operation { get; set; }
            
            /// <summary>
            /// Whether vRead can return past versions
            /// </summary>
            [FhirElement("readHistory", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean ReadHistoryElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// If allows/uses update to a new location
            /// </summary>
            [FhirElement("updateCreate", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean UpdateCreateElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// _include values supported by the server
            /// </summary>
            [FhirElement("searchInclude", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> SearchIncludeElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Additional search params defined
            /// </summary>
            [FhirElement("searchParam", Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Conformance.ConformanceRestResourceSearchParamComponent> SearchParam { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ConformanceImplementationComponent")]
        [DataContract]
        public partial class ConformanceImplementationComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Describes this specific instance
            /// </summary>
            [FhirElement("description", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Base URL for the installation
            /// </summary>
            [FhirElement("url", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri UrlElement { get; set; }
            
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
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ConformanceRestResourceOperationComponent")]
        [DataContract]
        public partial class ConformanceRestResourceOperationComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// read | vread | update | delete | history-instance | validate | history-type | create | search-type
            /// </summary>
            [FhirElement("code", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Conformance.RestfulOperationType> CodeElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Anything special about operation behavior
            /// </summary>
            [FhirElement("documentation", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DocumentationElement { get; set; }
            
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
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ConformanceMessagingEventComponent")]
        [DataContract]
        public partial class ConformanceMessagingEventComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Event type
            /// </summary>
            [FhirElement("code", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Code { get; set; }
            
            /// <summary>
            /// Consequence | Currency | Notification
            /// </summary>
            [FhirElement("category", Order=50)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Conformance.MessageSignificanceCategory> CategoryElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// sender | receiver
            /// </summary>
            [FhirElement("mode", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Conformance.ConformanceEventMode> ModeElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// http | ftp | mllp +
            /// </summary>
            [FhirElement("protocol", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> Protocol { get; set; }
            
            /// <summary>
            /// Resource that's focus of message
            /// </summary>
            [FhirElement("focus", Order=80)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code FocusElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Profile that describes the request
            /// </summary>
            [FhirElement("request", Order=90)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Request { get; set; }
            
            /// <summary>
            /// Profile that describes the response
            /// </summary>
            [FhirElement("response", Order=100)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Response { get; set; }
            
            /// <summary>
            /// Endpoint-specific event documentation
            /// </summary>
            [FhirElement("documentation", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DocumentationElement { get; set; }
            
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
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ConformanceRestSecurityComponent")]
        [DataContract]
        public partial class ConformanceRestSecurityComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Adds CORS Headers (http://enable-cors.org/)
            /// </summary>
            [FhirElement("cors", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean CorsElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// OAuth | OAuth2 | NTLM | Basic | Kerberos
            /// </summary>
            [FhirElement("service", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Service { get; set; }
            
            /// <summary>
            /// General description of how security works
            /// </summary>
            [FhirElement("description", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Certificates associated with security profiles
            /// </summary>
            [FhirElement("certificate", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Conformance.ConformanceRestSecurityCertificateComponent> Certificate { get; set; }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ConformanceRestSecurityCertificateComponent")]
        [DataContract]
        public partial class ConformanceRestSecurityCertificateComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Mime type for certificate
            /// </summary>
            [FhirElement("type", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Code TypeElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Actual certificate
            /// </summary>
            [FhirElement("blob", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Base64Binary BlobElement { get; set; }
            
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
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ConformanceRestOperationComponent")]
        [DataContract]
        public partial class ConformanceRestOperationComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// transaction | search-system | history-system
            /// </summary>
            [FhirElement("code", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Conformance.RestfulOperationSystem> CodeElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Anything special about operation behavior
            /// </summary>
            [FhirElement("documentation", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DocumentationElement { get; set; }
            
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
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("ConformanceRestResourceSearchParamComponent")]
        [DataContract]
        public partial class ConformanceRestResourceSearchParamComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Name of search parameter
            /// </summary>
            [FhirElement("name", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Source of definition for parameter
            /// </summary>
            [FhirElement("definition", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri DefinitionElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// number | date | string | token | reference | composite | quantity
            /// </summary>
            [FhirElement("type", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Conformance.SearchParamType> TypeElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Server-specific usage
            /// </summary>
            [FhirElement("documentation", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DocumentationElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Types of resource (if a resource reference)
            /// </summary>
            [FhirElement("target", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Code> TargetElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Chained names supported
            /// </summary>
            [FhirElement("chain", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> ChainElement { get; set; }
            
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
                }
            }
            
        }
        
        
        /// <summary>
        /// Logical id to reference this statement
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString IdentifierElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Logical id for this version of the statement
        /// </summary>
        [FhirElement("version", Order=80)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Informal name for this conformance statement
        /// </summary>
        [FhirElement("name", Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Publishing Organization
        /// </summary>
        [FhirElement("publisher", Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PublisherElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Contacts for Organization
        /// </summary>
        [FhirElement("telecom", Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Contact> Telecom { get; set; }
        
        /// <summary>
        /// Human description of the conformance statement
        /// </summary>
        [FhirElement("description", Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// draft | active | retired
        /// </summary>
        [FhirElement("status", Order=130)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Conformance.ConformanceStatementStatus> StatusElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// If for testing purposes, not real usage
        /// </summary>
        [FhirElement("experimental", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ExperimentalElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Publication Date
        /// </summary>
        [FhirElement("date", Order=150)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Software that is covered by this conformance statement
        /// </summary>
        [FhirElement("software", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.Conformance.ConformanceSoftwareComponent Software { get; set; }
        
        /// <summary>
        /// If this describes a specific instance
        /// </summary>
        [FhirElement("implementation", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.Conformance.ConformanceImplementationComponent Implementation { get; set; }
        
        /// <summary>
        /// FHIR Version
        /// </summary>
        [FhirElement("fhirVersion", Order=180)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Id FhirVersionElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// True if application accepts unknown elements
        /// </summary>
        [FhirElement("acceptUnknown", Order=190)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean AcceptUnknownElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// formats supported (xml | json | mime type)
        /// </summary>
        [FhirElement("format", Order=200)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Code> FormatElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Profiles supported by the system
        /// </summary>
        [FhirElement("profile", Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Profile { get; set; }
        
        /// <summary>
        /// If the endpoint is a RESTful one
        /// </summary>
        [FhirElement("rest", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Conformance.ConformanceRestComponent> Rest { get; set; }
        
        /// <summary>
        /// If messaging is supported
        /// </summary>
        [FhirElement("messaging", Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Conformance.ConformanceMessagingComponent> Messaging { get; set; }
        
        /// <summary>
        /// Document definition
        /// </summary>
        [FhirElement("document", Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Conformance.ConformanceDocumentComponent> Document { get; set; }
        
    }
    
}
