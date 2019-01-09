using Hl7.Fhir.Introspection;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class StructureDefinitionSerializationInfoTests
    {
        [ClassInitialize]
        public static void SetupSource(TestContext t)
        {
            source = ZipSource.CreateValidationSource();
        }

        static IResourceResolver source = null;

        [TestMethod]
        public void TestCanLocateTypes() => SerializationInfoTestHelpers.TestCanLocateTypes(new StructureDefinitionSummaryProvider(source));

        [TestMethod]
        public void TestCanGetElements() => SerializationInfoTestHelpers.TestCanGetElements(new StructureDefinitionSummaryProvider(source));

        [TestMethod]
        public void TestSpecialTypes() => SerializationInfoTestHelpers.TestSpecialTypes(new StructureDefinitionSummaryProvider(source));

        [TestMethod]
        public void TestProvidedOrder() => SerializationInfoTestHelpers.TestProvidedOrder(new StructureDefinitionSummaryProvider(source));

        [TestMethod]
        public void TestValueIsNotAChild() => SerializationInfoTestHelpers.TestValueIsNotAChild(new StructureDefinitionSummaryProvider(source));

    }
}
