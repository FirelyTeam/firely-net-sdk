using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Serialization;
using System.Xml;
using System.Collections.Generic;
using Hl7.Fhir.Validation;
using System.ComponentModel.DataAnnotations;

namespace Hl7.Fhir.Test
{
    [TestClass]
    public class ValidatePatient
    {
        [TestMethod]
        public void ValidateDemoPatient()
        {
            var s = this.GetType().Assembly.GetManifestResourceStream("Hl7.Fhir.Test.patient-example.xml");

            var patient = FhirParser.ParseResource(XmlReader.Create(s));

            ICollection<ValidationResult> results = new List<ValidationResult>();

            FhirValidator.Validate(patient,true);

            Assert.IsTrue(FhirValidator.TryValidate(patient, results, true));
        }
    }
}
