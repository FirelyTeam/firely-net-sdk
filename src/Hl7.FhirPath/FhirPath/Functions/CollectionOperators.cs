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
        public static bool? BooleanEval(this IEnumerable<ITypedElement> focus)
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


        public static bool Not(this IEnumerable<ITypedElement> focus)
        {
            return !(focus.BooleanEval().Value);
        }

        public static IEnumerable<ITypedElement> DistinctUnion(this IEnumerable<ITypedElement> a, IEnumerable<ITypedElement> b)
        {
            var result = a.Union(b, new EqualityOperators.ValueProviderEqualityComparer());
            return result;
        }

        //public static IEnumerable<IValueProvider> ConcatUnion(this IEnumerable<IValueProvider> a, IEnumerable<IValueProvider> b)
        //{
        //    return a.Concat(b);
        //}


        public static IEnumerable<ITypedElement> Item(this IEnumerable<ITypedElement> focus, int index)
        {
            return focus.Skip(index).Take(1);
        }

        public static ITypedElement Last(this IEnumerable<ITypedElement> focus)
        {
            return focus.Reverse().First();
        }

        public static IEnumerable<ITypedElement> Tail(this IEnumerable<ITypedElement> focus)
        {
            return focus.Skip(1);
        }

        public static bool Contains(this IEnumerable<ITypedElement> focus, ITypedElement value)
        {
            return focus.Contains(value, new EqualityOperators.ValueProviderEqualityComparer());
        }

        public static IEnumerable<ITypedElement> Distinct(this IEnumerable<ITypedElement> focus)
        {
            return focus.Distinct(new EqualityOperators.ValueProviderEqualityComparer());
        }

        public static bool IsDistinct(this IEnumerable<ITypedElement> focus)
        {
            return focus.Distinct(new EqualityOperators.ValueProviderEqualityComparer()).Count() == focus.Count();
        }

        public static bool SubsetOf(this IEnumerable<ITypedElement> focus, IEnumerable<ITypedElement> other)
        {
            return focus.All(fitem => other.Contains(fitem));
        }

        public static IEnumerable<ITypedElement> Navigate(this IEnumerable<ITypedElement> elements, string name)
        {
            return elements.SelectMany(e => e.Navigate(name));
        }

        public static IEnumerable<ITypedElement> Navigate(this ITypedElement element, string name)
        {
            if (char.IsUpper(name[0]))
            {

                if (!char.IsUpper(element.Name[0]))
                    throw Error.InvalidOperation("Resource type name may only appear at the root of a document");

                // If we are at a resource, we should match a path that is possibly not rooted in the resource
                // (e.g. doing "name.family" on a Patient is equivalent to "Patient.name.family")   
                // Also we do some poor polymorphism here: Resource.meta.lastUpdated is also allowed.
                var baseClasses = new[] { "Resource", "DomainResource" };
                if (element.InstanceType == name || baseClasses.Contains(name))
                {
                    return new List<ITypedElement>() { element };
                }
                else
                {
                    return Enumerable.Empty<ITypedElement>();
                }
            }
            else
            {
                return element.Children(name);
            }
        }
    }
}
