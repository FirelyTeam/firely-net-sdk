/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using System.Collections.Generic;
using System.IO;

namespace Hl7.Fhir.Specification.Source
{
    /// <summary>Interface for browsing and resolving FHIR artifacts by (file) name.</summary>
    public interface IArtifactSource
    {
        /// <summary>Returns a list of artifact filenames.</summary>
        IEnumerable<string> ListArtifactNames();

        /// <summary>Load the artifact with the specified filename.</summary>
        /// <remarks>
        /// This method does not support duplicate file names in separate subfolders of the content directory.
        /// The <seealso cref="ISummarySource"/> interface provides methods to unambiguously retrieve specific
        /// artifacts from the associated summary instance.
        /// </remarks>
        Stream LoadArtifactByName(string artifactName);
    }

}