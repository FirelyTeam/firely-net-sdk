/* 
 * Copyright (c) 2017, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>
    /// Delegate for harvesting an <see cref="ArtifactSummary"/> from the current resource of an
    /// <see cref="INavigatorStream"/>, independent of the underlying serialization format.
    /// </summary>
    public delegate ArtifactSummary ArtifactSummaryHarvester(INavigatorStream nav);

    /// <summary>Provides a default implementation for the <see cref="ArtifactSummaryHarvester"/> delegate.</summary>
    public static class DefaultArtifactSummaryHarvester
    {
        /// <summary>
        /// Extension method to harvest summary information from a set of resources via the specified
        /// <see cref="INavigatorStream"/>, independent of the underlying resource serialization format.
        /// </summary>
        /// <param name="harvester">A function that generates an <see cref="ArtifactSummary"/> from the current resource of the specified <see cref="INavigatorStream"/> instance.</param>
        /// <param name="stream">A sequence of <see cref="IElementNavigator"/> instances for a set of resources.</param>
        /// <returns>A sequence of <see cref="ArtifactSummary"/> instances for each of the target resources.</returns>
        public static IEnumerable<ArtifactSummary> HarvestAll(this ArtifactSummaryHarvester harvester, INavigatorStream stream)
        {
            while (stream.MoveNext())
            {
                var summary = harvester(stream);
                // Skip invalid input (no summary)
                if (summary != null)
                {
                    yield return summary;
                }
            }
        }

        /// <summary>
        /// Harvest summary information from the current resource of the specified
        /// <see cref="INavigatorStream"/>, independent of the actual resource serialization format.
        /// </summary>
        /// <param name="stream">An <see cref="INavigatorStream"/> instance that is positioned on a current entry.</param>
        /// <returns>A new <see cref="ArtifactSummary"/> record.</returns>
        public static ArtifactSummary Harvest(INavigatorStream stream)
        {
            // Dynamically create subtypes depending on resource type

            // INavigatorStream.Current property always returns a new navigator instance
            // => cache and reuse the returned instance
            var current = stream.Current;

            // Current returns null for invalid input
            if (current != null)
            {
                var rawType = current.Type;
                var resType = EnumUtility.ParseLiteral<ResourceType>(rawType);

                switch (resType)
                {
                    case null:
                        return new ArtifactSummary(stream, current);
                    case ResourceType.ConceptMap:
                        return new ConceptMapSummary(stream, current);
                    case ResourceType.NamingSystem:
                        return new NamingSystemSummary(stream, current);
                    case ResourceType.ValueSet:
                        return new ValueSetSummary(stream, current);
                    default:
                        if (ModelInfo.IsConformanceResource(rawType))
                        {
                            return new ConformanceResourceSummary(stream, current);
                        }
                        else
                        {
                            return new ArtifactSummary(stream, current);
                        }
                }
            }

            // Invalid input
            return null;
        }
    }

}
