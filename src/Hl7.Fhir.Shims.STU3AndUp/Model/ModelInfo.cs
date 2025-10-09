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

namespace Hl7.Fhir.Model;

public partial class ModelInfo
{
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
        => _fhirTypeToFhirTypeName.GetValueOrDefault(type);
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
        => _resourceTypeToFhirTypeName.GetValueOrDefault(type);

    #endregion

    /// <inheritdoc cref="IModelInfo.GetTypeForFhirType(string)"/>
    public static Type? GetTypeForFhirType(string name) => ModelInspector.GetTypeForFhirType(name);

    /// <inheritdoc cref="IModelInfo.GetFhirTypeNameForType(Type)"/>
    public static string? GetFhirTypeNameForType(Type type) => ModelInspector.GetFhirTypeNameForType(type);

    /// <inheritdoc cref="IModelInfo.IsKnownResource(string)"/>
    public static bool IsKnownResource(string name) => ModelInspector.IsKnownResource(name);

    /// <inheritdoc cref="IModelInfo.IsKnownResource(Type)"/>
    public static bool IsKnownResource(Type type) => ModelInspector.IsKnownResource(type);

    /// <summary>Determines if the specified <see cref="FHIRAllTypes"/> value represents a known FHIR resource.</summary>
    public static bool IsKnownResource(FHIRAllTypes type) => FhirTypeToFhirTypeName(type) is { } name && IsKnownResource(name);

    /// <inheritdoc cref="IModelInfo.IsPrimitive(string)"/>
    public static bool IsPrimitive(string name) => ModelInspector.IsPrimitive(name);

    /// <inheritdoc cref="IModelInfo.IsPrimitive(Type)"/>
    public static bool IsPrimitive(Type type) => ModelInspector.IsPrimitive(type);

    /// <summary>Determines if the specified <see cref="FHIRAllTypes"/> value represents a FHIR primitive data type.</summary>
    public static bool IsPrimitive(FHIRAllTypes type) => FhirTypeToFhirTypeName(type) is { } name && IsPrimitive(name);

    /// <inheritdoc cref="IModelInfo.IsDataType(string)"/>
    public static bool IsDataType(string name) => ModelInspector.IsDataType(name);

    /// <inheritdoc cref="IModelInfo.IsDataType(Type)"/>
    public static bool IsDataType(Type type) => ModelInspector.IsDataType(type);

    /// <summary>Determines if the specified <see cref="FHIRAllTypes"/> value represents a FHIR complex data type (NOT including resources and primitives).</summary>
    public static bool IsDataType(FHIRAllTypes type) => FhirTypeToFhirTypeName(type) is { } name && IsDataType(name);

    /// <inheritdoc cref="IModelInfo.IsReference(string)"/>
    public static bool IsReference(string name) => ModelInspector.IsReference(name);

    /// <inheritdoc cref="IModelInfo.IsReference(Type)"/>
    public static bool IsReference(Type type) => ModelInspector.IsReference(type);

    /// <summary>Determines if the specified <see cref="FHIRAllTypes"/> value represents a FHIR Reference type.</summary>
    public static bool IsReference(FHIRAllTypes type) => FhirTypeToFhirTypeName(type) is { } name && IsReference(name);

    /// <inheritdoc cref="IModelInfo.IsConformanceResource(Type)"/>
    public static bool IsConformanceResource(Type type) => ModelInspector.IsConformanceResource(type);

    /// <inheritdoc cref="IModelInfo.IsConformanceResource(string)"/>
    public static bool IsConformanceResource(string name) => ModelInspector.IsConformanceResource(name);

    /// <summary>
    /// Determines if the specified <see cref="FHIRAllTypes"/> value represents a FHIR conformance resource type
    /// (resources under the Conformance/Terminology/Implementation Support header in resourcelist.html)
    /// </summary>
    public static bool IsConformanceResource(FHIRAllTypes? type) => type.HasValue && FhirTypeToFhirTypeName(type.Value) is { } name && IsConformanceResource(name);

