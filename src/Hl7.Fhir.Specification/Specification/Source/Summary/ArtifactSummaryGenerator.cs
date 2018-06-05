/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

#if NET_FILESYSTEM

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Source;
using System;
using System.Collections.Generic;
using System.IO;

// Expose low-level interfaces from a separate child namespace, to prevent pollution
namespace Hl7.Fhir.Specification.Summary
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
        /// <see cref="ArtifactSummaryGenerator"/> uses to harvest specific summary
        /// information from different types of conformance resources.
        /// <para>
        /// Each harvester extracts summary information from a specific type of resource(s).
        /// The generator executes the harvesters in the specified order,
        /// until one of the harvester delegates returns <c>true</c>.
        /// The generator then skips any remaining harvesters and continues
        /// processing the next resource.
        /// </para>
        /// </summary>
        public static readonly ArtifactSummaryHarvester[] ConformanceHarvesters
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

        /// <summary>
        /// Generate a list of artifact summary information for a resource file on disk,
        /// using the default <see cref="ConformanceHarvesters"/>.
        /// <para>
        /// If the target resource represents a <see cref="Bundle"/> instance, then the generator
        /// returns a list of summaries for all resource entries contained in the bundle.
        /// </para>
        /// </summary>
        /// <param name="origin">The file path of the target artifact (or the containing Bundle).</param>
        /// <returns>A list of new <see cref="ArtifactSummary"/> instances.</returns>
        /// <remarks>
        /// The generator catches all runtime exceptions that occur during harvesting and returns
        /// them as <see cref="ArtifactSummary"/> instances with <see cref="ArtifactSummary.IsFaulted"/>
        /// equal to <c>true</c> and <see cref="ArtifactSummary.Error"/> returning the exception.
        /// </remarks>
        public static List<ArtifactSummary> Generate(string origin) => Generate(origin, ConformanceHarvesters);

        /// <summary>
        /// Generate a list of artifact summary information from a resource input stream,
        /// using the default <see cref="ConformanceHarvesters"/>.
        /// <para>
        /// If the target resource represents a <see cref="Bundle"/> instance, then the generator
        /// returns a list of summaries for all resource entries contained in the bundle.
        /// </para>
        /// </summary>
        /// <param name="navStream">An <see cref="INavigatorStream"/> instance.</param>
        /// <returns>A list of new <see cref="ArtifactSummary"/> instances.</returns>
        /// <remarks>
        /// The generator catches all runtime exceptions that occur during harvesting and returns
        /// them as <see cref="ArtifactSummary"/> instances with <see cref="ArtifactSummary.IsFaulted"/>
        /// equal to <c>true</c> and <see cref="ArtifactSummary.Error"/> returning the exception.
        /// </remarks>
        public static List<ArtifactSummary> Generate(INavigatorStream navStream) => Generate(navStream, null, ConformanceHarvesters);

        /// <summary>
        /// Generate a list of artifact summary information from a resource input stream,
        /// using the default <see cref="ConformanceHarvesters"/>.
        /// <para>
        /// If the target resource represents a <see cref="Bundle"/> instance, then the generator
        /// returns a list of summaries for all resource entries contained in the bundle.
        /// </para>
        /// </summary>
        /// <param name="navStream">An <see cref="INavigatorStream"/> instance.</param>
        /// <param name="initProperties">
        /// An optional summary properties initialization method, or <c>null</c>.
        /// If specified, the generator will call this method for each generated summary,
        /// allowing the caller to modify or enrich the set of generated summary properties.
        /// </param>
        /// <returns>A list of new <see cref="ArtifactSummary"/> instances.</returns>
        /// <remarks>
        /// The generator catches all runtime exceptions that occur during harvesting and returns
        /// them as <see cref="ArtifactSummary"/> instances with <see cref="ArtifactSummary.IsFaulted"/>
        /// equal to <c>true</c> and <see cref="ArtifactSummary.Error"/> returning the exception.
        /// </remarks>
        public static List<ArtifactSummary> Generate(
            INavigatorStream navStream,
            Action<ArtifactSummaryPropertyBag> initProperties) => Generate(navStream, initProperties, ConformanceHarvesters);

        /// <summary>
        /// Generate a list of artifact summary information for a resource file on disk,
        /// using the specified list of <see cref="ArtifactSummaryHarvester"/> instances.
        /// <para>
        /// If the target resource represents a <see cref="Bundle"/> instance, then the generator
        /// returns a list of summaries for all resource entries contained in the bundle.
        /// </para>
        /// </summary>
        /// <param name="origin">The file path of the target artifact (or the containing Bundle).</param>
        /// <param name="harvesters">
        /// A list of <see cref="ArtifactSummaryHarvester"/> delegates that the
        /// generator calls to harvest summary information from each artifact.
        /// If the harvester list equals <c>null</c> or empty, then the generator will
        /// harvest only the common default summary properties.
        /// </param>
        /// <returns>A list of new <see cref="ArtifactSummary"/> instances.</returns>
        /// <remarks>
        /// The generator catches all runtime exceptions that occur during harvesting and returns
        /// them as <see cref="ArtifactSummary"/> instances with <see cref="ArtifactSummary.IsFaulted"/>
        /// equal to <c>true</c> and <see cref="ArtifactSummary.Error"/> returning the exception.
        /// </remarks>
        public static List<ArtifactSummary> Generate(
            string origin,
            params ArtifactSummaryHarvester[] harvesters)
        {
            List<ArtifactSummary> result = null;

            // Try to create navigator stream factory
            // May fail if the specified input is invalid => return error summary
            INavigatorStream navStream = null;
            try
            {
                navStream = DefaultNavigatorStreamFactory.Create(origin);

                // Factory returns null for unknown file formats
                if (navStream != null)
                {

                    // Get some source file properties
                    var fi = new FileInfo(origin);

                    // Resources from same origin share a common serialization format
                    string format =
                        fi.Extension == FhirFileFormats.XmlFileExtension ? FhirSerializationFormats.Xml
                        : fi.Extension == FhirFileFormats.JsonFileExtension ? FhirSerializationFormats.Json
                        : null;

                    // Local helper method to initialize specific summary properties
                    void InitializeSummaryFromOrigin(ArtifactSummaryPropertyBag properties)
                    {
                        properties.SetOrigin(origin);
                        properties.SetFileSize(fi.Length);
                        properties.SetLastModified(fi.LastWriteTimeUtc);
                        properties.SetSerializationFormat(format);
                    }

                    result = Generate(navStream, InitializeSummaryFromOrigin, harvesters);
                }
            }
            catch (Exception ex)
            {
                result = new List<ArtifactSummary>
                {
                    ArtifactSummary.FromException(ex, origin)
                };
            }

            return result;

        }

        /// <summary>
        /// Generate a list of artifact summary information from a resource input stream,
        /// using the default <see cref="ConformanceHarvesters"/>.
        /// <para>
        /// If the target resource represents a <see cref="Bundle"/> instance, then the generator
        /// returns a list of summaries for all resource entries contained in the bundle.
        /// </para>
        /// </summary>
        /// <param name="navStream">An <see cref="INavigatorStream"/> instance.</param>
        /// <param name="initProperties">
        /// An optional summary properties initialization method, or <c>null</c>.
        /// If specified, the generator will call this method for each generated summary,
        /// allowing the caller to modify or enrich the set of generated summary properties.
        /// </param>
        /// <param name="harvesters">
        /// A list of <see cref="ArtifactSummaryHarvester"/> delegates that the
        /// generator calls to harvest summary information from each artifact.
        /// If the harvester list equals <c>null</c> or empty, then the generator will
        /// harvest only the common default summary properties.
        /// </param>
        /// <returns>A list of new <see cref="ArtifactSummary"/> instances.</returns>
        /// <remarks>
        /// The generator catches all runtime exceptions that occur during harvesting and returns
        /// them as <see cref="ArtifactSummary"/> instances with <see cref="ArtifactSummary.IsFaulted"/>
        /// equal to <c>true</c> and <see cref="ArtifactSummary.Error"/> returning the exception.
        /// </remarks>

        public static List<ArtifactSummary> Generate(
            INavigatorStream navStream,
            Action<ArtifactSummaryPropertyBag> initProperties,
            params ArtifactSummaryHarvester[] harvesters)
        {
            var result = new List<ArtifactSummary>();

            // Factory returns null for unknown file formats
            if (navStream == null) { return result; }

            try
            {
                // Run default or specified (custom) harvesters
                if (harvesters == null || harvesters.Length == 0)
                {
                    harvesters = ConformanceHarvesters;
                }

                while (navStream.MoveNext())
                {
                    var current = navStream.Current;
                    if (current != null)
                    {
                        var properties = new ArtifactSummaryPropertyBag();

                        // Initialize default summary information
                        // Note: not exposed by IElementNavigator, cannot use harvester
                        properties.SetPosition(navStream.Position);
                        properties.SetTypeName(current.Type);
                        properties.SetResourceUri(navStream.Position);

                        // Allow caller to modify/enrich harvested properties
                        initProperties?.Invoke(properties);

                        // Generate the final (immutable) ArtifactSummary instance
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
                result.Add(ArtifactSummary.FromException(ex));
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

            // [WMR 20180419] Support empty harvester list (harvest only default props, no custom props)
            if (harvesters != null && harvesters.Length > 0)
            {
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
                                if (harvester != null && harvester.Invoke(nav, props))
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
            }

            // Create final summary from harvested properties and optional error
            return new ArtifactSummary(props, error);
        }

    }
}

#endif
