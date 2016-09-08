/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Source
{
    public static class ResourceResolverExtensions
    {
        public static StructureDefinition FindExtensionDefinition(this IResourceResolver resolver, string uri, bool requireSnapshot = false)
        {
            var sd = resolver.ResolveByCanonicalUri(uri) as StructureDefinition;
            if (sd == null) return null;

            if (!sd.IsExtension)
                throw Error.Argument("uri", "Given uri exists as a StructureDefinition, but is not an extension");

            if (sd.Snapshot == null && requireSnapshot)
                return null;

            return sd;
        }

        public static StructureDefinition FindStructureDefinition(this IResourceResolver resolver, string uri, bool requireSnapshot = false)
        {
            var sd = resolver.ResolveByCanonicalUri(uri) as StructureDefinition;
            if (sd == null) return null;

            if (sd.Snapshot == null && requireSnapshot)
                return null;

            return sd;
        }

        public static StructureDefinition FindStructureDefinitionForCoreType(this IResourceResolver resolver, string typename)
        {
            var url = ModelInfo.CanonicalUriForFhirCoreType(typename);
            return resolver.FindStructureDefinition(url);
        }

        public static StructureDefinition FindStructureDefinitionForCoreType(this IResourceResolver resolver, FHIRDefinedType type)
        {
            return resolver.FindStructureDefinitionForCoreType(ModelInfo.FhirTypeToFhirTypeName(type));
        }

        /// <summary>
        /// Tries to locate a valueset using a combined algorithm: first, the uri is used to find a valueset by system.
        /// If that fails, the valueset is searched for by canonical url. Failing that, the function tries to locate the
        /// valueset by resource url.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static ValueSet FindValueSet(this IConformanceSource source, string uri)
        {
            var vs = source.FindValueSetBySystem(uri);

            if (vs == null)
                vs = source.ResolveByCanonicalUri(uri) as ValueSet;

            if (vs == null)
                vs = source.ResolveByUri(uri) as ValueSet;

            return vs;
        }


        public static IEnumerable<T> FindAll<T>(this IConformanceSource source) where T:Resource
        {
            var type = ModelInfo.GetFhirTypeNameForType(typeof(T));

            if (type != null)
            {
                var resourceType = EnumUtility.ParseLiteral<ResourceType>(type);
                var uris = source.ListResourceUris(resourceType);
                return uris.Select(u => source.ResolveByCanonicalUri(u) as T).Where(r => r != null);
            }
            else
                return null;
        }

        /// <summary>Resolve a <see cref="StructureDefinition"/> from a TypeRef.Code element, handle unknown/custom core types.</summary>
        /// <param name="resolver">An <see cref="IArtifactSource"/> reference.</param>
        /// <param name="typeCodeElement">A <see cref="ElementDefinition.TypeRefComponent.CodeElement"/> reference.</param>
        /// <returns>A <see cref="StructureDefinition"/> instance, or <c>null</c>.</returns>
        internal static StructureDefinition GetStructureDefinitionForTypeCode(this IResourceResolver resolver, Code<FHIRDefinedType> typeCodeElement)
        {
            StructureDefinition sd = null;
            var typeCode = typeCodeElement.Value;
            if (typeCode.HasValue)
            {
                sd = resolver.FindStructureDefinitionForCoreType(typeCode.Value);
            }
            else
            {
                // Unknown/custom core type; try to resolve from raw object value
                var typeName = typeCodeElement.ObjectValue as string;
                if (!string.IsNullOrEmpty(typeName))
                {
                    sd = resolver.FindStructureDefinitionForCoreType(typeName);
                }
            }
            return sd;
        }

    }
}
