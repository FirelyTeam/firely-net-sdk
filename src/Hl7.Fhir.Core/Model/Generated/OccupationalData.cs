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
// Generated for FHIR v3.2.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Patient’s or family member's work information
    /// </summary>
    [FhirType("OccupationalData", IsResource=true)]
    [DataContract]
    public partial class OccupationalData : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.OccupationalData; } }
        [NotMapped]
        public override string TypeName { get { return "OccupationalData"; } }
        
        /// <summary>
        /// 74165-2 History of Employment Status (CodeSystem: LOINC urn:oid:2.16.840.1.113883.6.1)
        /// (url: http://hl7.org/fhir/ValueSet/history-of-employment-status)
        /// </summary>
        [FhirEnumeration("HistoryOfEmploymentStatus")]
        public enum HistoryOfEmploymentStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://loinc.org)
            /// </summary>
            [EnumLiteral("74165-2", "http://loinc.org"), Description("History of employment status NIOSH")]
            N741652,
        }

        /// <summary>
        /// 87510-4 Retirement Status (CodeSystem: LOINC urn:oid:2.16.840.1.113883.6.1)
        /// (url: http://hl7.org/fhir/ValueSet/retirement-status)
        /// </summary>
        [FhirEnumeration("RetirementStatus")]
        public enum RetirementStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/retirement-status)
            /// </summary>
            [EnumLiteral("87510-4", "http://hl7.org/fhir/retirement-status"), Description("Retirement Status")]
            N875104,
        }

        /// <summary>
        /// 87511-2 Hazardous Duty Work (CodeSystem: LOINC urn:oid:2.16.840.1.113883.6.1)
        /// (url: http://hl7.org/fhir/ValueSet/hazadardous-duty-work)
        /// </summary>
        [FhirEnumeration("HazardousDutyWork")]
        public enum HazardousDutyWork
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/hazadardous-duty-work)
            /// </summary>
            [EnumLiteral("87511-2", "http://hl7.org/fhir/hazadardous-duty-work"), Description("Hazardous Duty Work")]
            N875112,
        }

        /// <summary>
        /// 21843-8 Usual Occupation (CodeSystem: LOINC urn:oid:2.16.840.1.113883.6.1)
        /// (url: http://hl7.org/fhir/ValueSet/usual-occupation)
        /// </summary>
        [FhirEnumeration("UsualOccupation")]
        public enum UsualOccupation
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://loinc.org)
            /// </summary>
            [EnumLiteral("21843-8", "http://loinc.org"), Description("History of Usual occupation")]
            N218438,
        }

        [FhirType("EmploymentStatusComponent")]
        [DataContract]
        public partial class EmploymentStatusComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "EmploymentStatusComponent"; } }
            
            /// <summary>
            /// 74165-2
            /// </summary>
            [FhirElement("code", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.OccupationalData.HistoryOfEmploymentStatus> CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.OccupationalData.HistoryOfEmploymentStatus> _CodeElement;
            
            /// <summary>
            /// 74165-2
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.OccupationalData.HistoryOfEmploymentStatus? Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        CodeElement = null; 
                    else
                        CodeElement = new Code<Hl7.Fhir.Model.OccupationalData.HistoryOfEmploymentStatus>(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Employment status effective time
            /// </summary>
            [FhirElement("effective", Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Effective
            {
                get { return _Effective; }
                set { _Effective = value; OnPropertyChanged("Effective"); }
            }
            
            private Hl7.Fhir.Model.Element _Effective;
            
            /// <summary>
            /// Employment status value
            /// </summary>
            [FhirElement("value", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Value;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as EmploymentStatusComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Code<Hl7.Fhir.Model.OccupationalData.HistoryOfEmploymentStatus>)CodeElement.DeepCopy();
                    if(Effective != null) dest.Effective = (Hl7.Fhir.Model.Element)Effective.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.CodeableConcept)Value.DeepCopy();
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
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(Effective, otherT.Effective)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as EmploymentStatusComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(Effective, otherT.Effective)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (CodeElement != null) yield return CodeElement;
                    if (Effective != null) yield return Effective;
                    if (Value != null) yield return Value;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (CodeElement != null) yield return new ElementValue("code", false, CodeElement);
                    if (Effective != null) yield return new ElementValue("effective", false, Effective);
                    if (Value != null) yield return new ElementValue("value", false, Value);
                }
            }

            
        }
        
        
        [FhirType("RetirementStatusComponent")]
        [DataContract]
        public partial class RetirementStatusComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "RetirementStatusComponent"; } }
            
            /// <summary>
            /// 87510-4
            /// </summary>
            [FhirElement("code", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.OccupationalData.RetirementStatus> CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.OccupationalData.RetirementStatus> _CodeElement;
            
            /// <summary>
            /// 87510-4
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.OccupationalData.RetirementStatus? Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        CodeElement = null; 
                    else
                        CodeElement = new Code<Hl7.Fhir.Model.OccupationalData.RetirementStatus>(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Retirement status effective time
            /// </summary>
            [FhirElement("effective", Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Effective
            {
                get { return _Effective; }
                set { _Effective = value; OnPropertyChanged("Effective"); }
            }
            
            private Hl7.Fhir.Model.Element _Effective;
            
            /// <summary>
            /// Retirement status value
            /// </summary>
            [FhirElement("value", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Value;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RetirementStatusComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Code<Hl7.Fhir.Model.OccupationalData.RetirementStatus>)CodeElement.DeepCopy();
                    if(Effective != null) dest.Effective = (Hl7.Fhir.Model.Element)Effective.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.CodeableConcept)Value.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new RetirementStatusComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RetirementStatusComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(Effective, otherT.Effective)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RetirementStatusComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(Effective, otherT.Effective)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (CodeElement != null) yield return CodeElement;
                    if (Effective != null) yield return Effective;
                    if (Value != null) yield return Value;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (CodeElement != null) yield return new ElementValue("code", false, CodeElement);
                    if (Effective != null) yield return new ElementValue("effective", false, Effective);
                    if (Value != null) yield return new ElementValue("value", false, Value);
                }
            }

            
        }
        
        
        [FhirType("CombatZoneHazardousDutyComponent")]
        [DataContract]
        public partial class CombatZoneHazardousDutyComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "CombatZoneHazardousDutyComponent"; } }
            
            /// <summary>
            /// 87511-2
            /// </summary>
            [FhirElement("code", Order=40)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Code<Hl7.Fhir.Model.OccupationalData.HazardousDutyWork>> CodeElement
            {
                get { if(_CodeElement==null) _CodeElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.OccupationalData.HazardousDutyWork>>(); return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private List<Code<Hl7.Fhir.Model.OccupationalData.HazardousDutyWork>> _CodeElement;
            
            /// <summary>
            /// 87511-2
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<Hl7.Fhir.Model.OccupationalData.HazardousDutyWork?> Code
            {
                get { return CodeElement != null ? CodeElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        CodeElement = null; 
                    else
                        CodeElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.OccupationalData.HazardousDutyWork>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.OccupationalData.HazardousDutyWork>(elem)));
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Combat Zone Hazardous Duty effective time
            /// </summary>
            [FhirElement("effective", Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Effective
            {
                get { return _Effective; }
                set { _Effective = value; OnPropertyChanged("Effective"); }
            }
            
            private Hl7.Fhir.Model.Element _Effective;
            
            /// <summary>
            /// Combat Zone Hazardous Duty value
            /// </summary>
            [FhirElement("value", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Value;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CombatZoneHazardousDutyComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = new List<Code<Hl7.Fhir.Model.OccupationalData.HazardousDutyWork>>(CodeElement.DeepCopy());
                    if(Effective != null) dest.Effective = (Hl7.Fhir.Model.Element)Effective.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.CodeableConcept)Value.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new CombatZoneHazardousDutyComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CombatZoneHazardousDutyComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(Effective, otherT.Effective)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CombatZoneHazardousDutyComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(Effective, otherT.Effective)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in CodeElement) { if (elem != null) yield return elem; }
                    if (Effective != null) yield return Effective;
                    if (Value != null) yield return Value;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in CodeElement) { if (elem != null) yield return new ElementValue("code", true, elem); }
                    if (Effective != null) yield return new ElementValue("effective", false, Effective);
                    if (Value != null) yield return new ElementValue("value", false, Value);
                }
            }

            
        }
        
        
        [FhirType("UsualOccupationComponent")]
        [DataContract]
        public partial class UsualOccupationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "UsualOccupationComponent"; } }
            
            /// <summary>
            /// 21843-8
            /// </summary>
            [FhirElement("code", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.OccupationalData.UsualOccupation> CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.OccupationalData.UsualOccupation> _CodeElement;
            
            /// <summary>
            /// 21843-8
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.OccupationalData.UsualOccupation? Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        CodeElement = null; 
                    else
                        CodeElement = new Code<Hl7.Fhir.Model.OccupationalData.UsualOccupation>(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Usual Occupation effective time
            /// </summary>
            [FhirElement("effective", Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Effective
            {
                get { return _Effective; }
                set { _Effective = value; OnPropertyChanged("Effective"); }
            }
            
            private Hl7.Fhir.Model.Element _Effective;
            
            /// <summary>
            /// Usual Occupation value
            /// </summary>
            [FhirElement("value", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Value;
            
            /// <summary>
            /// Usual Occupation duration
            /// </summary>
            [FhirElement("duration", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.OccupationalData.DurationComponent Duration
            {
                get { return _Duration; }
                set { _Duration = value; OnPropertyChanged("Duration"); }
            }
            
            private Hl7.Fhir.Model.OccupationalData.DurationComponent _Duration;
            
            /// <summary>
            /// Usual Occupation industry
            /// </summary>
            [FhirElement("industry", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.OccupationalData.IndustryComponent Industry
            {
                get { return _Industry; }
                set { _Industry = value; OnPropertyChanged("Industry"); }
            }
            
            private Hl7.Fhir.Model.OccupationalData.IndustryComponent _Industry;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as UsualOccupationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Code<Hl7.Fhir.Model.OccupationalData.UsualOccupation>)CodeElement.DeepCopy();
                    if(Effective != null) dest.Effective = (Hl7.Fhir.Model.Element)Effective.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.CodeableConcept)Value.DeepCopy();
                    if(Duration != null) dest.Duration = (Hl7.Fhir.Model.OccupationalData.DurationComponent)Duration.DeepCopy();
                    if(Industry != null) dest.Industry = (Hl7.Fhir.Model.OccupationalData.IndustryComponent)Industry.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new UsualOccupationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as UsualOccupationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(Effective, otherT.Effective)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
                if( !DeepComparable.Matches(Duration, otherT.Duration)) return false;
                if( !DeepComparable.Matches(Industry, otherT.Industry)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as UsualOccupationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(Effective, otherT.Effective)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                if( !DeepComparable.IsExactly(Duration, otherT.Duration)) return false;
                if( !DeepComparable.IsExactly(Industry, otherT.Industry)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (CodeElement != null) yield return CodeElement;
                    if (Effective != null) yield return Effective;
                    if (Value != null) yield return Value;
                    if (Duration != null) yield return Duration;
                    if (Industry != null) yield return Industry;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (CodeElement != null) yield return new ElementValue("code", false, CodeElement);
                    if (Effective != null) yield return new ElementValue("effective", false, Effective);
                    if (Value != null) yield return new ElementValue("value", false, Value);
                    if (Duration != null) yield return new ElementValue("duration", false, Duration);
                    if (Industry != null) yield return new ElementValue("industry", false, Industry);
                }
            }

            
        }
        
        
        [FhirType("DurationComponent")]
        [DataContract]
        public partial class DurationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "DurationComponent"; } }
            
            /// <summary>
            /// Usual Occupation duration code
            /// </summary>
            [FhirElement("code", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// Usual Occupation duration value
            /// </summary>
            [FhirElement("value", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Period Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.Period _Value;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DurationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Period)Value.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DurationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DurationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DurationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Code != null) yield return Code;
                    if (Value != null) yield return Value;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Code != null) yield return new ElementValue("code", false, Code);
                    if (Value != null) yield return new ElementValue("value", false, Value);
                }
            }

            
        }
        
        
        [FhirType("IndustryComponent")]
        [DataContract]
        public partial class IndustryComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "IndustryComponent"; } }
            
            /// <summary>
            /// Usual Occupation industry code
            /// </summary>
            [FhirElement("code", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// Usual Occupation industry value
            /// </summary>
            [FhirElement("value", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Value;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as IndustryComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.CodeableConcept)Value.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new IndustryComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as IndustryComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as IndustryComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Code != null) yield return Code;
                    if (Value != null) yield return Value;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Code != null) yield return new ElementValue("code", false, Code);
                    if (Value != null) yield return new ElementValue("value", false, Value);
                }
            }

            
        }
        
        
        [FhirType("PastOrPresentOccupationComponent")]
        [DataContract]
        public partial class PastOrPresentOccupationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "PastOrPresentOccupationComponent"; } }
            
            /// <summary>
            /// Past Or Present Occupation code
            /// </summary>
            [FhirElement("code", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// Past Or Present Occupation effective time
            /// </summary>
            [FhirElement("effective", Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Effective
            {
                get { return _Effective; }
                set { _Effective = value; OnPropertyChanged("Effective"); }
            }
            
            private Hl7.Fhir.Model.Element _Effective;
            
            /// <summary>
            /// Past Or Present Occupation value
            /// </summary>
            [FhirElement("value", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Value;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PastOrPresentOccupationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(Effective != null) dest.Effective = (Hl7.Fhir.Model.Element)Effective.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.CodeableConcept)Value.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new PastOrPresentOccupationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PastOrPresentOccupationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Effective, otherT.Effective)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PastOrPresentOccupationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Effective, otherT.Effective)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                
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
                    if (Value != null) yield return Value;
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
                    if (Value != null) yield return new ElementValue("value", false, Value);
                }
            }

            
        }
        
        
        /// <summary>
        /// Unique identifier for the occupational data record
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
        /// Who the occupational data is collected about
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=110)]
        [CLSCompliant(false)]
		[References("Patient")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Occupational Data author time
        /// </summary>
        [FhirElement("date", InSummary=true, Order=120)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// Occupational Data author time
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
        /// Occupational Data author
        /// </summary>
        [FhirElement("author", InSummary=true, Order=130)]
        [CLSCompliant(false)]
		[References("Practitioner","PractitionerRole","Patient","RelatedPerson")]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Author
        {
            get { if(_Author==null) _Author = new List<Hl7.Fhir.Model.ResourceReference>(); return _Author; }
            set { _Author = value; OnPropertyChanged("Author"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Author;
        
        /// <summary>
        /// Employment status
        /// </summary>
        [FhirElement("employmentStatus", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.OccupationalData.EmploymentStatusComponent EmploymentStatus
        {
            get { return _EmploymentStatus; }
            set { _EmploymentStatus = value; OnPropertyChanged("EmploymentStatus"); }
        }
        
        private Hl7.Fhir.Model.OccupationalData.EmploymentStatusComponent _EmploymentStatus;
        
        /// <summary>
        /// Retirement status
        /// </summary>
        [FhirElement("retirementStatus", Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.OccupationalData.RetirementStatusComponent RetirementStatus_
        {
            get { return _RetirementStatus_; }
            set { _RetirementStatus_ = value; OnPropertyChanged("RetirementStatus_"); }
        }
        
        private Hl7.Fhir.Model.OccupationalData.RetirementStatusComponent _RetirementStatus_;
        
        /// <summary>
        /// Combat Zone Hazardous Duty
        /// </summary>
        [FhirElement("combatZoneHazardousDuty", Order=160)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.OccupationalData.CombatZoneHazardousDutyComponent> CombatZoneHazardousDuty
        {
            get { if(_CombatZoneHazardousDuty==null) _CombatZoneHazardousDuty = new List<Hl7.Fhir.Model.OccupationalData.CombatZoneHazardousDutyComponent>(); return _CombatZoneHazardousDuty; }
            set { _CombatZoneHazardousDuty = value; OnPropertyChanged("CombatZoneHazardousDuty"); }
        }
        
        private List<Hl7.Fhir.Model.OccupationalData.CombatZoneHazardousDutyComponent> _CombatZoneHazardousDuty;
        
        /// <summary>
        /// Usual Occupation
        /// </summary>
        [FhirElement("usualOccupation", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.OccupationalData.UsualOccupationComponent UsualOccupation_
        {
            get { return _UsualOccupation_; }
            set { _UsualOccupation_ = value; OnPropertyChanged("UsualOccupation_"); }
        }
        
        private Hl7.Fhir.Model.OccupationalData.UsualOccupationComponent _UsualOccupation_;
        
        /// <summary>
        /// Past Or Present Occupation
        /// </summary>
        [FhirElement("pastOrPresentOccupation", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.OccupationalData.PastOrPresentOccupationComponent PastOrPresentOccupation
        {
            get { return _PastOrPresentOccupation; }
            set { _PastOrPresentOccupation = value; OnPropertyChanged("PastOrPresentOccupation"); }
        }
        
        private Hl7.Fhir.Model.OccupationalData.PastOrPresentOccupationComponent _PastOrPresentOccupation;
        

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
                if(Author != null) dest.Author = new List<Hl7.Fhir.Model.ResourceReference>(Author.DeepCopy());
                if(EmploymentStatus != null) dest.EmploymentStatus = (Hl7.Fhir.Model.OccupationalData.EmploymentStatusComponent)EmploymentStatus.DeepCopy();
                if(RetirementStatus_ != null) dest.RetirementStatus_ = (Hl7.Fhir.Model.OccupationalData.RetirementStatusComponent)RetirementStatus_.DeepCopy();
                if(CombatZoneHazardousDuty != null) dest.CombatZoneHazardousDuty = new List<Hl7.Fhir.Model.OccupationalData.CombatZoneHazardousDutyComponent>(CombatZoneHazardousDuty.DeepCopy());
                if(UsualOccupation_ != null) dest.UsualOccupation_ = (Hl7.Fhir.Model.OccupationalData.UsualOccupationComponent)UsualOccupation_.DeepCopy();
                if(PastOrPresentOccupation != null) dest.PastOrPresentOccupation = (Hl7.Fhir.Model.OccupationalData.PastOrPresentOccupationComponent)PastOrPresentOccupation.DeepCopy();
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
            if( !DeepComparable.Matches(Author, otherT.Author)) return false;
            if( !DeepComparable.Matches(EmploymentStatus, otherT.EmploymentStatus)) return false;
            if( !DeepComparable.Matches(RetirementStatus_, otherT.RetirementStatus_)) return false;
            if( !DeepComparable.Matches(CombatZoneHazardousDuty, otherT.CombatZoneHazardousDuty)) return false;
            if( !DeepComparable.Matches(UsualOccupation_, otherT.UsualOccupation_)) return false;
            if( !DeepComparable.Matches(PastOrPresentOccupation, otherT.PastOrPresentOccupation)) return false;
            
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
            if( !DeepComparable.IsExactly(Author, otherT.Author)) return false;
            if( !DeepComparable.IsExactly(EmploymentStatus, otherT.EmploymentStatus)) return false;
            if( !DeepComparable.IsExactly(RetirementStatus_, otherT.RetirementStatus_)) return false;
            if( !DeepComparable.IsExactly(CombatZoneHazardousDuty, otherT.CombatZoneHazardousDuty)) return false;
            if( !DeepComparable.IsExactly(UsualOccupation_, otherT.UsualOccupation_)) return false;
            if( !DeepComparable.IsExactly(PastOrPresentOccupation, otherT.PastOrPresentOccupation)) return false;
            
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
				foreach (var elem in Author) { if (elem != null) yield return elem; }
				if (EmploymentStatus != null) yield return EmploymentStatus;
				if (RetirementStatus_ != null) yield return RetirementStatus_;
				foreach (var elem in CombatZoneHazardousDuty) { if (elem != null) yield return elem; }
				if (UsualOccupation_ != null) yield return UsualOccupation_;
				if (PastOrPresentOccupation != null) yield return PastOrPresentOccupation;
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
                foreach (var elem in Author) { if (elem != null) yield return new ElementValue("author", true, elem); }
                if (EmploymentStatus != null) yield return new ElementValue("employmentStatus", false, EmploymentStatus);
                if (RetirementStatus_ != null) yield return new ElementValue("retirementStatus", false, RetirementStatus_);
                foreach (var elem in CombatZoneHazardousDuty) { if (elem != null) yield return new ElementValue("combatZoneHazardousDuty", true, elem); }
                if (UsualOccupation_ != null) yield return new ElementValue("usualOccupation", false, UsualOccupation_);
                if (PastOrPresentOccupation != null) yield return new ElementValue("pastOrPresentOccupation", false, PastOrPresentOccupation);
            }
        }

    }
    
}
