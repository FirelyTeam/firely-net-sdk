using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;

namespace Hl7.Fhir.Specification.Source.Summary
{
    // STU3 Specific

    /// <summary>For accessing common artifact summary details.</summary>
    /// <remarks>Common details are initialized by the <see cref="ArtifactSummaryGenerator"/> itself.</remarks>
    public static class ArtifactSummaryDetails
    {
        public const string OriginKey = "Origin";
        public const string PositionKey = "Position";
        public const string ResourceUriKey = "Resource.Uri";
        public const string ResourceTypeNameKey = "Resource.TypeName";

        /// <summary>Returns the original location of the artifact (bundle).</summary>
        public static string GetOrigin(this IArtifactSummaryDetailsProvider details) => details[OriginKey] as string;

        /// <summary>Returns an opaque value that represents the position of the artifact within the container.</summary>
        /// <remarks>Allows the <see cref="DirectorySource"/> to retrieve and deserialize the associated artifact.</remarks>
        public static string GetPosition(this IArtifactSummaryDetailsProvider details) => details[PositionKey] as string;

        /// <summary>Returns the resource uri.</summary>
        /// <remarks>The <see cref="IElementNavigator"/> returns a generated value for resources that are not bundle entries.</remarks>
        public static string GetResourceUri(this IArtifactSummaryDetailsProvider details) => details[ResourceUriKey] as string;

        /// <summary>Returns the resource type name.</summary>
        public static string GetResourceTypeName(this IArtifactSummaryDetailsProvider details) => details[ResourceTypeNameKey] as string;
    }

    /// <summary>For extracting summary details from a <see cref="NamingSystem"/> resource.</summary>
    public static class NamingSystemSummaryDetails
    {
        static readonly string NamingSystemTypeName = ResourceType.NamingSystem.GetLiteral();

        public static readonly string UniqueIdKey = "NamingSystem.UniqueId";

        /// <summary>Returns the value extracted from the <see cref="NamingSystem.UniqueId"/> element.</summary>
        public static string[] GetNamingSystemUniqueId(this IArtifactSummaryDetailsProvider details) => details[UniqueIdKey] as string[];

        public static bool HasNamingSystemUniqueId(this IArtifactSummaryDetailsProvider details, string uniqueId)
        {
            if (uniqueId != null)
            {
                var ids = GetNamingSystemUniqueId(details);
                // return ids != null ? ids.Contains(id) : false;
                return ids != null && Array.IndexOf(ids, uniqueId) > -1;
            }
            return false;
        }

