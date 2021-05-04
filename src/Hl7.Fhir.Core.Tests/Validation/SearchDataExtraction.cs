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
#if NET40
using ICSharpCode.SharpZipLib.Zip;
#endif

namespace Hl7.Fhir.Test.Validation
{
    [TestClass]
    public class ValidateSearchExtractionAllExamplesTest
    {
        [TestInitialize]
        public void Setup()
        {
            ElementNavFhirExtensions.PrepareFhirSymbolTableFunctions();
        }


        [TestMethod]
        [TestCategory("LongRunner")]
        public void SearchExtractionAllExamples()
        {
            string examplesZip = @"TestData\examples.zip";

            FhirXmlParser parser = new FhirXmlParser();
            int errorCount = 0;
            int parserErrorCount = 0;
            int testFileCount = 0;
            Dictionary<String, int> exampleSearchValues = new Dictionary<string, int>();
            Dictionary<string, int> failedInvariantCodes = new Dictionary<string, int>();
#if NET40
            var zip = new ZipArchive(new ZipFile(examplesZip));
#else
            var zip = ZipFile.OpenRead(examplesZip);
#endif
            using (zip)
            {
                foreach (var entry in zip.Entries)
                {
                    Stream file = entry.Open();
                    using (file)
                    {
                        // Verified examples that fail validations

                        if (entry.Name.Contains("v2-tables"))
                            continue; // this file is known to have a single dud valueset - have reported on Zulip
                                      // https://chat.fhir.org/#narrow/stream/48-terminology/subject/v2.20Table.200550
                        if (entry.Name == "observation-decimal(decimal).xml")
                            continue; // this file has a Literal with value '-1.000000000000000000e245', which does not fit into a c# datatype

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

            Assert.IsTrue(43 >= errorCount, String.Format("Failed Validating, missing data in {0} of {1} search parameters", missingSearchValues.Count(), exampleSearchValues.Count));
            Assert.AreEqual(0, parserErrorCount, String.Format("Failed search parameter data extraction, {0} files failed parsing", parserErrorCount));
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

        private static void ExtractExamplesFromResource(Dictionary<string, int> exampleSearchValues, Resource resource, ModelInfo.SearchParamDefinition index, string key)
        {
            var resourceModel = new ScopedNode(resource.ToTypedElement());

            IEnumerable<ITypedElement> results;
            try
            {
                results = resourceModel.Select(index.Expression, new FhirEvaluationContext(resourceModel) { ElementResolver = mockResolver });
            }
            catch (Exception)
            {
                Trace.WriteLine($"Failed processing search expression {index.Name}: {index.Expression}");
                throw;
            }
            if (results.Count() > 0)
            {
                foreach (var t2 in results)
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