    /// <summary>
    /// Determines if the specified <see cref="ResourceType"/> value represents a FHIR conformance resource type
    /// (resources under the Conformance/Terminology/Implementation Support header in resourcelist.html)
    /// </summary>
    public static bool IsConformanceResource(ResourceType? type) => type.HasValue && ResourceTypeToFhirTypeName(type.Value) is { } name && IsConformanceResource(name);

    /// <inheritdoc cref="IModelInfo.IsCoreModelType(string)"/>
    public static bool IsCoreModelType(string name) => ModelInspector.IsCoreModelType(name);

    /// <inheritdoc cref="IModelInfo.IsCoreModelType(Type)"/>
    public static bool IsCoreModelType(Type type) => ModelInspector.IsCoreModelType(type);

    /// <inheritdoc cref="IModelInfo.IsCoreModelTypeUri(Uri)"/>
    public static bool IsCoreModelTypeUri(Uri uri) => ModelInspector.IsCoreModelTypeUri(uri);

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

    /// <inheritdoc cref="IModelInfo.IsCoreSuperType(Type)"/>
    public static bool IsCoreSuperType(Type type) => ModelInspector.IsCoreSuperType(type);

    /// <inheritdoc cref="IModelInfo.IsCoreSuperType(string)"/>
    public static bool IsCoreSuperType(string name) => ModelInspector.IsCoreSuperType(name);

    public static bool IsBindable(string type) => ModelInspector.IsBindable(type);

    public static bool IsBindable(FHIRAllTypes t) => FhirTypeToFhirTypeName(t) is { } typeName && IsBindable(typeName);

    public static bool CheckMinorVersionCompatibility(string externalVersion)
    {
        return SemVersion.CheckMinorVersionCompatibility(Version, externalVersion);
    }

    /// <inheritdoc cref="IModelInfo.IsInstanceTypeFor(string, string)"/>
    public static bool IsInstanceTypeFor(string superclass, string subclass) => ModelInspector.IsInstanceTypeFor(superclass, subclass);

    /// <inheritdoc cref="IModelInfo.IsInstanceTypeFor(Type, Type)"/>
    public static bool IsInstanceTypeFor(Type superclass, Type subclass) => ModelInspector.IsInstanceTypeFor(superclass, subclass);

    public static bool IsInstanceTypeFor(FHIRAllTypes superclass, FHIRAllTypes subclass)
    {
        var superclassname = FhirTypeToFhirTypeName(superclass);
        var subclassname = FhirTypeToFhirTypeName(subclass);
        return superclassname is not null && subclassname is not null && IsInstanceTypeFor(superclassname, subclassname);
    }

    /// <inheritdoc cref="IModelInfo.CanonicalUriForFhirCoreType(string)"/>
    public static Canonical CanonicalUriForFhirCoreType(string typename) => Canonical.ForCoreType(typename);

    /// <inheritdoc cref="IModelInfo.CanonicalUriForFhirCoreType(Type)"/>
    public static Canonical? CanonicalUriForFhirCoreType(Type type) => ModelInspector.CanonicalUriForFhirCoreType(type);

    public static Canonical? CanonicalUriForFhirCoreType(FHIRAllTypes type) => FhirTypeToFhirTypeName(type) is { } name ? CanonicalUriForFhirCoreType(name) : null;

        private static readonly Lazy<ModelInspector> _modelInspector = new(() =>
        {
#pragma warning disable CS0618 // Type or member is obsolete
            var inspector = ModelInspector.ForAssembly(typeof(ModelInfo).GetTypeInfo().Assembly);
#pragma warning restore CS0618 // Type or member is obsolete
            if (inspector.FhirRelease != Specification.FhirRelease.STU3)
            {
                // In case of release 4 or higher, also load the assembly with common conformance resources, like StructureDefinition
                inspector.Import(typeof(StructureDefinition).GetTypeInfo().Assembly);
            }
            return inspector;
        });

        /// <summary>
        /// Gets the <see cref="ModelInspector"/> providing metadata for the resources and
        /// datatypes in this release of FHIR.
        /// </summary>
        public static ModelInspector ModelInspector => _modelInspector.Value;
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