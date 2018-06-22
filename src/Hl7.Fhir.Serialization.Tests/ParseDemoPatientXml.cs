using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model.Primitives;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Tests;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
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
        public IElementNavigator getXmlNavU(string xml) => FhirXmlNavigator.Untyped(xml);

        // This test should resurface once you read this through a validating reader navigator (or somesuch)
        [TestMethod]
        public void CanReadThroughUntypedNavigator()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var nav = getXmlNavU(tpXml);

            Assert.AreEqual("Patient", nav.Name);
            Assert.AreEqual("Patient", nav.GetResourceTypeFromAnnotation());

            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.AreEqual("id", nav.Name);
            Assert.AreEqual("pat1", nav.Value);

            var pat = nav.Clone();

            Assert.IsFalse(nav.MoveToFirstChild());

            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual("text", nav.Name);
            var text = nav.Clone();

            Assert.IsTrue(text.MoveToFirstChild("status")); // status
            Assert.AreEqual("generated", text.Value);

            Assert.IsTrue(text.MoveToNext());
            Assert.AreEqual("div", text.Name);
            Assert.IsTrue(((string)text.Value).StartsWith("<div xmlns="));       // special handling of xhtml

            Assert.IsFalse(text.MoveToFirstChild()); // cannot move into xhtml
            Assert.AreEqual("div", text.Name); // still on xhtml <div>
            Assert.IsFalse(text.MoveToNext());  // nothing more in <text>

            Assert.IsTrue(nav.MoveToNext()); // contained
            Assert.AreEqual("contained", nav.Name);
            Assert.AreEqual("Patient", nav.GetResourceTypeFromAnnotation());

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

            Assert.IsTrue(pat.MoveToNext("birthDate"));
            Assert.AreEqual("1974-12-25", pat.Value);

            Assert.IsTrue(pat.MoveToNext("deceasedBoolean"));
            Assert.AreEqual("false", pat.Value);
        }

        [TestMethod]
        public void ElementNavPerformanceXml()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var nav = getXmlNavU(tpXml);

            // run extraction once to allow for caching
            extract();

            //System.Threading.Thread.Sleep(20000);

            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < 50_000; i++)
            {
                extract();
            }
            sw.Stop();

            Debug.WriteLine($"Navigating took {sw.ElapsedMilliseconds/50 } micros");

            void extract()
            {
                var usual = nav.Children("identifier").First().Children("use").First().Value;
                var phone = nav.Children("telecom").First().Children("system").First().Value;
                var prefs = nav.Children("communication").Where(c => c.Children("preferred").Any(pr => pr.Value is string s && s == "true")).Count();
                var link = nav.Children("link").Children("other").Children("reference");
            }
        }

        [TestMethod]
        public void ProducesCorrectUntypedLocations()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var patient = getXmlNavU(tpXml);

            Assert.AreEqual("Patient", patient.Location);

            patient.MoveToFirstChild();
            Assert.AreEqual("Patient.id[0]", patient.Location);

            patient.MoveToNext();   // text
            patient.MoveToNext("identifier");
            Assert.AreEqual("Patient.identifier[0]", patient.Location);
            var idNav = patient.Clone();

            Assert.IsTrue(patient.MoveToFirstChild());
            Assert.AreEqual("Patient.identifier[0].use[0]", patient.Location);

            idNav.MoveToNext(); // identifier
            Assert.AreEqual("Patient.identifier[1]", idNav.Location);

            Assert.IsTrue(idNav.MoveToFirstChild());
            Assert.AreEqual("Patient.identifier[1].use[0]", idNav.Location);
        }
        
        [TestMethod]
        public void ReadsAttributesAsElements()
        {
            var nav = getXmlNavU("<Patient xmlns='http://hl7.org/fhir' xmlns:q='http://somenamespace' q:myattr='dummy' />");

            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.AreEqual("myattr", nav.Name);        // none-xmlns attributes will come through
            var xmldetails = (nav as IAnnotated).Annotation<XmlSerializationDetails>();
            Assert.AreEqual(XNamespace.Get("http://somenamespace"), xmldetails.Namespace);

            Assert.AreEqual("Patient.myattr[0]", nav.Location);
        }


        [TestMethod]
        public void HasLineNumbers()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var nav = getXmlNavU(tpXml);

            Assert.IsTrue(nav.MoveToFirstChild());

            var posInfo = (nav as IAnnotated).Annotation<PositionInfo>();
            Assert.IsNotNull(posInfo);
            Assert.AreNotEqual(-1, posInfo.LineNumber);
            Assert.AreNotEqual(-1, posInfo.LinePosition);
            Assert.AreNotEqual(0, posInfo.LineNumber);
            Assert.AreNotEqual(0, posInfo.LinePosition);

        }

        [TestMethod]
        public void TestAllFeatures()
        {
            var tpXml = File.ReadAllText(@"TestData\all-xml-features.xml");

            // will allow whitespace and comments to come through
            var reader = XmlReader.Create(new StringReader(tpXml));
            var nav = FhirXmlNavigator.Untyped(reader);

            Assert.AreEqual("SomeResource", nav.Name);

            var xmldetails = (nav as IAnnotated).Annotation<XmlSerializationDetails>();
            var commentdetails = (nav as IAnnotated).Annotation<SourceComments>();
            Assert.IsNotNull(xmldetails);
            Assert.AreEqual(XmlNodeType.Element, xmldetails.NodeType);
            Assert.AreEqual("http://hl7.org/fhir", xmldetails.Namespace.NamespaceName);
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
                Assert.AreEqual(xd.Namespace + "customAttribute", XName.Get("customAttribute", "http://example.org/some-ns"));
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
                Assert.AreEqual(XmlNs.XHTMLNS, xd.Namespace);
                Assert.AreEqual(2, cd.CommentsBefore.Length);
                Assert.AreEqual(" next line intentionally left empty ", cd.CommentsBefore.First());
                Assert.AreEqual(" Div is really special, since the value includes the node itself ", cd.CommentsBefore.Last());

            }
        }

        [TestMethod]
        public void RoundtripXml()
        {
            // Note: this is a low-level test, just testing xml roundtripping, given that
            // the source and destination are both XML.  It uses non-fhir data, so it would
            // only work in that situation. Just testing whether all xml details come through
            // to faithfully reproduce the source.
            var tpXml = File.ReadAllText(@"TestData\roundtrippable.xml");

            // will allow whitespace and comments to come through
            var reader = XmlReader.Create(new StringReader(tpXml));
            var nav = FhirXmlNavigator.Untyped(reader);

            var xmlBuilder = new StringBuilder();
            var serializer = new NavigatorXmlWriter();
            using (var writer = XmlWriter.Create(xmlBuilder))
            {
                serializer.Write(nav, writer);
            }

            var output = xmlBuilder.ToString();
            XmlAssert.AreSame("roundtrippable.xml", tpXml, output);            
        }

        [TestMethod]
        public void CatchesLowLevelErrors()
        {
            var tpXml = File.ReadAllText(@"TestData\with-errors.xml");
            var patient = getXmlNavU(tpXml);

            var result = new List<ExceptionRaisedEventArgs>();

            using ((patient as IExceptionSource).Intercept(arg => { result.Add(arg); return true; } ))
            {
                var x = patient.DescendantsAndSelf().ToList();
            }

            Assert.IsTrue(result.Count > 0);
        }
    }
}