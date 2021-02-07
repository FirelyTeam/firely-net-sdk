﻿/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Hl7.Fhir.Model;
using System.IO.Compression;
using Hl7.Fhir.Specification;
using Hl7.Fhir.Specification.Source;
using System.Collections.Generic;
using System;
using Hl7.Fhir.Tests;
#if NET40
using ICSharpCode.SharpZipLib.Zip;
using System.Linq;
#endif

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

#if NET40
        public static ZipArchive ReadTestZip(string filename)
        {
            string file = GetFullPathForExample(filename);
            return new ZipArchive(new ZipFile(file));
        }

        public class ZipArchiveEntry
        {
            private ZipFile _zip;
            private ZipEntry _zipEntry;

            public ZipArchiveEntry(ZipEntry zipEntry, ZipFile zipFile)
            {
                _zipEntry = zipEntry;
                _zip = zipFile;
            }

            public string Name
            {
                get
                {
                    return _zipEntry.Name;
                }
            }

            public Stream Open()
            {
                return _zip.GetInputStream(_zipEntry);
            }
        }

        public class ZipArchive : IDisposable
        {
            private ZipFile _zip;

            public ZipArchive(ZipFile zip)
            {
                _zip = zip;
            }

            public IEnumerable<ZipArchiveEntry> Entries
            {
                get
                {
                    return _zip.Cast<ZipEntry>().Select(e => new ZipArchiveEntry(e, _zip));
                }
            }

            public void Dispose()
            {
                ((IDisposable)_zip).Dispose();
            }

            public void ExtractToDirectory(string directory)
            {
                byte[] buffer = new byte[4096];

                foreach (ZipEntry entry in _zip)
                {
                    using (Stream entryStream = _zip.GetInputStream(entry))
                    {
                        string fullPath = Path.Combine(directory, entry.Name.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar));
                        FileInfo entryFileInfo = new FileInfo(fullPath);

                        if (!Directory.Exists(entryFileInfo.DirectoryName))
                        {
                            Directory.CreateDirectory(entryFileInfo.DirectoryName);
                        }

                        using (FileStream entryOutputStream = File.Create(fullPath))
                        {
                            int bytesRead = entryStream.Read(buffer, 0, 4096);

                            while (bytesRead > 0)
                            {
                                entryOutputStream.Write(buffer, 0, bytesRead);
                                bytesRead = entryStream.Read(buffer, 0, 4096);
                            }
                        }
                    }
                }
            }
        }
#else
        public static ZipArchive ReadTestZip(string filename)
        {
            string file = GetFullPathForExample(filename);
            return ZipFile.OpenRead(file);
        }
#endif

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

            List<string> errors = new List<string>();
            var intermediate1Path = Path.Combine(baseTestPath, "intermediate1");
            Debug.WriteLine("Converting files in {0} to {1}", baseTestPath, intermediate1Path);
            var sw = new Stopwatch();
            sw.Start();
            int convertedFileCount = convertFiles(examplePath, intermediate1Path, usingPoco, provider, errors);
            sw.Stop();
            Debug.WriteLine("Conversion took {0} seconds", sw.ElapsedMilliseconds / 1000);
            sw.Reset();

            var intermediate2Path = Path.Combine(baseTestPath, "intermediate2");
            Debug.WriteLine("Re-converting files in {0} back to original format in {1}", intermediate1Path, intermediate2Path);
            sw.Start();
            convertFiles(intermediate1Path, intermediate2Path, usingPoco, provider, errors);
            sw.Stop();
            Console.WriteLine("Conversion of {1} files took {0} seconds", sw.ElapsedMilliseconds / 1000, convertedFileCount);
            sw.Reset();

            Debug.WriteLine("Comparing files in {0} to files in {1}", baseTestPath, intermediate2Path);

            compareFiles(examplePath, intermediate2Path, errors);
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine(String.Join("\r\n", errors));
            Assert.AreEqual(0, errors.Count, "Errors were encountered comparing converted content");
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

        private static int convertFiles(string inputPath, string outputPath, bool usingPoco, IStructureDefinitionSummaryProvider provider, List<string> errors)
        {
            int fileCount = 0;
            var files = Directory.EnumerateFiles(inputPath);
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
                    if (usingPoco)
                        convertResourcePoco(file, outputFile);
                    else
                        convertResourceNav(file, outputFile, provider);
                }
                catch(Exception ex)
                {
                    errors.Add($"{exampleName}{ext}: " + ex.Message);
                }
            }

            Debug.WriteLine("Done!");
            return fileCount;
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


        private static void convertResourcePoco(string inputFile, string outputFile)
        {
            if (inputFile.EndsWith(".xml"))
            {
                var xml = File.ReadAllText(inputFile);
                var resource = new FhirXmlParser(new ParserSettings { PermissiveParsing = true }).Parse<Resource>(xml);

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
                var resource = new FhirJsonParser(new ParserSettings { PermissiveParsing = true }).Parse<Resource>(json);
                var xml = new FhirXmlSerializer().SerializeToString(resource);
                File.WriteAllText(outputFile, xml);
            }
        }

        private static void convertResourceNav(string inputFile, string outputFile, IStructureDefinitionSummaryProvider provider)
        {
            if (inputFile.EndsWith(".xml"))
            {
                var xml = File.ReadAllText(inputFile);
                var nav = XmlParsingHelpers.ParseToTypedElement(xml, provider, new FhirXmlParsingSettings { PermissiveParsing = true });
                var json = nav.ToJson();
                File.WriteAllText(outputFile, json);
            }
            else
            {
                var json = File.ReadAllText(inputFile);
                var nav = JsonParsingHelpers.ParseToTypedElement(json, provider, 
                    settings: new FhirJsonParsingSettings { AllowJsonComments = true, PermissiveParsing = true } );
                var xml = nav.ToXml();
                File.WriteAllText(outputFile, xml);
            }
        }

    }
}
