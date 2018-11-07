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
    /// Measurements and simple assertions
    /// </summary>
    [FhirType("Observation", IsResource=true)]
    [DataContract]
    public partial class Observation : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Observation; } }
        [NotMapped]
        public override string TypeName { get { return "Observation"; } }
        
        /// <summary>
        /// Codes specifying how two observations are related.
        /// (url: http://hl7.org/fhir/ValueSet/observation-relationshiptypes)
        /// </summary>
        [FhirEnumeration("ObservationRelationshipType")]
        public enum ObservationRelationshipType
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/observation-relationshiptypes)
            /// </summary>
            [EnumLiteral("has-member", "http://hl7.org/fhir/observation-relationshiptypes"), Description("Has Member")]
            HasMember,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/observation-relationshiptypes)
            /// </summary>
            [EnumLiteral("derived-from", "http://hl7.org/fhir/observation-relationshiptypes"), Description("Derived From")]
            DerivedFrom,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/observation-relationshiptypes)
            /// </summary>
            [EnumLiteral("sequel-to", "http://hl7.org/fhir/observation-relationshiptypes"), Description("Sequel To")]
            SequelTo,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/observation-relationshiptypes)
            /// </summary>
            [EnumLiteral("replaces", "http://hl7.org/fhir/observation-relationshiptypes"), Description("Replaces")]
            Replaces,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/observation-relationshiptypes)
            /// </summary>
            [EnumLiteral("qualified-by", "http://hl7.org/fhir/observation-relationshiptypes"), Description("Qualified By")]
            QualifiedBy,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/observation-relationshiptypes)
            /// </summary>
            [EnumLiteral("interfered-by", "http://hl7.org/fhir/observation-relationshiptypes"), Description("Interfered By")]
            InterferedBy,
        }

        [FhirType("ReferenceRangeComponent")]
        [DataContract]
        public partial class ReferenceRangeComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ReferenceRangeComponent"; } }
            
            /// <summary>
            /// Low Range, if relevant
            /// </summary>
            [FhirElement("low", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Low
            {
                get { return _Low; }
                set { _Low = value; OnPropertyChanged("Low"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _Low;
            
            /// <summary>
            /// High Range, if relevant
            /// </summary>
            [FhirElement("high", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity High
            {
                get { return _High; }
                set { _High = value; OnPropertyChanged("High"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _High;
            
            /// <summary>
            /// Reference range qualifier
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
            /// Reference range population
            /// </summary>
            [FhirElement("appliesTo", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> AppliesTo
            {
                get { if(_AppliesTo==null) _AppliesTo = new List<Hl7.Fhir.Model.CodeableConcept>(); return _AppliesTo; }
                set { _AppliesTo = value; OnPropertyChanged("AppliesTo"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _AppliesTo;
            
            /// <summary>
            /// Applicable age range, if relevant
            /// </summary>
            [FhirElement("age", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.Range Age
            {
                get { return _Age; }
                set { _Age = value; OnPropertyChanged("Age"); }
            }
            
            private Hl7.Fhir.Model.Range _Age;
            
            /// <summary>
            /// Text based reference range in an observation
            /// </summary>
            [FhirElement("text", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString TextElement
            {
                get { return _TextElement; }
                set { _TextElement = value; OnPropertyChanged("TextElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _TextElement;
            
            /// <summary>
            /// Text based reference range in an observation
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
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ReferenceRangeComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Low != null) dest.Low = (Hl7.Fhir.Model.SimpleQuantity)Low.DeepCopy();
                    if(High != null) dest.High = (Hl7.Fhir.Model.SimpleQuantity)High.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(AppliesTo != null) dest.AppliesTo = new List<Hl7.Fhir.Model.CodeableConcept>(AppliesTo.DeepCopy());
                    if(Age != null) dest.Age = (Hl7.Fhir.Model.Range)Age.DeepCopy();
                    if(TextElement != null) dest.TextElement = (Hl7.Fhir.Model.FhirString)TextElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ReferenceRangeComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ReferenceRangeComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Low, otherT.Low)) return false;
                if( !DeepComparable.Matches(High, otherT.High)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(AppliesTo, otherT.AppliesTo)) return false;
                if( !DeepComparable.Matches(Age, otherT.Age)) return false;
                if( !DeepComparable.Matches(TextElement, otherT.TextElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ReferenceRangeComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Low, otherT.Low)) return false;
                if( !DeepComparable.IsExactly(High, otherT.High)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(AppliesTo, otherT.AppliesTo)) return false;
                if( !DeepComparable.IsExactly(Age, otherT.Age)) return false;
                if( !DeepComparable.IsExactly(TextElement, otherT.TextElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Low != null) yield return Low;
                    if (High != null) yield return High;
                    if (Type != null) yield return Type;
                    foreach (var elem in AppliesTo) { if (elem != null) yield return elem; }
                    if (Age != null) yield return Age;
                    if (TextElement != null) yield return TextElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Low != null) yield return new ElementValue("low", Low);
                    if (High != null) yield return new ElementValue("high", High);
                    if (Type != null) yield return new ElementValue("type", Type);
                    foreach (var elem in AppliesTo) { if (elem != null) yield return new ElementValue("appliesTo", elem); }
                    if (Age != null) yield return new ElementValue("age", Age);
                    if (TextElement != null) yield return new ElementValue("text", TextElement);
                }
            }

            
        }
        
        
        [FhirType("RelatedComponent")]
        [DataContract]
        public partial class RelatedComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "RelatedComponent"; } }
            
            /// <summary>
            /// has-member | derived-from | sequel-to | replaces | qualified-by | interfered-by
            /// </summary>
            [FhirElement("type", Order=40)]
            [DataMember]
            public Code<Hl7.Fhir.Model.Observation.ObservationRelationshipType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.Observation.ObservationRelationshipType> _TypeElement;
            
            /// <summary>
            /// has-member | derived-from | sequel-to | replaces | qualified-by | interfered-by
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.Observation.ObservationRelationshipType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        TypeElement = null; 
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.Observation.ObservationRelationshipType>(value);
                    OnPropertyChanged("Type");
                }
            }
            
            /// <summary>
            /// Resource that is related to this one
            /// </summary>
            [FhirElement("target", Order=50)]
            [CLSCompliant(false)]
			[References("Observation","QuestionnaireResponse","Sequence")]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.ResourceReference Target
            {
                get { return _Target; }
                set { _Target = value; OnPropertyChanged("Target"); }
            }
            
            private Hl7.Fhir.Model.ResourceReference _Target;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as RelatedComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.Observation.ObservationRelationshipType>)TypeElement.DeepCopy();
                    if(Target != null) dest.Target = (Hl7.Fhir.Model.ResourceReference)Target.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new RelatedComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as RelatedComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.Matches(Target, otherT.Target)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as RelatedComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
                if( !DeepComparable.IsExactly(Target, otherT.Target)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (TypeElement != null) yield return TypeElement;
                    if (Target != null) yield return Target;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                    if (Target != null) yield return new ElementValue("target", Target);
                }
            }

            
        }
        
        
        [FhirType("ComponentComponent")]
        [DataContract]
        public partial class ComponentComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IBackboneElement
        {
            [NotMapped]
            public override string TypeName { get { return "ComponentComponent"; } }
            
            /// <summary>
            /// Type of component observation (code / type)
            /// </summary>
            [FhirElement("code", InSummary=true, Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// Actual component result
            /// </summary>
            [FhirElement("value", InSummary=true, Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Quantity),typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.SampledData),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
            [DataMember]
            public Hl7.Fhir.Model.Element Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.Element _Value;
            
            /// <summary>
            /// Why the component result is missing
            /// </summary>
            [FhirElement("dataAbsentReason", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept DataAbsentReason
            {
                get { return _DataAbsentReason; }
                set { _DataAbsentReason = value; OnPropertyChanged("DataAbsentReason"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _DataAbsentReason;
            
            /// <summary>
            /// High, low, normal, etc.
            /// </summary>
            [FhirElement("interpretation", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Interpretation
            {
                get { return _Interpretation; }
                set { _Interpretation = value; OnPropertyChanged("Interpretation"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Interpretation;
            
            /// <summary>
            /// Provides guide for interpretation of component result
            /// </summary>
            [FhirElement("referenceRange", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Observation.ReferenceRangeComponent> ReferenceRange
            {
                get { if(_ReferenceRange==null) _ReferenceRange = new List<Hl7.Fhir.Model.Observation.ReferenceRangeComponent>(); return _ReferenceRange; }
                set { _ReferenceRange = value; OnPropertyChanged("ReferenceRange"); }
            }
            
            private List<Hl7.Fhir.Model.Observation.ReferenceRangeComponent> _ReferenceRange;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ComponentComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
                    if(DataAbsentReason != null) dest.DataAbsentReason = (Hl7.Fhir.Model.CodeableConcept)DataAbsentReason.DeepCopy();
                    if(Interpretation != null) dest.Interpretation = (Hl7.Fhir.Model.CodeableConcept)Interpretation.DeepCopy();
                    if(ReferenceRange != null) dest.ReferenceRange = new List<Hl7.Fhir.Model.Observation.ReferenceRangeComponent>(ReferenceRange.DeepCopy());
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
                if( !DeepComparable.Matches(DataAbsentReason, otherT.DataAbsentReason)) return false;
                if( !DeepComparable.Matches(Interpretation, otherT.Interpretation)) return false;
                if( !DeepComparable.Matches(ReferenceRange, otherT.ReferenceRange)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ComponentComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
                if( !DeepComparable.IsExactly(DataAbsentReason, otherT.DataAbsentReason)) return false;
                if( !DeepComparable.IsExactly(Interpretation, otherT.Interpretation)) return false;
                if( !DeepComparable.IsExactly(ReferenceRange, otherT.ReferenceRange)) return false;
                
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
                    if (DataAbsentReason != null) yield return DataAbsentReason;
                    if (Interpretation != null) yield return Interpretation;
                    foreach (var elem in ReferenceRange) { if (elem != null) yield return elem; }
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
                    if (DataAbsentReason != null) yield return new ElementValue("dataAbsentReason", DataAbsentReason);
                    if (Interpretation != null) yield return new ElementValue("interpretation", Interpretation);
                    foreach (var elem in ReferenceRange) { if (elem != null) yield return new ElementValue("referenceRange", elem); }
                }
            }

            
        }
        
        
        /// <summary>
        /// Business Identifier for observation
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
        /// Fulfills plan, proposal or order
        /// </summary>
        [FhirElement("basedOn", InSummary=true, Order=100)]
        [CLSCompliant(false)]
		[References("CarePlan","DeviceRequest","ImmunizationRecommendation","MedicationRequest","NutritionOrder","ProcedureRequest","ReferralRequest")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> BasedOn
        {
            get { if(_BasedOn==null) _BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(); return _BasedOn; }
            set { _BasedOn = value; OnPropertyChanged("BasedOn"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _BasedOn;
        
        /// <summary>
        /// registered | preliminary | final | amended +
        /// </summary>
        [FhirElement("status", InSummary=true, Order=110)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.ObservationStatus> StatusElement
        {
            get { return _StatusElement; }
            set { _StatusElement = value; OnPropertyChanged("StatusElement"); }
        }
        
        private Code<Hl7.Fhir.Model.ObservationStatus> _StatusElement;
        
        /// <summary>
        /// registered | preliminary | final | amended +
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.ObservationStatus? Status
        {
            get { return StatusElement != null ? StatusElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  StatusElement = null; 
                else
                  StatusElement = new Code<Hl7.Fhir.Model.ObservationStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// Classification of  type of observation
        /// </summary>
        [FhirElement("category", Order=120)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Category
        {
            get { if(_Category==null) _Category = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Category; }
            set { _Category = value; OnPropertyChanged("Category"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Category;
        
        /// <summary>
        /// Type of observation (code / type)
        /// </summary>
        [FhirElement("code", InSummary=true, Order=130)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Code
        {
            get { return _Code; }
            set { _Code = value; OnPropertyChanged("Code"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Code;
        
        /// <summary>
        /// Who and/or what this is about
        /// </summary>
        [FhirElement("subject", InSummary=true, Order=140)]
        [CLSCompliant(false)]
		[References("Patient","Group","Device","Location")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Subject;
        
        /// <summary>
        /// Healthcare event during which this observation is made
        /// </summary>
        [FhirElement("context", Order=150)]
        [CLSCompliant(false)]
		[References("Encounter","EpisodeOfCare")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Context
        {
            get { return _Context; }
            set { _Context = value; OnPropertyChanged("Context"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Context;
        
        /// <summary>
        /// Clinically relevant time/time-period for observation
        /// </summary>
        [FhirElement("effective", InSummary=true, Order=160, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
        [DataMember]
        public Hl7.Fhir.Model.Element Effective
        {
            get { return _Effective; }
            set { _Effective = value; OnPropertyChanged("Effective"); }
        }
        
        private Hl7.Fhir.Model.Element _Effective;
        
        /// <summary>
        /// Date/Time this was made available
        /// </summary>
        [FhirElement("issued", InSummary=true, Order=170)]
        [DataMember]
        public Hl7.Fhir.Model.Instant IssuedElement
        {
            get { return _IssuedElement; }
            set { _IssuedElement = value; OnPropertyChanged("IssuedElement"); }
        }
        
        private Hl7.Fhir.Model.Instant _IssuedElement;
        
        /// <summary>
        /// Date/Time this was made available
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public DateTimeOffset? Issued
        {
            get { return IssuedElement != null ? IssuedElement.Value : null; }
            set
            {
                if (!value.HasValue)
                  IssuedElement = null; 
                else
                  IssuedElement = new Hl7.Fhir.Model.Instant(value);
                OnPropertyChanged("Issued");
            }
        }
        
        /// <summary>
        /// Who is responsible for the observation
        /// </summary>
        [FhirElement("performer", InSummary=true, Order=180)]
        [CLSCompliant(false)]
		[References("Practitioner","Organization","Patient","RelatedPerson")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Performer
        {
            get { if(_Performer==null) _Performer = new List<Hl7.Fhir.Model.ResourceReference>(); return _Performer; }
            set { _Performer = value; OnPropertyChanged("Performer"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Performer;
        
        /// <summary>
        /// Actual result
        /// </summary>
        [FhirElement("value", InSummary=true, Order=190, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
		[AllowedTypes(typeof(Quantity),typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.Range),typeof(Hl7.Fhir.Model.Ratio),typeof(Hl7.Fhir.Model.SampledData),typeof(Hl7.Fhir.Model.Attachment),typeof(Hl7.Fhir.Model.Time),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period))]
        [DataMember]
        public Hl7.Fhir.Model.Element Value
        {
            get { return _Value; }
            set { _Value = value; OnPropertyChanged("Value"); }
        }
        
        private Hl7.Fhir.Model.Element _Value;
        
        /// <summary>
        /// Why the result is missing
        /// </summary>
        [FhirElement("dataAbsentReason", Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept DataAbsentReason
        {
            get { return _DataAbsentReason; }
            set { _DataAbsentReason = value; OnPropertyChanged("DataAbsentReason"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _DataAbsentReason;
        
        /// <summary>
        /// High, low, normal, etc.
        /// </summary>
        [FhirElement("interpretation", Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Interpretation
        {
            get { return _Interpretation; }
            set { _Interpretation = value; OnPropertyChanged("Interpretation"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Interpretation;
        
        /// <summary>
        /// Comments about result
        /// </summary>
        [FhirElement("comment", Order=220)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString CommentElement
        {
            get { return _CommentElement; }
            set { _CommentElement = value; OnPropertyChanged("CommentElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _CommentElement;
        
        /// <summary>
        /// Comments about result
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Comment
        {
            get { return CommentElement != null ? CommentElement.Value : null; }
            set
            {
                if (value == null)
                  CommentElement = null; 
                else
                  CommentElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Comment");
            }
        }
        
        /// <summary>
        /// Observed body part
        /// </summary>
        [FhirElement("bodySite", Order=230)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept BodySite
        {
            get { return _BodySite; }
            set { _BodySite = value; OnPropertyChanged("BodySite"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _BodySite;
        
        /// <summary>
        /// How it was done
        /// </summary>
        [FhirElement("method", Order=240)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Method
        {
            get { return _Method; }
            set { _Method = value; OnPropertyChanged("Method"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Method;
        
        /// <summary>
        /// Specimen used for this observation
        /// </summary>
        [FhirElement("specimen", Order=250)]
        [CLSCompliant(false)]
		[References("Specimen")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Specimen
        {
            get { return _Specimen; }
            set { _Specimen = value; OnPropertyChanged("Specimen"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Specimen;
        
        /// <summary>
        /// (Measurement) Device
        /// </summary>
        [FhirElement("device", Order=260)]
        [CLSCompliant(false)]
		[References("Device","DeviceMetric")]
        [DataMember]
        public Hl7.Fhir.Model.ResourceReference Device
        {
            get { return _Device; }
            set { _Device = value; OnPropertyChanged("Device"); }
        }
        
        private Hl7.Fhir.Model.ResourceReference _Device;
        
        /// <summary>
        /// Provides guide for interpretation
        /// </summary>
        [FhirElement("referenceRange", Order=270)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Observation.ReferenceRangeComponent> ReferenceRange
        {
            get { if(_ReferenceRange==null) _ReferenceRange = new List<Hl7.Fhir.Model.Observation.ReferenceRangeComponent>(); return _ReferenceRange; }
            set { _ReferenceRange = value; OnPropertyChanged("ReferenceRange"); }
        }
        
        private List<Hl7.Fhir.Model.Observation.ReferenceRangeComponent> _ReferenceRange;
        
        /// <summary>
        /// Resource related to this observation
        /// </summary>
        [FhirElement("related", InSummary=true, Order=280)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Observation.RelatedComponent> Related
        {
            get { if(_Related==null) _Related = new List<Hl7.Fhir.Model.Observation.RelatedComponent>(); return _Related; }
            set { _Related = value; OnPropertyChanged("Related"); }
        }
        
        private List<Hl7.Fhir.Model.Observation.RelatedComponent> _Related;
        
        /// <summary>
        /// Component results
        /// </summary>
        [FhirElement("component", InSummary=true, Order=290)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Observation.ComponentComponent> Component
        {
            get { if(_Component==null) _Component = new List<Hl7.Fhir.Model.Observation.ComponentComponent>(); return _Component; }
            set { _Component = value; OnPropertyChanged("Component"); }
        }
        
        private List<Hl7.Fhir.Model.Observation.ComponentComponent> _Component;
        

        public static ElementDefinition.ConstraintComponent Observation_OBS_7 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "value.empty() or code!=component.code",
            Key = "obs-7",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "If code is the same as a component code then the value element associated with the code SHALL NOT be present",
            Xpath = "not(exists(f:*[starts-with(local-name(.), 'value')])) or not(count(for $coding in f:code/f:coding return parent::*/f:component/f:code/f:coding[f:code/@value=$coding/f:code/@value and f:system/@value=$coding/f:system/@value])=0)"
        };

        public static ElementDefinition.ConstraintComponent Observation_OBS_6 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "dataAbsentReason.empty() or value.empty()",
            Key = "obs-6",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "dataAbsentReason SHALL only be present if Observation.value[x] is not present",
            Xpath = "not(exists(f:dataAbsentReason)) or (not(exists(*[starts-with(local-name(.), 'value')])))"
        };

        public static ElementDefinition.ConstraintComponent Observation_OBS_3 = new ElementDefinition.ConstraintComponent()
        {
            Expression = "referenceRange.all(low.exists() or high.exists() or text.exists())",
            Key = "obs-3",
            Severity = ElementDefinition.ConstraintSeverity.Warning,
            Human = "Must have at least a low or a high or text",
            Xpath = "(exists(f:low) or exists(f:high)or exists(f:text))"
        };

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

            InvariantConstraints.Add(Observation_OBS_7);
            InvariantConstraints.Add(Observation_OBS_6);
            InvariantConstraints.Add(Observation_OBS_3);
        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Observation;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(BasedOn != null) dest.BasedOn = new List<Hl7.Fhir.Model.ResourceReference>(BasedOn.DeepCopy());
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.ObservationStatus>)StatusElement.DeepCopy();
                if(Category != null) dest.Category = new List<Hl7.Fhir.Model.CodeableConcept>(Category.DeepCopy());
                if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.ResourceReference)Subject.DeepCopy();
                if(Context != null) dest.Context = (Hl7.Fhir.Model.ResourceReference)Context.DeepCopy();
                if(Effective != null) dest.Effective = (Hl7.Fhir.Model.Element)Effective.DeepCopy();
                if(IssuedElement != null) dest.IssuedElement = (Hl7.Fhir.Model.Instant)IssuedElement.DeepCopy();
                if(Performer != null) dest.Performer = new List<Hl7.Fhir.Model.ResourceReference>(Performer.DeepCopy());
                if(Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
                if(DataAbsentReason != null) dest.DataAbsentReason = (Hl7.Fhir.Model.CodeableConcept)DataAbsentReason.DeepCopy();
                if(Interpretation != null) dest.Interpretation = (Hl7.Fhir.Model.CodeableConcept)Interpretation.DeepCopy();
                if(CommentElement != null) dest.CommentElement = (Hl7.Fhir.Model.FhirString)CommentElement.DeepCopy();
                if(BodySite != null) dest.BodySite = (Hl7.Fhir.Model.CodeableConcept)BodySite.DeepCopy();
                if(Method != null) dest.Method = (Hl7.Fhir.Model.CodeableConcept)Method.DeepCopy();
                if(Specimen != null) dest.Specimen = (Hl7.Fhir.Model.ResourceReference)Specimen.DeepCopy();
                if(Device != null) dest.Device = (Hl7.Fhir.Model.ResourceReference)Device.DeepCopy();
                if(ReferenceRange != null) dest.ReferenceRange = new List<Hl7.Fhir.Model.Observation.ReferenceRangeComponent>(ReferenceRange.DeepCopy());
                if(Related != null) dest.Related = new List<Hl7.Fhir.Model.Observation.RelatedComponent>(Related.DeepCopy());
                if(Component != null) dest.Component = new List<Hl7.Fhir.Model.Observation.ComponentComponent>(Component.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new Observation());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Observation;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(Category, otherT.Category)) return false;
            if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(Context, otherT.Context)) return false;
            if( !DeepComparable.Matches(Effective, otherT.Effective)) return false;
            if( !DeepComparable.Matches(IssuedElement, otherT.IssuedElement)) return false;
            if( !DeepComparable.Matches(Performer, otherT.Performer)) return false;
            if( !DeepComparable.Matches(Value, otherT.Value)) return false;
            if( !DeepComparable.Matches(DataAbsentReason, otherT.DataAbsentReason)) return false;
            if( !DeepComparable.Matches(Interpretation, otherT.Interpretation)) return false;
            if( !DeepComparable.Matches(CommentElement, otherT.CommentElement)) return false;
            if( !DeepComparable.Matches(BodySite, otherT.BodySite)) return false;
            if( !DeepComparable.Matches(Method, otherT.Method)) return false;
            if( !DeepComparable.Matches(Specimen, otherT.Specimen)) return false;
            if( !DeepComparable.Matches(Device, otherT.Device)) return false;
            if( !DeepComparable.Matches(ReferenceRange, otherT.ReferenceRange)) return false;
            if( !DeepComparable.Matches(Related, otherT.Related)) return false;
            if( !DeepComparable.Matches(Component, otherT.Component)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Observation;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(BasedOn, otherT.BasedOn)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(Category, otherT.Category)) return false;
            if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(Context, otherT.Context)) return false;
            if( !DeepComparable.IsExactly(Effective, otherT.Effective)) return false;
            if( !DeepComparable.IsExactly(IssuedElement, otherT.IssuedElement)) return false;
            if( !DeepComparable.IsExactly(Performer, otherT.Performer)) return false;
            if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
            if( !DeepComparable.IsExactly(DataAbsentReason, otherT.DataAbsentReason)) return false;
            if( !DeepComparable.IsExactly(Interpretation, otherT.Interpretation)) return false;
            if( !DeepComparable.IsExactly(CommentElement, otherT.CommentElement)) return false;
            if( !DeepComparable.IsExactly(BodySite, otherT.BodySite)) return false;
            if( !DeepComparable.IsExactly(Method, otherT.Method)) return false;
            if( !DeepComparable.IsExactly(Specimen, otherT.Specimen)) return false;
            if( !DeepComparable.IsExactly(Device, otherT.Device)) return false;
            if( !DeepComparable.IsExactly(ReferenceRange, otherT.ReferenceRange)) return false;
            if( !DeepComparable.IsExactly(Related, otherT.Related)) return false;
            if( !DeepComparable.IsExactly(Component, otherT.Component)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				foreach (var elem in Identifier) { if (elem != null) yield return elem; }
				foreach (var elem in BasedOn) { if (elem != null) yield return elem; }
				if (StatusElement != null) yield return StatusElement;
				foreach (var elem in Category) { if (elem != null) yield return elem; }
				if (Code != null) yield return Code;
				if (Subject != null) yield return Subject;
				if (Context != null) yield return Context;
				if (Effective != null) yield return Effective;
				if (IssuedElement != null) yield return IssuedElement;
				foreach (var elem in Performer) { if (elem != null) yield return elem; }
				if (Value != null) yield return Value;
				if (DataAbsentReason != null) yield return DataAbsentReason;
				if (Interpretation != null) yield return Interpretation;
				if (CommentElement != null) yield return CommentElement;
				if (BodySite != null) yield return BodySite;
				if (Method != null) yield return Method;
				if (Specimen != null) yield return Specimen;
				if (Device != null) yield return Device;
				foreach (var elem in ReferenceRange) { if (elem != null) yield return elem; }
				foreach (var elem in Related) { if (elem != null) yield return elem; }
				foreach (var elem in Component) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                foreach (var elem in Identifier) { if (elem != null) yield return new ElementValue("identifier", elem); }
                foreach (var elem in BasedOn) { if (elem != null) yield return new ElementValue("basedOn", elem); }
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                foreach (var elem in Category) { if (elem != null) yield return new ElementValue("category", elem); }
                if (Code != null) yield return new ElementValue("code", Code);
                if (Subject != null) yield return new ElementValue("subject", Subject);
                if (Context != null) yield return new ElementValue("context", Context);
                if (Effective != null) yield return new ElementValue("effective", Effective);
                if (IssuedElement != null) yield return new ElementValue("issued", IssuedElement);
                foreach (var elem in Performer) { if (elem != null) yield return new ElementValue("performer", elem); }
                if (Value != null) yield return new ElementValue("value", Value);
                if (DataAbsentReason != null) yield return new ElementValue("dataAbsentReason", DataAbsentReason);
                if (Interpretation != null) yield return new ElementValue("interpretation", Interpretation);
                if (CommentElement != null) yield return new ElementValue("comment", CommentElement);
                if (BodySite != null) yield return new ElementValue("bodySite", BodySite);
                if (Method != null) yield return new ElementValue("method", Method);
                if (Specimen != null) yield return new ElementValue("specimen", Specimen);
                if (Device != null) yield return new ElementValue("device", Device);
                foreach (var elem in ReferenceRange) { if (elem != null) yield return new ElementValue("referenceRange", elem); }
                foreach (var elem in Related) { if (elem != null) yield return new ElementValue("related", elem); }
                foreach (var elem in Component) { if (elem != null) yield return new ElementValue("component", elem); }
            }
        }

    }
    
}
