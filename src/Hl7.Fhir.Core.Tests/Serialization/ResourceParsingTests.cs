/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Serialization;
using System.IO;
using Hl7.Fhir.Model;
using System.Diagnostics;
using System.Collections.Generic;
using Tasks = System.Threading.Tasks;
using Hl7.Fhir.ElementModel;

namespace Hl7.Fhir.Tests.Serialization
{
    [TestClass]
    public class ResourceParsingTests
    {
        [TestMethod]
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
        public void ReturnsLineNumbersXml()
        {
            var xml = "<Patient xmlns='http://hl7.org/fhir'><iDontExist value='piet' /></Patient>";
            var parser = new FhirXmlParser();

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
            var parser = new FhirXmlParser();

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
            var parser = new FhirXmlParser(new ParserSettings() { PermissiveParsing = false});

            try
            {
                parser.Parse<Resource>(xml);
                Assert.Fail("Should have thrown on Patient without namespace");
            }
            catch (FormatException fe)
            {
                Assert.IsTrue(fe.Message.Contains("expected the HL7 FHIR namespace"));
            }

            xml = "<Patient xmlns='http://hl7.org/fhir'><f:active value='false' xmlns:f='http://somehwere.else.nl' /></Patient>";
            
            try
            {
                parser.Parse<Resource>(xml);
                Assert.Fail("Should have thrown on Patient.active with incorrect namespace");
            }
            catch (FormatException fe)
            {
                Assert.IsTrue(fe.Message.Contains("which is not allowed"));
            }
        }

        [TestMethod]
        public void AcceptXsiStuffOnRoot()
        {
            var xml = "<Patient xmlns='http://hl7.org/fhir' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' " +
                            "xsi:schemaLocation='http://hl7.org/fhir ../../schema/fhir-all.xsd'><active value='true' /></Patient>";
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
            catch (FormatException fe)
            {
                Debug.WriteLine(fe.Message);
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
        public async Tasks.Task RetainSpacesInAttribute()
        {
            var xml = "<Basic xmlns='http://hl7.org/fhir'><extension url='http://blabla.nl'><valueString value='Daar gaat ie dan" + "&#xA;" + "verdwijnt dit?' /></extension></Basic>";

            var basic = await FhirXmlParser.ParseAsync<DomainResource>(xml);

            Assert.IsTrue(basic.GetStringExtension("http://blabla.nl").Contains("\n"));

            var outp = await FhirXmlSerializer.SerializeToStringAsync(basic);
            Assert.IsTrue(outp.Contains("&#xA;"));
        }

        internal FhirXmlParser FhirXmlParser = new FhirXmlParser();
        internal FhirJsonParser FhirJsonParser = new FhirJsonParser();
        internal FhirXmlSerializer FhirXmlSerializer = new FhirXmlSerializer();
        internal FhirJsonSerializer FhirJsonSerializer = new FhirJsonSerializer();

        [TestMethod]
        public async Tasks.Task ParsePerfJson()
        {
            string json = TestDataHelper.ReadTestData("TestPatient.json");
            var pser = new FhirJsonParser();

            // Assume that we can happily read the patient gender when enums are enforced
            var p = await pser.ParseAsync<Patient>(json);

            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < 500; i++)
                p = await pser.ParseAsync<Patient>(json);
            sw.Stop();
            Debug.WriteLine($"Parsing took {sw.ElapsedMilliseconds/500.0*1000} micros");
        }

        [TestMethod]
        public void ParsePerfXml()
        {
            string xml = TestDataHelper.ReadTestData("TestPatient.xml");
            var pser = new FhirXmlParser();

            // Assume that we can happily read the patient gender when enums are enforced
            var p = pser.Parse<Patient>(xml);

            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < 500; i++)
                p = pser.Parse<Patient>(xml);
            sw.Stop();
            Debug.WriteLine($"Parsing took {sw.ElapsedMilliseconds / 500.0 * 1000} micros");
        }

     
        [TestMethod]
        public async Tasks.Task AcceptUnknownEnums()
        {
            string json = TestDataHelper.ReadTestData("TestPatient.json");
            var pser = new FhirJsonParser();

            // Assume that we can happily read the patient gender when enums are enforced
            var p = await pser.ParseAsync<Patient>(json);
            Assert.IsNotNull(p.Gender);
            Assert.AreEqual("male", p.GenderElement.ObjectValue);
            Assert.AreEqual(AdministrativeGender.Male, p.Gender.Value);

            // Verify that if we relax the restriction that everything still works
            pser.Settings.AllowUnrecognizedEnums = true;
            p = await pser.ParseAsync<Patient>(json);

            Assert.IsNotNull(p.Gender);
            Assert.AreEqual("male", p.GenderElement.ObjectValue);
            Assert.AreEqual(AdministrativeGender.Male, p.Gender.Value);


            // Now, pollute the data with an incorrect administrative gender
            // and verify that the system throws the format exception
            var xml2 = json.Replace("\"male\"", "\"superman\"");

            try
            {
                pser.Settings.AllowUnrecognizedEnums = false;
                p = await pser.ParseAsync<Patient>(xml2);
                Assert.Fail();
            }
            catch(FormatException)
            {
                // By default, should *not* accept unknown enums
            }

            // Now, allow unknown enums and check support
            pser.Settings.AllowUnrecognizedEnums = true;
            p = await pser.ParseAsync<Patient>(xml2);
            Assert.IsNull(p.Gender);
            Assert.AreEqual("superman", p.GenderElement.ObjectValue);
        }

