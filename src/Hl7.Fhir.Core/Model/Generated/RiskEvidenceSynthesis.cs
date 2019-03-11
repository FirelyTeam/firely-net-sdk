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
    /// A quantified estimate of risk based on a body of evidence
    /// </summary>
    [FhirType("RiskEvidenceSynthesis", IsResource=true)]
    [DataContract]
    public partial class RiskEvidenceSynthesis : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.RiskEvidenceSynthesis; } }
        [NotMapped]
        public override string TypeName { get { return "RiskEvidenceSynthesis"; } }
        
        [FhirType("SampleSizeComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class SampleSizeComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "SampleSizeComponent"; } }
            
            /// <summary>
            /// Description of sample size
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
            /// Description of sample size
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
            /// How many studies?
            /// </summary>
            [FhirElement("numberOfStudies", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Integer NumberOfStudiesElement
            {
                get { return _NumberOfStudiesElement; }
                set { _NumberOfStudiesElement = value; OnPropertyChanged("NumberOfStudiesElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _NumberOfStudiesElement;
            
            /// <summary>
            /// How many studies?
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? NumberOfStudies
            {
                get { return NumberOfStudiesElement != null ? NumberOfStudiesElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        NumberOfStudiesElement = null; 
                    else
                        NumberOfStudiesElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("NumberOfStudies");
                }
            }
            
            /// <summary>
            /// How many participants?
            /// </summary>
            [FhirElement("numberOfParticipants", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Integer NumberOfParticipantsElement
            {
                get { return _NumberOfParticipantsElement; }
                set { _NumberOfParticipantsElement = value; OnPropertyChanged("NumberOfParticipantsElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _NumberOfParticipantsElement;
            
            /// <summary>
            /// How many participants?
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? NumberOfParticipants
            {
                get { return NumberOfParticipantsElement != null ? NumberOfParticipantsElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        NumberOfParticipantsElement = null; 
                    else
                        NumberOfParticipantsElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("NumberOfParticipants");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SampleSizeComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(NumberOfStudiesElement != null) dest.NumberOfStudiesElement = (Hl7.Fhir.Model.Integer)NumberOfStudiesElement.DeepCopy();
                    if(NumberOfParticipantsElement != null) dest.NumberOfParticipantsElement = (Hl7.Fhir.Model.Integer)NumberOfParticipantsElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SampleSizeComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SampleSizeComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(NumberOfStudiesElement, otherT.NumberOfStudiesElement)) return false;
                if( !DeepComparable.Matches(NumberOfParticipantsElement, otherT.NumberOfParticipantsElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SampleSizeComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(NumberOfStudiesElement, otherT.NumberOfStudiesElement)) return false;
                if( !DeepComparable.IsExactly(NumberOfParticipantsElement, otherT.NumberOfParticipantsElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (NumberOfStudiesElement != null) yield return NumberOfStudiesElement;
                    if (NumberOfParticipantsElement != null) yield return NumberOfParticipantsElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (NumberOfStudiesElement != null) yield return new ElementValue("numberOfStudies", NumberOfStudiesElement);
                    if (NumberOfParticipantsElement != null) yield return new ElementValue("numberOfParticipants", NumberOfParticipantsElement);
                }
            }

            
        }
        
        
        [FhirType("RiskEstimateComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class RiskEstimateComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "RiskEstimateComponent"; } }
            
            /// <summary>
            /// Description of risk estimate
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
            /// Description of risk estimate
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
            /// Type of risk estimate
            /// </summary>
            [FhirElement("type", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Point estimate
            /// </summary>
            [FhirElement("value", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _ValueElement;
            
            /// <summary>
            /// Point estimate
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Value
            {
                get { return ValueElement != null ? ValueElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        ValueElement = null; 
                    else
                        ValueElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Value");
                }
            }
            
            /// <summary>
            /// What unit is the outcome described in?
            /// </summary>
            [FhirElement("unitOfMeasure", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept UnitOfMeasure
            {
                get { return _UnitOfMeasure; }
                set { _UnitOfMeasure = value; OnPropertyChanged("UnitOfMeasure"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _UnitOfMeasure;
            
            /// <summary>
            /// Sample size for group measured
            /// </summary>
            [FhirElement("denominatorCount", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Integer DenominatorCountElement
            {
                get { return _DenominatorCountElement; }
                set { _DenominatorCountElement = value; OnPropertyChanged("DenominatorCountElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _DenominatorCountElement;
            
            /// <summary>
            /// Sample size for group measured
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? DenominatorCount
            {
                get { return DenominatorCountElement != null ? DenominatorCountElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        DenominatorCountElement = null; 
                    else
                        DenominatorCountElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("DenominatorCount");
                }
            }
            
            /// <summary>
            /// Number with the outcome
            /// </summary>
            [FhirElement("numeratorCount", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.Integer NumeratorCountElement
            {
                get { return _NumeratorCountElement; }
                set { _NumeratorCountElement = value; OnPropertyChanged("NumeratorCountElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _NumeratorCountElement;
            
            /// <summary>
            /// Number with the outcome
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? NumeratorCount
            {
                get { return NumeratorCountElement != null ? NumeratorCountElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        NumeratorCountElement = null; 
                    else
                        NumeratorCountElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("NumeratorCount");
                }
            }
            
            /// <summary>
            /// How precise the estimate is
            /// </summary>
            [FhirElement("precisionEstimate", Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.RiskEvidenceSynthesis.PrecisionEstimateComponent> PrecisionEstimate
            {
                get { if(_PrecisionEstimate==null) _PrecisionEstimate = new List<Hl7.Fhir.Model.RiskEvidenceSynthesis.PrecisionEstimateComponent>(); return _PrecisionEstimate; }
                set { _PrecisionEstimate = value; OnPropertyChanged("PrecisionEstimate"); }
            }
            
            private List<Hl7.Fhir.Model.RiskEvidenceSynthesis.PrecisionEstimateComponent> _PrecisionEstimate;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RiskEstimateComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirDecimal)ValueElement.DeepCopy();
                    if(UnitOfMeasure != null) dest.UnitOfMeasure = (Hl7.Fhir.Model.CodeableConcept)UnitOfMeasure.DeepCopy();
                    if(DenominatorCountElement != null) dest.DenominatorCountElement = (Hl7.Fhir.Model.Integer)DenominatorCountElement.DeepCopy();
                    if(NumeratorCountElement != null) dest.NumeratorCountElement = (Hl7.Fhir.Model.Integer)NumeratorCountElement.DeepCopy();
                    if(PrecisionEstimate != null) dest.PrecisionEstimate = new List<Hl7.Fhir.Model.RiskEvidenceSynthesis.PrecisionEstimateComponent>(PrecisionEstimate.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new RiskEstimateComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RiskEstimateComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
                if( !DeepComparable.Matches(UnitOfMeasure, otherT.UnitOfMeasure)) return false;
                if( !DeepComparable.Matches(DenominatorCountElement, otherT.DenominatorCountElement)) return false;
                if( !DeepComparable.Matches(NumeratorCountElement, otherT.NumeratorCountElement)) return false;
                if( !DeepComparable.Matches(PrecisionEstimate, otherT.PrecisionEstimate)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RiskEstimateComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
                if( !DeepComparable.IsExactly(UnitOfMeasure, otherT.UnitOfMeasure)) return false;
                if( !DeepComparable.IsExactly(DenominatorCountElement, otherT.DenominatorCountElement)) return false;
                if( !DeepComparable.IsExactly(NumeratorCountElement, otherT.NumeratorCountElement)) return false;
                if( !DeepComparable.IsExactly(PrecisionEstimate, otherT.PrecisionEstimate)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (Type != null) yield return Type;
                    if (ValueElement != null) yield return ValueElement;
                    if (UnitOfMeasure != null) yield return UnitOfMeasure;
                    if (DenominatorCountElement != null) yield return DenominatorCountElement;
                    if (NumeratorCountElement != null) yield return NumeratorCountElement;
                    foreach (var elem in PrecisionEstimate) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                    if (UnitOfMeasure != null) yield return new ElementValue("unitOfMeasure", UnitOfMeasure);
                    if (DenominatorCountElement != null) yield return new ElementValue("denominatorCount", DenominatorCountElement);
                    if (NumeratorCountElement != null) yield return new ElementValue("numeratorCount", NumeratorCountElement);
                    foreach (var elem in PrecisionEstimate) { if (elem != null) yield return new ElementValue("precisionEstimate", elem); }
                }
            }

            
        }
        
        
        [FhirType("PrecisionEstimateComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class PrecisionEstimateComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "PrecisionEstimateComponent"; } }
            
            /// <summary>
            /// Type of precision estimate
            /// </summary>
            [FhirElement("type", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Level of confidence interval
            /// </summary>
            [FhirElement("level", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal LevelElement
            {
                get { return _LevelElement; }
                set { _LevelElement = value; OnPropertyChanged("LevelElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _LevelElement;
            
            /// <summary>
            /// Level of confidence interval
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? Level
            {
                get { return LevelElement != null ? LevelElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        LevelElement = null; 
                    else
                        LevelElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("Level");
                }
            }
            
            /// <summary>
            /// Lower bound
            /// </summary>
            [FhirElement("from", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal FromElement
            {
                get { return _FromElement; }
                set { _FromElement = value; OnPropertyChanged("FromElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _FromElement;
            
            /// <summary>
            /// Lower bound
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? From
            {
                get { return FromElement != null ? FromElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        FromElement = null; 
                    else
                        FromElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("From");
                }
            }
            
            /// <summary>
            /// Upper bound
            /// </summary>
            [FhirElement("to", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal ToElement
            {
                get { return _ToElement; }
                set { _ToElement = value; OnPropertyChanged("ToElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _ToElement;
            
            /// <summary>
            /// Upper bound
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public decimal? To
            {
                get { return ToElement != null ? ToElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        ToElement = null; 
                    else
                        ToElement = new Hl7.Fhir.Model.FhirDecimal(value);
                    OnPropertyChanged("To");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PrecisionEstimateComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(LevelElement != null) dest.LevelElement = (Hl7.Fhir.Model.FhirDecimal)LevelElement.DeepCopy();
                    if(FromElement != null) dest.FromElement = (Hl7.Fhir.Model.FhirDecimal)FromElement.DeepCopy();
                    if(ToElement != null) dest.ToElement = (Hl7.Fhir.Model.FhirDecimal)ToElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new PrecisionEstimateComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PrecisionEstimateComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(LevelElement, otherT.LevelElement)) return false;
                if( !DeepComparable.Matches(FromElement, otherT.FromElement)) return false;
                if( !DeepComparable.Matches(ToElement, otherT.ToElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PrecisionEstimateComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(LevelElement, otherT.LevelElement)) return false;
                if( !DeepComparable.IsExactly(FromElement, otherT.FromElement)) return false;
                if( !DeepComparable.IsExactly(ToElement, otherT.ToElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    if (LevelElement != null) yield return LevelElement;
                    if (FromElement != null) yield return FromElement;
                    if (ToElement != null) yield return ToElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (LevelElement != null) yield return new ElementValue("level", LevelElement);
                    if (FromElement != null) yield return new ElementValue("from", FromElement);
                    if (ToElement != null) yield return new ElementValue("to", ToElement);
                }
            }

            
        }
        
        
        [FhirType("CertaintyComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class CertaintyComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "CertaintyComponent"; } }
            
            /// <summary>
            /// Certainty rating
            /// </summary>
            [FhirElement("rating", Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Rating
            {
                get { if(_Rating==null) _Rating = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Rating; }
                set { _Rating = value; OnPropertyChanged("Rating"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Rating;
            
            /// <summary>
            /// Used for footnotes or explanatory notes
            /// </summary>
            [FhirElement("note", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Annotation> Note
            {
                get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
                set { _Note = value; OnPropertyChanged("Note"); }
            }
            
            private List<Hl7.Fhir.Model.Annotation> _Note;
            
            /// <summary>
            /// A component that contributes to the overall certainty
            /// </summary>
            [FhirElement("certaintySubcomponent", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.RiskEvidenceSynthesis.CertaintySubcomponentComponent> CertaintySubcomponent
            {
                get { if(_CertaintySubcomponent==null) _CertaintySubcomponent = new List<Hl7.Fhir.Model.RiskEvidenceSynthesis.CertaintySubcomponentComponent>(); return _CertaintySubcomponent; }
                set { _CertaintySubcomponent = value; OnPropertyChanged("CertaintySubcomponent"); }
            }
            
            private List<Hl7.Fhir.Model.RiskEvidenceSynthesis.CertaintySubcomponentComponent> _CertaintySubcomponent;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CertaintyComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Rating != null) dest.Rating = new List<Hl7.Fhir.Model.CodeableConcept>(Rating.DeepCopy());
                    if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                    if(CertaintySubcomponent != null) dest.CertaintySubcomponent = new List<Hl7.Fhir.Model.RiskEvidenceSynthesis.CertaintySubcomponentComponent>(CertaintySubcomponent.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new CertaintyComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CertaintyComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Rating, otherT.Rating)) return false;
                if( !DeepComparable.Matches(Note, otherT.Note)) return false;
                if( !DeepComparable.Matches(CertaintySubcomponent, otherT.CertaintySubcomponent)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CertaintyComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Rating, otherT.Rating)) return false;
                if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
                if( !DeepComparable.IsExactly(CertaintySubcomponent, otherT.CertaintySubcomponent)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Rating) { if (elem != null) yield return elem; }
                    foreach (var elem in Note) { if (elem != null) yield return elem; }
                    foreach (var elem in CertaintySubcomponent) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Rating) { if (elem != null) yield return new ElementValue("rating", elem); }
                    foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                    foreach (var elem in CertaintySubcomponent) { if (elem != null) yield return new ElementValue("certaintySubcomponent", elem); }
                }
            }

            
        }
        
        
        [FhirType("CertaintySubcomponentComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class CertaintySubcomponentComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "CertaintySubcomponentComponent"; } }
            
            /// <summary>
            /// Type of subcomponent of certainty rating
            /// </summary>
            [FhirElement("type", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Subcomponent certainty rating
            /// </summary>
            [FhirElement("rating", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Rating
            {
                get { if(_Rating==null) _Rating = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Rating; }
                set { _Rating = value; OnPropertyChanged("Rating"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Rating;
            
            /// <summary>
            /// Used for footnotes or explanatory notes
            /// </summary>
            [FhirElement("note", Order=60)]
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
                var dest = other as CertaintySubcomponentComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Rating != null) dest.Rating = new List<Hl7.Fhir.Model.CodeableConcept>(Rating.DeepCopy());
                    if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new CertaintySubcomponentComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CertaintySubcomponentComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Rating, otherT.Rating)) return false;
                if( !DeepComparable.Matches(Note, otherT.Note)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CertaintySubcomponentComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Rating, otherT.Rating)) return false;
                if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    foreach (var elem in Rating) { if (elem != null) yield return elem; }
                    foreach (var elem in Note) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    foreach (var elem in Rating) { if (elem != null) yield return new ElementValue("rating", elem); }
                    foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                }
            }

            
        }
        
        
        /// <summary>
        /// Canonical identifier for this risk evidence synthesis, represented as a URI (globally unique)
        /// </summary>
        [FhirElement("url", InSummary=true, Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri UrlElement
        {
            get { return _UrlElement; }
            set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _UrlElement;
        
        /// <summary>
        /// Canonical identifier for this risk evidence synthesis, represented as a URI (globally unique)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Url
        {
            get { return UrlElement != null ? UrlElement.Value : null; }
            set
            {
                if (value == null)
                  UrlElement = null; 
                else
                  UrlElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("Url");
            }
        }
        
        /// <summary>
        /// Additional identifier for the risk evidence synthesis
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=100)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Business version of the risk evidence synthesis
        /// </summary>
        [FhirElement("version", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement
        {
            get { return _VersionElement; }
            set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _VersionElement;
        
        /// <summary>
        /// Business version of the risk evidence synthesis
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Version
        {
            get { return VersionElement != null ? VersionElement.Value : null; }
            set
            {
                if (value == null)
                  VersionElement = null; 
                else
                  VersionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Version");
            }
        }
        
        /// <summary>
        /// Name for this risk evidence synthesis (computer friendly)
        /// </summary>
        [FhirElement("name", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// Name for this risk evidence synthesis (computer friendly)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Name
        {
            get { return NameElement != null ? NameElement.Value : null; }
            set
            {
                if (value == null)
                  NameElement = null; 
                else
                  NameElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Name");
            }
        }
        
        /// <summary>
        /// Name for this risk evidence synthesis (human friendly)
        /// </summary>
        [FhirElement("title", InSummary=true, Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString TitleElement
        {
            get { return _TitleElement; }
            set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _TitleElement;
        
        /// <summary>
        /// Name for this risk evidence synthesis (human friendly)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Title
        {
            get { return TitleElement != null ? TitleElement.Value : null; }
            set
            {
                if (value == null)
                  TitleElement = null; 
                else
                  TitleElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Title");
            }
        }
        
        /// <summary>
        /// draft | active | retired | unknown
        /// </summary>
        [FhirElement("status", InSummary=true, Order=140)]
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
        /// Date last changed
        /// </summary>
        [FhirElement("date", InSummary=true, Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// Date last changed
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
        /// Name of the publisher (organization or individual)
        /// </summary>
        [FhirElement("publisher", InSummary=true, Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PublisherElement
        {
            get { return _PublisherElement; }
            set { _PublisherElement = value; OnPropertyChanged("PublisherElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _PublisherElement;
        
        /// <summary>
        /// Name of the publisher (organization or individual)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Publisher
        {
            get { return PublisherElement != null ? PublisherElement.Value : null; }
            set
            {
                if (value == null)
                  PublisherElement = null; 
                else
                  PublisherElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Publisher");
            }
        }
        
        /// <summary>
        /// Contact details for the publisher
        /// </summary>
        [FhirElement("contact", InSummary=true, Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ContactDetail> Contact
        {
            get { if(_Contact==null) _Contact = new List<ContactDetail>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<ContactDetail> _Contact;
        
        /// <summary>
        /// Natural language description of the risk evidence synthesis
        /// </summary>
        [FhirElement("description", InSummary=true, Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown Description
        {
            get { return _Description; }
            set { _Description = value; OnPropertyChanged("Description"); }
        }
        
        private Hl7.Fhir.Model.Markdown _Description;
        
        /// <summary>
        /// Used for footnotes or explanatory notes
        /// </summary>
        [FhirElement("note", Order=190)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Note;
        
        /// <summary>
        /// The context that the content is intended to support
        /// </summary>
        [FhirElement("useContext", InSummary=true, Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<UsageContext> UseContext
        {
            get { if(_UseContext==null) _UseContext = new List<UsageContext>(); return _UseContext; }
            set { _UseContext = value; OnPropertyChanged("UseContext"); }
        }
        
        private List<UsageContext> _UseContext;
        
        /// <summary>
        /// Intended jurisdiction for risk evidence synthesis (if applicable)
        /// </summary>
        [FhirElement("jurisdiction", InSummary=true, Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Jurisdiction
        {
            get { if(_Jurisdiction==null) _Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Jurisdiction; }
            set { _Jurisdiction = value; OnPropertyChanged("Jurisdiction"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Jurisdiction;
        
        /// <summary>
        /// Use and/or publishing restrictions
        /// </summary>
        [FhirElement("copyright", Order=220)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown Copyright
        {
            get { return _Copyright; }
            set { _Copyright = value; OnPropertyChanged("Copyright"); }
        }
        
        private Hl7.Fhir.Model.Markdown _Copyright;
        
        /// <summary>
        /// When the risk evidence synthesis was approved by publisher
        /// </summary>
        [FhirElement("approvalDate", Order=230)]
        [DataMember]
        public Hl7.Fhir.Model.Date ApprovalDateElement
        {
            get { return _ApprovalDateElement; }
            set { _ApprovalDateElement = value; OnPropertyChanged("ApprovalDateElement"); }
        }
        
        private Hl7.Fhir.Model.Date _ApprovalDateElement;
        
        /// <summary>
        /// When the risk evidence synthesis was approved by publisher
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ApprovalDate
        {
            get { return ApprovalDateElement != null ? ApprovalDateElement.Value : null; }
            set
            {
                if (value == null)
                  ApprovalDateElement = null; 
                else
                  ApprovalDateElement = new Hl7.Fhir.Model.Date(value);
                OnPropertyChanged("ApprovalDate");
            }
        }
        
        /// <summary>
        /// When the risk evidence synthesis was last reviewed
        /// </summary>
        [FhirElement("lastReviewDate", Order=240)]
        [DataMember]
        public Hl7.Fhir.Model.Date LastReviewDateElement
        {
            get { return _LastReviewDateElement; }
            set { _LastReviewDateElement = value; OnPropertyChanged("LastReviewDateElement"); }
        }
        
        private Hl7.Fhir.Model.Date _LastReviewDateElement;
        
        /// <summary>
        /// When the risk evidence synthesis was last reviewed
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string LastReviewDate
        {
            get { return LastReviewDateElement != null ? LastReviewDateElement.Value : null; }
            set
            {
                if (value == null)
                  LastReviewDateElement = null; 
                else
                  LastReviewDateElement = new Hl7.Fhir.Model.Date(value);
                OnPropertyChanged("LastReviewDate");
            }
        }
        
        /// <summary>
        /// When the risk evidence synthesis is expected to be used
        /// </summary>
        [FhirElement("effectivePeriod", InSummary=true, Order=250)]
        [DataMember]
        public Hl7.Fhir.Model.Period EffectivePeriod
        {
            get { return _EffectivePeriod; }
            set { _EffectivePeriod = value; OnPropertyChanged("EffectivePeriod"); }
        }
        
        private Hl7.Fhir.Model.Period _EffectivePeriod;
        
        /// <summary>
        /// The category of the EffectEvidenceSynthesis, such as Education, Treatment, Assessment, etc.
        /// </summary>
        [FhirElement("topic", Order=260)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Topic
        {
            get { if(_Topic==null) _Topic = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Topic; }
            set { _Topic = value; OnPropertyChanged("Topic"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Topic;
        
        /// <summary>
        /// Who authored the content
        /// </summary>
        [FhirElement("author", Order=270)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ContactDetail> Author
        {
            get { if(_Author==null) _Author = new List<ContactDetail>(); return _Author; }
            set { _Author = value; OnPropertyChanged("Author"); }
        }
        
        private List<ContactDetail> _Author;
        
        /// <summary>
        /// Who edited the content
        /// </summary>
        [FhirElement("editor", Order=280)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ContactDetail> Editor
        {
            get { if(_Editor==null) _Editor = new List<ContactDetail>(); return _Editor; }
            set { _Editor = value; OnPropertyChanged("Editor"); }
        }
        
        private List<ContactDetail> _Editor;
        
        /// <summary>
        /// Who reviewed the content
        /// </summary>
        [FhirElement("reviewer", Order=290)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ContactDetail> Reviewer
        {
            get { if(_Reviewer==null) _Reviewer = new List<ContactDetail>(); return _Reviewer; }
            set { _Reviewer = value; OnPropertyChanged("Reviewer"); }
        }
        
        private List<ContactDetail> _Reviewer;
        
        /// <summary>
        /// Who endorsed the content
        /// </summary>
        [FhirElement("endorser", Order=300)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ContactDetail> Endorser
        {
            get { if(_Endorser==null) _Endorser = new List<ContactDetail>(); return _Endorser; }
            set { _Endorser = value; OnPropertyChanged("Endorser"); }
        }
        
        private List<ContactDetail> _Endorser;
        
        /// <summary>
        /// Additional documentation, citations, etc.
        /// </summary>
        [FhirElement("relatedArtifact", Order=310)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<RelatedArtifact> RelatedArtifact
        {
            get { if(_RelatedArtifact==null) _RelatedArtifact = new List<RelatedArtifact>(); return _RelatedArtifact; }
            set { _RelatedArtifact = value; OnPropertyChanged("RelatedArtifact"); }
        }
        
        private List<RelatedArtifact> _RelatedArtifact;
        
        /// <summary>
        /// Type of synthesis
        /// </summary>
        [FhirElement("synthesisType", Order=320)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept SynthesisType
        {
            get { return _SynthesisType; }
            set { _SynthesisType = value; OnPropertyChanged("SynthesisType"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _SynthesisType;
        
        /// <summary>
        /// Type of study
        /// </summary>
        [FhirElement("studyType", Order=330)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept StudyType
        {
            get { return _StudyType; }
            set { _StudyType = value; OnPropertyChanged("StudyType"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _StudyType;
        
        /// <summary>
        /// What population?
        /// </summary>
        [FhirElement("population", InSummary=true, Order=340)]
        [CLSCompliant(false)]
		[References("EvidenceVariable")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Population
        {
            get { return _Population; }
            set { _Population = value; OnPropertyChanged("Population"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Population;
        
        /// <summary>
        /// What exposure?
        /// </summary>
        [FhirElement("exposure", InSummary=true, Order=350)]
        [CLSCompliant(false)]
		[References("EvidenceVariable")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Exposure
        {
            get { return _Exposure; }
            set { _Exposure = value; OnPropertyChanged("Exposure"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Exposure;
        
        /// <summary>
        /// What outcome?
        /// </summary>
        [FhirElement("outcome", InSummary=true, Order=360)]
        [CLSCompliant(false)]
		[References("EvidenceVariable")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Outcome
        {
            get { return _Outcome; }
            set { _Outcome = value; OnPropertyChanged("Outcome"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Outcome;
        
        /// <summary>
        /// What sample size was involved?
        /// </summary>
        [FhirElement("sampleSize", Order=370)]
        [DataMember]
        public Hl7.Fhir.Model.RiskEvidenceSynthesis.SampleSizeComponent SampleSize
        {
            get { return _SampleSize; }
            set { _SampleSize = value; OnPropertyChanged("SampleSize"); }
        }
        
        private Hl7.Fhir.Model.RiskEvidenceSynthesis.SampleSizeComponent _SampleSize;
        
        /// <summary>
        /// What was the estimated risk
        /// </summary>
        [FhirElement("riskEstimate", InSummary=true, Order=380)]
        [DataMember]
        public Hl7.Fhir.Model.RiskEvidenceSynthesis.RiskEstimateComponent RiskEstimate
        {
            get { return _RiskEstimate; }
            set { _RiskEstimate = value; OnPropertyChanged("RiskEstimate"); }
        }
        
        private Hl7.Fhir.Model.RiskEvidenceSynthesis.RiskEstimateComponent _RiskEstimate;
        
        /// <summary>
        /// How certain is the risk
        /// </summary>
        [FhirElement("certainty", Order=390)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.RiskEvidenceSynthesis.CertaintyComponent> Certainty
        {
            get { if(_Certainty==null) _Certainty = new List<Hl7.Fhir.Model.RiskEvidenceSynthesis.CertaintyComponent>(); return _Certainty; }
            set { _Certainty = value; OnPropertyChanged("Certainty"); }
        }
        
        private List<Hl7.Fhir.Model.RiskEvidenceSynthesis.CertaintyComponent> _Certainty;
        

        public static ElementDefinition.ConstraintComponent RiskEvidenceSynthesis_RVS_0 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "name.matches('[A-Z]([A-Za-z0-9_]){0,254}')",
            Key = "rvs-0",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Name should be usable as an identifier for the module by machine processing applications such as code generation",
            Xpath = "not(exists(f:name/@value)) or matches(f:name/@value, '[A-Z]([A-Za-z0-9_]){0,254}')"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(RiskEvidenceSynthesis_RVS_0);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as RiskEvidenceSynthesis;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.PublicationStatus>)StatusElement.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(PublisherElement != null) dest.PublisherElement = (Hl7.Fhir.Model.FhirString)PublisherElement.DeepCopy();
                if(Contact != null) dest.Contact = new List<ContactDetail>(Contact.DeepCopy());
                if(Description != null) dest.Description = (Hl7.Fhir.Model.Markdown)Description.DeepCopy();
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                if(UseContext != null) dest.UseContext = new List<UsageContext>(UseContext.DeepCopy());
                if(Jurisdiction != null) dest.Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(Jurisdiction.DeepCopy());
                if(Copyright != null) dest.Copyright = (Hl7.Fhir.Model.Markdown)Copyright.DeepCopy();
                if(ApprovalDateElement != null) dest.ApprovalDateElement = (Hl7.Fhir.Model.Date)ApprovalDateElement.DeepCopy();
                if(LastReviewDateElement != null) dest.LastReviewDateElement = (Hl7.Fhir.Model.Date)LastReviewDateElement.DeepCopy();
                if(EffectivePeriod != null) dest.EffectivePeriod = (Hl7.Fhir.Model.Period)EffectivePeriod.DeepCopy();
                if(Topic != null) dest.Topic = new List<Hl7.Fhir.Model.CodeableConcept>(Topic.DeepCopy());
                if(Author != null) dest.Author = new List<ContactDetail>(Author.DeepCopy());
                if(Editor != null) dest.Editor = new List<ContactDetail>(Editor.DeepCopy());
                if(Reviewer != null) dest.Reviewer = new List<ContactDetail>(Reviewer.DeepCopy());
                if(Endorser != null) dest.Endorser = new List<ContactDetail>(Endorser.DeepCopy());
                if(RelatedArtifact != null) dest.RelatedArtifact = new List<RelatedArtifact>(RelatedArtifact.DeepCopy());
                if(SynthesisType != null) dest.SynthesisType = (Hl7.Fhir.Model.CodeableConcept)SynthesisType.DeepCopy();
                if(StudyType != null) dest.StudyType = (Hl7.Fhir.Model.CodeableConcept)StudyType.DeepCopy();
                if(Population != null) dest.Population = (Hl7.Fhir.Model.ResourceReference)Population.DeepCopy();
                if(Exposure != null) dest.Exposure = (Hl7.Fhir.Model.ResourceReference)Exposure.DeepCopy();
                if(Outcome != null) dest.Outcome = (Hl7.Fhir.Model.ResourceReference)Outcome.DeepCopy();
                if(SampleSize != null) dest.SampleSize = (Hl7.Fhir.Model.RiskEvidenceSynthesis.SampleSizeComponent)SampleSize.DeepCopy();
                if(RiskEstimate != null) dest.RiskEstimate = (Hl7.Fhir.Model.RiskEvidenceSynthesis.RiskEstimateComponent)RiskEstimate.DeepCopy();
                if(Certainty != null) dest.Certainty = new List<Hl7.Fhir.Model.RiskEvidenceSynthesis.CertaintyComponent>(Certainty.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new RiskEvidenceSynthesis());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as RiskEvidenceSynthesis;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(Description, otherT.Description)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            if( !DeepComparable.Matches(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.Matches(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.Matches(Copyright, otherT.Copyright)) return false;
            if( !DeepComparable.Matches(ApprovalDateElement, otherT.ApprovalDateElement)) return false;
            if( !DeepComparable.Matches(LastReviewDateElement, otherT.LastReviewDateElement)) return false;
            if( !DeepComparable.Matches(EffectivePeriod, otherT.EffectivePeriod)) return false;
            if( !DeepComparable.Matches(Topic, otherT.Topic)) return false;
            if( !DeepComparable.Matches(Author, otherT.Author)) return false;
            if( !DeepComparable.Matches(Editor, otherT.Editor)) return false;
            if( !DeepComparable.Matches(Reviewer, otherT.Reviewer)) return false;
            if( !DeepComparable.Matches(Endorser, otherT.Endorser)) return false;
            if( !DeepComparable.Matches(RelatedArtifact, otherT.RelatedArtifact)) return false;
            if( !DeepComparable.Matches(SynthesisType, otherT.SynthesisType)) return false;
            if( !DeepComparable.Matches(StudyType, otherT.StudyType)) return false;
            if( !DeepComparable.Matches(Population, otherT.Population)) return false;
            if( !DeepComparable.Matches(Exposure, otherT.Exposure)) return false;
            if( !DeepComparable.Matches(Outcome, otherT.Outcome)) return false;
            if( !DeepComparable.Matches(SampleSize, otherT.SampleSize)) return false;
            if( !DeepComparable.Matches(RiskEstimate, otherT.RiskEstimate)) return false;
            if( !DeepComparable.Matches(Certainty, otherT.Certainty)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as RiskEvidenceSynthesis;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(Description, otherT.Description)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            if( !DeepComparable.IsExactly(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.IsExactly(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.IsExactly(Copyright, otherT.Copyright)) return false;
            if( !DeepComparable.IsExactly(ApprovalDateElement, otherT.ApprovalDateElement)) return false;
            if( !DeepComparable.IsExactly(LastReviewDateElement, otherT.LastReviewDateElement)) return false;
            if( !DeepComparable.IsExactly(EffectivePeriod, otherT.EffectivePeriod)) return false;
            if( !DeepComparable.IsExactly(Topic, otherT.Topic)) return false;
            if( !DeepComparable.IsExactly(Author, otherT.Author)) return false;
            if( !DeepComparable.IsExactly(Editor, otherT.Editor)) return false;
            if( !DeepComparable.IsExactly(Reviewer, otherT.Reviewer)) return false;
            if( !DeepComparable.IsExactly(Endorser, otherT.Endorser)) return false;
            if( !DeepComparable.IsExactly(RelatedArtifact, otherT.RelatedArtifact)) return false;
            if( !DeepComparable.IsExactly(SynthesisType, otherT.SynthesisType)) return false;
            if( !DeepComparable.IsExactly(StudyType, otherT.StudyType)) return false;
            if( !DeepComparable.IsExactly(Population, otherT.Population)) return false;
            if( !DeepComparable.IsExactly(Exposure, otherT.Exposure)) return false;
            if( !DeepComparable.IsExactly(Outcome, otherT.Outcome)) return false;
            if( !DeepComparable.IsExactly(SampleSize, otherT.SampleSize)) return false;
            if( !DeepComparable.IsExactly(RiskEstimate, otherT.RiskEstimate)) return false;
            if( !DeepComparable.IsExactly(Certainty, otherT.Certainty)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				if (UrlElement != null) yield return UrlElement;
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				if (VersionElement != null) yield return VersionElement;
				if (NameElement != null) yield return NameElement;
				if (TitleElement != null) yield return TitleElement;
				if (StatusElement != null) yield return StatusElement;
				if (DateElement != null) yield return DateElement;
				if (PublisherElement != null) yield return PublisherElement;
				foreach (var elem in Contact) { if (elem != null) yield return elem; }
				if (Description != null) yield return Description;
				foreach (var elem in Note) { if (elem != null) yield return elem; }
				foreach (var elem in UseContext) { if (elem != null) yield return elem; }
				foreach (var elem in Jurisdiction) { if (elem != null) yield return elem; }
				if (Copyright != null) yield return Copyright;
				if (ApprovalDateElement != null) yield return ApprovalDateElement;
				if (LastReviewDateElement != null) yield return LastReviewDateElement;
				if (EffectivePeriod != null) yield return EffectivePeriod;
				foreach (var elem in Topic) { if (elem != null) yield return elem; }
				foreach (var elem in Author) { if (elem != null) yield return elem; }
				foreach (var elem in Editor) { if (elem != null) yield return elem; }
				foreach (var elem in Reviewer) { if (elem != null) yield return elem; }
				foreach (var elem in Endorser) { if (elem != null) yield return elem; }
				foreach (var elem in RelatedArtifact) { if (elem != null) yield return elem; }
				if (SynthesisType != null) yield return SynthesisType;
				if (StudyType != null) yield return StudyType;
				if (Population != null) yield return Population;
				if (Exposure != null) yield return Exposure;
				if (Outcome != null) yield return Outcome;
				if (SampleSize != null) yield return SampleSize;
				if (RiskEstimate != null) yield return RiskEstimate;
				foreach (var elem in Certainty) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (UrlElement != null) yield return new ElementValue("url", UrlElement);
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                if (VersionElement != null) yield return new ElementValue("version", VersionElement);
                if (NameElement != null) yield return new ElementValue("name", NameElement);
                if (TitleElement != null) yield return new ElementValue("title", TitleElement);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (DateElement != null) yield return new ElementValue("date", DateElement);
                if (PublisherElement != null) yield return new ElementValue("publisher", PublisherElement);
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                if (Description != null) yield return new ElementValue("description", Description);
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                foreach (var elem in UseContext) { if (elem != null) yield return new ElementValue("useContext", elem); }
                foreach (var elem in Jurisdiction) { if (elem != null) yield return new ElementValue("jurisdiction", elem); }
                if (Copyright != null) yield return new ElementValue("copyright", Copyright);
                if (ApprovalDateElement != null) yield return new ElementValue("approvalDate", ApprovalDateElement);
                if (LastReviewDateElement != null) yield return new ElementValue("lastReviewDate", LastReviewDateElement);
                if (EffectivePeriod != null) yield return new ElementValue("effectivePeriod", EffectivePeriod);
                foreach (var elem in Topic) { if (elem != null) yield return new ElementValue("topic", elem); }
                foreach (var elem in Author) { if (elem != null) yield return new ElementValue("author", elem); }
                foreach (var elem in Editor) { if (elem != null) yield return new ElementValue("editor", elem); }
                foreach (var elem in Reviewer) { if (elem != null) yield return new ElementValue("reviewer", elem); }
                foreach (var elem in Endorser) { if (elem != null) yield return new ElementValue("endorser", elem); }
                foreach (var elem in RelatedArtifact) { if (elem != null) yield return new ElementValue("relatedArtifact", elem); }
                if (SynthesisType != null) yield return new ElementValue("synthesisType", SynthesisType);
                if (StudyType != null) yield return new ElementValue("studyType", StudyType);
                if (Population != null) yield return new ElementValue("population", Population);
                if (Exposure != null) yield return new ElementValue("exposure", Exposure);
                if (Outcome != null) yield return new ElementValue("outcome", Outcome);
                if (SampleSize != null) yield return new ElementValue("sampleSize", SampleSize);
                if (RiskEstimate != null) yield return new ElementValue("riskEstimate", RiskEstimate);
                foreach (var elem in Certainty) { if (elem != null) yield return new ElementValue("certainty", elem); }
            }
        }

    }
    
}
