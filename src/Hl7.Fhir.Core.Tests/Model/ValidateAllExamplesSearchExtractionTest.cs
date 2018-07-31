/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.FhirPath;
using Hl7.Fhir.Utility;
using Hl7.Fhir.ElementModel;

namespace Hl7.Fhir.Tests.Model
{
    [TestClass]
    public class ValidateSearchExtractionAllExamplesTest
    {
        [TestMethod]
        [TestCategory("LongRunner")]
        public void SearchExtractionAllExamples()
        {
            FhirXmlParser parser = new FhirXmlParser();
            int errorCount = 0;
            int testFileCount = 0;
            Dictionary<String, int> exampleSearchValues = new Dictionary<string, int>();
            Dictionary<string, int> failedInvariantCodes = new Dictionary<string, int>();
            var zip = TestDataHelper.ReadTestZip("examples.zip");

            using (zip)
            {
                foreach (var entry in zip.Entries)
                {
                    Stream file = entry.Open();
                    using (file)
                    {
                        // Verified examples that fail validations

                        //// vsd-3, vsd-8
                        //if (file.EndsWith("valueset-ucum-common(ucum-common).xml"))
                        //    continue;

                        testFileCount++;
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

            Assert.IsTrue(140 >= errorCount, String.Format("Failed search parameter data extraction, missing data in {0} of {1} search parameters", missingSearchValues.Count(), exampleSearchValues.Count));
        }

        private static void ExtractValuesForSearchParameterFromFile(Dictionary<string, int> exampleSearchValues, Resource resource)
        {
            // Extract the search properties
            var searchparameters = ModelInfo.SearchParameters.Where(r => r.Resource == resource.ResourceType.ToString() && !String.IsNullOrEmpty(r.Expression));
            foreach (var index in searchparameters)
            {
                // prepare the search data cache
                string key = resource.ResourceType.ToString() + "_" + index.Name;
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

        private static void ExtractExamplesFromResource(Dictionary<string, int> exampleSearchValues, Resource resource, ModelInfo.SearchParamDefinition index, string key)
        {
            var resourceModel = resource.ToElementNavigator();
            var navigator = resource.ToElementNavigator();

            try
            {
                var results = resourceModel.Select(index.Expression, new EvaluationContext(navigator));
                if (results.Count() > 0)
                {
                    foreach (var t2 in results)
                    {
                        if (t2 != null)
                        {
                            if (t2 is PocoNavigator && (t2 as PocoNavigator).FhirValue != null)
                            {
                                // Validate the type of data returned against the type of search parameter
                            //    Debug.Write(index.Resource + "." + index.Name + ": ");
                            //    Debug.WriteLine((t2 as FhirPath.ModelNavigator).FhirValue.ToString());// + "\r\n";
                                exampleSearchValues[key]++;
                            }
                            else if (t2.Value is Hl7.FhirPath.ConstantValue)
                            {
                            //    Debug.Write(index.Resource + "." + index.Name + ": ");
                            //    Debug.WriteLine((t2.Value as Hl7.FhirPath.ConstantValue).Value);
                                exampleSearchValues[key]++;
                            }
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
            catch(ArgumentException ex)
            {
                Debug.WriteLine("FATAL: Error parsing expression in search index {0}.{1} {2}\r\n\t{3}", index.Resource, index.Name, index.Expression, ex.Message);
            }
        }
    }
}
