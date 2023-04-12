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
    /// Immunization evaluation information
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "ImmunizationEvaluation", IsResource=true)]
    [DataContract]
    public partial class ImmunizationEvaluation : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ImmunizationEvaluation; } }
        [NotMapped]
        public override string TypeName { get { return "ImmunizationEvaluation"; } }
    
        
        /// <summary>
        /// Business identifier
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
        /// completed | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.R4.ImmunizationEvaluationStatusCodes> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.R4.ImmunizationEvaluationStatusCodes> _StatusElement;
        
        /// <summary>
        /// completed | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.R4.ImmunizationEvaluationStatusCodes? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.R4.ImmunizationEvaluationStatusCodes>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Who this evaluation is for
        /// </summary>
        [FhirElement("patient", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [References("Patient")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient
        {
            get { return _Patient; }
            set { _Patient = value; OnPropertyChanged("Patient"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Patient;
        
        /// <summary>
        /// Date evaluation was performed
        /// </summary>
        [FhirElement("date", Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// Date evaluation was performed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Date
        {
            get { return DateElement != null ? DateElement.Value : null; }
            set
            {
                if (value == null)
                    DateElement = null;
                else
                    DateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Date");
            }
        }
        
        /// <summary>
        /// Who is responsible for publishing the recommendations
        /// </summary>
        [FhirElement("authority", Order=130)]
        [CLSCompliant(false)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Authority
        {
            get { return _Authority; }
            set { _Authority = value; OnPropertyChanged("Authority"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Authority;
        
        /// <summary>
        /// Evaluation target disease
        /// </summary>
        [FhirElement("targetDisease", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept TargetDisease
        {
            get { return _TargetDisease; }
            set { _TargetDisease = value; OnPropertyChanged("TargetDisease"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _TargetDisease;
        
        /// <summary>
        /// Immunization being evaluated
        /// </summary>
        [FhirElement("immunizationEvent", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [References("Immunization")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference ImmunizationEvent
        {
            get { return _ImmunizationEvent; }
            set { _ImmunizationEvent = value; OnPropertyChanged("ImmunizationEvent"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _ImmunizationEvent;
        
        /// <summary>
        /// Status of the dose relative to published recommendations
        /// </summary>
        [FhirElement("doseStatus", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept DoseStatus
        {
            get { return _DoseStatus; }
            set { _DoseStatus = value; OnPropertyChanged("DoseStatus"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _DoseStatus;
        
        /// <summary>
        /// Reason for the dose status
        /// </summary>
        [FhirElement("doseStatusReason", Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> DoseStatusReason
        {
            get { if(_DoseStatusReason==null) _DoseStatusReason = new List<Hl7.Fhir.Model.CodeableConcept>(); return _DoseStatusReason; }
            set { _DoseStatusReason = value; OnPropertyChanged("DoseStatusReason"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _DoseStatusReason;
        
        /// <summary>
        /// Evaluation notes
        /// </summary>
        [FhirElement("description", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
        /// <summary>
        /// Evaluation notes
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
        /// Name of vaccine series
        /// </summary>
        [FhirElement("series", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString SeriesElement
        {
            get { return _SeriesElement; }
            set { _SeriesElement = value; OnPropertyChanged("SeriesElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _SeriesElement;
        
        /// <summary>
        /// Name of vaccine series
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Series
        {
            get { return SeriesElement != null ? SeriesElement.Value : null; }
            set
            {
                if (value == null)
                    SeriesElement = null;
                else
                    SeriesElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Series");
            }
        }
        
        /// <summary>
        /// Dose number within series
        /// </summary>
        [FhirElement("doseNumber", Order=200, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.FhirString))]
        [DataMember]
        public Hl7.Fhir.Model.Element DoseNumber
        {
            get { return _DoseNumber; }
            set { _DoseNumber = value; OnPropertyChanged("DoseNumber"); }
        }
        
        private Hl7.Fhir.Model.Element _DoseNumber;
        
        /// <summary>
        /// Recommended number of doses for immunity
        /// </summary>
        [FhirElement("seriesDoses", Order=210, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.PositiveInt),typeof(Hl7.Fhir.Model.FhirString))]
        [DataMember]
        public Hl7.Fhir.Model.Element SeriesDoses
        {
            get { return _SeriesDoses; }
            set { _SeriesDoses = value; OnPropertyChanged("SeriesDoses"); }
        }
        
        private Hl7.Fhir.Model.Element _SeriesDoses;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ImmunizationEvaluation;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.R4.ImmunizationEvaluationStatusCodes>)StatusElement.DeepCopy();
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(Authority != null) dest.Authority = (Hl7.Fhir.Model.ResourceReference)Authority.DeepCopy();
                if(TargetDisease != null) dest.TargetDisease = (Hl7.Fhir.Model.CodeableConcept)TargetDisease.DeepCopy();
                if(ImmunizationEvent != null) dest.ImmunizationEvent = (Hl7.Fhir.Model.ResourceReference)ImmunizationEvent.DeepCopy();
                if(DoseStatus != null) dest.DoseStatus = (Hl7.Fhir.Model.CodeableConcept)DoseStatus.DeepCopy();
                if(DoseStatusReason != null) dest.DoseStatusReason = new List<Hl7.Fhir.Model.CodeableConcept>(DoseStatusReason.DeepCopy());
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(SeriesElement != null) dest.SeriesElement = (Hl7.Fhir.Model.FhirString)SeriesElement.DeepCopy();
                if(DoseNumber != null) dest.DoseNumber = (Hl7.Fhir.Model.Element)DoseNumber.DeepCopy();
                if(SeriesDoses != null) dest.SeriesDoses = (Hl7.Fhir.Model.Element)SeriesDoses.DeepCopy();
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new ImmunizationEvaluation());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ImmunizationEvaluation;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(Authority, otherT.Authority)) return false;
            if( !DeepComparable.Matches(TargetDisease, otherT.TargetDisease)) return false;
            if( !DeepComparable.Matches(ImmunizationEvent, otherT.ImmunizationEvent)) return false;
            if( !DeepComparable.Matches(DoseStatus, otherT.DoseStatus)) return false;
            if( !DeepComparable.Matches(DoseStatusReason, otherT.DoseStatusReason)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(SeriesElement, otherT.SeriesElement)) return false;
            if( !DeepComparable.Matches(DoseNumber, otherT.DoseNumber)) return false;
            if( !DeepComparable.Matches(SeriesDoses, otherT.SeriesDoses)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ImmunizationEvaluation;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(Authority, otherT.Authority)) return false;
            if( !DeepComparable.IsExactly(TargetDisease, otherT.TargetDisease)) return false;
            if( !DeepComparable.IsExactly(ImmunizationEvent, otherT.ImmunizationEvent)) return false;
            if( !DeepComparable.IsExactly(DoseStatus, otherT.DoseStatus)) return false;
            if( !DeepComparable.IsExactly(DoseStatusReason, otherT.DoseStatusReason)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(SeriesElement, otherT.SeriesElement)) return false;
            if( !DeepComparable.IsExactly(DoseNumber, otherT.DoseNumber)) return false;
            if( !DeepComparable.IsExactly(SeriesDoses, otherT.SeriesDoses)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("ImmunizationEvaluation");
            base.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("patient", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); Patient?.Serialize(sink);
            sink.Element("date", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DateElement?.Serialize(sink);
            sink.Element("authority", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Authority?.Serialize(sink);
            sink.Element("targetDisease", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); TargetDisease?.Serialize(sink);
            sink.Element("immunizationEvent", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); ImmunizationEvent?.Serialize(sink);
            sink.Element("doseStatus", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); DoseStatus?.Serialize(sink);
            sink.BeginList("doseStatusReason", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in DoseStatusReason)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
            sink.Element("series", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); SeriesElement?.Serialize(sink);
            sink.Element("doseNumber", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); DoseNumber?.Serialize(sink);
            sink.Element("seriesDoses", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); SeriesDoses?.Serialize(sink);
            sink.End();
        }
    
        internal override bool SetElementFromSource(string elementName, Serialization.ParserSource source)
        {
            if (base.SetElementFromSource(elementName, source))
            {
                return true;
            }
            switch (elementName)
            {
                case "identifier":
                    Identifier = source.GetList<Hl7.Fhir.Model.Identifier>();
                    return true;
                case "status":
                    StatusElement = source.Get<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.ImmunizationEvaluationStatusCodes>>();
                    return true;
                case "patient":
                    Patient = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "date":
                    DateElement = source.Get<Hl7.Fhir.Model.FhirDateTime>();
                    return true;
                case "authority":
                    Authority = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "targetDisease":
                    TargetDisease = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "immunizationEvent":
                    ImmunizationEvent = source.Get<Hl7.Fhir.Model.ResourceReference>();
                    return true;
                case "doseStatus":
                    DoseStatus = source.Get<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "doseStatusReason":
                    DoseStatusReason = source.GetList<Hl7.Fhir.Model.CodeableConcept>();
                    return true;
                case "description":
                    DescriptionElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "series":
                    SeriesElement = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "doseNumberPositiveInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.PositiveInt>(DoseNumber, "doseNumber");
                    DoseNumber = source.Get<Hl7.Fhir.Model.PositiveInt>();
                    return true;
                case "doseNumberString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(DoseNumber, "doseNumber");
                    DoseNumber = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
                case "seriesDosesPositiveInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.PositiveInt>(SeriesDoses, "seriesDoses");
                    SeriesDoses = source.Get<Hl7.Fhir.Model.PositiveInt>();
                    return true;
                case "seriesDosesString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(SeriesDoses, "seriesDoses");
                    SeriesDoses = source.Get<Hl7.Fhir.Model.FhirString>();
                    return true;
            }
            return false;
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
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "patient":
                    Patient = source.Populate(Patient);
                    return true;
                case "date":
                    DateElement = source.PopulateValue(DateElement);
                    return true;
                case "_date":
                    DateElement = source.Populate(DateElement);
                    return true;
                case "authority":
                    Authority = source.Populate(Authority);
                    return true;
                case "targetDisease":
                    TargetDisease = source.Populate(TargetDisease);
                    return true;
                case "immunizationEvent":
                    ImmunizationEvent = source.Populate(ImmunizationEvent);
                    return true;
                case "doseStatus":
                    DoseStatus = source.Populate(DoseStatus);
                    return true;
                case "doseStatusReason":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "description":
                    DescriptionElement = source.PopulateValue(DescriptionElement);
                    return true;
                case "_description":
                    DescriptionElement = source.Populate(DescriptionElement);
                    return true;
                case "series":
                    SeriesElement = source.PopulateValue(SeriesElement);
                    return true;
                case "_series":
                    SeriesElement = source.Populate(SeriesElement);
                    return true;
                case "doseNumberPositiveInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.PositiveInt>(DoseNumber, "doseNumber");
                    DoseNumber = source.PopulateValue(DoseNumber as Hl7.Fhir.Model.PositiveInt);
                    return true;
                case "_doseNumberPositiveInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.PositiveInt>(DoseNumber, "doseNumber");
                    DoseNumber = source.Populate(DoseNumber as Hl7.Fhir.Model.PositiveInt);
                    return true;
                case "doseNumberString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(DoseNumber, "doseNumber");
                    DoseNumber = source.PopulateValue(DoseNumber as Hl7.Fhir.Model.FhirString);
                    return true;
                case "_doseNumberString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(DoseNumber, "doseNumber");
                    DoseNumber = source.Populate(DoseNumber as Hl7.Fhir.Model.FhirString);
                    return true;
                case "seriesDosesPositiveInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.PositiveInt>(SeriesDoses, "seriesDoses");
                    SeriesDoses = source.PopulateValue(SeriesDoses as Hl7.Fhir.Model.PositiveInt);
                    return true;
                case "_seriesDosesPositiveInt":
                    source.CheckDuplicates<Hl7.Fhir.Model.PositiveInt>(SeriesDoses, "seriesDoses");
                    SeriesDoses = source.Populate(SeriesDoses as Hl7.Fhir.Model.PositiveInt);
                    return true;
                case "seriesDosesString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(SeriesDoses, "seriesDoses");
                    SeriesDoses = source.PopulateValue(SeriesDoses as Hl7.Fhir.Model.FhirString);
                    return true;
                case "_seriesDosesString":
                    source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(SeriesDoses, "seriesDoses");
                    SeriesDoses = source.Populate(SeriesDoses as Hl7.Fhir.Model.FhirString);
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
                case "doseStatusReason":
                    source.PopulateListItem(DoseStatusReason, index);
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
                if (StatusElement != null) yield return StatusElement;
                if (Patient != null) yield return Patient;
                if (DateElement != null) yield return DateElement;
                if (Authority != null) yield return Authority;
                if (TargetDisease != null) yield return TargetDisease;
                if (ImmunizationEvent != null) yield return ImmunizationEvent;
                if (DoseStatus != null) yield return DoseStatus;
                foreach (var elem in DoseStatusReason) { if (elem != null) yield return elem; }
                if (DescriptionElement != null) yield return DescriptionElement;
                if (SeriesElement != null) yield return SeriesElement;
                if (DoseNumber != null) yield return DoseNumber;
                if (SeriesDoses != null) yield return SeriesDoses;
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (Patient != null) yield return new ElementValue("patient", Patient);
                if (DateElement != null) yield return new ElementValue("date", DateElement);
                if (Authority != null) yield return new ElementValue("authority", Authority);
                if (TargetDisease != null) yield return new ElementValue("targetDisease", TargetDisease);
                if (ImmunizationEvent != null) yield return new ElementValue("immunizationEvent", ImmunizationEvent);
                if (DoseStatus != null) yield return new ElementValue("doseStatus", DoseStatus);
                foreach (var elem in DoseStatusReason) { if (elem != null) yield return new ElementValue("doseStatusReason", elem); }
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                if (SeriesElement != null) yield return new ElementValue("series", SeriesElement);
                if (DoseNumber != null) yield return new ElementValue("doseNumber", DoseNumber);
                if (SeriesDoses != null) yield return new ElementValue("seriesDoses", SeriesDoses);
            }
        }
    
    }

}
