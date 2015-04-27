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
    /// A conformance statement
    /// </summary>
    [FhirType("Conformance", IsResource=true)]
    [DataContract]
    public partial class Conformance : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Conformance; } }
        [NotMapped]
        public override string TypeName { get { return "Conformance"; } }
        
        /// <summary>
        /// Operations supported by REST at the system level
        /// </summary>
        [FhirEnumeration("SystemRestfulInteraction")]
        public enum SystemRestfulInteraction
        {
            [EnumLiteral("transaction")]
            Transaction,
            [EnumLiteral("search-system")]
            SearchSystem,
            [EnumLiteral("history-system")]
            HistorySystem,
        }
        
        /// <summary>
        /// Whether the application produces or consumes documents
        /// </summary>
        [FhirEnumeration("DocumentMode")]
        public enum DocumentMode
        {
            /// <summary>
            /// The application produces documents of the specified type.
            /// </summary>
            [EnumLiteral("producer")]
            Producer,
            /// <summary>
            /// The application consumes documents of the specified type.
            /// </summary>
            [EnumLiteral("consumer")]
            Consumer,
        }
        
        /// <summary>
        /// How the system supports versioning for a resource
        /// </summary>
        [FhirEnumeration("ResourceVersionPolicy")]
        public enum ResourceVersionPolicy
        {
            /// <summary>
            /// VersionId meta-property is not suppoerted (server) or used (client).
            /// </summary>
            [EnumLiteral("no-version")]
            NoVersion,
            /// <summary>
            /// VersionId meta-property is suppoerted (server) or used (client).
            /// </summary>
            [EnumLiteral("versioned")]
            Versioned,
            /// <summary>
            /// VersionId is must be correct for updates (server) or will be specified (If-match header) for updates (client).
            /// </summary>
            [EnumLiteral("versioned-update")]
            VersionedUpdate,
        }
        
        /// <summary>
        /// The mode of a RESTful conformance statement
        /// </summary>
        [FhirEnumeration("RestfulConformanceMode")]
        public enum RestfulConformanceMode
        {
            /// <summary>
            /// The application acts as a client for this resource.
            /// </summary>
            [EnumLiteral("client")]
            Client,
            /// <summary>
            /// The application acts as a server for this resource.
            /// </summary>
            [EnumLiteral("server")]
            Server,
        }
        
        /// <summary>
        /// The protocol used for message transport
        /// </summary>
        [FhirEnumeration("MessageTransport")]
        public enum MessageTransport
        {
            /// <summary>
            /// The application sends or receives messages using HTTP POST (may be over http or https).
            /// </summary>
            [EnumLiteral("http")]
            Http,
            /// <summary>
            /// The application sends or receives messages using File Transfer Protocol.
            /// </summary>
            [EnumLiteral("ftp")]
            Ftp,
            /// <summary>
            /// The application sends or receivers messages using HL7's Minimal Lower Level Protocol.
            /// </summary>
            [EnumLiteral("mllp")]
            Mllp,
        }
        
        /// <summary>
        /// Operations supported by REST at the type or instance level
        /// </summary>
        [FhirEnumeration("TypeRestfulInteraction")]
        public enum TypeRestfulInteraction
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
        /// The mode of a message conformance statement
        /// </summary>
        [FhirEnumeration("ConformanceEventMode")]
        public enum ConformanceEventMode
        {
            /// <summary>
            /// The application sends requests and receives responses.
            /// </summary>
            [EnumLiteral("sender")]
            Sender,
            /// <summary>
            /// The application receives requests and sends responses.
            /// </summary>
            [EnumLiteral("receiver")]
            Receiver,
        }
        
        /// <summary>
        /// The impact of the content of a message
        /// </summary>
        [FhirEnumeration("MessageSignificanceCategory")]
        public enum MessageSignificanceCategory
        {
            /// <summary>
            /// The message represents/requests a change that should not be processed more than once. E.g. Making a booking for an appointment.
            /// </summary>
            [EnumLiteral("Consequence")]
            Consequence,
            /// <summary>
            /// The message represents a response to query for current information. Retrospective processing is wrong and/or wasteful.
            /// </summary>
            [EnumLiteral("Currency")]
            Currency,
            /// <summary>
            /// The content is not necessarily intended to be current, and it can be reprocessed, though there may be version issues created by processing old notifications.
            /// </summary>
            [EnumLiteral("Notification")]
            Notification,
        }
        
        /// <summary>
        /// Data types allowed to be used for search parameters
        /// </summary>
        [FhirEnumeration("SearchParamType")]
        public enum SearchParamType
        {
            /// <summary>
            /// Search parameter SHALL be a number (a whole number, or a decimal).
            /// </summary>
            [EnumLiteral("number")]
            Number,
            /// <summary>
            /// Search parameter is on a date/time. The date format is the standard XML format, though other formats may be supported.
            /// </summary>
            [EnumLiteral("date")]
            Date,
            /// <summary>
            /// Search parameter is a simple string, like a name part. Search is case-insensitive and accent-insensitive. May match just the start of a string. String parameters may contain spaces.
            /// </summary>
            [EnumLiteral("string")]
            String,
            /// <summary>
            /// Search parameter on a coded element or identifier. May be used to search through the text, displayname, code and code/codesystem (for codes) and label, system and key (for identifier). Its value is either a string or a pair of namespace and value, separated by a "|", depending on the modifier used.
            /// </summary>
            [EnumLiteral("token")]
            Token,
            /// <summary>
            /// A reference to another resource.
            /// </summary>
            [EnumLiteral("reference")]
            Reference,
            /// <summary>
            /// A composite search parameter that combines a search on two values together.
            /// </summary>
            [EnumLiteral("composite")]
            Composite,
            /// <summary>
            /// A search parameter that searches on a quantity.
            /// </summary>
            [EnumLiteral("quantity")]
            Quantity,
            /// <summary>
            /// A search parameter that searches on a URI (RFC 3986).
            /// </summary>
            [EnumLiteral("uri")]
            Uri,
        }
        
        /// <summary>
        /// Types of security services used with FHIR
        /// </summary>
        [FhirEnumeration("RestfulSecurityService")]
        public enum RestfulSecurityService
        {
            /// <summary>
            /// OAuth (see oauth.net).
            /// </summary>
            [EnumLiteral("OAuth")]
            OAuth,
            /// <summary>
            /// OAuth version 2 (see oauth.net).
            /// </summary>
            [EnumLiteral("OAuth2")]
            OAuth2,
            /// <summary>
            /// Microsoft NTLM Authentication.
            /// </summary>
            [EnumLiteral("NTLM")]
            NTLM,
            /// <summary>
            /// Basic authentication defined in HTTP specification.
            /// </summary>
            [EnumLiteral("Basic")]
            Basic,
            /// <summary>
            /// see http://www.ietf.org/rfc/rfc4120.txt.
            /// </summary>
            [EnumLiteral("Kerberos")]
            Kerberos,
        }
        
        [FhirType("ConformanceRestComponent")]
        [DataContract]
        public partial class ConformanceRestComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ConformanceRestComponent"; } }
            
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
            
            /// <summary>
            /// client | server
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// General description of implementation
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
                get { if(_Resource==null) _Resource = new List<Hl7.Fhir.Model.Conformance.ConformanceRestResourceComponent>(); return _Resource; }
                set { _Resource = value; OnPropertyChanged("Resource"); }
            }
            
            private List<Hl7.Fhir.Model.Conformance.ConformanceRestResourceComponent> _Resource;
            
            /// <summary>
            /// What operations are supported?
            /// </summary>
            [FhirElement("interaction", InSummary=true, Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Conformance.SystemInteractionComponent> Interaction
            {
                get { if(_Interaction==null) _Interaction = new List<Hl7.Fhir.Model.Conformance.SystemInteractionComponent>(); return _Interaction; }
                set { _Interaction = value; OnPropertyChanged("Interaction"); }
            }
            
            private List<Hl7.Fhir.Model.Conformance.SystemInteractionComponent> _Interaction;
            
            /// <summary>
            /// Definition of an operation or a custom query
            /// </summary>
            [FhirElement("operation", InSummary=true, Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Conformance.ConformanceRestOperationComponent> Operation
            {
                get { if(_Operation==null) _Operation = new List<Hl7.Fhir.Model.Conformance.ConformanceRestOperationComponent>(); return _Operation; }
                set { _Operation = value; OnPropertyChanged("Operation"); }
            }
            
            private List<Hl7.Fhir.Model.Conformance.ConformanceRestOperationComponent> _Operation;
            
            /// <summary>
            /// How documents are accepted in /Mailbox
            /// </summary>
            [FhirElement("documentMailbox", InSummary=true, Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirUri> DocumentMailboxElement
            {
                get { if(_DocumentMailboxElement==null) _DocumentMailboxElement = new List<Hl7.Fhir.Model.FhirUri>(); return _DocumentMailboxElement; }
                set { _DocumentMailboxElement = value; OnPropertyChanged("DocumentMailboxElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirUri> _DocumentMailboxElement;
            
            /// <summary>
            /// How documents are accepted in /Mailbox
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> DocumentMailbox
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
            
            /// <summary>
            /// Compartments served/used by system
            /// </summary>
            [FhirElement("compartment", InSummary=true, Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirUri> CompartmentElement
            {
                get { if(_CompartmentElement==null) _CompartmentElement = new List<Hl7.Fhir.Model.FhirUri>(); return _CompartmentElement; }
                set { _CompartmentElement = value; OnPropertyChanged("CompartmentElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirUri> _CompartmentElement;
            
            /// <summary>
            /// Compartments served/used by system
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Compartment
            {
                get { return CompartmentElement != null ? CompartmentElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      CompartmentElement = null; 
                    else
                      CompartmentElement = new List<Hl7.Fhir.Model.FhirUri>(value.Select(elem=>new Hl7.Fhir.Model.FhirUri(elem)));
                    OnPropertyChanged("Compartment");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ConformanceRestComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ModeElement != null) dest.ModeElement = (Code<Hl7.Fhir.Model.Conformance.RestfulConformanceMode>)ModeElement.DeepCopy();
                    if(DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
                    if(Security != null) dest.Security = (Hl7.Fhir.Model.Conformance.ConformanceRestSecurityComponent)Security.DeepCopy();
                    if(Resource != null) dest.Resource = new List<Hl7.Fhir.Model.Conformance.ConformanceRestResourceComponent>(Resource.DeepCopy());
                    if(Interaction != null) dest.Interaction = new List<Hl7.Fhir.Model.Conformance.SystemInteractionComponent>(Interaction.DeepCopy());
                    if(Operation != null) dest.Operation = new List<Hl7.Fhir.Model.Conformance.ConformanceRestOperationComponent>(Operation.DeepCopy());
                    if(DocumentMailboxElement != null) dest.DocumentMailboxElement = new List<Hl7.Fhir.Model.FhirUri>(DocumentMailboxElement.DeepCopy());
                    if(CompartmentElement != null) dest.CompartmentElement = new List<Hl7.Fhir.Model.FhirUri>(CompartmentElement.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ConformanceRestComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ConformanceRestComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
                if( !DeepComparable.Matches(Security, otherT.Security)) return false;
                if( !DeepComparable.Matches(Resource, otherT.Resource)) return false;
                if( !DeepComparable.Matches(Interaction, otherT.Interaction)) return false;
                if( !DeepComparable.Matches(Operation, otherT.Operation)) return false;
                if( !DeepComparable.Matches(DocumentMailboxElement, otherT.DocumentMailboxElement)) return false;
                if( !DeepComparable.Matches(CompartmentElement, otherT.CompartmentElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ConformanceRestComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
                if( !DeepComparable.IsExactly(Security, otherT.Security)) return false;
                if( !DeepComparable.IsExactly(Resource, otherT.Resource)) return false;
                if( !DeepComparable.IsExactly(Interaction, otherT.Interaction)) return false;
                if( !DeepComparable.IsExactly(Operation, otherT.Operation)) return false;
                if( !DeepComparable.IsExactly(DocumentMailboxElement, otherT.DocumentMailboxElement)) return false;
                if( !DeepComparable.IsExactly(CompartmentElement, otherT.CompartmentElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ConformanceSoftwareComponent")]
        [DataContract]
        public partial class ConformanceSoftwareComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ConformanceSoftwareComponent"; } }
            
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
            
            /// <summary>
            /// A name the software is known by
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
            
            /// <summary>
            /// Version covered by this statement
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
            
            /// <summary>
            /// Date this version released
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ConformanceSoftwareComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                    if(ReleaseDateElement != null) dest.ReleaseDateElement = (Hl7.Fhir.Model.FhirDateTime)ReleaseDateElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ConformanceSoftwareComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ConformanceSoftwareComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
                if( !DeepComparable.Matches(ReleaseDateElement, otherT.ReleaseDateElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ConformanceSoftwareComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
                if( !DeepComparable.IsExactly(ReleaseDateElement, otherT.ReleaseDateElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ConformanceMessagingComponent")]
        [DataContract]
        public partial class ConformanceMessagingComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ConformanceMessagingComponent"; } }
            
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
            
            /// <summary>
            /// Actual endpoint being described
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
            
            /// <summary>
            /// Reliable Message Cache Length (min)
            /// </summary>
            [FhirElement("reliableCache", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.UnsignedInt ReliableCacheElement
            {
                get { return _ReliableCacheElement; }
                set { _ReliableCacheElement = value; OnPropertyChanged("ReliableCacheElement"); }
            }
            
            private Hl7.Fhir.Model.UnsignedInt _ReliableCacheElement;
            
            /// <summary>
            /// Reliable Message Cache Length (min)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
                      ReliableCacheElement = new Hl7.Fhir.Model.UnsignedInt(value);
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
            
            /// <summary>
            /// Messaging interface behavior details
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
                get { if(_Event==null) _Event = new List<Hl7.Fhir.Model.Conformance.ConformanceMessagingEventComponent>(); return _Event; }
                set { _Event = value; OnPropertyChanged("Event"); }
            }
            
            private List<Hl7.Fhir.Model.Conformance.ConformanceMessagingEventComponent> _Event;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ConformanceMessagingComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(EndpointElement != null) dest.EndpointElement = (Hl7.Fhir.Model.FhirUri)EndpointElement.DeepCopy();
                    if(ReliableCacheElement != null) dest.ReliableCacheElement = (Hl7.Fhir.Model.UnsignedInt)ReliableCacheElement.DeepCopy();
                    if(DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
                    if(Event != null) dest.Event = new List<Hl7.Fhir.Model.Conformance.ConformanceMessagingEventComponent>(Event.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ConformanceMessagingComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ConformanceMessagingComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(EndpointElement, otherT.EndpointElement)) return false;
                if( !DeepComparable.Matches(ReliableCacheElement, otherT.ReliableCacheElement)) return false;
                if( !DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
                if( !DeepComparable.Matches(Event, otherT.Event)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ConformanceMessagingComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(EndpointElement, otherT.EndpointElement)) return false;
                if( !DeepComparable.IsExactly(ReliableCacheElement, otherT.ReliableCacheElement)) return false;
                if( !DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
                if( !DeepComparable.IsExactly(Event, otherT.Event)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ConformanceDocumentComponent")]
        [DataContract]
        public partial class ConformanceDocumentComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ConformanceDocumentComponent"; } }
            
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
            
            /// <summary>
            /// producer | consumer
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Description of document support
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            [References("StructureDefinition")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Profile
            {
                get { return _Profile; }
                set { _Profile = value; OnPropertyChanged("Profile"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Profile;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ConformanceDocumentComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ModeElement != null) dest.ModeElement = (Code<Hl7.Fhir.Model.Conformance.DocumentMode>)ModeElement.DeepCopy();
                    if(DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
                    if(Profile != null) dest.Profile = (Hl7.Fhir.Model.ResourceReference)Profile.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ConformanceDocumentComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ConformanceDocumentComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
                if( !DeepComparable.Matches(Profile, otherT.Profile)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ConformanceDocumentComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
                if( !DeepComparable.IsExactly(Profile, otherT.Profile)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ConformanceRestResourceComponent")]
        [DataContract]
        public partial class ConformanceRestResourceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ConformanceRestResourceComponent"; } }
            
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
            
            /// <summary>
            /// A resource type that is supported
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            [References("StructureDefinition")]
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
            [FhirElement("interaction", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Conformance.ResourceInteractionComponent> Interaction
            {
                get { if(_Interaction==null) _Interaction = new List<Hl7.Fhir.Model.Conformance.ResourceInteractionComponent>(); return _Interaction; }
                set { _Interaction = value; OnPropertyChanged("Interaction"); }
            }
            
            private List<Hl7.Fhir.Model.Conformance.ResourceInteractionComponent> _Interaction;
            
            /// <summary>
            /// no-version | versioned | versioned-update
            /// </summary>
            [FhirElement("versioning", InSummary=true, Order=70)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Conformance.ResourceVersionPolicy> VersioningElement
            {
                get { return _VersioningElement; }
                set { _VersioningElement = value; OnPropertyChanged("VersioningElement"); }
            }
            
            private Code<Hl7.Fhir.Model.Conformance.ResourceVersionPolicy> _VersioningElement;
            
            /// <summary>
            /// no-version | versioned | versioned-update
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Conformance.ResourceVersionPolicy? Versioning
            {
                get { return VersioningElement != null ? VersioningElement.Value : null; }
                set
                {
                    if(value == null)
                      VersioningElement = null; 
                    else
                      VersioningElement = new Code<Hl7.Fhir.Model.Conformance.ResourceVersionPolicy>(value);
                    OnPropertyChanged("Versioning");
                }
            }
            
            /// <summary>
            /// Whether vRead can return past versions
            /// </summary>
            [FhirElement("readHistory", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean ReadHistoryElement
            {
                get { return _ReadHistoryElement; }
                set { _ReadHistoryElement = value; OnPropertyChanged("ReadHistoryElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _ReadHistoryElement;
            
            /// <summary>
            /// Whether vRead can return past versions
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            /// If update can commit to a new identity
            /// </summary>
            [FhirElement("updateCreate", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean UpdateCreateElement
            {
                get { return _UpdateCreateElement; }
                set { _UpdateCreateElement = value; OnPropertyChanged("UpdateCreateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _UpdateCreateElement;
            
            /// <summary>
            /// If update can commit to a new identity
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            /// If allows/uses conditional create
            /// </summary>
            [FhirElement("conditionalCreate", InSummary=true, Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean ConditionalCreateElement
            {
                get { return _ConditionalCreateElement; }
                set { _ConditionalCreateElement = value; OnPropertyChanged("ConditionalCreateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _ConditionalCreateElement;
            
            /// <summary>
            /// If allows/uses conditional create
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? ConditionalCreate
            {
                get { return ConditionalCreateElement != null ? ConditionalCreateElement.Value : null; }
                set
                {
                    if(value == null)
                      ConditionalCreateElement = null; 
                    else
                      ConditionalCreateElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("ConditionalCreate");
                }
            }
            
            /// <summary>
            /// If allows/uses conditional update
            /// </summary>
            [FhirElement("conditionalUpdate", InSummary=true, Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean ConditionalUpdateElement
            {
                get { return _ConditionalUpdateElement; }
                set { _ConditionalUpdateElement = value; OnPropertyChanged("ConditionalUpdateElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _ConditionalUpdateElement;
            
            /// <summary>
            /// If allows/uses conditional update
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? ConditionalUpdate
            {
                get { return ConditionalUpdateElement != null ? ConditionalUpdateElement.Value : null; }
                set
                {
                    if(value == null)
                      ConditionalUpdateElement = null; 
                    else
                      ConditionalUpdateElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("ConditionalUpdate");
                }
            }
            
            /// <summary>
            /// If allows/uses conditional delete
            /// </summary>
            [FhirElement("conditionalDelete", InSummary=true, Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean ConditionalDeleteElement
            {
                get { return _ConditionalDeleteElement; }
                set { _ConditionalDeleteElement = value; OnPropertyChanged("ConditionalDeleteElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _ConditionalDeleteElement;
            
            /// <summary>
            /// If allows/uses conditional delete
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? ConditionalDelete
            {
                get { return ConditionalDeleteElement != null ? ConditionalDeleteElement.Value : null; }
                set
                {
                    if(value == null)
                      ConditionalDeleteElement = null; 
                    else
                      ConditionalDeleteElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("ConditionalDelete");
                }
            }
            
            /// <summary>
            /// _include values supported by the server
            /// </summary>
            [FhirElement("searchInclude", InSummary=true, Order=130)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> SearchIncludeElement
            {
                get { if(_SearchIncludeElement==null) _SearchIncludeElement = new List<Hl7.Fhir.Model.FhirString>(); return _SearchIncludeElement; }
                set { _SearchIncludeElement = value; OnPropertyChanged("SearchIncludeElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _SearchIncludeElement;
            
            /// <summary>
            /// _include values supported by the server
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            /// Search params supported by implementation
            /// </summary>
            [FhirElement("searchParam", InSummary=true, Order=140)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Conformance.ConformanceRestResourceSearchParamComponent> SearchParam
            {
                get { if(_SearchParam==null) _SearchParam = new List<Hl7.Fhir.Model.Conformance.ConformanceRestResourceSearchParamComponent>(); return _SearchParam; }
                set { _SearchParam = value; OnPropertyChanged("SearchParam"); }
            }
            
            private List<Hl7.Fhir.Model.Conformance.ConformanceRestResourceSearchParamComponent> _SearchParam;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ConformanceRestResourceComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Hl7.Fhir.Model.Code)TypeElement.DeepCopy();
                    if(Profile != null) dest.Profile = (Hl7.Fhir.Model.ResourceReference)Profile.DeepCopy();
                    if(Interaction != null) dest.Interaction = new List<Hl7.Fhir.Model.Conformance.ResourceInteractionComponent>(Interaction.DeepCopy());
                    if(VersioningElement != null) dest.VersioningElement = (Code<Hl7.Fhir.Model.Conformance.ResourceVersionPolicy>)VersioningElement.DeepCopy();
                    if(ReadHistoryElement != null) dest.ReadHistoryElement = (Hl7.Fhir.Model.FhirBoolean)ReadHistoryElement.DeepCopy();
                    if(UpdateCreateElement != null) dest.UpdateCreateElement = (Hl7.Fhir.Model.FhirBoolean)UpdateCreateElement.DeepCopy();
                    if(ConditionalCreateElement != null) dest.ConditionalCreateElement = (Hl7.Fhir.Model.FhirBoolean)ConditionalCreateElement.DeepCopy();
                    if(ConditionalUpdateElement != null) dest.ConditionalUpdateElement = (Hl7.Fhir.Model.FhirBoolean)ConditionalUpdateElement.DeepCopy();
                    if(ConditionalDeleteElement != null) dest.ConditionalDeleteElement = (Hl7.Fhir.Model.FhirBoolean)ConditionalDeleteElement.DeepCopy();
                    if(SearchIncludeElement != null) dest.SearchIncludeElement = new List<Hl7.Fhir.Model.FhirString>(SearchIncludeElement.DeepCopy());
                    if(SearchParam != null) dest.SearchParam = new List<Hl7.Fhir.Model.Conformance.ConformanceRestResourceSearchParamComponent>(SearchParam.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ConformanceRestResourceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ConformanceRestResourceComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(Profile, otherT.Profile)) return false;
                if( !DeepComparable.Matches(Interaction, otherT.Interaction)) return false;
                if( !DeepComparable.Matches(VersioningElement, otherT.VersioningElement)) return false;
                if( !DeepComparable.Matches(ReadHistoryElement, otherT.ReadHistoryElement)) return false;
                if( !DeepComparable.Matches(UpdateCreateElement, otherT.UpdateCreateElement)) return false;
                if( !DeepComparable.Matches(ConditionalCreateElement, otherT.ConditionalCreateElement)) return false;
                if( !DeepComparable.Matches(ConditionalUpdateElement, otherT.ConditionalUpdateElement)) return false;
                if( !DeepComparable.Matches(ConditionalDeleteElement, otherT.ConditionalDeleteElement)) return false;
                if( !DeepComparable.Matches(SearchIncludeElement, otherT.SearchIncludeElement)) return false;
                if( !DeepComparable.Matches(SearchParam, otherT.SearchParam)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ConformanceRestResourceComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(Profile, otherT.Profile)) return false;
                if( !DeepComparable.IsExactly(Interaction, otherT.Interaction)) return false;
                if( !DeepComparable.IsExactly(VersioningElement, otherT.VersioningElement)) return false;
                if( !DeepComparable.IsExactly(ReadHistoryElement, otherT.ReadHistoryElement)) return false;
                if( !DeepComparable.IsExactly(UpdateCreateElement, otherT.UpdateCreateElement)) return false;
                if( !DeepComparable.IsExactly(ConditionalCreateElement, otherT.ConditionalCreateElement)) return false;
                if( !DeepComparable.IsExactly(ConditionalUpdateElement, otherT.ConditionalUpdateElement)) return false;
                if( !DeepComparable.IsExactly(ConditionalDeleteElement, otherT.ConditionalDeleteElement)) return false;
                if( !DeepComparable.IsExactly(SearchIncludeElement, otherT.SearchIncludeElement)) return false;
                if( !DeepComparable.IsExactly(SearchParam, otherT.SearchParam)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ConformanceImplementationComponent")]
        [DataContract]
        public partial class ConformanceImplementationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ConformanceImplementationComponent"; } }
            
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
            
            /// <summary>
            /// Describes this specific instance
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Base URL for the installation
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Url
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ConformanceImplementationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ConformanceImplementationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ConformanceImplementationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ConformanceImplementationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ConformanceContactComponent")]
        [DataContract]
        public partial class ConformanceContactComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ConformanceContactComponent"; } }
            
            /// <summary>
            /// Name of a individual to contact
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
            /// Name of a individual to contact
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
            /// Contact details for individual or publisher
            /// </summary>
            [FhirElement("telecom", InSummary=true, Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ContactPoint> Telecom
            {
                get { if(_Telecom==null) _Telecom = new List<Hl7.Fhir.Model.ContactPoint>(); return _Telecom; }
                set { _Telecom = value; OnPropertyChanged("Telecom"); }
            }
            
            private List<Hl7.Fhir.Model.ContactPoint> _Telecom;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ConformanceContactComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(Telecom != null) dest.Telecom = new List<Hl7.Fhir.Model.ContactPoint>(Telecom.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ConformanceContactComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ConformanceContactComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(Telecom, otherT.Telecom)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ConformanceContactComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(Telecom, otherT.Telecom)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ConformanceMessagingEventComponent")]
        [DataContract]
        public partial class ConformanceMessagingEventComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ConformanceMessagingEventComponent"; } }
            
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
            
            /// <summary>
            /// Consequence | Currency | Notification
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// sender | receiver
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
                get { if(_Protocol==null) _Protocol = new List<Hl7.Fhir.Model.Coding>(); return _Protocol; }
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
            
            /// <summary>
            /// Resource that's focus of message
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            [References("StructureDefinition")]
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
            [References("StructureDefinition")]
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
            
            /// <summary>
            /// Endpoint-specific event documentation
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ConformanceMessagingEventComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.Coding)Code.DeepCopy();
                    if(CategoryElement != null) dest.CategoryElement = (Code<Hl7.Fhir.Model.Conformance.MessageSignificanceCategory>)CategoryElement.DeepCopy();
                    if(ModeElement != null) dest.ModeElement = (Code<Hl7.Fhir.Model.Conformance.ConformanceEventMode>)ModeElement.DeepCopy();
                    if(Protocol != null) dest.Protocol = new List<Hl7.Fhir.Model.Coding>(Protocol.DeepCopy());
                    if(FocusElement != null) dest.FocusElement = (Hl7.Fhir.Model.Code)FocusElement.DeepCopy();
                    if(Request != null) dest.Request = (Hl7.Fhir.Model.ResourceReference)Request.DeepCopy();
                    if(Response != null) dest.Response = (Hl7.Fhir.Model.ResourceReference)Response.DeepCopy();
                    if(DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ConformanceMessagingEventComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ConformanceMessagingEventComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(CategoryElement, otherT.CategoryElement)) return false;
                if( !DeepComparable.Matches(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.Matches(Protocol, otherT.Protocol)) return false;
                if( !DeepComparable.Matches(FocusElement, otherT.FocusElement)) return false;
                if( !DeepComparable.Matches(Request, otherT.Request)) return false;
                if( !DeepComparable.Matches(Response, otherT.Response)) return false;
                if( !DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ConformanceMessagingEventComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(CategoryElement, otherT.CategoryElement)) return false;
                if( !DeepComparable.IsExactly(ModeElement, otherT.ModeElement)) return false;
                if( !DeepComparable.IsExactly(Protocol, otherT.Protocol)) return false;
                if( !DeepComparable.IsExactly(FocusElement, otherT.FocusElement)) return false;
                if( !DeepComparable.IsExactly(Request, otherT.Request)) return false;
                if( !DeepComparable.IsExactly(Response, otherT.Response)) return false;
                if( !DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("SystemInteractionComponent")]
        [DataContract]
        public partial class SystemInteractionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "SystemInteractionComponent"; } }
            
            /// <summary>
            /// transaction | search-system | history-system
            /// </summary>
            [FhirElement("code", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Conformance.SystemRestfulInteraction> CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.Conformance.SystemRestfulInteraction> _CodeElement;
            
            /// <summary>
            /// transaction | search-system | history-system
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Conformance.SystemRestfulInteraction? Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if(value == null)
                      CodeElement = null; 
                    else
                      CodeElement = new Code<Hl7.Fhir.Model.Conformance.SystemRestfulInteraction>(value);
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
            
            /// <summary>
            /// Anything special about operation behavior
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SystemInteractionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Code<Hl7.Fhir.Model.Conformance.SystemRestfulInteraction>)CodeElement.DeepCopy();
                    if(DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SystemInteractionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SystemInteractionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SystemInteractionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ResourceInteractionComponent")]
        [DataContract]
        public partial class ResourceInteractionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ResourceInteractionComponent"; } }
            
            /// <summary>
            /// read | vread | update | delete | history-instance | validate | history-type | create | search-type
            /// </summary>
            [FhirElement("code", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Conformance.TypeRestfulInteraction> CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.Conformance.TypeRestfulInteraction> _CodeElement;
            
            /// <summary>
            /// read | vread | update | delete | history-instance | validate | history-type | create | search-type
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Conformance.TypeRestfulInteraction? Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if(value == null)
                      CodeElement = null; 
                    else
                      CodeElement = new Code<Hl7.Fhir.Model.Conformance.TypeRestfulInteraction>(value);
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
            
            /// <summary>
            /// Anything special about operation behavior
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ResourceInteractionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Code<Hl7.Fhir.Model.Conformance.TypeRestfulInteraction>)CodeElement.DeepCopy();
                    if(DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ResourceInteractionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ResourceInteractionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ResourceInteractionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ConformanceRestSecurityComponent")]
        [DataContract]
        public partial class ConformanceRestSecurityComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ConformanceRestSecurityComponent"; } }
            
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
            
            /// <summary>
            /// Adds CORS Headers (http://enable-cors.org/)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
                get { if(_Service==null) _Service = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Service; }
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
            
            /// <summary>
            /// General description of how security works
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
                get { if(_Certificate==null) _Certificate = new List<Hl7.Fhir.Model.Conformance.ConformanceRestSecurityCertificateComponent>(); return _Certificate; }
                set { _Certificate = value; OnPropertyChanged("Certificate"); }
            }
            
            private List<Hl7.Fhir.Model.Conformance.ConformanceRestSecurityCertificateComponent> _Certificate;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ConformanceRestSecurityComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CorsElement != null) dest.CorsElement = (Hl7.Fhir.Model.FhirBoolean)CorsElement.DeepCopy();
                    if(Service != null) dest.Service = new List<Hl7.Fhir.Model.CodeableConcept>(Service.DeepCopy());
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Certificate != null) dest.Certificate = new List<Hl7.Fhir.Model.Conformance.ConformanceRestSecurityCertificateComponent>(Certificate.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ConformanceRestSecurityComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ConformanceRestSecurityComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CorsElement, otherT.CorsElement)) return false;
                if( !DeepComparable.Matches(Service, otherT.Service)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(Certificate, otherT.Certificate)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ConformanceRestSecurityComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CorsElement, otherT.CorsElement)) return false;
                if( !DeepComparable.IsExactly(Service, otherT.Service)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Certificate, otherT.Certificate)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ConformanceRestSecurityCertificateComponent")]
        [DataContract]
        public partial class ConformanceRestSecurityCertificateComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ConformanceRestSecurityCertificateComponent"; } }
            
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
            
            /// <summary>
            /// Mime type for certificate
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Actual certificate
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ConformanceRestSecurityCertificateComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Hl7.Fhir.Model.Code)TypeElement.DeepCopy();
                    if(BlobElement != null) dest.BlobElement = (Hl7.Fhir.Model.Base64Binary)BlobElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ConformanceRestSecurityCertificateComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ConformanceRestSecurityCertificateComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(BlobElement, otherT.BlobElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ConformanceRestSecurityCertificateComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(BlobElement, otherT.BlobElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ConformanceRestOperationComponent")]
        [DataContract]
        public partial class ConformanceRestOperationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ConformanceRestOperationComponent"; } }
            
            /// <summary>
            /// Name by which the operation/query is invoked
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
            /// Name by which the operation/query is invoked
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
            /// The defined operation/query
            /// </summary>
            [FhirElement("definition", InSummary=true, Order=50)]
            [References("OperationDefinition")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Definition
            {
                get { return _Definition; }
                set { _Definition = value; OnPropertyChanged("Definition"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Definition;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ConformanceRestOperationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(Definition != null) dest.Definition = (Hl7.Fhir.Model.ResourceReference)Definition.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ConformanceRestOperationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ConformanceRestOperationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(Definition, otherT.Definition)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ConformanceRestOperationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(Definition, otherT.Definition)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ConformanceRestResourceSearchParamComponent")]
        [DataContract]
        public partial class ConformanceRestResourceSearchParamComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ConformanceRestResourceSearchParamComponent"; } }
            
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
            
            /// <summary>
            /// Name of search parameter
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
            
            /// <summary>
            /// Source of definition for parameter
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Definition
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
            /// number | date | string | token | reference | composite | quantity | uri
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
            
            /// <summary>
            /// number | date | string | token | reference | composite | quantity | uri
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Server-specific usage
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
                get { if(_TargetElement==null) _TargetElement = new List<Hl7.Fhir.Model.Code>(); return _TargetElement; }
                set { _TargetElement = value; OnPropertyChanged("TargetElement"); }
            }
            
            private List<Hl7.Fhir.Model.Code> _TargetElement;
            
            /// <summary>
            /// Types of resource (if a resource reference)
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
                get { if(_ChainElement==null) _ChainElement = new List<Hl7.Fhir.Model.FhirString>(); return _ChainElement; }
                set { _ChainElement = value; OnPropertyChanged("ChainElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _ChainElement;
            
            /// <summary>
            /// Chained names supported
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ConformanceRestResourceSearchParamComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(DefinitionElement != null) dest.DefinitionElement = (Hl7.Fhir.Model.FhirUri)DefinitionElement.DeepCopy();
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.Conformance.SearchParamType>)TypeElement.DeepCopy();
                    if(DocumentationElement != null) dest.DocumentationElement = (Hl7.Fhir.Model.FhirString)DocumentationElement.DeepCopy();
                    if(TargetElement != null) dest.TargetElement = new List<Hl7.Fhir.Model.Code>(TargetElement.DeepCopy());
                    if(ChainElement != null) dest.ChainElement = new List<Hl7.Fhir.Model.FhirString>(ChainElement.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ConformanceRestResourceSearchParamComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ConformanceRestResourceSearchParamComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(DefinitionElement, otherT.DefinitionElement)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(DocumentationElement, otherT.DocumentationElement)) return false;
                if( !DeepComparable.Matches(TargetElement, otherT.TargetElement)) return false;
                if( !DeepComparable.Matches(ChainElement, otherT.ChainElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ConformanceRestResourceSearchParamComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(DefinitionElement, otherT.DefinitionElement)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(DocumentationElement, otherT.DocumentationElement)) return false;
                if( !DeepComparable.IsExactly(TargetElement, otherT.TargetElement)) return false;
                if( !DeepComparable.IsExactly(ChainElement, otherT.ChainElement)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// Logical uri to reference this statement
        /// </summary>
        [FhirElement("url", InSummary=true, Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri UrlElement
        {
            get { return _UrlElement; }
            set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _UrlElement;
        
        /// <summary>
        /// Logical uri to reference this statement
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Url
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
        
        /// <summary>
        /// Logical id for this version of the statement
        /// </summary>
        [FhirElement("version", InSummary=true, Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement
        {
            get { return _VersionElement; }
            set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _VersionElement;
        
        /// <summary>
        /// Logical id for this version of the statement
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
        /// Informal name for this conformance statement
        /// </summary>
        [FhirElement("name", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// Informal name for this conformance statement
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
        /// Name of the publisher (Organization or individual)
        /// </summary>
        [FhirElement("publisher", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PublisherElement
        {
            get { return _PublisherElement; }
            set { _PublisherElement = value; OnPropertyChanged("PublisherElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _PublisherElement;
        
        /// <summary>
        /// Name of the publisher (Organization or individual)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
        /// Contact details of the publisher
        /// </summary>
        [FhirElement("contact", InSummary=true, Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Conformance.ConformanceContactComponent> Contact
        {
            get { if(_Contact==null) _Contact = new List<Hl7.Fhir.Model.Conformance.ConformanceContactComponent>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<Hl7.Fhir.Model.Conformance.ConformanceContactComponent> _Contact;
        
        /// <summary>
        /// Human description of the conformance statement
        /// </summary>
        [FhirElement("description", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
        /// <summary>
        /// Human description of the conformance statement
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
        /// Why is this needed?
        /// </summary>
        [FhirElement("requirements", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString RequirementsElement
        {
            get { return _RequirementsElement; }
            set { _RequirementsElement = value; OnPropertyChanged("RequirementsElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _RequirementsElement;
        
        /// <summary>
        /// Why is this needed?
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Requirements
        {
            get { return RequirementsElement != null ? RequirementsElement.Value : null; }
            set
            {
                if(value == null)
                  RequirementsElement = null; 
                else
                  RequirementsElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Requirements");
            }
        }
        
        /// <summary>
        /// Use and/or Publishing restrictions
        /// </summary>
        [FhirElement("copyright", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString CopyrightElement
        {
            get { return _CopyrightElement; }
            set { _CopyrightElement = value; OnPropertyChanged("CopyrightElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _CopyrightElement;
        
        /// <summary>
        /// Use and/or Publishing restrictions
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Copyright
        {
            get { return CopyrightElement != null ? CopyrightElement.Value : null; }
            set
            {
                if(value == null)
                  CopyrightElement = null; 
                else
                  CopyrightElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Copyright");
            }
        }
        
        /// <summary>
        /// draft | active | retired
        /// </summary>
        [FhirElement("status", InSummary=true, Order=170)]
        [DataMember]
        public Code<Hl7.Fhir.Model.ConformanceResourceStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.ConformanceResourceStatus> _StatusElement;
        
        /// <summary>
        /// draft | active | retired
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.ConformanceResourceStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.ConformanceResourceStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// If for testing purposes, not real usage
        /// </summary>
        [FhirElement("experimental", InSummary=true, Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ExperimentalElement
        {
            get { return _ExperimentalElement; }
            set { _ExperimentalElement = value; OnPropertyChanged("ExperimentalElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _ExperimentalElement;
        
        /// <summary>
        /// If for testing purposes, not real usage
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
        /// Publication Date(/time)
        /// </summary>
        [FhirElement("date", InSummary=true, Order=190)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// Publication Date(/time)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
        [FhirElement("software", InSummary=true, Order=200)]
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
        [FhirElement("implementation", InSummary=true, Order=210)]
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
        [FhirElement("fhirVersion", InSummary=true, Order=220)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Id FhirVersionElement
        {
            get { return _FhirVersionElement; }
            set { _FhirVersionElement = value; OnPropertyChanged("FhirVersionElement"); }
        }
        
        private Hl7.Fhir.Model.Id _FhirVersionElement;
        
        /// <summary>
        /// FHIR Version
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
        [FhirElement("acceptUnknown", InSummary=true, Order=230)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean AcceptUnknownElement
        {
            get { return _AcceptUnknownElement; }
            set { _AcceptUnknownElement = value; OnPropertyChanged("AcceptUnknownElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _AcceptUnknownElement;
        
        /// <summary>
        /// True if application accepts unknown elements
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
        [FhirElement("format", Order=240)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Code> FormatElement
        {
            get { if(_FormatElement==null) _FormatElement = new List<Hl7.Fhir.Model.Code>(); return _FormatElement; }
            set { _FormatElement = value; OnPropertyChanged("FormatElement"); }
        }
        
        private List<Hl7.Fhir.Model.Code> _FormatElement;
        
        /// <summary>
        /// formats supported (xml | json | mime type)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
        [FhirElement("profile", Order=250)]
        [References("StructureDefinition")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Profile
        {
            get { if(_Profile==null) _Profile = new List<Hl7.Fhir.Model.ResourceReference>(); return _Profile; }
            set { _Profile = value; OnPropertyChanged("Profile"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Profile;
        
        /// <summary>
        /// If the endpoint is a RESTful one
        /// </summary>
        [FhirElement("rest", Order=260)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Conformance.ConformanceRestComponent> Rest
        {
            get { if(_Rest==null) _Rest = new List<Hl7.Fhir.Model.Conformance.ConformanceRestComponent>(); return _Rest; }
            set { _Rest = value; OnPropertyChanged("Rest"); }
        }
        
        private List<Hl7.Fhir.Model.Conformance.ConformanceRestComponent> _Rest;
        
        /// <summary>
        /// If messaging is supported
        /// </summary>
        [FhirElement("messaging", Order=270)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Conformance.ConformanceMessagingComponent> Messaging
        {
            get { if(_Messaging==null) _Messaging = new List<Hl7.Fhir.Model.Conformance.ConformanceMessagingComponent>(); return _Messaging; }
            set { _Messaging = value; OnPropertyChanged("Messaging"); }
        }
        
        private List<Hl7.Fhir.Model.Conformance.ConformanceMessagingComponent> _Messaging;
        
        /// <summary>
        /// Document definition
        /// </summary>
        [FhirElement("document", Order=280)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Conformance.ConformanceDocumentComponent> Document
        {
            get { if(_Document==null) _Document = new List<Hl7.Fhir.Model.Conformance.ConformanceDocumentComponent>(); return _Document; }
            set { _Document = value; OnPropertyChanged("Document"); }
        }
        
        private List<Hl7.Fhir.Model.Conformance.ConformanceDocumentComponent> _Document;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Conformance;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(PublisherElement != null) dest.PublisherElement = (Hl7.Fhir.Model.FhirString)PublisherElement.DeepCopy();
                if(Contact != null) dest.Contact = new List<Hl7.Fhir.Model.Conformance.ConformanceContactComponent>(Contact.DeepCopy());
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(RequirementsElement != null) dest.RequirementsElement = (Hl7.Fhir.Model.FhirString)RequirementsElement.DeepCopy();
                if(CopyrightElement != null) dest.CopyrightElement = (Hl7.Fhir.Model.FhirString)CopyrightElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.ConformanceResourceStatus>)StatusElement.DeepCopy();
                if(ExperimentalElement != null) dest.ExperimentalElement = (Hl7.Fhir.Model.FhirBoolean)ExperimentalElement.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(Software != null) dest.Software = (Hl7.Fhir.Model.Conformance.ConformanceSoftwareComponent)Software.DeepCopy();
                if(Implementation != null) dest.Implementation = (Hl7.Fhir.Model.Conformance.ConformanceImplementationComponent)Implementation.DeepCopy();
                if(FhirVersionElement != null) dest.FhirVersionElement = (Hl7.Fhir.Model.Id)FhirVersionElement.DeepCopy();
                if(AcceptUnknownElement != null) dest.AcceptUnknownElement = (Hl7.Fhir.Model.FhirBoolean)AcceptUnknownElement.DeepCopy();
                if(FormatElement != null) dest.FormatElement = new List<Hl7.Fhir.Model.Code>(FormatElement.DeepCopy());
                if(Profile != null) dest.Profile = new List<Hl7.Fhir.Model.ResourceReference>(Profile.DeepCopy());
                if(Rest != null) dest.Rest = new List<Hl7.Fhir.Model.Conformance.ConformanceRestComponent>(Rest.DeepCopy());
                if(Messaging != null) dest.Messaging = new List<Hl7.Fhir.Model.Conformance.ConformanceMessagingComponent>(Messaging.DeepCopy());
                if(Document != null) dest.Document = new List<Hl7.Fhir.Model.Conformance.ConformanceDocumentComponent>(Document.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Conformance());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Conformance;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(RequirementsElement, otherT.RequirementsElement)) return false;
            if( !DeepComparable.Matches(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(Software, otherT.Software)) return false;
            if( !DeepComparable.Matches(Implementation, otherT.Implementation)) return false;
            if( !DeepComparable.Matches(FhirVersionElement, otherT.FhirVersionElement)) return false;
            if( !DeepComparable.Matches(AcceptUnknownElement, otherT.AcceptUnknownElement)) return false;
            if( !DeepComparable.Matches(FormatElement, otherT.FormatElement)) return false;
            if( !DeepComparable.Matches(Profile, otherT.Profile)) return false;
            if( !DeepComparable.Matches(Rest, otherT.Rest)) return false;
            if( !DeepComparable.Matches(Messaging, otherT.Messaging)) return false;
            if( !DeepComparable.Matches(Document, otherT.Document)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Conformance;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(RequirementsElement, otherT.RequirementsElement)) return false;
            if( !DeepComparable.IsExactly(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(Software, otherT.Software)) return false;
            if( !DeepComparable.IsExactly(Implementation, otherT.Implementation)) return false;
            if( !DeepComparable.IsExactly(FhirVersionElement, otherT.FhirVersionElement)) return false;
            if( !DeepComparable.IsExactly(AcceptUnknownElement, otherT.AcceptUnknownElement)) return false;
            if( !DeepComparable.IsExactly(FormatElement, otherT.FormatElement)) return false;
            if( !DeepComparable.IsExactly(Profile, otherT.Profile)) return false;
            if( !DeepComparable.IsExactly(Rest, otherT.Rest)) return false;
            if( !DeepComparable.IsExactly(Messaging, otherT.Messaging)) return false;
            if( !DeepComparable.IsExactly(Document, otherT.Document)) return false;
            
            return true;
        }
        
    }
    
}
