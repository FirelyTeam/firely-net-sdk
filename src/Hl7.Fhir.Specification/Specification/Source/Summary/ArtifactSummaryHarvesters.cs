/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

#if NET_FILESYSTEM

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Hl7.Fhir.Specification.Summary;
using System;
using System.Collections.Generic;
using System.Diagnostics;

// Expose low-level interfaces from a separate child namespace, to prevent pollution
namespace Hl7.Fhir.Specification.Source
{
    // Define a set of default ArtifactSummaryHarvester delegate implementations,
    // with property keys and helper extension methods for accessing the harvested properties.

    // DSTU2 Specific!

    /// <summary>For accessing common artifact summary properties stored in an <see cref="ArtifactSummaryPropertyBag"/>.</summary>
    /// <remarks>
    /// The <see cref="ArtifactSummaryGenerator"/> creates an <see cref="ArtifactSummaryPropertyBag"/> 
    /// for each artifact and adds a set of common summary properties, independent of the resource type.
    /// This class provides property keys and extension methods to access the common properties.
    /// </remarks>
    public static class ArtifactSummaryProperties
    {
        public const string OriginKey = "Origin";
        public const string FileSizeKey = "Size";
        public const string LastModifiedKey = "LastModified";
        public const string SerializationFormatKey = "Format";
        public const string PositionKey = "Position";
        public const string TypeNameKey = "TypeName";
        public const string ResourceUriKey = "Uri";

        /// <summary>Try to retrieve the property value for the specified key.</summary>
        /// <param name="properties">An artifact summary property bag.</param>
        /// <param name="key">A property key.</param>
        /// <returns>An object value, or <c>null</c>.</returns>
        public static object GetValueOrDefault(this IArtifactSummaryPropertyBag properties, string key)
            => properties.TryGetValue(key, out object result) ? result : null;

        /// <summary>Try to retrieve the property value for the specified key.</summary>
        /// <param name="properties">An artifact summary property bag.</param>
        /// <param name="key">A property key.</param>
        /// <typeparam name="T">The type of the property value.</typeparam>
        /// <returns>A value of type <typeparamref name="T"/>, or <c>null</c>.</returns>
        public static T GetValueOrDefault<T>(this IArtifactSummaryPropertyBag properties, string key)
            where T : class
            => properties.GetValueOrDefault(key) as T;

        /// <summary>Get the Origin property value from the specified artifact summary property bag, if available.</summary>
        public static string GetOrigin(this IArtifactSummaryPropertyBag properties)
            => properties.GetValueOrDefault<string>(OriginKey);

        internal static void SetOrigin(this ArtifactSummaryPropertyBag properties, string value)
        {
            properties[OriginKey] = value;
        }

        /// <summary>Get the Size property value from the specified artifact summary property bag, if available.</summary>
        public static long? GetFileSize(this IArtifactSummaryPropertyBag properties)
            => (long?)properties.GetValueOrDefault(FileSizeKey);

        internal static void SetFileSize(this ArtifactSummaryPropertyBag properties, long value)
        {
            properties[FileSizeKey] = value;
        }

        /// <summary>Get the LastModified property value from the specified artifact summary property bag, if available.</summary>
        public static DateTime? GetLastModified(this IArtifactSummaryPropertyBag properties)
            => (DateTime?)properties.GetValueOrDefault(LastModifiedKey);

        internal static void SetLastModified(this ArtifactSummaryPropertyBag properties, DateTime value)
        {
            properties[LastModifiedKey] = value;
        }

        /// <summary>
        /// Get a string value from the specified artifact summary property bag that represents the artifact
        /// serialization format, as defined by the <see cref="FhirSerializationFormats"/> class, if available.
        /// </summary>
        public static string GetSerializationFormat(this IArtifactSummaryPropertyBag properties)
            => properties.GetValueOrDefault<string>(SerializationFormatKey);

        internal static void SetSerializationFormat(this ArtifactSummaryPropertyBag properties, string value)
        {
            Debug.Assert(Array.IndexOf(FhirSerializationFormats.All, value) >= 0);
            properties[SerializationFormatKey] = value;
        }

        /// <summary>Get the Position property value from the specified artifact summary property bag, if available.</summary>
        public static string GetPosition(this IArtifactSummaryPropertyBag properties)
            => properties.GetValueOrDefault<string>(PositionKey);

        internal static void SetPosition(this ArtifactSummaryPropertyBag properties, string value)
        {
            properties[PositionKey] = value;
        }

        /// <summary>Get the TypeName property value from the specified artifact summary property bag, if available.</summary>
        public static string GetTypeName(this IArtifactSummaryPropertyBag properties)
            => properties.GetValueOrDefault<string>(TypeNameKey);

