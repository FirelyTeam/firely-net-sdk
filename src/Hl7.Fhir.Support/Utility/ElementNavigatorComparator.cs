/* 
 * Copyright (c) 2017, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Support.Utility
{
    public static class ElementNavigatorComparator
    {
        public struct ComparisonResult
        {
            public bool Success;
            public string FailureLocation;
            public string Details;

            public static ComparisonResult Fail(string location, string details=null) =>
                new ComparisonResult { Success = false, FailureLocation = location, Details = details };

            public static readonly ComparisonResult OK = new ComparisonResult() { Success = true };
        }


        public static ComparisonResult IsEqualTo(this IElementNavigator expected, IElementNavigator actual)
        {
            if (!Object.Equals(expected.Value,actual.Value)) return ComparisonResult.Fail(actual.Location, $"value: was '{actual.Value}', expected '{expected.Value}'");
            if (expected.Name != actual.Name) return ComparisonResult.Fail(actual.Location, $"name: was '{actual.Name}', expected '{expected.Name}'");
            if (expected.Type != actual.Type) return ComparisonResult.Fail(actual.Location, $"type: was '{actual.Type}', expected '{expected.Type}'");
            if (expected.Location != actual.Location) ComparisonResult.Fail(actual.Location, $"location: was '{actual.Location}', expected '{expected.Location}'");

            // Ignore ordering (only relevant to xml)
            var childrenExp = expected.Children().OrderBy(e=>e.Name).ToArray();
            var childrenActual = actual.Children().OrderBy(e=>e.Name).ToArray();

            if (childrenExp.Length != childrenActual.Length) ComparisonResult.Fail(actual.Location, $"number of children was {childrenActual.Length}, expected {childrenExp.Length}");

            for(var index=0; index<childrenExp.Length; index++)
            {
                var result = childrenExp[index].IsEqualTo(childrenActual[index]);
                if (!result.Success) return result;
            }

            return ComparisonResult.OK;
        }
    }
}
