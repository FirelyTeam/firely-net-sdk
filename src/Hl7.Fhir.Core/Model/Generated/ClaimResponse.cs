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
    /// Remittance resource
    /// </summary>
    [FhirType("ClaimResponse", IsResource=true)]
    [DataContract]
    public partial class ClaimResponse : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.ClaimResponse; } }
        [NotMapped]
        public override string TypeName { get { return "ClaimResponse"; } }
        
        [FhirType("ItemComponent")]
        [DataContract]
        public partial class ItemComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ItemComponent"; } }
            
            /// <summary>
            /// Service instance
            /// </summary>
            [FhirElement("sequenceLinkId", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceLinkIdElement
            {
                get { return _SequenceLinkIdElement; }
                set { _SequenceLinkIdElement = value; OnPropertyChanged("SequenceLinkIdElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceLinkIdElement;
            
            /// <summary>
            /// Service instance
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? SequenceLinkId
            {
                get { return SequenceLinkIdElement != null ? SequenceLinkIdElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        SequenceLinkIdElement = null; 
                    else
                        SequenceLinkIdElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("SequenceLinkId");
                }
            }
            
            /// <summary>
            /// List of note numbers which apply
            /// </summary>
            [FhirElement("noteNumber", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> NoteNumberElement
            {
                get { if(_NoteNumberElement==null) _NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _NoteNumberElement; }
                set { _NoteNumberElement = value; OnPropertyChanged("NoteNumberElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _NoteNumberElement;
            
            /// <summary>
            /// List of note numbers which apply
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> NoteNumber
            {
                get { return NoteNumberElement != null ? NoteNumberElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        NoteNumberElement = null; 
                    else
                        NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(value.Select(elem=>new Hl7.Fhir.Model.PositiveInt(elem)));
                    OnPropertyChanged("NoteNumber");
                }
            }
            
            /// <summary>
            /// Adjudication details
            /// </summary>
            [FhirElement("adjudication", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ClaimResponse.AdjudicationComponent> Adjudication
            {
                get { if(_Adjudication==null) _Adjudication = new List<Hl7.Fhir.Model.ClaimResponse.AdjudicationComponent>(); return _Adjudication; }
                set { _Adjudication = value; OnPropertyChanged("Adjudication"); }
            }
            
            private List<Hl7.Fhir.Model.ClaimResponse.AdjudicationComponent> _Adjudication;
            
            /// <summary>
            /// Detail line items
            /// </summary>
            [FhirElement("detail", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ClaimResponse.ItemDetailComponent> Detail
            {
                get { if(_Detail==null) _Detail = new List<Hl7.Fhir.Model.ClaimResponse.ItemDetailComponent>(); return _Detail; }
                set { _Detail = value; OnPropertyChanged("Detail"); }
            }
            
            private List<Hl7.Fhir.Model.ClaimResponse.ItemDetailComponent> _Detail;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ItemComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceLinkIdElement != null) dest.SequenceLinkIdElement = (Hl7.Fhir.Model.PositiveInt)SequenceLinkIdElement.DeepCopy();
                    if(NoteNumberElement != null) dest.NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(NoteNumberElement.DeepCopy());
                    if(Adjudication != null) dest.Adjudication = new List<Hl7.Fhir.Model.ClaimResponse.AdjudicationComponent>(Adjudication.DeepCopy());
                    if(Detail != null) dest.Detail = new List<Hl7.Fhir.Model.ClaimResponse.ItemDetailComponent>(Detail.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ItemComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ItemComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceLinkIdElement, otherT.SequenceLinkIdElement)) return false;
                if( !DeepComparable.Matches(NoteNumberElement, otherT.NoteNumberElement)) return false;
                if( !DeepComparable.Matches(Adjudication, otherT.Adjudication)) return false;
                if( !DeepComparable.Matches(Detail, otherT.Detail)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ItemComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceLinkIdElement, otherT.SequenceLinkIdElement)) return false;
                if( !DeepComparable.IsExactly(NoteNumberElement, otherT.NoteNumberElement)) return false;
                if( !DeepComparable.IsExactly(Adjudication, otherT.Adjudication)) return false;
                if( !DeepComparable.IsExactly(Detail, otherT.Detail)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SequenceLinkIdElement != null) yield return SequenceLinkIdElement;
                    foreach (var elem in NoteNumberElement) { if (elem != null) yield return elem; }
                    foreach (var elem in Adjudication) { if (elem != null) yield return elem; }
                    foreach (var elem in Detail) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SequenceLinkIdElement != null) yield return new ElementValue("sequenceLinkId", SequenceLinkIdElement);
                    foreach (var elem in NoteNumberElement) { if (elem != null) yield return new ElementValue("noteNumber", elem); }
                    foreach (var elem in Adjudication) { if (elem != null) yield return new ElementValue("adjudication", elem); }
                    foreach (var elem in Detail) { if (elem != null) yield return new ElementValue("detail", elem); }
                }
            }

            
        }
        
        
        [FhirType("AdjudicationComponent")]
        [DataContract]
        public partial class AdjudicationComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "AdjudicationComponent"; } }
            
            /// <summary>
            /// Adjudication category such as co-pay, eligible, benefit, etc.
            /// </summary>
            [FhirElement("category", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Category
            {
                get { return _Category; }
                set { _Category = value; OnPropertyChanged("Category"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Category;
            
            /// <summary>
            /// Explanation of Adjudication outcome
            /// </summary>
            [FhirElement("reason", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Reason
            {
                get { return _Reason; }
                set { _Reason = value; OnPropertyChanged("Reason"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Reason;
            
            /// <summary>
            /// Monetary amount
            /// </summary>
            [FhirElement("amount", Order=60)]
            [DataMember]
            public Money Amount
            {
                get { return _Amount; }
                set { _Amount = value; OnPropertyChanged("Amount"); }
            }
            
            private Money _Amount;
            
            /// <summary>
            /// Non-monetary value
            /// </summary>
            [FhirElement("value", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirDecimal ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirDecimal _ValueElement;
            
            /// <summary>
            /// Non-monetary value
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AdjudicationComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                    if(Reason != null) dest.Reason = (Hl7.Fhir.Model.CodeableConcept)Reason.DeepCopy();
                    if(Amount != null) dest.Amount = (Money)Amount.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirDecimal)ValueElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new AdjudicationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AdjudicationComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Category, otherT.Category)) return false;
                if( !DeepComparable.Matches(Reason, otherT.Reason)) return false;
                if( !DeepComparable.Matches(Amount, otherT.Amount)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AdjudicationComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
                if( !DeepComparable.IsExactly(Reason, otherT.Reason)) return false;
                if( !DeepComparable.IsExactly(Amount, otherT.Amount)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Category != null) yield return Category;
                    if (Reason != null) yield return Reason;
                    if (Amount != null) yield return Amount;
                    if (ValueElement != null) yield return ValueElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Category != null) yield return new ElementValue("category", Category);
                    if (Reason != null) yield return new ElementValue("reason", Reason);
                    if (Amount != null) yield return new ElementValue("amount", Amount);
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                }
            }

            
        }
        
        
        [FhirType("ItemDetailComponent")]
        [DataContract]
        public partial class ItemDetailComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ItemDetailComponent"; } }
            
            /// <summary>
            /// Service instance
            /// </summary>
            [FhirElement("sequenceLinkId", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceLinkIdElement
            {
                get { return _SequenceLinkIdElement; }
                set { _SequenceLinkIdElement = value; OnPropertyChanged("SequenceLinkIdElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceLinkIdElement;
            
            /// <summary>
            /// Service instance
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? SequenceLinkId
            {
                get { return SequenceLinkIdElement != null ? SequenceLinkIdElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        SequenceLinkIdElement = null; 
                    else
                        SequenceLinkIdElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("SequenceLinkId");
                }
            }
            
            /// <summary>
            /// List of note numbers which apply
            /// </summary>
            [FhirElement("noteNumber", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> NoteNumberElement
            {
                get { if(_NoteNumberElement==null) _NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _NoteNumberElement; }
                set { _NoteNumberElement = value; OnPropertyChanged("NoteNumberElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _NoteNumberElement;
            
            /// <summary>
            /// List of note numbers which apply
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> NoteNumber
            {
                get { return NoteNumberElement != null ? NoteNumberElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        NoteNumberElement = null; 
                    else
                        NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(value.Select(elem=>new Hl7.Fhir.Model.PositiveInt(elem)));
                    OnPropertyChanged("NoteNumber");
                }
            }
            
            /// <summary>
            /// Detail level adjudication details
            /// </summary>
            [FhirElement("adjudication", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ClaimResponse.AdjudicationComponent> Adjudication
            {
                get { if(_Adjudication==null) _Adjudication = new List<Hl7.Fhir.Model.ClaimResponse.AdjudicationComponent>(); return _Adjudication; }
                set { _Adjudication = value; OnPropertyChanged("Adjudication"); }
            }
            
            private List<Hl7.Fhir.Model.ClaimResponse.AdjudicationComponent> _Adjudication;
            
            /// <summary>
            /// Subdetail line items
            /// </summary>
            [FhirElement("subDetail", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ClaimResponse.SubDetailComponent> SubDetail
            {
                get { if(_SubDetail==null) _SubDetail = new List<Hl7.Fhir.Model.ClaimResponse.SubDetailComponent>(); return _SubDetail; }
                set { _SubDetail = value; OnPropertyChanged("SubDetail"); }
            }
            
            private List<Hl7.Fhir.Model.ClaimResponse.SubDetailComponent> _SubDetail;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ItemDetailComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceLinkIdElement != null) dest.SequenceLinkIdElement = (Hl7.Fhir.Model.PositiveInt)SequenceLinkIdElement.DeepCopy();
                    if(NoteNumberElement != null) dest.NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(NoteNumberElement.DeepCopy());
                    if(Adjudication != null) dest.Adjudication = new List<Hl7.Fhir.Model.ClaimResponse.AdjudicationComponent>(Adjudication.DeepCopy());
                    if(SubDetail != null) dest.SubDetail = new List<Hl7.Fhir.Model.ClaimResponse.SubDetailComponent>(SubDetail.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ItemDetailComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ItemDetailComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceLinkIdElement, otherT.SequenceLinkIdElement)) return false;
                if( !DeepComparable.Matches(NoteNumberElement, otherT.NoteNumberElement)) return false;
                if( !DeepComparable.Matches(Adjudication, otherT.Adjudication)) return false;
                if( !DeepComparable.Matches(SubDetail, otherT.SubDetail)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ItemDetailComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceLinkIdElement, otherT.SequenceLinkIdElement)) return false;
                if( !DeepComparable.IsExactly(NoteNumberElement, otherT.NoteNumberElement)) return false;
                if( !DeepComparable.IsExactly(Adjudication, otherT.Adjudication)) return false;
                if( !DeepComparable.IsExactly(SubDetail, otherT.SubDetail)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SequenceLinkIdElement != null) yield return SequenceLinkIdElement;
                    foreach (var elem in NoteNumberElement) { if (elem != null) yield return elem; }
                    foreach (var elem in Adjudication) { if (elem != null) yield return elem; }
                    foreach (var elem in SubDetail) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SequenceLinkIdElement != null) yield return new ElementValue("sequenceLinkId", SequenceLinkIdElement);
                    foreach (var elem in NoteNumberElement) { if (elem != null) yield return new ElementValue("noteNumber", elem); }
                    foreach (var elem in Adjudication) { if (elem != null) yield return new ElementValue("adjudication", elem); }
                    foreach (var elem in SubDetail) { if (elem != null) yield return new ElementValue("subDetail", elem); }
                }
            }

            
        }
        
        
        [FhirType("SubDetailComponent")]
        [DataContract]
        public partial class SubDetailComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "SubDetailComponent"; } }
            
            /// <summary>
            /// Service instance
            /// </summary>
            [FhirElement("sequenceLinkId", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceLinkIdElement
            {
                get { return _SequenceLinkIdElement; }
                set { _SequenceLinkIdElement = value; OnPropertyChanged("SequenceLinkIdElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceLinkIdElement;
            
            /// <summary>
            /// Service instance
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? SequenceLinkId
            {
                get { return SequenceLinkIdElement != null ? SequenceLinkIdElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        SequenceLinkIdElement = null; 
                    else
                        SequenceLinkIdElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("SequenceLinkId");
                }
            }
            
            /// <summary>
            /// List of note numbers which apply
            /// </summary>
            [FhirElement("noteNumber", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> NoteNumberElement
            {
                get { if(_NoteNumberElement==null) _NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _NoteNumberElement; }
                set { _NoteNumberElement = value; OnPropertyChanged("NoteNumberElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _NoteNumberElement;
            
            /// <summary>
            /// List of note numbers which apply
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> NoteNumber
            {
                get { return NoteNumberElement != null ? NoteNumberElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        NoteNumberElement = null; 
                    else
                        NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(value.Select(elem=>new Hl7.Fhir.Model.PositiveInt(elem)));
                    OnPropertyChanged("NoteNumber");
                }
            }
            
            /// <summary>
            /// Subdetail level adjudication details
            /// </summary>
            [FhirElement("adjudication", Order=60)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ClaimResponse.AdjudicationComponent> Adjudication
            {
                get { if(_Adjudication==null) _Adjudication = new List<Hl7.Fhir.Model.ClaimResponse.AdjudicationComponent>(); return _Adjudication; }
                set { _Adjudication = value; OnPropertyChanged("Adjudication"); }
            }
            
            private List<Hl7.Fhir.Model.ClaimResponse.AdjudicationComponent> _Adjudication;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SubDetailComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceLinkIdElement != null) dest.SequenceLinkIdElement = (Hl7.Fhir.Model.PositiveInt)SequenceLinkIdElement.DeepCopy();
                    if(NoteNumberElement != null) dest.NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(NoteNumberElement.DeepCopy());
                    if(Adjudication != null) dest.Adjudication = new List<Hl7.Fhir.Model.ClaimResponse.AdjudicationComponent>(Adjudication.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SubDetailComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SubDetailComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceLinkIdElement, otherT.SequenceLinkIdElement)) return false;
                if( !DeepComparable.Matches(NoteNumberElement, otherT.NoteNumberElement)) return false;
                if( !DeepComparable.Matches(Adjudication, otherT.Adjudication)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SubDetailComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceLinkIdElement, otherT.SequenceLinkIdElement)) return false;
                if( !DeepComparable.IsExactly(NoteNumberElement, otherT.NoteNumberElement)) return false;
                if( !DeepComparable.IsExactly(Adjudication, otherT.Adjudication)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SequenceLinkIdElement != null) yield return SequenceLinkIdElement;
                    foreach (var elem in NoteNumberElement) { if (elem != null) yield return elem; }
                    foreach (var elem in Adjudication) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SequenceLinkIdElement != null) yield return new ElementValue("sequenceLinkId", SequenceLinkIdElement);
                    foreach (var elem in NoteNumberElement) { if (elem != null) yield return new ElementValue("noteNumber", elem); }
                    foreach (var elem in Adjudication) { if (elem != null) yield return new ElementValue("adjudication", elem); }
                }
            }

            
        }
        
        
        [FhirType("AddedItemComponent")]
        [DataContract]
        public partial class AddedItemComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "AddedItemComponent"; } }
            
            /// <summary>
            /// Service instances
            /// </summary>
            [FhirElement("sequenceLinkId", Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> SequenceLinkIdElement
            {
                get { if(_SequenceLinkIdElement==null) _SequenceLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _SequenceLinkIdElement; }
                set { _SequenceLinkIdElement = value; OnPropertyChanged("SequenceLinkIdElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _SequenceLinkIdElement;
            
            /// <summary>
            /// Service instances
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> SequenceLinkId
            {
                get { return SequenceLinkIdElement != null ? SequenceLinkIdElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        SequenceLinkIdElement = null; 
                    else
                        SequenceLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(value.Select(elem=>new Hl7.Fhir.Model.PositiveInt(elem)));
                    OnPropertyChanged("SequenceLinkId");
                }
            }
            
            /// <summary>
            /// Revenue or cost center code
            /// </summary>
            [FhirElement("revenue", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Revenue
            {
                get { return _Revenue; }
                set { _Revenue = value; OnPropertyChanged("Revenue"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Revenue;
            
            /// <summary>
            /// Type of service or product
            /// </summary>
            [FhirElement("category", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Category
            {
                get { return _Category; }
                set { _Category = value; OnPropertyChanged("Category"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Category;
            
            /// <summary>
            /// Group, Service or Product
            /// </summary>
            [FhirElement("service", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Service
            {
                get { return _Service; }
                set { _Service = value; OnPropertyChanged("Service"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Service;
            
            /// <summary>
            /// Service/Product billing modifiers
            /// </summary>
            [FhirElement("modifier", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Modifier
            {
                get { if(_Modifier==null) _Modifier = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Modifier; }
                set { _Modifier = value; OnPropertyChanged("Modifier"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Modifier;
            
            /// <summary>
            /// Professional fee or Product charge
            /// </summary>
            [FhirElement("fee", Order=90)]
            [DataMember]
            public Money Fee
            {
                get { return _Fee; }
                set { _Fee = value; OnPropertyChanged("Fee"); }
            }
            
            private Money _Fee;
            
            /// <summary>
            /// List of note numbers which apply
            /// </summary>
            [FhirElement("noteNumber", Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> NoteNumberElement
            {
                get { if(_NoteNumberElement==null) _NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _NoteNumberElement; }
                set { _NoteNumberElement = value; OnPropertyChanged("NoteNumberElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _NoteNumberElement;
            
            /// <summary>
            /// List of note numbers which apply
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> NoteNumber
            {
                get { return NoteNumberElement != null ? NoteNumberElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        NoteNumberElement = null; 
                    else
                        NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(value.Select(elem=>new Hl7.Fhir.Model.PositiveInt(elem)));
                    OnPropertyChanged("NoteNumber");
                }
            }
            
            /// <summary>
            /// Added items adjudication
            /// </summary>
            [FhirElement("adjudication", Order=110)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ClaimResponse.AdjudicationComponent> Adjudication
            {
                get { if(_Adjudication==null) _Adjudication = new List<Hl7.Fhir.Model.ClaimResponse.AdjudicationComponent>(); return _Adjudication; }
                set { _Adjudication = value; OnPropertyChanged("Adjudication"); }
            }
            
            private List<Hl7.Fhir.Model.ClaimResponse.AdjudicationComponent> _Adjudication;
            
            /// <summary>
            /// Added items details
            /// </summary>
            [FhirElement("detail", Order=120)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ClaimResponse.AddedItemsDetailComponent> Detail
            {
                get { if(_Detail==null) _Detail = new List<Hl7.Fhir.Model.ClaimResponse.AddedItemsDetailComponent>(); return _Detail; }
                set { _Detail = value; OnPropertyChanged("Detail"); }
            }
            
            private List<Hl7.Fhir.Model.ClaimResponse.AddedItemsDetailComponent> _Detail;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AddedItemComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceLinkIdElement != null) dest.SequenceLinkIdElement = new List<Hl7.Fhir.Model.PositiveInt>(SequenceLinkIdElement.DeepCopy());
                    if(Revenue != null) dest.Revenue = (Hl7.Fhir.Model.CodeableConcept)Revenue.DeepCopy();
                    if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                    if(Service != null) dest.Service = (Hl7.Fhir.Model.CodeableConcept)Service.DeepCopy();
                    if(Modifier != null) dest.Modifier = new List<Hl7.Fhir.Model.CodeableConcept>(Modifier.DeepCopy());
                    if(Fee != null) dest.Fee = (Money)Fee.DeepCopy();
                    if(NoteNumberElement != null) dest.NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(NoteNumberElement.DeepCopy());
                    if(Adjudication != null) dest.Adjudication = new List<Hl7.Fhir.Model.ClaimResponse.AdjudicationComponent>(Adjudication.DeepCopy());
                    if(Detail != null) dest.Detail = new List<Hl7.Fhir.Model.ClaimResponse.AddedItemsDetailComponent>(Detail.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new AddedItemComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AddedItemComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceLinkIdElement, otherT.SequenceLinkIdElement)) return false;
                if( !DeepComparable.Matches(Revenue, otherT.Revenue)) return false;
                if( !DeepComparable.Matches(Category, otherT.Category)) return false;
                if( !DeepComparable.Matches(Service, otherT.Service)) return false;
                if( !DeepComparable.Matches(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.Matches(Fee, otherT.Fee)) return false;
                if( !DeepComparable.Matches(NoteNumberElement, otherT.NoteNumberElement)) return false;
                if( !DeepComparable.Matches(Adjudication, otherT.Adjudication)) return false;
                if( !DeepComparable.Matches(Detail, otherT.Detail)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AddedItemComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceLinkIdElement, otherT.SequenceLinkIdElement)) return false;
                if( !DeepComparable.IsExactly(Revenue, otherT.Revenue)) return false;
                if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
                if( !DeepComparable.IsExactly(Service, otherT.Service)) return false;
                if( !DeepComparable.IsExactly(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.IsExactly(Fee, otherT.Fee)) return false;
                if( !DeepComparable.IsExactly(NoteNumberElement, otherT.NoteNumberElement)) return false;
                if( !DeepComparable.IsExactly(Adjudication, otherT.Adjudication)) return false;
                if( !DeepComparable.IsExactly(Detail, otherT.Detail)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in SequenceLinkIdElement) { if (elem != null) yield return elem; }
                    if (Revenue != null) yield return Revenue;
                    if (Category != null) yield return Category;
                    if (Service != null) yield return Service;
                    foreach (var elem in Modifier) { if (elem != null) yield return elem; }
                    if (Fee != null) yield return Fee;
                    foreach (var elem in NoteNumberElement) { if (elem != null) yield return elem; }
                    foreach (var elem in Adjudication) { if (elem != null) yield return elem; }
                    foreach (var elem in Detail) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in SequenceLinkIdElement) { if (elem != null) yield return new ElementValue("sequenceLinkId", elem); }
                    if (Revenue != null) yield return new ElementValue("revenue", Revenue);
                    if (Category != null) yield return new ElementValue("category", Category);
                    if (Service != null) yield return new ElementValue("service", Service);
                    foreach (var elem in Modifier) { if (elem != null) yield return new ElementValue("modifier", elem); }
                    if (Fee != null) yield return new ElementValue("fee", Fee);
                    foreach (var elem in NoteNumberElement) { if (elem != null) yield return new ElementValue("noteNumber", elem); }
                    foreach (var elem in Adjudication) { if (elem != null) yield return new ElementValue("adjudication", elem); }
                    foreach (var elem in Detail) { if (elem != null) yield return new ElementValue("detail", elem); }
                }
            }

            
        }
        
        
        [FhirType("AddedItemsDetailComponent")]
        [DataContract]
        public partial class AddedItemsDetailComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "AddedItemsDetailComponent"; } }
            
            /// <summary>
            /// Revenue or cost center code
            /// </summary>
            [FhirElement("revenue", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Revenue
            {
                get { return _Revenue; }
                set { _Revenue = value; OnPropertyChanged("Revenue"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Revenue;
            
            /// <summary>
            /// Type of service or product
            /// </summary>
            [FhirElement("category", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Category
            {
                get { return _Category; }
                set { _Category = value; OnPropertyChanged("Category"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Category;
            
            /// <summary>
            /// Service or Product
            /// </summary>
            [FhirElement("service", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Service
            {
                get { return _Service; }
                set { _Service = value; OnPropertyChanged("Service"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Service;
            
            /// <summary>
            /// Service/Product billing modifiers
            /// </summary>
            [FhirElement("modifier", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Modifier
            {
                get { if(_Modifier==null) _Modifier = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Modifier; }
                set { _Modifier = value; OnPropertyChanged("Modifier"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Modifier;
            
            /// <summary>
            /// Professional fee or Product charge
            /// </summary>
            [FhirElement("fee", Order=80)]
            [DataMember]
            public Money Fee
            {
                get { return _Fee; }
                set { _Fee = value; OnPropertyChanged("Fee"); }
            }
            
            private Money _Fee;
            
            /// <summary>
            /// List of note numbers which apply
            /// </summary>
            [FhirElement("noteNumber", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.PositiveInt> NoteNumberElement
            {
                get { if(_NoteNumberElement==null) _NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(); return _NoteNumberElement; }
                set { _NoteNumberElement = value; OnPropertyChanged("NoteNumberElement"); }
            }
            
            private List<Hl7.Fhir.Model.PositiveInt> _NoteNumberElement;
            
            /// <summary>
            /// List of note numbers which apply
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<int?> NoteNumber
            {
                get { return NoteNumberElement != null ? NoteNumberElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        NoteNumberElement = null; 
                    else
                        NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(value.Select(elem=>new Hl7.Fhir.Model.PositiveInt(elem)));
                    OnPropertyChanged("NoteNumber");
                }
            }
            
            /// <summary>
            /// Added items detail adjudication
            /// </summary>
            [FhirElement("adjudication", Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ClaimResponse.AdjudicationComponent> Adjudication
            {
                get { if(_Adjudication==null) _Adjudication = new List<Hl7.Fhir.Model.ClaimResponse.AdjudicationComponent>(); return _Adjudication; }
                set { _Adjudication = value; OnPropertyChanged("Adjudication"); }
            }
            
            private List<Hl7.Fhir.Model.ClaimResponse.AdjudicationComponent> _Adjudication;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as AddedItemsDetailComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Revenue != null) dest.Revenue = (Hl7.Fhir.Model.CodeableConcept)Revenue.DeepCopy();
                    if(Category != null) dest.Category = (Hl7.Fhir.Model.CodeableConcept)Category.DeepCopy();
                    if(Service != null) dest.Service = (Hl7.Fhir.Model.CodeableConcept)Service.DeepCopy();
                    if(Modifier != null) dest.Modifier = new List<Hl7.Fhir.Model.CodeableConcept>(Modifier.DeepCopy());
                    if(Fee != null) dest.Fee = (Money)Fee.DeepCopy();
                    if(NoteNumberElement != null) dest.NoteNumberElement = new List<Hl7.Fhir.Model.PositiveInt>(NoteNumberElement.DeepCopy());
                    if(Adjudication != null) dest.Adjudication = new List<Hl7.Fhir.Model.ClaimResponse.AdjudicationComponent>(Adjudication.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new AddedItemsDetailComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AddedItemsDetailComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Revenue, otherT.Revenue)) return false;
                if( !DeepComparable.Matches(Category, otherT.Category)) return false;
                if( !DeepComparable.Matches(Service, otherT.Service)) return false;
                if( !DeepComparable.Matches(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.Matches(Fee, otherT.Fee)) return false;
                if( !DeepComparable.Matches(NoteNumberElement, otherT.NoteNumberElement)) return false;
                if( !DeepComparable.Matches(Adjudication, otherT.Adjudication)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AddedItemsDetailComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Revenue, otherT.Revenue)) return false;
                if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
                if( !DeepComparable.IsExactly(Service, otherT.Service)) return false;
                if( !DeepComparable.IsExactly(Modifier, otherT.Modifier)) return false;
                if( !DeepComparable.IsExactly(Fee, otherT.Fee)) return false;
                if( !DeepComparable.IsExactly(NoteNumberElement, otherT.NoteNumberElement)) return false;
                if( !DeepComparable.IsExactly(Adjudication, otherT.Adjudication)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Revenue != null) yield return Revenue;
                    if (Category != null) yield return Category;
                    if (Service != null) yield return Service;
                    foreach (var elem in Modifier) { if (elem != null) yield return elem; }
                    if (Fee != null) yield return Fee;
                    foreach (var elem in NoteNumberElement) { if (elem != null) yield return elem; }
                    foreach (var elem in Adjudication) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Revenue != null) yield return new ElementValue("revenue", Revenue);
                    if (Category != null) yield return new ElementValue("category", Category);
                    if (Service != null) yield return new ElementValue("service", Service);
                    foreach (var elem in Modifier) { if (elem != null) yield return new ElementValue("modifier", elem); }
                    if (Fee != null) yield return new ElementValue("fee", Fee);
                    foreach (var elem in NoteNumberElement) { if (elem != null) yield return new ElementValue("noteNumber", elem); }
                    foreach (var elem in Adjudication) { if (elem != null) yield return new ElementValue("adjudication", elem); }
                }
            }

            
        }
        
        
        [FhirType("ErrorComponent")]
        [DataContract]
        public partial class ErrorComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ErrorComponent"; } }
            
            /// <summary>
            /// Item sequence number
            /// </summary>
            [FhirElement("sequenceLinkId", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceLinkIdElement
            {
                get { return _SequenceLinkIdElement; }
                set { _SequenceLinkIdElement = value; OnPropertyChanged("SequenceLinkIdElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceLinkIdElement;
            
            /// <summary>
            /// Item sequence number
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? SequenceLinkId
            {
                get { return SequenceLinkIdElement != null ? SequenceLinkIdElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        SequenceLinkIdElement = null; 
                    else
                        SequenceLinkIdElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("SequenceLinkId");
                }
            }
            
            /// <summary>
            /// Detail sequence number
            /// </summary>
            [FhirElement("detailSequenceLinkId", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt DetailSequenceLinkIdElement
            {
                get { return _DetailSequenceLinkIdElement; }
                set { _DetailSequenceLinkIdElement = value; OnPropertyChanged("DetailSequenceLinkIdElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _DetailSequenceLinkIdElement;
            
            /// <summary>
            /// Detail sequence number
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? DetailSequenceLinkId
            {
                get { return DetailSequenceLinkIdElement != null ? DetailSequenceLinkIdElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        DetailSequenceLinkIdElement = null; 
                    else
                        DetailSequenceLinkIdElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("DetailSequenceLinkId");
                }
            }
            
            /// <summary>
            /// Subdetail sequence number
            /// </summary>
            [FhirElement("subdetailSequenceLinkId", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SubdetailSequenceLinkIdElement
            {
                get { return _SubdetailSequenceLinkIdElement; }
                set { _SubdetailSequenceLinkIdElement = value; OnPropertyChanged("SubdetailSequenceLinkIdElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SubdetailSequenceLinkIdElement;
            
            /// <summary>
            /// Subdetail sequence number
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? SubdetailSequenceLinkId
            {
                get { return SubdetailSequenceLinkIdElement != null ? SubdetailSequenceLinkIdElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        SubdetailSequenceLinkIdElement = null; 
                    else
                        SubdetailSequenceLinkIdElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("SubdetailSequenceLinkId");
                }
            }
            
            /// <summary>
            /// Error code detailing processing issues
            /// </summary>
            [FhirElement("code", Order=70)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ErrorComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceLinkIdElement != null) dest.SequenceLinkIdElement = (Hl7.Fhir.Model.PositiveInt)SequenceLinkIdElement.DeepCopy();
                    if(DetailSequenceLinkIdElement != null) dest.DetailSequenceLinkIdElement = (Hl7.Fhir.Model.PositiveInt)DetailSequenceLinkIdElement.DeepCopy();
                    if(SubdetailSequenceLinkIdElement != null) dest.SubdetailSequenceLinkIdElement = (Hl7.Fhir.Model.PositiveInt)SubdetailSequenceLinkIdElement.DeepCopy();
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ErrorComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ErrorComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceLinkIdElement, otherT.SequenceLinkIdElement)) return false;
                if( !DeepComparable.Matches(DetailSequenceLinkIdElement, otherT.DetailSequenceLinkIdElement)) return false;
                if( !DeepComparable.Matches(SubdetailSequenceLinkIdElement, otherT.SubdetailSequenceLinkIdElement)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ErrorComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceLinkIdElement, otherT.SequenceLinkIdElement)) return false;
                if( !DeepComparable.IsExactly(DetailSequenceLinkIdElement, otherT.DetailSequenceLinkIdElement)) return false;
                if( !DeepComparable.IsExactly(SubdetailSequenceLinkIdElement, otherT.SubdetailSequenceLinkIdElement)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SequenceLinkIdElement != null) yield return SequenceLinkIdElement;
                    if (DetailSequenceLinkIdElement != null) yield return DetailSequenceLinkIdElement;
                    if (SubdetailSequenceLinkIdElement != null) yield return SubdetailSequenceLinkIdElement;
                    if (Code != null) yield return Code;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SequenceLinkIdElement != null) yield return new ElementValue("sequenceLinkId", SequenceLinkIdElement);
                    if (DetailSequenceLinkIdElement != null) yield return new ElementValue("detailSequenceLinkId", DetailSequenceLinkIdElement);
                    if (SubdetailSequenceLinkIdElement != null) yield return new ElementValue("subdetailSequenceLinkId", SubdetailSequenceLinkIdElement);
                    if (Code != null) yield return new ElementValue("code", Code);
                }
            }

            
        }
        
        
        [FhirType("PaymentComponent")]
        [DataContract]
        public partial class PaymentComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "PaymentComponent"; } }
            
            /// <summary>
            /// Partial or Complete
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
            /// Payment adjustment for non-Claim issues
            /// </summary>
            [FhirElement("adjustment", Order=50)]
            [DataMember]
            public Money Adjustment
            {
                get { return _Adjustment; }
                set { _Adjustment = value; OnPropertyChanged("Adjustment"); }
            }
            
            private Money _Adjustment;
            
            /// <summary>
            /// Explanation for the non-claim adjustment
            /// </summary>
            [FhirElement("adjustmentReason", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept AdjustmentReason
            {
                get { return _AdjustmentReason; }
                set { _AdjustmentReason = value; OnPropertyChanged("AdjustmentReason"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _AdjustmentReason;
            
            /// <summary>
            /// Expected data of Payment
            /// </summary>
            [FhirElement("date", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.Date DateElement
            {
                get { return _DateElement; }
                set { _DateElement = value; OnPropertyChanged("DateElement"); }
            }
            
            private Hl7.Fhir.Model.Date _DateElement;
            
            /// <summary>
            /// Expected data of Payment
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
                        DateElement = new Hl7.Fhir.Model.Date(value);
                    OnPropertyChanged("Date");
                }
            }
            
            /// <summary>
            /// Payable amount after adjustment
            /// </summary>
            [FhirElement("amount", Order=80)]
            [DataMember]
            public Money Amount
            {
                get { return _Amount; }
                set { _Amount = value; OnPropertyChanged("Amount"); }
            }
            
            private Money _Amount;
            
            /// <summary>
            /// Identifier of the payment instrument
            /// </summary>
            [FhirElement("identifier", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PaymentComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Adjustment != null) dest.Adjustment = (Money)Adjustment.DeepCopy();
                    if(AdjustmentReason != null) dest.AdjustmentReason = (Hl7.Fhir.Model.CodeableConcept)AdjustmentReason.DeepCopy();
                    if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.Date)DateElement.DeepCopy();
                    if(Amount != null) dest.Amount = (Money)Amount.DeepCopy();
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new PaymentComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PaymentComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Adjustment, otherT.Adjustment)) return false;
                if( !DeepComparable.Matches(AdjustmentReason, otherT.AdjustmentReason)) return false;
                if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.Matches(Amount, otherT.Amount)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PaymentComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Adjustment, otherT.Adjustment)) return false;
                if( !DeepComparable.IsExactly(AdjustmentReason, otherT.AdjustmentReason)) return false;
                if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
                if( !DeepComparable.IsExactly(Amount, otherT.Amount)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Type != null) yield return Type;
                    if (Adjustment != null) yield return Adjustment;
                    if (AdjustmentReason != null) yield return AdjustmentReason;
                    if (DateElement != null) yield return DateElement;
                    if (Amount != null) yield return Amount;
                    if (Identifier != null) yield return Identifier;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Adjustment != null) yield return new ElementValue("adjustment", Adjustment);
                    if (AdjustmentReason != null) yield return new ElementValue("adjustmentReason", AdjustmentReason);
                    if (DateElement != null) yield return new ElementValue("date", DateElement);
                    if (Amount != null) yield return new ElementValue("amount", Amount);
                    if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                }
            }

            
        }
        
        
        [FhirType("NoteComponent")]
        [DataContract]
        public partial class NoteComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "NoteComponent"; } }
            
            /// <summary>
            /// Sequence Number for this note
            /// </summary>
            [FhirElement("number", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt NumberElement
            {
                get { return _NumberElement; }
                set { _NumberElement = value; OnPropertyChanged("NumberElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _NumberElement;
            
            /// <summary>
            /// Sequence Number for this note
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Number
            {
                get { return NumberElement != null ? NumberElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        NumberElement = null; 
                    else
                        NumberElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("Number");
                }
            }
            
            /// <summary>
            /// display | print | printoper
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
            /// Note explanatory text
            /// </summary>
            [FhirElement("text", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TextElement
            {
                get { return _TextElement; }
                set { _TextElement = value; OnPropertyChanged("TextElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TextElement;
            
            /// <summary>
            /// Note explanatory text
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Text
            {
                get { return TextElement != null ? TextElement.Value : null; }
                set
                {
                    if (value == null)
                        TextElement = null; 
                    else
                        TextElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Text");
                }
            }
            
            /// <summary>
            /// Language if different from the resource
            /// </summary>
            [FhirElement("language", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Language
            {
                get { return _Language; }
                set { _Language = value; OnPropertyChanged("Language"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Language;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as NoteComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(NumberElement != null) dest.NumberElement = (Hl7.Fhir.Model.PositiveInt)NumberElement.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(TextElement != null) dest.TextElement = (Hl7.Fhir.Model.FhirString)TextElement.DeepCopy();
                    if(Language != null) dest.Language = (Hl7.Fhir.Model.CodeableConcept)Language.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new NoteComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as NoteComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(NumberElement, otherT.NumberElement)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(TextElement, otherT.TextElement)) return false;
                if( !DeepComparable.Matches(Language, otherT.Language)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as NoteComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(NumberElement, otherT.NumberElement)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(TextElement, otherT.TextElement)) return false;
                if( !DeepComparable.IsExactly(Language, otherT.Language)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (NumberElement != null) yield return NumberElement;
                    if (Type != null) yield return Type;
                    if (TextElement != null) yield return TextElement;
                    if (Language != null) yield return Language;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (NumberElement != null) yield return new ElementValue("number", NumberElement);
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (TextElement != null) yield return new ElementValue("text", TextElement);
                    if (Language != null) yield return new ElementValue("language", Language);
                }
            }

            
        }
        
        
        [FhirType("InsuranceComponent")]
        [DataContract]
        public partial class InsuranceComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "InsuranceComponent"; } }
            
            /// <summary>
            /// Service instance identifier
            /// </summary>
            [FhirElement("sequence", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.PositiveInt SequenceElement
            {
                get { return _SequenceElement; }
                set { _SequenceElement = value; OnPropertyChanged("SequenceElement"); }
            }
            
            private Hl7.Fhir.Model.PositiveInt _SequenceElement;
            
            /// <summary>
            /// Service instance identifier
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public int? Sequence
            {
                get { return SequenceElement != null ? SequenceElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        SequenceElement = null; 
                    else
                        SequenceElement = new Hl7.Fhir.Model.PositiveInt(value);
                    OnPropertyChanged("Sequence");
                }
            }
            
            /// <summary>
            /// Is the focal Coverage
            /// </summary>
            [FhirElement("focal", Order=50)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean FocalElement
            {
                get { return _FocalElement; }
                set { _FocalElement = value; OnPropertyChanged("FocalElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _FocalElement;
            
            /// <summary>
            /// Is the focal Coverage
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? Focal
            {
                get { return FocalElement != null ? FocalElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        FocalElement = null; 
                    else
                        FocalElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("Focal");
                }
            }
            
            /// <summary>
            /// Insurance information
            /// </summary>
            [FhirElement("coverage", Order=60)]
            [CLSCompliant(false)]
			[References("Coverage")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Coverage
            {
                get { return _Coverage; }
                set { _Coverage = value; OnPropertyChanged("Coverage"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Coverage;
            
            /// <summary>
            /// Business agreement
            /// </summary>
            [FhirElement("businessArrangement", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString BusinessArrangementElement
            {
                get { return _BusinessArrangementElement; }
                set { _BusinessArrangementElement = value; OnPropertyChanged("BusinessArrangementElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _BusinessArrangementElement;
            
            /// <summary>
            /// Business agreement
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string BusinessArrangement
            {
                get { return BusinessArrangementElement != null ? BusinessArrangementElement.Value : null; }
                set
                {
                    if (value == null)
                        BusinessArrangementElement = null; 
                    else
                        BusinessArrangementElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("BusinessArrangement");
                }
            }
            
            /// <summary>
            /// Pre-Authorization/Determination Reference
            /// </summary>
            [FhirElement("preAuthRef", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> PreAuthRefElement
            {
                get { if(_PreAuthRefElement==null) _PreAuthRefElement = new List<Hl7.Fhir.Model.FhirString>(); return _PreAuthRefElement; }
                set { _PreAuthRefElement = value; OnPropertyChanged("PreAuthRefElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _PreAuthRefElement;
            
            /// <summary>
            /// Pre-Authorization/Determination Reference
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> PreAuthRef
            {
                get { return PreAuthRefElement != null ? PreAuthRefElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        PreAuthRefElement = null; 
                    else
                        PreAuthRefElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("PreAuthRef");
                }
            }
            
            /// <summary>
            /// Adjudication results
            /// </summary>
            [FhirElement("claimResponse", Order=90)]
            [CLSCompliant(false)]
			[References("ClaimResponse")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference ClaimResponse
            {
                get { return _ClaimResponse; }
                set { _ClaimResponse = value; OnPropertyChanged("ClaimResponse"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _ClaimResponse;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as InsuranceComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(SequenceElement != null) dest.SequenceElement = (Hl7.Fhir.Model.PositiveInt)SequenceElement.DeepCopy();
                    if(FocalElement != null) dest.FocalElement = (Hl7.Fhir.Model.FhirBoolean)FocalElement.DeepCopy();
                    if(Coverage != null) dest.Coverage = (Hl7.Fhir.Model.ResourceReference)Coverage.DeepCopy();
                    if(BusinessArrangementElement != null) dest.BusinessArrangementElement = (Hl7.Fhir.Model.FhirString)BusinessArrangementElement.DeepCopy();
                    if(PreAuthRefElement != null) dest.PreAuthRefElement = new List<Hl7.Fhir.Model.FhirString>(PreAuthRefElement.DeepCopy());
                    if(ClaimResponse != null) dest.ClaimResponse = (Hl7.Fhir.Model.ResourceReference)ClaimResponse.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new InsuranceComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as InsuranceComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.Matches(FocalElement, otherT.FocalElement)) return false;
                if( !DeepComparable.Matches(Coverage, otherT.Coverage)) return false;
                if( !DeepComparable.Matches(BusinessArrangementElement, otherT.BusinessArrangementElement)) return false;
                if( !DeepComparable.Matches(PreAuthRefElement, otherT.PreAuthRefElement)) return false;
                if( !DeepComparable.Matches(ClaimResponse, otherT.ClaimResponse)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as InsuranceComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(SequenceElement, otherT.SequenceElement)) return false;
                if( !DeepComparable.IsExactly(FocalElement, otherT.FocalElement)) return false;
                if( !DeepComparable.IsExactly(Coverage, otherT.Coverage)) return false;
                if( !DeepComparable.IsExactly(BusinessArrangementElement, otherT.BusinessArrangementElement)) return false;
                if( !DeepComparable.IsExactly(PreAuthRefElement, otherT.PreAuthRefElement)) return false;
                if( !DeepComparable.IsExactly(ClaimResponse, otherT.ClaimResponse)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (SequenceElement != null) yield return SequenceElement;
                    if (FocalElement != null) yield return FocalElement;
                    if (Coverage != null) yield return Coverage;
                    if (BusinessArrangementElement != null) yield return BusinessArrangementElement;
                    foreach (var elem in PreAuthRefElement) { if (elem != null) yield return elem; }
                    if (ClaimResponse != null) yield return ClaimResponse;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (SequenceElement != null) yield return new ElementValue("sequence", SequenceElement);
                    if (FocalElement != null) yield return new ElementValue("focal", FocalElement);
                    if (Coverage != null) yield return new ElementValue("coverage", Coverage);
                    if (BusinessArrangementElement != null) yield return new ElementValue("businessArrangement", BusinessArrangementElement);
                    foreach (var elem in PreAuthRefElement) { if (elem != null) yield return new ElementValue("preAuthRef", elem); }
                    if (ClaimResponse != null) yield return new ElementValue("claimResponse", ClaimResponse);
                }
            }

            
        }
        
        
        /// <summary>
        /// Response  number
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
        /// The subject of the Products and Services
        /// </summary>
        [FhirElement("patient", Order=110)]
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
        /// Creation date
        /// </summary>
        [FhirElement("created", Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime CreatedElement
        {
            get { return _CreatedElement; }
            set { _CreatedElement = value; OnPropertyChanged("CreatedElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _CreatedElement;
        
        /// <summary>
        /// Creation date
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
        /// Insurance issuing organization
        /// </summary>
        [FhirElement("insurer", Order=130)]
        [CLSCompliant(false)]
		[References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Insurer
        {
            get { return _Insurer; }
            set { _Insurer = value; OnPropertyChanged("Insurer"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Insurer;
        
        /// <summary>
        /// Responsible practitioner
        /// </summary>
        [FhirElement("requestProvider", Order=140)]
        [CLSCompliant(false)]
		[References("Practitioner")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference RequestProvider
        {
            get { return _RequestProvider; }
            set { _RequestProvider = value; OnPropertyChanged("RequestProvider"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _RequestProvider;
        
        /// <summary>
        /// Responsible organization
        /// </summary>
        [FhirElement("requestOrganization", Order=150)]
        [CLSCompliant(false)]
		[References("Organization")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference RequestOrganization
        {
            get { return _RequestOrganization; }
            set { _RequestOrganization = value; OnPropertyChanged("RequestOrganization"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _RequestOrganization;
        
        /// <summary>
        /// Id of resource triggering adjudication
        /// </summary>
        [FhirElement("request", Order=160)]
        [CLSCompliant(false)]
		[References("Claim")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Request
        {
            get { return _Request; }
            set { _Request = value; OnPropertyChanged("Request"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Request;
        
        /// <summary>
        /// complete | error | partial
        /// </summary>
        [FhirElement("outcome", Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Outcome
        {
            get { return _Outcome; }
            set { _Outcome = value; OnPropertyChanged("Outcome"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Outcome;
        
        /// <summary>
        /// Disposition Message
        /// </summary>
        [FhirElement("disposition", Order=180)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString DispositionElement
        {
            get { return _DispositionElement; }
            set { _DispositionElement = value; OnPropertyChanged("DispositionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _DispositionElement;
        
        /// <summary>
        /// Disposition Message
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Disposition
        {
            get { return DispositionElement != null ? DispositionElement.Value : null; }
            set
            {
                if (value == null)
                  DispositionElement = null; 
                else
                  DispositionElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Disposition");
            }
        }
        
        /// <summary>
        /// Party to be paid any benefits payable
        /// </summary>
        [FhirElement("payeeType", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept PayeeType
        {
            get { return _PayeeType; }
            set { _PayeeType = value; OnPropertyChanged("PayeeType"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _PayeeType;
        
        /// <summary>
        /// Line items
        /// </summary>
        [FhirElement("item", Order=200)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ClaimResponse.ItemComponent> Item
        {
            get { if(_Item==null) _Item = new List<Hl7.Fhir.Model.ClaimResponse.ItemComponent>(); return _Item; }
            set { _Item = value; OnPropertyChanged("Item"); }
        }
        
        private List<Hl7.Fhir.Model.ClaimResponse.ItemComponent> _Item;
        
        /// <summary>
        /// Insurer added line items
        /// </summary>
        [FhirElement("addItem", Order=210)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ClaimResponse.AddedItemComponent> AddItem
        {
            get { if(_AddItem==null) _AddItem = new List<Hl7.Fhir.Model.ClaimResponse.AddedItemComponent>(); return _AddItem; }
            set { _AddItem = value; OnPropertyChanged("AddItem"); }
        }
        
        private List<Hl7.Fhir.Model.ClaimResponse.AddedItemComponent> _AddItem;
        
        /// <summary>
        /// Processing errors
        /// </summary>
        [FhirElement("error", Order=220)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ClaimResponse.ErrorComponent> Error
        {
            get { if(_Error==null) _Error = new List<Hl7.Fhir.Model.ClaimResponse.ErrorComponent>(); return _Error; }
            set { _Error = value; OnPropertyChanged("Error"); }
        }
        
        private List<Hl7.Fhir.Model.ClaimResponse.ErrorComponent> _Error;
        
        /// <summary>
        /// Total Cost of service from the Claim
        /// </summary>
        [FhirElement("totalCost", Order=230)]
        [DataMember]
        public Money TotalCost
        {
            get { return _TotalCost; }
            set { _TotalCost = value; OnPropertyChanged("TotalCost"); }
        }
        
        private Money _TotalCost;
        
        /// <summary>
        /// Unallocated deductible
        /// </summary>
        [FhirElement("unallocDeductable", Order=240)]
        [DataMember]
        public Money UnallocDeductable
        {
            get { return _UnallocDeductable; }
            set { _UnallocDeductable = value; OnPropertyChanged("UnallocDeductable"); }
        }
        
        private Money _UnallocDeductable;
        
        /// <summary>
        /// Total benefit payable for the Claim
        /// </summary>
        [FhirElement("totalBenefit", Order=250)]
        [DataMember]
        public Money TotalBenefit
        {
            get { return _TotalBenefit; }
            set { _TotalBenefit = value; OnPropertyChanged("TotalBenefit"); }
        }
        
        private Money _TotalBenefit;
        
        /// <summary>
        /// Payment details, if paid
        /// </summary>
        [FhirElement("payment", Order=260)]
        [DataMember]
        public Hl7.Fhir.Model.ClaimResponse.PaymentComponent Payment
        {
            get { return _Payment; }
            set { _Payment = value; OnPropertyChanged("Payment"); }
        }
        
        private Hl7.Fhir.Model.ClaimResponse.PaymentComponent _Payment;
        
        /// <summary>
        /// Funds reserved status
        /// </summary>
        [FhirElement("reserved", Order=270)]
        [DataMember]
        public Hl7.Fhir.Model.Coding Reserved
        {
            get { return _Reserved; }
            set { _Reserved = value; OnPropertyChanged("Reserved"); }
        }
        
        private Hl7.Fhir.Model.Coding _Reserved;
        
        /// <summary>
        /// Printed Form Identifier
        /// </summary>
        [FhirElement("form", Order=280)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Form
        {
            get { return _Form; }
            set { _Form = value; OnPropertyChanged("Form"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Form;
        
        /// <summary>
        /// Processing notes
        /// </summary>
        [FhirElement("processNote", Order=290)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ClaimResponse.NoteComponent> ProcessNote
        {
            get { if(_ProcessNote==null) _ProcessNote = new List<Hl7.Fhir.Model.ClaimResponse.NoteComponent>(); return _ProcessNote; }
            set { _ProcessNote = value; OnPropertyChanged("ProcessNote"); }
        }
        
        private List<Hl7.Fhir.Model.ClaimResponse.NoteComponent> _ProcessNote;
        
        /// <summary>
        /// Request for additional information
        /// </summary>
        [FhirElement("communicationRequest", Order=300)]
        [CLSCompliant(false)]
		[References("CommunicationRequest")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> CommunicationRequest
        {
            get { if(_CommunicationRequest==null) _CommunicationRequest = new List<Hl7.Fhir.Model.ResourceReference>(); return _CommunicationRequest; }
            set { _CommunicationRequest = value; OnPropertyChanged("CommunicationRequest"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _CommunicationRequest;
        
        /// <summary>
        /// Insurance or medical plan
        /// </summary>
        [FhirElement("insurance", Order=310)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ClaimResponse.InsuranceComponent> Insurance
        {
            get { if(_Insurance==null) _Insurance = new List<Hl7.Fhir.Model.ClaimResponse.InsuranceComponent>(); return _Insurance; }
            set { _Insurance = value; OnPropertyChanged("Insurance"); }
        }
        
        private List<Hl7.Fhir.Model.ClaimResponse.InsuranceComponent> _Insurance;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as ClaimResponse;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.FinancialResourceStatusCodes>)StatusElement.DeepCopy();
                if(Patient != null) dest.Patient = (Hl7.Fhir.Model.ResourceReference)Patient.DeepCopy();
                if(CreatedElement != null) dest.CreatedElement = (Hl7.Fhir.Model.FhirDateTime)CreatedElement.DeepCopy();
                if(Insurer != null) dest.Insurer = (Hl7.Fhir.Model.ResourceReference)Insurer.DeepCopy();
                if(RequestProvider != null) dest.RequestProvider = (Hl7.Fhir.Model.ResourceReference)RequestProvider.DeepCopy();
                if(RequestOrganization != null) dest.RequestOrganization = (Hl7.Fhir.Model.ResourceReference)RequestOrganization.DeepCopy();
                if(Request != null) dest.Request = (Hl7.Fhir.Model.ResourceReference)Request.DeepCopy();
                if(Outcome != null) dest.Outcome = (Hl7.Fhir.Model.CodeableConcept)Outcome.DeepCopy();
                if(DispositionElement != null) dest.DispositionElement = (Hl7.Fhir.Model.FhirString)DispositionElement.DeepCopy();
                if(PayeeType != null) dest.PayeeType = (Hl7.Fhir.Model.CodeableConcept)PayeeType.DeepCopy();
                if(Item != null) dest.Item = new List<Hl7.Fhir.Model.ClaimResponse.ItemComponent>(Item.DeepCopy());
                if(AddItem != null) dest.AddItem = new List<Hl7.Fhir.Model.ClaimResponse.AddedItemComponent>(AddItem.DeepCopy());
                if(Error != null) dest.Error = new List<Hl7.Fhir.Model.ClaimResponse.ErrorComponent>(Error.DeepCopy());
                if(TotalCost != null) dest.TotalCost = (Money)TotalCost.DeepCopy();
                if(UnallocDeductable != null) dest.UnallocDeductable = (Money)UnallocDeductable.DeepCopy();
                if(TotalBenefit != null) dest.TotalBenefit = (Money)TotalBenefit.DeepCopy();
                if(Payment != null) dest.Payment = (Hl7.Fhir.Model.ClaimResponse.PaymentComponent)Payment.DeepCopy();
                if(Reserved != null) dest.Reserved = (Hl7.Fhir.Model.Coding)Reserved.DeepCopy();
                if(Form != null) dest.Form = (Hl7.Fhir.Model.CodeableConcept)Form.DeepCopy();
                if(ProcessNote != null) dest.ProcessNote = new List<Hl7.Fhir.Model.ClaimResponse.NoteComponent>(ProcessNote.DeepCopy());
                if(CommunicationRequest != null) dest.CommunicationRequest = new List<Hl7.Fhir.Model.ResourceReference>(CommunicationRequest.DeepCopy());
                if(Insurance != null) dest.Insurance = new List<Hl7.Fhir.Model.ClaimResponse.InsuranceComponent>(Insurance.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new ClaimResponse());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as ClaimResponse;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Patient, otherT.Patient)) return false;
            if( !DeepComparable.Matches(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.Matches(Insurer, otherT.Insurer)) return false;
            if( !DeepComparable.Matches(RequestProvider, otherT.RequestProvider)) return false;
            if( !DeepComparable.Matches(RequestOrganization, otherT.RequestOrganization)) return false;
            if( !DeepComparable.Matches(Request, otherT.Request)) return false;
            if( !DeepComparable.Matches(Outcome, otherT.Outcome)) return false;
            if( !DeepComparable.Matches(DispositionElement, otherT.DispositionElement)) return false;
            if( !DeepComparable.Matches(PayeeType, otherT.PayeeType)) return false;
            if( !DeepComparable.Matches(Item, otherT.Item)) return false;
            if( !DeepComparable.Matches(AddItem, otherT.AddItem)) return false;
            if( !DeepComparable.Matches(Error, otherT.Error)) return false;
            if( !DeepComparable.Matches(TotalCost, otherT.TotalCost)) return false;
            if( !DeepComparable.Matches(UnallocDeductable, otherT.UnallocDeductable)) return false;
            if( !DeepComparable.Matches(TotalBenefit, otherT.TotalBenefit)) return false;
            if( !DeepComparable.Matches(Payment, otherT.Payment)) return false;
            if( !DeepComparable.Matches(Reserved, otherT.Reserved)) return false;
            if( !DeepComparable.Matches(Form, otherT.Form)) return false;
            if( !DeepComparable.Matches(ProcessNote, otherT.ProcessNote)) return false;
            if( !DeepComparable.Matches(CommunicationRequest, otherT.CommunicationRequest)) return false;
            if( !DeepComparable.Matches(Insurance, otherT.Insurance)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as ClaimResponse;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Patient, otherT.Patient)) return false;
            if( !DeepComparable.IsExactly(CreatedElement, otherT.CreatedElement)) return false;
            if( !DeepComparable.IsExactly(Insurer, otherT.Insurer)) return false;
            if( !DeepComparable.IsExactly(RequestProvider, otherT.RequestProvider)) return false;
            if( !DeepComparable.IsExactly(RequestOrganization, otherT.RequestOrganization)) return false;
            if( !DeepComparable.IsExactly(Request, otherT.Request)) return false;
            if( !DeepComparable.IsExactly(Outcome, otherT.Outcome)) return false;
            if( !DeepComparable.IsExactly(DispositionElement, otherT.DispositionElement)) return false;
            if( !DeepComparable.IsExactly(PayeeType, otherT.PayeeType)) return false;
            if( !DeepComparable.IsExactly(Item, otherT.Item)) return false;
            if( !DeepComparable.IsExactly(AddItem, otherT.AddItem)) return false;
            if( !DeepComparable.IsExactly(Error, otherT.Error)) return false;
            if( !DeepComparable.IsExactly(TotalCost, otherT.TotalCost)) return false;
            if( !DeepComparable.IsExactly(UnallocDeductable, otherT.UnallocDeductable)) return false;
            if( !DeepComparable.IsExactly(TotalBenefit, otherT.TotalBenefit)) return false;
            if( !DeepComparable.IsExactly(Payment, otherT.Payment)) return false;
            if( !DeepComparable.IsExactly(Reserved, otherT.Reserved)) return false;
            if( !DeepComparable.IsExactly(Form, otherT.Form)) return false;
            if( !DeepComparable.IsExactly(ProcessNote, otherT.ProcessNote)) return false;
            if( !DeepComparable.IsExactly(CommunicationRequest, otherT.CommunicationRequest)) return false;
            if( !DeepComparable.IsExactly(Insurance, otherT.Insurance)) return false;
            
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
				if (Patient != null) yield return Patient;
				if (CreatedElement != null) yield return CreatedElement;
				if (Insurer != null) yield return Insurer;
				if (RequestProvider != null) yield return RequestProvider;
				if (RequestOrganization != null) yield return RequestOrganization;
				if (Request != null) yield return Request;
				if (Outcome != null) yield return Outcome;
				if (DispositionElement != null) yield return DispositionElement;
				if (PayeeType != null) yield return PayeeType;
				foreach (var elem in Item) { if (elem != null) yield return elem; }
				foreach (var elem in AddItem) { if (elem != null) yield return elem; }
				foreach (var elem in Error) { if (elem != null) yield return elem; }
				if (TotalCost != null) yield return TotalCost;
				if (UnallocDeductable != null) yield return UnallocDeductable;
				if (TotalBenefit != null) yield return TotalBenefit;
				if (Payment != null) yield return Payment;
				if (Reserved != null) yield return Reserved;
				if (Form != null) yield return Form;
				foreach (var elem in ProcessNote) { if (elem != null) yield return elem; }
				foreach (var elem in CommunicationRequest) { if (elem != null) yield return elem; }
				foreach (var elem in Insurance) { if (elem != null) yield return elem; }
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
                if (Patient != null) yield return new ElementValue("patient", Patient);
                if (CreatedElement != null) yield return new ElementValue("created", CreatedElement);
                if (Insurer != null) yield return new ElementValue("insurer", Insurer);
                if (RequestProvider != null) yield return new ElementValue("requestProvider", RequestProvider);
                if (RequestOrganization != null) yield return new ElementValue("requestOrganization", RequestOrganization);
                if (Request != null) yield return new ElementValue("request", Request);
                if (Outcome != null) yield return new ElementValue("outcome", Outcome);
                if (DispositionElement != null) yield return new ElementValue("disposition", DispositionElement);
                if (PayeeType != null) yield return new ElementValue("payeeType", PayeeType);
                foreach (var elem in Item) { if (elem != null) yield return new ElementValue("item", elem); }
                foreach (var elem in AddItem) { if (elem != null) yield return new ElementValue("addItem", elem); }
                foreach (var elem in Error) { if (elem != null) yield return new ElementValue("error", elem); }
                if (TotalCost != null) yield return new ElementValue("totalCost", TotalCost);
                if (UnallocDeductable != null) yield return new ElementValue("unallocDeductable", UnallocDeductable);
                if (TotalBenefit != null) yield return new ElementValue("totalBenefit", TotalBenefit);
                if (Payment != null) yield return new ElementValue("payment", Payment);
                if (Reserved != null) yield return new ElementValue("reserved", Reserved);
                if (Form != null) yield return new ElementValue("form", Form);
                foreach (var elem in ProcessNote) { if (elem != null) yield return new ElementValue("processNote", elem); }
                foreach (var elem in CommunicationRequest) { if (elem != null) yield return new ElementValue("communicationRequest", elem); }
                foreach (var elem in Insurance) { if (elem != null) yield return new ElementValue("insurance", elem); }
            }
        }

    }
    
}
