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
// Generated for FHIR v3.0.1
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// The formal response to a guidance request
    /// </summary>
    [FhirType("GuidanceResponse", IsResource=true)]
    [DataContract]
    public partial class GuidanceResponse : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.GuidanceResponse; } }
        [NotMapped]
        public override string TypeName { get { return "GuidanceResponse"; } }
        
        /// <summary>
        /// The status of a guidance response
        /// (url: http://hl7.org/fhir/ValueSet/guidance-response-status)
        /// </summary>
        [FhirEnumeration("GuidanceResponseStatus")]
        public enum GuidanceResponseStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/guidance-response-status)
            /// </summary>
            [EnumLiteral("success", "http://hl7.org/fhir/guidance-response-status"), Description("Success")]
            Success,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/guidance-response-status)
            /// </summary>
            [EnumLiteral("data-requested", "http://hl7.org/fhir/guidance-response-status"), Description("Data Requested")]
            DataRequested,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/guidance-response-status)
            /// </summary>
            [EnumLiteral("data-required", "http://hl7.org/fhir/guidance-response-status"), Description("Data Required")]
            DataRequired,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/guidance-response-status)
            /// </summary>
            [EnumLiteral("in-progress", "http://hl7.org/fhir/guidance-response-status"), Description("In Progress")]
            InProgress,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/guidance-response-status)
            /// </summary>
            [EnumLiteral("failure", "http://hl7.org/fhir/guidance-response-status"), Description("Failure")]
            Failure,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/guidance-response-status)
            /// </summary>
            [EnumLiteral("entered-in-error", "http://hl7.org/fhir/guidance-response-status"), Description("Entered In Error")]
            EnteredInError,
        }

        /// <summary>
        /// The id of the request associated with this response, if any
        /// </summary>
        [FhirElement("requestId", InSummary=true, Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.Id RequestIdElement
        {
            get { return _RequestIdElement; }
            set { _RequestIdElement = value; OnPropertyChanged("RequestIdElement"); }
        }
        
        private Hl7.Fhir.Model.Id _RequestIdElement;
        
        /// <summary>
        /// The id of the request associated with this response, if any
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string RequestId
        {
            get { return RequestIdElement != null ? RequestIdElement.Value : null; }
            set
            {
                if (value == null)
                  RequestIdElement = null; 
                else
                  RequestIdElement = new Hl7.Fhir.Model.Id(value);
                OnPropertyChanged("RequestId");
            }
        }
        
        /// <summary>
        /// Business identifier
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// A reference to a knowledge module
        /// </summary>
        [FhirElement("module", InSummary=true, Order=110)]
        [CLSCompliant(false)]
		[References("ServiceDefinition")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Module
        {
            get { return _Module; }
            set { _Module = value; OnPropertyChanged("Module"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Module;
        
        /// <summary>
        /// success | data-requested | data-required | in-progress | failure | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary=true, Order=120)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.GuidanceResponse.GuidanceResponseStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.GuidanceResponse.GuidanceResponseStatus> _StatusElement;
        
        /// <summary>
        /// success | data-requested | data-required | in-progress | failure | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.GuidanceResponse.GuidanceResponseStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.GuidanceResponse.GuidanceResponseStatus>(value);
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
        /// Encounter or Episode during which the response was returned
        /// </summary>
        [FhirElement("context", Order=140)]
        [CLSCompliant(false)]
		[References("Encounter","EpisodeOfCare")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Context
        {
            get { return _Context; }
            set { _Context = value; OnPropertyChanged("Context"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Context;
        
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
        /// Reason for the response
        /// </summary>
        [FhirElement("reason", Order=170, Choice=ChoiceType.DatatypeChoice)]
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
        /// Additional notes about the response
        /// </summary>
        [FhirElement("note", Order=180)]
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
        [FhirElement("evaluationMessage", Order=190)]
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
        [FhirElement("outputParameters", Order=200)]
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
        [FhirElement("result", Order=210)]
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
        [FhirElement("dataRequirement", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<DataRequirement> DataRequirement
        {
            get { if(_DataRequirement==null) _DataRequirement = new List<DataRequirement>(); return _DataRequirement; }
            set { _DataRequirement = value; OnPropertyChanged("DataRequirement"); }
        }
        
        private List<DataRequirement> _DataRequirement;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as GuidanceResponse;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(RequestIdElement != null) dest.RequestIdElement = (Hl7.Fhir.Model.Id)RequestIdElement.DeepCopy();
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(Module != null) dest.Module = (Hl7.Fhir.Model.ResourceReference)Module.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.GuidanceResponse.GuidanceResponseStatus>)StatusElement.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Context != null) dest.Context = (Hl7.Fhir.Model.ResourceReference)Context.DeepCopy();
                if(OccurrenceDateTimeElement != null) dest.OccurrenceDateTimeElement = (Hl7.Fhir.Model.FhirDateTime)OccurrenceDateTimeElement.DeepCopy();
                if(Performer != null) dest.Performer = (Hl7.Fhir.Model.ResourceReference)Performer.DeepCopy();
                if(Reason != null) dest.Reason = (Hl7.Fhir.Model.Element)Reason.DeepCopy();
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                if(EvaluationMessage != null) dest.EvaluationMessage = new List<Hl7.Fhir.Model.ResourceReference>(EvaluationMessage.DeepCopy());
                if(OutputParameters != null) dest.OutputParameters = (Hl7.Fhir.Model.ResourceReference)OutputParameters.DeepCopy();
                if(Result != null) dest.Result = (Hl7.Fhir.Model.ResourceReference)Result.DeepCopy();
                if(DataRequirement != null) dest.DataRequirement = new List<DataRequirement>(DataRequirement.DeepCopy());
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
            if( !DeepComparable.Matches(RequestIdElement, otherT.RequestIdElement)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(Module, otherT.Module)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Context, otherT.Context)) return false;
            if( !DeepComparable.Matches(OccurrenceDateTimeElement, otherT.OccurrenceDateTimeElement)) return false;
            if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
            if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
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
            if( !DeepComparable.IsExactly(RequestIdElement, otherT.RequestIdElement)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(Module, otherT.Module)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Context, otherT.Context)) return false;
            if( !DeepComparable.IsExactly(OccurrenceDateTimeElement, otherT.OccurrenceDateTimeElement)) return false;
            if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
            if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            if( !DeepComparable.IsExactly(EvaluationMessage, otherT.EvaluationMessage)) return false;
            if( !DeepComparable.IsExactly(OutputParameters, otherT.OutputParameters)) return false;
            if( !DeepComparable.IsExactly(Result, otherT.Result)) return false;
            if( !DeepComparable.IsExactly(DataRequirement, otherT.DataRequirement)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				if (RequestIdElement != null) yield return RequestIdElement;
				if (Identifier != null) yield return Identifier;
				if (Module != null) yield return Module;
				if (StatusElement != null) yield return StatusElement;
				if (Subject != null) yield return Subject;
				if (Context != null) yield return Context;
				if (OccurrenceDateTimeElement != null) yield return OccurrenceDateTimeElement;
				if (Performer != null) yield return Performer;
				if (Reason != null) yield return Reason;
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
                if (RequestIdElement != null) yield return new ElementValue("requestId", RequestIdElement);
                if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                if (Module != null) yield return new ElementValue("module", Module);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Context != null) yield return new ElementValue("context", Context);
                if (OccurrenceDateTimeElement != null) yield return new ElementValue("occurrenceDateTime", OccurrenceDateTimeElement);
                if (Performer != null) yield return new ElementValue("performer", Performer);
                if (Reason != null) yield return new ElementValue("reason", Reason);
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                foreach (var elem in EvaluationMessage) { if (elem != null) yield return new ElementValue("evaluationMessage", elem); }
                if (OutputParameters != null) yield return new ElementValue("outputParameters", OutputParameters);
                if (Result != null) yield return new ElementValue("result", Result);
                foreach (var elem in DataRequirement) { if (elem != null) yield return new ElementValue("dataRequirement", elem); }
            }
        }

    }
    
}
