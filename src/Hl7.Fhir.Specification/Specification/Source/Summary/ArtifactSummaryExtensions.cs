﻿/* 
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
using System.IO;
using System.Linq;

namespace Hl7.Fhir.Specification.Source.Summary
{
    /// <summary>Extension methods for <see cref="ArtifactSummary"/> instances and sequences.</summary>
    public static class ArtifactSummaryExtensions
    {
        // Extension methods for ArtifactSummary


        /// <summary>Try to load a resource from the summary origin.</summary>
        public static Resource LoadResource(this ArtifactSummary summary) => summary.LoadResource<Resource>();

        /// <summary>Try to load a structure definition from the summary origin.</summary>
        public static StructureDefinition LoadStructure(this ArtifactSummary summary) => summary.LoadResource<StructureDefinition>();

        // [WMR 20171204] SummaryGenerator only returns summaries for XML and JSON files (not XSD)
        // So currently, this method only serves as a fall-back for xml/json files that 
        // the FHIR parser cannot deserialize (non-FHIR, invalid, incompatible FHIR version, ...)

        /// <summary>Try to open the summary origin for reading.</summary>
        /// <returns>A <see cref="Stream"/> instance.</returns>
        /// <remarks>
        /// Allows processing non-FHIR/invalid/incompatible artifacts.
        /// Use the <seealso cref="ArtifactSummary.LoadResource{T}"/> method to load FHIR resources.
        /// </remarks>
        public static Stream LoadArtifact(this ArtifactSummary summary) => File.OpenRead(summary.Origin);


        // Extension methods for IEnumerable<ArtifactSummary>


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

        /// <summary>Filter <see cref="ArtifactSummary"/> instances for <see cref="CodeSystem"/> resources with the specified valueSet uri.</summary>
        public static IEnumerable<ArtifactSummary> FindCodeSystems(this IEnumerable<ArtifactSummary> summaries, string valueSetUri)
            => summaries.OfResourceType(ResourceType.CodeSystem).Where(r => r.GetCodeSystemValueSet() == valueSetUri);

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

        /// <summary>Resolve the <see cref="ArtifactSummary"/> for the <see cref="NamingSystem"/> resource with the specified uniqueId.</summary>
        public static ArtifactSummary ResolveNamingSystem(this IEnumerable<ArtifactSummary> summaries, string uniqueId)
            => summaries.FindNamingSystems(uniqueId).SingleOrDefault();

        /// <summary>Resolve the <see cref="ArtifactSummary"/> for the <see cref="CodeSystem"/> resource with the specified ValueSet uri.</summary>
        public static ArtifactSummary ResolveCodeSystem(this IEnumerable<ArtifactSummary> summaries, string uri)
            => summaries.FindCodeSystems(uri).SingleOrDefault();

        /// <summary>Resolve the <see cref="ArtifactSummary"/> for the <see cref="ConceptMap"/> resource with the specified source and/or target uri(s).</summary>
        public static ArtifactSummary ResolveConceptMap(this IEnumerable<ArtifactSummary> summaries, string sourceUri = null, string targetUri = null)
            => summaries.FindConceptMaps(sourceUri, targetUri).SingleOrDefault();
    }
}

#endif
