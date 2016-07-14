/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using Hl7.Fhir.FluentPath.Binding;
using Hl7.Fhir.Support;
using HL7.Fhir.FluentPath;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.FluentPath
{
    public static class IValueProviderListExtensions
    {

        public static IEnumerable<IElementNavigator> JustElements(this IEnumerable<IValueProvider> focus)
        {
            return focus.OfType<IElementNavigator>();
        }

    
        // Evaluate a collection as a boolean as described in "4.1 Boolean evaluation of collections"
        public static bool? BooleanEval(this IEnumerable<IValueProvider> focus)
        {
            if (!focus.Any()) return null;

            if (focus.Count() == 1 && focus.Single().Value is bool)
            {
                return (bool)focus.Single().Value;
            }

            // Otherwise, we have "some" content, which we'll consider "true"
            else
                return true;
        }

     
        public static bool Not(this IEnumerable<IValueProvider> focus)
        {
            return !(focus.BooleanEval().Value);
        }

        public static long CountItems(this IEnumerable<IValueProvider> focus)
        {
            return focus.Count();
        }


        public static IEnumerable<IValueProvider> Item(this IEnumerable<IValueProvider> focus, int index)
        {
            return focus.Skip(index).Take(1);
        }


     
        //public static IEnumerable<IValueProvider> Any(this IEnumerable<IValueProvider> focus,
        //Func<IEnumerable<IValueProvider>, IEnumerable<IValueProvider>> condition)
        //{
        //    return FhirValueList.Create(focus.Any(v => condition(FhirValueList.Create(v)).booleanEval()));
        //}

        //public static IEnumerable<IValueProvider> All(this IEnumerable<IValueProvider> focus,
        //        Func<IEnumerable<IValueProvider>, IEnumerable<IValueProvider>> condition)
        //{
        //    return FhirValueList.Create(focus.All(v => condition(FhirValueList.Create(v)).booleanEval()));
        //}


        //public static IEnumerable<IValueProvider> Select(this IEnumerable<IValueProvider> focus,
        //        Func<IEnumerable<IValueProvider>, IEnumerable<IValueProvider>> mapper)
        //{
        //    return focus.SelectMany(v => mapper(FhirValueList.Create(v)));
        //}


        //public static IEnumerable<IValueProvider> Distinct(this IEnumerable<IValueProvider> focus)
        //{
        //    return focus.Distinct(new FhirPathValueEqualityComparer());
        //}


        //public static IEnumerable<IFluentPathElement> Resolve(this IEnumerable<IFluentPathValue> focus, FhirClient client)
        //{
        //    return focus.Resolve(new BaseEvaluationContext(client));
        //}

        //public static IEnumerable<IFluentPathElement> Resolve(this IEnumerable<IFluentPathValue> focus, IEvaluationContext context)
        //{
        //    foreach (var item in focus)
        //    {
        //        string url = null;

        //        // Something that looks like a Reference
        //        if (item is IFluentPathElement)
        //        {
        //            var maybeReference = ((IFluentPathElement)item).Children("reference").SingleOrDefault();

        //            if (maybeReference != null && maybeReference.Value != null && maybeReference.Value is string)
        //                url = maybeReference.AsString();
        //        }

        //        // A string as a direct url
        //        if (item.Value != null && item.Value is string)
        //            url = item.AsString();

        //        if(url != null)
        //            yield return context.ResolveResource(url);
        //    }
        //}



        //public static IEnumerable<IValueProvider> Extension(this IEnumerable<IValueProvider> focus, string url)
        //{            
        //    return focus.Children("extension").Where(es => es.Children("url").IsEqualTo(url));
        //}

        public static IEnumerable<IElementNavigator> Children(this IEnumerable<IValueProvider> focus, string name)
        {
            return focus.JustElements().SelectMany(node => node.EnumerateChildrenByName(name));
        }

        //public static IEnumerable<IFhirPathElement> Child(this IEnumerable<IFhirPathValue> focus, string name)
        //{
        //    return focus.JustFhirPathElements().Child(name);
        //}

        //public static IEnumerable<IFhirPathElement> Child(this IEnumerable<IFhirPathElement> focus, string name)
        //{
        //    return focus.SelectMany(node => node.Children().Where(child => child.IsMatch(name)));
        //}


        public static IEnumerable<IElementNavigator> Children(this IValueProvider focus)
        {
            if (focus is IElementNavigator)
            {
                return ((IElementNavigator)focus).EnumerateChildren();
            }

            return Enumerable.Empty<IElementNavigator>();
        }

        public static IEnumerable<IElementNavigator> Children(this IEnumerable<IValueProvider> focus)
        {
            return focus.JustElements().SelectMany(node => node.EnumerateChildren());
        }

        public static IEnumerable<IElementNavigator> Descendants(this IEnumerable<IElementNavigator> focus)
        {
            return focus.SelectMany(node => node.Descendants());
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

        //class FhirPathValueEqualityComparer : IEqualityComparer<IValueProvider>
        //{
        //    public bool Equals(IValueProvider x, IValueProvider y)
        //    {
        //        var res = x.IsEqualTo(y);
        //        return res != null && res.Value is bool &&  ((bool)res.Value) == true;
        //    }

        //    public int GetHashCode(IValueProvider value)
        //    {
        //        var result = value.Value != null ? value.Value.GetHashCode() : 0;

        //        if (value is IElementNavigator)
        //        {
        //            result ^= (((IElementNavigator)value).GetChildNames().SingleOrDefault() ?? "key").GetHashCode();
        //        }

        //        return result;
        //    }
        //}
    }
}


