using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Specification.Tests
{
    static class IListExtensions
    {
        public static int FindIndex<T>(this IList<T> list, Predicate<T> match)
        {
            if (list == null) { throw new ArgumentNullException(nameof(list)); }
            if (match == null) { throw new ArgumentNullException(nameof(match)); }

            for (int i = 0; i < list.Count; i++)
            {
                if (match(list[i])) { return i; }
            }

            return -1;
        }
    }
}
