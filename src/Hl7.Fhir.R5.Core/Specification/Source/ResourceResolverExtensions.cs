/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using System;
using T = System.Threading.Tasks;

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
        public static async T.Task<StructureDefinition> FindStructureDefinitionForCoreTypeAsync(this IAsyncResourceResolver resolver, FHIRAllTypes type)
            => await resolver.FindStructureDefinitionForCoreTypeAsync(ModelInfo.FhirTypeToFhirTypeName(type)).ConfigureAwait(false);
    }
}
