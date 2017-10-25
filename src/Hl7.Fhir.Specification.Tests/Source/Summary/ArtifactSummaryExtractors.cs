using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Tests.Source.Summary
{
    public delegate bool ArtifactSummaryExtractor(IElementNavigator nav, ArtifactSummaryPropertyBag properties);
    // Func<IElementNavigator nav, ArtifactSummaryPropertyBag properties, bool>

    // Move to separate namespace in order to avoid pollution?
    public static class SummaryNavigationExtensions
    {
        public static bool Find(this IElementNavigator nav, string element)
        {
            return nav.Name == element || nav.MoveToNext(element);
        }

        public static bool TryExtractValue(this IElementNavigator nav, ArtifactSummaryPropertyBag properties, string key)
        {
            var value = nav.Value?.ToString();
            if (value != null)
            {
                properties[key] = value;
                return true;
            }
            return false;
        }

        public static bool TryExtractValue(this IElementNavigator nav, ArtifactSummaryPropertyBag properties, string key, string element)
        {
            return nav.Find(element) && nav.TryExtractValue(properties, key);
        }

        public static bool TryExtractValue(this IElementNavigator nav, ArtifactSummaryPropertyBag properties, string key, string element, string childElement)
        {
            if (nav.Find(element))
            {
                var childNav = nav.Clone();
                return childNav.MoveToFirstChild(childElement) && childNav.TryExtractValue(properties, key);
            }
            return false;
        }

        public static bool TryExtractValue(this IElementNavigator nav, List<string> values)
        {
            var value = nav.Value?.ToString();
            if (value != null)
            {
                values.Add(value);
                return true;
            }
            return false;
        }

        public static bool TryExtractValues(this IElementNavigator nav, ArtifactSummaryPropertyBag properties, string key, string element, string childElement)
        {
            if (nav.Find(element))
            {
                var values = new List<string>();
                do
                {
                    var childNav = nav.Clone();
                    if (childNav.MoveToFirstChild(childElement))
                    {
                        TryExtractValue(childNav, values);
                    }
                } while (nav.MoveToNext(element));
                if (values.Count > 0)
                {
                    properties[key] = values.ToArray();
                    return true;
                }
            }
            return false;
        }
    }

    public static class NamingSystemSummaryProperties
    {
        static readonly string NamingSystemTypeName = ResourceType.NamingSystem.GetLiteral();

        public static readonly string UniqueIdKey = "UniqueId";

        public static string[] UniqueId(this ArtifactSummaryPropertyBag properties) => properties[UniqueIdKey] as string[];

        public static bool Extract(IElementNavigator nav, ArtifactSummaryPropertyBag properties)
        {
            if (nav.Type == NamingSystemTypeName)
            {
                // Assume nav is on root
                // Better: let caller handle this; move to first child and call extractors
                if (nav.MoveToFirstChild())
                {
                    nav.TryExtractValues(properties, UniqueIdKey, "uniqueId", "value");
                }
                return true;
            }
            return false;
        }
    }

    public static class ConformanceSummaryProperties
    {
        public static readonly string CanonicalKey = "Canonical";
        public static readonly string NameKey = "Name";
        public static readonly string StatusKey = "Status";

        public static string Canonical(this ArtifactSummaryPropertyBag properties) => properties[CanonicalKey] as string;
        public static string Name(this ArtifactSummaryPropertyBag properties) => properties[NameKey] as string;
        public static string Status(this ArtifactSummaryPropertyBag properties) => properties[StatusKey] as string;

        public static bool Extract(IElementNavigator nav, ArtifactSummaryPropertyBag properties)
        {
            if (ModelInfo.IsConformanceResource(nav.Type))
            {
                // Assume nav is on root
                if (nav.MoveToFirstChild())
                {
                    nav.TryExtractValue(properties, "url", CanonicalKey);
                    nav.TryExtractValue(properties, "name", NameKey);
                    nav.TryExtractValue(properties, "status", StatusKey);
                }
                return true;
            }
            return false;
        }
    }

    public static class ValueSetSummaryProperties
    {
        static readonly string ValueSetTypeName = ResourceType.ValueSet.GetLiteral();

        public static readonly string ValueSetSystemKey = "ValueSetSystem";

        public static string ValueSetSystem(this ArtifactSummaryPropertyBag properties) => properties[ValueSetSystemKey] as string;

        public static bool Extract(IElementNavigator nav, ArtifactSummaryPropertyBag properties)
        {
            if (nav.Type == ValueSetTypeName)
            {
                // Extractor chaining
                if (ConformanceSummaryProperties.Extract(nav, properties))
                {
                    nav.TryExtractValue(properties, ValueSetSystemKey, "codeSystem", "system");
                }
                return true;
            }
            return false;
        }
    }

    public static class ConceptMapSummaryProperties
    {
        static readonly string ConcentMapTypeName = ResourceType.ConceptMap.GetLiteral();

        public static readonly string ConceptMapSourceKey = "ConceptMapSource";
        public static readonly string ConceptMapTargetKey = "ConceptMapTarget";

        public static string ConceptMapSource(this ArtifactSummaryPropertyBag properties) => properties[ConceptMapSourceKey] as string;

        public static string ConceptMapTarget(this ArtifactSummaryPropertyBag properties) => properties[ConceptMapTargetKey] as string;

        public static bool Extract(IElementNavigator nav, ArtifactSummaryPropertyBag properties)
        {
            if (nav.Type == ConcentMapTypeName)
            {
                // Extractor chaining
                if (ConformanceSummaryProperties.Extract(nav, properties))
                {
                    if (!nav.TryExtractValue(properties, ConceptMapSourceKey, "sourceUri"))
                    {
                        nav.TryExtractValue(properties, ConceptMapSourceKey, "sourceReference", "reference");
                    }

                    if (!nav.TryExtractValue(properties, ConceptMapTargetKey, "targetUri"))
                    {
                        nav.TryExtractValue(properties, ConceptMapTargetKey, "targetReference", "reference");
                    }
                }
                return true;
            }
            return false;
        }
    }

    public static class StructureDefinitionSummaryProperties
    {
        static readonly string StructureDefinitionTypeName = ResourceType.StructureDefinition.GetLiteral();

        public static readonly string KindKey = "Kind";
        public static readonly string ConstrainedTypeKey = "ConstrainedType";
        public static readonly string ContextTypeKey = "ContextType";
        public static readonly string BaseKey = "Base";

        public static string Kind(this ArtifactSummaryPropertyBag properties) => properties[KindKey] as string;
        public static string ConstrainedType(this ArtifactSummaryPropertyBag properties) => properties[ConstrainedTypeKey] as string;
        public static string ContextType(this ArtifactSummaryPropertyBag properties) => properties[ContextTypeKey] as string;
        public static string Base(this ArtifactSummaryPropertyBag properties) => properties[BaseKey] as string;

        public static bool Extract(IElementNavigator nav, ArtifactSummaryPropertyBag properties)
        {
            if (nav.Type == StructureDefinitionTypeName)
            {
                // Extractor chaining
                if (ConformanceSummaryProperties.Extract(nav, properties))
                {
                    nav.TryExtractValue(properties, KindKey, "kind");
                    nav.TryExtractValue(properties, ConstrainedTypeKey, "constrainedType");
                    nav.TryExtractValue(properties, ContextTypeKey, "contextType");
                    nav.TryExtractValue(properties, BaseKey, "base");
                }
                return true;
            }
            return false;
        }

    }

}
