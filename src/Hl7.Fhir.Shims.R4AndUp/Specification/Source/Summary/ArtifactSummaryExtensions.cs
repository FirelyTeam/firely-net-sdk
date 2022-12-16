/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using Hl7.Fhir.Utility;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>Extension methods for <see cref="IEnumerable{ArtifactSummary}"/> sequences.</summary>
    public static class ArtifactSummaryExtensions
    {
        /// <summary>
        /// Filter <see cref="ArtifactSummary"/> instances for resources with the specified <see cref="ResourceType"/>.
        /// If <paramref name="resourceType"/> equals <c>null</c>, then filter summaries for all valid FHIR resources.
        /// </summary>
        public static IEnumerable<ArtifactSummary> OfResourceType(this IEnumerable<ArtifactSummary> summaries, ResourceType resourceType)
            => summaries.OfResourceType(resourceType.GetLiteral());

        /// <summary>
        /// Filter <see cref="ArtifactSummary"/> instances for resources with the specified <see cref="ResourceType"/>.
        /// If <paramref name="resourceType"/> equals <c>null</c>, then filter summaries for all valid FHIR resources.
        /// </summary>
        public static IEnumerable<ArtifactSummary> OfResourceType(this IEnumerable<ArtifactSummary> summaries, ResourceType? resourceType)
            => summaries.OfResourceType(resourceType is not null ? resourceType.GetLiteral() : null);

        /// <summary>Find <see cref="ArtifactSummary"/> instances for conformance resources with the specified canonical url.</summary>
        public static IEnumerable<ArtifactSummary> FindConformanceResources(this IEnumerable<ArtifactSummary> summaries, string canonicalUrl)
            => summaries.FindConformanceResources(canonicalUrl, ModelInfo.ModelInspector);

        /// <summary>Resolve the <see cref="ArtifactSummary"/> for the resource with the specified uri.</summary>
        public static ArtifactSummary? ResolveByUri(this IEnumerable<ArtifactSummary> summaries, string uri)
            => summaries.ResolveByUri(uri, ModelInfo.ModelInspector);

        /// <summary>Resolve the <see cref="ArtifactSummary"/> for the comformance resource with the specified canonical uri.</summary>
        /// <exception cref="ResolvingConflictException">The <see cref="ISummarySource"/> instance encountered conflicting Conformance Resource artifacts with the same canonical url identifier.</exception>
        public static ArtifactSummary? ResolveByCanonicalUri(this IEnumerable<ArtifactSummary> summaries, string canonicalUrl)
            => summaries.ResolveByCanonicalUri(canonicalUrl, ModelInfo.ModelInspector);

        /// <summary>Resolve the <see cref="ArtifactSummary"/> for the <c>NamingSystem</c> resource with the specified uniqueId.</summary>
        public static ArtifactSummary? ResolveNamingSystem(this IEnumerable<ArtifactSummary> summaries, string uniqueId)
            => summaries.ResolveNamingSystem(uniqueId, ModelInfo.ModelInspector);

        /// <summary>Resolve the <see cref="ArtifactSummary"/> for the <see cref="CodeSystem"/> resource with the specified ValueSet uri.</summary>
        public static ArtifactSummary? ResolveCodeSystem(this IEnumerable<ArtifactSummary> summaries, string uri)
            => summaries.ResolveCodeSystem(uri, ModelInfo.ModelInspector);

        /// <summary>Resolve the <see cref="ArtifactSummary"/> for the <c>ConceptMap</c> resource with the specified source and/or target uri(s).</summary>
        public static ArtifactSummary? ResolveConceptMap(this IEnumerable<ArtifactSummary> summaries, string? sourceUri = null, string? targetUri = null)
            => summaries.ResolveConceptMap(ModelInfo.ModelInspector, sourceUri, targetUri);
    }
}
#nullable restore