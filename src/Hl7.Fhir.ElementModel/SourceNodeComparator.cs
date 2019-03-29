/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */

using System.Linq;

namespace Hl7.Fhir.ElementModel
{
    public static class SourceNodeComparator
    {
        public static TreeComparisonResult IsEqualTo(this ISourceNode expected, ISourceNode actual)
        {
            if (expected.Name != actual.Name)
                return TreeComparisonResult.Fail(actual.Location, $"name: was '{actual.Name}', expected '{expected.Name}'");
            if (expected.Text != actual.Text) return TreeComparisonResult.Fail(actual.Location, $"value: was '{actual.Text}', expected '{expected.Text}'");
            if (expected.Location != actual.Location) TreeComparisonResult.Fail(actual.Location, $"Path: was '{actual.Location}', expected '{expected.Location}'");

            // Ignore ordering (only relevant to xml)
            var childrenExp = expected.Children().OrderBy(e => e.Name);
            var childrenActual = actual.Children().OrderBy(e => e.Name).GetEnumerator();

            // Don't compare lengths, as this would require complete enumeration of both collections
            // just enumerate through the lists, comparing each item as they go.
            // first fail (or list end) will drop out.
            foreach (var exp in childrenExp)
            {
                if (!childrenActual.MoveNext())
                    TreeComparisonResult.Fail(actual.Location, $"number of children was different");

                var result = exp.IsEqualTo(childrenActual.Current);
                if (!result.Success)
                    return result;
            }
            if (childrenActual.MoveNext())
                TreeComparisonResult.Fail(actual.Location, $"number of children was different");

            return TreeComparisonResult.OK;
        }
    }
}
