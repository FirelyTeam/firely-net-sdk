/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;

namespace Hl7.FhirPath.Functions
{
    internal static class CollectionOperators
    {
        public static bool? BooleanEval(this IEnumerable<IElementNavigator> focus)
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


        public static bool Not(this IEnumerable<IElementNavigator> focus)
        {
            return !(focus.BooleanEval().Value);
        }

        public static IEnumerable<IElementNavigator> DistinctUnion(this IEnumerable<IElementNavigator> a, IEnumerable<IElementNavigator> b)
        {
            var result = a.Union(b, new EqualityOperators.ValueProviderEqualityComparer());
            return result;
        }

        //public static IEnumerable<IValueProvider> ConcatUnion(this IEnumerable<IValueProvider> a, IEnumerable<IValueProvider> b)
        //{
        //    return a.Concat(b);
        //}


        public static IEnumerable<IElementNavigator> Item(this IEnumerable<IElementNavigator> focus, int index)
        {
            return focus.Skip(index).Take(1);
        }

        public static IElementNavigator Last(this IEnumerable<IElementNavigator> focus)
        {
            return focus.Reverse().First();
        }

        public static IEnumerable<IElementNavigator> Tail(this IEnumerable<IElementNavigator> focus)
        {
            return focus.Skip(1);
        }

        public static bool Contains(this IEnumerable<IElementNavigator> focus, IElementNavigator value)
        {
            return focus.Contains(value, new EqualityOperators.ValueProviderEqualityComparer());
        }

        public static IEnumerable<IElementNavigator> Distinct(this IEnumerable<IElementNavigator> focus)
        {
            return focus.Distinct(new EqualityOperators.ValueProviderEqualityComparer());
        }

        public static bool IsDistinct(this IEnumerable<IElementNavigator> focus)
        {
            return focus.Distinct(new EqualityOperators.ValueProviderEqualityComparer()).Count() == focus.Count();
        }

        public static bool SubsetOf(this IEnumerable<IElementNavigator> focus, IEnumerable<IElementNavigator> other)
        {
            return focus.All(fitem => other.Contains(fitem));
        }

        public static IEnumerable<IElementNavigator> Navigate(this IEnumerable<IElementNavigator> elements, string name)
        {
            return elements.SelectMany(e => e.Navigate(name));
        }

        public static IEnumerable<IElementNavigator> Navigate(this IElementNavigator nav, string name)
        {
            if (char.IsUpper(name[0]))
            {

                if (!char.IsUpper(nav.Name[0]))
                    throw Error.InvalidOperation("Resource type name may only appear at the root of a document");

                // If we are at a resource, we should match a path that is possibly not rooted in the resource
                // (e.g. doing "name.family" on a Patient is equivalent to "Patient.name.family")   
                // Also we do some poor polymorphism here: Resource.meta.lastUpdated is also allowed.
                var baseClasses = new[] { "Resource", "DomainResource" };
                if (nav.Type == name || baseClasses.Contains(name))
                {
                    return new List<IElementNavigator>() { nav };
                }
                else
                {
                    return Enumerable.Empty<IElementNavigator>();
                }
            }
            else
            {
                return nav.Children(name);
            }
        }
    }
}
