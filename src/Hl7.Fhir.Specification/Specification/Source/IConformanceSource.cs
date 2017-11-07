/* 
 * Copyright (c) 2017, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using System;
using System.Collections.Generic;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source.Summary;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>Interface for browsing and resolving FHIR conformance resources.</summary>
    public interface IConformanceSource : IResourceResolver
    {
        /// <summary>Returns a list of summary information for all the FHIR artifacts provided by the source.</summary>
        IEnumerable<ArtifactSummary> ListSummaries();

        /// <summary>List all resource uris for the resources managed by the source, optionally filtered by type.</summary>
        /// <param name="filter">A <see cref="ResourceType"/> enum value.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> sequence of uri strings.</returns>
        IEnumerable<string> ListResourceUris(ResourceType? filter = null);

        /// <summary>
        /// Find ValueSets that define codes for a Codesystem with the given system uri
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        ValueSet FindValueSetBySystem(string system);

        /// <summary>
        /// Find ConceptMaps which map from the given sourceUri to the given targetUri
        /// </summary>
        /// <param name="sourceUri">An uri that is either the source uri, source ValueSet system or source StructureDefinition canonical url for the map.</param>
        /// <param name="targetUri">An uri that is either the target uri, target ValueSet system or target StructureDefinition canonical url for the map.</param>
        /// <returns></returns>
        /// <remarks>Either sourceUri may be null, or targetUri, but not both</remarks>
        IEnumerable<ConceptMap> FindConceptMaps(string sourceUri=null, string targetUri=null);

        /// <summary>
        /// Finds a NamingSystem resource by matching any of a system's UniqueIds
        /// </summary>
        /// <param name="uniqueid"></param>
        /// <returns></returns>
        NamingSystem FindNamingSystem(string uniqueid);
    }

    /// <summary>Extension methods for the <see cref="IConformanceSource"/> interface.</summary>
    public static class ConformanceSourceExtensions
    {
        /// <summary>Returns a list of <see cref="ArtifactSummary"/> instances with error information.</summary>
        /// <returns>A <see cref="List{T}"/> of <see cref="ArtifactSummary"/> instances.</returns>
        public static IEnumerable<ArtifactSummary> Errors(this IConformanceSource source) => source.ListSummaries().Errors();

        /// <summary>Returns a list of <see cref="ArtifactSummary"/> instances for resources of the specified <see cref="ResourceType"/>.</summary>
        /// <returns>A <see cref="List{T}"/> of <see cref="ArtifactSummary"/> instances.</returns>
        public static IEnumerable<ArtifactSummary> Summaries(this IConformanceSource source, ResourceType resourceType) => source.ListSummaries().OfResourceType(resourceType);
    }

}