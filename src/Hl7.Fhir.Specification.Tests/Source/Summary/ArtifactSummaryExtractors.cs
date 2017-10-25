using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;

namespace Hl7.Fhir.Specification.Tests.Source.Summary
{
    /// <summary>Represents a method that extracts summary details from an artifact.</summary>
    /// <param name="nav">An <see cref="IElementNavigator"/> instance to navigate the artifact.</param>
    /// <param name="details">A collection for saving the summary details extracted from the artifact.</param>
    /// <returns>
    /// Returns <c>true </c> to indicate that all relevant details have been extracted from the artifact and the extraction process can finish.
    /// Returns <c>false</c> to try and continue extracting additional summary details.
    /// </returns>
    /// <remarks>
    /// The specified <see cref="IElementNavigator"/> is positioned on the first child element level (e.g. <c>StructureDefinition.url</c>).
    /// The target method can extract summary details starting from the current position in a forward direction.
    /// When finished, the navigator should again be positioned an the first level,
    /// for other extractors to continue reading.
    /// </remarks>
    public delegate bool ArtifactSummaryDetailsExtractor(IElementNavigator nav, ArtifactSummaryDetailsCollection details);
    // Func<IElementNavigator nav, ArtifactSummaryDetails details, bool>

    /// <summary>For accessing common artifact summary details.</summary>
    public static class ArtifactSummaryDetails
    {
        /// <summary>Collection key for the <see cref="Origin"/> property value.</summary>
        public const string OriginKey = "Origin";
        /// <summary>Collection key for the <see cref="Position"/> property value.</summary>
        public const string PositionKey = "Position";
        /// <summary>Collection key for the <see cref="ResourceUri"/> property value.</summary>
        public const string ResourceUriKey = "ResourceUri";
        /// <summary>Collection key for the <see cref="ResourceType"/> property value.</summary>
        public const string ResourceTypeKey = "ResourceType";

        public static string Origin(this IArtifactSummaryDetailsProvider details) => details[OriginKey] as string;
        public static string Position(this IArtifactSummaryDetailsProvider details) => details[PositionKey] as string;
        public static string ResourceUri(this IArtifactSummaryDetailsProvider details) => details[ResourceUriKey] as string;
        public static string ResourceType(this IArtifactSummaryDetailsProvider details) => details[ResourceTypeKey] as string;
    }

    /// <summary>For extracting summary details from a NamingSystem resource.</summary>
    public static class NamingSystemSummaryDetails
    {
        static readonly string NamingSystemTypeName = ResourceType.NamingSystem.GetLiteral();

        public static readonly string UniqueIdKey = "UniqueId";

        public static string[] UniqueId(this IArtifactSummaryDetailsProvider details) => details[UniqueIdKey] as string[];

        /// <summary>Extract summary details from a NamingSystem resource.</summary>
        /// <returns><c>true</c> if the current target is a NamingSystem resource, or <c>false</c> otherwise.</returns>
        public static bool Extract(IElementNavigator nav, ArtifactSummaryDetailsCollection details)
        {
            var typeName = details.ResourceType();
            if (typeName == NamingSystemTypeName)
            {
                nav.TryExtractValues(details, UniqueIdKey, "uniqueId", "value");
                return true;
            }
            return false;
        }
    }

    /// <summary>For extracting summary details from a conformance resource.</summary>
    public static class ConformanceSummaryDetails
    {
        public static readonly string CanonicalKey = "Canonical";
        public static readonly string NameKey = "Name";
        public static readonly string StatusKey = "Status";

        public static string Canonical(this IArtifactSummaryDetailsProvider details) => details[CanonicalKey] as string;
        public static string Name(this IArtifactSummaryDetailsProvider details) => details[NameKey] as string;
        public static string Status(this IArtifactSummaryDetailsProvider details) => details[StatusKey] as string;

        /// <summary>Extract summary details from a Conformance Resource.</summary>
        /// <returns><c>true</c> if the current target is a conformance resource, or <c>false</c> otherwise.</returns>
        public static bool Extract(IElementNavigator nav, ArtifactSummaryDetailsCollection details)
        {
            var typeName = details.ResourceType();
            if (ModelInfo.IsConformanceResource(typeName))
            {
                nav.TryExtractValue(details, CanonicalKey, "url");
                nav.TryExtractValue(details, NameKey, "name");
                nav.TryExtractValue(details, StatusKey, "status");
                return true;
            }
            return false;
        }
    }

