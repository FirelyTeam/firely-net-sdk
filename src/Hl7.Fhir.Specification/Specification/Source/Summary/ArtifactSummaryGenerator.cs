/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Source;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Expose low-level interfaces from a separate child namespace, to prevent pollution
namespace Hl7.Fhir.Specification.Summary
{
    /// <summary>Represents a method that tries to harvest specific summary information from an artifact.</summary>
    /// <param name="nav">An <see cref="ISourceNode"/> instance to navigate the artifact.</param>
    /// <param name="properties">A dictionary for storing the harvested summary information.</param>
    /// <returns>
    /// Returns <c>true </c> to indicate that all relevant properties have been harvested from the artifact and the summary is ready to be generated.
    /// Returns <c>false</c> to try and continue harvesting additional summary information.
    /// </returns>
    /// <remarks>
    /// The specified <see cref="ISourceNode"/> is positioned on the first child element level (e.g. <c>StructureDefinition.url</c>).
    /// The target method can fetch summary information starting from the current position in a forward direction.
    /// When finished, the navigator should again be positioned on the first nesting level, so any remaining
    /// delegates can continue harvesting additional information from there.
    /// </remarks>
    public delegate bool ArtifactSummaryHarvester(ISourceNode nav, ArtifactSummaryPropertyBag properties);

    /// <summary>
    /// For generating artifact summary information from a file path or <see cref="INavigatorStream"/>,
    /// independent of the underlying resource serialization format.
    /// </summary>
    public class ArtifactSummaryGenerator
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

        /// <summary>Singleton. Returns a global default instance.</summary>
        public static ArtifactSummaryGenerator Default { get; } = new ArtifactSummaryGenerator();

        /// <summary>Default constructor. Creates a new instance of the <see cref="ArtifactSummaryGenerator"/>.</summary>
        public ArtifactSummaryGenerator() { }

        /// <summary>Constructor. Creates a new instance of the <see cref="ArtifactSummaryGenerator"/>.</summary>
        /// <param name="excludeSummariesForUnknownArtifacts">
        /// If <c>true</c>, then the generator will ignore non-parseable (invalid or non-FHIR)
        /// content files and exclude the summary from the result.
        /// </param>
        public ArtifactSummaryGenerator(bool excludeSummariesForUnknownArtifacts)
        {
            ExcludeSummariesForUnknownArtifacts = excludeSummariesForUnknownArtifacts;
        }

        /// <summary>
        /// Determines wether the <see cref="ArtifactSummaryGenerator"/> should exclude
        /// artifact summaries for non-parseable (invalid or non-FHIR) content files.
        /// <para>
        /// By default (<c>false</c>), the generator will harvest summaries from all files
        /// that exist in the specified content directory and match the specified mask,
        /// including files that cannot be parsed (e.g. invalid or non-FHIR content).
        /// </para>
        /// <para>
        /// If <c>true</c>, then the generator will only harvest summaries from valid
        /// FHIR artifacts that exist in the specified content directory and match the
        /// specified mask. Unparseable files are ignored and excluded from the result.
        /// </para>
        /// </summary>
        public bool ExcludeSummariesForUnknownArtifacts { get; set; } // = false;

        /// <summary>
        /// Generate a list of artifact summary information for a resource file on disk,
        /// using the default <see cref="ConformanceHarvesters"/>.
        /// <para>
        /// If the target resource represents a <see cref="Bundle"/> instance, then the generator
        /// returns a list of summaries for all resource entries contained in the bundle.
        /// </para>
        /// </summary>
        /// <param name="filePath">The file path of the target artifact (or the containing Bundle).</param>
        /// <returns>A list of new <see cref="ArtifactSummary"/> instances.</returns>
        /// <remarks>
        /// The generator catches all runtime exceptions that occur during harvesting and returns
        /// them as <see cref="ArtifactSummary"/> instances with <see cref="ArtifactSummary.IsFaulted"/>
        /// equal to <c>true</c> and <see cref="ArtifactSummary.Error"/> returning the exception.
        /// </remarks>
        public List<ArtifactSummary> Generate(string filePath)
        {
            return Generate(filePath, ConformanceHarvesters);
        }

