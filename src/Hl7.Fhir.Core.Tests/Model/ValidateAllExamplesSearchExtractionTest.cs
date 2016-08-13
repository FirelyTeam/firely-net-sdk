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
using Hl7.Fhir.Validation;
using System.ComponentModel.DataAnnotations;

namespace Hl7.Fhir.Tests.Model
{
    [TestClass]
#if PORTABLE45
	public class PortableValidateSearchExtractionAllExamplesTest
#else
    public class ValidateSearchExtractionAllExamplesTest
#endif
    {
        [TestMethod]
        [TestCategory("LongRunner")]
        public void SearchExtractionAllExamples()
        {
            string examplesZip = @"TestData\examples.zip";

            // Create an empty temporary directory for us to dump the intermediary files in
            string baseTestPath = Path.Combine(Path.GetTempPath(), "FHIRValidateAllTestXml");
            if (Directory.Exists(baseTestPath))
                Directory.Delete(baseTestPath, true);
            Directory.CreateDirectory(baseTestPath);

            // Unzip files into this path
            Debug.WriteLine("Unzipping example files from {0} to {1}", examplesZip, baseTestPath);
            ZipFile.ExtractToDirectory(examplesZip, baseTestPath);

            Debug.WriteLine(String.Format("Validating files in {0}", baseTestPath));
            var files = Directory.EnumerateFiles(baseTestPath);
            FhirXmlParser parser = new FhirXmlParser();
            int errorCount = 0;
            int testFileCount = 0;
            Dictionary<String, int> exampleSearchValues = new Dictionary<string, int>();
            Dictionary<string, int> failedInvariantCodes = new Dictionary<string, int>();
            foreach (string file in files)
            {
                // Verified examples that fail validations

                //// vsd-3, vsd-8
                //if (file.EndsWith("valueset-ucum-common(ucum-common).xml"))
                //    continue;

                testFileCount++;
                // Debug.WriteLine(String.Format("Validating {0}", file));
                var resource = parser.Parse<Resource>(System.IO.File.ReadAllText(file));

                // Extract the search properties
                var searchparameters = ModelInfo.SearchParameters.Where(r => r.Resource == resource.ResourceType.ToString() && !String.IsNullOrEmpty(r.Expression));
                foreach (var index in searchparameters)
                {
                    // prepare the search data cache
                    string key = resource.ResourceType.ToString() + "_" + index.Name;
                    if (!exampleSearchValues.ContainsKey(key))
                        exampleSearchValues.Add(key, 0);

                    // Extract the values from the example
                    var resourceModel = FluentPath.ModelNavigator.CreateInput(resource);
                    var navigator = FluentPath.ModelNavigator.CreateInput(resource);
                    var results = Hl7.FluentPath.PathExpression.Select(index.Expression, resourceModel, navigator);
                    if (results.Count() > 0)
                    {
                        foreach (var t2 in results)
                        {
                            if (t2 != null)
                            {
                                if (t2 is FluentPath.ModelNavigator && (t2 as FluentPath.ModelNavigator).FhirValue != null)
                                {
                                    // Validate the type of data returned against the type of search parameter
                                    Debug.Write(index.Resource + "." + index.Name + ": ");
                                    Debug.WriteLine((t2 as FluentPath.ModelNavigator).FhirValue.ToString());// + "\r\n";
                                    exampleSearchValues[key]++;
                                    // System.Diagnostics.Trace.WriteLine(string.Format("{0}: {1}", xpath.Value, t2.AsStringRepresentation()));
                                }
                                else
                                {
                                    if (t2.Value is Hl7.FluentPath.ConstantValue)
                                    {
                                        Debug.Write(index.Resource + "." + index.Name + ": ");
                                        Debug.WriteLine((t2.Value as Hl7.FluentPath.ConstantValue).Value);
                                        exampleSearchValues[key]++;
                                    }
                                }
                            }
                        }
                    }

                }
            }

            var missingSearchValues = exampleSearchValues.Where(i => i.Value == 0);
            if (missingSearchValues.Count() > 0)
            {
                Debug.WriteLine(String.Format("\r\n------------------\r\nValidation failed, missing data in {0} of {1} search parameters", missingSearchValues.Count(), exampleSearchValues.Count));
                foreach (var item in missingSearchValues)
                {
                    Trace.WriteLine("\t" + item.Key);
                }
                // Trace.WriteLine(outcome.ToString());
                errorCount++;
            }

            Assert.IsTrue(235 >= errorCount, String.Format("Failed Validating, missing data in {0} of {1} search parameters", missingSearchValues.Count(), exampleSearchValues.Count));
        }

    }
}
