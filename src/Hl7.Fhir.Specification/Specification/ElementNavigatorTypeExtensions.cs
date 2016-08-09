using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Navigation
{
    // [WMR 20160808] NEW

    public static class ElementNavigatorExtensions
    {
        /// <summary>Enumerate all distinct external element type profiles.</summary>
        /// <param name="elements">A <see cref="StructureDefinition.SnapshotComponent"/> or <see cref="StructureDefinition.DifferentialComponent"/> instance.</param>
        /// <returns>A sequence of type profile resource references.</returns>
        public static IEnumerable<string> DistinctTypeProfiles(this IElementList elements)
        {
            return DistinctTypeProfiles(elements.Element);
        }

        /// <summary>Enumerate all distinct external element type profiles.</summary>
        /// <param name="elements">A list of <see cref="ElementDefinition"/> instances.</param>
        /// <returns>A sequence of type profile resource references.</returns>
        public static IEnumerable<string> DistinctTypeProfiles(this IList<ElementDefinition> elements)
        {
            var profiles = elements.SelectMany(e => e.Type).SelectMany(t => t.Profile);
            return profiles.OrderBy(p => p).Distinct();

        }
    }
}
