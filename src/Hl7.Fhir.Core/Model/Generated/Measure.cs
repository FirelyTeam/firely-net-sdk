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
// Generated for FHIR v1.0.2
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
        /// The type of population
        /// (url: http://hl7.org/fhir/ValueSet/measure-population)
        /// </summary>
        [FhirEnumeration("MeasurePopulationType")]
        public enum MeasurePopulationType
        {
            /// <summary>
            /// The initial population for the measure
            /// (system: http://hl7.org/fhir/measure-population)
            /// </summary>
            [EnumLiteral("initial-population"), Description("Initial Population")]
            InitialPopulation,
            /// <summary>
            /// The numerator for the measure
            /// (system: http://hl7.org/fhir/measure-population)
            /// </summary>
            [EnumLiteral("numerator"), Description("Numerator")]
            Numerator,
            /// <summary>
            /// The numerator exclusion for the measure
            /// (system: http://hl7.org/fhir/measure-population)
            /// </summary>
            [EnumLiteral("numerator-exclusion"), Description("Numerator Exclusion")]
            NumeratorExclusion,
            /// <summary>
            /// The denominator for the measure
            /// (system: http://hl7.org/fhir/measure-population)
            /// </summary>
            [EnumLiteral("denominator"), Description("Denominator")]
            Denominator,
            /// <summary>
            /// The denominator exclusion for the measure
            /// (system: http://hl7.org/fhir/measure-population)
            /// </summary>
            [EnumLiteral("denominator-exclusion"), Description("Denominator Exclusion")]
            DenominatorExclusion,
            /// <summary>
            /// The denominator exception for the measure
            /// (system: http://hl7.org/fhir/measure-population)
            /// </summary>
            [EnumLiteral("denominator-exception"), Description("Denominator Exception")]
            DenominatorException,
            /// <summary>
            /// The measure population for the measure
            /// (system: http://hl7.org/fhir/measure-population)
            /// </summary>
            [EnumLiteral("measure-population"), Description("Measure Population")]
            MeasurePopulation,
            /// <summary>
            /// The measure population exclusion for the measure
            /// (system: http://hl7.org/fhir/measure-population)
            /// </summary>
            [EnumLiteral("measure-population-exclusion"), Description("Measure Population Exclusion")]
            MeasurePopulationExclusion,
            /// <summary>
            /// The measure score for the measure
            /// (system: http://hl7.org/fhir/measure-population)
            /// </summary>
            [EnumLiteral("measure-score"), Description("Measure Score")]
            MeasureScore,
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
            [FhirElement("type", Order=40)]
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
            /// 
            /// </summary>
            [FhirElement("name", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// 
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
            /// 
            /// </summary>
            [FhirElement("description", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// 
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
            /// 
            /// </summary>
            [FhirElement("criteria", Order=70)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CriteriaElement
            {
                get { return _CriteriaElement; }
                set { _CriteriaElement = value; OnPropertyChanged("CriteriaElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CriteriaElement;
            
            /// <summary>
            /// 
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
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(CriteriaElement, otherT.CriteriaElement)) return false;
                
                return true;
            }
            
        }
        
        
        /// <summary>
        /// Logical identifier
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
        /// The version of the module, if any
        /// </summary>
        [FhirElement("version", InSummary=true, Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement
        {
            get { return _VersionElement; }
            set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _VersionElement;
        
        /// <summary>
        /// The version of the module, if any
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Version
        {
            get { return VersionElement != null ? VersionElement.Value : null; }
            set
            {
                if(value == null)
                  VersionElement = null; 
                else
                  VersionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Version");
            }
        }
        
        /// <summary>
        /// Metadata for the measure
        /// </summary>
        [FhirElement("moduleMetadata", Order=110)]
        [References("ModuleMetadata")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference ModuleMetadata
        {
            get { return _ModuleMetadata; }
            set { _ModuleMetadata = value; OnPropertyChanged("ModuleMetadata"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _ModuleMetadata;
        
        /// <summary>
        /// Logic used by the measure
        /// </summary>
        [FhirElement("library", Order=120)]
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
        /// 
        /// </summary>
        [FhirElement("population", Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Measure.PopulationComponent> Population
        {
            get { if(_Population==null) _Population = new List<Hl7.Fhir.Model.Measure.PopulationComponent>(); return _Population; }
            set { _Population = value; OnPropertyChanged("Population"); }
        }
        
        private List<Hl7.Fhir.Model.Measure.PopulationComponent> _Population;
        
        /// <summary>
        /// 
        /// </summary>
        [FhirElement("stratifier", Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> StratifierElement
        {
            get { if(_StratifierElement==null) _StratifierElement = new List<Hl7.Fhir.Model.FhirString>(); return _StratifierElement; }
            set { _StratifierElement = value; OnPropertyChanged("StratifierElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _StratifierElement;
        
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Stratifier
        {
            get { return StratifierElement != null ? StratifierElement.Select(elem => elem.Value) : null; }
            set
            {
                if(value == null)
                  StratifierElement = null; 
                else
                  StratifierElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("Stratifier");
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        [FhirElement("supplementalData", Order=150)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> SupplementalDataElement
        {
            get { if(_SupplementalDataElement==null) _SupplementalDataElement = new List<Hl7.Fhir.Model.FhirString>(); return _SupplementalDataElement; }
            set { _SupplementalDataElement = value; OnPropertyChanged("SupplementalDataElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _SupplementalDataElement;
        
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> SupplementalData
        {
            get { return SupplementalDataElement != null ? SupplementalDataElement.Select(elem => elem.Value) : null; }
            set
            {
                if(value == null)
                  SupplementalDataElement = null; 
                else
                  SupplementalDataElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("SupplementalData");
            }
        }
        
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Measure;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(ModuleMetadata != null) dest.ModuleMetadata = (Hl7.Fhir.Model.ResourceReference)ModuleMetadata.DeepCopy();
                if(Library != null) dest.Library = new List<Hl7.Fhir.Model.ResourceReference>(Library.DeepCopy());
                if(Population != null) dest.Population = new List<Hl7.Fhir.Model.Measure.PopulationComponent>(Population.DeepCopy());
                if(StratifierElement != null) dest.StratifierElement = new List<Hl7.Fhir.Model.FhirString>(StratifierElement.DeepCopy());
                if(SupplementalDataElement != null) dest.SupplementalDataElement = new List<Hl7.Fhir.Model.FhirString>(SupplementalDataElement.DeepCopy());
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
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.Matches(ModuleMetadata, otherT.ModuleMetadata)) return false;
            if( !DeepComparable.Matches(Library, otherT.Library)) return false;
            if( !DeepComparable.Matches(Population, otherT.Population)) return false;
            if( !DeepComparable.Matches(StratifierElement, otherT.StratifierElement)) return false;
            if( !DeepComparable.Matches(SupplementalDataElement, otherT.SupplementalDataElement)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Measure;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.IsExactly(ModuleMetadata, otherT.ModuleMetadata)) return false;
            if( !DeepComparable.IsExactly(Library, otherT.Library)) return false;
            if( !DeepComparable.IsExactly(Population, otherT.Population)) return false;
            if( !DeepComparable.IsExactly(StratifierElement, otherT.StratifierElement)) return false;
            if( !DeepComparable.IsExactly(SupplementalDataElement, otherT.SupplementalDataElement)) return false;
            
            return true;
        }
        
    }
    
}