        internal static void SetTypeName(this ArtifactSummaryPropertyBag properties, string value)
        {
            properties[TypeNameKey] = value;
        }

        /// <summary>Get the ResourceUri property value from the specified artifact summary property bag, if available.</summary>
        public static string GetResourceUri(this IArtifactSummaryPropertyBag properties)
            => properties.GetValueOrDefault<string>(ResourceUriKey);

        internal static void SetResourceUri(this ArtifactSummaryPropertyBag properties, string value)
        {
            properties[ResourceUriKey] = value;
        }
    }

    /// <summary>For harvesting specific summary information from a <see cref="NamingSystem"/> resource.</summary>
    public static class NamingSystemSummaryProperties
    {
        static readonly string NamingSystemTypeName = ResourceType.NamingSystem.GetLiteral();

        public static readonly string UniqueIdKey = "NamingSystem.uniqueId";

        /// <summary>Determines if the specified instance represents summary information about a <see cref="NamingSystem"/> resource.</summary>
        public static bool IsNamingSystemSummary(this IArtifactSummaryPropertyBag properties)
            => properties.GetTypeName() == NamingSystemTypeName;

        /// <summary>Harvest specific summary information from a <see cref="NamingSystem"/> resource.</summary>
        /// <returns><c>true</c> if the current target represents a <see cref="NamingSystem"/> resource, or <c>false</c> otherwise.</returns>
        /// <remarks>The <see cref="ArtifactSummaryGenerator"/> calls this method through a <see cref="ArtifactSummaryHarvester"/> delegate.</remarks>
        public static bool Harvest(IElementNavigator nav, ArtifactSummaryPropertyBag properties)
        {
            if (IsNamingSystemSummary(properties))
            {
                nav.HarvestValues(properties, UniqueIdKey, "uniqueId", "value");
                return true;
            }
            return false;
        }

        /// <summary>Get the <c>NamingSystem.uniqueId</c> property value from the specified artifact summary property bag, if available.</summary>
        /// <remarks>Only applies to summaries of <see cref="NamingSystem"/> resources.</remarks>
        public static string[] GetNamingSystemUniqueId(this IArtifactSummaryPropertyBag properties)
            => properties.GetValueOrDefault<string[]>(UniqueIdKey);

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
    }

    /// <summary>For harvesting common summary information from a conformance resource.</summary>
    public static class ConformanceSummaryProperties
    {
        public static readonly string CanonicalUrlKey = "Conformance.url";
        public static readonly string NameKey = "Conformance.name";
        public static readonly string StatusKey = "Conformance.status";

        /// <summary>Determines if the specified instance represents summary information about a conformance resource.</summary>
        public static bool IsConformanceSummary(this IArtifactSummaryPropertyBag properties)
            => ModelInfo.IsConformanceResource(properties.GetTypeName());

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
        public static bool Harvest(IElementNavigator nav, ArtifactSummaryPropertyBag properties)
        {
            if (IsConformanceSummary(properties))
            {
                nav.HarvestValue(properties, CanonicalUrlKey, "url");
                nav.HarvestValue(properties, NameKey, "name");
                nav.HarvestValue(properties, StatusKey, "status");
                return true;
            }
            return false;
        }

        /// <summary>Get the canonical <c>url</c> property value from the specified artifact summary property bag, if available.</summary>
        /// <remarks>Only applies to summaries of conformance resources.</remarks>
        public static string GetConformanceCanonicalUrl(this IArtifactSummaryPropertyBag properties)
            => properties.GetValueOrDefault<string>(CanonicalUrlKey);

        /// <summary>Get the <c>name</c> property value from the specified artifact summary property bag, if available.</summary>
        /// <remarks>Only applies to summaries of conformance resources.</remarks>
        public static string GetConformanceName(this IArtifactSummaryPropertyBag properties)
            => properties.GetValueOrDefault<string>(NameKey);

        /// <summary>Get the <c>status</c> property value from the specified artifact summary property bag, if available.</summary>
        /// <remarks>Only applies to summaries of conformance resources.</remarks>
        public static string GetConformanceStatus(this IArtifactSummaryPropertyBag properties)
            => properties.GetValueOrDefault<string>(StatusKey);
    }

    /// <summary>For harvesting specific summary information from a <see cref="StructureDefinition"/> resource.</summary>
    public static class StructureDefinitionSummaryProperties
    {
        static readonly string StructureDefinitionTypeName = ResourceType.StructureDefinition.GetLiteral();

