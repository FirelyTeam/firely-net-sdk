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
        /// Degree of preference of a type of conditioned specimen.
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

        [FhirType("TypeTestedComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class TypeTestedComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "TypeTestedComponent"; } }
            
            /// <summary>
            /// Primary or secondary specimen
            /// </summary>
            [FhirElement("isDerived", Order=40)]
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
            /// The specimen's container
            /// </summary>
            [FhirElement("container", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.SpecimenDefinition.ContainerComponent Container
            {
                get { return _Container; }
                set { _Container = value; OnPropertyChanged("Container"); }
            }
            
            private Hl7.Fhir.Model.SpecimenDefinition.ContainerComponent _Container;
            
            /// <summary>
            /// Specimen requirements
            /// </summary>
            [FhirElement("requirement", Order=80)]
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
            [FhirElement("retentionTime", Order=90)]
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
            [FhirElement("rejectionCriterion", Order=100)]
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
            [FhirElement("handling", Order=110)]
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
                var dest = other as TypeTestedComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(IsDerivedElement != null) dest.IsDerivedElement = (Hl7.Fhir.Model.FhirBoolean)IsDerivedElement.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(PreferenceElement != null) dest.PreferenceElement = (Code<Hl7.Fhir.Model.SpecimenDefinition.SpecimenContainedPreference>)PreferenceElement.DeepCopy();
                    if(Container != null) dest.Container = (Hl7.Fhir.Model.SpecimenDefinition.ContainerComponent)Container.DeepCopy();
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
                return CopyTo(new TypeTestedComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as TypeTestedComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(IsDerivedElement, otherT.IsDerivedElement)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(PreferenceElement, otherT.PreferenceElement)) return false;
                if( !DeepComparable.Matches(Container, otherT.Container)) return false;
                if( !DeepComparable.Matches(RequirementElement, otherT.RequirementElement)) return false;
                if( !DeepComparable.Matches(RetentionTime, otherT.RetentionTime)) return false;
                if( !DeepComparable.Matches(RejectionCriterion, otherT.RejectionCriterion)) return false;
                if( !DeepComparable.Matches(Handling, otherT.Handling)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as TypeTestedComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(IsDerivedElement, otherT.IsDerivedElement)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(PreferenceElement, otherT.PreferenceElement)) return false;
                if( !DeepComparable.IsExactly(Container, otherT.Container)) return false;
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
                    if (Container != null) yield return Container;
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
                    if (IsDerivedElement != null) yield return new ElementValue("isDerived", IsDerivedElement);
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (PreferenceElement != null) yield return new ElementValue("preference", PreferenceElement);
                    if (Container != null) yield return new ElementValue("container", Container);
                    if (RequirementElement != null) yield return new ElementValue("requirement", RequirementElement);
                    if (RetentionTime != null) yield return new ElementValue("retentionTime", RetentionTime);
                    foreach (var elem in RejectionCriterion) { if (elem != null) yield return new ElementValue("rejectionCriterion", elem); }
                    foreach (var elem in Handling) { if (elem != null) yield return new ElementValue("handling", elem); }
                }
            }

            
        }
        
        
        [FhirType("ContainerComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class ContainerComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "ContainerComponent"; } }
            
            /// <summary>
            /// Container material
            /// </summary>
            [FhirElement("material", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Material
            {
                get { return _Material; }
                set { _Material = value; OnPropertyChanged("Material"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Material;
            
            /// <summary>
            /// Kind of container associated with the kind of specimen
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
            /// Color of container cap
            /// </summary>
            [FhirElement("cap", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Cap
            {
                get { return _Cap; }
                set { _Cap = value; OnPropertyChanged("Cap"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Cap;
            
            /// <summary>
            /// Container description
            /// </summary>
            [FhirElement("description", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Container description
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
            /// Container capacity
            /// </summary>
            [FhirElement("capacity", Order=80)]
            [DataMember]
            public Hl7.Fhir.Model.SimpleQuantity Capacity
            {
                get { return _Capacity; }
                set { _Capacity = value; OnPropertyChanged("Capacity"); }
            }
            
            private Hl7.Fhir.Model.SimpleQuantity _Capacity;
            
            /// <summary>
            /// Minimum volume
            /// </summary>
            [FhirElement("minimumVolume", Order=90, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
			[AllowedTypes(typeof(Hl7.Fhir.Model.SimpleQuantity),typeof(Hl7.Fhir.Model.FhirString))]
            [DataMember]
            public Hl7.Fhir.Model.Element MinimumVolume
            {
                get { return _MinimumVolume; }
                set { _MinimumVolume = value; OnPropertyChanged("MinimumVolume"); }
            }
            
            private Hl7.Fhir.Model.Element _MinimumVolume;
            
            /// <summary>
            /// Additive associated with container
            /// </summary>
            [FhirElement("additive", Order=100)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.SpecimenDefinition.AdditiveComponent> Additive
            {
                get { if(_Additive==null) _Additive = new List<Hl7.Fhir.Model.SpecimenDefinition.AdditiveComponent>(); return _Additive; }
                set { _Additive = value; OnPropertyChanged("Additive"); }
            }
            
            private List<Hl7.Fhir.Model.SpecimenDefinition.AdditiveComponent> _Additive;
            
            /// <summary>
            /// Specimen container preparation
            /// </summary>
            [FhirElement("preparation", Order=110)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PreparationElement
            {
                get { return _PreparationElement; }
                set { _PreparationElement = value; OnPropertyChanged("PreparationElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PreparationElement;
            
            /// <summary>
            /// Specimen container preparation
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Preparation
            {
                get { return PreparationElement != null ? PreparationElement.Value : null; }
                set
                {
                    if (value == null)
                        PreparationElement = null; 
                    else
                        PreparationElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Preparation");
                }
            }
            
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ContainerComponent;
                
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Material != null) dest.Material = (Hl7.Fhir.Model.CodeableConcept)Material.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(Cap != null) dest.Cap = (Hl7.Fhir.Model.CodeableConcept)Cap.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Capacity != null) dest.Capacity = (Hl7.Fhir.Model.SimpleQuantity)Capacity.DeepCopy();
                    if(MinimumVolume != null) dest.MinimumVolume = (Hl7.Fhir.Model.Element)MinimumVolume.DeepCopy();
                    if(Additive != null) dest.Additive = new List<Hl7.Fhir.Model.SpecimenDefinition.AdditiveComponent>(Additive.DeepCopy());
                    if(PreparationElement != null) dest.PreparationElement = (Hl7.Fhir.Model.FhirString)PreparationElement.DeepCopy();
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
                if( !DeepComparable.Matches(Material, otherT.Material)) return false;
                if( !DeepComparable.Matches(Type, otherT.Type)) return false;
                if( !DeepComparable.Matches(Cap, otherT.Cap)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(Capacity, otherT.Capacity)) return false;
                if( !DeepComparable.Matches(MinimumVolume, otherT.MinimumVolume)) return false;
                if( !DeepComparable.Matches(Additive, otherT.Additive)) return false;
                if( !DeepComparable.Matches(PreparationElement, otherT.PreparationElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ContainerComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Material, otherT.Material)) return false;
                if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
                if( !DeepComparable.IsExactly(Cap, otherT.Cap)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Capacity, otherT.Capacity)) return false;
                if( !DeepComparable.IsExactly(MinimumVolume, otherT.MinimumVolume)) return false;
                if( !DeepComparable.IsExactly(Additive, otherT.Additive)) return false;
                if( !DeepComparable.IsExactly(PreparationElement, otherT.PreparationElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Material != null) yield return Material;
                    if (Type != null) yield return Type;
                    if (Cap != null) yield return Cap;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (Capacity != null) yield return Capacity;
                    if (MinimumVolume != null) yield return MinimumVolume;
                    foreach (var elem in Additive) { if (elem != null) yield return elem; }
                    if (PreparationElement != null) yield return PreparationElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Material != null) yield return new ElementValue("material", Material);
                    if (Type != null) yield return new ElementValue("type", Type);
                    if (Cap != null) yield return new ElementValue("cap", Cap);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (Capacity != null) yield return new ElementValue("capacity", Capacity);
                    if (MinimumVolume != null) yield return new ElementValue("minimumVolume", MinimumVolume);
                    foreach (var elem in Additive) { if (elem != null) yield return new ElementValue("additive", elem); }
                    if (PreparationElement != null) yield return new ElementValue("preparation", PreparationElement);
                }
            }

            
        }
        
        
        [FhirType("AdditiveComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class AdditiveComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "AdditiveComponent"; } }
            
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
                var dest = other as AdditiveComponent;
                
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
                return CopyTo(new AdditiveComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as AdditiveComponent;
                if(otherT == null) return false;
                
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Additive, otherT.Additive)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as AdditiveComponent;
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
                    if (Additive != null) yield return new ElementValue("additive", Additive);
                }
            }

            
        }
        
        
        [FhirType("HandlingComponent", NamedBackboneElement=true)]
        [DataContract]
        public partial class HandlingComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged
        {
            [NotMapped]
            public override string TypeName { get { return "HandlingComponent"; } }
            
            /// <summary>
            /// Temperature qualifier
            /// </summary>
            [FhirElement("temperatureQualifier", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept TemperatureQualifier
            {
                get { return _TemperatureQualifier; }
                set { _TemperatureQualifier = value; OnPropertyChanged("TemperatureQualifier"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _TemperatureQualifier;
            
            /// <summary>
            /// Temperature range
            /// </summary>
            [FhirElement("temperatureRange", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Range TemperatureRange
            {
                get { return _TemperatureRange; }
                set { _TemperatureRange = value; OnPropertyChanged("TemperatureRange"); }
            }
            
            private Hl7.Fhir.Model.Range _TemperatureRange;
            
            /// <summary>
            /// Maximum preservation time
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
            /// Preservation instruction
            /// </summary>
            [FhirElement("instruction", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString InstructionElement
            {
                get { return _InstructionElement; }
                set { _InstructionElement = value; OnPropertyChanged("InstructionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _InstructionElement;
            
            /// <summary>
            /// Preservation instruction
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
                    if(TemperatureQualifier != null) dest.TemperatureQualifier = (Hl7.Fhir.Model.CodeableConcept)TemperatureQualifier.DeepCopy();
                    if(TemperatureRange != null) dest.TemperatureRange = (Hl7.Fhir.Model.Range)TemperatureRange.DeepCopy();
                    if(MaxDuration != null) dest.MaxDuration = (Duration)MaxDuration.DeepCopy();
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
                if( !DeepComparable.Matches(TemperatureQualifier, otherT.TemperatureQualifier)) return false;
                if( !DeepComparable.Matches(TemperatureRange, otherT.TemperatureRange)) return false;
                if( !DeepComparable.Matches(MaxDuration, otherT.MaxDuration)) return false;
                if( !DeepComparable.Matches(InstructionElement, otherT.InstructionElement)) return false;
                
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as HandlingComponent;
                if(otherT == null) return false;
                
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(TemperatureQualifier, otherT.TemperatureQualifier)) return false;
                if( !DeepComparable.IsExactly(TemperatureRange, otherT.TemperatureRange)) return false;
                if( !DeepComparable.IsExactly(MaxDuration, otherT.MaxDuration)) return false;
                if( !DeepComparable.IsExactly(InstructionElement, otherT.InstructionElement)) return false;
                
                return true;
            }


            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (TemperatureQualifier != null) yield return TemperatureQualifier;
                    if (TemperatureRange != null) yield return TemperatureRange;
                    if (MaxDuration != null) yield return MaxDuration;
                    if (InstructionElement != null) yield return InstructionElement;
                }
            }

            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (TemperatureQualifier != null) yield return new ElementValue("temperatureQualifier", TemperatureQualifier);
                    if (TemperatureRange != null) yield return new ElementValue("temperatureRange", TemperatureRange);
                    if (MaxDuration != null) yield return new ElementValue("maxDuration", MaxDuration);
                    if (InstructionElement != null) yield return new ElementValue("instruction", InstructionElement);
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
        [FhirElement("patientPreparation", InSummary=true, Order=110)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> PatientPreparation
        {
            get { if(_PatientPreparation==null) _PatientPreparation = new List<Hl7.Fhir.Model.CodeableConcept>(); return _PatientPreparation; }
            set { _PatientPreparation = value; OnPropertyChanged("PatientPreparation"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _PatientPreparation;
        
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
        [FhirElement("typeTested", Order=140)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.SpecimenDefinition.TypeTestedComponent> TypeTested
        {
            get { if(_TypeTested==null) _TypeTested = new List<Hl7.Fhir.Model.SpecimenDefinition.TypeTestedComponent>(); return _TypeTested; }
            set { _TypeTested = value; OnPropertyChanged("TypeTested"); }
        }
        
        private List<Hl7.Fhir.Model.SpecimenDefinition.TypeTestedComponent> _TypeTested;
        

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
                if(PatientPreparation != null) dest.PatientPreparation = new List<Hl7.Fhir.Model.CodeableConcept>(PatientPreparation.DeepCopy());
                if(TimeAspectElement != null) dest.TimeAspectElement = (Hl7.Fhir.Model.FhirString)TimeAspectElement.DeepCopy();
                if(Collection != null) dest.Collection = new List<Hl7.Fhir.Model.CodeableConcept>(Collection.DeepCopy());
                if(TypeTested != null) dest.TypeTested = new List<Hl7.Fhir.Model.SpecimenDefinition.TypeTestedComponent>(TypeTested.DeepCopy());
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
            if( !DeepComparable.Matches(PatientPreparation, otherT.PatientPreparation)) return false;
            if( !DeepComparable.Matches(TimeAspectElement, otherT.TimeAspectElement)) return false;
            if( !DeepComparable.Matches(Collection, otherT.Collection)) return false;
            if( !DeepComparable.Matches(TypeTested, otherT.TypeTested)) return false;
            
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as SpecimenDefinition;
            if(otherT == null) return false;
            
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(TypeCollected, otherT.TypeCollected)) return false;
            if( !DeepComparable.IsExactly(PatientPreparation, otherT.PatientPreparation)) return false;
            if( !DeepComparable.IsExactly(TimeAspectElement, otherT.TimeAspectElement)) return false;
            if( !DeepComparable.IsExactly(Collection, otherT.Collection)) return false;
            if( !DeepComparable.IsExactly(TypeTested, otherT.TypeTested)) return false;
            
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
				foreach (var elem in PatientPreparation) { if (elem != null) yield return elem; }
				if (TimeAspectElement != null) yield return TimeAspectElement;
				foreach (var elem in Collection) { if (elem != null) yield return elem; }
				foreach (var elem in TypeTested) { if (elem != null) yield return elem; }
            }
        }

        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                if (TypeCollected != null) yield return new ElementValue("typeCollected", TypeCollected);
                foreach (var elem in PatientPreparation) { if (elem != null) yield return new ElementValue("patientPreparation", elem); }
                if (TimeAspectElement != null) yield return new ElementValue("timeAspect", TimeAspectElement);
                foreach (var elem in Collection) { if (elem != null) yield return new ElementValue("collection", elem); }
                foreach (var elem in TypeTested) { if (elem != null) yield return new ElementValue("typeTested", elem); }
            }
        }

    }
    
}
