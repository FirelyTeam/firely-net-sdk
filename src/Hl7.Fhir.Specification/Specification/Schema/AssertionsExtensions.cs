using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Specification.Schema
{
    public static class AssertionsExtensions
    {
        public static IEnumerable<Assertions> Product(this IEnumerable<Assertions> left, IEnumerable<Assertions> right)
            => from leftST in left
               from rightST in right
               select leftST + rightST;
    }
}

