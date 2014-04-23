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
// Generated on Thu, Apr 17, 2014 11:39+0200 for FHIR v0.80
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Event record kept for security purposes
    /// </summary>
    [FhirType("SecurityEvent", IsResource=true)]
    [DataContract]
    public partial class SecurityEvent : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Code representing the functional application role of Participant Object being audited
        /// </summary>
        [FhirEnumeration("SecurityEventObjectRole")]
        public enum SecurityEventObjectRole
        {
            [EnumLiteral("1")]
            N1, // This object is the patient that is the subject of care related to this event.  It is identifiable by patient ID or equivalent.  The patient may be either human or animal.
            [EnumLiteral("2")]
            N2, // This is a location identified as related to the event.  This is usually the location where the event took place.  Note that for shipping, the usual events are arrival at a location or departure from a location.
            [EnumLiteral("3")]
            N3, // This object is any kind of persistent document created as a result of the event.  This could be a paper report, film, electronic report, DICOM Study, etc.  Issues related to medical records life cycle management are conveyed elsewhere.
            [EnumLiteral("4")]
            N4, // A logical object related to the event.  (Deprecated).
            [EnumLiteral("5")]
            N5, // This is any configurable file used to control creation of documents.  Examples include the objects maintained by the HL7 Master File transactions, Value Sets, etc.
            [EnumLiteral("6")]
            N6, // A human participant not otherwise identified by some other category.
            [EnumLiteral("7")]
            N7, // (deprecated).
            [EnumLiteral("8")]
            N8, // Typically a licensed person who is providing or performing care related to the event, generally a physician.   The key distinction between doctor and practitioner is with regards to their role, not the licensing.  The doctor is the human who actually performed the work.  The practitioner is the human or organization that is responsible for the work.
            [EnumLiteral("9")]
            N9, // A person or system that is being notified as part of the event.  This is relevant in situations where automated systems provide notifications to other parties when an event took place.
            [EnumLiteral("10")]
            N10, // Insurance company, or any other organization who accepts responsibility for paying for the healthcare event.
            [EnumLiteral("11")]
            N11, // A person or active system object involved in the event with a security role.
            [EnumLiteral("12")]
            N12, // A person or system object involved in the event with the authority to modify security roles of other objects.
            [EnumLiteral("13")]
            N13, // A passive object, such as a role table, that is relevant to the event.
            [EnumLiteral("14")]
            N14, // (deprecated)  Relevant to certain RBAC security methodologies.
            [EnumLiteral("15")]
            N15, // Any person or organization responsible for providing care.  This encompasses all forms of care, licensed or otherwise, and all sorts of teams and care groups. Note, the distinction between practitioners and the doctor that actually provided the care to the patient.
            [EnumLiteral("16")]
            N16, // The source or destination for data transfer, when it does not match some other role.
            [EnumLiteral("17")]
            N17, // A source or destination for data transfer, that acts as an archive, database, or similar role.
            [EnumLiteral("18")]
            N18, // An object that holds schedule information.  This could be an appointment book, availability information, etc.
            [EnumLiteral("19")]
            N19, // An organization or person that is the recipient of services.  This could be an organization that is buying services for a patient, or a person that is buying services for an animal.
            [EnumLiteral("20")]
            N20, // An order, task, work item, procedure step, or other description of work to be performed.  E.g., a particular instance of an MPPS.
            [EnumLiteral("21")]
            N21, // A list of jobs or a system that provides lists of jobs.  E.g., an MWL SCP.
            [EnumLiteral("22")]
            N22, // (Deprecated).
            [EnumLiteral("23")]
            N23, // An object that specifies or controls the routing or delivery of items.  For example, a distribution list is the routing criteria for mail.  The items delivered may be documents, jobs, or other objects.
            [EnumLiteral("24")]
            N24, // The contents of a query.  This is used to capture the contents of any kind of query.  For security surveillance purposes knowing the queries being made is very important.
        }
        
        /// <summary>
        /// Indicator for type of action performed during the event that generated the audit.
        /// </summary>
        [FhirEnumeration("SecurityEventAction")]
        public enum SecurityEventAction
        {
            [EnumLiteral("C")]
            C, // Create a new database object, such as Placing an Order.
            [EnumLiteral("R")]
            R, // Display or print data, such as a Doctor Census.
            [EnumLiteral("U")]
            U, // Update data, such as Revise Patient Information.
            [EnumLiteral("D")]
            D, // Delete items, such as a doctor master file record.
            [EnumLiteral("E")]
            E, // Perform a system or application function such as log-on, program execution or use of an object's method, or perform a query/search operation.
        }
        
        /// <summary>
        /// Code for the participant object type being audited
        /// </summary>
        [FhirEnumeration("SecurityEventObjectType")]
        public enum SecurityEventObjectType
        {
            [EnumLiteral("1")]
            N1, // Person.
            [EnumLiteral("2")]
            N2, // System Object.
            [EnumLiteral("3")]
            N3, // Organization.
            [EnumLiteral("4")]
            N4, // Other.
        }
        
        /// <summary>
        /// Identifier for the data life-cycle stage for the participant object
        /// </summary>
        [FhirEnumeration("SecurityEventObjectLifecycle")]
        public enum SecurityEventObjectLifecycle
        {
            [EnumLiteral("1")]
            N1, // Origination / Creation.
            [EnumLiteral("2")]
            N2, // Import / Copy from original.
            [EnumLiteral("3")]
            N3, // Amendment.
            [EnumLiteral("4")]
            N4, // Verification.
            [EnumLiteral("5")]
            N5, // Translation.
            [EnumLiteral("6")]
            N6, // Access / Use.
            [EnumLiteral("7")]
            N7, // De-identification.
            [EnumLiteral("8")]
            N8, // Aggregation, summarization, derivation.
            [EnumLiteral("9")]
            N9, // Report.
            [EnumLiteral("10")]
            N10, // Export / Copy to target.
            [EnumLiteral("11")]
            N11, // Disclosure.
            [EnumLiteral("12")]
            N12, // Receipt of disclosure.
            [EnumLiteral("13")]
            N13, // Archiving.
            [EnumLiteral("14")]
            N14, // Logical deletion.
            [EnumLiteral("15")]
            N15, // Permanent erasure / Physical destruction.
        }
        
        /// <summary>
        /// The type of network access point that originated the audit event
        /// </summary>
        [FhirEnumeration("SecurityEventParticipantNetworkType")]
        public enum SecurityEventParticipantNetworkType
        {
            [EnumLiteral("1")]
            N1, // Machine Name, including DNS name.
            [EnumLiteral("2")]
            N2, // IP Address.
            [EnumLiteral("3")]
            N3, // Telephone Number.
            [EnumLiteral("4")]
            N4, // Email address.
            [EnumLiteral("5")]
            N5, // URI (User directory, HTTP-PUT, ftp, etc.).
        }
        
        /// <summary>
        /// Indicates whether the event succeeded or failed
        /// </summary>
        [FhirEnumeration("SecurityEventOutcome")]
        public enum SecurityEventOutcome
        {
            [EnumLiteral("0")]
            N0, // The operation completed successfully (whether with warnings or not).
            [EnumLiteral("4")]
            N4, // The action was not successful due to some kind of catered for error (often equivalent to an HTTP 400 response).
            [EnumLiteral("8")]
            N8, // The action was not successful due to some kind of unexpected error (often equivalent to an HTTP 500 response).
            [EnumLiteral("12")]
            N12, // An error of such magnitude occurred that the system is not longer available for use (i.e. the system died).
        }
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("SecurityEventObjectDetailComponent")]
        [DataContract]
        public partial class SecurityEventObjectDetailComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Name of the property
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            private Hl7.Fhir.Model.FhirString _TypeElement;
            
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
                      TypeElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Property value
            /// </summary>
            [FhirElement("value", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Base64Binary ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            private Hl7.Fhir.Model.Base64Binary _ValueElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public byte[] Value
            {
                get { return ValueElement != null ? ValueElement.Value : null; }
                set
                {
                    if(value == null)
                      ValueElement = null; 
                    else
                      ValueElement = new Hl7.Fhir.Model.Base64Binary(value);
                    OnPropertyChanged("Value");
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("SecurityEventObjectComponent")]
        [DataContract]
        public partial class SecurityEventObjectComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Specific instance of object (e.g. versioned)
            /// </summary>
            [FhirElement("identifier", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// Specific instance of resource (e.g. versioned)
            /// </summary>
            [FhirElement("reference", InSummary=true, Order=50)]
            [References()]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Reference
            {
                get { return _Reference; }
                set { _Reference = value; OnPropertyChanged("Reference"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Reference;
            
            /// <summary>
            /// Object type being audited
            /// </summary>
            [FhirElement("type", InSummary=true, Order=60)]
            [DataMember]
            public Code<Hl7.Fhir.Model.SecurityEvent.SecurityEventObjectType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            private Code<Hl7.Fhir.Model.SecurityEvent.SecurityEventObjectType> _TypeElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.SecurityEvent.SecurityEventObjectType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if(value == null)
                      TypeElement = null; 
                    else
                      TypeElement = new Code<Hl7.Fhir.Model.SecurityEvent.SecurityEventObjectType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Functional application role of Object
            /// </summary>
            [FhirElement("role", InSummary=true, Order=70)]
            [DataMember]
            public Code<Hl7.Fhir.Model.SecurityEvent.SecurityEventObjectRole> RoleElement
            {
                get { return _RoleElement; }
                set { _RoleElement = value; OnPropertyChanged("RoleElement"); }
            }
            private Code<Hl7.Fhir.Model.SecurityEvent.SecurityEventObjectRole> _RoleElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.SecurityEvent.SecurityEventObjectRole? Role
            {
                get { return RoleElement != null ? RoleElement.Value : null; }
                set
                {
                    if(value == null)
                      RoleElement = null; 
                    else
                      RoleElement = new Code<Hl7.Fhir.Model.SecurityEvent.SecurityEventObjectRole>(value);
                    OnPropertyChanged("Role");
                }
            }
            
            /// <summary>
            /// Life-cycle stage for the object
            /// </summary>
            [FhirElement("lifecycle", InSummary=true, Order=80)]
            [DataMember]
            public Code<Hl7.Fhir.Model.SecurityEvent.SecurityEventObjectLifecycle> LifecycleElement
            {
                get { return _LifecycleElement; }
                set { _LifecycleElement = value; OnPropertyChanged("LifecycleElement"); }
            }
            private Code<Hl7.Fhir.Model.SecurityEvent.SecurityEventObjectLifecycle> _LifecycleElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.SecurityEvent.SecurityEventObjectLifecycle? Lifecycle
            {
                get { return LifecycleElement != null ? LifecycleElement.Value : null; }
                set
                {
                    if(value == null)
                      LifecycleElement = null; 
                    else
                      LifecycleElement = new Code<Hl7.Fhir.Model.SecurityEvent.SecurityEventObjectLifecycle>(value);
                    OnPropertyChanged("Lifecycle");
                }
            }
            
            /// <summary>
            /// Policy-defined sensitivity for the object
            /// </summary>
            [FhirElement("sensitivity", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Sensitivity
            {
                get { return _Sensitivity; }
                set { _Sensitivity = value; OnPropertyChanged("Sensitivity"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _Sensitivity;
            
            /// <summary>
            /// Instance-specific descriptor for Object
            /// </summary>
            [FhirElement("name", InSummary=true, Order=100)]
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
            /// Descriptive text
            /// </summary>
            [FhirElement("description", InSummary=true, Order=110)]
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
            /// Actual query for object
            /// </summary>
            [FhirElement("query", InSummary=true, Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.Base64Binary QueryElement
            {
                get { return _QueryElement; }
                set { _QueryElement = value; OnPropertyChanged("QueryElement"); }
            }
            private Hl7.Fhir.Model.Base64Binary _QueryElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public byte[] Query
            {
                get { return QueryElement != null ? QueryElement.Value : null; }
                set
                {
                    if(value == null)
                      QueryElement = null; 
                    else
                      QueryElement = new Hl7.Fhir.Model.Base64Binary(value);
                    OnPropertyChanged("Query");
                }
            }
            
            /// <summary>
            /// Additional Information about the Object
            /// </summary>
            [FhirElement("detail", InSummary=true, Order=130)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.SecurityEvent.SecurityEventObjectDetailComponent> Detail
            {
                get { return _Detail; }
                set { _Detail = value; OnPropertyChanged("Detail"); }
            }
            private List<Hl7.Fhir.Model.SecurityEvent.SecurityEventObjectDetailComponent> _Detail;
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("SecurityEventSourceComponent")]
        [DataContract]
        public partial class SecurityEventSourceComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Logical source location within the enterprise
            /// </summary>
            [FhirElement("site", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString SiteElement
            {
                get { return _SiteElement; }
                set { _SiteElement = value; OnPropertyChanged("SiteElement"); }
            }
            private Hl7.Fhir.Model.FhirString _SiteElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Site
            {
                get { return SiteElement != null ? SiteElement.Value : null; }
                set
                {
                    if(value == null)
                      SiteElement = null; 
                    else
                      SiteElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Site");
                }
            }
            
            /// <summary>
            /// The id of source where event originated
            /// </summary>
            [FhirElement("identifier", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
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
            /// The type of source where event originated
            /// </summary>
            [FhirElement("type", InSummary=true, Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            private List<Hl7.Fhir.Model.Coding> _Type;
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("SecurityEventEventComponent")]
        [DataContract]
        public partial class SecurityEventEventComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Type/identifier of event
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
            /// More specific type/id for the event
            /// </summary>
            [FhirElement("subtype", InSummary=true, Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Subtype
            {
                get { return _Subtype; }
                set { _Subtype = value; OnPropertyChanged("Subtype"); }
            }
            private List<Hl7.Fhir.Model.CodeableConcept> _Subtype;
            
            /// <summary>
            /// Type of action performed during the event
            /// </summary>
            [FhirElement("action", InSummary=true, Order=60)]
            [DataMember]
            public Code<Hl7.Fhir.Model.SecurityEvent.SecurityEventAction> ActionElement
            {
                get { return _ActionElement; }
                set { _ActionElement = value; OnPropertyChanged("ActionElement"); }
            }
            private Code<Hl7.Fhir.Model.SecurityEvent.SecurityEventAction> _ActionElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.SecurityEvent.SecurityEventAction? Action
            {
                get { return ActionElement != null ? ActionElement.Value : null; }
                set
                {
                    if(value == null)
                      ActionElement = null; 
                    else
                      ActionElement = new Code<Hl7.Fhir.Model.SecurityEvent.SecurityEventAction>(value);
                    OnPropertyChanged("Action");
                }
            }
            
            /// <summary>
            /// Time when the event occurred on source
            /// </summary>
            [FhirElement("dateTime", InSummary=true, Order=70)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Instant DateTimeElement
            {
                get { return _DateTimeElement; }
                set { _DateTimeElement = value; OnPropertyChanged("DateTimeElement"); }
            }
            private Hl7.Fhir.Model.Instant _DateTimeElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public DateTimeOffset? DateTime
            {
                get { return DateTimeElement != null ? DateTimeElement.Value : null; }
                set
                {
                    if(value == null)
                      DateTimeElement = null; 
                    else
                      DateTimeElement = new Hl7.Fhir.Model.Instant(value);
                    OnPropertyChanged("DateTime");
                }
            }
            
            /// <summary>
            /// Whether the event succeeded or failed
            /// </summary>
            [FhirElement("outcome", InSummary=true, Order=80)]
            [DataMember]
            public Code<Hl7.Fhir.Model.SecurityEvent.SecurityEventOutcome> OutcomeElement
            {
                get { return _OutcomeElement; }
                set { _OutcomeElement = value; OnPropertyChanged("OutcomeElement"); }
            }
            private Code<Hl7.Fhir.Model.SecurityEvent.SecurityEventOutcome> _OutcomeElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.SecurityEvent.SecurityEventOutcome? Outcome
            {
                get { return OutcomeElement != null ? OutcomeElement.Value : null; }
                set
                {
                    if(value == null)
                      OutcomeElement = null; 
                    else
                      OutcomeElement = new Code<Hl7.Fhir.Model.SecurityEvent.SecurityEventOutcome>(value);
                    OnPropertyChanged("Outcome");
                }
            }
            
            /// <summary>
            /// Description of the event outcome
            /// </summary>
            [FhirElement("outcomeDesc", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString OutcomeDescElement
            {
                get { return _OutcomeDescElement; }
                set { _OutcomeDescElement = value; OnPropertyChanged("OutcomeDescElement"); }
            }
            private Hl7.Fhir.Model.FhirString _OutcomeDescElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string OutcomeDesc
            {
                get { return OutcomeDescElement != null ? OutcomeDescElement.Value : null; }
                set
                {
                    if(value == null)
                      OutcomeDescElement = null; 
                    else
                      OutcomeDescElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("OutcomeDesc");
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("SecurityEventParticipantNetworkComponent")]
        [DataContract]
        public partial class SecurityEventParticipantNetworkComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Identifier for the network access point of the user device
            /// </summary>
            [FhirElement("identifier", InSummary=true, Order=40)]
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
            /// The type of network access point
            /// </summary>
            [FhirElement("type", InSummary=true, Order=50)]
            [DataMember]
            public Code<Hl7.Fhir.Model.SecurityEvent.SecurityEventParticipantNetworkType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            private Code<Hl7.Fhir.Model.SecurityEvent.SecurityEventParticipantNetworkType> _TypeElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.SecurityEvent.SecurityEventParticipantNetworkType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if(value == null)
                      TypeElement = null; 
                    else
                      TypeElement = new Code<Hl7.Fhir.Model.SecurityEvent.SecurityEventParticipantNetworkType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
        }
        
        
        /// <summary>
        /// null
        /// </summary>
        [FhirType("SecurityEventParticipantComponent")]
        [DataContract]
        public partial class SecurityEventParticipantComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// User roles (e.g. local RBAC codes)
            /// </summary>
            [FhirElement("role", InSummary=true, Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Role
            {
                get { return _Role; }
                set { _Role = value; OnPropertyChanged("Role"); }
            }
            private List<Hl7.Fhir.Model.CodeableConcept> _Role;
            
            /// <summary>
            /// Direct reference to resource
            /// </summary>
            [FhirElement("reference", InSummary=true, Order=50)]
            [References("Practitioner","Patient","Device")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Reference
            {
                get { return _Reference; }
                set { _Reference = value; OnPropertyChanged("Reference"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Reference;
            
            /// <summary>
            /// Unique identifier for the user
            /// </summary>
            [FhirElement("userId", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString UserIdElement
            {
                get { return _UserIdElement; }
                set { _UserIdElement = value; OnPropertyChanged("UserIdElement"); }
            }
            private Hl7.Fhir.Model.FhirString _UserIdElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string UserId
            {
                get { return UserIdElement != null ? UserIdElement.Value : null; }
                set
                {
                    if(value == null)
                      UserIdElement = null; 
                    else
                      UserIdElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("UserId");
                }
            }
            
            /// <summary>
            /// Alternative User id e.g. authentication
            /// </summary>
            [FhirElement("altId", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString AltIdElement
            {
                get { return _AltIdElement; }
                set { _AltIdElement = value; OnPropertyChanged("AltIdElement"); }
            }
            private Hl7.Fhir.Model.FhirString _AltIdElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string AltId
            {
                get { return AltIdElement != null ? AltIdElement.Value : null; }
                set
                {
                    if(value == null)
                      AltIdElement = null; 
                    else
                      AltIdElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("AltId");
                }
            }
            
            /// <summary>
            /// Human-meaningful name for the user
            /// </summary>
            [FhirElement("name", InSummary=true, Order=80)]
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
            /// Whether user is initiator
            /// </summary>
            [FhirElement("requestor", InSummary=true, Order=90)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean RequestorElement
            {
                get { return _RequestorElement; }
                set { _RequestorElement = value; OnPropertyChanged("RequestorElement"); }
            }
            private Hl7.Fhir.Model.FhirBoolean _RequestorElement;
            
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Requestor
            {
                get { return RequestorElement != null ? RequestorElement.Value : null; }
                set
                {
                    if(value == null)
                      RequestorElement = null; 
                    else
                      RequestorElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Requestor");
                }
            }
            
            /// <summary>
            /// Type of media
            /// </summary>
            [FhirElement("media", InSummary=true, Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Media
            {
                get { return _Media; }
                set { _Media = value; OnPropertyChanged("Media"); }
            }
            private Hl7.Fhir.Model.Coding _Media;
            
            /// <summary>
            /// Logical network location for application activity
            /// </summary>
            [FhirElement("network", InSummary=true, Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.SecurityEvent.SecurityEventParticipantNetworkComponent Network
            {
                get { return _Network; }
                set { _Network = value; OnPropertyChanged("Network"); }
            }
            private Hl7.Fhir.Model.SecurityEvent.SecurityEventParticipantNetworkComponent _Network;
            
        }
        
        
        /// <summary>
        /// What was done
        /// </summary>
        [FhirElement("event", Order=70)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.SecurityEvent.SecurityEventEventComponent Event
        {
            get { return _Event; }
            set { _Event = value; OnPropertyChanged("Event"); }
        }
        private Hl7.Fhir.Model.SecurityEvent.SecurityEventEventComponent _Event;
        
        /// <summary>
        /// A person, a hardware device or software process
        /// </summary>
        [FhirElement("participant", Order=80)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.SecurityEvent.SecurityEventParticipantComponent> Participant
        {
            get { return _Participant; }
            set { _Participant = value; OnPropertyChanged("Participant"); }
        }
        private List<Hl7.Fhir.Model.SecurityEvent.SecurityEventParticipantComponent> _Participant;
        
        /// <summary>
        /// Application systems and processes
        /// </summary>
        [FhirElement("source", Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.SecurityEvent.SecurityEventSourceComponent Source
        {
            get { return _Source; }
            set { _Source = value; OnPropertyChanged("Source"); }
        }
        private Hl7.Fhir.Model.SecurityEvent.SecurityEventSourceComponent _Source;
        
        /// <summary>
        /// Specific instances of data or objects that have been accessed
        /// </summary>
        [FhirElement("object", Order=100)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.SecurityEvent.SecurityEventObjectComponent> Object
        {
            get { return _Object; }
            set { _Object = value; OnPropertyChanged("Object"); }
        }
        private List<Hl7.Fhir.Model.SecurityEvent.SecurityEventObjectComponent> _Object;
        
    }
    
}
