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
        public ISourceNavigator getXmlNavU(string xml, FhirXmlNavigatorSettings settings = null) =>
            FhirXmlNavigator.Untyped(xml, settings);                    

        // This test should resurface once you read this through a validating reader navigator (or somesuch)
        [TestMethod]
        public void CanReadThroughUntypedNavigator()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var nav = getXmlNavU(tpXml);
            ParseDemoPatient.CanReadThroughNavigator(nav.ToElementNavigator(), typed:false);
        }

        [TestMethod]
        public void CloningWorks()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var nav = getXmlNavU(tpXml);
            ParseDemoPatient.CloningWorks(nav);
        }


        [TestMethod]
        public void ElementNavPerformanceUntypedXml()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var nav = getXmlNavU(tpXml);
            ParseDemoPatient.ElementNavPerformance(nav.ToElementNavigator());
        }

        [TestMethod]
        public void ProducesCorrectUntypedLocations()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var patient = getXmlNavU(tpXml);

            ParseDemoPatient.ProducesCorrectUntypedLocations(patient);
        }


        [TestMethod]
        public void ReadsAttributesAsElements()
        {
            var nav = getXmlNavU("<Patient xmlns='http://hl7.org/fhir' xmlns:q='http://example.org' q:myattr='dummy' " +
                "anotherattr='nons' />", 
                new FhirXmlNavigatorSettings { AllowedExternalNamespaces = new[] { XNamespace.Get("http://example.org") } });
            
            Assert.IsTrue(nav.MoveToFirstChild());
            Assert.AreEqual("myattr", nav.Name);        // none-xmlns attributes will come through
            var xmldetails = (nav as IAnnotated).Annotation<XmlSerializationDetails>();
            Assert.AreEqual(XNamespace.Get("http://example.org"), xmldetails.Namespace);
            Assert.AreEqual("Patient.myattr[0]", nav.Path);

            Assert.IsTrue(nav.MoveToNext());
            Assert.AreEqual("anotherattr", nav.Name);        // none-xmlns attributes will come through
            xmldetails = (nav as IAnnotated).Annotation<XmlSerializationDetails>();
            Assert.AreEqual(XNamespace.None, xmldetails.Namespace);
        }


        [TestMethod]
        public void HasLineNumbers()
        {
            var tpXml = File.ReadAllText(@"TestData\fp-test-patient.xml");
            var nav = getXmlNavU(tpXml);

            ParseDemoPatient.HasLineNumbers<XmlSerializationDetails>(nav.ToElementNavigator());
        }

        [TestMethod]
        public void TestPermissiveParsing()
        {
            var tpXml = File.ReadAllText(@"TestData\all-xml-features.xml");

            // will allow whitespace and comments to come through
            var reader = XmlReader.Create(new StringReader(tpXml));
            var nav = FhirXmlNavigator.Untyped(reader, new FhirXmlNavigatorSettings { PermissiveParsing = true });

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

            nav.MoveToFirstChild(); assertAnElement(nav.Clone());
            nav.MoveToNext(); assertAnElementWithValueAndChildren(nav.Clone());
            nav.MoveToNext(); assertDiv(nav.Clone());
            Assert.IsFalse(nav.MoveToNext());

            void assertAnElement(ISourceNavigator cn)
            {
                Assert.AreEqual("anElement", cn.Name);
                Assert.AreEqual("true", cn.Text);
                Assert.AreEqual(1, cn.Children().Count());
                cn.MoveToFirstChild();

                Assert.AreEqual("customAttribute", cn.Name);
                Assert.AreEqual("primitive", cn.Text);

                var xd = (cn as IAnnotated).Annotation<XmlSerializationDetails>();
                Assert.AreEqual(XmlNodeType.Attribute, xd.NodeType);
                Assert.AreEqual(xd.Namespace + "customAttribute", XName.Get("customAttribute", "http://example.org/some-ns"));
                Assert.IsFalse(cn.Children().Any());
            }

            void assertAnElementWithValueAndChildren(ISourceNavigator cn)
            {
                Assert.AreEqual("anElementWithValueAndChildren", cn.Name);
                Assert.AreEqual("4", cn.Text);

                var mylittledetails = (cn as IAnnotated).Annotation<XmlSerializationDetails>();
                Assert.IsTrue(mylittledetails.NodeText.Contains("Crap, mixed content!"));
                Assert.IsTrue(mylittledetails.NodeText.Contains("Is Merged"));

                Assert.IsTrue(cn.MoveToFirstChild());
                firstChild(cn.Clone());
                cn.MoveToNext(); secondChild(cn.Clone());
                cn.MoveToNext(); thirdChild(cn.Clone());
                Assert.IsFalse(cn.MoveToNext());

                void firstChild(ISourceNavigator ccn)
                {
                    Assert.AreEqual("firstChild", ccn.Name);
                    Assert.IsNull(ccn.Text);
                    Assert.AreEqual(1, ccn.Children().Count());

                    var xd = (ccn as IAnnotated).Annotation<XmlSerializationDetails>();
                    Assert.AreEqual("I have text content", xd.NodeText);

                    ccn.MoveToFirstChild();
                    Assert.AreEqual("customAttribute", ccn.Name);
                    Assert.AreEqual("morning", ccn.Text);
                    Assert.IsFalse(ccn.Children().Any());
                }

                void secondChild(ISourceNavigator ccn)
                {
                    Assert.AreEqual("secondChild", ccn.Name);
                    Assert.AreEqual("afternoon", ccn.Text);
                    Assert.IsFalse(ccn.Children().Any());

                    var xd = (ccn as IAnnotated).Annotation<XmlSerializationDetails>();
                    Assert.AreEqual("I have text content too", xd.NodeText);
                }

                void thirdChild(ISourceNavigator ccn)
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

            void assertDiv(ISourceNavigator cnn)
            {
                var val = cnn.Text;
                Assert.IsTrue(val.StartsWith("<div") && val.Contains("Some html"));
                Assert.IsFalse(cnn.Children().Any());  // html should not be represented as children

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
            ParseDemoPatient.RoundtripXml(xmlText => FhirXmlNavigator.Untyped(xmlText).ToElementNavigator());
        }

        [TestMethod]
        public void TryInvalidUntypedSource()
        {
            var jsonNav = FhirJsonNavigator.Untyped("{ 'resourceType': 'Patient', 'active':true }");

            try
            {
                var xmlWriter = new FhirXmlWriter();

                var output = SerializationUtil.WriteXmlToString(writer => xmlWriter.Write(jsonNav, writer));
                Assert.Fail();
            }
            catch(NotSupportedException)
            {
            }
        }

        [TestMethod]
        public void CheckBundleEntryNavigation()
        {
            var bundleXml = File.ReadAllText(@"TestData\BundleWithOneEntry.xml");
            var xmlNav = getXmlNavU(bundleXml).ToElementNavigator();
            var entryNav = xmlNav.Select("entry.resource").First();
            var id = entryNav.Scalar("id");
            Assert.IsNotNull(id);
        }

        [TestMethod]
        public void CatchesLowLevelErrors()
        {
            var tpXml = File.ReadAllText(@"TestData\with-errors.xml");
            var patient = getXmlNavU(tpXml);
            var result = ParseDemoPatient.VisitAndCatch(patient);
            var originalCount = result.Count;
            Assert.AreEqual(11,result.Count);
            Assert.IsTrue(!result.Any(r => r.Message.Contains("schemaLocation")));

            patient = getXmlNavU(tpXml, new FhirXmlNavigatorSettings() { DisallowSchemaLocation = true });
            result = ParseDemoPatient.VisitAndCatch(patient);
            Assert.IsTrue(result.Count == originalCount+1);    // one extra error about schemaLocation being present
            Assert.IsTrue(result.Any(r => r.Message.Contains("schemaLocation")));

            patient = getXmlNavU(tpXml, new FhirXmlNavigatorSettings() { PermissiveParsing = true });
            result = ParseDemoPatient.VisitAndCatch(patient);
            Assert.AreEqual(0, result.Count);
        }
    }
}