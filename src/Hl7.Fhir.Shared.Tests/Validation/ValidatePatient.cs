/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Xml;

namespace Hl7.Fhir.Tests.Validation
{
    [TestClass]
    public class ValidatePatient
    {
        [TestMethod]
        public void ValidateDemoPatient()
        {
            var s = new StringReader(TestDataHelper.ReadTestData(@"TestPatient.xml"));

            var patient = new FhirXmlParser().Parse<Patient>(XmlReader.Create(s));

            ICollection<ValidationResult> results = new List<ValidationResult>();

            foreach (var contained in patient.Contained) ((DomainResource)contained).Text = new Narrative() { Div = "<wrong />", Status = Narrative.NarrativeStatus.Generated };

            Assert.IsFalse(DotNetAttributeValidation.TryValidate(patient, results, true));
            Assert.IsTrue(results.Count > 0);

            results.Clear();
            foreach (DomainResource contained in patient.Contained) contained.Text = null;

            // Try again
            Assert.IsTrue(DotNetAttributeValidation.TryValidate(patient, results, true));

            patient.Identifier[0].System = "urn:oid:crap really not valid";

            results = new List<ValidationResult>();

            Assert.IsFalse(DotNetAttributeValidation.TryValidate(patient, results, true));
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void ValidatePatientWithDataAbsentExtension()
        {
            // Test for issue #3171 - Patient.Validate(true) throws NullReferenceException 
            // when BirthDate has data-absent-reason extension but no value
            var patient = new Patient()
            {
                BirthDateElement = new Date()
                {
                    Extension = new List<Extension>()
                    {
                        new Extension
                        {
                            Url = "http://hl7.org/fhir/StructureDefinition/data-absent-reason",
                            Value = new Code
                            {
                                Value = "unknown"
                            }
                        }
                    }
                }
            };

            // This should not throw an exception
            try
            {
                patient.Validate(true);
                // If we get here, the validation succeeded without throwing an exception
                Assert.IsTrue(true, "Validation completed without throwing an exception");
            }
            catch (System.NullReferenceException ex)
            {
                Assert.Fail($"Validation threw NullReferenceException: {ex.Message}");
            }

            // Also test with TryValidate
            ICollection<ValidationResult> results = new List<ValidationResult>();
            try
            {
                bool isValid = DotNetAttributeValidation.TryValidate(patient, results, true);
                // The validation may or may not pass (depends on other validation rules), 
                // but it should not throw an exception
                Assert.IsTrue(true, "TryValidate completed without throwing an exception");
            }
            catch (System.NullReferenceException ex)
            {
                Assert.Fail($"TryValidate threw NullReferenceException: {ex.Message}");
            }
        }
    }
}
