/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
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

            var patient = new FhirXmlDeserializer().Deserialize<Patient>(XmlReader.Create(s));
            
            foreach (var contained in patient.Contained) ((DomainResource)contained).Text = new Narrative() { Div = "<wrong />", Status = Narrative.NarrativeStatus.Generated };

            patient.Validate().Should().NotBeEmpty();
            
            foreach (DomainResource contained in patient.Contained) contained.Text = null;

            // Try again
            patient.Validate().Should().BeEmpty();

            patient.Identifier[0].System = "urn:oid:crap really not valid";

            patient.Validate().Should().NotBeEmpty();
        }
    }
}