        /// <summary>
        /// Generate a list of artifact summary information for a resource file on disk,
        /// using the specified list of <see cref="ArtifactSummaryHarvester"/> instances.
        /// <para>
        /// If the target resource represents a <see cref="Bundle"/> instance, then the generator
        /// returns a list of summaries for all resource entries contained in the bundle.
        /// </para>
        /// </summary>
        /// <param name="filePath">The file path of the target artifact (or the containing Bundle).</param>
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
        public List<ArtifactSummary> Generate(
            string filePath,
            params ArtifactSummaryHarvester[] harvesters)
        {
            List<ArtifactSummary> result = null;

            // Try to create navigator stream factory
            // May fail if the specified input is invalid => return error summary
            INavigatorStream navStream = null;
            try
            {
                // Get some source file properties
                var fi = new FileInfo(filePath);

                // Local helper method to initialize origin-specific summary properties
                // All resources from common origin share common property values
                void InitializeSummaryFromOrigin(ArtifactSummaryPropertyBag properties)
                {
                    properties.SetOrigin(filePath);
                    properties.SetFileSize(fi.Length);
                    // implicit conversion to DateTimeOffet. This is allowed, because LastWriteTimeUtc is of DateTimeKind.Utc
                    properties.SetLastModified(fi.LastWriteTimeUtc);
                    switch (fi.Extension)
                    {
                        case FhirFileFormats.XmlFileExtension:
                            properties.SetSerializationFormat(FhirSerializationFormats.Xml);
                            break;
                        case FhirFileFormats.JsonFileExtension:
                            properties.SetSerializationFormat(FhirSerializationFormats.Json);
                            break;
                    }
                }

                // Factory returns null for unknown file formats
                navStream = DefaultNavigatorStreamFactory.Create(filePath);
                result = Generate(navStream, InitializeSummaryFromOrigin, harvesters);
            }
            catch (Exception ex)
            {
                result = new List<ArtifactSummary>
                {
                    ArtifactSummary.FromException(ex, filePath)
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
        /// <returns>A list of new <see cref="ArtifactSummary"/> instances.</returns>
        /// <remarks>
        /// The generator catches all runtime exceptions that occur during harvesting and returns
        /// them as <see cref="ArtifactSummary"/> instances with <see cref="ArtifactSummary.IsFaulted"/>
        /// equal to <c>true</c> and <see cref="ArtifactSummary.Error"/> returning the exception.
        /// </remarks>
        public List<ArtifactSummary> Generate(INavigatorStream navStream)
        {
            return Generate(navStream, null, ConformanceHarvesters);
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
        /// <param name="customPropertyInitializer">
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
        public List<ArtifactSummary> Generate(
            INavigatorStream navStream,
            Action<ArtifactSummaryPropertyBag> customPropertyInitializer)
        {
            return Generate(navStream, customPropertyInitializer, ConformanceHarvesters);
        }

        /// <summary>
        /// Generate a list of artifact summary information from a resource input stream,
        /// using the specified list of <see cref="ArtifactSummaryHarvester"/> instances.
        /// <para>
        /// If the target resource represents a <see cref="Bundle"/> instance, then the generator
        /// returns a list of summaries for all resource entries contained in the bundle.
        /// </para>
        /// </summary>
        /// <param name="navStream">An <see cref="INavigatorStream"/> instance.</param>
        /// <param name="customPropertyInitializer">
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
        public List<ArtifactSummary> Generate(
            INavigatorStream navStream,
            Action<ArtifactSummaryPropertyBag> customPropertyInitializer,
            params ArtifactSummaryHarvester[] harvesters)
        {
            var result = new List<ArtifactSummary>();

            // Factory returns null for unknown file formats
            if (navStream != null)
            {
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
                            properties.SetTypeName(current.GetResourceTypeIndicator());
                            properties.SetResourceUri(navStream.Position);

                            // Allow caller to modify/enrich harvested properties
                            customPropertyInitializer?.Invoke(properties);

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
            }

            // [WMR 20180813] Initialize a basic summary for invalid/non-FHIR artifacts
            if (result.Count == 0 && !ExcludeSummariesForUnknownArtifacts && customPropertyInitializer != null)
            {
                var properties = new ArtifactSummaryPropertyBag();

                // Initialize default summary information
                customPropertyInitializer.Invoke(properties);

                // Generate the final (immutable) ArtifactSummary instance
                var summary = new ArtifactSummary(properties);
                result.Add(summary);
            }

            return result;
        }

        // Generate summary for a single artifact
        static ArtifactSummary generate(
            ArtifactSummaryPropertyBag props,
            ISourceNode nav,
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
                    if (nav.Children().Any())
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