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
// Generated for FHIR v1.0.2
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Sample for analysis
    /// </summary>
    [FhirType("Specimen", IsResource=true)]
    [DataContract]
    public partial class Specimen : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Specimen; } }
        [NotMapped]
        public override string TypeName { get { return "Specimen"; } }
        
        /// <summary>
        /// Codes providing the status/availability of a specimen.
        /// (url: http://hl7.org/fhir/ValueSet/specimen-status)
        /// </summary>
        [FhirEnumeration("SpecimenStatus")]
        public enum SpecimenStatus
        {
            /// <summary>
            /// The physical specimen is present and in good condition.
            /// (system: http://hl7.org/fhir/specimen-status)
            /// </summary>
            [EnumLiteral("available", "http://hl7.org/fhir/specimen-status"), Description("Available")]
            Available,
            /// <summary>
            /// There is no physical specimen because it is either lost, destroyed or consumed.
            /// (system: http://hl7.org/fhir/specimen-status)
            /// </summary>
            [EnumLiteral("unavailable", "http://hl7.org/fhir/specimen-status"), Description("Unavailable")]
            Unavailable,
            /// <summary>
            /// The specimen cannot be used because of a quality issue such as a broken container, contamination, or too old.
            /// (system: http://hl7.org/fhir/specimen-status)
            /// </summary>
            [EnumLiteral("unsatisfactory", "http://hl7.org/fhir/specimen-status"), Description("Unsatisfactory")]
            Unsatisfactory,
            /// <summary>
            /// The specimen was entered in error and therefore nullified.
            /// (system: http://hl7.org/fhir/specimen-status)
            /// </summary>
            [EnumLiteral("entered-in-error", "http://hl7.org/fhir/specimen-status"), Description("Entered-in-error")]
            EnteredInError,
        }

        [FhirType("CollectionComponent")]
        [DataContract]
        public partial class CollectionComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "CollectionComponent"; } }
            
            /// <summary>
            /// Who collected the specimen
            /// </summary>
            [FhirElement("collector", InSummary=true, Order=40)]
            [CLSCompliant(false)]
			[References("Practitioner")]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Collector
            {
                get { return _Collector; }
                set { _Collector = value; OnPropertyChanged("Collector"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Collector;
            
            /// <summary>
            /// Collector comments
            /// </summary>
            [FhirElement("comment", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.FhirString> CommentElement
            {
                get { if(_CommentElement==null) _CommentElement = new List<Hl7.Fhir.Model.FhirString>(); return _CommentElement; }
                set { _CommentElement = value; OnPropertyChanged("CommentElement"); }
            }
            
            private List<Hl7.Fhir.Model.FhirString> _CommentElement;
            
            /// <summary>
            /// Collector comments
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<string> Comment
            {
                get { return CommentElement != null ? CommentElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        CommentElement = null; 
                    else
                        CommentElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                    OnPropertyChanged("Comment");
                }
            }
            
            /// <summary>
            /// Collection time
            /// </summary>
            [FhirElement("collected", InSummary=true, Order=60, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
            [DataMember]
            public Hl7.Fhir.Model.Element Collected
            {
                get { return _Collected; }
                set { _Collected = value; OnPropertyChanged("Collected"); }
            }
            
            private Hl7.Fhir.Model.Element _Collected;
            
            /// <summary>
            /// The quantity of specimen collected
            /// </summary>
            [FhirElement("quantity", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Quantity
            {
                get { return _Quantity; }
                set { _Quantity = value; OnPropertyChanged("Quantity"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _Quantity;
            
            /// <summary>
            /// Technique used to perform collection
            /// </summary>
            [FhirElement("method", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Method
            {
                get { return _Method; }
                set { _Method = value; OnPropertyChanged("Method"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Method;
            
            /// <summary>
            /// Anatomical collection site
            /// </summary>
            [FhirElement("bodySite", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept BodySite
            {
                get { return _BodySite; }
                set { _BodySite = value; OnPropertyChanged("BodySite"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _BodySite;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CollectionComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Collector != null) dest.Collector = (Hl7.Fhir.Model.ResourceReference)Collector.DeepCopy();
                    if(CommentElement != null) dest.CommentElement = new List<Hl7.Fhir.Model.FhirString>(CommentElement.DeepCopy());
                    if(Collected != null) dest.Collected = (Hl7.Fhir.Model.Element)Collected.DeepCopy();
                    if(Quantity != null) dest.Quantity = (Hl7.Fhir.Model.SimpleQuantity)Quantity.DeepCopy();
                    if(Method != null) dest.Method = (Hl7.Fhir.Model.CodeableConcept)Method.DeepCopy();
                    if(BodySite != null) dest.BodySite = (Hl7.Fhir.Model.CodeableConcept)BodySite.DeepCopy();
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
                if( !DeepComparable.Matches(CommentElement, otherT.CommentElement)) return false;
                if( !DeepComparable.Matches(Collected, otherT.Collected)) return false;
                if( !DeepComparable.Matches(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.Matches(Method, otherT.Method)) return false;
                if( !DeepComparable.Matches(BodySite, otherT.BodySite)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CollectionComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Collector, otherT.Collector)) return false;
                if( !DeepComparable.IsExactly(CommentElement, otherT.CommentElement)) return false;
                if( !DeepComparable.IsExactly(Collected, otherT.Collected)) return false;
                if( !DeepComparable.IsExactly(Quantity, otherT.Quantity)) return false;
                if( !DeepComparable.IsExactly(Method, otherT.Method)) return false;
                if( !DeepComparable.IsExactly(BodySite, otherT.BodySite)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Collector != null) yield return Collector;
                    foreach (var elem in CommentElement) { if (elem != null) yield return elem; }
                    if (Collected != null) yield return Collected;
                    if (Quantity != null) yield return Quantity;
                    if (Method != null) yield return Method;
                    if (BodySite != null) yield return BodySite;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Collector != null) yield return new ElementValue("collector", Collector);
                    foreach (var elem in CommentElement) { if (elem != null) yield return new ElementValue("comment", elem); }
                    if (Collected != null) yield return new ElementValue("collected", Collected);
                    if (Quantity != null) yield return new ElementValue("quantity", Quantity);
                    if (Method != null) yield return new ElementValue("method", Method);
                    if (BodySite != null) yield return new ElementValue("bodySite", BodySite);
                }
            }

            
        }
        
        
        [FhirType("TreatmentComponent")]
        [DataContract]
        public partial class TreatmentComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "TreatmentComponent"; } }
            
            /// <summary>
            /// Textual description of procedure
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
            /// Textual description of procedure
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
            /// Indicates the treatment or processing step  applied to the specimen
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
            /// Material used in the processing step
            /// </summary>
            [FhirElement("additive", Order=60)]
            [CLSCompliant(false)]
			[References("Substance")]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.ResourceReference> Additive
            {
                get { if(_Additive==null) _Additive = new List<Hl7.Fhir.Model.ResourceReference>(); return _Additive; }
                set { _Additive = value; OnPropertyChanged("Additive"); }
            }
            
            private List<Hl7.Fhir.Model.ResourceReference> _Additive;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TreatmentComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Procedure != null) dest.Procedure = (Hl7.Fhir.Model.CodeableConcept)Procedure.DeepCopy();
                    if(Additive != null) dest.Additive = new List<Hl7.Fhir.Model.ResourceReference>(Additive.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new TreatmentComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TreatmentComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(Procedure, otherT.Procedure)) return false;
                if( !DeepComparable.Matches(Additive, otherT.Additive)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TreatmentComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Procedure, otherT.Procedure)) return false;
                if( !DeepComparable.IsExactly(Additive, otherT.Additive)) return false;
                
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
                    foreach (var elem in Additive) { if (elem != null) yield return elem; }
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
                    foreach (var elem in Additive) { if (elem != null) yield return new ElementValue("additive", elem); }
                }
            }

            
        }
        
        
        [FhirType("ContainerComponent")]
        [DataContract]
        public partial class ContainerComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ContainerComponent"; } }
            
            /// <summary>
            /// Id for the container
            /// </summary>
            [FhirElement("identifier", InSummary=true, Order=40)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Identifier> Identifier
            {
                get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private List<Hl7.Fhir.Model.Identifier> _Identifier;
            
            /// <summary>
            /// Textual description of the container
            /// </summary>
            [FhirElement("description", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Textual description of the container
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
            /// Kind of container directly associated with specimen
            /// </summary>
            [FhirElement("type", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Type
            {
                get { return _Type; }
                set { _Type = value; OnPropertyChanged("Type"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Type;
            
            /// <summary>
            /// Container volume or size
            /// </summary>
            [FhirElement("capacity", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Capacity
            {
                get { return _Capacity; }
                set { _Capacity = value; OnPropertyChanged("Capacity"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _Capacity;
            
            /// <summary>
            /// Quantity of specimen within container
            /// </summary>
            [FhirElement("specimenQuantity", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity SpecimenQuantity
            {
                get { return _SpecimenQuantity; }
                set { _SpecimenQuantity = value; OnPropertyChanged("SpecimenQuantity"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _SpecimenQuantity;
            
            /// <summary>
            /// Additive associated with container
            /// </summary>
            [FhirElement("additive", Order=90, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
            [DataMember]
            public Hl7.Fhir.Model.Element Additive
            {
                get { return _Additive; }
                set { _Additive = value; OnPropertyChanged("Additive"); }
            }
            
            private Hl7.Fhir.Model.Element _Additive;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ContainerComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Capacity != null) dest.Capacity = (Hl7.Fhir.Model.SimpleQuantity)Capacity.DeepCopy();
                    if(SpecimenQuantity != null) dest.SpecimenQuantity = (Hl7.Fhir.Model.SimpleQuantity)SpecimenQuantity.DeepCopy();
                    if(Additive != null) dest.Additive = (Hl7.Fhir.Model.Element)Additive.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ContainerComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ContainerComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Capacity, otherT.Capacity)) return false;
                if( !DeepComparable.Matches(SpecimenQuantity, otherT.SpecimenQuantity)) return false;
                if( !DeepComparable.Matches(Additive, otherT.Additive)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ContainerComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Capacity, otherT.Capacity)) return false;
                if( !DeepComparable.IsExactly(SpecimenQuantity, otherT.SpecimenQuantity)) return false;
                if( !DeepComparable.IsExactly(Additive, otherT.Additive)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (Type != null) yield return Type;
                    if (Capacity != null) yield return Capacity;
                    if (SpecimenQuantity != null) yield return SpecimenQuantity;
                    if (Additive != null) yield return Additive;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Capacity != null) yield return new ElementValue("capacity", Capacity);
                    if (SpecimenQuantity != null) yield return new ElementValue("specimenQuantity", SpecimenQuantity);
                    if (Additive != null) yield return new ElementValue("additive", Additive);
                }
            }

            
        }
        
        
        /// <summary>
        /// External Identifier
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
        /// available | unavailable | unsatisfactory | entered-in-error
        /// </summary>
        [FhirElement("status", InSummary=true, Order=100)]
        [DataMember]
        public Code<Hl7.Fhir.Model.Specimen.SpecimenStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.Specimen.SpecimenStatus> _StatusElement;
        
        /// <summary>
        /// available | unavailable | unsatisfactory | entered-in-error
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.Specimen.SpecimenStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.Specimen.SpecimenStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Kind of material that forms the specimen
        /// </summary>
        [FhirElement("type", InSummary=true, Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Type;
        
        /// <summary>
        /// Specimen from which this specimen originated
        /// </summary>
        [FhirElement("parent", Order=120)]
        [CLSCompliant(false)]
		[References("Specimen")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Parent
        {
            get { if(_Parent==null) _Parent = new List<Hl7.Fhir.Model.ResourceReference>(); return _Parent; }
            set { _Parent = value; OnPropertyChanged("Parent"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Parent;
        
        /// <summary>
        /// Where the specimen came from. This may be from the patient(s) or from the environment or a device
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=130)]
        [CLSCompliant(false)]
		[References("Patient","Group","Device","Substance")]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Identifier assigned by the lab
        /// </summary>
        [FhirElement("accessionIdentifier", InSummary=true, Order=140)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier AccessionIdentifier
        {
            get { return _AccessionIdentifier; }
            set { _AccessionIdentifier = value; OnPropertyChanged("AccessionIdentifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _AccessionIdentifier;
        
        /// <summary>
        /// The time when specimen was received for processing
        /// </summary>
        [FhirElement("receivedTime", InSummary=true, Order=150)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime ReceivedTimeElement
        {
            get { return _ReceivedTimeElement; }
            set { _ReceivedTimeElement = value; OnPropertyChanged("ReceivedTimeElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _ReceivedTimeElement;
        
        /// <summary>
        /// The time when specimen was received for processing
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ReceivedTime
        {
            get { return ReceivedTimeElement != null ? ReceivedTimeElement.Value : null; }
            set
            {
                if (value == null)
                  ReceivedTimeElement = null; 
                else
                  ReceivedTimeElement = new Hl7.Fhir.Model.FhirDateTime(value);
                OnPropertyChanged("ReceivedTime");
            }
        }
        
        /// <summary>
        /// Collection details
        /// </summary>
        [FhirElement("collection", Order=160)]
        [DataMember]
        public Hl7.Fhir.Model.Specimen.CollectionComponent Collection
        {
            get { return _Collection; }
            set { _Collection = value; OnPropertyChanged("Collection"); }
        }
        
        private Hl7.Fhir.Model.Specimen.CollectionComponent _Collection;
        
        /// <summary>
        /// Treatment and processing step details
        /// </summary>
        [FhirElement("treatment", Order=170)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Specimen.TreatmentComponent> Treatment
        {
            get { if(_Treatment==null) _Treatment = new List<Hl7.Fhir.Model.Specimen.TreatmentComponent>(); return _Treatment; }
            set { _Treatment = value; OnPropertyChanged("Treatment"); }
        }
        
        private List<Hl7.Fhir.Model.Specimen.TreatmentComponent> _Treatment;
        
        /// <summary>
        /// Direct container of specimen (tube/slide, etc.)
        /// </summary>
        [FhirElement("container", Order=180)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Specimen.ContainerComponent> Container
        {
            get { if(_Container==null) _Container = new List<Hl7.Fhir.Model.Specimen.ContainerComponent>(); return _Container; }
            set { _Container = value; OnPropertyChanged("Container"); }
        }
        
        private List<Hl7.Fhir.Model.Specimen.ContainerComponent> _Container;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Specimen;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.Specimen.SpecimenStatus>)StatusElement.DeepCopy();
                if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                if(Parent != null) dest.Parent = new List<Hl7.Fhir.Model.ResourceReference>(Parent.DeepCopy());
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(AccessionIdentifier != null) dest.AccessionIdentifier = (Hl7.Fhir.Model.Identifier)AccessionIdentifier.DeepCopy();
                if(ReceivedTimeElement != null) dest.ReceivedTimeElement = (Hl7.Fhir.Model.FhirDateTime)ReceivedTimeElement.DeepCopy();
                if(Collection != null) dest.Collection = (Hl7.Fhir.Model.Specimen.CollectionComponent)Collection.DeepCopy();
                if(Treatment != null) dest.Treatment = new List<Hl7.Fhir.Model.Specimen.TreatmentComponent>(Treatment.DeepCopy());
                if(Container != null) dest.Container = new List<Hl7.Fhir.Model.Specimen.ContainerComponent>(Container.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Specimen());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Specimen;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(Parent, otherT.Parent)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(AccessionIdentifier, otherT.AccessionIdentifier)) return false;
            if( !DeepComparable.Matches(ReceivedTimeElement, otherT.ReceivedTimeElement)) return false;
            if( !DeepComparable.Matches(Collection, otherT.Collection)) return false;
            if( !DeepComparable.Matches(Treatment, otherT.Treatment)) return false;
            if( !DeepComparable.Matches(Container, otherT.Container)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Specimen;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(Parent, otherT.Parent)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(AccessionIdentifier, otherT.AccessionIdentifier)) return false;
            if( !DeepComparable.IsExactly(ReceivedTimeElement, otherT.ReceivedTimeElement)) return false;
            if( !DeepComparable.IsExactly(Collection, otherT.Collection)) return false;
            if( !DeepComparable.IsExactly(Treatment, otherT.Treatment)) return false;
            if( !DeepComparable.IsExactly(Container, otherT.Container)) return false;
            
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
				if (Type != null) yield return Type;
				foreach (var elem in Parent) { if (elem != null) yield return elem; }
				if (Subject != null) yield return Subject;
				if (AccessionIdentifier != null) yield return AccessionIdentifier;
				if (ReceivedTimeElement != null) yield return ReceivedTimeElement;
				if (Collection != null) yield return Collection;
				foreach (var elem in Treatment) { if (elem != null) yield return elem; }
				foreach (var elem in Container) { if (elem != null) yield return elem; }
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
                if (Type != null) yield return new ElementValue("type", Type);
                foreach (var elem in Parent) { if (elem != null) yield return new ElementValue("parent", elem); }
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (AccessionIdentifier != null) yield return new ElementValue("accessionIdentifier", AccessionIdentifier);
                if (ReceivedTimeElement != null) yield return new ElementValue("receivedTime", ReceivedTimeElement);
                if (Collection != null) yield return new ElementValue("collection", Collection);
                foreach (var elem in Treatment) { if (elem != null) yield return new ElementValue("treatment", elem); }
                foreach (var elem in Container) { if (elem != null) yield return new ElementValue("container", elem); }
            }
        }

    }
    
}
