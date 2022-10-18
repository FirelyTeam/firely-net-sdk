/* 
 * Copyright (c) 2022, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>Reads FHIR artifacts (Profiles, ValueSets, ...) from a ZIP archive. Thread-safe.
    /// </summary>
    /// <remarks>This class is for backward compatible only. It contains the methods <see cref="FindConceptMaps"/> and <see cref="FindNamingSystem"/> which are 
    /// removed from the interface <see cref="IConformanceSource"/>
    /// </remarks>
    public class ConformanceZipSource : ZipSource
    {
        /// <inheritdoc/>
        public ConformanceZipSource(string zipPath) : base(zipPath)
        {
            directorySourceFactory = (contentDirectory, settings) => new ConformanceDirectorySource(contentDirectory, settings);
        }

        /// <inheritdoc/>
        public ConformanceZipSource(string zipPath, DirectorySourceSettings settings) : base(zipPath, settings)
        {
            directorySourceFactory = (contentDirectory, settings) => new ConformanceDirectorySource(contentDirectory, settings);
        }

        private ConformanceDirectorySource NewSource => FileSource as ConformanceDirectorySource;

        /// <summary>Find <see cref="ConceptMap"/> resources which map from the given source to the given target.</summary>
        /// <param name="sourceUri">An uri that is either the source uri, source ValueSet system or source StructureDefinition canonical url for the map.</param>
        /// <param name="targetUri">An uri that is either the target uri, target ValueSet system or target StructureDefinition canonical url for the map.</param>
        /// <returns>A sequence of <see cref="ConceptMap"/> resources.</returns>
        /// <remarks>Either sourceUri may be null, or targetUri, but not both</remarks>
        public new IEnumerable<ConceptMap> FindConceptMaps(string sourceUri = null, string targetUri = null)
            => NewSource.FindConceptMaps(sourceUri, targetUri);

        /// <summary>Finds a <see cref="NamingSystem"/> resource by matching any of a system's UniqueIds.</summary>
        /// <param name="uniqueId">The unique id of a <see cref="NamingSystem"/> resource.</param>
        /// <returns>A <see cref="NamingSystem"/> resource, or <c>null</c>.</returns>
        public new NamingSystem FindNamingSystem(string uniqueId) => NewSource.FindNamingSystem(uniqueId);
    }
}