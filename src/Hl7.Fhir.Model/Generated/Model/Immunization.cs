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
// Generated on Tue, Jul 1, 2014 16:22+0200 for FHIR v0.0.81
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Immunization event information
    /// </summary>
    [FhirType("Immunization", IsResource=true)]
    [DataContract]
    public partial class Immunization : Hl7.Fhir.Model.Resource, System.ComponentModel.INotifyPropertyChanged
    {
        [FhirType("ImmunizationVaccinationProtocolComponent")]
        [DataContract]
        public partial class ImmunizationVaccinationProtocolComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// What dose number within series?
            /// </summary>
            [FhirElement("doseSequence", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Integer DoseSequenceElement
            {
                get { return _DoseSequenceElement; }
                set { _DoseSequenceElement = value; OnPropertyChanged("DoseSequenceElement"); }
            }
            private Hl7.Fhir.Model.Integer _DoseSequenceElement;
            
            /// <summary>
            /// What dose number within series?
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? DoseSequence
            {
                get { return DoseSequenceElement != null ? DoseSequenceElement.Value : null; }
                set
                {
                    if(value == null)
                      DoseSequenceElement = null; 
                    else
                      DoseSequenceElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("DoseSequence");
                }
            }
            
            /// <summary>
            /// Details of vaccine protocol
            /// </summary>
            [FhirElement("description", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Details of vaccine protocol
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
            /// Who is responsible for protocol
            /// </summary>
            [FhirElement("authority", InSummary=true, Order=60)]
            [References("Organization")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Authority
            {
                get { return _Authority; }
                set { _Authority = value; OnPropertyChanged("Authority"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Authority;
            
            /// <summary>
            /// Name of vaccine series
            /// </summary>
            [FhirElement("series", InSummary=true, Order=70)]
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
                    if(value == null)
                      SeriesElement = null; 
                    else
                      SeriesElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Series");
                }
            }
            
            /// <summary>
            /// Recommended number of doses for immunity
            /// </summary>
            [FhirElement("seriesDoses", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Integer SeriesDosesElement
            {
                get { return _SeriesDosesElement; }
                set { _SeriesDosesElement = value; OnPropertyChanged("SeriesDosesElement"); }
            }
            private Hl7.Fhir.Model.Integer _SeriesDosesElement;
            
            /// <summary>
            /// Recommended number of doses for immunity
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? SeriesDoses
            {
                get { return SeriesDosesElement != null ? SeriesDosesElement.Value : null; }
                set
                {
                    if(value == null)
                      SeriesDosesElement = null; 
                    else
                      SeriesDosesElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("SeriesDoses");
                }
            }
            
            /// <summary>
            /// Disease immunized against
            /// </summary>
            [FhirElement("doseTarget", InSummary=true, Order=90)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept DoseTarget
            {
                get { return _DoseTarget; }
                set { _DoseTarget = value; OnPropertyChanged("DoseTarget"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _DoseTarget;
            
            /// <summary>
            /// Does dose count towards immunity?
            /// </summary>
            [FhirElement("doseStatus", InSummary=true, Order=100)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept DoseStatus
            {
                get { return _DoseStatus; }
                set { _DoseStatus = value; OnPropertyChanged("DoseStatus"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _DoseStatus;
            
            /// <summary>
            /// Why does does count/not count?
            /// </summary>
            [FhirElement("doseStatusReason", InSummary=true, Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept DoseStatusReason
            {
                get { return _DoseStatusReason; }
                set { _DoseStatusReason = value; OnPropertyChanged("DoseStatusReason"); }
            }
            private Hl7.Fhir.Model.CodeableConcept _DoseStatusReason;
            
        }
        
        
        [FhirType("ImmunizationExplanationComponent")]
        [DataContract]
        public partial class ImmunizationExplanationComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// Why immunization occurred
            /// </summary>
            [FhirElement("reason", InSummary=true, Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Reason
            {
                get { return _Reason; }
                set { _Reason = value; OnPropertyChanged("Reason"); }
            }
            private List<Hl7.Fhir.Model.CodeableConcept> _Reason;
            
            /// <summary>
            /// Why immunization did not occur
            /// </summary>
            [FhirElement("refusalReason", InSummary=true, Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> RefusalReason
            {
                get { return _RefusalReason; }
                set { _RefusalReason = value; OnPropertyChanged("RefusalReason"); }
            }
            private List<Hl7.Fhir.Model.CodeableConcept> _RefusalReason;
            
        }
        
        
        [FhirType("ImmunizationReactionComponent")]
        [DataContract]
        public partial class ImmunizationReactionComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged
        {
            /// <summary>
            /// When did reaction start?
            /// </summary>
            [FhirElement("date", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime DateElement
            {
                get { return _DateElement; }
                set { _DateElement = value; OnPropertyChanged("DateElement"); }
            }
            private Hl7.Fhir.Model.FhirDateTime _DateElement;
            
            /// <summary>
            /// When did reaction start?
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Date
            {
                get { return DateElement != null ? DateElement.Value : null; }
                set
                {
                    if(value == null)
                      DateElement = null; 
                    else
                      DateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("Date");
                }
            }
            
            /// <summary>
            /// Additional information on reaction
            /// </summary>
            [FhirElement("detail", InSummary=true, Order=50)]
            [References("AdverseReaction","Observation")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Detail
            {
                get { return _Detail; }
                set { _Detail = value; OnPropertyChanged("Detail"); }
            }
            private Hl7.Fhir.Model.ResourceReference _Detail;
            
            /// <summary>
            /// Was reaction self-reported?
            /// </summary>
            [FhirElement("reported", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean ReportedElement
            {
                get { return _ReportedElement; }
                set { _ReportedElement = value; OnPropertyChanged("ReportedElement"); }
            }
            private Hl7.Fhir.Model.FhirBoolean _ReportedElement;
            
            /// <summary>
            /// Was reaction self-reported?
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Reported
            {
                get { return ReportedElement != null ? ReportedElement.Value : null; }
                set
                {
                    if(value == null)
                      ReportedElement = null; 
                    else
                      ReportedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Reported");
                }
            }
            
        }
        
        
        /// <summary>
        /// Business identifier
        /// </summary>
        [FhirElement("identifier", Order=70)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Vaccination administration date
        /// </summary>
        [FhirElement("date", Order=80)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// Vaccination administration date
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Date
        {
            get { return DateElement != null ? DateElement.Value : null; }
            set
            {
                if(value == null)
                  DateElement = null; 
                else
                  DateElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Date");
            }
        }
        
        /// <summary>
        /// Vaccine product administered
        /// </summary>
        [FhirElement("vaccineType", Order=90)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept VaccineType
        {
            get { return _VaccineType; }
            set { _VaccineType = value; OnPropertyChanged("VaccineType"); }
        }
        private Hl7.Fhir.Model.CodeableConcept _VaccineType;
        
        /// <summary>
        /// Who was immunized?
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
        /// Was immunization refused?
        /// </summary>
        [FhirElement("refusedIndicator", Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean RefusedIndicatorElement
        {
            get { return _RefusedIndicatorElement; }
            set { _RefusedIndicatorElement = value; OnPropertyChanged("RefusedIndicatorElement"); }
        }
        private Hl7.Fhir.Model.FhirBoolean _RefusedIndicatorElement;
        
        /// <summary>
        /// Was immunization refused?
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? RefusedIndicator
        {
            get { return RefusedIndicatorElement != null ? RefusedIndicatorElement.Value : null; }
            set
            {
                if(value == null)
                  RefusedIndicatorElement = null; 
                else
                  RefusedIndicatorElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("RefusedIndicator");
            }
        }
        
        /// <summary>
        /// Is this a self-reported record?
        /// </summary>
        [FhirElement("reported", Order=120)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ReportedElement
        {
            get { return _ReportedElement; }
            set { _ReportedElement = value; OnPropertyChanged("ReportedElement"); }
        }
        private Hl7.Fhir.Model.FhirBoolean _ReportedElement;
        
        /// <summary>
        /// Is this a self-reported record?
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Reported
        {
            get { return ReportedElement != null ? ReportedElement.Value : null; }
            set
            {
                if(value == null)
                  ReportedElement = null; 
                else
                  ReportedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Reported");
            }
        }
        
        /// <summary>
        /// Who administered vaccine?
        /// </summary>
        [FhirElement("performer", Order=130)]
        [References("Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Performer
        {
            get { return _Performer; }
            set { _Performer = value; OnPropertyChanged("Performer"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Performer;
        
        /// <summary>
        /// Who ordered vaccination?
        /// </summary>
        [FhirElement("requester", Order=140)]
        [References("Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Requester
        {
            get { return _Requester; }
            set { _Requester = value; OnPropertyChanged("Requester"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Requester;
        
        /// <summary>
        /// Vaccine manufacturer
        /// </summary>
        [FhirElement("manufacturer", Order=150)]
        [References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Manufacturer
        {
            get { return _Manufacturer; }
            set { _Manufacturer = value; OnPropertyChanged("Manufacturer"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Manufacturer;
        
        /// <summary>
        /// Where did vaccination occur?
        /// </summary>
        [FhirElement("location", Order=160)]
        [References("Location")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Location
        {
            get { return _Location; }
            set { _Location = value; OnPropertyChanged("Location"); }
        }
        private Hl7.Fhir.Model.ResourceReference _Location;
        
        /// <summary>
        /// Vaccine lot number
        /// </summary>
        [FhirElement("lotNumber", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString LotNumberElement
        {
            get { return _LotNumberElement; }
            set { _LotNumberElement = value; OnPropertyChanged("LotNumberElement"); }
        }
        private Hl7.Fhir.Model.FhirString _LotNumberElement;
        
        /// <summary>
        /// Vaccine lot number
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string LotNumber
        {
            get { return LotNumberElement != null ? LotNumberElement.Value : null; }
            set
            {
                if(value == null)
                  LotNumberElement = null; 
                else
                  LotNumberElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("LotNumber");
            }
        }
        
        /// <summary>
        /// Vaccine expiration date
        /// </summary>
        [FhirElement("expirationDate", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.Date ExpirationDateElement
        {
            get { return _ExpirationDateElement; }
            set { _ExpirationDateElement = value; OnPropertyChanged("ExpirationDateElement"); }
        }
        private Hl7.Fhir.Model.Date _ExpirationDateElement;
        
        /// <summary>
        /// Vaccine expiration date
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ExpirationDate
        {
            get { return ExpirationDateElement != null ? ExpirationDateElement.Value : null; }
            set
            {
                if(value == null)
                  ExpirationDateElement = null; 
                else
                  ExpirationDateElement = new Hl7.Fhir.Model.Date(value);
                OnPropertyChanged("ExpirationDate");
            }
        }
        
        /// <summary>
        /// Body site vaccine  was administered
        /// </summary>
        [FhirElement("site", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Site
        {
            get { return _Site; }
            set { _Site = value; OnPropertyChanged("Site"); }
        }
        private Hl7.Fhir.Model.CodeableConcept _Site;
        
        /// <summary>
        /// How vaccine entered body
        /// </summary>
        [FhirElement("route", Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Route
        {
            get { return _Route; }
            set { _Route = value; OnPropertyChanged("Route"); }
        }
        private Hl7.Fhir.Model.CodeableConcept _Route;
        
        /// <summary>
        /// Amount of vaccine administered
        /// </summary>
        [FhirElement("doseQuantity", Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.Quantity DoseQuantity
        {
            get { return _DoseQuantity; }
            set { _DoseQuantity = value; OnPropertyChanged("DoseQuantity"); }
        }
        private Hl7.Fhir.Model.Quantity _DoseQuantity;
        
        /// <summary>
        /// Administration / refusal reasons
        /// </summary>
        [FhirElement("explanation", Order=220)]
        [DataMember]
        public Hl7.Fhir.Model.Immunization.ImmunizationExplanationComponent Explanation
        {
            get { return _Explanation; }
            set { _Explanation = value; OnPropertyChanged("Explanation"); }
        }
        private Hl7.Fhir.Model.Immunization.ImmunizationExplanationComponent _Explanation;
        
        /// <summary>
        /// Details of a reaction that follows immunization
        /// </summary>
        [FhirElement("reaction", Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Immunization.ImmunizationReactionComponent> Reaction
        {
            get { return _Reaction; }
            set { _Reaction = value; OnPropertyChanged("Reaction"); }
        }
        private List<Hl7.Fhir.Model.Immunization.ImmunizationReactionComponent> _Reaction;
        
        /// <summary>
        /// What protocol was followed
        /// </summary>
        [FhirElement("vaccinationProtocol", Order=240)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Immunization.ImmunizationVaccinationProtocolComponent> VaccinationProtocol
        {
            get { return _VaccinationProtocol; }
            set { _VaccinationProtocol = value; OnPropertyChanged("VaccinationProtocol"); }
        }
        private List<Hl7.Fhir.Model.Immunization.ImmunizationVaccinationProtocolComponent> _VaccinationProtocol;
        
    }
    
}
