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
// Generated for FHIR v3.3.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Patient's or family member's work information (ODH)
    /// </summary>
    [FhirType("OccupationalData", IsResource=true)]
    [DataContract]
    public partial class OccupationalData : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.OccupationalData; } }
        [NotMapped]
        public override string TypeName { get { return "OccupationalData"; } }
        
        [FhirType("EmploymentStatusComponent")]
        [DataContract]
        public partial class EmploymentStatusComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "EmploymentStatusComponent"; } }
            
            /// <summary>
            /// Employment status code
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
            /// Employment status effective time period
            /// </summary>
            [FhirElement("effective", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Period Effective
            {
                get { return _Effective; }
                set { _Effective = value; OnPropertyChanged("Effective"); }
            }
            
            private Hl7.Fhir.Model.Period _Effective;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as EmploymentStatusComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(Effective != null) dest.Effective = (Hl7.Fhir.Model.Period)Effective.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new EmploymentStatusComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as EmploymentStatusComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Effective, otherT.Effective)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as EmploymentStatusComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Effective, otherT.Effective)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Code != null) yield return Code;
                    if (Effective != null) yield return Effective;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Code != null) yield return new ElementValue("code", false, Code);
                    if (Effective != null) yield return new ElementValue("effective", false, Effective);
                }
            }

            
        }
        
        
        [FhirType("UsualWorkComponent")]
        [DataContract]
        public partial class UsualWorkComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "UsualWorkComponent"; } }
            
            /// <summary>
            /// Usual Work occupation
            /// </summary>
            [FhirElement("occupation", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Occupation
            {
                get { return _Occupation; }
                set { _Occupation = value; OnPropertyChanged("Occupation"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Occupation;
            
            /// <summary>
            /// Usual Work industry
            /// </summary>
            [FhirElement("industry", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Industry
            {
                get { return _Industry; }
                set { _Industry = value; OnPropertyChanged("Industry"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Industry;
            
            /// <summary>
            /// Usual Work start time
            /// </summary>
            [FhirElement("start", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDateTime StartElement
            {
                get { return _StartElement; }
                set { _StartElement = value; OnPropertyChanged("StartElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDateTime _StartElement;
            
            /// <summary>
            /// Usual Work start time
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Start
            {
                get { return StartElement != null ? StartElement.Value : null; }
                set
                {
                    if (value == null)
                        StartElement = null; 
                    else
                        StartElement = new Hl7.Fhir.Model.FhirDateTime(value);
                    OnPropertyChanged("Start");
                }
            }
            
            /// <summary>
            /// Usual Work duration
            /// </summary>
            [FhirElement("duration", InSummary=true, Order=70)]
            [DataMember]
            public Duration Duration
            {
                get { return _Duration; }
                set { _Duration = value; OnPropertyChanged("Duration"); }
            }
            
            private Duration _Duration;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as UsualWorkComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Occupation != null) dest.Occupation = (Hl7.Fhir.Model.CodeableConcept)Occupation.DeepCopy();
                    if(Industry != null) dest.Industry = (Hl7.Fhir.Model.CodeableConcept)Industry.DeepCopy();
                    if(StartElement != null) dest.StartElement = (Hl7.Fhir.Model.FhirDateTime)StartElement.DeepCopy();
                    if(Duration != null) dest.Duration = (Duration)Duration.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new UsualWorkComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as UsualWorkComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Occupation, otherT.Occupation)) return false;
                if( !DeepComparable.Matches(Industry, otherT.Industry)) return false;
                if( !DeepComparable.Matches(StartElement, otherT.StartElement)) return false;
                if( !DeepComparable.Matches(Duration, otherT.Duration)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as UsualWorkComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Occupation, otherT.Occupation)) return false;
                if( !DeepComparable.IsExactly(Industry, otherT.Industry)) return false;
                if( !DeepComparable.IsExactly(StartElement, otherT.StartElement)) return false;
                if( !DeepComparable.IsExactly(Duration, otherT.Duration)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Occupation != null) yield return Occupation;
                    if (Industry != null) yield return Industry;
                    if (StartElement != null) yield return StartElement;
                    if (Duration != null) yield return Duration;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Occupation != null) yield return new ElementValue("occupation", false, Occupation);
                    if (Industry != null) yield return new ElementValue("industry", false, Industry);
                    if (StartElement != null) yield return new ElementValue("start", false, StartElement);
                    if (Duration != null) yield return new ElementValue("duration", false, Duration);
                }
            }

            
        }
        
        
        [FhirType("PastOrPresentJobComponent")]
        [DataContract]
        public partial class PastOrPresentJobComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "PastOrPresentJobComponent"; } }
            
            /// <summary>
            /// Past or Present Job occupation
            /// </summary>
            [FhirElement("occupation", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Occupation
            {
                get { return _Occupation; }
                set { _Occupation = value; OnPropertyChanged("Occupation"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Occupation;
            
            /// <summary>
            /// Past or Present Job industry
            /// </summary>
            [FhirElement("industry", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Industry
            {
                get { return _Industry; }
                set { _Industry = value; OnPropertyChanged("Industry"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Industry;
            
            /// <summary>
            /// Past or Present Job effective time period
            /// </summary>
            [FhirElement("effective", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Period Effective
            {
                get { return _Effective; }
                set { _Effective = value; OnPropertyChanged("Effective"); }
            }
            
            private Hl7.Fhir.Model.Period _Effective;
            
            /// <summary>
            /// Past or Present Job employer
            /// </summary>
            [FhirElement("employer", InSummary=true, Order=70)]
            [CLSCompliant(false)]
			[References("Organization")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Employer
            {
                get { return _Employer; }
                set { _Employer = value; OnPropertyChanged("Employer"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Employer;
            
            /// <summary>
            /// Past or Present Job work classification
            /// </summary>
            [FhirElement("workClassification", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept WorkClassification
            {
                get { return _WorkClassification; }
                set { _WorkClassification = value; OnPropertyChanged("WorkClassification"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _WorkClassification;
            
            /// <summary>
            /// Past or Present Job supervisory level
            /// </summary>
            [FhirElement("supervisoryLevel", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept SupervisoryLevel
            {
                get { return _SupervisoryLevel; }
                set { _SupervisoryLevel = value; OnPropertyChanged("SupervisoryLevel"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _SupervisoryLevel;
            
            /// <summary>
            /// Past or Present Job job duty
            /// </summary>
            [FhirElement("jobDuty", InSummary=true, Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> JobDutyElement
            {
                get { if(_JobDutyElement==null) _JobDutyElement = new List<Hl7.Fhir.Model.FhirString>(); return _JobDutyElement; }
                set { _JobDutyElement = value; OnPropertyChanged("JobDutyElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _JobDutyElement;
            
            /// <summary>
            /// Past or Present Job job duty
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> JobDuty
            {
                get { return JobDutyElement != null ? JobDutyElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        JobDutyElement = null; 
                    else
                        JobDutyElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("JobDuty");
                }
            }
            
            /// <summary>
            /// Past or Present Job occupational hazard
            /// </summary>
            [FhirElement("occupationalHazard", InSummary=true, Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> OccupationalHazardElement
            {
                get { if(_OccupationalHazardElement==null) _OccupationalHazardElement = new List<Hl7.Fhir.Model.FhirString>(); return _OccupationalHazardElement; }
                set { _OccupationalHazardElement = value; OnPropertyChanged("OccupationalHazardElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _OccupationalHazardElement;
            
            /// <summary>
            /// Past or Present Job occupational hazard
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> OccupationalHazard
            {
                get { return OccupationalHazardElement != null ? OccupationalHazardElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        OccupationalHazardElement = null; 
                    else
                        OccupationalHazardElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("OccupationalHazard");
                }
            }
            
            /// <summary>
            /// Past or Present Job work schedule
            /// </summary>
            [FhirElement("workSchedule", InSummary=true, Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.OccupationalData.WorkScheduleComponent WorkSchedule
            {
                get { return _WorkSchedule; }
                set { _WorkSchedule = value; OnPropertyChanged("WorkSchedule"); }
            }
            
            private Hl7.Fhir.Model.OccupationalData.WorkScheduleComponent _WorkSchedule;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PastOrPresentJobComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Occupation != null) dest.Occupation = (Hl7.Fhir.Model.CodeableConcept)Occupation.DeepCopy();
                    if(Industry != null) dest.Industry = (Hl7.Fhir.Model.CodeableConcept)Industry.DeepCopy();
                    if(Effective != null) dest.Effective = (Hl7.Fhir.Model.Period)Effective.DeepCopy();
                    if(Employer != null) dest.Employer = (Hl7.Fhir.Model.ResourceReference)Employer.DeepCopy();
                    if(WorkClassification != null) dest.WorkClassification = (Hl7.Fhir.Model.CodeableConcept)WorkClassification.DeepCopy();
                    if(SupervisoryLevel != null) dest.SupervisoryLevel = (Hl7.Fhir.Model.CodeableConcept)SupervisoryLevel.DeepCopy();
                    if(JobDutyElement != null) dest.JobDutyElement = new List<Hl7.Fhir.Model.FhirString>(JobDutyElement.DeepCopy());
                    if(OccupationalHazardElement != null) dest.OccupationalHazardElement = new List<Hl7.Fhir.Model.FhirString>(OccupationalHazardElement.DeepCopy());
                    if(WorkSchedule != null) dest.WorkSchedule = (Hl7.Fhir.Model.OccupationalData.WorkScheduleComponent)WorkSchedule.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new PastOrPresentJobComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PastOrPresentJobComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Occupation, otherT.Occupation)) return false;
                if( !DeepComparable.Matches(Industry, otherT.Industry)) return false;
                if( !DeepComparable.Matches(Effective, otherT.Effective)) return false;
                if( !DeepComparable.Matches(Employer, otherT.Employer)) return false;
                if( !DeepComparable.Matches(WorkClassification, otherT.WorkClassification)) return false;
                if( !DeepComparable.Matches(SupervisoryLevel, otherT.SupervisoryLevel)) return false;
                if( !DeepComparable.Matches(JobDutyElement, otherT.JobDutyElement)) return false;
                if( !DeepComparable.Matches(OccupationalHazardElement, otherT.OccupationalHazardElement)) return false;
                if( !DeepComparable.Matches(WorkSchedule, otherT.WorkSchedule)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PastOrPresentJobComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Occupation, otherT.Occupation)) return false;
                if( !DeepComparable.IsExactly(Industry, otherT.Industry)) return false;
                if( !DeepComparable.IsExactly(Effective, otherT.Effective)) return false;
                if( !DeepComparable.IsExactly(Employer, otherT.Employer)) return false;
                if( !DeepComparable.IsExactly(WorkClassification, otherT.WorkClassification)) return false;
                if( !DeepComparable.IsExactly(SupervisoryLevel, otherT.SupervisoryLevel)) return false;
                if( !DeepComparable.IsExactly(JobDutyElement, otherT.JobDutyElement)) return false;
                if( !DeepComparable.IsExactly(OccupationalHazardElement, otherT.OccupationalHazardElement)) return false;
                if( !DeepComparable.IsExactly(WorkSchedule, otherT.WorkSchedule)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Occupation != null) yield return Occupation;
                    if (Industry != null) yield return Industry;
                    if (Effective != null) yield return Effective;
                    if (Employer != null) yield return Employer;
                    if (WorkClassification != null) yield return WorkClassification;
                    if (SupervisoryLevel != null) yield return SupervisoryLevel;
                    foreach (var elem in JobDutyElement) { if (elem != null) yield return elem; }
                    foreach (var elem in OccupationalHazardElement) { if (elem != null) yield return elem; }
                    if (WorkSchedule != null) yield return WorkSchedule;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Occupation != null) yield return new ElementValue("occupation", false, Occupation);
                    if (Industry != null) yield return new ElementValue("industry", false, Industry);
                    if (Effective != null) yield return new ElementValue("effective", false, Effective);
                    if (Employer != null) yield return new ElementValue("employer", false, Employer);
                    if (WorkClassification != null) yield return new ElementValue("workClassification", false, WorkClassification);
                    if (SupervisoryLevel != null) yield return new ElementValue("supervisoryLevel", false, SupervisoryLevel);
                    foreach (var elem in JobDutyElement) { if (elem != null) yield return new ElementValue("jobDuty", true, elem); }
                    foreach (var elem in OccupationalHazardElement) { if (elem != null) yield return new ElementValue("occupationalHazard", true, elem); }
                    if (WorkSchedule != null) yield return new ElementValue("workSchedule", false, WorkSchedule);
                }
            }

            
        }
        
        
        [FhirType("WorkScheduleComponent")]
        [DataContract]
        public partial class WorkScheduleComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "WorkScheduleComponent"; } }
            
            /// <summary>
            /// Past or Present Job work schedule code
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
            /// Past or Present Job work schedule weekly work days
            /// </summary>
            [FhirElement("weeklyWorkDays", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal WeeklyWorkDaysElement
            {
                get { return _WeeklyWorkDaysElement; }
                set { _WeeklyWorkDaysElement = value; OnPropertyChanged("WeeklyWorkDaysElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _WeeklyWorkDaysElement;
            
            /// <summary>
            /// Past or Present Job work schedule weekly work days
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? WeeklyWorkDays
            {
                get { return WeeklyWorkDaysElement != null ? WeeklyWorkDaysElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        WeeklyWorkDaysElement = null; 
                    else
                        WeeklyWorkDaysElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("WeeklyWorkDays");
                }
            }
            
            /// <summary>
            /// Past or Present Job work schedule daily work hours
            /// </summary>
            [FhirElement("dailyWorkHours", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal DailyWorkHoursElement
            {
                get { return _DailyWorkHoursElement; }
                set { _DailyWorkHoursElement = value; OnPropertyChanged("DailyWorkHoursElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _DailyWorkHoursElement;
            
            /// <summary>
            /// Past or Present Job work schedule daily work hours
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? DailyWorkHours
            {
                get { return DailyWorkHoursElement != null ? DailyWorkHoursElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        DailyWorkHoursElement = null; 
                    else
                        DailyWorkHoursElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("DailyWorkHours");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as WorkScheduleComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(WeeklyWorkDaysElement != null) dest.WeeklyWorkDaysElement = (Hl7.Fhir.Model.FhirDecimal)WeeklyWorkDaysElement.DeepCopy();
                    if(DailyWorkHoursElement != null) dest.DailyWorkHoursElement = (Hl7.Fhir.Model.FhirDecimal)DailyWorkHoursElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new WorkScheduleComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as WorkScheduleComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(WeeklyWorkDaysElement, otherT.WeeklyWorkDaysElement)) return false;
                if( !DeepComparable.Matches(DailyWorkHoursElement, otherT.DailyWorkHoursElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as WorkScheduleComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(WeeklyWorkDaysElement, otherT.WeeklyWorkDaysElement)) return false;
                if( !DeepComparable.IsExactly(DailyWorkHoursElement, otherT.DailyWorkHoursElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Code != null) yield return Code;
                    if (WeeklyWorkDaysElement != null) yield return WeeklyWorkDaysElement;
                    if (DailyWorkHoursElement != null) yield return DailyWorkHoursElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Code != null) yield return new ElementValue("code", false, Code);
                    if (WeeklyWorkDaysElement != null) yield return new ElementValue("weeklyWorkDays", false, WeeklyWorkDaysElement);
                    if (DailyWorkHoursElement != null) yield return new ElementValue("dailyWorkHours", false, DailyWorkHoursElement);
                }
            }

            
        }
        
        
        /// <summary>
        /// Unique identifier for the occupational data (ODH) record
        /// </summary>
        [FhirElement("identifier", Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// draft | active | retired | unknown
        /// </summary>
        [FhirElement("status", InSummary=true, Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.PublicationStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.PublicationStatus> _StatusElement;
        
        /// <summary>
        /// draft | active | retired | unknown
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.PublicationStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.PublicationStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Who the occupational data (ODH) is collected about
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=110)]
        [CLSCompliant(false)]
		[References("Patient","RelatedPerson")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Occupational Data (ODH) recording time
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
        /// Occupational Data (ODH) recording time
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
        /// Occupational Data (ODH) recorder
        /// </summary>
        [FhirElement("recorder", Order=130)]
        [CLSCompliant(false)]
		[References("Practitioner","PractitionerRole","Patient","RelatedPerson")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Recorder
        {
            get { if(_Recorder==null) _Recorder = new List<Hl7.Fhir.Model.ResourceReference>(); return _Recorder; }
            set { _Recorder = value; OnPropertyChanged("Recorder"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Recorder;
        
        /// <summary>
        /// Occupational Data (ODH) informant
        /// </summary>
        [FhirElement("informant", Order=140)]
        [CLSCompliant(false)]
		[References("Patient","RelatedPerson")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Informant
        {
            get { if(_Informant==null) _Informant = new List<Hl7.Fhir.Model.ResourceReference>(); return _Informant; }
            set { _Informant = value; OnPropertyChanged("Informant"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Informant;
        
        /// <summary>
        /// Employment status
        /// </summary>
        [FhirElement("employmentStatus", InSummary=true, Order=150)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.OccupationalData.EmploymentStatusComponent> EmploymentStatus
        {
            get { if(_EmploymentStatus==null) _EmploymentStatus = new List<Hl7.Fhir.Model.OccupationalData.EmploymentStatusComponent>(); return _EmploymentStatus; }
            set { _EmploymentStatus = value; OnPropertyChanged("EmploymentStatus"); }
        }
        
        private List<Hl7.Fhir.Model.OccupationalData.EmploymentStatusComponent> _EmploymentStatus;
        
        /// <summary>
        /// Retirement date
        /// </summary>
        [FhirElement("retirementDate", InSummary=true, Order=160)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirDateTime> RetirementDateElement
        {
            get { if(_RetirementDateElement==null) _RetirementDateElement = new List<Hl7.Fhir.Model.FhirDateTime>(); return _RetirementDateElement; }
            set { _RetirementDateElement = value; OnPropertyChanged("RetirementDateElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirDateTime> _RetirementDateElement;
        
        /// <summary>
        /// Retirement date
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> RetirementDate
        {
            get { return RetirementDateElement != null ? RetirementDateElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                  RetirementDateElement = null; 
                else
                  RetirementDateElement = new List<Hl7.Fhir.Model.FhirDateTime>(value.Select(elem=>new Hl7.Fhir.Model.FhirDateTime(elem)));
                OnPropertyChanged("RetirementDate");
            }
        }
        
        /// <summary>
        /// Combat Zone Work period
        /// </summary>
        [FhirElement("combatZonePeriod", InSummary=true, Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Period> CombatZonePeriod
        {
            get { if(_CombatZonePeriod==null) _CombatZonePeriod = new List<Hl7.Fhir.Model.Period>(); return _CombatZonePeriod; }
            set { _CombatZonePeriod = value; OnPropertyChanged("CombatZonePeriod"); }
        }
        
        private List<Hl7.Fhir.Model.Period> _CombatZonePeriod;
        
        /// <summary>
        /// Usual Work
        /// </summary>
        [FhirElement("usualWork", InSummary=true, Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.OccupationalData.UsualWorkComponent UsualWork
        {
            get { return _UsualWork; }
            set { _UsualWork = value; OnPropertyChanged("UsualWork"); }
        }
        
        private Hl7.Fhir.Model.OccupationalData.UsualWorkComponent _UsualWork;
        
        /// <summary>
        /// Past or Present Job
        /// </summary>
        [FhirElement("pastOrPresentJob", InSummary=true, Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.OccupationalData.PastOrPresentJobComponent> PastOrPresentJob
        {
            get { if(_PastOrPresentJob==null) _PastOrPresentJob = new List<Hl7.Fhir.Model.OccupationalData.PastOrPresentJobComponent>(); return _PastOrPresentJob; }
            set { _PastOrPresentJob = value; OnPropertyChanged("PastOrPresentJob"); }
        }
        
        private List<Hl7.Fhir.Model.OccupationalData.PastOrPresentJobComponent> _PastOrPresentJob;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as OccupationalData;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.PublicationStatus>)StatusElement.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(Recorder != null) dest.Recorder = new List<Hl7.Fhir.Model.ResourceReference>(Recorder.DeepCopy());
                if(Informant != null) dest.Informant = new List<Hl7.Fhir.Model.ResourceReference>(Informant.DeepCopy());
                if(EmploymentStatus != null) dest.EmploymentStatus = new List<Hl7.Fhir.Model.OccupationalData.EmploymentStatusComponent>(EmploymentStatus.DeepCopy());
                if(RetirementDateElement != null) dest.RetirementDateElement = new List<Hl7.Fhir.Model.FhirDateTime>(RetirementDateElement.DeepCopy());
                if(CombatZonePeriod != null) dest.CombatZonePeriod = new List<Hl7.Fhir.Model.Period>(CombatZonePeriod.DeepCopy());
                if(UsualWork != null) dest.UsualWork = (Hl7.Fhir.Model.OccupationalData.UsualWorkComponent)UsualWork.DeepCopy();
                if(PastOrPresentJob != null) dest.PastOrPresentJob = new List<Hl7.Fhir.Model.OccupationalData.PastOrPresentJobComponent>(PastOrPresentJob.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new OccupationalData());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as OccupationalData;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(Recorder, otherT.Recorder)) return false;
            if( !DeepComparable.Matches(Informant, otherT.Informant)) return false;
            if( !DeepComparable.Matches(EmploymentStatus, otherT.EmploymentStatus)) return false;
            if( !DeepComparable.Matches(RetirementDateElement, otherT.RetirementDateElement)) return false;
            if( !DeepComparable.Matches(CombatZonePeriod, otherT.CombatZonePeriod)) return false;
            if( !DeepComparable.Matches(UsualWork, otherT.UsualWork)) return false;
            if( !DeepComparable.Matches(PastOrPresentJob, otherT.PastOrPresentJob)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as OccupationalData;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(Recorder, otherT.Recorder)) return false;
            if( !DeepComparable.IsExactly(Informant, otherT.Informant)) return false;
            if( !DeepComparable.IsExactly(EmploymentStatus, otherT.EmploymentStatus)) return false;
            if( !DeepComparable.IsExactly(RetirementDateElement, otherT.RetirementDateElement)) return false;
            if( !DeepComparable.IsExactly(CombatZonePeriod, otherT.CombatZonePeriod)) return false;
            if( !DeepComparable.IsExactly(UsualWork, otherT.UsualWork)) return false;
            if( !DeepComparable.IsExactly(PastOrPresentJob, otherT.PastOrPresentJob)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				if (Identifier != null) yield return Identifier;
				if (StatusElement != null) yield return StatusElement;
				if (Subject != null) yield return Subject;
				if (DateElement != null) yield return DateElement;
				foreach (var elem in Recorder) { if (elem != null) yield return elem; }
				foreach (var elem in Informant) { if (elem != null) yield return elem; }
				foreach (var elem in EmploymentStatus) { if (elem != null) yield return elem; }
				foreach (var elem in RetirementDateElement) { if (elem != null) yield return elem; }
				foreach (var elem in CombatZonePeriod) { if (elem != null) yield return elem; }
				if (UsualWork != null) yield return UsualWork;
				foreach (var elem in PastOrPresentJob) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (Identifier != null) yield return new ElementValue("identifier", false, Identifier);
                if (StatusElement != null) yield return new ElementValue("status", false, StatusElement);
                if (Subject != null) yield return new ElementValue("subject", false, Subject);
                if (DateElement != null) yield return new ElementValue("date", false, DateElement);
                foreach (var elem in Recorder) { if (elem != null) yield return new ElementValue("recorder", true, elem); }
                foreach (var elem in Informant) { if (elem != null) yield return new ElementValue("informant", true, elem); }
                foreach (var elem in EmploymentStatus) { if (elem != null) yield return new ElementValue("employmentStatus", true, elem); }
                foreach (var elem in RetirementDateElement) { if (elem != null) yield return new ElementValue("retirementDate", true, elem); }
                foreach (var elem in CombatZonePeriod) { if (elem != null) yield return new ElementValue("combatZonePeriod", true, elem); }
                if (UsualWork != null) yield return new ElementValue("usualWork", false, UsualWork);
                foreach (var elem in PastOrPresentJob) { if (elem != null) yield return new ElementValue("pastOrPresentJob", true, elem); }
            }
        }

    }
    
}
