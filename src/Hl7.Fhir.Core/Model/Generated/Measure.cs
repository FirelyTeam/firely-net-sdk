using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using System.ComponentModel;

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
// Generated for FHIR v1.3.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A quality measure
    /// </summary>
    [FhirType("Measure", IsResource=true)]
    [DataContract]
    public partial class Measure : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Measure; } }
        [NotMapped]
        public override string TypeName { get { return "Measure"; } }
        
        /// <summary>
        /// The scoring type of the measure
        /// (url: http://hl7.org/fhir/ValueSet/measure-scoring)
        /// </summary>
        [FhirEnumeration("MeasureScoring")]
        public enum MeasureScoring
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measure-scoring)
            /// </summary>
            [EnumLiteral("proportion"), Description("Proportion")]
            Proportion,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measure-scoring)
            /// </summary>
            [EnumLiteral("ratio"), Description("Ratio")]
            Ratio,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measure-scoring)
            /// </summary>
            [EnumLiteral("continuous-variable"), Description("Continuous Variable")]
            ContinuousVariable,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measure-scoring)
            /// </summary>
            [EnumLiteral("cohort"), Description("Cohort")]
            Cohort,
        }

        /// <summary>
        /// The type of measure
        /// (url: http://hl7.org/fhir/ValueSet/measure-type)
        /// </summary>
        [FhirEnumeration("MeasureType")]
        public enum MeasureType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measure-type)
            /// </summary>
            [EnumLiteral("process"), Description("Process")]
            Process,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measure-type)
            /// </summary>
            [EnumLiteral("outcome"), Description("Outcome")]
            Outcome,
        }

        /// <summary>
        /// The type of population
        /// (url: http://hl7.org/fhir/ValueSet/measure-population)
        /// </summary>
        [FhirEnumeration("MeasurePopulationType")]
        public enum MeasurePopulationType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measure-population)
            /// </summary>
            [EnumLiteral("initial-population"), Description("Initial Population")]
            InitialPopulation,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measure-population)
            /// </summary>
            [EnumLiteral("numerator"), Description("Numerator")]
            Numerator,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measure-population)
            /// </summary>
            [EnumLiteral("numerator-exclusion"), Description("Numerator Exclusion")]
            NumeratorExclusion,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measure-population)
            /// </summary>
            [EnumLiteral("denominator"), Description("Denominator")]
            Denominator,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measure-population)
            /// </summary>
            [EnumLiteral("denominator-exclusion"), Description("Denominator Exclusion")]
            DenominatorExclusion,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measure-population)
            /// </summary>
            [EnumLiteral("denominator-exception"), Description("Denominator Exception")]
            DenominatorException,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measure-population)
            /// </summary>
            [EnumLiteral("measure-population"), Description("Measure Population")]
            MeasurePopulation,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measure-population)
            /// </summary>
            [EnumLiteral("measure-population-exclusion"), Description("Measure Population Exclusion")]
            MeasurePopulationExclusion,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measure-population)
            /// </summary>
            [EnumLiteral("measure-score"), Description("Measure Score")]
            MeasureScore,
        }

        /// <summary>
        /// The intended usage for supplemental data elements in the measure
        /// (url: http://hl7.org/fhir/ValueSet/measure-data-usage)
        /// </summary>
        [FhirEnumeration("MeasureDataUsage")]
        public enum MeasureDataUsage
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measure-data-usage)
            /// </summary>
            [EnumLiteral("supplemental-data"), Description("Supplemental Data")]
            SupplementalData,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/measure-data-usage)
            /// </summary>
            [EnumLiteral("risk-adjustment-factor"), Description("Risk Adjustment Factor")]
            RiskAdjustmentFactor,
        }

        [FhirType("GroupComponent")]
        [DataContract]
        public partial class GroupComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "GroupComponent"; } }
            
            /// <summary>
            /// Unique identifier
            /// </summary>
            [FhirElement("identifier", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// Short name
            /// </summary>
            [FhirElement("name", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Short name
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if(value == null)
                      NameElement = null; 
                    else
                      NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// Summary description
            /// </summary>
            [FhirElement("description", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Summary description
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
            /// Population criteria
            /// </summary>
            [FhirElement("population", InSummary=true, Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Measure.PopulationComponent> Population
            {
                get { if(_Population==null) _Population = new List<Hl7.Fhir.Model.Measure.PopulationComponent>(); return _Population; }
                set { _Population = value; OnPropertyChanged("Population"); }
            }
            
            private List<Hl7.Fhir.Model.Measure.PopulationComponent> _Population;
            
            /// <summary>
            /// Stratifier criteria for the measure
            /// </summary>
            [FhirElement("stratifier", InSummary=true, Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Measure.StratifierComponent> Stratifier
            {
                get { if(_Stratifier==null) _Stratifier = new List<Hl7.Fhir.Model.Measure.StratifierComponent>(); return _Stratifier; }
                set { _Stratifier = value; OnPropertyChanged("Stratifier"); }
            }
            
            private List<Hl7.Fhir.Model.Measure.StratifierComponent> _Stratifier;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as GroupComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Population != null) dest.Population = new List<Hl7.Fhir.Model.Measure.PopulationComponent>(Population.DeepCopy());
                    if(Stratifier != null) dest.Stratifier = new List<Hl7.Fhir.Model.Measure.StratifierComponent>(Stratifier.DeepCopy());
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
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(Population, otherT.Population)) return false;
                if( !DeepComparable.Matches(Stratifier, otherT.Stratifier)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as GroupComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Population, otherT.Population)) return false;
                if( !DeepComparable.IsExactly(Stratifier, otherT.Stratifier)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("PopulationComponent")]
        [DataContract]
        public partial class PopulationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "PopulationComponent"; } }
            
            /// <summary>
            /// initial-population | numerator | numerator-exclusion | denominator | denominator-exclusion | denominator-exception | measure-population | measure-population-exclusion | measure-score
            /// </summary>
            [FhirElement("type", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Measure.MeasurePopulationType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.Measure.MeasurePopulationType> _TypeElement;
            
            /// <summary>
            /// initial-population | numerator | numerator-exclusion | denominator | denominator-exclusion | denominator-exception | measure-population | measure-population-exclusion | measure-score
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Measure.MeasurePopulationType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if(value == null)
                      TypeElement = null; 
                    else
                      TypeElement = new Code<Hl7.Fhir.Model.Measure.MeasurePopulationType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Unique identifier
            /// </summary>
            [FhirElement("identifier", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// Short name
            /// </summary>
            [FhirElement("name", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Short name
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Name
            {
                get { return NameElement != null ? NameElement.Value : null; }
                set
                {
                    if(value == null)
                      NameElement = null; 
                    else
                      NameElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Name");
                }
            }
            
            /// <summary>
            /// The human readable description of this population criteria
            /// </summary>
            [FhirElement("description", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// The human readable description of this population criteria
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
            /// The name of a valid referenced CQL expression (may be namespaced) that defines this population criteria
            /// </summary>
            [FhirElement("criteria", InSummary=true, Order=80)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CriteriaElement
            {
                get { return _CriteriaElement; }
                set { _CriteriaElement = value; OnPropertyChanged("CriteriaElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CriteriaElement;
            
            /// <summary>
            /// The name of a valid referenced CQL expression (may be namespaced) that defines this population criteria
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Criteria
            {
                get { return CriteriaElement != null ? CriteriaElement.Value : null; }
                set
                {
                    if(value == null)
                      CriteriaElement = null; 
                    else
                      CriteriaElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Criteria");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PopulationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.Measure.MeasurePopulationType>)TypeElement.DeepCopy();
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(CriteriaElement != null) dest.CriteriaElement = (Hl7.Fhir.Model.FhirString)CriteriaElement.DeepCopy();
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
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(CriteriaElement, otherT.CriteriaElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PopulationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(CriteriaElement, otherT.CriteriaElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("StratifierComponent")]
        [DataContract]
        public partial class StratifierComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "StratifierComponent"; } }
            
            /// <summary>
            /// The identifier for the stratifier used to coordinate the reported data back to this stratifier
            /// </summary>
            [FhirElement("identifier", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// Stratifier criteria
            /// </summary>
            [FhirElement("criteria", InSummary=true, Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CriteriaElement
            {
                get { return _CriteriaElement; }
                set { _CriteriaElement = value; OnPropertyChanged("CriteriaElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CriteriaElement;
            
            /// <summary>
            /// Stratifier criteria
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Criteria
            {
                get { return CriteriaElement != null ? CriteriaElement.Value : null; }
                set
                {
                    if(value == null)
                      CriteriaElement = null; 
                    else
                      CriteriaElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Criteria");
                }
            }
            
            /// <summary>
            /// Path to the stratifier
            /// </summary>
            [FhirElement("path", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PathElement
            {
                get { return _PathElement; }
                set { _PathElement = value; OnPropertyChanged("PathElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PathElement;
            
            /// <summary>
            /// Path to the stratifier
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Path
            {
                get { return PathElement != null ? PathElement.Value : null; }
                set
                {
                    if(value == null)
                      PathElement = null; 
                    else
                      PathElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Path");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as StratifierComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(CriteriaElement != null) dest.CriteriaElement = (Hl7.Fhir.Model.FhirString)CriteriaElement.DeepCopy();
                    if(PathElement != null) dest.PathElement = (Hl7.Fhir.Model.FhirString)PathElement.DeepCopy();
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
                if( !DeepComparable.Matches(CriteriaElement, otherT.CriteriaElement)) return false;
                if( !DeepComparable.Matches(PathElement, otherT.PathElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as StratifierComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(CriteriaElement, otherT.CriteriaElement)) return false;
                if( !DeepComparable.IsExactly(PathElement, otherT.PathElement)) return false;
                
                return true;
            }
            
        }
        
        
        [FhirType("SupplementalDataComponent")]
        [DataContract]
        public partial class SupplementalDataComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "SupplementalDataComponent"; } }
            
            /// <summary>
            /// Identifier, unique within the measure
            /// </summary>
            [FhirElement("identifier", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// supplemental-data | risk-adjustment-factor
            /// </summary>
            [FhirElement("usage", InSummary=true, Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Code<Hl7.Fhir.Model.Measure.MeasureDataUsage>> UsageElement
            {
                get { if(_UsageElement==null) _UsageElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.Measure.MeasureDataUsage>>(); return _UsageElement; }
                set { _UsageElement = value; OnPropertyChanged("UsageElement"); }
            }
            
            private List<Code<Hl7.Fhir.Model.Measure.MeasureDataUsage>> _UsageElement;
            
            /// <summary>
            /// supplemental-data | risk-adjustment-factor
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<Hl7.Fhir.Model.Measure.MeasureDataUsage?> Usage
            {
                get { return UsageElement != null ? UsageElement.Select(elem => elem.Value) : null; }
                set
                {
                    if(value == null)
                      UsageElement = null; 
                    else
                      UsageElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.Measure.MeasureDataUsage>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.Measure.MeasureDataUsage>(elem)));
                    OnPropertyChanged("Usage");
                }
            }
            
            /// <summary>
            /// Supplemental data criteria
            /// </summary>
            [FhirElement("criteria", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CriteriaElement
            {
                get { return _CriteriaElement; }
                set { _CriteriaElement = value; OnPropertyChanged("CriteriaElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CriteriaElement;
            
            /// <summary>
            /// Supplemental data criteria
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Criteria
            {
                get { return CriteriaElement != null ? CriteriaElement.Value : null; }
                set
                {
                    if(value == null)
                      CriteriaElement = null; 
                    else
                      CriteriaElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Criteria");
                }
            }
            
            /// <summary>
            /// Path to the supplemental data element
            /// </summary>
            [FhirElement("path", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PathElement
            {
                get { return _PathElement; }
                set { _PathElement = value; OnPropertyChanged("PathElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PathElement;
            
            /// <summary>
            /// Path to the supplemental data element
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Path
            {
                get { return PathElement != null ? PathElement.Value : null; }
                set
                {
                    if(value == null)
                      PathElement = null; 
                    else
                      PathElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Path");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SupplementalDataComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(UsageElement != null) dest.UsageElement = new List<Code<Hl7.Fhir.Model.Measure.MeasureDataUsage>>(UsageElement.DeepCopy());
                    if(CriteriaElement != null) dest.CriteriaElement = (Hl7.Fhir.Model.FhirString)CriteriaElement.DeepCopy();
                    if(PathElement != null) dest.PathElement = (Hl7.Fhir.Model.FhirString)PathElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SupplementalDataComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SupplementalDataComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(UsageElement, otherT.UsageElement)) return false;
                if( !DeepComparable.Matches(CriteriaElement, otherT.CriteriaElement)) return false;
                if( !DeepComparable.Matches(PathElement, otherT.PathElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SupplementalDataComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(UsageElement, otherT.UsageElement)) return false;
                if( !DeepComparable.IsExactly(CriteriaElement, otherT.CriteriaElement)) return false;
                if( !DeepComparable.IsExactly(PathElement, otherT.PathElement)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// Metadata for the measure
        /// </summary>
        [FhirElement("moduleMetadata", InSummary=true, Order=90)]
        [DataMember]
        public ModuleMetadata ModuleMetadata
        {
            get { return _ModuleMetadata; }
            set { _ModuleMetadata = value; OnPropertyChanged("ModuleMetadata"); }
        }
        
        private ModuleMetadata _ModuleMetadata;
        
        /// <summary>
        /// Logic used by the measure
        /// </summary>
        [FhirElement("library", InSummary=true, Order=100)]
        [References("Library")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Library
        {
            get { if(_Library==null) _Library = new List<Hl7.Fhir.Model.ResourceReference>(); return _Library; }
            set { _Library = value; OnPropertyChanged("Library"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Library;
        
        /// <summary>
        /// Disclaimer for the measure
        /// </summary>
        [FhirElement("disclaimer", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown Disclaimer
        {
            get { return _Disclaimer; }
            set { _Disclaimer = value; OnPropertyChanged("Disclaimer"); }
        }
        
        private Hl7.Fhir.Model.Markdown _Disclaimer;
        
        /// <summary>
        /// proportion | ratio | continuous-variable | cohort
        /// </summary>
        [FhirElement("scoring", InSummary=true, Order=120)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Measure.MeasureScoring> ScoringElement
        {
            get { return _ScoringElement; }
            set { _ScoringElement = value; OnPropertyChanged("ScoringElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Measure.MeasureScoring> _ScoringElement;
        
        /// <summary>
        /// proportion | ratio | continuous-variable | cohort
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Measure.MeasureScoring? Scoring
        {
            get { return ScoringElement != null ? ScoringElement.Value : null; }
            set
            {
                if(value == null)
                  ScoringElement = null; 
                else
                  ScoringElement = new Code<Hl7.Fhir.Model.Measure.MeasureScoring>(value);
                OnPropertyChanged("Scoring");
            }
        }
        
        /// <summary>
        /// process | outcome
        /// </summary>
        [FhirElement("type", InSummary=true, Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Code<Hl7.Fhir.Model.Measure.MeasureType>> TypeElement
        {
            get { if(_TypeElement==null) _TypeElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.Measure.MeasureType>>(); return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        
        private List<Code<Hl7.Fhir.Model.Measure.MeasureType>> _TypeElement;
        
        /// <summary>
        /// process | outcome
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<Hl7.Fhir.Model.Measure.MeasureType?> Type
        {
            get { return TypeElement != null ? TypeElement.Select(elem => elem.Value) : null; }
            set
            {
                if(value == null)
                  TypeElement = null; 
                else
                  TypeElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.Measure.MeasureType>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.Measure.MeasureType>(elem)));
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// How is risk adjustment applied for this measure
        /// </summary>
        [FhirElement("riskAdjustment", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString RiskAdjustmentElement
        {
            get { return _RiskAdjustmentElement; }
            set { _RiskAdjustmentElement = value; OnPropertyChanged("RiskAdjustmentElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _RiskAdjustmentElement;
        
        /// <summary>
        /// How is risk adjustment applied for this measure
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string RiskAdjustment
        {
            get { return RiskAdjustmentElement != null ? RiskAdjustmentElement.Value : null; }
            set
            {
                if(value == null)
                  RiskAdjustmentElement = null; 
                else
                  RiskAdjustmentElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("RiskAdjustment");
            }
        }
        
        /// <summary>
        /// How is rate aggregation performed for this measure
        /// </summary>
        [FhirElement("rateAggregation", InSummary=true, Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString RateAggregationElement
        {
            get { return _RateAggregationElement; }
            set { _RateAggregationElement = value; OnPropertyChanged("RateAggregationElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _RateAggregationElement;
        
        /// <summary>
        /// How is rate aggregation performed for this measure
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string RateAggregation
        {
            get { return RateAggregationElement != null ? RateAggregationElement.Value : null; }
            set
            {
                if(value == null)
                  RateAggregationElement = null; 
                else
                  RateAggregationElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("RateAggregation");
            }
        }
        
        /// <summary>
        /// Why does this measure exist
        /// </summary>
        [FhirElement("rationale", InSummary=true, Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown Rationale
        {
            get { return _Rationale; }
            set { _Rationale = value; OnPropertyChanged("Rationale"); }
        }
        
        private Hl7.Fhir.Model.Markdown _Rationale;
        
        /// <summary>
        /// Clinical recommendation
        /// </summary>
        [FhirElement("clinicalRecommendationStatement", InSummary=true, Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown ClinicalRecommendationStatement
        {
            get { return _ClinicalRecommendationStatement; }
            set { _ClinicalRecommendationStatement = value; OnPropertyChanged("ClinicalRecommendationStatement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _ClinicalRecommendationStatement;
        
        /// <summary>
        /// Improvement notation for the measure, e.g. higher score indicates better quality
        /// </summary>
        [FhirElement("improvementNotation", InSummary=true, Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ImprovementNotationElement
        {
            get { return _ImprovementNotationElement; }
            set { _ImprovementNotationElement = value; OnPropertyChanged("ImprovementNotationElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _ImprovementNotationElement;
        
        /// <summary>
        /// Improvement notation for the measure, e.g. higher score indicates better quality
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ImprovementNotation
        {
            get { return ImprovementNotationElement != null ? ImprovementNotationElement.Value : null; }
            set
            {
                if(value == null)
                  ImprovementNotationElement = null; 
                else
                  ImprovementNotationElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("ImprovementNotation");
            }
        }
        
        /// <summary>
        /// A natural language definition of the measure
        /// </summary>
        [FhirElement("definition", InSummary=true, Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown Definition
        {
            get { return _Definition; }
            set { _Definition = value; OnPropertyChanged("Definition"); }
        }
        
        private Hl7.Fhir.Model.Markdown _Definition;
        
        /// <summary>
        /// The guidance for the measure
        /// </summary>
        [FhirElement("guidance", InSummary=true, Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown Guidance
        {
            get { return _Guidance; }
            set { _Guidance = value; OnPropertyChanged("Guidance"); }
        }
        
        private Hl7.Fhir.Model.Markdown _Guidance;
        
        /// <summary>
        /// The measure set, e.g. Preventive Care and Screening
        /// </summary>
        [FhirElement("set", InSummary=true, Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString SetElement
        {
            get { return _SetElement; }
            set { _SetElement = value; OnPropertyChanged("SetElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _SetElement;
        
        /// <summary>
        /// The measure set, e.g. Preventive Care and Screening
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Set
        {
            get { return SetElement != null ? SetElement.Value : null; }
            set
            {
                if(value == null)
                  SetElement = null; 
                else
                  SetElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Set");
            }
        }
        
        /// <summary>
        /// Population criteria group
        /// </summary>
        [FhirElement("group", InSummary=true, Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Measure.GroupComponent> Group
        {
            get { if(_Group==null) _Group = new List<Hl7.Fhir.Model.Measure.GroupComponent>(); return _Group; }
            set { _Group = value; OnPropertyChanged("Group"); }
        }
        
        private List<Hl7.Fhir.Model.Measure.GroupComponent> _Group;
        
        /// <summary>
        /// Supplemental data
        /// </summary>
        [FhirElement("supplementalData", InSummary=true, Order=230)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Measure.SupplementalDataComponent> SupplementalData
        {
            get { if(_SupplementalData==null) _SupplementalData = new List<Hl7.Fhir.Model.Measure.SupplementalDataComponent>(); return _SupplementalData; }
            set { _SupplementalData = value; OnPropertyChanged("SupplementalData"); }
        }
        
        private List<Hl7.Fhir.Model.Measure.SupplementalDataComponent> _SupplementalData;
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Measure;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(ModuleMetadata != null) dest.ModuleMetadata = (ModuleMetadata)ModuleMetadata.DeepCopy();
                if(Library != null) dest.Library = new List<Hl7.Fhir.Model.ResourceReference>(Library.DeepCopy());
                if(Disclaimer != null) dest.Disclaimer = (Hl7.Fhir.Model.Markdown)Disclaimer.DeepCopy();
                if(ScoringElement != null) dest.ScoringElement = (Code<Hl7.Fhir.Model.Measure.MeasureScoring>)ScoringElement.DeepCopy();
                if(TypeElement != null) dest.TypeElement = new List<Code<Hl7.Fhir.Model.Measure.MeasureType>>(TypeElement.DeepCopy());
                if(RiskAdjustmentElement != null) dest.RiskAdjustmentElement = (Hl7.Fhir.Model.FhirString)RiskAdjustmentElement.DeepCopy();
                if(RateAggregationElement != null) dest.RateAggregationElement = (Hl7.Fhir.Model.FhirString)RateAggregationElement.DeepCopy();
                if(Rationale != null) dest.Rationale = (Hl7.Fhir.Model.Markdown)Rationale.DeepCopy();
                if(ClinicalRecommendationStatement != null) dest.ClinicalRecommendationStatement = (Hl7.Fhir.Model.Markdown)ClinicalRecommendationStatement.DeepCopy();
                if(ImprovementNotationElement != null) dest.ImprovementNotationElement = (Hl7.Fhir.Model.FhirString)ImprovementNotationElement.DeepCopy();
                if(Definition != null) dest.Definition = (Hl7.Fhir.Model.Markdown)Definition.DeepCopy();
                if(Guidance != null) dest.Guidance = (Hl7.Fhir.Model.Markdown)Guidance.DeepCopy();
                if(SetElement != null) dest.SetElement = (Hl7.Fhir.Model.FhirString)SetElement.DeepCopy();
                if(Group != null) dest.Group = new List<Hl7.Fhir.Model.Measure.GroupComponent>(Group.DeepCopy());
                if(SupplementalData != null) dest.SupplementalData = new List<Hl7.Fhir.Model.Measure.SupplementalDataComponent>(SupplementalData.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Measure());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Measure;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(ModuleMetadata, otherT.ModuleMetadata)) return false;
            if( !DeepComparable.Matches(Library, otherT.Library)) return false;
            if( !DeepComparable.Matches(Disclaimer, otherT.Disclaimer)) return false;
            if( !DeepComparable.Matches(ScoringElement, otherT.ScoringElement)) return false;
            if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.Matches(RiskAdjustmentElement, otherT.RiskAdjustmentElement)) return false;
            if( !DeepComparable.Matches(RateAggregationElement, otherT.RateAggregationElement)) return false;
            if( !DeepComparable.Matches(Rationale, otherT.Rationale)) return false;
            if( !DeepComparable.Matches(ClinicalRecommendationStatement, otherT.ClinicalRecommendationStatement)) return false;
            if( !DeepComparable.Matches(ImprovementNotationElement, otherT.ImprovementNotationElement)) return false;
            if( !DeepComparable.Matches(Definition, otherT.Definition)) return false;
            if( !DeepComparable.Matches(Guidance, otherT.Guidance)) return false;
            if( !DeepComparable.Matches(SetElement, otherT.SetElement)) return false;
            if( !DeepComparable.Matches(Group, otherT.Group)) return false;
            if( !DeepComparable.Matches(SupplementalData, otherT.SupplementalData)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Measure;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(ModuleMetadata, otherT.ModuleMetadata)) return false;
            if( !DeepComparable.IsExactly(Library, otherT.Library)) return false;
            if( !DeepComparable.IsExactly(Disclaimer, otherT.Disclaimer)) return false;
            if( !DeepComparable.IsExactly(ScoringElement, otherT.ScoringElement)) return false;
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(RiskAdjustmentElement, otherT.RiskAdjustmentElement)) return false;
            if( !DeepComparable.IsExactly(RateAggregationElement, otherT.RateAggregationElement)) return false;
            if( !DeepComparable.IsExactly(Rationale, otherT.Rationale)) return false;
            if( !DeepComparable.IsExactly(ClinicalRecommendationStatement, otherT.ClinicalRecommendationStatement)) return false;
            if( !DeepComparable.IsExactly(ImprovementNotationElement, otherT.ImprovementNotationElement)) return false;
            if( !DeepComparable.IsExactly(Definition, otherT.Definition)) return false;
            if( !DeepComparable.IsExactly(Guidance, otherT.Guidance)) return false;
            if( !DeepComparable.IsExactly(SetElement, otherT.SetElement)) return false;
            if( !DeepComparable.IsExactly(Group, otherT.Group)) return false;
            if( !DeepComparable.IsExactly(SupplementalData, otherT.SupplementalData)) return false;
            
            return true;
        }
        
    }
    
}
