/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.ElementModel;
using Hl7.FhirPath;
using Hl7.FhirPath.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.FhirPath.Functions
{
    public static class EqualityOperators
    {
        public static bool? IsEqualTo(this IEnumerable<ITypedElement> left, IEnumerable<ITypedElement> right, bool compareNames = false)
        {
            // If one or both of the arguments is an empty collection, a comparison operator will return an empty collection.
            // (though we might handle this more generally with the null-propagating functionality of the compiler
            // framework already.
            if (left is null || right is null) return null;

            var r = right.GetEnumerator();

            foreach (var l in left)
            {
                if (!r.MoveNext()) return false;        // number of children not the same
                var comparisonResult = l.IsEqualTo(r.Current, compareNames);
                if (comparisonResult == false) return false;
                if (comparisonResult == null) return null;
            }

            if (r.MoveNext())
                return false;   // number of children not the same
            else
                return true;
        }

        // Note that the Equals as defined by FhirPath/CQL only returns empty when one or both of the arguments
        // are empty. Otherwise, it will return either false or true. Uncomparable values (i.e. datetimes
        // with incompatible precisions) are mapped to false, as are arguments of different types.
        public static bool? IsEqualTo(this ITypedElement left, ITypedElement right, bool compareNames = false)
        {
            // If one or both of the arguments is an empty collection, a comparison operator will return an empty collection.
            // (though we might handle this more generally with the null-propagating functionality of the compiler
            // framework already.
            if (left is null || right is null) return null;

            // TODO: Merge with ElementNodeComparator.IsEqualTo

            if (compareNames && (left.Name != right.Name)) return false;

            var l = left.Value;
            var r = right.Value;

            // TODO: this is actually a cast with knowledge of FHIR->System mappings, we don't want that here anymore
            // Convert quantities
            if (left.InstanceType == "Quantity" && l == null)
                l = Typecasts.ParseQuantity(left);
            if (right.InstanceType == "Quantity" && r == null)
                r = Typecasts.ParseQuantity(right);

            // Compare primitives (or extended primitives)
            if (l != null && r != null && P.Any.TryConvert(l, out var lAny) && P.Any.TryConvert(r, out var rAny))
            {
                return IsEqualTo(lAny, rAny);
            }
            else if (l == null && r == null)
            {
                // Compare complex types (extensions on primitives are not compared, but handled (=ignored) above
                var childrenL = left!.Children();
                var childrenR = right!.Children();

                return childrenL.IsEqualTo(childrenR, compareNames: true);    // NOTE: Assumes null will never be returned when any() children exist
            }
            else
            {
                // Else, we're comparing a complex (without a value) to a primitive which (probably) should return false
                return false;
            }
        }

        public static bool? IsEqualTo(P.Any? left, P.Any? right)
        {
            // If one or both of the arguments is an empty collection, a comparison operator will return an empty collection.
            // (though we might handle this more generally with the null-propagating functionality of the compiler
            // framework already.
            if (left == null || right == null) return null;

            // Try to convert both operands to a common type if they differ.
            // When that fails, the CompareTo function on each type will itself
            // report an error if they cannot handle that.
            // TODO: in the end the engine/compiler will handle this and report an overload resolution fail
            tryCoerce(ref left, ref right);

            return left is P.ICqlEquatable cqle ? cqle.IsEqualTo(right) : null;
        }


        private static bool tryCoerce(ref P.Any left, ref P.Any right)
        {
            left = upcastOne(left, right);
            right = upcastOne(right, left);

            return left.GetType() == right.GetType();

            static P.Any upcastOne(P.Any value, P.Any other) =>
                value switch
                {
                    P.Integer _ when other is P.Long => (P.Long)(P.Integer)value,
                    P.Integer _ when other is P.Decimal => (P.Decimal)(P.Integer)value,
                    P.Integer _ when other is P.Quantity => (P.Quantity)(P.Integer)value,
                    P.Long _ when other is P.Decimal => (P.Decimal)(P.Long)value,
                    P.Long _ when other is P.Quantity => (P.Quantity)(P.Long)value,
                    P.Decimal _ when other is P.Quantity => (P.Quantity)(P.Decimal)value,
                    P.Date _ when other is P.DateTime => (P.DateTime)(P.Date)value,
                    _ => value
                };
        }

        public static bool IsEquivalentTo(this IEnumerable<ITypedElement> left, IEnumerable<ITypedElement> right, bool compareNames = false)
        {
            var r = right.ToList();
            int count = 0;

            foreach (var l in left)
            {
                count += 1;
                if (!r.Any(ri => l.IsEquivalentTo(ri, compareNames))) return false;
            }

            return count == r.Count;
        }


        public static bool IsEquivalentTo(this ITypedElement left, ITypedElement right, bool compareNames = false)
        {
            // Note that because of this behaviour, we should switch off null-propagating behaviour of IsEquivalent to
            if (left is null && right is null) return true;
            if (left is null || right is null) return false;

            if (compareNames && !namesAreEquivalent(left, right)) return false;

            var l = left.Value;
            var r = right.Value;

            // TODO: this is actually a cast with knowledge of FHIR->System mappings, we don't want that here anymore
            // Convert quantities
            if (left.InstanceType == "Quantity" && l == null)
                l = Typecasts.ParseQuantity(left);
            if (right.InstanceType == "Quantity" && r == null)
                r = Typecasts.ParseQuantity(right);

            // Compare primitives (or extended primitives)
            if (l != null && r != null && P.Any.TryConvert(l, out var lAny) && P.Any.TryConvert(r, out var rAny))
            {
                return IsEquivalentTo(lAny, rAny);
            }
            else if (l == null && r == null)
            {
                // Compare complex types (extensions on primitives are not compared, but handled (=ignored) above
                var childrenL = left.Children();
                var childrenR = right.Children();

                return childrenL.IsEquivalentTo(childrenR, compareNames: true);    // NOTE: Assumes null will never be returned when any() children exist
            }
            else
            {
                // Else, we're comparing a complex (without a value) to a primitive which (probably) should return false
                return false;
            }

            static bool namesAreEquivalent(ITypedElement le, ITypedElement ri)
            {
                if (le.Name == "id" && ri.Name == "id") return true;      // IN FHIR: don't compare 'id' elements for equivalence
                if (le.Name != ri.Name) return false;

                return true;
            }
        }

        public static bool IsEquivalentTo(P.Any? left, P.Any? right)
        {
            if (left == null && right == null) return true;
            if (left == null || right == null) return false;

            // Try to convert both operands to a common type if they differ.
            // When that fails, the CompareTo function on each type will itself
            // report an error if they cannot handle that.
            // TODO: in the end the engine/compiler will handle this and report an overload resolution fail
            tryCoerce(ref left, ref right);

            return left is P.ICqlEquatable cqle && cqle.IsEquivalentTo(right);
        }




        public static bool? Compare(P.Any left, P.Any right, string op)
        {
            // If one or both of the arguments is an empty collection, a comparison operator will return an empty collection.
            // (though we might handle this more generally with the null-propagating functionality of the compiler
            // framework already.
            if (left == null || right == null) return null;

            // Try to convert both operands to a common type if they differ.
            // When that fails, the CompareTo function on each type will itself
            // report an error if they cannot handle that.
            // TODO: in the end the engine/compiler will handle this and report an overload resolution fail
            tryCoerce(ref left, ref right);

            if (left is P.ICqlOrderable orderable) return interpret(orderable.CompareTo(right));

            // Now, only the non-comparables are left (coding, concept, boolean).
            // TODO: We should be able to retrieve the cql name of the type, not the
            // dotnet type somehow.
            throw new InvalidOperationException($"Values of type {left.GetType().Name} is not an ordered type and cannot be compared.");

            bool? interpret(int? compareResult)
            {
                if (compareResult is null) return null;

                return op switch
                {
                    "<" => compareResult < 0,
                    "<=" => compareResult <= 0,
                    "=" => compareResult == 0,
                    ">" => compareResult > 0,
                    ">=" => compareResult >= 0,
                    _ => throw new ArgumentException($"Unknown comparison op '{op}'", nameof(op))
                };
            }
        }

        public static readonly IEqualityComparer<ITypedElement> TypedElementEqualityComparer = new ValueProviderEqualityComparer();

        private class ValueProviderEqualityComparer : IEqualityComparer<ITypedElement>
        {
            public bool Equals(ITypedElement? x, ITypedElement? y)
            {
                if (x is null && y is null) return true;
                if (x is null || y is null) return false;

                // TODO: this is not completely correct behaviour
                // The functions Union /Contains/Distinct etc that use
                // this equality should probably also be changed to use
                // 3-valued equality.
                return x.IsEqualTo(y) == true;
            }

            public int GetHashCode(ITypedElement element)
            {
                var result = element.Value != null ? element.Value.GetHashCode() : 0;

                if (element is ITypedElement element1)
                {
                    var childnames = string.Concat(element1.Children().Select(c => c.Name));
                    if (!string.IsNullOrEmpty(childnames))
                        result ^= childnames.GetHashCode();
                }

                return result;
            }
        }
    }
}
