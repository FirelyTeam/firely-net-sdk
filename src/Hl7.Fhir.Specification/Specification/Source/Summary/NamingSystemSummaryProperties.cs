/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */

using System;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Summary;
using Hl7.Fhir.Utility;

// Expose low-level interfaces from a separate child namespace, to prevent pollution
namespace Hl7.Fhir.Specification.Source
{
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
        public static bool Harvest(ISourceNode nav, ArtifactSummaryPropertyBag properties)
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
}