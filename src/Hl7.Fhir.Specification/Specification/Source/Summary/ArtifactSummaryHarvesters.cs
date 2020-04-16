/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Summary;
using Hl7.Fhir.Utility;

// Expose low-level interfaces from a separate child namespace, to prevent pollution
namespace Hl7.Fhir.Specification.Source
{
    /// <summary>For harvesting specific summary information from a <see cref="ConceptMap"/> resource.</summary>
    public static class ArtifactSummaryHarvesters
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
        public static bool Harvest(ISourceNode nav, ArtifactSummaryPropertyBag properties)
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