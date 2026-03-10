using Hl7.Fhir.ElementModel;
using Hl7.FhirPath.Functions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace HL7.FhirPath.Tests.Functions
{
    [TestClass]
    public class CollectionOperatorsTests
    {
        [TestMethod]
        public void Intersect()
        {
            var a = ElementNode.ForPrimitive("A");
            var b1 = ElementNode.ForPrimitive("B");
            var c = ElementNode.ForPrimitive("C");
            var b2 = ElementNode.ForPrimitive("B");

            var col1 = new ITypedElement[] { a, b1 };
            var col2 = new ITypedElement[] { c, b2 };
            var col3 = new ITypedElement[] { c };

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
            var left = ElementNode.CreateList(1, 3, 3, 5, 6);
            var right = ElementNode.CreateList(3, 5, 5, 6, 8);
            CollectionAssert.AreEqual(ElementNode.CreateList(3, 5, 6).ToList(),
                    left.Intersect(right).ToList());
        }

        [TestMethod]
        public void TestExclude()
        {
            var left = ElementNode.CreateList(1, 3, 3, 5, 6);
            var right = ElementNode.CreateList(5, 6);
            CollectionAssert.AreEqual(ElementNode.CreateList(1, 3, 3).ToList(),
                    left.Exclude(right).ToList());
        }
    }
}
