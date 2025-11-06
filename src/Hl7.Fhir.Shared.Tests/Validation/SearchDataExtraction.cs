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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml;

namespace Hl7.Fhir.Test.Validation;

[TestClass]
public partial class ValidateSearchExtractionAllExamplesTest
{
    [TestMethod]
    [TestCategory("LongRunner")]
    public void SearchExtractionAllExamples()
    {
        string examplesZip = @"TestData/examples.zip";

        FhirXmlDeserializer deserializer = FhirXmlDeserializer.RECOVERABLE;
        int errorCount = 0;
        int parserErrorCount = 0;
        Dictionary<String, int> exampleSearchValues = new();

        using var zip = ZipFile.OpenRead(examplesZip);
        foreach (var entry in zip.Entries)
        {
            if (mustSkip(entry.Name)) continue;

            Stream file = entry.Open();
            using (file)
            {
                try
                {
                    // Debug.WriteLine(String.Format("Validating {0}", file));
                    var reader = SerializationUtil.WrapXmlReader(XmlReader.Create(file));
                    var resource = deserializer.Deserialize<Resource>(reader);

                    extractValuesForSearchParameterFromFile(exampleSearchValues, resource);

                    if (resource is Bundle bundle)
                    {
                        foreach (var item in bundle.Entry.Where(item => item.Resource != null))
                        {
                            extractValuesForSearchParameterFromFile(exampleSearchValues, item.Resource);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("Error processing file " + entry.Name + ": " + ex.Message);
                    parserErrorCount++;
                }
            }
        }

        var missingSearchValues = exampleSearchValues.Where(i => i.Value == 0).ToArray();
        if (missingSearchValues.Any())
        {
            Debug.WriteLine(
                $"\r\n------------------\r\n" +
                $"Validation failed, missing data in {missingSearchValues.Length} of " +
                $"{exampleSearchValues.Count} search parameters");

            foreach (var item in missingSearchValues)
            {
                Trace.WriteLine("\t" + item.Key);
            }

            errorCount++;
        }

        Assert.IsGreaterThanOrEqualTo(errorCount,
43, $"Failed Validating, missing data in {missingSearchValues.Length} of " +
            $"{exampleSearchValues.Count} search parameters");
        Assert.AreEqual(0, parserErrorCount,
            $"Failed search parameter data extraction, {parserErrorCount} files failed parsing");

        bool mustSkip(string fileName) => _filesToBeSkipped.Any(s => s.StartsWith(fileName));
    }

    private static void extractValuesForSearchParameterFromFile(Dictionary<string, int> exampleSearchValues, Resource resource)
    {
        // Extract the search properties
        var searchparameters = ModelInfo.SearchParameters.Where(r => r.Resource == resource.TypeName && !String.IsNullOrEmpty(r.Expression));
        foreach (var index in searchparameters)
        {
            // prepare the search data cache
            string key = resource.TypeName + "_" + index.Name;
            exampleSearchValues.TryAdd(key, 0);

            // Extract the values from the example
            extractExamplesFromResource(exampleSearchValues, resource, index, key);
        }

        // If there are any contained resources, extract index data from those too!
        if (resource is DomainResource domainResource)
        {
            if (domainResource.Contained is { Count: > 0 })
            {
                foreach (var conResource in domainResource.Contained)
                {
                    extractValuesForSearchParameterFromFile(exampleSearchValues, conResource);
                }
            }
        }
    }

    private static void extractExamplesFromResource(Dictionary<string, int> exampleSearchValues, Resource resource, SearchParamDefinition index, string key)
    {
        List<Base> results;
        try
        {
            // we perform the Select on a Poco, because then we get the FHIR dialect of FhirPath as well.
            results = resource.Select(index.Expression!,
                new FhirEvaluationContext { ElementResolver = mockResolver }).ToList();
        }
        catch (Exception)
        {
            Trace.WriteLine($"Failed processing search expression {index.Name}: {index.Expression}");
            throw;
        }

        if (!results.Any()) return;

        foreach (var t2 in results.Select(r => r.ToTypedElement()))
        {
            var fhirval = t2.Annotation<IFhirValueProvider>();
            if (fhirval?.FhirValue != null)
            {
                // Validate the type of data returned against the type of search parameter
                //    Debug.Write(index.Resource + "." + index.Name + ": ");
                //    Debug.WriteLine((t2 as FhirPath.ModelNavigator).FhirValue.ToString());// + "\r\n";
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
            }
            else
            {
                Debug.Write(index.Resource + "." + index.Name + ": ");
                Debug.WriteLine(t2.Value);
            }

            exampleSearchValues[key]++;
        }
    }

    private static PocoNode mockResolver(string url)
    {
        var ri = new ResourceIdentity(url);

        if (!string.IsNullOrEmpty(ri.ResourceType))
        {
            var type = ModelInfo.GetTypeForFhirType(ri.ResourceType)!;
            DomainResource res = Activator.CreateInstance(type) as DomainResource;
            res!.Id = ri.Id;
            return res.ToPocoNode();
        }
        return null;
    }
}