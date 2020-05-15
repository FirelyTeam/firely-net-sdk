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
    /// Declares the existence of and describes a code system or code system supplement
    /// </summary>
    [FhirType(Hl7.Fhir.Model.Version.R4, "CodeSystem", IsResource=true)]
    [DataContract]
    public partial class CodeSystem : Hl7.Fhir.Model.DomainResource, Hl7.Fhir.Model.ICodeSystem, System.ComponentModel.INotifyPropertyChanged
    {
        [NotMapped]
        public override ResourceType ResourceType { get { return ResourceType.CodeSystem; } }
        [NotMapped]
        public override string TypeName { get { return "CodeSystem"; } }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "FilterComponent")]
        [DataContract]
        public partial class FilterComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ICodeSystemFilterComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "FilterComponent"; } }
            
            /// <summary>
            /// Code that identifies the filter
            /// </summary>
            [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Hl7.Fhir.Model.Code _CodeElement;
            
            /// <summary>
            /// Code that identifies the filter
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if (value == null)
                        CodeElement = null;
                    else
                        CodeElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// How or why the filter is used
            /// </summary>
            [FhirElement("description", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// How or why the filter is used
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
            /// = | is-a | descendent-of | is-not-a | regex | in | not-in | generalizes | exists
            /// </summary>
            [FhirElement("operator", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=-1)]
            [DataMember]
            public List<Code<Hl7.Fhir.Model.R4.FilterOperator>> OperatorElement
            {
                get { if(_OperatorElement==null) _OperatorElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.FilterOperator>>(); return _OperatorElement; }
                set { _OperatorElement = value; OnPropertyChanged("OperatorElement"); }
            }
            
            private List<Code<Hl7.Fhir.Model.R4.FilterOperator>> _OperatorElement;
            
            /// <summary>
            /// = | is-a | descendent-of | is-not-a | regex | in | not-in | generalizes | exists
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public IEnumerable<Hl7.Fhir.Model.R4.FilterOperator?> Operator
            {
                get { return OperatorElement != null ? OperatorElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        OperatorElement = null;
                    else
                        OperatorElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.FilterOperator>>(value.Select(elem=>new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.R4.FilterOperator>(elem)));
                    OnPropertyChanged("Operator");
                }
            }
            
            /// <summary>
            /// What to use for the value
            /// </summary>
            [FhirElement("value", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ValueElement;
            
            /// <summary>
            /// What to use for the value
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Value
            {
                get { return ValueElement != null ? ValueElement.Value : null; }
                set
                {
                    if (value == null)
                        ValueElement = null;
                    else
                        ValueElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Value");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("FilterComponent");
                base.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); CodeElement?.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DescriptionElement?.Serialize(sink);
                sink.BeginList("operator", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true);
                sink.Serialize(OperatorElement);
                sink.End();
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); ValueElement?.Serialize(sink);
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as FilterComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(OperatorElement != null) dest.OperatorElement = new List<Code<Hl7.Fhir.Model.R4.FilterOperator>>(OperatorElement.DeepCopy());
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirString)ValueElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new FilterComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as FilterComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(OperatorElement, otherT.OperatorElement)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as FilterComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(OperatorElement, otherT.OperatorElement)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (CodeElement != null) yield return CodeElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    foreach (var elem in OperatorElement) { if (elem != null) yield return elem; }
                    if (ValueElement != null) yield return ValueElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    foreach (var elem in OperatorElement) { if (elem != null) yield return new ElementValue("operator", elem); }
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "PropertyComponent")]
        [DataContract]
        public partial class PropertyComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ICodeSystemPropertyComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "PropertyComponent"; } }
            
            /// <summary>
            /// Identifies the property on the concepts, and when referred to in operations
            /// </summary>
            [FhirElement("code", InSummary=Hl7.Fhir.Model.Version.All, Order=40)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Hl7.Fhir.Model.Code _CodeElement;
            
            /// <summary>
            /// Identifies the property on the concepts, and when referred to in operations
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if (value == null)
                        CodeElement = null;
                    else
                        CodeElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Formal identifier for the property
            /// </summary>
            [FhirElement("uri", InSummary=Hl7.Fhir.Model.Version.All, Order=50)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirUri UriElement
            {
                get { return _UriElement; }
                set { _UriElement = value; OnPropertyChanged("UriElement"); }
            }
            
            private Hl7.Fhir.Model.FhirUri _UriElement;
            
            /// <summary>
            /// Formal identifier for the property
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Uri
            {
                get { return UriElement != null ? UriElement.Value : null; }
                set
                {
                    if (value == null)
                        UriElement = null;
                    else
                        UriElement = new Hl7.Fhir.Model.FhirUri(value);
                    OnPropertyChanged("Uri");
                }
            }
            
            /// <summary>
            /// Why the property is defined, and/or what it conveys
            /// </summary>
            [FhirElement("description", InSummary=Hl7.Fhir.Model.Version.All, Order=60)]
            [CLSCompliant(false)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DescriptionElement
            {
                get { return _DescriptionElement; }
                set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DescriptionElement;
            
            /// <summary>
            /// Why the property is defined, and/or what it conveys
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
            /// code | Coding | string | integer | boolean | dateTime | decimal
            /// </summary>
            [FhirElement("type", InSummary=Hl7.Fhir.Model.Version.All, Order=70)]
            [CLSCompliant(false)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.R4.PropertyType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }
            
            private Code<Hl7.Fhir.Model.R4.PropertyType> _TypeElement;
            
            /// <summary>
            /// code | Coding | string | integer | boolean | dateTime | decimal
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public Hl7.Fhir.Model.R4.PropertyType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (value == null)
                        TypeElement = null;
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.R4.PropertyType>(value);
                    OnPropertyChanged("Type");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("PropertyComponent");
                base.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); CodeElement?.Serialize(sink);
                sink.Element("uri", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); UriElement?.Serialize(sink);
                sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); DescriptionElement?.Serialize(sink);
                sink.Element("type", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); TypeElement?.Serialize(sink);
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PropertyComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
                    if(UriElement != null) dest.UriElement = (Hl7.Fhir.Model.FhirUri)UriElement.DeepCopy();
                    if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                    if(TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.R4.PropertyType>)TypeElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new PropertyComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PropertyComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(UriElement, otherT.UriElement)) return false;
                if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PropertyComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(UriElement, otherT.UriElement)) return false;
                if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if( !DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (CodeElement != null) yield return CodeElement;
                    if (UriElement != null) yield return UriElement;
                    if (DescriptionElement != null) yield return DescriptionElement;
                    if (TypeElement != null) yield return TypeElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                    if (UriElement != null) yield return new ElementValue("uri", UriElement);
                    if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                    if (TypeElement != null) yield return new ElementValue("type", TypeElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ConceptDefinitionComponent")]
        [DataContract]
        public partial class ConceptDefinitionComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ICodeSystemConceptDefinitionComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ConceptDefinitionComponent"; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.ICodeSystemDesignationComponent> Hl7.Fhir.Model.ICodeSystemConceptDefinitionComponent.Designation { get { return Designation; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.ICodeSystemConceptPropertyComponent> Hl7.Fhir.Model.ICodeSystemConceptDefinitionComponent.Property { get { return Property; } }
            
            [NotMapped]
            IEnumerable<Hl7.Fhir.Model.ICodeSystemConceptDefinitionComponent> Hl7.Fhir.Model.ICodeSystemConceptDefinitionComponent.Concept { get { return Concept; } }
            
            /// <summary>
            /// Code that identifies concept
            /// </summary>
            [FhirElement("code", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Hl7.Fhir.Model.Code _CodeElement;
            
            /// <summary>
            /// Code that identifies concept
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if (value == null)
                        CodeElement = null;
                    else
                        CodeElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Text to display to the user
            /// </summary>
            [FhirElement("display", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DisplayElement
            {
                get { return _DisplayElement; }
                set { _DisplayElement = value; OnPropertyChanged("DisplayElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DisplayElement;
            
            /// <summary>
            /// Text to display to the user
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Display
            {
                get { return DisplayElement != null ? DisplayElement.Value : null; }
                set
                {
                    if (value == null)
                        DisplayElement = null;
                    else
                        DisplayElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Display");
                }
            }
            
            /// <summary>
            /// Formal definition
            /// </summary>
            [FhirElement("definition", Order=60)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString DefinitionElement
            {
                get { return _DefinitionElement; }
                set { _DefinitionElement = value; OnPropertyChanged("DefinitionElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _DefinitionElement;
            
            /// <summary>
            /// Formal definition
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Definition
            {
                get { return DefinitionElement != null ? DefinitionElement.Value : null; }
                set
                {
                    if (value == null)
                        DefinitionElement = null;
                    else
                        DefinitionElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Definition");
                }
            }
            
            /// <summary>
            /// Additional representations for the concept
            /// </summary>
            [FhirElement("designation", Order=70)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<DesignationComponent> Designation
            {
                get { if(_Designation==null) _Designation = new List<DesignationComponent>(); return _Designation; }
                set { _Designation = value; OnPropertyChanged("Designation"); }
            }
            
            private List<DesignationComponent> _Designation;
            
            /// <summary>
            /// Property value for the concept
            /// </summary>
            [FhirElement("property", Order=80)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<ConceptPropertyComponent> Property
            {
                get { if(_Property==null) _Property = new List<ConceptPropertyComponent>(); return _Property; }
                set { _Property = value; OnPropertyChanged("Property"); }
            }
            
            private List<ConceptPropertyComponent> _Property;
            
            /// <summary>
            /// Child Concepts (is-a/contains/categorizes)
            /// </summary>
            [FhirElement("concept", Order=90)]
            [Cardinality(Min=0,Max=-1)]
            [DataMember]
            public List<ConceptDefinitionComponent> Concept
            {
                get { if(_Concept==null) _Concept = new List<ConceptDefinitionComponent>(); return _Concept; }
                set { _Concept = value; OnPropertyChanged("Concept"); }
            }
            
            private List<ConceptDefinitionComponent> _Concept;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ConceptDefinitionComponent");
                base.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); CodeElement?.Serialize(sink);
                sink.Element("display", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DisplayElement?.Serialize(sink);
                sink.Element("definition", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DefinitionElement?.Serialize(sink);
                sink.BeginList("designation", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Designation)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("property", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Property)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.BeginList("concept", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
                foreach(var item in Concept)
                {
                    item?.Serialize(sink);
                }
                sink.End();
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ConceptDefinitionComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
                    if(DisplayElement != null) dest.DisplayElement = (Hl7.Fhir.Model.FhirString)DisplayElement.DeepCopy();
                    if(DefinitionElement != null) dest.DefinitionElement = (Hl7.Fhir.Model.FhirString)DefinitionElement.DeepCopy();
                    if(Designation != null) dest.Designation = new List<DesignationComponent>(Designation.DeepCopy());
                    if(Property != null) dest.Property = new List<ConceptPropertyComponent>(Property.DeepCopy());
                    if(Concept != null) dest.Concept = new List<ConceptDefinitionComponent>(Concept.DeepCopy());
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ConceptDefinitionComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ConceptDefinitionComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(DisplayElement, otherT.DisplayElement)) return false;
                if( !DeepComparable.Matches(DefinitionElement, otherT.DefinitionElement)) return false;
                if( !DeepComparable.Matches(Designation, otherT.Designation)) return false;
                if( !DeepComparable.Matches(Property, otherT.Property)) return false;
                if( !DeepComparable.Matches(Concept, otherT.Concept)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ConceptDefinitionComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(DisplayElement, otherT.DisplayElement)) return false;
                if( !DeepComparable.IsExactly(DefinitionElement, otherT.DefinitionElement)) return false;
                if( !DeepComparable.IsExactly(Designation, otherT.Designation)) return false;
                if( !DeepComparable.IsExactly(Property, otherT.Property)) return false;
                if( !DeepComparable.IsExactly(Concept, otherT.Concept)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (CodeElement != null) yield return CodeElement;
                    if (DisplayElement != null) yield return DisplayElement;
                    if (DefinitionElement != null) yield return DefinitionElement;
                    foreach (var elem in Designation) { if (elem != null) yield return elem; }
                    foreach (var elem in Property) { if (elem != null) yield return elem; }
                    foreach (var elem in Concept) { if (elem != null) yield return elem; }
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                    if (DisplayElement != null) yield return new ElementValue("display", DisplayElement);
                    if (DefinitionElement != null) yield return new ElementValue("definition", DefinitionElement);
                    foreach (var elem in Designation) { if (elem != null) yield return new ElementValue("designation", elem); }
                    foreach (var elem in Property) { if (elem != null) yield return new ElementValue("property", elem); }
                    foreach (var elem in Concept) { if (elem != null) yield return new ElementValue("concept", elem); }
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "DesignationComponent")]
        [DataContract]
        public partial class DesignationComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ICodeSystemDesignationComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "DesignationComponent"; } }
            
            /// <summary>
            /// Human language of the designation
            /// </summary>
            [FhirElement("language", Order=40)]
            [DataMember]
            public Hl7.Fhir.Model.Code LanguageElement
            {
                get { return _LanguageElement; }
                set { _LanguageElement = value; OnPropertyChanged("LanguageElement"); }
            }
            
            private Hl7.Fhir.Model.Code _LanguageElement;
            
            /// <summary>
            /// Human language of the designation
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Language
            {
                get { return LanguageElement != null ? LanguageElement.Value : null; }
                set
                {
                    if (value == null)
                        LanguageElement = null;
                    else
                        LanguageElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Language");
                }
            }
            
            /// <summary>
            /// Details how this designation would be used
            /// </summary>
            [FhirElement("use", Order=50)]
            [DataMember]
            public Hl7.Fhir.Model.Coding Use
            {
                get { return _Use; }
                set { _Use = value; OnPropertyChanged("Use"); }
            }
            
            private Hl7.Fhir.Model.Coding _Use;
            
            /// <summary>
            /// The text value for this designation
            /// </summary>
            [FhirElement("value", Order=60)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.FhirString ValueElement
            {
                get { return _ValueElement; }
                set { _ValueElement = value; OnPropertyChanged("ValueElement"); }
            }
            
            private Hl7.Fhir.Model.FhirString _ValueElement;
            
            /// <summary>
            /// The text value for this designation
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Value
            {
                get { return ValueElement != null ? ValueElement.Value : null; }
                set
                {
                    if (value == null)
                        ValueElement = null;
                    else
                        ValueElement = new Hl7.Fhir.Model.FhirString(value);
                    OnPropertyChanged("Value");
                }
            }
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("DesignationComponent");
                base.Serialize(sink);
                sink.Element("language", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); LanguageElement?.Serialize(sink);
                sink.Element("use", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); Use?.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); ValueElement?.Serialize(sink);
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DesignationComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(LanguageElement != null) dest.LanguageElement = (Hl7.Fhir.Model.Code)LanguageElement.DeepCopy();
                    if(Use != null) dest.Use = (Hl7.Fhir.Model.Coding)Use.DeepCopy();
                    if(ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirString)ValueElement.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new DesignationComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DesignationComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(LanguageElement, otherT.LanguageElement)) return false;
                if( !DeepComparable.Matches(Use, otherT.Use)) return false;
                if( !DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DesignationComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(LanguageElement, otherT.LanguageElement)) return false;
                if( !DeepComparable.IsExactly(Use, otherT.Use)) return false;
                if( !DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (LanguageElement != null) yield return LanguageElement;
                    if (Use != null) yield return Use;
                    if (ValueElement != null) yield return ValueElement;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (LanguageElement != null) yield return new ElementValue("language", LanguageElement);
                    if (Use != null) yield return new ElementValue("use", Use);
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                }
            }
        
        
        }
    
    
        [FhirType(Hl7.Fhir.Model.Version.R4, "ConceptPropertyComponent")]
        [DataContract]
        public partial class ConceptPropertyComponent : Hl7.Fhir.Model.BackboneElement, Hl7.Fhir.Model.ICodeSystemConceptPropertyComponent, System.ComponentModel.INotifyPropertyChanged, IComponent
        {
            [NotMapped]
            public override string TypeName { get { return "ConceptPropertyComponent"; } }
            
            /// <summary>
            /// Reference to CodeSystem.property.code
            /// </summary>
            [FhirElement("code", Order=40)]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Code CodeElement
            {
                get { return _CodeElement; }
                set { _CodeElement = value; OnPropertyChanged("CodeElement"); }
            }
            
            private Hl7.Fhir.Model.Code _CodeElement;
            
            /// <summary>
            /// Reference to CodeSystem.property.code
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [NotMapped]
            [IgnoreDataMemberAttribute]
            public string Code
            {
                get { return CodeElement != null ? CodeElement.Value : null; }
                set
                {
                    if (value == null)
                        CodeElement = null;
                    else
                        CodeElement = new Hl7.Fhir.Model.Code(value);
                    OnPropertyChanged("Code");
                }
            }
            
            /// <summary>
            /// Value of the property for this concept
            /// </summary>
            [FhirElement("value", Order=50, Choice=ChoiceType.DatatypeChoice)]
            [CLSCompliant(false)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Code),typeof(Hl7.Fhir.Model.Coding),typeof(Hl7.Fhir.Model.FhirString),typeof(Hl7.Fhir.Model.Integer),typeof(Hl7.Fhir.Model.FhirBoolean),typeof(Hl7.Fhir.Model.FhirDateTime),typeof(Hl7.Fhir.Model.FhirDecimal))]
            [Cardinality(Min=1,Max=1)]
            [DataMember]
            public Hl7.Fhir.Model.Element Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }
            
            private Hl7.Fhir.Model.Element _Value;
        
            internal override void Serialize(Serialization.SerializerSink sink)
            {
                sink.BeginDataType("ConceptPropertyComponent");
                base.Serialize(sink);
                sink.Element("code", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, false); CodeElement?.Serialize(sink);
                sink.Element("value", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, true, true); Value?.Serialize(sink);
                sink.End();
            }
        
            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ConceptPropertyComponent;
            
                if (dest != null)
                {
                    base.CopyTo(dest);
                    if(CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
                    if(Value != null) dest.Value = (Hl7.Fhir.Model.Element)Value.DeepCopy();
                    return dest;
                }
                else
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
            }
            
            public override IDeepCopyable DeepCopy()
            {
                 return CopyTo(new ConceptPropertyComponent());
            }
            
            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ConceptPropertyComponent;
                if(otherT == null) return false;
            
                if(!base.Matches(otherT)) return false;
                if( !DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.Matches(Value, otherT.Value)) return false;
            
                return true;
            }
            
            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ConceptPropertyComponent;
                if(otherT == null) return false;
            
                if(!base.IsExactly(otherT)) return false;
                if( !DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if( !DeepComparable.IsExactly(Value, otherT.Value)) return false;
            
                return true;
            }
        
        
            [NotMapped]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (CodeElement != null) yield return CodeElement;
                    if (Value != null) yield return Value;
                }
            }
            
            [NotMapped]
            internal override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                    if (Value != null) yield return new ElementValue("value", Value);
                }
            }
        
        
        }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.IContactDetail> Hl7.Fhir.Model.ICodeSystem.Contact { get { return Contact; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.ICodeSystemFilterComponent> Hl7.Fhir.Model.ICodeSystem.Filter { get { return Filter; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.ICodeSystemPropertyComponent> Hl7.Fhir.Model.ICodeSystem.Property { get { return Property; } }
        
        [NotMapped]
        IEnumerable<Hl7.Fhir.Model.ICodeSystemConceptDefinitionComponent> Hl7.Fhir.Model.ICodeSystem.Concept { get { return Concept; } }
    
        
        /// <summary>
        /// Canonical identifier for this code system, represented as a URI (globally unique) (Coding.system)
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
        /// Canonical identifier for this code system, represented as a URI (globally unique) (Coding.system)
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
        /// Additional identifier for the code system (business identifier)
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
        /// Business version of the code system (Coding.version)
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
        /// Business version of the code system (Coding.version)
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
        /// Name for this code system (computer friendly)
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
        /// Name for this code system (computer friendly)
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
        /// Name for this code system (human friendly)
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
        /// Name for this code system (human friendly)
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
        /// Date last changed
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
        /// Contact details for the publisher
        /// </summary>
        [FhirElement("contact", InSummary=Hl7.Fhir.Model.Version.All, Order=180)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<Hl7.Fhir.Model.R4.ContactDetail> Contact
        {
            get { if(_Contact==null) _Contact = new List<Hl7.Fhir.Model.R4.ContactDetail>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }
        
        private List<Hl7.Fhir.Model.R4.ContactDetail> _Contact;
        
        /// <summary>
        /// Natural language description of the code system
        /// </summary>
        [FhirElement("description", Order=190)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown DescriptionElement
        {
            get { return _DescriptionElement; }
            set { _DescriptionElement = value; OnPropertyChanged("DescriptionElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _DescriptionElement;
        
        /// <summary>
        /// Natural language description of the code system
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
        /// The context that the content is intended to support
        /// </summary>
        [FhirElement("useContext", InSummary=Hl7.Fhir.Model.Version.All, Order=200)]
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
        /// Intended jurisdiction for code system (if applicable)
        /// </summary>
        [FhirElement("jurisdiction", InSummary=Hl7.Fhir.Model.Version.All, Order=210)]
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
        /// Why this code system is defined
        /// </summary>
        [FhirElement("purpose", Order=220)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown PurposeElement
        {
            get { return _PurposeElement; }
            set { _PurposeElement = value; OnPropertyChanged("PurposeElement"); }
        }
        
        private Hl7.Fhir.Model.Markdown _PurposeElement;
        
        /// <summary>
        /// Why this code system is defined
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
        /// Use and/or publishing restrictions
        /// </summary>
        [FhirElement("copyright", Order=230)]
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
        /// If code comparison is case sensitive
        /// </summary>
        [FhirElement("caseSensitive", InSummary=Hl7.Fhir.Model.Version.All, Order=240)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean CaseSensitiveElement
        {
            get { return _CaseSensitiveElement; }
            set { _CaseSensitiveElement = value; OnPropertyChanged("CaseSensitiveElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _CaseSensitiveElement;
        
        /// <summary>
        /// If code comparison is case sensitive
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? CaseSensitive
        {
            get { return CaseSensitiveElement != null ? CaseSensitiveElement.Value : null; }
            set
            {
                if (value == null)
                    CaseSensitiveElement = null;
                else
                    CaseSensitiveElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("CaseSensitive");
            }
        }
        
        /// <summary>
        /// Canonical reference to the value set with entire code system
        /// </summary>
        [FhirElement("valueSet", InSummary=Hl7.Fhir.Model.Version.All, Order=250)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Canonical ValueSetElement
        {
            get { return _ValueSetElement; }
            set { _ValueSetElement = value; OnPropertyChanged("ValueSetElement"); }
        }
        
        private Hl7.Fhir.Model.Canonical _ValueSetElement;
        
        /// <summary>
        /// Canonical reference to the value set with entire code system
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
        /// grouped-by | is-a | part-of | classified-with
        /// </summary>
        [FhirElement("hierarchyMeaning", InSummary=Hl7.Fhir.Model.Version.All, Order=260)]
        [CLSCompliant(false)]
        [DataMember]
        public Code<Hl7.Fhir.Model.CodeSystemHierarchyMeaning> HierarchyMeaningElement
        {
            get { return _HierarchyMeaningElement; }
            set { _HierarchyMeaningElement = value; OnPropertyChanged("HierarchyMeaningElement"); }
        }
        
        private Code<Hl7.Fhir.Model.CodeSystemHierarchyMeaning> _HierarchyMeaningElement;
        
        /// <summary>
        /// grouped-by | is-a | part-of | classified-with
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.CodeSystemHierarchyMeaning? HierarchyMeaning
        {
            get { return HierarchyMeaningElement != null ? HierarchyMeaningElement.Value : null; }
            set
            {
                if (value == null)
                    HierarchyMeaningElement = null;
                else
                    HierarchyMeaningElement = new Code<Hl7.Fhir.Model.CodeSystemHierarchyMeaning>(value);
                OnPropertyChanged("HierarchyMeaning");
            }
        }
        
        /// <summary>
        /// If code system defines a compositional grammar
        /// </summary>
        [FhirElement("compositional", InSummary=Hl7.Fhir.Model.Version.All, Order=270)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean CompositionalElement
        {
            get { return _CompositionalElement; }
            set { _CompositionalElement = value; OnPropertyChanged("CompositionalElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _CompositionalElement;
        
        /// <summary>
        /// If code system defines a compositional grammar
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? Compositional
        {
            get { return CompositionalElement != null ? CompositionalElement.Value : null; }
            set
            {
                if (value == null)
                    CompositionalElement = null;
                else
                    CompositionalElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("Compositional");
            }
        }
        
        /// <summary>
        /// If definitions are not stable
        /// </summary>
        [FhirElement("versionNeeded", InSummary=Hl7.Fhir.Model.Version.All, Order=280)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean VersionNeededElement
        {
            get { return _VersionNeededElement; }
            set { _VersionNeededElement = value; OnPropertyChanged("VersionNeededElement"); }
        }
        
        private Hl7.Fhir.Model.FhirBoolean _VersionNeededElement;
        
        /// <summary>
        /// If definitions are not stable
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool? VersionNeeded
        {
            get { return VersionNeededElement != null ? VersionNeededElement.Value : null; }
            set
            {
                if (value == null)
                    VersionNeededElement = null;
                else
                    VersionNeededElement = new Hl7.Fhir.Model.FhirBoolean(value);
                OnPropertyChanged("VersionNeeded");
            }
        }
        
        /// <summary>
        /// not-present | example | fragment | complete | supplement
        /// </summary>
        [FhirElement("content", InSummary=Hl7.Fhir.Model.Version.All, Order=290)]
        [CLSCompliant(false)]
        [Cardinality(Min=1,Max=1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.R4.CodeSystemContentMode> ContentElement
        {
            get { return _ContentElement; }
            set { _ContentElement = value; OnPropertyChanged("ContentElement"); }
        }
        
        private Code<Hl7.Fhir.Model.R4.CodeSystemContentMode> _ContentElement;
        
        /// <summary>
        /// not-present | example | fragment | complete | supplement
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Hl7.Fhir.Model.R4.CodeSystemContentMode? Content
        {
            get { return ContentElement != null ? ContentElement.Value : null; }
            set
            {
                if (value == null)
                    ContentElement = null;
                else
                    ContentElement = new Code<Hl7.Fhir.Model.R4.CodeSystemContentMode>(value);
                OnPropertyChanged("Content");
            }
        }
        
        /// <summary>
        /// Canonical URL of Code System this adds designations and properties to
        /// </summary>
        [FhirElement("supplements", InSummary=Hl7.Fhir.Model.Version.All, Order=300)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.Canonical SupplementsElement
        {
            get { return _SupplementsElement; }
            set { _SupplementsElement = value; OnPropertyChanged("SupplementsElement"); }
        }
        
        private Hl7.Fhir.Model.Canonical _SupplementsElement;
        
        /// <summary>
        /// Canonical URL of Code System this adds designations and properties to
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Supplements
        {
            get { return SupplementsElement != null ? SupplementsElement.Value : null; }
            set
            {
                if (value == null)
                    SupplementsElement = null;
                else
                    SupplementsElement = new Hl7.Fhir.Model.Canonical(value);
                OnPropertyChanged("Supplements");
            }
        }
        
        /// <summary>
        /// Total concepts in the code system
        /// </summary>
        [FhirElement("count", InSummary=Hl7.Fhir.Model.Version.All, Order=310)]
        [CLSCompliant(false)]
        [DataMember]
        public Hl7.Fhir.Model.UnsignedInt CountElement
        {
            get { return _CountElement; }
            set { _CountElement = value; OnPropertyChanged("CountElement"); }
        }
        
        private Hl7.Fhir.Model.UnsignedInt _CountElement;
        
        /// <summary>
        /// Total concepts in the code system
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? Count
        {
            get { return CountElement != null ? CountElement.Value : null; }
            set
            {
                if (value == null)
                    CountElement = null;
                else
                    CountElement = new Hl7.Fhir.Model.UnsignedInt(value);
                OnPropertyChanged("Count");
            }
        }
        
        /// <summary>
        /// Filter that can be used in a value set
        /// </summary>
        [FhirElement("filter", InSummary=Hl7.Fhir.Model.Version.All, Order=320)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<FilterComponent> Filter
        {
            get { if(_Filter==null) _Filter = new List<FilterComponent>(); return _Filter; }
            set { _Filter = value; OnPropertyChanged("Filter"); }
        }
        
        private List<FilterComponent> _Filter;
        
        /// <summary>
        /// Additional information supplied about each concept
        /// </summary>
        [FhirElement("property", InSummary=Hl7.Fhir.Model.Version.All, Order=330)]
        [CLSCompliant(false)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<PropertyComponent> Property
        {
            get { if(_Property==null) _Property = new List<PropertyComponent>(); return _Property; }
            set { _Property = value; OnPropertyChanged("Property"); }
        }
        
        private List<PropertyComponent> _Property;
        
        /// <summary>
        /// Concepts in the code system
        /// </summary>
        [FhirElement("concept", Order=340)]
        [Cardinality(Min=0,Max=-1)]
        [DataMember]
        public List<ConceptDefinitionComponent> Concept
        {
            get { if(_Concept==null) _Concept = new List<ConceptDefinitionComponent>(); return _Concept; }
            set { _Concept = value; OnPropertyChanged("Concept"); }
        }
        
        private List<ConceptDefinitionComponent> _Concept;
    
    
        public static ElementDefinitionConstraint[] CodeSystem_Constraints =
        {
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "csd-1",
                severity: ConstraintSeverity.Warning,
                expression: "concept.code.combine($this.descendants().concept.code).isDistinct()",
                human: "Within a code system definition, all the codes SHALL be unique",
                xpath: "count(distinct-values(descendant::f:concept/f:code/@value))=count(descendant::f:concept)"
            ),
            new ElementDefinitionConstraint(
                versions: new[] {Hl7.Fhir.Model.Version.R4},
                key: "csd-0",
                severity: ConstraintSeverity.Warning,
                expression: "name.matches('[A-Z]([A-Za-z0-9_]){0,254}')",
                human: "Name should be usable as an identifier for the module by machine processing applications such as code generation",
                xpath: "not(exists(f:name/@value)) or matches(f:name/@value, '[A-Z]([A-Za-z0-9_]){0,254}')"
            ),
        };
    
        public override void AddDefaultConstraints()
        {
            base.AddDefaultConstraints();
            InvariantConstraints.AddRange(CodeSystem_Constraints);
        }
    
        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as CodeSystem;
        
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
                if(Contact != null) dest.Contact = new List<Hl7.Fhir.Model.R4.ContactDetail>(Contact.DeepCopy());
                if(DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.Markdown)DescriptionElement.DeepCopy();
                if(UseContext != null) dest.UseContext = new List<Hl7.Fhir.Model.UsageContext>(UseContext.DeepCopy());
                if(Jurisdiction != null) dest.Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(Jurisdiction.DeepCopy());
                if(PurposeElement != null) dest.PurposeElement = (Hl7.Fhir.Model.Markdown)PurposeElement.DeepCopy();
                if(CopyrightElement != null) dest.CopyrightElement = (Hl7.Fhir.Model.Markdown)CopyrightElement.DeepCopy();
                if(CaseSensitiveElement != null) dest.CaseSensitiveElement = (Hl7.Fhir.Model.FhirBoolean)CaseSensitiveElement.DeepCopy();
                if(ValueSetElement != null) dest.ValueSetElement = (Hl7.Fhir.Model.Canonical)ValueSetElement.DeepCopy();
                if(HierarchyMeaningElement != null) dest.HierarchyMeaningElement = (Code<Hl7.Fhir.Model.CodeSystemHierarchyMeaning>)HierarchyMeaningElement.DeepCopy();
                if(CompositionalElement != null) dest.CompositionalElement = (Hl7.Fhir.Model.FhirBoolean)CompositionalElement.DeepCopy();
                if(VersionNeededElement != null) dest.VersionNeededElement = (Hl7.Fhir.Model.FhirBoolean)VersionNeededElement.DeepCopy();
                if(ContentElement != null) dest.ContentElement = (Code<Hl7.Fhir.Model.R4.CodeSystemContentMode>)ContentElement.DeepCopy();
                if(SupplementsElement != null) dest.SupplementsElement = (Hl7.Fhir.Model.Canonical)SupplementsElement.DeepCopy();
                if(CountElement != null) dest.CountElement = (Hl7.Fhir.Model.UnsignedInt)CountElement.DeepCopy();
                if(Filter != null) dest.Filter = new List<FilterComponent>(Filter.DeepCopy());
                if(Property != null) dest.Property = new List<PropertyComponent>(Property.DeepCopy());
                if(Concept != null) dest.Concept = new List<ConceptDefinitionComponent>(Concept.DeepCopy());
                return dest;
            }
            else
                throw new ArgumentException("Can only copy to an object of the same type", "other");
        }
        
        public override IDeepCopyable DeepCopy()
        {
             return CopyTo(new CodeSystem());
        }
        
        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as CodeSystem;
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
            if( !DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if( !DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.Matches(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.Matches(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.Matches(PurposeElement, otherT.PurposeElement)) return false;
            if( !DeepComparable.Matches(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.Matches(CaseSensitiveElement, otherT.CaseSensitiveElement)) return false;
            if( !DeepComparable.Matches(ValueSetElement, otherT.ValueSetElement)) return false;
            if( !DeepComparable.Matches(HierarchyMeaningElement, otherT.HierarchyMeaningElement)) return false;
            if( !DeepComparable.Matches(CompositionalElement, otherT.CompositionalElement)) return false;
            if( !DeepComparable.Matches(VersionNeededElement, otherT.VersionNeededElement)) return false;
            if( !DeepComparable.Matches(ContentElement, otherT.ContentElement)) return false;
            if( !DeepComparable.Matches(SupplementsElement, otherT.SupplementsElement)) return false;
            if( !DeepComparable.Matches(CountElement, otherT.CountElement)) return false;
            if( !DeepComparable.Matches(Filter, otherT.Filter)) return false;
            if( !DeepComparable.Matches(Property, otherT.Property)) return false;
            if( !DeepComparable.Matches(Concept, otherT.Concept)) return false;
        
            return true;
        }
        
        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as CodeSystem;
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
            if( !DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if( !DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
            if( !DeepComparable.IsExactly(UseContext, otherT.UseContext)) return false;
            if( !DeepComparable.IsExactly(Jurisdiction, otherT.Jurisdiction)) return false;
            if( !DeepComparable.IsExactly(PurposeElement, otherT.PurposeElement)) return false;
            if( !DeepComparable.IsExactly(CopyrightElement, otherT.CopyrightElement)) return false;
            if( !DeepComparable.IsExactly(CaseSensitiveElement, otherT.CaseSensitiveElement)) return false;
            if( !DeepComparable.IsExactly(ValueSetElement, otherT.ValueSetElement)) return false;
            if( !DeepComparable.IsExactly(HierarchyMeaningElement, otherT.HierarchyMeaningElement)) return false;
            if( !DeepComparable.IsExactly(CompositionalElement, otherT.CompositionalElement)) return false;
            if( !DeepComparable.IsExactly(VersionNeededElement, otherT.VersionNeededElement)) return false;
            if( !DeepComparable.IsExactly(ContentElement, otherT.ContentElement)) return false;
            if( !DeepComparable.IsExactly(SupplementsElement, otherT.SupplementsElement)) return false;
            if( !DeepComparable.IsExactly(CountElement, otherT.CountElement)) return false;
            if( !DeepComparable.IsExactly(Filter, otherT.Filter)) return false;
            if( !DeepComparable.IsExactly(Property, otherT.Property)) return false;
            if( !DeepComparable.IsExactly(Concept, otherT.Concept)) return false;
        
            return true;
        }
    
        internal override void Serialize(Serialization.SerializerSink sink)
        {
            sink.BeginResource("CodeSystem");
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
            sink.BeginList("contact", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Contact)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.Element("description", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); DescriptionElement?.Serialize(sink);
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
            sink.Element("purpose", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); PurposeElement?.Serialize(sink);
            sink.Element("copyright", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false, false); CopyrightElement?.Serialize(sink);
            sink.Element("caseSensitive", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CaseSensitiveElement?.Serialize(sink);
            sink.Element("valueSet", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); ValueSetElement?.Serialize(sink);
            sink.Element("hierarchyMeaning", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); HierarchyMeaningElement?.Serialize(sink);
            sink.Element("compositional", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CompositionalElement?.Serialize(sink);
            sink.Element("versionNeeded", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); VersionNeededElement?.Serialize(sink);
            sink.Element("content", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, true, false); ContentElement?.Serialize(sink);
            sink.Element("supplements", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); SupplementsElement?.Serialize(sink);
            sink.Element("count", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false, false); CountElement?.Serialize(sink);
            sink.BeginList("filter", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Filter)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("property", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.All, false);
            foreach(var item in Property)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.BeginList("concept", Hl7.Fhir.Model.Version.All, Hl7.Fhir.Model.Version.None, false);
            foreach(var item in Concept)
            {
                item?.Serialize(sink);
            }
            sink.End();
            sink.End();
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
                foreach (var elem in Contact) { if (elem != null) yield return elem; }
                if (DescriptionElement != null) yield return DescriptionElement;
                foreach (var elem in UseContext) { if (elem != null) yield return elem; }
                foreach (var elem in Jurisdiction) { if (elem != null) yield return elem; }
                if (PurposeElement != null) yield return PurposeElement;
                if (CopyrightElement != null) yield return CopyrightElement;
                if (CaseSensitiveElement != null) yield return CaseSensitiveElement;
                if (ValueSetElement != null) yield return ValueSetElement;
                if (HierarchyMeaningElement != null) yield return HierarchyMeaningElement;
                if (CompositionalElement != null) yield return CompositionalElement;
                if (VersionNeededElement != null) yield return VersionNeededElement;
                if (ContentElement != null) yield return ContentElement;
                if (SupplementsElement != null) yield return SupplementsElement;
                if (CountElement != null) yield return CountElement;
                foreach (var elem in Filter) { if (elem != null) yield return elem; }
                foreach (var elem in Property) { if (elem != null) yield return elem; }
                foreach (var elem in Concept) { if (elem != null) yield return elem; }
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
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                if (DescriptionElement != null) yield return new ElementValue("description", DescriptionElement);
                foreach (var elem in UseContext) { if (elem != null) yield return new ElementValue("useContext", elem); }
                foreach (var elem in Jurisdiction) { if (elem != null) yield return new ElementValue("jurisdiction", elem); }
                if (PurposeElement != null) yield return new ElementValue("purpose", PurposeElement);
                if (CopyrightElement != null) yield return new ElementValue("copyright", CopyrightElement);
                if (CaseSensitiveElement != null) yield return new ElementValue("caseSensitive", CaseSensitiveElement);
                if (ValueSetElement != null) yield return new ElementValue("valueSet", ValueSetElement);
                if (HierarchyMeaningElement != null) yield return new ElementValue("hierarchyMeaning", HierarchyMeaningElement);
                if (CompositionalElement != null) yield return new ElementValue("compositional", CompositionalElement);
                if (VersionNeededElement != null) yield return new ElementValue("versionNeeded", VersionNeededElement);
                if (ContentElement != null) yield return new ElementValue("content", ContentElement);
                if (SupplementsElement != null) yield return new ElementValue("supplements", SupplementsElement);
                if (CountElement != null) yield return new ElementValue("count", CountElement);
                foreach (var elem in Filter) { if (elem != null) yield return new ElementValue("filter", elem); }
                foreach (var elem in Property) { if (elem != null) yield return new ElementValue("property", elem); }
                foreach (var elem in Concept) { if (elem != null) yield return new ElementValue("concept", elem); }
            }
        }
    
    }

}
