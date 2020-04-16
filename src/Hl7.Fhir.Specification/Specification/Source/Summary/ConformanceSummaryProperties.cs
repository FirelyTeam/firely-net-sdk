/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Summary;

// Expose low-level interfaces from a separate child namespace, to prevent pollution
namespace Hl7.Fhir.Specification.Source
{
    /// <summary>For harvesting common summary information from a conformance resource.</summary>
    public static class ConformanceSummaryProperties
    {
        public static readonly string CanonicalUrlKey = "Conformance.url";
        public static readonly string NameKey = "Conformance.name";
        public static readonly string VersionKey = "Conformance.version";
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
        /// <seealso cref="ArtifactSummaryHarvesters"/>
        public static bool Harvest(ISourceNode nav, ArtifactSummaryPropertyBag properties)
        {
            if (IsConformanceSummary(properties))
            {
                nav.HarvestValue(properties, CanonicalUrlKey, "url");
                nav.HarvestValue(properties, VersionKey, "version");
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

        /// <summary>Get the <c>version</c> property value from the specified artifact summary property bag, if available.</summary>
        /// <remarks>Only applies to summaries of conformance resources.</remarks>
        public static string GetConformanceVersion(this IArtifactSummaryPropertyBag properties)
            => properties.GetValueOrDefault<string>(VersionKey);

        /// <summary>Get the <c>name</c> property value from the specified artifact summary property bag, if available.</summary>
        /// <remarks>Only applies to summaries of conformance resources.</remarks>
        public static string GetConformanceName(this IArtifactSummaryPropertyBag properties)
            => properties.GetValueOrDefault<string>(NameKey);

        /// <summary>Get the <c>status</c> property value from the specified artifact summary property bag, if available.</summary>
        /// <remarks>Only applies to summaries of conformance resources.</remarks>
        public static string GetConformanceStatus(this IArtifactSummaryPropertyBag properties)
            => properties.GetValueOrDefault<string>(StatusKey);
    }
}