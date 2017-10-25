using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Serialization;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Tests.Source.Summary
{
    /// <summary>Represents a factory method that creates a new <see cref="ArtifactSummary"/> instance.</summary>
    /// <param name="typeName">The type name of the artifact.</param>
    /// <param name="details">A collection of summary details extracted from the artifact.</param>
    /// <param name="error">A error that occured while extracting details from the artifact, or <c>null</c>.</param>
    /// <returns>A new <see cref="ArtifactSummary"/> instance.</returns>
    public delegate ArtifactSummary ArtifactSummaryFactory(string typeName, ArtifactSummaryDetailsCollection details, Exception error = null);

    /// <summary>
    /// For generating artifact summary information from an <see cref="INavigatorStream"/>,
    /// independent of the underlying resource serialization format.
    /// </summary>
    public static class ArtifactSummaryGenerator
    {
        /// <summary>Generate a list of summary information from a <see cref="INavigatorStream"/> instance.</summary>
        /// <param name="origin">The original location of the underlying resource (bundle).</param>
        /// <param name="streamFactory">
        /// An optional <see cref="NavigatorStreamFactory"/> delegate for creating the
        /// <see cref="INavigatorStream"/> instance to navigate the underlying resource (container).
        /// By default, the generator calls <see cref="DefaultNavigatorStreamFactory.Create(string)"/>,
        /// which supports navigation for "*.xml" and "*.json" files.
        /// </param>
        /// <param name="summaryFactory">
        /// By default, the generator returns a list of <see cref="ArtifactSummary"/> instances.
        /// Alternatively, you can specify a custom <see cref="ArtifactSummaryFactory"/> delegate
        /// to create custom return values, depending on the extracted summary details.
        /// This allows you to generate various specialized subclasses with additional strongly typed properties.
        /// </param>
        /// <param name="extractors">
        /// One or more optional (custom) <see cref="ArtifactSummaryDetailsExtractor"/> delegates to
        /// extract (custom) summary details from an artifact. For each artifact, the generator first
        /// extracts the default summary details and then calls any custom extractor delegates in the
        /// specified order. If a delegate returns <c>true</c> to signal extraction has finished, the
        /// generator will not call any of the remaining delegates and immediately proceed to create
        /// the <see cref="ArtifactSummary"/> return value.
        /// </param>
        /// <returns>A list of new <see cref="ArtifactSummary"/> instances.</returns>
        public static List<ArtifactSummary> Generate(
            string origin, 
            NavigatorStreamFactory streamFactory = null,
            ArtifactSummaryFactory summaryFactory = null, 
            params ArtifactSummaryDetailsExtractor[] extractors)
        {
            // [WMR 20171023] In case of error, return completed summaries and error info
            var result = new List<ArtifactSummary>();

            INavigatorStream navStream = null;
            try
            {
                // Call custom or default navigator factory
                navStream = streamFactory?.Invoke(origin)
                    ?? DefaultNavigatorStreamFactory.Create(origin);

                // Use custom or default summary factory
                if (summaryFactory == null)
                {
                    summaryFactory = DefaultArtifactSummaryFactory;
                }
                // Always run default extractors first, then any custom extractors
                ArtifactSummaryDetailsExtractor[] allExtractors = DefaultArtifactSummaryExtractors;
                if (extractors.Length > 0)
                {
                    Array.Resize(ref allExtractors, DefaultArtifactSummaryExtractors.Length + extractors.Length);
                    extractors.CopyTo(allExtractors, DefaultArtifactSummaryExtractors.Length);
                }

                while (navStream.MoveNext())
                {
                    var current = navStream.Current;
                    if (current != null)
                    {
                        var props = new ArtifactSummaryDetailsCollection();

                        // Add default summary details
                        props[ArtifactSummaryDetails.OriginKey] = origin;
                        props[ArtifactSummaryDetails.PositionKey] = navStream.Position;
                        props[ArtifactSummaryDetails.ResourceUriKey] = navStream.Position;
                        props[ArtifactSummaryDetails.ResourceTypeKey] = current.Type;

                        var summary = generate(props, current, summaryFactory, allExtractors);

                        result.Add(summary);
                    }
                }
            }
            catch (Exception ex)
            {
                result.Add(ArtifactSummary.FromException(origin, ex));
            }
            finally
            {
                navStream?.Dispose();
            }
            return result;
        }

        // Extract summary from a single artifact
        static ArtifactSummary generate(
            ArtifactSummaryDetailsCollection props,
            IElementNavigator nav, 
            ArtifactSummaryFactory summaryFactory, 
            ArtifactSummaryDetailsExtractor[] extractors)
        {
            try
            {
                // Extract summary details via specified extractors
                // Top-level extractors receive navigator positioned on the first child element level

                // Catch individual exceptions inside loop, return as AggregateException
                var errors = new List<Exception>();
                if (nav.MoveToFirstChild())
                {
                    foreach (var extractor in extractors)
                    {
                        try
                        {
                            if (extractor?.Invoke(nav, props) == true)
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
                var error = errors.Count > 0 ? new AggregateException(errors) : null;

                // Call factory to create final summary from extracted details
                var result = summaryFactory.Invoke(nav.Type, props, error);

                return result;
            }
            catch (Exception ex)
            {
                // Error in summary factory?
                // Make sure we always return a valid summary
                return ArtifactSummary.FromException(props, ex);
            }
        }

        // Default ArtifactSummaryFactory, always returns a new ArtifactSummary instance
        // Custom ArtifactSummaryFactory implementations can return specialized ArtifactSummary subclasses, depending on the type name
        static ArtifactSummary DefaultArtifactSummaryFactory(string typeName, ArtifactSummaryDetailsCollection details, Exception error = null)
        {
            // Example:
            //
            //if (!string.IsNullOrEmpty(typeName))
            //{
            //    switch (typeName)
            //    {
            //        case NamingSystemSummaryDetails.TypeName: return new NamingSystemSummary(details, error)
            //        // ... etc ...
            //    }
            //}
            return new ArtifactSummary(details, error);
        }

        // Default extractors, executed in-order until one extractor returns true
        static readonly ArtifactSummaryDetailsExtractor[] DefaultArtifactSummaryExtractors
            = new ArtifactSummaryDetailsExtractor[]
            {
                NamingSystemSummaryDetails.Extract,
                // Specific conformance resources first
                ConceptMapSummaryDetails.Extract,
                ValueSetSummaryDetails.Extract,
                StructureDefinitionSummaryDetails.Extract,
                // Fall back for all other conformance resources
                ConformanceSummaryDetails.Extract
            };

    }
}
