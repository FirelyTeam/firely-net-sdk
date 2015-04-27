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
    /// A request for a procedure to be performed
    /// </summary>
    [FhirType("ProcedureRequest", IsResource=true)]
    [DataContract]
    public partial class ProcedureRequest : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ProcedureRequest; } }
        [NotMapped]
        public override string TypeName { get { return "ProcedureRequest"; } }
        
        /// <summary>
        /// The priority of the request
        /// </summary>
        [FhirEnumeration("ProcedureRequestPriority")]
        public enum ProcedureRequestPriority
        {
            /// <summary>
            /// The request has a normal priority.
            /// </summary>
            [EnumLiteral("routine")]
            Routine,
            /// <summary>
            /// The request should be done urgently.
            /// </summary>
            [EnumLiteral("urgent")]
            Urgent,
            /// <summary>
            /// The request is time-critical.
            /// </summary>
            [EnumLiteral("stat")]
            Stat,
            /// <summary>
            /// The request should be acted on as soon as possible.
            /// </summary>
            [EnumLiteral("asap")]
            Asap,
        }
        
        /// <summary>
        /// The status of the request
        /// </summary>
        [FhirEnumeration("ProcedureRequestStatus")]
        public enum ProcedureRequestStatus
        {
            /// <summary>
            /// The request has been proposed.
            /// </summary>
            [EnumLiteral("proposed")]
            Proposed,
            /// <summary>
            /// The request is in preliminary form, prior to being requested.
            /// </summary>
            [EnumLiteral("draft")]
            Draft,
            /// <summary>
            /// The request has been placed.
            /// </summary>
            [EnumLiteral("requested")]
            Requested,
            /// <summary>
            /// The receiving system has received the request but not yet decided whether it will be performed.
            /// </summary>
            [EnumLiteral("received")]
            Received,
            /// <summary>
            /// The receiving system has accepted the request, but work has not yet commenced.
            /// </summary>
            [EnumLiteral("accepted")]
            Accepted,
            /// <summary>
            /// The work to fulfill the request is happening.
            /// </summary>
            [EnumLiteral("in-progress")]
            InProgress,
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
            /// The request was attempted, but due to some procedural error, it could not be completed.
            /// </summary>
            [EnumLiteral("aborted")]
            Aborted,
        }
        
        [FhirType("ProcedureRequestBodySiteComponent")]
        [DataContract]
        public partial class ProcedureRequestBodySiteComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ProcedureRequestBodySiteComponent"; } }
            
            /// <summary>
            /// Target body site
            /// </summary>
            [FhirElement("site", InSummary=true, Order=40, Choice=ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Site
            {
                get { return _Site; }
                set { _Site = value; OnPropertyChanged("Site"); }
            }
            
            private Hl7.Fhir.Model.Element _Site;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ProcedureRequestBodySiteComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Site != null) dest.Site = (Hl7.Fhir.Model.Element)Site.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ProcedureRequestBodySiteComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ProcedureRequestBodySiteComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Site, otherT.Site)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ProcedureRequestBodySiteComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Site, otherT.Site)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// Identifier
        /// </summary>
        [FhirElement("identifier", Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Subject
        /// </summary>
        [FhirElement("subject", Order=100)]
        [References("Patient")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Procedure Type
        /// </summary>
        [FhirElement("type", Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Type;
        
        /// <summary>
        /// Target body sites
        /// </summary>
        [FhirElement("bodySite", Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ProcedureRequest.ProcedureRequestBodySiteComponent> BodySite
        {
            get { if(_BodySite==null) _BodySite = new List<Hl7.Fhir.Model.ProcedureRequest.ProcedureRequestBodySiteComponent>(); return _BodySite; }
            set { _BodySite = value; OnPropertyChanged("BodySite"); }
        }
        
        private List<Hl7.Fhir.Model.ProcedureRequest.ProcedureRequestBodySiteComponent> _BodySite;
        
        /// <summary>
        /// Indication
        /// </summary>
        [FhirElement("indication", Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Indication
        {
            get { if(_Indication==null) _Indication = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Indication; }
            set { _Indication = value; OnPropertyChanged("Indication"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Indication;
        
        /// <summary>
        /// Procedure timing schedule
        /// </summary>
        [FhirElement("timing", Order=140, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.Timing))]
        [DataMember]
        public Hl7.Fhir.Model.Element Timing
        {
            get { return _Timing; }
            set { _Timing = value; OnPropertyChanged("Timing"); }
        }
        
        private Hl7.Fhir.Model.Element _Timing;
        
        /// <summary>
        /// Encounter
        /// </summary>
        [FhirElement("encounter", Order=150)]
        [References("Encounter")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Encounter
        {
            get { return _Encounter; }
            set { _Encounter = value; OnPropertyChanged("Encounter"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Encounter;
        
        /// <summary>
        /// Performer
        /// </summary>
        [FhirElement("performer", Order=160)]
        [References("Practitioner","Organization","Patient","RelatedPerson")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Performer
        {
            get { return _Performer; }
            set { _Performer = value; OnPropertyChanged("Performer"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Performer;
        
        /// <summary>
        /// proposed | draft | requested | received | accepted | in-progress | completed | suspended | rejected | aborted
        /// </summary>
        [FhirElement("status", Order=170)]
        [DataMember]
        public Code<Hl7.Fhir.Model.ProcedureRequest.ProcedureRequestStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.ProcedureRequest.ProcedureRequestStatus> _StatusElement;
        
        /// <summary>
        /// proposed | draft | requested | received | accepted | in-progress | completed | suspended | rejected | aborted
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.ProcedureRequest.ProcedureRequestStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if(value == null)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.ProcedureRequest.ProcedureRequestStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Notes
        /// </summary>
        [FhirElement("notes", Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> NotesElement
        {
            get { if(_NotesElement==null) _NotesElement = new List<Hl7.Fhir.Model.FhirString>(); return _NotesElement; }
            set { _NotesElement = value; OnPropertyChanged("NotesElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _NotesElement;
        
        /// <summary>
        /// Notes
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Notes
        {
            get { return NotesElement != null ? NotesElement.Select(elem => elem.Value) : null; }
            set
            {
                if(value == null)
                  NotesElement = null; 
                else
                  NotesElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("Notes");
            }
        }
        
        /// <summary>
        /// PRN
        /// </summary>
        [FhirElement("asNeeded", Order=190, Choice=ChoiceType.DatatypeChoice)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.CodeableConcept))]
        [DataMember]
        public Hl7.Fhir.Model.Element AsNeeded
        {
            get { return _AsNeeded; }
            set { _AsNeeded = value; OnPropertyChanged("AsNeeded"); }
        }
        
        private Hl7.Fhir.Model.Element _AsNeeded;
        
        /// <summary>
        /// When Requested
        /// </summary>
        [FhirElement("orderedOn", Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime OrderedOnElement
        {
            get { return _OrderedOnElement; }
            set { _OrderedOnElement = value; OnPropertyChanged("OrderedOnElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _OrderedOnElement;
        
        /// <summary>
        /// When Requested
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string OrderedOn
        {
            get { return OrderedOnElement != null ? OrderedOnElement.Value : null; }
            set
            {
                if(value == null)
                  OrderedOnElement = null; 
                else
                  OrderedOnElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("OrderedOn");
            }
        }
        
        /// <summary>
        /// Ordering Party
        /// </summary>
        [FhirElement("orderer", Order=210)]
        [References("Practitioner","Patient","RelatedPerson","Device")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Orderer
        {
            get { return _Orderer; }
            set { _Orderer = value; OnPropertyChanged("Orderer"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Orderer;
        
        /// <summary>
        /// routine | urgent | stat | asap
        /// </summary>
        [FhirElement("priority", Order=220)]
        [DataMember]
        public Code<Hl7.Fhir.Model.ProcedureRequest.ProcedureRequestPriority> PriorityElement
        {
            get { return _PriorityElement; }
            set { _PriorityElement = value; OnPropertyChanged("PriorityElement"); }
        }
        
        private Code<Hl7.Fhir.Model.ProcedureRequest.ProcedureRequestPriority> _PriorityElement;
        
        /// <summary>
        /// routine | urgent | stat | asap
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.ProcedureRequest.ProcedureRequestPriority? Priority
        {
            get { return PriorityElement != null ? PriorityElement.Value : null; }
            set
            {
                if(value == null)
                  PriorityElement = null; 
                else
                  PriorityElement = new Code<Hl7.Fhir.Model.ProcedureRequest.ProcedureRequestPriority>(value);
                OnPropertyChanged("Priority");
            }
        }
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ProcedureRequest;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(BodySite != null) dest.BodySite = new List<Hl7.Fhir.Model.ProcedureRequest.ProcedureRequestBodySiteComponent>(BodySite.DeepCopy());
                if(Indication != null) dest.Indication = new List<Hl7.Fhir.Model.CodeableConcept>(Indication.DeepCopy());
                if(Timing != null) dest.Timing = (Hl7.Fhir.Model.Element)Timing.DeepCopy();
                if(Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
                if(Performer != null) dest.Performer = (Hl7.Fhir.Model.ResourceReference)Performer.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.ProcedureRequest.ProcedureRequestStatus>)StatusElement.DeepCopy();
                if(NotesElement != null) dest.NotesElement = new List<Hl7.Fhir.Model.FhirString>(NotesElement.DeepCopy());
                if(AsNeeded != null) dest.AsNeeded = (Hl7.Fhir.Model.Element)AsNeeded.DeepCopy();
                if(OrderedOnElement != null) dest.OrderedOnElement = (Hl7.Fhir.Model.FhirDateTime)OrderedOnElement.DeepCopy();
                if(Orderer != null) dest.Orderer = (Hl7.Fhir.Model.ResourceReference)Orderer.DeepCopy();
                if(PriorityElement != null) dest.PriorityElement = (Code<Hl7.Fhir.Model.ProcedureRequest.ProcedureRequestPriority>)PriorityElement.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new ProcedureRequest());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ProcedureRequest;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(BodySite, otherT.BodySite)) return false;
            if( !DeepComparable.Matches(Indication, otherT.Indication)) return false;
            if( !DeepComparable.Matches(Timing, otherT.Timing)) return false;
            if( !DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(NotesElement, otherT.NotesElement)) return false;
            if( !DeepComparable.Matches(AsNeeded, otherT.AsNeeded)) return false;
            if( !DeepComparable.Matches(OrderedOnElement, otherT.OrderedOnElement)) return false;
            if( !DeepComparable.Matches(Orderer, otherT.Orderer)) return false;
            if( !DeepComparable.Matches(PriorityElement, otherT.PriorityElement)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ProcedureRequest;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(BodySite, otherT.BodySite)) return false;
            if( !DeepComparable.IsExactly(Indication, otherT.Indication)) return false;
            if( !DeepComparable.IsExactly(Timing, otherT.Timing)) return false;
            if( !DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(NotesElement, otherT.NotesElement)) return false;
            if( !DeepComparable.IsExactly(AsNeeded, otherT.AsNeeded)) return false;
            if( !DeepComparable.IsExactly(OrderedOnElement, otherT.OrderedOnElement)) return false;
            if( !DeepComparable.IsExactly(Orderer, otherT.Orderer)) return false;
            if( !DeepComparable.IsExactly(PriorityElement, otherT.PriorityElement)) return false;
            
            return true;
        }
        
    }
    
}
