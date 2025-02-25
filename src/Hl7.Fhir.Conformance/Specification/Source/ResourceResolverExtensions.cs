/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Source
{
    public static class ResourceResolverExtensions
    {
        /// <inheritdoc cref="FindExtensionDefinitionAsync(IAsyncResourceResolver, string)"/>
        [Obsolete("Using synchronous resolvers is not recommended anymore, use FindExtensionDefinitionAsync() instead.")]
        public static StructureDefinition FindExtensionDefinition(this IResourceResolver resolver, string uri)
        {
            if (resolver.ResolveByCanonicalUri(uri) is not StructureDefinition sd) return null;

            if (!sd.IsExtension)
                throw Error.Argument(nameof(uri), $"Found StructureDefinition at '{uri}', but is not an extension");

            return sd;
        }


        /// <summary>
        /// Resolve the given url and verify it defines an Extension.
        /// </summary>
        /// <returns>Returns a StructureDefinition if it is resolvable and defines an extension, otherwise <c>null</c>.</returns>
        public static async Tasks.Task<StructureDefinition> FindExtensionDefinitionAsync(this IAsyncResourceResolver resolver, string uri)
        {
            var result = await resolver.TryResolveByCanonicalUriAsync(uri).ConfigureAwait(false);
            if (result.Value is not StructureDefinition sd) return null;

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
        public static async Tasks.Task<StructureDefinition> FindStructureDefinitionAsync(this IAsyncResourceResolver resolver, string uri)
        {
            var result = await resolver.TryResolveByCanonicalUriAsync(uri).ConfigureAwait(false);
            return result.Value as StructureDefinition;
        }

        /// <inheritdoc cref="FindStructureDefinitionForCoreTypeAsync(IAsyncResourceResolver, string)"/>
        [Obsolete("Using synchronous resolvers is not recommended anymore, use FindStructureDefinitionForCoreTypeAsync() instead.")]
        public static StructureDefinition FindStructureDefinitionForCoreType(this IResourceResolver resolver, string typename)
        {
            var url = Uri.IsWellFormedUriString(typename, UriKind.Absolute) ? typename : Canonical.ForCoreType(typename).Value;
            return resolver.FindStructureDefinition(url);
        }

        /// <summary>
        /// Resolve the StructureDefinition for the FHIR-defined type given in <paramref name="typename"/>.
        /// </summary>
        /// <remarks>If the <paramref name="typename"/> is a uri, will resolve the given uri, if it is a simple typename,
        /// it will resolve the typename below <c>http://hl7.org/fhir/StructureDefinition/</c>.
        /// </remarks>
        public static async Tasks.Task<StructureDefinition> FindStructureDefinitionForCoreTypeAsync(this IAsyncResourceResolver resolver, string typename)
        {
            var url = Uri.IsWellFormedUriString(typename, UriKind.Absolute) ? typename :
                Canonical.ForCoreType(typename).Value;
            return await resolver.FindStructureDefinitionAsync(url).ConfigureAwait(false);
        }

        /// <inheritdoc cref="FindValueSetAsync(IAsyncResourceResolver, string)"/>
        [Obsolete("Using synchronous resolvers is not recommended anymore, use FindValueSetAsync() instead.")]
        public static ValueSet FindValueSet(this IResourceResolver source, string uri)
            => source.ResolveByCanonicalUri(uri) as ValueSet;

        /// <summary>
        /// Find a ValueSet by canonical url.
        /// </summary>
        public static async Tasks.Task<ValueSet> FindValueSetAsync(this IAsyncResourceResolver source, string uri)
        {
            var result = await source.TryResolveByCanonicalUriAsync(uri).ConfigureAwait(false);
            return result.Value as ValueSet;
        }

        /// <inheritdoc cref="FindCodeSystemAsync(IAsyncResourceResolver, string)"/>
        [Obsolete("Using synchronous resolvers is not recommended anymore, use FindCodeSystemAsync() instead.")]
        public static CodeSystem FindCodeSystem(this IResourceResolver source, string uri)
            => source.ResolveByCanonicalUri(uri) as CodeSystem;

        /// <summary>
        /// Find a CodeSystem by canonical url.
        /// </summary>
        public static async Tasks.Task<CodeSystem> FindCodeSystemAsync(this IAsyncResourceResolver source, string uri)
        {
            var result = await source.TryResolveByCanonicalUriAsync(uri).ConfigureAwait(false);
            return result.Value as CodeSystem;
        }
    }
}