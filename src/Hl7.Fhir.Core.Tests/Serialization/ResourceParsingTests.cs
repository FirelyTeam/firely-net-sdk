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

            try
            {
                FhirParser.ParseResourceFromXml(xml);
                Assert.Fail("Should have failed on unknown member");
            }
            catch (FormatException)
            {
            }

            SerializationConfig.AcceptUnknownMembers = true;
            FhirParser.ParseResourceFromXml(xml);
        }


        [TestMethod]
        public void AcceptXsiStuffOnRoot()
        {
            var xml = "<Patient xmlns='http://hl7.org/fhir' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' " +
                            "xsi:schemaLocation='http://hl7.org/fhir ../../schema/fhir-all.xsd'></Patient>";

            FhirParser.ParseResourceFromXml(xml);

            SerializationConfig.EnforceNoXsiAttributesOnRoot = true;

            try
            {
                FhirParser.ParseResourceFromXml(xml);
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

            FhirParser.ParseResourceFromXml(xml);
            Assert.IsNotNull(FhirParser.ParseResourceFromXml(xml));
        }


        [TestMethod]
        public void RetainSpacesInAttribute()
        {
            var xml = "<Basic xmlns='http://hl7.org/fhir'><extension url='http://blabla.nl'><valueString value='Daar gaat ie dan" + "&#xA;" + "verdwijnt dit?' /></extension></Basic>";

            var basic = (DomainResource)FhirParser.ParseFromXml(xml);

            Assert.IsTrue(basic.GetStringExtension("http://blabla.nl").Contains("\n"));

            var outp = FhirSerializer.SerializeResourceToXml(basic);
            Assert.IsTrue(outp.Contains("&#xA;"));
        }

        [TestMethod]
        public void EdgecaseRoundtrip()
        {
            string json = File.ReadAllText(@"TestData\json-edge-cases.json");

            var poco = FhirParser.ParseResourceFromJson(json);
            Assert.IsNotNull(poco);
            var xml = FhirSerializer.SerializeResourceToXml(poco);
            Assert.IsNotNull(xml);
            File.WriteAllText(@"c:\temp\edgecase.xml", xml);

            poco = FhirParser.ParseResourceFromXml(xml);
            Assert.IsNotNull(poco);
            var json2 = FhirSerializer.SerializeResourceToJson(poco);
            Assert.IsNotNull(json2);
            File.WriteAllText(@"c:\temp\edgecase.json", json2);
           
            JsonAssert.AreSame(json, json2);
        }


        [TestMethod]
        public void ContainedBaseIsNotAddedToId()
        {
            var p = new Patient() { Id = "jaap" };
            var o = new Observation() { Subject = new ResourceReference() { Reference = "#"+p.Id } };
            o.Contained.Add(p);
            o.ResourceBase = new Uri("http://nu.nl/fhir");

            var xml = FhirSerializer.SerializeResourceToXml(o);
            Assert.IsTrue(xml.Contains("value=\"#jaap\""));

            var o2 = FhirParser.ParseFromXml(xml) as Observation;
            o2.ResourceBase = new Uri("http://nu.nl/fhir");
            xml = FhirSerializer.SerializeResourceToXml(o2);
            Assert.IsTrue(xml.Contains("value=\"#jaap\""));
        }

        [TestMethod]
        public void SerializeSummaryWithQuotes()
        {
            var p = new Patient();
            p.Text = new Narrative() { Div = "<div xmlns=\"http://www.w3.org/1999/xhtml\">Nasty, a text with both \"double\" quotes and 'single' quotes</div>" };

            var xml = FhirSerializer.SerializeResourceToXml(p);
            Assert.IsNotNull(FhirParser.ParseResourceFromXml(xml));
            var json = FhirSerializer.SerializeResourceToJson(p);
            Assert.IsNotNull(FhirParser.ParseResourceFromJson(json));
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
                var prof = FhirParser.ParseResourceFromJson(json);
                Assert.Fail("Should have failed parsing");
            }
            catch (FormatException ex)
            {
                Assert.IsTrue(ex.Message.Contains("both be null"));
            }
        }
    }
}
