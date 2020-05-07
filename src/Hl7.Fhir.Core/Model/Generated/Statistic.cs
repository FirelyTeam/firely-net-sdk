using System;
using System.Collections.Generic;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Validation;
using System.Linq;
using System.Runtime.Serialization;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Specification;

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
// Generated for FHIR v4.4.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Single statistic
    /// </summary>
    [FhirType("Statistic")]
    [DataContract]
    public partial class Statistic : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "Statistic"; } }
        
        [FhirType("SampleSizeComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class SampleSizeComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "SampleSizeComponent"; } }
            
            /// <summary>
            /// Textual description of sample size for statistic
            /// </summary>
            [FhirElement("description", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Textual description of sample size for statistic
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
            /// Footnote or explanatory note about the sample size
            /// </summary>
            [FhirElement("note", InSummary=true, Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Annotation> Note
            {
                get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
                set { _Note = value; OnPropertyChanged("Note"); }
            }
            
            private List<Hl7.Fhir.Model.Annotation> _Note;
            
            /// <summary>
            /// Number of contributing studies
            /// </summary>
            [FhirElement("numberOfStudies", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.Integer NumberOfStudiesElement
            {
                get { return _NumberOfStudiesElement; }
                set { _NumberOfStudiesElement = value; OnPropertyChanged("NumberOfStudiesElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _NumberOfStudiesElement;
            
            /// <summary>
            /// Number of contributing studies
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
            /// Cumulative number of participants
            /// </summary>
            [FhirElement("numberOfParticipants", InSummary=true, Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Integer NumberOfParticipantsElement
            {
                get { return _NumberOfParticipantsElement; }
                set { _NumberOfParticipantsElement = value; OnPropertyChanged("NumberOfParticipantsElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _NumberOfParticipantsElement;
            
            /// <summary>
            /// Cumulative number of participants
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
            
            /// <summary>
            /// Number of participants with known results for measured variables
            /// </summary>
            [FhirElement("knownDataCount", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Integer KnownDataCountElement
            {
                get { return _KnownDataCountElement; }
                set { _KnownDataCountElement = value; OnPropertyChanged("KnownDataCountElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _KnownDataCountElement;
            
            /// <summary>
            /// Number of participants with known results for measured variables
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? KnownDataCount
            {
                get { return KnownDataCountElement != null ? KnownDataCountElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                      KnownDataCountElement = null; 
                    else
                      KnownDataCountElement = new Hl7.Fhir.Model.Integer(value);
                    OnPropertyChanged("KnownDataCount");
                }
            }
            
            /// <summary>
            /// Number of participants with “positive” results, only used to report actual numerator count for a proportion
            /// </summary>
            [FhirElement("numeratorCount", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.Integer NumeratorCountElement
            {
                get { return _NumeratorCountElement; }
                set { _NumeratorCountElement = value; OnPropertyChanged("NumeratorCountElement"); }
            }
            
            private Hl7.Fhir.Model.Integer _NumeratorCountElement;
            
            /// <summary>
            /// Number of participants with “positive” results, only used to report actual numerator count for a proportion
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SampleSizeComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                    if(NumberOfStudiesElement != null) dest.NumberOfStudiesElement = (Hl7.Fhir.Model.Integer)NumberOfStudiesElement.DeepCopy();
                    if(NumberOfParticipantsElement != null) dest.NumberOfParticipantsElement = (Hl7.Fhir.Model.Integer)NumberOfParticipantsElement.DeepCopy();
                    if(KnownDataCountElement != null) dest.KnownDataCountElement = (Hl7.Fhir.Model.Integer)KnownDataCountElement.DeepCopy();
                    if(NumeratorCountElement != null) dest.NumeratorCountElement = (Hl7.Fhir.Model.Integer)NumeratorCountElement.DeepCopy();
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
                if( !DeepComparable.Matches(Note, otherT.Note)) return false;
                if( !DeepComparable.Matches(NumberOfStudiesElement, otherT.NumberOfStudiesElement)) return false;
                if( !DeepComparable.Matches(NumberOfParticipantsElement, otherT.NumberOfParticipantsElement)) return false;
                if( !DeepComparable.Matches(KnownDataCountElement, otherT.KnownDataCountElement)) return false;
                if( !DeepComparable.Matches(NumeratorCountElement, otherT.NumeratorCountElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SampleSizeComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
                if( !DeepComparable.IsExactly(NumberOfStudiesElement, otherT.NumberOfStudiesElement)) return false;
                if( !DeepComparable.IsExactly(NumberOfParticipantsElement, otherT.NumberOfParticipantsElement)) return false;
                if( !DeepComparable.IsExactly(KnownDataCountElement, otherT.KnownDataCountElement)) return false;
                if( !DeepComparable.IsExactly(NumeratorCountElement, otherT.NumeratorCountElement)) return false;
                
                return true;
            }

            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    foreach (var elem in Note) { if (elem != null) yield return elem; }
                    if (NumberOfStudiesElement != null) yield return NumberOfStudiesElement;
                    if (NumberOfParticipantsElement != null) yield return NumberOfParticipantsElement;
                    if (KnownDataCountElement != null) yield return KnownDataCountElement;
                    if (NumeratorCountElement != null) yield return NumeratorCountElement;
                }
            }

            [NotMapped]
            public override IEnumerable<ElementValue> NamedChildren 
            { 
                get 
                { 
                    foreach (var item in base.NamedChildren) yield return item; 
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                    if (NumberOfStudiesElement != null) yield return new ElementValue("numberOfStudies", NumberOfStudiesElement);
                    if (NumberOfParticipantsElement != null) yield return new ElementValue("numberOfParticipants", NumberOfParticipantsElement);
                    if (KnownDataCountElement != null) yield return new ElementValue("knownDataCount", KnownDataCountElement);
                    if (NumeratorCountElement != null) yield return new ElementValue("numeratorCount", NumeratorCountElement);
 
                } 
            } 
            
        }                
        [FhirType("AttributeEstimateComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class AttributeEstimateComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "AttributeEstimateComponent"; } }
            
            /// <summary>
            /// Textual description of the precision estimate
            /// </summary>
            [FhirElement("description", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Textual description of the precision estimate
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
            /// Footnote or explanatory note about the estimate
            /// </summary>
            [FhirElement("note", InSummary=true, Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Annotation> Note
            {
                get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
                set { _Note = value; OnPropertyChanged("Note"); }
            }
            
            private List<Hl7.Fhir.Model.Annotation> _Note;
            
            /// <summary>
            /// The estimateType of precision estimate, eg confidence interval or p value type
            /// </summary>
            [FhirElement("type", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// The singular quantity of the precision estimate, for precision estimates represented as single values; also used to report unit of measure
            /// </summary>
            [FhirElement("quantity", InSummary=true, Order=70)]
            [DataMember]
            public Quantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            
            private Quantity _Quantity;
            
            /// <summary>
            /// Level of confidence interval, eg 0.95 for 95% confidence interval
            /// </summary>
            [FhirElement("level", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal LevelElement
            {
                get { return _LevelElement; }
                set { _LevelElement = value; OnPropertyChanged("LevelElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _LevelElement;
            
            /// <summary>
            /// Level of confidence interval, eg 0.95 for 95% confidence interval
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
            /// Lower and upper bound values of the precision estimate
            /// </summary>
            [FhirElement("range", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.Range Range
            {
                get { return _Range; }
                set { _Range = value; OnPropertyChanged("Range"); }
            }
            
            private Hl7.Fhir.Model.Range _Range;
            
            /// <summary>
            /// An estimate of the precision of the estimate
            /// </summary>
            [FhirElement("estimateQualifier", InSummary=true, Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Statistic.EstimateQualifierComponent> EstimateQualifier
            {
                get { if(_EstimateQualifier==null) _EstimateQualifier = new List<Hl7.Fhir.Model.Statistic.EstimateQualifierComponent>(); return _EstimateQualifier; }
                set { _EstimateQualifier = value; OnPropertyChanged("EstimateQualifier"); }
            }
            
            private List<Hl7.Fhir.Model.Statistic.EstimateQualifierComponent> _EstimateQualifier;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AttributeEstimateComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Quantity)Quantity.DeepCopy();
                    if(LevelElement != null) dest.LevelElement = (Hl7.Fhir.Model.FhirDecimal)LevelElement.DeepCopy();
                    if(Range != null) dest.Range = (Hl7.Fhir.Model.Range)Range.DeepCopy();
                    if(EstimateQualifier != null) dest.EstimateQualifier = new List<Hl7.Fhir.Model.Statistic.EstimateQualifierComponent>(EstimateQualifier.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new AttributeEstimateComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AttributeEstimateComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(Note, otherT.Note)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(LevelElement, otherT.LevelElement)) return false;
                if( !DeepComparable.Matches(Range, otherT.Range)) return false;
                if( !DeepComparable.Matches(EstimateQualifier, otherT.EstimateQualifier)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AttributeEstimateComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(LevelElement, otherT.LevelElement)) return false;
                if( !DeepComparable.IsExactly(Range, otherT.Range)) return false;
                if( !DeepComparable.IsExactly(EstimateQualifier, otherT.EstimateQualifier)) return false;
                
                return true;
            }

            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    foreach (var elem in Note) { if (elem != null) yield return elem; }
                    if (Type != null) yield return Type;
                    if (Quantity != null) yield return Quantity;
                    if (LevelElement != null) yield return LevelElement;
                    if (Range != null) yield return Range;
                    foreach (var elem in EstimateQualifier) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            public override IEnumerable<ElementValue> NamedChildren 
            { 
                get 
                { 
                    foreach (var item in base.NamedChildren) yield return item; 
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                    if (LevelElement != null) yield return new ElementValue("level", LevelElement);
                    if (Range != null) yield return new ElementValue("range", Range);
                    foreach (var elem in EstimateQualifier) { if (elem != null) yield return new ElementValue("estimateQualifier", elem); }
 
                } 
            } 
            
        }                
        [FhirType("EstimateQualifierComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class EstimateQualifierComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "EstimateQualifierComponent"; } }
            
            /// <summary>
            /// Textual description of the precision estimate
            /// </summary>
            [FhirElement("description", InSummary=true, Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Textual description of the precision estimate
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
            /// Footnote or explanatory note about the estimate
            /// </summary>
            [FhirElement("note", InSummary=true, Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Annotation> Note
            {
                get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
                set { _Note = value; OnPropertyChanged("Note"); }
            }
            
            private List<Hl7.Fhir.Model.Annotation> _Note;
            
            /// <summary>
            /// The estimateType of attribute estimate, eg confidence interval or p value type
            /// </summary>
            [FhirElement("type", InSummary=true, Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// The singular quantity of the attribute estimate, for attribute estimates represented as single values; also used to report unit of measure
            /// </summary>
            [FhirElement("quantity", InSummary=true, Order=70)]
            [DataMember]
            public Quantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            
            private Quantity _Quantity;
            
            /// <summary>
            /// Level of confidence interval, eg 0.95 for 95% confidence interval
            /// </summary>
            [FhirElement("level", InSummary=true, Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal LevelElement
            {
                get { return _LevelElement; }
                set { _LevelElement = value; OnPropertyChanged("LevelElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _LevelElement;
            
            /// <summary>
            /// Level of confidence interval, eg 0.95 for 95% confidence interval
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
            /// Lower and upper bound values of the precision estimate
            /// </summary>
            [FhirElement("range", InSummary=true, Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.Range Range
            {
                get { return _Range; }
                set { _Range = value; OnPropertyChanged("Range"); }
            }
            
            private Hl7.Fhir.Model.Range _Range;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as EstimateQualifierComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Quantity)Quantity.DeepCopy();
                    if(LevelElement != null) dest.LevelElement = (Hl7.Fhir.Model.FhirDecimal)LevelElement.DeepCopy();
                    if(Range != null) dest.Range = (Hl7.Fhir.Model.Range)Range.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new EstimateQualifierComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as EstimateQualifierComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(Note, otherT.Note)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(LevelElement, otherT.LevelElement)) return false;
                if( !DeepComparable.Matches(Range, otherT.Range)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as EstimateQualifierComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(LevelElement, otherT.LevelElement)) return false;
                if( !DeepComparable.IsExactly(Range, otherT.Range)) return false;
                
                return true;
            }

            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    foreach (var elem in Note) { if (elem != null) yield return elem; }
                    if (Type != null) yield return Type;
                    if (Quantity != null) yield return Quantity;
                    if (LevelElement != null) yield return LevelElement;
                    if (Range != null) yield return Range;
                }
            }

            [NotMapped]
            public override IEnumerable<ElementValue> NamedChildren 
            { 
                get 
                { 
                    foreach (var item in base.NamedChildren) yield return item; 
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                    if (LevelElement != null) yield return new ElementValue("level", LevelElement);
                    if (Range != null) yield return new ElementValue("range", Range);
 
                } 
            } 
            
        }                
        /// <summary>
        /// Description of content
        /// </summary>
        [FhirElement("description", InSummary=true, Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DescriptionElement;
        
        /// <summary>
        /// Description of content
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
        /// Footnotes and/or explanatory notes
        /// </summary>
        [FhirElement("note", InSummary=true, Order=100)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Annotation> Note
        {
            get { if(_Note==null) _Note = new List<Hl7.Fhir.Model.Annotation>(); return _Note; }
            set { _Note = value; OnPropertyChanged("Note"); }
        }
        
        private List<Hl7.Fhir.Model.Annotation> _Note;
        
        /// <summary>
        /// Type of statistic, eg relative risk
        /// </summary>
        [FhirElement("statisticType", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept StatisticType
        {
            get { return _StatisticType; }
            set { _StatisticType = value; OnPropertyChanged("StatisticType"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _StatisticType;
        
        /// <summary>
        /// Statistic value
        /// </summary>
        [FhirElement("quantity", InSummary=true, Order=120)]
        [DataMember]
        public Quantity Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; OnPropertyChanged("Quantity"); }
        }
        
        private Quantity _Quantity;
        
        /// <summary>
        /// Number of samples in the statistic
        /// </summary>
        [FhirElement("sampleSize", InSummary=true, Order=130)]
        [DataMember]
        public Hl7.Fhir.Model.Statistic.SampleSizeComponent SampleSize
        {
            get { return _SampleSize; }
            set { _SampleSize = value; OnPropertyChanged("SampleSize"); }
        }
        
        private Hl7.Fhir.Model.Statistic.SampleSizeComponent _SampleSize;
        
        /// <summary>
        /// An estimate of the precision of the statistic
        /// </summary>
        [FhirElement("attributeEstimate", InSummary=true, Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Statistic.AttributeEstimateComponent> AttributeEstimate
        {
            get { if(_AttributeEstimate==null) _AttributeEstimate = new List<Hl7.Fhir.Model.Statistic.AttributeEstimateComponent>(); return _AttributeEstimate; }
            set { _AttributeEstimate = value; OnPropertyChanged("AttributeEstimate"); }
        }
        
        private List<Hl7.Fhir.Model.Statistic.AttributeEstimateComponent> _AttributeEstimate;
        

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Statistic;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if(Note != null) dest.Note = new List<Hl7.Fhir.Model.Annotation>(Note.DeepCopy());
                if(StatisticType != null) dest.StatisticType = (Hl7.Fhir.Model.CodeableConcept)StatisticType.DeepCopy();
                if(Quantity != null) dest.Quantity = (Quantity)Quantity.DeepCopy();
                if(SampleSize != null) dest.SampleSize = (Hl7.Fhir.Model.Statistic.SampleSizeComponent)SampleSize.DeepCopy();
                if(AttributeEstimate != null) dest.AttributeEstimate = new List<Hl7.Fhir.Model.Statistic.AttributeEstimateComponent>(AttributeEstimate.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Statistic());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Statistic;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(Note, otherT.Note)) return false;
            if( !DeepComparable.Matches(StatisticType, otherT.StatisticType)) return false;
            if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
            if( !DeepComparable.Matches(SampleSize, otherT.SampleSize)) return false;
            if( !DeepComparable.Matches(AttributeEstimate, otherT.AttributeEstimate)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Statistic;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(Note, otherT.Note)) return false;
            if( !DeepComparable.IsExactly(StatisticType, otherT.StatisticType)) return false;
            if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
            if( !DeepComparable.IsExactly(SampleSize, otherT.SampleSize)) return false;
            if( !DeepComparable.IsExactly(AttributeEstimate, otherT.AttributeEstimate)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                if (DescriptionElement != null) yield return DescriptionElement;
                foreach (var elem in Note) { if (elem != null) yield return elem; }
                if (StatisticType != null) yield return StatisticType;
                if (Quantity != null) yield return Quantity;
                if (SampleSize != null) yield return SampleSize;
                foreach (var elem in AttributeEstimate) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        public override IEnumerable<ElementValue> NamedChildren 
        { 
            get 
            { 
                foreach (var item in base.NamedChildren) yield return item; 
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                foreach (var elem in Note) { if (elem != null) yield return new ElementValue("note", elem); }
                if (StatisticType != null) yield return new ElementValue("statisticType", StatisticType);
                if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                if (SampleSize != null) yield return new ElementValue("sampleSize", SampleSize);
                foreach (var elem in AttributeEstimate) { if (elem != null) yield return new ElementValue("attributeEstimate", elem); }
 
            } 
        } 
    
    
    }
    
}
