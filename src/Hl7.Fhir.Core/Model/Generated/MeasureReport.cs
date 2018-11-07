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
    /// Results of a measure evaluation
    /// </summary>
    [FhirType("MeasureReport", IsResource=true)]
    [DataContract]
    public partial class MeasureReport : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.MeasureReport; } }
        [NotMapped]
        public override string TypeName { get { return "MeasureReport"; } }
        
        /// <summary>
        /// The status of the measure report
        /// (url: http://hl7.org/fhir/ValueSet/measure-report-status)
        /// </summary>
        [FhirEnumeration("MeasureReportStatus")]
        public enum MeasureReportStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measure-report-status)
            /// </summary>
            [EnumLiteral("complete", "http://hl7.org/fhir/measure-report-status"), Description("Complete")]
            Complete,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measure-report-status)
            /// </summary>
            [EnumLiteral("pending", "http://hl7.org/fhir/measure-report-status"), Description("Pending")]
            Pending,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measure-report-status)
            /// </summary>
            [EnumLiteral("error", "http://hl7.org/fhir/measure-report-status"), Description("Error")]
            Error,
        }

        /// <summary>
        /// The type of the measure report
        /// (url: http://hl7.org/fhir/ValueSet/measure-report-type)
        /// </summary>
        [FhirEnumeration("MeasureReportType")]
        public enum MeasureReportType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measure-report-type)
            /// </summary>
            [EnumLiteral("individual", "http://hl7.org/fhir/measure-report-type"), Description("Individual")]
            Individual,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measure-report-type)
            /// </summary>
            [EnumLiteral("patient-list", "http://hl7.org/fhir/measure-report-type"), Description("Patient List")]
            PatientList,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measure-report-type)
            /// </summary>
            [EnumLiteral("summary", "http://hl7.org/fhir/measure-report-type"), Description("Summary")]
            Summary,
        }

        [FhirType("GroupComponent")]
        [DataContract]
        public partial class GroupComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "GroupComponent"; } }
            
            /// <summary>
            /// What group of the measure
            /// </summary>
            [FhirElement("identifier", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// The populations in the group
            /// </summary>
            [FhirElement("population", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.MeasureReport.PopulationComponent> Population
            {
                get { if(_Population==null) _Population = new List<Hl7.Fhir.Model.MeasureReport.PopulationComponent>(); return _Population; }
                set { _Population = value; OnPropertyChanged("Population"); }
            }
            
            private List<Hl7.Fhir.Model.MeasureReport.PopulationComponent> _Population;
            
            /// <summary>
            /// What score this group achieved
            /// </summary>
            [FhirElement("measureScore", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal MeasureScoreElement
            {
                get { return _MeasureScoreElement; }
                set { _MeasureScoreElement = value; OnPropertyChanged("MeasureScoreElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _MeasureScoreElement;
            
            /// <summary>
            /// What score this group achieved
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? MeasureScore
            {
                get { return MeasureScoreElement != null ? MeasureScoreElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        MeasureScoreElement = null; 
                    else
                        MeasureScoreElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("MeasureScore");
                }
            }
            
            /// <summary>
            /// Stratification results
            /// </summary>
            [FhirElement("stratifier", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.MeasureReport.StratifierComponent> Stratifier
            {
                get { if(_Stratifier==null) _Stratifier = new List<Hl7.Fhir.Model.MeasureReport.StratifierComponent>(); return _Stratifier; }
                set { _Stratifier = value; OnPropertyChanged("Stratifier"); }
            }
            
            private List<Hl7.Fhir.Model.MeasureReport.StratifierComponent> _Stratifier;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as GroupComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(Population != null) dest.Population = new List<Hl7.Fhir.Model.MeasureReport.PopulationComponent>(Population.DeepCopy());
                    if(MeasureScoreElement != null) dest.MeasureScoreElement = (Hl7.Fhir.Model.FhirDecimal)MeasureScoreElement.DeepCopy();
                    if(Stratifier != null) dest.Stratifier = new List<Hl7.Fhir.Model.MeasureReport.StratifierComponent>(Stratifier.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new GroupComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as GroupComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(Population, otherT.Population)) return false;
                if( !DeepComparable.Matches(MeasureScoreElement, otherT.MeasureScoreElement)) return false;
                if( !DeepComparable.Matches(Stratifier, otherT.Stratifier)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as GroupComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(Population, otherT.Population)) return false;
                if( !DeepComparable.IsExactly(MeasureScoreElement, otherT.MeasureScoreElement)) return false;
                if( !DeepComparable.IsExactly(Stratifier, otherT.Stratifier)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Identifier != null) yield return Identifier;
                    foreach (var elem in Population) { if (elem != null) yield return elem; }
                    if (MeasureScoreElement != null) yield return MeasureScoreElement;
                    foreach (var elem in Stratifier) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                    foreach (var elem in Population) { if (elem != null) yield return new ElementValue("population", elem); }
                    if (MeasureScoreElement != null) yield return new ElementValue("measureScore", MeasureScoreElement);
                    foreach (var elem in Stratifier) { if (elem != null) yield return new ElementValue("stratifier", elem); }
                }
            }

            
        }
        
        
        [FhirType("PopulationComponent")]
        [DataContract]
        public partial class PopulationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "PopulationComponent"; } }
            
            /// <summary>
            /// Population identifier as defined in the measure
            /// </summary>
            [FhirElement("identifier", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// initial-population | numerator | numerator-exclusion | denominator | denominator-exclusion | denominator-exception | measure-population | measure-population-exclusion | measure-score
            /// </summary>
            [FhirElement("code", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// Size of the population
            /// </summary>
            [FhirElement("count", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Integer CountElement
            {
                get { return _CountElement; }
                set { _CountElement = value; OnPropertyChanged("CountElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _CountElement;
            
            /// <summary>
            /// Size of the population
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Count
            {
                get { return CountElement != null ? CountElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        CountElement = null; 
                    else
                        CountElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Count");
                }
            }
            
            /// <summary>
            /// For patient-list reports, the patients in this population
            /// </summary>
            [FhirElement("patients", Order=70)]
            [CLSCompliant(false)]
			[References("List")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Patients
            {
                get { return _Patients; }
                set { _Patients = value; OnPropertyChanged("Patients"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Patients;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PopulationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(CountElement != null) dest.CountElement = (Hl7.Fhir.Model.Integer)CountElement.DeepCopy();
                    if(Patients != null) dest.Patients = (Hl7.Fhir.Model.ResourceReference)Patients.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new PopulationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PopulationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(CountElement, otherT.CountElement)) return false;
                if( !DeepComparable.Matches(Patients, otherT.Patients)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PopulationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(CountElement, otherT.CountElement)) return false;
                if( !DeepComparable.IsExactly(Patients, otherT.Patients)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Identifier != null) yield return Identifier;
                    if (Code != null) yield return Code;
                    if (CountElement != null) yield return CountElement;
                    if (Patients != null) yield return Patients;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                    if (Code != null) yield return new ElementValue("code", Code);
                    if (CountElement != null) yield return new ElementValue("count", CountElement);
                    if (Patients != null) yield return new ElementValue("patients", Patients);
                }
            }

            
        }
        
        
        [FhirType("StratifierComponent")]
        [DataContract]
        public partial class StratifierComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "StratifierComponent"; } }
            
            /// <summary>
            /// What stratifier of the group
            /// </summary>
            [FhirElement("identifier", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// Stratum results, one for each unique value in the stratifier
            /// </summary>
            [FhirElement("stratum", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.MeasureReport.StratifierGroupComponent> Stratum
            {
                get { if(_Stratum==null) _Stratum = new List<Hl7.Fhir.Model.MeasureReport.StratifierGroupComponent>(); return _Stratum; }
                set { _Stratum = value; OnPropertyChanged("Stratum"); }
            }
            
            private List<Hl7.Fhir.Model.MeasureReport.StratifierGroupComponent> _Stratum;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as StratifierComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(Stratum != null) dest.Stratum = new List<Hl7.Fhir.Model.MeasureReport.StratifierGroupComponent>(Stratum.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new StratifierComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as StratifierComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(Stratum, otherT.Stratum)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as StratifierComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(Stratum, otherT.Stratum)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Identifier != null) yield return Identifier;
                    foreach (var elem in Stratum) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                    foreach (var elem in Stratum) { if (elem != null) yield return new ElementValue("stratum", elem); }
                }
            }

            
        }
        
        
        [FhirType("StratifierGroupComponent")]
        [DataContract]
        public partial class StratifierGroupComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "StratifierGroupComponent"; } }
            
            /// <summary>
            /// The stratum value, e.g. male
            /// </summary>
            [FhirElement("value", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ValueElement;
            
            /// <summary>
            /// The stratum value, e.g. male
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Value
            {
                get { return ValueElement != null ? ValueElement.Value : null; }
                set
                {
                    if (value == null)
                        ValueElement = null; 
                    else
                        ValueElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Value");
                }
            }
            
            /// <summary>
            /// Population results in this stratum
            /// </summary>
            [FhirElement("population", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.MeasureReport.StratifierGroupPopulationComponent> Population
            {
                get { if(_Population==null) _Population = new List<Hl7.Fhir.Model.MeasureReport.StratifierGroupPopulationComponent>(); return _Population; }
                set { _Population = value; OnPropertyChanged("Population"); }
            }
            
            private List<Hl7.Fhir.Model.MeasureReport.StratifierGroupPopulationComponent> _Population;
            
            /// <summary>
            /// What score this stratum achieved
            /// </summary>
            [FhirElement("measureScore", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal MeasureScoreElement
            {
                get { return _MeasureScoreElement; }
                set { _MeasureScoreElement = value; OnPropertyChanged("MeasureScoreElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _MeasureScoreElement;
            
            /// <summary>
            /// What score this stratum achieved
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? MeasureScore
            {
                get { return MeasureScoreElement != null ? MeasureScoreElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        MeasureScoreElement = null; 
                    else
                        MeasureScoreElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("MeasureScore");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as StratifierGroupComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirString)ValueElement.DeepCopy();
                    if(Population != null) dest.Population = new List<Hl7.Fhir.Model.MeasureReport.StratifierGroupPopulationComponent>(Population.DeepCopy());
                    if(MeasureScoreElement != null) dest.MeasureScoreElement = (Hl7.Fhir.Model.FhirDecimal)MeasureScoreElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new StratifierGroupComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as StratifierGroupComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
                if( !DeepComparable.Matches(Population, otherT.Population)) return false;
                if( !DeepComparable.Matches(MeasureScoreElement, otherT.MeasureScoreElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as StratifierGroupComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
                if( !DeepComparable.IsExactly(Population, otherT.Population)) return false;
                if( !DeepComparable.IsExactly(MeasureScoreElement, otherT.MeasureScoreElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (ValueElement != null) yield return ValueElement;
                    foreach (var elem in Population) { if (elem != null) yield return elem; }
                    if (MeasureScoreElement != null) yield return MeasureScoreElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                    foreach (var elem in Population) { if (elem != null) yield return new ElementValue("population", elem); }
                    if (MeasureScoreElement != null) yield return new ElementValue("measureScore", MeasureScoreElement);
                }
            }

            
        }
        
        
        [FhirType("StratifierGroupPopulationComponent")]
        [DataContract]
        public partial class StratifierGroupPopulationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "StratifierGroupPopulationComponent"; } }
            
            /// <summary>
            /// Population identifier as defined in the measure
            /// </summary>
            [FhirElement("identifier", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// initial-population | numerator | numerator-exclusion | denominator | denominator-exclusion | denominator-exception | measure-population | measure-population-exclusion | measure-score
            /// </summary>
            [FhirElement("code", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// Size of the population
            /// </summary>
            [FhirElement("count", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Integer CountElement
            {
                get { return _CountElement; }
                set { _CountElement = value; OnPropertyChanged("CountElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _CountElement;
            
            /// <summary>
            /// Size of the population
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Count
            {
                get { return CountElement != null ? CountElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        CountElement = null; 
                    else
                        CountElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Count");
                }
            }
            
            /// <summary>
            /// For patient-list reports, the patients in this population
            /// </summary>
            [FhirElement("patients", Order=70)]
            [CLSCompliant(false)]
			[References("List")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Patients
            {
                get { return _Patients; }
                set { _Patients = value; OnPropertyChanged("Patients"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Patients;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as StratifierGroupPopulationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(CountElement != null) dest.CountElement = (Hl7.Fhir.Model.Integer)CountElement.DeepCopy();
                    if(Patients != null) dest.Patients = (Hl7.Fhir.Model.ResourceReference)Patients.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new StratifierGroupPopulationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as StratifierGroupPopulationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(CountElement, otherT.CountElement)) return false;
                if( !DeepComparable.Matches(Patients, otherT.Patients)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as StratifierGroupPopulationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(CountElement, otherT.CountElement)) return false;
                if( !DeepComparable.IsExactly(Patients, otherT.Patients)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Identifier != null) yield return Identifier;
                    if (Code != null) yield return Code;
                    if (CountElement != null) yield return CountElement;
                    if (Patients != null) yield return Patients;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                    if (Code != null) yield return new ElementValue("code", Code);
                    if (CountElement != null) yield return new ElementValue("count", CountElement);
                    if (Patients != null) yield return new ElementValue("patients", Patients);
                }
            }

            
        }
        
        
        /// <summary>
        /// Additional identifier for the Report
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// complete | pending | error
        /// </summary>
        [FhirElement("status", InSummary=true, Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.MeasureReport.MeasureReportStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.MeasureReport.MeasureReportStatus> _StatusElement;
        
        /// <summary>
        /// complete | pending | error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.MeasureReport.MeasureReportStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.MeasureReport.MeasureReportStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// individual | patient-list | summary
        /// </summary>
        [FhirElement("type", InSummary=true, Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.MeasureReport.MeasureReportType> TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.MeasureReport.MeasureReportType> _TypeElement;
        
        /// <summary>
        /// individual | patient-list | summary
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.MeasureReport.MeasureReportType? Type
        {
            get { return TypeElement != null ? TypeElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  TypeElement = null; 
                else
                  TypeElement = new Code<Hl7.Fhir.Model.MeasureReport.MeasureReportType>(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// What measure was evaluated
        /// </summary>
        [FhirElement("measure", InSummary=true, Order=120)]
        [CLSCompliant(false)]
		[References("Measure")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Measure
        {
            get { return _Measure; }
            set { _Measure = value; OnPropertyChanged("Measure"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Measure;
        
        /// <summary>
        /// What patient the report is for
        /// </summary>
        [FhirElement("patient", InSummary=true, Order=130)]
        [CLSCompliant(false)]
		[References("Patient")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Patient
        {
            get { return _Patient; }
            set { _Patient = value; OnPropertyChanged("Patient"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Patient;
        
        /// <summary>
        /// When the report was generated
        /// </summary>
        [FhirElement("date", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// When the report was generated
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
        /// Who is reporting the data
        /// </summary>
        [FhirElement("reportingOrganization", InSummary=true, Order=150)]
        [CLSCompliant(false)]
		[References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference ReportingOrganization
        {
            get { return _ReportingOrganization; }
            set { _ReportingOrganization = value; OnPropertyChanged("ReportingOrganization"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _ReportingOrganization;
        
        /// <summary>
        /// What period the report covers
        /// </summary>
        [FhirElement("period", InSummary=true, Order=160)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Period Period
        {
            get { return _Period; }
            set { _Period = value; OnPropertyChanged("Period"); }
        }
        
        private Hl7.Fhir.Model.Period _Period;
        
        /// <summary>
        /// Measure results for each group
        /// </summary>
        [FhirElement("group", Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.MeasureReport.GroupComponent> Group
        {
            get { if(_Group==null) _Group = new List<Hl7.Fhir.Model.MeasureReport.GroupComponent>(); return _Group; }
            set { _Group = value; OnPropertyChanged("Group"); }
        }
        
        private List<Hl7.Fhir.Model.MeasureReport.GroupComponent> _Group;
        
        /// <summary>
        /// What data was evaluated to produce the measure score
        /// </summary>
        [FhirElement("evaluatedResources", Order=180)]
        [CLSCompliant(false)]
		[References("Bundle")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference EvaluatedResources
        {
            get { return _EvaluatedResources; }
            set { _EvaluatedResources = value; OnPropertyChanged("EvaluatedResources"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _EvaluatedResources;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as MeasureReport;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.MeasureReport.MeasureReportStatus>)StatusElement.DeepCopy();
                if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.MeasureReport.MeasureReportType>)TypeElement.DeepCopy();
                if(Measure != null) dest.Measure = (Hl7.Fhir.Model.ResourceReference)Measure.DeepCopy();
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(ReportingOrganization != null) dest.ReportingOrganization = (Hl7.Fhir.Model.ResourceReference)ReportingOrganization.DeepCopy();
                if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                if(Group != null) dest.Group = new List<Hl7.Fhir.Model.MeasureReport.GroupComponent>(Group.DeepCopy());
                if(EvaluatedResources != null) dest.EvaluatedResources = (Hl7.Fhir.Model.ResourceReference)EvaluatedResources.DeepCopy();
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new MeasureReport());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as MeasureReport;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.Matches(Measure, otherT.Measure)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(ReportingOrganization, otherT.ReportingOrganization)) return false;
            if( !DeepComparable.Matches(Period, otherT.Period)) return false;
            if( !DeepComparable.Matches(Group, otherT.Group)) return false;
            if( !DeepComparable.Matches(EvaluatedResources, otherT.EvaluatedResources)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as MeasureReport;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(Measure, otherT.Measure)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(ReportingOrganization, otherT.ReportingOrganization)) return false;
            if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
            if( !DeepComparable.IsExactly(Group, otherT.Group)) return false;
            if( !DeepComparable.IsExactly(EvaluatedResources, otherT.EvaluatedResources)) return false;
            
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
				if (TypeElement != null) yield return TypeElement;
				if (Measure != null) yield return Measure;
				if (Patient != null) yield return Patient;
				if (DateElement != null) yield return DateElement;
				if (ReportingOrganization != null) yield return ReportingOrganization;
				if (Period != null) yield return Period;
				foreach (var elem in Group) { if (elem != null) yield return elem; }
				if (EvaluatedResources != null) yield return EvaluatedResources;
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                if (Measure != null) yield return new ElementValue("measure", Measure);
                if (Patient != null) yield return new ElementValue("patient", Patient);
                if (DateElement != null) yield return new ElementValue("date", DateElement);
                if (ReportingOrganization != null) yield return new ElementValue("reportingOrganization", ReportingOrganization);
                if (Period != null) yield return new ElementValue("period", Period);
                foreach (var elem in Group) { if (elem != null) yield return new ElementValue("group", elem); }
                if (EvaluatedResources != null) yield return new ElementValue("evaluatedResources", EvaluatedResources);
            }
        }

    }
    
}
