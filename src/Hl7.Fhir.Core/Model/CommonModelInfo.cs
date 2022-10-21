/* 
 * Copyright (c) 2022, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Utility;
using System;
using System.Diagnostics;

namespace Hl7.Fhir.Model
{
    public class CommonModelInfo
    {
        private readonly ModelInspector _inspector;

        public CommonModelInfo(ModelInspector inspector)
        {
            _inspector = inspector;
        }

        public static readonly Uri FhirCoreProfileBaseUri = new(@"http://hl7.org/fhir/StructureDefinition/");

        /// <summary>Returns the C# <see cref="Type"/> that represents the FHIR type with the specified name, or <c>null</c>.</summary>
        public Type? CommonGetTypeForFhirType(string name) => CommonGetTypeForFhirType(_inspector, name);

        public static Type? CommonGetTypeForFhirType(ModelInspector inspector, string name) => inspector.FindClassMapping(name) is { } mapping ? mapping.NativeType : null;

        /// <summary>Returns the FHIR type name represented by the specified C# <see cref="Type"/>, or <c>null</c>.</summary>
        public string? GetCommonFhirTypeNameForType(Type type) => GetCommonFhirTypeNameForType(_inspector, type);

        public static string? GetCommonFhirTypeNameForType2(Type type) => GetCommonFhirTypeNameForType(ModelInspector.ForAssembly(type.Assembly), type);

        public static string? GetCommonFhirTypeNameForType(ModelInspector inspector, Type type) => inspector.FindClassMapping(type) is { } mapping ? mapping.Name : null;

        /// <summary>Determines if the specified value represents the name of a known FHIR resource.</summary>
        public bool CommonIsKnownResource(string name) => _inspector.FindClassMapping(name) is { } mapping && mapping.IsResource;

        /// <summary>Determines if the specified <see cref="Type"/> instance represents a known FHIR resource.</summary>
        public bool CommonIsKnownResource(Type type) => _inspector.FindClassMapping(type) is { } mapping && mapping.IsResource;

        /// <summary>Determines if the specified value represents the name of a FHIR primitive data type.</summary>
        public bool CommonIsPrimitive(string name) => CommonIsPrimitive(_inspector, name);
        /// <summary>Determines if the specified <see cref="Type"/> instance represents a FHIR primitive data type.</summary>
        public bool CommonIsPrimitive(Type type) => CommonIsPrimitive(_inspector, type);

        public static bool CommonIsPrimitive(ModelInspector inspector, Type type) => inspector.FindClassMapping(type)?.IsFhirPrimitive ?? false;
        public static bool CommonIsPrimitive(ModelInspector inspector, string name) => inspector.FindClassMapping(name)?.IsFhirPrimitive ?? false;
        public static bool CommonIsPrimitive2(string name) => CommonIsPrimitive(ModelInspector.ForAssembly(typeof(PrimitiveType).Assembly), name);
        public static bool CommonIsPrimitive2(Type type) => CommonIsPrimitive(ModelInspector.ForAssembly(typeof(PrimitiveType).Assembly), type);

        /// <summary>Determines if the specified value represents the name of a FHIR complex data type (NOT including resources and primitives).</summary>
        public bool CommonIsDataType(string name) => CommonIsDataType(_inspector, name);
        /// <summary>Determines if the specified <see cref="Type"/> instance represents a FHIR complex data type (NOT including resources and primitives).</summary>
        public bool CommonIsDataType(Type type) => CommonIsDataType(_inspector, type);

        public static bool CommonIsDataType(ModelInspector inspector, string name) => inspector.FindClassMapping(name) is { } mapping && !mapping.IsFhirPrimitive && !mapping.IsResource;
        public static bool CommonIsDataType(ModelInspector inspector, Type type) => inspector.FindClassMapping(type) is { } mapping && !mapping.IsFhirPrimitive && !mapping.IsResource;

        /// <summary>Determines if the specified value represents the type name of a FHIR Reference, i.e. equals "Reference".</summary>
        public bool CommonIsReference(string name) => CommonGetTypeForFhirType(name) is { } type && CommonIsReference(type);

        /// <summary>Determines if the specified <see cref="Type"/> instance represents a FHIR Reference type.</summary>
        public bool CommonIsReference(Type type) => type.CanBeTreatedAsType(typeof(ResourceReference));

        /// <summary>
        /// Determines if the specified <see cref="Type"/> value represents a FHIR conformance resource type
        /// (resources under the Conformance/Terminology/Implementation Support header in resourcelist.html)
        /// </summary>
        public bool CommonIsConformanceResource(Type type) => type.CanBeTreatedAsType(typeof(IConformanceResource));

        /// <summary>
        /// Determines if the specified value represents a FHIR conformance resource type
        /// (resources under the Conformance/Terminology/Implementation Support header in resourcelist.html)
        /// </summary>
        public bool CommonIsConformanceResource(string name) => CommonGetTypeForFhirType(name) is { } type && CommonIsConformanceResource(type);

        /// <summary>Determines if the specified value represents the canonical uri of a core FHIR Resource, FHIR Datatype or FHIR primitive.</summary>
        /// <remarks>This function does not recognize "system" types, these are the basic types that the FHIR
        /// datatypes are built upon, but are not specific to the FHIR datamodel.</remarks>
        public bool CommonIsCoreModelType(string name) => CommonIsCoreModelType(_inspector, name);
        public static bool CommonIsCoreModelType(ModelInspector inspector, string name) => inspector.FindClassMapping(name) is not null;
        /// <summary>Determines if the specified value represents the type of a core Resource, Datatype or primitive.</summary>
        public bool CommonIsCoreModelType(Type type) => _inspector.FindClassMapping(type) is not null;

        /// <summary>Determines if the specified value represents the canonical uri of a core FHIR Resource, FHIR Datatype or FHIR primitive.</summary>
        /// <remarks>This function does not recognize "system" types, these are the basic types that the FHIR
        /// datatypes are built upon, but are not specific to the FHIR datamodel.</remarks>
        public bool CommonIsCoreModelTypeUri(Uri uri) =>
            uri is not null
                // [WMR 20181025] Issue #746
                // Note: FhirCoreProfileBaseUri.IsBaseOf(new Uri("Dummy", UriKind.RelativeOrAbsolute)) = true...?!
                && uri.IsAbsoluteUri
                && FhirCoreProfileBaseUri.IsBaseOf(uri)
                && CommonIsCoreModelType(FhirCoreProfileBaseUri.MakeRelativeUri(uri).ToString());

        /// <summary>
        /// Returns whether the type has subclasses in the core spec.
        /// </summary>
        public static bool CommonIsCoreSuperType(Type type) =>
            type == typeof(Base) ||
            type == typeof(Resource) ||
            type == typeof(DomainResource) ||
            type == typeof(Element) ||
            type == typeof(BackboneElement) ||
            type == typeof(DataType) ||
            type == typeof(PrimitiveType) ||
            type == typeof(BackboneType);

        /// <summary>
        /// Returns whether the value has subclasses in the core spec.
        /// </summary>
        public bool CommonIsCoreSuperType(string name) => CommonGetTypeForFhirType(name) is { } type && CommonIsCoreSuperType(type);

        public bool CommonIsInstanceTypeFor(string superclass, string subclass)
        {
            var superType = CommonGetTypeForFhirType(superclass);
            var subType = CommonGetTypeForFhirType(subclass);

            return subType is not null && superType is not null && CommonIsInstanceTypeFor(superType, subType);
        }

        public static bool CommonIsInstanceTypeFor(ModelInspector inspector, string superclass, string subclass) =>
            CommonGetTypeForFhirType(inspector, superclass) is { } superType &&
            CommonGetTypeForFhirType(inspector, subclass) is { } subType &&
            CommonIsInstanceTypeFor(superType, subType);

        public static bool CommonIsInstanceTypeFor(Type superclass, Type subclass) => superclass == subclass || superclass.IsAssignableFrom(subclass);

        public bool CommonIsBindable(string type) => CommonIsBindable(_inspector, type);
        public static bool CommonIsBindable(ModelInspector inspector, string type) => inspector.FindClassMapping(type) is { } mapping ? mapping.IsBindable : false;
        public static bool CommonIsBindable2(string type) => CommonIsBindable(ModelInspector.ForAssembly(typeof(PrimitiveType).Assembly), type);

        public static Canonical CommonCanonicalUriForFhirCoreType(string typename) => new("http://hl7.org/fhir/StructureDefinition/" + typename);

        public Canonical? CommonCanonicalUriForFhirCoreType(Type type) => GetCommonFhirTypeNameForType(type) is { } name ? CommonModelInfo.CommonCanonicalUriForFhirCoreType(name) : null;




        [System.Diagnostics.DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")] // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
        public class SearchParamDefinition
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private string DebuggerDisplay
            {
                get
                {
                    return String.Format("{0} {1} {2} ({3})", Resource, Name, Type, Expression);
                }
            }

            public string? Resource { get; set; }
            public string? Name { get; set; }
            public string? Url { get; set; }
            public Markdown? Description { get; set; }
            public SearchParamType Type { get; set; }

            /// <summary>
            /// If this search parameter is a Composite, this array contains 
            /// the list of search parameters the param is a combination of
            /// </summary>
            public string[]? CompositeParams { get; set; }

            /// <summary>
            /// One or more paths into the Resource instance that the search parameter 
            /// uses 
            /// </summary>
            public string[]? Path { get; set; }

            /// <summary>
            /// The XPath expression for evaluating this search parameter
            /// </summary>
            public string? XPath { get; set; }

            /// <summary>
            /// The FHIR Path expresssion that can be used to extract the data
            /// for this search parameter
            /// </summary>
            public string? Expression { get; set; }

            /// <summary>
            /// If this is a reference, the possible types of resources that the
            /// parameters references to
            /// </summary>
            public ResourceType[]? Target { get; set; }
        }

    }
}
#nullable restore