using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Serialization;

namespace Hl7.Fhir.Test.Serialization
{
    [TestClass]
    public class ResourceParsingTests
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

            SerializationConfig.EnforceNoXsiAttributesInRoot = true;

            try
            {
                FhirParser.ParseResourceFromXml(xml);
                Assert.Fail("Should have failed on xsi: elements in root");
            }
            catch (FormatException fe)
            {
            }
        }
    }
}
