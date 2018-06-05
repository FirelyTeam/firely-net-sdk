/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

#if NET_FILESYSTEM

using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>Extension methods for <see cref="IEnumerable{ArtifactSummary}"/> sequences.</summary>
    public static class ArtifactSummaryExtensions
    {
        /// <summary>Filter <see cref="ArtifactSummary"/> instances with errors.</summary>
        public static IEnumerable<ArtifactSummary> Errors(this IEnumerable<ArtifactSummary> summaries)
            => summaries.Where(s => s.IsFaulted);

        /// <summary>Filter <see cref="ArtifactSummary"/> instances for resources with the specified <see cref="ResourceType"/>.</summary>
        public static IEnumerable<ArtifactSummary> OfResourceType(this IEnumerable<ArtifactSummary> summaries, ResourceType resourceType)
            => summaries.Where(s => s.ResourceType == resourceType);

        /// <summary>Filter <see cref="ArtifactSummary"/> instances for resources with the specified <see cref="ResourceType"/>.</summary>
        public static IEnumerable<ArtifactSummary> OfResourceType(this IEnumerable<ArtifactSummary> summaries, ResourceType? resourceType)
            => resourceType.HasValue
            ? summaries.Where(s => s.ResourceType == resourceType.Value)
            : summaries;

        /// <summary>Find <see cref="ArtifactSummary"/> instances for <see cref="NamingSystem"/> resources with the specified uniqueId value.</summary>
        public static IEnumerable<ArtifactSummary> FindNamingSystems(this IEnumerable<ArtifactSummary> summaries, string uniqueId)
            => summaries.OfResourceType(ResourceType.NamingSystem).Where(ns => ns.HasNamingSystemUniqueId(uniqueId));

        /// <summary>Filter <see cref="ArtifactSummary"/> instances for conformance resources.</summary>
        public static IEnumerable<ArtifactSummary> ConformanceResources(this IEnumerable<ArtifactSummary> summaries)
            => summaries.Where(s => ModelInfo.IsConformanceResource(s.ResourceType));

        /// <summary>Find <see cref="ArtifactSummary"/> instances for conformance resources with the specified canonical url.</summary>
        public static IEnumerable<ArtifactSummary> FindConformanceResources(this IEnumerable<ArtifactSummary> summaries, string canonicalUrl)
            => summaries.ConformanceResources().Where(r => r.GetConformanceCanonicalUrl() == canonicalUrl);

        /// <summary>Find <see cref="ArtifactSummary"/> instances for <see cref="ValueSet"/> resources with the specified codeSystem system.</summary>
        public static IEnumerable<ArtifactSummary> FindValueSets(this IEnumerable<ArtifactSummary> summaries, string system)
            => summaries.OfResourceType(ResourceType.ValueSet).Where(r => r.GetValueSetSystem() == system);

        /// <summary>Find <see cref="ArtifactSummary"/> instances for <see cref="ConceptMap"/> resources with the specified source and/or target uri(s).</summary>
        public static IEnumerable<ArtifactSummary> FindConceptMaps(this IEnumerable<ArtifactSummary> summaries, string sourceUri = null, string targetUri = null)
        {
            IEnumerable<ArtifactSummary> result = summaries.OfResourceType(ResourceType.ConceptMap);
            if (sourceUri != null)
            {
                result = result.Where(cm => cm.GetConceptMapSource() == sourceUri);
            }
            if (targetUri != null)
            {
                result = result.Where(cm => cm.GetConceptMapTarget() == targetUri);
            }
            return result;
        }

        /// <summary>Resolve the <see cref="ArtifactSummary"/> for the resource with the specified uri.</summary>
        public static ArtifactSummary ResolveByUri(this IEnumerable<ArtifactSummary> summaries, string uri)
            => summaries.Where(r => r.ResourceUri == uri).SingleOrDefault();

        /// <summary>Resolve the <see cref="ArtifactSummary"/> for the comformance resource with the specified canonical uri.</summary>
        public static ArtifactSummary ResolveByCanonicalUri(this IEnumerable<ArtifactSummary> summaries, string canonicalUrl)
            => summaries.FindConformanceResources(canonicalUrl).SingleOrDefault();

        /// <summary>Resolve the <see cref="ArtifactSummary"/> for the NamingSystem resource with the specified uniqueId.</summary>
        public static ArtifactSummary ResolveNamingSystem(this IEnumerable<ArtifactSummary> summaries, string uniqueId)
            => summaries.FindNamingSystems(uniqueId).SingleOrDefault();

        /// <summary>Resolve the <see cref="ArtifactSummary"/> for the ValueSet resource with the specified codeSystem system.</summary>
        public static ArtifactSummary ResolveValueSet(this IEnumerable<ArtifactSummary> summaries, string system)
            => summaries.FindValueSets(system).SingleOrDefault();

        /// <summary>Resolve the <see cref="ArtifactSummary"/> for the ConceptMap resource with the specified source and/or target uri(s).</summary>
        public static ArtifactSummary ResolveConceptMap(this IEnumerable<ArtifactSummary> summaries, string sourceUri = null, string targetUri = null)
            => summaries.FindConceptMaps(sourceUri, targetUri).SingleOrDefault();
    }
}

#endif
