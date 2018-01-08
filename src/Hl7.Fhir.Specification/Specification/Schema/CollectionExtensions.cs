using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Schema
{
    public static class JsonCollectionExtensions
    {
        public static readonly IEnumerable<Assertions> Empty = Enumerable.Empty<Assertions>();
        public static IEnumerable<Assertions> Collect(this IAssertion assertion) =>
            assertion is ICollectable ic ? ic.Collect() : Empty;


    }
}

