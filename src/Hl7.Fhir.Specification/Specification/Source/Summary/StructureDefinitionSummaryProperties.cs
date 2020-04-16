/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Summary;
using Hl7.Fhir.Utility;

// Expose low-level interfaces from a separate child namespace, to prevent pollution
namespace Hl7.Fhir.Specification.Source
{
    /// <summary>For harvesting specific summary information from a <see cref="StructureDefinition"/> resource.</summary>
    public static class StructureDefinitionSummaryProperties
    {
        static readonly string StructureDefinitionTypeName = ResourceType.StructureDefinition.GetLiteral();

        public static readonly string FhirVersionKey = "StructureDefinition.fhirVersion";
        public static readonly string KindKey = "StructureDefinition.kind";
        public static readonly string TypeKey = "StructureDefinition.type";
        public static readonly string ContextTypeKey = "StructureDefinition.contextType";
        public static readonly string ContextKey = "StructureDefinition.context";
        public static readonly string BaseDefinitionKey = "StructureDefinition.baseDefinition";
        public static readonly string DerivationKey = "StructureDefinition.derivation";

        public static readonly string FmmExtensionUrl = @"http://hl7.org/fhir/StructureDefinition/structuredefinition-fmm";
        public static readonly string MaturityLevelKey = "StructureDefinition.maturityLevel";

        public static readonly string WgExtensionUrl = @"http://hl7.org/fhir/StructureDefinition/structuredefinition-wg";
        public static readonly string WorkingGroupKey = "StructureDefinition.workingGroup";

        public static readonly string RootDefinitionKey = "StructureDefinition.rootDefinition";

        /// <summary>Determines if the specified instance represents summary information about a <see cref="StructureDefinition"/> resource.</summary>
        public static bool IsStructureDefinitionSummary(this IArtifactSummaryPropertyBag properties)
            => properties.GetTypeName() == StructureDefinitionTypeName;

        /// <summary>Harvest specific summary information from a <see cref="StructureDefinition"/> resource.</summary>
        /// <returns><c>true</c> if the current target represents a <see cref="StructureDefinition"/> resource, or <c>false</c> otherwise.</returns>
        /// <remarks>The <see cref="ArtifactSummaryGenerator"/> calls this method from a <see cref="ArtifactSummaryHarvester"/> delegate.</remarks>
        public static bool Harvest(ISourceNode nav, ArtifactSummaryPropertyBag properties)
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
                    nav.HarvestValue(properties, ContextTypeKey, "contextType");
                    // [WMR 20180919] NEW: Extension context
                    nav.HarvestValues(properties, ContextKey, "context");
                    nav.HarvestValue(properties, TypeKey, "type");
                    nav.HarvestValue(properties, BaseDefinitionKey, "baseDefinition");
                    nav.HarvestValue(properties, DerivationKey, "derivation");

                    // [WMR 20180725] Also harvest definition property from (first) root element in snapshot/differential
                    // HL7 FHIR website displays this text as introduction on top of each resource/datatype page
                    var elementNode = nav.Children("snapshot").FirstOrDefault() ?? nav.Children("differential").FirstOrDefault();
                    if (elementNode != null)
                    {
                        var childNode = elementNode.Children("element").FirstOrDefault();
                        if(childNode != null && Navigation.ElementDefinitionNavigator.IsRootPath(childNode.Name))
                        {
                            childNode.HarvestValue(properties, RootDefinitionKey, "definition");
                        }
                    }
                }
                return true;
            }
            return false;
        }

        // Callback for HarvestExtensions, called for each individual extension entry
        static void harvestExtension(ISourceNode nav, IDictionary<string, object> properties, string url)
        {
            if (StringComparer.Ordinal.Equals(FmmExtensionUrl, url))
            {
                var child = nav.Children("valueInteger").FirstOrDefault();
                if (child != null)
                {
                    properties[MaturityLevelKey] = child.Text;
                }
            }
            else if (StringComparer.Ordinal.Equals(WgExtensionUrl, url))
            {
                var child = nav.Children("valueCode").FirstOrDefault();
                if (child != null)
                {
                    properties[WorkingGroupKey] = child.Text;
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

        /// <summary>Get the <c>StructureDefinition.contextType</c> property value from the specified artifact summary property bag, if available.</summary>
        /// <remarks>Only applies to summaries of <see cref="StructureDefinition"/> resources.</remarks>
        public static string GetStructureDefinitionContextType(this IArtifactSummaryPropertyBag properties)
            => properties.GetValueOrDefault<string>(ContextTypeKey);

        /// <summary>Get the <c>StructureDefinition.context</c> property value from the specified artifact summary property bag, if available.</summary>
        /// <remarks>Only applies to summaries of <see cref="StructureDefinition"/> resources.</remarks>
        public static string[] GetStructureDefinitionContext(this IArtifactSummaryPropertyBag properties)
            => properties.GetValueOrDefault<string[]>(ContextKey);

        /// <summary>Get the <c>StructureDefinition.type</c> property value from the specified artifact summary property bag, if available.</summary>
        /// <remarks>Only applies to summaries of <see cref="StructureDefinition"/> resources.</remarks>
        public static string GetStructureDefinitionType(this IArtifactSummaryPropertyBag properties)
            => properties.GetValueOrDefault<string>(TypeKey);

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

        /// <summary>Get the value of the root element definition from the specified artifact summary property bag, if available.</summary>
        /// <remarks>
        /// Returns the definition text of the root element.
        /// Only applies to summaries of <see cref="StructureDefinition"/> resources.
        /// </remarks>
        public static string GetStructureDefinitionRootDefinition(this IArtifactSummaryPropertyBag properties)
           => properties.GetValueOrDefault<string>(RootDefinitionKey);

    }
}