        // This test doesn't work on netcore due to the
        // JSON parser not handling large decimal values, so edit the file to skip the large decimal
        // and remove the Ignore here.
        [TestMethod]
        public async Tasks.Task EdgecaseRoundtrip()
        {
            string json = TestDataHelper.ReadTestData("json-edge-cases.json");
            var tempPath = Path.GetTempPath();

            var poco = await FhirJsonParser.ParseAsync<Resource>(json);
            Assert.IsNotNull(poco);
            var xml = await FhirXmlSerializer.SerializeToStringAsync(poco);
            Assert.IsNotNull(xml);
            await File.WriteAllTextAsync(Path.Combine(tempPath, "edgecase.xml"), xml);

            poco = await FhirXmlParser.ParseAsync<Resource>(xml);
            Assert.IsNotNull(poco);
            var json2 = await FhirJsonSerializer.SerializeToStringAsync(poco);
            Assert.IsNotNull(json2);
            await File.WriteAllTextAsync(Path.Combine(tempPath, "edgecase.json"), json2);

            List<string> errors = new List<string>();
            JsonAssert.AreSame("edgecase.json", json, json2, errors);
            Console.WriteLine(String.Join("\r\n", errors));
            Assert.AreEqual(0, errors.Count, "Errors were encountered comparing converted content");
        }

        [TestMethod]
        public async Tasks.Task ContainedBaseIsNotAddedToId()
        {
            var p = new Patient() { Id = "jaap" };
            var o = new Observation() { Subject = new ResourceReference() { Reference = "#" + p.Id } };
            o.Contained.Add(p);
            o.ResourceBase = new Uri("http://nu.nl/fhir");

            var xml = await FhirXmlSerializer.SerializeToStringAsync(o);
            Assert.IsTrue(xml.Contains("value=\"#jaap\""));

            var o2 = await FhirXmlParser.ParseAsync<Observation>(xml);
            o2.ResourceBase = new Uri("http://nu.nl/fhir");
            xml = await FhirXmlSerializer.SerializeToStringAsync(o2);
            Assert.IsTrue(xml.Contains("value=\"#jaap\""));
        }


        [TestMethod]
        public async Tasks.Task EmptyRoundTrip()
        {
            var patient = new Patient
            {
                Identifier = new List<Identifier>
                {
                    new Identifier("https://mydomain.com/identifiers/Something", "123"),
                    new Identifier("https://mydomain.com/identifiers/Spaces", "   "),
                    new Identifier("https://mydomain.com/identifiers/Empty", string.Empty),
                    new Identifier("https://mydomain.com/identifiers/Null", null)
                }
            };

            var json = await FhirJsonSerializer.SerializeToStringAsync(patient);
            var parsedPatient = await FhirJsonParser.ParseAsync<Patient>(json);

            Assert.AreEqual(patient.Identifier.Count, parsedPatient.Identifier.Count);
            for (var i = 0; i < patient.Identifier.Count; i++)
            {
                Assert.AreEqual(patient.Identifier[i].System, parsedPatient.Identifier[i].System);
                if (string.IsNullOrWhiteSpace(patient.Identifier[i].Value))
                {
                    Assert.IsNull(parsedPatient.Identifier[i].Value);
                }
                else
                {
                    Assert.AreEqual(patient.Identifier[i].Value, parsedPatient.Identifier[i].Value);
                }
            }

            var xml = await FhirXmlSerializer.SerializeToStringAsync(patient);
            parsedPatient = await FhirXmlParser.ParseAsync<Patient>(xml);

            Assert.AreEqual(patient.Identifier.Count, parsedPatient.Identifier.Count);
            for (var i = 0; i < patient.Identifier.Count; i++)
            {
                Assert.AreEqual(patient.Identifier[i].System, parsedPatient.Identifier[i].System);
                if (string.IsNullOrWhiteSpace(patient.Identifier[i].Value))
                {
                    Assert.IsNull(parsedPatient.Identifier[i].Value);
                }
                else
                {
                    Assert.AreEqual(patient.Identifier[i].Value, parsedPatient.Identifier[i].Value);
                }
            }
        }


        [TestMethod]
        public async Tasks.Task SerializeNarrativeWithQuotes()
        {
            var p = new Patient
            {
                Text = new Narrative() { Div = "<div xmlns=\"http://www.w3.org/1999/xhtml\">Nasty, a text with both \"double\" quotes and 'single' quotes</div>" }
            };

            var xml = await FhirXmlSerializer.SerializeToStringAsync(p);
            Assert.IsNotNull(await FhirXmlParser.ParseAsync<Resource>(xml));
            var json = await FhirJsonSerializer.SerializeToStringAsync(p);
            Assert.IsNotNull(await FhirJsonParser.ParseAsync<Resource>(json));
        }

        [TestMethod]
        public async Tasks.Task NarrativeMustBeValidXml()
        {
            try
            {
                var json =
                    "{\"resourceType\": \"Patient\", \"text\": {\"status\": \"generated\", \"div\": \"text without div\" } }";
                var patient = await new FhirJsonParser(new ParserSettings { PermissiveParsing = false }).ParseAsync<Patient>(json);

                Assert.Fail("Should have thrown on invalid Div format");
            }
            catch (FormatException fe)
            {
                Assert.IsTrue(fe.Message.Contains("Invalid Xml encountered"));
            }
        }

        [TestMethod]
        public void ParseEmptyContained()
        {
            var xml = "<Patient xmlns='http://hl7.org/fhir'><contained></contained></Patient>";
            var parser = new FhirXmlParser();

            ExceptionAssert.Throws<StructuralTypeException>(() => parser.Parse<Patient>(xml));
        }
    }
}
