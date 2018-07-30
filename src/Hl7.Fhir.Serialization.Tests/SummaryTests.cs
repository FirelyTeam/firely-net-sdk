using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Tests;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class SummaryTests
    {
        public IElementNavigator getXmlNav(string xml, FhirXmlNavigatorSettings s = null) => 
            FhirXmlNavigator.ForResource(xml, new PocoSerializationInfoProvider(), s);

        [TestMethod]
        public void Summary()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var typeinfo = new PocoSerializationInfoProvider().Provide("Patient");
            var inSummary = typeinfo.GetElements().Where(e => e.InSummary).ToList();

            var nav = new ScopedNavigator(getXmlNav(tpXml));
            var masker = MaskingNavigator.ForSummary(nav);
            var output = masker.ToXml();

            var maskedChildren = masker.Children().ToList();
            Assert.IsTrue(maskedChildren.Count < inSummary.Count);
            Assert.IsTrue(maskedChildren.Select(c => c.Name).All(c => inSummary.Any(s => s.ElementName == c)));
        }

        [TestMethod]
        public void SummaryText()
        {
            var tpXml = File.ReadAllText(@"TestData\mask-text.xml");
            var typeinfo = new PocoSerializationInfoProvider().Provide("ValueSet");

            var nav = new ScopedNavigator(getXmlNav(tpXml));
            var masker = MaskingNavigator.ForText(nav);
            var output = masker.ToXml();

            var maskedChildren = masker.Descendants().Count();
            Assert.AreEqual(8,maskedChildren);
        }

        [TestMethod]
        public void SummaryData()
        {
            var tpXml = File.ReadAllText(@"TestData\mask-text.xml");
            var typeinfo = new PocoSerializationInfoProvider().Provide("ValueSet");

            var nav = new ScopedNavigator(getXmlNav(tpXml));
            var masker = MaskingNavigator.ForData(nav);
            var output = masker.ToXml();

            var maskedChildren = masker.Descendants().Count();
            Assert.AreEqual(nav.Descendants().Count()-3 , maskedChildren);
        }       
    }
}