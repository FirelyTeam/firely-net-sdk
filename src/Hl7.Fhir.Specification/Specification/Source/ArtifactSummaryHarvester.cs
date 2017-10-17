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

namespace Hl7.Fhir.Specification.Source
{
    // [WMR 20171011] Note:
    // - Derive from ArtifactSummary to define additional custom properties
    // - Derive from ArtifactSummaryHarvester to actually initialize the custom properties

    /// <summary>
    /// Default factory for <see cref="ArtifactSummary"/> records.
    /// Extracts a concrete set of summary information from a raw FHIR artifact,
    /// independent of the underlying serialization format, by calling the
    /// generic <see cref="IElementNavigator"/> interface.
    /// 
    /// Consumers can implement support for additional custom resource summary information
    /// by defining two specialized classes that derive from <see cref="ArtifactSummary"/>
    /// and <see cref="ArtifactSummaryHarvester"/>.
    /// </summary>
    public class ArtifactSummaryHarvester
    {
        /// <summary>Returns the global default instance.</summary>
        public static ArtifactSummaryHarvester Default { get; } = new ArtifactSummaryHarvester();

        /// <summary>ctor</summary>
        public ArtifactSummaryHarvester() { }

        /// <summary>
        /// Extract summary information from a sequence of resources via the <see cref="IElementNavigator"/>
        /// interface, independent of the underlying resource serialization format.
        /// </summary>
        /// <param name="stream">A sequence of <see cref="IElementNavigator"/> instances for a set of (bundle) resources.</param>
        /// <returns>A sequence of <see cref="ArtifactSummary"/> instances for each of the target resources.</returns>
        public IEnumerable<ArtifactSummary> Enumerate(INavigatorStream stream)
        {
            while (stream.MoveNext())
            {
                var summary = Generate(stream);
                // Skip invalid input (no summary)
                if (summary != null)
                {
                    yield return summary;
                }
            }
        }

        /// <summary>
        /// Creates and initializes a new <see cref="ArtifactSummary"/> record by extracting
        /// a concrete set of summary information from the current entry of the specified
        /// <see cref="INavigatorStream"/>, independent of the actual resource serialization format.
        /// Derived classes can override this method to generate custom summary information.
        /// </summary>
        /// <param name="stream">An <see cref="INavigatorStream"/> instance that is positioned on a current entry.</param>
        /// <returns>A new <see cref="ArtifactSummary"/> record.</returns>
        protected virtual ArtifactSummary Generate(INavigatorStream stream)
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

                if (resType == null)
                {
                    return new ArtifactSummary(stream, current);
                }
                else if (resType == ResourceType.ConceptMap)
                {
                    return new ConceptMapSummary(stream, current);
                }
                else if (resType == ResourceType.NamingSystem)
                {
                    return new NamingSystemSummary(stream, current);
                }
                else if (resType == ResourceType.ValueSet)
                {
                    return new ValueSetSummary(stream, current);
                }
                else if (ModelInfo.IsConformanceResource(rawType))
                {
                    return new ConformanceResourceSummary(stream, current);
                }
                else
                {
                    return new ArtifactSummary(stream, current);
                }
            }

            // Invalid input
            return null;
        }
    }

}