    /// <summary>For extracting summary details from a ValueSet resource.</summary>
    public static class ValueSetSummaryDetails
    {
        static readonly string ValueSetTypeName = ResourceType.ValueSet.GetLiteral();

        public static readonly string ValueSetSystemKey = "ValueSetSystem";

        public static string ValueSetSystem(this IArtifactSummaryDetailsProvider details) => details[ValueSetSystemKey] as string;

        /// <summary>Extract summary details from a ValueSet resource.</summary>
        /// <returns><c>true</c> if the current target is a ValueSet, or <c>false</c> otherwise.</returns>
        public static bool Extract(IElementNavigator nav, ArtifactSummaryDetailsCollection details)
        {
            var typeName = details.ResourceType();
            if (typeName == ValueSetTypeName)
            {
                // Extractor chaining
                if (ConformanceSummaryDetails.Extract(nav, details))
                {
                    nav.TryExtractValue(details, ValueSetSystemKey, "codeSystem", "system");
                }
                return true;
            }
            return false;
        }
    }

    /// <summary>For extracting summary details from a ConceptMap resource.</summary>
    public static class ConceptMapSummaryDetails
    {
        static readonly string ConcentMapTypeName = ResourceType.ConceptMap.GetLiteral();

        public static readonly string ConceptMapSourceKey = "ConceptMapSource";
        public static readonly string ConceptMapTargetKey = "ConceptMapTarget";

        public static string ConceptMapSource(this IArtifactSummaryDetailsProvider details) => details[ConceptMapSourceKey] as string;

        public static string ConceptMapTarget(this IArtifactSummaryDetailsProvider details) => details[ConceptMapTargetKey] as string;

        /// <summary>Extract summary details from a ConceptMap resource.</summary>
        /// <returns><c>true</c> if the current target is a ConceptMap, or <c>false</c> otherwise.</returns>
        public static bool Extract(IElementNavigator nav, ArtifactSummaryDetailsCollection details)
        {
            var typeName = details.ResourceType();
            if (typeName == ConcentMapTypeName)
            {
                // Extractor chaining
                if (ConformanceSummaryDetails.Extract(nav, details))
                {
                    if (!nav.TryExtractValue(details, ConceptMapSourceKey, "sourceUri"))
                    {
                        nav.TryExtractValue(details, ConceptMapSourceKey, "sourceReference", "reference");
                    }

                    if (!nav.TryExtractValue(details, ConceptMapTargetKey, "targetUri"))
                    {
                        nav.TryExtractValue(details, ConceptMapTargetKey, "targetReference", "reference");
                    }
                }
                return true;
            }
            return false;
        }
    }

    /// <summary>For extracting summary details from a StructureDefinition resource.</summary>
    public static class StructureDefinitionSummaryDetails
    {
        static readonly string StructureDefinitionTypeName = ResourceType.StructureDefinition.GetLiteral();

        public static readonly string KindKey = "Kind";
        public static readonly string ConstrainedTypeKey = "ConstrainedType";
        public static readonly string ContextTypeKey = "ContextType";
        public static readonly string BaseKey = "Base";

        public static string Kind(this IArtifactSummaryDetailsProvider details) => details[KindKey] as string;
        public static string ConstrainedType(this IArtifactSummaryDetailsProvider details) => details[ConstrainedTypeKey] as string;
        public static string ContextType(this IArtifactSummaryDetailsProvider details) => details[ContextTypeKey] as string;
        public static string Base(this IArtifactSummaryDetailsProvider details) => details[BaseKey] as string;

        /// <summary>Extract summary details from a StructureDefinition resource.</summary>
        /// <returns><c>true</c> if the current target is a StructureDefinition, or <c>false</c> otherwise.</returns>
        public static bool Extract(IElementNavigator nav, ArtifactSummaryDetailsCollection details)
        {
            var typeName = details.ResourceType();
            if (typeName == StructureDefinitionTypeName)
            {
                // Extractor chaining
                if (ConformanceSummaryDetails.Extract(nav, details))
                {
                    nav.TryExtractValue(details, KindKey, "kind");
                    nav.TryExtractValue(details, ConstrainedTypeKey, "constrainedType");
                    nav.TryExtractValue(details, ContextTypeKey, "contextType");
                    nav.TryExtractValue(details, BaseKey, "base");
                }
                return true;
            }
            return false;
        }

    }

}
