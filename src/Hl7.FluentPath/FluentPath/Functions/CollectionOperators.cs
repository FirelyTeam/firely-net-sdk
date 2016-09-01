/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
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
using Hl7.ElementModel;
using Hl7.FluentPath.Support;

namespace Hl7.FluentPath.Functions
{
    internal static class CollectionOperators
    {
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

        public static IEnumerable<IValueProvider> DistinctUnion(this IEnumerable<IValueProvider> a, IEnumerable<IValueProvider> b)
        {
            var result = a.Union(b, new EqualityOperators.ValueProviderEqualityComparer());
            return result;
        }

        //public static IEnumerable<IValueProvider> ConcatUnion(this IEnumerable<IValueProvider> a, IEnumerable<IValueProvider> b)
        //{
        //    return a.Concat(b);
        //}


        public static IEnumerable<IValueProvider> Item(this IEnumerable<IValueProvider> focus, int index)
        {
            return focus.Skip(index).Take(1);
        }

        public static IValueProvider Last(this IEnumerable<IValueProvider> focus)
        {
            return focus.Reverse().First();
        }

        public static IEnumerable<IValueProvider> Tail(this IEnumerable<IValueProvider> focus)
        {
            return focus.Skip(1);
        }
        
        public static bool Contains(this IEnumerable<IValueProvider> focus, IValueProvider value)
        {
            return focus.Contains(value, new EqualityOperators.ValueProviderEqualityComparer());
        }

        public static IEnumerable<IValueProvider> Distinct(this IEnumerable<IValueProvider> focus)
        {
            return focus.Distinct(new EqualityOperators.ValueProviderEqualityComparer());
        }

        public static bool IsDistinct(this IEnumerable<IValueProvider> focus)
        {
            return focus.Distinct(new EqualityOperators.ValueProviderEqualityComparer()).Count() == focus.Count();
        }

        public static bool SubsetOf(this IEnumerable<IValueProvider> focus, IEnumerable<IValueProvider> other)
        {
            return focus.All(fitem => other.Contains(fitem));
        }

        public static IEnumerable<IValueProvider> Navigate(this IEnumerable<IValueProvider> elements, string name)
        {
            return elements.SelectMany(e => e.Navigate(name));
        }

        public static IEnumerable<IValueProvider> Navigate(this IValueProvider element, string name)
        {
            if(!(element is IElementNavigator))
                return FhirValueList.Empty;

            var nav = (IElementNavigator)element;

            if (char.IsUpper(name[0]))
            {

                if (!char.IsUpper(nav.Name[0]))
                    throw Error.InvalidOperation("Resource type name may only appear at the root of a document");

                // If we are at a resource, we should match a path that is possibly not rooted in the resource
                // (e.g. doing "name.family" on a Patient is equivalent to "Patient.name.family")        
                if (nav is ITypeNameProvider)
                {
                    if (((ITypeNameProvider)nav).TypeName == name)
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
                    throw Error.InvalidOperation("Cannot verify whether the root object is of type '{0}'. ".FormatWith(name) +
                        "You could try leaving out the resource name of the expression.");
                }
            }
            else
            {
                return nav.GetChildrenByName(name);
            }
        }

     

    }
}
