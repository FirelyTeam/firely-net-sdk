﻿/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

#if NET_FILESYSTEM

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Source.Summary
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
        public static long GetFileSize(this IArtifactSummaryPropertyBag properties)
            => (long)properties.GetValueOrDefault(FileSizeKey);

        internal static void SetFileSize(this ArtifactSummaryPropertyBag properties, long value)
        {
            properties[FileSizeKey] = value;
        }

        /// <summary>Get the LastModified property value from the specified artifact summary property bag, if available.</summary>
        public static DateTime GetLastModified(this IArtifactSummaryPropertyBag properties)
            => (DateTime)properties.GetValueOrDefault(LastModifiedKey);

        internal static void SetLastModified(this ArtifactSummaryPropertyBag properties, DateTime value)
        {
            properties[LastModifiedKey] = value;
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
        public static readonly string TypeKey = "StructureDefinition.type";
        public static readonly string ContextTypeKey = "StructureDefinition.contextType";
        public static readonly string BaseDefinitionKey = "StructureDefinition.baseDefinition";
        public static readonly string DerivationKey = "StructureDefinition.derivation";

        public static readonly string FmmExtensionUrl = @"http://hl7.org/fhir/StructureDefinition/structuredefinition-fmm";
        public static readonly string MaturityLevelKey = "StructureDefinition.maturityLevel";

        public const string WgExtensionUrl = @"http://hl7.org/fhir/StructureDefinition/structuredefinition-wg";
        public static readonly string WorkingGroupKey = "StructureDefinition.workingGroup";

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
                // [WMR 20171218] Harvest global core extensions, e.g. maturity level & working group
                nav.HarvestExtensions(properties, harvestExtension);

                // Explicit extractor chaining
                if (ConformanceSummaryProperties.Harvest(nav, properties))
                {
                    nav.HarvestValue(properties, FhirVersionKey, "fhirVersion");
                    nav.HarvestValue(properties, KindKey, "kind");
                    nav.HarvestValue(properties, TypeKey, "type");
                    nav.HarvestValue(properties, ContextTypeKey, "contextType");
                    nav.HarvestValue(properties, BaseDefinitionKey, "baseDefinition");
                    nav.HarvestValue(properties, DerivationKey, "derivation");
                }
                return true;
            }
            return false;
        }

        // Callback for HarvestExtensions, called for each individual extension entry
        static void harvestExtension(IElementNavigator nav, IDictionary<string, object> properties, string url)
        {
            if (StringComparer.Ordinal.Equals(FmmExtensionUrl, url))
            {
                if (nav.MoveToNext("valueInteger"))
                {
                    properties[MaturityLevelKey] = nav.Value;
                }
            }
            else if (StringComparer.Ordinal.Equals(WgExtensionUrl, url))
            {
                if (nav.MoveToNext("valueCode"))
                {
                    properties[WorkingGroupKey] = nav.Value;
                }
            }
        }

        /// <summary>Get the <c>StructureDefinition.fhirVersion</c> property value from the specified artifact summary property bag, if available.</summary>
        /// <remarks>Only applies to summaries of <see cref="StructureDefinition"/> resources.</remarks>
        public static string GetStructureDefinitionFhirVersion(this IArtifactSummaryPropertyBag properties)
            => properties.GetValueOrDefault<string>(FhirVersionKey);

        /// <summary>Get the <c>StructureDefinition.kind</c> property value from the specified artifact summary property bag, if available.</summary>
        /// <remarks>Only applies to summaries of <see cref="StructureDefinition"/> resources.</remarks>
        public static string GetStructureDefinitionKind(this IArtifactSummaryPropertyBag properties)
            => properties.GetValueOrDefault<string>(KindKey);

        /// <summary>Get the <c>StructureDefinition.type</c> property value from the specified artifact summary property bag, if available.</summary>
        /// <remarks>Only applies to summaries of <see cref="StructureDefinition"/> resources.</remarks>
        public static string GetStructureDefinitionType(this IArtifactSummaryPropertyBag properties)
            => properties.GetValueOrDefault<string>(TypeKey);

        /// <summary>Get the <c>StructureDefinition.contextType</c> property value from the specified artifact summary property bag, if available.</summary>
        /// <remarks>Only applies to summaries of <see cref="StructureDefinition"/> resources.</remarks>
        public static string GetStructureDefinitionContextType(this IArtifactSummaryPropertyBag properties)
            => properties.GetValueOrDefault<string>(ContextTypeKey);

        /// <summary>Get the <c>StructureDefinition.baseDefinition</c> property value from the specified artifact summary property bag, if available.</summary>
        /// <remarks>Only applies to summaries of <see cref="StructureDefinition"/> resources.</remarks>
        public static string GetStructureDefinitionBaseDefinition(this IArtifactSummaryPropertyBag properties)
            => properties.GetValueOrDefault<string>(BaseDefinitionKey);

        /// <summary>Get the <c>StructureDefinition.derivation</c> property value from the specified artifact summary property bag, if available.</summary>
        /// <remarks>Only applies to summaries of <see cref="StructureDefinition"/> resources.</remarks>
        public static string GetStructureDefinitionDerivation(this IArtifactSummaryPropertyBag properties)
            => properties.GetValueOrDefault<string>(DerivationKey);

        /// <summary>Get the value of the maturity level extension from the specified artifact summary property bag, if available.</summary>
        /// <remarks>
        /// Returns the resource maturity level, as defined by the official FHIR extension "http://hl7.org/fhir/StructureDefinition/structuredefinition-fmm".
        /// Only applies to summaries of <see cref="StructureDefinition"/> resources that define FHIR core resources.
        /// </remarks>
        public static string GetStructureDefinitionMaturityLevel(this IArtifactSummaryPropertyBag properties)
            => properties.GetValueOrDefault<string>(MaturityLevelKey);

        /// <summary>Get the value of the working group extension from the specified artifact summary property bag, if available.</summary>
        /// <remarks>
        /// Returns the associated working group, as defined by the official FHIR extension "http://hl7.org/fhir/StructureDefinition/structuredefinition-wg".
        /// Only applies to summaries of <see cref="StructureDefinition"/> resources that define FHIR core resources.
        /// </remarks>
        public static string GetStructureDefinitionWorkingGroup(this IArtifactSummaryPropertyBag properties)
           => properties.GetValueOrDefault<string>(WorkingGroupKey);

    }

    /// <summary>For harvesting specific summary information from a <see cref="CodeSystem"/> resource.</summary>
    public static class CodeSystemSummaryProperties
    { 
        static readonly string CodeSystemTypeName = ResourceType.CodeSystem.GetLiteral();

        public static readonly string ValueSetKey = "CodeSystem.valueSet";

        /// <summary>Determines if the specified instance represents summary information about a <see cref="CodeSystem"/> resource.</summary>
        public static bool IsCodeSystemSummary(this IArtifactSummaryPropertyBag properties)
            => properties.GetTypeName() == CodeSystemTypeName;

        /// <summary>Harvest specific summary information from a <see cref="CodeSystem"/> resource.</summary>
        /// <returns><c>true</c> if the current target represents a <see cref="CodeSystem"/> resource, or <c>false</c> otherwise.</returns>
        /// <remarks>The <see cref="ArtifactSummaryGenerator"/> calls this method from a <see cref="ArtifactSummaryHarvester"/> delegate.</remarks>
        internal static bool Harvest(IElementNavigator nav, ArtifactSummaryPropertyBag properties)
        {
            if (properties.IsCodeSystemSummary())
            {
                // Explicit harvester chaining
                if (ConformanceSummaryProperties.Harvest(nav, properties))
                {
                    nav.HarvestValue(properties, ValueSetKey, "valueSet", "value");
                }
                return true;
            }
            return false;
        }

        /// <summary>Get the <c>CodeSystem.valueSet</c> property value from the specified artifact summary property bag, if available.</summary>
        /// <remarks>Only applies to summaries of <see cref="CodeSystem"/> resources.</remarks>
        public static string GetCodeSystemValueSet(this IArtifactSummaryPropertyBag properties)
            => properties.GetValueOrDefault<string>(ValueSetKey);
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
                // Explicit harvester chaining
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
