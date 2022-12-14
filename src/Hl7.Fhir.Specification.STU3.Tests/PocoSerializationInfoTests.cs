using Hl7.Fhir.Specification;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class PocoSerializationInfoTests
    {
        //[TestMethod]
        //public void TestResourceInfo()
        //{
        //    var ip = new PocoModelMetadataProvider();
        //    Assert.IsTrue(ip.IsResource("Patient"));
        //    Assert.IsTrue(ip.IsResource("DomainResource"));
        //    Assert.IsTrue(ip.IsResource("Resource"));
        //    Assert.IsFalse(ip.IsResource("Identifier"));
        //}

        [TestMethod]
        public void TestCanLocateTypes() => SerializationInfoTestHelpers.TestCanLocateTypes(new PocoStructureDefinitionSummaryProvider());

        [TestMethod]
        public void TestCanLocateTypesByCanonical() => SerializationInfoTestHelpers.TestCanLocateTypesByCanonical(new PocoStructureDefinitionSummaryProvider());

        [TestMethod]
        public void TestCanGetElements() => SerializationInfoTestHelpers.TestCanGetElements(new PocoStructureDefinitionSummaryProvider());

        [TestMethod]
        public void TestSpecialTypes() => SerializationInfoTestHelpers.TestSpecialTypes(new PocoStructureDefinitionSummaryProvider());

        [TestMethod]
        public void TestProvidedOrder() => SerializationInfoTestHelpers.TestProvidedOrder(new PocoStructureDefinitionSummaryProvider());

        [TestMethod]
        public void TestValueIsNotAChild() => SerializationInfoTestHelpers.TestValueIsNotAChild(new PocoStructureDefinitionSummaryProvider());

        //These tests uses a profile that is not expressed as a POCO, so these tests are not applicable
        //public void TestXmlRepresetation() => SerializationInfoTestHelpers.TestXmlRepresetation(new PocoStructureDefinitionSummaryProvider());
        //public void TestRequiresSnapshot() => SerializationInfoTestHelpers.TestRequiresSnapshot(new PocoStructureDefinitionSummaryProvider());

    }
}
