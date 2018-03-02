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
using Hl7.Fhir.Model.DSTU2;
using System.Diagnostics;
using System.Collections.Generic;

namespace Hl7.Fhir.Tests.Serialization
{
    [TestClass]
    public class ResourceParsingTests
    {
        [TestMethod]
        public void ConfigureFailOnUnknownMember()
        {
            var xml = "<Patient xmlns='http://hl7.org/fhir'><daytona></daytona></Patient>";
            var parser = new FhirXmlParser(Fhir.Model.Version.DSTU2);

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
        public void ReturnsLineNumbersXml()
        {
            var xml = "<Patient xmlns='http://hl7.org/fhir'><iDontExist value='piet' /></Patient>";
            var parser = new FhirXmlParser(Fhir.Model.Version.DSTU2);

            try
            {
                parser.Parse<Resource>(xml);
                Assert.Fail("Should have thrown");
            }
            catch(FormatException fe)
            {
                Assert.IsFalse(fe.Message.Contains("pos -1"));
            }            
        }

        [TestMethod]
        public void ReturnsLineNumbersJson()
        {
            var xml = "<Patient xmlns='http://hl7.org/fhir'><iDontExist value='piet' /></Patient>";
            var parser = new FhirXmlParser(Fhir.Model.Version.DSTU2);

            try
            {
                parser.Parse<Resource>(xml);
                Assert.Fail("Should have thrown");
            }
            catch (FormatException fe)
            {
                Assert.IsFalse(fe.Message.Contains("pos -1"));
            }            
        }


        [TestMethod]
        public void RequiresHl7Namespace()
        {
            var xml = "<Patient><active value='false' /></Patient>";
            var parser = new FhirXmlParser(Fhir.Model.Version.DSTU2);

            try
            {
                parser.Parse<Resource>(xml);
                Assert.Fail("Should have thrown on Patient without namespace");
            }
            catch (FormatException fe)
            {
                Assert.IsTrue(fe.Message.Contains("Cannot derive type"));
            }

            xml = "<Patient xmlns='http://hl7.org/fhir'><f:active value='false' xmlns:f='http://somehwere.else.nl' /></Patient>";
            
            try
            {
                parser.Parse<Resource>(xml);
                Assert.Fail("Should have thrown on Patient.active with incorrect namespace");
            }
            catch (FormatException fe)
            {
                Assert.IsTrue(fe.Message.Contains("unsupported namespace"));
            }
        }

        [TestMethod]
        public void AcceptXsiStuffOnRoot()
        {
            var xml = "<Patient xmlns='http://hl7.org/fhir' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' " +
                            "xsi:schemaLocation='http://hl7.org/fhir ../../schema/fhir-all.xsd'></Patient>";
            var parser = new FhirXmlParser(Fhir.Model.Version.DSTU2);

            // By default, parser will accept xsi: elements
            parser.Parse<Resource>(xml);

            // Now, enforce xsi: attributes are no longer accepted
            parser.Settings.DisallowXsiAttributesOnRoot = true;

            try
            {
                parser.Parse<Resource>(xml);
                Assert.Fail("Should have failed on xsi: elements in root");
            }
            catch (FormatException fe)
            {
                Debug.WriteLine(fe.Message);
            }
        }


        [TestMethod]
        public void AcceptNsReassignments()
        {
            var xml = "<ns4:ValueSet xmlns:ns4=\"http://hl7.org/fhir\"><f:identifier xmlns:f=\"http://hl7.org/fhir\"><f:value value=\"....\"/></f:identifier></ns4:ValueSet>";

            FhirDstu2XmlParser.Parse<Resource>(xml);
            Assert.IsNotNull(FhirDstu2XmlParser.Parse<Resource>(xml));
        }


        [TestMethod]
        public void RetainSpacesInAttribute()
        {
            var xml = "<Basic xmlns='http://hl7.org/fhir'><extension url='http://blabla.nl'><valueString value='Daar gaat ie dan" + "&#xA;" + "verdwijnt dit?' /></extension></Basic>";

            var basic = FhirDstu2XmlParser.Parse<DomainResource>(xml);

            Assert.IsTrue(basic.GetStringExtension("http://blabla.nl").Contains("\n"));

            var outp = FhirDstu2XmlSerializer.SerializeToString(basic);
            Assert.IsTrue(outp.Contains("&#xA;"));
        }

        internal FhirXmlParser FhirDstu2XmlParser = new FhirXmlParser(Fhir.Model.Version.DSTU2);
        internal FhirJsonParser FhirDstu2JsonParser = new FhirJsonParser(Fhir.Model.Version.DSTU2);
        internal FhirXmlSerializer FhirDstu2XmlSerializer = new FhirXmlSerializer(Fhir.Model.Version.DSTU2);
        internal FhirJsonSerializer FhirDstu2JsonSerializer = new FhirJsonSerializer(Fhir.Model.Version.DSTU2);

        [TestMethod]
        public void ParsePerfJson()
        {
            string json = TestDataHelper.ReadTestData("TestPatient.json");

            // Assume that we can happily read the patient gender when enums are enforced
            var p = FhirDstu2JsonParser.Parse<Patient>(json);

            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < 500; i++)
                p = FhirDstu2JsonParser.Parse<Patient>(json);
            sw.Stop();
            Debug.WriteLine($"Parsing took {sw.ElapsedMilliseconds/500.0*1000} micros");
        }

