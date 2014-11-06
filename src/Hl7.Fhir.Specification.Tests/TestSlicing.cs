using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Model;
using System.Xml.XPath;
using System.Xml;
using System.Linq;
using Hl7.Fhir.IO;
using Hl7.Fhir.Validation;


namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class TestSlicing
    {
        public SpecificationWorkspace Spec(string fileuri, string structure)
        {
            FhirFile.ExpandProfileFile("TestData\\" + fileuri + ".xml", "TestData\\testprofile.xml");
            Uri uri = new Uri("http://disk/testdata/testprofile.xml#"+structure);

            SpecificationProvider provider = SpecificationProvider.CreateOffline(new FileArtifactSource("TestData"));
            SpecificationBuilder builder = new SpecificationBuilder(provider);

            builder.Add(StructureFactory.PrimitiveTypes());
            builder.Add(StructureFactory.NonFhirNamespaces());
            builder.Add(uri);
            builder.Expand();

            return builder.ToSpecification();
        }

        public XPathNavigator Load(string filename, string resource)
        {
            filename = string.Format("TestData\\{0}.xml", filename);
            return FhirFile.LoadFeedResource(filename, resource);
        }

        [TestMethod]
        public void TestMethod1()
        {
            var spec = Spec("SlicingRegularTestProfile", "lipidPanel");
            var diagnosticreport = Load("message_SlicingRegularTestProfile", "DiagnosticReport");
            Report report = spec.Validate(diagnosticreport);
            var errors = report.Errors.ToList();
            Assert.IsTrue(report.Errors.Count() == 0);
        }

    }
}

