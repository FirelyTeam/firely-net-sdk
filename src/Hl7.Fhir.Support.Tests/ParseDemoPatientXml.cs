using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Tests;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.FhirPath.Tests.XmlNavTests
{
    [TestClass]
    public class ParseDemoPatientXml
    {
        public IElementNavigator getXmlNav(string xml) => XmlDomFhirNavigator.Create(xml, new PocoModelMetadataProvider());

        // This test should resurface once you read this through a validating reader navigator (or somesuch)
        [TestMethod, Ignore]
        public void CanReadThroughNavigator()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var nav = getXmlNav(tpXml);

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
            var nav = getXmlNav(tpXml);

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
            var patient = getXmlNav(tpXml);

            Assert.AreEqual("Patient", patient.Location);

            patient.MoveToFirstChild();
            Assert.AreEqual("Patient.id[0]", patient.Location);

            patient.MoveToNext();   // text
            patient.MoveToNext("identifier");
            Assert.AreEqual("Patient.identifier[0]", patient.Location);

            patient.MoveToFirstChild();
            Assert.AreEqual("Patient.identifier[0].use[0]", patient.Location);
        }

        [TestMethod]
        public void ReadsAttributesAsElements()
        {
            var nav = getXmlNav("<Patient xmlns='http://hl7.org/fhir' xmlns:q='http://somenamespace' q:myattr='dummy' />");

            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.AreEqual("myattr", nav.Name);        // none-xmlns attributes will come through
            var xmldetails = (nav as IAnnotated).Annotation<XmlSerializationDetails>();
            Assert.AreEqual("http://somenamespace", xmldetails.Name.NamespaceName);

            Assert.AreEqual("Patient.myattr[0]", nav.Location);
        }


        // resurface when read through a validating navigator
        [TestMethod, Ignore]
        public void CompareXmlJsonParseOutcomes()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var tpJson = File.ReadAllText(@"TestData\fp-test-patient.json");
            var navXml = getXmlNav(tpXml);
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
            var nav = getXmlNav(tpXml);

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
            var nav = XmlDomFhirNavigator.Create(reader, new PocoModelMetadataProvider());

            Assert.AreEqual("SomeResource", nav.Name);

            var xmldetails = (nav as IAnnotated).Annotation<XmlSerializationDetails>();
            var commentdetails = (nav as IAnnotated).Annotation<SourceComments>();
            Assert.IsNotNull(xmldetails);
            Assert.AreEqual(XmlNodeType.Element, xmldetails.NodeType);
            Assert.AreEqual("http://hl7.org/fhir", xmldetails.Name.NamespaceName);
            Assert.IsTrue(commentdetails.CommentsBefore.Single().Contains("structural errors"));
            Assert.IsTrue(commentdetails.DocumentEndComments.Single().Contains("standard FHIR"));
            Assert.IsNull(nav.Value);

            // namespace attributes should not be found

            nav.MoveToFirstChild(); assertAnElement(nav.Clone());
            nav.MoveToNext(); assertAnElementWithValueAndChildren(nav.Clone());
            nav.MoveToNext(); assertDiv(nav.Clone());
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
                Assert.AreEqual(XmlNodeType.Attribute, xd.NodeType);
                Assert.AreEqual(xd.Name, XName.Get("customAttribute", "http://example.org/some-ns"));
                Assert.IsFalse(cn.HasChildren());
            }

            void assertAnElementWithValueAndChildren(IElementNavigator cn)
            {
                Assert.AreEqual("anElementWithValueAndChildren", cn.Name);
                Assert.AreEqual("4", cn.Value);

                var mylittledetails = (cn as IAnnotated).Annotation<XmlSerializationDetails>();
                Assert.IsTrue(mylittledetails.NodeText.Contains("Crap, mixed content!"));
                Assert.IsTrue(mylittledetails.NodeText.Contains("Is Merged"));

                Assert.IsTrue(cn.MoveToFirstChild());
                firstChild(cn.Clone());
                cn.MoveToNext(); secondChild(cn.Clone());
                cn.MoveToNext(); thirdChild(cn.Clone());
                Assert.IsFalse(cn.MoveToNext());

                void firstChild(IElementNavigator ccn)
                {
                    Assert.AreEqual("firstChild", ccn.Name);
                    Assert.IsNull(ccn.Value);
                    Assert.AreEqual(1, ccn.Children().Count());

                    var xd = (ccn as IAnnotated).Annotation<XmlSerializationDetails>();
                    Assert.AreEqual("I have text content", xd.NodeText);

                    ccn.MoveToFirstChild();
                    Assert.AreEqual("customAttribute", ccn.Name);
                    Assert.AreEqual("morning", ccn.Value);
                    Assert.IsFalse(ccn.HasChildren());
                }

                void secondChild(IElementNavigator ccn)
                {
                    Assert.AreEqual("secondChild", ccn.Name);
                    Assert.AreEqual("afternoon", ccn.Value);
                    Assert.IsFalse(ccn.HasChildren());

                    var xd = (ccn as IAnnotated).Annotation<XmlSerializationDetails>();
                    Assert.AreEqual("I have text content too", xd.NodeText);
                }

                void thirdChild(IElementNavigator ccn)
                {
                    Assert.AreEqual("ThirdChild", ccn.Name);
                    Assert.IsNull(ccn.Value);
                    Assert.IsFalse(ccn.HasChildren());

                    var xd = (ccn as IAnnotated).Annotation<XmlSerializationDetails>();
                    var cd = (ccn as IAnnotated).Annotation<SourceComments>();
                    Assert.AreEqual(" this should be possible ", cd.ClosingComments.Single());
                    Assert.IsFalse(cd.CommentsBefore.Any());
                }
            }

            void assertDiv(IElementNavigator cnn)
            {
                var val = cnn.Value as string;
                Assert.IsTrue(val.StartsWith("<div") && val.Contains("Some html"));
                Assert.IsFalse(cnn.HasChildren());  // html should not be represented as children

                var xd = (nav as IAnnotated).Annotation<XmlSerializationDetails>();
                var cd = (nav as IAnnotated).Annotation<SourceComments>();
                Assert.AreEqual(XmlNs.XHTML, xd.Name.NamespaceName);
                Assert.AreEqual(2, cd.CommentsBefore.Length);
                Assert.AreEqual(" next line intentionally left empty ", cd.CommentsBefore.First());
                Assert.AreEqual(" Div is really special, since the value includes the node itself ", cd.CommentsBefore.Last());

            }
        }

        [TestMethod]
        public void RoundtripXml()
        {
            var tpXml = File.ReadAllText(@"TestData\roundtrippable.xml");

            // will allow whitespace and comments to come through
            var reader = XmlReader.Create(new StringReader(tpXml));
            var nav = XmlDomFhirNavigator.Create(reader, new PocoModelMetadataProvider());

            var xmlBuilder = new StringBuilder();
            var serializer = new NavigatorXmlWriter();
            using (var writer = XmlWriter.Create(xmlBuilder))
            {
                serializer.Write(nav, writer);
            }

            var output = xmlBuilder.ToString();
            XmlAssert.AreSame("roundtrippable.xml", tpXml, output);            
        }

    }
}