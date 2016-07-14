using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.FluentPath.Functions
{
    public static class CollectionOperators
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
    }
}
