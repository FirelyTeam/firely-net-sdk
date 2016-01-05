/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.FhirPath
{
    public static class Operations
    {

        public static IEnumerable<IFhirPathElement> JustFhirPathElements(this IEnumerable<IFhirPathValue> focus)
        {
            return focus.OfType<IFhirPathElement>();
        }
   
        public static bool AsBoolean(this IEnumerable<IFhirPathValue> focus)
        {
            var result = false;

            // An empty result is considered "false"
            if (!focus.Any())
                result = false;

            // A single result that's a boolean should be interpreted as a boolean
            else if (focus.Count() == 1 && focus.Single().Value is Boolean)
            {
                return (bool)focus.Single().Value;
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

        public static IFhirPathValue ItemAt(this IEnumerable<IFhirPathValue> focus, int index)
        {
            return focus.Skip(index).FirstOrDefault();
        }

        public static IEnumerable<IFhirPathElement> Children(this IEnumerable<IFhirPathValue> focus, string name)
        {
            return focus.JustFhirPathElements().SelectMany(node => node.Children(name));
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
            return focus.JustFhirPathElements().SelectMany(node => node.Children());
        }

        public static bool IsEqualTo(this IEnumerable<IFhirPathValue> us, IEnumerable<IFhirPathValue> them)
        {
            if (!us.Any() && !them.Any()) return true;
            if (us.Count() != them.Count()) return false;

            return us.Zip(them, (left, right) => left.IsEqualTo(right)).All(r => r == true);
        }

        public static IEnumerable<IFhirPathValue> Add(this IEnumerable<IFhirPathValue> left, IEnumerable<IFhirPathValue> right)
        {
            if (left.Count() == 1 && right.Count() == 1)
                yield return left.Single().Add(right.Single());
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


