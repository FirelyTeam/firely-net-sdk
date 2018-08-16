/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using System;
using System.Linq;

namespace Hl7.Fhir.Validation
{
    internal static class FixedPatternValidationExtensions
    {
        public static OperationOutcome ValidateFixed(this Validator v, ElementDefinition definition, ITypedElement instance)
        {
            var outcome = new OperationOutcome();

            if (definition.Fixed != null)
            {
                ITypedElement fixedValueNav = definition.Fixed.ToTypedElement();

                if (!instance.IsExactlyEqualTo(fixedValueNav))
                {
                    v.Trace(outcome, $"Value is not exactly equal to fixed value '{toReadable(definition.Fixed)}'",
                            Issue.CONTENT_DOES_NOT_MATCH_FIXED_VALUE, instance);
                }
            }

            return outcome;
        }

        public static OperationOutcome ValidatePattern(this Validator v, ElementDefinition definition, ITypedElement instance)
        {
            var outcome = new OperationOutcome();

            if (definition.Pattern != null)
            {
                ITypedElement patternValueNav = definition.Pattern.ToTypedElement();

                if (!instance.Matches(patternValueNav))
                {
                    v.Trace(outcome, $"Value does not match pattern '{toReadable(definition.Pattern)}'",
                            Issue.CONTENT_DOES_NOT_MATCH_PATTERN_VALUE, instance);
                }
            }

            return outcome;
        }


        private static string toReadable(Base value)
        {
            if (value is Primitive)
                return value.ToString();
            else
                return new FhirJsonSerializer().SerializeToString(value);
        }

        public static bool IsExactlyEqualTo(this ITypedElement left, ITypedElement right)
        {
            if (left == null && right == null) return true;
            if (left == null || right == null) return false;

            if (!ValueEquality(left.Value, right.Value)) return false;

            // Compare the children.
            var childrenL = left.Children();
            var childrenR = right.Children();

            if (childrenL.Count() != childrenR.Count()) return false;

            return childrenL.Zip(childrenR, 
                            (childL, childR) => childL.Name == childR.Name && childL.IsExactlyEqualTo(childR)).All(t => t);
        }


        public static bool ValueEquality<T1, T2>(T1 val1, T2 val2)
        {
            // Compare the value
            if (val1 == null && val2 == null) return true;
            if (val1 == null || val2 == null) return false;

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



        public static bool Matches(this ITypedElement value, ITypedElement pattern)
        {
            if (value == null && pattern == null) return true;
            if (value == null || pattern == null) return false;
            
            if (!ValueEquality(value.Value, pattern.Value)) return false;

            // Compare the children.
            var valueChildren = value.Children();
            var patternChildren = pattern.Children();

            return patternChildren.All(patternChild => valueChildren.Any(valueChild =>
                  patternChild.Name == valueChild.Name && valueChild.Matches(patternChild)));

        }
    }
}
