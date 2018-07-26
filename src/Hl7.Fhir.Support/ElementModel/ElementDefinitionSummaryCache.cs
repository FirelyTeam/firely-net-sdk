/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */


using Hl7.Fhir.Specification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.ElementModel
{
    /// <summary>
    /// Internal class to optimize access to a list of children, basically a dictionary of
    /// (element name, IElementSerializationInfo) pairs, optimized for quick access.
    /// </summary>
    internal class ElementDefinitionSummaryCache : Dictionary<string, IElementDefinitionSummary>
    {
        public static ElementDefinitionSummaryCache ForType(IStructureDefinitionSummary type)
            => new ElementDefinitionSummaryCache(type.GetElements().ToDictionary(c => c.ElementName));

        public static ElementDefinitionSummaryCache Empty = new ElementDefinitionSummaryCache();

        public static ElementDefinitionSummaryCache ForRoot(IElementDefinitionSummary rootInfo)
        {
            if (rootInfo == null) throw new ArgumentNullException(nameof(rootInfo));

            return new ElementDefinitionSummaryCache(new Dictionary<string, IElementDefinitionSummary>
                { { rootInfo.ElementName, rootInfo } });
        }

        public bool TryGetBySuffixedName(string name, out IElementDefinitionSummary info)
        {
            // Simplest case, one on one match between name and element name
            if (TryGetValue(name, out info))
                return true;

            info = this.Where(kvp => name.StartsWith(kvp.Key)).Select(kvp => kvp.Value).FirstOrDefault();

            // False hit -> we matched the prefix, but the property is not actually
            // a choice element (simply misspelled).
            if (info != null && !info.IsChoiceElement && name != info.ElementName)
                info = null;

            return info != null;
        }

        private ElementDefinitionSummaryCache() : base(new Dictionary<string, IElementDefinitionSummary>())
        {
        }

        private ElementDefinitionSummaryCache(IDictionary<string, IElementDefinitionSummary> elements) : base(elements)
        {
        }
    }
}
