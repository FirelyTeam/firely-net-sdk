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
using System.Collections.Generic;
using System.Linq;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Source
{
    public static class ResourceResolverExtensions
    {
        /// <inheritdoc cref="FindStructureDefinitionForCoreTypeAsync(IAsyncResourceResolver, FHIRAllTypes)"/>
        [Obsolete("Using synchronous resolvers is not recommended anymore, use FindStructureDefinitionForCoreTypeAsync() instead.")]
        public static StructureDefinition FindStructureDefinitionForCoreType(this IResourceResolver resolver, FHIRAllTypes type)
            => resolver.FindStructureDefinitionForCoreType(ModelInfo.FhirTypeToFhirTypeName(type));

        /// <summary>
        /// Resolve the StructureDefinition for the FHIR-defined type given in <paramref name="type"/>.
        /// </summary>
        public static async Tasks.Task<StructureDefinition> FindStructureDefinitionForCoreTypeAsync(this IAsyncResourceResolver resolver, FHIRAllTypes type)
            => await resolver.FindStructureDefinitionForCoreTypeAsync(ModelInfo.FhirTypeToFhirTypeName(type)).ConfigureAwait(false);

        /// <summary>
        /// Find all resources by resource type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The <see cref="IConformanceSource"/> </param>
        /// <returns>All the resources by Resource Type in the <see cref="IConformanceSource"/>.</returns>
        public static IEnumerable<T> FindAll<T>(this IConformanceSource source) where T : Resource
        {
            var typeName = ModelInfo.GetFhirTypeNameForType(typeof(T));

            if (typeName is not null)
            {
                var resourceType = EnumUtility.ParseLiteral<ResourceType>(typeName);
                var uris = source.ListResourceUris(resourceType);
                return uris
                    .Select(source.TryResolveByUri)
                    .Where(x => x.Success)
                    .Select(x => x.Value as T)
                    .Where(r => r != null);
            }
            else
                return null;
        }
    }
}