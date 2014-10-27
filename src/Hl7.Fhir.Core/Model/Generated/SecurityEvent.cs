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
// Generated on Thu, Oct 23, 2014 14:22+0200 for FHIR v0.0.82
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
            /// <summary>
            /// This object is the patient that is the subject of care related to this event.  It is identifiable by patient ID or equivalent.  The patient may be either human or animal.
            /// </summary>
            [EnumLiteral("1")]
            N1,
            /// <summary>
            /// This is a location identified as related to the event.  This is usually the location where the event took place.  Note that for shipping, the usual events are arrival at a location or departure from a location.
            /// </summary>
            [EnumLiteral("2")]
            N2,
            /// <summary>
            /// This object is any kind of persistent document created as a result of the event.  This could be a paper report, film, electronic report, DICOM Study, etc.  Issues related to medical records life cycle management are conveyed elsewhere.
            /// </summary>
            [EnumLiteral("3")]
            N3,
            /// <summary>
            /// A logical object related to the event.  (Deprecated).
            /// </summary>
            [EnumLiteral("4")]
            N4,
            /// <summary>
            /// This is any configurable file used to control creation of documents.  Examples include the objects maintained by the HL7 Master File transactions, Value Sets, etc.
            /// </summary>
            [EnumLiteral("5")]
            N5,
            /// <summary>
            /// A human participant not otherwise identified by some other category.
            /// </summary>
            [EnumLiteral("6")]
            N6,
            /// <summary>
            /// (deprecated).
            /// </summary>
            [EnumLiteral("7")]
            N7,
            /// <summary>
            /// Typically a licensed person who is providing or performing care related to the event, generally a physician.   The key distinction between doctor and practitioner is with regards to their role, not the licensing.  The doctor is the human who actually performed the work.  The practitioner is the human or organization that is responsible for the work.
            /// </summary>
            [EnumLiteral("8")]
            N8,
            /// <summary>
            /// A person or system that is being notified as part of the event.  This is relevant in situations where automated systems provide notifications to other parties when an event took place.
            /// </summary>
            [EnumLiteral("9")]
            N9,
            /// <summary>
            /// Insurance company, or any other organization who accepts responsibility for paying for the healthcare event.
            /// </summary>
            [EnumLiteral("10")]
            N10,
            /// <summary>
            /// A person or active system object involved in the event with a security role.
            /// </summary>
            [EnumLiteral("11")]
            N11,
            /// <summary>
            /// A person or system object involved in the event with the authority to modify security roles of other objects.
            /// </summary>
            [EnumLiteral("12")]
            N12,
            /// <summary>
            /// A passive object, such as a role table, that is relevant to the event.
            /// </summary>
            [EnumLiteral("13")]
            N13,
            /// <summary>
            /// (deprecated)  Relevant to certain RBAC security methodologies.
            /// </summary>
            [EnumLiteral("14")]
            N14,
            /// <summary>
            /// Any person or organization responsible for providing care.  This encompasses all forms of care, licensed or otherwise, and all sorts of teams and care groups. Note, the distinction between practitioners and the doctor that actually provided the care to the patient.
            /// </summary>
            [EnumLiteral("15")]
            N15,
            /// <summary>
            /// The source or destination for data transfer, when it does not match some other role.
            /// </summary>
            [EnumLiteral("16")]
            N16,
            /// <summary>
            /// A source or destination for data transfer, that acts as an archive, database, or similar role.
            /// </summary>
            [EnumLiteral("17")]
            N17,
            /// <summary>
            /// An object that holds schedule information.  This could be an appointment book, availability information, etc.
            /// </summary>
            [EnumLiteral("18")]
            N18,
            /// <summary>
            /// An organization or person that is the recipient of services.  This could be an organization that is buying services for a patient, or a person that is buying services for an animal.
            /// </summary>
            [EnumLiteral("19")]
            N19,
            /// <summary>
            /// An order, task, work item, procedure step, or other description of work to be performed.  E.g., a particular instance of an MPPS.
            /// </summary>
            [EnumLiteral("20")]
            N20,
            /// <summary>
            /// A list of jobs or a system that provides lists of jobs.  E.g., an MWL SCP.
            /// </summary>
            [EnumLiteral("21")]
            N21,
            /// <summary>
            /// (Deprecated).
            /// </summary>
            [EnumLiteral("22")]
            N22,
            /// <summary>
            /// An object that specifies or controls the routing or delivery of items.  For example, a distribution list is the routing criteria for mail.  The items delivered may be documents, jobs, or other objects.
            /// </summary>
            [EnumLiteral("23")]
            N23,
            /// <summary>
            /// The contents of a query.  This is used to capture the contents of any kind of query.  For security surveillance purposes knowing the queries being made is very important.
            /// </summary>
            [EnumLiteral("24")]
            N24,
        }
        
        /// <summary>
        /// Indicator for type of action performed during the event that generated the audit.
        /// </summary>
        [FhirEnumeration("SecurityEventAction")]
        public enum SecurityEventAction
        {
            /// <summary>
            /// Create a new database object, such as Placing an Order.
            /// </summary>
            [EnumLiteral("C")]
            C,
            /// <summary>
            /// Display or print data, such as a Doctor Census.
            /// </summary>
            [EnumLiteral("R")]
            R,
            /// <summary>
            /// Update data, such as Revise Patient Information.
            /// </summary>
            [EnumLiteral("U")]
            U,
            /// <summary>
            /// Delete items, such as a doctor master file record.
            /// </summary>
            [EnumLiteral("D")]
            D,
            /// <summary>
            /// Perform a system or application function such as log-on, program execution or use of an object's method, or perform a query/search operation.
            /// </summary>
            [EnumLiteral("E")]
            E,
        }
        
        /// <summary>
        /// Code for the participant object type being audited
        /// </summary>
        [FhirEnumeration("SecurityEventObjectType")]
        public enum SecurityEventObjectType
        {
            /// <summary>
            /// Person.
            /// </summary>
            [EnumLiteral("1")]
            N1,
            /// <summary>
            /// System Object.
            /// </summary>
            [EnumLiteral("2")]
            N2,
            /// <summary>
            /// Organization.
            /// </summary>
            [EnumLiteral("3")]
            N3,
            /// <summary>
            /// Other.
            /// </summary>
            [EnumLiteral("4")]
            N4,
        }
        
        /// <summary>
        /// Identifier for the data life-cycle stage for the participant object
        /// </summary>
        [FhirEnumeration("SecurityEventObjectLifecycle")]
        public enum SecurityEventObjectLifecycle
        {
            /// <summary>
            /// Origination / Creation.
            /// </summary>
            [EnumLiteral("1")]
            N1,
            /// <summary>
            /// Import / Copy from original.
            /// </summary>
            [EnumLiteral("2")]
            N2,
            /// <summary>
            /// Amendment.
            /// </summary>
            [EnumLiteral("3")]
            N3,
            /// <summary>
            /// Verification.
            /// </summary>
            [EnumLiteral("4")]
            N4,
            /// <summary>
            /// Translation.
            /// </summary>
            [EnumLiteral("5")]
            N5,
            /// <summary>
            /// Access / Use.
            /// </summary>
            [EnumLiteral("6")]
            N6,
            /// <summary>
            /// De-identification.
            /// </summary>
            [EnumLiteral("7")]
            N7,
            /// <summary>
            /// Aggregation, summarization, derivation.
            /// </summary>
            [EnumLiteral("8")]
            N8,
            /// <summary>
            /// Report.
            /// </summary>
            [EnumLiteral("9")]
            N9,
            /// <summary>
            /// Export / Copy to target.
            /// </summary>
            [EnumLiteral("10")]
            N10,
            /// <summary>
            /// Disclosure.
            /// </summary>
            [EnumLiteral("11")]
            N11,
            /// <summary>
            /// Receipt of disclosure.
            /// </summary>
            [EnumLiteral("12")]
            N12,
            /// <summary>
            /// Archiving.
            /// </summary>
            [EnumLiteral("13")]
            N13,
            /// <summary>
            /// Logical deletion.
            /// </summary>
            [EnumLiteral("14")]
            N14,
            /// <summary>
            /// Permanent erasure / Physical destruction.
            /// </summary>
            [EnumLiteral("15")]
            N15,
        }
        
        /// <summary>
        /// The type of network access point that originated the audit event
        /// </summary>
        [FhirEnumeration("SecurityEventParticipantNetworkType")]
        public enum SecurityEventParticipantNetworkType
        {
            /// <summary>
            /// Machine Name, including DNS name.
            /// </summary>
            [EnumLiteral("1")]
            N1,
            /// <summary>
            /// IP Address.
            /// </summary>
            [EnumLiteral("2")]
            N2,
            /// <summary>
            /// Telephone Number.
            /// </summary>
            [EnumLiteral("3")]
            N3,
            /// <summary>
            /// Email address.
            /// </summary>
            [EnumLiteral("4")]
            N4,
            /// <summary>
            /// URI (User directory, HTTP-PUT, ftp, etc.).
            /// </summary>
            [EnumLiteral("5")]
            N5,
        }
        
        /// <summary>
        /// Indicates whether the event succeeded or failed
        /// </summary>
        [FhirEnumeration("SecurityEventOutcome")]
        public enum SecurityEventOutcome
        {
            /// <summary>
            /// The operation completed successfully (whether with warnings or not).
            /// </summary>
            [EnumLiteral("0")]
            N0,
            /// <summary>
            /// The action was not successful due to some kind of catered for error (often equivalent to an HTTP 400 response).
            /// </summary>
            [EnumLiteral("4")]
            N4,
            /// <summary>
            /// The action was not successful due to some kind of unexpected error (often equivalent to an HTTP 500 response).
            /// </summary>
            [EnumLiteral("8")]
            N8,
            /// <summary>
            /// An error of such magnitude occurred that the system is not longer available for use (i.e. the system died).
            /// </summary>
            [EnumLiteral("12")]
            N12,
        }
        
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
            
            /// <summary>
            /// Name of the property
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
            
            /// <summary>
            /// Property value
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SecurityEventObjectDetailComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Hl7.Fhir.Model.FhirString)TypeElement.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.Base64Binary)ValueElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SecurityEventObjectDetailComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SecurityEventObjectDetailComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SecurityEventObjectDetailComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
                
                return true;
            }
            
        }
        
        
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
            
            /// <summary>
            /// Object type being audited
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Functional application role of Object
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Life-cycle stage for the object
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Instance-specific descriptor for Object
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
            
            /// <summary>
            /// Descriptive text
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
            
            /// <summary>
            /// Actual query for object
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SecurityEventObjectComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(Reference != null) dest.Reference = (Hl7.Fhir.Model.ResourceReference)Reference.DeepCopy();
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.SecurityEvent.SecurityEventObjectType>)TypeElement.DeepCopy();
                    if(RoleElement != null) dest.RoleElement = (Code<Hl7.Fhir.Model.SecurityEvent.SecurityEventObjectRole>)RoleElement.DeepCopy();
                    if(LifecycleElement != null) dest.LifecycleElement = (Code<Hl7.Fhir.Model.SecurityEvent.SecurityEventObjectLifecycle>)LifecycleElement.DeepCopy();
                    if(Sensitivity != null) dest.Sensitivity = (Hl7.Fhir.Model.CodeableConcept)Sensitivity.DeepCopy();
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(QueryElement != null) dest.QueryElement = (Hl7.Fhir.Model.Base64Binary)QueryElement.DeepCopy();
                    if(Detail != null) dest.Detail = new List<Hl7.Fhir.Model.SecurityEvent.SecurityEventObjectDetailComponent>(Detail.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SecurityEventObjectComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SecurityEventObjectComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(Reference, otherT.Reference)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(RoleElement, otherT.RoleElement)) return false;
                if( !DeepComparable.Matches(LifecycleElement, otherT.LifecycleElement)) return false;
                if( !DeepComparable.Matches(Sensitivity, otherT.Sensitivity)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(QueryElement, otherT.QueryElement)) return false;
                if( !DeepComparable.Matches(Detail, otherT.Detail)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SecurityEventObjectComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(Reference, otherT.Reference)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(RoleElement, otherT.RoleElement)) return false;
                if( !DeepComparable.IsExactly(LifecycleElement, otherT.LifecycleElement)) return false;
                if( !DeepComparable.IsExactly(Sensitivity, otherT.Sensitivity)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(QueryElement, otherT.QueryElement)) return false;
                if( !DeepComparable.IsExactly(Detail, otherT.Detail)) return false;
                
                return true;
            }
            
        }
        
        
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
            
            /// <summary>
            /// Logical source location within the enterprise
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// The id of source where event originated
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SecurityEventSourceComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SiteElement != null) dest.SiteElement = (Hl7.Fhir.Model.FhirString)SiteElement.DeepCopy();
                    if(IdentifierElement != null) dest.IdentifierElement = (Hl7.Fhir.Model.FhirString)IdentifierElement.DeepCopy();
                    if(Type != null) dest.Type = new List<Hl7.Fhir.Model.Coding>(Type.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SecurityEventSourceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SecurityEventSourceComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SiteElement, otherT.SiteElement)) return false;
                if( !DeepComparable.Matches(IdentifierElement, otherT.IdentifierElement)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SecurityEventSourceComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SiteElement, otherT.SiteElement)) return false;
                if( !DeepComparable.IsExactly(IdentifierElement, otherT.IdentifierElement)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                
                return true;
            }
            
        }
        
        
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
            
            /// <summary>
            /// Type of action performed during the event
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Time when the event occurred on source
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Whether the event succeeded or failed
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Description of the event outcome
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SecurityEventEventComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Subtype != null) dest.Subtype = new List<Hl7.Fhir.Model.CodeableConcept>(Subtype.DeepCopy());
                    if(ActionElement != null) dest.ActionElement = (Code<Hl7.Fhir.Model.SecurityEvent.SecurityEventAction>)ActionElement.DeepCopy();
                    if(DateTimeElement != null) dest.DateTimeElement = (Hl7.Fhir.Model.Instant)DateTimeElement.DeepCopy();
                    if(OutcomeElement != null) dest.OutcomeElement = (Code<Hl7.Fhir.Model.SecurityEvent.SecurityEventOutcome>)OutcomeElement.DeepCopy();
                    if(OutcomeDescElement != null) dest.OutcomeDescElement = (Hl7.Fhir.Model.FhirString)OutcomeDescElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SecurityEventEventComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SecurityEventEventComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Subtype, otherT.Subtype)) return false;
                if( !DeepComparable.Matches(ActionElement, otherT.ActionElement)) return false;
                if( !DeepComparable.Matches(DateTimeElement, otherT.DateTimeElement)) return false;
                if( !DeepComparable.Matches(OutcomeElement, otherT.OutcomeElement)) return false;
                if( !DeepComparable.Matches(OutcomeDescElement, otherT.OutcomeDescElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SecurityEventEventComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Subtype, otherT.Subtype)) return false;
                if( !DeepComparable.IsExactly(ActionElement, otherT.ActionElement)) return false;
                if( !DeepComparable.IsExactly(DateTimeElement, otherT.DateTimeElement)) return false;
                if( !DeepComparable.IsExactly(OutcomeElement, otherT.OutcomeElement)) return false;
                if( !DeepComparable.IsExactly(OutcomeDescElement, otherT.OutcomeDescElement)) return false;
                
                return true;
            }
            
        }
        
        
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
            
            /// <summary>
            /// Identifier for the network access point of the user device
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
            
            /// <summary>
            /// The type of network access point
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SecurityEventParticipantNetworkComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(IdentifierElement != null) dest.IdentifierElement = (Hl7.Fhir.Model.FhirString)IdentifierElement.DeepCopy();
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.SecurityEvent.SecurityEventParticipantNetworkType>)TypeElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SecurityEventParticipantNetworkComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SecurityEventParticipantNetworkComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(IdentifierElement, otherT.IdentifierElement)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SecurityEventParticipantNetworkComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(IdentifierElement, otherT.IdentifierElement)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                
                return true;
            }
            
        }
        
        
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
            
            /// <summary>
            /// Unique identifier for the user
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Alternative User id e.g. authentication
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            /// <summary>
            /// Human-meaningful name for the user
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
            
            /// <summary>
            /// Whether user is initiator
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SecurityEventParticipantComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Role != null) dest.Role = new List<Hl7.Fhir.Model.CodeableConcept>(Role.DeepCopy());
                    if(Reference != null) dest.Reference = (Hl7.Fhir.Model.ResourceReference)Reference.DeepCopy();
                    if(UserIdElement != null) dest.UserIdElement = (Hl7.Fhir.Model.FhirString)UserIdElement.DeepCopy();
                    if(AltIdElement != null) dest.AltIdElement = (Hl7.Fhir.Model.FhirString)AltIdElement.DeepCopy();
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(RequestorElement != null) dest.RequestorElement = (Hl7.Fhir.Model.FhirBoolean)RequestorElement.DeepCopy();
                    if(Media != null) dest.Media = (Hl7.Fhir.Model.Coding)Media.DeepCopy();
                    if(Network != null) dest.Network = (Hl7.Fhir.Model.SecurityEvent.SecurityEventParticipantNetworkComponent)Network.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SecurityEventParticipantComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SecurityEventParticipantComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Role, otherT.Role)) return false;
                if( !DeepComparable.Matches(Reference, otherT.Reference)) return false;
                if( !DeepComparable.Matches(UserIdElement, otherT.UserIdElement)) return false;
                if( !DeepComparable.Matches(AltIdElement, otherT.AltIdElement)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(RequestorElement, otherT.RequestorElement)) return false;
                if( !DeepComparable.Matches(Media, otherT.Media)) return false;
                if( !DeepComparable.Matches(Network, otherT.Network)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SecurityEventParticipantComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Role, otherT.Role)) return false;
                if( !DeepComparable.IsExactly(Reference, otherT.Reference)) return false;
                if( !DeepComparable.IsExactly(UserIdElement, otherT.UserIdElement)) return false;
                if( !DeepComparable.IsExactly(AltIdElement, otherT.AltIdElement)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(RequestorElement, otherT.RequestorElement)) return false;
                if( !DeepComparable.IsExactly(Media, otherT.Media)) return false;
                if( !DeepComparable.IsExactly(Network, otherT.Network)) return false;
                
                return true;
            }
            
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
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as SecurityEvent;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Event != null) dest.Event = (Hl7.Fhir.Model.SecurityEvent.SecurityEventEventComponent)Event.DeepCopy();
                if(Participant != null) dest.Participant = new List<Hl7.Fhir.Model.SecurityEvent.SecurityEventParticipantComponent>(Participant.DeepCopy());
                if(Source != null) dest.Source = (Hl7.Fhir.Model.SecurityEvent.SecurityEventSourceComponent)Source.DeepCopy();
                if(Object != null) dest.Object = new List<Hl7.Fhir.Model.SecurityEvent.SecurityEventObjectComponent>(Object.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new SecurityEvent());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as SecurityEvent;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Event, otherT.Event)) return false;
            if( !DeepComparable.Matches(Participant, otherT.Participant)) return false;
            if( !DeepComparable.Matches(Source, otherT.Source)) return false;
            if( !DeepComparable.Matches(Object, otherT.Object)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as SecurityEvent;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Event, otherT.Event)) return false;
            if( !DeepComparable.IsExactly(Participant, otherT.Participant)) return false;
            if( !DeepComparable.IsExactly(Source, otherT.Source)) return false;
            if( !DeepComparable.IsExactly(Object, otherT.Object)) return false;
            
            return true;
        }
        
    }
    
}
