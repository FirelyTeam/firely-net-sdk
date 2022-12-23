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

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class RoundTripNewSerializers
    {
        private static readonly string jsonTestFolder = "FHIRRoundTripTestJson";
        private static readonly string xmlTestFolder = "FHIRRoundTripTestXml";
        private static readonly string inputFolder = "input";
        private static readonly string intermediate1Folder = "intermediate1";
        private static readonly string intermediate2Folder = "intermediate2";

        [DynamicData(nameof(prepareExampleZipFilesXml), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayNames))]
        [DataTestMethod]
        [TestCategory("LongRunner")]
        public void FullRoundtripOfAllExamplesXmlNewSerializer(string file, string baseTestPath, FhirXmlPocoSerializer xmlSerializer, FhirXmlPocoDeserializer xmlDeserializer, JsonSerializerOptions jsonOptions)
        {
            doRoundTrip(baseTestPath, file, xmlSerializer, xmlDeserializer, jsonOptions);
        }

        [DynamicData(nameof(prepareExampleZipFilesJson), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetTestDisplayNames))]
        [DataTestMethod]
        [TestCategory("LongRunner")]
        public void FullRoundtripOfAllExamplesJsonNewSerializer(string file, string baseTestPath, FhirXmlPocoSerializer xmlSerializer, FhirXmlPocoDeserializer xmlDeserializer, JsonSerializerOptions jsonOptions)
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
            var objects = new List<object[]>();

            var intermediate1Path = Path.Combine(targetDir, intermediate1Folder);
            createEmptyDir(intermediate1Path);

            var intermediate2Path = Path.Combine(targetDir, intermediate2Folder);
            createEmptyDir(intermediate2Path);

            var xmlSerializer = new FhirXmlPocoSerializer(ModelInfo.ModelInspector.FhirRelease);
            var xmlDeserializer = new FhirXmlPocoDeserializer(ModelInfo.ModelInspector);
            var jsonOptions = new JsonSerializerOptions().ForFhir(ModelInfo.ModelInspector).Pretty();

            files.ForEach(f => objects.Add(new object[] { f, targetDir, xmlSerializer, xmlDeserializer, jsonOptions }));

            return objects;
        }

        public static string GetTestDisplayNames(MethodInfo methodInfo, object[] values)
        {
            return Path.GetFileName((string)values[0]);
        }

        private static void doRoundTrip(string baseTestPath, string file, FhirXmlPocoSerializer xmlSerializer, FhirXmlPocoDeserializer xmlDeserializer, JsonSerializerOptions jsonOptions)
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

        private static void convertFile(string file, string outputPath, FhirXmlPocoSerializer xmlSerializer, FhirXmlPocoDeserializer xmlDeserializer, JsonSerializerOptions jsonOptions, List<string> errors)
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

        private static void convertResource(string inputFile, string outputFile, FhirXmlPocoSerializer xmlSerializer, FhirXmlPocoDeserializer xmlDeserializer, JsonSerializerOptions options)
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
                    resource = (Resource)e.PartialResult;
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
                Resource resource;
                try
                {
                    resource = JsonSerializer.Deserialize<Resource>(json, options);
                }
                catch (DeserializationFailedException e)
                {
                    resource = (Resource)e.PartialResult;
                }

                var sb = new StringBuilder();
                using (var w = XmlWriter.Create(sb))
                {

                    xmlSerializer.Serialize(resource, w);
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
    }
}
