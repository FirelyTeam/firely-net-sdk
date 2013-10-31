using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.Serialization.Test
{
    [TestClass]
    public class ResourceReadingTest
    {
        [TestMethod]
        public void TestLoadResource()
        {
            Stream s = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Hl7.Fhir.Serialization.Test.TestPatient.json");
            var jsonReader = new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(s));
            var root = JObject.Load(jsonReader);
            BindingConfiguration.AcceptUnknownMembers = true;

            BindingConfiguration.ModelAssemblies.Add(typeof(Resource).Assembly);

            var inspector = new ModelInspector();
            inspector.Import(typeof(Resource).Assembly);
            inspector.Process();

            var reader = new ResourceReader(inspector, root);

            var result = reader.Deserialize();
        }
    }
}
