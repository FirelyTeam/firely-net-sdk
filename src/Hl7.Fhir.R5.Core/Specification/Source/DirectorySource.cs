/* 
 * Copyright (c) 2022, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/firely-net-sdk/blob/master/LICENSE
 */

using Hl7.Fhir.Model;

namespace Hl7.Fhir.Specification.Source
{
    /// <inheritdoc/>
    public class DirectorySource : CommonDirectorySource
    {
        /// <inheritdoc/>
        public DirectorySource() : base(ModelInfo.ModelInspector)
        {
        }

        /// <inheritdoc/>
        public DirectorySource(string contentDirectory) : base(ModelInfo.ModelInspector, contentDirectory)
        {
        }

        /// <inheritdoc/>
        public DirectorySource(DirectorySourceSettings settings) : base(ModelInfo.ModelInspector, settings)
        {
        }

        /// <inheritdoc/>
        public DirectorySource(string contentDirectory, DirectorySourceSettings settings) : base(ModelInfo.ModelInspector, contentDirectory, settings)
        {
        }
    }
}
