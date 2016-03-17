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
        public void FullRoundtripOfAllExamples()
        {
            string examplesXml = @"TestData\examples.zip";
            string examplesJson = @"TestData\examples-json.zip";

            // Create an empty temporary directory for us to dump the roundtripped intermediary files in
            string baseTestPath = Path.Combine(Path.GetTempPath(), "FHIRRoundTripTest");
            createEmptyDir(baseTestPath);

            Debug.WriteLine("First, roundtripping xml->json->xml");
            var baseTestPathXml = Path.Combine(baseTestPath, "FromXml");
            createEmptyDir(baseTestPathXml);
            doRoundTrip(examplesXml, baseTestPathXml, false);

            Debug.WriteLine("Then, roundtripping json->xml->json");
            var baseTestPathJson = Path.Combine(baseTestPath, "FromJson");
            createEmptyDir(baseTestPathJson);
            doRoundTrip(examplesJson, baseTestPathJson, false);
        }

        [TestMethod]
        public void XmlTurtleRoundtripOfAllExamples()
        {
            string examplesXml = @"TestData\examples.zip";

            // Create an empty temporary directory for us to dump the roundtripped intermediary files in
            string baseTestPath = Path.Combine(Path.GetTempPath(), "FHIRRoundTripTest");
            createEmptyDir(baseTestPath);

            Debug.WriteLine("First, roundtripping xml->turtle->xml");
            var baseTestPathXml = Path.Combine(baseTestPath, "FromXml");
            createEmptyDir(baseTestPathXml);
            doRoundTrip(examplesXml, baseTestPathXml, true);
        }

        private static void createEmptyDir(string baseTestPath)
        {
            if (Directory.Exists(baseTestPath)) Directory.Delete(baseTestPath, true);
            Directory.CreateDirectory(baseTestPath);
        }

        private void doRoundTrip(string examplesZip, string baseTestPath, bool turtle)
        {
            var examplePath = Path.Combine(baseTestPath, "input");
            Directory.CreateDirectory(examplePath);
            // Unzip files into this path
            Debug.WriteLine("Unzipping example files from {0} to {1}", examplesZip, examplePath);

            ZipFile.ExtractToDirectory(examplesZip, examplePath);
      
            var intermediate1Path = Path.Combine(baseTestPath, "intermediate1");
            Debug.WriteLine("Converting files in {0} to {1}", baseTestPath, intermediate1Path);
            convertFiles(examplePath, intermediate1Path, turtle);
            var intermediate2Path = Path.Combine(baseTestPath, "intermediate2");
            Debug.WriteLine("Re-converting files in {0} back to original format in {1}", intermediate1Path, intermediate2Path);
            convertFiles(intermediate1Path, intermediate2Path, turtle);
            Debug.WriteLine("Comparing files in {0} to files in {1}", baseTestPath, intermediate2Path);
            compareFiles(examplePath, intermediate2Path);
        }


        private void convertFiles(string inputPath, string outputPath, bool turtle)
        {
            var files = Directory.EnumerateFiles(inputPath);
            if (!Directory.Exists(outputPath)) Directory.CreateDirectory(outputPath);

            foreach (string file in files)
            {
                string exampleName = Path.GetFileNameWithoutExtension(file);
                string ext = Path.GetExtension(file);
                var toExt = ext == ".xml" ? (turtle?".ttl":".json") : ".xml";
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
            if (expectedFile.EndsWith(".xml"))
                XmlAssert.AreSame(File.ReadAllText(expectedFile), File.ReadAllText(actualFile));
            else if (expectedFile.EndsWith(".json"))
                JsonAssert.AreSame(File.ReadAllText(expectedFile), File.ReadAllText(actualFile));
            else if (expectedFile.EndsWith(".ttl"))
                Debug.WriteLine("compare Turtle");
            else
                Debug.WriteLine("compare Something else?");
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

            if (inputFile.EndsWith(".xml") && outputFile.EndsWith(".json"))
            {
                var xml = File.ReadAllText(inputFile);
                var resource = FhirParser.ParseResourceFromXml(xml);
                //var r2 = resource.DeepCopy();
                var json = FhirSerializer.SerializeResourceToJson(resource);
                File.WriteAllText(outputFile, json);
            }
            else if (inputFile.EndsWith(".json") && outputFile.EndsWith(".xml"))
            {
                var json = File.ReadAllText(inputFile);
                var resource = FhirParser.ParseResourceFromJson(json);
                var xml = FhirSerializer.SerializeResourceToXml(resource);
                File.WriteAllText(outputFile, xml);
            }
            else if (inputFile.EndsWith(".xml") && outputFile.EndsWith(".ttl"))
            {
                var xml = File.ReadAllText(inputFile);
                var resource = FhirParser.ParseResourceFromXml(xml);
                var turtle = FhirSerializer.SerializeResourceToTurtle(resource);
                File.WriteAllText(outputFile, turtle);
            }
            else if (inputFile.EndsWith(".ttl") && outputFile.EndsWith(".xml"))
            {
                var turtle = File.ReadAllText(inputFile);
                var resource = FhirParser.ParseResourceFromTurtle(turtle);
                var xml = FhirSerializer.SerializeResourceToXml(resource);
                File.WriteAllText(outputFile, xml);
            }
            else
                Debug.WriteLine("convertResource unsupported convertion");
        }

        private void convertFeed(string inputFile, string outputFile)
        {
            //TODO: call validation after reading

            if (inputFile.EndsWith(".xml"))
            {
                var xml = File.ReadAllText(inputFile);
                var resource = FhirParser.ParseResourceFromXml(xml);
                var json = FhirSerializer.SerializeResourceToJson(resource);
                File.WriteAllText(outputFile, json);
            }
            else if (inputFile.EndsWith(".json"))
            {
                var json = File.ReadAllText(inputFile);
                var resource = FhirParser.ParseResourceFromJson(json);
                var xml = FhirSerializer.SerializeResourceToXml(resource);
                File.WriteAllText(outputFile, xml);
            }
            else if (inputFile.EndsWith(".ttl"))
            {
                var turtle = File.ReadAllText(inputFile);
                var resource = FhirParser.ParseResourceFromTurtle(turtle);
                var xml = FhirSerializer.SerializeResourceToXml(resource);
                File.WriteAllText(outputFile, xml);
            }
            else
                Debug.WriteLine("convertFeed unsupported convertion");
        }
    }
}
