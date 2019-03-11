﻿using System;
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
// Generated for FHIR v4.0.0
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
        /// The status of the measure report.
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
        /// The type of the measure report.
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
            [EnumLiteral("subject-list", "http://hl7.org/fhir/measure-report-type"), Description("Subject List")]
            SubjectList,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measure-report-type)
            /// </summary>
            [EnumLiteral("summary", "http://hl7.org/fhir/measure-report-type"), Description("Summary")]
            Summary,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measure-report-type)
            /// </summary>
            [EnumLiteral("data-collection", "http://hl7.org/fhir/measure-report-type"), Description("Data Collection")]
            DataCollection,
        }

        [FhirType("GroupComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class GroupComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "GroupComponent"; } }
            
            /// <summary>
            /// Meaning of the group
            /// </summary>
            [FhirElement("code", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
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
            public Quantity MeasureScore
            {
                get { return _MeasureScore; }
                set { _MeasureScore = value; OnPropertyChanged("MeasureScore"); }
            }
            
            private Quantity _MeasureScore;
            
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
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(Population != null) dest.Population = new List<Hl7.Fhir.Model.MeasureReport.PopulationComponent>(Population.DeepCopy());
                    if(MeasureScore != null) dest.MeasureScore = (Quantity)MeasureScore.DeepCopy();
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
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Population, otherT.Population)) return false;
                if( !DeepComparable.Matches(MeasureScore, otherT.MeasureScore)) return false;
                if( !DeepComparable.Matches(Stratifier, otherT.Stratifier)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as GroupComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Population, otherT.Population)) return false;
                if( !DeepComparable.IsExactly(MeasureScore, otherT.MeasureScore)) return false;
                if( !DeepComparable.IsExactly(Stratifier, otherT.Stratifier)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Code != null) yield return Code;
                    foreach (var elem in Population) { if (elem != null) yield return elem; }
                    if (MeasureScore != null) yield return MeasureScore;
                    foreach (var elem in Stratifier) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Code != null) yield return new ElementValue("code", Code);
                    foreach (var elem in Population) { if (elem != null) yield return new ElementValue("population", elem); }
                    if (MeasureScore != null) yield return new ElementValue("measureScore", MeasureScore);
                    foreach (var elem in Stratifier) { if (elem != null) yield return new ElementValue("stratifier", elem); }
                }
            }

            
        }
        
        
        [FhirType("PopulationComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class PopulationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "PopulationComponent"; } }
            
            /// <summary>
            /// initial-population | numerator | numerator-exclusion | denominator | denominator-exclusion | denominator-exception | measure-population | measure-population-exclusion | measure-observation
            /// </summary>
            [FhirElement("code", InSummary=true, Order=40)]
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
            [FhirElement("count", Order=50)]
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
            /// For subject-list reports, the subject results in this population
            /// </summary>
            [FhirElement("subjectResults", Order=60)]
            [CLSCompliant(false)]
			[References("List")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference SubjectResults
            {
                get { return _SubjectResults; }
                set { _SubjectResults = value; OnPropertyChanged("SubjectResults"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _SubjectResults;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PopulationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(CountElement != null) dest.CountElement = (Hl7.Fhir.Model.Integer)CountElement.DeepCopy();
                    if(SubjectResults != null) dest.SubjectResults = (Hl7.Fhir.Model.ResourceReference)SubjectResults.DeepCopy();
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
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(CountElement, otherT.CountElement)) return false;
                if( !DeepComparable.Matches(SubjectResults, otherT.SubjectResults)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PopulationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(CountElement, otherT.CountElement)) return false;
                if( !DeepComparable.IsExactly(SubjectResults, otherT.SubjectResults)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Code != null) yield return Code;
                    if (CountElement != null) yield return CountElement;
                    if (SubjectResults != null) yield return SubjectResults;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Code != null) yield return new ElementValue("code", Code);
                    if (CountElement != null) yield return new ElementValue("count", CountElement);
                    if (SubjectResults != null) yield return new ElementValue("subjectResults", SubjectResults);
                }
            }

            
        }
        
        
        [FhirType("StratifierComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class StratifierComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "StratifierComponent"; } }
            
            /// <summary>
            /// What stratifier of the group
            /// </summary>
            [FhirElement("code", Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Code
            {
                get { if(_Code==null) _Code = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Code;
            
            /// <summary>
            /// Stratum results, one for each unique value, or set of values, in the stratifier, or stratifier components
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
                    if(Code != null) dest.Code = new List<Hl7.Fhir.Model.CodeableConcept>(Code.DeepCopy());
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
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Stratum, otherT.Stratum)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as StratifierComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Stratum, otherT.Stratum)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Code) { if (elem != null) yield return elem; }
                    foreach (var elem in Stratum) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Code) { if (elem != null) yield return new ElementValue("code", elem); }
                    foreach (var elem in Stratum) { if (elem != null) yield return new ElementValue("stratum", elem); }
                }
            }

            
        }
        
        
        [FhirType("StratifierGroupComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class StratifierGroupComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "StratifierGroupComponent"; } }
            
            /// <summary>
            /// The stratum value, e.g. male
            /// </summary>
            [FhirElement("value", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Value;
            
            /// <summary>
            /// Stratifier component values
            /// </summary>
            [FhirElement("component", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.MeasureReport.ComponentComponent> Component
            {
                get { if(_Component==null) _Component = new List<Hl7.Fhir.Model.MeasureReport.ComponentComponent>(); return _Component; }
                set { _Component = value; OnPropertyChanged("Component"); }
            }
            
            private List<Hl7.Fhir.Model.MeasureReport.ComponentComponent> _Component;
            
            /// <summary>
            /// Population results in this stratum
            /// </summary>
            [FhirElement("population", Order=60)]
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
            [FhirElement("measureScore", Order=70)]
            [DataMember]
            public Quantity MeasureScore
            {
                get { return _MeasureScore; }
                set { _MeasureScore = value; OnPropertyChanged("MeasureScore"); }
            }
            
            private Quantity _MeasureScore;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as StratifierGroupComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.CodeableConcept)Value.DeepCopy();
                    if(Component != null) dest.Component = new List<Hl7.Fhir.Model.MeasureReport.ComponentComponent>(Component.DeepCopy());
                    if(Population != null) dest.Population = new List<Hl7.Fhir.Model.MeasureReport.StratifierGroupPopulationComponent>(Population.DeepCopy());
                    if(MeasureScore != null) dest.MeasureScore = (Quantity)MeasureScore.DeepCopy();
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
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
                if( !DeepComparable.Matches(Component, otherT.Component)) return false;
                if( !DeepComparable.Matches(Population, otherT.Population)) return false;
                if( !DeepComparable.Matches(MeasureScore, otherT.MeasureScore)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as StratifierGroupComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                if( !DeepComparable.IsExactly(Component, otherT.Component)) return false;
                if( !DeepComparable.IsExactly(Population, otherT.Population)) return false;
                if( !DeepComparable.IsExactly(MeasureScore, otherT.MeasureScore)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Value != null) yield return Value;
                    foreach (var elem in Component) { if (elem != null) yield return elem; }
                    foreach (var elem in Population) { if (elem != null) yield return elem; }
                    if (MeasureScore != null) yield return MeasureScore;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Value != null) yield return new ElementValue("value", Value);
                    foreach (var elem in Component) { if (elem != null) yield return new ElementValue("component", elem); }
                    foreach (var elem in Population) { if (elem != null) yield return new ElementValue("population", elem); }
                    if (MeasureScore != null) yield return new ElementValue("measureScore", MeasureScore);
                }
            }

            
        }
        
        
        [FhirType("ComponentComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ComponentComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ComponentComponent"; } }
            
            /// <summary>
            /// What stratifier component of the group
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
            /// The stratum component value, e.g. male
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
                var dest = other as ComponentComponent;
                
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
                return CopyTo(new ComponentComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ComponentComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ComponentComponent;
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
                    if (Code != null) yield return new ElementValue("code", Code);
                    if (Value != null) yield return new ElementValue("value", Value);
                }
            }

            
        }
        
        
        [FhirType("StratifierGroupPopulationComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class StratifierGroupPopulationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "StratifierGroupPopulationComponent"; } }
            
            /// <summary>
            /// initial-population | numerator | numerator-exclusion | denominator | denominator-exclusion | denominator-exception | measure-population | measure-population-exclusion | measure-observation
            /// </summary>
            [FhirElement("code", Order=40)]
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
            [FhirElement("count", Order=50)]
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
            /// For subject-list reports, the subject results in this population
            /// </summary>
            [FhirElement("subjectResults", Order=60)]
            [CLSCompliant(false)]
			[References("List")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference SubjectResults
            {
                get { return _SubjectResults; }
                set { _SubjectResults = value; OnPropertyChanged("SubjectResults"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _SubjectResults;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as StratifierGroupPopulationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(CountElement != null) dest.CountElement = (Hl7.Fhir.Model.Integer)CountElement.DeepCopy();
                    if(SubjectResults != null) dest.SubjectResults = (Hl7.Fhir.Model.ResourceReference)SubjectResults.DeepCopy();
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
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(CountElement, otherT.CountElement)) return false;
                if( !DeepComparable.Matches(SubjectResults, otherT.SubjectResults)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as StratifierGroupPopulationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(CountElement, otherT.CountElement)) return false;
                if( !DeepComparable.IsExactly(SubjectResults, otherT.SubjectResults)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Code != null) yield return Code;
                    if (CountElement != null) yield return CountElement;
                    if (SubjectResults != null) yield return SubjectResults;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Code != null) yield return new ElementValue("code", Code);
                    if (CountElement != null) yield return new ElementValue("count", CountElement);
                    if (SubjectResults != null) yield return new ElementValue("subjectResults", SubjectResults);
                }
            }

            
        }
        
        
        /// <summary>
        /// Additional identifier for the MeasureReport
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=90)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
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
        /// individual | subject-list | summary | data-collection
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
        /// individual | subject-list | summary | data-collection
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
        /// What measure was calculated
        /// </summary>
        [FhirElement("measure", InSummary=true, Order=120)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.Canonical MeasureElement
        {
            get { return _MeasureElement; }
            set { _MeasureElement = value; OnPropertyChanged("MeasureElement"); }
        }
        
        private Hl7.Fhir.Model.Canonical _MeasureElement;
        
        /// <summary>
        /// What measure was calculated
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Measure
        {
            get { return MeasureElement != null ? MeasureElement.Value : null; }
            set
            {
                if (value == null)
                  MeasureElement = null; 
                else
                  MeasureElement = new Hl7.Fhir.Model.Canonical(value);
                OnPropertyChanged("Measure");
            }
        }
        
        /// <summary>
        /// What individual(s) the report is for
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=130)]
        [CLSCompliant(false)]
		[References("Patient","Practitioner","PractitionerRole","Location","Device","RelatedPerson","Group")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
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
        [FhirElement("reporter", InSummary=true, Order=150)]
        [CLSCompliant(false)]
		[References("Practitioner","PractitionerRole","Location","Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Reporter
        {
            get { return _Reporter; }
            set { _Reporter = value; OnPropertyChanged("Reporter"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Reporter;
        
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
        /// increase | decrease
        /// </summary>
        [FhirElement("improvementNotation", InSummary=true, Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept ImprovementNotation
        {
            get { return _ImprovementNotation; }
            set { _ImprovementNotation = value; OnPropertyChanged("ImprovementNotation"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _ImprovementNotation;
        
        /// <summary>
        /// Measure results for each group
        /// </summary>
        [FhirElement("group", Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.MeasureReport.GroupComponent> Group
        {
            get { if(_Group==null) _Group = new List<Hl7.Fhir.Model.MeasureReport.GroupComponent>(); return _Group; }
            set { _Group = value; OnPropertyChanged("Group"); }
        }
        
        private List<Hl7.Fhir.Model.MeasureReport.GroupComponent> _Group;
        
        /// <summary>
        /// What data was used to calculate the measure score
        /// </summary>
        [FhirElement("evaluatedResource", Order=190)]
        [CLSCompliant(false)]
		[References()]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> EvaluatedResource
        {
            get { if(_EvaluatedResource==null) _EvaluatedResource = new List<Hl7.Fhir.Model.ResourceReference>(); return _EvaluatedResource; }
            set { _EvaluatedResource = value; OnPropertyChanged("EvaluatedResource"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _EvaluatedResource;
        

        public static ElementDefinition.ConstraintComponent MeasureReport_MRP_2 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "group.stratifier.stratum.all(value.exists() xor component.exists())",
            Key = "mrp-2",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Stratifiers SHALL be either a single criteria or a set of criteria components",
            Xpath = "not(f:kind/@value='data-collection') or not(exists(f:group))"
        };

        public static ElementDefinition.ConstraintComponent MeasureReport_MRP_1 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "(type != 'data-collection') or group.exists().not()",
            Key = "mrp-1",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Measure Reports used for data collection SHALL NOT communicate group and score information",
            Xpath = "not(f:kind/@value='data-collection') or not(exists(f:group))"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(MeasureReport_MRP_2);
            InvariantConstraints.Add(MeasureReport_MRP_1);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as MeasureReport;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.MeasureReport.MeasureReportStatus>)StatusElement.DeepCopy();
                if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.MeasureReport.MeasureReportType>)TypeElement.DeepCopy();
                if(MeasureElement != null) dest.MeasureElement = (Hl7.Fhir.Model.Canonical)MeasureElement.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(Reporter != null) dest.Reporter = (Hl7.Fhir.Model.ResourceReference)Reporter.DeepCopy();
                if(Period != null) dest.Period = (Hl7.Fhir.Model.Period)Period.DeepCopy();
                if(ImprovementNotation != null) dest.ImprovementNotation = (Hl7.Fhir.Model.CodeableConcept)ImprovementNotation.DeepCopy();
                if(Group != null) dest.Group = new List<Hl7.Fhir.Model.MeasureReport.GroupComponent>(Group.DeepCopy());
                if(EvaluatedResource != null) dest.EvaluatedResource = new List<Hl7.Fhir.Model.ResourceReference>(EvaluatedResource.DeepCopy());
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
            if( !DeepComparable.Matches(MeasureElement, otherT.MeasureElement)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(Reporter, otherT.Reporter)) return false;
            if( !DeepComparable.Matches(Period, otherT.Period)) return false;
            if( !DeepComparable.Matches(ImprovementNotation, otherT.ImprovementNotation)) return false;
            if( !DeepComparable.Matches(Group, otherT.Group)) return false;
            if( !DeepComparable.Matches(EvaluatedResource, otherT.EvaluatedResource)) return false;
            
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
            if( !DeepComparable.IsExactly(MeasureElement, otherT.MeasureElement)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(Reporter, otherT.Reporter)) return false;
            if( !DeepComparable.IsExactly(Period, otherT.Period)) return false;
            if( !DeepComparable.IsExactly(ImprovementNotation, otherT.ImprovementNotation)) return false;
            if( !DeepComparable.IsExactly(Group, otherT.Group)) return false;
            if( !DeepComparable.IsExactly(EvaluatedResource, otherT.EvaluatedResource)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				if (StatusElement != null) yield return StatusElement;
				if (TypeElement != null) yield return TypeElement;
				if (MeasureElement != null) yield return MeasureElement;
				if (Subject != null) yield return Subject;
				if (DateElement != null) yield return DateElement;
				if (Reporter != null) yield return Reporter;
				if (Period != null) yield return Period;
				if (ImprovementNotation != null) yield return ImprovementNotation;
				foreach (var elem in Group) { if (elem != null) yield return elem; }
				foreach (var elem in EvaluatedResource) { if (elem != null) yield return elem; }
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
                if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                if (MeasureElement != null) yield return new ElementValue("measure", MeasureElement);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (DateElement != null) yield return new ElementValue("date", DateElement);
                if (Reporter != null) yield return new ElementValue("reporter", Reporter);
                if (Period != null) yield return new ElementValue("period", Period);
                if (ImprovementNotation != null) yield return new ElementValue("improvementNotation", ImprovementNotation);
                foreach (var elem in Group) { if (elem != null) yield return new ElementValue("group", elem); }
                foreach (var elem in EvaluatedResource) { if (elem != null) yield return new ElementValue("evaluatedResource", elem); }
            }
        }

    }
    
}