        [TestMethod]
        public void ParsePerfXml()
        {
            string xml = TestDataHelper.ReadTestData("TestPatient.xml");

            // Assume that we can happily read the patient gender when enums are enforced
            var p = FhirDstu2XmlParser.Parse<Patient>(xml);

            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < 500; i++)
                p = FhirDstu2XmlParser.Parse<Patient>(xml);
            sw.Stop();
            Debug.WriteLine($"Parsing took {sw.ElapsedMilliseconds / 500.0 * 1000} micros");
        }

     
        [TestMethod]
        public void AcceptUnknownEnums()
        {
            string json = TestDataHelper.ReadTestData("TestPatient.json");
            var pser = new FhirJsonParser(Fhir.Model.Version.DSTU2);

            // Assume that we can happily read the patient gender when enums are enforced
            var p = pser.Parse<Patient>(json);
            Assert.IsNotNull(p.Gender);
            Assert.AreEqual("male", p.GenderElement.ObjectValue);
            Assert.AreEqual(AdministrativeGender.Male, p.Gender.Value);

            // Verify that if we relax the restriction that everything still works
            pser.Settings.AllowUnrecognizedEnums = true;
            p = pser.Parse<Patient>(json);

            Assert.IsNotNull(p.Gender);
            Assert.AreEqual("male", p.GenderElement.ObjectValue);
            Assert.AreEqual(AdministrativeGender.Male, p.Gender.Value);


            // Now, pollute the data with an incorrect administrative gender
            // and verify that the system throws the format exception
            var xml2 = json.Replace("\"male\"", "\"superman\"");

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

        // This test doesn't work on netcore due to the
        // JSON parser not handling large decimal values, so edit the file to skip the large decimal
        // and remove the Ignore here.
        [TestMethod]
        public void EdgecaseRoundtrip()
        {
            string json = TestDataHelper.ReadTestData("json-edge-cases.json");
            var tempPath = Path.GetTempPath();

            var poco = FhirDstu2JsonParser.Parse<Resource>(json);
            Assert.IsNotNull(poco);
            var xml = FhirDstu2XmlSerializer.SerializeToString(poco);
            Assert.IsNotNull(xml);
            File.WriteAllText(Path.Combine(tempPath, "edgecase.xml"), xml);

            poco = FhirDstu2XmlParser.Parse<Resource>(xml);
            Assert.IsNotNull(poco);
            var json2 = FhirDstu2JsonSerializer.SerializeToString(poco);
            Assert.IsNotNull(json2);
            File.WriteAllText(Path.Combine(tempPath, "edgecase.json"), json2);

            List<string> errors = new List<string>();
            JsonAssert.AreSame("edgecase.json", json, json2, errors);
            Assert.AreEqual(0, errors.Count, "Errors were encountered comparing converted content\r\n" + String.Join("\r\n", errors));
        }

        [TestMethod]
        public void ContainedBaseIsNotAddedToId()
        {
            var p = new Patient() { Id = "jaap" };
            var o = new Observation() { Subject = new ResourceReference() { Reference = "#" + p.Id } };
            o.Contained.Add(p);
            o.ResourceBase = new Uri("http://nu.nl/fhir");

            var xml = FhirDstu2XmlSerializer.SerializeToString(o);
            Assert.IsTrue(xml.Contains("value=\"#jaap\""));

            var o2 = FhirDstu2XmlParser.Parse<Observation>(xml);
            o2.ResourceBase = new Uri("http://nu.nl/fhir");
            xml = FhirDstu2XmlSerializer.SerializeToString(o2);
            Assert.IsTrue(xml.Contains("value=\"#jaap\""));
        }

        [TestMethod]
        public void SerializeNarrativeWithQuotes()
        {
            var p = new Patient();
            p.Text = new Narrative() { Div = "<div xmlns=\"http://www.w3.org/1999/xhtml\">Nasty, a text with both \"double\" quotes and 'single' quotes</div>" };

            var xml = FhirDstu2XmlSerializer.SerializeToString(p);
            Assert.IsNotNull(FhirDstu2XmlParser.Parse<Resource>(xml));
            var json = FhirDstu2JsonSerializer.SerializeToString(p);
            Assert.IsNotNull(FhirDstu2JsonParser.Parse<Resource>(json));
        }
    }
}
