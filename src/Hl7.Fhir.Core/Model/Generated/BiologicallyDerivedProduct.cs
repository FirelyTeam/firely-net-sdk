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
    /// A material substance originating from a biological entity
    /// </summary>
    [FhirType("BiologicallyDerivedProduct", IsResource=true)]
    [DataContract]
    public partial class BiologicallyDerivedProduct : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.BiologicallyDerivedProduct; } }
        [NotMapped]
        public override string TypeName { get { return "BiologicallyDerivedProduct"; } }
        
        /// <summary>
        /// Biologically Derived Product Category.
        /// (url: http://hl7.org/fhir/ValueSet/product-category)
        /// </summary>
        [FhirEnumeration("BiologicallyDerivedProductCategory")]
        public enum BiologicallyDerivedProductCategory
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/product-category)
            /// </summary>
            [EnumLiteral("organ", "http://hl7.org/fhir/product-category"), Description("Organ")]
            Organ,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/product-category)
            /// </summary>
            [EnumLiteral("tissue", "http://hl7.org/fhir/product-category"), Description("Tissue")]
            Tissue,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/product-category)
            /// </summary>
            [EnumLiteral("fluid", "http://hl7.org/fhir/product-category"), Description("Fluid")]
            Fluid,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/product-category)
            /// </summary>
            [EnumLiteral("cells", "http://hl7.org/fhir/product-category"), Description("Cells")]
            Cells,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/product-category)
            /// </summary>
            [EnumLiteral("biologicalAgent", "http://hl7.org/fhir/product-category"), Description("BiologicalAgent")]
            BiologicalAgent,
        }

        /// <summary>
        /// Biologically Derived Product Status.
        /// (url: http://hl7.org/fhir/ValueSet/product-status)
        /// </summary>
        [FhirEnumeration("BiologicallyDerivedProductStatus")]
        public enum BiologicallyDerivedProductStatus
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/product-status)
            /// </summary>
            [EnumLiteral("available", "http://hl7.org/fhir/product-status"), Description("Available")]
            Available,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/product-status)
            /// </summary>
            [EnumLiteral("unavailable", "http://hl7.org/fhir/product-status"), Description("Unavailable")]
            Unavailable,
        }

        /// <summary>
        /// BiologicallyDerived Product Storage Scale.
        /// (url: http://hl7.org/fhir/ValueSet/product-storage-scale)
        /// </summary>
        [FhirEnumeration("BiologicallyDerivedProductStorageScale")]
        public enum BiologicallyDerivedProductStorageScale
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/product-storage-scale)
            /// </summary>
            [EnumLiteral("farenheit", "http://hl7.org/fhir/product-storage-scale"), Description("Fahrenheit")]
            Farenheit,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/product-storage-scale)
            /// </summary>
            [EnumLiteral("celsius", "http://hl7.org/fhir/product-storage-scale"), Description("Celsius")]
            Celsius,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/product-storage-scale)
            /// </summary>
            [EnumLiteral("kelvin", "http://hl7.org/fhir/product-storage-scale"), Description("Kelvin")]
            Kelvin,
        }

        [FhirType("CollectionComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class CollectionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "CollectionComponent"; } }
            
            /// <summary>
            /// Individual performing collection
            /// </summary>
            [FhirElement("collector", Order=40)]
            [CLSCompliant(false)]
			[References("Practitioner","PractitionerRole")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Collector
            {
                get { return _Collector; }
                set { _Collector = value; OnPropertyChanged("Collector"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Collector;
            
            /// <summary>
            /// Who is product from
            /// </summary>
            [FhirElement("source", Order=50)]
            [CLSCompliant(false)]
			[References("Patient","Organization")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Source
            {
                get { return _Source; }
                set { _Source = value; OnPropertyChanged("Source"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Source;
            
            /// <summary>
            /// Time of product collection
            /// </summary>
            [FhirElement("collected", Order=60, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
            [DataMember]
            public Hl7.Fhir.Model.Element Collected
            {
                get { return _Collected; }
                set { _Collected = value; OnPropertyChanged("Collected"); }
            }
            
            private Hl7.Fhir.Model.Element _Collected;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CollectionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Collector != null) dest.Collector = (Hl7.Fhir.Model.ResourceReference)Collector.DeepCopy();
                    if(Source != null) dest.Source = (Hl7.Fhir.Model.ResourceReference)Source.DeepCopy();
                    if(Collected != null) dest.Collected = (Hl7.Fhir.Model.Element)Collected.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new CollectionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CollectionComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Collector, otherT.Collector)) return false;
                if( !DeepComparable.Matches(Source, otherT.Source)) return false;
                if( !DeepComparable.Matches(Collected, otherT.Collected)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CollectionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Collector, otherT.Collector)) return false;
                if( !DeepComparable.IsExactly(Source, otherT.Source)) return false;
                if( !DeepComparable.IsExactly(Collected, otherT.Collected)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Collector != null) yield return Collector;
                    if (Source != null) yield return Source;
                    if (Collected != null) yield return Collected;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Collector != null) yield return new ElementValue("collector", Collector);
                    if (Source != null) yield return new ElementValue("source", Source);
                    if (Collected != null) yield return new ElementValue("collected", Collected);
                }
            }

            
        }
        
        
        [FhirType("ProcessingComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ProcessingComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ProcessingComponent"; } }
            
            /// <summary>
            /// Description of of processing
            /// </summary>
            [FhirElement("description", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Description of of processing
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
            /// Procesing code
            /// </summary>
            [FhirElement("procedure", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Procedure
            {
                get { return _Procedure; }
                set { _Procedure = value; OnPropertyChanged("Procedure"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Procedure;
            
            /// <summary>
            /// Substance added during processing
            /// </summary>
            [FhirElement("additive", Order=60)]
            [CLSCompliant(false)]
			[References("Substance")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Additive
            {
                get { return _Additive; }
                set { _Additive = value; OnPropertyChanged("Additive"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Additive;
            
            /// <summary>
            /// Time of processing
            /// </summary>
            [FhirElement("time", Order=70, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
            [DataMember]
            public Hl7.Fhir.Model.Element Time
            {
                get { return _Time; }
                set { _Time = value; OnPropertyChanged("Time"); }
            }
            
            private Hl7.Fhir.Model.Element _Time;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ProcessingComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Procedure != null) dest.Procedure = (Hl7.Fhir.Model.CodeableConcept)Procedure.DeepCopy();
                    if(Additive != null) dest.Additive = (Hl7.Fhir.Model.ResourceReference)Additive.DeepCopy();
                    if(Time != null) dest.Time = (Hl7.Fhir.Model.Element)Time.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ProcessingComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ProcessingComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(Procedure, otherT.Procedure)) return false;
                if( !DeepComparable.Matches(Additive, otherT.Additive)) return false;
                if( !DeepComparable.Matches(Time, otherT.Time)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ProcessingComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Procedure, otherT.Procedure)) return false;
                if( !DeepComparable.IsExactly(Additive, otherT.Additive)) return false;
                if( !DeepComparable.IsExactly(Time, otherT.Time)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (Procedure != null) yield return Procedure;
                    if (Additive != null) yield return Additive;
                    if (Time != null) yield return Time;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (Procedure != null) yield return new ElementValue("procedure", Procedure);
                    if (Additive != null) yield return new ElementValue("additive", Additive);
                    if (Time != null) yield return new ElementValue("time", Time);
                }
            }

            
        }
        
        
        [FhirType("ManipulationComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ManipulationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ManipulationComponent"; } }
            
            /// <summary>
            /// Description of manipulation
            /// </summary>
            [FhirElement("description", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Description of manipulation
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
            /// Time of manipulation
            /// </summary>
            [FhirElement("time", Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
            [DataMember]
            public Hl7.Fhir.Model.Element Time
            {
                get { return _Time; }
                set { _Time = value; OnPropertyChanged("Time"); }
            }
            
            private Hl7.Fhir.Model.Element _Time;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ManipulationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Time != null) dest.Time = (Hl7.Fhir.Model.Element)Time.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ManipulationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ManipulationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(Time, otherT.Time)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ManipulationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Time, otherT.Time)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (Time != null) yield return Time;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (Time != null) yield return new ElementValue("time", Time);
                }
            }

            
        }
        
        
        [FhirType("StorageComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class StorageComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "StorageComponent"; } }
            
            /// <summary>
            /// Description of storage
            /// </summary>
            [FhirElement("description", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Description of storage
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
            /// Storage temperature
            /// </summary>
            [FhirElement("temperature", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal TemperatureElement
            {
                get { return _TemperatureElement; }
                set { _TemperatureElement = value; OnPropertyChanged("TemperatureElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _TemperatureElement;
            
            /// <summary>
            /// Storage temperature
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Temperature
            {
                get { return TemperatureElement != null ? TemperatureElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        TemperatureElement = null; 
                    else
                        TemperatureElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Temperature");
                }
            }
            
            /// <summary>
            /// farenheit | celsius | kelvin
            /// </summary>
            [FhirElement("scale", Order=60)]
            [DataMember]
            public Code<Hl7.Fhir.Model.BiologicallyDerivedProduct.BiologicallyDerivedProductStorageScale> ScaleElement
            {
                get { return _ScaleElement; }
                set { _ScaleElement = value; OnPropertyChanged("ScaleElement"); }
            }
            
            private Code<Hl7.Fhir.Model.BiologicallyDerivedProduct.BiologicallyDerivedProductStorageScale> _ScaleElement;
            
            /// <summary>
            /// farenheit | celsius | kelvin
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.BiologicallyDerivedProduct.BiologicallyDerivedProductStorageScale? Scale
            {
                get { return ScaleElement != null ? ScaleElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        ScaleElement = null; 
                    else
                        ScaleElement = new Code<Hl7.Fhir.Model.BiologicallyDerivedProduct.BiologicallyDerivedProductStorageScale>(value);
                    OnPropertyChanged("Scale");
                }
            }
            
            /// <summary>
            /// Storage timeperiod
            /// </summary>
            [FhirElement("duration", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Period Duration
            {
                get { return _Duration; }
                set { _Duration = value; OnPropertyChanged("Duration"); }
            }
            
            private Hl7.Fhir.Model.Period _Duration;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as StorageComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(TemperatureElement != null) dest.TemperatureElement = (Hl7.Fhir.Model.FhirDecimal)TemperatureElement.DeepCopy();
                    if(ScaleElement != null) dest.ScaleElement = (Code<Hl7.Fhir.Model.BiologicallyDerivedProduct.BiologicallyDerivedProductStorageScale>)ScaleElement.DeepCopy();
                    if(Duration != null) dest.Duration = (Hl7.Fhir.Model.Period)Duration.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new StorageComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as StorageComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(TemperatureElement, otherT.TemperatureElement)) return false;
                if( !DeepComparable.Matches(ScaleElement, otherT.ScaleElement)) return false;
                if( !DeepComparable.Matches(Duration, otherT.Duration)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as StorageComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(TemperatureElement, otherT.TemperatureElement)) return false;
                if( !DeepComparable.IsExactly(ScaleElement, otherT.ScaleElement)) return false;
                if( !DeepComparable.IsExactly(Duration, otherT.Duration)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (TemperatureElement != null) yield return TemperatureElement;
                    if (ScaleElement != null) yield return ScaleElement;
                    if (Duration != null) yield return Duration;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (TemperatureElement != null) yield return new ElementValue("temperature", TemperatureElement);
                    if (ScaleElement != null) yield return new ElementValue("scale", ScaleElement);
                    if (Duration != null) yield return new ElementValue("duration", Duration);
                }
            }

            
        }
        
        
        /// <summary>
        /// External ids for this item
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
        /// organ | tissue | fluid | cells | biologicalAgent
        /// </summary>
        [FhirElement("productCategory", Order=100)]
        [DataMember]
        public Code<Hl7.Fhir.Model.BiologicallyDerivedProduct.BiologicallyDerivedProductCategory> ProductCategoryElement
        {
            get { return _ProductCategoryElement; }
            set { _ProductCategoryElement = value; OnPropertyChanged("ProductCategoryElement"); }
        }
        
        private Code<Hl7.Fhir.Model.BiologicallyDerivedProduct.BiologicallyDerivedProductCategory> _ProductCategoryElement;
        
        /// <summary>
        /// organ | tissue | fluid | cells | biologicalAgent
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.BiologicallyDerivedProduct.BiologicallyDerivedProductCategory? ProductCategory
        {
            get { return ProductCategoryElement != null ? ProductCategoryElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  ProductCategoryElement = null; 
                else
                  ProductCategoryElement = new Code<Hl7.Fhir.Model.BiologicallyDerivedProduct.BiologicallyDerivedProductCategory>(value);
                OnPropertyChanged("ProductCategory");
            }
        }
        
        /// <summary>
        /// What this biologically derived product is
        /// </summary>
        [FhirElement("productCode", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept ProductCode
        {
            get { return _ProductCode; }
            set { _ProductCode = value; OnPropertyChanged("ProductCode"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _ProductCode;
        
        /// <summary>
        /// available | unavailable
        /// </summary>
        [FhirElement("status", Order=120)]
        [DataMember]
        public Code<Hl7.Fhir.Model.BiologicallyDerivedProduct.BiologicallyDerivedProductStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.BiologicallyDerivedProduct.BiologicallyDerivedProductStatus> _StatusElement;
        
        /// <summary>
        /// available | unavailable
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.BiologicallyDerivedProduct.BiologicallyDerivedProductStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.BiologicallyDerivedProduct.BiologicallyDerivedProductStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Procedure request
        /// </summary>
        [FhirElement("request", Order=130)]
        [CLSCompliant(false)]
		[References("ServiceRequest")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Request
        {
            get { if(_Request==null) _Request = new List<Hl7.Fhir.Model.ResourceReference>(); return _Request; }
            set { _Request = value; OnPropertyChanged("Request"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Request;
        
        /// <summary>
        /// The amount of this biologically derived product
        /// </summary>
        [FhirElement("quantity", Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.Integer QuantityElement
        {
            get { return _QuantityElement; }
            set { _QuantityElement = value; OnPropertyChanged("QuantityElement"); }
        }
        
        private Hl7.Fhir.Model.Integer _QuantityElement;
        
        /// <summary>
        /// The amount of this biologically derived product
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? Quantity
        {
            get { return QuantityElement != null ? QuantityElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  QuantityElement = null; 
                else
                  QuantityElement = new Hl7.Fhir.Model.Integer(value);
                OnPropertyChanged("Quantity");
            }
        }
        
        /// <summary>
        /// BiologicallyDerivedProduct parent
        /// </summary>
        [FhirElement("parent", Order=150)]
        [CLSCompliant(false)]
		[References("BiologicallyDerivedProduct")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Parent
        {
            get { if(_Parent==null) _Parent = new List<Hl7.Fhir.Model.ResourceReference>(); return _Parent; }
            set { _Parent = value; OnPropertyChanged("Parent"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Parent;
        
        /// <summary>
        /// How this product was collected
        /// </summary>
        [FhirElement("collection", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.BiologicallyDerivedProduct.CollectionComponent Collection
        {
            get { return _Collection; }
            set { _Collection = value; OnPropertyChanged("Collection"); }
        }
        
        private Hl7.Fhir.Model.BiologicallyDerivedProduct.CollectionComponent _Collection;
        
        /// <summary>
        /// Any processing of the product during collection
        /// </summary>
        [FhirElement("processing", Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.BiologicallyDerivedProduct.ProcessingComponent> Processing
        {
            get { if(_Processing==null) _Processing = new List<Hl7.Fhir.Model.BiologicallyDerivedProduct.ProcessingComponent>(); return _Processing; }
            set { _Processing = value; OnPropertyChanged("Processing"); }
        }
        
        private List<Hl7.Fhir.Model.BiologicallyDerivedProduct.ProcessingComponent> _Processing;
        
        /// <summary>
        /// Any manipulation of product post-collection
        /// </summary>
        [FhirElement("manipulation", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.BiologicallyDerivedProduct.ManipulationComponent Manipulation
        {
            get { return _Manipulation; }
            set { _Manipulation = value; OnPropertyChanged("Manipulation"); }
        }
        
        private Hl7.Fhir.Model.BiologicallyDerivedProduct.ManipulationComponent _Manipulation;
        
        /// <summary>
        /// Product storage
        /// </summary>
        [FhirElement("storage", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.BiologicallyDerivedProduct.StorageComponent> Storage
        {
            get { if(_Storage==null) _Storage = new List<Hl7.Fhir.Model.BiologicallyDerivedProduct.StorageComponent>(); return _Storage; }
            set { _Storage = value; OnPropertyChanged("Storage"); }
        }
        
        private List<Hl7.Fhir.Model.BiologicallyDerivedProduct.StorageComponent> _Storage;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as BiologicallyDerivedProduct;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(ProductCategoryElement != null) dest.ProductCategoryElement = (Code<Hl7.Fhir.Model.BiologicallyDerivedProduct.BiologicallyDerivedProductCategory>)ProductCategoryElement.DeepCopy();
                if(ProductCode != null) dest.ProductCode = (Hl7.Fhir.Model.CodeableConcept)ProductCode.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.BiologicallyDerivedProduct.BiologicallyDerivedProductStatus>)StatusElement.DeepCopy();
                if(Request != null) dest.Request = new List<Hl7.Fhir.Model.ResourceReference>(Request.DeepCopy());
                if(QuantityElement != null) dest.QuantityElement = (Hl7.Fhir.Model.Integer)QuantityElement.DeepCopy();
                if(Parent != null) dest.Parent = new List<Hl7.Fhir.Model.ResourceReference>(Parent.DeepCopy());
                if(Collection != null) dest.Collection = (Hl7.Fhir.Model.BiologicallyDerivedProduct.CollectionComponent)Collection.DeepCopy();
                if(Processing != null) dest.Processing = new List<Hl7.Fhir.Model.BiologicallyDerivedProduct.ProcessingComponent>(Processing.DeepCopy());
                if(Manipulation != null) dest.Manipulation = (Hl7.Fhir.Model.BiologicallyDerivedProduct.ManipulationComponent)Manipulation.DeepCopy();
                if(Storage != null) dest.Storage = new List<Hl7.Fhir.Model.BiologicallyDerivedProduct.StorageComponent>(Storage.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new BiologicallyDerivedProduct());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as BiologicallyDerivedProduct;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(ProductCategoryElement, otherT.ProductCategoryElement)) return false;
            if( !DeepComparable.Matches(ProductCode, otherT.ProductCode)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Request, otherT.Request)) return false;
            if( !DeepComparable.Matches(QuantityElement, otherT.QuantityElement)) return false;
            if( !DeepComparable.Matches(Parent, otherT.Parent)) return false;
            if( !DeepComparable.Matches(Collection, otherT.Collection)) return false;
            if( !DeepComparable.Matches(Processing, otherT.Processing)) return false;
            if( !DeepComparable.Matches(Manipulation, otherT.Manipulation)) return false;
            if( !DeepComparable.Matches(Storage, otherT.Storage)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as BiologicallyDerivedProduct;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(ProductCategoryElement, otherT.ProductCategoryElement)) return false;
            if( !DeepComparable.IsExactly(ProductCode, otherT.ProductCode)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Request, otherT.Request)) return false;
            if( !DeepComparable.IsExactly(QuantityElement, otherT.QuantityElement)) return false;
            if( !DeepComparable.IsExactly(Parent, otherT.Parent)) return false;
            if( !DeepComparable.IsExactly(Collection, otherT.Collection)) return false;
            if( !DeepComparable.IsExactly(Processing, otherT.Processing)) return false;
            if( !DeepComparable.IsExactly(Manipulation, otherT.Manipulation)) return false;
            if( !DeepComparable.IsExactly(Storage, otherT.Storage)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				if (ProductCategoryElement != null) yield return ProductCategoryElement;
				if (ProductCode != null) yield return ProductCode;
				if (StatusElement != null) yield return StatusElement;
				foreach (var elem in Request) { if (elem != null) yield return elem; }
				if (QuantityElement != null) yield return QuantityElement;
				foreach (var elem in Parent) { if (elem != null) yield return elem; }
				if (Collection != null) yield return Collection;
				foreach (var elem in Processing) { if (elem != null) yield return elem; }
				if (Manipulation != null) yield return Manipulation;
				foreach (var elem in Storage) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (ProductCategoryElement != null) yield return new ElementValue("productCategory", ProductCategoryElement);
                if (ProductCode != null) yield return new ElementValue("productCode", ProductCode);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                foreach (var elem in Request) { if (elem != null) yield return new ElementValue("request", elem); }
                if (QuantityElement != null) yield return new ElementValue("quantity", QuantityElement);
                foreach (var elem in Parent) { if (elem != null) yield return new ElementValue("parent", elem); }
                if (Collection != null) yield return new ElementValue("collection", Collection);
                foreach (var elem in Processing) { if (elem != null) yield return new ElementValue("processing", elem); }
                if (Manipulation != null) yield return new ElementValue("manipulation", Manipulation);
                foreach (var elem in Storage) { if (elem != null) yield return new ElementValue("storage", elem); }
            }
        }

    }
    
}
