using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Linq;

namespace Hl7.Fhir.Specification.Source.Summary
{
    // DSTU2 Specific!

    /// <summary>For accessing common artifact summary details.</summary>
    /// <remarks>Common details are initialized by the <see cref="ArtifactSummaryGenerator"/> itself.</remarks>
    public static class ArtifactSummaryDetails
    {
        public const string OriginKey = "Origin";
        public const string PositionKey = "Position";
        public const string ResourceUriKey = "Resource.Uri";
        public const string ResourceTypeNameKey = "Resource.TypeName";

        public static string GetOrigin(this IArtifactSummaryDetailsProvider details) => details[OriginKey] as string;
        public static string GetPosition(this IArtifactSummaryDetailsProvider details) => details[PositionKey] as string;
        public static string GetResourceUri(this IArtifactSummaryDetailsProvider details) => details[ResourceUriKey] as string;
        public static string GetResourceTypeName(this IArtifactSummaryDetailsProvider details) => details[ResourceTypeNameKey] as string;
    }

    /// <summary>For extracting summary details from a NamingSystem resource.</summary>
    public static class NamingSystemSummaryDetails
    {
        static readonly string NamingSystemTypeName = ResourceType.NamingSystem.GetLiteral();

        public static readonly string UniqueIdKey = "NamingSystem.UniqueId";

        public static string[] GetNamingSystemUniqueId(this IArtifactSummaryDetailsProvider details) => details[UniqueIdKey] as string[];

        public static bool HasNamingSystemUniqueId(this IArtifactSummaryDetailsProvider details, string uniqueId)
        {
            if (uniqueId != null)
            {
                var ids = GetNamingSystemUniqueId(details);
                // return ids != null ? ids.Contains(id) : false;
                if (ids != null)
                {
                    for (int i = 0; i < ids.Length; i++)
                    {
                        if (ids[i] == uniqueId) { return true; }
                    }
                }
            }
            return false;
        }

        /// <summary>Extract summary details from a NamingSystem resource.</summary>
        /// <returns><c>true</c> if the current target is a NamingSystem resource, or <c>false</c> otherwise.</returns>
        /// <remarks>The <see cref="ArtifactSummaryGenerator"/> calls this method from a <see cref="ArtifactSummaryDetailsExtractor"/> delegate.</remarks>
        internal static bool Extract(IElementNavigator nav, ArtifactSummaryDetailsCollection details)
        {
            // if (details.IsNamingSystem())
            if (details.GetResourceTypeName() == NamingSystemTypeName)
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
        public static readonly string ConformanceCanonicalUrlKey = "Conformance.CanonicalUrl";
        public static readonly string ConformanceNameKey = "Conformance.Name";
        public static readonly string ConformanceStatusKey = "Conformance.Status";

        public static string GetConformanceCanonicalUrl(this IArtifactSummaryDetailsProvider details) => details[ConformanceCanonicalUrlKey] as string;
        public static string GetConformanceName(this IArtifactSummaryDetailsProvider details) => details[ConformanceNameKey] as string;
        public static string GetConformanceStatus(this IArtifactSummaryDetailsProvider details) => details[ConformanceStatusKey] as string;

        /// <summary>Extract summary details from a Conformance Resource.</summary>
        /// <returns><c>true</c> if the current target is a conformance resource, or <c>false</c> otherwise.</returns>
        /// <remarks>
        /// The <see cref="ArtifactSummaryGenerator"/> calls this method from a <see cref="ArtifactSummaryDetailsExtractor"/> delegate.
        /// Also called directly by other ArtifactSummaryDetailsExtractor handlers for specific conformance
        /// resources, before extracting additional summary details.
        /// </remarks>
        /// <seealso cref="StructureDefinitionSummaryDetails"/>
        /// <seealso cref="ValueSetSummaryDetails"/>
        /// <seealso cref="ConceptMapSummaryDetails"/>
        internal static bool Extract(IElementNavigator nav, ArtifactSummaryDetailsCollection details)
        {
            // if (details.IsConformanceResource())
            if (ModelInfo.IsConformanceResource(details.GetResourceTypeName()))
            {
                nav.TryExtractValue(details, ConformanceCanonicalUrlKey, "url");
                nav.TryExtractValue(details, ConformanceNameKey, "name");
                nav.TryExtractValue(details, ConformanceStatusKey, "status");
                return true;
            }
            return false;
        }
    }

