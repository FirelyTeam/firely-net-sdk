/* 
 * Copyright (c) 2017, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;

namespace Hl7.Fhir.Specification.Source.Summary
{
    // Defines a set of default artifact summary harvester delegates,
    // with associated property keys and helper extension methods

    // DSTU2 Specific!

    /// <summary>For accessing common artifact summary properties stored in a <see cref="ArtifactSummaryPropertyBag"/>.</summary>
    /// <remarks>
    /// The <see cref="ArtifactSummaryGenerator"/> creates a <see cref="ArtifactSummaryPropertyBag"/> 
    /// for each artifact and adds a set of common summary properties, independent of the actual resource type.
    /// This class provides the property keys and extension methods to retrieve the values.
    /// </remarks>
    public static class ArtifactSummaryProperties
    {
        public const string OriginKey = "Origin";
        public const string PositionKey = "Position";
        public const string ResourceUriKey = "Resource.Uri";
        public const string ResourceTypeNameKey = "Resource.TypeName";

        /// <summary>Try to retrieve the property value for the specified key.</summary>
        /// <param name="properties">An <see cref="IArtifactSummaryPropertyBag"/> instance.</param>
        /// <param name="key">A property key.</param>
        /// <returns>An object value, or <c>null</c>.</returns>
        public static object GetValueOrDefault(this IArtifactSummaryPropertyBag properties, string key)
            => properties.TryGetValue(key, out object result) ? result : null;

        /// <summary>Try to retrieve the property value for the specified key.</summary>
        /// <param name="properties">An <see cref="IArtifactSummaryPropertyBag"/> instance.</param>
        /// <param name="key">A property key.</param>
        /// <typeparam name="T">The type of the property value.</typeparam>
        /// <returns>A value of type <typeparamref name="T"/>, or <c>null</c>.</returns>
        public static T GetValueOrDefault<T>(this IArtifactSummaryPropertyBag properties, string key)
            where T : class
            => properties.GetValueOrDefault(key) as T;

        /// <summary>Get the Origin property value from the specified collection of artifact summary properties, if available.</summary>
        public static string GetOrigin(this IArtifactSummaryPropertyBag properties) => properties.GetValueOrDefault<string>(OriginKey);

        /// <summary>Get the Position property value from the specified collection of artifact summary properties, if available.</summary>
        public static string GetPosition(this IArtifactSummaryPropertyBag properties) => properties.GetValueOrDefault<string>(PositionKey);

        /// <summary>Get the ResourceUri property value from the specified collection of artifact summary properties, if available.</summary>
        public static string GetResourceUri(this IArtifactSummaryPropertyBag properties) => properties.GetValueOrDefault<string>(ResourceUriKey);

        /// <summary>Get the ResourceTypeName property value from the specified collection of artifact summary properties, if available.</summary>
        public static string GetResourceTypeName(this IArtifactSummaryPropertyBag properties) => properties.GetValueOrDefault<string>(ResourceTypeNameKey);
    }

    /// <summary>For harvesting specific summary information from a <see cref="NamingSystem"/> resource.</summary>
    public static class NamingSystemSummaryProperties
    {
        static readonly string NamingSystemTypeName = ResourceType.NamingSystem.GetLiteral();
        public static readonly string UniqueIdKey = "NamingSystem.UniqueId";

        /// <summary>Get the <c>NamingSystem.uniqueId</c> property value from the specified collection of artifact summary properties, if available.</summary>
        /// <remarks>Only applies to summaries of <see cref="NamingSystem"/> resources.</remarks>
        public static string[] GetNamingSystemUniqueId(this IArtifactSummaryPropertyBag properties) => properties.GetValueOrDefault<string[]>(UniqueIdKey);

        /// <summary>
        /// Determines if the current summary properties represent a <see cref="NamingSystem"/>
        /// resource with the specified <c>uniqueId</c> value.
        /// </summary>
        public static bool HasNamingSystemUniqueId(this IArtifactSummaryPropertyBag properties, string uniqueId)
        {
            if (uniqueId != null)
            {
                var ids = GetNamingSystemUniqueId(properties);
                return ids != null && Array.IndexOf(ids, uniqueId) > -1;
            }
            return false;
        }

        /// <summary>Determines if the specified instance represents summary information about a <see cref="NamingSystem"/> resource.</summary>
        public static bool IsNamingSystemSummary(this IArtifactSummaryPropertyBag properties) => properties.GetResourceTypeName() == NamingSystemTypeName;

        /// <summary>Harvest specific summary information from a <see cref="NamingSystem"/> resource.</summary>
        /// <returns><c>true</c> if the current target represents a <see cref="NamingSystem"/> resource, or <c>false</c> otherwise.</returns>
        /// <remarks>The <see cref="ArtifactSummaryGenerator"/> calls this method through a <see cref="ArtifactSummaryHarvester"/> delegate.</remarks>
        internal static bool Harvest(IElementNavigator nav, ArtifactSummaryPropertyBag properties)
        {
            if (IsNamingSystemSummary(properties))
            {
                nav.HarvestValues(properties, UniqueIdKey, "uniqueId", "value");
                return true;
            }
            return false;
        }
    }

    /// <summary>For harvesting common summary information from a conformance resource.</summary>
    public static class ConformanceSummaryProperties
    {
        public static readonly string ConformanceCanonicalUrlKey = "Conformance.CanonicalUrl";
        public static readonly string ConformanceNameKey = "Conformance.Name";
        public static readonly string ConformanceStatusKey = "Conformance.Status";

        /// <summary>Get the canonical <c>url</c> property value from the specified collection of artifact summary properties, if available.</summary>
        /// <remarks>Only applies to summaries of conformance resources.</remarks>
        public static string GetConformanceCanonicalUrl(this IArtifactSummaryPropertyBag properties) => properties.GetValueOrDefault<string>(ConformanceCanonicalUrlKey);

        /// <summary>Get the <c>name</c> property value from the specified collection of artifact summary properties, if available.</summary>
        /// <remarks>Only applies to summaries of conformance resources.</remarks>
        public static string GetConformanceName(this IArtifactSummaryPropertyBag properties) => properties.GetValueOrDefault<string>(ConformanceNameKey);

        /// <summary>Get the <c>status</c> property value from the specified collection of artifact summary properties, if available.</summary>
        /// <remarks>Only applies to summaries of conformance resources.</remarks>
        public static string GetConformanceStatus(this IArtifactSummaryPropertyBag properties) => properties.GetValueOrDefault<string>(ConformanceStatusKey);

        /// <summary>Determines if the specified instance represents summary information about a conformance resource.</summary>
        public static bool IsConformanceSummary(this IArtifactSummaryPropertyBag properties) => ModelInfo.IsConformanceResource(properties.GetResourceTypeName());

        /// <summary>Harvest common summary information from a conformance resource.</summary>
        /// <returns><c>true</c> if the current target represents a conformance resource, or <c>false</c> otherwise.</returns>
        /// <remarks>
        /// The <see cref="ArtifactSummaryGenerator"/> calls this method through a <see cref="ArtifactSummaryHarvester"/> delegate.
        /// Also called directly by other <see cref="ArtifactSummaryHarvester"/> delegates to harvest summary
        /// information common to all conformance resources, before harvesting any additional type specific
        /// information.
        /// </remarks>
        /// <seealso cref="StructureDefinitionSummaryProperties"/>
        /// <seealso cref="ValueSetSummaryProperties"/>
        /// <seealso cref="ConceptMapSummaryProperties"/>
        internal static bool Harvest(IElementNavigator nav, ArtifactSummaryPropertyBag properties)
        {
            if (IsConformanceSummary(properties))
            {
                nav.HarvestValue(properties, ConformanceCanonicalUrlKey, "url");
                nav.HarvestValue(properties, ConformanceNameKey, "name");
                nav.HarvestValue(properties, ConformanceStatusKey, "status");
                return true;
            }
            return false;
        }
    }

    /// <summary>For harvesting specific summary information from a <see cref="StructureDefinition"/> resource.</summary>
    public static class StructureDefinitionSummaryProperties
    {
        static readonly string StructureDefinitionTypeName = ResourceType.StructureDefinition.GetLiteral();

        public static readonly string StructureDefinitionKindKey = "StructureDefinition.Kind";
        public static readonly string StructureDefinitionConstrainedTypeKey = "StructureDefinition.ConstrainedType";
        public static readonly string StructureDefinitionContextTypeKey = "StructureDefinition.ContextType";
        public static readonly string StructureDefinitionBaseKey = "StructureDefinition.Base";

        /// <summary>Get the <c>StructureDefinition.Kind</c> property value from the specified collection of artifact summary properties, if available.</summary>
        /// <remarks>Only applies to summaries of <see cref="StructureDefinition"/> resources.</remarks>
        public static string GetStructureDefinitionKind(this IArtifactSummaryPropertyBag properties) => properties.GetValueOrDefault<string>(StructureDefinitionKindKey);

        /// <summary>Get the <c>StructureDefinition.constrainedType</c> property value from the specified collection of artifact summary properties, if available.</summary>
        /// <remarks>Only applies to summaries of <see cref="StructureDefinition"/> resources.</remarks>
        public static string GetStructureDefinitionConstrainedType(this IArtifactSummaryPropertyBag properties) => properties.GetValueOrDefault<string>(StructureDefinitionConstrainedTypeKey);

        /// <summary>Get the <c>StructureDefinition.contextType</c> property value from the specified collection of artifact summary properties, if available.</summary>
        /// <remarks>Only applies to summaries of <see cref="StructureDefinition"/> resources.</remarks>
        public static string GetStructureDefinitionContextType(this IArtifactSummaryPropertyBag properties) => properties.GetValueOrDefault<string>(StructureDefinitionContextTypeKey);

        /// <summary>Get the <c>StructureDefinition.base</c> property value from the specified collection of artifact summary properties, if available.</summary>
        /// <remarks>Only applies to summaries of <see cref="StructureDefinition"/> resources.</remarks>
        public static string GetStructureDefinitionBase(this IArtifactSummaryPropertyBag properties) => properties.GetValueOrDefault<string>(StructureDefinitionBaseKey);

        /// <summary>Determines if the specified instance represents summary information about a <see cref="StructureDefinition"/> resource.</summary>
        public static bool IsStructureDefinitionSummary(this IArtifactSummaryPropertyBag properties) => properties.GetResourceTypeName() == StructureDefinitionTypeName;

        /// <summary>Harvest specific summary information from a <see cref="StructureDefinition"/> resource.</summary>
        /// <returns><c>true</c> if the current target represents a <see cref="StructureDefinition"/> resource, or <c>false</c> otherwise.</returns>
        /// <remarks>The <see cref="ArtifactSummaryGenerator"/> calls this method from a <see cref="ArtifactSummaryHarvester"/> delegate.</remarks>
        internal static bool Harvest(IElementNavigator nav, ArtifactSummaryPropertyBag properties)
        {
            if (IsStructureDefinitionSummary(properties))
            {
                // Explicit extractor chaining
                if (ConformanceSummaryProperties.Harvest(nav, properties))
                {
                    nav.HarvestValue(properties, StructureDefinitionKindKey, "kind");
                    nav.HarvestValue(properties, StructureDefinitionConstrainedTypeKey, "constrainedType");
                    nav.HarvestValue(properties, StructureDefinitionContextTypeKey, "contextType");
                    nav.HarvestValue(properties, StructureDefinitionBaseKey, "base");
                }
                return true;
            }
            return false;
        }

    }

    /// <summary>For harvesting specific summary information from a <see cref="ValueSet"/> resource.</summary>
    public static class ValueSetSummaryProperties
    {
        static readonly string ValueSetTypeName = ResourceType.ValueSet.GetLiteral();

        public static readonly string ValueSetSystemKey = "ValueSet.System";

        /// <summary>Get the <c>ValueSet.system</c> property value from the specified collection of artifact summary properties, if available.</summary>
        /// <remarks>Only applies to summaries of <see cref="ValueSet"/> resources.</remarks>
        public static string GetValueSetSystem(this IArtifactSummaryPropertyBag properties) => properties.GetValueOrDefault<string>(ValueSetSystemKey);

        /// <summary>Determines if the specified instance represents summary information about a <see cref="StructureDefinition"/> resource.</summary>
        public static bool IsValueSetSummary(this IArtifactSummaryPropertyBag properties) => properties.GetResourceTypeName() == ValueSetTypeName;

        /// <summary>Harvest specific summary information from a <see cref="ValueSet"/> resource.</summary>
        /// <returns><c>true</c> if the current target is a ValueSet, or <c>false</c> otherwise.</returns>
        /// <remarks>The <see cref="ArtifactSummaryGenerator"/> calls this method from a <see cref="ArtifactSummaryHarvester"/> delegate.</remarks>
        internal static bool Harvest(IElementNavigator nav, ArtifactSummaryPropertyBag properties)
        {
            if (IsValueSetSummary(properties))
            {
                // Explicit extractor chaining
                if (ConformanceSummaryProperties.Harvest(nav, properties))
                {
                    nav.HarvestValue(properties, ValueSetSystemKey, "codeSystem", "system");
                }
                return true;
            }
            return false;
        }
    }

    /// <summary>For harvesting specific summary information from a <see cref="ConceptMap"/> resource.</summary>
    public static class ConceptMapSummaryProperties
    {
        static readonly string ConceptMapTypeName = ResourceType.ConceptMap.GetLiteral();

        public static readonly string ConceptMapSourceKey = "ConceptMap.Source";
        public static readonly string ConceptMapTargetKey = "ConceptMap.Target";

        /// <summary>Get the <c>ConceptMap.source[x]</c> property value from the specified collection of artifact summary properties, if available.</summary>
        /// <remarks>Only applies to summaries of <see cref="ConceptMap"/> resources.</remarks>
        public static string GetConceptMapSource(this IArtifactSummaryPropertyBag properties) => properties.GetValueOrDefault<string>(ConceptMapSourceKey);

        /// <summary>Get the <c>ConceptMap.target[x]</c> property value from the specified collection of artifact summary properties, if available.</summary>
        /// <remarks>Only applies to summaries of <see cref="ConceptMap"/> resources.</remarks>
        public static string GetConceptMapTarget(this IArtifactSummaryPropertyBag properties) => properties.GetValueOrDefault<string>(ConceptMapTargetKey);

        /// <summary>Determines if the specified instance represents summary information about a <see cref="ConceptMap"/> resource.</summary>
        public static bool IsConceptMapSummary(this IArtifactSummaryPropertyBag properties) => properties.GetResourceTypeName() == ConceptMapTypeName;

        /// <summary>Harvest specific summary information from a <see cref="ConceptMap"/> resource.</summary>
        /// <returns><c>true</c> if the current target represents a <see cref="ConceptMap"/> resource, or <c>false</c> otherwise.</returns>
        /// <remarks>The <see cref="ArtifactSummaryGenerator"/> calls this method from a <see cref="ArtifactSummaryHarvester"/> delegate.</remarks>
        internal static bool Harvest(IElementNavigator nav, ArtifactSummaryPropertyBag properties)
        {
            if (IsConceptMapSummary(properties))
            {
                // Explicit extractor chaining
                if (ConformanceSummaryProperties.Harvest(nav, properties))
                {
                    if (!nav.HarvestValue(properties, ConceptMapSourceKey, "sourceUri"))
                    {
                        nav.HarvestValue(properties, ConceptMapSourceKey, "sourceReference", "reference");
                    }

                    if (!nav.HarvestValue(properties, ConceptMapTargetKey, "targetUri"))
                    {
                        nav.HarvestValue(properties, ConceptMapTargetKey, "targetReference", "reference");
                    }
                }
                return true;
            }
            return false;
        }
    }

}
