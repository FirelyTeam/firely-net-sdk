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
                CodeSystemSummaryProperties.Harvest,
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
            var result = new List<ArtifactSummary>();

            // In case of error, return completed summaries and error info
            INavigatorStream navStream = null;
            try
            {
                // Call default navigator factory
                navStream = DefaultNavigatorStreamFactory.Create(origin);

                // Factory returns null for unknown file formats
                if (navStream == null) { return result; }

                // Get some source file properties
                var fi = new FileInfo(origin);

                // Resources from same origin share a common serialization format
                string format =
                    fi.Extension == FhirFileFormats.XmlFileExtension ? FhirSerializationFormats.Xml
                    : fi.Extension == FhirFileFormats.JsonFileExtension ? FhirSerializationFormats.Json
                    : null;

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
                        properties.SetSerializationFormat(format);
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

        // [WMR 20180418] NEW: Harvest summaries from a resource stream

        /// <summary>
        /// Generate a list of artifact summary information from a streamed resource,
        /// using the default <see cref="ConformanceHarvesters"/>.
        /// <para>
        /// If the target resource represents a <see cref="Bundle"/> instance, then the generator
        /// returns a list of summaries for all resource entries contained in the bundle.
        /// </para>
        /// </summary>
        /// <param name="stream">
        /// A readable stream that returns a <see cref="Hl7.Fhir.Model.Resource"/> instance.
        /// Note: the caller is responsible for closing/disposing the specified stream.
        /// </param>
        /// <param name="format">
        /// A string value that represents the FHIR resource serialization format,
        /// as defined by <see cref="FhirSerializationFormats"/>.
        /// </param>
        /// <returns>A list of new <see cref="ArtifactSummary"/> instances.</returns>
        /// <remarks>
        /// The generator catches all runtime exceptions that occur during harvesting and returns
        /// them as <see cref="ArtifactSummary"/> instances with <see cref="ArtifactSummary.IsFaulted"/>
        /// equal to <c>true</c> and <see cref="ArtifactSummary.Error"/> returning the exception.
        /// </remarks>        
        public static List<ArtifactSummary> Generate(Stream stream, string format) => Generate(stream, format, ConformanceHarvesters);

        /// <summary>
        /// Generate a list of artifact summary information from a streamed resource,
        /// using the specified list of <see cref="ArtifactSummaryHarvester"/> instances.
        /// <para>
        /// If the target resource represents a <see cref="Bundle"/> instance, then the generator
        /// returns a list of summaries for all resource entries contained in the bundle.
        /// </para>
        /// </summary>
        /// <param name="stream">
        /// A readable stream that returns <see cref="Hl7.Fhir.Model.Resource"/> instances.
        /// Note: the caller is responsible for closing/disposing the specified stream.
        /// </param>
        /// <param name="format">
        /// A string value that represents the FHIR resource serialization format,
        /// as defined by <see cref="FhirSerializationFormats"/>.
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
            Stream stream,
            string format,
            params ArtifactSummaryHarvester[] harvesters)
        {
            var result = new List<ArtifactSummary>();

            // In case of error, return completed summaries and error info
            INavigatorStream navStream = null;
            try
            {
                // Call default navigator factory
                navStream = DefaultNavigatorStreamFactory.Create(stream, format, false);

                // Factory returns null for unknown file formats
                if (navStream == null) { return result; }

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

                        // Note: Origin & LastModified properties are unavailable for input streams
                        // properties.SetOrigin(...);
                        // properties.SetLastModified(...);

                        properties.SetSerializationFormat(format);
                        if (stream.CanSeek && stream.Length > 0)
                        {
                            properties.SetFileSize(stream.Length);
                        }
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