    /// <summary>For extracting summary details from a StructureDefinition resource.</summary>
    public static class StructureDefinitionSummaryDetails
    {
        static readonly string StructureDefinitionTypeName = ResourceType.StructureDefinition.GetLiteral();

        public static readonly string StructureDefinitionKindKey = "StructureDefinition.Kind";
        public static readonly string StructureDefinitionConstrainedTypeKey = "StructureDefinition.ConstrainedType";
        public static readonly string StructureDefinitionContextTypeKey = "StructureDefinition.ContextType";
        public static readonly string StructureDefinitionBaseKey = "StructureDefinition.Base";

        public static string GetStructureDefinitionKind(this IArtifactSummaryDetailsProvider details) => details[StructureDefinitionKindKey] as string;
        public static string GetStructureDefinitionConstrainedType(this IArtifactSummaryDetailsProvider details) => details[StructureDefinitionConstrainedTypeKey] as string;
        public static string GetStructureDefinitionContextType(this IArtifactSummaryDetailsProvider details) => details[StructureDefinitionContextTypeKey] as string;
        public static string GetStructureDefinitionBase(this IArtifactSummaryDetailsProvider details) => details[StructureDefinitionBaseKey] as string;

        /// <summary>Extract summary details from a StructureDefinition resource.</summary>
        /// <returns><c>true</c> if the current target is a StructureDefinition, or <c>false</c> otherwise.</returns>
        /// <remarks>The <see cref="ArtifactSummaryGenerator"/> calls this method from a <see cref="ArtifactSummaryDetailsExtractor"/> delegate.</remarks>
        internal static bool Extract(IElementNavigator nav, ArtifactSummaryDetailsCollection details)
        {
            // if (details.IsStructureDefinition())
            if (details.GetResourceTypeName() == StructureDefinitionTypeName)
            {
                // Explicit extractor chaining
                if (ConformanceSummaryDetails.Extract(nav, details))
                {
                    nav.TryExtractValue(details, StructureDefinitionKindKey, "kind");
                    nav.TryExtractValue(details, StructureDefinitionConstrainedTypeKey, "constrainedType");
                    nav.TryExtractValue(details, StructureDefinitionContextTypeKey, "contextType");
                    nav.TryExtractValue(details, StructureDefinitionBaseKey, "base");
                }
                return true;
            }
            return false;
        }

    }

    /// <summary>For extracting summary details from a ValueSet resource.</summary>
    public static class ValueSetSummaryDetails
    {
        static readonly string ValueSetTypeName = ResourceType.ValueSet.GetLiteral();

        public static readonly string ValueSetSystemKey = "ValueSet.System";

        public static string GetValueSetSystem(this IArtifactSummaryDetailsProvider details) => details[ValueSetSystemKey] as string;

        /// <summary>Extract summary details from a ValueSet resource.</summary>
        /// <returns><c>true</c> if the current target is a ValueSet, or <c>false</c> otherwise.</returns>
        /// <remarks>The <see cref="ArtifactSummaryGenerator"/> calls this method from a <see cref="ArtifactSummaryDetailsExtractor"/> delegate.</remarks>
        internal static bool Extract(IElementNavigator nav, ArtifactSummaryDetailsCollection details)
        {
            // if (details.IsValueSet())
            if (details.GetResourceTypeName() == ValueSetTypeName)
            {
                // Explicit extractor chaining
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
        static readonly string ConceptMapTypeName = ResourceType.ConceptMap.GetLiteral();

        public static readonly string ConceptMapSourceKey = "ConceptMap.Source";
        public static readonly string ConceptMapTargetKey = "ConceptMap.Target";

        public static string GetConceptMapSource(this IArtifactSummaryDetailsProvider details) => details[ConceptMapSourceKey] as string;
        public static string GetConceptMapTarget(this IArtifactSummaryDetailsProvider details) => details[ConceptMapTargetKey] as string;

        /// <summary>Extract summary details from a ConceptMap resource.</summary>
        /// <returns><c>true</c> if the current target is a ConceptMap, or <c>false</c> otherwise.</returns>
        /// <remarks>The <see cref="ArtifactSummaryGenerator"/> calls this method from a <see cref="ArtifactSummaryDetailsExtractor"/> delegate.</remarks>
        internal static bool Extract(IElementNavigator nav, ArtifactSummaryDetailsCollection details)
        {
            // if (details.IsConceptMap())
            if (details.GetResourceTypeName() == ConceptMapTypeName)
            {
                // Explicit extractor chaining
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

}
