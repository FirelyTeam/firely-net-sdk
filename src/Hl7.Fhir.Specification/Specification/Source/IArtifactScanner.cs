/* 
 * Copyright (c) 2017, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.Model;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Source
{
    internal interface IArtifactScanner
    {
        /// <summary>Retrieve the artifact that is identified by the specified summary information.</summary>
        /// <param name="entry">Artifact summary.</param>
        /// <returns>A <see cref="Resource"/> instance.</returns>
        Resource Retrieve(ArtifactSummary entry);

        /// <summary>Scan the source and extract summary information from all the available artifacts.</summary>
        /// <returns>A list of <see cref="ArtifactSummary"/> instances.</returns>
        List<ArtifactSummary> List();
    }
}