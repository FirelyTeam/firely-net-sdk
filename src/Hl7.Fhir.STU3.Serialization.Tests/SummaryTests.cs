using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Specification.Source;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class SummaryTests
    {
        public ITypedElement getXmlNode(string xml, FhirXmlParsingSettings s = null) =>
            XmlParsingHelpers.ParseToTypedElement(xml, new PocoStructureDefinitionSummaryProvider(), s);
        
        [TestMethod]
        public async Task Summary()
        {
            var tpXml = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));
            var typeinfo = new PocoStructureDefinitionSummaryProvider().Provide("Patient");
            var inSummary = typeinfo.GetElements().Where(e => e.InSummary).ToList();

            var nav = new ScopedNode(getXmlNode(tpXml));
            var masker = MaskingNode.ForSummary(nav);
            var output = await masker.ToXmlAsync();

            var maskedChildren = masker.Children().ToList();
            Assert.IsTrue(maskedChildren.Count < inSummary.Count);
            Assert.IsTrue(maskedChildren.Select(c => c.Name).All(c => inSummary.Any(s => s.ElementName == c)));
        }

        [TestMethod]
        public async Task SummaryText()
        {
            var tpXml = File.ReadAllText(Path.Combine("TestData", "mask-text.xml"));
            var typeinfo = new PocoStructureDefinitionSummaryProvider().Provide("ValueSet");

            var nav = new ScopedNode(getXmlNode(tpXml));
            var masker = MaskingNode.ForText(nav);
            var output = await masker.ToXmlAsync();

            var m = masker.Descendants().ToList();
            var maskedChildren = masker.Descendants().Count();
            Assert.AreEqual(8,maskedChildren);
        }

        [TestMethod]
        public async Task SummaryData()
        {
            var tpXml = File.ReadAllText(Path.Combine("TestData", "mask-text.xml"));
            var typeinfo = new PocoStructureDefinitionSummaryProvider().Provide("ValueSet");

            var nav = new ScopedNode(getXmlNode(tpXml));
            var masker = MaskingNode.ForData(nav);
            var output = await masker.ToXmlAsync();

            var maskedChildren = masker.Descendants().Count();
            Assert.AreEqual(nav.Descendants().Count()-3 , maskedChildren);
        }

        [TestMethod]
        public void SummaryCountUsingStructureDefinitionSummaryProvider()
        {
            var tpXml = File.ReadAllText(Path.Combine("TestData", "mask-text.xml"));

            var nav = new ScopedNode(getXmlNodeSDSP(tpXml));
            var masker = MaskingNode.ForCount(nav);

            var maskedChildren = masker.Descendants().Count();
            Assert.AreEqual(maskedChildren, 2);

            ITypedElement getXmlNodeSDSP(string xml, FhirXmlParsingSettings s = null) =>
                XmlParsingHelpers.ParseToTypedElement(xml, new StructureDefinitionSummaryProvider(ZipSource.CreateValidationSource()), s);
        }
    }
}