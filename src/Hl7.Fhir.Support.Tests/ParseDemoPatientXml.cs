using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

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
        public void ElementNavPerformanceXml()
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
        
        [TestMethod]
        public void TestAllFeaturesXml()
        {
            var tpXml = File.ReadAllText(@"TestData\all-xml-features.xml");

            // will allow whitespace and comments to come through
            var reader = XmlReader.Create(new StringReader(tpXml));          
            var nav = XmlDomFhirNavigator.Create(reader);

            Assert.AreEqual("SomeResource", nav.Name);
            Assert.AreEqual("SomeResource", nav.Type);

            var xmldetails = (nav as IAnnotated).Annotation<XmlSerializationDetails>();
            Assert.IsNotNull(xmldetails);
            Assert.AreEqual(XmlNodeType.Element, xmldetails.NodeType);
            Assert.AreEqual("http://hl7.org/fhir", xmldetails.Name.NamespaceName);
            Assert.IsTrue(xmldetails.DocumentStartComments.Single().Contains("structural errors"));
            Assert.IsTrue(xmldetails.CommentsAfter.Single().Contains("standard FHIR"));
            Assert.IsNull(nav.Value);

            // namespace attributes should not be found

            nav.MoveToFirstChild(); assertAnElement(nav.Clone());
            nav.MoveToNext(); assertAnElementWithValueAndChildren(nav.Clone());
            nav.MoveToNext(); assertDiv(nav.Clone());
            nav.MoveToNext(); assertResourceContainer(nav.Clone());
            Assert.IsFalse(nav.MoveToNext());

            void assertAnElement(IElementNavigator cn)
            {
                Assert.AreEqual("anElement", cn.Name);
                Assert.AreEqual("true", cn.Value);
                Assert.AreEqual(1, cn.Children().Count());
                cn.MoveToFirstChild();

                Assert.AreEqual("customAttribute", cn.Name);
                Assert.AreEqual("primitive", cn.Value);
                var xd = (cn as IAnnotated).Annotation<XmlSerializationDetails>();
                Assert.IsNotNull(xd);
                Assert.AreEqual(XmlNodeType.Attribute,xd.NodeType);
                Assert.IsFalse(cn.HasChildren());
            }

            void assertAnElementWithValueAndChildren(IElementNavigator cn)
            {
                Assert.AreEqual("anElementWithValueAndChildren", cn.Name);
                Assert.AreEqual("4", cn.Value);
                var mylittledetails = (cn as IAnnotated).Annotation<XmlSerializationDetails>();
                Assert.IsNotNull(mylittledetails);
                Assert.IsTrue(mylittledetails.NodeText.Contains("Crap, mixed content!"));
                Assert.IsTrue(mylittledetails.NodeText.Contains("Is Merged"));
                Assert.AreEqual(2, mylittledetails.CommentsAfter.Length);

                Assert.IsTrue(cn.MoveToFirstChild());
                firstChild(cn.Clone());
                cn.MoveToNext(); secondChild(cn.Clone());
                cn.MoveToNext(); thirdChild(cn.Clone());
                cn.MoveToNext(); fourthChild(cn.Clone());
                Assert.IsFalse(cn.MoveToNext());

                void firstChild(IElementNavigator ccn)
                {
                    Assert.AreEqual("firstChild", ccn.Name);
                    Assert.AreEqual("I have text content", ccn.Value);
                    Assert.IsFalse(ccn.HasChildren());
                }

                void secondChild(IElementNavigator ccn)
                {
                    Assert.AreEqual("secondChild", ccn.Name);
                    Assert.AreEqual("I have text content too", ccn.Value);
                    Assert.AreEqual(1, ccn.Children().Count());
                    ccn.MoveToFirstChild();
                    Assert.AreEqual("customAttribute", ccn.Name);
                    Assert.AreEqual("morning", ccn.Value);
                    Assert.IsFalse(ccn.HasChildren());
                }

                void thirdChild(IElementNavigator ccn)
                {
                    Assert.AreEqual("thirdChild", ccn.Name);
                    Assert.AreEqual("afternoon", ccn.Value);
                    Assert.IsFalse(ccn.HasChildren());
                    var xd = (ccn as IAnnotated).Annotation<XmlSerializationDetails>();
                    Assert.IsNotNull(xd);
                    Assert.AreEqual("Invisible, since there is a @value", xd.NodeText);
                    Assert.IsFalse(xd.OpeningComments.Any());
                }

                void fourthChild(IElementNavigator ccn)
                {
                    Assert.AreEqual("fourthChild", ccn.Name);
                    Assert.IsNull(ccn.Value);
                    Assert.IsFalse(ccn.HasChildren());
                    var xd = (ccn as IAnnotated).Annotation<XmlSerializationDetails>();
                    Assert.IsNotNull(xd);
                    Assert.AreEqual(" this should be possible ", xd.OpeningComments.Single());
                    Assert.IsFalse(xd.CommentsAfter.Any());
                }
            }

            void assertDiv(IElementNavigator cnn)
            {
                var val = cnn.Value as string;
                Assert.IsTrue(val.StartsWith("<div") && val.Contains("Some html"));
                Assert.IsFalse(cnn.HasChildren());  // html should not be represented as children

                var xd = (nav as IAnnotated).Annotation<XmlSerializationDetails>();
                Assert.AreEqual(XmlNs.XHTML, xd.Name.NamespaceName);
            }

            void assertResourceContainer(IElementNavigator cnn)
            {

            }
        }
    }
}