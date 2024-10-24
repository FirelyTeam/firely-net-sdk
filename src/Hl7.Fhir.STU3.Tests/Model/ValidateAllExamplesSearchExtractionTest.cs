/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
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
        public ILookup<ResourceType, ModelInfo.SearchParamDefinition> SpList;


        [TestMethod]
        [TestCategory("LongRunner")]
        public void SearchExtractionAllExamples()
        {
            SpList = ModelInfo.SearchParameters
                .Where(spd => !String.IsNullOrEmpty(spd.Expression))
                .Select(spd =>
                    new { Rt = (ResourceType)Enum.Parse(typeof(ResourceType), spd.Resource), Def = spd })
                    .ToLookup(ks => ks.Rt, es => es.Def);

            SearchExtractionAllExamplesInternal();
     //       SearchExtractionAllExamplesInternal();
        }


        private void SearchExtractionAllExamplesInternal()
        {
            FhirXmlParser parser = new FhirXmlParser(new ParserSettings { PermissiveParsing = true });
            int errorCount = 0;
            int parserErrorCount = 0;
            int testFileCount = 0;
            var exampleSearchValues = new Dictionary<ModelInfo.SearchParamDefinition, Holder>();
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

            var missingSearchValues = exampleSearchValues.Where(i => i.Value.count == 0);

            if (missingSearchValues.Count() > 0)
            {
                Debug.WriteLine(String.Format("\r\n------------------\r\nValidation failed, missing data in {0} of {1} search parameters", missingSearchValues.Count(), exampleSearchValues.Count));
                foreach (var item in missingSearchValues)
                {
                    Trace.WriteLine("\t" + item.Key.Resource.ToString() + "_" + item.Key.Name);
                }
                // Trace.WriteLine(outcome.ToString());
                errorCount++;
            }

            Assert.IsTrue(43 >= errorCount, String.Format("Failed search parameter data extraction, missing data in {0} of {1} search parameters", missingSearchValues.Count(), exampleSearchValues.Count));
            Assert.AreEqual(0, parserErrorCount, String.Format("Failed search parameter data extraction, {0} files failed parsing", parserErrorCount));
        }

        private void ExtractValuesForSearchParameterFromFile(Dictionary<ModelInfo.SearchParamDefinition, Holder> exampleSearchValues, Resource resource)
        {
            // Extract the search properties
            resource.TryDeriveResourceType(out var rt);
            var searchparameters = SpList[rt];
            foreach (var index in searchparameters)
            {
                // prepare the search data cache
                if (!exampleSearchValues.ContainsKey(index))
                    exampleSearchValues.Add(index, new Holder());

                // Extract the values from the example
                ExtractExamplesFromResource(exampleSearchValues, resource, index);
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


        class Holder
        {
            public int count;
        }

       
  
        private static void ExtractExamplesFromResource(Dictionary<ModelInfo.SearchParamDefinition, Holder> exampleSearchValues, Resource resource, 
            ModelInfo.SearchParamDefinition index )
        {
            var resourceModel = resource.ToTypedElement();

            try
            {
                var results = resourceModel.Select(index.Expression, new EvaluationContext());
                if (results.Count() > 0)
                {
                    foreach (var t2 in results)
                    {
                        if (t2 != null)
                        {
                            exampleSearchValues[index].count++;

                            if (t2 is PocoElementNode && (t2 as PocoElementNode).FhirValue != null)
                            {
                                // Validate the type of data returned against the type of search parameter
                                //    Debug.Write(index.Resource + "." + index.Name + ": ");
                                //    Debug.WriteLine((t2 as FhirPath.ModelNavigator).FhirValue.ToString());// + "\r\n";

                            }
                            //else if (t2.Value is Hl7.FhirPath.ConstantValue)
                            //{
                            //    //    Debug.Write(index.Resource + "." + index.Name + ": ");
                            //    //    Debug.WriteLine((t2.Value as Hl7.FhirPath.ConstantValue).Value);
                            //}
                            else if (t2.Value is bool)
                            {
                                //    Debug.Write(index.Resource + "." + index.Name + ": ");
                                //    Debug.WriteLine((bool)t2.Value);
                            }
                            else
                            {
                                Debug.Write(index.Resource + "." + index.Name + ": ");
                                Debug.WriteLine(t2.Value);
                            }
                        }
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Debug.WriteLine("FATAL: Error parsing expression in search index {0}.{1} {2}\r\n\t{3}", index.Resource, index.Name, index.Expression, ex.Message);
            }
        }
    }
}
