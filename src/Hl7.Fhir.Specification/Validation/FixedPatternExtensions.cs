/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.ElementModel;
using Hl7.Fhir.FluentPath;
using Hl7.Fhir.Model;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Validation
{
    internal static class FixedPatternExtensions
    {
        public static OperationOutcome ValidateFixed(this Validator v, ElementDefinition definition, IElementNavigator instance)
        {
            var outcome = new OperationOutcome();

            if (definition.Fixed != null)
            {
                // Construct an IValueProvider based on the POCO parsed from profileElement.fixed/pattern etc.
                IElementNavigator fixedValueNav = new PocoNavigator(definition.Fixed);

                outcome.Verify(() => instance.IsExactlyEqualTo(fixedValueNav), "Value is not exactly equal to fixed value '{0}'"
                            .FormatWith(definition.Fixed.ToString()), Issue.CONTENT_DOES_NOT_MATCH_FIXED_VALUE, instance);
            }

            return outcome;
        }


        public static bool IsExactlyEqualTo(this IEnumerable<IElementNavigator> left, IEnumerable<IElementNavigator> right)
        {
            if (left.Count() != right.Count()) return false;

            return left.Zip(right, (l, r) => l.IsExactlyEqualTo(r)).All(x => x);
        }

        public static bool ValueEquality<T1, T2>(T1 val1, T2 val2)
        {
            try
            {
                // convert val2 to type of val1.
                T1 boxed2 = (T1)Convert.ChangeType(val2, typeof(T1));

                // compare now that same type.
                return val1.Equals(boxed2);
            }
            catch
            {
                return false;
            }
        }

        public static bool IsExactlyEqualTo(this IElementNavigator left, IElementNavigator right)
        {
            if (left == null && right == null) return true;
            if (left == null || right == null) return false;
            
            var l = left.Value;
            var r = right.Value;

            // Compare the value
            if (l == null && r != null) return false;
            if (l != null && r == null) return false;

            if (l != null && r != null)
                if (!ValueEquality(l, r)) return false;

            // Compare the children.
            var childrenL = left.Children();
            var childrenR = right.Children();

            bool allNamesAreEqual = childrenL.Zip(childrenR, (childL, childR) => childL.Name == childR.Name).All(t => t);

            return allNamesAreEqual &&
                    childrenL.IsExactlyEqualTo(childrenR);    // NOTE: Assumes null will never be returned when any() children exist
        }
    }
}
