/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Hl7.FhirPath.Functions
{
    internal static class CollectionOperators
    {
        /// <summary>
        /// Evaluates a collection as a single boolean, following the FHIRPath "singleton evaluation of
        /// collections" rules for places where an operator or function expects a boolean (criteria of
        /// where(), exists(), all(), iif(), operands of and/or/xor/implies, ...).
        /// </summary>
        /// <returns><c>null</c> for an empty collection, the value of a single boolean, or <c>true</c> for
        /// any other single item.</returns>
        /// <exception cref="InvalidOperationException">The collection has more than one item.</exception>
        public static bool? BooleanEval(this IEnumerable<PocoNode> focus) => booleanEval(focus, lenient: false);

        /// <summary>
        /// Converts the final result of an expression to a boolean, the way the public helpers
        /// (<c>Predicate</c>, <c>IsTrue</c>, <c>IsBoolean</c>) and the conformance test runner do. Unlike
        /// <see cref="BooleanEval"/> this does not raise an error for a result with more than one item,
        /// but treats it as <c>true</c>, like <c>convertToBoolean()</c> in the Java reference engine.
        /// </summary>
        /// <returns><c>null</c> for an empty collection, the value of a single boolean, or <c>true</c>
        /// otherwise.</returns>
        public static bool? LenientBooleanEval(this IEnumerable<PocoNode> focus) => booleanEval(focus, lenient: true);

        private static bool? booleanEval(IEnumerable<PocoNode> focus, bool lenient)
        {
            // Strict: the first two items are enough to know whether there is more than one. Lenient: read every
            // item, so that an error raised while evaluating a later item of a lazy result (e.g. the criteria of a
            // where()) is still reported, as before and as in the Java reference engine.
            var items = lenient ? focus.ToArray() : focus.Take(2).ToArray();

            return items.Length switch
            {
                0 => null,
                1 => items[0] is PrimitiveNode { Value: bool boolean } ? boolean : true,
                _ => lenient
                    ? true
                    : throw Error.InvalidOperation(
                        "Unable to evaluate as a boolean: the collection has more than one item, where a single boolean was expected."),
            };
        }


        // Empty input is handled by null propagation (doNullProp), and BooleanEval raises the error for
        // a collection with more than one item.
        public static bool Not(this IEnumerable<PocoNode> focus)
            => !focus.BooleanEval().Value;
        
        public static IEnumerable<PocoNode> DistinctUnion(this IEnumerable<PocoNode> a, IEnumerable<PocoNode> b)
            => a.Union<PocoNode>(b, EqualityOperators.TypedElementEqualityComparer);

        /// <summary>
        /// Returns the item at the given (0-based) index, or an empty collection when the index
        /// is out of range (including negative indices).
        /// </summary>
        public static IEnumerable<PocoNode> Item(this IEnumerable<PocoNode> focus, int index)
            => index < 0 ? [] : focus.Skip(index).Take(1);

        public static PocoNode Last(this IEnumerable<PocoNode> focus)
            => focus.Reverse().First();

        public static IEnumerable<PocoNode> Tail(this IEnumerable<PocoNode> focus)
            => focus.Skip(1);

        public static bool Contains(this IEnumerable<PocoNode> focus, PocoNode value)
            => focus.Contains(value, EqualityOperators.TypedElementEqualityComparer);
        
        public static IEnumerable<PocoNode> Distinct(this IEnumerable<PocoNode> focus)
            => focus.Distinct<PocoNode>(EqualityOperators.TypedElementEqualityComparer);

        public static bool IsDistinct(this IEnumerable<PocoNode> focus)
            => focus.Distinct(EqualityOperators.TypedElementEqualityComparer).Count() == focus.Count();

        public static bool SubsetOf(this IEnumerable<PocoNode> focus, IEnumerable<PocoNode> other)
            => focus.All(fitem => other.Contains(fitem));
        
        public static IEnumerable<PocoNode> Intersect(this IEnumerable<PocoNode> focus, IEnumerable<PocoNode> other)
            => focus.Intersect<PocoNode>(other, EqualityOperators.TypedElementEqualityComparer);

        public static IEnumerable<PocoNode> Exclude(this IEnumerable<PocoNode> focus, IEnumerable<PocoNode> other)
            => focus.Where(f => !other.Contains(f));

        public static int IndexOf(this IEnumerable<PocoNode> focus, PocoNode item, int start = 0)
        {
            var typedElements = focus as PocoNode[] ?? focus.ToArray();
            for (int i = start; i < typedElements.Length; i++)
            {
                if (EqualityOperators.TypedElementEqualityComparer.Equals(typedElements[i], item))
                {
                    return i;
                }
            }
            return -1;
        }

        public static int LastIndexOf(this IEnumerable<PocoNode> focus, PocoNode item, int to = -1)
        {
            var typedElements = focus as PocoNode[] ?? focus.ToArray();
            to = to < 0 ? typedElements.Count() - 1 : to;
            for (int i = to; i >= 0; i--)
            {
                if (EqualityOperators.TypedElementEqualityComparer.Equals(typedElements[i], item))
                {
                    return i;
                }
            }
            return -1;
        }
        

        public static IEnumerable<PocoNode> Navigate(this IEnumerable<PocoNode> elements, string name)
            => elements.SelectMany(e => e.Navigate(name));

        public static IEnumerable<PocoNode> Navigate(this PocoNode element, string name)
        {
            if (char.IsUpper(name[0]))
            {
                // If we are at a resource, we should match a path that is possibly not rooted in the resource
                // (e.g. doing "name.family" on a Patient is equivalent to "Patient.name.family")   
                if(element.Is(name))
                {
                    return new List<PocoNode>() { element };
                }
            }

            // Handle .value property on FHIR primitives to access the actual primitive value
            if (name == "value" && element is PrimitiveNode primitiveNode)
            {
                // Return the primitive value as a FhirPath system type
                if (primitiveNode.Value is not null)
                {
                    return new[] { PocoNode.ForAnyPrimitive(primitiveNode.Value) };
                }
                return Enumerable.Empty<PocoNode>();
            }

            return element.Child(name) ?? Enumerable.Empty<PocoNode>();
        }

        public static string FpJoin(this IEnumerable<PocoNode> collection, string separator = null)
        {
            //if the collection is empty return the empty result
            if (!collection.Any())
                return string.Empty;

            //only join collections with string values inside
            if (!collection.All(c => c.GetValue() is string))
                throw Error.InvalidOperation("Join function can only be performed on string collections.");

            var values = collection.Select(n => n.GetValue());
            return string.Join(separator, values);
        }
    }
}
