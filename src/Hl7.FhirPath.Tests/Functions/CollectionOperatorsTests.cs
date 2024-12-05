using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.FhirPath.Functions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace HL7.FhirPath.Tests.Functions
{
    [TestClass]
    public class CollectionOperatorsTests
    {
        [TestMethod]
        public void Intersect()
        {
            var a = PocoNodeOrList.ForPrimitive<FhirString>("A");
            var b1 = PocoNodeOrList.ForPrimitive<FhirString>("B");
            var c = PocoNodeOrList.ForPrimitive<FhirString>("C");
            var b2 = PocoNodeOrList.ForPrimitive<FhirString>("B");
            

            var col1 = new IScopedNode[] { a, b1 };
            var col2 = new IScopedNode[] { c, b2 };
            var col3 = new IScopedNode[] { c };

            var result = col1.Intersect(col2);
            Assert.IsNotNull(result);
            Assert.AreEqual("B", result.First().Value);

            result = col2.Intersect(col1);
            Assert.IsNotNull(result);
            Assert.AreEqual("B", result.First().Value);

            result = col1.Intersect(col3);
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Any());
        }


        [TestMethod]
        public void TestIntersect()
        {
            IEnumerable<IScopedNode> left = PocoNodeOrList.FromList<Integer>([1, 3, 3, 5, 6]);
            IEnumerable<IScopedNode> right = PocoNodeOrList.FromList<Integer>([3, 5, 5, 6, 8]);
            PocoNodeOrList.FromList<Integer>([3, 5, 6]).Should().BeEquivalentTo(left.Intersect(right).ToList());
        }

        [TestMethod]
        public void TestExclude()
        {
            IEnumerable<IScopedNode> left =
                PocoNodeOrList.FromList<Integer>([1, 3, 3, 5, 6]);
            IEnumerable<IScopedNode> right =
                PocoNodeOrList.FromList<Integer>([5, 6]);
            PocoNodeOrList.FromList<Integer>([1, 3, 3]).Should().BeEquivalentTo(left.Exclude(right).ToList());
        }
    }
}
