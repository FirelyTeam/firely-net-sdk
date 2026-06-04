using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Model.CdsHooks;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hl7.Fhir.Support.Poco.Tests.CdsHooks;

[TestClass]
public class CdsHooksSerializationTests
{
    [TestMethod]
    [Experimental("ExperimentalApi")]
    public void CdsHookCombined_Serialize()
    {
        var request = new Request
        {
            HookInstance = "d1577c69-dfbe-44ad-ba6d-3e05e953b2ea",
            FhirServer = new Uri("http://fhir.example.com"),
            Hook = "order-sign",
            Context = new Context
            {
                UserId = "Practitioner/example",
                PatientId = "1288992"
            },
            Prefetch = new Dictionary<string, Resource>
            {
                {
                    "patient",
                    new Patient
                    {
                        Id = "1288992",
                        Active = true,
                        Name = new List<HumanName> { new HumanName { Family = "Shaw", Given = new List<string> { "Amy" } } }
                    }
                }
            }
        };

        var options = new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }.ForCdsHooks().Compact();
        var json = JsonSerializer.Serialize(request, options);

        var expectedJson = """{"hookInstance":"d1577c69-dfbe-44ad-ba6d-3e05e953b2ea","fhirServer":"http://fhir.example.com","hook":"order-sign","context":{"userId":"Practitioner/example","patientId":"1288992"},"prefetch":{"patient":{"resourceType":"Patient","id":"1288992","active":true,"name":[{"family":"Shaw","given":["Amy"]}]}}}""";

        expectedJson.Should().BeOneOf([json]);
    }
}