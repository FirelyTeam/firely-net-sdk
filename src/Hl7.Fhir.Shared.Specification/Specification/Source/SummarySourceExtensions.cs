/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>Extension methods for the <see cref="ISummarySource"/> interface.</summary>
    public static class SummarySourceExtensions
    {
        /// <summary>Returns a list of <see cref="ArtifactSummary"/> instances for resources of the specified <see cref="ResourceType"/>.</summary>
        /// <param name="source">A <see cref="ISummarySource"/> instance.</param>
        /// <param name="resourceType"><see cref="ResourceType"/></param>
        /// <returns>A sequence of <see cref="ArtifactSummary"/> instances.</returns>
        public static IEnumerable<ArtifactSummary> ListSummaries(this ISummarySource source, ResourceType resourceType)
            => source.ListSummaries(resourceType.GetLiteral());
    }
}
