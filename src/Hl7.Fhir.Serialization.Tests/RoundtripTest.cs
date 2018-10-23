/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Hl7.Fhir.Model;
using System.IO.Compression;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Tests;
using Hl7.Fhir.Specification.Source;

namespace Hl7.Fhir.Serialization.Tests
{
    [TestClass]
    public class RoundtripTest
    { 
        [TestMethod]
        [TestCategory("LongRunner")]
        public void FullRoundtripOfAllExamplesXmlPoco()
        {
            FullRoundtripOfAllExamples("examples.zip", "FHIRRoundTripTestXml", 
                "Roundtripping xml->json->xml", usingPoco: true, provider:null);
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        public void FullRoundtripOfAllExamplesJsonPoco()
        {
            FullRoundtripOfAllExamples("examples-json.zip", "FHIRRoundTripTestJson",
                "Roundtripping json->xml->json", usingPoco: true, provider: null);
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        public void FullRoundtripOfAllExamplesXmlNavPocoProvider()
        {
            FullRoundtripOfAllExamples("examples.zip", "FHIRRoundTripTestXml",
                "Roundtripping xml->json->xml", usingPoco: false, provider: new PocoStructureDefinitionSummaryProvider());
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        public void FullRoundtripOfAllExamplesJsonNavPocoProvider()
        {
            FullRoundtripOfAllExamples("examples-json.zip", "FHIRRoundTripTestJson",
                "Roundtripping json->xml->json", usingPoco: false, provider: new PocoStructureDefinitionSummaryProvider());
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        public void FullRoundtripOfAllExamplesXmlNavSdProvider()
        {
            var source = new CachedResolver(ZipSource.CreateValidationSource());
            FullRoundtripOfAllExamples("examples.zip", "FHIRRoundTripTestXml",
                "Roundtripping xml->json->xml", usingPoco: false, provider: new StructureDefinitionSummaryProvider(source));
        }

        [TestMethod]
        [TestCategory("LongRunner")]
        public void FullRoundtripOfAllExamplesJsonNavSdProvider()
        {
            var source = new CachedResolver(ZipSource.CreateValidationSource());
            FullRoundtripOfAllExamples("examples-json.zip", "FHIRRoundTripTestJson",
                "Roundtripping json->xml->json", usingPoco: false, provider: new StructureDefinitionSummaryProvider(source));
        }

        private static string GetFullPathForExample(string filename) => Path.Combine("TestData", filename);

        private static ZipArchive ReadTestZip(string filename)
        {
            string file = GetFullPathForExample(filename);
            return ZipFile.OpenRead(file);
        }

        public static void FullRoundtripOfAllExamples(string zipname, string dirname, string label, bool usingPoco, IStructureDefinitionSummaryProvider provider)
        {
            ZipArchive examples = ReadTestZip(zipname);

            // Create an empty temporary directory for us to dump the roundtripped intermediary files in
            string baseTestPath = Path.Combine(Path.GetTempPath(), dirname);
            createEmptyDir(baseTestPath);

            Debug.WriteLine(label);
            createEmptyDir(baseTestPath);
            doRoundTrip(examples, baseTestPath, usingPoco, provider);
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

        private static void doRoundTrip(ZipArchive examplesZip, string baseTestPath, bool usingPoco, IStructureDefinitionSummaryProvider provider)
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
            convertFiles(examplePath, intermediate1Path, usingPoco, provider);
            sw.Stop();
            Debug.WriteLine("Conversion took {0} seconds", sw.ElapsedMilliseconds / 1000);
            sw.Reset();

            var intermediate2Path = Path.Combine(baseTestPath, "intermediate2");
            Debug.WriteLine("Re-converting files in {0} back to original format in {1}", intermediate1Path, intermediate2Path);
            sw.Start();
            convertFiles(intermediate1Path, intermediate2Path, usingPoco, provider);
            sw.Stop();
            Debug.WriteLine("Conversion took {0} seconds", sw.ElapsedMilliseconds / 1000);
            sw.Reset();

            Debug.WriteLine("Comparing files in {0} to files in {1}", baseTestPath, intermediate2Path);
            compareFiles(examplePath, intermediate2Path);
        }


        private static void convertFiles(string inputPath, string outputPath, bool usingPoco, IStructureDefinitionSummaryProvider provider)
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

                if (usingPoco)
                    convertResourcePoco(file, outputFile);
                else
                    convertResourceNav(file, outputFile, provider);

            }

            Debug.WriteLine("Done!");
        }


        private static void compareFiles(string expectedPath, string actualPath)
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

                compareFile(file, actualFile);
            }
        }

        private static void compareFile(string expectedFile, string actualFile)
        {
            if (expectedFile.EndsWith(".xml"))
                XmlAssert.AreSame(new FileInfo(expectedFile).Name, File.ReadAllText(expectedFile),
                    File.ReadAllText(actualFile));
            else
            {
                if (new FileInfo(expectedFile).Name != "json-edge-cases.json")
                {
                    JsonAssert.AreSame(File.ReadAllText(expectedFile),
                                        File.ReadAllText(actualFile));
                }
            }
        }


        private static void convertResourcePoco(string inputFile, string outputFile)
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

        private static void convertResourceNav(string inputFile, string outputFile, IStructureDefinitionSummaryProvider provider)
        {
            //TODO: call validation after reading
            if (inputFile.Contains("expansions.") || inputFile.Contains("profiles-resources") || inputFile.Contains("profiles-others") || inputFile.Contains("valuesets."))
                return;
            if (inputFile.EndsWith(".xml"))
            {
                var xml = File.ReadAllText(inputFile);
                var nav = XmlParsingHelpers.ParseToTypedElement(xml, provider);
                var json = nav.ToJson();
                File.WriteAllText(outputFile, json);
            }
            else
            {
                var json = File.ReadAllText(inputFile);
                var nav = JsonParsingHelpers.ParseToTypedElement(json, provider, 
                    settings: new FhirJsonParsingSettings { AllowJsonComments = true } );
                var xml = nav.ToXml();
                File.WriteAllText(outputFile, xml);
            }
        }

    }
}
