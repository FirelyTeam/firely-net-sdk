using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class ValidationAnalysis
    {
        [TestMethod]
        public void Validate()
        {
            /*
            string docname = "C:\\temp\\output.html";
            var spec = Factory.GetPatientSpec(expand: true, online: false);
            var resource = FhirFile.LoadResource("TestData\\Patient.CardinalityMinus.xml");
            Report report = spec.Validate(resource);
            IPresenter presenter = new AnalysisPresenter(report);
            Document.RenderAndSave(presenter, docname);
            Process.Start(docname);
            */
        }
    }
}
