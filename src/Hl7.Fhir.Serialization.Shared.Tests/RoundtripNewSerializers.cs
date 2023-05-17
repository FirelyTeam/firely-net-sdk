#nullable enable
using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Tests;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Xml;
using ERR = Hl7.Fhir.Serialization.FhirJsonException;


namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public partial class RoundTripNewSerializers
    {
        private static readonly string jsonTestFolder = "FHIRRoundTripTestJson";
        private static readonly string xmlTestFolder = "FHIRRoundTripTestXml";
        private static readonly string inputFolder = "input";
        private static readonly string intermediate1Folder = "intermediate1";
        private static readonly string intermediate2Folder = "intermediate2";

#if R5
        private readonly string _attachmentJson = "{\"size\":\"12\"}";

        private static IEnumerable<object[]> attachmentSource()
        {
            yield return new object[] { "{\"size\":\"12\", \"title\": \"Correct Attachment\"}", 12L, null! };
            yield return new object[] { "{\"size\":12, \"title\": \"An incorrect Attachment\"}", null!, ERR.LONG_INCORRECT_FORMAT_CODE };
            yield return new object[] { "{\"size\":25.345, \"title\": \"An incorrect Attachment\"}", null!, ERR.NUMBER_CANNOT_BE_PARSED_CODE };
            yield return new object[] { "{\"size\":\"12.345\", \"title\": \"An incorrect Attachment\"}", null!, ERR.LONG_CANNOT_BE_PARSED_CODE };
        }
#else
        private readonly string _attachmentJson = "{\"size\":12}";

        private static IEnumerable<object[]> attachmentSource()
        {
            yield return new object[] { "{\"size\":12, \"title\": \"Correct Attachment\"}", 12L, null! };
            yield return new object[] { "{\"size\":12.345, \"title\": \"An incorrect Attachment\"}", null!, ERR.NUMBER_CANNOT_BE_PARSED_CODE };
            yield return new object[] { "{\"size\":\"12\", \"title\": \"An incorrect Attachment\"}", null!, ERR.LONG_INCORRECT_FORMAT_CODE };
            yield return new object[] { "{\"size\":\"12.345\", \"title\": \"An incorrect Attachment\"}", null!, ERR.LONG_INCORRECT_FORMAT_CODE };
        }
#endif


        [DynamicData(nameof(prepareExampleZipFilesXml), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayNames))]
        [DataTestMethod]
        [TestCategory("LongRunner")]
        public void FullRoundtripOfAllExamplesXmlNewSerializer(string file, string baseTestPath, FhirXmlPocoSerializer xmlSerializer, BaseFhirXmlPocoDeserializer xmlDeserializer, JsonSerializerOptions jsonOptions)
        {
            doRoundTrip(baseTestPath, file, xmlSerializer, xmlDeserializer, jsonOptions);
        }

        [DynamicData(nameof(prepareExampleZipFilesJson), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayNames))]
        [DataTestMethod]
        [TestCategory("LongRunner")]
        public void FullRoundtripOfAllExamplesJsonNewSerializer(string file, string baseTestPath, FhirXmlPocoSerializer xmlSerializer, BaseFhirXmlPocoDeserializer xmlDeserializer, JsonSerializerOptions jsonOptions)
        {
            doRoundTrip(baseTestPath, file, xmlSerializer, xmlDeserializer, jsonOptions);
        }
        private static IEnumerable<object[]> prepareExampleZipFilesXml()
        {
            return prepareExampleZipFiles("examples.zip", Path.Combine(Path.GetTempPath(), xmlTestFolder));
        }

        private static IEnumerable<object[]> prepareExampleZipFilesJson()
        {

            return prepareExampleZipFiles("examples-json.zip", Path.Combine(Path.GetTempPath(), jsonTestFolder));
        }

        private static IEnumerable<object[]> prepareExampleZipFiles(string zipname, string targetDir)
        {
            ZipArchive examples = extractTestZip(zipname);
            createEmptyDir(targetDir);

            var inputPath = Path.Combine(targetDir, inputFolder);
            createEmptyDir(inputPath);

            examples.ExtractToDirectory(inputPath);
            var files = getFiles(Path.Combine(inputPath), new[] { "*.xml", "*.json" }, SearchOption.AllDirectories).ToList();

            var intermediate1Path = Path.Combine(targetDir, intermediate1Folder);
            createEmptyDir(intermediate1Path);

            var intermediate2Path = Path.Combine(targetDir, intermediate2Folder);
            createEmptyDir(intermediate2Path);

            var xmlSerializer = new FhirXmlPocoSerializer();
            var xmlDeserializer = new FhirXmlPocoDeserializer();
            var jsonOptions = new JsonSerializerOptions().ForFhir(ModelInfo.ModelInspector).Pretty();

            return files.Where(f => !skipFile(f))
                 .Select(f => new object[] { f, targetDir, xmlSerializer, xmlDeserializer, jsonOptions })
                 .ToList();
        }
        private static bool skipFile(string file)
        {
            if (file.Contains("notification-") || file.Contains("subscriptionstatus-"))
                return true; // These are Subscription resources that have invalid data in R5.
            if (file.Contains("examplescenario-example"))
                return true; // this resource has a property name resourceType (which is reserved in the .net json serializer)
            if (file.Contains("json-edge-cases"))
                return true; // known issues with binary contained resource having content, not data
            if (file.Contains("observation-decimal"))
                return true; // exponential number example is tooo big (and too small)
            if (file.Contains("package-min-ver"))
                return true; // not a resource
            if (file.Contains("profiles-other"))
                return true;
            if (file.Contains("profiles-resources"))
                return true;
            if (file.Contains("profiles-types"))
                return true;
            if (file.Contains("dataelements"))
                return true;
            if (file.Contains("valuesets"))
                return true;
            if (file.Contains("xver-paths-4.6") || file.Contains("hl7.fhir.r5.corexml.manifest") || file.Contains("hl7.fhir.r5.expansions.manifest") || file.Contains("hl7.fhir.r5.core.manifest") || file.Contains("uml"))
                return true; // non-fhir-files in the R5 examples.zip
#if R5
            // These examples contain resourceType which cannot be handled by our serializers
            if (file.Contains("subscription-example")) return true;
            if (file.Contains("consent-example-smartonfhir")) return true;
#endif 
            return false;
        }


        public static string GetTestDisplayNames(MethodInfo methodInfo, object[] values)
        {
            return Path.GetFileName((string)values[0]);
        }

        private static void doRoundTrip(string baseTestPath, string file, FhirXmlPocoSerializer xmlSerializer, BaseFhirXmlPocoDeserializer xmlDeserializer, JsonSerializerOptions jsonOptions)
        {
            var errors = new List<string>();

            var intermediate1Path = Path.Combine(baseTestPath, intermediate1Folder);

            //First conversion
            convertFile(file, intermediate1Path, xmlSerializer, xmlDeserializer, jsonOptions, errors);
            var intermediate2Path = Path.Combine(baseTestPath, intermediate2Folder);

            string exampleWithOppositeExtension = changeFileExtension(file);

            string intermediate1File = Path.Combine(intermediate1Path, exampleWithOppositeExtension);
            //Convert back to original
            convertFile(intermediate1File, intermediate2Path, xmlSerializer, xmlDeserializer, jsonOptions, errors);

            //compare original with roundtrip output
            compareFile(file, Path.Combine(intermediate2Path, Path.GetFileName(file)), errors);
            Assert.AreEqual(0, errors.Count, "Errors were encountered comparing converted content");
        }

        private static string changeFileExtension(string file)
        {
            string exampleName = Path.GetFileNameWithoutExtension(file);
            string ext = Path.GetExtension(file);
            var toExt = ext == ".xml" ? ".json" : ".xml";
            return exampleName + toExt;
        }

        private static ZipArchive extractTestZip(string filename)
        {
            string file = getFullPathForExample(filename);
            return ZipFile.OpenRead(file);
        }

        private static string getFullPathForExample(string filename) => Path.Combine("TestData", filename);

        private static void createEmptyDir(string baseTestPath)
        {
            if (Directory.Exists(baseTestPath)) Directory.Delete(baseTestPath, true);
            Directory.CreateDirectory(baseTestPath);
        }

        private static void convertFile(string file, string outputPath, FhirXmlPocoSerializer xmlSerializer, BaseFhirXmlPocoDeserializer xmlDeserializer, JsonSerializerOptions jsonOptions, List<string> errors)
        {
            string exampleWithOppositeExtension = changeFileExtension(file);
            string outputFile = Path.Combine(outputPath, exampleWithOppositeExtension);
            try
            {
                convertResource(file, outputFile, xmlSerializer, xmlDeserializer, jsonOptions);
            }
            catch (Exception ex)
            {
                errors.Add(ex.Message);
            }
        }

        private static void convertResource(string inputFile, string outputFile, FhirXmlPocoSerializer xmlSerializer, BaseFhirXmlPocoDeserializer xmlDeserializer, JsonSerializerOptions options)
        {
            if (inputFile.EndsWith(".xml"))
            {
                var xml = File.ReadAllText(inputFile);
                var xmlreader = XmlReader.Create(new StringReader(xml));
                xmlreader.Read();

                Resource resource;
                try
                {
                    resource = xmlDeserializer.DeserializeResource(xmlreader);
                }
                catch (DeserializationFailedException e)
                {
                    resource = (Resource)e.PartialResult!;
                }

                var r2 = resource.DeepCopy();
                Assert.IsTrue(resource.Matches(r2 as Resource), "Serialization of " + inputFile + " did not match output - Matches test");
                Assert.IsTrue(resource.IsExactly(r2 as Resource), "Serialization of " + inputFile + " did not match output - IsExactly test");
                Assert.IsFalse(resource.Matches(null), "Serialization of " + inputFile + " matched null - Matches test");
                Assert.IsFalse(resource.IsExactly(null), "Serialization of " + inputFile + " matched null - IsExactly test");

                var json = JsonSerializer.Serialize(resource, options);
                File.WriteAllText(outputFile, json);
            }
            else
            {
                var json = File.ReadAllText(inputFile);
                Resource? resource;
                try
                {
                    resource = JsonSerializer.Deserialize<Resource>(json, options);
                }
                catch (DeserializationFailedException e)
                {
                    resource = (Resource)e.PartialResult!;
                }

                var sb = new StringBuilder();
                using (var w = XmlWriter.Create(sb))
                {

                    xmlSerializer.Serialize(resource!, w);
                }

                File.WriteAllText(outputFile, sb.ToString());
            }
        }

        private static IEnumerable<string> getFiles(string path,
                     string[] searchPatterns,
                     SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return searchPatterns.AsParallel()
                   .SelectMany(searchPattern =>
                          Directory.EnumerateFiles(path, searchPattern, searchOption));
        }

        private static void compareFile(string expectedFile, string actualFile, List<string> errors)
        {
            if (expectedFile.EndsWith(".xml"))
                XmlAssert.AreSame(new FileInfo(expectedFile).Name, File.ReadAllText(expectedFile),
                File.ReadAllText(actualFile));

            else
            {
                JsonAssert.AreSame(new FileInfo(expectedFile).Name, File.ReadAllText(expectedFile),
                                    File.ReadAllText(actualFile), errors);
            }
        }

        [TestMethod]
        public void RoundTripAttachmentWithSize()
        {
            var options = new JsonSerializerOptions().ForFhir(ModelInfo.ModelInspector);
            var attachment = JsonSerializer.Deserialize<Attachment>(_attachmentJson, options);
            attachment.Should().BeOfType<Attachment>().Subject.Size.Should().Be(12L);
            var json = JsonSerializer.Serialize(attachment, options);
            json.Should().Be(_attachmentJson);
        }

        [DataTestMethod]
        [DynamicData(nameof(attachmentSource), DynamicDataSourceType.Method)]
        public void ParseAttachment(string input, long? expectedAttachmentSize, string? errorCode)
        {
            var options = new JsonSerializerOptions().ForFhir(ModelInfo.ModelInspector);
            if (errorCode is not null)
            {
                Action action = () => JsonSerializer.Deserialize<Attachment>(input, options);

                action.Should().Throw<DeserializationFailedException>().Which.Exceptions.Should().OnlyContain(e => e.ErrorCode == errorCode);
            }
            else
            {
                var attachment = JsonSerializer.Deserialize<Attachment>(input, options);
                attachment.Should().NotBeNull();
                attachment!.Size.Should().Be(expectedAttachmentSize!.Value);
            }
        }
    }
}
#nullable restore