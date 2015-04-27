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
    /// Immunization event information
    /// </summary>
    [FhirType("Immunization", IsResource=true)]
    [DataContract]
    public partial class Immunization : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Immunization; } }
        [NotMapped]
        public override string TypeName { get { return "Immunization"; } }
        
        [FhirType("ImmunizationVaccinationProtocolComponent")]
        [DataContract]
        public partial class ImmunizationVaccinationProtocolComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ImmunizationVaccinationProtocolComponent"; } }
            
            /// <summary>
            /// What dose number within series?
            /// </summary>
            [FhirElement("doseSequence", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt DoseSequenceElement
            {
                get { return _DoseSequenceElement; }
                set { _DoseSequenceElement = value; OnPropertyChanged("DoseSequenceElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _DoseSequenceElement;
            
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
                      DoseSequenceElement = new Hl7.Fhir.Model.PositiveInt(value);
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
            public Hl7.Fhir.Model.PositiveInt SeriesDosesElement
            {
                get { return _SeriesDosesElement; }
                set { _SeriesDosesElement = value; OnPropertyChanged("SeriesDosesElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SeriesDosesElement;
            
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
                      SeriesDosesElement = new Hl7.Fhir.Model.PositiveInt(value);
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ImmunizationVaccinationProtocolComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DoseSequenceElement != null) dest.DoseSequenceElement = (Hl7.Fhir.Model.PositiveInt)DoseSequenceElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Authority != null) dest.Authority = (Hl7.Fhir.Model.ResourceReference)Authority.DeepCopy();
                    if(SeriesElement != null) dest.SeriesElement = (Hl7.Fhir.Model.FhirString)SeriesElement.DeepCopy();
                    if(SeriesDosesElement != null) dest.SeriesDosesElement = (Hl7.Fhir.Model.PositiveInt)SeriesDosesElement.DeepCopy();
                    if(DoseTarget != null) dest.DoseTarget = (Hl7.Fhir.Model.CodeableConcept)DoseTarget.DeepCopy();
                    if(DoseStatus != null) dest.DoseStatus = (Hl7.Fhir.Model.CodeableConcept)DoseStatus.DeepCopy();
                    if(DoseStatusReason != null) dest.DoseStatusReason = (Hl7.Fhir.Model.CodeableConcept)DoseStatusReason.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ImmunizationVaccinationProtocolComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ImmunizationVaccinationProtocolComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DoseSequenceElement, otherT.DoseSequenceElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(Authority, otherT.Authority)) return false;
                if( !DeepComparable.Matches(SeriesElement, otherT.SeriesElement)) return false;
                if( !DeepComparable.Matches(SeriesDosesElement, otherT.SeriesDosesElement)) return false;
                if( !DeepComparable.Matches(DoseTarget, otherT.DoseTarget)) return false;
                if( !DeepComparable.Matches(DoseStatus, otherT.DoseStatus)) return false;
                if( !DeepComparable.Matches(DoseStatusReason, otherT.DoseStatusReason)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ImmunizationVaccinationProtocolComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DoseSequenceElement, otherT.DoseSequenceElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Authority, otherT.Authority)) return false;
                if( !DeepComparable.IsExactly(SeriesElement, otherT.SeriesElement)) return false;
                if( !DeepComparable.IsExactly(SeriesDosesElement, otherT.SeriesDosesElement)) return false;
                if( !DeepComparable.IsExactly(DoseTarget, otherT.DoseTarget)) return false;
                if( !DeepComparable.IsExactly(DoseStatus, otherT.DoseStatus)) return false;
                if( !DeepComparable.IsExactly(DoseStatusReason, otherT.DoseStatusReason)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ImmunizationExplanationComponent")]
        [DataContract]
        public partial class ImmunizationExplanationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ImmunizationExplanationComponent"; } }
            
            /// <summary>
            /// Why immunization occurred
            /// </summary>
            [FhirElement("reason", InSummary=true, Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Reason
            {
                get { if(_Reason==null) _Reason = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Reason; }
                set { _Reason = value; OnPropertyChanged("Reason"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Reason;
            
            /// <summary>
            /// Why immunization did not occur
            /// </summary>
            [FhirElement("reasonNotGiven", InSummary=true, Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> ReasonNotGiven
            {
                get { if(_ReasonNotGiven==null) _ReasonNotGiven = new List<Hl7.Fhir.Model.CodeableConcept>(); return _ReasonNotGiven; }
                set { _ReasonNotGiven = value; OnPropertyChanged("ReasonNotGiven"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _ReasonNotGiven;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ImmunizationExplanationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Reason != null) dest.Reason = new List<Hl7.Fhir.Model.CodeableConcept>(Reason.DeepCopy());
                    if(ReasonNotGiven != null) dest.ReasonNotGiven = new List<Hl7.Fhir.Model.CodeableConcept>(ReasonNotGiven.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ImmunizationExplanationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ImmunizationExplanationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
                if( !DeepComparable.Matches(ReasonNotGiven, otherT.ReasonNotGiven)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ImmunizationExplanationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
                if( !DeepComparable.IsExactly(ReasonNotGiven, otherT.ReasonNotGiven)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("ImmunizationReactionComponent")]
        [DataContract]
        public partial class ImmunizationReactionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ImmunizationReactionComponent"; } }
            
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
            [References("Observation")]
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ImmunizationReactionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                    if(Detail != null) dest.Detail = (Hl7.Fhir.Model.ResourceReference)Detail.DeepCopy();
                    if(ReportedElement != null) dest.ReportedElement = (Hl7.Fhir.Model.FhirBoolean)ReportedElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ImmunizationReactionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ImmunizationReactionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.Matches(Detail, otherT.Detail)) return false;
                if( !DeepComparable.Matches(ReportedElement, otherT.ReportedElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ImmunizationReactionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.IsExactly(Detail, otherT.Detail)) return false;
                if( !DeepComparable.IsExactly(ReportedElement, otherT.ReportedElement)) return false;
                
                return true;
            }
            
        }
        
        
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
        /// Vaccination administration date
        /// </summary>
        [FhirElement("date", Order=100)]
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
        [FhirElement("vaccineType", Order=110)]
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
        [FhirElement("patient", Order=120)]
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
        /// Was immunization given?
        /// </summary>
        [FhirElement("wasNotGiven", Order=130)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean WasNotGivenElement
        {
            get { return _WasNotGivenElement; }
            set { _WasNotGivenElement = value; OnPropertyChanged("WasNotGivenElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _WasNotGivenElement;
        
        /// <summary>
        /// Was immunization given?
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? WasNotGiven
        {
            get { return WasNotGivenElement != null ? WasNotGivenElement.Value : null; }
            set
            {
                if(value == null)
                  WasNotGivenElement = null; 
                else
                  WasNotGivenElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("WasNotGiven");
            }
        }
        
        /// <summary>
        /// Is this a self-reported record?
        /// </summary>
        [FhirElement("reported", Order=140)]
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
        [FhirElement("performer", Order=150)]
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
        [FhirElement("requester", Order=160)]
        [References("Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Requester
        {
            get { return _Requester; }
            set { _Requester = value; OnPropertyChanged("Requester"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Requester;
        
        /// <summary>
        /// Encounter administered as part of
        /// </summary>
        [FhirElement("encounter", Order=170)]
        [References("Encounter")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Encounter
        {
            get { return _Encounter; }
            set { _Encounter = value; OnPropertyChanged("Encounter"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Encounter;
        
        /// <summary>
        /// Vaccine manufacturer
        /// </summary>
        [FhirElement("manufacturer", Order=180)]
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
        [FhirElement("location", Order=190)]
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
        [FhirElement("lotNumber", Order=200)]
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
        [FhirElement("expirationDate", Order=210)]
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
        [FhirElement("site", Order=220)]
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
        [FhirElement("route", Order=230)]
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
        [FhirElement("doseQuantity", Order=240)]
        [DataMember]
        public Hl7.Fhir.Model.Quantity DoseQuantity
        {
            get { return _DoseQuantity; }
            set { _DoseQuantity = value; OnPropertyChanged("DoseQuantity"); }
        }
        
        private Hl7.Fhir.Model.Quantity _DoseQuantity;
        
        /// <summary>
        /// Administration / non-administration reasons
        /// </summary>
        [FhirElement("explanation", Order=250)]
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
        [FhirElement("reaction", Order=260)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Immunization.ImmunizationReactionComponent> Reaction
        {
            get { if(_Reaction==null) _Reaction = new List<Hl7.Fhir.Model.Immunization.ImmunizationReactionComponent>(); return _Reaction; }
            set { _Reaction = value; OnPropertyChanged("Reaction"); }
        }
        
        private List<Hl7.Fhir.Model.Immunization.ImmunizationReactionComponent> _Reaction;
        
        /// <summary>
        /// What protocol was followed
        /// </summary>
        [FhirElement("vaccinationProtocol", Order=270)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Immunization.ImmunizationVaccinationProtocolComponent> VaccinationProtocol
        {
            get { if(_VaccinationProtocol==null) _VaccinationProtocol = new List<Hl7.Fhir.Model.Immunization.ImmunizationVaccinationProtocolComponent>(); return _VaccinationProtocol; }
            set { _VaccinationProtocol = value; OnPropertyChanged("VaccinationProtocol"); }
        }
        
        private List<Hl7.Fhir.Model.Immunization.ImmunizationVaccinationProtocolComponent> _VaccinationProtocol;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Immunization;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(VaccineType != null) dest.VaccineType = (Hl7.Fhir.Model.CodeableConcept)VaccineType.DeepCopy();
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(WasNotGivenElement != null) dest.WasNotGivenElement = (Hl7.Fhir.Model.FhirBoolean)WasNotGivenElement.DeepCopy();
                if(ReportedElement != null) dest.ReportedElement = (Hl7.Fhir.Model.FhirBoolean)ReportedElement.DeepCopy();
                if(Performer != null) dest.Performer = (Hl7.Fhir.Model.ResourceReference)Performer.DeepCopy();
                if(Requester != null) dest.Requester = (Hl7.Fhir.Model.ResourceReference)Requester.DeepCopy();
                if(Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
                if(Manufacturer != null) dest.Manufacturer = (Hl7.Fhir.Model.ResourceReference)Manufacturer.DeepCopy();
                if(Location != null) dest.Location = (Hl7.Fhir.Model.ResourceReference)Location.DeepCopy();
                if(LotNumberElement != null) dest.LotNumberElement = (Hl7.Fhir.Model.FhirString)LotNumberElement.DeepCopy();
                if(ExpirationDateElement != null) dest.ExpirationDateElement = (Hl7.Fhir.Model.Date)ExpirationDateElement.DeepCopy();
                if(Site != null) dest.Site = (Hl7.Fhir.Model.CodeableConcept)Site.DeepCopy();
                if(Route != null) dest.Route = (Hl7.Fhir.Model.CodeableConcept)Route.DeepCopy();
                if(DoseQuantity != null) dest.DoseQuantity = (Hl7.Fhir.Model.Quantity)DoseQuantity.DeepCopy();
                if(Explanation != null) dest.Explanation = (Hl7.Fhir.Model.Immunization.ImmunizationExplanationComponent)Explanation.DeepCopy();
                if(Reaction != null) dest.Reaction = new List<Hl7.Fhir.Model.Immunization.ImmunizationReactionComponent>(Reaction.DeepCopy());
                if(VaccinationProtocol != null) dest.VaccinationProtocol = new List<Hl7.Fhir.Model.Immunization.ImmunizationVaccinationProtocolComponent>(VaccinationProtocol.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Immunization());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Immunization;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(VaccineType, otherT.VaccineType)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(WasNotGivenElement, otherT.WasNotGivenElement)) return false;
            if( !DeepComparable.Matches(ReportedElement, otherT.ReportedElement)) return false;
            if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
            if( !DeepComparable.Matches(Requester, otherT.Requester)) return false;
            if( !DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.Matches(Manufacturer, otherT.Manufacturer)) return false;
            if( !DeepComparable.Matches(Location, otherT.Location)) return false;
            if( !DeepComparable.Matches(LotNumberElement, otherT.LotNumberElement)) return false;
            if( !DeepComparable.Matches(ExpirationDateElement, otherT.ExpirationDateElement)) return false;
            if( !DeepComparable.Matches(Site, otherT.Site)) return false;
            if( !DeepComparable.Matches(Route, otherT.Route)) return false;
            if( !DeepComparable.Matches(DoseQuantity, otherT.DoseQuantity)) return false;
            if( !DeepComparable.Matches(Explanation, otherT.Explanation)) return false;
            if( !DeepComparable.Matches(Reaction, otherT.Reaction)) return false;
            if( !DeepComparable.Matches(VaccinationProtocol, otherT.VaccinationProtocol)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Immunization;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(VaccineType, otherT.VaccineType)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(WasNotGivenElement, otherT.WasNotGivenElement)) return false;
            if( !DeepComparable.IsExactly(ReportedElement, otherT.ReportedElement)) return false;
            if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
            if( !DeepComparable.IsExactly(Requester, otherT.Requester)) return false;
            if( !DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.IsExactly(Manufacturer, otherT.Manufacturer)) return false;
            if( !DeepComparable.IsExactly(Location, otherT.Location)) return false;
            if( !DeepComparable.IsExactly(LotNumberElement, otherT.LotNumberElement)) return false;
            if( !DeepComparable.IsExactly(ExpirationDateElement, otherT.ExpirationDateElement)) return false;
            if( !DeepComparable.IsExactly(Site, otherT.Site)) return false;
            if( !DeepComparable.IsExactly(Route, otherT.Route)) return false;
            if( !DeepComparable.IsExactly(DoseQuantity, otherT.DoseQuantity)) return false;
            if( !DeepComparable.IsExactly(Explanation, otherT.Explanation)) return false;
            if( !DeepComparable.IsExactly(Reaction, otherT.Reaction)) return false;
            if( !DeepComparable.IsExactly(VaccinationProtocol, otherT.VaccinationProtocol)) return false;
            
            return true;
        }
        
    }
    
}