        public static readonly string FhirVersionKey = "StructureDefinition.fhirVersion";
        public static readonly string KindKey = "StructureDefinition.kind";
        public static readonly string ConstrainedTypeKey = "StructureDefinition.constrainedType";
        public static readonly string ContextTypeKey = "StructureDefinition.contextType";
        public static readonly string BaseKey = "StructureDefinition.base";

        public static readonly string FmmExtensionUrl = @"http://hl7.org/fhir/StructureDefinition/structuredefinition-fmm";
        public static readonly string MaturityLevelKey = "StructureDefinition.maturityLevel";

        /// <summary>Determines if the specified instance represents summary information about a <see cref="StructureDefinition"/> resource.</summary>
        public static bool IsStructureDefinitionSummary(this IArtifactSummaryPropertyBag properties)
            => properties.GetTypeName() == StructureDefinitionTypeName;

        /// <summary>Harvest specific summary information from a <see cref="StructureDefinition"/> resource.</summary>
        /// <returns><c>true</c> if the current target represents a <see cref="StructureDefinition"/> resource, or <c>false</c> otherwise.</returns>
        /// <remarks>The <see cref="ArtifactSummaryGenerator"/> calls this method from a <see cref="ArtifactSummaryHarvester"/> delegate.</remarks>
        public static bool Harvest(IElementNavigator nav, ArtifactSummaryPropertyBag properties)
        {
            if (IsStructureDefinitionSummary(properties))
            {
                // [WMR 20171218] Harvest global core extensions, e.g. MaturityLevel
                nav.HarvestExtensions(properties, harvestExtension);

                // Explicit extractor chaining
                if (ConformanceSummaryProperties.Harvest(nav, properties))
                {
                    nav.HarvestValue(properties, FhirVersionKey, "fhirVersion");
                    nav.HarvestValue(properties, KindKey, "kind");
                    nav.HarvestValue(properties, ConstrainedTypeKey, "constrainedType");
                    nav.HarvestValue(properties, ContextTypeKey, "contextType");
                    nav.HarvestValue(properties, BaseKey, "base");
                }
                return true;
            }
            return false;
        }

        // Callback for HarvestExtensions, called for each individual extension entry
        static void harvestExtension(IElementNavigator nav, IDictionary<string, object> properties, string url)
        {
            if (StringComparer.Ordinal.Equals(FmmExtensionUrl, url) && nav.MoveToNext("valueInteger"))
            {
                properties[MaturityLevelKey] = nav.Value;
            }
        }

        /// <summary>Get the <c>StructureDefinition.fhirVersion</c> property value from the specified artifact summary property bag, if available.</summary>
        /// <remarks>Only applies to summaries of <see cref="StructureDefinition"/> resources.</remarks>
        public static string GetStructureDefinitionFhirVersion(this IArtifactSummaryPropertyBag properties)
            => properties.GetValueOrDefault<string>(FhirVersionKey);

        /// <summary>Get the <c>StructureDefinition.Kind</c> property value from the specified artifact summary property bag, if available.</summary>
        /// <remarks>Only applies to summaries of <see cref="StructureDefinition"/> resources.</remarks>
        public static string GetStructureDefinitionKind(this IArtifactSummaryPropertyBag properties)
            => properties.GetValueOrDefault<string>(KindKey);

        /// <summary>Get the <c>StructureDefinition.constrainedType</c> property value from the specified artifact summary property bag, if available.</summary>
        /// <remarks>Only applies to summaries of <see cref="StructureDefinition"/> resources.</remarks>
        public static string GetStructureDefinitionConstrainedType(this IArtifactSummaryPropertyBag properties)
            => properties.GetValueOrDefault<string>(ConstrainedTypeKey);

        /// <summary>Get the <c>StructureDefinition.contextType</c> property value from the specified artifact summary property bag, if available.</summary>
        /// <remarks>Only applies to summaries of <see cref="StructureDefinition"/> resources.</remarks>
        public static string GetStructureDefinitionContextType(this IArtifactSummaryPropertyBag properties)
            => properties.GetValueOrDefault<string>(ContextTypeKey);

        /// <summary>Get the <c>StructureDefinition.base</c> property value from the specified artifact summary property bag, if available.</summary>
        /// <remarks>Only applies to summaries of <see cref="StructureDefinition"/> resources.</remarks>
        public static string GetStructureDefinitionBase(this IArtifactSummaryPropertyBag properties)
            => properties.GetValueOrDefault<string>(BaseKey);

