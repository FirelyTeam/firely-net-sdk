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

namespace Hl7.Fhir.Introspection;

/// <summary>
/// A list of <see cref="PropertyMapping"/>s, indexed by name and order and choice.
/// </summary>
internal class PropertyMappingCollection
{
    internal PropertyMappingCollection(IEnumerable<PropertyMapping> mappings)
    {
        ByName = mappings.ToDictionary(m => m.Name, StringComparer.OrdinalIgnoreCase);
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