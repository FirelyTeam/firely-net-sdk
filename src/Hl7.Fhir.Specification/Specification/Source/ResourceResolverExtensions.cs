/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using T = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Source
{
    public static class ResourceResolverExtensions
    {
        /// <inheritdoc cref="FindExtensionDefinitionAsync(IAsyncResourceResolver, string)"/>
        [Obsolete("Using synchronous resolvers is not recommended anymore, use FindExtensionDefinitionAsync() instead.")]
        public static StructureDefinition FindExtensionDefinition(this IResourceResolver resolver, string uri)
        {
            if (!(resolver.ResolveByCanonicalUri(uri) is StructureDefinition sd)) return null;

            if (!sd.IsExtension)
                throw Error.Argument(nameof(uri), $"Found StructureDefinition at '{uri}', but is not an extension");

            return sd;
        }


        /// <summary>
        /// Resolve the given url and verify it defines an Extension.
        /// </summary>
        /// <returns>Returns a StructureDefinition if it is resolvable and defines an extension, otherwise <c>null</c>.</returns>
        public static async T.Task<StructureDefinition> FindExtensionDefinitionAsync(this IAsyncResourceResolver resolver, string uri)
        {
            if (!(await resolver.ResolveByCanonicalUriAsync(uri).ConfigureAwait(false) is StructureDefinition sd)) return null;

            if (!sd.IsExtension)
                throw Error.Argument(nameof(uri), $"Found StructureDefinition at '{uri}', but is not an extension");

            return sd;
        }

        /// <inheritdoc cref="FindStructureDefinitionAsync(IAsyncResourceResolver, string)"/>
        [Obsolete("Using synchronous resolvers is not recommended anymore, use FindStructureDefinitionAsync() instead.")]
        public static StructureDefinition FindStructureDefinition(this IResourceResolver resolver, string uri)
            => resolver.ResolveByCanonicalUri(uri) as StructureDefinition;

        /// <summary>
        /// Resolve the given url and verify it is a StructureDefinition
        /// </summary>
        /// <returns>The resolved StructureDefinition or <c>null</c> if it cannot be resolved or does not resolve to a StructureDefinition.</returns>
        public static async T.Task<StructureDefinition> FindStructureDefinitionAsync(this IAsyncResourceResolver resolver, string uri)
            => await resolver.ResolveByCanonicalUriAsync(uri).ConfigureAwait(false) as StructureDefinition;

        /// <inheritdoc cref="FindStructureDefinitionForCoreTypeAsync(IAsyncResourceResolver, string)"/>
        [Obsolete("Using synchronous resolvers is not recommended anymore, use FindStructureDefinitionForCoreTypeAsync() instead.")]
        public static StructureDefinition FindStructureDefinitionForCoreType(this IResourceResolver resolver, string typename)
        {
            var url = Uri.IsWellFormedUriString(typename, UriKind.Absolute) ? typename : ModelInfo.CanonicalUriForFhirCoreType(typename);
#pragma warning disable CS0618 // Type or member is obsolete
            return resolver.FindStructureDefinition(url);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        /// <summary>
        /// Resolve the StructureDefinition for the FHIR-defined type given in <paramref name="typename"/>.
        /// </summary>
        /// <remarks>If the <paramref name="typename"/> is a uri, will resolve the given uri, if it is a simple typename,
        /// it will resolve the typename below <c>http://hl7.org/fhir/StructureDefinition/</c>.
        /// </remarks>
        public static async T.Task<StructureDefinition> FindStructureDefinitionForCoreTypeAsync(this IAsyncResourceResolver resolver, string typename)
        {
            var url = Uri.IsWellFormedUriString(typename, UriKind.Absolute) ? typename : ModelInfo.CanonicalUriForFhirCoreType(typename);
            return await resolver.FindStructureDefinitionAsync(url).ConfigureAwait(false);
        }

        /// <inheritdoc cref="FindStructureDefinitionForCoreTypeAsync(IAsyncResourceResolver, FHIRAllTypes)"/>
        [Obsolete("Using synchronous resolvers is not recommended anymore, use FindStructureDefinitionForCoreTypeAsync() instead.")]
        public static StructureDefinition FindStructureDefinitionForCoreType(this IResourceResolver resolver, FHIRAllTypes type)
#pragma warning disable CS0618 // Type or member is obsolete
            => resolver.FindStructureDefinitionForCoreType(ModelInfo.FhirTypeToFhirTypeName(type));
#pragma warning restore CS0618 // Type or member is obsolete

        /// <summary>
        /// Resolve the StructureDefinition for the FHIR-defined type given in <paramref name="type"/>.
        /// </summary>
        public static async T.Task<StructureDefinition> FindStructureDefinitionForCoreTypeAsync(this IAsyncResourceResolver resolver, FHIRAllTypes type)
            => await resolver.FindStructureDefinitionForCoreTypeAsync(ModelInfo.FhirTypeToFhirTypeName(type)).ConfigureAwait(false);

        /// <inheritdoc cref="FindValueSetAsync(IAsyncResourceResolver, string)"/>
        [Obsolete("Using synchronous resolvers is not recommended anymore, use FindValueSetAsync() instead.")]
        public static ValueSet FindValueSet(this IResourceResolver source, string uri)
            => source.ResolveByCanonicalUri(uri) as ValueSet;

        /// <summary>
        /// Find a ValueSet by canonical url.
        /// </summary>
        public static async T.Task<ValueSet> FindValueSetAsync(this IAsyncResourceResolver source, string uri)
            => await source.ResolveByCanonicalUriAsync(uri).ConfigureAwait(false) as ValueSet;

        /// <inheritdoc cref="FindCodeSystemAsync(IAsyncResourceResolver, string)"/>
        [Obsolete("Using synchronous resolvers is not recommended anymore, use FindCodeSystemAsync() instead.")]
        public static CodeSystem FindCodeSystem(this IResourceResolver source, string uri)
            => source.ResolveByCanonicalUri(uri) as CodeSystem;

        /// <summary>
        /// Find a CodeSystem by canonical url.
        /// </summary>
        public static async T.Task<CodeSystem> FindCodeSystemAsync(this IAsyncResourceResolver source, string uri)
            => await source.ResolveByCanonicalUriAsync(uri).ConfigureAwait(false) as CodeSystem;

        public static IEnumerable<T> FindAll<T>(this IConformanceSource source) where T : Resource
        {
            var type = ModelInfo.GetFhirTypeNameForType(typeof(T));

            if (type != null)
            {
                var resourceType = EnumUtility.ParseLiteral<ResourceType>(type);
                var uris = source.ListResourceUris(resourceType);
                return uris.Select(u => source.ResolveByUri(u) as T).Where(r => r != null);
            }
            else
                return null;
        }
    }
}
