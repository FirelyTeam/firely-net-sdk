/* 
 * Copyright (c) 2017, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */
 
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>
    /// Abstract base class for efficiently extracting metadata from a raw FHIR resource file.
    /// Also provides a method resolve the full resource based on the previously extracted metadata.
    /// </summary>
    internal abstract class ArtifactScanner
    {
        /// <summary>Base url for generating virtual resource urls.</summary>
        public const string ResourceUrlBase = "http://example.org/";

        readonly ArtifactSummaryHarvester _harvester;
        readonly string _path;

        /// <summary>ctor</summary>
        /// <param name="path">Full path specification of a FHIR resource file.</param>
        /// <param name="harvester">An <see cref="ArtifactSummaryHarvester"/> instance to extract a concrete set of summary data from the resource.</param>
        public ArtifactScanner(string path, ArtifactSummaryHarvester harvester)
        {
            _path = path ?? throw new ArgumentNullException(nameof(path));
            _harvester = harvester ?? throw new ArgumentNullException(nameof(harvester));
        }

        /// <summary>Scan the source and extract summary information from all the available artifacts.</summary>
        /// <returns>A list of <see cref="ArtifactSummary"/> instances.</returns>
        public List<ArtifactSummary> List()
        {
            IEnumerable<ArtifactSummary> summaries = Enumerable.Empty<ArtifactSummary>();
            var input = CreateStream(_path);
            if (input != null)
            {
                using (input)
                {
                    summaries = _harvester.Generate(input);
                }
            }
            return new List<ArtifactSummary>(summaries);
        }

        /// <summary>
        /// Called by the <see cref="ArtifactScanner.List"/> method in order to create a
        /// concrete instance of a class that implements the <see cref="INavigatorStream"/> interface.
        /// Concrete subclasses must override and implement this method.
        /// </summary>
        /// <param name="path">Full path specification of the target FHIR resource file.</param>
        /// <returns>An instance of a class that supports the <see cref="INavigatorStream"/> interface.</returns>
        protected abstract INavigatorStream CreateStream(string path);

        /// <summary>
        /// Format a fully specified virtual resource uri for the specified resource type and id.
        /// The generated uri is based on the <see cref="ResourceUrlBase"/> value.
        /// </summary>
        /// <param name="resourceType">The typename of the resource.</param>
        /// <param name="id">The unique resource identifier.</param>
        /// <returns>A fully qualified uri string.</returns>
        protected static string CreateResourceUri(string resourceType, string id)
            => ResourceUrlBase + resourceType + "/" + id;

        /// <summary>Retrieve the artifact that is identified by the specified summary information.</summary>
        /// <param name="entry">Summary information for a specific artifact, as returned by the <see cref="ArtifactScanner.List"/> method.</param>
        /// <returns>A FHIR <see cref="Resource"/> instance.</returns>
        public abstract Resource Retrieve(ArtifactSummary entry);

    }
}
