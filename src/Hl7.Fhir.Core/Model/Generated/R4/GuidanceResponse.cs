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
// Generated for FHIR v4.0.1
//
namespace Hl7.Fhir.Model.R4
{
    /// <summary>
    /// The formal response to a guidance request
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "GuidanceResponse", IsResource=true)]
    [DataContract]
    public partial class GuidanceResponse : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IGuidanceResponse, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.GuidanceResponse; } }
        [NotMapped]
        public override string TypeName { get { return "GuidanceResponse"; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IDataRequirement> Hl7.Fhir.Model.IGuidanceResponse.DataRequirement { get { return DataRequirement; } }
    
        
        /// <summary>
        /// The identifier of the request associated with this response, if any
        /// </summary>
        [FhirElement("requestIdentifier", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier RequestIdentifier
        {
            get { return _RequestIdentifier; }
            set { _RequestIdentifier = value; OnPropertyChanged("RequestIdentifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _RequestIdentifier;
        
        /// <summary>
        /// Business identifier
        /// </summary>
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
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
        /// What guidance was requested
        /// </summary>
        [FhirElement("module", InSummary=Hl7.Fhir.Model.Version.All, Order=110, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.FhirUri),typeof(Hl7.Fhir.Model.Canonical),typeof(Hl7.Fhir.Model.CodeableConcept))]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Element Module
        {
            get { return _Module; }
            set { _Module = value; OnPropertyChanged("Module"); }
        }
        
        private Hl7.Fhir.Model.Element _Module;
        
        /// <summary>
        /// success | data-requested | data-required | in-progress | failure | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.GuidanceResponseStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.GuidanceResponseStatus> _StatusElement;
        
        /// <summary>
        /// success | data-requested | data-required | in-progress | failure | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.GuidanceResponseStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.GuidanceResponseStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Patient the request was performed for
        /// </summary>
        [FhirElement("subject", Order=130)]
        [CLSCompliant(false)]
        [References("Patient","Group")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Encounter during which the response was returned
        /// </summary>
        [FhirElement("encounter", Order=140)]
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
        /// When the guidance response was processed
        /// </summary>
        [FhirElement("occurrenceDateTime", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime OccurrenceDateTimeElement
        {
            get { return _OccurrenceDateTimeElement; }
            set { _OccurrenceDateTimeElement = value; OnPropertyChanged("OccurrenceDateTimeElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _OccurrenceDateTimeElement;
        
        /// <summary>
        /// When the guidance response was processed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string OccurrenceDateTime
        {
            get { return OccurrenceDateTimeElement != null ? OccurrenceDateTimeElement.Value : null; }
            set
            {
                if (value == null)
                    OccurrenceDateTimeElement = null;
                else
                    OccurrenceDateTimeElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("OccurrenceDateTime");
            }
        }
        
        /// <summary>
        /// Device returning the guidance
        /// </summary>
        [FhirElement("performer", Order=160)]
        [CLSCompliant(false)]
        [References("Device")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Performer
        {
            get { return _Performer; }
            set { _Performer = value; OnPropertyChanged("Performer"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Performer;
        
        /// <summary>
        /// Why guidance is needed
        /// </summary>
        [FhirElement("reasonCode", Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> ReasonCode
        {
            get { if(_ReasonCode==null) _ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ReasonCode; }
            set { _ReasonCode = value; OnPropertyChanged("ReasonCode"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _ReasonCode;
        
        /// <summary>
        /// Why guidance is needed
        /// </summary>
        [FhirElement("reasonReference", Order=180)]
        [CLSCompliant(false)]
        [References("Condition","Observation","DiagnosticReport","DocumentReference")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> ReasonReference
        {
            get { if(_ReasonReference==null) _ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(); return _ReasonReference; }
            set { _ReasonReference = value; OnPropertyChanged("ReasonReference"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _ReasonReference;
        
        /// <summary>
        /// Additional notes about the response
        /// </summary>
        [FhirElement("note", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Note;
        
        /// <summary>
        /// Messages resulting from the evaluation of the artifact or artifacts
        /// </summary>
        [FhirElement("evaluationMessage", Order=200)]
        [CLSCompliant(false)]
        [References("OperationOutcome")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> EvaluationMessage
        {
            get { if(_EvaluationMessage==null) _EvaluationMessage = new List<Hl7.Fhir.Model.ResourceReference>(); return _EvaluationMessage; }
            set { _EvaluationMessage = value; OnPropertyChanged("EvaluationMessage"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _EvaluationMessage;
        
        /// <summary>
        /// The output parameters of the evaluation, if any
        /// </summary>
        [FhirElement("outputParameters", Order=210)]
        [CLSCompliant(false)]
        [References("Parameters")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference OutputParameters
        {
            get { return _OutputParameters; }
            set { _OutputParameters = value; OnPropertyChanged("OutputParameters"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _OutputParameters;
        
        /// <summary>
        /// Proposed actions, if any
        /// </summary>
        [FhirElement("result", Order=220)]
        [CLSCompliant(false)]
        [References("CarePlan","RequestGroup")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Result
        {
            get { return _Result; }
            set { _Result = value; OnPropertyChanged("Result"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Result;
        
        /// <summary>
        /// Additional required data
        /// </summary>
        [FhirElement("dataRequirement", Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.R4.DataRequirement> DataRequirement
        {
            get { if(_DataRequirement==null) _DataRequirement = new List<Hl7.Fhir.Model.R4.DataRequirement>(); return _DataRequirement; }
            set { _DataRequirement = value; OnPropertyChanged("DataRequirement"); }
        }
        
        private List<Hl7.Fhir.Model.R4.DataRequirement> _DataRequirement;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as GuidanceResponse;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(RequestIdentifier != null) dest.RequestIdentifier = (Hl7.Fhir.Model.Identifier)RequestIdentifier.DeepCopy();
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(Module != null) dest.Module = (Hl7.Fhir.Model.Element)Module.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.GuidanceResponseStatus>)StatusElement.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
                if(OccurrenceDateTimeElement != null) dest.OccurrenceDateTimeElement = (Hl7.Fhir.Model.FhirDateTime)OccurrenceDateTimeElement.DeepCopy();
                if(Performer != null) dest.Performer = (Hl7.Fhir.Model.ResourceReference)Performer.DeepCopy();
                if(ReasonCode != null) dest.ReasonCode = new List<Hl7.Fhir.Model.CodeableConcept>(ReasonCode.DeepCopy());
                if(ReasonReference != null) dest.ReasonReference = new List<Hl7.Fhir.Model.ResourceReference>(ReasonReference.DeepCopy());
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                if(EvaluationMessage != null) dest.EvaluationMessage = new List<Hl7.Fhir.Model.ResourceReference>(EvaluationMessage.DeepCopy());
                if(OutputParameters != null) dest.OutputParameters = (Hl7.Fhir.Model.ResourceReference)OutputParameters.DeepCopy();
                if(Result != null) dest.Result = (Hl7.Fhir.Model.ResourceReference)Result.DeepCopy();
                if(DataRequirement != null) dest.DataRequirement = new List<Hl7.Fhir.Model.R4.DataRequirement>(DataRequirement.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new GuidanceResponse());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as GuidanceResponse;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(RequestIdentifier, otherT.RequestIdentifier)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Module, otherT.Module)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.Matches(OccurrenceDateTimeElement, otherT.OccurrenceDateTimeElement)) return false;
            if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
            if( !DeepComparable.Matches(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.Matches(ReasonReference, otherT.ReasonReference)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            if( !DeepComparable.Matches(EvaluationMessage, otherT.EvaluationMessage)) return false;
            if( !DeepComparable.Matches(OutputParameters, otherT.OutputParameters)) return false;
            if( !DeepComparable.Matches(Result, otherT.Result)) return false;
            if( !DeepComparable.Matches(DataRequirement, otherT.DataRequirement)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as GuidanceResponse;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(RequestIdentifier, otherT.RequestIdentifier)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Module, otherT.Module)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.IsExactly(OccurrenceDateTimeElement, otherT.OccurrenceDateTimeElement)) return false;
            if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
            if( !DeepComparable.IsExactly(ReasonCode, otherT.ReasonCode)) return false;
            if( !DeepComparable.IsExactly(ReasonReference, otherT.ReasonReference)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            if( !DeepComparable.IsExactly(EvaluationMessage, otherT.EvaluationMessage)) return false;
            if( !DeepComparable.IsExactly(OutputParameters, otherT.OutputParameters)) return false;
            if( !DeepComparable.IsExactly(Result, otherT.Result)) return false;
            if( !DeepComparable.IsExactly(DataRequirement, otherT.DataRequirement)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("GuidanceResponse");
            base.Serialize(sink);
            sink.Element("requestIdentifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); RequestIdentifier?.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("module", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, true); Module?.Serialize(sink);
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("subject", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Subject?.Serialize(sink);
            sink.Element("encounter", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Encounter?.Serialize(sink);
            sink.Element("occurrenceDateTime", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); OccurrenceDateTimeElement?.Serialize(sink);
            sink.Element("performer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Performer?.Serialize(sink);
            sink.BeginList("reasonCode", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in ReasonCode)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("reasonReference", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in ReasonReference)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("note", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Note)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("evaluationMessage", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in EvaluationMessage)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("outputParameters", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); OutputParameters?.Serialize(sink);
            sink.Element("result", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Result?.Serialize(sink);
            sink.BeginList("dataRequirement", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in DataRequirement)
            {
                item?.Serialize(sink);
            }
            sink.End();
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
                case "requestIdentifier":
                    RequestIdentifier = source.Populate(RequestIdentifier);
                    return true;
                case "identifier":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "moduleUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Module, "module");
                    Module = source.PopulateValue(Module as Hl7.Fhir.Model.FhirUri);
                    return true;
                case "_moduleUri":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirUri>(Module, "module");
                    Module = source.Populate(Module as Hl7.Fhir.Model.FhirUri);
                    return true;
                case "moduleCanonical":
                    source.CheckDuplicates<Hl7.Fhir.Model.Canonical>(Module, "module");
                    Module = source.PopulateValue(Module as Hl7.Fhir.Model.Canonical);
                    return true;
                case "_moduleCanonical":
                    source.CheckDuplicates<Hl7.Fhir.Model.Canonical>(Module, "module");
                    Module = source.Populate(Module as Hl7.Fhir.Model.Canonical);
                    return true;
                case "moduleCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Module, "module");
                    Module = source.Populate(Module as Hl7.Fhir.Model.CodeableConcept);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "subject":
                    Subject = source.Populate(Subject);
                    return true;
                case "encounter":
                    Encounter = source.Populate(Encounter);
                    return true;
                case "occurrenceDateTime":
                    OccurrenceDateTimeElement = source.PopulateValue(OccurrenceDateTimeElement);
                    return true;
                case "_occurrenceDateTime":
                    OccurrenceDateTimeElement = source.Populate(OccurrenceDateTimeElement);
                    return true;
                case "performer":
                    Performer = source.Populate(Performer);
                    return true;
                case "reasonCode":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "reasonReference":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "note":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "evaluationMessage":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "outputParameters":
                    OutputParameters = source.Populate(OutputParameters);
                    return true;
                case "result":
                    Result = source.Populate(Result);
                    return true;
                case "dataRequirement":
                    source.SetList(this, jsonPropertyName);
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
                case "reasonCode":
                    source.PopulateListItem(ReasonCode, index);
                    return true;
                case "reasonReference":
                    source.PopulateListItem(ReasonReference, index);
                    return true;
                case "note":
                    source.PopulateListItem(Note, index);
                    return true;
                case "evaluationMessage":
                    source.PopulateListItem(EvaluationMessage, index);
                    return true;
                case "dataRequirement":
                    source.PopulateListItem(DataRequirement, index);
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
                if (RequestIdentifier != null) yield return RequestIdentifier;
                foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                if (Module != null) yield return Module;
                if (StatusElement != null) yield return StatusElement;
                if (Subject != null) yield return Subject;
                if (Encounter != null) yield return Encounter;
                if (OccurrenceDateTimeElement != null) yield return OccurrenceDateTimeElement;
                if (Performer != null) yield return Performer;
                foreach (var elem in ReasonCode) { if (elem != null) yield return elem; }
                foreach (var elem in ReasonReference) { if (elem != null) yield return elem; }
                foreach (var elem in Note) { if (elem != null) yield return elem; }
                foreach (var elem in EvaluationMessage) { if (elem != null) yield return elem; }
                if (OutputParameters != null) yield return OutputParameters;
                if (Result != null) yield return Result;
                foreach (var elem in DataRequirement) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (RequestIdentifier != null) yield return new ElementValue("requestIdentifier", RequestIdentifier);
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (Module != null) yield return new ElementValue("module", Module);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Encounter != null) yield return new ElementValue("encounter", Encounter);
                if (OccurrenceDateTimeElement != null) yield return new ElementValue("occurrenceDateTime", OccurrenceDateTimeElement);
                if (Performer != null) yield return new ElementValue("performer", Performer);
                foreach (var elem in ReasonCode) { if (elem != null) yield return new ElementValue("reasonCode", elem); }
                foreach (var elem in ReasonReference) { if (elem != null) yield return new ElementValue("reasonReference", elem); }
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                foreach (var elem in EvaluationMessage) { if (elem != null) yield return new ElementValue("evaluationMessage", elem); }
                if (OutputParameters != null) yield return new ElementValue("outputParameters", OutputParameters);
                if (Result != null) yield return new ElementValue("result", Result);
                foreach (var elem in DataRequirement) { if (elem != null) yield return new ElementValue("dataRequirement", elem); }
            }
        }
    
    }

}
