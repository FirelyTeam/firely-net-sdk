using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Tasks = System.Threading.Tasks;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class ValidationManifestTest
    {
        private static Validator _testValidator;
        private static DirectorySource _dirSource;        

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _dirSource = new DirectorySource(@"TestData\\validation-test-suite", new DirectorySourceSettings { IncludeSubDirectories = true });
            var zipSource = ZipSource.CreateValidationSource();
            var testResolver = new CachedResolver(new MultiResolver(zipSource, _dirSource));

            var settings = ValidationSettings.CreateDefault();
            settings.GenerateSnapshot = true;
            settings.GenerateSnapshotSettings = new Snapshot.SnapshotGeneratorSettings()
                                                {
                                                    ForceRegenerateSnapshots = true,
                                                    GenerateSnapshotForExternalProfiles = true,
                                                    GenerateElementIds = true
                                                };
            settings.ResourceResolver = testResolver;

           _testValidator = new Validator(settings);
        }

        [Ignore]
        [TestMethod]
        [DataTestMethod]
        [CustomDataSource]
        public async Tasks.Task TestValidationManifest(ValidationTestCase testCase)
        {
            await RunTestCase(testCase);
        } 

        public static async Tasks.Task RunTestCase(ValidationTestCase testCase)
        {            
            var resourceText = await File.ReadAllTextAsync(@$"TestData\validation-test-suite\{testCase.FileName}");
            var testResource = testCase.FileName.EndsWith(".xml") ?
                await new FhirXmlParser().ParseAsync<Resource>(resourceText) :
                await new FhirJsonParser().ParseAsync<Resource>(resourceText);
            Assert.IsNotNull(testResource);

            var profileFilePath = testCase?.Profiles?.FirstOrDefault() ?? testCase?.ValidationProfile?.Source;
            var profileUris = _dirSource.ListSummaries().Where(s => s.Origin.EndsWith(Path.DirectorySeparatorChar + profileFilePath)).Select(s => s.GetConformanceCanonicalUrl()).ToArray();

            OperationOutcome result = null;
            if (profileUris.Any())
            {
                result = _testValidator.Validate(testResource, profileUris);
            }
            else
            {
                result = _testValidator.Validate(testResource);
            }

            var javaErrors = testCase?.ValidationProfile?.Java?.ErrorCount ?? testCase?.Java?.ErrorCount;

            if (javaErrors != null)
            {

                Assert.AreEqual(javaErrors, result.Errors);
            }
            else
            {
                Assert.IsTrue(result.Success);
            }

            var javaWarnings = testCase?.ValidationProfile?.Java?.WarningCount ?? testCase?.Java?.WarningCount;

            if (javaWarnings != null)
            {
                Assert.AreEqual(javaWarnings, result.Warnings);
            }
        }
      
        [Ignore]
        [TestMethod]
        public async Tasks.Task RunSingleTest()
        {
            var testCase = ValidatorManifestParser.Parse().Where(t => t.FileName == "questionnaireResponse-enableWhen-test3.xml").FirstOrDefault();
            Assert.IsNotNull(testCase);
            await RunTestCase(testCase);
        }

    }

    internal class CustomDataSourceAttribute : Attribute, ITestDataSource
    {
        //Ignore these tests 
        private readonly string[] _ignoreTests = { };

        public string GetDisplayName(MethodInfo methodInfo, object[] data)
        {
            if(data.FirstOrDefault() is ValidationTestCase testCase)
            {
                return testCase?.FileName;
            }

            return null;
        }

        public IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            var data = ValidatorManifestParser.Parse();
            return data.Where(d=> d.Version != null && ModelInfo.CheckMinorVersionCompatibility(d.Version))
                       .Where(d=> !_ignoreTests.Contains(d.FileName))
                       .Select(e => new object[]{ e });
        }
    }

    internal static class ValidatorManifestParser
    {
        public static List<ValidationTestCase> Parse()
        {          
            var manifest = File.ReadAllText(@"TestData\validation-test-suite\manifest.json");
            var json = JObject.Parse(manifest);
            var testCases = CreateValidationCase(json);
            return testCases;
        }

        private static List<ValidationTestCase> CreateValidationCase(JObject json)
        {
            var validationCases = new List<ValidationTestCase>();
            foreach(var child in json["test-cases"].Children())
            {
                validationCases.Add(CreateValidationTestCase(child));
            }
            return validationCases;
        }

        private static ValidationTestCase CreateValidationTestCase(JToken json)
        {
            var values = json.FirstOrDefault()?.ToObject<JObject>();
            var validationObject = new ValidationTestCase
            {
                FileName = json.ToObject<JProperty>()?.Name,
                UseTest = values["usetest"]?.Value<bool>(),
                Version = values["version"]?.Value<string>(),
                Language = values["language"]?.Value<string>(),
                Questionnaire = values["questionnaire"]?.Value<string>(),
                AllowedExtensionsDomains = GetAllowedExtensionDomains(values),
                CodeSystems = ((JArray)values["questionnaire"])?.Values<string>()?.ToList(),
                Profiles = ((JArray)values["profiles"])?.Values<string>()?.ToList(),
                ValidationProfile = values["profile"] != null ? GetProfileInfo(values["profile"]) : null,
                Java = values["java"] != null ? GetJavaValidatorResults(values["java"]) : null,
                Logical = values["logical"] != null ? GetLogicalModelInfo(values["logical"]) : null
            };
            return validationObject;
        }

        private static ValidationTestCase.LogicalModel GetLogicalModelInfo(JToken json)
        {
            var values = json.ToObject<JObject>();
            return new ValidationTestCase.LogicalModel
            {
                Supporting = values["supporting"]?.Values<string>().ToList(),
                Expressions = values["expressions"]?.Values<string>().ToList(),
                Java = values["java"] != null ? GetJavaValidatorResults(values["java"]) : null
            };
        }

        private static ValidationTestCase.Profile GetProfileInfo(JToken json)
        {
            var values = json.ToObject<JObject>();
            return new ValidationTestCase.Profile
            {
                Source = values["source"]?.Value<string>(),
                Supporting = values["supporting"]?.Values<string>().ToList(),
                Java = values["java"] != null ? GetJavaValidatorResults(values["java"]) : null
            };
        }

        private static ValidationTestCase.ExpectedResult GetJavaValidatorResults(JToken json)
        {            
            var values = json.ToObject<JObject>();
            return new ValidationTestCase.ExpectedResult
            {
                ErrorCount = values["errorCount"]?.Value<int>(),
                WarningCount = values["warningCount"]?.Value<int>(),
                InfoCount = values["infoCount"]?.Value<int>(),
                Output = values["output"]?.Values<string>().ToList()
            };
        }

        private static List<string> GetAllowedExtensionDomains(JObject values)
        {
            var extensiondomains = new List<string>();

            if(values["allowed-extension-domain"] != null)
                extensiondomains.Add(values["allowed-extension-domain"].Value<string>());
            if(values["allowed-extension-domains"] != null)
                extensiondomains.AddRange(((JArray)values["allowed-extension-domains"])?.Values<string>());

            return extensiondomains;            
        }
    }
     
    public class ValidationTestCase { 

        public string FileName { get; set; }
        public bool? UseTest { get; set; }
        public List<string> AllowedExtensionsDomains { get; set; }
        public string Language { get; set; }
        public string Questionnaire { get; set; }
        public List<string> CodeSystems { get; set; }
        public List<string> Profiles { get; set; }
        public Profile ValidationProfile { get; set; }  
        public string Version { get; set; }
        public ExpectedResult Java { get; set; }
        public LogicalModel Logical { get; set; }


        public class ExpectedResult
        {
            public int? ErrorCount { get; set; }
            public int? WarningCount { get; set; }
            public int? InfoCount { get; set; }
            public List<string> Output { get; set; }
        }

        public class Profile
        {
            public string Source { get; set; }
            public List<string> Supporting { get; set; }
            public  ExpectedResult Java { get; set; }
        }

        public class LogicalModel
        {
            public List<string> Supporting { get; set; }
            public List<string> Expressions { get; set; }
            public ExpectedResult Java { get; set; }
        }
    }  
}
