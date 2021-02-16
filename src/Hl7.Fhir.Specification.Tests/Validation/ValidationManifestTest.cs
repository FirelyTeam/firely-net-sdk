using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Specification.Source;
using Hl7.Fhir.Specification.Terminology;
using Hl7.Fhir.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hl7.Fhir.Specification.Tests
{
    [TestClass]
    public class ValidationManifestTest
    {
        [Flags]
        enum AssertionOptions
        {
            NoAssertion = 1 << 1,
            JavaAssertion = 1 << 2,
            FirelySdkAssertion = 1 << 3,
            OutputTextAssertion = 1 << 4
        }

        private static Validator _testValidator;
        private static DirectorySource _dirSource;
        private static List<TestCase> _testCases = new List<TestCase>(); // only used by AddFirelySdkResults

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _dirSource = new DirectorySource(@"TestData\\validation-test-suite", new DirectorySourceSettings { IncludeSubDirectories = true });
            var zipSource = ZipSource.CreateValidationSource();
            var resolver = new CachedResolver(new MultiResolver(zipSource, _dirSource));

            var settings = ValidationSettings.CreateDefault();
            settings.GenerateSnapshot = true;
            settings.GenerateSnapshotSettings = new Snapshot.SnapshotGeneratorSettings()
            {
                ForceRegenerateSnapshots = true,
                GenerateSnapshotForExternalProfiles = true,
                GenerateElementIds = true
            };
            settings.ResourceResolver = resolver;
            settings.TerminologyService = new LocalTerminologyService(resolver);

            _testValidator = new Validator(settings);
        }

        [Ignore]
        [DataTestMethod]
        [ValidationManifestDataSource(@"TestData\validation-test-suite\manifest.json", ignoreTests: new[] { "message", "message-empty-entry" })]
        public void TestValidationManifest(TestCase testCase) => runTestCase(testCase);

        // [Ignore]
        [DataTestMethod]
        [ValidationManifestDataSource(@"TestData\validation-test-suite\manifest.json", singleTest: "nl/nl-core-patient-01")]
        public void RunSingleTest(TestCase testCase) => runTestCase(testCase);

        [DataTestMethod]
        [ValidationManifestDataSource(@"TestData\validation-test-suite\manifest-with-firelysdk-results.json")]
        public void RunFirelySdkTests(TestCase testCase) => runTestCase(testCase, AssertionOptions.FirelySdkAssertion | AssertionOptions.OutputTextAssertion);

        private (OperationOutcome, OperationOutcome) runTestCase(TestCase testCase, AssertionOptions options = AssertionOptions.JavaAssertion)
        {
            var testResource = parseResource(@$"TestData\validation-test-suite\{testCase.FileName}");

            OperationOutcome outcomeWithProfile = null;
            if (testCase.Profile?.Source is { } source)
            {
                var profileUri = _dirSource.ListSummaries().First(s => s.Origin.EndsWith(Path.DirectorySeparatorChar + source)).GetConformanceCanonicalUrl();

                outcomeWithProfile = _testValidator.Validate(testResource, profileUri);
                assertResult(options.HasFlag(AssertionOptions.JavaAssertion) ? testCase.Profile.Java : testCase.Profile.FirelySDK, outcomeWithProfile, options);
            }

            OperationOutcome outcome = _testValidator.Validate(testResource);
            assertResult(options.HasFlag(AssertionOptions.JavaAssertion) ? testCase.Java : testCase.FirelySDK, outcome, options);

            return (outcome, outcomeWithProfile);
        }

        private void assertResult(ExpectedResult result, OperationOutcome outcome, AssertionOptions options)
        {
            if (options.HasFlag(AssertionOptions.NoAssertion)) return; // no assertion asked

            result.Should().NotBeNull("There should be an expected result");

            (outcome.Errors + outcome.Fatals).Should().Be(result.ErrorCount ?? 0);
            outcome.Warnings.Should().Be(result.WarningCount ?? 0);

            if (options.HasFlag(AssertionOptions.OutputTextAssertion))
            {
                outcome.Issue.Select(i => i.ToString()).ToList().Should().BeEquivalentTo(result.Output);
            }
        }

        private Resource parseResource(string fileName)
        {
            var resourceText = File.ReadAllText(fileName);
            var testResource = fileName.EndsWith(".xml") ?
                new FhirXmlParser().Parse<Resource>(resourceText) :
                new FhirJsonParser().Parse<Resource>(resourceText);
            Assert.IsNotNull(testResource);
            return testResource;
        }

        private ExpectedResult writeFirelySDK(OperationOutcome outcome)
        {
            return new ExpectedResult
            {
                ErrorCount = outcome.Errors + outcome.Fatals,
                WarningCount = outcome.Warnings,
                Output = outcome.Issue.Select(i => i.ToString()).ToList()
            };
        }

        [Ignore]
        [DataTestMethod]
        [ValidationManifestDataSource(@"TestData\validation-test-suite\manifest.json", ignoreTests: new[] { "message", "message-empty-entry" })]
        public void AddFirelySdkResults(TestCase testCase)
        {
            var (outcome, outcomeProfile) = runTestCase(testCase, AssertionOptions.NoAssertion);

            testCase.FirelySDK = writeFirelySDK(outcome);
            if (outcomeProfile != null)
            {
                testCase.Profile.FirelySDK = writeFirelySDK(outcomeProfile);
            }

            _testCases.Add(testCase);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            if (_testCases.Any())
            {
                var newManifest = new Manifest
                {
                    TestCases = _testCases
                };

                var json = JsonSerializer.Serialize(newManifest,
                                new JsonSerializerOptions()
                                {
                                    WriteIndented = true,
                                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                                });
                File.WriteAllText(@"..\..\..\TestData\validation-test-suite\manifest-with-firelysdk-results.json", json);
            }
        }

        [Ignore]
        [TestMethod]
        public void RoundTripTest()
        {
            var expected = File.ReadAllText(@"TestData\validation-test-suite\manifest.json");
            var manifest = JsonSerializer.Deserialize<Manifest>(expected, new JsonSerializerOptions() { AllowTrailingCommas = true });
            manifest.Should().NotBeNull();
            manifest.TestCases.Should().NotBeNull();
            manifest.TestCases.Should().HaveCountGreaterThan(0);

            var actual = JsonSerializer.Serialize(manifest,
                new JsonSerializerOptions()
                {
                    WriteIndented = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,


                });

            List<string> errors = new List<string>();
            //JsonAssert.AreSame("manifest.json", expected, actual, errors);
            errors.Should().BeEmpty();
        }
    }


    class ValidationManifestDataSourceAttribute : Attribute, ITestDataSource
    {
        private string _manifestFileName;
        private string _singleTest;
        private IEnumerable<string> _ignoreTests;

        public ValidationManifestDataSourceAttribute(string manifestFileName, string singleTest = null, string[] ignoreTests = null)
        {
            _manifestFileName = manifestFileName;
            _singleTest = singleTest;
            _ignoreTests = ignoreTests;
        }

        public string GetDisplayName(MethodInfo methodInfo, object[] data)
        {
            if (data.FirstOrDefault() is TestCase testCase)
            {
                return testCase.Name;
            }

            return default;
        }

        public IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            var manifestJson = File.ReadAllText(_manifestFileName);
            var manifest = JsonSerializer.Deserialize<Manifest>(manifestJson, new JsonSerializerOptions() { AllowTrailingCommas = true });

            IEnumerable<TestCase> testCases = manifest.TestCases;

            testCases = testCases.Where(t => t.Version != null && ModelInfo.CheckMinorVersionCompatibility(t.Version));

            if (!string.IsNullOrEmpty(_singleTest))
                testCases = testCases.Where(t => t.Name == _singleTest);
            if (_ignoreTests != null)
                testCases = testCases.Where(t => !_ignoreTests.Contains(t.Name));

            return testCases.Select(e => new object[] { e });
        }
    }

    public class ExpectedResult
    {
        [JsonPropertyName("errorCount")]
        public int? ErrorCount { get; set; }

        [JsonPropertyName("output")]
        public List<string> Output { get; set; }

        [JsonPropertyName("warningCount")]
        public int? WarningCount { get; set; }

        [JsonPropertyName("todo")]
        public string Todo { get; set; }

        [JsonPropertyName("infoCount")]
        public int? InfoCount { get; set; }
    }

    public class Profile
    {
        [JsonPropertyName("source")]
        public string Source { get; set; }

        [JsonPropertyName("java")]
        public ExpectedResult Java { get; set; }

        [JsonPropertyName("firely-sdk")]
        public ExpectedResult FirelySDK { get; set; }

        [JsonPropertyName("supporting")]
        public List<string> Supporting { get; set; }
    }

    public class TestCase
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("file")]
        public string FileName { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("java")]
        public ExpectedResult Java { get; set; }

        [JsonPropertyName("firely-sdk")]
        public ExpectedResult FirelySDK { get; set; }

        [JsonPropertyName("profiles")]
        public List<string> Profiles { get; set; }

        [JsonPropertyName("profile")]
        public Profile Profile { get; set; }

        [JsonPropertyName("supporting")]
        public List<string> Supporting { get; set; }

        [JsonPropertyName("allowed-extension-domain")]
        public string AllowedExtensionDomain { get; set; }
    }

    public class Manifest
    {
        [JsonPropertyName("documentation")]
        public List<string> Documentation { get; set; }

        [JsonPropertyName("test-cases")]
        public List<TestCase> TestCases { get; set; }
    }

}
