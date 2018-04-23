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

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>Interface for harvesting summary information from a set of FHIR artifacts.</summary>
    public interface ISummarySource : IResourceResolver
    {
        /// <summary>Returns a list of <see cref="ArtifactSummary"/> instances with key information about each FHIR artifact provided by the source.</summary>
        IEnumerable<ArtifactSummary> ListSummaries();

        /// <summary>
        /// Load the target artifact described by the specified <see cref="ArtifactSummary"/> instance.
        /// <para>
        /// Implementations should inspect the harvested summary information to uniquely identify and resolve
        /// the target artifact, specifically the <see cref="ArtifactSummary.Origin"/> and
        /// <see cref="ArtifactSummary.Position"/> property values.
        /// </para>
        /// </summary> 
        /// <typeparam name="T">The resource type to return.</typeparam>
        /// <returns>A new resource instance of type <typeparamref name="T"/>, or <c>null</c>.</returns>
        T LoadResource<T>(ArtifactSummary summary) where T : Resource;
    }

    /// <summary>Extension methods for the <see cref="ISummarySource"/> interface.</summary>
    public static class SummarySourceExtensions
    {
        /// <summary>Load the target artifact described by the specified <see cref="ArtifactSummary"/> instance.</summary> 
        /// <returns>A new <see cref="Resource"/> instance, or <c>null</c>.</returns>
        public static Resource LoadResource(this ISummarySource source, ArtifactSummary summary) => source.LoadResource<Resource>(summary);

        /// <summary>Returns a list of <see cref="ArtifactSummary"/> instances with error information.</summary>
        /// <returns>A <see cref="List{T}"/> of <see cref="ArtifactSummary"/> instances.</returns>
        public static IEnumerable<ArtifactSummary> ListSummaryErrors(this ISummarySource source) => source.ListSummaries().Errors();

        /// <summary>Returns a list of <see cref="ArtifactSummary"/> instances for resources of the specified <see cref="ResourceType"/>.</summary>
        /// <returns>A <see cref="List{T}"/> of <see cref="ArtifactSummary"/> instances.</returns>
        public static IEnumerable<ArtifactSummary> ListSummaries(this ISummarySource source, ResourceType resourceType) => source.ListSummaries().OfResourceType(resourceType);
    }
}
