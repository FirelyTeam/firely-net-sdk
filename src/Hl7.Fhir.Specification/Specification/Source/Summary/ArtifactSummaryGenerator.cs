/* 
 * Copyright (c) 2017, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

#if NET_FILESYSTEM

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Serialization;
using System;
using System.Collections.Generic;
using System.IO;

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
        /// A list of default <see cref="ArtifactSummaryHarvester"/> delegates that the
        /// <see cref="ArtifactSummaryGenerator"/> calls to harvest summary information
        /// from an artifact.
        /// </summary>
        public static readonly ArtifactSummaryHarvester[] DefaultHarvesters
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
        /// An optional list of <see cref="ArtifactSummaryHarvester"/> delegates that the generator will call
        /// instead of the default harvesters to harvest summary information from an artifact.
        /// </param>
        /// <returns>A list of new <see cref="ArtifactSummary"/> instances.</returns>
        /// <remarks>
        /// For each artifact, the generator executes all (default or specified) harvester delegates
        /// in the specified order. When a delegate returns <c>true</c> to signal that harvesting has
        /// finished, the generator will not call any of the remaining delegates and immediately
        /// proceed to create the final <see cref="ArtifactSummary"/> return value.
        /// <para>
        /// By default, if the <paramref name="harvesters"/> parameter value is null or empty, the
        /// <see cref="ArtifactSummaryGenerator"/> calls the built-in default harvesters
        /// as specified by <see cref="ArtifactSummaryGenerator.DefaultHarvesters"/>.
        /// However if the caller specifies one or more harvester delegates, then the summary
        /// generator calls only the provided delegates, in the specified order.
        /// A custom delegate array may include one or more of the default harvesters.
        /// </para>
        /// <para>
        /// The generator catches all runtime exceptions that occur during harvesting and returns
        /// them as <see cref="ArtifactSummary"/> instances with <see cref="ArtifactSummary.IsFaulted"/>
        /// equal to <c>true</c> and <see cref="ArtifactSummary.Error"/> returning the exception.
        /// </para>
        /// </remarks>
        public static List<ArtifactSummary> Generate(
            string origin,
            params ArtifactSummaryHarvester[] harvesters)
        {
            var result = new List<ArtifactSummary>();

            // In case of error, return completed summaries and error info
            INavigatorStream navStream = null;
            try
            {
                // Call default navigator factory
                navStream = DefaultNavigatorStreamFactory.Create(origin);

                // Get some source file properties
                var fi = new FileInfo(origin);

                // Factory returns null for unknown file formats
                if (navStream == null) { return result; }

                // Run default or specified (custom) harvesters
                if (harvesters == null || harvesters.Length == 0)
                {
                    harvesters = DefaultHarvesters;
                }

                while (navStream.MoveNext())
                {
                    var current = navStream.Current;
                    if (current != null)
                    {
                        var properties = new ArtifactSummaryPropertyBag();

                        // Initialize default summary information
                        // Note: not exposed by IElementNavigator, cannot use harvester
                        properties.SetOrigin(origin);
                        properties.SetFileSize(fi.Length);
                        properties.SetLastModified(fi.LastWriteTimeUtc);
                        properties.SetPosition(navStream.Position);
                        properties.SetTypeName(current.Type);
                        properties.SetResourceUri(navStream.Position);

                        var summary = generate(properties, current, harvesters);

                        result.Add(summary);
                    }
                }
            }
            // TODO Catch specific exceptions
            // catch (System.IO.FileNotFoundException)
            // catch (UnauthorizedAccessException)
            // catch (System.Security.SecurityException)
            // catch (FormatException)
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
                        // TODO Catch specific exceptions
                        // catch (FormatException)
                        catch (Exception ex)
                        {
                            errors.Add(ex);
                        }
                    }
                }

                // Combine all errors into single AggregateException
                error = errors.Count > 0 ? new AggregateException(errors) : null;
            }
            // TODO Catch specific exceptions
            // catch (FormatException)
            // catch (NotSupportedException)
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

#endif
