// Originally generated from hl7.fhir.r3.core version: 3.0.2

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

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

namespace Hl7.Fhir.Model
{
    /// <summary>
    /// A set of codes drawn from one or more code systems
    /// </summary>
    [Serializable]
    [DataContract]
    [FhirType("CodeSystem", "http://hl7.org/fhir/StructureDefinition/CodeSystem", IsResource = true)]
    public partial class TestCodeSystem : Hl7.Fhir.Model.DomainResource
    {
        /// <summary>
        /// FHIR Type Name
        /// </summary>
        public override string TypeName { get { return "CodeSystem"; } }

        /// <summary>
        /// The meaning of the hierarchy of concepts in a code system
        /// (url: http://hl7.org/fhir/ValueSet/codesystem-hierarchy-meaning)
        /// (system: http://hl7.org/fhir/codesystem-hierarchy-meaning)
        /// </summary>
        [FhirEnumeration("CodeSystemHierarchyMeaning")]
        public enum CodeSystemHierarchyMeaning
        {
            /// <summary>
            /// No particular relationship between the concepts can be assumed, except what can be determined by inspection of the definitions of the elements (possible reasons to use this: importing from a source where this is not defined, or where various parts of the hierarchy have different meanings)
            /// (system: http://hl7.org/fhir/codesystem-hierarchy-meaning)
            /// </summary>
            [EnumLiteral("grouped-by", "http://hl7.org/fhir/codesystem-hierarchy-meaning"), Description("Grouped By")]
            GroupedBy,
            /// <summary>
            /// A hierarchy where the child concepts have an IS-A relationship with the parents - that is, all the properties of the parent are also true for its child concepts
            /// (system: http://hl7.org/fhir/codesystem-hierarchy-meaning)
            /// </summary>
            [EnumLiteral("is-a", "http://hl7.org/fhir/codesystem-hierarchy-meaning"), Description("Is-A")]
            IsA,
            /// <summary>
            /// Child elements list the individual parts of a composite whole (e.g. body site)
            /// (system: http://hl7.org/fhir/codesystem-hierarchy-meaning)
            /// </summary>
            [EnumLiteral("part-of", "http://hl7.org/fhir/codesystem-hierarchy-meaning"), Description("Part Of")]
            PartOf,
            /// <summary>
            /// Child concepts in the hierarchy may have only one parent, and there is a presumption that the code system is a "closed world" meaning all things must be in the hierarchy. This results in concepts such as "not otherwise classified."
            /// (system: http://hl7.org/fhir/codesystem-hierarchy-meaning)
            /// </summary>
            [EnumLiteral("classified-with", "http://hl7.org/fhir/codesystem-hierarchy-meaning"), Description("Classified With")]
            ClassifiedWith,
        }

        /// <summary>
        /// How much of the content of the code system - the concepts and codes it defines - are represented in a code system resource
        /// (url: http://hl7.org/fhir/ValueSet/codesystem-content-mode)
        /// (system: http://hl7.org/fhir/codesystem-content-mode)
        /// </summary>
        [FhirEnumeration("CodeSystemContentMode")]
        public enum CodeSystemContentMode
        {
            /// <summary>
            /// None of the concepts defined by the code system are included in the code system resource
            /// (system: http://hl7.org/fhir/codesystem-content-mode)
            /// </summary>
            [EnumLiteral("not-present", "http://hl7.org/fhir/codesystem-content-mode"), Description("Not Present")]
            NotPresent,
            /// <summary>
            /// A few representative concepts are included in the code system resource
            /// (system: http://hl7.org/fhir/codesystem-content-mode)
            /// </summary>
            [EnumLiteral("example", "http://hl7.org/fhir/codesystem-content-mode"), Description("Example")]
            Example,
            /// <summary>
            /// A subset of the code system concepts are included in the code system resource
            /// (system: http://hl7.org/fhir/codesystem-content-mode)
            /// </summary>
            [EnumLiteral("fragment", "http://hl7.org/fhir/codesystem-content-mode"), Description("Fragment")]
            Fragment,
            /// <summary>
            /// All the concepts defined by the code system are included in the code system resource
            /// (system: http://hl7.org/fhir/codesystem-content-mode)
            /// </summary>
            [EnumLiteral("complete", "http://hl7.org/fhir/codesystem-content-mode"), Description("Complete")]
            Complete,
        }

        /// <summary>
        /// The type of a property value
        /// (url: http://hl7.org/fhir/ValueSet/concept-property-type)
        /// (system: http://hl7.org/fhir/concept-property-type)
        /// </summary>
        [FhirEnumeration("PropertyType")]
        public enum PropertyType
        {
            /// <summary>
            /// The property value is a code that identifies a concept defined in the code system
            /// (system: http://hl7.org/fhir/concept-property-type)
            /// </summary>
            [EnumLiteral("code", "http://hl7.org/fhir/concept-property-type"), Description("code (internal reference)")]
            Code,
            /// <summary>
            /// The property  value is a code defined in an external code system. This may be used for translations, but is not the intent
            /// (system: http://hl7.org/fhir/concept-property-type)
            /// </summary>
            [EnumLiteral("Coding", "http://hl7.org/fhir/concept-property-type"), Description("Coding (external reference)")]
            Coding,
            /// <summary>
            /// The property value is a string
            /// (system: http://hl7.org/fhir/concept-property-type)
            /// </summary>
            [EnumLiteral("string", "http://hl7.org/fhir/concept-property-type"), Description("string")]
            String,
            /// <summary>
            /// The property value is a string (often used to assign ranking values to concepts for supporting score assessments)
            /// (system: http://hl7.org/fhir/concept-property-type)
            /// </summary>
            [EnumLiteral("integer", "http://hl7.org/fhir/concept-property-type"), Description("integer")]
            Integer,
            /// <summary>
            /// The property value is a boolean true | false
            /// (system: http://hl7.org/fhir/concept-property-type)
            /// </summary>
            [EnumLiteral("boolean", "http://hl7.org/fhir/concept-property-type"), Description("boolean")]
            Boolean,
            /// <summary>
            /// The property is a date or a date + time
            /// (system: http://hl7.org/fhir/concept-property-type)
            /// </summary>
            [EnumLiteral("dateTime", "http://hl7.org/fhir/concept-property-type"), Description("dateTime")]
            DateTime,
        }

        /// <summary>
        /// Filter that can be used in a value set
        /// </summary>
        [Serializable]
        [DataContract]
        [FhirType("CodeSystem#Filter", IsNestedType = true)]
        public partial class FilterComponent : Hl7.Fhir.Model.BackboneElement
        {
            /// <summary>
            /// FHIR Type Name
            /// </summary>
            public override string TypeName { get { return "CodeSystem#Filter"; } }

