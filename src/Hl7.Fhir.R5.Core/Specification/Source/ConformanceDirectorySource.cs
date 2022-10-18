/* 
 * Copyright (c) 2022, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>
    /// Reads FHIR artifacts (Profiles, ValueSets, ...) from a directory on disk. Thread-safe.
    /// </summary>
    /// <remarks>This class is for backward compatible only. It contains the methods <see cref="FindConceptMaps"/> and <see cref="FindNamingSystem"/> which are 
    /// removed from the interface <see cref="IConformanceSource"/>
    /// </remarks>
    public class ConformanceDirectorySource : DirectorySource
    {
        /// <inheritdoc/>
        public ConformanceDirectorySource(string contentDirectory, DirectorySourceSettings settings) : base(contentDirectory, settings)
        {
        }

        /// <summary>Find <see cref="ConceptMap"/> resources which map from the given source to the given target.</summary>
        /// <param name="sourceUri">An uri that is either the source uri, source ValueSet system or source StructureDefinition canonical url for the map.</param>
        /// <param name="targetUri">An uri that is either the target uri, target ValueSet system or target StructureDefinition canonical url for the map.</param>
        /// <returns>A sequence of <see cref="ConceptMap"/> resources.</returns>
        /// <remarks>Either sourceUri may be null, or targetUri, but not both</remarks>
        public new IEnumerable<ConceptMap> FindConceptMaps(string sourceUri = null, string targetUri = null)
        {
            if (sourceUri == null && targetUri == null)
            {
                throw Error.ArgumentNull(nameof(targetUri), $"{nameof(sourceUri)} and {nameof(targetUri)} arguments cannot both be null");
            }
            var summaries = GetSummaries().FindConceptMaps(sourceUri, targetUri);
            return summaries.Select(summary => loadResourceInternal<ConceptMap>(summary)).Where(r => r != null);
        }

        /// <summary>Finds a <see cref="NamingSystem"/> resource by matching any of a system's UniqueIds.</summary>
        /// <param name="uniqueId">The unique id of a <see cref="NamingSystem"/> resource.</param>
        /// <returns>A <see cref="NamingSystem"/> resource, or <c>null</c>.</returns>
        public new NamingSystem FindNamingSystem(string uniqueId)
        {
            if (uniqueId == null) throw Error.ArgumentNull(nameof(uniqueId));
            var summary = GetSummaries().ResolveNamingSystem(uniqueId);
            return loadResourceInternal<NamingSystem>(summary);
        }
    }
}