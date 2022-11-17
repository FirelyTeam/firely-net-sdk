#nullable enable
/* 
 * Copyright (c) 2022, Firely (info@fire.ly) and contributors
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
    /// <inheritdoc/>
    public class DirectorySource : CommonDirectorySource, IConformanceSource
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

        #region IConformanceSource
        /// <inheritdoc/>
        public IEnumerable<ConceptMap> FindConceptMaps(string? sourceUri = null, string? targetUri = null) =>
            FindConceptMaps<ConceptMap>(sourceUri, targetUri);

        /// <inheritdoc/>
        public NamingSystem? FindNamingSystem(string uniqueId) => FindNamingSystem<NamingSystem>(uniqueId);

        /// <inheritdoc/>
        public IEnumerable<string> ListResourceUris(ResourceType? filter = null) =>
            ListResourceUris(filter?.GetLiteral());
        #endregion
    }
}
#nullable restore