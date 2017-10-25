using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Tests.Source.Summary
{
    public delegate bool ArtifactSummaryExtractor(IElementNavigator nav, ArtifactSummaryProperties properties);
    // Func<IElementNavigator nav, ArtifactSummaryProperties properties, bool>

    // Move to separate namespace in order to avoid pollution?
    public static class SummaryNavigationExtensions
    {
        /// <summary>
        /// Try to position the navigator on the element with the specified name.
        /// Maintain current position if element name matches, otherwise move to next match (if it exists).
        /// </summary>
        public static bool Find(this IElementNavigator nav, string element)
        {
            return nav.Name == element || nav.MoveToNext(element);
        }

        /// <summary>Extract the value of the current element into the property bag using the specified key.</summary>
        public static bool TryExtractValue(this IElementNavigator nav, ArtifactSummaryProperties properties, string key)
        {
            var value = nav.Value?.ToString();
            if (value != null)
            {
                properties[key] = value;
                return true;
            }
            return false;
        }

        /// <summary>Extract the value of the (current or sibling) element with the specified name into the property bag using the specified key.</summary>
        public static bool TryExtractValue(this IElementNavigator nav, ArtifactSummaryProperties properties, string key, string element)
        {
            return nav.Find(element) && nav.TryExtractValue(properties, key);
        }

        /// <summary>Extract the value of a child element into the property bag using the specified key.</summary>
        public static bool TryExtractValue(this IElementNavigator nav, ArtifactSummaryProperties properties, string key, string element, string childElement)
        {
            if (nav.Find(element))
            {
                var childNav = nav.Clone();
                return childNav.MoveToFirstChild(childElement) && childNav.TryExtractValue(properties, key);
            }
            return false;
        }

        /// <summary>Add the value of the current element to the specified list, if not missing or empty.</summary>
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

        /// <summary>Extract an array of child element values into the property bag using the specified key.</summary>
        public static bool TryExtractValues(this IElementNavigator nav, ArtifactSummaryProperties properties, string key, string element, string childElement)
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

        public static string[] UniqueId(this ArtifactSummaryProperties properties) => properties[UniqueIdKey] as string[];

        public static bool Extract(IElementNavigator nav, ArtifactSummaryProperties properties)
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

        public static string Canonical(this ArtifactSummaryProperties properties) => properties[CanonicalKey] as string;
        public static string Name(this ArtifactSummaryProperties properties) => properties[NameKey] as string;
        public static string Status(this ArtifactSummaryProperties properties) => properties[StatusKey] as string;

        public static bool Extract(IElementNavigator nav, ArtifactSummaryProperties properties)
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

        public static string ValueSetSystem(this ArtifactSummaryProperties properties) => properties[ValueSetSystemKey] as string;

        public static bool Extract(IElementNavigator nav, ArtifactSummaryProperties properties)
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

        public static string ConceptMapSource(this ArtifactSummaryProperties properties) => properties[ConceptMapSourceKey] as string;

        public static string ConceptMapTarget(this ArtifactSummaryProperties properties) => properties[ConceptMapTargetKey] as string;

        public static bool Extract(IElementNavigator nav, ArtifactSummaryProperties properties)
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

        public static string Kind(this ArtifactSummaryProperties properties) => properties[KindKey] as string;
        public static string ConstrainedType(this ArtifactSummaryProperties properties) => properties[ConstrainedTypeKey] as string;
        public static string ContextType(this ArtifactSummaryProperties properties) => properties[ContextTypeKey] as string;
        public static string Base(this ArtifactSummaryProperties properties) => properties[BaseKey] as string;

        public static bool Extract(IElementNavigator nav, ArtifactSummaryProperties properties)
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
