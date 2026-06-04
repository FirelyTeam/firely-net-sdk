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
            var a = PocoNode.ForPrimitive<FhirString>("A");
            var b1 = PocoNode.ForPrimitive<FhirString>("B");
            var c = PocoNode.ForPrimitive<FhirString>("C");
            var b2 = PocoNode.ForPrimitive<FhirString>("B");
            

            var col1 = new PocoNode[] { a, b1 };
            var col2 = new PocoNode[] { c, b2 };
            var col3 = new PocoNode[] { c };

            var result = col1.Intersect(col2);
            Assert.IsNotNull(result);
            Assert.AreEqual("B", result.First().GetValue());

            result = col2.Intersect(col1);
            Assert.IsNotNull(result);
            Assert.AreEqual("B", result.First().GetValue());

            result = col1.Intersect(col3);
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Any());
        }


        [TestMethod]
        public void TestIntersect()
        {
            IEnumerable<PocoNode> left = PocoNode.FromList<Integer>([1, 3, 3, 5, 6]); 
            IEnumerable<PocoNode> right = PocoNode.FromList<Integer>([3, 5, 5, 6, 8]);
            PocoNode.FromList<Integer>([3, 5, 6]).IsEqualTo(left.Intersect(right).ToList()).Should().BeTrue();
        }

        [TestMethod]
        public void TestExclude()
        {
            IEnumerable<PocoNode> left =
                PocoNode.FromList<Integer>([1, 3, 3, 5, 6]);
            IEnumerable<PocoNode> right =
                PocoNode.FromList<Integer>([5, 6]);
            PocoNode.FromList<Integer>([1, 3, 3]).IsEqualTo(left.Exclude(right).ToList()).Should().BeTrue();
        }
    }
}
