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
namespace Hl7.Fhir.Model.STU3
{
    /// <summary>
    /// A quality measure definition
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.STU3, "Measure", IsResource=true)]
    [DataContract]
    public partial class Measure : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.IMeasure, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.Measure; } }
        [NotMapped]
        public override string TypeName { get { return "Measure"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "GroupComponent")]
        [DataContract]
        public partial class GroupComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IMeasureGroupComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "GroupComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IMeasurePopulationComponent> Hl7.Fhir.Model.IMeasureGroupComponent.Population { get { return Population; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.IMeasureStratifierComponent> Hl7.Fhir.Model.IMeasureGroupComponent.Stratifier { get { return Stratifier; } }
            
            /// <summary>
            /// Unique identifier
            /// </summary>
            [FhirElement("identifier", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// Short name
            /// </summary>
            [FhirElement("name", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Short name
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
            /// Summary description
            /// </summary>
            [FhirElement("description", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Summary description
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
            /// Population criteria
            /// </summary>
            [FhirElement("population", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<PopulationComponent> Population
            {
                get { if(_Population==null) _Population = new List<PopulationComponent>(); return _Population; }
                set { _Population = value; OnPropertyChanged("Population"); }
            }
            
            private List<PopulationComponent> _Population;
            
            /// <summary>
            /// Stratifier criteria for the measure
            /// </summary>
            [FhirElement("stratifier", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<StratifierComponent> Stratifier
            {
                get { if(_Stratifier==null) _Stratifier = new List<StratifierComponent>(); return _Stratifier; }
                set { _Stratifier = value; OnPropertyChanged("Stratifier"); }
            }
            
            private List<StratifierComponent> _Stratifier;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("GroupComponent");
                base.Serialize(sink);
                sink.Element("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); Identifier?.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); NameElement?.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
                sink.BeginList("population", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Population)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("stratifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Stratifier)
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
                    case "name":
                        NameElement = source.PopulateValue(NameElement);
                        return true;
                    case "_name":
                        NameElement = source.Populate(NameElement);
                        return true;
                    case "description":
                        DescriptionElement = source.PopulateValue(DescriptionElement);
                        return true;
                    case "_description":
                        DescriptionElement = source.Populate(DescriptionElement);
                        return true;
                    case "population":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "stratifier":
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
                    case "population":
                        source.PopulateListItem(Population, index);
                        return true;
                    case "stratifier":
                        source.PopulateListItem(Stratifier, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as GroupComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(Population != null) dest.Population = new List<PopulationComponent>(Population.DeepCopy());
                    if(Stratifier != null) dest.Stratifier = new List<StratifierComponent>(Stratifier.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new GroupComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as GroupComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(Population, otherT.Population)) return false;
                if( !DeepComparable.Matches(Stratifier, otherT.Stratifier)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as GroupComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(Population, otherT.Population)) return false;
                if( !DeepComparable.IsExactly(Stratifier, otherT.Stratifier)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Identifier != null) yield return Identifier;
                    if (NameElement != null) yield return NameElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    foreach (var elem in Population) { if (elem != null) yield return elem; }
                    foreach (var elem in Stratifier) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    foreach (var elem in Population) { if (elem != null) yield return new ElementValue("population", elem); }
                    foreach (var elem in Stratifier) { if (elem != null) yield return new ElementValue("stratifier", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "PopulationComponent")]
        [DataContract]
        public partial class PopulationComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IMeasurePopulationComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "PopulationComponent"; } }
            
            /// <summary>
            /// Unique identifier
            /// </summary>
            [FhirElement("identifier", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// initial-population | numerator | numerator-exclusion | denominator | denominator-exclusion | denominator-exception | measure-population | measure-population-exclusion | measure-observation
            /// </summary>
            [FhirElement("code", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.CodeableConcept Code
            {
                get { return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private Hl7.Fhir.Model.CodeableConcept _Code;
            
            /// <summary>
            /// Short name
            /// </summary>
            [FhirElement("name", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString NameElement
            {
                get { return _NameElement; }
                set { _NameElement = value; OnPropertyChanged("NameElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _NameElement;
            
            /// <summary>
            /// Short name
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
            /// The human readable description of this population criteria
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
            /// The human readable description of this population criteria
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
            /// The name of a valid referenced CQL expression (may be namespaced) that defines this population criteria
            /// </summary>
            [FhirElement("criteria", Order=80)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CriteriaElement
            {
                get { return _CriteriaElement; }
                set { _CriteriaElement = value; OnPropertyChanged("CriteriaElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CriteriaElement;
            
            /// <summary>
            /// The name of a valid referenced CQL expression (may be namespaced) that defines this population criteria
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Criteria
            {
                get { return CriteriaElement != null ? CriteriaElement.Value : null; }
                set
                {
                    if (value == null)
                        CriteriaElement = null;
                    else
                        CriteriaElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Criteria");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("PopulationComponent");
                base.Serialize(sink);
                sink.Element("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Identifier?.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Code?.Serialize(sink);
                sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); NameElement?.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
                sink.Element("criteria", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); CriteriaElement?.Serialize(sink);
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
                    case "code":
                        Code = source.Populate(Code);
                        return true;
                    case "name":
                        NameElement = source.PopulateValue(NameElement);
                        return true;
                    case "_name":
                        NameElement = source.Populate(NameElement);
                        return true;
                    case "description":
                        DescriptionElement = source.PopulateValue(DescriptionElement);
                        return true;
                    case "_description":
                        DescriptionElement = source.Populate(DescriptionElement);
                        return true;
                    case "criteria":
                        CriteriaElement = source.PopulateValue(CriteriaElement);
                        return true;
                    case "_criteria":
                        CriteriaElement = source.Populate(CriteriaElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PopulationComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(Code != null) dest.Code = (Hl7.Fhir.Model.CodeableConcept)Code.DeepCopy();
                    if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(CriteriaElement != null) dest.CriteriaElement = (Hl7.Fhir.Model.FhirString)CriteriaElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new PopulationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PopulationComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
                if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(CriteriaElement, otherT.CriteriaElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PopulationComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
                if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(CriteriaElement, otherT.CriteriaElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Identifier != null) yield return Identifier;
                    if (Code != null) yield return Code;
                    if (NameElement != null) yield return NameElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (CriteriaElement != null) yield return CriteriaElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                    if (Code != null) yield return new ElementValue("code", Code);
                    if (NameElement != null) yield return new ElementValue("name", NameElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (CriteriaElement != null) yield return new ElementValue("criteria", CriteriaElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "StratifierComponent")]
        [DataContract]
        public partial class StratifierComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IMeasureStratifierComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "StratifierComponent"; } }
            
            /// <summary>
            /// The identifier for the stratifier used to coordinate the reported data back to this stratifier
            /// </summary>
            [FhirElement("identifier", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// How the measure should be stratified
            /// </summary>
            [FhirElement("criteria", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CriteriaElement
            {
                get { return _CriteriaElement; }
                set { _CriteriaElement = value; OnPropertyChanged("CriteriaElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CriteriaElement;
            
            /// <summary>
            /// How the measure should be stratified
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Criteria
            {
                get { return CriteriaElement != null ? CriteriaElement.Value : null; }
                set
                {
                    if (value == null)
                        CriteriaElement = null;
                    else
                        CriteriaElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Criteria");
                }
            }
            
            /// <summary>
            /// Path to the stratifier
            /// </summary>
            [FhirElement("path", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PathElement
            {
                get { return _PathElement; }
                set { _PathElement = value; OnPropertyChanged("PathElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PathElement;
            
            /// <summary>
            /// Path to the stratifier
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Path
            {
                get { return PathElement != null ? PathElement.Value : null; }
                set
                {
                    if (value == null)
                        PathElement = null;
                    else
                        PathElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Path");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("StratifierComponent");
                base.Serialize(sink);
                sink.Element("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Identifier?.Serialize(sink);
                sink.Element("criteria", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CriteriaElement?.Serialize(sink);
                sink.Element("path", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PathElement?.Serialize(sink);
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
                    case "criteria":
                        CriteriaElement = source.PopulateValue(CriteriaElement);
                        return true;
                    case "_criteria":
                        CriteriaElement = source.Populate(CriteriaElement);
                        return true;
                    case "path":
                        PathElement = source.PopulateValue(PathElement);
                        return true;
                    case "_path":
                        PathElement = source.Populate(PathElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as StratifierComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(CriteriaElement != null) dest.CriteriaElement = (Hl7.Fhir.Model.FhirString)CriteriaElement.DeepCopy();
                    if(PathElement != null) dest.PathElement = (Hl7.Fhir.Model.FhirString)PathElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new StratifierComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as StratifierComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(CriteriaElement, otherT.CriteriaElement)) return false;
                if( !DeepComparable.Matches(PathElement, otherT.PathElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as StratifierComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(CriteriaElement, otherT.CriteriaElement)) return false;
                if( !DeepComparable.IsExactly(PathElement, otherT.PathElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Identifier != null) yield return Identifier;
                    if (CriteriaElement != null) yield return CriteriaElement;
                    if (PathElement != null) yield return PathElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                    if (CriteriaElement != null) yield return new ElementValue("criteria", CriteriaElement);
                    if (PathElement != null) yield return new ElementValue("path", PathElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.STU3, "SupplementalDataComponent")]
        [DataContract]
        public partial class SupplementalDataComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.IMeasureSupplementalDataComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SupplementalDataComponent"; } }
            
            /// <summary>
            /// Identifier, unique within the measure
            /// </summary>
            [FhirElement("identifier", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Identifier Identifier
            {
                get { return _Identifier; }
                set { _Identifier = value; OnPropertyChanged("Identifier"); }
            }
            
            private Hl7.Fhir.Model.Identifier _Identifier;
            
            /// <summary>
            /// supplemental-data | risk-adjustment-factor
            /// </summary>
            [FhirElement("usage", Order=50)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.CodeableConcept> Usage
            {
                get { if(_Usage==null) _Usage = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Usage; }
                set { _Usage = value; OnPropertyChanged("Usage"); }
            }
            
            private List<Hl7.Fhir.Model.CodeableConcept> _Usage;
            
            /// <summary>
            /// Expression describing additional data to be reported
            /// </summary>
            [FhirElement("criteria", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString CriteriaElement
            {
                get { return _CriteriaElement; }
                set { _CriteriaElement = value; OnPropertyChanged("CriteriaElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _CriteriaElement;
            
            /// <summary>
            /// Expression describing additional data to be reported
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Criteria
            {
                get { return CriteriaElement != null ? CriteriaElement.Value : null; }
                set
                {
                    if (value == null)
                        CriteriaElement = null;
                    else
                        CriteriaElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Criteria");
                }
            }
            
            /// <summary>
            /// Path to the supplemental data element
            /// </summary>
            [FhirElement("path", Order=70)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PathElement
            {
                get { return _PathElement; }
                set { _PathElement = value; OnPropertyChanged("PathElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PathElement;
            
            /// <summary>
            /// Path to the supplemental data element
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Path
            {
                get { return PathElement != null ? PathElement.Value : null; }
                set
                {
                    if (value == null)
                        PathElement = null;
                    else
                        PathElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Path");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SupplementalDataComponent");
                base.Serialize(sink);
                sink.Element("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Identifier?.Serialize(sink);
                sink.BeginList("usage", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Usage)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.Element("criteria", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CriteriaElement?.Serialize(sink);
                sink.Element("path", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PathElement?.Serialize(sink);
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
                    case "usage":
                        source.SetList(this, jsonPropertyName);
                        return true;
                    case "criteria":
                        CriteriaElement = source.PopulateValue(CriteriaElement);
                        return true;
                    case "_criteria":
                        CriteriaElement = source.Populate(CriteriaElement);
                        return true;
                    case "path":
                        PathElement = source.PopulateValue(PathElement);
                        return true;
                    case "_path":
                        PathElement = source.Populate(PathElement);
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
                    case "usage":
                        source.PopulateListItem(Usage, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SupplementalDataComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
                    if(Usage != null) dest.Usage = new List<Hl7.Fhir.Model.CodeableConcept>(Usage.DeepCopy());
                    if(CriteriaElement != null) dest.CriteriaElement = (Hl7.Fhir.Model.FhirString)CriteriaElement.DeepCopy();
                    if(PathElement != null) dest.PathElement = (Hl7.Fhir.Model.FhirString)PathElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new SupplementalDataComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SupplementalDataComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.Matches(Usage, otherT.Usage)) return false;
                if( !DeepComparable.Matches(CriteriaElement, otherT.CriteriaElement)) return false;
                if( !DeepComparable.Matches(PathElement, otherT.PathElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SupplementalDataComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
                if( !DeepComparable.IsExactly(Usage, otherT.Usage)) return false;
                if( !DeepComparable.IsExactly(CriteriaElement, otherT.CriteriaElement)) return false;
                if( !DeepComparable.IsExactly(PathElement, otherT.PathElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (Identifier != null) yield return Identifier;
                    foreach (var elem in Usage) { if (elem != null) yield return elem; }
                    if (CriteriaElement != null) yield return CriteriaElement;
                    if (PathElement != null) yield return PathElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                    foreach (var elem in Usage) { if (elem != null) yield return new ElementValue("usage", elem); }
                    if (CriteriaElement != null) yield return new ElementValue("criteria", CriteriaElement);
                    if (PathElement != null) yield return new ElementValue("path", PathElement);
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IContactDetail> Hl7.Fhir.Model.IMeasure.Contact { get { return Contact; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IRelatedArtifact> Hl7.Fhir.Model.IMeasure.RelatedArtifact { get { return RelatedArtifact; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IMeasureGroupComponent> Hl7.Fhir.Model.IMeasure.Group { get { return Group; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IMeasureSupplementalDataComponent> Hl7.Fhir.Model.IMeasure.SupplementalData { get { return SupplementalData; } }
    
        
        /// <summary>
        /// Logical URI to reference this measure (globally unique)
        /// </summary>
        [FhirElement("url", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri UrlElement
        {
            get { return _UrlElement; }
            set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
        }
        
        private Hl7.Fhir.Model.FhirUri _UrlElement;
        
        /// <summary>
        /// Logical URI to reference this measure (globally unique)
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
        /// Additional identifier for the measure
        /// </summary>
        [FhirElement("identifier", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Identifier> Identifier
        {
            get { if(_Identifier==null) _Identifier = new List<Hl7.Fhir.Model.Identifier>(); return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }
        
        private List<Hl7.Fhir.Model.Identifier> _Identifier;
        
        /// <summary>
        /// Business version of the measure
        /// </summary>
        [FhirElement("version", InSummary=Hl7.Fhir.Model.Version.All, Order=110)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString VersionElement
        {
            get { return _VersionElement; }
            set { _VersionElement = value; OnPropertyChanged("VersionElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _VersionElement;
        
        /// <summary>
        /// Business version of the measure
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
        /// Name for this measure (computer friendly)
        /// </summary>
        [FhirElement("name", InSummary=Hl7.Fhir.Model.Version.All, Order=120)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString NameElement
        {
            get { return _NameElement; }
            set { _NameElement = value; OnPropertyChanged("NameElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _NameElement;
        
        /// <summary>
        /// Name for this measure (computer friendly)
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
        /// Name for this measure (human friendly)
        /// </summary>
        [FhirElement("title", InSummary=Hl7.Fhir.Model.Version.All, Order=130)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString TitleElement
        {
            get { return _TitleElement; }
            set { _TitleElement = value; OnPropertyChanged("TitleElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _TitleElement;
        
        /// <summary>
        /// Name for this measure (human friendly)
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
        [FhirElement("status", InSummary=Hl7.Fhir.Model.Version.All, Order=140)]
        [CLSCompliant(false)]
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
                if (value == null)
                    StatusElement = null;
                else
                    StatusElement = new Code<Hl7.Fhir.Model.PublicationStatus>(value);
                OnPropertyChanged("Status");
            }
        }
        
        /// <summary>
        /// For testing purposes, not real usage
        /// </summary>
        [FhirElement("experimental", InSummary=Hl7.Fhir.Model.Version.All, Order=150)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean ExperimentalElement
        {
            get { return _ExperimentalElement; }
            set { _ExperimentalElement = value; OnPropertyChanged("ExperimentalElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _ExperimentalElement;
        
        /// <summary>
        /// For testing purposes, not real usage
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Experimental
        {
            get { return ExperimentalElement != null ? ExperimentalElement.Value : null; }
            set
            {
                if (value == null)
                    ExperimentalElement = null;
                else
                    ExperimentalElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Experimental");
            }
        }
        
        /// <summary>
        /// Date this was last changed
        /// </summary>
        [FhirElement("date", InSummary=Hl7.Fhir.Model.Version.All, Order=160)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirDateTime DateElement
        {
            get { return _DateElement; }
            set { _DateElement = value; OnPropertyChanged("DateElement"); }
        }
        
        private Hl7.Fhir.Model.FhirDateTime _DateElement;
        
        /// <summary>
        /// Date this was last changed
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
        [FhirElement("publisher", InSummary=Hl7.Fhir.Model.Version.All, Order=170)]
        [CLSCompliant(false)]
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
        /// Natural language description of the measure
        /// </summary>
        [FhirElement("description", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _DescriptionElement;
        
        /// <summary>
        /// Natural language description of the measure
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
                    DescriptionElement = new Hl7.Fhir.Model.Markdown(value);
                OnPropertyChanged("Description");
            }
        }
        
        /// <summary>
        /// Why this measure is defined
        /// </summary>
        [FhirElement("purpose", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown PurposeElement
        {
            get { return _PurposeElement; }
            set { _PurposeElement = value; OnPropertyChanged("PurposeElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _PurposeElement;
        
        /// <summary>
        /// Why this measure is defined
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Purpose
        {
            get { return PurposeElement != null ? PurposeElement.Value : null; }
            set
            {
                if (value == null)
                    PurposeElement = null;
                else
                    PurposeElement = new Hl7.Fhir.Model.Markdown(value);
                OnPropertyChanged("Purpose");
            }
        }
        
        /// <summary>
        /// Describes the clinical usage of the measure
        /// </summary>
        [FhirElement("usage", Order=200)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString UsageElement
        {
            get { return _UsageElement; }
            set { _UsageElement = value; OnPropertyChanged("UsageElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _UsageElement;
        
        /// <summary>
        /// Describes the clinical usage of the measure
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Usage
        {
            get { return UsageElement != null ? UsageElement.Value : null; }
            set
            {
                if (value == null)
                    UsageElement = null;
                else
                    UsageElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Usage");
            }
        }
        
        /// <summary>
        /// When the measure was approved by publisher
        /// </summary>
        [FhirElement("approvalDate", Order=210)]
        [DataMember]
        public Hl7.Fhir.Model.Date ApprovalDateElement
        {
            get { return _ApprovalDateElement; }
            set { _ApprovalDateElement = value; OnPropertyChanged("ApprovalDateElement"); }
        }
        
        private Hl7.Fhir.Model.Date _ApprovalDateElement;
        
        /// <summary>
        /// When the measure was approved by publisher
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
        /// When the measure was last reviewed
        /// </summary>
        [FhirElement("lastReviewDate", Order=220)]
        [DataMember]
        public Hl7.Fhir.Model.Date LastReviewDateElement
        {
            get { return _LastReviewDateElement; }
            set { _LastReviewDateElement = value; OnPropertyChanged("LastReviewDateElement"); }
        }
        
        private Hl7.Fhir.Model.Date _LastReviewDateElement;
        
        /// <summary>
        /// When the measure was last reviewed
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
        /// When the measure is expected to be used
        /// </summary>
        [FhirElement("effectivePeriod", InSummary=Hl7.Fhir.Model.Version.All, Order=230)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Period EffectivePeriod
        {
            get { return _EffectivePeriod; }
            set { _EffectivePeriod = value; OnPropertyChanged("EffectivePeriod"); }
        }
        
        private Hl7.Fhir.Model.Period _EffectivePeriod;
        
        /// <summary>
        /// Context the content is intended to support
        /// </summary>
        [FhirElement("useContext", InSummary=Hl7.Fhir.Model.Version.All, Order=240)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.UsageContext> UseContext
        {
            get { if(_UseContext==null) _UseContext = new List<Hl7.Fhir.Model.UsageContext>(); return _UseContext; }
            set { _UseContext = value; OnPropertyChanged("UseContext"); }
        }
        
        private List<Hl7.Fhir.Model.UsageContext> _UseContext;
        
        /// <summary>
        /// Intended jurisdiction for measure (if applicable)
        /// </summary>
        [FhirElement("jurisdiction", InSummary=Hl7.Fhir.Model.Version.All, Order=250)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Jurisdiction
        {
            get { if(_Jurisdiction==null) _Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Jurisdiction; }
            set { _Jurisdiction = value; OnPropertyChanged("Jurisdiction"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Jurisdiction;
        
        /// <summary>
        /// E.g. Education, Treatment, Assessment, etc
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
        /// A content contributor
        /// </summary>
        [FhirElement("contributor", Order=270)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.STU3.Contributor> Contributor
        {
            get { if(_Contributor==null) _Contributor = new List<Hl7.Fhir.Model.STU3.Contributor>(); return _Contributor; }
            set { _Contributor = value; OnPropertyChanged("Contributor"); }
        }
        
        private List<Hl7.Fhir.Model.STU3.Contributor> _Contributor;
        
        /// <summary>
        /// Contact details for the publisher
        /// </summary>
        [FhirElement("contact", InSummary=Hl7.Fhir.Model.Version.All, Order=280)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.STU3.ContactDetail> Contact
        {
            get { if(_Contact==null) _Contact = new List<Hl7.Fhir.Model.STU3.ContactDetail>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<Hl7.Fhir.Model.STU3.ContactDetail> _Contact;
        
        /// <summary>
        /// Use and/or publishing restrictions
        /// </summary>
        [FhirElement("copyright", Order=290)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown CopyrightElement
        {
            get { return _CopyrightElement; }
            set { _CopyrightElement = value; OnPropertyChanged("CopyrightElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _CopyrightElement;
        
        /// <summary>
        /// Use and/or publishing restrictions
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Copyright
        {
            get { return CopyrightElement != null ? CopyrightElement.Value : null; }
            set
            {
                if (value == null)
                    CopyrightElement = null;
                else
                    CopyrightElement = new Hl7.Fhir.Model.Markdown(value);
                OnPropertyChanged("Copyright");
            }
        }
        
        /// <summary>
        /// Additional documentation, citations, etc
        /// </summary>
        [FhirElement("relatedArtifact", Order=300)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.STU3.RelatedArtifact> RelatedArtifact
        {
            get { if(_RelatedArtifact==null) _RelatedArtifact = new List<Hl7.Fhir.Model.STU3.RelatedArtifact>(); return _RelatedArtifact; }
            set { _RelatedArtifact = value; OnPropertyChanged("RelatedArtifact"); }
        }
        
        private List<Hl7.Fhir.Model.STU3.RelatedArtifact> _RelatedArtifact;
        
        /// <summary>
        /// Logic used by the measure
        /// </summary>
        [FhirElement("library", Order=310)]
        [CLSCompliant(false)]
        [References("Library")]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ResourceReference> Library
        {
            get { if(_Library==null) _Library = new List<Hl7.Fhir.Model.ResourceReference>(); return _Library; }
            set { _Library = value; OnPropertyChanged("Library"); }
        }
        
        private List<Hl7.Fhir.Model.ResourceReference> _Library;
        
        /// <summary>
        /// Disclaimer for use of the measure or its referenced content
        /// </summary>
        [FhirElement("disclaimer", InSummary=Hl7.Fhir.Model.Version.All, Order=320)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown DisclaimerElement
        {
            get { return _DisclaimerElement; }
            set { _DisclaimerElement = value; OnPropertyChanged("DisclaimerElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _DisclaimerElement;
        
        /// <summary>
        /// Disclaimer for use of the measure or its referenced content
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Disclaimer
        {
            get { return DisclaimerElement != null ? DisclaimerElement.Value : null; }
            set
            {
                if (value == null)
                    DisclaimerElement = null;
                else
                    DisclaimerElement = new Hl7.Fhir.Model.Markdown(value);
                OnPropertyChanged("Disclaimer");
            }
        }
        
        /// <summary>
        /// proportion | ratio | continuous-variable | cohort
        /// </summary>
        [FhirElement("scoring", InSummary=Hl7.Fhir.Model.Version.All, Order=330)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept Scoring
        {
            get { return _Scoring; }
            set { _Scoring = value; OnPropertyChanged("Scoring"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _Scoring;
        
        /// <summary>
        /// opportunity | all-or-nothing | linear | weighted
        /// </summary>
        [FhirElement("compositeScoring", InSummary=Hl7.Fhir.Model.Version.All, Order=340)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.CodeableConcept CompositeScoring
        {
            get { return _CompositeScoring; }
            set { _CompositeScoring = value; OnPropertyChanged("CompositeScoring"); }
        }
        
        private Hl7.Fhir.Model.CodeableConcept _CompositeScoring;
        
        /// <summary>
        /// process | outcome | structure | patient-reported-outcome | composite
        /// </summary>
        [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=350)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Type
        {
            get { if(_Type==null) _Type = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Type; }
            set { _Type = value; OnPropertyChanged("Type"); }
        }
        
        private List<Hl7.Fhir.Model.CodeableConcept> _Type;
        
        /// <summary>
        /// How is risk adjustment applied for this measure
        /// </summary>
        [FhirElement("riskAdjustment", InSummary=Hl7.Fhir.Model.Version.All, Order=360)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString RiskAdjustmentElement
        {
            get { return _RiskAdjustmentElement; }
            set { _RiskAdjustmentElement = value; OnPropertyChanged("RiskAdjustmentElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _RiskAdjustmentElement;
        
        /// <summary>
        /// How is risk adjustment applied for this measure
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string RiskAdjustment
        {
            get { return RiskAdjustmentElement != null ? RiskAdjustmentElement.Value : null; }
            set
            {
                if (value == null)
                    RiskAdjustmentElement = null;
                else
                    RiskAdjustmentElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("RiskAdjustment");
            }
        }
        
        /// <summary>
        /// How is rate aggregation performed for this measure
        /// </summary>
        [FhirElement("rateAggregation", InSummary=Hl7.Fhir.Model.Version.All, Order=370)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString RateAggregationElement
        {
            get { return _RateAggregationElement; }
            set { _RateAggregationElement = value; OnPropertyChanged("RateAggregationElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _RateAggregationElement;
        
        /// <summary>
        /// How is rate aggregation performed for this measure
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string RateAggregation
        {
            get { return RateAggregationElement != null ? RateAggregationElement.Value : null; }
            set
            {
                if (value == null)
                    RateAggregationElement = null;
                else
                    RateAggregationElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("RateAggregation");
            }
        }
        
        /// <summary>
        /// Why does this measure exist
        /// </summary>
        [FhirElement("rationale", InSummary=Hl7.Fhir.Model.Version.All, Order=380)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown RationaleElement
        {
            get { return _RationaleElement; }
            set { _RationaleElement = value; OnPropertyChanged("RationaleElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _RationaleElement;
        
        /// <summary>
        /// Why does this measure exist
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Rationale
        {
            get { return RationaleElement != null ? RationaleElement.Value : null; }
            set
            {
                if (value == null)
                    RationaleElement = null;
                else
                    RationaleElement = new Hl7.Fhir.Model.Markdown(value);
                OnPropertyChanged("Rationale");
            }
        }
        
        /// <summary>
        /// Summary of clinical guidelines
        /// </summary>
        [FhirElement("clinicalRecommendationStatement", InSummary=Hl7.Fhir.Model.Version.All, Order=390)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown ClinicalRecommendationStatementElement
        {
            get { return _ClinicalRecommendationStatementElement; }
            set { _ClinicalRecommendationStatementElement = value; OnPropertyChanged("ClinicalRecommendationStatementElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _ClinicalRecommendationStatementElement;
        
        /// <summary>
        /// Summary of clinical guidelines
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ClinicalRecommendationStatement
        {
            get { return ClinicalRecommendationStatementElement != null ? ClinicalRecommendationStatementElement.Value : null; }
            set
            {
                if (value == null)
                    ClinicalRecommendationStatementElement = null;
                else
                    ClinicalRecommendationStatementElement = new Hl7.Fhir.Model.Markdown(value);
                OnPropertyChanged("ClinicalRecommendationStatement");
            }
        }
        
        /// <summary>
        /// Improvement notation for the measure, e.g. higher score indicates better quality
        /// </summary>
        [FhirElement("improvementNotation", InSummary=Hl7.Fhir.Model.Version.All, Order=400)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString ImprovementNotationElement
        {
            get { return _ImprovementNotationElement; }
            set { _ImprovementNotationElement = value; OnPropertyChanged("ImprovementNotationElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _ImprovementNotationElement;
        
        /// <summary>
        /// Improvement notation for the measure, e.g. higher score indicates better quality
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ImprovementNotation
        {
            get { return ImprovementNotationElement != null ? ImprovementNotationElement.Value : null; }
            set
            {
                if (value == null)
                    ImprovementNotationElement = null;
                else
                    ImprovementNotationElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("ImprovementNotation");
            }
        }
        
        /// <summary>
        /// Defined terms used in the measure documentation
        /// </summary>
        [FhirElement("definition", InSummary=Hl7.Fhir.Model.Version.All, Order=410)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Markdown> DefinitionElement
        {
            get { if(_DefinitionElement==null) _DefinitionElement = new List<Hl7.Fhir.Model.Markdown>(); return _DefinitionElement; }
            set { _DefinitionElement = value; OnPropertyChanged("DefinitionElement"); }
        }
        
        private List<Hl7.Fhir.Model.Markdown> _DefinitionElement;
        
        /// <summary>
        /// Defined terms used in the measure documentation
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Definition
        {
            get { return DefinitionElement != null ? DefinitionElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    DefinitionElement = null;
                else
                    DefinitionElement = new List<Hl7.Fhir.Model.Markdown>(value.Select(elem=>new Hl7.Fhir.Model.Markdown(elem)));
                OnPropertyChanged("Definition");
            }
        }
        
        /// <summary>
        /// Additional guidance for implementers
        /// </summary>
        [FhirElement("guidance", InSummary=Hl7.Fhir.Model.Version.All, Order=420)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown GuidanceElement
        {
            get { return _GuidanceElement; }
            set { _GuidanceElement = value; OnPropertyChanged("GuidanceElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _GuidanceElement;
        
        /// <summary>
        /// Additional guidance for implementers
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Guidance
        {
            get { return GuidanceElement != null ? GuidanceElement.Value : null; }
            set
            {
                if (value == null)
                    GuidanceElement = null;
                else
                    GuidanceElement = new Hl7.Fhir.Model.Markdown(value);
                OnPropertyChanged("Guidance");
            }
        }
        
        /// <summary>
        /// The measure set, e.g. Preventive Care and Screening
        /// </summary>
        [FhirElement("set", InSummary=Hl7.Fhir.Model.Version.All, Order=430)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirString SetElement
        {
            get { return _SetElement; }
            set { _SetElement = value; OnPropertyChanged("SetElement"); }
        }
        
        private Hl7.Fhir.Model.FhirString _SetElement;
        
        /// <summary>
        /// The measure set, e.g. Preventive Care and Screening
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Set
        {
            get { return SetElement != null ? SetElement.Value : null; }
            set
            {
                if (value == null)
                    SetElement = null;
                else
                    SetElement = new Hl7.Fhir.Model.FhirString(value);
                OnPropertyChanged("Set");
            }
        }
        
        /// <summary>
        /// Population criteria group
        /// </summary>
        [FhirElement("group", Order=440)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<GroupComponent> Group
        {
            get { if(_Group==null) _Group = new List<GroupComponent>(); return _Group; }
            set { _Group = value; OnPropertyChanged("Group"); }
        }
        
        private List<GroupComponent> _Group;
        
        /// <summary>
        /// What other data should be reported with the measure
        /// </summary>
        [FhirElement("supplementalData", Order=450)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<SupplementalDataComponent> SupplementalData
        {
            get { if(_SupplementalData==null) _SupplementalData = new List<SupplementalDataComponent>(); return _SupplementalData; }
            set { _SupplementalData = value; OnPropertyChanged("SupplementalData"); }
        }
        
        private List<SupplementalDataComponent> _SupplementalData;
    
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as Measure;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
                if(Identifier != null) dest.Identifier = new List<Hl7.Fhir.Model.Identifier>(Identifier.DeepCopy());
                if(VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
                if(NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
                if(TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
                if(StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.PublicationStatus>)StatusElement.DeepCopy();
                if(ExperimentalElement != null) dest.ExperimentalElement = (Hl7.Fhir.Model.FhirBoolean)ExperimentalElement.DeepCopy();
                if(DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
                if(PublisherElement != null) dest.PublisherElement = (Hl7.Fhir.Model.FhirString)PublisherElement.DeepCopy();
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.Markdown)DescriptionElement.DeepCopy();
                if(PurposeElement != null) dest.PurposeElement = (Hl7.Fhir.Model.Markdown)PurposeElement.DeepCopy();
                if(UsageElement != null) dest.UsageElement = (Hl7.Fhir.Model.FhirString)UsageElement.DeepCopy();
                if(ApprovalDateElement != null) dest.ApprovalDateElement = (Hl7.Fhir.Model.Date)ApprovalDateElement.DeepCopy();
                if(LastReviewDateElement != null) dest.LastReviewDateElement = (Hl7.Fhir.Model.Date)LastReviewDateElement.DeepCopy();
                if(EffectivePeriod != null) dest.EffectivePeriod = (Hl7.Fhir.Model.Period)EffectivePeriod.DeepCopy();
                if(UseContext != null) dest.UseContext = new List<Hl7.Fhir.Model.UsageContext>(UseContext.DeepCopy());
                if(Jurisdiction != null) dest.Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(Jurisdiction.DeepCopy());
                if(Topic != null) dest.Topic = new List<Hl7.Fhir.Model.CodeableConcept>(Topic.DeepCopy());
                if(Contributor != null) dest.Contributor = new List<Hl7.Fhir.Model.STU3.Contributor>(Contributor.DeepCopy());
                if(Contact != null) dest.Contact = new List<Hl7.Fhir.Model.STU3.ContactDetail>(Contact.DeepCopy());
                if(CopyrightElement != null) dest.CopyrightElement = (Hl7.Fhir.Model.Markdown)CopyrightElement.DeepCopy();
                if(RelatedArtifact != null) dest.RelatedArtifact = new List<Hl7.Fhir.Model.STU3.RelatedArtifact>(RelatedArtifact.DeepCopy());
                if(Library != null) dest.Library = new List<Hl7.Fhir.Model.ResourceReference>(Library.DeepCopy());
                if(DisclaimerElement != null) dest.DisclaimerElement = (Hl7.Fhir.Model.Markdown)DisclaimerElement.DeepCopy();
                if(Scoring != null) dest.Scoring = (Hl7.Fhir.Model.CodeableConcept)Scoring.DeepCopy();
                if(CompositeScoring != null) dest.CompositeScoring = (Hl7.Fhir.Model.CodeableConcept)CompositeScoring.DeepCopy();
                if(Type != null) dest.Type = new List<Hl7.Fhir.Model.CodeableConcept>(Type.DeepCopy());
                if(RiskAdjustmentElement != null) dest.RiskAdjustmentElement = (Hl7.Fhir.Model.FhirString)RiskAdjustmentElement.DeepCopy();
                if(RateAggregationElement != null) dest.RateAggregationElement = (Hl7.Fhir.Model.FhirString)RateAggregationElement.DeepCopy();
                if(RationaleElement != null) dest.RationaleElement = (Hl7.Fhir.Model.Markdown)RationaleElement.DeepCopy();
                if(ClinicalRecommendationStatementElement != null) dest.ClinicalRecommendationStatementElement = (Hl7.Fhir.Model.Markdown)ClinicalRecommendationStatementElement.DeepCopy();
                if(ImprovementNotationElement != null) dest.ImprovementNotationElement = (Hl7.Fhir.Model.FhirString)ImprovementNotationElement.DeepCopy();
                if(DefinitionElement != null) dest.DefinitionElement = new List<Hl7.Fhir.Model.Markdown>(DefinitionElement.DeepCopy());
                if(GuidanceElement != null) dest.GuidanceElement = (Hl7.Fhir.Model.Markdown)GuidanceElement.DeepCopy();
                if(SetElement != null) dest.SetElement = (Hl7.Fhir.Model.FhirString)SetElement.DeepCopy();
                if(Group != null) dest.Group = new List<GroupComponent>(Group.DeepCopy());
                if(SupplementalData != null) dest.SupplementalData = new List<SupplementalDataComponent>(SupplementalData.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new Measure());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as Measure;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.Matches(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.Matches(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(PurposeElement, otherT.PurposeElement)) return false;
            if( !DeepComparable.Matches(UsageElement, otherT.UsageElement)) return false;
            if( !DeepComparable.Matches(ApprovalDateElement, otherT.ApprovalDateElement)) return false;
            if( !DeepComparable.Matches(LastReviewDateElement, otherT.LastReviewDateElement)) return false;
            if( !DeepComparable.Matches(EffectivePeriod, otherT.EffectivePeriod)) return false;
            if( !DeepComparable.Matches(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.Matches(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.Matches(Topic, otherT.Topic)) return false;
            if( !DeepComparable.Matches(Contributor, otherT.Contributor)) return false;
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.Matches(RelatedArtifact, otherT.RelatedArtifact)) return false;
            if( !DeepComparable.Matches(Library, otherT.Library)) return false;
            if( !DeepComparable.Matches(DisclaimerElement, otherT.DisclaimerElement)) return false;
            if( !DeepComparable.Matches(Scoring, otherT.Scoring)) return false;
            if( !DeepComparable.Matches(CompositeScoring, otherT.CompositeScoring)) return false;
            if( !DeepComparable.Matches(Type, otherT.Type)) return false;
            if( !DeepComparable.Matches(RiskAdjustmentElement, otherT.RiskAdjustmentElement)) return false;
            if( !DeepComparable.Matches(RateAggregationElement, otherT.RateAggregationElement)) return false;
            if( !DeepComparable.Matches(RationaleElement, otherT.RationaleElement)) return false;
            if( !DeepComparable.Matches(ClinicalRecommendationStatementElement, otherT.ClinicalRecommendationStatementElement)) return false;
            if( !DeepComparable.Matches(ImprovementNotationElement, otherT.ImprovementNotationElement)) return false;
            if( !DeepComparable.Matches(DefinitionElement, otherT.DefinitionElement)) return false;
            if( !DeepComparable.Matches(GuidanceElement, otherT.GuidanceElement)) return false;
            if( !DeepComparable.Matches(SetElement, otherT.SetElement)) return false;
            if( !DeepComparable.Matches(Group, otherT.Group)) return false;
            if( !DeepComparable.Matches(SupplementalData, otherT.SupplementalData)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as Measure;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
            if( !DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if( !DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if( !DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if( !DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
            if( !DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if( !DeepComparable.IsExactly(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if( !DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if( !DeepComparable.IsExactly(PublisherElement, otherT.PublisherElement)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(PurposeElement, otherT.PurposeElement)) return false;
            if( !DeepComparable.IsExactly(UsageElement, otherT.UsageElement)) return false;
            if( !DeepComparable.IsExactly(ApprovalDateElement, otherT.ApprovalDateElement)) return false;
            if( !DeepComparable.IsExactly(LastReviewDateElement, otherT.LastReviewDateElement)) return false;
            if( !DeepComparable.IsExactly(EffectivePeriod, otherT.EffectivePeriod)) return false;
            if( !DeepComparable.IsExactly(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.IsExactly(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.IsExactly(Topic, otherT.Topic)) return false;
            if( !DeepComparable.IsExactly(Contributor, otherT.Contributor)) return false;
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.IsExactly(RelatedArtifact, otherT.RelatedArtifact)) return false;
            if( !DeepComparable.IsExactly(Library, otherT.Library)) return false;
            if( !DeepComparable.IsExactly(DisclaimerElement, otherT.DisclaimerElement)) return false;
            if( !DeepComparable.IsExactly(Scoring, otherT.Scoring)) return false;
            if( !DeepComparable.IsExactly(CompositeScoring, otherT.CompositeScoring)) return false;
            if( !DeepComparable.IsExactly(Type, otherT.Type)) return false;
            if( !DeepComparable.IsExactly(RiskAdjustmentElement, otherT.RiskAdjustmentElement)) return false;
            if( !DeepComparable.IsExactly(RateAggregationElement, otherT.RateAggregationElement)) return false;
            if( !DeepComparable.IsExactly(RationaleElement, otherT.RationaleElement)) return false;
            if( !DeepComparable.IsExactly(ClinicalRecommendationStatementElement, otherT.ClinicalRecommendationStatementElement)) return false;
            if( !DeepComparable.IsExactly(ImprovementNotationElement, otherT.ImprovementNotationElement)) return false;
            if( !DeepComparable.IsExactly(DefinitionElement, otherT.DefinitionElement)) return false;
            if( !DeepComparable.IsExactly(GuidanceElement, otherT.GuidanceElement)) return false;
            if( !DeepComparable.IsExactly(SetElement, otherT.SetElement)) return false;
            if( !DeepComparable.IsExactly(Group, otherT.Group)) return false;
            if( !DeepComparable.IsExactly(SupplementalData, otherT.SupplementalData)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("Measure");
            base.Serialize(sink);
            sink.Element("url", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); UrlElement?.Serialize(sink);
            sink.BeginList("identifier", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Identifier)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("version", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); VersionElement?.Serialize(sink);
            sink.Element("name", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); NameElement?.Serialize(sink);
            sink.Element("title", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); TitleElement?.Serialize(sink);
            sink.Element("status", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); StatusElement?.Serialize(sink);
            sink.Element("experimental", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ExperimentalElement?.Serialize(sink);
            sink.Element("date", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DateElement?.Serialize(sink);
            sink.Element("publisher", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PublisherElement?.Serialize(sink);
            sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DescriptionElement?.Serialize(sink);
            sink.Element("purpose", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PurposeElement?.Serialize(sink);
            sink.Element("usage", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); UsageElement?.Serialize(sink);
            sink.Element("approvalDate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); ApprovalDateElement?.Serialize(sink);
            sink.Element("lastReviewDate", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); LastReviewDateElement?.Serialize(sink);
            sink.Element("effectivePeriod", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); EffectivePeriod?.Serialize(sink);
            sink.BeginList("useContext", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in UseContext)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("jurisdiction", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Jurisdiction)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("topic", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Topic)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("contributor", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Contributor)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("contact", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Contact)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("copyright", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CopyrightElement?.Serialize(sink);
            sink.BeginList("relatedArtifact", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in RelatedArtifact)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("library", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Library)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("disclaimer", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DisclaimerElement?.Serialize(sink);
            sink.Element("scoring", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); Scoring?.Serialize(sink);
            sink.Element("compositeScoring", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CompositeScoring?.Serialize(sink);
            sink.BeginList("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Type)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("riskAdjustment", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); RiskAdjustmentElement?.Serialize(sink);
            sink.Element("rateAggregation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); RateAggregationElement?.Serialize(sink);
            sink.Element("rationale", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); RationaleElement?.Serialize(sink);
            sink.Element("clinicalRecommendationStatement", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ClinicalRecommendationStatementElement?.Serialize(sink);
            sink.Element("improvementNotation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ImprovementNotationElement?.Serialize(sink);
            sink.BeginList("definition", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            sink.Serialize(DefinitionElement);
            sink.End();
            sink.Element("guidance", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); GuidanceElement?.Serialize(sink);
            sink.Element("set", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); SetElement?.Serialize(sink);
            sink.BeginList("group", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Group)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("supplementalData", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in SupplementalData)
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
                case "url":
                    UrlElement = source.PopulateValue(UrlElement);
                    return true;
                case "_url":
                    UrlElement = source.Populate(UrlElement);
                    return true;
                case "identifier":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "version":
                    VersionElement = source.PopulateValue(VersionElement);
                    return true;
                case "_version":
                    VersionElement = source.Populate(VersionElement);
                    return true;
                case "name":
                    NameElement = source.PopulateValue(NameElement);
                    return true;
                case "_name":
                    NameElement = source.Populate(NameElement);
                    return true;
                case "title":
                    TitleElement = source.PopulateValue(TitleElement);
                    return true;
                case "_title":
                    TitleElement = source.Populate(TitleElement);
                    return true;
                case "status":
                    StatusElement = source.PopulateValue(StatusElement);
                    return true;
                case "_status":
                    StatusElement = source.Populate(StatusElement);
                    return true;
                case "experimental":
                    ExperimentalElement = source.PopulateValue(ExperimentalElement);
                    return true;
                case "_experimental":
                    ExperimentalElement = source.Populate(ExperimentalElement);
                    return true;
                case "date":
                    DateElement = source.PopulateValue(DateElement);
                    return true;
                case "_date":
                    DateElement = source.Populate(DateElement);
                    return true;
                case "publisher":
                    PublisherElement = source.PopulateValue(PublisherElement);
                    return true;
                case "_publisher":
                    PublisherElement = source.Populate(PublisherElement);
                    return true;
                case "description":
                    DescriptionElement = source.PopulateValue(DescriptionElement);
                    return true;
                case "_description":
                    DescriptionElement = source.Populate(DescriptionElement);
                    return true;
                case "purpose":
                    PurposeElement = source.PopulateValue(PurposeElement);
                    return true;
                case "_purpose":
                    PurposeElement = source.Populate(PurposeElement);
                    return true;
                case "usage":
                    UsageElement = source.PopulateValue(UsageElement);
                    return true;
                case "_usage":
                    UsageElement = source.Populate(UsageElement);
                    return true;
                case "approvalDate":
                    ApprovalDateElement = source.PopulateValue(ApprovalDateElement);
                    return true;
                case "_approvalDate":
                    ApprovalDateElement = source.Populate(ApprovalDateElement);
                    return true;
                case "lastReviewDate":
                    LastReviewDateElement = source.PopulateValue(LastReviewDateElement);
                    return true;
                case "_lastReviewDate":
                    LastReviewDateElement = source.Populate(LastReviewDateElement);
                    return true;
                case "effectivePeriod":
                    EffectivePeriod = source.Populate(EffectivePeriod);
                    return true;
                case "useContext":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "jurisdiction":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "topic":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "contributor":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "contact":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "copyright":
                    CopyrightElement = source.PopulateValue(CopyrightElement);
                    return true;
                case "_copyright":
                    CopyrightElement = source.Populate(CopyrightElement);
                    return true;
                case "relatedArtifact":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "library":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "disclaimer":
                    DisclaimerElement = source.PopulateValue(DisclaimerElement);
                    return true;
                case "_disclaimer":
                    DisclaimerElement = source.Populate(DisclaimerElement);
                    return true;
                case "scoring":
                    Scoring = source.Populate(Scoring);
                    return true;
                case "compositeScoring":
                    CompositeScoring = source.Populate(CompositeScoring);
                    return true;
                case "type":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "riskAdjustment":
                    RiskAdjustmentElement = source.PopulateValue(RiskAdjustmentElement);
                    return true;
                case "_riskAdjustment":
                    RiskAdjustmentElement = source.Populate(RiskAdjustmentElement);
                    return true;
                case "rateAggregation":
                    RateAggregationElement = source.PopulateValue(RateAggregationElement);
                    return true;
                case "_rateAggregation":
                    RateAggregationElement = source.Populate(RateAggregationElement);
                    return true;
                case "rationale":
                    RationaleElement = source.PopulateValue(RationaleElement);
                    return true;
                case "_rationale":
                    RationaleElement = source.Populate(RationaleElement);
                    return true;
                case "clinicalRecommendationStatement":
                    ClinicalRecommendationStatementElement = source.PopulateValue(ClinicalRecommendationStatementElement);
                    return true;
                case "_clinicalRecommendationStatement":
                    ClinicalRecommendationStatementElement = source.Populate(ClinicalRecommendationStatementElement);
                    return true;
                case "improvementNotation":
                    ImprovementNotationElement = source.PopulateValue(ImprovementNotationElement);
                    return true;
                case "_improvementNotation":
                    ImprovementNotationElement = source.Populate(ImprovementNotationElement);
                    return true;
                case "definition":
                case "_definition":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "guidance":
                    GuidanceElement = source.PopulateValue(GuidanceElement);
                    return true;
                case "_guidance":
                    GuidanceElement = source.Populate(GuidanceElement);
                    return true;
                case "set":
                    SetElement = source.PopulateValue(SetElement);
                    return true;
                case "_set":
                    SetElement = source.Populate(SetElement);
                    return true;
                case "group":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "supplementalData":
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
                case "identifier":
                    source.PopulateListItem(Identifier, index);
                    return true;
                case "useContext":
                    source.PopulateListItem(UseContext, index);
                    return true;
                case "jurisdiction":
                    source.PopulateListItem(Jurisdiction, index);
                    return true;
                case "topic":
                    source.PopulateListItem(Topic, index);
                    return true;
                case "contributor":
                    source.PopulateListItem(Contributor, index);
                    return true;
                case "contact":
                    source.PopulateListItem(Contact, index);
                    return true;
                case "relatedArtifact":
                    source.PopulateListItem(RelatedArtifact, index);
                    return true;
                case "library":
                    source.PopulateListItem(Library, index);
                    return true;
                case "type":
                    source.PopulateListItem(Type, index);
                    return true;
                case "definition":
                    source.PopulatePrimitiveListItemValue(DefinitionElement, index);
                    return true;
                case "_definition":
                    source.PopulatePrimitiveListItem(DefinitionElement, index);
                    return true;
                case "group":
                    source.PopulateListItem(Group, index);
                    return true;
                case "supplementalData":
                    source.PopulateListItem(SupplementalData, index);
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
                if (UrlElement != null) yield return UrlElement;
                foreach (var elem in Identifier) { if (elem != null) yield return elem; }
                if (VersionElement != null) yield return VersionElement;
                if (NameElement != null) yield return NameElement;
                if (TitleElement != null) yield return TitleElement;
                if (StatusElement != null) yield return StatusElement;
                if (ExperimentalElement != null) yield return ExperimentalElement;
                if (DateElement != null) yield return DateElement;
                if (PublisherElement != null) yield return PublisherElement;
                if (DescriptionElement != null) yield return DescriptionElement;
                if (PurposeElement != null) yield return PurposeElement;
                if (UsageElement != null) yield return UsageElement;
                if (ApprovalDateElement != null) yield return ApprovalDateElement;
                if (LastReviewDateElement != null) yield return LastReviewDateElement;
                if (EffectivePeriod != null) yield return EffectivePeriod;
                foreach (var elem in UseContext) { if (elem != null) yield return elem; }
                foreach (var elem in Jurisdiction) { if (elem != null) yield return elem; }
                foreach (var elem in Topic) { if (elem != null) yield return elem; }
                foreach (var elem in Contributor) { if (elem != null) yield return elem; }
                foreach (var elem in Contact) { if (elem != null) yield return elem; }
                if (CopyrightElement != null) yield return CopyrightElement;
                foreach (var elem in RelatedArtifact) { if (elem != null) yield return elem; }
                foreach (var elem in Library) { if (elem != null) yield return elem; }
                if (DisclaimerElement != null) yield return DisclaimerElement;
                if (Scoring != null) yield return Scoring;
                if (CompositeScoring != null) yield return CompositeScoring;
                foreach (var elem in Type) { if (elem != null) yield return elem; }
                if (RiskAdjustmentElement != null) yield return RiskAdjustmentElement;
                if (RateAggregationElement != null) yield return RateAggregationElement;
                if (RationaleElement != null) yield return RationaleElement;
                if (ClinicalRecommendationStatementElement != null) yield return ClinicalRecommendationStatementElement;
                if (ImprovementNotationElement != null) yield return ImprovementNotationElement;
                foreach (var elem in DefinitionElement) { if (elem != null) yield return elem; }
                if (GuidanceElement != null) yield return GuidanceElement;
                if (SetElement != null) yield return SetElement;
                foreach (var elem in Group) { if (elem != null) yield return elem; }
                foreach (var elem in SupplementalData) { if (elem != null) yield return elem; }
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
                if (ExperimentalElement != null) yield return new ElementValue("experimental", ExperimentalElement);
                if (DateElement != null) yield return new ElementValue("date", DateElement);
                if (PublisherElement != null) yield return new ElementValue("publisher", PublisherElement);
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                if (PurposeElement != null) yield return new ElementValue("purpose", PurposeElement);
                if (UsageElement != null) yield return new ElementValue("usage", UsageElement);
                if (ApprovalDateElement != null) yield return new ElementValue("approvalDate", ApprovalDateElement);
                if (LastReviewDateElement != null) yield return new ElementValue("lastReviewDate", LastReviewDateElement);
                if (EffectivePeriod != null) yield return new ElementValue("effectivePeriod", EffectivePeriod);
                foreach (var elem in UseContext) { if (elem != null) yield return new ElementValue("useContext", elem); }
                foreach (var elem in Jurisdiction) { if (elem != null) yield return new ElementValue("jurisdiction", elem); }
                foreach (var elem in Topic) { if (elem != null) yield return new ElementValue("topic", elem); }
                foreach (var elem in Contributor) { if (elem != null) yield return new ElementValue("contributor", elem); }
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                if (CopyrightElement != null) yield return new ElementValue("copyright", CopyrightElement);
                foreach (var elem in RelatedArtifact) { if (elem != null) yield return new ElementValue("relatedArtifact", elem); }
                foreach (var elem in Library) { if (elem != null) yield return new ElementValue("library", elem); }
                if (DisclaimerElement != null) yield return new ElementValue("disclaimer", DisclaimerElement);
                if (Scoring != null) yield return new ElementValue("scoring", Scoring);
                if (CompositeScoring != null) yield return new ElementValue("compositeScoring", CompositeScoring);
                foreach (var elem in Type) { if (elem != null) yield return new ElementValue("type", elem); }
                if (RiskAdjustmentElement != null) yield return new ElementValue("riskAdjustment", RiskAdjustmentElement);
                if (RateAggregationElement != null) yield return new ElementValue("rateAggregation", RateAggregationElement);
                if (RationaleElement != null) yield return new ElementValue("rationale", RationaleElement);
                if (ClinicalRecommendationStatementElement != null) yield return new ElementValue("clinicalRecommendationStatement", ClinicalRecommendationStatementElement);
                if (ImprovementNotationElement != null) yield return new ElementValue("improvementNotation", ImprovementNotationElement);
                foreach (var elem in DefinitionElement) { if (elem != null) yield return new ElementValue("definition", elem); }
                if (GuidanceElement != null) yield return new ElementValue("guidance", GuidanceElement);
                if (SetElement != null) yield return new ElementValue("set", SetElement);
                foreach (var elem in Group) { if (elem != null) yield return new ElementValue("group", elem); }
                foreach (var elem in SupplementalData) { if (elem != null) yield return new ElementValue("supplementalData", elem); }
            }
        }
    
    }

}
