/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Serialization;
using System.IO;
using Hl7.Fhir.Model;
using Hl7.Fhir.Model.DSTU2;
using System.Diagnostics;
using System.Collections.Generic;
using Hl7.Fhir.ElementModel;
using System.Linq;

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
                Assert.IsTrue(fe.Message.Contains("expected the HL7 FHIR namespace"));
            }

            // The old parser failed on elements in different namespaces, that is wrong though - they should just be ignored
            //
            //      xml = "<Patient xmlns='http://hl7.org/fhir'><f:active value='false' xmlns:f='http://somehwere.else.nl' /></Patient>";
            //      
            //      try
            //      {
            //          parser.Parse<Resource>(xml);
            //          Assert.Fail("Should have thrown on Patient.active with incorrect namespace");
            //      }
            //      catch (FormatException fe)
            //      {
            //          Assert.IsTrue(fe.Message.Contains("which is not allowed"));
            //      }
        }

        [TestMethod]
        public void IgnoresDifferentNamespace()
        {
            var xml = "<Patient xmlns='http://hl7.org/fhir'><f:active value='false' xmlns:f='http://somehwere.else.nl' /><active value='true'/></Patient>";
            var parser = new FhirXmlParser(Fhir.Model.Version.DSTU2);
            var patient = parser.Parse<Patient>(xml);
            Assert.IsNotNull(patient.Active);
            Assert.IsTrue(patient.Active.Value);
        }

        [TestMethod]
        public void AcceptXsiStuffOnRoot()
        {
            var xml = "<Patient xmlns='http://hl7.org/fhir' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' " +
                            "xsi:schemaLocation='http://hl7.org/fhir ../../schema/fhir-all.xsd'><active value='true' /></Patient>";
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

            var valueSet = FhirDstu2XmlParser.Parse<Resource>(xml) as ValueSet;
            Assert.IsNotNull(valueSet);
            Assert.AreEqual("....", valueSet.Identifier?.Value);
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

            JsonAssert.AreSame(json, json2);
        }

        [TestMethod]
        public void JsonFhirComments()
        {
            var dstuPatientWithCommentJson = "{\"resourceType\":\"Patient\",\"name\":[{\"fhir_comments\":[\"Peter James Chalmers, but called Jim\"],\"family\":[\"Chalmers\"],\"given\":[\"Peter\",\"James\"]}],\"gender\":\"male\",\"birthDate\": \"1974-12-25\"}";
            var dstu2Patient = FhirDstu2JsonParser.Parse<Patient>(dstuPatientWithCommentJson);
            Assert.AreEqual("Chalmers", dstu2Patient.Name.SingleOrDefault()?.Family?.SingleOrDefault());

            var patientWithCommentJson = "{\"resourceType\":\"Patient\",\"name\":[{\"fhir_comments\":[\"Peter James Chalmers, but called Jim\"],\"family\":\"Chalmers\",\"given\":[\"Peter\",\"James\"]}],\"gender\":\"male\",\"birthDate\": \"1974-12-25\"}";
            var fhirStu3JsonParser = new FhirJsonParser(Fhir.Model.Version.STU3);
            var exception = Assert.ThrowsException<FormatException>(() => fhirStu3JsonParser.Parse<Fhir.Model.STU3.Patient>(patientWithCommentJson));
            Assert.IsTrue(exception.Message.Contains("The 'fhir_comments' feature is disabled."));
            var fhirR4JsonParser = new FhirJsonParser(Fhir.Model.Version.R4);
            exception = Assert.ThrowsException<FormatException>(() => fhirR4JsonParser.Parse<Fhir.Model.STU3.Patient>(patientWithCommentJson));
            Assert.IsTrue(exception.Message.Contains("The 'fhir_comments' feature is disabled."));
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

        [TestMethod]
        public void Dstu2QuantitySubTypesRoundTrip()
        {
            var p = new Patient();
            p.Extension = new List<Extension>
            {
                new Extension( "Age", new Fhir.Model.DSTU2.Age { Code = "year", Value = 40 } ),
                new Extension( "Count", new Fhir.Model.DSTU2.Count { Code = "1", Value = 17 } ),
                new Extension( "Distance", new Fhir.Model.DSTU2.Distance { Code = "km", Value = 42.5M } ),
                new Extension( "Duration", new Fhir.Model.DSTU2.Duration { Code = "s", Value = 3.72M } ),
                new Extension( "Money", new Fhir.Model.DSTU2.Money { Code = "USD", Value = 1000 } ),
                new Extension( "SimpleQuantity", new SimpleQuantity { Code = "ml/s", Value = 45.78M } ),
                new Extension( "Quantity", new Quantity { Code = "mol/s", Value = 3 } )
            };

            var xml = FhirDstu2XmlSerializer.SerializeToString(p);
            var patientFromXml = FhirDstu2XmlParser.Parse<Patient>(xml);

            // In DSTU2 the classes derived from Quantity are not separate FHIR types - so they are serialized/deserialized back to Quantity

            Assert.IsInstanceOfType(patientFromXml.Extension[0].Value, typeof(Quantity));
            Assert.AreEqual("year", ((Quantity)patientFromXml.Extension[0].Value).Code);
            Assert.AreEqual(40, ((Quantity)patientFromXml.Extension[0].Value).Value);
            Assert.IsInstanceOfType(patientFromXml.Extension[1].Value, typeof(Quantity));
            Assert.AreEqual("1", ((Quantity)patientFromXml.Extension[1].Value).Code);
            Assert.AreEqual(17, ((Quantity)patientFromXml.Extension[1].Value).Value);
            Assert.IsInstanceOfType(patientFromXml.Extension[2].Value, typeof(Quantity));
            Assert.AreEqual("km", ((Quantity)patientFromXml.Extension[2].Value).Code);
            Assert.AreEqual(42.5M, ((Quantity)patientFromXml.Extension[2].Value).Value);
            Assert.IsInstanceOfType(patientFromXml.Extension[3].Value, typeof(Quantity));
            Assert.AreEqual("s", ((Quantity)patientFromXml.Extension[3].Value).Code);
            Assert.AreEqual(3.72M, ((Quantity)patientFromXml.Extension[3].Value).Value);
            Assert.IsInstanceOfType(patientFromXml.Extension[4].Value, typeof(Quantity));
            Assert.AreEqual("USD", ((Quantity)patientFromXml.Extension[4].Value).Code);
            Assert.AreEqual(1000, ((Quantity)patientFromXml.Extension[4].Value).Value);
            Assert.IsInstanceOfType(patientFromXml.Extension[5].Value, typeof(Quantity));
            Assert.AreEqual("ml/s", ((Quantity)patientFromXml.Extension[5].Value).Code);
            Assert.AreEqual(45.78M, ((Quantity)patientFromXml.Extension[5].Value).Value);
            Assert.IsInstanceOfType(patientFromXml.Extension[6].Value, typeof(Quantity));
            Assert.AreEqual("mol/s", ((Quantity)patientFromXml.Extension[6].Value).Code);
            Assert.AreEqual(3, ((Quantity)patientFromXml.Extension[6].Value).Value);

            var json = FhirDstu2JsonSerializer.SerializeToString(p);
            var patientFromJson = FhirDstu2JsonParser.Parse<Patient>(json);

            Assert.IsInstanceOfType(patientFromJson.Extension[0].Value, typeof(Quantity));
            Assert.AreEqual("year", ((Quantity)patientFromJson.Extension[0].Value).Code);
            Assert.AreEqual(40, ((Quantity)patientFromJson.Extension[0].Value).Value);
            Assert.IsInstanceOfType(patientFromJson.Extension[1].Value, typeof(Quantity));
            Assert.AreEqual("1", ((Quantity)patientFromJson.Extension[1].Value).Code);
            Assert.AreEqual(17, ((Quantity)patientFromJson.Extension[1].Value).Value);
            Assert.IsInstanceOfType(patientFromJson.Extension[2].Value, typeof(Quantity));
            Assert.AreEqual("km", ((Quantity)patientFromJson.Extension[2].Value).Code);
            Assert.AreEqual(42.5M, ((Quantity)patientFromJson.Extension[2].Value).Value);
            Assert.IsInstanceOfType(patientFromJson.Extension[3].Value, typeof(Quantity));
            Assert.AreEqual("s", ((Quantity)patientFromJson.Extension[3].Value).Code);
            Assert.AreEqual(3.72M, ((Quantity)patientFromJson.Extension[3].Value).Value);
            Assert.IsInstanceOfType(patientFromJson.Extension[4].Value, typeof(Quantity));
            Assert.AreEqual("USD", ((Quantity)patientFromJson.Extension[4].Value).Code);
            Assert.AreEqual(1000, ((Quantity)patientFromJson.Extension[4].Value).Value);
            Assert.IsInstanceOfType(patientFromJson.Extension[5].Value, typeof(Quantity));
            Assert.AreEqual("ml/s", ((Quantity)patientFromJson.Extension[5].Value).Code);
            Assert.AreEqual(45.78M, ((Quantity)patientFromJson.Extension[5].Value).Value);
            Assert.IsInstanceOfType(patientFromJson.Extension[6].Value, typeof(Quantity));
            Assert.AreEqual("mol/s", ((Quantity)patientFromJson.Extension[6].Value).Code);
            Assert.AreEqual(3, ((Quantity)patientFromJson.Extension[6].Value).Value);
        }

        [TestMethod]
        public void Stu3QuantitySubTypesRoundTrip()
        {
            var p = new Fhir.Model.STU3.Patient();
            p.Extension = new List<Extension>
            {
                new Extension( "Age", new Fhir.Model.STU3.Age { Code = "year", Value = 40 } ),
                new Extension( "Count", new Fhir.Model.STU3.Count { Code = "1", Value = 17 } ),
                new Extension( "Distance", new Fhir.Model.STU3.Distance { Code = "km", Value = 42.5M } ),
                new Extension( "Duration", new Fhir.Model.STU3.Duration { Code = "s", Value = 3.72M } ),
                new Extension( "Money", new Fhir.Model.STU3.Money { Code = "USD", Value = 1000 } ),
                new Extension( "SimpleQuantity", new SimpleQuantity { Code = "ml/s", Value = 45.78M } ),
                new Extension( "Quantity", new Quantity { Code = "mol/s", Value = 3 } )
            };

            var xmlSerializer = new FhirXmlSerializer(Fhir.Model.Version.STU3);
            var xml = xmlSerializer.SerializeToString(p);
            var xmlParser = new FhirXmlParser(Fhir.Model.Version.STU3);
            var patientFromXml = xmlParser.Parse<Fhir.Model.STU3.Patient>(xml);

            // In STU3 the classes derived from Quantity are separate FHIR types - except SimpleQuantity, that is serialized/deserialized back to Quantity

            Assert.IsInstanceOfType(patientFromXml.Extension[0].Value, typeof(Fhir.Model.STU3.Age));
            Assert.AreEqual("year", ((Quantity)patientFromXml.Extension[0].Value).Code);
            Assert.AreEqual(40, ((Quantity)patientFromXml.Extension[0].Value).Value);
            Assert.IsInstanceOfType(patientFromXml.Extension[1].Value, typeof(Fhir.Model.STU3.Count));
            Assert.AreEqual("1", ((Quantity)patientFromXml.Extension[1].Value).Code);
            Assert.AreEqual(17, ((Quantity)patientFromXml.Extension[1].Value).Value);
            Assert.IsInstanceOfType(patientFromXml.Extension[2].Value, typeof(Fhir.Model.STU3.Distance));
            Assert.AreEqual("km", ((Quantity)patientFromXml.Extension[2].Value).Code);
            Assert.AreEqual(42.5M, ((Quantity)patientFromXml.Extension[2].Value).Value);
            Assert.IsInstanceOfType(patientFromXml.Extension[3].Value, typeof(Fhir.Model.STU3.Duration));
            Assert.AreEqual("s", ((Quantity)patientFromXml.Extension[3].Value).Code);
            Assert.AreEqual(3.72M, ((Quantity)patientFromXml.Extension[3].Value).Value);
            Assert.IsInstanceOfType(patientFromXml.Extension[4].Value, typeof(Fhir.Model.STU3.Money));
            Assert.AreEqual("USD", ((Quantity)patientFromXml.Extension[4].Value).Code);
            Assert.AreEqual(1000, ((Quantity)patientFromXml.Extension[4].Value).Value);
            Assert.IsInstanceOfType(patientFromXml.Extension[5].Value, typeof(Quantity));   // SimpleQuantity => Quantity
            Assert.AreEqual("ml/s", ((Quantity)patientFromXml.Extension[5].Value).Code);
            Assert.AreEqual(45.78M, ((Quantity)patientFromXml.Extension[5].Value).Value);
            Assert.IsInstanceOfType(patientFromXml.Extension[6].Value, typeof(Quantity));
            Assert.AreEqual("mol/s", ((Quantity)patientFromXml.Extension[6].Value).Code);
            Assert.AreEqual(3, ((Quantity)patientFromXml.Extension[6].Value).Value);

            var jsonSerializer = new FhirJsonSerializer(Fhir.Model.Version.STU3);
            var json = jsonSerializer.SerializeToString(p);
            var jsonParser = new FhirJsonParser(Fhir.Model.Version.STU3);
            var patientFromJson = jsonParser.Parse<Fhir.Model.STU3.Patient>(json);

            Assert.IsInstanceOfType(patientFromJson.Extension[0].Value, typeof(Fhir.Model.STU3.Age));
            Assert.AreEqual("year", ((Quantity)patientFromJson.Extension[0].Value).Code);
            Assert.AreEqual(40, ((Quantity)patientFromJson.Extension[0].Value).Value);
            Assert.IsInstanceOfType(patientFromJson.Extension[1].Value, typeof(Fhir.Model.STU3.Count));
            Assert.AreEqual("1", ((Quantity)patientFromJson.Extension[1].Value).Code);
            Assert.AreEqual(17, ((Quantity)patientFromJson.Extension[1].Value).Value);
            Assert.IsInstanceOfType(patientFromJson.Extension[2].Value, typeof(Fhir.Model.STU3.Distance));
            Assert.AreEqual("km", ((Quantity)patientFromJson.Extension[2].Value).Code);
            Assert.AreEqual(42.5M, ((Quantity)patientFromJson.Extension[2].Value).Value);
            Assert.IsInstanceOfType(patientFromJson.Extension[3].Value, typeof(Fhir.Model.STU3.Duration));
            Assert.AreEqual("s", ((Quantity)patientFromJson.Extension[3].Value).Code);
            Assert.AreEqual(3.72M, ((Quantity)patientFromJson.Extension[3].Value).Value);
            Assert.IsInstanceOfType(patientFromJson.Extension[4].Value, typeof(Fhir.Model.STU3.Money));
            Assert.AreEqual("USD", ((Quantity)patientFromJson.Extension[4].Value).Code);
            Assert.AreEqual(1000, ((Quantity)patientFromJson.Extension[4].Value).Value);
            Assert.IsInstanceOfType(patientFromJson.Extension[5].Value, typeof(Quantity));   // SimpleQuantity => Quantity
            Assert.AreEqual("ml/s", ((Quantity)patientFromJson.Extension[5].Value).Code);
            Assert.AreEqual(45.78M, ((Quantity)patientFromJson.Extension[5].Value).Value);
            Assert.IsInstanceOfType(patientFromJson.Extension[6].Value, typeof(Quantity));
            Assert.AreEqual("mol/s", ((Quantity)patientFromJson.Extension[6].Value).Code);
            Assert.AreEqual(3, ((Quantity)patientFromJson.Extension[6].Value).Value);
        }

        [TestMethod]
        public void R4QuantitySubTypesRoundTrip()
        {
            var p = new Fhir.Model.R4.Patient();
            p.Extension = new List<Extension>
            {
                new Extension( "Age", new Fhir.Model.R4.Age { Code = "year", Value = 40 } ),
                new Extension( "Count", new Fhir.Model.R4.Count { Code = "1", Value = 17 } ),
                new Extension( "Distance", new Fhir.Model.R4.Distance { Code = "km", Value = 42.5M } ),
                new Extension( "Duration", new Fhir.Model.R4.Duration { Code = "s", Value = 3.72M } ),
                new Extension( "Money", new Fhir.Model.R4.Money { Currency = Fhir.Model.R4.Currencies.USD, Value = 1000 } ),
                new Extension( "SimpleQuantity", new SimpleQuantity { Code = "ml/s", Value = 45.78M } ),
                new Extension( "Quantity", new Quantity { Code = "mol/s", Value = 3 } )
            };

            var xmlSerializer = new FhirXmlSerializer(Fhir.Model.Version.R4);
            var xml = xmlSerializer.SerializeToString(p);
            var xmlParser = new FhirXmlParser(Fhir.Model.Version.R4);
            var patientFromXml = xmlParser.Parse<Fhir.Model.R4.Patient>(xml);

            // In R4 the classes derived from Quantity are separate FHIR types - except SimpleQuantity, that is serialized/deserialized back to Quantity,
            // and Money, that is not derived from Quantity anymore

            Assert.IsInstanceOfType(patientFromXml.Extension[0].Value, typeof(Fhir.Model.R4.Age));
            Assert.AreEqual("year", ((Quantity)patientFromXml.Extension[0].Value).Code);
            Assert.AreEqual(40, ((Quantity)patientFromXml.Extension[0].Value).Value);
            Assert.IsInstanceOfType(patientFromXml.Extension[1].Value, typeof(Fhir.Model.R4.Count));
            Assert.AreEqual("1", ((Quantity)patientFromXml.Extension[1].Value).Code);
            Assert.AreEqual(17, ((Quantity)patientFromXml.Extension[1].Value).Value);
            Assert.IsInstanceOfType(patientFromXml.Extension[2].Value, typeof(Fhir.Model.R4.Distance));
            Assert.AreEqual("km", ((Quantity)patientFromXml.Extension[2].Value).Code);
            Assert.AreEqual(42.5M, ((Quantity)patientFromXml.Extension[2].Value).Value);
            Assert.IsInstanceOfType(patientFromXml.Extension[3].Value, typeof(Fhir.Model.R4.Duration));
            Assert.AreEqual("s", ((Quantity)patientFromXml.Extension[3].Value).Code);
            Assert.AreEqual(3.72M, ((Quantity)patientFromXml.Extension[3].Value).Value);
            Assert.IsInstanceOfType(patientFromXml.Extension[4].Value, typeof(Fhir.Model.R4.Money));
            Assert.AreEqual(Fhir.Model.R4.Currencies.USD, ((Fhir.Model.R4.Money)patientFromXml.Extension[4].Value).Currency);
            Assert.AreEqual(1000, ((Fhir.Model.R4.Money)patientFromXml.Extension[4].Value).Value);
            Assert.IsInstanceOfType(patientFromXml.Extension[5].Value, typeof(Quantity));   // SimpleQuantity => Quantity
            Assert.AreEqual("ml/s", ((Quantity)patientFromXml.Extension[5].Value).Code);
            Assert.AreEqual(45.78M, ((Quantity)patientFromXml.Extension[5].Value).Value);
            Assert.IsInstanceOfType(patientFromXml.Extension[6].Value, typeof(Quantity));
            Assert.AreEqual("mol/s", ((Quantity)patientFromXml.Extension[6].Value).Code);
            Assert.AreEqual(3, ((Quantity)patientFromXml.Extension[6].Value).Value);

            var jsonSerializer = new FhirJsonSerializer(Fhir.Model.Version.R4);
            var json = jsonSerializer.SerializeToString(p);
            var jsonParser = new FhirJsonParser(Fhir.Model.Version.R4);
            var patientFromJson = jsonParser.Parse<Fhir.Model.R4.Patient>(json);

            Assert.IsInstanceOfType(patientFromJson.Extension[0].Value, typeof(Fhir.Model.R4.Age));
            Assert.AreEqual("year", ((Quantity)patientFromJson.Extension[0].Value).Code);
            Assert.AreEqual(40, ((Quantity)patientFromJson.Extension[0].Value).Value);
            Assert.IsInstanceOfType(patientFromJson.Extension[1].Value, typeof(Fhir.Model.R4.Count));
            Assert.AreEqual("1", ((Quantity)patientFromJson.Extension[1].Value).Code);
            Assert.AreEqual(17, ((Quantity)patientFromJson.Extension[1].Value).Value);
            Assert.IsInstanceOfType(patientFromJson.Extension[2].Value, typeof(Fhir.Model.R4.Distance));
            Assert.AreEqual("km", ((Quantity)patientFromJson.Extension[2].Value).Code);
            Assert.AreEqual(42.5M, ((Quantity)patientFromJson.Extension[2].Value).Value);
            Assert.IsInstanceOfType(patientFromJson.Extension[3].Value, typeof(Fhir.Model.R4.Duration));
            Assert.AreEqual("s", ((Quantity)patientFromJson.Extension[3].Value).Code);
            Assert.AreEqual(3.72M, ((Quantity)patientFromJson.Extension[3].Value).Value);
            Assert.IsInstanceOfType(patientFromJson.Extension[4].Value, typeof(Fhir.Model.R4.Money));
            Assert.AreEqual(Fhir.Model.R4.Currencies.USD, ((Fhir.Model.R4.Money)patientFromJson.Extension[4].Value).Currency);
            Assert.AreEqual(1000, ((Fhir.Model.R4.Money)patientFromJson.Extension[4].Value).Value);
            Assert.IsInstanceOfType(patientFromJson.Extension[5].Value, typeof(Quantity));   // SimpleQuantity => Quantity
            Assert.AreEqual("ml/s", ((Quantity)patientFromJson.Extension[5].Value).Code);
            Assert.AreEqual(45.78M, ((Quantity)patientFromJson.Extension[5].Value).Value);
            Assert.IsInstanceOfType(patientFromJson.Extension[6].Value, typeof(Quantity));
            Assert.AreEqual("mol/s", ((Quantity)patientFromJson.Extension[6].Value).Code);
            Assert.AreEqual(3, ((Quantity)patientFromJson.Extension[6].Value).Value);
        }

        [TestMethod]
        public void Dstu2EmptyRoundTrip()
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

            var json = FhirDstu2JsonSerializer.SerializeToString(patient);
            var parsedPatient = FhirDstu2JsonParser.Parse<Patient>(json);

            Assert.AreEqual(patient.Identifier.Count, parsedPatient.Identifier.Count);
            for(var i=0; i<patient.Identifier.Count; i++)
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

            var xml = FhirDstu2XmlSerializer.SerializeToString(patient);
            parsedPatient = FhirDstu2XmlParser.Parse<Patient>(xml);

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
        public void NarrativeMustBeValidXml()
        {
            try
            {
                var json =
                    "{\"resourceType\": \"Patient\", \"text\": {\"status\": \"generated\", \"div\": \"text without div\" } }";
                var patient = new FhirJsonParser(new ParserSettings(Fhir.Model.Version.DSTU2) { PermissiveParsing = false }).Parse<Patient>(json);

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
            var parser = new FhirXmlParser(Fhir.Model.Version.DSTU2);

            Assert.ThrowsException<FormatException>(() => parser.Parse<Patient>(xml));
        }
    }
}
