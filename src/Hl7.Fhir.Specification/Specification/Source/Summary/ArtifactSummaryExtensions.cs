using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source.Summary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Source.Summary
{
    public static class ArtifactSummaryExtensions
    {
        /// <summary>Filter error items from the specified <see cref="ArtifactSummary"/> sequence.</summary>
        /// <param name="summaries"></param>
        /// <returns></returns>
        public static IEnumerable<ArtifactSummary> Errors(this IEnumerable<ArtifactSummary> summaries)
            => summaries.Where(s => s.IsFaulted);

        public static IEnumerable<ArtifactSummary> OfResourceType(this IEnumerable<ArtifactSummary> summaries, ResourceType resourceType)
            => summaries.Where(s => s.ResourceType == resourceType);

        public static IEnumerable<ArtifactSummary> NamingSystems(this IEnumerable<ArtifactSummary> summaries, string uniqueId)
            => summaries.OfResourceType(ResourceType.NamingSystem).Where(ns => ns.HasNamingSystemUniqueId(uniqueId));

        public static IEnumerable<ArtifactSummary> ConformanceResources(this IEnumerable<ArtifactSummary> summaries)
            => summaries.Where(s => ModelInfo.IsConformanceResource(s.ResourceType));

        public static IEnumerable<ArtifactSummary> ConformanceResources(this IEnumerable<ArtifactSummary> summaries, string canonicalUrl)
            => summaries.ConformanceResources().Where(r => r.GetConformanceCanonicalUrl() == canonicalUrl);

        public static IEnumerable<ArtifactSummary> ValueSets(this IEnumerable<ArtifactSummary> summaries, string system)
            => summaries.OfResourceType(ResourceType.ValueSet).Where(r => r.GetValueSetSystem() == system);

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

        public static ArtifactSummary ResolveByUri(this IEnumerable<ArtifactSummary> summaries, string uri)
            => summaries.Where(r => r.ResourceUri == uri).SingleOrDefault();

        public static ArtifactSummary ResolveByCanonicalUri(this IEnumerable<ArtifactSummary> summaries, string canonicalUrl)
            => summaries.ConformanceResources(canonicalUrl).SingleOrDefault();

        public static ArtifactSummary ResolveNamingSystem(this IEnumerable<ArtifactSummary> summaries, string uniqueId)
            => summaries.NamingSystems(uniqueId).SingleOrDefault();

        public static ArtifactSummary ResolveValueSet(this IEnumerable<ArtifactSummary> summaries, string system)
            => summaries.ValueSets(system).SingleOrDefault();

        public static ArtifactSummary ResolveConceptMap(this IEnumerable<ArtifactSummary> summaries, string sourceUri = null, string targetUri = null)
            => summaries.ConceptMaps(sourceUri, targetUri).SingleOrDefault();
    }
}
