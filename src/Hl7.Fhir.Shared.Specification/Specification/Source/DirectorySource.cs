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
using System.Linq;

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
        public IEnumerable<ConceptMap> FindConceptMaps(string sourceUri = null, string targetUri = null)
        {
            if (sourceUri == null && targetUri == null)
            {
                throw Error.ArgumentNull(nameof(targetUri), $"{nameof(sourceUri)} and {nameof(targetUri)} arguments cannot both be null");
            }
            var summaries = GetSummaries().FindConceptMaps(sourceUri, targetUri);
            return summaries.Select(summary => loadResourceInternal<ConceptMap>(summary)).Where(r => r != null);
        }

        /// <inheritdoc/>
        public NamingSystem FindNamingSystem(string uniqueId)
        {
            if (uniqueId == null) throw Error.ArgumentNull(nameof(uniqueId));
            var summary = GetSummaries().ResolveNamingSystem(uniqueId, ModelInfo.ModelInspector);
            return loadResourceInternal<NamingSystem>(summary);
        }

        /// <inheritdoc/>
        public IEnumerable<string> ListResourceUris(ResourceType? filter = null)
        {
            // [WMR 20180813] Do not return null values from non-FHIR artifacts (ResourceUri = null)
            // => OfResourceType filters valid FHIR artifacts (ResourceUri != null)
            return GetSummaries().OfResourceType(filter?.GetLiteral()).Select(dsi => dsi.ResourceUri);
        }
        #endregion
    }
}
