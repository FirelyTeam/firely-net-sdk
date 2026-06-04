using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Hl7.Fhir.Specification.Tests;

/// <summary>
/// All this is to do is read all the unit test data to ensure that they are all compatible with STU3
/// (By just trying to de-serialize all the content)
/// </summary>
[TestClass]
public class SpecificationTestDataVersionCheck
{
    [TestMethod]
    public void VerifyAllTestDataSpecification()
    {
        string location = typeof(TestDataHelper).GetTypeInfo().Assembly.Location;
        var path = Path.GetDirectoryName(location) + "/TestData";
        Console.WriteLine(path);
        List <string> issues = [];
        validateFolder(path, path, issues);
        Assert.IsEmpty(issues);
    }

    private static void validateFolder(string basePath, string path, List<string> issues)
    {
        if (path.Contains("grahame-validation-examples"))
            return;
        if (path.Contains("source-test"))
            return;
        if (path.Contains("Type Slicing"))
            return;
        if (path.Contains("validation-test-suite"))
            return;


        var xmlParser = FhirXmlDeserializer.OSTRICH;
        var jsonParser = FhirJsonDeserializer.OSTRICH;
        Console.WriteLine($"Validating test files in {path.Replace(basePath, "")}");
        foreach (var item in Directory.EnumerateFiles(path))
        {
            string content = File.ReadAllText(item);
            try
            {
                if (item.EndsWith(".dll"))
                    continue;
                if (item.EndsWith(".exe"))
                    continue;
                if (item.EndsWith(".pdb"))
                    continue;
                if (item.EndsWith("manifest.json"))
                    continue;
                if (new FileInfo(item).Extension == ".xml")
                {
                    // Console.WriteLine($"    {item.Replace(path + "\\", "")}");
                    xmlParser.Deserialize<Resource>(content);
                }
                else if (new FileInfo(item).Extension == ".json")
                {
                    // Console.WriteLine($"    {item.Replace(path + "\\", "")}");
                    jsonParser.Deserialize<Resource>(content);
                }
                else
                {
                    Console.WriteLine($"    {item} (unknown content)");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"    {item} (parse error)");
                Console.WriteLine($"        --> {ex.Message}");
                issues.Add($"        --> {ex.Message}");
            }
        }
        foreach (var item in Directory.EnumerateDirectories(path))
        {
            validateFolder(basePath, item, issues);
        }
    }
}