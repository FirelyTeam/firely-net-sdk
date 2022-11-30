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
// Generated for FHIR v4.0.1
//
namespace Hl7.Fhir.Model.R4
{
    /// <summary>
    /// Describes a required data item
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "DataRequirement")]
    [DataContract]
    public partial class DataRequirement : Hl7.Fhir.Model.Element, Hl7.Fhir.Model.IDataRequirement, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override string TypeName { get { return "DataRequirement"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "CodeFilterComponent")]
        [DataContract]
        public partial class CodeFilterComponent : Hl7.Fhir.Model.Element, Hl7.Fhir.Model.IDataRequirementCodeFilterComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "CodeFilterComponent"; } }
            
            /// <summary>
            /// A code-valued attribute to filter on
            /// </summary>
            [FhirElement("path", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PathElement
            {
                get { return _PathElement; }
                set { _PathElement = value; OnPropertyChanged("PathElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PathElement;
            
            /// <summary>
            /// A code-valued attribute to filter on
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
            
            /// <summary>
            /// A coded (token) parameter to search on
            /// </summary>
            [FhirElement("searchParam", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString SearchParamElement
            {
                get { return _SearchParamElement; }
                set { _SearchParamElement = value; OnPropertyChanged("SearchParamElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _SearchParamElement;
            
            /// <summary>
            /// A coded (token) parameter to search on
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string SearchParam
            {
                get { return SearchParamElement != null ? SearchParamElement.Value : null; }
                set
                {
                    if (value == null)
                        SearchParamElement = null;
                    else
                        SearchParamElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("SearchParam");
                }
            }
            
            /// <summary>
            /// Valueset for the filter
            /// </summary>
            [FhirElement("valueSet", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.Canonical ValueSetElement
            {
                get { return _ValueSetElement; }
                set { _ValueSetElement = value; OnPropertyChanged("ValueSetElement"); }
            }
            
            private Hl7.Fhir.Model.Canonical _ValueSetElement;
            
            /// <summary>
            /// Valueset for the filter
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string ValueSet
            {
                get { return ValueSetElement != null ? ValueSetElement.Value : null; }
                set
                {
                    if (value == null)
                        ValueSetElement = null;
                    else
                        ValueSetElement = new Hl7.Fhir.Model.Canonical(value);
                    OnPropertyChanged("ValueSet");
                }
            }
            
            /// <summary>
            /// What code is expected
            /// </summary>
            [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<Hl7.Fhir.Model.Coding> Code
            {
                get { if(_Code==null) _Code = new List<Hl7.Fhir.Model.Coding>(); return _Code; }
                set { _Code = value; OnPropertyChanged("Code"); }
            }
            
            private List<Hl7.Fhir.Model.Coding> _Code;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("CodeFilterComponent");
                base.Serialize(sink);
                sink.Element("path", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PathElement?.Serialize(sink);
                sink.Element("searchParam", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); SearchParamElement?.Serialize(sink);
                sink.Element("valueSet", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ValueSetElement?.Serialize(sink);
                sink.BeginList("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
                foreach(var item in Code)
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
                    case "path":
                        PathElement = source.PopulateValue(PathElement);
                        return true;
                    case "_path":
                        PathElement = source.Populate(PathElement);
                        return true;
                    case "searchParam":
                        SearchParamElement = source.PopulateValue(SearchParamElement);
                        return true;
                    case "_searchParam":
                        SearchParamElement = source.Populate(SearchParamElement);
                        return true;
                    case "valueSet":
                        ValueSetElement = source.PopulateValue(ValueSetElement);
                        return true;
                    case "_valueSet":
                        ValueSetElement = source.Populate(ValueSetElement);
                        return true;
                    case "code":
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
                    case "code":
                        source.PopulateListItem(Code, index);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as CodeFilterComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(PathElement != null) dest.PathElement = (Hl7.Fhir.Model.FhirString)PathElement.DeepCopy();
                    if(SearchParamElement != null) dest.SearchParamElement = (Hl7.Fhir.Model.FhirString)SearchParamElement.DeepCopy();
                    if(ValueSetElement != null) dest.ValueSetElement = (Hl7.Fhir.Model.Canonical)ValueSetElement.DeepCopy();
                    if(Code != null) dest.Code = new List<Hl7.Fhir.Model.Coding>(Code.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new CodeFilterComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as CodeFilterComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.Matches(SearchParamElement, otherT.SearchParamElement)) return false;
                if( !DeepComparable.Matches(ValueSetElement, otherT.ValueSetElement)) return false;
                if( !DeepComparable.Matches(Code, otherT.Code)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as CodeFilterComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.IsExactly(SearchParamElement, otherT.SearchParamElement)) return false;
                if( !DeepComparable.IsExactly(ValueSetElement, otherT.ValueSetElement)) return false;
                if( !DeepComparable.IsExactly(Code, otherT.Code)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (PathElement != null) yield return PathElement;
                    if (SearchParamElement != null) yield return SearchParamElement;
                    if (ValueSetElement != null) yield return ValueSetElement;
                    foreach (var elem in Code) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (PathElement != null) yield return new ElementValue("path", PathElement);
                    if (SearchParamElement != null) yield return new ElementValue("searchParam", SearchParamElement);
                    if (ValueSetElement != null) yield return new ElementValue("valueSet", ValueSetElement);
                    foreach (var elem in Code) { if (elem != null) yield return new ElementValue("code", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "DateFilterComponent")]
        [DataContract]
        public partial class DateFilterComponent : Hl7.Fhir.Model.Element, Hl7.Fhir.Model.IDataRequirementDateFilterComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "DateFilterComponent"; } }
            
            /// <summary>
            /// A date-valued attribute to filter on
            /// </summary>
            [FhirElement("path", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PathElement
            {
                get { return _PathElement; }
                set { _PathElement = value; OnPropertyChanged("PathElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PathElement;
            
            /// <summary>
            /// A date-valued attribute to filter on
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
            
            /// <summary>
            /// A date valued parameter to search on
            /// </summary>
            [FhirElement("searchParam", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString SearchParamElement
            {
                get { return _SearchParamElement; }
                set { _SearchParamElement = value; OnPropertyChanged("SearchParamElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _SearchParamElement;
            
            /// <summary>
            /// A date valued parameter to search on
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string SearchParam
            {
                get { return SearchParamElement != null ? SearchParamElement.Value : null; }
                set
                {
                    if (value == null)
                        SearchParamElement = null;
                    else
                        SearchParamElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("SearchParam");
                }
            }
            
            /// <summary>
            /// The value of the filter, as a Period, DateTime, or Duration value
            /// </summary>
            [FhirElement("value", InSummary=Hl7.Fhir.Model.Version.All, Order=60, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.Period),typeof(Hl7.Fhir.Model.R4.Duration))]
            [DataMember]
            public Hl7.Fhir.Model.Element Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.Element _Value;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("DateFilterComponent");
                base.Serialize(sink);
                sink.Element("path", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); PathElement?.Serialize(sink);
                sink.Element("searchParam", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); SearchParamElement?.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Value?.Serialize(sink);
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
                    case "path":
                        PathElement = source.PopulateValue(PathElement);
                        return true;
                    case "_path":
                        PathElement = source.Populate(PathElement);
                        return true;
                    case "searchParam":
                        SearchParamElement = source.PopulateValue(SearchParamElement);
                        return true;
                    case "_searchParam":
                        SearchParamElement = source.Populate(SearchParamElement);
                        return true;
                    case "valueDateTime":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Value, "value");
                        Value = source.PopulateValue(Value as Hl7.Fhir.Model.FhirDateTime);
                        return true;
                    case "_valueDateTime":
                        source.CheckDuplicates<Hl7.Fhir.Model.FhirDateTime>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.FhirDateTime);
                        return true;
                    case "valuePeriod":
                        source.CheckDuplicates<Hl7.Fhir.Model.Period>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.Period);
                        return true;
                    case "valueDuration":
                        source.CheckDuplicates<Hl7.Fhir.Model.R4.Duration>(Value, "value");
                        Value = source.Populate(Value as Hl7.Fhir.Model.R4.Duration);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DateFilterComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(PathElement != null) dest.PathElement = (Hl7.Fhir.Model.FhirString)PathElement.DeepCopy();
                    if(SearchParamElement != null) dest.SearchParamElement = (Hl7.Fhir.Model.FhirString)SearchParamElement.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new DateFilterComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DateFilterComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.Matches(SearchParamElement, otherT.SearchParamElement)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DateFilterComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.IsExactly(SearchParamElement, otherT.SearchParamElement)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (PathElement != null) yield return PathElement;
                    if (SearchParamElement != null) yield return SearchParamElement;
                    if (Value != null) yield return Value;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (PathElement != null) yield return new ElementValue("path", PathElement);
                    if (SearchParamElement != null) yield return new ElementValue("searchParam", SearchParamElement);
                    if (Value != null) yield return new ElementValue("value", Value);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "SortComponent")]
        [DataContract]
        public partial class SortComponent : Hl7.Fhir.Model.Element, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "SortComponent"; } }
            
            /// <summary>
            /// The name of the attribute to perform the sort
            /// </summary>
            [FhirElement("path", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString PathElement
            {
                get { return _PathElement; }
                set { _PathElement = value; OnPropertyChanged("PathElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _PathElement;
            
            /// <summary>
            /// The name of the attribute to perform the sort
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
            
            /// <summary>
            /// ascending | descending
            /// </summary>
            [FhirElement("direction", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.R4.SortDirection> DirectionElement
            {
                get { return _DirectionElement; }
                set { _DirectionElement = value; OnPropertyChanged("DirectionElement"); }
            }
            
            private Code<Hl7.Fhir.Model.R4.SortDirection> _DirectionElement;
            
            /// <summary>
            /// ascending | descending
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.R4.SortDirection? Direction
            {
                get { return DirectionElement != null ? DirectionElement.Value : null; }
                set
                {
                    if (value == null)
                        DirectionElement = null;
                    else
                        DirectionElement = new Code<Hl7.Fhir.Model.R4.SortDirection>(value);
                    OnPropertyChanged("Direction");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("SortComponent");
                base.Serialize(sink);
                sink.Element("path", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); PathElement?.Serialize(sink);
                sink.Element("direction", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); DirectionElement?.Serialize(sink);
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
                    case "path":
                        PathElement = source.PopulateValue(PathElement);
                        return true;
                    case "_path":
                        PathElement = source.Populate(PathElement);
                        return true;
                    case "direction":
                        DirectionElement = source.PopulateValue(DirectionElement);
                        return true;
                    case "_direction":
                        DirectionElement = source.Populate(DirectionElement);
                        return true;
                }
                return false;
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as SortComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(PathElement != null) dest.PathElement = (Hl7.Fhir.Model.FhirString)PathElement.DeepCopy();
                    if(DirectionElement != null) dest.DirectionElement = (Code<Hl7.Fhir.Model.R4.SortDirection>)DirectionElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new SortComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as SortComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.Matches(DirectionElement, otherT.DirectionElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as SortComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(PathElement, otherT.PathElement)) return false;
                if( !DeepComparable.IsExactly(DirectionElement, otherT.DirectionElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (PathElement != null) yield return PathElement;
                    if (DirectionElement != null) yield return DirectionElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (PathElement != null) yield return new ElementValue("path", PathElement);
                    if (DirectionElement != null) yield return new ElementValue("direction", DirectionElement);
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IDataRequirementCodeFilterComponent> Hl7.Fhir.Model.IDataRequirement.CodeFilter { get { return CodeFilter; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IDataRequirementDateFilterComponent> Hl7.Fhir.Model.IDataRequirement.DateFilter { get { return DateFilter; } }
    
        
        /// <summary>
        /// The type of the required data
        /// </summary>
        [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=30)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.R4.FHIRAllTypes> TypeElement
        {
            get { return _TypeElement; }
            set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
        }
        
        private Code<Hl7.Fhir.Model.R4.FHIRAllTypes> _TypeElement;
        
        /// <summary>
        /// The type of the required data
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.R4.FHIRAllTypes? Type
        {
            get { return TypeElement != null ? TypeElement.Value : null; }
            set
            {
                if (value == null)
                    TypeElement = null;
                else
                    TypeElement = new Code<Hl7.Fhir.Model.R4.FHIRAllTypes>(value);
                OnPropertyChanged("Type");
            }
        }
        
        /// <summary>
        /// The profile of the required data
        /// </summary>
        [FhirElement("profile", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.Canonical> ProfileElement
        {
            get { if(_ProfileElement==null) _ProfileElement = new List<Hl7.Fhir.Model.Canonical>(); return _ProfileElement; }
            set { _ProfileElement = value; OnPropertyChanged("ProfileElement"); }
        }
        
        private List<Hl7.Fhir.Model.Canonical> _ProfileElement;
        
        /// <summary>
        /// The profile of the required data
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> Profile
        {
            get { return ProfileElement != null ? ProfileElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    ProfileElement = null;
                else
                    ProfileElement = new List<Hl7.Fhir.Model.Canonical>(value.Select(elem=>new Hl7.Fhir.Model.Canonical(elem)));
                OnPropertyChanged("Profile");
            }
        }
        
        /// <summary>
        /// E.g. Patient, Practitioner, RelatedPerson, Organization, Location, Device
        /// </summary>
        [FhirElement("subject", InSummary=Hl7.Fhir.Model.Version.All, Order=50, Choice=ChoiceType.DatatypeChoice)]
        [CLSCompliant(false)]
        [AllowedTypes(typeof(Hl7.Fhir.Model.CodeableConcept),typeof(Hl7.Fhir.Model.ResourceReference))]
        [DataMember]
        public Hl7.Fhir.Model.Element Subject
        {
            get { return _Subject; }
            set { _Subject = value; OnPropertyChanged("Subject"); }
        }
        
        private Hl7.Fhir.Model.Element _Subject;
        
        /// <summary>
        /// Indicates specific structure elements that are referenced by the knowledge module
        /// </summary>
        [FhirElement("mustSupport", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.FhirString> MustSupportElement
        {
            get { if(_MustSupportElement==null) _MustSupportElement = new List<Hl7.Fhir.Model.FhirString>(); return _MustSupportElement; }
            set { _MustSupportElement = value; OnPropertyChanged("MustSupportElement"); }
        }
        
        private List<Hl7.Fhir.Model.FhirString> _MustSupportElement;
        
        /// <summary>
        /// Indicates specific structure elements that are referenced by the knowledge module
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IEnumerable<string> MustSupport
        {
            get { return MustSupportElement != null ? MustSupportElement.Select(elem => elem.Value) : null; }
            set
            {
                if (value == null)
                    MustSupportElement = null;
                else
                    MustSupportElement = new List<Hl7.Fhir.Model.FhirString>(value.Select(elem=>new Hl7.Fhir.Model.FhirString(elem)));
                OnPropertyChanged("MustSupport");
            }
        }
        
        /// <summary>
        /// What codes are expected
        /// </summary>
        [FhirElement("codeFilter", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<CodeFilterComponent> CodeFilter
        {
            get { if(_CodeFilter==null) _CodeFilter = new List<CodeFilterComponent>(); return _CodeFilter; }
            set { _CodeFilter = value; OnPropertyChanged("CodeFilter"); }
        }
        
        private List<CodeFilterComponent> _CodeFilter;
        
        /// <summary>
        /// What dates/date ranges are expected
        /// </summary>
        [FhirElement("dateFilter", InSummary=Hl7.Fhir.Model.Version.All, Order=80)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<DateFilterComponent> DateFilter
        {
            get { if(_DateFilter==null) _DateFilter = new List<DateFilterComponent>(); return _DateFilter; }
            set { _DateFilter = value; OnPropertyChanged("DateFilter"); }
        }
        
        private List<DateFilterComponent> _DateFilter;
        
        /// <summary>
        /// Number of results
        /// </summary>
        [FhirElement("limit", InSummary=Hl7.Fhir.Model.Version.All, Order=90)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.PositiveInt LimitElement
        {
            get { return _LimitElement; }
            set { _LimitElement = value; OnPropertyChanged("LimitElement"); }
        }
        
        private Hl7.Fhir.Model.PositiveInt _LimitElement;
        
        /// <summary>
        /// Number of results
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? Limit
        {
            get { return LimitElement != null ? LimitElement.Value : null; }
            set
            {
                if (value == null)
                    LimitElement = null;
                else
                    LimitElement = new Hl7.Fhir.Model.PositiveInt(value);
                OnPropertyChanged("Limit");
            }
        }
        
        /// <summary>
        /// Order of the results
        /// </summary>
        [FhirElement("sort", InSummary=Hl7.Fhir.Model.Version.All, Order=100)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<SortComponent> Sort
        {
            get { if(_Sort==null) _Sort = new List<SortComponent>(); return _Sort; }
            set { _Sort = value; OnPropertyChanged("Sort"); }
        }
        
        private List<SortComponent> _Sort;
    
    
        public static ElementDefinitionConstraint[] DataRequirement_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "drq-1",
                severity: ConstraintSeverity.Warning,
                expression: "codeFilter.all(path.exists() xor searchParam.exists())",
                human: "Either a path or a searchParam must be provided, but not both",
                xpath: "(exists(f:path) and not(exists(f:searchParam))) or (not(exists(f:path)) and exists(f:searchParam))"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "drq-2",
                severity: ConstraintSeverity.Warning,
                expression: "dateFilter.all(path.exists() xor searchParam.exists())",
                human: "Either a path or a searchParam must be provided, but not both",
                xpath: "(exists(f:path) and not(exists(f:searchParam))) or (not(exists(f:path)) and exists(f:searchParam))"
            ),
        };
    
        // TODO: Add code to enforce the above constraints
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as DataRequirement;
        
            if (dest != null)
            {
                base.CopyTo(dest);
                if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.R4.FHIRAllTypes>)TypeElement.DeepCopy();
                if(ProfileElement != null) dest.ProfileElement = new List<Hl7.Fhir.Model.Canonical>(ProfileElement.DeepCopy());
                if(Subject != null) dest.Subject = (Hl7.Fhir.Model.Element)Subject.DeepCopy();
                if(MustSupportElement != null) dest.MustSupportElement = new List<Hl7.Fhir.Model.FhirString>(MustSupportElement.DeepCopy());
                if(CodeFilter != null) dest.CodeFilter = new List<CodeFilterComponent>(CodeFilter.DeepCopy());
                if(DateFilter != null) dest.DateFilter = new List<DateFilterComponent>(DateFilter.DeepCopy());
                if(LimitElement != null) dest.LimitElement = (Hl7.Fhir.Model.PositiveInt)LimitElement.DeepCopy();
                if(Sort != null) dest.Sort = new List<SortComponent>(Sort.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new DataRequirement());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as DataRequirement;
            if(otherT == null) return false;
        
            if(!base.Matches(otherT)) return false;
            if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.Matches(ProfileElement, otherT.ProfileElement)) return false;
            if( !DeepComparable.Matches(Subject, otherT.Subject)) return false;
            if( !DeepComparable.Matches(MustSupportElement, otherT.MustSupportElement)) return false;
            if( !DeepComparable.Matches(CodeFilter, otherT.CodeFilter)) return false;
            if( !DeepComparable.Matches(DateFilter, otherT.DateFilter)) return false;
            if( !DeepComparable.Matches(LimitElement, otherT.LimitElement)) return false;
            if( !DeepComparable.Matches(Sort, otherT.Sort)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as DataRequirement;
            if(otherT == null) return false;
        
            if(!base.IsExactly(otherT)) return false;
            if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            if( !DeepComparable.IsExactly(ProfileElement, otherT.ProfileElement)) return false;
            if( !DeepComparable.IsExactly(Subject, otherT.Subject)) return false;
            if( !DeepComparable.IsExactly(MustSupportElement, otherT.MustSupportElement)) return false;
            if( !DeepComparable.IsExactly(CodeFilter, otherT.CodeFilter)) return false;
            if( !DeepComparable.IsExactly(DateFilter, otherT.DateFilter)) return false;
            if( !DeepComparable.IsExactly(LimitElement, otherT.LimitElement)) return false;
            if( !DeepComparable.IsExactly(Sort, otherT.Sort)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginDataType("DataRequirement");
            base.Serialize(sink);
            sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); TypeElement?.Serialize(sink);
            sink.BeginList("profile", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            sink.Serialize(ProfileElement);
            sink.End();
            sink.Element("subject", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, true); Subject?.Serialize(sink);
            sink.BeginList("mustSupport", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            sink.Serialize(MustSupportElement);
            sink.End();
            sink.BeginList("codeFilter", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in CodeFilter)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("dateFilter", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in DateFilter)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("limit", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); LimitElement?.Serialize(sink);
            sink.BeginList("sort", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Sort)
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
                case "type":
                    TypeElement = source.PopulateValue(TypeElement);
                    return true;
                case "_type":
                    TypeElement = source.Populate(TypeElement);
                    return true;
                case "profile":
                case "_profile":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "subjectCodeableConcept":
                    source.CheckDuplicates<Hl7.Fhir.Model.CodeableConcept>(Subject, "subject");
                    Subject = source.Populate(Subject as Hl7.Fhir.Model.CodeableConcept);
                    return true;
                case "subjectReference":
                    source.CheckDuplicates<Hl7.Fhir.Model.ResourceReference>(Subject, "subject");
                    Subject = source.Populate(Subject as Hl7.Fhir.Model.ResourceReference);
                    return true;
                case "mustSupport":
                case "_mustSupport":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "codeFilter":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "dateFilter":
                    source.SetList(this, jsonPropertyName);
                    return true;
                case "limit":
                    LimitElement = source.PopulateValue(LimitElement);
                    return true;
                case "_limit":
                    LimitElement = source.Populate(LimitElement);
                    return true;
                case "sort":
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
                case "profile":
                    source.PopulatePrimitiveListItemValue(ProfileElement, index);
                    return true;
                case "_profile":
                    source.PopulatePrimitiveListItem(ProfileElement, index);
                    return true;
                case "mustSupport":
                    source.PopulatePrimitiveListItemValue(MustSupportElement, index);
                    return true;
                case "_mustSupport":
                    source.PopulatePrimitiveListItem(MustSupportElement, index);
                    return true;
                case "codeFilter":
                    source.PopulateListItem(CodeFilter, index);
                    return true;
                case "dateFilter":
                    source.PopulateListItem(DateFilter, index);
                    return true;
                case "sort":
                    source.PopulateListItem(Sort, index);
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
                if (TypeElement != null) yield return TypeElement;
                foreach (var elem in ProfileElement) { if (elem != null) yield return elem; }
                if (Subject != null) yield return Subject;
                foreach (var elem in MustSupportElement) { if (elem != null) yield return elem; }
                foreach (var elem in CodeFilter) { if (elem != null) yield return elem; }
                foreach (var elem in DateFilter) { if (elem != null) yield return elem; }
                if (LimitElement != null) yield return LimitElement;
                foreach (var elem in Sort) { if (elem != null) yield return elem; }
            }
        }
        
        [NotMapped]
        internal override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                foreach (var elem in ProfileElement) { if (elem != null) yield return new ElementValue("profile", elem); }
                if (Subject != null) yield return new ElementValue("subject", Subject);
                foreach (var elem in MustSupportElement) { if (elem != null) yield return new ElementValue("mustSupport", elem); }
                foreach (var elem in CodeFilter) { if (elem != null) yield return new ElementValue("codeFilter", elem); }
                foreach (var elem in DateFilter) { if (elem != null) yield return new ElementValue("dateFilter", elem); }
                if (LimitElement != null) yield return new ElementValue("limit", LimitElement);
                foreach (var elem in Sort) { if (elem != null) yield return new ElementValue("sort", elem); }
            }
        }
    
    }

}
