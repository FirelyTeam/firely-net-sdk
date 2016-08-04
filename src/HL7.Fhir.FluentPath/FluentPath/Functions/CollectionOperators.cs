/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.FluentPath.Functions
{
    internal static class CollectionOperators
    {
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

        public static IEnumerable<IValueProvider> Trace(this IEnumerable<IValueProvider> focus, string name)
        {
            System.Diagnostics.Trace.WriteLine("=== Trace {0} ===".FormatWith(name));

            if (focus == null)
                System.Diagnostics.Trace.WriteLine("(null)");

            else if (focus is IEnumerable<IValueProvider>)
            {
                System.Diagnostics.Trace.WriteLine("Collection:".FormatWith(name));
                foreach (var element in (IEnumerable<IValueProvider>)focus)
                {
                    if (element.Value != null)
                        System.Diagnostics.Trace.WriteLine("   " + element.Value.ToString());
                }
            }
            else if (focus is IValueProvider)
            {
                var element = (IValueProvider)focus;
                System.Diagnostics.Trace.WriteLine("Value:".FormatWith(name));

                if (element.Value != null)
                {
                    System.Diagnostics.Trace.WriteLine(element.Value.ToString());
                }
            }
            else
                System.Diagnostics.Trace.WriteLine(focus.ToString());

            System.Diagnostics.Trace.WriteLine(Environment.NewLine);

            return focus;
        }

    }
}
