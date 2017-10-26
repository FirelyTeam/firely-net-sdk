using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Source.Summary
{
    public static class ArtifactSummaryExtensions
    {
        /// <summary>Filter <see cref="ArtifactSummary"/> instances with errors.</summary>
        public static IEnumerable<ArtifactSummary> Errors(this IEnumerable<ArtifactSummary> summaries)
            => summaries.Where(s => s.IsFaulted);

        /// <summary>Filter <see cref="ArtifactSummary"/> instances with the specified <see cref="ResourceType"/>.</summary>
        public static IEnumerable<ArtifactSummary> OfResourceType(this IEnumerable<ArtifactSummary> summaries, ResourceType resourceType)
            => summaries.Where(s => s.ResourceType == resourceType);

        /// <summary>Filter <see cref="ArtifactSummary"/> instances for NamingSystem artifacts with the specified uniqueId value.</summary>
        public static IEnumerable<ArtifactSummary> NamingSystems(this IEnumerable<ArtifactSummary> summaries, string uniqueId)
            => summaries.OfResourceType(ResourceType.NamingSystem).Where(ns => ns.HasNamingSystemUniqueId(uniqueId));

        /// <summary>Filter <see cref="ArtifactSummary"/> instances for conformance resources.</summary>
        public static IEnumerable<ArtifactSummary> ConformanceResources(this IEnumerable<ArtifactSummary> summaries)
            => summaries.Where(s => ModelInfo.IsConformanceResource(s.ResourceType));

        /// <summary>Filter <see cref="ArtifactSummary"/> instances for conformance resources with the specified canonical url.</summary>
        public static IEnumerable<ArtifactSummary> ConformanceResources(this IEnumerable<ArtifactSummary> summaries, string canonicalUrl)
            => summaries.ConformanceResources().Where(r => r.GetConformanceCanonicalUrl() == canonicalUrl);

        /// <summary>Filter <see cref="ArtifactSummary"/> instances for ValueSet resources with the specified codeSystem system.</summary>
        public static IEnumerable<ArtifactSummary> ValueSets(this IEnumerable<ArtifactSummary> summaries, string system)
            => summaries.OfResourceType(ResourceType.ValueSet).Where(r => r.GetValueSetSystem() == system);

        /// <summary>Filter <see cref="ArtifactSummary"/> instances for ConceptMap resources with the specified source and/or target uri(s).</summary>
        public static IEnumerable<ArtifactSummary> ConceptMaps(this IEnumerable<ArtifactSummary> summaries, string sourceUri = null, string targetUri = null)
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
            => summaries.ConformanceResources(canonicalUrl).SingleOrDefault();

        /// <summary>Resolve the <see cref="ArtifactSummary"/> for the NamingSystem resource with the specified uniqueId.</summary>
        public static ArtifactSummary ResolveNamingSystem(this IEnumerable<ArtifactSummary> summaries, string uniqueId)
            => summaries.NamingSystems(uniqueId).SingleOrDefault();

        /// <summary>Resolve the <see cref="ArtifactSummary"/> for the ValueSet resource with the specified codeSystem system.</summary>
        public static ArtifactSummary ResolveValueSet(this IEnumerable<ArtifactSummary> summaries, string system)
            => summaries.ValueSets(system).SingleOrDefault();

        /// <summary>Resolve the <see cref="ArtifactSummary"/> for the ConceptMap resource with the specified source and/or target uri(s).</summary>
        public static ArtifactSummary ResolveConceptMap(this IEnumerable<ArtifactSummary> summaries, string sourceUri = null, string targetUri = null)
            => summaries.ConceptMaps(sourceUri, targetUri).SingleOrDefault();
    }
}