        /// <summary>Extract summary details from a <see cref="NamingSystem"/> resource.</summary>
        /// <returns><c>true</c> if the current target is a NamingSystem resource, or <c>false</c> otherwise.</returns>
        /// <remarks>The <see cref="ArtifactSummaryGenerator"/> calls this method from a <see cref="ArtifactSummaryDetailsExtractor"/> delegate.</remarks>
        internal static bool Extract(IElementNavigator nav, ArtifactSummaryDetailsCollection details)
        {
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

        /// <summary>Returns the value extracted from the <see cref="IConformanceResource.Url"/> element.</summary>
        public static string GetConformanceCanonicalUrl(this IArtifactSummaryDetailsProvider details) => details[ConformanceCanonicalUrlKey] as string;

        /// <summary>Returns the value extracted from the <see cref="IConformanceResource.Name"/> element.</summary>
        public static string GetConformanceName(this IArtifactSummaryDetailsProvider details) => details[ConformanceNameKey] as string;

        /// <summary>Returns the value extracted from the <see cref="IConformanceResource.Status"/> element.</summary>
        public static string GetConformanceStatus(this IArtifactSummaryDetailsProvider details) => details[ConformanceStatusKey] as string;

        /// <summary>Extract summary details from a Conformance Resource.</summary>
        /// <returns><c>true</c> if the current target is a conformance resource, or <c>false</c> otherwise.</returns>
        /// <remarks>
        /// The <see cref="ArtifactSummaryGenerator"/> calls this method from a <see cref="ArtifactSummaryDetailsExtractor"/> delegate.
        /// Also called directly by other ArtifactSummaryDetailsExtractor handlers for specific conformance
        /// resources, before extracting additional summary details.
        /// </remarks>
        /// <seealso cref="StructureDefinitionSummaryDetails"/>
        /// <seealso cref="CodeSystemSummaryDetails"/>
        /// <seealso cref="ConceptMapSummaryDetails"/>
        internal static bool Extract(IElementNavigator nav, ArtifactSummaryDetailsCollection details)
        {
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

    /// <summary>For extracting summary details from a <see cref="StructureDefinition"/> resource.</summary>
    public static class StructureDefinitionSummaryDetails
    {
        static readonly string StructureDefinitionTypeName = ResourceType.StructureDefinition.GetLiteral();

        public static readonly string StructureDefinitionFhirVersionKey = "StructureDefinition.FhirVersion";
        public static readonly string StructureDefinitionKindKey = "StructureDefinition.Kind";
        public static readonly string StructureDefinitionTypeKey = "StructureDefinition.Type";
        public static readonly string StructureDefinitionContextTypeKey = "StructureDefinition.ContextType";
        public static readonly string StructureDefinitionBaseDefinitionKey = "StructureDefinition.BaseDefinition";
        public static readonly string StructureDefinitionDerivationKey = "StructureDefinition.Derivation";

        /// <summary>Returns the value extracted from the <see cref="StructureDefinition.FhirVersion"/> element.</summary>
        public static string GetStructureDefinitionFhirVersion(this IArtifactSummaryDetailsProvider details) => details[StructureDefinitionFhirVersionKey] as string;

        /// <summary>Returns the value extracted from the <see cref="StructureDefinition.Kind"/> element.</summary>
        public static string GetStructureDefinitionKind(this IArtifactSummaryDetailsProvider details) => details[StructureDefinitionKindKey] as string;

        /// <summary>Returns the value extracted from the <see cref="StructureDefinition.ConstrainedType"/> element.</summary>
        public static string GetStructureDefinitionType(this IArtifactSummaryDetailsProvider details) => details[StructureDefinitionTypeKey] as string;

        /// <summary>Returns the value extracted from the <see cref="StructureDefinition.ContextType"/> element.</summary>
        public static string GetStructureDefinitionContextType(this IArtifactSummaryDetailsProvider details) => details[StructureDefinitionContextTypeKey] as string;

        /// <summary>Returns the value extracted from the <see cref="StructureDefinition.BaseDefinition"/> element.</summary>
        public static string GetStructureDefinitionBaseDefinition(this IArtifactSummaryDetailsProvider details) => details[StructureDefinitionBaseDefinitionKey] as string;

        /// <summary>Returns the value extracted from the <see cref="StructureDefinition.BaseDefinition"/> element.</summary>
        public static string GetStructureDefinitionDerivation(this IArtifactSummaryDetailsProvider details) => details[StructureDefinitionDerivationKey] as string;

        /// <summary>Extract summary details from a <see cref="StructureDefinition"/> resource.</summary>
        /// <returns><c>true</c> if the current target is a StructureDefinition, or <c>false</c> otherwise.</returns>
        /// <remarks>The <see cref="ArtifactSummaryGenerator"/> calls this method from a <see cref="ArtifactSummaryDetailsExtractor"/> delegate.</remarks>
        internal static bool Extract(IElementNavigator nav, ArtifactSummaryDetailsCollection details)
        {
            if (details.GetResourceTypeName() == StructureDefinitionTypeName)
            {
                // Explicit extractor chaining
                if (ConformanceSummaryDetails.Extract(nav, details))
                {
                    nav.TryExtractValue(details, StructureDefinitionFhirVersionKey, "fhirVersion");
                    nav.TryExtractValue(details, StructureDefinitionKindKey, "kind");
                    nav.TryExtractValue(details, StructureDefinitionContextTypeKey, "contextType");
                    nav.TryExtractValue(details, StructureDefinitionTypeKey, "type");
                    nav.TryExtractValue(details, StructureDefinitionBaseDefinitionKey, "baseDefinition");
                    nav.TryExtractValue(details, StructureDefinitionDerivationKey, "derivation");
                }
                return true;
            }
            return false;
        }

    }

    /// <summary>For extracting summary details from a <see cref="CodeSystem"/> resource.</summary>
    public static class CodeSystemSummaryDetails
    {
        static readonly string CodeSystemTypeName = ResourceType.CodeSystem.GetLiteral();

        public static readonly string CodeSystemValueSetKey = "CodeSystem.ValueSet";

        /// <summary>Returns the value extracted from the <see cref="CodeSystem.ValueSet"/> element.</summary>
        public static string GetCodeSystemValueSet(this IArtifactSummaryDetailsProvider details) => details[CodeSystemValueSetKey] as string;

        /// <summary>Extract summary details from a <see cref="CodeSystem"/> resource.</summary>
        /// <returns><c>true</c> if the current target is a ValueSet, or <c>false</c> otherwise.</returns>
        /// <remarks>The <see cref="ArtifactSummaryGenerator"/> calls this method from a <see cref="ArtifactSummaryDetailsExtractor"/> delegate.</remarks>
        internal static bool Extract(IElementNavigator nav, ArtifactSummaryDetailsCollection details)
        {
            if (details.GetResourceTypeName() == CodeSystemTypeName)
            {
                // Explicit extractor chaining
                if (ConformanceSummaryDetails.Extract(nav, details))
                {
                    nav.TryExtractValue(details, CodeSystemValueSetKey, "valueSet", "value");
                }
                return true;
            }
            return false;
        }
    }

    /// <summary>For extracting summary details from a <see cref="ConceptMap"/> resource.</summary>
    public static class ConceptMapSummaryDetails
    {
        static readonly string ConceptMapTypeName = ResourceType.ConceptMap.GetLiteral();

        public static readonly string ConceptMapSourceKey = "ConceptMap.Source";
        public static readonly string ConceptMapTargetKey = "ConceptMap.Target";

        /// <summary>Returns the value extracted from the <see cref="ConceptMap.Source"/> element.</summary>
        public static string GetConceptMapSource(this IArtifactSummaryDetailsProvider details) => details[ConceptMapSourceKey] as string;

        /// <summary>Returns the value extracted from the <see cref="ConceptMap.Target"/> element.</summary>
        public static string GetConceptMapTarget(this IArtifactSummaryDetailsProvider details) => details[ConceptMapTargetKey] as string;

        /// <summary>Extract summary details from a <see cref="ConceptMap"/> resource.</summary>
        /// <returns><c>true</c> if the current target is a ConceptMap, or <c>false</c> otherwise.</returns>
        /// <remarks>The <see cref="ArtifactSummaryGenerator"/> calls this method from a <see cref="ArtifactSummaryDetailsExtractor"/> delegate.</remarks>
        internal static bool Extract(IElementNavigator nav, ArtifactSummaryDetailsCollection details)
        {
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
