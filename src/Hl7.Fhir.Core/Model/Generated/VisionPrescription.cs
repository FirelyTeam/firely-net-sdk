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
    /// Prescription for vision correction products for a patient
    /// </summary>
    [FhirType("VisionPrescription", IsResource=true)]
    [DataContract]
    public partial class VisionPrescription : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.VisionPrescription; } }
        [NotMapped]
        public override string TypeName { get { return "VisionPrescription"; } }
        
        /// <summary>
        /// A coded concept listing the eye codes.
        /// (url: http://hl7.org/fhir/ValueSet/vision-eye-codes)
        /// </summary>
        [FhirEnumeration("VisionEyes")]
        public enum VisionEyes
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/vision-eye-codes)
            /// </summary>
            [EnumLiteral("right", "http://hl7.org/fhir/vision-eye-codes"), Description("Right Eye")]
            Right,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/vision-eye-codes)
            /// </summary>
            [EnumLiteral("left", "http://hl7.org/fhir/vision-eye-codes"), Description("Left Eye")]
            Left,
        }

        /// <summary>
        /// A coded concept listing the base codes.
        /// (url: http://hl7.org/fhir/ValueSet/vision-base-codes)
        /// </summary>
        [FhirEnumeration("VisionBase")]
        public enum VisionBase
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/vision-base-codes)
            /// </summary>
            [EnumLiteral("up", "http://hl7.org/fhir/vision-base-codes"), Description("Up")]
            Up,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/vision-base-codes)
            /// </summary>
            [EnumLiteral("down", "http://hl7.org/fhir/vision-base-codes"), Description("Down")]
            Down,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/vision-base-codes)
            /// </summary>
            [EnumLiteral("in", "http://hl7.org/fhir/vision-base-codes"), Description("In")]
            In,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/vision-base-codes)
            /// </summary>
            [EnumLiteral("out", "http://hl7.org/fhir/vision-base-codes"), Description("Out")]
            Out,
        }

        [FhirType("LensSpecificationComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class LensSpecificationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "LensSpecificationComponent"; } }
            
            /// <summary>
            /// Product to be supplied
            /// </summary>
            [FhirElement("product", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Product
            {
                get { return _Product; }
                set { _Product = value; OnPropertyChanged("Product"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Product;
            
            /// <summary>
            /// right | left
            /// </summary>
            [FhirElement("eye", InSummary=true, Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.VisionPrescription.VisionEyes> EyeElement
            {
                get { return _EyeElement; }
                set { _EyeElement = value; OnPropertyChanged("EyeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.VisionPrescription.VisionEyes> _EyeElement;
            
            /// <summary>
            /// right | left
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.VisionPrescription.VisionEyes? Eye
            {
                get { return EyeElement != null ? EyeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        EyeElement = null; 
                    else
                        EyeElement = new Code<Hl7.Fhir.Model.VisionPrescription.VisionEyes>(value);
                    OnPropertyChanged("Eye");
                }
            }
            
            /// <summary>
            /// Power of the lens
            /// </summary>
            [FhirElement("sphere", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal SphereElement
            {
                get { return _SphereElement; }
                set { _SphereElement = value; OnPropertyChanged("SphereElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _SphereElement;
            
            /// <summary>
            /// Power of the lens
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Sphere
            {
                get { return SphereElement != null ? SphereElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        SphereElement = null; 
                    else
                        SphereElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Sphere");
                }
            }
            
            /// <summary>
            /// Lens power for astigmatism
            /// </summary>
            [FhirElement("cylinder", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal CylinderElement
            {
                get { return _CylinderElement; }
                set { _CylinderElement = value; OnPropertyChanged("CylinderElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _CylinderElement;
            
            /// <summary>
            /// Lens power for astigmatism
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Cylinder
            {
                get { return CylinderElement != null ? CylinderElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        CylinderElement = null; 
                    else
                        CylinderElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Cylinder");
                }
            }
            
            /// <summary>
            /// Lens meridian which contain no power for astigmatism
            /// </summary>
            [FhirElement("axis", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Integer AxisElement
            {
                get { return _AxisElement; }
                set { _AxisElement = value; OnPropertyChanged("AxisElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _AxisElement;
            
            /// <summary>
            /// Lens meridian which contain no power for astigmatism
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Axis
            {
                get { return AxisElement != null ? AxisElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        AxisElement = null; 
                    else
                        AxisElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("Axis");
                }
            }
            
            /// <summary>
            /// Eye alignment compensation
            /// </summary>
            [FhirElement("prism", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.VisionPrescription.PrismComponent> Prism
            {
                get { if(_Prism==null) _Prism = new List<Hl7.Fhir.Model.VisionPrescription.PrismComponent>(); return _Prism; }
                set { _Prism = value; OnPropertyChanged("Prism"); }
            }
            
            private List<Hl7.Fhir.Model.VisionPrescription.PrismComponent> _Prism;
            
            /// <summary>
            /// Added power for multifocal levels
            /// </summary>
            [FhirElement("add", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal AddElement
            {
                get { return _AddElement; }
                set { _AddElement = value; OnPropertyChanged("AddElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _AddElement;
            
            /// <summary>
            /// Added power for multifocal levels
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Add
            {
                get { return AddElement != null ? AddElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        AddElement = null; 
                    else
                        AddElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Add");
                }
            }
            
            /// <summary>
            /// Contact lens power
            /// </summary>
            [FhirElement("power", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal PowerElement
            {
                get { return _PowerElement; }
                set { _PowerElement = value; OnPropertyChanged("PowerElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _PowerElement;
            
            /// <summary>
            /// Contact lens power
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Power
            {
                get { return PowerElement != null ? PowerElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        PowerElement = null; 
                    else
                        PowerElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Power");
                }
            }
            
            /// <summary>
            /// Contact lens back curvature
            /// </summary>
            [FhirElement("backCurve", Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal BackCurveElement
            {
                get { return _BackCurveElement; }
                set { _BackCurveElement = value; OnPropertyChanged("BackCurveElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _BackCurveElement;
            
            /// <summary>
            /// Contact lens back curvature
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? BackCurve
            {
                get { return BackCurveElement != null ? BackCurveElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        BackCurveElement = null; 
                    else
                        BackCurveElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("BackCurve");
                }
            }
            
            /// <summary>
            /// Contact lens diameter
            /// </summary>
            [FhirElement("diameter", Order=130)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal DiameterElement
            {
                get { return _DiameterElement; }
                set { _DiameterElement = value; OnPropertyChanged("DiameterElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _DiameterElement;
            
            /// <summary>
            /// Contact lens diameter
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Diameter
            {
                get { return DiameterElement != null ? DiameterElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        DiameterElement = null; 
                    else
                        DiameterElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Diameter");
                }
            }
            
            /// <summary>
            /// Lens wear duration
            /// </summary>
            [FhirElement("duration", Order=140)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Duration
            {
                get { return _Duration; }
                set { _Duration = value; OnPropertyChanged("Duration"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _Duration;
            
            /// <summary>
            /// Color required
            /// </summary>
            [FhirElement("color", Order=150)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ColorElement
            {
                get { return _ColorElement; }
                set { _ColorElement = value; OnPropertyChanged("ColorElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ColorElement;
            
            /// <summary>
            /// Color required
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Color
            {
                get { return ColorElement != null ? ColorElement.Value : null; }
                set
                {
                    if (value == null)
                        ColorElement = null; 
                    else
                        ColorElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Color");
                }
            }
            
            /// <summary>
            /// Brand required
            /// </summary>
            [FhirElement("brand", Order=160)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString BrandElement
            {
                get { return _BrandElement; }
                set { _BrandElement = value; OnPropertyChanged("BrandElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _BrandElement;
            
            /// <summary>
            /// Brand required
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Brand
            {
                get { return BrandElement != null ? BrandElement.Value : null; }
                set
                {
                    if (value == null)
                        BrandElement = null; 
                    else
                        BrandElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Brand");
                }
            }
            
            /// <summary>
            /// Notes for coatings
            /// </summary>
            [FhirElement("note", Order=170)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Annotation> Note
            {
                get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
                set { _Note = value; OnPropertyChanged("Note"); }
            }
            
            private List<Hl7.Fhir.Model.Annotation> _Note;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as LensSpecificationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Product != null) dest.Product = (Hl7.Fhir.Model.CodeableConcept)Product.DeepCopy();
                    if(EyeElement != null) dest.EyeElement = (Code<Hl7.Fhir.Model.VisionPrescription.VisionEyes>)EyeElement.DeepCopy();
                    if(SphereElement != null) dest.SphereElement = (Hl7.Fhir.Model.FhirDecimal)SphereElement.DeepCopy();
                    if(CylinderElement != null) dest.CylinderElement = (Hl7.Fhir.Model.FhirDecimal)CylinderElement.DeepCopy();
                    if(AxisElement != null) dest.AxisElement = (Hl7.Fhir.Model.Integer)AxisElement.DeepCopy();
                    if(Prism != null) dest.Prism = new List<Hl7.Fhir.Model.VisionPrescription.PrismComponent>(Prism.DeepCopy());
                    if(AddElement != null) dest.AddElement = (Hl7.Fhir.Model.FhirDecimal)AddElement.DeepCopy();
                    if(PowerElement != null) dest.PowerElement = (Hl7.Fhir.Model.FhirDecimal)PowerElement.DeepCopy();
                    if(BackCurveElement != null) dest.BackCurveElement = (Hl7.Fhir.Model.FhirDecimal)BackCurveElement.DeepCopy();
                    if(DiameterElement != null) dest.DiameterElement = (Hl7.Fhir.Model.FhirDecimal)DiameterElement.DeepCopy();
                    if(Duration != null) dest.Duration = (Hl7.Fhir.Model.SimpleQuantity)Duration.DeepCopy();
                    if(ColorElement != null) dest.ColorElement = (Hl7.Fhir.Model.FhirString)ColorElement.DeepCopy();
                    if(BrandElement != null) dest.BrandElement = (Hl7.Fhir.Model.FhirString)BrandElement.DeepCopy();
                    if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new LensSpecificationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as LensSpecificationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Product, otherT.Product)) return false;
                if( !DeepComparable.Matches(EyeElement, otherT.EyeElement)) return false;
                if( !DeepComparable.Matches(SphereElement, otherT.SphereElement)) return false;
                if( !DeepComparable.Matches(CylinderElement, otherT.CylinderElement)) return false;
                if( !DeepComparable.Matches(AxisElement, otherT.AxisElement)) return false;
                if( !DeepComparable.Matches(Prism, otherT.Prism)) return false;
                if( !DeepComparable.Matches(AddElement, otherT.AddElement)) return false;
                if( !DeepComparable.Matches(PowerElement, otherT.PowerElement)) return false;
                if( !DeepComparable.Matches(BackCurveElement, otherT.BackCurveElement)) return false;
                if( !DeepComparable.Matches(DiameterElement, otherT.DiameterElement)) return false;
                if( !DeepComparable.Matches(Duration, otherT.Duration)) return false;
                if( !DeepComparable.Matches(ColorElement, otherT.ColorElement)) return false;
                if( !DeepComparable.Matches(BrandElement, otherT.BrandElement)) return false;
                if( !DeepComparable.Matches(Note, otherT.Note)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as LensSpecificationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Product, otherT.Product)) return false;
                if( !DeepComparable.IsExactly(EyeElement, otherT.EyeElement)) return false;
                if( !DeepComparable.IsExactly(SphereElement, otherT.SphereElement)) return false;
                if( !DeepComparable.IsExactly(CylinderElement, otherT.CylinderElement)) return false;
                if( !DeepComparable.IsExactly(AxisElement, otherT.AxisElement)) return false;
                if( !DeepComparable.IsExactly(Prism, otherT.Prism)) return false;
                if( !DeepComparable.IsExactly(AddElement, otherT.AddElement)) return false;
                if( !DeepComparable.IsExactly(PowerElement, otherT.PowerElement)) return false;
                if( !DeepComparable.IsExactly(BackCurveElement, otherT.BackCurveElement)) return false;
                if( !DeepComparable.IsExactly(DiameterElement, otherT.DiameterElement)) return false;
                if( !DeepComparable.IsExactly(Duration, otherT.Duration)) return false;
                if( !DeepComparable.IsExactly(ColorElement, otherT.ColorElement)) return false;
                if( !DeepComparable.IsExactly(BrandElement, otherT.BrandElement)) return false;
                if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Product != null) yield return Product;
                    if (EyeElement != null) yield return EyeElement;
                    if (SphereElement != null) yield return SphereElement;
                    if (CylinderElement != null) yield return CylinderElement;
                    if (AxisElement != null) yield return AxisElement;
                    foreach (var elem in Prism) { if (elem != null) yield return elem; }
                    if (AddElement != null) yield return AddElement;
                    if (PowerElement != null) yield return PowerElement;
                    if (BackCurveElement != null) yield return BackCurveElement;
                    if (DiameterElement != null) yield return DiameterElement;
                    if (Duration != null) yield return Duration;
                    if (ColorElement != null) yield return ColorElement;
                    if (BrandElement != null) yield return BrandElement;
                    foreach (var elem in Note) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Product != null) yield return new ElementValue("product", Product);
                    if (EyeElement != null) yield return new ElementValue("eye", EyeElement);
                    if (SphereElement != null) yield return new ElementValue("sphere", SphereElement);
                    if (CylinderElement != null) yield return new ElementValue("cylinder", CylinderElement);
                    if (AxisElement != null) yield return new ElementValue("axis", AxisElement);
                    foreach (var elem in Prism) { if (elem != null) yield return new ElementValue("prism", elem); }
                    if (AddElement != null) yield return new ElementValue("add", AddElement);
                    if (PowerElement != null) yield return new ElementValue("power", PowerElement);
                    if (BackCurveElement != null) yield return new ElementValue("backCurve", BackCurveElement);
                    if (DiameterElement != null) yield return new ElementValue("diameter", DiameterElement);
                    if (Duration != null) yield return new ElementValue("duration", Duration);
                    if (ColorElement != null) yield return new ElementValue("color", ColorElement);
                    if (BrandElement != null) yield return new ElementValue("brand", BrandElement);
                    foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                }
            }

            
        }
        
        
        [FhirType("PrismComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class PrismComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "PrismComponent"; } }
            
            /// <summary>
            /// Amount of adjustment
            /// </summary>
            [FhirElement("amount", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal AmountElement
            {
                get { return _AmountElement; }
                set { _AmountElement = value; OnPropertyChanged("AmountElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _AmountElement;
            
            /// <summary>
            /// Amount of adjustment
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Amount
            {
                get { return AmountElement != null ? AmountElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        AmountElement = null; 
                    else
                        AmountElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Amount");
                }
            }
            
            /// <summary>
            /// up | down | in | out
            /// </summary>
            [FhirElement("base", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.VisionPrescription.VisionBase> BaseElement
            {
                get { return _BaseElement; }
                set { _BaseElement = value; OnPropertyChanged("BaseElement"); }
            }
            
            private Code<Hl7.Fhir.Model.VisionPrescription.VisionBase> _BaseElement;
            
            /// <summary>
            /// up | down | in | out
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.VisionPrescription.VisionBase? Base
            {
                get { return BaseElement != null ? BaseElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        BaseElement = null; 
                    else
                        BaseElement = new Code<Hl7.Fhir.Model.VisionPrescription.VisionBase>(value);
                    OnPropertyChanged("Base");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PrismComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(AmountElement != null) dest.AmountElement = (Hl7.Fhir.Model.FhirDecimal)AmountElement.DeepCopy();
                    if(BaseElement != null) dest.BaseElement = (Code<Hl7.Fhir.Model.VisionPrescription.VisionBase>)BaseElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new PrismComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PrismComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(AmountElement, otherT.AmountElement)) return false;
                if( !DeepComparable.Matches(BaseElement, otherT.BaseElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PrismComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(AmountElement, otherT.AmountElement)) return false;
                if( !DeepComparable.IsExactly(BaseElement, otherT.BaseElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (AmountElement != null) yield return AmountElement;
                    if (BaseElement != null) yield return BaseElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (AmountElement != null) yield return new ElementValue("amount", AmountElement);
                    if (BaseElement != null) yield return new ElementValue("base", BaseElement);
                }
            }

            
        }
        
        
        /// <summary>
        /// Business Identifier for vision prescription
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
        /// active | cancelled | draft | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary=true, Order=100)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.FinancialResourceStatusCodes> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.FinancialResourceStatusCodes> _StatusElement;
        
        /// <summary>
        /// active | cancelled | draft | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.FinancialResourceStatusCodes? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.FinancialResourceStatusCodes>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Response creation date
        /// </summary>
        [FhirElement("created", InSummary=true, Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime CreatedElement
        {
            get { return _CreatedElement; }
            set { _CreatedElement = value; OnPropertyChanged("CreatedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _CreatedElement;
        
        /// <summary>
        /// Response creation date
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Created
        {
            get { return CreatedElement != null ? CreatedElement.Value : null; }
            set
            {
                if (value == null)
                  CreatedElement = null; 
                else
                  CreatedElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("Created");
            }
        }
        
        /// <summary>
        /// Who prescription is for
        /// </summary>
        [FhirElement("patient", InSummary=true, Order=120)]
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
        /// Created during encounter / admission / stay
        /// </summary>
        [FhirElement("encounter", Order=130)]
        [CLSCompliant(false)]
		[References("Encounter")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Encounter
        {
            get { return _Encounter; }
            set { _Encounter = value; OnPropertyChanged("Encounter"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Encounter;
        
        /// <summary>
        /// When prescription was authorized
        /// </summary>
        [FhirElement("dateWritten", InSummary=true, Order=140)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateWrittenElement
        {
            get { return _DateWrittenElement; }
            set { _DateWrittenElement = value; OnPropertyChanged("DateWrittenElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateWrittenElement;
        
        /// <summary>
        /// When prescription was authorized
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string DateWritten
        {
            get { return DateWrittenElement != null ? DateWrittenElement.Value : null; }
            set
            {
                if (value == null)
                  DateWrittenElement = null; 
                else
                  DateWrittenElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("DateWritten");
            }
        }
        
        /// <summary>
        /// Who authorized the vision prescription
        /// </summary>
        [FhirElement("prescriber", InSummary=true, Order=150)]
        [CLSCompliant(false)]
		[References("Practitioner","PractitionerRole")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Prescriber
        {
            get { return _Prescriber; }
            set { _Prescriber = value; OnPropertyChanged("Prescriber"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Prescriber;
        
        /// <summary>
        /// Vision lens authorization
        /// </summary>
        [FhirElement("lensSpecification", InSummary=true, Order=160)]
        [Cardinality(Min=1,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.VisionPrescription.LensSpecificationComponent> LensSpecification
        {
            get { if(_LensSpecification==null) _LensSpecification = new List<Hl7.Fhir.Model.VisionPrescription.LensSpecificationComponent>(); return _LensSpecification; }
            set { _LensSpecification = value; OnPropertyChanged("LensSpecification"); }
        }
        
        private List<Hl7.Fhir.Model.VisionPrescription.LensSpecificationComponent> _LensSpecification;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as VisionPrescription;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.FinancialResourceStatusCodes>)StatusElement.DeepCopy();
                if(CreatedElement != null) dest.CreatedElement = (Hl7.Fhir.Model.FhirDateTime)CreatedElement.DeepCopy();
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(Encounter != null) dest.Encounter = (Hl7.Fhir.Model.ResourceReference)Encounter.DeepCopy();
                if(DateWrittenElement != null) dest.DateWrittenElement = (Hl7.Fhir.Model.FhirDateTime)DateWrittenElement.DeepCopy();
                if(Prescriber != null) dest.Prescriber = (Hl7.Fhir.Model.ResourceReference)Prescriber.DeepCopy();
                if(LensSpecification != null) dest.LensSpecification = new List<Hl7.Fhir.Model.VisionPrescription.LensSpecificationComponent>(LensSpecification.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new VisionPrescription());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as VisionPrescription;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.Matches(DateWrittenElement, otherT.DateWrittenElement)) return false;
            if( !DeepComparable.Matches(Prescriber, otherT.Prescriber)) return false;
            if( !DeepComparable.Matches(LensSpecification, otherT.LensSpecification)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as VisionPrescription;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(Encounter, otherT.Encounter)) return false;
            if( !DeepComparable.IsExactly(DateWrittenElement, otherT.DateWrittenElement)) return false;
            if( !DeepComparable.IsExactly(Prescriber, otherT.Prescriber)) return false;
            if( !DeepComparable.IsExactly(LensSpecification, otherT.LensSpecification)) return false;
            
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
				if (CreatedElement != null) yield return CreatedElement;
				if (Patient != null) yield return Patient;
				if (Encounter != null) yield return Encounter;
				if (DateWrittenElement != null) yield return DateWrittenElement;
				if (Prescriber != null) yield return Prescriber;
				foreach (var elem in LensSpecification) { if (elem != null) yield return elem; }
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
                if (CreatedElement != null) yield return new ElementValue("created", CreatedElement);
                if (Patient != null) yield return new ElementValue("patient", Patient);
                if (Encounter != null) yield return new ElementValue("encounter", Encounter);
                if (DateWrittenElement != null) yield return new ElementValue("dateWritten", DateWrittenElement);
                if (Prescriber != null) yield return new ElementValue("prescriber", Prescriber);
                foreach (var elem in LensSpecification) { if (elem != null) yield return new ElementValue("lensSpecification", elem); }
            }
        }

    }
    
}
