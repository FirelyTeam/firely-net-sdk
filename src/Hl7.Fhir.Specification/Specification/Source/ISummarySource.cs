/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Specification.Source.Summary;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>Interface for browsing and resolving FHIR conformance resources with summary information.</summary>
    public interface ISummarySource : IConformanceSource
    {
        // <summary>Returns a list of summary information for all the FHIR artifacts provided by the source.</summary>
        ReadOnlyCollection<ArtifactSummary> ListSummaries();
    }

    /// <summary>Extension methods for the <see cref="ISummarySource"/> interface.</summary>
    public static class SummarySourceExtensions
    {
        /// <summary>Returns a list of <see cref="ArtifactSummary"/> instances with error information.</summary>
        /// <returns>A <see cref="List{T}"/> of <see cref="ArtifactSummary"/> instances.</returns>
        public static IEnumerable<ArtifactSummary> ListSummaryErrors(this ISummarySource source) => source.ListSummaries().Errors();

        /// <summary>Returns a list of <see cref="ArtifactSummary"/> instances for resources of the specified <see cref="ResourceType"/>.</summary>
        /// <returns>A <see cref="List{T}"/> of <see cref="ArtifactSummary"/> instances.</returns>
        public static IEnumerable<ArtifactSummary> ListSummaries(this ISummarySource source, ResourceType resourceType) => source.ListSummaries().OfResourceType(resourceType);
    }
}
