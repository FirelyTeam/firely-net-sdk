using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Hl7.Fhir.Model;
using System.Diagnostics;
using Hl7.Fhir.Serialization;

namespace Hl7.Fhir.Test.Serialization
{
    [TestClass]
    public class ResourceWritingTest
    {
        [TestMethod]
        public void TestWriteResource()
        {
            Stream s = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Hl7.Fhir.Test.TestPatient.json");

            SerializationConfig.AcceptUnknownMembers = true;
            SerializationConfig.AddModelAssembly(typeof(Resource).Assembly);

            var jsonReader = new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(s));
            var result = FhirParser.ParseResource(jsonReader);

            var xml = FhirSerializer.SerializeResourceToXml(result);
            var json = FhirSerializer.SerializeResourceToJson(result);

            //Stopwatch x = new Stopwatch();

            //x.Start();

            //for (int i = 0; i < 10000; i++)
            //{
            //    s.Seek(0, SeekOrigin.Begin);
            //    jsonReader = new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(s));
            //    root = JObject.Load(jsonReader);
            //    reader = new ResourceReader(new JsonDomFhirReader(root));
            //    result = reader.Deserialize();
            //}
            //x.Stop();

            //Debug.WriteLine(x.ElapsedMilliseconds);
            
        }
    }
}
