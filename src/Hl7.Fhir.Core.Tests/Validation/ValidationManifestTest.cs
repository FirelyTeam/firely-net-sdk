using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace Hl7.Fhir.Tests.Validation
{
    [TestClass]
    public class ValidationManifestTest
    {
        [TestMethod]
        public void TestInit()
        {
            var tests = new ValidatorManifestParser().Parse(); 
        }
    }

    internal class ValidatorManifestParser
    {
        public List<ValidationTestCase> Parse()
        {
          
            var manifest = File.ReadAllText(@"Validation\\ValidatorTestData\\manifest.json");
            var json = JObject.Parse(manifest);
            var testCases = CreateValidationCase(json);
            return testCases;
        }

        private List<ValidationTestCase> CreateValidationCase(JObject json)
        {
            var validationCases = new List<ValidationTestCase>();
            foreach(var child in json["test-cases"].Children())
            {
                validationCases.Add(CreateValidationTestCase(child));
            }
            return validationCases;
        }

        private ValidationTestCase CreateValidationTestCase(JToken json)
        {
            var values = json.FirstOrDefault()?.ToObject<JObject>();
            var validationObject = new ValidationTestCase
            {
                Name = json.ToObject<JProperty>().Name,
                UseTest = json["usetest"]?.Value<bool>(),
                Version = values["version"]?.Value<string>()
            };
            return validationObject;
        }
    }
     
    internal class ValidationTestCase { 

        public string Name { get; set; }
        public bool? UseTest { get; set; }
        public List<string> AllowedExtensionsDomains { get; set; }
        public string Language { get; set; }
        public string Questionnaires { get; set; }
        public List<string> CodeSystems { get; set; }
        public List<string> Profiles { get; set; }
        public string Version { get; set; }
        public ExpectedResult Java { get; set; }


        internal class ExpectedResult
        {
            public int? ErrorCount { get; set; }
            public int? WarningCount { get; set; }
            public int? InfoCount { get; set; }
            public string Output { get; set; }
        }

        internal class ValidationProfile
        {
            public string Profile { get; set; }
            public List<string> Supporting { get; set; }
            public  ExpectedResult Java { get; set; }
        }

        internal class ValidationLogicalModel
        {
            public List<string> Supporting { get; set; }
            public List<string> Expressions { get; set; }
            public ExpectedResult Java { get; set; }
        }

    }


  
}
