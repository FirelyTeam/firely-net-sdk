/* 
 * Copyright (c) 2017, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Serialization;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Source.Summary
{
    /// <summary>Represents a method that tries to harvest specific summary information from an artifact.</summary>
    /// <param name="nav">An <see cref="IElementNavigator"/> instance to navigate the artifact.</param>
    /// <param name="properties">A dictionary for storing the harvested summary information.</param>
    /// <returns>
    /// Returns <c>true </c> to indicate that all relevant properties have been harvested from the artifact and the summary is ready to be generated.
    /// Returns <c>false</c> to try and continue harvesting additional summary information.
    /// </returns>
    /// <remarks>
    /// The specified <see cref="IElementNavigator"/> is positioned on the first child element level (e.g. <c>StructureDefinition.url</c>).
    /// The target method can fetch summary information starting from the current position in a forward direction.
    /// When finished, the navigator should again be positioned on the first nesting level, so any remaining
    /// delegates can continue harvesting additional information from there.
    /// </remarks>
    public delegate bool ArtifactSummaryHarvester(IElementNavigator nav, ArtifactSummaryPropertyBag properties);

    /// <summary>
    /// For generating artifact summary information from an <see cref="INavigatorStream"/>,
    /// independent of the underlying resource serialization format.
    /// </summary>
    public static class ArtifactSummaryGenerator
    {

        /// <summary>
        /// A list of default <see cref="ArtifactSummaryHarvester"/> delegates
        /// for the <see cref="ArtifactSummaryGenerator"/> class.
        /// </summary>
        public static readonly ArtifactSummaryHarvester[] DefaultArtifactSummaryHarvesters
            = new ArtifactSummaryHarvester[]
            {
                NamingSystemSummaryProperties.Harvest,
                // Specific conformance resources first
                StructureDefinitionSummaryProperties.Harvest,
                ValueSetSummaryProperties.Harvest,
                ConceptMapSummaryProperties.Harvest,
                // Fall back for all other conformance resources
                ConformanceSummaryProperties.Harvest
            };

        /// <summary>Generate a list of artifact summary information from an <see cref="INavigatorStream"/> instance.</summary>
        /// <param name="origin">The original location of the target artifact (or the containing Bundle).</param>
        /// <param name="harvesters">
        /// An optional list of <see cref="ArtifactSummaryHarvester"/> delegates to harvest summary information
        /// from an artifact. By default, if this argument is missing or empty, the generator executes all of
        /// the default summary harvesters as defined by <see cref="DefaultArtifactSummaryHarvesters"/>.
        /// </param>
        /// <returns>A list of new <see cref="ArtifactSummary"/> instances.</returns>
        /// <remarks>
        /// For each artifact, the generator executes all the harvester delegates in the specified order.
        /// If a caller returns <c>true</c> to signal that harvesting has finished, the
        /// generator will not call any of the remaining delegates and immediately proceed to create
        /// the final <see cref="ArtifactSummary"/> return value.
        /// <para>
        /// By default, the generator calls all the harvesters defined by <see cref="DefaultArtifactSummaryHarvesters"/>.
        /// However if the caller specifies one or more harvester delegates, then the generator will
        /// call only the provided delegates in the specified order. The caller can also explicitly
        /// specify one or more default harvester delegates.
        /// </para>
        /// <para>
        /// The generator catches all runtime exceptions that occur during harvesting and converts
        /// the errors to <see cref="ArtifactSummary"/> instances, with the <see cref="ArtifactSummary.IsFaulted"/>
        /// property equal to <c>true</c> and the <see cref="ArtifactSummary.Error"/> property returning the
        /// exception.
        /// </para>
        /// </remarks>
        public static List<ArtifactSummary> Generate(
            string origin,
            params ArtifactSummaryHarvester[] harvesters)
        {
            // [WMR 20171023] In case of error, return completed summaries and error info
            var result = new List<ArtifactSummary>();

            INavigatorStream navStream = null;
            try
            {
                // Call default navigator factory
                navStream = DefaultNavigatorStreamFactory.Create(origin);

                // Factory returns null for unknown file formats
                if (navStream == null) { return result; }

                // Run default or specified (custom) harvesters
                if (harvesters == null || harvesters.Length == 0)
                {
                    harvesters = DefaultArtifactSummaryHarvesters;
                }

                while (navStream.MoveNext())
                {
                    var current = navStream.Current;
                    if (current != null)
                    {
                        var properties = new ArtifactSummaryPropertyBag
                        {
                            // Add default summary information
                            // Note: not exposed by IElementNavigator, cannot use harvester
                            [ArtifactSummaryProperties.OriginKey] = origin,
                            [ArtifactSummaryProperties.PositionKey] = navStream.Position,
                            [ArtifactSummaryProperties.ResourceUriKey] = navStream.Position,
                            [ArtifactSummaryProperties.ResourceTypeNameKey] = current.Type
                        };

                        var summary = generate(properties, current, harvesters); // allHarvesters

                        result.Add(summary);
                    }
                }
            }
            catch (Exception ex)
            {
                result.Add(ArtifactSummary.FromException(ex, origin));
            }
            finally
            {
                navStream?.Dispose();
            }
            return result;
        }

        // Generate summary for a single artifact
        static ArtifactSummary generate(
            ArtifactSummaryPropertyBag props,
            IElementNavigator nav, 
            ArtifactSummaryHarvester[] harvesters)
        {
            Exception error = null;
            try
            {
                // Harvest summary information via specified harvesters
                // Top-level harvesters receive navigator positioned on the first child element level

                // Catch individual exceptions inside loop, return as AggregateException
                var errors = new List<Exception>();
                if (nav.MoveToFirstChild())
                {
                    foreach (var harvester in harvesters)
                    {
                        try
                        {
                            if (harvester != null &&  harvester.Invoke(nav, props))
                            {
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            errors.Add(ex);
                        }
                    }
                }

                // Combine all errors into single AggregateException
                error = errors.Count > 0 ? new AggregateException(errors) : null;
            }
            catch (Exception ex)
            {
                // Error in summary factory?
                // Make sure we always return a valid summary
                error = ex;
            }

            // Create final summary from harvested properties and optional error
            return new ArtifactSummary(props, error);
        }

    }
}
