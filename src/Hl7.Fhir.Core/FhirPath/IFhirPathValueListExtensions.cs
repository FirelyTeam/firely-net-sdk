/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.FhirPath
{
    public static class IFhirPathValueListExtensions
    {

        public static IEnumerable<IFhirPathElement> JustElements(this IEnumerable<IFhirPathValue> focus)
        {
            return focus.OfType<IFhirPathElement>();
        }

        public static IEnumerable<IFhirPathValue> JustValues(this IEnumerable<IFhirPathValue> focus)
        {
            return focus.Where(f => f.Value != null);
        }
   
        public static bool AsBoolean(this IEnumerable<IFhirPathValue> focus)
        {
            var result = false;

            // An empty result is considered "false"
            if (!focus.Any())
                result = false;

            // A single result that's a boolean should be interpreted as a boolean
            else if (focus.JustValues().Count() == 1 && focus.JustValues().Single().Value is Boolean)
            {
                return focus.Single().AsBool();
            }

            // Otherwise, we have "some" content, which we'll consider "true"
            else
                result = true;

            return result;
        }

        public static bool Empty(this IEnumerable<IFhirPathValue> focus)
        {
            return !focus.Any();
        }

        public static IFhirPathValue Item(this IEnumerable<IFhirPathValue> focus, int index)
        {
            return focus.Skip(index).FirstOrDefault();
        }

        public static long? AsInteger(this IEnumerable<IFhirPathValue> focus)
        {
            if (focus.JustValues().Count() == 1)
            {
                var val = focus.Single().Value;
                if(val != null)
                {
                    if (val is long) return (long)val;
                    //if (val is decimal) return (Int64)Math.Round((decimal)val);
                    if (val is string)
                    {
                        long result;
                        if (Int64.TryParse((string)val, out result))
                            return result;
                    }

                }
            }

            return null;
        }


        public static IEnumerable<IFhirPathValue> StartingWith(this IEnumerable<IFhirPathValue> focus, string prefix)
        {
            return focus.JustValues().Where(value => value.AsStringRepresentation().StartsWith(prefix));
        }

        public static IEnumerable<IFhirPathElement> Resolve(this IEnumerable<IFhirPathValue> focus, FhirClient client)
        {
            return focus.Resolve(new EvaluationContext(client));
        }

        public static IEnumerable<IFhirPathElement> Resolve(this IEnumerable<IFhirPathValue> focus, IEvaluationContext context)
        {
            foreach (var item in focus)
            {
                string url = null;

                // Something that looks like a Reference
                if (item is IFhirPathElement)
                {
                    var maybeReference = ((IFhirPathElement)item).Children("reference").SingleOrDefault();

                    if (maybeReference != null && maybeReference.Value != null && maybeReference.Value is string)
                        url = maybeReference.AsString();
                }

                // A string as a direct url
                if (item.Value != null && item.Value is string)
                    url = item.AsString();

                if(url != null)
                    yield return context.ResolveResource(url);
            }
        }

        public static int MaxLength(this IEnumerable<IFhirPathValue> focus)
        {
            return focus.JustValues()
                .Aggregate(0, (val, item) => Math.Max(item.AsStringRepresentation().Length, val));
        }

        public static bool SubsetOf(this IEnumerable<IFhirPathValue> left, IEnumerable<IFhirPathValue> right)
        {
            return left.All(l => right.Any(r => l.IsEqualTo(r)));
        }

        public static IEnumerable<IFhirPathElement> Children(this IEnumerable<IFhirPathValue> focus, string name)
        {
            return focus.JustElements().SelectMany(node => node.Children(name));
        }

        //public static IEnumerable<IFhirPathElement> Child(this IEnumerable<IFhirPathValue> focus, string name)
        //{
        //    return focus.JustFhirPathElements().Child(name);
        //}

        //public static IEnumerable<IFhirPathElement> Child(this IEnumerable<IFhirPathElement> focus, string name)
        //{
        //    return focus.SelectMany(node => node.Children().Where(child => child.IsMatch(name)));
        //}


        public static IEnumerable<IFhirPathElement> Children(this IFhirPathValue focus)
        {
            if (focus is IFhirPathElement)
            {
                return ((IFhirPathElement)focus).Children();
            }

            return Enumerable.Empty<IFhirPathElement>();
        }

        public static IEnumerable<IFhirPathElement> Children(this IEnumerable<IFhirPathValue> focus)
        {
            return focus.JustElements().SelectMany(node => node.Children());
        }

        public static IEnumerable<IFhirPathElement> Descendants(this IEnumerable<IFhirPathElement> focus)
        {
            return focus.SelectMany(node => node.Descendants());
        }

        public static IEnumerable<IFhirPathElement> Parents(this IEnumerable<IFhirPathElement> focus)
        {
            return focus.Select(node => node.Parent);
        }

        public static bool IsEqualTo(this IEnumerable<IFhirPathValue> left, IEnumerable<IFhirPathValue> right)
        {
            if (!left.Any() && !right.Any()) return true;
            if (left.Count() != right.Count()) return false;

            return left.Zip(right, (l, r) => l.IsEqualTo(r)).All(x => x == true);
        }

        public static bool IsEquivalentTo(this IEnumerable<IFhirPathValue> left, IEnumerable<IFhirPathValue> right)
        {
            if (!left.Any() && !right.Any()) return true;
            if (left.Count() != right.Count()) return false;

            return left.All(l => right.Any(r => l.IsEquivalentTo(r)));
        }


        public static IEnumerable<IFhirPathValue> GreaterThan(this IEnumerable<IFhirPathValue> left, IEnumerable<IFhirPathValue> right)
        {
            if (left.Count() == 1 && right.Count() == 1)
                yield return left.Single().GreaterThan(right.Single());
        }

        public static IEnumerable<IFhirPathValue> GreaterOrEqual(this IEnumerable<IFhirPathValue> left, IEnumerable<IFhirPathValue> right)
        {
            if (left.Count() == 1 && right.Count() == 1)
                yield return left.Single().GreaterOrEqual(right.Single());
        }

        public static IEnumerable<IFhirPathValue> LessThan(this IEnumerable<IFhirPathValue> left, IEnumerable<IFhirPathValue> right)
        {
            if (left.Count() == 1 && right.Count() == 1)
                yield return left.Single().LessThan(right.Single());
        }

        public static IEnumerable<IFhirPathValue> LessOrEqual(this IEnumerable<IFhirPathValue> left, IEnumerable<IFhirPathValue> right)
        {
            if (left.Count() == 1 && right.Count() == 1)
                yield return left.Single().LessOrEqual(right.Single());
        }

        public static IEnumerable<IFhirPathValue> Add(this IEnumerable<IFhirPathValue> left, IEnumerable<IFhirPathValue> right)
        {
            if (left.Count() == 1 && right.Count() == 1)
                yield return left.Single().Add(right.Single());
        }

        public static IEnumerable<IFhirPathValue> Sub(this IEnumerable<IFhirPathValue> left, IEnumerable<IFhirPathValue> right)
        {
            if (left.Count() == 1 && right.Count() == 1)
                yield return left.Single().Sub(right.Single());
        }

        public static IEnumerable<IFhirPathValue> Mul(this IEnumerable<IFhirPathValue> left, IEnumerable<IFhirPathValue> right)
        {
            if (left.Count() == 1 && right.Count() == 1)
                yield return left.Single().Mul(right.Single());
        }

        public static IEnumerable<IFhirPathValue> Div(this IEnumerable<IFhirPathValue> left, IEnumerable<IFhirPathValue> right)
        {
            if (left.Count() == 1 && right.Count() == 1)
                yield return left.Single().Div(right.Single());
        }

        ///// <summary>Enumerate the descendants of the specified nodes.</summary>
        ///// <typeparam name="T">The type of a tree node.</typeparam>
        ///// <param name="nodeSet">A set of tree nodes.</param>
        ///// <returns>An enumerator for nodes of type <typeparamref name="T"/>.</returns>
        //public static IEnumerable<T> Descendants<T>(this IEnumerable<T> nodeSet) where T : FhirInstanceTree
        //{
        //    return nodeSet.SelectMany(node => node.Descendants());
        //}
        ///// <summary>Enumerate the specified nodes and their descendants.</summary>
        ///// <typeparam name="T">The type of a tree node.</typeparam>
        ///// <param name="nodeSet">A set of tree nodes.</param>
        ///// <returns>An enumerator for nodes of type <typeparamref name="T"/>.</returns>
        //public static IEnumerable<T> DescendantsAndSelf<T>(this IEnumerable<T> nodeSet) where T : FhirInstanceTree
        //{
        //    return nodeSet.SelectMany(node => node.DescendantsAndSelf());
        //}

        ///// <summary>Enumerate the ancestors of the specified nodes.</summary>
        ///// <typeparam name="T">The type of a tree node.</typeparam>
        ///// <param name="nodeSet">A set of tree nodes.</param>
        ///// <returns>An enumerator for nodes of type <typeparamref name="T"/>.</returns>
        //public static IEnumerable<T> Ancestors<T>(this IEnumerable<T> nodeSet) where T : FhirInstanceTree
        //{
        //    return nodeSet.SelectMany(node => node.Ancestors());
        //}

        ///// <summary>Enumerate the specified nodes and their ancestors.</summary>
        ///// <typeparam name="T">The type of a tree node.</typeparam>
        ///// <param name="nodeSet">A set of tree nodes.</param>
        ///// <returns>An enumerator for nodes of type <typeparamref name="T"/>.</returns>
        //public static IEnumerable<T> AncestorsAndSelf<T>(this IEnumerable<T> nodeSet) where T : FhirInstanceTree
        //{
        //    return nodeSet.SelectMany(node => node.AncestorsAndSelf());
        //}

        ///// <summary>Enumerate the siblings preceding the specified nodes.</summary>
        ///// <typeparam name="T">The type of a tree node.</typeparam>
        ///// <param name="nodeSet">A set of tree nodes.</param>
        ///// <returns>An enumerator for nodes of type <typeparamref name="T"/>.</returns>
        //public static IEnumerable<T> PrecedingSiblings<T>(this IEnumerable<T> nodeSet) where T : FhirInstanceTree
        //{
        //    return nodeSet.SelectMany(node => node.PrecedingSiblings());
        //}

        ///// <summary>Enumerate the siblings following the specified nodes.</summary>
        ///// <typeparam name="T">The type of a tree node.</typeparam>
        ///// <param name="nodeSet">A set of tree nodes.</param>
        ///// <returns>An enumerator for nodes of type <typeparamref name="T"/>.</returns>
        //public static IEnumerable<T> FollowingSiblings<T>(this IEnumerable<T> nodeSet) where T : FhirInstanceTree
        //{
        //    return nodeSet.SelectMany(node => node.PrecedingSiblings());
        //}
    }
}


