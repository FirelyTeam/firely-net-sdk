/*
  Copyright (c) 2011-2012, HL7, Inc
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

using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Support;
using Hl7.Fhir.Introspection;
using System.Diagnostics;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Model
{
    public partial class ModelInfo
    {
        [System.Diagnostics.DebuggerDisplay(@"\{{DebuggerDisplay,nq}}")] // http://blogs.msdn.com/b/jaredpar/archive/2011/03/18/debuggerdisplay-attribute-best-practices.aspx
        public class SearchParamDefinition
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            [NotMapped]
            private string DebuggerDisplay
            {
                get
                {
                    return String.Format("{0} {1} {2} ({3})", Resource, Name, Type, Expression);
                }
            }

            public string Resource { get; set; }
            public string Name { get; set; }
            public string Url { get; set; }
            public string Description { get; set; }
            public SearchParamType Type { get; set; }

            /// <summary>
            /// If this search parameter is a Composite, this array contains 
            /// the list of search parameters the param is a combination of
            /// </summary>
            public string[] CompositeParams { get; set; }

            /// <summary>
            /// One or more paths into the Resource instance that the search parameter 
            /// uses 
            /// </summary>
            public string[] Path { get; set; }

            /// <summary>
            /// The XPath expression for evaluating this search parameter
            /// </summary>
            public string XPath { get; set; }

            /// <summary>
            /// The FHIR Path expresssion that can be used to extract the data
            /// for this search parameter
            /// </summary>
            public string Expression { get; set; }

            /// <summary>
            /// If this is a reference, the possible types of resources that the
            /// parameters references to
            /// </summary>
            public ResourceType[] Target { get; set; }
        }

        // [WMR 2017-10-25] Remove Lazy initialization
        // These methods are used frequently throughout the API (and by clients) and initialization cost is low

        static readonly Dictionary<string, FHIRAllTypes> _fhirTypeNameToFhirType
            = Enum.GetValues(typeof(FHIRAllTypes)).OfType<FHIRAllTypes>().ToDictionary(type => type.GetLiteral());

        static readonly Dictionary<FHIRAllTypes, string> _fhirTypeToFhirTypeName
            = _fhirTypeNameToFhirType.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

        /// <summary>Returns the FHIR type name represented by the specified <see cref="FHIRAllTypes"/> enum value, or <c>null</c>.</summary>
        public static FHIRAllTypes? FhirTypeNameToFhirType(string typeName)
            => _fhirTypeNameToFhirType.TryGetValue(typeName, out var result) ? (FHIRAllTypes?)result : null;

        /// <summary>Returns the <see cref="FHIRAllTypes"/> enum value that represents the specified FHIR type name, or <c>null</c>.</summary>
        public static string FhirTypeToFhirTypeName(FHIRAllTypes type)
            => _fhirTypeToFhirTypeName.TryGetValue(type, out var result) ? result : null;

        // [WMR 20171025] NEW: Conversion methods for ResourceType

        private static readonly Dictionary<string, ResourceType> _fhirTypeNameToResourceType
            = Enum.GetValues(typeof(ResourceType)).OfType<ResourceType>().ToDictionary(type => type.GetLiteral());

        private static readonly Dictionary<ResourceType, string> _resourceTypeToFhirTypeName
            = _fhirTypeNameToResourceType.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

        /// <summary>Returns the FHIR type name represented by the specified <see cref="ResourceType"/> enum value, or <c>null</c>.</summary>
        public static ResourceType? FhirTypeNameToResourceType(string typeName)
            => _fhirTypeNameToResourceType.TryGetValue(typeName, out var result) ? (ResourceType?)result : null;

        /// <summary>Returns the <see cref="ResourceType"/> enum value that represents the specified FHIR type name, or <c>null</c>.</summary>
        public static string ResourceTypeToFhirTypeName(ResourceType type)
            => _resourceTypeToFhirTypeName.TryGetValue(type, out var result) ? result : null;

        /// <summary>Returns the C# <see cref="Type"/> that represents the FHIR type with the specified name, or <c>null</c>.</summary>
        public static Type GetTypeForFhirType(string name)
        {
            return FhirTypeToCsType.TryGetValue(name, out var result) ? result : null;
        }

        /// <summary>Returns the FHIR type name represented by the specified C# <see cref="Type"/>, or <c>null</c>.</summary>
        public static string GetFhirTypeNameForType(Type type)
        {
            return FhirCsTypeToString.TryGetValue(type, out var result) ? result : null;
        }

        [Obsolete("Use GetFhirTypeNameForType() instead")]
        public static string GetFhirTypeForType(Type type)
        {
            return GetFhirTypeNameForType(type);
        }

        /// <summary>Determines if the specified value represents the name of a known FHIR resource.</summary>
        public static bool IsKnownResource(string name)
        {
            return SupportedResources.Contains(name);
        }

        /// <summary>Determines if the specified <see cref="Type"/> instance represents a known FHIR resource.</summary>
        public static bool IsKnownResource(Type type)
        {
            var name = GetFhirTypeNameForType(type);

            return name != null && IsKnownResource(name);
        }

        /// <summary>Determines if the specified <see cref="FHIRAllTypes"/> value represents a known FHIR resource.</summary>
        public static bool IsKnownResource(FHIRAllTypes type)
        {
            var name = FhirTypeToFhirTypeName(type);
            return name != null && IsKnownResource(name);
        }

        [Obsolete("Use GetTypeForFhirType() which covers all types, not just resources")]
        public static Type GetTypeForResourceName(string name)
        {
            if (!IsKnownResource(name)) return null;

            return GetTypeForFhirType(name);
        }

        [Obsolete("Use GetFhirTypeNameForType() which covers all types, not just resources")]
        public static string GetResourceNameForType(Type type)
        {
            var name = GetFhirTypeForType(type);

            if (name != null && IsKnownResource(name))
                return name;
            else
                return null;
        }

        /// <summary>Determines if the specified value represents the name of a FHIR primitive data type.</summary>
        public static bool IsPrimitive(string name)
        {
            if (String.IsNullOrEmpty(name)) return false;

            return FhirTypeToCsType.ContainsKey(name) && Char.IsLower(name[0]);
        }

        /// <summary>Determines if the specified <see cref="Type"/> instance represents a FHIR primitive data type.</summary>
        public static bool IsPrimitive(Type type)
        {
            var name = GetFhirTypeNameForType(type);

            return name != null && Char.IsLower(name[0]);
        }

        /// <summary>Determines if the specified <see cref="FHIRAllTypes"/> value represents a FHIR primitive data type.</summary>
        public static bool IsPrimitive(FHIRAllTypes type)
        {
            return IsPrimitive(FhirTypeToFhirTypeName(type));
        }

        /// <summary>Determines if the specified value represents the name of a FHIR complex data type (NOT including resources and primitives).</summary>
        public static bool IsDataType(string name)
        {
            if (String.IsNullOrEmpty(name)) return false;

            return FhirTypeToCsType.ContainsKey(name) && !IsKnownResource(name) && !IsPrimitive(name);
        }

        /// <summary>Determines if the specified <see cref="Type"/> instance represents a FHIR complex data type (NOT including resources and primitives).</summary>
        public static bool IsDataType(Type type)
        {
            var name = GetFhirTypeNameForType(type);

            return name != null && !IsKnownResource(name) && !IsPrimitive(name);
        }

        /// <summary>Determines if the specified <see cref="FHIRAllTypes"/> value represents a FHIR complex data type (NOT including resources and primitives).</summary>
        public static bool IsDataType(FHIRAllTypes type)
        {
            return IsDataType(FhirTypeToFhirTypeName(type));
        }

        // [WMR 20160421] Dynamically resolve FHIR type name 'Reference'
        private static readonly string _referenceTypeName = FHIRAllTypes.Reference.GetLiteral();

        /// <summary>Determines if the specified value represents the type name of a FHIR Reference, i.e. equals "Reference".</summary>
        public static bool IsReference(string name)
        {
            return name == _referenceTypeName; // "Reference";
        }

        /// <summary>Determines if the specified <see cref="Type"/> instance represents a FHIR Reference type.</summary>
        public static bool IsReference(Type type)
        {
            return IsReference(type.Name);
        }

        /// <summary>Determines if the specified <see cref="FHIRAllTypes"/> value represents a FHIR Reference type.</summary>
        public static bool IsReference(FHIRAllTypes type)
        {
            return type == FHIRAllTypes.Reference;
        }

        /// <summary>
        /// Determines if the specified <see cref="FHIRAllTypes"/> value represents a FHIR conformance resource type
        /// (resources under the Conformance/Terminology/Implementation Support header in resourcelist.html)
        /// </summary>
        public static bool IsConformanceResource(Type type)
        {
            return IsConformanceResource(type.Name);
        }

        /// <summary>
        /// Determines if the specified <see cref="FHIRAllTypes"/> value represents a FHIR conformance resource type
        /// (resources under the Conformance/Terminology/Implementation Support header in resourcelist.html)
        /// </summary>
        public static bool IsConformanceResource(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;

            var t = FhirTypeNameToFhirType(name);

            if (t.HasValue)
                return IsConformanceResource(t.Value);
            else
                return false;
        }

        /// <summary>Subset of <see cref="FHIRAllTypes"/> enumeration values for conformance resources.</summary>
        public static readonly FHIRAllTypes[] ConformanceResources = 
        {
            FHIRAllTypes.StructureDefinition,
            FHIRAllTypes.StructureMap,
            FHIRAllTypes.DataElement,
            FHIRAllTypes.CapabilityStatement,
            FHIRAllTypes.MessageDefinition,
            FHIRAllTypes.OperationDefinition,
            FHIRAllTypes.SearchParameter,
            FHIRAllTypes.CompartmentDefinition,
            FHIRAllTypes.ImplementationGuide,
            FHIRAllTypes.CodeSystem,
            FHIRAllTypes.ValueSet,
            FHIRAllTypes.ConceptMap,
            FHIRAllTypes.ExpansionProfile,
            FHIRAllTypes.NamingSystem,
            FHIRAllTypes.TestScript,
            FHIRAllTypes.TestReport
        };

        /// <summary>
        /// Determines if the specified <see cref="FHIRAllTypes"/> value represents a FHIR conformance resource type
        /// (resources under the Conformance/Terminology/Implementation Support header in resourcelist.html)
        /// </summary>
        public static bool IsConformanceResource(FHIRAllTypes type) => ConformanceResources.Contains(type);

        /// <summary>
        /// Determines if the specified <see cref="FHIRAllTypes"/> value represents a FHIR conformance resource type
        /// (resources under the Conformance/Terminology/Implementation Support header in resourcelist.html)
        /// </summary>
        public static bool IsConformanceResource(FHIRAllTypes? type) => type.HasValue && ConformanceResources.Contains(type.Value);

        /// <summary>Subset of <see cref="ResourceType"/> enumeration values for conformance resources.</summary>
        public static readonly ResourceType[] ConformanceResourceTypes =
        {
            ResourceType.StructureDefinition,
            ResourceType.StructureMap,
            ResourceType.DataElement,
            ResourceType.CapabilityStatement,
            ResourceType.MessageDefinition,
            ResourceType.OperationDefinition,
            ResourceType.SearchParameter,
            ResourceType.CompartmentDefinition,
            ResourceType.ImplementationGuide,
            ResourceType.CodeSystem,
            ResourceType.ValueSet,
            ResourceType.ConceptMap,
            ResourceType.ExpansionProfile,
            ResourceType.NamingSystem,
            ResourceType.TestScript,
            ResourceType.TestReport
        };

        /// <summary>
        /// Determines if the specified <see cref="ResourceType"/> value represents a FHIR conformance resource type
        /// (resources under the Conformance/Terminology/Implementation Support header in resourcelist.html)
        /// </summary>
        public static bool IsConformanceResource(ResourceType type) => ConformanceResourceTypes.Contains(type);

        /// <summary>
        /// Determines if the specified <see cref="ResourceType"/> value represents a FHIR conformance resource type
        /// (resources under the Conformance/Terminology/Implementation Support header in resourcelist.html)
        /// </summary>
        public static bool IsConformanceResource(ResourceType? type) => type.HasValue && ConformanceResourceTypes.Contains(type.Value);


        /// <summary>Determines if the specified value represents the name of a core Resource, Datatype or primitive.</summary>
        public static bool IsCoreModelType(string name) => FhirTypeToCsType.ContainsKey(name);
            // => IsKnownResource(name) || IsDataType(name) || IsPrimitive(name);

        
        public static readonly Uri FhirCoreProfileBaseUri = new Uri(@"http://hl7.org/fhir/StructureDefinition/");

        /// <summary>Determines if the specified value represents the canonical uri of a core Resource, Datatype or primitive.</summary>
        public static bool IsCoreModelTypeUri(Uri uri)
        {
            return uri != null
                && FhirCoreProfileBaseUri.IsBaseOf(uri)
                && IsCoreModelType(FhirCoreProfileBaseUri.MakeRelativeUri(uri).ToString());
        }

        /// <summary>
        /// Returns whether the type has subclasses in the core spec
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <remarks>Quantity is not listed here, since its subclasses are
        /// actually profiles on Quantity. Likewise, there is no real inheritance
        /// in the primitives, so string is not a superclass for markdown</remarks>
        public static bool IsCoreSuperType(FHIRAllTypes type)
        {
            return
                type == FHIRAllTypes.Resource ||
                type == FHIRAllTypes.DomainResource ||
                type == FHIRAllTypes.Element ||
                type == FHIRAllTypes.BackboneElement;
        }

        public static bool IsCoreSuperType(string type)
        {
            var fat = FhirTypeNameToFhirType(type);

            if (fat == null) return false;

            return IsCoreSuperType(fat.Value);
        }

        public static bool IsProfiledQuantity(FHIRAllTypes type)
        {
            return type == FHIRAllTypes.SimpleQuantity;
        }
        
        public static bool IsProfiledQuantity(string type)
        {
            var definedType = FhirTypeNameToFhirType(type);
            if (definedType == null) return false;

            return IsProfiledQuantity(definedType.Value);
        }

        public static bool IsInstanceTypeFor(string superclass, string subclass)
        {
            var superType = FhirTypeNameToFhirType(superclass);
            var subType = FhirTypeNameToFhirType(subclass);

            if (subType == null || superType == null) return false;

            return IsInstanceTypeFor(superType.Value, subType.Value);
        }

        private static readonly FHIRAllTypes[] QUANTITY_SUBCLASSES = new[] { FHIRAllTypes.Age, FHIRAllTypes.Distance, FHIRAllTypes.Duration,
                            FHIRAllTypes.Count, FHIRAllTypes.Money };
        private static readonly FHIRAllTypes[] STRING_SUBCLASSES = new[] { FHIRAllTypes.Code, FHIRAllTypes.Id, FHIRAllTypes.Markdown };
        private static readonly FHIRAllTypes[] INTEGER_SUBCLASSES = new[] { FHIRAllTypes.UnsignedInt, FHIRAllTypes.PositiveInt };

        public static bool IsInstanceTypeFor(FHIRAllTypes superclass, FHIRAllTypes subclass)
        {
            if (superclass == subclass) return true;

            if (IsKnownResource(subclass))
            {
                if (superclass == FHIRAllTypes.Resource)
                    return true;
                else if (superclass == FHIRAllTypes.DomainResource)
                    return subclass != FHIRAllTypes.Parameters && subclass != FHIRAllTypes.Bundle && subclass != FHIRAllTypes.Binary;
                else
                    return false;
            }
            else
            {
                if (superclass == FHIRAllTypes.Element)
                    return true;
                else if (superclass == FHIRAllTypes.Quantity)
                    return QUANTITY_SUBCLASSES.Contains(subclass);
                else if (superclass == FHIRAllTypes.String)
                    return STRING_SUBCLASSES.Contains(subclass);
                else if (superclass == FHIRAllTypes.Integer)
                    return INTEGER_SUBCLASSES.Contains(subclass);
                else
                    return false;
            }
        }

        public static string CanonicalUriForFhirCoreType(string typename)
        {
            return "http://hl7.org/fhir/StructureDefinition/" + typename;
        }

        public static string CanonicalUriForFhirCoreType(FHIRAllTypes type)
        {
            return CanonicalUriForFhirCoreType(type.GetLiteral());
        }

    }

    public static class ModelInfoExtensions
    {
        public static string GetCollectionName(this Type type)
        {
            if (type.CanBeTreatedAsType(typeof(Resource)))
                return ModelInfo.GetFhirTypeNameForType(type);
            else
                throw new ArgumentException(String.Format(
                    "Cannot determine collection name, type {0} is not a resource type", type.Name));
        }
    }

}