        /// <summary>Get the value of the maturity level extension from the specified artifact summary property bag, if available.</summary>
        /// <remarks>
        /// Returns the resource maturity level, as defined by the official FHIR extension "http://hl7.org/fhir/StructureDefinition/structuredefinition-fmm".
        /// Only applies to summaries of <see cref="StructureDefinition"/> resources that define FHIR core resources.
        /// </remarks>
        public static string GetStructureDefinitionMaturityLevel(this IArtifactSummaryPropertyBag properties)
            => properties.GetValueOrDefault<string>(MaturityLevelKey);
    }

    /// <summary>For harvesting specific summary information from a <see cref="ValueSet"/> resource.</summary>
    public static class ValueSetSummaryProperties
    {
        static readonly string ValueSetTypeName = ResourceType.ValueSet.GetLiteral();

        public static readonly string SystemKey = "ValueSet.system";

        /// <summary>Determines if the specified instance represents summary information about a <see cref="StructureDefinition"/> resource.</summary>
        public static bool IsValueSetSummary(this IArtifactSummaryPropertyBag properties)
            => properties.GetTypeName() == ValueSetTypeName;

        /// <summary>Harvest specific summary information from a <see cref="ValueSet"/> resource.</summary>
        /// <returns><c>true</c> if the current target is a ValueSet, or <c>false</c> otherwise.</returns>
        /// <remarks>The <see cref="ArtifactSummaryGenerator"/> calls this method from a <see cref="ArtifactSummaryHarvester"/> delegate.</remarks>
        public static bool Harvest(IElementNavigator nav, ArtifactSummaryPropertyBag properties)
        {
            if (IsValueSetSummary(properties))
            {
                // Explicit extractor chaining
                if (ConformanceSummaryProperties.Harvest(nav, properties))
                {
                    nav.HarvestValue(properties, SystemKey, "codeSystem", "system");
                }
                return true;
            }
            return false;
        }

        /// <summary>Get the <c>ValueSet.system</c> property value from the specified artifact summary property bag, if available.</summary>
        /// <remarks>Only applies to summaries of <see cref="ValueSet"/> resources.</remarks>
        public static string GetValueSetSystem(this IArtifactSummaryPropertyBag properties)
            => properties.GetValueOrDefault<string>(SystemKey);
    }

    /// <summary>For harvesting specific summary information from a <see cref="ConceptMap"/> resource.</summary>
    public static class ConceptMapSummaryProperties
    {
        static readonly string ConceptMapTypeName = ResourceType.ConceptMap.GetLiteral();

        public static readonly string SourceKey = "ConceptMap.source";
        public static readonly string TargetKey = "ConceptMap.target";

        /// <summary>Determines if the specified instance represents summary information about a <see cref="ConceptMap"/> resource.</summary>
        public static bool IsConceptMapSummary(this IArtifactSummaryPropertyBag properties)
            => properties.GetTypeName() == ConceptMapTypeName;

        /// <summary>Harvest specific summary information from a <see cref="ConceptMap"/> resource.</summary>
        /// <returns><c>true</c> if the current target represents a <see cref="ConceptMap"/> resource, or <c>false</c> otherwise.</returns>
        /// <remarks>The <see cref="ArtifactSummaryGenerator"/> calls this method from a <see cref="ArtifactSummaryHarvester"/> delegate.</remarks>
        public static bool Harvest(IElementNavigator nav, ArtifactSummaryPropertyBag properties)
        {
            if (IsConceptMapSummary(properties))
            {
                // Explicit extractor chaining
                if (ConformanceSummaryProperties.Harvest(nav, properties))
                {
                    if (!nav.HarvestValue(properties, SourceKey, "sourceUri"))
                    {
                        nav.HarvestValue(properties, SourceKey, "sourceReference", "reference");
                    }

                    if (!nav.HarvestValue(properties, TargetKey, "targetUri"))
                    {
                        nav.HarvestValue(properties, TargetKey, "targetReference", "reference");
                    }
                }
                return true;
            }
            return false;
        }
        
        /// <summary>Get the <c>ConceptMap.source[x]</c> property value from the specified artifact summary property bag, if available.</summary>
        /// <remarks>Only applies to summaries of <see cref="ConceptMap"/> resources.</remarks>
        public static string GetConceptMapSource(this IArtifactSummaryPropertyBag properties)
            => properties.GetValueOrDefault<string>(SourceKey);

        /// <summary>Get the <c>ConceptMap.target[x]</c> property value from the specified artifact summary property bag, if available.</summary>
        /// <remarks>Only applies to summaries of <see cref="ConceptMap"/> resources.</remarks>
        public static string GetConceptMapTarget(this IArtifactSummaryPropertyBag properties)
            => properties.GetValueOrDefault<string>(TargetKey);
    }

}

#endif