            /// <summary>
            /// Code that identifies the filter
            /// </summary>
            [FhirElement("code", InSummary = true, Order = 40)]
            [Cardinality(Min = 1, Max = 1)]
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
            [IgnoreDataMember]
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
            [FhirElement("description", InSummary = true, Order = 50)]
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
            [IgnoreDataMember]
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
            /// Operators that can be used with filter
            /// </summary>
            [FhirElement("operator", InSummary = true, Order = 60)]
            [DeclaredType(Type = typeof(Code))]
            [Cardinality(Min = 1, Max = -1)]
            [DataMember]
            public List<Code<Hl7.Fhir.Model.FilterOperator>> OperatorElement
            {
                get { if (_OperatorElement == null) _OperatorElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.FilterOperator>>(); return _OperatorElement; }
                set { _OperatorElement = value; OnPropertyChanged("OperatorElement"); }
            }

            private List<Code<Hl7.Fhir.Model.FilterOperator>> _OperatorElement;

            /// <summary>
            /// Operators that can be used with filter
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [IgnoreDataMember]
            public IEnumerable<Hl7.Fhir.Model.FilterOperator?> Operator
            {
                get { return OperatorElement != null ? OperatorElement.Select(elem => elem.Value) : null; }
                set
                {
                    if (value == null)
                        OperatorElement = null;
                    else
                        OperatorElement = new List<Hl7.Fhir.Model.Code<Hl7.Fhir.Model.FilterOperator>>(value.Select(elem => new Hl7.Fhir.Model.Code<Hl7.Fhir.Model.FilterOperator>(elem)));
                    OnPropertyChanged("Operator");
                }
            }

            /// <summary>
            /// What to use for the value
            /// </summary>
            [FhirElement("value", InSummary = true, Order = 70)]
            [Cardinality(Min = 1, Max = 1)]
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
            [IgnoreDataMember]
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

            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as FilterComponent;

                if (dest == null)
                {
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
                }

                base.CopyTo(dest);
                if (CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
                if (DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if (OperatorElement != null) dest.OperatorElement = new List<Code<Hl7.Fhir.Model.FilterOperator>>(OperatorElement.DeepCopy());
                if (ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirString)ValueElement.DeepCopy();
                return dest;
            }

            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new FilterComponent());
            }

            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as FilterComponent;
                if (otherT == null) return false;

                if (!base.Matches(otherT)) return false;
                if (!DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if (!DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if (!DeepComparable.Matches(OperatorElement, otherT.OperatorElement)) return false;
                if (!DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;

                return true;
            }

            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as FilterComponent;
                if (otherT == null) return false;

                if (!base.IsExactly(otherT)) return false;
                if (!DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if (!DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if (!DeepComparable.IsExactly(OperatorElement, otherT.OperatorElement)) return false;
                if (!DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;

                return true;
            }

            [IgnoreDataMember]
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

            [IgnoreDataMember]
            public override IEnumerable<ElementValue> NamedChildren
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

            protected override bool TryGetValue(string key, out object value)
            {
                switch (key)
                {
                    case "code":
                        value = CodeElement;
                        return CodeElement is not null;
                    case "description":
                        value = DescriptionElement;
                        return DescriptionElement is not null;
                    case "operator":
                        value = OperatorElement;
                        return OperatorElement?.Any() == true;
                    case "value":
                        value = ValueElement;
                        return ValueElement is not null;
                    default:
                        return base.TryGetValue(key, out value);
                };

            }

            protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
            {
                foreach (var kvp in base.GetElementPairs()) yield return kvp;
                if (CodeElement is not null) yield return new KeyValuePair<string, object>("code", CodeElement);
                if (DescriptionElement is not null) yield return new KeyValuePair<string, object>("description", DescriptionElement);
                if (OperatorElement?.Any() == true) yield return new KeyValuePair<string, object>("operator", OperatorElement);
                if (ValueElement is not null) yield return new KeyValuePair<string, object>("value", ValueElement);
            }

        }

        /// <summary>
        /// Additional information supplied about each concept
        /// </summary>
        [Serializable]
        [DataContract]
        [FhirType("CodeSystem#Property", IsNestedType = true)]
        public partial class PropertyComponent : Hl7.Fhir.Model.BackboneElement
        {
            /// <summary>
            /// FHIR Type Name
            /// </summary>
            public override string TypeName { get { return "CodeSystem#Property"; } }

            /// <summary>
            /// Identifies the property on the concepts, and when referred to in operations
            /// </summary>
            [FhirElement("code", InSummary = true, Order = 40)]
            [Cardinality(Min = 1, Max = 1)]
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
            [IgnoreDataMember]
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
            [FhirElement("uri", InSummary = true, Order = 50)]
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
            [IgnoreDataMember]
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
            [FhirElement("description", InSummary = true, Order = 60)]
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
            [IgnoreDataMember]
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
            /// code | Coding | string | integer | boolean | dateTime
            /// </summary>
            [FhirElement("type", InSummary = true, Order = 70)]
            [DeclaredType(Type = typeof(Code))]
            [Cardinality(Min = 1, Max = 1)]
            [DataMember]
            public Code<Hl7.Fhir.Model.TestCodeSystem.PropertyType> TypeElement
            {
                get { return _TypeElement; }
                set { _TypeElement = value; OnPropertyChanged("TypeElement"); }
            }

            private Code<Hl7.Fhir.Model.TestCodeSystem.PropertyType> _TypeElement;

            /// <summary>
            /// code | Coding | string | integer | boolean | dateTime
            /// </summary>
            /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
            [IgnoreDataMember]
            public Hl7.Fhir.Model.TestCodeSystem.PropertyType? Type
            {
                get { return TypeElement != null ? TypeElement.Value : null; }
                set
                {
                    if (value == null)
                        TypeElement = null;
                    else
                        TypeElement = new Code<Hl7.Fhir.Model.TestCodeSystem.PropertyType>(value);
                    OnPropertyChanged("Type");
                }
            }

            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as PropertyComponent;

                if (dest == null)
                {
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
                }

                base.CopyTo(dest);
                if (CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
                if (UriElement != null) dest.UriElement = (Hl7.Fhir.Model.FhirUri)UriElement.DeepCopy();
                if (DescriptionElement != null) dest.DescriptionElement = (Hl7.Fhir.Model.FhirString)DescriptionElement.DeepCopy();
                if (TypeElement != null) dest.TypeElement = (Code<Hl7.Fhir.Model.TestCodeSystem.PropertyType>)TypeElement.DeepCopy();
                return dest;
            }

            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new PropertyComponent());
            }

            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as PropertyComponent;
                if (otherT == null) return false;

                if (!base.Matches(otherT)) return false;
                if (!DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if (!DeepComparable.Matches(UriElement, otherT.UriElement)) return false;
                if (!DeepComparable.Matches(DescriptionElement, otherT.DescriptionElement)) return false;
                if (!DeepComparable.Matches(TypeElement, otherT.TypeElement)) return false;

                return true;
            }

            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as PropertyComponent;
                if (otherT == null) return false;

                if (!base.IsExactly(otherT)) return false;
                if (!DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if (!DeepComparable.IsExactly(UriElement, otherT.UriElement)) return false;
                if (!DeepComparable.IsExactly(DescriptionElement, otherT.DescriptionElement)) return false;
                if (!DeepComparable.IsExactly(TypeElement, otherT.TypeElement)) return false;

                return true;
            }

            [IgnoreDataMember]
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

            [IgnoreDataMember]
            public override IEnumerable<ElementValue> NamedChildren
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

            protected override bool TryGetValue(string key, out object value)
            {
                switch (key)
                {
                    case "code":
                        value = CodeElement;
                        return CodeElement is not null;
                    case "uri":
                        value = UriElement;
                        return UriElement is not null;
                    case "description":
                        value = DescriptionElement;
                        return DescriptionElement is not null;
                    case "type":
                        value = TypeElement;
                        return TypeElement is not null;
                    default:
                        return base.TryGetValue(key, out value);
                };

            }

            protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
            {
                foreach (var kvp in base.GetElementPairs()) yield return kvp;
                if (CodeElement is not null) yield return new KeyValuePair<string, object>("code", CodeElement);
                if (UriElement is not null) yield return new KeyValuePair<string, object>("uri", UriElement);
                if (DescriptionElement is not null) yield return new KeyValuePair<string, object>("description", DescriptionElement);
                if (TypeElement is not null) yield return new KeyValuePair<string, object>("type", TypeElement);
            }

        }

        /// <summary>
        /// Concepts in the code system
        /// </summary>
        [Serializable]
        [DataContract]
        [FhirType("CodeSystem#ConceptDefinition", IsNestedType = true)]
        public partial class ConceptDefinitionComponent : Hl7.Fhir.Model.BackboneElement
        {
            /// <summary>
            /// FHIR Type Name
            /// </summary>
            public override string TypeName { get { return "CodeSystem#ConceptDefinition"; } }

            /// <summary>
            /// Code that identifies concept
            /// </summary>
            [FhirElement("code", Order = 40)]
            [Cardinality(Min = 1, Max = 1)]
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
            [IgnoreDataMember]
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
            [FhirElement("display", Order = 50)]
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
            [IgnoreDataMember]
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
            [FhirElement("definition", Order = 60)]
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
            [IgnoreDataMember]
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
            [FhirElement("designation", Order = 70)]
            [Cardinality(Min = 0, Max = -1)]
            [DataMember]
            public List<Hl7.Fhir.Model.TestCodeSystem.DesignationComponent> Designation
            {
                get { if (_Designation == null) _Designation = new List<Hl7.Fhir.Model.TestCodeSystem.DesignationComponent>(); return _Designation; }
                set { _Designation = value; OnPropertyChanged("Designation"); }
            }

            private List<Hl7.Fhir.Model.TestCodeSystem.DesignationComponent> _Designation;

            /// <summary>
            /// Property value for the concept
            /// </summary>
            [FhirElement("property", Order = 80)]
            [Cardinality(Min = 0, Max = -1)]
            [DataMember]
            public List<Hl7.Fhir.Model.TestCodeSystem.ConceptPropertyComponent> Property
            {
                get { if (_Property == null) _Property = new List<Hl7.Fhir.Model.TestCodeSystem.ConceptPropertyComponent>(); return _Property; }
                set { _Property = value; OnPropertyChanged("Property"); }
            }

            private List<Hl7.Fhir.Model.TestCodeSystem.ConceptPropertyComponent> _Property;

            /// <summary>
            /// Child Concepts (is-a/contains/categorizes)
            /// </summary>
            [FhirElement("concept", Order = 90)]
            [Cardinality(Min = 0, Max = -1)]
            [DataMember]
            public List<Hl7.Fhir.Model.TestCodeSystem.ConceptDefinitionComponent> Concept
            {
                get { if (_Concept == null) _Concept = new List<Hl7.Fhir.Model.TestCodeSystem.ConceptDefinitionComponent>(); return _Concept; }
                set { _Concept = value; OnPropertyChanged("Concept"); }
            }

            private List<Hl7.Fhir.Model.TestCodeSystem.ConceptDefinitionComponent> _Concept;

            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ConceptDefinitionComponent;

                if (dest == null)
                {
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
                }

                base.CopyTo(dest);
                if (CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
                if (DisplayElement != null) dest.DisplayElement = (Hl7.Fhir.Model.FhirString)DisplayElement.DeepCopy();
                if (DefinitionElement != null) dest.DefinitionElement = (Hl7.Fhir.Model.FhirString)DefinitionElement.DeepCopy();
                if (Designation != null) dest.Designation = new List<Hl7.Fhir.Model.TestCodeSystem.DesignationComponent>(Designation.DeepCopy());
                if (Property != null) dest.Property = new List<Hl7.Fhir.Model.TestCodeSystem.ConceptPropertyComponent>(Property.DeepCopy());
                if (Concept != null) dest.Concept = new List<Hl7.Fhir.Model.TestCodeSystem.ConceptDefinitionComponent>(Concept.DeepCopy());
                return dest;
            }

            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ConceptDefinitionComponent());
            }

            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ConceptDefinitionComponent;
                if (otherT == null) return false;

