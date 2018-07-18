/* 
 * Copyright (c) 2017, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Serialization;
using System;
using System.Linq;
using static Hl7.Fhir.Utility.ElementNavigatorComparator;

namespace Hl7.Fhir.Utility
{
    public static class SourceNavigatorComparator
    {
        public static ComparisonResult IsEqualTo(this ISourceNavigator expected, ISourceNavigator actual)
        {
            if (!namesEqual(expected.Name, actual.Name)) return ComparisonResult.Fail(actual.Path, $"name: was '{actual.Name}', expected '{expected.Name}'");
            if (expected.Text != actual.Text) return ComparisonResult.Fail(actual.Path, $"value: was '{actual.Text}', expected '{expected.Text}'");
            if (expected.Path != actual.Path) ComparisonResult.Fail(actual.Path, $"Path: was '{actual.Path}', expected '{expected.Path}'");

            // Ignore ordering (only relevant to xml)
            var childrenExp = expected.Children().OrderBy(e => e.Name);
            var childrenActual = actual.Children().OrderBy(e => e.Name).GetEnumerator();

            // Don't compare lengths, as this would require complete enumeration of both collections
            // just enumerate through the lists, comparing each item as they go.
            // first fail (or list end) will drop out.
            foreach (var exp in childrenExp)
            {
                if (!childrenActual.MoveNext())
                    ComparisonResult.Fail(actual.Path, $"number of children was different");

                var result = exp.IsEqualTo(childrenActual.Current);
                if (!result.Success)
                    return result;
            }
            if (childrenActual.MoveNext())
                ComparisonResult.Fail(actual.Path, $"number of children was different");

            return ComparisonResult.OK;

            bool namesEqual(string e, string a) => e == a || (a != null && e != null && (a.StartsWith(e)));
        }
    }
}
