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
using Hl7.Fhir.Utility;

// Expose low-level interfaces from a separate child namespace, to prevent pollution
namespace Hl7.Fhir.Specification.Source
{
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
        public static bool Harvest(ISourceNode nav, ArtifactSummaryPropertyBag properties)
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
}