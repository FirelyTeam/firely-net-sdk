using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Serialization;
using System.Xml;
using Hl7.Fhir.Validation;
using System.ComponentModel.DataAnnotations;

namespace Hl7.Fhir.Test.Serialization
{
    /// <summary>
    /// Summary description for ValidationTest
    /// </summary>
    [TestClass]
    public class ValidationTest
    {
        public ValidationTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        [TestMethod]
        public void ValidateDemoPatient()
        {
            var s = this.GetType().Assembly.GetManifestResourceStream("Hl7.Fhir.Test.patient-example.xml");

            var patient = FhirParser.ParseResource(XmlReader.Create(s));

            ICollection<ValidationResult> dummy = new List<ValidationResult>();

            ModelValidator.Validate(patient);

            Assert.IsTrue(ModelValidator.TryValidate(patient, dummy));
        }
    }
}
