/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Serialization;
using System.Xml;
using System.Collections.Generic;
using Hl7.Fhir.Validation;
using System.ComponentModel.DataAnnotations;
using Hl7.Fhir.Model;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Hl7.Fhir.Test
{
    [TestClass]
    public class ValidatePatient
    {
        [TestMethod]
        public void ValidateDemoPatient()
        {
            var s = this.GetType().Assembly.GetManifestResourceStream("Hl7.Fhir.Test.patient-example.xml");

            var patient = (Patient)FhirParser.ParseResource(XmlReader.Create(s));

            ICollection<ValidationResult> results = new List<ValidationResult>();

            FhirValidator.Validate(patient,true);

            Assert.IsTrue(FhirValidator.TryValidate(patient, results, true));

            patient.Identifier[0].System = "urn:oid:crap really not valid";

            results = new List<ValidationResult>();
            
            Assert.IsFalse(FhirValidator.TryValidate(patient, results, true));
            Assert.IsTrue(results.Count > 0);
        }
    }
}
