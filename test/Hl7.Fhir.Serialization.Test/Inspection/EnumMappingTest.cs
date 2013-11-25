using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Support;
using Hl7.Fhir.Introspection;

namespace Hl7.Fhir.Test.Inspection
{
    [TestClass]
    public class EnumMappingTest
    {
        [TestMethod]
        public void TestCreation()
        {
            EnumMapping mapping = EnumMapping.Create(typeof(TestEnum));

            Assert.AreEqual("Testee", mapping.Name);
            Assert.AreEqual(typeof(TestEnum), mapping.EnumType);
            Assert.IsTrue(mapping.ContainsLiteral("Item1"));
            Assert.IsFalse(mapping.ContainsLiteral("Item2"));
            Assert.IsFalse(mapping.ContainsLiteral("iTeM1"));
            Assert.IsTrue(mapping.ContainsLiteral("ItemTwo"));

            Assert.AreEqual(TestEnum.Item2, mapping.ParseLiteral("ItemTwo"));
            Assert.AreEqual(TestEnum.Item1, mapping.ParseLiteral("Item1"));
            Assert.AreEqual("ItemTwo", mapping.GetLiteral(TestEnum.Item2));
            Assert.AreEqual("Item1", mapping.GetLiteral(TestEnum.Item1));
        }


        [FhirEnumeration("Testee")]
        enum TestEnum
        {
            Item1 = 4,

            [EnumLiteral("ItemTwo")]
            Item2
        }
    }
}
