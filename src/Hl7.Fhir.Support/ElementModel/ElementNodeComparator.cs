/* 
 * Copyright (c) 2018, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using System;
using System.Linq;
using static Hl7.Fhir.ElementModel.ElementNavigatorComparator;

namespace Hl7.Fhir.Utility
{
    public static class ElementNodeComparator
    {
        public static ComparisonResult IsEqualTo(this ITypedElement expected, ITypedElement actual)
        {
            if (expected.Name != actual.Name)
                return ComparisonResult.Fail(actual.Location, $"name: was '{actual.Name}', expected '{expected.Name}'");
            if (!Object.Equals(expected.Value, actual.Value))
                return ComparisonResult.Fail(actual.Location, $"value: was '{actual.Value}', expected '{expected.Value}'");
            if (expected.InstanceType != actual.InstanceType && actual.InstanceType != null) return ComparisonResult.Fail(actual.Location, $"type: was '{actual.InstanceType}', expected '{expected.InstanceType}'");
            if (expected.Location != actual.Location) ComparisonResult.Fail(actual.Location, $"Path: was '{actual.Location}', expected '{expected.Location}'");

            // Ignore ordering (only relevant to xml)
            var childrenExp = expected.Children().OrderBy(e => e.Name);
            var childrenActual = actual.Children().OrderBy(e => e.Name).GetEnumerator();

            // Don't compare lengths, as this would require complete enumeration of both collections
            // just enumerate through the lists, comparing each item as they go.
            // first fail (or list end) will drop out.
            foreach (var exp in childrenExp)
            {
                if (!childrenActual.MoveNext())
                    ComparisonResult.Fail(actual.Location, $"number of children was different");

                var result = exp.IsEqualTo(childrenActual.Current);
                if (!result.Success)
                    return result;
            }
            if (childrenActual.MoveNext())
                ComparisonResult.Fail(actual.Location, $"number of children was different");

            return ComparisonResult.OK;
        }
    }
}
