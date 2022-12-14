/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Introspection
{
    internal class PropertyMappingCollection
    {
        public PropertyMappingCollection()
        {
            // nothing beyond default initializers.
        }

        public PropertyMappingCollection(Dictionary<string, PropertyMapping> byName)
        {
            if (byName.Comparer != StringComparer.OrdinalIgnoreCase)
                throw new ArgumentException("Dictionary should be keyed by OrdinalIgnoreCase.");

            ByName = byName;
            ByOrder = byName.Values.OrderBy(pm => pm.Order).ToList();
            ChoiceProperties = ByOrder.Where(pm => pm.Choice == ChoiceType.DatatypeChoice).ToList();
        }

        /// <summary>
        /// List of the properties, in the order of appearance.
        /// </summary>
        public readonly List<PropertyMapping> ByOrder = new();

        /// <summary>
        /// The list of properties that represent choice elements.
        /// </summary>
        public readonly List<PropertyMapping> ChoiceProperties = new();

        /// <summary>
        /// List of the properties, keyed by name.
        /// </summary>
        public readonly Dictionary<string, PropertyMapping> ByName = new();
    }
}

#nullable restore