                if (!base.Matches(otherT)) return false;
                if (!DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if (!DeepComparable.Matches(DisplayElement, otherT.DisplayElement)) return false;
                if (!DeepComparable.Matches(DefinitionElement, otherT.DefinitionElement)) return false;
                if (!DeepComparable.Matches(Designation, otherT.Designation)) return false;
                if (!DeepComparable.Matches(Property, otherT.Property)) return false;
                if (!DeepComparable.Matches(Concept, otherT.Concept)) return false;

                return true;
            }

            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ConceptDefinitionComponent;
                if (otherT == null) return false;

                if (!base.IsExactly(otherT)) return false;
                if (!DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if (!DeepComparable.IsExactly(DisplayElement, otherT.DisplayElement)) return false;
                if (!DeepComparable.IsExactly(DefinitionElement, otherT.DefinitionElement)) return false;
                if (!DeepComparable.IsExactly(Designation, otherT.Designation)) return false;
                if (!DeepComparable.IsExactly(Property, otherT.Property)) return false;
                if (!DeepComparable.IsExactly(Concept, otherT.Concept)) return false;

                return true;
            }

            [IgnoreDataMember]
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

            [IgnoreDataMember]
            public override IEnumerable<ElementValue> NamedChildren
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

            protected override bool TryGetValue(string key, out object value)
            {
                switch (key)
                {
                    case "code":
                        value = CodeElement;
                        return CodeElement is not null;
                    case "display":
                        value = DisplayElement;
                        return DisplayElement is not null;
                    case "definition":
                        value = DefinitionElement;
                        return DefinitionElement is not null;
                    case "designation":
                        value = Designation;
                        return Designation?.Any() == true;
                    case "property":
                        value = Property;
                        return Property?.Any() == true;
                    case "concept":
                        value = Concept;
                        return Concept?.Any() == true;
                    default:
                        return base.TryGetValue(key, out value);
                };

            }

            protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
            {
                foreach (var kvp in base.GetElementPairs()) yield return kvp;
                if (CodeElement is not null) yield return new KeyValuePair<string, object>("code", CodeElement);
                if (DisplayElement is not null) yield return new KeyValuePair<string, object>("display", DisplayElement);
                if (DefinitionElement is not null) yield return new KeyValuePair<string, object>("definition", DefinitionElement);
                if (Designation?.Any() == true) yield return new KeyValuePair<string, object>("designation", Designation);
                if (Property?.Any() == true) yield return new KeyValuePair<string, object>("property", Property);
                if (Concept?.Any() == true) yield return new KeyValuePair<string, object>("concept", Concept);
            }

        }

