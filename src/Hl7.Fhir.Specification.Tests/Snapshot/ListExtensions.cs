using Hl7.Fhir.Model.DSTU2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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

        [Conditional("DEBUG")]
        public static void Dump(this IEnumerable<ElementDefinition> elements, string header = null) => Dump(elements.ToList(), header);

        [Conditional("DEBUG")]
        public static void Dump(this List<ElementDefinition> elements, string header = null)
        {
            Debug.WriteLineIf(!string.IsNullOrEmpty(header), header);
            for (int i = 0; i < elements.Count; i++)
            {
                var elem = elements[i];
                Debug.Write(elem.Path);
                Debug.WriteIf(elem.Name != null, " '" + elem.Name + "'");
                if (elem.Slicing != null)
                {
                    Debug.Write(" => sliced on: " + string.Join(" | ", elem.Slicing.Discriminator));
                }
                Debug.WriteLine("");
            }
        }


    }
}
