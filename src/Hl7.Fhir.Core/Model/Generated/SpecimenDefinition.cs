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
// Generated for FHIR v3.3.0
//
namespace Hl7.Fhir.Model
{
    /// <summary>
    /// Kind of specimen
    /// </summary>
    [FhirType("SpecimenDefinition", IsResource=true)]
    [DataContract]
    public partial class SpecimenDefinition : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.SpecimenDefinition; } }
        [NotMapped]
        public override string TypeName { get { return "SpecimenDefinition"; } }
        
        /// <summary>
        /// Degree of preference of a type of conditioned specimen
        /// (url: http://hl7.org/fhir/ValueSet/specimen-contained-preference)
        /// </summary>
        [FhirEnumeration("SpecimenContainedPreference")]
        public enum SpecimenContainedPreference
        {
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/specimen-contained-preference)
            /// </summary>
            [EnumLiteral("preferred", "http://hl7.org/fhir/specimen-contained-preference"), Description("Preferred")]
            Preferred,
            /// <summary>
            /// MISSING DESCRIPTION
            /// (system: http://hl7.org/fhir/specimen-contained-preference)
            /// </summary>
            [EnumLiteral("alternate", "http://hl7.org/fhir/specimen-contained-preference"), Description("Alternate")]
            Alternate,
        }

        [FhirType("SpecimenToLabComponent")]
        [DataContract]
        public partial class SpecimenToLabComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "SpecimenToLabComponent"; } }
            
            /// <summary>
            /// Primary or secondary specimen
            /// </summary>
            [FhirElement("isDerived", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirBoolean IsDerivedElement
            {
                get { return _IsDerivedElement; }
                set { _IsDerivedElement = value; OnPropertyChanged("IsDerivedElement"); }
            }
            
            private Hl7.Fhir.Model.FhirBoolean _IsDerivedElement;
            
            /// <summary>
            /// Primary or secondary specimen
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public bool? IsDerived
            {
                get { return IsDerivedElement != null ? IsDerivedElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        IsDerivedElement = null; 
                    else
                        IsDerivedElement = new Hl7.Fhir.Model.FhirBoolean(value);
                    OnPropertyChanged("IsDerived");
                }
            }
            
            /// <summary>
            /// Type of intended specimen
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
            /// preferred | alternate
            /// </summary>
            [FhirElement("preference", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.SpecimenDefinition.SpecimenContainedPreference> PreferenceElement
            {
                get { return _PreferenceElement; }
                set { _PreferenceElement = value; OnPropertyChanged("PreferenceElement"); }
            }
            
            private Code<Hl7.Fhir.Model.SpecimenDefinition.SpecimenContainedPreference> _PreferenceElement;
            
            /// <summary>
            /// preferred | alternate
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.SpecimenDefinition.SpecimenContainedPreference? Preference
            {
                get { return PreferenceElement != null ? PreferenceElement.Value : null; }
                set
                {
                    if (!value.HasValue)
                        PreferenceElement = null; 
                    else
                        PreferenceElement = new Code<Hl7.Fhir.Model.SpecimenDefinition.SpecimenContainedPreference>(value);
                    OnPropertyChanged("Preference");
                }
            }
            
            /// <summary>
            /// Container material
            /// </summary>
            [FhirElement("containerMaterial", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept ContainerMaterial
            {
                get { return _ContainerMaterial; }
                set { _ContainerMaterial = value; OnPropertyChanged("ContainerMaterial"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _ContainerMaterial;
            
            /// <summary>
            /// Kind of container associated with the kind of specimen
            /// </summary>
            [FhirElement("containerType", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept ContainerType
            {
                get { return _ContainerType; }
                set { _ContainerType = value; OnPropertyChanged("ContainerType"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _ContainerType;
            
            /// <summary>
            /// Color of container cap
            /// </summary>
            [FhirElement("containerCap", Order=90)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept ContainerCap
            {
                get { return _ContainerCap; }
                set { _ContainerCap = value; OnPropertyChanged("ContainerCap"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _ContainerCap;
            
            /// <summary>
            /// Container description
            /// </summary>
            [FhirElement("containerDescription", Order=100)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ContainerDescriptionElement
            {
                get { return _ContainerDescriptionElement; }
                set { _ContainerDescriptionElement = value; OnPropertyChanged("ContainerDescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ContainerDescriptionElement;
            
            /// <summary>
            /// Container description
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ContainerDescription
            {
                get { return ContainerDescriptionElement != null ? ContainerDescriptionElement.Value : null; }
                set
                {
                    if (value == null)
                        ContainerDescriptionElement = null; 
                    else
                        ContainerDescriptionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("ContainerDescription");
                }
            }
            
            /// <summary>
            /// Container capacity
            /// </summary>
            [FhirElement("containerCapacity", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity ContainerCapacity
            {
                get { return _ContainerCapacity; }
                set { _ContainerCapacity = value; OnPropertyChanged("ContainerCapacity"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _ContainerCapacity;
            
            /// <summary>
            /// Minimum volume
            /// </summary>
            [FhirElement("containerMinimumVolume", Order=120)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity ContainerMinimumVolume
            {
                get { return _ContainerMinimumVolume; }
                set { _ContainerMinimumVolume = value; OnPropertyChanged("ContainerMinimumVolume"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _ContainerMinimumVolume;
            
            /// <summary>
            /// Additive associated with container
            /// </summary>
            [FhirElement("containerAdditive", Order=130)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.SpecimenDefinition.ContainerAdditiveComponent> ContainerAdditive
            {
                get { if(_ContainerAdditive==null) _ContainerAdditive = new List<Hl7.Fhir.Model.SpecimenDefinition.ContainerAdditiveComponent>(); return _ContainerAdditive; }
                set { _ContainerAdditive = value; OnPropertyChanged("ContainerAdditive"); }
            }
            
            private List<Hl7.Fhir.Model.SpecimenDefinition.ContainerAdditiveComponent> _ContainerAdditive;
            
            /// <summary>
            /// Specimen container preparation
            /// </summary>
            [FhirElement("containerPreparation", Order=140)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ContainerPreparationElement
            {
                get { return _ContainerPreparationElement; }
                set { _ContainerPreparationElement = value; OnPropertyChanged("ContainerPreparationElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ContainerPreparationElement;
            
            /// <summary>
            /// Specimen container preparation
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ContainerPreparation
            {
                get { return ContainerPreparationElement != null ? ContainerPreparationElement.Value : null; }
                set
                {
                    if (value == null)
                        ContainerPreparationElement = null; 
                    else
                        ContainerPreparationElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("ContainerPreparation");
                }
            }
            
            /// <summary>
            /// Specimen requirements
            /// </summary>
            [FhirElement("requirement", Order=150)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString RequirementElement
            {
                get { return _RequirementElement; }
                set { _RequirementElement = value; OnPropertyChanged("RequirementElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _RequirementElement;
            
            /// <summary>
            /// Specimen requirements
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Requirement
            {
                get { return RequirementElement != null ? RequirementElement.Value : null; }
                set
                {
                    if (value == null)
                        RequirementElement = null; 
                    else
                        RequirementElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Requirement");
                }
            }
            
            /// <summary>
            /// Specimen retention time
            /// </summary>
            [FhirElement("retentionTime", Order=160)]
            [DataMember]
            public Duration RetentionTime
            {
                get { return _RetentionTime; }
                set { _RetentionTime = value; OnPropertyChanged("RetentionTime"); }
            }
            
            private Duration _RetentionTime;
            
            /// <summary>
            /// Rejection criterion
            /// </summary>
            [FhirElement("rejectionCriterion", Order=170)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> RejectionCriterion
            {
                get { if(_RejectionCriterion==null) _RejectionCriterion = new List<Hl7.Fhir.Model.CodeableConcept>(); return _RejectionCriterion; }
                set { _RejectionCriterion = value; OnPropertyChanged("RejectionCriterion"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _RejectionCriterion;
            
            /// <summary>
            /// Specimen handling before testing
            /// </summary>
            [FhirElement("handling", Order=180)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.SpecimenDefinition.HandlingComponent> Handling
            {
                get { if(_Handling==null) _Handling = new List<Hl7.Fhir.Model.SpecimenDefinition.HandlingComponent>(); return _Handling; }
                set { _Handling = value; OnPropertyChanged("Handling"); }
            }
            
            private List<Hl7.Fhir.Model.SpecimenDefinition.HandlingComponent> _Handling;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SpecimenToLabComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(IsDerivedElement != null) dest.IsDerivedElement = (Hl7.Fhir.Model.FhirBoolean)IsDerivedElement.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(PreferenceElement != null) dest.PreferenceElement = (Code<Hl7.Fhir.Model.SpecimenDefinition.SpecimenContainedPreference>)PreferenceElement.DeepCopy();
                    if(ContainerMaterial != null) dest.ContainerMaterial = (Hl7.Fhir.Model.CodeableConcept)ContainerMaterial.DeepCopy();
                    if(ContainerType != null) dest.ContainerType = (Hl7.Fhir.Model.CodeableConcept)ContainerType.DeepCopy();
                    if(ContainerCap != null) dest.ContainerCap = (Hl7.Fhir.Model.CodeableConcept)ContainerCap.DeepCopy();
                    if(ContainerDescriptionElement != null) dest.ContainerDescriptionElement = (Hl7.Fhir.Model.FhirString)ContainerDescriptionElement.DeepCopy();
                    if(ContainerCapacity != null) dest.ContainerCapacity = (Hl7.Fhir.Model.SimpleQuantity)ContainerCapacity.DeepCopy();
                    if(ContainerMinimumVolume != null) dest.ContainerMinimumVolume = (Hl7.Fhir.Model.SimpleQuantity)ContainerMinimumVolume.DeepCopy();
                    if(ContainerAdditive != null) dest.ContainerAdditive = new List<Hl7.Fhir.Model.SpecimenDefinition.ContainerAdditiveComponent>(ContainerAdditive.DeepCopy());
                    if(ContainerPreparationElement != null) dest.ContainerPreparationElement = (Hl7.Fhir.Model.FhirString)ContainerPreparationElement.DeepCopy();
                    if(RequirementElement != null) dest.RequirementElement = (Hl7.Fhir.Model.FhirString)RequirementElement.DeepCopy();
                    if(RetentionTime != null) dest.RetentionTime = (Duration)RetentionTime.DeepCopy();
                    if(RejectionCriterion != null) dest.RejectionCriterion = new List<Hl7.Fhir.Model.CodeableConcept>(RejectionCriterion.DeepCopy());
                    if(Handling != null) dest.Handling = new List<Hl7.Fhir.Model.SpecimenDefinition.HandlingComponent>(Handling.DeepCopy());
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new SpecimenToLabComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SpecimenToLabComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(IsDerivedElement, otherT.IsDerivedElement)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(PreferenceElement, otherT.PreferenceElement)) return false;
                if( !DeepComparable.Matches(ContainerMaterial, otherT.ContainerMaterial)) return false;
                if( !DeepComparable.Matches(ContainerType, otherT.ContainerType)) return false;
                if( !DeepComparable.Matches(ContainerCap, otherT.ContainerCap)) return false;
                if( !DeepComparable.Matches(ContainerDescriptionElement, otherT.ContainerDescriptionElement)) return false;
                if( !DeepComparable.Matches(ContainerCapacity, otherT.ContainerCapacity)) return false;
                if( !DeepComparable.Matches(ContainerMinimumVolume, otherT.ContainerMinimumVolume)) return false;
                if( !DeepComparable.Matches(ContainerAdditive, otherT.ContainerAdditive)) return false;
                if( !DeepComparable.Matches(ContainerPreparationElement, otherT.ContainerPreparationElement)) return false;
                if( !DeepComparable.Matches(RequirementElement, otherT.RequirementElement)) return false;
                if( !DeepComparable.Matches(RetentionTime, otherT.RetentionTime)) return false;
                if( !DeepComparable.Matches(RejectionCriterion, otherT.RejectionCriterion)) return false;
                if( !DeepComparable.Matches(Handling, otherT.Handling)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SpecimenToLabComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(IsDerivedElement, otherT.IsDerivedElement)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(PreferenceElement, otherT.PreferenceElement)) return false;
                if( !DeepComparable.IsExactly(ContainerMaterial, otherT.ContainerMaterial)) return false;
                if( !DeepComparable.IsExactly(ContainerType, otherT.ContainerType)) return false;
                if( !DeepComparable.IsExactly(ContainerCap, otherT.ContainerCap)) return false;
                if( !DeepComparable.IsExactly(ContainerDescriptionElement, otherT.ContainerDescriptionElement)) return false;
                if( !DeepComparable.IsExactly(ContainerCapacity, otherT.ContainerCapacity)) return false;
                if( !DeepComparable.IsExactly(ContainerMinimumVolume, otherT.ContainerMinimumVolume)) return false;
                if( !DeepComparable.IsExactly(ContainerAdditive, otherT.ContainerAdditive)) return false;
                if( !DeepComparable.IsExactly(ContainerPreparationElement, otherT.ContainerPreparationElement)) return false;
                if( !DeepComparable.IsExactly(RequirementElement, otherT.RequirementElement)) return false;
                if( !DeepComparable.IsExactly(RetentionTime, otherT.RetentionTime)) return false;
                if( !DeepComparable.IsExactly(RejectionCriterion, otherT.RejectionCriterion)) return false;
                if( !DeepComparable.IsExactly(Handling, otherT.Handling)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (IsDerivedElement != null) yield return IsDerivedElement;
                    if (Type != null) yield return Type;
                    if (PreferenceElement != null) yield return PreferenceElement;
                    if (ContainerMaterial != null) yield return ContainerMaterial;
                    if (ContainerType != null) yield return ContainerType;
                    if (ContainerCap != null) yield return ContainerCap;
                    if (ContainerDescriptionElement != null) yield return ContainerDescriptionElement;
                    if (ContainerCapacity != null) yield return ContainerCapacity;
                    if (ContainerMinimumVolume != null) yield return ContainerMinimumVolume;
                    foreach (var elem in ContainerAdditive) { if (elem != null) yield return elem; }
                    if (ContainerPreparationElement != null) yield return ContainerPreparationElement;
                    if (RequirementElement != null) yield return RequirementElement;
                    if (RetentionTime != null) yield return RetentionTime;
                    foreach (var elem in RejectionCriterion) { if (elem != null) yield return elem; }
                    foreach (var elem in Handling) { if (elem != null) yield return elem; }
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (IsDerivedElement != null) yield return new ElementValue("isDerived", false, IsDerivedElement);
                    if (Type != null) yield return new ElementValue("type", false, Type);
                    if (PreferenceElement != null) yield return new ElementValue("preference", false, PreferenceElement);
                    if (ContainerMaterial != null) yield return new ElementValue("containerMaterial", false, ContainerMaterial);
                    if (ContainerType != null) yield return new ElementValue("containerType", false, ContainerType);
                    if (ContainerCap != null) yield return new ElementValue("containerCap", false, ContainerCap);
                    if (ContainerDescriptionElement != null) yield return new ElementValue("containerDescription", false, ContainerDescriptionElement);
                    if (ContainerCapacity != null) yield return new ElementValue("containerCapacity", false, ContainerCapacity);
                    if (ContainerMinimumVolume != null) yield return new ElementValue("containerMinimumVolume", false, ContainerMinimumVolume);
                    foreach (var elem in ContainerAdditive) { if (elem != null) yield return new ElementValue("containerAdditive", true, elem); }
                    if (ContainerPreparationElement != null) yield return new ElementValue("containerPreparation", false, ContainerPreparationElement);
                    if (RequirementElement != null) yield return new ElementValue("requirement", false, RequirementElement);
                    if (RetentionTime != null) yield return new ElementValue("retentionTime", false, RetentionTime);
                    foreach (var elem in RejectionCriterion) { if (elem != null) yield return new ElementValue("rejectionCriterion", true, elem); }
                    foreach (var elem in Handling) { if (elem != null) yield return new ElementValue("handling", true, elem); }
                }
            }

            
        }
        
        
        [FhirType("ContainerAdditiveComponent")]
        [DataContract]
        public partial class ContainerAdditiveComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ContainerAdditiveComponent"; } }
            
            /// <summary>
            /// Additive associated with container
            /// </summary>
            [FhirElement("additive", Order=40, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Additive
            {
                get { return _Additive; }
                set { _Additive = value; OnPropertyChanged("Additive"); }
            }
            
            private Hl7.Fhir.Model.Element _Additive;
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ContainerAdditiveComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Additive != null) dest.Additive = (Hl7.Fhir.Model.Element)Additive.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ContainerAdditiveComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ContainerAdditiveComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Additive, otherT.Additive)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ContainerAdditiveComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Additive, otherT.Additive)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Additive != null) yield return Additive;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Additive != null) yield return new ElementValue("additive", false, Additive);
                }
            }

            
        }
        
        
        [FhirType("HandlingComponent")]
        [DataContract]
        public partial class HandlingComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "HandlingComponent"; } }
            
            /// <summary>
            /// Conservation condition set
            /// </summary>
            [FhirElement("conditionSet", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept ConditionSet
            {
                get { return _ConditionSet; }
                set { _ConditionSet = value; OnPropertyChanged("ConditionSet"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _ConditionSet;
            
            /// <summary>
            /// Temperature range
            /// </summary>
            [FhirElement("tempRange", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Range TempRange
            {
                get { return _TempRange; }
                set { _TempRange = value; OnPropertyChanged("TempRange"); }
            }
            
            private Hl7.Fhir.Model.Range _TempRange;
            
            /// <summary>
            /// Maximum conservation time
            /// </summary>
            [FhirElement("maxDuration", Order=60)]
            [DataMember]
            public Duration MaxDuration
            {
                get { return _MaxDuration; }
                set { _MaxDuration = value; OnPropertyChanged("MaxDuration"); }
            }
            
            private Duration _MaxDuration;
            
            /// <summary>
            /// Light exposure
            /// </summary>
            [FhirElement("lightExposure", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString LightExposureElement
            {
                get { return _LightExposureElement; }
                set { _LightExposureElement = value; OnPropertyChanged("LightExposureElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _LightExposureElement;
            
            /// <summary>
            /// Light exposure
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string LightExposure
            {
                get { return LightExposureElement != null ? LightExposureElement.Value : null; }
                set
                {
                    if (value == null)
                        LightExposureElement = null; 
                    else
                        LightExposureElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("LightExposure");
                }
            }
            
            /// <summary>
            /// Conservation instruction
            /// </summary>
            [FhirElement("instruction", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString InstructionElement
            {
                get { return _InstructionElement; }
                set { _InstructionElement = value; OnPropertyChanged("InstructionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _InstructionElement;
            
            /// <summary>
            /// Conservation instruction
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Instruction
            {
                get { return InstructionElement != null ? InstructionElement.Value : null; }
                set
                {
                    if (value == null)
                        InstructionElement = null; 
                    else
                        InstructionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Instruction");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as HandlingComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(ConditionSet != null) dest.ConditionSet = (Hl7.Fhir.Model.CodeableConcept)ConditionSet.DeepCopy();
                    if(TempRange != null) dest.TempRange = (Hl7.Fhir.Model.Range)TempRange.DeepCopy();
                    if(MaxDuration != null) dest.MaxDuration = (Duration)MaxDuration.DeepCopy();
                    if(LightExposureElement != null) dest.LightExposureElement = (Hl7.Fhir.Model.FhirString)LightExposureElement.DeepCopy();
                    if(InstructionElement != null) dest.InstructionElement = (Hl7.Fhir.Model.FhirString)InstructionElement.DeepCopy();
                    return dest;
                }
                else
                	throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new HandlingComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as HandlingComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(ConditionSet, otherT.ConditionSet)) return false;
                if( !DeepComparable.Matches(TempRange, otherT.TempRange)) return false;
                if( !DeepComparable.Matches(MaxDuration, otherT.MaxDuration)) return false;
                if( !DeepComparable.Matches(LightExposureElement, otherT.LightExposureElement)) return false;
                if( !DeepComparable.Matches(InstructionElement, otherT.InstructionElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as HandlingComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(ConditionSet, otherT.ConditionSet)) return false;
                if( !DeepComparable.IsExactly(TempRange, otherT.TempRange)) return false;
                if( !DeepComparable.IsExactly(MaxDuration, otherT.MaxDuration)) return false;
                if( !DeepComparable.IsExactly(LightExposureElement, otherT.LightExposureElement)) return false;
                if( !DeepComparable.IsExactly(InstructionElement, otherT.InstructionElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (ConditionSet != null) yield return ConditionSet;
                    if (TempRange != null) yield return TempRange;
                    if (MaxDuration != null) yield return MaxDuration;
                    if (LightExposureElement != null) yield return LightExposureElement;
                    if (InstructionElement != null) yield return InstructionElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (ConditionSet != null) yield return new ElementValue("conditionSet", false, ConditionSet);
                    if (TempRange != null) yield return new ElementValue("tempRange", false, TempRange);
                    if (MaxDuration != null) yield return new ElementValue("maxDuration", false, MaxDuration);
                    if (LightExposureElement != null) yield return new ElementValue("lightExposure", false, LightExposureElement);
                    if (InstructionElement != null) yield return new ElementValue("instruction", false, InstructionElement);
                }
            }

            
        }
        
        
        /// <summary>
        /// Business identifier of a kind of specimen
        /// </summary>
        [FhirElement("identifier", InSummary=true, Order=90)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private Hl7.Fhir.Model.Identifier _Identifier;
        
        /// <summary>
        /// Kind of material to collect
        /// </summary>
        [FhirElement("typeCollected", InSummary=true, Order=100)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept TypeCollected
        {
            get { return _TypeCollected; }
            set { _TypeCollected = value; OnPropertyChanged("TypeCollected"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _TypeCollected;
        
        /// <summary>
        /// Patient preparation for collection
        /// </summary>
        [FhirElement("patientPreparation", Order=110)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString PatientPreparationElement
        {
            get { return _PatientPreparationElement; }
            set { _PatientPreparationElement = value; OnPropertyChanged("PatientPreparationElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _PatientPreparationElement;
        
        /// <summary>
        /// Patient preparation for collection
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string PatientPreparation
        {
            get { return PatientPreparationElement != null ? PatientPreparationElement.Value : null; }
            set
            {
                if (value == null)
                  PatientPreparationElement = null; 
                else
                  PatientPreparationElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("PatientPreparation");
            }
        }
        
        /// <summary>
        /// Time aspect for collection
        /// </summary>
        [FhirElement("timeAspect", InSummary=true, Order=120)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString TimeAspectElement
        {
            get { return _TimeAspectElement; }
            set { _TimeAspectElement = value; OnPropertyChanged("TimeAspectElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _TimeAspectElement;
        
        /// <summary>
        /// Time aspect for collection
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string TimeAspect
        {
            get { return TimeAspectElement != null ? TimeAspectElement.Value : null; }
            set
            {
                if (value == null)
                  TimeAspectElement = null; 
                else
                  TimeAspectElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("TimeAspect");
            }
        }
        
        /// <summary>
        /// Specimen collection procedure
        /// </summary>
        [FhirElement("collection", InSummary=true, Order=130)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Collection
        {
            get { if(_Collection==null) _Collection = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Collection; }
            set { _Collection = value; OnPropertyChanged("Collection"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Collection;
        
        /// <summary>
        /// Specimen in container intended for testing by lab
        /// </summary>
        [FhirElement("specimenToLab", Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.SpecimenDefinition.SpecimenToLabComponent> SpecimenToLab
        {
            get { if(_SpecimenToLab==null) _SpecimenToLab = new List<Hl7.Fhir.Model.SpecimenDefinition.SpecimenToLabComponent>(); return _SpecimenToLab; }
            set { _SpecimenToLab = value; OnPropertyChanged("SpecimenToLab"); }
        }
        
        private List<Hl7.Fhir.Model.SpecimenDefinition.SpecimenToLabComponent> _SpecimenToLab;
        

        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();

        }

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as SpecimenDefinition;
            
            if (dest != null)
            {
                base.CopyTo(dest);
                if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                if(TypeCollected != null) dest.TypeCollected = (Hl7.Fhir.Model.CodeableConcept)TypeCollected.DeepCopy();
                if(PatientPreparationElement != null) dest.PatientPreparationElement = (Hl7.Fhir.Model.FhirString)PatientPreparationElement.DeepCopy();
                if(TimeAspectElement != null) dest.TimeAspectElement = (Hl7.Fhir.Model.FhirString)TimeAspectElement.DeepCopy();
                if(Collection != null) dest.Collection = new List<Hl7.Fhir.Model.CodeableConcept>(Collection.DeepCopy());
                if(SpecimenToLab != null) dest.SpecimenToLab = new List<Hl7.Fhir.Model.SpecimenDefinition.SpecimenToLabComponent>(SpecimenToLab.DeepCopy());
                return dest;
            }
            else
            	throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new SpecimenDefinition());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as SpecimenDefinition;
            if(otherT == null) return false;
            
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(TypeCollected, otherT.TypeCollected)) return false;
            if( !DeepComparable.Matches(PatientPreparationElement, otherT.PatientPreparationElement)) return false;
            if( !DeepComparable.Matches(TimeAspectElement, otherT.TimeAspectElement)) return false;
            if( !DeepComparable.Matches(Collection, otherT.Collection)) return false;
            if( !DeepComparable.Matches(SpecimenToLab, otherT.SpecimenToLab)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as SpecimenDefinition;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(TypeCollected, otherT.TypeCollected)) return false;
            if( !DeepComparable.IsExactly(PatientPreparationElement, otherT.PatientPreparationElement)) return false;
            if( !DeepComparable.IsExactly(TimeAspectElement, otherT.TimeAspectElement)) return false;
            if( !DeepComparable.IsExactly(Collection, otherT.Collection)) return false;
            if( !DeepComparable.IsExactly(SpecimenToLab, otherT.SpecimenToLab)) return false;
            
            return true;
        }

        [NotMapped]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
				if (Identifier != null) yield return Identifier;
				if (TypeCollected != null) yield return TypeCollected;
				if (PatientPreparationElement != null) yield return PatientPreparationElement;
				if (TimeAspectElement != null) yield return TimeAspectElement;
				foreach (var elem in Collection) { if (elem != null) yield return elem; }
				foreach (var elem in SpecimenToLab) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (Identifier != null) yield return new ElementValue("identifier", false, Identifier);
                if (TypeCollected != null) yield return new ElementValue("typeCollected", false, TypeCollected);
                if (PatientPreparationElement != null) yield return new ElementValue("patientPreparation", false, PatientPreparationElement);
                if (TimeAspectElement != null) yield return new ElementValue("timeAspect", false, TimeAspectElement);
                foreach (var elem in Collection) { if (elem != null) yield return new ElementValue("collection", true, elem); }
                foreach (var elem in SpecimenToLab) { if (elem != null) yield return new ElementValue("specimenToLab", true, elem); }
            }
        }

    }
    
}
