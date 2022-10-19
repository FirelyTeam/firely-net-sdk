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

#nullable enable

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hl7.Fhir.Model
{
    public partial class ModelInfo : CommonModelInfo
    {
        public ModelInfo() : base(ModelInspector) { }

        private static ModelInfo _singleton = new();


        #region FHIRAllType functions
        private static readonly Dictionary<string, FHIRAllTypes> _fhirTypeNameToFhirType
            = Enum.GetValues(typeof(FHIRAllTypes)).OfType<FHIRAllTypes>().ToDictionary(type => type.GetLiteral());

        private static readonly Dictionary<FHIRAllTypes, string> _fhirTypeToFhirTypeName
            = _fhirTypeNameToFhirType.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

        /// <summary>Returns the FHIR type name represented by the specified <see cref="FHIRAllTypes"/> enum value, or <c>null</c>.</summary>
        public static FHIRAllTypes? FhirTypeNameToFhirType(string typeName)
            => _fhirTypeNameToFhirType.TryGetValue(typeName, out var result) ? result : null;

        /// <summary>Returns the <see cref="FHIRAllTypes"/> enum value that represents the specified FHIR type name, or <c>null</c>.</summary>
        public static string? FhirTypeToFhirTypeName(FHIRAllTypes type)
            => _fhirTypeToFhirTypeName.TryGetValue(type, out var result) ? result : null;
        #endregion

        #region ResourceType functions
        private static readonly Dictionary<string, ResourceType> _fhirTypeNameToResourceType
            = Enum.GetValues(typeof(ResourceType)).OfType<ResourceType>().ToDictionary(type => type.GetLiteral());

        private static readonly Dictionary<ResourceType, string> _resourceTypeToFhirTypeName
            = _fhirTypeNameToResourceType.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

        /// <summary>Returns the FHIR type name represented by the specified <see cref="ResourceType"/> enum value, or <c>null</c>.</summary>
        public static ResourceType? FhirTypeNameToResourceType(string typeName)
            => _fhirTypeNameToResourceType.TryGetValue(typeName, out var result) ? result : null;

        /// <summary>Returns the <see cref="ResourceType"/> enum value that represents the specified FHIR type name, or <c>null</c>.</summary>
        public static string? ResourceTypeToFhirTypeName(ResourceType type)
            => _resourceTypeToFhirTypeName.TryGetValue(type, out var result) ? result : null;

        #endregion

        /// <summary>Returns the C# <see cref="Type"/> that represents the FHIR type with the specified name, or <c>null</c>.</summary>
        //public static Type GetTypeForFhirType(string name)
        //{
        //   return FhirTypeToCsType.TryGetValue(name, out var result) ? result : null;
        //}
        public static Type? GetTypeForFhirType(string name) => _singleton.CommonGetTypeForFhirType(name);

        /// <summary>Returns the FHIR type name represented by the specified C# <see cref="Type"/>, or <c>null</c>.</summary>
        //public static string GetFhirTypeNameForType(Type type)
        //{
        //    return FhirCsTypeToString.TryGetValue(type, out var result) ? result : null;
        //}
        public static string? GetFhirTypeNameForType(Type type) => _singleton.GetCommonFhirTypeNameForType(type);

        /// <summary>Determines if the specified value represents the name of a known FHIR resource.</summary>
        //public static bool IsKnownResource(string name)
        //{
        //    return SupportedResources.Contains(name);
        //}

        public static bool IsKnownResource(string name) => _singleton.CommonIsKnownResource(name);

        /// <summary>Determines if the specified <see cref="Type"/> instance represents a known FHIR resource.</summary>
        //public static bool IsKnownResource(Type type)
        //{
        //    var name = GetFhirTypeNameForType(type);

        //    return name != null && IsKnownResource(name);
        //}
        public static bool IsKnownResource(Type type) => _singleton.CommonIsKnownResource(type);

        /// <summary>Determines if the specified <see cref="FHIRAllTypes"/> value represents a known FHIR resource.</summary>
        //public static bool IsKnownResource(FHIRAllTypes type)
        //{
        //    var name = FhirTypeToFhirTypeName(type);
        //    return name != null && IsKnownResource(name);
        //}
        public static bool IsKnownResource(FHIRAllTypes type) => FhirTypeToFhirTypeName(type) is { } name && IsKnownResource(name);

        /// <summary>Determines if the specified value represents the name of a FHIR primitive data type.</summary>
        //public static bool IsPrimitive(string name)
        //{
        //    if (String.IsNullOrEmpty(name)) return false;
        //    var type = GetTypeForFhirType(name);
        //    if (type is null) return false;

        //    return IsPrimitive(type);

        //}
        public static bool IsPrimitive(string name) => _singleton.CommonIsPrimitive(name);

        /// <summary>Determines if the specified <see cref="Type"/> instance represents a FHIR primitive data type.</summary>
        public static bool IsPrimitive(Type type) => _singleton.CommonIsPrimitive(type);
        //public static bool IsPrimitive(Type type)
        //{
        //    return typeof(PrimitiveType).IsAssignableFrom(type);
        //}

        /// <summary>Determines if the specified <see cref="FHIRAllTypes"/> value represents a FHIR primitive data type.</summary>
        public static bool IsPrimitive(FHIRAllTypes type) => FhirTypeToFhirTypeName(type) is { } name && IsPrimitive(name);
        //public static bool IsPrimitive(FHIRAllTypes type)
        //{
        //    return IsPrimitive(FhirTypeToFhirTypeName(type));
        //}

        /// <summary>Determines if the specified value represents the name of a FHIR complex data type (NOT including resources and primitives).</summary>
        //public static bool IsDataType(string name)
        //{
        //    if (String.IsNullOrEmpty(name)) return false;

        //    return FhirTypeToCsType.ContainsKey(name) && !IsKnownResource(name) && !IsPrimitive(name);
        //}
        public static bool IsDataType(string name) => _singleton.CommonIsDataType(name);

        /// <summary>Determines if the specified <see cref="Type"/> instance represents a FHIR complex data type (NOT including resources and primitives).</summary>
        //public static bool IsDataType(Type type)
        //{
        //    var name = GetFhirTypeNameForType(type);

        //    return name != null && !IsKnownResource(name) && !IsPrimitive(name);
        //}
        public static bool IsDataType(Type type) => _singleton.CommonIsDataType(type);

        /// <summary>Determines if the specified <see cref="FHIRAllTypes"/> value represents a FHIR complex data type (NOT including resources and primitives).</summary>
        public static bool IsDataType(FHIRAllTypes type) => FhirTypeToFhirTypeName(type) is { } name && IsDataType(name);
        //{
        //    return IsDataType(FhirTypeToFhirTypeName(type));
        //}

        // [WMR 20160421] Dynamically resolve FHIR type name 'Reference'
        //private static readonly string _referenceTypeName = FHIRAllTypes.Reference.GetLiteral();

        /// <summary>Determines if the specified value represents the type name of a FHIR Reference, i.e. equals "Reference".</summary>
        //public static bool IsReference(string name)
        //{
        //    return name == _referenceTypeName; // "Reference";
        //}
        public static bool IsReference(string name) => _singleton.CommonIsReference(name);

        /// <summary>Determines if the specified <see cref="Type"/> instance represents a FHIR Reference type.</summary>
        //public static bool IsReference(Type type)
        //{
        //    return type.CanBeTreatedAsType(typeof(ResourceReference));
        //    //return IsReference(type.Name);
        //}
        public static bool IsReference(Type type) => _singleton.CommonIsReference(type);

        /// <summary>Determines if the specified <see cref="FHIRAllTypes"/> value represents a FHIR Reference type.</summary>
        //public static bool IsReference(FHIRAllTypes type)
        //{
        //    return type == FHIRAllTypes.Reference;
        //}
        public static bool IsReference(FHIRAllTypes type) => FhirTypeToFhirTypeName(type) is { } name && IsReference(name);

        /// <summary>
        /// Determines if the specified <see cref="Type"/> value represents a FHIR conformance resource type
        /// (resources under the Conformance/Terminology/Implementation Support header in resourcelist.html)
        /// </summary>
        //public static bool IsConformanceResource(Type type)
        //{
        //    return type.CanBeTreatedAsType(typeof(IConformanceResource));
        //    //return IsConformanceResource(type.Name);
        //}
        public static bool IsConformanceResource(Type type) => _singleton.CommonIsConformanceResource(type);

        /// <summary>
        /// Determines if the specified <see cref="FHIRAllTypes"/> value represents a FHIR conformance resource type
        /// (resources under the Conformance/Terminology/Implementation Support header in resourcelist.html)
        /// </summary>
        //public static bool IsConformanceResource(string name)
        //{
        //    if (string.IsNullOrEmpty(name)) return false;

        //    var t = FhirTypeNameToFhirType(name);

        //    if (t.HasValue)
        //        return IsConformanceResource(t.Value);
        //    else
        //        return false;
        //}

        public static bool IsConformanceResource(string name) => _singleton.CommonIsConformanceResource(name);

        // <summary>Subset of <see cref="FHIRAllTypes"/> enumeration values for conformance resources.</summary>
        //public static readonly FHIRAllTypes[] ConformanceResources =
        //{
        //    FHIRAllTypes.StructureDefinition,
        //    FHIRAllTypes.StructureMap,
        //    FHIRAllTypes.CapabilityStatement,
        //    FHIRAllTypes.MessageDefinition,
        //    FHIRAllTypes.OperationDefinition,
        //    FHIRAllTypes.SearchParameter,
        //    FHIRAllTypes.CompartmentDefinition,
        //    FHIRAllTypes.ImplementationGuide,
        //    FHIRAllTypes.GraphDefinition,
        //    FHIRAllTypes.CodeSystem,
        //    FHIRAllTypes.ValueSet,
        //    FHIRAllTypes.ConceptMap,
        //    FHIRAllTypes.NamingSystem,
        //    FHIRAllTypes.TestScript,
        //    //FHIRAllTypes.TestReport,
        //    FHIRAllTypes.Questionnaire,
        //    FHIRAllTypes.TerminologyCapabilities
        //};

        /// <summary>
        /// Determines if the specified <see cref="FHIRAllTypes"/> value represents a FHIR conformance resource type
        /// (resources under the Conformance/Terminology/Implementation Support header in resourcelist.html)
        /// </summary>
        //public static bool IsConformanceResource(FHIRAllTypes type) => ConformanceResources.Contains(type);
        public static bool IsConformanceResource(FHIRAllTypes? type) => type.HasValue && FhirTypeToFhirTypeName(type.Value) is { } name && IsConformanceResource(name);

        /// <summary>Subset of <see cref="ResourceType"/> enumeration values for conformance resources.</summary>
        public static readonly ResourceType[] ConformanceResourceTypes =
        {
            ResourceType.StructureDefinition,
            ResourceType.StructureMap,
            ResourceType.CapabilityStatement,
            ResourceType.MessageDefinition,
            ResourceType.OperationDefinition,
            ResourceType.SearchParameter,
            ResourceType.CompartmentDefinition,
            ResourceType.ImplementationGuide,
            ResourceType.GraphDefinition,
            ResourceType.CodeSystem,
            ResourceType.ValueSet,
            ResourceType.ConceptMap,
            ResourceType.NamingSystem,
            ResourceType.TestScript,
            //ResourceType.TestReport,
            ResourceType.TerminologyCapabilities
        };

        /// <summary>
        /// Determines if the specified <see cref="ResourceType"/> value represents a FHIR conformance resource type
        /// (resources under the Conformance/Terminology/Implementation Support header in resourcelist.html)
        /// </summary>
        //public static bool IsConformanceResource(ResourceType type) => ConformanceResourceTypes.Contains(type);
        public static bool IsConformanceResource(ResourceType? type) => type.HasValue && ResourceTypeToFhirTypeName(type.Value) is { } name && IsConformanceResource(name);

        // <summary>
        // Determines if the specified <see cref="ResourceType"/> value represents a FHIR conformance resource type
        // (resources under the Conformance/Terminology/Implementation Support header in resourcelist.html)
        // </summary>
        // public static bool IsConformanceResource(ResourceType? type) => type.HasValue && ConformanceResourceTypes.Contains(type.Value);


        /// <summary>Determines if the specified value represents the canonical uri of a core FHIR Resource, FHIR Datatype or FHIR primitive.</summary>
        /// <remarks>This function does not recognize "system" types, these are the basic types that the FHIR
        /// datatypes are built upon, but are not specific to the FHIR datamodel.</remarks>
        //public static bool IsCoreModelType(string name) => FhirTypeToCsType.ContainsKey(name);
        public static bool IsCoreModelType(string name) => _singleton.CommonIsCoreModelType(name);

        /// <summary>Determines if the specified value represents the type of a core Resource, Datatype or primitive.</summary>
        //public static bool IsCoreModelType(Type type) => FhirCsTypeToString.ContainsKey(type);
        public static bool IsCoreModelType(Type type) => _singleton.CommonIsCoreModelType(type);
        // => IsKnownResource(name) || IsDataType(name) || IsPrimitive(name);


        //public static readonly Uri FhirCoreProfileBaseUri = new Uri(@"http://hl7.org/fhir/StructureDefinition/");

        /// <summary>Determines if the specified value represents the canonical uri of a core FHIR Resource, FHIR Datatype or FHIR primitive.</summary>
        /// <remarks>This function does not recognize "system" types, these are the basic types that the FHIR
        /// datatypes are built upon, but are not specific to the FHIR datamodel.</remarks>
        //public static bool IsCoreModelTypeUri(Uri uri)
        //{
        //    return uri != null
        //        // [WMR 20181025] Issue #746
        //        // Note: FhirCoreProfileBaseUri.IsBaseOf(new Uri("Dummy", UriKind.RelativeOrAbsolute)) = true...?!
        //        && uri.IsAbsoluteUri
        //        && FhirCoreProfileBaseUri.IsBaseOf(uri)
        //        && IsCoreModelType(FhirCoreProfileBaseUri.MakeRelativeUri(uri).ToString());
        //}
        public static bool IsCoreModelTypeUri(Uri uri) => _singleton.CommonIsCoreModelTypeUri(uri);

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

        //public static bool IsCoreSuperType(Type type)
        //{
        //    return
        //        type == typeof(Base) ||
        //        type == typeof(Resource) ||
        //        type == typeof(DomainResource) ||
        //        type == typeof(Element) ||
        //        type == typeof(BackboneElement) ||
        //        type == typeof(DataType) ||
        //        type == typeof(PrimitiveType) ||
        //        type == typeof(BackboneType);
        //}
        public static bool IsCoreSuperType(Type type) => CommonIsCoreSuperType(type);

        //public static bool IsCoreSuperType(string type)
        //{
        //    var fat = FhirTypeNameToFhirType(type);

        //    if (fat == null) return false;

        //    return IsCoreSuperType(fat.Value);
        //}
        public static bool IsCoreSuperType(string name) => _singleton.CommonIsCoreSuperType(name);

        //[Obsolete("Profiled quantities have been removed from the POCO model and will not appear in data anymore.")]
        //public static bool IsProfiledQuantity(FHIRAllTypes type)
        //{
        //    return type == FHIRAllTypes.SimpleQuantity || type == FHIRAllTypes.MoneyQuantity;
        //}

        //[Obsolete("Profiled quantities have been removed from the POCO model and will not appear in data anymore.")]
        //public static bool IsProfiledQuantity(string type)
        //{
        //    var definedType = FhirTypeNameToFhirType(type);
        //    if (definedType == null) return false;

        //    return IsProfiledQuantity(definedType.Value);
        //}

        public static bool CheckMinorVersionCompatibility(string externalVersion)
        {
            if (string.IsNullOrEmpty(externalVersion))
            {
                throw new ArgumentNullException();
            }

            var minorFhirVersion = getMajorAndMinorVersion(Version);
            var externalMinorVersion = getMajorAndMinorVersion(externalVersion);

            if (string.IsNullOrEmpty(minorFhirVersion) || string.IsNullOrEmpty(externalVersion))
            {
                return false;
            }
            else
            {
                return minorFhirVersion == externalMinorVersion;
            }
        }

        private static string? getMajorAndMinorVersion(string version)
        {
            var versionnumbers = version.Split('.');
            if (versionnumbers.Count() >= 2)
            {
                return string.Join(".", versionnumbers[0], versionnumbers[1]);
            }
            else
            {
                return null;
            }
        }



        //public static bool IsInstanceTypeFor(string superclass, string subclass)
        //{
        //    if (superclass == subclass) return true;

        //    var superType = GetTypeForFhirType(superclass);
        //    var subType = GetTypeForFhirType(subclass);

        //    if (subType == null || superType == null) return false;

        //    return IsInstanceTypeFor(superType, subType);
        //}
        public static bool IsInstanceTypeFor(string superclass, string subclass) => _singleton.CommonIsInstanceTypeFor(superclass, subclass);

        //public static bool IsInstanceTypeFor(Type superclass, Type subclass)
        //{
        //    if (superclass == subclass) return true;

        //    return superclass.IsAssignableFrom(subclass);
        //}
        public static bool IsInstanceTypeFor(Type superclass, Type subclass) => _singleton.CommonIsInstanceTypeFor(superclass, subclass);


        //public static bool IsInstanceTypeFor(FHIRAllTypes superclass, FHIRAllTypes subclass)
        //{
        //    if (superclass == subclass) return true;

        //    var superclassName = FhirTypeToFhirTypeName(superclass);
        //    var subclassName = FhirTypeToFhirTypeName(subclass);

        //    return IsInstanceTypeFor(superclassName, subclassName);
        //}
        public static bool IsInstanceTypeFor(FHIRAllTypes superclass, FHIRAllTypes subclass)
        {
            var superclassname = FhirTypeToFhirTypeName(superclass);
            var subclassname = FhirTypeToFhirTypeName(subclass);
            return superclassname is not null && subclassname is not null && IsInstanceTypeFor(superclassname, subclassname);
        }

        //public static Canonical CanonicalUriForFhirCoreType(string typename)
        //{
        //    return new Canonical("http://hl7.org/fhir/StructureDefinition/" + typename);
        //}

        /* Unmerged change from project 'Hl7.Fhir.R5.Core (net452)'
        Before:
                public static Canonical CanonicalUriForFhirCoreType(string typename) => _singleton.CommonCanonicalUriForFhirCoreType(typename);
        After:
                public static Canonical CanonicalUriForFhirCoreType(string typename) => CommonModelInfo.CommonCanonicalUriForFhirCoreType(typename);
        */

        /* Unmerged change from project 'Hl7.Fhir.R5.Core (netstandard2.0)'
        Before:
                public static Canonical CanonicalUriForFhirCoreType(string typename) => _singleton.CommonCanonicalUriForFhirCoreType(typename);
        After:
                public static Canonical CanonicalUriForFhirCoreType(string typename) => CommonModelInfo.CommonCanonicalUriForFhirCoreType(typename);
        */
        public static Canonical CanonicalUriForFhirCoreType(string typename) => CommonCanonicalUriForFhirCoreType(typename);

        //public static Canonical CanonicalUriForFhirCoreType(Type type)
        //{
        //    return CanonicalUriForFhirCoreType(GetFhirTypeNameForType(type));
        //}
        public static Canonical? CanonicalUriForFhirCoreType(Type type) => _singleton.CommonCanonicalUriForFhirCoreType(type);

        //public static Canonical CanonicalUriForFhirCoreType(FHIRAllTypes type)
        //{
        //    return CanonicalUriForFhirCoreType(type.GetLiteral());
        //}
        public static Canonical? CanonicalUriForFhirCoreType(FHIRAllTypes type) => FhirTypeToFhirTypeName(type) is { } name ? CanonicalUriForFhirCoreType(name) : null;

        /// <summary>
        /// Gets the <see cref="ModelInspector"/> providing metadata for the resources and
        /// datatypes in this release of FHIR.
        /// </summary>
        public static ModelInspector ModelInspector
        {
            get
            {
                var inspector = ModelInspector.ForAssembly(typeof(ModelInfo).GetTypeInfo().Assembly);
                inspector.Import(typeof(StructureDefinition).GetTypeInfo().Assembly);
                return inspector;
            }
        }

        public static readonly Type[] OpenTypes =
        {
            typeof(Model.Address),
            typeof(Model.Age),
            typeof(Model.Annotation),
            typeof(Model.Attachment),
            typeof(Model.Base64Binary),
            typeof(Model.FhirBoolean),
            typeof(Model.Canonical),
            typeof(Model.Code),
            typeof(Model.CodeableConcept),
            typeof(Model.Coding),
            typeof(Model.ContactDetail),
            typeof(Model.ContactPoint),
            typeof(Model.Contributor),
            typeof(Model.Count),
            typeof(Model.DataRequirement),
            typeof(Model.Date),
            typeof(Model.FhirDateTime),
            typeof(Model.FhirDecimal),
            typeof(Model.Distance),
            typeof(Model.Dosage),
            typeof(Model.Duration),
            typeof(Model.Expression),
            typeof(Model.HumanName),
            typeof(Model.Id),
            typeof(Model.Identifier),
            typeof(Model.Instant),
            typeof(Model.Integer),
            typeof(Model.Markdown),
            typeof(Model.Meta),
            typeof(Model.Money),
            typeof(Model.Oid),
            typeof(Model.ParameterDefinition),
            typeof(Model.Period),
            typeof(Model.PositiveInt),
            typeof(Model.Quantity),
            typeof(Model.Range),
            typeof(Model.Ratio),
            typeof(Model.ResourceReference),
            typeof(Model.RelatedArtifact),
            typeof(Model.SampledData),
            typeof(Model.Signature),
            typeof(Model.FhirString),
            typeof(Model.Time),
            typeof(Model.Timing),
            typeof(Model.TriggerDefinition),
            typeof(Model.UnsignedInt),
            typeof(Model.FhirUri),
            typeof(Model.FhirUrl),
            typeof(Model.UsageContext),
            typeof(Model.Uuid)
        };

    }

    public static class ModelInfoExtensions
    {
        public static bool TryDeriveResourceType(this Resource r, out ResourceType rt)
        {
            var result = ModelInfo.FhirTypeNameToResourceType(r.TypeName);
            rt = result.GetValueOrDefault(default);
            return result.HasValue;
        }
    }

}
#nullable restore