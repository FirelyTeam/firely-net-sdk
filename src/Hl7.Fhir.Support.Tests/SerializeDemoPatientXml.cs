using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Tests;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Hl7.FhirPath.Tests.XmlNavTests
{
    [TestClass]
    public class SerializeDemoPatientXml
    {
        public IElementNavigator getXmlNav(string xml) => XmlDomFhirNavigator.Create(xml, new PocoModelMetadataProvider());

        [TestMethod]
        public void CanSerializeThroughNavigatorAndCompare()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var nav = getXmlNav(tpXml);

            var xmlBuilder = new StringBuilder();
            var serializer = new NavigatorXmlWriter();
            using (var writer = XmlWriter.Create(xmlBuilder))
            {
                serializer.Write(nav, writer);
            }

            var output = xmlBuilder.ToString();
            XmlAssert.AreSame("fp-test-patient.xml", tpXml, output);
        }

    }
}