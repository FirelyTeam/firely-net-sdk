/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Hl7.FhirPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml;
using static Hl7.Fhir.Model.ModelInfo;

namespace Hl7.Fhir.Test.Validation
{
    [TestClass]
    public partial class ValidateSearchExtractionAllExamplesTest
    {
        [TestMethod]
        [TestCategory("LongRunner")]
        public void SearchExtractionAllExamples()
        {
            string examplesZip = @"TestData/examples.zip";

            FhirXmlParser parser = new();
            int errorCount = 0;
            int parserErrorCount = 0;
            int testFileCount = 0;
            Dictionary<String, int> exampleSearchValues = new();
            Dictionary<string, int> failedInvariantCodes = new();

            using var zip = ZipFile.OpenRead(examplesZip);
            foreach (var entry in zip.Entries)
            {
                if (mustSkip(entry.Name)) continue;

                Stream file = entry.Open();
                using (file)
                {
                    testFileCount++;

                    try
                    {
                        // Debug.WriteLine(String.Format("Validating {0}", file));
                        var reader = SerializationUtil.WrapXmlReader(XmlReader.Create(file));
                        var resource = parser.Parse<Resource>(reader);


                        ExtractValuesForSearchParameterFromFile(exampleSearchValues, resource);

                        if (resource is Bundle)
                        {
                            foreach (var item in (resource as Bundle).Entry)
                            {
                                if (item.Resource != null)
                                {
                                    ExtractValuesForSearchParameterFromFile(exampleSearchValues, item.Resource);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Trace.WriteLine("Error processing file " + entry.Name + ": " + ex.Message);
                        parserErrorCount++;
                    }
                }
            }

            var missingSearchValues = exampleSearchValues.Where(i => i.Value == 0);
            if (missingSearchValues.Any())
            {
                Debug.WriteLine(String.Format("\r\n------------------\r\nValidation failed, missing data in {0} of {1} search parameters", missingSearchValues.Count(), exampleSearchValues.Count));
                foreach (var item in missingSearchValues)
                {
                    Trace.WriteLine("\t" + item.Key);
                }
                // Trace.WriteLine(outcome.ToString());
                errorCount++;
            }

            Assert.IsTrue(43 >= errorCount, String.Format("Failed Validating, missing data in {0} of {1} search parameters", missingSearchValues.Count(), exampleSearchValues.Count));
            Assert.AreEqual(0, parserErrorCount, String.Format("Failed search parameter data extraction, {0} files failed parsing", parserErrorCount));

            bool mustSkip(string fileName) => _filesToBeSkipped.Any(s => s.StartsWith(fileName));
        }

        private static void ExtractValuesForSearchParameterFromFile(Dictionary<string, int> exampleSearchValues, Resource resource)
        {
            // Extract the search properties
            var searchparameters = ModelInfo.SearchParameters.Where(r => r.Resource == resource.TypeName && !String.IsNullOrEmpty(r.Expression));
            foreach (var index in searchparameters)
            {
                // prepare the search data cache
                string key = resource.TypeName + "_" + index.Name;
                if (!exampleSearchValues.ContainsKey(key))
                    exampleSearchValues.Add(key, 0);

                // Extract the values from the example
                ExtractExamplesFromResource(exampleSearchValues, resource, index, key);
            }

            // If there are any contained resources, extract index data from those too!
            if (resource is DomainResource)
            {
                if ((resource as DomainResource).Contained != null && (resource as DomainResource).Contained.Count > 0)
                {
                    foreach (var conResource in (resource as DomainResource).Contained)
                    {
                        ExtractValuesForSearchParameterFromFile(exampleSearchValues, conResource);
                    }
                }
            }
        }

        private static void ExtractExamplesFromResource(Dictionary<string, int> exampleSearchValues, Resource resource, SearchParamDefinition index, string key)
        {
            IEnumerable<Base> results;
            try
            {
                // we perform the Select on a Poco, because then we get the FHIR dialect of FhirPath as well.
                results = resource.Select(index.Expression!, new FhirEvaluationContext { ElementResolver = mockResolver });
            }
            catch (Exception)
            {
                Trace.WriteLine($"Failed processing search expression {index.Name}: {index.Expression}");
                throw;
            }
            if (results.Any())
            {
                foreach (var t2 in results.Select(r => r.ToTypedElement()))
                {
                    if (t2 != null)
                    {
                        var fhirval = t2.Annotation<IFhirValueProvider>();
                        if (fhirval?.FhirValue != null)
                        {
                            // Validate the type of data returned against the type of search parameter
                            //    Debug.Write(index.Resource + "." + index.Name + ": ");
                            //    Debug.WriteLine((t2 as FhirPath.ModelNavigator).FhirValue.ToString());// + "\r\n";
                            exampleSearchValues[key]++;
                        }
                        //else if (t2.Value is Hl7.FhirPath.ConstantValue)
                        //{
                        //    //    Debug.Write(index.Resource + "." + index.Name + ": ");
                        //    //    Debug.WriteLine((t2.Value as Hl7.FhirPath.ConstantValue).Value);
                        //    exampleSearchValues[key]++;
                        //}
                        else if (t2.Value is bool)
                        {
                            //    Debug.Write(index.Resource + "." + index.Name + ": ");
                            //    Debug.WriteLine((bool)t2.Value);
                            exampleSearchValues[key]++;
                        }
                        else
                        {
                            Debug.Write(index.Resource + "." + index.Name + ": ");
                            Debug.WriteLine(t2.Value);
                            exampleSearchValues[key]++;
                        }
                    }
                }
            }
        }

        private static ITypedElement mockResolver(string url)
        {
            ResourceIdentity ri = new ResourceIdentity(url);
            if (!string.IsNullOrEmpty(ri.ResourceType))
            {
                var fac = new Hl7.Fhir.Serialization.DefaultModelFactory();
                var type = ModelInfo.GetTypeForFhirType(ri.ResourceType);
                DomainResource res = fac.Create(type) as DomainResource;
                res.Id = ri.Id;
                return res.ToTypedElement();
            }
            return null;
        }
    }
}