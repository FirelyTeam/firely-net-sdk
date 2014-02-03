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
    /// A resource that describes a message that is exchanged between systems
    /// </summary>
    [FhirType("MessageHeader", IsResource=true)]
    [DataContract]
    public partial class MessageHeader : Hl7.Fhir.Model.Resource
    {
        /// <summary>
        /// The kind of response to a message
        /// </summary>
        [FhirEnumeration("ResponseType")]
        public enum ResponseType
        {
            [EnumLiteral("ok")]
            Ok, // The message was accepted and processed without error.
            [EnumLiteral("transient-error")]
            TransientError, // Some internal unexpected error occurred - wait and try again. Note - this is usually used for things like database unavailable, which may be expected to resolve, though human intervention may be required.
            [EnumLiteral("fatal-error")]
            FatalError, // The message was rejected because of some content in it. There is no point in re-sending without change. The response narrative SHALL describe what the issue is.
        }
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("MessageDestinationComponent")]
        [DataContract]
        public partial class MessageDestinationComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Name of system
            /// </summary>
            [FhirElement("name", Order=40)]
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
            /// Particular delivery destination within the destination
            /// </summary>
            [FhirElement("target", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Target { get; set; }
            
            /// <summary>
            /// Actual destination address or id
            /// </summary>
            [FhirElement("endpoint", Order=60)]
            [Cardinality(Min=1,Max=1)]
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
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("MessageSourceComponent")]
        [DataContract]
        public partial class MessageSourceComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Name of system
            /// </summary>
            [FhirElement("name", Order=40)]
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
            /// Name of software running the system
            /// </summary>
            [FhirElement("software", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString SoftwareElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Version of software running
            /// </summary>
            [FhirElement("version", Order=60)]
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
            /// Human contact for problems
            /// </summary>
            [FhirElement("contact", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Contact Contact { get; set; }
            
            /// <summary>
            /// Actual message source address or id
            /// </summary>
            [FhirElement("endpoint", Order=80)]
            [Cardinality(Min=1,Max=1)]
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
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("MessageHeaderResponseComponent")]
        [DataContract]
        public partial class MessageHeaderResponseComponent : Hl7.Fhir.Model.Element
        {
            /// <summary>
            /// Id of original message
            /// </summary>
            [FhirElement("identifier", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Id IdentifierElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// ok | transient-error | fatal-error
            /// </summary>
            [FhirElement("code", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.MessageHeader.ResponseType> CodeElement { get; set; }
            
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
                }
            }
            
            /// <summary>
            /// Specific list of hints/warnings/errors
            /// </summary>
            [FhirElement("details", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Details { get; set; }
            
        }
        
        
        /// <summary>
        /// Id of this message
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Id IdentifierElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Time that the message was sent
        /// </summary>
        [FhirElement("timestamp", Order=80)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Instant TimestampElement { get; set; }
        
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
            }
        }
        
        /// <summary>
        /// Code for the event this message represents
        /// </summary>
        [FhirElement("event", Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Coding Event { get; set; }
        
        /// <summary>
        /// If this is a reply to prior message
        /// </summary>
        [FhirElement("response", Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.MessageHeader.MessageHeaderResponseComponent Response { get; set; }
        
        /// <summary>
        /// Message Source Application
        /// </summary>
        [FhirElement("source", Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.MessageHeader.MessageSourceComponent Source { get; set; }
        
        /// <summary>
        /// Message Destination Application(s)
        /// </summary>
        [FhirElement("destination", Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.MessageHeader.MessageDestinationComponent> Destination { get; set; }
        
        /// <summary>
        /// The source of the data entry
        /// </summary>
        [FhirElement("enterer", Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Enterer { get; set; }
        
        /// <summary>
        /// The source of the decision
        /// </summary>
        [FhirElement("author", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Author { get; set; }
        
        /// <summary>
        /// Intended "real-world" recipient for the data
        /// </summary>
        [FhirElement("receiver", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Receiver { get; set; }
        
        /// <summary>
        /// Final responsibility for event
        /// </summary>
        [FhirElement("responsible", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Responsible { get; set; }
        
        /// <summary>
        /// Cause of event
        /// </summary>
        [FhirElement("reason", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Reason { get; set; }
        
        /// <summary>
        /// The actual content of the message
        /// </summary>
        [FhirElement("data", Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Data { get; set; }
        
    }
    
}
