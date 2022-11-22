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
namespace Hl7.Fhir.Model.DSTU2
{
    /// <summary>
    /// A request for a procedure to be performed
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.DSTU2, "ProcedureRequest", IsResource=true)]
    [DataContract]
    public partial class ProcedureRequest : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IProcedureRequest, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ProcedureRequest; } }
        [NotMapped]
        public override string TypeName { get { return "ProcedureRequest"; } }
    
        
        /// <summary>
        /// Unique identifier for the request
        /// </summary>
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Who the procedure should be done to
        /// </summary>
        [FhirElement("subject", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [References("Patient","Group")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// What procedure to perform
        /// </summary>
        [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Code
        {
            get { return _Code; }
            set { _Code = value; OnPropertyChanged("Code"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Code;
        
        /// <summary>
        /// What part of body to perform on
        /// </summary>
        [FhirElement("bodySite", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> BodySite
        {
            get { if(_BodySite==null) _BodySite = new List<Hl7.Fhir.Model.CodeableConcept>(); return _BodySite; }
            set { _BodySite = value; OnPropertyChanged("BodySite"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _BodySite;
        
        /// <summary>
        /// Why procedure should occur
        /// </summary>
        [FhirElement("reason", InSummary=Hl7.Fhir.Model.Version.All, Order=130, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Reason
        {
            get { return _Reason; }
            set { _Reason = value; OnPropertyChanged("Reason"); }
        }
        
        private Hl7.Fhir.Model.Element _Reason;
        
        /// <summary>
        /// When procedure should occur
        /// </summary>
        [FhirElement("scheduled", InSummary=Hl7.Fhir.Model.Version.All, Order=140, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.DSTU2.Timing))]
        [DataMember]
        public Hl7.Fhir.Model.Element Scheduled
        {
            get { return _Scheduled; }
            set { _Scheduled = value; OnPropertyChanged("Scheduled"); }
        }
        
        private Hl7.Fhir.Model.Element _Scheduled;
        
        /// <summary>
        /// Encounter request created during
        /// </summary>
        [FhirElement("encounter", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [References("Encounter")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Encounter
        {
            get { return _Encounter; }
            set { _Encounter = value; OnPropertyChanged("Encounter"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Encounter;
        
        /// <summary>
        /// Who should perform the procedure
        /// </summary>
        [FhirElement("performer", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
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
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DSTU2.ProcedureRequestStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.DSTU2.ProcedureRequestStatus> _StatusElement;
        
        /// <summary>
        /// proposed | draft | requested | received | accepted | in-progress | completed | suspended | rejected | aborted
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DSTU2.ProcedureRequestStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.DSTU2.ProcedureRequestStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Additional information about desired procedure
        /// </summary>
        [FhirElement("notes", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Notes
        {
            get { if(_Notes==null) _Notes = new List<Hl7.Fhir.Model.Annotation>(); return _Notes; }
            set { _Notes = value; OnPropertyChanged("Notes"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Notes;
        
        /// <summary>
        /// Preconditions for procedure
        /// </summary>
        [FhirElement("asNeeded", InSummary=Hl7.Fhir.Model.Version.All, Order=190, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.CodeableConcept))]
        [DataMember]
        public Hl7.Fhir.Model.Element AsNeeded
        {
            get { return _AsNeeded; }
            set { _AsNeeded = value; OnPropertyChanged("AsNeeded"); }
        }
        
        private Hl7.Fhir.Model.Element _AsNeeded;
        
        /// <summary>
        /// When request was created
        /// </summary>
        [FhirElement("orderedOn", InSummary=Hl7.Fhir.Model.Version.All, Order=200)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime OrderedOnElement
        {
            get { return _OrderedOnElement; }
            set { _OrderedOnElement = value; OnPropertyChanged("OrderedOnElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _OrderedOnElement;
        
        /// <summary>
        /// When request was created
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string OrderedOn
        {
            get { return OrderedOnElement != null ? OrderedOnElement.Value : null; }
            set
            {
                if (value == null)
                    OrderedOnElement = null;
                else
                    OrderedOnElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("OrderedOn");
            }
        }
        
        /// <summary>
        /// Who made request
        /// </summary>
        [FhirElement("orderer", InSummary=Hl7.Fhir.Model.Version.All, Order=210)]
        [CLSCompliant(false)]
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
        [FhirElement("priority", InSummary=Hl7.Fhir.Model.Version.All, Order=220)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.DSTU2.ProcedureRequestPriority> PriorityElement
        {
            get { return _PriorityElement; }
            set { _PriorityElement = value; OnPropertyChanged("PriorityElement"); }
        }
        
        private Code<Hl7.Fhir.Model.DSTU2.ProcedureRequestPriority> _PriorityElement;
        
        /// <summary>
        /// routine | urgent | stat | asap
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.DSTU2.ProcedureRequestPriority? Priority
        {
            get { return PriorityElement != null ? PriorityElement.Value : null; }
            set
            {
                if (value == null)
                    PriorityElement = null;
                else
                    PriorityElement = new Code<Hl7.Fhir.Model.DSTU2.ProcedureRequestPriority>(value);
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
                if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                if(BodySite != null) dest.BodySite = new List<Hl7.Fhir.Model.CodeableConcept>(BodySite.DeepCopy());
                if(Reason != null) dest.Reason = (Hl7.Fhir.Model.Element)Reason.DeepCopy();
                if(Scheduled != null) dest.Scheduled = (Hl7.Fhir.Model.Element)Scheduled.DeepCopy();
                if(Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
                if(Performer != null) dest.Performer = (Hl7.Fhir.Model.ResourceReference)Performer.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.DSTU2.ProcedureRequestStatus>)StatusElement.DeepCopy();
                if(Notes != null) dest.Notes = new List<Hl7.Fhir.Model.Annotation>(Notes.DeepCopy());
                if(AsNeeded != null) dest.AsNeeded = (Hl7.Fhir.Model.Element)AsNeeded.DeepCopy();
                if(OrderedOnElement != null) dest.OrderedOnElement = (Hl7.Fhir.Model.FhirDateTime)OrderedOnElement.DeepCopy();
                if(Orderer != null) dest.Orderer = (Hl7.Fhir.Model.ResourceReference)Orderer.DeepCopy();
                if(PriorityElement != null) dest.PriorityElement = (Code<Hl7.Fhir.Model.DSTU2.ProcedureRequestPriority>)PriorityElement.DeepCopy();
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
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(BodySite, otherT.BodySite)) return false;
            if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
            if( !DeepComparable.Matches(Scheduled, otherT.Scheduled)) return false;
            if( !DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Notes, otherT.Notes)) return false;
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
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(BodySite, otherT.BodySite)) return false;
            if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
            if( !DeepComparable.IsExactly(Scheduled, otherT.Scheduled)) return false;
            if( !DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Notes, otherT.Notes)) return false;
            if( !DeepComparable.IsExactly(AsNeeded, otherT.AsNeeded)) return false;
            if( !DeepComparable.IsExactly(OrderedOnElement, otherT.OrderedOnElement)) return false;
            if( !DeepComparable.IsExactly(Orderer, otherT.Orderer)) return false;
            if( !DeepComparable.IsExactly(PriorityElement, otherT.PriorityElement)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("ProcedureRequest");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("subject", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Subject?.Serialize(sink);
            sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Code?.Serialize(sink);
            sink.BeginList("bodySite", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in BodySite)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("reason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Reason?.Serialize(sink);
            sink.Element("scheduled", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Scheduled?.Serialize(sink);
            sink.Element("encounter", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Encounter?.Serialize(sink);
            sink.Element("performer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Performer?.Serialize(sink);
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); StatusElement?.Serialize(sink);
            sink.BeginList("notes", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Notes)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("asNeeded", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); AsNeeded?.Serialize(sink);
            sink.Element("orderedOn", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); OrderedOnElement?.Serialize(sink);
            sink.Element("orderer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Orderer?.Serialize(sink);
            sink.Element("priority", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PriorityElement?.Serialize(sink);
            sink.End();
        }
    
        internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
        {
            if (base.SetElementFromJson(jsonPropertyName, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "identifier":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "subject":
                    Subject = source.Populate(Subject);
                    return true;
                case "code":
                    Code = source.Populate(Code);
                    return true;
                case "bodySite":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "reasonCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Reason, "reason");
                    Reason = source.Populate(Reason as Hl7.Fhir.Model.CodeableConcept);
                    return true;
                case "reasonReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Reason, "reason");
                    Reason = source.Populate(Reason as Hl7.Fhir.Model.ResourceReference);
                    return true;
                case "scheduledDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Scheduled, "scheduled");
                    Scheduled = source.PopulateValue(Scheduled as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "_scheduledDateTime":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Scheduled, "scheduled");
                    Scheduled = source.Populate(Scheduled as Hl7.Fhir.Model.FhirDateTime);
                    return true;
                case "scheduledPeriod":
                    source.CheckDuplicates<Hl7.Fhir.Model.Period>(Scheduled, "scheduled");
                    Scheduled = source.Populate(Scheduled as Hl7.Fhir.Model.Period);
                    return true;
                case "scheduledTiming":
                    source.CheckDuplicates<Hl7.Fhir.Model.DSTU2.Timing>(Scheduled, "scheduled");
                    Scheduled = source.Populate(Scheduled as Hl7.Fhir.Model.DSTU2.Timing);
                    return true;
                case "encounter":
                    Encounter = source.Populate(Encounter);
                    return true;
                case "performer":
                    Performer = source.Populate(Performer);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "notes":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "asNeededBoolean":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(AsNeeded, "asNeeded");
                    AsNeeded = source.PopulateValue(AsNeeded as Hl7.Fhir.Model.FhirBoolean);
                    return true;
                case "_asNeededBoolean":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirBoolean>(AsNeeded, "asNeeded");
                    AsNeeded = source.Populate(AsNeeded as Hl7.Fhir.Model.FhirBoolean);
                    return true;
                case "asNeededCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(AsNeeded, "asNeeded");
                    AsNeeded = source.Populate(AsNeeded as Hl7.Fhir.Model.CodeableConcept);
                    return true;
                case "orderedOn":
                    OrderedOnElement = source.PopulateValue(OrderedOnElement);
                    return true;
                case "_orderedOn":
                    OrderedOnElement = source.Populate(OrderedOnElement);
                    return true;
                case "orderer":
                    Orderer = source.Populate(Orderer);
                    return true;
                case "priority":
                    PriorityElement = source.PopulateValue(PriorityElement);
                    return true;
                case "_priority":
                    PriorityElement = source.Populate(PriorityElement);
                    return true;
            }
            return false;
        }
        
        internal override bool SetListElementFromJson(string jsonPropertyName, int index, ref Serialization.JsonSource source)
        {
            if (base.SetListElementFromJson(jsonPropertyName, index, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "identifier":
                    source.PopulateListItem(Identifier, index);
                    return true;
                case "bodySite":
                    source.PopulateListItem(BodySite, index);
                    return true;
                case "notes":
                    source.PopulateListItem(Notes, index);
                    return true;
            }
            return false;
        }
    
        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                if (Subject != null) yield return Subject;
                if (Code != null) yield return Code;
                foreach (var elem in BodySite) { if (elem != null) yield return elem; }
                if (Reason != null) yield return Reason;
                if (Scheduled != null) yield return Scheduled;
                if (Encounter != null) yield return Encounter;
                if (Performer != null) yield return Performer;
                if (StatusElement != null) yield return StatusElement;
                foreach (var elem in Notes) { if (elem != null) yield return elem; }
                if (AsNeeded != null) yield return AsNeeded;
                if (OrderedOnElement != null) yield return OrderedOnElement;
                if (Orderer != null) yield return Orderer;
                if (PriorityElement != null) yield return PriorityElement;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Code != null) yield return new ElementValue("code", Code);
                foreach (var elem in BodySite) { if (elem != null) yield return new ElementValue("bodySite", elem); }
                if (Reason != null) yield return new ElementValue("reason", Reason);
                if (Scheduled != null) yield return new ElementValue("scheduled", Scheduled);
                if (Encounter != null) yield return new ElementValue("encounter", Encounter);
                if (Performer != null) yield return new ElementValue("performer", Performer);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                foreach (var elem in Notes) { if (elem != null) yield return new ElementValue("notes", elem); }
                if (AsNeeded != null) yield return new ElementValue("asNeeded", AsNeeded);
                if (OrderedOnElement != null) yield return new ElementValue("orderedOn", OrderedOnElement);
                if (Orderer != null) yield return new ElementValue("orderer", Orderer);
                if (PriorityElement != null) yield return new ElementValue("priority", PriorityElement);
            }
        }
    
    }

}
