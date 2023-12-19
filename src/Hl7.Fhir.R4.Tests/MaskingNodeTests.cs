using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Tests;

[TestClass]
public class MaskingNodeTests
{
    [TestMethod]
    public void MaskingNode_WithChoiceElementFromJson_KeepElement()
    {
        // Resource exactly as retrieved from DB in FS.
        // This test fails due to assertion failure 
        var resourceJson = @"{
  ""resourceType"": ""Patient"",
  ""id"": ""louis-test"",
  ""birthDate"": ""1976-07-22"",
  ""deceasedDateTime"": ""1998-10-14T00:22:15+00:00"",
  ""meta"": {
    ""versionId"": ""b7fe3246-4901-45ac-bf7f-aa9128b95cf8"",
    ""lastUpdated"": ""2023-12-19T14:39:12.750+00:00""
  }
}";
        var sourceNode = FhirJsonNode.Parse(resourceJson, settings: new FhirJsonParsingSettings { AllowJsonComments = true, PermissiveParsing = true });
        var typedNode = sourceNode.ToTypedElement(ModelInfo.ModelInspector);
        var elementsToBeIncluded = new List<string> { "deceased" };
        
        var maskingNode = new MaskingNode(new ScopedNode(typedNode), new MaskingNodeSettings
        {
            IncludeElements = elementsToBeIncluded.ToArray(),
            IncludeMandatory = true,
            PreserveBundle = MaskingNodeSettings.PreserveBundleMode.All
        });
        
        var maskingNodeChildren = maskingNode.Children().ToList();
        maskingNodeChildren.Should().HaveCount(1, "'deceased' node should be included");
    }
    
    [TestMethod]
    public void MaskingNode_WithChoiceElementFromModel_KeepElement()
    {
        // This test succeeds
        var patient = new Patient
        {
            // Set other patient details as needed
            Identifier = new List<Identifier> { new("http://example.org/patient-identifier", "12346") },
            BirthDate = "1980-01-01", 
            Deceased = new FhirDateTime("2023-12-15"),
        };
        
        var typedNode = patient.ToTypedElement(ModelInfo.ModelInspector);
        var elementsToBeIncluded = new List<string> { "deceased" };
        
        var maskingNode = new MaskingNode(new ScopedNode(typedNode), new MaskingNodeSettings
        {
            IncludeElements = elementsToBeIncluded.ToArray(),
            IncludeMandatory = true,
            PreserveBundle = MaskingNodeSettings.PreserveBundleMode.All
        });

        var maskingNodeChildren = maskingNode.Children().ToList();
        maskingNodeChildren.Should().HaveCount(1, "'deceased' node should be included");
    }
}