/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.ElementModel;

namespace Hl7.FhirPath.Functions
{
    internal static class ThreeValuedLogicExtensions
    {
        // Version of And that does short-cut evaluation using 3V boolean logic
        public static bool? And(this Func<bool?> left, Func<bool?> right)
        {
            var l = left();

            if (l == false) return false;       // short-cut, no need to evaluate right

            return l.And(right());
        }

        // Version of And that does direct evaluation using 3V boolean logic
        public static bool? And(this bool? left, bool? right)
        {
            if (left == false || right == false) return false;
            if (left == true && right == true) return true;

            return null;
        }

        // Version of Or that does short-cut evaluation using 3V boolean logic
        public static bool? Or(this Func<bool?> left, Func<bool?> right)
        {
            var l = left();

            if (l == true) return true;       // short-cut, no need to evaluate right

            return l.Or(right());
        }

        // Version of Or that does direct evaluation using 3V boolean logic
        public static bool? Or(this bool? left, bool? right)
        {
            if (left == true || right == true) return true;
            if (left == false && right == false) return false;

            return null;
        }

        // Version of XOr that does short-cut evaluation using 3V boolean logic
        public static bool? XOr(this Func<bool?> left, Func<bool?> right)
        {
            var l = left();

            if (l == null) return null;       // short-cut, no need to evaluate right

            return l.XOr(right());
        }

        // Version of XOr that does direct evaluation using 3V boolean logic
        public static bool? XOr(this bool? left, bool? right)
        {
            if (left == null || right == null) return null;

            return left.Value ^ right.Value;
        }

        // Version of Implies that does short-cut evaluation using 3V boolean logic
        public static bool? Implies(this Func<bool?> left, Func<bool?> right)
        {
            var l = left();

            if (l == false) return true;       // short-cut, no need to evaluate right

            return l.Implies(right());
        }

        // Version of Implies that does direct evaluation using 3V boolean logic
        public static bool? Implies(this bool? left, bool? right)
        {
            if (left == false) return true;
            if (right == true) return true;
            if (left == true && right == false) return false;

            return null;
        }

        public static IEnumerable<ITypedElement> IIf(this IEnumerable<ITypedElement> focus, bool? condition, IEnumerable<ITypedElement> result)
        {
            return IIf(focus, condition, result, null);
        }

        public static IEnumerable<ITypedElement> IIf(this IEnumerable<ITypedElement> focus, bool? condition, IEnumerable<ITypedElement> result, IEnumerable<ITypedElement> otherwise)
        {
            if (condition == true)
                return result;
            else
                return otherwise;
        }
    }

}
