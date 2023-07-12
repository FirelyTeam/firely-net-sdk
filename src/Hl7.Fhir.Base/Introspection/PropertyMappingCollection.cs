/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Introspection
{
    internal class PropertyMappingCollection
    {
        public PropertyMappingCollection(IEnumerable<PropertyMapping> mappings)
        {
            var byName = new Dictionary<string, PropertyMapping>(StringComparer.OrdinalIgnoreCase);

            foreach (var mapping in mappings)
            {
                var propKey = mapping.Name;
                if (byName.ContainsKey(propKey))
                    throw Error.InvalidOperation($"Class has multiple properties that are named '{propKey}'. The property name must be unique.");

                byName[propKey] = mapping;
            }

            ByName = byName;
            ByOrder = ByName.Values.OrderBy(pm => pm.Order).ToList();
            ChoiceProperties = ByOrder.Where(pm => pm.Choice == ChoiceType.DatatypeChoice).ToList();
        }

        /// <summary>
        /// List of the properties, in the order of appearance.
        /// </summary>
        public readonly IReadOnlyList<PropertyMapping> ByOrder;

        /// <summary>
        /// The list of properties that represent choice elements.
        /// </summary>
        public readonly IReadOnlyList<PropertyMapping> ChoiceProperties;

        /// <summary>
        /// List of the properties, keyed by name.
        /// </summary>
        public readonly IReadOnlyDictionary<string, PropertyMapping> ByName;
    }
}

#nullable restore