using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Hl7.Fhir.Model;
using System.Diagnostics;
using Hl7.Fhir.Serialization;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Test.Serialization
{
    [TestClass]
    public class EdgeCasePatientRoundtrip
    {

        [TestMethod]
        public void Roundtrip()
        {
            Stream xmlExample = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Hl7.Fhir.Test.TestPatient.xml");
            string xml = new StreamReader(xmlExample).ReadToEnd();
            Stream jsonExample = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Hl7.Fhir.Test.TestPatient.json");
            string json = new StreamReader(jsonExample).ReadToEnd();

            SerializationConfig.AcceptUnknownMembers = true;
            SerializationConfig.AddModelAssembly(typeof(Resource).Assembly);

            var poco = FhirParser.ParseResourceFromXml(xml);
            Assert.IsNotNull(poco);
            var output = FhirSerializer.SerializeResourceToXml(poco);
            Assert.IsNotNull(output);
            XmlAssert.AreSame(xml, output);

            poco = FhirParser.ParseResourceFromJson(json);
            Assert.IsNotNull(poco);
            output = FhirSerializer.SerializeResourceToJson(poco);
            Assert.IsNotNull(output);
            JsonAssert.AreSame(json, output);  
        }
    }
}
