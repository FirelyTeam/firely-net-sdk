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
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Event record kept for security purposes
    /// </summary>
    [FhirType("AuditEvent", IsResource=true)]
    [DataContract]
    public partial class AuditEvent : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.AuditEvent; } }
        [NotMapped]
        public override string TypeName { get { return "AuditEvent"; } }
        
        /// <summary>
        /// Indicator for type of action performed during the event that generated the audit.
        /// (url: http://hl7.org/fhir/ValueSet/audit-event-action)
        /// </summary>
        [FhirEnumeration("AuditEventAction")]
        public enum AuditEventAction
        {
            /// <summary>
            /// Create a new database object, such as placing an order.
            /// (system: http://hl7.org/fhir/audit-event-action)
            /// </summary>
            [EnumLiteral("C", "http://hl7.org/fhir/audit-event-action"), Description("Create")]
            C,
            /// <summary>
            /// Display or print data, such as a doctor census.
            /// (system: http://hl7.org/fhir/audit-event-action)
            /// </summary>
            [EnumLiteral("R", "http://hl7.org/fhir/audit-event-action"), Description("Read/View/Print")]
            R,
            /// <summary>
            /// Update data, such as revise patient information.
            /// (system: http://hl7.org/fhir/audit-event-action)
            /// </summary>
            [EnumLiteral("U", "http://hl7.org/fhir/audit-event-action"), Description("Update")]
            U,
            /// <summary>
            /// Delete items, such as a doctor master file record.
            /// (system: http://hl7.org/fhir/audit-event-action)
            /// </summary>
            [EnumLiteral("D", "http://hl7.org/fhir/audit-event-action"), Description("Delete")]
            D,
            /// <summary>
            /// Perform a system or application function such as log-on, program execution or use of an object's method, or perform a query/search operation.
            /// (system: http://hl7.org/fhir/audit-event-action)
            /// </summary>
            [EnumLiteral("E", "http://hl7.org/fhir/audit-event-action"), Description("Execute")]
            E,
        }

        /// <summary>
        /// Indicates whether the event succeeded or failed
        /// (url: http://hl7.org/fhir/ValueSet/audit-event-outcome)
        /// </summary>
        [FhirEnumeration("AuditEventOutcome")]
        public enum AuditEventOutcome
        {
            /// <summary>
            /// The operation completed successfully (whether with warnings or not).
            /// (system: http://hl7.org/fhir/audit-event-outcome)
            /// </summary>
            [EnumLiteral("0", "http://hl7.org/fhir/audit-event-outcome"), Description("Success")]
            N0,
            /// <summary>
            /// The action was not successful due to some kind of catered for error (often equivalent to an HTTP 400 response).
            /// (system: http://hl7.org/fhir/audit-event-outcome)
            /// </summary>
            [EnumLiteral("4", "http://hl7.org/fhir/audit-event-outcome"), Description("Minor failure")]
            N4,
            /// <summary>
            /// The action was not successful due to some kind of unexpected error (often equivalent to an HTTP 500 response).
            /// (system: http://hl7.org/fhir/audit-event-outcome)
            /// </summary>
            [EnumLiteral("8", "http://hl7.org/fhir/audit-event-outcome"), Description("Serious failure")]
            N8,
            /// <summary>
            /// An error of such magnitude occurred that the system is no longer available for use (i.e. the system died).
            /// (system: http://hl7.org/fhir/audit-event-outcome)
            /// </summary>
            [EnumLiteral("12", "http://hl7.org/fhir/audit-event-outcome"), Description("Major failure")]
            N12,
        }

        /// <summary>
        /// The type of network access point of this participant in the audit event
        /// (url: http://hl7.org/fhir/ValueSet/network-type)
        /// </summary>
        [FhirEnumeration("AuditEventParticipantNetworkType")]
        public enum AuditEventParticipantNetworkType
        {
            /// <summary>
            /// The machine name, including DNS name.
            /// (system: http://hl7.org/fhir/network-type)
            /// </summary>
            [EnumLiteral("1", "http://hl7.org/fhir/network-type"), Description("Machine Name")]
            N1,
            /// <summary>
            /// The assigned Internet Protocol (IP) address.
            /// (system: http://hl7.org/fhir/network-type)
            /// </summary>
            [EnumLiteral("2", "http://hl7.org/fhir/network-type"), Description("IP Address")]
            N2,
            /// <summary>
            /// The assigned telephone number.
            /// (system: http://hl7.org/fhir/network-type)
            /// </summary>
            [EnumLiteral("3", "http://hl7.org/fhir/network-type"), Description("Telephone Number")]
            N3,
            /// <summary>
            /// The assigned email address.
            /// (system: http://hl7.org/fhir/network-type)
            /// </summary>
            [EnumLiteral("4", "http://hl7.org/fhir/network-type"), Description("Email address")]
            N4,
            /// <summary>
            /// URI (User directory, HTTP-PUT, ftp, etc.).
            /// (system: http://hl7.org/fhir/network-type)
            /// </summary>
            [EnumLiteral("5", "http://hl7.org/fhir/network-type"), Description("URI")]
            N5,
        }

        [FhirType("EventComponent")]
        [DataContract]
        public partial class EventComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "EventComponent"; } }
            
            /// <summary>
            /// Type/identifier of event
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.Coding _Type;
            
            /// <summary>
            /// More specific type/id for the event
            /// </summary>
            [FhirElement("subtype", InSummary=true, Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> Subtype
            {
                get { if(_Subtype==null) _Subtype = new List<Hl7.Fhir.Model.Coding>(); return _Subtype; }
                set { _Subtype = value; OnPropertyChanged("Subtype"); }
            }
            
            private List<Hl7.Fhir.Model.Coding> _Subtype;
            
            /// <summary>
            /// Type of action performed during the event
            /// </summary>
            [FhirElement("action", InSummary=true, Order=60)]
            [DataMember]
            public Code<Hl7.Fhir.Model.AuditEvent.AuditEventAction> ActionElement
            {
                get { return _ActionElement; }
                set { _ActionElement = value; OnPropertyChanged("ActionElement"); }
            }
            
            private Code<Hl7.Fhir.Model.AuditEvent.AuditEventAction> _ActionElement;
            
            /// <summary>
            /// Type of action performed during the event
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.AuditEvent.AuditEventAction? Action
            {
                get { return ActionElement != null ? ActionElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        ActionElement = null; 
                    else
                        ActionElement = new Code<Hl7.Fhir.Model.AuditEvent.AuditEventAction>(value);
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
                    if (!value.HasValue)
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
            public Code<Hl7.Fhir.Model.AuditEvent.AuditEventOutcome> OutcomeElement
            {
                get { return _OutcomeElement; }
                set { _OutcomeElement = value; OnPropertyChanged("OutcomeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.AuditEvent.AuditEventOutcome> _OutcomeElement;
            
            /// <summary>
            /// Whether the event succeeded or failed
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.AuditEvent.AuditEventOutcome? Outcome
            {
                get { return OutcomeElement != null ? OutcomeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        OutcomeElement = null; 
                    else
                        OutcomeElement = new Code<Hl7.Fhir.Model.AuditEvent.AuditEventOutcome>(value);
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
                    if (value == null)
                        OutcomeDescElement = null; 
                    else
                        OutcomeDescElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("OutcomeDesc");
                }
            }
            
            /// <summary>
            /// The purposeOfUse of the event
            /// </summary>
            [FhirElement("purposeOfEvent", InSummary=true, Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> PurposeOfEvent
            {
                get { if(_PurposeOfEvent==null) _PurposeOfEvent = new List<Hl7.Fhir.Model.Coding>(); return _PurposeOfEvent; }
                set { _PurposeOfEvent = value; OnPropertyChanged("PurposeOfEvent"); }
            }
            
            private List<Hl7.Fhir.Model.Coding> _PurposeOfEvent;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as EventComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.Coding)Type.DeepCopy();
                    if(Subtype != null) dest.Subtype = new List<Hl7.Fhir.Model.Coding>(Subtype.DeepCopy());
                    if(ActionElement != null) dest.ActionElement = (Code<Hl7.Fhir.Model.AuditEvent.AuditEventAction>)ActionElement.DeepCopy();
                    if(DateTimeElement != null) dest.DateTimeElement = (Hl7.Fhir.Model.Instant)DateTimeElement.DeepCopy();
                    if(OutcomeElement != null) dest.OutcomeElement = (Code<Hl7.Fhir.Model.AuditEvent.AuditEventOutcome>)OutcomeElement.DeepCopy();
                    if(OutcomeDescElement != null) dest.OutcomeDescElement = (Hl7.Fhir.Model.FhirString)OutcomeDescElement.DeepCopy();
                    if(PurposeOfEvent != null) dest.PurposeOfEvent = new List<Hl7.Fhir.Model.Coding>(PurposeOfEvent.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new EventComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as EventComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Subtype, otherT.Subtype)) return false;
                if( !DeepComparable.Matches(ActionElement, otherT.ActionElement)) return false;
                if( !DeepComparable.Matches(DateTimeElement, otherT.DateTimeElement)) return false;
                if( !DeepComparable.Matches(OutcomeElement, otherT.OutcomeElement)) return false;
                if( !DeepComparable.Matches(OutcomeDescElement, otherT.OutcomeDescElement)) return false;
                if( !DeepComparable.Matches(PurposeOfEvent, otherT.PurposeOfEvent)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as EventComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Subtype, otherT.Subtype)) return false;
                if( !DeepComparable.IsExactly(ActionElement, otherT.ActionElement)) return false;
                if( !DeepComparable.IsExactly(DateTimeElement, otherT.DateTimeElement)) return false;
                if( !DeepComparable.IsExactly(OutcomeElement, otherT.OutcomeElement)) return false;
                if( !DeepComparable.IsExactly(OutcomeDescElement, otherT.OutcomeDescElement)) return false;
                if( !DeepComparable.IsExactly(PurposeOfEvent, otherT.PurposeOfEvent)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    foreach (var elem in Subtype) { if (elem != null) yield return elem; }
                    if (ActionElement != null) yield return ActionElement;
                    if (DateTimeElement != null) yield return DateTimeElement;
                    if (OutcomeElement != null) yield return OutcomeElement;
                    if (OutcomeDescElement != null) yield return OutcomeDescElement;
                    foreach (var elem in PurposeOfEvent) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    foreach (var elem in Subtype) { if (elem != null) yield return new ElementValue("subtype", elem); }
                    if (ActionElement != null) yield return new ElementValue("action", ActionElement);
                    if (DateTimeElement != null) yield return new ElementValue("dateTime", DateTimeElement);
                    if (OutcomeElement != null) yield return new ElementValue("outcome", OutcomeElement);
                    if (OutcomeDescElement != null) yield return new ElementValue("outcomeDesc", OutcomeDescElement);
                    foreach (var elem in PurposeOfEvent) { if (elem != null) yield return new ElementValue("purposeOfEvent", elem); }
                }
            }

            
        }
        
        
        [FhirType("ParticipantComponent")]
        [DataContract]
        public partial class ParticipantComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ParticipantComponent"; } }
            
            /// <summary>
            /// User roles (e.g. local RBAC codes)
            /// </summary>
            [FhirElement("role", Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Role
            {
                get { if(_Role==null) _Role = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Role; }
                set { _Role = value; OnPropertyChanged("Role"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Role;
            
            /// <summary>
            /// Direct reference to resource
            /// </summary>
            [FhirElement("reference", InSummary=true, Order=50)]
            [CLSCompliant(false)]
			[References("Practitioner","Organization","Device","Patient","RelatedPerson")]
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
            public Hl7.Fhir.Model.Identifier UserId
            {
                get { return _UserId; }
                set { _UserId = value; OnPropertyChanged("UserId"); }
            }
            
            private Hl7.Fhir.Model.Identifier _UserId;
            
            /// <summary>
            /// Alternative User id e.g. authentication
            /// </summary>
            [FhirElement("altId", Order=70)]
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
                    if (value == null)
                        AltIdElement = null; 
                    else
                        AltIdElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("AltId");
                }
            }
            
            /// <summary>
            /// Human-meaningful name for the user
            /// </summary>
            [FhirElement("name", Order=80)]
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
                    if (value == null)
                        NameElement = null; 
                    else
                        NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Whether user is initiator
            /// </summary>
            [FhirElement("requestor", Order=90)]
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
                    if (!value.HasValue)
                        RequestorElement = null; 
                    else
                        RequestorElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Requestor");
                }
            }
            
            /// <summary>
            /// Where
            /// </summary>
            [FhirElement("location", Order=100)]
            [CLSCompliant(false)]
			[References("Location")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Location
            {
                get { return _Location; }
                set { _Location = value; OnPropertyChanged("Location"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Location;
            
            /// <summary>
            /// Policy that authorized event
            /// </summary>
            [FhirElement("policy", Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirUri> PolicyElement
            {
                get { if(_PolicyElement==null) _PolicyElement = new List<Hl7.Fhir.Model.FhirUri>(); return _PolicyElement; }
                set { _PolicyElement = value; OnPropertyChanged("PolicyElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirUri> _PolicyElement;
            
            /// <summary>
            /// Policy that authorized event
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Policy
            {
                get { return PolicyElement != null ? PolicyElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        PolicyElement = null; 
                    else
                        PolicyElement = new List<Hl7.Fhir.Model.FhirUri>(value.Select(elem=>new Hl7.Fhir.Model.FhirUri(elem)));
                    OnPropertyChanged("Policy");
                }
            }
            
            /// <summary>
            /// Type of media
            /// </summary>
            [FhirElement("media", Order=120)]
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
            [FhirElement("network", Order=130)]
            [DataMember]
            public Hl7.Fhir.Model.AuditEvent.NetworkComponent Network
            {
                get { return _Network; }
                set { _Network = value; OnPropertyChanged("Network"); }
            }
            
            private Hl7.Fhir.Model.AuditEvent.NetworkComponent _Network;
            
            /// <summary>
            /// Reason given for this user
            /// </summary>
            [FhirElement("purposeOfUse", Order=140)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> PurposeOfUse
            {
                get { if(_PurposeOfUse==null) _PurposeOfUse = new List<Hl7.Fhir.Model.Coding>(); return _PurposeOfUse; }
                set { _PurposeOfUse = value; OnPropertyChanged("PurposeOfUse"); }
            }
            
            private List<Hl7.Fhir.Model.Coding> _PurposeOfUse;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ParticipantComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Role != null) dest.Role = new List<Hl7.Fhir.Model.CodeableConcept>(Role.DeepCopy());
                    if(Reference != null) dest.Reference = (Hl7.Fhir.Model.ResourceReference)Reference.DeepCopy();
                    if(UserId != null) dest.UserId = (Hl7.Fhir.Model.Identifier)UserId.DeepCopy();
                    if(AltIdElement != null) dest.AltIdElement = (Hl7.Fhir.Model.FhirString)AltIdElement.DeepCopy();
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(RequestorElement != null) dest.RequestorElement = (Hl7.Fhir.Model.FhirBoolean)RequestorElement.DeepCopy();
                    if(Location != null) dest.Location = (Hl7.Fhir.Model.ResourceReference)Location.DeepCopy();
                    if(PolicyElement != null) dest.PolicyElement = new List<Hl7.Fhir.Model.FhirUri>(PolicyElement.DeepCopy());
                    if(Media != null) dest.Media = (Hl7.Fhir.Model.Coding)Media.DeepCopy();
                    if(Network != null) dest.Network = (Hl7.Fhir.Model.AuditEvent.NetworkComponent)Network.DeepCopy();
                    if(PurposeOfUse != null) dest.PurposeOfUse = new List<Hl7.Fhir.Model.Coding>(PurposeOfUse.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ParticipantComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ParticipantComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Role, otherT.Role)) return false;
                if( !DeepComparable.Matches(Reference, otherT.Reference)) return false;
                if( !DeepComparable.Matches(UserId, otherT.UserId)) return false;
                if( !DeepComparable.Matches(AltIdElement, otherT.AltIdElement)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(RequestorElement, otherT.RequestorElement)) return false;
                if( !DeepComparable.Matches(Location, otherT.Location)) return false;
                if( !DeepComparable.Matches(PolicyElement, otherT.PolicyElement)) return false;
                if( !DeepComparable.Matches(Media, otherT.Media)) return false;
                if( !DeepComparable.Matches(Network, otherT.Network)) return false;
                if( !DeepComparable.Matches(PurposeOfUse, otherT.PurposeOfUse)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ParticipantComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Role, otherT.Role)) return false;
                if( !DeepComparable.IsExactly(Reference, otherT.Reference)) return false;
                if( !DeepComparable.IsExactly(UserId, otherT.UserId)) return false;
                if( !DeepComparable.IsExactly(AltIdElement, otherT.AltIdElement)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(RequestorElement, otherT.RequestorElement)) return false;
                if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
                if( !DeepComparable.IsExactly(PolicyElement, otherT.PolicyElement)) return false;
                if( !DeepComparable.IsExactly(Media, otherT.Media)) return false;
                if( !DeepComparable.IsExactly(Network, otherT.Network)) return false;
                if( !DeepComparable.IsExactly(PurposeOfUse, otherT.PurposeOfUse)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Role) { if (elem != null) yield return elem; }
                    if (Reference != null) yield return Reference;
                    if (UserId != null) yield return UserId;
                    if (AltIdElement != null) yield return AltIdElement;
                    if (NameElement != null) yield return NameElement;
                    if (RequestorElement != null) yield return RequestorElement;
                    if (Location != null) yield return Location;
                    foreach (var elem in PolicyElement) { if (elem != null) yield return elem; }
                    if (Media != null) yield return Media;
                    if (Network != null) yield return Network;
                    foreach (var elem in PurposeOfUse) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Role) { if (elem != null) yield return new ElementValue("role", elem); }
                    if (Reference != null) yield return new ElementValue("reference", Reference);
                    if (UserId != null) yield return new ElementValue("userId", UserId);
                    if (AltIdElement != null) yield return new ElementValue("altId", AltIdElement);
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (RequestorElement != null) yield return new ElementValue("requestor", RequestorElement);
                    if (Location != null) yield return new ElementValue("location", Location);
                    foreach (var elem in PolicyElement) { if (elem != null) yield return new ElementValue("policy", elem); }
                    if (Media != null) yield return new ElementValue("media", Media);
                    if (Network != null) yield return new ElementValue("network", Network);
                    foreach (var elem in PurposeOfUse) { if (elem != null) yield return new ElementValue("purposeOfUse", elem); }
                }
            }

            
        }
        
        
        [FhirType("NetworkComponent")]
        [DataContract]
        public partial class NetworkComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "NetworkComponent"; } }
            
            /// <summary>
            /// Identifier for the network access point of the user device
            /// </summary>
            [FhirElement("address", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString AddressElement
            {
                get { return _AddressElement; }
                set { _AddressElement = value; OnPropertyChanged("AddressElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _AddressElement;
            
            /// <summary>
            /// Identifier for the network access point of the user device
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
                        AddressElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Address");
                }
            }
            
            /// <summary>
            /// The type of network access point
            /// </summary>
            [FhirElement("type", Order=50)]
            [DataMember]
            public Code<Hl7.Fhir.Model.AuditEvent.AuditEventParticipantNetworkType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.AuditEvent.AuditEventParticipantNetworkType> _TypeElement;
            
            /// <summary>
            /// The type of network access point
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.AuditEvent.AuditEventParticipantNetworkType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        TypeElement = null; 
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.AuditEvent.AuditEventParticipantNetworkType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as NetworkComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(AddressElement != null) dest.AddressElement = (Hl7.Fhir.Model.FhirString)AddressElement.DeepCopy();
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.AuditEvent.AuditEventParticipantNetworkType>)TypeElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new NetworkComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as NetworkComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(AddressElement, otherT.AddressElement)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as NetworkComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(AddressElement, otherT.AddressElement)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (AddressElement != null) yield return AddressElement;
                    if (TypeElement != null) yield return TypeElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (AddressElement != null) yield return new ElementValue("address", AddressElement);
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                }
            }

            
        }
        
        
        [FhirType("SourceComponent")]
        [DataContract]
        public partial class SourceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "SourceComponent"; } }
            
            /// <summary>
            /// Logical source location within the enterprise
            /// </summary>
            [FhirElement("site", Order=40)]
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
                    if (value == null)
                        SiteElement = null; 
                    else
                        SiteElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Site");
                }
            }
            
            /// <summary>
            /// The identity of source detecting the event
            /// </summary>
            [FhirElement("identifier", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// The type of source where event originated
            /// </summary>
            [FhirElement("type", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> Type
            {
                get { if(_Type==null) _Type = new List<Hl7.Fhir.Model.Coding>(); return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private List<Hl7.Fhir.Model.Coding> _Type;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SourceComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SiteElement != null) dest.SiteElement = (Hl7.Fhir.Model.FhirString)SiteElement.DeepCopy();
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(Type != null) dest.Type = new List<Hl7.Fhir.Model.Coding>(Type.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SourceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SourceComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SiteElement, otherT.SiteElement)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SourceComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SiteElement, otherT.SiteElement)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SiteElement != null) yield return SiteElement;
                    if (Identifier != null) yield return Identifier;
                    foreach (var elem in Type) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SiteElement != null) yield return new ElementValue("site", SiteElement);
                    if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                    foreach (var elem in Type) { if (elem != null) yield return new ElementValue("type", elem); }
                }
            }

            
        }
        
        
        [FhirType("ObjectComponent")]
        [DataContract]
        public partial class ObjectComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ObjectComponent"; } }
            
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
            [CLSCompliant(false)]
			[References()]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Reference
            {
                get { return _Reference; }
                set { _Reference = value; OnPropertyChanged("Reference"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Reference;
            
            /// <summary>
            /// Type of object involved
            /// </summary>
            [FhirElement("type", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.Coding _Type;
            
            /// <summary>
            /// What role the Object played
            /// </summary>
            [FhirElement("role", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Role
            {
                get { return _Role; }
                set { _Role = value; OnPropertyChanged("Role"); }
            }
            
            private Hl7.Fhir.Model.Coding _Role;
            
            /// <summary>
            /// Life-cycle stage for the object
            /// </summary>
            [FhirElement("lifecycle", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Lifecycle
            {
                get { return _Lifecycle; }
                set { _Lifecycle = value; OnPropertyChanged("Lifecycle"); }
            }
            
            private Hl7.Fhir.Model.Coding _Lifecycle;
            
            /// <summary>
            /// Security labels applied to the object
            /// </summary>
            [FhirElement("securityLabel", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> SecurityLabel
            {
                get { if(_SecurityLabel==null) _SecurityLabel = new List<Hl7.Fhir.Model.Coding>(); return _SecurityLabel; }
                set { _SecurityLabel = value; OnPropertyChanged("SecurityLabel"); }
            }
            
            private List<Hl7.Fhir.Model.Coding> _SecurityLabel;
            
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
                    if (value == null)
                        NameElement = null; 
                    else
                        NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Descriptive text
            /// </summary>
            [FhirElement("description", Order=110)]
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
                    if (value == null)
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
                    if (value == null)
                        QueryElement = null; 
                    else
                        QueryElement = new Hl7.Fhir.Model.Base64Binary(value);
                    OnPropertyChanged("Query");
                }
            }
            
            /// <summary>
            /// Additional Information about the Object
            /// </summary>
            [FhirElement("detail", Order=130)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.AuditEvent.DetailComponent> Detail
            {
                get { if(_Detail==null) _Detail = new List<Hl7.Fhir.Model.AuditEvent.DetailComponent>(); return _Detail; }
                set { _Detail = value; OnPropertyChanged("Detail"); }
            }
            
            private List<Hl7.Fhir.Model.AuditEvent.DetailComponent> _Detail;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ObjectComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(Reference != null) dest.Reference = (Hl7.Fhir.Model.ResourceReference)Reference.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.Coding)Type.DeepCopy();
                    if(Role != null) dest.Role = (Hl7.Fhir.Model.Coding)Role.DeepCopy();
                    if(Lifecycle != null) dest.Lifecycle = (Hl7.Fhir.Model.Coding)Lifecycle.DeepCopy();
                    if(SecurityLabel != null) dest.SecurityLabel = new List<Hl7.Fhir.Model.Coding>(SecurityLabel.DeepCopy());
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(QueryElement != null) dest.QueryElement = (Hl7.Fhir.Model.Base64Binary)QueryElement.DeepCopy();
                    if(Detail != null) dest.Detail = new List<Hl7.Fhir.Model.AuditEvent.DetailComponent>(Detail.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ObjectComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ObjectComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(Reference, otherT.Reference)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Role, otherT.Role)) return false;
                if( !DeepComparable.Matches(Lifecycle, otherT.Lifecycle)) return false;
                if( !DeepComparable.Matches(SecurityLabel, otherT.SecurityLabel)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(QueryElement, otherT.QueryElement)) return false;
                if( !DeepComparable.Matches(Detail, otherT.Detail)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ObjectComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(Reference, otherT.Reference)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Role, otherT.Role)) return false;
                if( !DeepComparable.IsExactly(Lifecycle, otherT.Lifecycle)) return false;
                if( !DeepComparable.IsExactly(SecurityLabel, otherT.SecurityLabel)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(QueryElement, otherT.QueryElement)) return false;
                if( !DeepComparable.IsExactly(Detail, otherT.Detail)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Identifier != null) yield return Identifier;
                    if (Reference != null) yield return Reference;
                    if (Type != null) yield return Type;
                    if (Role != null) yield return Role;
                    if (Lifecycle != null) yield return Lifecycle;
                    foreach (var elem in SecurityLabel) { if (elem != null) yield return elem; }
                    if (NameElement != null) yield return NameElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (QueryElement != null) yield return QueryElement;
                    foreach (var elem in Detail) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                    if (Reference != null) yield return new ElementValue("reference", Reference);
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Role != null) yield return new ElementValue("role", Role);
                    if (Lifecycle != null) yield return new ElementValue("lifecycle", Lifecycle);
                    foreach (var elem in SecurityLabel) { if (elem != null) yield return new ElementValue("securityLabel", elem); }
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (QueryElement != null) yield return new ElementValue("query", QueryElement);
                    foreach (var elem in Detail) { if (elem != null) yield return new ElementValue("detail", elem); }
                }
            }

            
        }
        
        
        [FhirType("DetailComponent")]
        [DataContract]
        public partial class DetailComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "DetailComponent"; } }
            
            /// <summary>
            /// Name of the property
            /// </summary>
            [FhirElement("type", Order=40)]
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
                    if (value == null)
                        TypeElement = null; 
                    else
                        TypeElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Property value
            /// </summary>
            [FhirElement("value", Order=50)]
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
                    if (value == null)
                        ValueElement = null; 
                    else
                        ValueElement = new Hl7.Fhir.Model.Base64Binary(value);
                    OnPropertyChanged("Value");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DetailComponent;
                
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
                return CopyTo(new DetailComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DetailComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DetailComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (TypeElement != null) yield return TypeElement;
                    if (ValueElement != null) yield return ValueElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                }
            }

            
        }
        
        
        /// <summary>
        /// What was done
        /// </summary>
        [FhirElement("event", Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.AuditEvent.EventComponent Event
        {
            get { return _Event; }
            set { _Event = value; OnPropertyChanged("Event"); }
        }
        
        private Hl7.Fhir.Model.AuditEvent.EventComponent _Event;
        
        /// <summary>
        /// A person, a hardware device or software process
        /// </summary>
        [FhirElement("participant", Order=100)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.AuditEvent.ParticipantComponent> Participant
        {
            get { if(_Participant==null) _Participant = new List<Hl7.Fhir.Model.AuditEvent.ParticipantComponent>(); return _Participant; }
            set { _Participant = value; OnPropertyChanged("Participant"); }
        }
        
        private List<Hl7.Fhir.Model.AuditEvent.ParticipantComponent> _Participant;
        
        /// <summary>
        /// Application systems and processes
        /// </summary>
        [FhirElement("source", Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.AuditEvent.SourceComponent Source
        {
            get { return _Source; }
            set { _Source = value; OnPropertyChanged("Source"); }
        }
        
        private Hl7.Fhir.Model.AuditEvent.SourceComponent _Source;
        
        /// <summary>
        /// Specific instances of data or objects that have been accessed
        /// </summary>
        [FhirElement("object", Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.AuditEvent.ObjectComponent> Object
        {
            get { if(_Object==null) _Object = new List<Hl7.Fhir.Model.AuditEvent.ObjectComponent>(); return _Object; }
            set { _Object = value; OnPropertyChanged("Object"); }
        }
        
        private List<Hl7.Fhir.Model.AuditEvent.ObjectComponent> _Object;
        

        public static ElementDefinition.ConstraintComponent AuditEvent_SEV_1 = new ElementDefinition.ConstraintComponent()
        {
            Extension = new List<Model.Extension>() { new Model.Extension("http://hl7.org/fhir/StructureDefinition/structuredefinition-expression", new FhirString("object.all(name.empty() or query.empty())"))},
            Key = "sev-1",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Either a name or a query (NOT both)",
            Xpath = "not(exists(f:name)) or not(exists(f:query))"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(AuditEvent_SEV_1);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as AuditEvent;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Event != null) dest.Event = (Hl7.Fhir.Model.AuditEvent.EventComponent)Event.DeepCopy();
                if(Participant != null) dest.Participant = new List<Hl7.Fhir.Model.AuditEvent.ParticipantComponent>(Participant.DeepCopy());
                if(Source != null) dest.Source = (Hl7.Fhir.Model.AuditEvent.SourceComponent)Source.DeepCopy();
                if(Object != null) dest.Object = new List<Hl7.Fhir.Model.AuditEvent.ObjectComponent>(Object.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new AuditEvent());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as AuditEvent;
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
            var otherT = other as AuditEvent;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Event, otherT.Event)) return false;
            if( !DeepComparable.IsExactly(Participant, otherT.Participant)) return false;
            if( !DeepComparable.IsExactly(Source, otherT.Source)) return false;
            if( !DeepComparable.IsExactly(Object, otherT.Object)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				if (Event != null) yield return Event;
				foreach (var elem in Participant) { if (elem != null) yield return elem; }
				if (Source != null) yield return Source;
				foreach (var elem in Object) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (Event != null) yield return new ElementValue("event", Event);
                foreach (var elem in Participant) { if (elem != null) yield return new ElementValue("participant", elem); }
                if (Source != null) yield return new ElementValue("source", Source);
                foreach (var elem in Object) { if (elem != null) yield return new ElementValue("object", elem); }
            }
        }

    }
    
}
