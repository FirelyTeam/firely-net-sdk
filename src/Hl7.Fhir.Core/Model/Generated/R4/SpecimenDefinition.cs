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
// Generated for FHIR v4.0.1
//
namespace Hl7.Fhir.Model.R4
{
    /// <summary>
    /// Kind of specimen
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "SpecimenDefinition", IsResource=true)]
    [DataContract]
    public partial class SpecimenDefinition : Hl7.Fhir.Model.DomainResource, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.SpecimenDefinition; } }
        [NotMapped]
        public override string TypeName { get { return "SpecimenDefinition"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "TypeTestedComponent")]
        [DataContract]
        public partial class TypeTestedComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
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
                    if (value == null)
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
            public Code<Hl7.Fhir.Model.R4.SpecimenContainedPreference> PreferenceElement
            {
                get { return _PreferenceElement; }
                set { _PreferenceElement = value; OnPropertyChanged("PreferenceElement"); }
            }
            
            private Code<Hl7.Fhir.Model.R4.SpecimenContainedPreference> _PreferenceElement;
            
            /// <summary>
            /// preferred | alternate
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.R4.SpecimenContainedPreference? Preference
            {
                get { return PreferenceElement != null ? PreferenceElement.Value : null; }
                set
                {
                    if (value == null)
                        PreferenceElement = null;
                    else
                        PreferenceElement = new Code<Hl7.Fhir.Model.R4.SpecimenContainedPreference>(value);
                    OnPropertyChanged("Preference");
                }
            }
            
            /// <summary>
            /// The specimen's container
            /// </summary>
            [FhirElement("container", Order=70)]
            [DataMember]
            public ContainerComponent Container
            {
                get { return _Container; }
                set { _Container = value; OnPropertyChanged("Container"); }
            }
            
            private ContainerComponent _Container;
            
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
            public Hl7.Fhir.Model.R4.Duration RetentionTime
            {
                get { return _RetentionTime; }
                set { _RetentionTime = value; OnPropertyChanged("RetentionTime"); }
            }
            
            private Hl7.Fhir.Model.R4.Duration _RetentionTime;
            
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
            public List<HandlingComponent> Handling
            {
                get { if(_Handling==null) _Handling = new List<HandlingComponent>(); return _Handling; }
                set { _Handling = value; OnPropertyChanged("Handling"); }
            }
            
            private List<HandlingComponent> _Handling;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("TypeTestedComponent");
                base.Serialize(sink);
                sink.Element("isDerived", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); IsDerivedElement?.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Type?.Serialize(sink);
                sink.Element("preference", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); PreferenceElement?.Serialize(sink);
                sink.Element("container", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Container?.Serialize(sink);
                sink.Element("requirement", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); RequirementElement?.Serialize(sink);
                sink.Element("retentionTime", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); RetentionTime?.Serialize(sink);
                sink.BeginList("rejectionCriterion", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in RejectionCriterion)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("handling", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Handling)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "isDerived":
                        IsDerivedElement = source.PopulateValue(IsDerivedElement);
                        return true;
                    case "_isDerived":
                        IsDerivedElement = source.Populate(IsDerivedElement);
                        return true;
                    case "type":
                        Type = source.Populate(Type);
                        return true;
                    case "preference":
                        PreferenceElement = source.PopulateValue(PreferenceElement);
                        return true;
                    case "_preference":
                        PreferenceElement = source.Populate(PreferenceElement);
                        return true;
                    case "container":
                        Container = source.Populate(Container);
                        return true;
                    case "requirement":
                        RequirementElement = source.PopulateValue(RequirementElement);
                        return true;
                    case "_requirement":
                        RequirementElement = source.Populate(RequirementElement);
                        return true;
                    case "retentionTime":
                        RetentionTime = source.Populate(RetentionTime);
                        return true;
                    case "rejectionCriterion":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "handling":
                        source.SetList(this, jsonPropertyName);
                        return true;
                }
                return false;
            }
            
            internal override bool SetListElementFromJson(string jsonPropertyName, int index, ref Serialization.JsonSource source)
            {
                if (base.SetListElementFromJson(jsonPropertyName, index, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "rejectionCriterion":
                        source.PopulateListItem(RejectionCriterion, index);
                        return true;
                    case "handling":
                        source.PopulateListItem(Handling, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as TypeTestedComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(IsDerivedElement != null) dest.IsDerivedElement = (Hl7.Fhir.Model.FhirBoolean)IsDerivedElement.DeepCopy();
                    if(Type != null) dest.Type = (Hl7.Fhir.Model.CodeableConcept)Type.DeepCopy();
                    if(PreferenceElement != null) dest.PreferenceElement = (Code<Hl7.Fhir.Model.R4.SpecimenContainedPreference>)PreferenceElement.DeepCopy();
                    if(Container != null) dest.Container = (ContainerComponent)Container.DeepCopy();
                    if(RequirementElement != null) dest.RequirementElement = (Hl7.Fhir.Model.FhirString)RequirementElement.DeepCopy();
                    if(RetentionTime != null) dest.RetentionTime = (Hl7.Fhir.Model.R4.Duration)RetentionTime.DeepCopy();
                    if(RejectionCriterion != null) dest.RejectionCriterion = new List<Hl7.Fhir.Model.CodeableConcept>(RejectionCriterion.DeepCopy());
                    if(Handling != null) dest.Handling = new List<HandlingComponent>(Handling.DeepCopy());
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
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ContainerComponent")]
        [DataContract]
        public partial class ContainerComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
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
            public List<AdditiveComponent> Additive
            {
                get { if(_Additive==null) _Additive = new List<AdditiveComponent>(); return _Additive; }
                set { _Additive = value; OnPropertyChanged("Additive"); }
            }
            
            private List<AdditiveComponent> _Additive;
            
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ContainerComponent");
                base.Serialize(sink);
                sink.Element("material", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Material?.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Type?.Serialize(sink);
                sink.Element("cap", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Cap?.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
                sink.Element("capacity", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Capacity?.Serialize(sink);
                sink.Element("minimumVolume", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, true); MinimumVolume?.Serialize(sink);
                sink.BeginList("additive", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Additive)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("preparation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PreparationElement?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "material":
                        Material = source.Populate(Material);
                        return true;
                    case "type":
                        Type = source.Populate(Type);
                        return true;
                    case "cap":
                        Cap = source.Populate(Cap);
                        return true;
                    case "description":
                        DescriptionElement = source.PopulateValue(DescriptionElement);
                        return true;
                    case "_description":
                        DescriptionElement = source.Populate(DescriptionElement);
                        return true;
                    case "capacity":
                        Capacity = source.Populate(Capacity);
                        return true;
                    case "minimumVolumeQuantity":
                        source.CheckDuplicates<Hl7.Fhir.Model.SimpleQuantity>(MinimumVolume, "minimumVolume");
                        MinimumVolume = source.Populate(MinimumVolume as Hl7.Fhir.Model.SimpleQuantity);
                        return true;
                    case "minimumVolumeString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(MinimumVolume, "minimumVolume");
                        MinimumVolume = source.PopulateValue(MinimumVolume as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "_minimumVolumeString":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirString>(MinimumVolume, "minimumVolume");
                        MinimumVolume = source.Populate(MinimumVolume as Hl7.Fhir.Model.FhirString);
                        return true;
                    case "additive":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "preparation":
                        PreparationElement = source.PopulateValue(PreparationElement);
                        return true;
                    case "_preparation":
                        PreparationElement = source.Populate(PreparationElement);
                        return true;
                }
                return false;
            }
            
            internal override bool SetListElementFromJson(string jsonPropertyName, int index, ref Serialization.JsonSource source)
            {
                if (base.SetListElementFromJson(jsonPropertyName, index, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "additive":
                        source.PopulateListItem(Additive, index);
                        return true;
                }
                return false;
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
                    if(Additive != null) dest.Additive = new List<AdditiveComponent>(Additive.DeepCopy());
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
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "AdditiveComponent")]
        [DataContract]
        public partial class AdditiveComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("AdditiveComponent");
                base.Serialize(sink);
                sink.Element("additive", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, true); Additive?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "additiveCodeableConcept":
                        source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Additive, "additive");
                        Additive = source.Populate(Additive as Hl7.Fhir.Model.CodeableConcept);
                        return true;
                    case "additiveReference":
                        source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Additive, "additive");
                        Additive = source.Populate(Additive as Hl7.Fhir.Model.ResourceReference);
                        return true;
                }
                return false;
            }
        
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
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "HandlingComponent")]
        [DataContract]
        public partial class HandlingComponent : Hl7.Fhir.Model.BackboneElement, System.ComponentModel.INotifyPropertyChanged, IComponent
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
            public Hl7.Fhir.Model.R4.Duration MaxDuration
            {
                get { return _MaxDuration; }
                set { _MaxDuration = value; OnPropertyChanged("MaxDuration"); }
            }
            
            private Hl7.Fhir.Model.R4.Duration _MaxDuration;
            
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
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("HandlingComponent");
                base.Serialize(sink);
                sink.Element("temperatureQualifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); TemperatureQualifier?.Serialize(sink);
                sink.Element("temperatureRange", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); TemperatureRange?.Serialize(sink);
                sink.Element("maxDuration", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); MaxDuration?.Serialize(sink);
                sink.Element("instruction", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); InstructionElement?.Serialize(sink);
                sink.End();
            }
        
            internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
            {
                if (base.SetElementFromJson(jsonPropertyName, ref source))
                {
                    return true;
                }
                switch (jsonPropertyName)
                {
                    case "temperatureQualifier":
                        TemperatureQualifier = source.Populate(TemperatureQualifier);
                        return true;
                    case "temperatureRange":
                        TemperatureRange = source.Populate(TemperatureRange);
                        return true;
                    case "maxDuration":
                        MaxDuration = source.Populate(MaxDuration);
                        return true;
                    case "instruction":
                        InstructionElement = source.PopulateValue(InstructionElement);
                        return true;
                    case "_instruction":
                        InstructionElement = source.Populate(InstructionElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as HandlingComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(TemperatureQualifier != null) dest.TemperatureQualifier = (Hl7.Fhir.Model.CodeableConcept)TemperatureQualifier.DeepCopy();
                    if(TemperatureRange != null) dest.TemperatureRange = (Hl7.Fhir.Model.Range)TemperatureRange.DeepCopy();
                    if(MaxDuration != null) dest.MaxDuration = (Hl7.Fhir.Model.R4.Duration)MaxDuration.DeepCopy();
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
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
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
        [FhirElement("typeCollected", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
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
        [FhirElement("patientPreparation", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
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
        [FhirElement("timeAspect", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
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
        [FhirElement("collection", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
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
        public List<TypeTestedComponent> TypeTested
        {
            get { if(_TypeTested==null) _TypeTested = new List<TypeTestedComponent>(); return _TypeTested; }
            set { _TypeTested = value; OnPropertyChanged("TypeTested"); }
        }
        
        private List<TypeTestedComponent> _TypeTested;
    
    
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
                if(TypeTested != null) dest.TypeTested = new List<TypeTestedComponent>(TypeTested.DeepCopy());
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
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("SpecimenDefinition");
            base.Serialize(sink);
            sink.Element("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Identifier?.Serialize(sink);
            sink.Element("typeCollected", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TypeCollected?.Serialize(sink);
            sink.BeginList("patientPreparation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in PatientPreparation)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("timeAspect", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TimeAspectElement?.Serialize(sink);
            sink.BeginList("collection", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Collection)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("typeTested", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in TypeTested)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.End();
        }
    
        internal override bool SetElementFromJson(string jsonPropertyName, ref Serialization.JsonSource source)
        {
            if (base.SetElementFromJson(jsonPropertyName, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "identifier":
                    Identifier = source.Populate(Identifier);
                    return true;
                case "typeCollected":
                    TypeCollected = source.Populate(TypeCollected);
                    return true;
                case "patientPreparation":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "timeAspect":
                    TimeAspectElement = source.PopulateValue(TimeAspectElement);
                    return true;
                case "_timeAspect":
                    TimeAspectElement = source.Populate(TimeAspectElement);
                    return true;
                case "collection":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "typeTested":
                    source.SetList(this, jsonPropertyName);
                    return true;
            }
            return false;
        }
        
        internal override bool SetListElementFromJson(string jsonPropertyName, int index, ref Serialization.JsonSource source)
        {
            if (base.SetListElementFromJson(jsonPropertyName, index, ref source))
            {
                return true;
            }
            switch (jsonPropertyName)
            {
                case "patientPreparation":
                    source.PopulateListItem(PatientPreparation, index);
                    return true;
                case "collection":
                    source.PopulateListItem(Collection, index);
                    return true;
                case "typeTested":
                    source.PopulateListItem(TypeTested, index);
                    return true;
            }
            return false;
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
