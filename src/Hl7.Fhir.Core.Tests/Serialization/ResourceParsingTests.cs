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
using System.IO;
using Hl7.Fhir.Model;
using System.Diagnostics;

namespace Hl7.Fhir.Tests.Serialization
{
    [TestClass]
#if PORTABLE45
	public class PortableResourceParsingTests
#else
	public class ResourceParsingTests
#endif
    {
        [TestMethod]
        //public void AcceptXsiStuffOnRoot()
        public void ConfigureFailOnUnknownMember()
        {
            var xml = "<Patient xmlns='http://hl7.org/fhir'><daytona></daytona></Patient>";
            var parser = new FhirXmlParser();

            try
            {
                parser.Parse<Resource>(xml);
                Assert.Fail("Should have failed on unknown member");
            }
            catch (FormatException)
            {
            }

            parser.Settings.AcceptUnknownMembers = true;
            parser.Parse<Resource>(xml);
        }


        [TestMethod]
        public void AcceptXsiStuffOnRoot()
        {
            var xml = "<Patient xmlns='http://hl7.org/fhir' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' " +
                            "xsi:schemaLocation='http://hl7.org/fhir ../../schema/fhir-all.xsd'></Patient>";
            var parser = new FhirXmlParser();

            // By default, parser will accept xsi: elements
            parser.Parse<Resource>(xml);

            // Now, enforce xsi: attributes are no longer accepted
            parser.Settings.DisallowXsiAttributesOnRoot = true;

            try
            {
                parser.Parse<Resource>(xml);
                Assert.Fail("Should have failed on xsi: elements in root");
            }
            catch (FormatException)
            {
            }
        }


        [TestMethod]
        public void AcceptNsReassignments()
        {
            var xml = "<ns4:ValueSet xmlns:ns4=\"http://hl7.org/fhir\"><f:identifier xmlns:f=\"http://hl7.org/fhir\"><f:value value=\"....\"/></f:identifier></ns4:ValueSet>";

            FhirXmlParser.Parse<Resource>(xml);
            Assert.IsNotNull(FhirXmlParser.Parse<Resource>(xml));
        }


        [TestMethod]
        public void RetainSpacesInAttribute()
        {
            var xml = "<Basic xmlns='http://hl7.org/fhir'><extension url='http://blabla.nl'><valueString value='Daar gaat ie dan" + "&#xA;" + "verdwijnt dit?' /></extension></Basic>";

            var basic = FhirXmlParser.Parse<DomainResource>(xml);

            Assert.IsTrue(basic.GetStringExtension("http://blabla.nl").Contains("\n"));

            var outp = FhirSerializer.SerializeResourceToXml(basic);
            Assert.IsTrue(outp.Contains("&#xA;"));
        }

        internal FhirXmlParser FhirXmlParser = new FhirXmlParser();
        internal FhirJsonParser FhirJsonParser = new FhirJsonParser();


        [TestMethod]
        public void AcceptUnknownEnums()
        {
            string xml = File.ReadAllText(@"TestData\TestPatient.xml");
            var pser = new FhirXmlParser();

            // Assume that we can happily read the patient gender when enums are enforced
            var p = pser.Parse<Patient>(xml);

            Assert.IsNotNull(p.Gender);
            Assert.AreEqual("male", p.GenderElement.ObjectValue);
            Assert.AreEqual(AdministrativeGender.Male, p.Gender.Value);

            // Verify that if we relax the restriction that everything still works
            pser.Settings.AllowUnrecognizedEnums = true;
            p = pser.Parse<Patient>(xml);

            Assert.IsNotNull(p.Gender);
            Assert.AreEqual("male", p.GenderElement.ObjectValue);
            Assert.AreEqual(AdministrativeGender.Male, p.Gender.Value);


            // Now, pollute the data with an incorrect administrative gender
            // and verify that the system throws the format exception
            var xml2 = xml.Replace("\"male\"", "\"superman\"");

            try
            {
                pser.Settings.AllowUnrecognizedEnums = false;
                p = pser.Parse<Patient>(xml2);
                Assert.Fail();
            }
            catch(FormatException)
            {
                // By default, should *not* accept unknown enums
            }

            // Now, allow unknown enums and check support
            pser.Settings.AllowUnrecognizedEnums = true;
            p = pser.Parse<Patient>(xml2);
            Assert.IsNull(p.Gender);
            Assert.AreEqual("superman", p.GenderElement.ObjectValue);
        }

#if !PORTABLE45
        // This test doesn't work on the portable framework due to the 
        // JSON parser on mobile doesn't handle the large decimal values
        [TestMethod]
        public void EdgecaseRoundtrip()
        {
            string json = File.ReadAllText(@"TestData\json-edge-cases.json");
            var tempPath = Path.GetTempPath();

            var poco = FhirJsonParser.Parse<Resource>(json);
            Assert.IsNotNull(poco);
            var xml = FhirSerializer.SerializeResourceToXml(poco);
            Assert.IsNotNull(xml);
            File.WriteAllText(Path.Combine(tempPath, "edgecase.xml"), xml);

            poco = FhirXmlParser.Parse<Resource>(xml);
            Assert.IsNotNull(poco);
            var json2 = FhirSerializer.SerializeResourceToJson(poco);
            Assert.IsNotNull(json2);
            File.WriteAllText(Path.Combine(tempPath, "edgecase.json"), json2);
           
            JsonAssert.AreSame(json, json2);
        }
#endif

        [TestMethod]
        public void ContainedBaseIsNotAddedToId()
        {
            var p = new Patient() { Id = "jaap" };
            var o = new Observation() { Subject = new ResourceReference() { Reference = "#"+p.Id } };
            o.Contained.Add(p);
            o.ResourceBase = new Uri("http://nu.nl/fhir");

            var xml = FhirSerializer.SerializeResourceToXml(o);
            Assert.IsTrue(xml.Contains("value=\"#jaap\""));

            var o2 = FhirXmlParser.Parse<Observation>(xml);
            o2.ResourceBase = new Uri("http://nu.nl/fhir");
            xml = FhirSerializer.SerializeResourceToXml(o2);
            Assert.IsTrue(xml.Contains("value=\"#jaap\""));
        }

        [TestMethod]
        public void SerializeNarrativeWithQuotes()
        {
            var p = new Patient();
            p.Text = new Narrative() { Div = "<div xmlns=\"http://www.w3.org/1999/xhtml\">Nasty, a text with both \"double\" quotes and 'single' quotes</div>" };

            var xml = FhirSerializer.SerializeResourceToXml(p);
            Assert.IsNotNull(FhirXmlParser.Parse<Resource>(xml));
            var json = FhirSerializer.SerializeResourceToJson(p);
            Assert.IsNotNull(FhirJsonParser.Parse<Resource>(json));
        }

        // TODO: Unfortunately, this is currently too much work to validate. See comments on the bottom of
        // https://github.com/ewoutkramer/fhir-net-api/issues/20
        [TestMethod]
        public void CatchArrayWithNull()
        {
            var json = @"{
                'resourceType': 'Patient',
                'identifier': [null]
                }";

            try
            {
                var prof = FhirJsonParser.Parse<Resource>(json);
                Assert.Fail("Should have failed parsing");
            }
            catch (FormatException ex)
            {
                Assert.IsTrue(ex.Message.Contains("both be null"));
            }
        }
    }
}
