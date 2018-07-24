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

#if false
        // [WMR 20160421] Slow, based on reflection...
        public static string FhirTypeToFhirTypeName(FHIRDefinedType type)
        {
            return type.GetLiteral();
        }

        // [WMR 20160421] Wrong!
        // FhirTypeToFhirTypeName parses the typename from EnumLiteral attribute on individual FHIRDefinedType member
        // FhirTypeNameToFhirType converts enum member name to FHIRDefinedType enum value
        // Currently, the EnumLiteral attribute value is always equal to the Enum member name and the C# type name
        // However, this is not guaranteed! e.g. a FHIR type name could be a reserved word in C#

        public static FHIRDefinedType? FhirTypeNameToFhirType(string name)
        {
            FHIRDefinedType result; // = FHIRDefinedType.Patient;

            if (Enum.TryParse<FHIRDefinedType>(name, ignoreCase: true, result: out result))
                return result;
            else
                return null;
        }
#elif false
        // [WMR 20160421] NEW - Improved & optimized
        // 1. Convert from/to FHIR type names as defined by EnumLiteral attributes on FHIRDefinedType enum members
        // 2. Cache lookup tables, to optimize runtime reflection

        /// <summary>Returns the <see cref="FHIRDefinedType"/> enum value that represents the specified FHIR type name, or <c>null</c>.</summary>
        public static string FhirTypeToFhirTypeName(FHIRDefinedType type)
        {
            string result;
            _fhirTypeToFhirTypeName.Value.TryGetValue(type, out result);
            return result;
        }

        private static Lazy<IDictionary<FHIRDefinedType, string>> _fhirTypeToFhirTypeName
            = new Lazy<IDictionary<FHIRDefinedType, string>>(InitFhirTypeToFhirTypeName);

        private static IDictionary<FHIRDefinedType, string> InitFhirTypeToFhirTypeName()
        {
            // Build reverse lookup table
            return _fhirTypeNameToFhirType.Value.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);
        }

        /// <summary>Returns the FHIR type name represented by the specified <see cref="FHIRDefinedType"/> enum value, or <c>null</c>.</summary>
        public static FHIRDefinedType? FhirTypeNameToFhirType(string typeName)
        {
            FHIRDefinedType result;
            if (_fhirTypeNameToFhirType.Value.TryGetValue(typeName, out result))
            {
                return result;
            }
            return null;
        }

        private static Lazy<IDictionary<string, FHIRDefinedType>> _fhirTypeNameToFhirType
            = new Lazy<IDictionary<string, FHIRDefinedType>>(InitFhirTypeNameToFhirType);

        private static IDictionary<string, FHIRDefinedType> InitFhirTypeNameToFhirType()
        {
            var values = Enum.GetValues(typeof(FHIRDefinedType)).OfType<FHIRDefinedType>();
            return values.ToDictionary(type => type.GetLiteral());
        }
#else
        // [WMR 2017-10-25] Remove Lazy initialization
        // These methods are used frequently throughout the API (and by clients) and initialization cost is low

        private static readonly Dictionary<string, FHIRDefinedType> _fhirTypeNameToFhirType
            = Enum.GetValues(typeof(FHIRDefinedType)).OfType<FHIRDefinedType>().ToDictionary(type => type.GetLiteral());

        private static readonly Dictionary<FHIRDefinedType, string> _fhirTypeToFhirTypeName
            = _fhirTypeNameToFhirType.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

        /// <summary>Returns the FHIR type name represented by the specified <see cref="FHIRDefinedType"/> enum value, or <c>null</c>.</summary>
        public static FHIRDefinedType? FhirTypeNameToFhirType(string typeName)
            => _fhirTypeNameToFhirType.TryGetValue(typeName, out var result) ? (FHIRDefinedType?)result : null;

        /// <summary>Returns the <see cref="FHIRDefinedType"/> enum value that represents the specified FHIR type name, or <c>null</c>.</summary>
        public static string FhirTypeToFhirTypeName(FHIRDefinedType type)
            => _fhirTypeToFhirTypeName.TryGetValue(type, out var result) ? result : null;
