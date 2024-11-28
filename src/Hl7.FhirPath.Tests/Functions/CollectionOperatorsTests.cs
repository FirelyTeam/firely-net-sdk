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
            var a = SinglePrimitiveElementNode<FhirString>.FromSystemPrimitive<FhirString>("A");
            var b1 = SinglePrimitiveElementNode<FhirString>.FromSystemPrimitive<FhirString>("B");
            var c = SinglePrimitiveElementNode<FhirString>.FromSystemPrimitive<FhirString>("C");
            var b2 = SinglePrimitiveElementNode<FhirString>.FromSystemPrimitive<FhirString>("B");
            

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
            IEnumerable<IScopedNode> left = RepeatingPrimitiveElementNode<Integer>.FromSystemPrimitives<Integer>([1, 3, 3, 5, 6]);
            IEnumerable<IScopedNode> right = RepeatingPrimitiveElementNode<Integer>.FromSystemPrimitives<Integer>([3, 5, 5, 6, 8]);
            RepeatingPrimitiveElementNode<Integer>.FromSystemPrimitives<Integer>([3, 5, 6]).Should().BeEquivalentTo(left.Intersect(right).ToList());
        }

        [TestMethod]
        public void TestExclude()
        {
            IEnumerable<IScopedNode> left =
                RepeatingPrimitiveElementNode<Integer>.FromSystemPrimitives<Integer>([1, 3, 3, 5, 6]);
            IEnumerable<IScopedNode> right =
                RepeatingPrimitiveElementNode<Integer>.FromSystemPrimitives<Integer>([5, 6]);
            RepeatingPrimitiveElementNode<Integer>.FromSystemPrimitives<Integer>([1, 3, 3]).Should().BeEquivalentTo(left.Exclude(right).ToList());
        }
    }
}
