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
    /// A request for a diagnostic service
    /// </summary>
    [FhirType("DiagnosticOrder", IsResource=true)]
    [DataContract]
    public partial class DiagnosticOrder : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// The status of a diagnostic order
        /// </summary>
        [FhirEnumeration("DiagnosticOrderStatus")]
        public enum DiagnosticOrderStatus
        {
            /// <summary>
            /// The request has been placed.
            /// </summary>
            [EnumLiteral("requested")]
            Requested,
            /// <summary>
            /// The receiving system has received the order, but not yet decided whether it will be performed.
            /// </summary>
            [EnumLiteral("received")]
            Received,
            /// <summary>
            /// The receiving system has accepted the order, but work has not yet commenced.
            /// </summary>
            [EnumLiteral("accepted")]
            Accepted,
            /// <summary>
            /// The work to fulfill the order is happening.
            /// </summary>
            [EnumLiteral("in progress")]
            InProgress,
            /// <summary>
            /// The work is complete, and the outcomes are being reviewed for approval.
            /// </summary>
            [EnumLiteral("review")]
            Review,
            /// <summary>
            /// The work has been complete, the report(s) released, and no further work is planned.
            /// </summary>
            [EnumLiteral("completed")]
            Completed,
            /// <summary>
            /// The request has been held by originating system/user request.
            /// </summary>
            [EnumLiteral("suspended")]
            Suspended,
            /// <summary>
            /// The receiving system has declined to fulfill the request.
            /// </summary>
            [EnumLiteral("rejected")]
            Rejected,
            /// <summary>
            /// The diagnostic investigation was attempted, but due to some procedural error, it could not be completed.
            /// </summary>
            [EnumLiteral("failed")]
            Failed,
        }
        
        /// <summary>
        /// The clinical priority of a diagnostic order
        /// </summary>
        [FhirEnumeration("DiagnosticOrderPriority")]
        public enum DiagnosticOrderPriority
        {
            /// <summary>
            /// The order has a normal priority.
            /// </summary>
            [EnumLiteral("routine")]
            Routine,
            /// <summary>
            /// The order should be urgently.
            /// </summary>
            [EnumLiteral("urgent")]
            Urgent,
            /// <summary>
            /// The order is time-critical.
            /// </summary>
            [EnumLiteral("stat")]
            Stat,
            /// <summary>
            /// The order should be acted on as soon as possible.
            /// </summary>
            [EnumLiteral("asap")]
            Asap,
        }
        
        [FhirType("DiagnosticOrderItemComponent")]
        [DataContract]
        public partial class DiagnosticOrderItemComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Code to indicate the item (test or panel) being ordered
            /// </summary>
            [FhirElement("code", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// If this item relates to specific specimens
            /// </summary>
            [FhirElement("specimen", InSummary=true, Order=50)]
            [References("Specimen")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Specimen
            {
                get { return _Specimen; }
                set { _Specimen = value; OnPropertyChanged("Specimen"); }
            }
            private List<Hl7.Fhir.Model.ResourceReference> _Specimen;
            
            /// <summary>
            /// Location of requested test (if applicable)
            /// </summary>
            [FhirElement("bodySite", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept BodySite
            {
                get { return _BodySite; }
                set { _BodySite = value; OnPropertyChanged("BodySite"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _BodySite;
            
            /// <summary>
            /// requested | received | accepted | in progress | review | completed | suspended | rejected | failed
            /// </summary>
            [FhirElement("status", InSummary=true, Order=70)]
            [DataMember]
            public Code<Hl7.Fhir.Model.DiagnosticOrder.DiagnosticOrderStatus> StatusElement
            {
                get { return _StatusElement; }
                set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
            }
            private Code<Hl7.Fhir.Model.DiagnosticOrder.DiagnosticOrderStatus> _StatusElement;
            
            /// <summary>
            /// requested | received | accepted | in progress | review | completed | suspended | rejected | failed
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.DiagnosticOrder.DiagnosticOrderStatus? Status
            {
                get { return StatusElement != null ? StatusElement.Value : null; }
                set
                {
                    if(value == null)
                      StatusElement = null; 
                    else
                      StatusElement = new Code<Hl7.Fhir.Model.DiagnosticOrder.DiagnosticOrderStatus>(value);
                    OnPropertyChanged("Status");
                }
            }
            
            /// <summary>
            /// Events specific to this item
            /// </summary>
            [FhirElement("event", InSummary=true, Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.DiagnosticOrder.DiagnosticOrderEventComponent> Event
            {
                get { return _Event; }
                set { _Event = value; OnPropertyChanged("Event"); }
            }
            private List<Hl7.Fhir.Model.DiagnosticOrder.DiagnosticOrderEventComponent> _Event;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DiagnosticOrderItemComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(Specimen != null) dest.Specimen = new List<Hl7.Fhir.Model.ResourceReference>(Specimen.DeepCopy());
                    if(BodySite != null) dest.BodySite = (Hl7.Fhir.Model.CodeableConcept)BodySite.DeepCopy();
                    if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.DiagnosticOrder.DiagnosticOrderStatus>)StatusElement.DeepCopy();
                    if(Event != null) dest.Event = new List<Hl7.Fhir.Model.DiagnosticOrder.DiagnosticOrderEventComponent>(Event.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DiagnosticOrderItemComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DiagnosticOrderItemComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Specimen, otherT.Specimen)) return false;
                if( !DeepComparable.Matches(BodySite, otherT.BodySite)) return false;
                if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
                if( !DeepComparable.Matches(Event, otherT.Event)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DiagnosticOrderItemComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Specimen, otherT.Specimen)) return false;
                if( !DeepComparable.IsExactly(BodySite, otherT.BodySite)) return false;
                if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
                if( !DeepComparable.IsExactly(Event, otherT.Event)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("DiagnosticOrderEventComponent")]
        [DataContract]
        public partial class DiagnosticOrderEventComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// requested | received | accepted | in progress | review | completed | suspended | rejected | failed
            /// </summary>
            [FhirElement("status", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.DiagnosticOrder.DiagnosticOrderStatus> StatusElement
            {
                get { return _StatusElement; }
                set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
            }
            private Code<Hl7.Fhir.Model.DiagnosticOrder.DiagnosticOrderStatus> _StatusElement;
            
            /// <summary>
            /// requested | received | accepted | in progress | review | completed | suspended | rejected | failed
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.DiagnosticOrder.DiagnosticOrderStatus? Status
            {
                get { return StatusElement != null ? StatusElement.Value : null; }
                set
                {
                    if(value == null)
                      StatusElement = null; 
                    else
                      StatusElement = new Code<Hl7.Fhir.Model.DiagnosticOrder.DiagnosticOrderStatus>(value);
                    OnPropertyChanged("Status");
                }
            }
            
            /// <summary>
            /// More information about the event and it's context
            /// </summary>
            [FhirElement("description", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Description
            {
                get { return _Description; }
                set { _Description = value; OnPropertyChanged("Description"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _Description;
            
            /// <summary>
            /// The date at which the event happened
            /// </summary>
            [FhirElement("dateTime", InSummary=true, Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime DateTimeElement
            {
                get { return _DateTimeElement; }
                set { _DateTimeElement = value; OnPropertyChanged("DateTimeElement"); }
            }
            private Hl7.Fhir.Model.FhirDateTime _DateTimeElement;
            
            /// <summary>
            /// The date at which the event happened
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string DateTime
            {
                get { return DateTimeElement != null ? DateTimeElement.Value : null; }
                set
                {
                    if(value == null)
                      DateTimeElement = null; 
                    else
                      DateTimeElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("DateTime");
                }
            }
            
            /// <summary>
            /// Who recorded or did this
            /// </summary>
            [FhirElement("actor", InSummary=true, Order=70)]
            [References("Practitioner","Device")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Actor
            {
                get { return _Actor; }
                set { _Actor = value; OnPropertyChanged("Actor"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Actor;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DiagnosticOrderEventComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.DiagnosticOrder.DiagnosticOrderStatus>)StatusElement.DeepCopy();
                    if(Description != null) dest.Description = (Hl7.Fhir.Model.CodeableConcept)Description.DeepCopy();
                    if(DateTimeElement != null) dest.DateTimeElement = (Hl7.Fhir.Model.FhirDateTime)DateTimeElement.DeepCopy();
                    if(Actor != null) dest.Actor = (Hl7.Fhir.Model.ResourceReference)Actor.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DiagnosticOrderEventComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DiagnosticOrderEventComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
                if( !DeepComparable.Matches(Description, otherT.Description)) return false;
                if( !DeepComparable.Matches(DateTimeElement, otherT.DateTimeElement)) return false;
                if( !DeepComparable.Matches(Actor, otherT.Actor)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DiagnosticOrderEventComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
                if( !DeepComparable.IsExactly(Description, otherT.Description)) return false;
                if( !DeepComparable.IsExactly(DateTimeElement, otherT.DateTimeElement)) return false;
                if( !DeepComparable.IsExactly(Actor, otherT.Actor)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// Who and/or what test is about
        /// </summary>
        [FhirElement("subject", Order=70)]
        [References("Patient","Group","Location","Device")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Who ordered the test
        /// </summary>
        [FhirElement("orderer", Order=80)]
        [References("Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Orderer
        {
            get { return _Orderer; }
            set { _Orderer = value; OnPropertyChanged("Orderer"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Orderer;
        
        /// <summary>
        /// Identifiers assigned to this order
        /// </summary>
        [FhirElement("identifier", Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// The encounter that this diagnostic order is associated with
        /// </summary>
        [FhirElement("encounter", Order=100)]
        [References("Encounter")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Encounter
        {
            get { return _Encounter; }
            set { _Encounter = value; OnPropertyChanged("Encounter"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Encounter;
        
        /// <summary>
        /// Explanation/Justification for test
        /// </summary>
        [FhirElement("clinicalNotes", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ClinicalNotesElement
        {
            get { return _ClinicalNotesElement; }
            set { _ClinicalNotesElement = value; OnPropertyChanged("ClinicalNotesElement"); }
        }
        private Hl7.Fhir.Model.FhirString _ClinicalNotesElement;
        
        /// <summary>
        /// Explanation/Justification for test
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ClinicalNotes
        {
            get { return ClinicalNotesElement != null ? ClinicalNotesElement.Value : null; }
            set
            {
                if(value == null)
                  ClinicalNotesElement = null; 
                else
                  ClinicalNotesElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("ClinicalNotes");
            }
        }
        
        /// <summary>
        /// If the whole order relates to specific specimens
        /// </summary>
        [FhirElement("specimen", Order=120)]
        [References("Specimen")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Specimen
        {
            get { return _Specimen; }
            set { _Specimen = value; OnPropertyChanged("Specimen"); }
        }
        private List<Hl7.Fhir.Model.ResourceReference> _Specimen;
        
        /// <summary>
        /// requested | received | accepted | in progress | review | completed | suspended | rejected | failed
        /// </summary>
        [FhirElement("status", Order=130)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DiagnosticOrder.DiagnosticOrderStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        private Code<Hl7.Fhir.Model.DiagnosticOrder.DiagnosticOrderStatus> _StatusElement;
        
        /// <summary>
        /// requested | received | accepted | in progress | review | completed | suspended | rejected | failed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DiagnosticOrder.DiagnosticOrderStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.DiagnosticOrder.DiagnosticOrderStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// routine | urgent | stat | asap
        /// </summary>
        [FhirElement("priority", Order=140)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DiagnosticOrder.DiagnosticOrderPriority> PriorityElement
        {
            get { return _PriorityElement; }
            set { _PriorityElement = value; OnPropertyChanged("PriorityElement"); }
        }
        private Code<Hl7.Fhir.Model.DiagnosticOrder.DiagnosticOrderPriority> _PriorityElement;
        
        /// <summary>
        /// routine | urgent | stat | asap
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DiagnosticOrder.DiagnosticOrderPriority? Priority
        {
            get { return PriorityElement != null ? PriorityElement.Value : null; }
            set
            {
                if(value == null)
                  PriorityElement = null; 
                else
                  PriorityElement = new Code<Hl7.Fhir.Model.DiagnosticOrder.DiagnosticOrderPriority>(value);
                OnPropertyChanged("Priority");
            }
        }
        
        /// <summary>
        /// A list of events of interest in the lifecycle
        /// </summary>
        [FhirElement("event", Order=150)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.DiagnosticOrder.DiagnosticOrderEventComponent> Event
        {
            get { return _Event; }
            set { _Event = value; OnPropertyChanged("Event"); }
        }
        private List<Hl7.Fhir.Model.DiagnosticOrder.DiagnosticOrderEventComponent> _Event;
        
        /// <summary>
        /// The items the orderer requested
        /// </summary>
        [FhirElement("item", Order=160)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.DiagnosticOrder.DiagnosticOrderItemComponent> Item
        {
            get { return _Item; }
            set { _Item = value; OnPropertyChanged("Item"); }
        }
        private List<Hl7.Fhir.Model.DiagnosticOrder.DiagnosticOrderItemComponent> _Item;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as DiagnosticOrder;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Orderer != null) dest.Orderer = (Hl7.Fhir.Model.ResourceReference)Orderer.DeepCopy();
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
                if(ClinicalNotesElement != null) dest.ClinicalNotesElement = (Hl7.Fhir.Model.FhirString)ClinicalNotesElement.DeepCopy();
                if(Specimen != null) dest.Specimen = new List<Hl7.Fhir.Model.ResourceReference>(Specimen.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.DiagnosticOrder.DiagnosticOrderStatus>)StatusElement.DeepCopy();
                if(PriorityElement != null) dest.PriorityElement = (Code<Hl7.Fhir.Model.DiagnosticOrder.DiagnosticOrderPriority>)PriorityElement.DeepCopy();
                if(Event != null) dest.Event = new List<Hl7.Fhir.Model.DiagnosticOrder.DiagnosticOrderEventComponent>(Event.DeepCopy());
                if(Item != null) dest.Item = new List<Hl7.Fhir.Model.DiagnosticOrder.DiagnosticOrderItemComponent>(Item.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new DiagnosticOrder());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as DiagnosticOrder;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Orderer, otherT.Orderer)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.Matches(ClinicalNotesElement, otherT.ClinicalNotesElement)) return false;
            if( !DeepComparable.Matches(Specimen, otherT.Specimen)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(PriorityElement, otherT.PriorityElement)) return false;
            if( !DeepComparable.Matches(Event, otherT.Event)) return false;
            if( !DeepComparable.Matches(Item, otherT.Item)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as DiagnosticOrder;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Orderer, otherT.Orderer)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.IsExactly(ClinicalNotesElement, otherT.ClinicalNotesElement)) return false;
            if( !DeepComparable.IsExactly(Specimen, otherT.Specimen)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(PriorityElement, otherT.PriorityElement)) return false;
            if( !DeepComparable.IsExactly(Event, otherT.Event)) return false;
            if( !DeepComparable.IsExactly(Item, otherT.Item)) return false;
            
            return true;
        }
        
    }
    
}
