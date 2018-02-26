/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Hl7.Fhir.Support;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using System.IO.Compression;

namespace Hl7.Fhir.Tests.Serialization
{
    [TestClass]
    public class RoundtripTest
    { 
        [TestMethod]   
        public void RoundTripOneExample()
        {
            roundTripOneExample("testscript-example(example).xml");
            roundTripOneExample("TestPatient.xml");
        }

        private void roundTripOneExample(string filename)
        {
            string testFileName = filename;
            var original = TestDataHelper.ReadTestData(testFileName);

            var t = new FhirXmlParser().Parse<Resource>(original);

            var outputXml = new FhirXmlSerializer().SerializeToString(t);
            XmlAssert.AreSame(testFileName, original, outputXml);

            var outputJson = new FhirJsonSerializer().SerializeToString(t);
            var t2 = new FhirJsonParser().Parse<Resource>(outputJson);
            Assert.IsTrue(t.IsExactly(t2));

            var outputXml2 = new FhirXmlSerializer().SerializeToString(t2);
            XmlAssert.AreSame(testFileName, original, outputXml2);            
        }


        [TestMethod]
        [TestCategory("LongRunner")]
        public void FullRoundtripOfAllExamplesXml()
        {
            ZipArchive examples = TestDataHelper.ReadTestZip("examples.zip");
            
            // Create an empty temporary directory for us to dump the roundtripped intermediary files in
            string baseTestPath = Path.Combine(Path.GetTempPath(), "FHIRRoundTripTestXml");
            createEmptyDir(baseTestPath);

            Debug.WriteLine("Roundtripping xml->json->xml");
            createEmptyDir(baseTestPath);
            doRoundTrip(examples, baseTestPath);
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        public void FullRoundtripOfAllExamplesJson()
        {
            ZipArchive examples = TestDataHelper.ReadTestZip("examples-json.zip");

            // Create an empty temporary directory for us to dump the roundtripped intermediary files in
            string baseTestPath = Path.Combine(Path.GetTempPath(), "FHIRRoundTripTestJson");
            createEmptyDir(baseTestPath);

            Debug.WriteLine("Roundtripping json->xml->json");
            createEmptyDir(baseTestPath);
            doRoundTrip(examples, baseTestPath);
        }


        //[TestMethod]
        //public void CompareIntermediate2Xml()
        //{
        //    // You can use this method to compare just the input against intermediate2, much faster than
        //    // unpacking and converting first. This only works AFTER a previous test has already converted
        //    // xml -> json -> xml
        //    compareFiles(@"C:\Users\ewout\AppData\Local\Temp\FHIRRoundTripTestXml\input", @"C:\Users\ewout\AppData\Local\Temp\FHIRRoundTripTestXml\intermediate2");
        //}

        //[TestMethod]
        //public void CompareIntermediate2Json()
        //{
        //    // You can use this method to compare just the input against intermediate2, much faster than
        //    // unpacking and converting first. This only works AFTER a previous test has already converted
        //    // json -> xml -> json
        //    compareFiles(@"C:\Users\ewout\AppData\Local\Temp\FHIRRoundTripTestJson\input", @"C:\Users\ewout\AppData\Local\Temp\FHIRRoundTripTestJson\intermediate2");
        //}


        private static void createEmptyDir(string baseTestPath)
        {
            if (Directory.Exists(baseTestPath)) Directory.Delete(baseTestPath, true);
            Directory.CreateDirectory(baseTestPath);
        }

        private void doRoundTrip(ZipArchive examplesZip, string baseTestPath)
        {
            var examplePath = Path.Combine(baseTestPath, "input");
            Directory.CreateDirectory(examplePath);
            // Unzip files into this path
            Debug.WriteLine("Unzipping example files from {0} to {1}", examplesZip, examplePath);

            examplesZip.ExtractToDirectory(examplePath);

            var intermediate1Path = Path.Combine(baseTestPath, "intermediate1");
            Debug.WriteLine("Converting files in {0} to {1}", baseTestPath, intermediate1Path);
            var sw = new Stopwatch();
            sw.Start();
            convertFiles(examplePath, intermediate1Path);
            sw.Stop();
            Debug.WriteLine("Conversion took {0} seconds", sw.ElapsedMilliseconds / 1000);
            sw.Reset();

            var intermediate2Path = Path.Combine(baseTestPath, "intermediate2");
            Debug.WriteLine("Re-converting files in {0} back to original format in {1}", intermediate1Path, intermediate2Path);
            sw.Start();
            convertFiles(intermediate1Path, intermediate2Path);
            sw.Stop();
            Debug.WriteLine("Conversion took {0} seconds", sw.ElapsedMilliseconds / 1000);
            sw.Reset();

            Debug.WriteLine("Comparing files in {0} to files in {1}", baseTestPath, intermediate2Path);

            List<string> errors = new List<string>();
            compareFiles(examplePath, intermediate2Path, errors);
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine(String.Join("\r\n", errors));
            Assert.AreEqual(0, errors.Count, "Errors were encountered comparing converted content");
        }


        private void convertFiles(string inputPath, string outputPath)
        {
            var files = Directory.EnumerateFiles(inputPath);
            if (!Directory.Exists(outputPath)) Directory.CreateDirectory(outputPath);

            foreach (string file in files)
            {
                if (file.Contains(".profile"))
                    continue;
                if (file.Contains(".schema"))
                    continue;
                string exampleName = Path.GetFileNameWithoutExtension(file);
                string ext = Path.GetExtension(file);
                var toExt = ext == ".xml" ? ".json" : ".xml";
                string outputFile = Path.Combine(outputPath, exampleName) + toExt;

                Debug.WriteLine("Converting {0} [{1}->{2}] ", exampleName, ext, toExt);

                if (file.Contains("expansions.") || file.Contains("profiles-resources") || file.Contains("profiles-others") || file.Contains("valuesets."))
                    continue;
                if (!isFeed(file))
                    convertResource(file, outputFile);
                else
                    convertFeed(file, outputFile);
            }

            Debug.WriteLine("Done!");
        }


        private void compareFiles(string expectedPath, string actualPath, List<string> errors)
        {
            var files = Directory.EnumerateFiles(expectedPath);

            foreach (string file in files)
            {
                if (file.Contains(".profile"))
                    continue;
                if (file.Contains(".schema"))
                    continue;
                string exampleName = Path.GetFileNameWithoutExtension(file);
                string extension = Path.GetExtension(file);
                string actualFile = Path.Combine(actualPath, exampleName) + extension;

                if (actualFile.Contains("dataelements.") || actualFile.Contains("expansions.") || actualFile.Contains("profiles-resources") || actualFile.Contains("profiles-others") || actualFile.Contains("valuesets."))
                    continue;

                if (!File.Exists(actualFile))
                    Assert.Fail("File {0}.{1} was not converted and not found in {2}", exampleName, extension,
                                        actualPath);

                Debug.WriteLine("Comparing " + exampleName);

                compareFile(file, actualFile, errors);
            }
        }

        private void compareFile(string expectedFile, string actualFile, List<string> errors)
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

        private bool isFeed(string filename)
        {
            var buffer = new char[250];


            using (var reader = new StreamReader(new FileStream(filename, FileMode.Open)))
            {
                reader.Read(buffer, 0, buffer.Length);
                var data = new String(buffer);

                if (data.Contains("<feed")) return true;
                if (data.Contains("resourceType") && data.Contains("Bundle") && !data.Contains("NewBundle")) return true;

                return false;
            }
        }


        private void convertResource(string inputFile, string outputFile)
        {
            //TODO: call validation after reading
            if (inputFile.Contains("expansions.") || inputFile.Contains("profiles-resources") || inputFile.Contains("profiles-others") || inputFile.Contains("valuesets."))
                return;
            if (inputFile.EndsWith(".xml"))
            {
                var xml = File.ReadAllText(inputFile);
                var resource = new FhirXmlParser().Parse<Resource>(xml);

                var r2 = resource.DeepCopy();
                Assert.IsTrue(resource.Matches(r2 as Resource), "Serialization of " + inputFile + " did not match output - Matches test");
                Assert.IsTrue(resource.IsExactly(r2 as Resource), "Serialization of " + inputFile + " did not match output - IsExactly test");
                Assert.IsFalse(resource.Matches(null), "Serialization of " + inputFile + " matched null - Matches test");
                Assert.IsFalse(resource.IsExactly(null), "Serialization of " + inputFile + " matched null - IsExactly test");

                var json = new FhirJsonSerializer().SerializeToString(resource);
                File.WriteAllText(outputFile, json);
            }
            else
            {
                var json = File.ReadAllText(inputFile);
                var resource = new FhirJsonParser().Parse<Resource>(json);
                var xml = new FhirXmlSerializer().SerializeToString(resource);
                File.WriteAllText(outputFile, xml);
            }
        }

        private void convertFeed(string inputFile, string outputFile)
        {
            //TODO: call validation after reading

            if (inputFile.EndsWith(".xml"))
            {
                var xml = File.ReadAllText(inputFile);
                var resource = new FhirXmlParser().Parse<Resource>(xml);

                var json = new FhirJsonSerializer().SerializeToString(resource);
                File.WriteAllText(outputFile, json);
            }
            else
            {
                var json = File.ReadAllText(inputFile);
                var resource = new FhirJsonParser().Parse<Resource>(json);
                var xml = new FhirXmlSerializer().SerializeToString(resource);
                File.WriteAllText(outputFile, xml);
            }
        }
    }
}