        /// <summary>
        /// Additional representations for the concept
        /// </summary>
        [Serializable]
        [DataContract]
        [FhirType("CodeSystem#Designation", IsNestedType = true)]
        public partial class DesignationComponent : Hl7.Fhir.Model.BackboneElement
        {
            /// <summary>
            /// FHIR Type Name
            /// </summary>
            public override string TypeName { get { return "CodeSystem#Designation"; } }

            /// <summary>
            /// Human language of the designation
            /// </summary>
            [FhirElement("language", Order = 40)]
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
            [IgnoreDataMember]
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
            [FhirElement("use", Order = 50)]
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
            [FhirElement("value", Order = 60)]
            [Cardinality(Min = 1, Max = 1)]
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
            [IgnoreDataMember]
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

            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as DesignationComponent;

                if (dest == null)
                {
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
                }

                base.CopyTo(dest);
                if (LanguageElement != null) dest.LanguageElement = (Hl7.Fhir.Model.Code)LanguageElement.DeepCopy();
                if (Use != null) dest.Use = (Hl7.Fhir.Model.Coding)Use.DeepCopy();
                if (ValueElement != null) dest.ValueElement = (Hl7.Fhir.Model.FhirString)ValueElement.DeepCopy();
                return dest;
            }

            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new DesignationComponent());
            }

            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as DesignationComponent;
                if (otherT == null) return false;

                if (!base.Matches(otherT)) return false;
                if (!DeepComparable.Matches(LanguageElement, otherT.LanguageElement)) return false;
                if (!DeepComparable.Matches(Use, otherT.Use)) return false;
                if (!DeepComparable.Matches(ValueElement, otherT.ValueElement)) return false;

                return true;
            }

            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as DesignationComponent;
                if (otherT == null) return false;

                if (!base.IsExactly(otherT)) return false;
                if (!DeepComparable.IsExactly(LanguageElement, otherT.LanguageElement)) return false;
                if (!DeepComparable.IsExactly(Use, otherT.Use)) return false;
                if (!DeepComparable.IsExactly(ValueElement, otherT.ValueElement)) return false;

                return true;
            }

            [IgnoreDataMember]
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

            [IgnoreDataMember]
            public override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (LanguageElement != null) yield return new ElementValue("language", LanguageElement);
                    if (Use != null) yield return new ElementValue("use", Use);
                    if (ValueElement != null) yield return new ElementValue("value", ValueElement);
                }
            }

            protected override bool TryGetValue(string key, out object value)
            {
                switch (key)
                {
                    case "language":
                        value = LanguageElement;
                        return LanguageElement is not null;
                    case "use":
                        value = Use;
                        return Use is not null;
                    case "value":
                        value = ValueElement;
                        return ValueElement is not null;
                    default:
                        return base.TryGetValue(key, out value);
                };

            }

            protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
            {
                foreach (var kvp in base.GetElementPairs()) yield return kvp;
                if (LanguageElement is not null) yield return new KeyValuePair<string, object>("language", LanguageElement);
                if (Use is not null) yield return new KeyValuePair<string, object>("use", Use);
                if (ValueElement is not null) yield return new KeyValuePair<string, object>("value", ValueElement);
            }

        }

        /// <summary>
        /// Property value for the concept
        /// </summary>
        [Serializable]
        [DataContract]
        [FhirType("CodeSystem#ConceptProperty", IsNestedType = true)]
        public partial class ConceptPropertyComponent : Hl7.Fhir.Model.BackboneElement
        {
            /// <summary>
            /// FHIR Type Name
            /// </summary>
            public override string TypeName { get { return "CodeSystem#ConceptProperty"; } }

            /// <summary>
            /// Reference to CodeSystem.property.code
            /// </summary>
            [FhirElement("code", Order = 40)]
            [Cardinality(Min = 1, Max = 1)]
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
            [IgnoreDataMember]
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
            [FhirElement("value", Order = 50, Choice = ChoiceType.DatatypeChoice)]
            [AllowedTypes(typeof(Hl7.Fhir.Model.Code), typeof(Hl7.Fhir.Model.Coding), typeof(Hl7.Fhir.Model.FhirString), typeof(Hl7.Fhir.Model.Integer), typeof(Hl7.Fhir.Model.FhirBoolean), typeof(Hl7.Fhir.Model.FhirDateTime))]
            [Cardinality(Min = 1, Max = 1)]
            [DataMember]
            public Hl7.Fhir.Model.DataType Value
            {
                get { return _Value; }
                set { _Value = value; OnPropertyChanged("Value"); }
            }

            private Hl7.Fhir.Model.DataType _Value;

            public override IDeepCopyable CopyTo(IDeepCopyable other)
            {
                var dest = other as ConceptPropertyComponent;

                if (dest == null)
                {
                    throw new ArgumentException("Can only copy to an object of the same type", "other");
                }

                base.CopyTo(dest);
                if (CodeElement != null) dest.CodeElement = (Hl7.Fhir.Model.Code)CodeElement.DeepCopy();
                if (Value != null) dest.Value = (Hl7.Fhir.Model.DataType)Value.DeepCopy();
                return dest;
            }

            public override IDeepCopyable DeepCopy()
            {
                return CopyTo(new ConceptPropertyComponent());
            }

            public override bool Matches(IDeepComparable other)
            {
                var otherT = other as ConceptPropertyComponent;
                if (otherT == null) return false;

                if (!base.Matches(otherT)) return false;
                if (!DeepComparable.Matches(CodeElement, otherT.CodeElement)) return false;
                if (!DeepComparable.Matches(Value, otherT.Value)) return false;

                return true;
            }

            public override bool IsExactly(IDeepComparable other)
            {
                var otherT = other as ConceptPropertyComponent;
                if (otherT == null) return false;

                if (!base.IsExactly(otherT)) return false;
                if (!DeepComparable.IsExactly(CodeElement, otherT.CodeElement)) return false;
                if (!DeepComparable.IsExactly(Value, otherT.Value)) return false;

                return true;
            }

            [IgnoreDataMember]
            public override IEnumerable<Base> Children
            {
                get
                {
                    foreach (var item in base.Children) yield return item;
                    if (CodeElement != null) yield return CodeElement;
                    if (Value != null) yield return Value;
                }
            }

            [IgnoreDataMember]
            public override IEnumerable<ElementValue> NamedChildren
            {
                get
                {
                    foreach (var item in base.NamedChildren) yield return item;
                    if (CodeElement != null) yield return new ElementValue("code", CodeElement);
                    if (Value != null) yield return new ElementValue("value", Value);
                }
            }

            protected override bool TryGetValue(string key, out object value)
            {
                switch (key)
                {
                    case "code":
                        value = CodeElement;
                        return CodeElement is not null;
                    case "value":
                        value = Value;
                        return Value is not null;
                    default:
                        return base.TryGetValue(key, out value);
                };

            }

            protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
            {
                foreach (var kvp in base.GetElementPairs()) yield return kvp;
                if (CodeElement is not null) yield return new KeyValuePair<string, object>("code", CodeElement);
                if (Value is not null) yield return new KeyValuePair<string, object>("value", Value);
            }

        }

        /// <summary>
        /// Logical URI to reference this code system (globally unique) (Coding.system)
        /// </summary>
        [FhirElement("url", InSummary = true, Order = 90)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri UrlElement
        {
            get { return _UrlElement; }
            set { _UrlElement = value; OnPropertyChanged("UrlElement"); }
        }

        private Hl7.Fhir.Model.FhirUri _UrlElement;

        /// <summary>
        /// Logical URI to reference this code system (globally unique) (Coding.system)
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [IgnoreDataMember]
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
        /// Additional identifier for the code system
        /// </summary>
        [FhirElement("identifier", InSummary = true, Order = 100)]
        [DataMember]
        public Hl7.Fhir.Model.Identifier Identifier
        {
            get { return _Identifier; }
            set { _Identifier = value; OnPropertyChanged("Identifier"); }
        }

        private Hl7.Fhir.Model.Identifier _Identifier;

        /// <summary>
        /// Business version of the code system (Coding.version)
        /// </summary>
        [FhirElement("version", InSummary = true, Order = 110)]
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
        [IgnoreDataMember]
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
        [FhirElement("name", InSummary = true, Order = 120)]
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
        [IgnoreDataMember]
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
        [FhirElement("title", InSummary = true, Order = 130)]
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
        [IgnoreDataMember]
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
        [FhirElement("status", InSummary = true, Order = 140)]
        [DeclaredType(Type = typeof(Code))]
        [Cardinality(Min = 1, Max = 1)]
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
        [IgnoreDataMember]
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
        [FhirElement("experimental", InSummary = true, Order = 150)]
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
        [IgnoreDataMember]
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
        [FhirElement("date", InSummary = true, Order = 160)]
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
        [IgnoreDataMember]
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
        [FhirElement("publisher", InSummary = true, Order = 170)]
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
        [IgnoreDataMember]
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
        [FhirElement("contact", InSummary = true, Order = 180)]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.ContactDetail> Contact
        {
            get { if (_Contact == null) _Contact = new List<Hl7.Fhir.Model.ContactDetail>(); return _Contact; }
            set { _Contact = value; OnPropertyChanged("Contact"); }
        }

        private List<Hl7.Fhir.Model.ContactDetail> _Contact;

        /// <summary>
        /// Natural language description of the code system
        /// </summary>
        [FhirElement("description", Order = 190)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown Description
        {
            get { return _Description; }
            set { _Description = value; OnPropertyChanged("Description"); }
        }

        private Hl7.Fhir.Model.Markdown _Description;

        /// <summary>
        /// Context the content is intended to support
        /// </summary>
        [FhirElement("useContext", InSummary = true, Order = 200)]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.UsageContext> UseContext
        {
            get { if (_UseContext == null) _UseContext = new List<Hl7.Fhir.Model.UsageContext>(); return _UseContext; }
            set { _UseContext = value; OnPropertyChanged("UseContext"); }
        }

        private List<Hl7.Fhir.Model.UsageContext> _UseContext;

        /// <summary>
        /// Intended jurisdiction for code system (if applicable)
        /// </summary>
        [FhirElement("jurisdiction", InSummary = true, Order = 210)]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.CodeableConcept> Jurisdiction
        {
            get { if (_Jurisdiction == null) _Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(); return _Jurisdiction; }
            set { _Jurisdiction = value; OnPropertyChanged("Jurisdiction"); }
        }

        private List<Hl7.Fhir.Model.CodeableConcept> _Jurisdiction;

        /// <summary>
        /// Why this code system is defined
        /// </summary>
        [FhirElement("purpose", Order = 220)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown Purpose
        {
            get { return _Purpose; }
            set { _Purpose = value; OnPropertyChanged("Purpose"); }
        }

        private Hl7.Fhir.Model.Markdown _Purpose;

        /// <summary>
        /// Use and/or publishing restrictions
        /// </summary>
        [FhirElement("copyright", Order = 230)]
        [DataMember]
        public Hl7.Fhir.Model.Markdown Copyright
        {
            get { return _Copyright; }
            set { _Copyright = value; OnPropertyChanged("Copyright"); }
        }

        private Hl7.Fhir.Model.Markdown _Copyright;

        /// <summary>
        /// If code comparison is case sensitive
        /// </summary>
        [FhirElement("caseSensitive", InSummary = true, Order = 240)]
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
        [IgnoreDataMember]
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
        /// Canonical URL for value set with entire code system
        /// </summary>
        [FhirElement("valueSet", InSummary = true, Order = 250)]
        [DataMember]
        public Hl7.Fhir.Model.FhirUri ValueSetElement
        {
            get { return _ValueSetElement; }
            set { _ValueSetElement = value; OnPropertyChanged("ValueSetElement"); }
        }

        private Hl7.Fhir.Model.FhirUri _ValueSetElement;

        /// <summary>
        /// Canonical URL for value set with entire code system
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [IgnoreDataMember]
        public string ValueSet
        {
            get { return ValueSetElement != null ? ValueSetElement.Value : null; }
            set
            {
                if (value == null)
                    ValueSetElement = null;
                else
                    ValueSetElement = new Hl7.Fhir.Model.FhirUri(value);
                OnPropertyChanged("ValueSet");
            }
        }

        /// <summary>
        /// grouped-by | is-a | part-of | classified-with
        /// </summary>
        [FhirElement("hierarchyMeaning", InSummary = true, Order = 260)]
        [DeclaredType(Type = typeof(Code))]
        [DataMember]
        public Code<Hl7.Fhir.Model.TestCodeSystem.CodeSystemHierarchyMeaning> HierarchyMeaningElement
        {
            get { return _HierarchyMeaningElement; }
            set { _HierarchyMeaningElement = value; OnPropertyChanged("HierarchyMeaningElement"); }
        }

        private Code<Hl7.Fhir.Model.TestCodeSystem.CodeSystemHierarchyMeaning> _HierarchyMeaningElement;

        /// <summary>
        /// grouped-by | is-a | part-of | classified-with
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [IgnoreDataMember]
        public Hl7.Fhir.Model.TestCodeSystem.CodeSystemHierarchyMeaning? HierarchyMeaning
        {
            get { return HierarchyMeaningElement != null ? HierarchyMeaningElement.Value : null; }
            set
            {
                if (value == null)
                    HierarchyMeaningElement = null;
                else
                    HierarchyMeaningElement = new Code<Hl7.Fhir.Model.TestCodeSystem.CodeSystemHierarchyMeaning>(value);
                OnPropertyChanged("HierarchyMeaning");
            }
        }

        /// <summary>
        /// If code system defines a post-composition grammar
        /// </summary>
        [FhirElement("compositional", InSummary = true, Order = 270)]
        [DataMember]
        public Hl7.Fhir.Model.FhirBoolean CompositionalElement
        {
            get { return _CompositionalElement; }
            set { _CompositionalElement = value; OnPropertyChanged("CompositionalElement"); }
        }

        private Hl7.Fhir.Model.FhirBoolean _CompositionalElement;

        /// <summary>
        /// If code system defines a post-composition grammar
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [IgnoreDataMember]
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
        [FhirElement("versionNeeded", InSummary = true, Order = 280)]
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
        [IgnoreDataMember]
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
        /// not-present | example | fragment | complete
        /// </summary>
        [FhirElement("content", InSummary = true, Order = 290)]
        [DeclaredType(Type = typeof(Code))]
        [Cardinality(Min = 1, Max = 1)]
        [DataMember]
        public Code<Hl7.Fhir.Model.TestCodeSystem.CodeSystemContentMode> ContentElement
        {
            get { return _ContentElement; }
            set { _ContentElement = value; OnPropertyChanged("ContentElement"); }
        }

        private Code<Hl7.Fhir.Model.TestCodeSystem.CodeSystemContentMode> _ContentElement;

        /// <summary>
        /// not-present | example | fragment | complete
        /// </summary>
        /// <remarks>This uses the native .NET datatype, rather than the FHIR equivalent</remarks>
        [IgnoreDataMember]
        public Hl7.Fhir.Model.TestCodeSystem.CodeSystemContentMode? Content
        {
            get { return ContentElement != null ? ContentElement.Value : null; }
            set
            {
                if (value == null)
                    ContentElement = null;
                else
                    ContentElement = new Code<Hl7.Fhir.Model.TestCodeSystem.CodeSystemContentMode>(value);
                OnPropertyChanged("Content");
            }
        }

        /// <summary>
        /// Total concepts in the code system
        /// </summary>
        [FhirElement("count", InSummary = true, Order = 300)]
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
        [IgnoreDataMember]
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
        [FhirElement("filter", InSummary = true, Order = 310)]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.TestCodeSystem.FilterComponent> Filter
        {
            get { if (_Filter == null) _Filter = new List<Hl7.Fhir.Model.TestCodeSystem.FilterComponent>(); return _Filter; }
            set { _Filter = value; OnPropertyChanged("Filter"); }
        }

        private List<Hl7.Fhir.Model.TestCodeSystem.FilterComponent> _Filter;

        /// <summary>
        /// Additional information supplied about each concept
        /// </summary>
        [FhirElement("property", InSummary = true, Order = 320)]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.TestCodeSystem.PropertyComponent> Property
        {
            get { if (_Property == null) _Property = new List<Hl7.Fhir.Model.TestCodeSystem.PropertyComponent>(); return _Property; }
            set { _Property = value; OnPropertyChanged("Property"); }
        }

        private List<Hl7.Fhir.Model.TestCodeSystem.PropertyComponent> _Property;

        /// <summary>
        /// Concepts in the code system
        /// </summary>
        [FhirElement("concept", Order = 330)]
        [Cardinality(Min = 0, Max = -1)]
        [DataMember]
        public List<Hl7.Fhir.Model.TestCodeSystem.ConceptDefinitionComponent> Concept
        {
            get { if (_Concept == null) _Concept = new List<Hl7.Fhir.Model.TestCodeSystem.ConceptDefinitionComponent>(); return _Concept; }
            set { _Concept = value; OnPropertyChanged("Concept"); }
        }

        private List<Hl7.Fhir.Model.TestCodeSystem.ConceptDefinitionComponent> _Concept;

        public override IDeepCopyable CopyTo(IDeepCopyable other)
        {
            var dest = other as TestCodeSystem;

            if (dest == null)
            {
                throw new ArgumentException("Can only copy to an object of the same type", "other");
            }

            base.CopyTo(dest);
            if (UrlElement != null) dest.UrlElement = (Hl7.Fhir.Model.FhirUri)UrlElement.DeepCopy();
            if (Identifier != null) dest.Identifier = (Hl7.Fhir.Model.Identifier)Identifier.DeepCopy();
            if (VersionElement != null) dest.VersionElement = (Hl7.Fhir.Model.FhirString)VersionElement.DeepCopy();
            if (NameElement != null) dest.NameElement = (Hl7.Fhir.Model.FhirString)NameElement.DeepCopy();
            if (TitleElement != null) dest.TitleElement = (Hl7.Fhir.Model.FhirString)TitleElement.DeepCopy();
            if (StatusElement != null) dest.StatusElement = (Code<Hl7.Fhir.Model.PublicationStatus>)StatusElement.DeepCopy();
            if (ExperimentalElement != null) dest.ExperimentalElement = (Hl7.Fhir.Model.FhirBoolean)ExperimentalElement.DeepCopy();
            if (DateElement != null) dest.DateElement = (Hl7.Fhir.Model.FhirDateTime)DateElement.DeepCopy();
            if (PublisherElement != null) dest.PublisherElement = (Hl7.Fhir.Model.FhirString)PublisherElement.DeepCopy();
            if (Contact != null) dest.Contact = new List<Hl7.Fhir.Model.ContactDetail>(Contact.DeepCopy());
            if (Description != null) dest.Description = (Hl7.Fhir.Model.Markdown)Description.DeepCopy();
            if (UseContext != null) dest.UseContext = new List<Hl7.Fhir.Model.UsageContext>(UseContext.DeepCopy());
            if (Jurisdiction != null) dest.Jurisdiction = new List<Hl7.Fhir.Model.CodeableConcept>(Jurisdiction.DeepCopy());
            if (Purpose != null) dest.Purpose = (Hl7.Fhir.Model.Markdown)Purpose.DeepCopy();
            if (Copyright != null) dest.Copyright = (Hl7.Fhir.Model.Markdown)Copyright.DeepCopy();
            if (CaseSensitiveElement != null) dest.CaseSensitiveElement = (Hl7.Fhir.Model.FhirBoolean)CaseSensitiveElement.DeepCopy();
            if (ValueSetElement != null) dest.ValueSetElement = (Hl7.Fhir.Model.FhirUri)ValueSetElement.DeepCopy();
            if (HierarchyMeaningElement != null) dest.HierarchyMeaningElement = (Code<Hl7.Fhir.Model.TestCodeSystem.CodeSystemHierarchyMeaning>)HierarchyMeaningElement.DeepCopy();
            if (CompositionalElement != null) dest.CompositionalElement = (Hl7.Fhir.Model.FhirBoolean)CompositionalElement.DeepCopy();
            if (VersionNeededElement != null) dest.VersionNeededElement = (Hl7.Fhir.Model.FhirBoolean)VersionNeededElement.DeepCopy();
            if (ContentElement != null) dest.ContentElement = (Code<Hl7.Fhir.Model.TestCodeSystem.CodeSystemContentMode>)ContentElement.DeepCopy();
            if (CountElement != null) dest.CountElement = (Hl7.Fhir.Model.UnsignedInt)CountElement.DeepCopy();
            if (Filter != null) dest.Filter = new List<Hl7.Fhir.Model.TestCodeSystem.FilterComponent>(Filter.DeepCopy());
            if (Property != null) dest.Property = new List<Hl7.Fhir.Model.TestCodeSystem.PropertyComponent>(Property.DeepCopy());
            if (Concept != null) dest.Concept = new List<Hl7.Fhir.Model.TestCodeSystem.ConceptDefinitionComponent>(Concept.DeepCopy());
            return dest;
        }

        public override IDeepCopyable DeepCopy()
        {
            return CopyTo(new TestCodeSystem());
        }

        public override bool Matches(IDeepComparable other)
        {
            var otherT = other as TestCodeSystem;
            if (otherT == null) return false;

            if (!base.Matches(otherT)) return false;
            if (!DeepComparable.Matches(UrlElement, otherT.UrlElement)) return false;
            if (!DeepComparable.Matches(Identifier, otherT.Identifier)) return false;
            if (!DeepComparable.Matches(VersionElement, otherT.VersionElement)) return false;
            if (!DeepComparable.Matches(NameElement, otherT.NameElement)) return false;
            if (!DeepComparable.Matches(TitleElement, otherT.TitleElement)) return false;
            if (!DeepComparable.Matches(StatusElement, otherT.StatusElement)) return false;
            if (!DeepComparable.Matches(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if (!DeepComparable.Matches(DateElement, otherT.DateElement)) return false;
            if (!DeepComparable.Matches(PublisherElement, otherT.PublisherElement)) return false;
            if (!DeepComparable.Matches(Contact, otherT.Contact)) return false;
            if (!DeepComparable.Matches(Description, otherT.Description)) return false;
            if (!DeepComparable.Matches(UseContext, otherT.UseContext)) return false;
            if (!DeepComparable.Matches(Jurisdiction, otherT.Jurisdiction)) return false;
            if (!DeepComparable.Matches(Purpose, otherT.Purpose)) return false;
            if (!DeepComparable.Matches(Copyright, otherT.Copyright)) return false;
            if (!DeepComparable.Matches(CaseSensitiveElement, otherT.CaseSensitiveElement)) return false;
            if (!DeepComparable.Matches(ValueSetElement, otherT.ValueSetElement)) return false;
            if (!DeepComparable.Matches(HierarchyMeaningElement, otherT.HierarchyMeaningElement)) return false;
            if (!DeepComparable.Matches(CompositionalElement, otherT.CompositionalElement)) return false;
            if (!DeepComparable.Matches(VersionNeededElement, otherT.VersionNeededElement)) return false;
            if (!DeepComparable.Matches(ContentElement, otherT.ContentElement)) return false;
            if (!DeepComparable.Matches(CountElement, otherT.CountElement)) return false;
            if (!DeepComparable.Matches(Filter, otherT.Filter)) return false;
            if (!DeepComparable.Matches(Property, otherT.Property)) return false;
            if (!DeepComparable.Matches(Concept, otherT.Concept)) return false;

            return true;
        }

        public override bool IsExactly(IDeepComparable other)
        {
            var otherT = other as TestCodeSystem;
            if (otherT == null) return false;

            if (!base.IsExactly(otherT)) return false;
            if (!DeepComparable.IsExactly(UrlElement, otherT.UrlElement)) return false;
            if (!DeepComparable.IsExactly(Identifier, otherT.Identifier)) return false;
            if (!DeepComparable.IsExactly(VersionElement, otherT.VersionElement)) return false;
            if (!DeepComparable.IsExactly(NameElement, otherT.NameElement)) return false;
            if (!DeepComparable.IsExactly(TitleElement, otherT.TitleElement)) return false;
            if (!DeepComparable.IsExactly(StatusElement, otherT.StatusElement)) return false;
            if (!DeepComparable.IsExactly(ExperimentalElement, otherT.ExperimentalElement)) return false;
            if (!DeepComparable.IsExactly(DateElement, otherT.DateElement)) return false;
            if (!DeepComparable.IsExactly(PublisherElement, otherT.PublisherElement)) return false;
            if (!DeepComparable.IsExactly(Contact, otherT.Contact)) return false;
            if (!DeepComparable.IsExactly(Description, otherT.Description)) return false;
            if (!DeepComparable.IsExactly(UseContext, otherT.UseContext)) return false;
            if (!DeepComparable.IsExactly(Jurisdiction, otherT.Jurisdiction)) return false;
            if (!DeepComparable.IsExactly(Purpose, otherT.Purpose)) return false;
            if (!DeepComparable.IsExactly(Copyright, otherT.Copyright)) return false;
            if (!DeepComparable.IsExactly(CaseSensitiveElement, otherT.CaseSensitiveElement)) return false;
            if (!DeepComparable.IsExactly(ValueSetElement, otherT.ValueSetElement)) return false;
            if (!DeepComparable.IsExactly(HierarchyMeaningElement, otherT.HierarchyMeaningElement)) return false;
            if (!DeepComparable.IsExactly(CompositionalElement, otherT.CompositionalElement)) return false;
            if (!DeepComparable.IsExactly(VersionNeededElement, otherT.VersionNeededElement)) return false;
            if (!DeepComparable.IsExactly(ContentElement, otherT.ContentElement)) return false;
            if (!DeepComparable.IsExactly(CountElement, otherT.CountElement)) return false;
            if (!DeepComparable.IsExactly(Filter, otherT.Filter)) return false;
            if (!DeepComparable.IsExactly(Property, otherT.Property)) return false;
            if (!DeepComparable.IsExactly(Concept, otherT.Concept)) return false;

            return true;
        }

        [IgnoreDataMember]
        public override IEnumerable<Base> Children
        {
            get
            {
                foreach (var item in base.Children) yield return item;
                if (UrlElement != null) yield return UrlElement;
                if (Identifier != null) yield return Identifier;
                if (VersionElement != null) yield return VersionElement;
                if (NameElement != null) yield return NameElement;
                if (TitleElement != null) yield return TitleElement;
                if (StatusElement != null) yield return StatusElement;
                if (ExperimentalElement != null) yield return ExperimentalElement;
                if (DateElement != null) yield return DateElement;
                if (PublisherElement != null) yield return PublisherElement;
                foreach (var elem in Contact) { if (elem != null) yield return elem; }
                if (Description != null) yield return Description;
                foreach (var elem in UseContext) { if (elem != null) yield return elem; }
                foreach (var elem in Jurisdiction) { if (elem != null) yield return elem; }
                if (Purpose != null) yield return Purpose;
                if (Copyright != null) yield return Copyright;
                if (CaseSensitiveElement != null) yield return CaseSensitiveElement;
                if (ValueSetElement != null) yield return ValueSetElement;
                if (HierarchyMeaningElement != null) yield return HierarchyMeaningElement;
                if (CompositionalElement != null) yield return CompositionalElement;
                if (VersionNeededElement != null) yield return VersionNeededElement;
                if (ContentElement != null) yield return ContentElement;
                if (CountElement != null) yield return CountElement;
                foreach (var elem in Filter) { if (elem != null) yield return elem; }
                foreach (var elem in Property) { if (elem != null) yield return elem; }
                foreach (var elem in Concept) { if (elem != null) yield return elem; }
            }
        }

        [IgnoreDataMember]
        public override IEnumerable<ElementValue> NamedChildren
        {
            get
            {
                foreach (var item in base.NamedChildren) yield return item;
                if (UrlElement != null) yield return new ElementValue("url", UrlElement);
                if (Identifier != null) yield return new ElementValue("identifier", Identifier);
                if (VersionElement != null) yield return new ElementValue("version", VersionElement);
                if (NameElement != null) yield return new ElementValue("name", NameElement);
                if (TitleElement != null) yield return new ElementValue("title", TitleElement);
                if (StatusElement != null) yield return new ElementValue("status", StatusElement);
                if (ExperimentalElement != null) yield return new ElementValue("experimental", ExperimentalElement);
                if (DateElement != null) yield return new ElementValue("date", DateElement);
                if (PublisherElement != null) yield return new ElementValue("publisher", PublisherElement);
                foreach (var elem in Contact) { if (elem != null) yield return new ElementValue("contact", elem); }
                if (Description != null) yield return new ElementValue("description", Description);
                foreach (var elem in UseContext) { if (elem != null) yield return new ElementValue("useContext", elem); }
                foreach (var elem in Jurisdiction) { if (elem != null) yield return new ElementValue("jurisdiction", elem); }
                if (Purpose != null) yield return new ElementValue("purpose", Purpose);
                if (Copyright != null) yield return new ElementValue("copyright", Copyright);
                if (CaseSensitiveElement != null) yield return new ElementValue("caseSensitive", CaseSensitiveElement);
                if (ValueSetElement != null) yield return new ElementValue("valueSet", ValueSetElement);
                if (HierarchyMeaningElement != null) yield return new ElementValue("hierarchyMeaning", HierarchyMeaningElement);
                if (CompositionalElement != null) yield return new ElementValue("compositional", CompositionalElement);
                if (VersionNeededElement != null) yield return new ElementValue("versionNeeded", VersionNeededElement);
                if (ContentElement != null) yield return new ElementValue("content", ContentElement);
                if (CountElement != null) yield return new ElementValue("count", CountElement);
                foreach (var elem in Filter) { if (elem != null) yield return new ElementValue("filter", elem); }
                foreach (var elem in Property) { if (elem != null) yield return new ElementValue("property", elem); }
                foreach (var elem in Concept) { if (elem != null) yield return new ElementValue("concept", elem); }
            }
        }

        protected override bool TryGetValue(string key, out object value)
        {
            switch (key)
            {
                case "url":
                    value = UrlElement;
                    return UrlElement is not null;
                case "identifier":
                    value = Identifier;
                    return Identifier is not null;
                case "version":
                    value = VersionElement;
                    return VersionElement is not null;
                case "name":
                    value = NameElement;
                    return NameElement is not null;
                case "title":
                    value = TitleElement;
                    return TitleElement is not null;
                case "status":
                    value = StatusElement;
                    return StatusElement is not null;
                case "experimental":
                    value = ExperimentalElement;
                    return ExperimentalElement is not null;
                case "date":
                    value = DateElement;
                    return DateElement is not null;
                case "publisher":
                    value = PublisherElement;
                    return PublisherElement is not null;
                case "contact":
                    value = Contact;
                    return Contact?.Any() == true;
                case "description":
                    value = Description;
                    return Description is not null;
                case "useContext":
                    value = UseContext;
                    return UseContext?.Any() == true;
                case "jurisdiction":
                    value = Jurisdiction;
                    return Jurisdiction?.Any() == true;
                case "purpose":
                    value = Purpose;
                    return Purpose is not null;
                case "copyright":
                    value = Copyright;
                    return Copyright is not null;
                case "caseSensitive":
                    value = CaseSensitiveElement;
                    return CaseSensitiveElement is not null;
                case "valueSet":
                    value = ValueSetElement;
                    return ValueSetElement is not null;
                case "hierarchyMeaning":
                    value = HierarchyMeaningElement;
                    return HierarchyMeaningElement is not null;
                case "compositional":
                    value = CompositionalElement;
                    return CompositionalElement is not null;
                case "versionNeeded":
                    value = VersionNeededElement;
                    return VersionNeededElement is not null;
                case "content":
                    value = ContentElement;
                    return ContentElement is not null;
                case "count":
                    value = CountElement;
                    return CountElement is not null;
                case "filter":
                    value = Filter;
                    return Filter?.Any() == true;
                case "property":
                    value = Property;
                    return Property?.Any() == true;
                case "concept":
                    value = Concept;
                    return Concept?.Any() == true;
                default:
                    return base.TryGetValue(key, out value);
            };

        }

        protected override IEnumerable<KeyValuePair<string, object>> GetElementPairs()
        {
            foreach (var kvp in base.GetElementPairs()) yield return kvp;
            if (UrlElement is not null) yield return new KeyValuePair<string, object>("url", UrlElement);
            if (Identifier is not null) yield return new KeyValuePair<string, object>("identifier", Identifier);
            if (VersionElement is not null) yield return new KeyValuePair<string, object>("version", VersionElement);
            if (NameElement is not null) yield return new KeyValuePair<string, object>("name", NameElement);
            if (TitleElement is not null) yield return new KeyValuePair<string, object>("title", TitleElement);
            if (StatusElement is not null) yield return new KeyValuePair<string, object>("status", StatusElement);
            if (ExperimentalElement is not null) yield return new KeyValuePair<string, object>("experimental", ExperimentalElement);
            if (DateElement is not null) yield return new KeyValuePair<string, object>("date", DateElement);
            if (PublisherElement is not null) yield return new KeyValuePair<string, object>("publisher", PublisherElement);
            if (Contact?.Any() == true) yield return new KeyValuePair<string, object>("contact", Contact);
            if (Description is not null) yield return new KeyValuePair<string, object>("description", Description);
            if (UseContext?.Any() == true) yield return new KeyValuePair<string, object>("useContext", UseContext);
            if (Jurisdiction?.Any() == true) yield return new KeyValuePair<string, object>("jurisdiction", Jurisdiction);
            if (Purpose is not null) yield return new KeyValuePair<string, object>("purpose", Purpose);
            if (Copyright is not null) yield return new KeyValuePair<string, object>("copyright", Copyright);
            if (CaseSensitiveElement is not null) yield return new KeyValuePair<string, object>("caseSensitive", CaseSensitiveElement);
            if (ValueSetElement is not null) yield return new KeyValuePair<string, object>("valueSet", ValueSetElement);
            if (HierarchyMeaningElement is not null) yield return new KeyValuePair<string, object>("hierarchyMeaning", HierarchyMeaningElement);
            if (CompositionalElement is not null) yield return new KeyValuePair<string, object>("compositional", CompositionalElement);
            if (VersionNeededElement is not null) yield return new KeyValuePair<string, object>("versionNeeded", VersionNeededElement);
            if (ContentElement is not null) yield return new KeyValuePair<string, object>("content", ContentElement);
            if (CountElement is not null) yield return new KeyValuePair<string, object>("count", CountElement);
            if (Filter?.Any() == true) yield return new KeyValuePair<string, object>("filter", Filter);
            if (Property?.Any() == true) yield return new KeyValuePair<string, object>("property", Property);
            if (Concept?.Any() == true) yield return new KeyValuePair<string, object>("concept", Concept);
        }

    }

}

// end of file
