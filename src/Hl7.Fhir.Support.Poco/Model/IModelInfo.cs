/* 
 * Copyright (c) 2022, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable

using System;

namespace Hl7.Fhir.Model
{
    public interface IModelInfo
    {
        Canonical? CanonicalUriForFhirCoreType(string typeName);

        Canonical? CanonicalUriForFhirCoreType(Type type);

        /// <summary>Returns the C# <see cref="Type"/> that represents the FHIR type with the specified name, or <c>null</c>.</summary>
        Type? GetTypeForFhirType(string name);

        bool IsBindable(string type);

        /// <summary>
        /// Determines if the specified value represents a FHIR conformance resource type
        /// (resources under the Conformance/Terminology/Implementation Support header in resourcelist.html)
        /// </summary>
        bool IsConformanceResource(string name);

        /// <summary>
        /// Determines if the specified <see cref="Type"/> value represents a FHIR conformance resource type
        /// (resources under the Conformance/Terminology/Implementation Support header in resourcelist.html)
        /// </summary>
        bool IsConformanceResource(Type type);

        /// <summary>Determines if the specified value represents the canonical uri of a core FHIR Resource, FHIR Datatype or FHIR primitive.</summary>
        /// <remarks>This function does not recognize "system" types, these are the basic types that the FHIR
        /// datatypes are built upon, but are not specific to the FHIR datamodel.</remarks>
        bool IsCoreModelType(string name);

        /// <summary>Determines if the specified value represents the type of a core Resource, Datatype or primitive.</summary>
        bool IsCoreModelType(Type type);

        /// <summary>Determines if the specified value represents the canonical uri of a core FHIR Resource, FHIR Datatype or FHIR primitive.</summary>
        /// <remarks>This function does not recognize "system" types, these are the basic types that the FHIR
        /// datatypes are built upon, but are not specific to the FHIR datamodel.</remarks>
        bool IsCoreModelTypeUri(Uri uri);

        /// <summary>
        /// Returns whether the value has subclasses in the core spec.
        /// </summary>
        bool IsCoreSuperType(string name);

        bool IsCoreSuperType(Type type);

        /// <summary>Determines if the specified value represents the name of a FHIR complex data type (NOT including resources and primitives).</summary>
        bool IsDataType(string name);

        /// <summary>Determines if the specified <see cref="Type"/> instance represents a FHIR complex data type (NOT including resources and primitives).</summary>
        bool IsDataType(Type type);

        bool IsInstanceTypeFor(string superclass, string subclass);

        bool IsInstanceTypeFor(Type superclass, Type subclass);

        /// <summary>Determines if the specified value represents the name of a known FHIR resource.</summary>
        bool IsKnownResource(string name);

        /// <summary>Determines if the specified <see cref="Type"/> instance represents a known FHIR resource.</summary>
        bool IsKnownResource(Type type);

        /// <summary>Determines if the specified value represents the name of a FHIR primitive data type.</summary>
        bool IsPrimitive(string name);

        /// <summary>Determines if the specified <see cref="Type"/> instance represents a FHIR primitive data type.</summary>
        bool IsPrimitive(Type type);

        /// <summary>Determines if the specified value represents the type name of a FHIR Reference, i.e. equals "Reference".</summary>
        bool IsReference(string name);

        /// <summary>Determines if the specified <see cref="Type"/> instance represents a FHIR Reference type.</summary>
        bool IsReference(Type type);

        /// <summary>Returns the FHIR type name represented by the specified C# <see cref="Type"/>, or <c>null</c>.</summary>
        string? GetFhirTypeNameForType(Type type);
    }
}
#nullable restore