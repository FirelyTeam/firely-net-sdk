using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.ModelBinding.Test
{
    [TestClass]
    public class ComplexMemberReaderTest
    {
        [TestMethod]
        public void TestLoadObject()
        {
            Stream s = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Hl7.Fhir.ModelBinding.Test.TestPatient.json");
            var jsonReader = new Newtonsoft.Json.JsonTextReader(new System.IO.StreamReader(s));
            var root = JObject.Load(jsonReader);
            BindingConfiguration.AcceptUnknownMembers = true;

           // var objReader = new ComplexMemberReader(root);
           // var result = objReader.Deserialize(typeof(Patient));

            BindingConfiguration.ModelAssemblies.Add(typeof(Resource).Assembly);

            var reader = new DispatchingReader(root);
            var result = reader.Deserialize();
        }
    }
}
