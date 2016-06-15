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
#if PORTABLE45
	public class PortableRoundtripTest
#else
	public class RoundtripTest
#endif
    { 
        [TestMethod]   
        public void RoundTripOneExample()
        {
            string exampleXml = @"TestData\testscript-example(example).xml";
            var original = File.ReadAllText(exampleXml);

            var t = new FhirXmlParser().Parse<TestScript>(original);
            var outputXml = FhirSerializer.SerializeResourceToXml(t);
            XmlAssert.AreSame(original, outputXml);

            var outputJson = FhirSerializer.SerializeResourceToJson(t);
            var t2 = new FhirJsonParser().Parse<TestScript>(outputJson);
//            Assert.IsTrue(t.IsExactly(t2));

            var outputXml2 = FhirSerializer.SerializeResourceToXml(t2);
            XmlAssert.AreSame(original, outputXml2);
        }
        [TestMethod]
        [TestCategory("LongRunner")]
        public void FullRoundtripOfAllExamplesXml()
        {
            string examplesXml = @"TestData\examples.zip";

            // Create an empty temporary directory for us to dump the roundtripped intermediary files in
            string baseTestPath = Path.Combine(Path.GetTempPath(), "FHIRRoundTripTestXml");
            createEmptyDir(baseTestPath);

            Debug.WriteLine("Roundtripping xml->json->xml");
            createEmptyDir(baseTestPath);
            doRoundTrip(examplesXml, baseTestPath);
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        public void FullRoundtripOfAllExamplesJson()
        {
            string examplesJson = @"TestData\examples-json.zip";

            // Create an empty temporary directory for us to dump the roundtripped intermediary files in
            string baseTestPath = Path.Combine(Path.GetTempPath(), "FHIRRoundTripTestJson");
            createEmptyDir(baseTestPath);

            Debug.WriteLine("Roundtripping json->xml->json");
            createEmptyDir(baseTestPath);
            doRoundTrip(examplesJson, baseTestPath);
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

        private void doRoundTrip(string examplesZip, string baseTestPath)
        {
            var examplePath = Path.Combine(baseTestPath, "input");
            Directory.CreateDirectory(examplePath);
            // Unzip files into this path
            Debug.WriteLine("Unzipping example files from {0} to {1}", examplesZip, examplePath);

            ZipFile.ExtractToDirectory(examplesZip, examplePath);
      
            var intermediate1Path = Path.Combine(baseTestPath, "intermediate1");
            Debug.WriteLine("Converting files in {0} to {1}", baseTestPath, intermediate1Path);
            convertFiles(examplePath, intermediate1Path);
            var intermediate2Path = Path.Combine(baseTestPath, "intermediate2");
            Debug.WriteLine("Re-converting files in {0} back to original format in {1}", intermediate1Path, intermediate2Path);
            convertFiles(intermediate1Path, intermediate2Path);
            Debug.WriteLine("Comparing files in {0} to files in {1}", baseTestPath, intermediate2Path);
            compareFiles(examplePath, intermediate2Path);
        }


        private void convertFiles(string inputPath, string outputPath)
        {
            var files = Directory.EnumerateFiles(inputPath);
            if (!Directory.Exists(outputPath)) Directory.CreateDirectory(outputPath);

            foreach (string file in files)
            {
                string exampleName = Path.GetFileNameWithoutExtension(file);
                string ext = Path.GetExtension(file);
                var toExt = ext == ".xml" ? ".json" : ".xml";
                string outputFile = Path.Combine(outputPath, exampleName) + toExt;

                Debug.WriteLine("Converting {0} [{1}->{2}] ",exampleName,ext,toExt);

                if (!isFeed(file))
                    convertResource(file, outputFile);
                else
                    convertFeed(file, outputFile);
            }

            Debug.WriteLine("Done!");
        }


        private void compareFiles(string expectedPath, string actualPath)
        {
            var files = Directory.EnumerateFiles(expectedPath);

            foreach (string file in files)
            {
                string exampleName = Path.GetFileNameWithoutExtension(file);
                string extension = Path.GetExtension(file);
                string actualFile = Path.Combine(actualPath, exampleName) + extension;

                if (!File.Exists(actualFile))
                    Assert.Fail("File {0}.{1} was not converted and not found in {2}", exampleName, extension,
                                        actualPath);

                Debug.WriteLine("Comparing " + exampleName);

                compareFile(file, actualFile);
            }
        }

        private void compareFile(string expectedFile, string actualFile)
        {
            if(expectedFile.EndsWith(".xml"))
                XmlAssert.AreSame(File.ReadAllText(expectedFile),File.ReadAllText(actualFile));
            else
                JsonAssert.AreSame(File.ReadAllText(expectedFile), File.ReadAllText(actualFile));
        }

        private bool isFeed(string filename)
        {
            var buffer = new char[250];

            using (var reader = new StreamReader(filename))
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

            if (inputFile.EndsWith(".xml"))
            {
                var xml = File.ReadAllText(inputFile);
                var resource = new FhirXmlParser().Parse<Resource>(xml);

                var r2 = resource.DeepCopy();
                Assert.IsTrue(resource.Matches(r2 as Resource), "Serialization of " + inputFile + " did not match output - Matches test");
                Assert.IsTrue(resource.IsExactly(r2 as Resource), "Serialization of " + inputFile + " did not match output - IsExactly test");
                Assert.IsFalse(resource.Matches(null), "Serialization of " + inputFile + " matched null - Matches test");
                Assert.IsFalse(resource.IsExactly(null), "Serialization of " + inputFile + " matched null - IsExactly test");

                var json = FhirSerializer.SerializeResourceToJson(resource);
                File.WriteAllText(outputFile, json);
            }
            else
            {
                var json = File.ReadAllText(inputFile);
                var resource = new FhirJsonParser().Parse<Resource>(json);
                var xml = FhirSerializer.SerializeResourceToXml(resource);
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

                var json = FhirSerializer.SerializeResourceToJson(resource);
                File.WriteAllText(outputFile, json);
            }
            else
            {
                var json = File.ReadAllText(inputFile);
                var resource = new FhirJsonParser().Parse<Resource>(json);
                var xml = FhirSerializer.SerializeResourceToXml(resource);
                File.WriteAllText(outputFile, xml);
            }
        }
    }
}
