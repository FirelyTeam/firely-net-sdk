using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Hl7.Fhir.Specification.Tests
{
    public static class IListExtensions
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
                Debug.WriteLine($"{i,3}: {Format(elements[i])}");
            }
        }

        [Conditional("DEBUG")]
        public static void Log(this List<ElementDefinition> elements, string header = null)
        {
            if (!string.IsNullOrEmpty(header))
            {
                Console.WriteLine(header);
            }
            for (int i = 0; i < elements.Count; i++)
            {
                Console.WriteLine($"{i,3}: {Format(elements[i])}");
            }
        }

        static string Format(ElementDefinition elem)
            => $"{elem.Path}{(elem.SliceName is null ? "" : " '" + elem.SliceName + "'")}{(elem.Slicing is null ? "" : " => sliced on: " + string.Join(" | ", elem.Slicing.Discriminator.Select(d => d.Type + ":" + d.Path)))}";

    }
}
