using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Hl7.FhirPath.Tests.XmlNavTests
{
    [TestClass]
    public class ParseDemoPatientXml
    {
        [TestMethod]
        public void CanReadThroughNavigator()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var nav = XmlDomFhirNavigator.Create(tpXml);

            Assert.AreEqual("Patient", nav.Name);
            Assert.AreEqual("Patient", nav.Type);

            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.AreEqual("id", nav.Name);
            Assert.AreEqual("pat1", nav.Value);

            Assert.IsFalse(nav.MoveToFirstChild());

            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual("text", nav.Name);
            var text = nav.Clone();

            Assert.IsTrue(text.MoveToFirstChild("status")); // status
            Assert.IsTrue(text.MoveToNext());
            Assert.AreEqual("div", text.Name);
            Assert.IsTrue(((string)text.Value).StartsWith("<div xmlns="));       // special handling of xhtml
            Assert.IsFalse(text.MoveToFirstChild()); // cannot move into xhtml
            Assert.AreEqual("div", text.Name); // still on xhtml <div>
            Assert.IsFalse(text.MoveToNext());  // nothing more in <text>

            Assert.IsTrue(nav.MoveToNext()); // contained
            Assert.AreEqual("contained", nav.Name);
            Assert.AreEqual("Patient", nav.Type);
            Assert.IsTrue(nav.MoveToFirstChild()); // id
            Assert.IsTrue(nav.MoveToNext()); // identifier
            var identifier = nav.Clone();

            Assert.IsTrue(identifier.MoveToFirstChild()); // system
            Assert.IsTrue(identifier.MoveToNext()); // value
            Assert.IsFalse(identifier.MoveToNext()); // still value

            Assert.AreEqual("value", identifier.Name);
            Assert.IsFalse(identifier.MoveToFirstChild());
            Assert.AreEqual("444222222", identifier.Value);

            Assert.IsTrue(nav.MoveToNext("name"));
            Assert.AreEqual("name", nav.Name);
            Assert.IsTrue(nav.MoveToFirstChild());  // id (attribute)
            Assert.AreEqual("id", nav.Name);
            Assert.AreEqual("firstname", nav.Value);
            Assert.IsTrue(nav.MoveToNext());  // use (element!)
            Assert.AreEqual("use", nav.Name);
        }

        [TestMethod]
        public void ElementNavPerformance()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var nav = XmlDomFhirNavigator.Create(tpXml);

            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < 10_000; i++)
            {
                var usual = nav.Children("identifier").First().Children("use").First().Value;
                var phone = nav.Children("telecom").First().Children("system").First().Value;
                var prefs = nav.Children("communication").Where(c => c.Children("preferred").Any(pr => pr.Value is string s && s == "true")).Count();
                var link = nav.Children("link").Children("other").Children("reference");
            }
            sw.Stop();

            Debug.WriteLine($"Navigating took {sw.ElapsedMilliseconds / 10} micros");
        }

        [TestMethod]
        public void ProducesCorrectLocations()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var patient = XmlDomFhirNavigator.Create(tpXml);

            Assert.AreEqual("Patient", patient.Location);

            patient.MoveToFirstChild();
            Assert.AreEqual("Patient.id[0]", patient.Location);

            patient.MoveToNext();   // text
            patient.MoveToNext();   // contained[0]
            patient.MoveToNext();   // contained[1]
            Assert.AreEqual("Patient.contained[1]", patient.Location);

            patient.MoveToFirstChild();
            Assert.AreEqual("Patient.contained[1].id[0]", patient.Location);
        }

        [TestMethod]
        public void ReadsAttributesAsElements()
        {
            var nav = XmlDomFhirNavigator.Create("<Patient xmlns='http://hl7.org/fhir' xmlns:q='http://somenamespace' q:myattr='dummy' />");

            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.AreEqual("myattr", nav.Name);        // none-xmlns attributes will come through
            var xmldetails = (nav as IAnnotated).Annotation<XmlSerializationDetails>();
            Assert.AreEqual("http://somenamespace", xmldetails.Name.NamespaceName);

            Assert.AreEqual("Patient.myattr[0]", nav.Location);
        }

        [TestMethod]
        public void CompareXmlJsonParseOutcomes()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var tpJson = File.ReadAllText(@"TestData\fp-test-patient.json");
            var navXml = XmlDomFhirNavigator.Create(tpXml);
            var navJson = JsonDomFhirNavigator.Create(tpJson);

            var compare = navXml.IsEqualTo(navJson);

            if (compare.Success == false)
            {
                Debug.WriteLine($"Difference in {compare.Details} at {compare.FailureLocation}");
                Assert.IsTrue(compare.Success);
            }
            Assert.IsTrue(compare.Success);
        }

        [TestMethod]
        public void HasLineNumbersXml()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var nav = XmlDomFhirNavigator.Create(tpXml);

            Assert.IsTrue(nav.MoveToFirstChild());

            var xmlDetails = (nav as IAnnotated)?.Annotation<XmlSerializationDetails>();
            Assert.IsNotNull(xmlDetails);
            Assert.AreNotEqual(-1, xmlDetails.LineNumber);
            Assert.AreNotEqual(-1, xmlDetails.LinePosition);
        }      
    }
}