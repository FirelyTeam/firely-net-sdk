using Hl7.Fhir.Model;
using Hl7.Fhir.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Xml;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class RoundTripNewSerializers
    {

        [TestMethod]
        [TestCategory("LongRunner")]
        public void FullRoundtripOfAllExamplesXmlNewSerializer()
        {
            fullRoundtripOfAllExamplesNewSerializer("examples.zip", "FHIRRoundTripTestXml",
                "Roundtripping xml->json->xml");
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        public void FullRoundtripOfAllExamplesJsonNewSerializer()
        {
            fullRoundtripOfAllExamplesNewSerializer("examples-json.zip", "FHIRRoundTripTestJson",
                "Roundtripping json->xml->json");
        }

        private static void fullRoundtripOfAllExamplesNewSerializer(string zipname, string dirname, string label)
        {
            ZipArchive examples = readTestZip(zipname);

            // Create an empty temporary directory for us to dump the roundtripped intermediary files in
            string baseTestPath = Path.Combine(Path.GetTempPath(), dirname);
            createEmptyDir(baseTestPath);

            Debug.WriteLine(label);
            createEmptyDir(baseTestPath);
            doRoundTrip(examples, baseTestPath);
        }
        private static ZipArchive readTestZip(string filename)
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

        private static void doRoundTrip(ZipArchive examplesZip, string baseTestPath)
        {
            var examplePath = Path.Combine(baseTestPath, "input");
            Directory.CreateDirectory(examplePath);
            // Unzip files into this path
            Debug.WriteLine("Unzipping example files from {0} to {1}", examplesZip, examplePath);

            examplesZip.ExtractToDirectory(examplePath);

            List<string> errors = new List<string>();
            var intermediate1Path = Path.Combine(baseTestPath, "intermediate1");
            Debug.WriteLine("Converting files in {0} to {1}", baseTestPath, intermediate1Path);
            var sw = new Stopwatch();
            sw.Start();
            int convertedFileCount = convertFiles(examplePath, intermediate1Path, errors);
            sw.Stop();
            Debug.WriteLine("Conversion of {1} files took {0} seconds", sw.ElapsedMilliseconds / 1000, convertedFileCount);
            sw.Reset();

            var intermediate2Path = Path.Combine(baseTestPath, "intermediate2");
            Debug.WriteLine("Re-converting files in {0} back to original format in {1}", intermediate1Path, intermediate2Path);
            sw.Start();
            convertedFileCount = convertFiles(intermediate1Path, intermediate2Path, errors);
            sw.Stop();
            Debug.WriteLine("Conversion of {1} files took {0} seconds", sw.ElapsedMilliseconds / 1000, convertedFileCount);
            sw.Reset();

            Debug.WriteLine("Comparing files in {0} to files in {1}", baseTestPath, intermediate2Path);

            compareFiles(examplePath, intermediate2Path, errors);
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine(String.Join("\r\n", errors));
            Assert.AreEqual(0, errors.Count, "Errors were encountered comparing converted content");
        }
        private static int convertFiles(string inputPath, string outputPath, List<string> errors)
        {
            int fileCount = 0;
            var files = getFiles(inputPath, new[] { "*.xml", "*.json" }, SearchOption.AllDirectories);
            if (!Directory.Exists(outputPath)) Directory.CreateDirectory(outputPath);

            foreach (string file in files)
            {
                if (SkipFile(file))
                    continue;
                string exampleName = Path.GetFileNameWithoutExtension(file);
                string ext = Path.GetExtension(file);
                var toExt = ext == ".xml" ? ".json" : ".xml";
                string outputFile = Path.Combine(outputPath, exampleName) + toExt;

                Debug.WriteLine("Converting {0} [{1}->{2}] ", exampleName, ext, toExt);

                if (file.Contains("expansions.") || file.Contains("profiles-resources") || file.Contains("profiles-others") || file.Contains("valuesets."))
                    continue;

                fileCount++;
                try
                {
                    convertResource(file, outputFile);
                }
                catch (Exception ex)
                {
                    errors.Add($"{exampleName}{ext}: " + ex.Message);
                }
            }

            Debug.WriteLine("Done!");
            return fileCount;
        }

        private static void convertResource(string inputFile, string outputFile)
        {
            if (inputFile.EndsWith(".xml"))
            {
                var xml = File.ReadAllText(inputFile);
                var xmlreader = XmlReader.Create(new StringReader(xml));
                xmlreader.Read();
                var deserializer = new FhirXmlPocoDeserializer(typeof(Patient).Assembly);
                Resource resource;
                try
                {
                    resource = deserializer.DeserializeResource(xmlreader);
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

                var options = new JsonSerializerOptions().ForFhir(typeof(Patient).Assembly).Pretty();
                var json = JsonSerializer.Serialize(resource, options);
                File.WriteAllText(outputFile, json);
            }
            else
            {
                var json = File.ReadAllText(inputFile);

                var options = new JsonSerializerOptions().ForFhir(typeof(Patient).Assembly).Pretty();

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
                var w = XmlWriter.Create(sb);
                var serializer = new FhirXmlPocoSerializer(Specification.FhirRelease.R4B);
                serializer.Serialize(resource, w);
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

        static bool SkipFile(string file)
        {
            if (file.Contains(".profile"))
                return true;
            if (file.Contains(".schema"))
                return true;
            if (file.Contains(".diff"))
                return true;
            if (file.Contains("examplescenario-example"))
                return true; // this resource has a property name resourceType (which is reserved in the .net json serializer)
            if (file.Contains("backbone-elements"))
                return true; // its not really a resource!
            if (file.Contains("json-edge-cases"))
                return true; // known issues with binary contained resource having content, not data
            if (file.Contains("observation-decimal"))
                return true; // exponential number example is tooo big (and too small)
            if (file.Contains("package-min-ver"))
                return true; // not a resource
            if (file.Contains("choice-elements"))
                return true; // not a resource

            if (file.Contains("v2-tables"))
                return true; // this file is known to have a single dud valueset - have reported on Zulip
                             // https://chat.fhir.org/#narrow/stream/48-terminology/subject/v2.20Table.200550
            return false;
        }

        private static void compareFiles(string expectedPath, string actualPath, List<string> errors)
        {
            var files = Directory.EnumerateFiles(expectedPath);

            foreach (string file in files)
            {
                if (SkipFile(file))
                    continue;
                string exampleName = Path.GetFileNameWithoutExtension(file);
                string extension = Path.GetExtension(file);
                string actualFile = Path.Combine(actualPath, exampleName) + extension;

                if (actualFile.Contains("dataelements.") || actualFile.Contains("expansions.") || actualFile.Contains("profiles-resources") || actualFile.Contains("profiles-others") || actualFile.Contains("valuesets."))
                    continue;

                if (!File.Exists(actualFile))
                {

                    errors.Add($"File {exampleName}.{extension} was not converted and not found in {actualPath}");
                    return;
                }

                // Debug.WriteLine("Comparing " + exampleName);

                compareFile(file, actualFile, errors);
            }
        }

        private static void compareFile(string expectedFile, string actualFile, List<string> errors)
        {
            if (expectedFile.EndsWith(".xml"))
                XmlAssert.AreSame(new FileInfo(expectedFile).Name, File.ReadAllText(expectedFile),
                    File.ReadAllText(actualFile));
            else
            {
                if (new FileInfo(expectedFile).Name != "json-edge-cases.json")
                {
                    JsonAssert.AreSame(new FileInfo(expectedFile).Name, File.ReadAllText(expectedFile),
                                        File.ReadAllText(actualFile), errors);
                }
            }
        }
    }
}
