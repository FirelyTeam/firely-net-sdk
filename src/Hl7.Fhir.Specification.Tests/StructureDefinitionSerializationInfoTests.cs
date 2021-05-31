using Hl7.Fhir.Specification;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class StructureDefinitionSerializationInfoTests
    {
        [ClassInitialize]
        public static void SetupSource(TestContext t)
        {
            source = new MultiResolver(
                ZipSource.CreateValidationSource(),
                new DirectorySource("TestData", new DirectorySourceSettings(includeSubdirectories: true)),
                new TestProfileArtifactSource()
                );
        }

        static IResourceResolver source = null;

        [TestMethod]
        public void TestCanLocateTypes() => SerializationInfoTestHelpers.TestCanLocateTypes(new StructureDefinitionSummaryProvider(source));

        [TestMethod]
        public void TestCanLocateTypesByCanonical() => SerializationInfoTestHelpers.TestCanLocateTypesByCanonical(new StructureDefinitionSummaryProvider(source));

        [TestMethod]
        public void TestCanGetElements() => SerializationInfoTestHelpers.TestCanGetElements(new StructureDefinitionSummaryProvider(source));

        [TestMethod]
        public void TestSpecialTypes() => SerializationInfoTestHelpers.TestSpecialTypes(new StructureDefinitionSummaryProvider(source));

        [TestMethod]
        public void TestProvidedOrder() => SerializationInfoTestHelpers.TestProvidedOrder(new StructureDefinitionSummaryProvider(source));

        [TestMethod]
        public void TestValueIsNotAChild() => SerializationInfoTestHelpers.TestValueIsNotAChild(new StructureDefinitionSummaryProvider(source));

        [TestMethod]
        public void TestXmlRepresetation() => SerializationInfoTestHelpers.TestXmlRepresetation(new StructureDefinitionSummaryProvider(source));

        [TestMethod]
        public void TestRequiresSnapshot() => SerializationInfoTestHelpers.TestRequiresSnapshot(new StructureDefinitionSummaryProvider(source));

    }
}