#endif

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
            // [WMR 20160421] Optimization
            //if (!FhirTypeToCsType.ContainsKey(name))
            //    return null;
            //else
            //    return FhirTypeToCsType[name];
            Type result;
            FhirTypeToCsType.TryGetValue(name, out result);
            return result;
        }

        /// <summary>Returns the FHIR type name represented by the specified C# <see cref="Type"/>, or <c>null</c>.</summary>
        public static string GetFhirTypeNameForType(Type type)
        {
            // [WMR 20160421] Optimization
            //if (!FhirCsTypeToString.ContainsKey(type))
            //    return null;
            //else
            //    return FhirCsTypeToString[type];
            string result;
            FhirCsTypeToString.TryGetValue(type, out result);
            return result;
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

        /// <summary>Determines if the specified <see cref="FHIRDefinedType"/> value represents a known FHIR resource.</summary>
        public static bool IsKnownResource(FHIRDefinedType type)
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

        /// <summary>Determines if the specified <see cref="FHIRDefinedType"/> value represents a FHIR primitive data type.</summary>
        public static bool IsPrimitive(FHIRDefinedType type)
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

        /// <summary>Determines if the specified <see cref="FHIRDefinedType"/> value represents a FHIR complex data type (NOT including resources and primitives).</summary>
        public static bool IsDataType(FHIRDefinedType type)
        {
            return IsDataType(FhirTypeToFhirTypeName(type));
        }

        // [WMR 20160421] Dynamically resolve FHIR type name 'Reference'
        private static readonly string _referenceTypeName = FHIRDefinedType.Reference.GetLiteral();

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

        /// <summary>Determines if the specified <see cref="FHIRDefinedType"/> value represents a FHIR Reference type.</summary>
        public static bool IsReference(FHIRDefinedType type)
        {
            return type == FHIRDefinedType.Reference;
        }

        /// <summary>
        /// Determines if the specified <see cref="Type"/> instance represents a FHIR conformance resource type,
        /// i.e. if it equals one of the following types:
        /// <list type="bullet">
        /// <item>Conformance</item>
        /// <item>StructureDefinition</item>
        /// <item>ValueSet</item>
        /// <item>ConceptMap</item>
        /// <item>DataElement</item>
        /// <item>OperationDefinition</item>
        /// <item>SearchParameter</item>
        /// <item>NamingSystem</item>
        /// <item>ImplementationGuide</item>
        /// <item>TestScript</item>
        /// </list>
        /// </summary>
        public static bool IsConformanceResource(Type type)
        {
            return IsConformanceResource(type.Name);
        }

        /// <summary>
        /// Determines if the specified value represents a the type name of a FHIR conformance resource,
        /// i.e. if the value equals one of the following strings:
        /// <list type="bullet">
        /// <item>Conformance</item>
        /// <item>StructureDefinition</item>
        /// <item>ValueSet</item>
        /// <item>ConceptMap</item>
        /// <item>DataElement</item>
        /// <item>OperationDefinition</item>
        /// <item>SearchParameter</item>
        /// <item>NamingSystem</item>
        /// <item>ImplementationGuide</item>
        /// <item>TestScript</item>
        /// </list>
        /// </summary>
        public static bool IsConformanceResource(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;

            var t = FhirTypeNameToFhirType(name);

            if (t != null)
                return IsConformanceResource(t.Value);
            else
                return false;
        }

        /// <summary>Subset of <see cref="FHIRDefinedType"/> enumeration values for conformance resources.</summary>
        public static readonly FHIRDefinedType[] ConformanceResources =
        {
            FHIRDefinedType.Conformance,
            FHIRDefinedType.StructureDefinition,
            FHIRDefinedType.ValueSet,
            FHIRDefinedType.ConceptMap,
            FHIRDefinedType.DataElement,
            FHIRDefinedType.OperationDefinition,
            FHIRDefinedType.SearchParameter,
            FHIRDefinedType.NamingSystem,
            FHIRDefinedType.ImplementationGuide,
            FHIRDefinedType.TestScript
        };

        /// <summary>Determines if the specified <see cref="FHIRDefinedType"/> value represents a FHIR conformance resource.</summary>
        public static bool IsConformanceResource(FHIRDefinedType type) => ConformanceResources.Contains(type);

        /// <summary>Determines if the specified <see cref="FHIRDefinedType"/> value represents a FHIR conformance resource.</summary>
        public static bool IsConformanceResource(FHIRDefinedType? type) => type.HasValue && ConformanceResources.Contains(type.Value);

        /// <summary>Subset of <see cref="ResourceType"/> enumeration values for conformance resources.</summary>
        public static readonly ResourceType[] ConformanceResourceTypes =
        {
            ResourceType.Conformance,
            ResourceType.StructureDefinition,
            ResourceType.ValueSet,
            ResourceType.ConceptMap,
            ResourceType.DataElement,
            ResourceType.OperationDefinition,
            ResourceType.SearchParameter,
            ResourceType.NamingSystem,
            ResourceType.ImplementationGuide,
            ResourceType.TestScript
        };

        /// <summary>Determines if the specified <see cref="ResourceType"/> value represents a FHIR conformance resource.</summary>
        public static bool IsConformanceResource(ResourceType type) => ConformanceResourceTypes.Contains(type);

        /// <summary>Determines if the specified <see cref="ResourceType"/> value represents a FHIR conformance resource.</summary>
        public static bool IsConformanceResource(ResourceType? type) => type.HasValue && ConformanceResourceTypes.Contains(type.Value);

        /// <summary>Determines if the specified value represents the name of a core Resource, Datatype or primitive.</summary>
        public static bool IsCoreModelType(string name) => FhirTypeToCsType.ContainsKey(name);
            // => IsKnownResource(name) || IsDataType(name) || IsPrimitive(name);

        
        static readonly Uri FhirCoreProfileBaseUri = new Uri(@"http://hl7.org/fhir/StructureDefinition/");

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
        public static bool IsCoreSuperType(FHIRDefinedType type)
        {
            return
                type == FHIRDefinedType.Resource ||
                type == FHIRDefinedType.DomainResource ||
                type == FHIRDefinedType.Element ||
                type == FHIRDefinedType.BackboneElement;
        }

        public static bool IsCoreSuperType(string type)
        {
            var fat = FhirTypeNameToFhirType(type);

            if (fat == null) return false;

            return IsCoreSuperType(fat.Value);
        }

        public static bool IsProfiledQuantity(FHIRDefinedType type)
        {
            return
                type == FHIRDefinedType.Age ||
                type == FHIRDefinedType.Distance ||
                type == FHIRDefinedType.SimpleQuantity ||
                type == FHIRDefinedType.Duration ||
                type == FHIRDefinedType.Count ||
                type == FHIRDefinedType.Money;
        }

        public static bool IsProfiledQuantity(string type)
        {
            var definedType = FhirTypeNameToFhirType(type);
            if (definedType == null) return false;

            return IsProfiledQuantity(definedType.Value);
        }


        public static bool IsInstanceTypeFor(FHIRDefinedType superType, FHIRDefinedType instanceType)
        {
            if (superType == instanceType) return true;

            if (IsKnownResource(instanceType))
            {
                if (superType == FHIRDefinedType.Resource)
                    return true;
                else if (superType == FHIRDefinedType.DomainResource)
                    return instanceType != FHIRDefinedType.Parameters && instanceType != FHIRDefinedType.Bundle && instanceType != FHIRDefinedType.Binary;
                else
                    return false;
            }
            else
                return superType == FHIRDefinedType.Element;
        }

        public static string CanonicalUriForFhirCoreType(string typename)
        {
            return "http://hl7.org/fhir/StructureDefinition/" + typename;
        }

        public static string CanonicalUriForFhirCoreType(FHIRDefinedType type)
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
