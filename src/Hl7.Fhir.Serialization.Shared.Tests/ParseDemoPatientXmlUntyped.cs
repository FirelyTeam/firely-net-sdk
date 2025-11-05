using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using Hl7.FhirPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class ParseDemoPatientXmlUntyped
    {
        private ISourceNode getXmlUntyped(string xml, FhirXmlParsingSettings settings = null)
        {
            settings ??= FhirXmlParsingSettings.CreateDefault();
            settings.PermissiveParsing = false;
            return FhirXmlNode.Parse(xml, settings);
        }


        // This test should resurface once you read this through a validating reader navigator (or somesuch)
        [TestMethod]
        public void CanReadThroughUntypedNavigator()
        {
            var tpXml = File.ReadAllText(Path.Combine("TestData", "fp-test-patient.xml"));
            var nav = getXmlUntyped(tpXml);
#pragma warning disable 612,618
            ParseDemoPatient.CanReadThroughTypedElement(nav.ToTypedElementLegacy(), typed: false);
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
            Assert.HasCount(2, navc);

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
            Assert.Contains("structural errors", commentdetails.CommentsBefore.Single());
            Assert.Contains("standard FHIR", commentdetails.DocumentEndComments.Single());
            Assert.IsNull(nav.Text);

            // namespace attributes should not be found
            var children = nav.Children().ToList();
            Assert.HasCount(3, children);
            assertAnElement(children[0]);
            assertAnElementWithValueAndChildren(children[1]);
            assertDiv(children[2]);

            static void assertAnElement(ISourceNode cn)
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

            static void assertAnElementWithValueAndChildren(ISourceNode cn)
            {
                Assert.AreEqual("anElementWithValueAndChildren", cn.Name);
                Assert.AreEqual("4", cn.Text);

                var mylittledetails = (cn as IAnnotated).Annotation<XmlSerializationDetails>();
                Assert.Contains("Crap, mixed content!", mylittledetails.NodeText);
                Assert.Contains("Is Merged", mylittledetails.NodeText);

                var cnc = cn.Children().ToList();
                Assert.HasCount(3, cnc);
                firstChild(cnc[0]);
                secondChild(cnc[1]);
                thirdChild(cnc[2]);

                static void firstChild(ISourceNode ccn)
                {
                    Assert.AreEqual("firstChild", ccn.Name);
                    Assert.IsNull(ccn.Text);
                    var ccnc = ccn.Children().ToList();
                    Assert.HasCount(1, ccnc);

                    var xd = (ccn as IAnnotated).Annotation<XmlSerializationDetails>();
                    Assert.AreEqual("I have text content", xd.NodeText);

                    Assert.AreEqual("customAttribute", ccnc[0].Name);
                    Assert.AreEqual("morning", ccnc[0].Text);
                }

                static void secondChild(ISourceNode ccn)
                {
                    Assert.AreEqual("secondChild", ccn.Name);
                    Assert.AreEqual("afternoon", ccn.Text);
                    Assert.IsFalse(ccn.Children().Any());

                    var xd = (ccn as IAnnotated).Annotation<XmlSerializationDetails>();
                    Assert.AreEqual("I have text content too", xd.NodeText);
                }

                static void thirdChild(ISourceNode ccn)
                {
                    Assert.AreEqual("ThirdChild", ccn.Name);
                    Assert.IsNull(ccn.Text);
                    Assert.IsTrue(ccn.Children().Any());                    
                    var cd = (ccn as IAnnotated).Annotation<SourceComments>();
                    Assert.AreEqual(" this should be possible ", cd.ClosingComments.Single());
                    Assert.IsFalse(cd.CommentsBefore.Any());
                }
            }

            static void assertDiv(ISourceNode cnn)
            {
                var val = cnn.Text;
                Assert.IsTrue(val.StartsWith("<div") && val.Contains("Some html"));
                Assert.IsFalse(cnn.Children().Any());  // html should not be represented as children

                var xd = (cnn as IAnnotated).Annotation<XmlSerializationDetails>();
                var cd = (cnn as IAnnotated).Annotation<SourceComments>();
                Assert.AreEqual(XmlNs.XHTMLNS, xd.Namespace);
                Assert.HasCount(2, cd.CommentsBefore);
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
        public async Task TryInvalidUntypedSource()
        {
            var jsonNav = await FhirJsonNode.ParseAsync("{ 'resourceType': 'Patient', 'active':true }");

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
            ParseDemoPatient.CheckBundleEntryNavigation(node.ToTypedElementLegacy());
#pragma warning restore 612, 618
        }

        [TestMethod]
        public void CatchesLowLevelErrors()
        {
            var tpXml = File.ReadAllText(Path.Combine("TestData", "with-errors.xml"));
            var patient = getXmlUntyped(tpXml);
            var result = patient.VisitAndCatch();
            var originalCount = result.Count;
            Assert.HasCount(11, result);
            Assert.IsFalse(result.Any(r => r.Message.Contains("schemaLocation")));

            patient = getXmlUntyped(tpXml, new FhirXmlParsingSettings() { DisallowSchemaLocation = true, PermissiveParsing = false });
            result = patient.VisitAndCatch();
            Assert.HasCount(originalCount + 1, result);    // one extra error about schemaLocation being present
            Assert.IsTrue(result.Any(r => r.Message.Contains("schemaLocation")));

            patient = FhirXmlNode.Parse(tpXml, new FhirXmlParsingSettings() { PermissiveParsing = true });
            result = patient.VisitAndCatch();
            Assert.IsEmpty(result);
        }

        [TestMethod]
        public void CatchesEmptyContainedResources()
        {
            var xml = "<Patient xmlns='http://hl7.org/fhir'><contained><OperationOutcome /></contained></Patient>";
            var pat = getXmlUntyped(xml);
            var errors = pat.VisitAndCatch();
            Assert.Contains("must have child elements", errors.Single().Message);

            xml = "<Patient xmlns='http://hl7.org/fhir'><contained /></Patient>";
            pat = getXmlUntyped(xml);
            errors = pat.VisitAndCatch();
            Assert.Contains("must have child elements", errors.Single().Message);
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
                Assert.Contains("Invalid Xml encountered", fe.Message);
            }
        }

    }
}