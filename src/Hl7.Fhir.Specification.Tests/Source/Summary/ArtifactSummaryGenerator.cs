using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Tests.Source.Summary
{
    public delegate ArtifactSummary ArtifactSummaryFactory(string typeName, ArtifactSummaryProperties properties);

    /// <summary>
    /// For generating artifact summary information from an <see cref="INavigatorStream"/>,
    /// independent of the underlying resource serialization format.
    /// </summary>
    public static class ArtifactSummaryGenerator
    {
        /// <summary>Generate a list of summary information from a <see cref="INavigatorStream"/> instance.</summary>
        /// <param name="origin">The original location of the underlying resource (container).</param>
        /// <param name="streamFactory">
        /// An optional <see cref="NavigatorStreamFactory"/> for creating the <see cref="INavigatorStream"/>
        /// instance to navigate the underlying resource (container).
        /// By default, the generator calls <see cref="DefaultNavigatorStreamFactory.Create(string)"/>.
        /// </param>
        /// <param name="summaryFactory">
        /// An optional <see cref="ArtifactSummaryFactory"/> for creating the (custom) <see cref="ArtifactSummary"/> instances to return.
        /// </param>
        /// <param name="extractors">
        /// One ore more optional (custom) <see cref="ArtifactSummaryExtractor"/> instances to extract (custom) summary information.
        /// </param>
        /// <returns>A list of new <see cref="ArtifactSummary"/> instances.</returns>
        public static List<ArtifactSummary> Generate(
            string origin, 
            NavigatorStreamFactory streamFactory = null,
            ArtifactSummaryFactory summaryFactory = null, 
            params ArtifactSummaryExtractor[] extractors)
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
                ArtifactSummaryExtractor[] allExtractors = DefaultArtifactSummaryExtractors;
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
                        var props = new ArtifactSummaryProperties();

                        // Add default properties
                        props[ArtifactSummary.OriginKey] = origin;
                        props[ArtifactSummary.PositionKey] = navStream.Position;
                        props[ArtifactSummary.ResourceUriKey] = navStream.Position;
                        props[ArtifactSummary.ResourceTypeKey] = current.Type;

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

        static ArtifactSummary generate(
            ArtifactSummaryProperties props,
            IElementNavigator nav, 
            ArtifactSummaryFactory summaryFactory, 
            ArtifactSummaryExtractor[] extractors)
        {
            try
            {
                // Extract properties via specified extractors
                // Top-level extractors receive navigator positioned on the first child element level
                if (nav.MoveToFirstChild())
                {
                    // TODO: catch individual exceptions inside loop, return as AggregateException
                    // var errors = new List<Exception>();
                    foreach (var extractor in extractors)
                    {
                        extractor?.Invoke(nav, props);
                        // try { extractor?.Invoke(nav, props); }
                        // catch (Exception ex) { errors.Add(ex); }
                    }
                    // TODO: Assign AggregateException(errors) through / after factory
                }

                // Generate summary
                var result = summaryFactory.Invoke(nav.Type, props);

                return result;
            }
            catch (Exception ex)
            {
                return ArtifactSummary.FromException(props, ex);
            }
        }

        // Default ArtifactSummaryFactory, always returns a new ArtifactSummary instance
        // Custom ArtifactSummaryFactory implementations can return specialized ArtifactSummary subclasses, depending on the type name
        static ArtifactSummary DefaultArtifactSummaryFactory(string typeName, ArtifactSummaryProperties properties)
        {
            //if (!string.IsNullOrEmpty(typeName))
            //{
            //    switch (typeName)
            //    {
            //        case NamingSystemSummaryProperties.TypeName: return new NamingSystemSummary(properties)
            //    }
            //}
            return new ArtifactSummary(properties);
        }

        // Default extractors, executed in-order until one extractor returns true
        static readonly ArtifactSummaryExtractor[] DefaultArtifactSummaryExtractors
            = new ArtifactSummaryExtractor[]
            {
                NamingSystemSummaryProperties.Extract,
                // Specific conformance resources first
                ConceptMapSummaryProperties.Extract,
                ValueSetSummaryProperties.Extract,
                StructureDefinitionSummaryProperties.Extract,
                // Fall back for all other conformance resources
                ConformanceSummaryProperties.Extract
            };

    }
}
