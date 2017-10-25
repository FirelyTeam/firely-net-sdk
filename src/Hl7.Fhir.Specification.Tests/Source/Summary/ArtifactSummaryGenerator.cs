using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Tests.Source.Summary
{
    public delegate ArtifactSummary ArtifactSummaryFactory(string typeName, ArtifactSummaryPropertyBag properties);

    public static class ArtifactSummaryGenerator
    {
        public static List<ArtifactSummary> GenerateAll(
            string origin, 
            NavigatorStreamFactory streamFactory,
            ArtifactSummaryFactory summaryFactory = null, 
            params ArtifactSummaryExtractor[] extractors)
        {
            // [WMR 20171023] In case of error, return completed summaries and error info
            var result = new List<ArtifactSummary>();

            INavigatorStream navStream = null;
            try
            {
                navStream = streamFactory(origin);
                while (navStream.MoveNext())
                {
                    var current = navStream.Current;
                    if (current != null)
                    {
                        var props = new ArtifactSummaryPropertyBag();

                        // Add default properties
                        props[ArtifactSummary.OriginKey] = origin;
                        props[ArtifactSummary.PositionKey] = navStream.Position;
                        props[ArtifactSummary.ResourceUriKey] = navStream.Position;
                        props[ArtifactSummary.ResourceTypeKey] = current.Type;

                        var summary = generate(props, current, summaryFactory, extractors);

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
            ArtifactSummaryPropertyBag props,
            IElementNavigator nav, 
            ArtifactSummaryFactory summaryFactory = null, 
            params ArtifactSummaryExtractor[] extractors)
        {
            try
            {
                // Extract custom properties via specified extractors (if any)
                // Top-level extractors receive navigator positioned on the first child element level
                if (nav.MoveToFirstChild())
                {
                    // TODO: catch individual exceptions inside loop, return as AggregateException
                    // var errors = new List<Exception>();
                    foreach (var extractor in DefaultArtifactSummaryExtractors.Concat(extractors))
                    {
                        extractor?.Invoke(nav, props);
                        // try { extractor?.Invoke(nav, props); }
                        // catch (Exception ex) { errors.Add(ex); }
                    }
                    // TODO: Assign AggregateException(errors) through / after factory
                }

                // Generate summary
                var result =
                    summaryFactory?.Invoke(nav.Type, props)
                    ?? DefaultArtifactSummaryFactory(nav.Type, props);

                return result;
            }
            catch (Exception ex)
            {
                return ArtifactSummary.FromException(props, ex);
            }
        }

        // Default ArtifactSummaryFactory, always returns a new ArtifactSummary instance
        // Custom ArtifactSummaryFactory implementations can return specialized ArtifactSummary subclasses, depending on the type name
        static ArtifactSummary DefaultArtifactSummaryFactory(string typeName, ArtifactSummaryPropertyBag properties)
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
