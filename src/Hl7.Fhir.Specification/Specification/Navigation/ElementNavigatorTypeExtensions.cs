/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

/*
 * [WMR 20160909] Removed, pollutes the Navigation namespace
 * Maybe move to Snapshot namespace?

using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Navigation
{
    // [WMR 20160808] NEW

    public static class ElementNavigatorTypeExtensions
    {
        /// <summary>Enumerate all distinct element type profiles.</summary>
        /// <param name="nav">A <see cref="ElementDefinitionNavigator"/> instance.</param>
        /// <returns>A sequence of type profile resource references.</returns>
        public static IEnumerable<string> EnumerateTypeProfiles(this ElementDefinitionNavigator nav)
        {
            return EnumerateTypeProfiles(nav.Elements);
        }

        /// <summary>Enumerate all distinct element type profiles.</summary>
        /// <param name="elements">A <see cref="StructureDefinition.SnapshotComponent"/> or <see cref="StructureDefinition.DifferentialComponent"/> instance.</param>
        /// <returns>A sequence of type profile resource references.</returns>
        public static IEnumerable<string> EnumerateTypeProfiles(this IElementList elements)
        {
            return EnumerateTypeProfiles(elements.Element);
        }

        /// <summary>Enumerate all element type profiles.</summary>
        /// <param name="elements">A list of <see cref="ElementDefinition"/> instances.</param>
        /// <returns>A sequence of type profile resource references.</returns>
        public static IEnumerable<string> EnumerateTypeProfiles(this IList<ElementDefinition> elements)
        {
            return elements.SelectMany(e => e.Type).SelectMany(t => t.Profile);
        }
    }
}
*/