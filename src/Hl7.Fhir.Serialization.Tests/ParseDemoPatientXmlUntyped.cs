using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using Hl7.FhirPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class ParseDemoPatientXmlUntyped
    {
        public ISourceNode getXmlUntyped(string xml, FhirXmlParsingSettings settings = null) =>
            FhirXmlNode.Parse(xml, settings);

        // This test should resurface once you read this through a validating reader navigator (or somesuch)
        [TestMethod]
        public void CanReadThroughUntypedNavigator()
        {
            var tpXml = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));
            var nav = getXmlUntyped(tpXml);
#pragma warning disable 612,618
            ParseDemoPatient.CanReadThroughNavigator(nav.ToTypedElement(), typed: false);
#pragma warning restore 612, 618
        }

        [TestMethod]
        public void ElementNavPerformanceUntypedXml()
        {
            var tpXml = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));
            var nav = getXmlUntyped(tpXml);
            ParseDemoPatient.ElementNavPerformance(nav);
        }

        [TestMethod]
        public void ProducesCorrectUntypedLocations()
        {
            var tpXml = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));
            var patient = getXmlUntyped(tpXml);

            ParseDemoPatient.ProducesCorrectUntypedLocations(patient);
        }


        [TestMethod]
        public void ReadsAttributesAsElements()
        {
            var nav = getXmlUntyped("<Patient xmlns='http://hl7.org/fhir' xmlns:q='http://example.org' q:myattr='dummy' " +
                "anotherattr='nons' />",
                new FhirXmlParsingSettings { AllowedExternalNamespaces = new[] { XNamespace.Get("http://example.org") } });

            var navc = nav.Children().ToList();
            Assert.AreEqual(2, navc.Count);

            Assert.AreEqual("myattr", navc[0].Name);        // none-xmlns attributes will come through
            var xmldetails = (navc[0] as IAnnotated).Annotation<XmlSerializationDetails>();
            Assert.AreEqual(XNamespace.Get("http://example.org"), xmldetails.Namespace);
            Assert.AreEqual("Patient.myattr[0]", navc[0].Location);

            Assert.AreEqual("anotherattr", navc[1].Name);        // none-xmlns attributes will come through
            xmldetails = (navc[1] as IAnnotated).Annotation<XmlSerializationDetails>();
            Assert.AreEqual(XNamespace.None, xmldetails.Namespace);
        }


        [TestMethod]
        public void HasLineNumbers()
        {
            var tpXml = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));
            var nav = getXmlUntyped(tpXml);

            ParseDemoPatient.HasLineNumbers<XmlSerializationDetails>(nav);
        }

        [TestMethod]
        public void TestPermissiveParsing()
        {
            var tpXml = File.ReadAllText(Path.Combine("TestData", "all-xml-features.xml"));

            // will allow whitespace and comments to come through
            var reader = XmlReader.Create(new StringReader(tpXml));
            var nav = FhirXmlNode.Read(reader, new FhirXmlParsingSettings { PermissiveParsing = true });

            Assert.AreEqual("SomeResource", nav.Name);

            var xmldetails = (nav as IAnnotated).Annotation<XmlSerializationDetails>();
            var commentdetails = (nav as IAnnotated).Annotation<SourceComments>();
            Assert.IsNotNull(xmldetails);
            Assert.AreEqual(XmlNodeType.Element, xmldetails.NodeType);
            Assert.AreEqual("http://hl7.org/fhir", xmldetails.Namespace.NamespaceName);
            Assert.IsTrue(commentdetails.CommentsBefore.Single().Contains("structural errors"));
            Assert.IsTrue(commentdetails.DocumentEndComments.Single().Contains("standard FHIR"));
            Assert.IsNull(nav.Text);

            // namespace attributes should not be found
            var children = nav.Children().ToList();
            Assert.AreEqual(3, children.Count);
            assertAnElement(children[0]);
            assertAnElementWithValueAndChildren(children[1]);
            assertDiv(children[2]);
   
            void assertAnElement(ISourceNode cn)
            {
                Assert.AreEqual("anElement", cn.Name);
                Assert.AreEqual("true", cn.Text);
                Assert.AreEqual(1, cn.Children().Count());
                cn = cn.Children().First();

                Assert.AreEqual("customAttribute", cn.Name);
                Assert.AreEqual("primitive", cn.Text);

                var xd = (cn as IAnnotated).Annotation<XmlSerializationDetails>();
                Assert.AreEqual(XmlNodeType.Attribute, xd.NodeType);
                Assert.AreEqual(xd.Namespace + "customAttribute", XName.Get("customAttribute", "http://example.org/some-ns"));
                Assert.IsFalse(cn.Children().Any());
            }

            void assertAnElementWithValueAndChildren(ISourceNode cn)
            {
                Assert.AreEqual("anElementWithValueAndChildren", cn.Name);
                Assert.AreEqual("4", cn.Text);

                var mylittledetails = (cn as IAnnotated).Annotation<XmlSerializationDetails>();
                Assert.IsTrue(mylittledetails.NodeText.Contains("Crap, mixed content!"));
                Assert.IsTrue(mylittledetails.NodeText.Contains("Is Merged"));

                var cnc = cn.Children().ToList();
                Assert.AreEqual(3,cnc.Count);
                firstChild(cnc[0]);
                secondChild(cnc[1]);
                thirdChild(cnc[2]);

                void firstChild(ISourceNode ccn)
                {
                    Assert.AreEqual("firstChild", ccn.Name);
                    Assert.IsNull(ccn.Text);
                    var ccnc = ccn.Children().ToList();
                    Assert.AreEqual(1, ccnc.Count);

                    var xd = (ccn as IAnnotated).Annotation<XmlSerializationDetails>();
                    Assert.AreEqual("I have text content", xd.NodeText);

                    Assert.AreEqual("customAttribute", ccnc[0].Name);
                    Assert.AreEqual("morning", ccnc[0].Text);
                }

                void secondChild(ISourceNode ccn)
                {
                    Assert.AreEqual("secondChild", ccn.Name);
                    Assert.AreEqual("afternoon", ccn.Text);
                    Assert.IsFalse(ccn.Children().Any());

                    var xd = (ccn as IAnnotated).Annotation<XmlSerializationDetails>();
                    Assert.AreEqual("I have text content too", xd.NodeText);
                }

                void thirdChild(ISourceNode ccn)
                {
                    Assert.AreEqual("ThirdChild", ccn.Name);
                    Assert.IsNull(ccn.Text);
                    Assert.IsTrue(ccn.Children().Any());

                    var xd = (ccn as IAnnotated).Annotation<XmlSerializationDetails>();
                    var cd = (ccn as IAnnotated).Annotation<SourceComments>();
                    Assert.AreEqual(" this should be possible ", cd.ClosingComments.Single());
                    Assert.IsFalse(cd.CommentsBefore.Any());
                }
            }

            void assertDiv(ISourceNode cnn)
            {
                var val = cnn.Text;
                Assert.IsTrue(val.StartsWith("<div") && val.Contains("Some html"));
                Assert.IsFalse(cnn.Children().Any());  // html should not be represented as children

                var xd = (cnn as IAnnotated).Annotation<XmlSerializationDetails>();
                var cd = (cnn as IAnnotated).Annotation<SourceComments>();
                Assert.AreEqual(XmlNs.XHTMLNS, xd.Namespace);
                Assert.AreEqual(2, cd.CommentsBefore.Length);
                Assert.AreEqual(" next line intentionally left empty ", cd.CommentsBefore.First());
                Assert.AreEqual(" Div is really special, since the value includes the node itself ", cd.CommentsBefore.Last());
            }
        }

        [TestMethod]
        public void RoundtripXmlUntyped()
        {
            ParseDemoPatient.RoundtripXml(xmlText => FhirXmlNode.Parse(xmlText));
        }

        [TestMethod]
        public void TryInvalidUntypedSource()
        {
            var jsonNav = FhirJsonNode.Parse("{ 'resourceType': 'Patient', 'active':true }");

            try
            {
                var output = jsonNav.ToXml();
                Assert.Fail();
            }
            catch (NotSupportedException)
            {
            }
        }

        [TestMethod]
        public void CheckBundleEntryNavigation()
        {
            var bundle = File.ReadAllText(Path.Combine("TestData", "BundleWithOneEntry.xml"));
            var node = getXmlUntyped(bundle);
#pragma warning disable 612, 618
            ParseDemoPatient.CheckBundleEntryNavigation(node.ToTypedElement());
#pragma warning restore 612, 618
        }

        [TestMethod]
        public void CatchesLowLevelErrors()
        {
            var tpXml = File.ReadAllText(Path.Combine("TestData", "with-errors.xml"));
            var patient = getXmlUntyped(tpXml);
            var result = patient.VisitAndCatch();
            var originalCount = result.Count;
            Assert.AreEqual(11, result.Count);
            Assert.IsTrue(!result.Any(r => r.Message.Contains("schemaLocation")));

            patient = getXmlUntyped(tpXml, new FhirXmlParsingSettings() { DisallowSchemaLocation = true });
            result = patient.VisitAndCatch();
            Assert.IsTrue(result.Count == originalCount + 1);    // one extra error about schemaLocation being present
            Assert.IsTrue(result.Any(r => r.Message.Contains("schemaLocation")));

            patient = getXmlUntyped(tpXml, new FhirXmlParsingSettings() { PermissiveParsing = true });
            result = patient.VisitAndCatch();
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void CatchesEmptyContainedResources()
        {
            var xml = "<Patient xmlns='http://hl7.org/fhir'><contained><OperationOutcome /></contained></Patient>";
            var pat = getXmlUntyped(xml);
            var errors = pat.VisitAndCatch();
            Assert.IsTrue(errors.Single().Message.Contains("must have child elements"));

            xml = "<Patient xmlns='http://hl7.org/fhir'><contained /></Patient>";
            pat = getXmlUntyped(xml);
            errors = pat.VisitAndCatch();
            Assert.IsTrue(errors.Single().Message.Contains("must have child elements"));
        }

        [TestMethod]
        public void PreservesParsingExceptionDetails()
        {
            try
            {
                var nav = FhirXmlNode.Parse("{");
                var dummy = nav.Text;
                Assert.Fail();
            }
            catch (FormatException fe)
            {
                Assert.IsInstanceOfType(fe.InnerException, typeof(XmlException));
            }
        }

        [TestMethod]
        public void CatchParseErrors()
        {
            var tpXml = "<Patient>";

            try
            {
                var patient = getXmlUntyped(tpXml);
                Assert.Fail();
            }
            catch (FormatException fe)
            {
                Assert.IsTrue(fe.Message.Contains("Invalid Xml encountered"));
            }
        }

    }
}