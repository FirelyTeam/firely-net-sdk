/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Support.Tests.Serialization;

[TestClass]
public class TestDictionaryImplementation
{
    [TestMethod]
    public void CanEnumerateFhirPrimitive()
    {
        var b = new FhirBoolean(null).GetElementList();
        b.Any().Should().Be(false);

        b = new FhirBoolean(true).GetElementList();
        b.Any().Should().Be(false);

        var nb = new FhirBoolean(true);
        nb.SetStringExtension("http://nu.nl", "then");
        nb.ElementId = "id1";

        b = nb.GetElementList();
        b.Count.Should().Be(2);

        b[0].Key.Should().Be("id");
        b[0].Value.Should().BeOfType<FhirString>().Which.Value.Should().Be("id1");
        b[1].Key.Should().Be("extension");
        b[1].Value.Should().BeAssignableTo<IEnumerable<Extension>>();

        nb.TryGetValue("id", out var v).Should().BeTrue();
        v.Should().BeOfType<FhirString>().Which.Value.Should().Be("id1");
        nb.TryGetValue("idX", out _).Should().BeFalse();
    }

    [TestMethod]
    public void CanEnumerateNarrative()
    {
        var b = new Narrative("<p>bla</p>").GetElementList();
        b.Count.Should().Be(2);

        b[0].Key.Should().Be("status");
        b[0].Value.Should().BeOfType<Code<Narrative.NarrativeStatus>>().Which.Value.Should().Be(Narrative.NarrativeStatus.Generated);

        b[1].Key.Should().Be("div");
        b[1].Value.Should().BeOfType<XHtml>().Which.Value.Should().Be("<p>bla</p>");
    }

    [TestMethod]
    public void CanEnumerateExtension()
    {
        // Explicitly test hand-written IROD implementation.
        var ext = new Extension("http://nu.nl", new FhirBoolean(true));
        var b = ext.GetElementList();
        b.Count.Should().Be(2);
        b[0].Key.Should().Be("url");
        b[0].Value.Should().BeOfType<FhirUri>().Which.Value.Should().Be("http://nu.nl");
        b[1].Key.Should().Be("value");
        b[1].Value.Should().BeOfType<FhirBoolean>().Which.Value.Should().BeTrue();

        ext.TryGetValue("valueString", out _).Should().BeFalse();
        ext.TryGetValue("valueBoolean", out _).Should().BeFalse();
        ext.TryGetValue("valueXXXXBoolean", out _).Should().BeFalse();
        ext.TryGetValue("value", out var fb).Should().BeTrue();

        fb.Should().BeOfType<FhirBoolean>().Which.Value.Should().BeTrue();

        ext["value"].Should().BeOfType<FhirBoolean>().Which.Value.Should().BeTrue();
    }

    [TestMethod]
    public void HandlesChoiceElements()
    {
        var b = new Parameters.ParameterComponent { Name = "test1", Value = new FhirBoolean(true) };

        b.TryGetValue("valueString", out _).Should().BeFalse();
        b.TryGetValue("valueBoolean", out _).Should().BeFalse();
        b.TryGetValue("value", out var fb).Should().BeTrue();
        b.TryGetValue("valueXXXXBoolean", out _).Should().BeFalse();
        fb.Should().BeOfType<FhirBoolean>().Which.Value.Should().BeTrue();

        b["value"].Should().BeOfType<FhirBoolean>().Which.Value.Should().BeTrue();
    }

    private OperationOutcome setupOutcome()
    {
        OperationOutcome oo = new()
        {
            Id = "1",
            Meta = new Meta { Profile = ["http://simplifier.net/profiles/x"], VersionId = "2" }
        };

        var fu = new FhirUri();
        fu.SetStringExtension("http://ha.nl", "hi");
        oo.Meta.ProfileElement.Add(fu);

        oo.Issue.Add(
            new OperationOutcome.IssueComponent()
            {
                Code = OperationOutcome.IssueType.BusinessRule,
                Details = new CodeableConcept("http://nu.nl", "then"),
                Diagnostics = "This has low level information",
                Expression = ["Patient.x"],
                Severity = OperationOutcome.IssueSeverity.Error
            });
        oo.Id = "1";

        return oo;
    }

    [TestMethod]
    public void CanEnumerateResource()
    {
        var b = setupOutcome();
        b.GetElementList().Count.Should().Be(3);
        b.TryGetValue("resourceType", out _).Should().BeFalse();  // we do not generate "resourceType" anymore

        // Check a backbone
        var bb = b["issue"].Should().BeOfType<List<OperationOutcome.IssueComponent>>()
            .Subject.Single();
        bb.GetElementList().Select(kvp => kvp.Key)
            .Should().BeEquivalentTo("code", "details", "diagnostics", "expression", "severity");
    }

    [TestMethod]
    public void CanEnumerateContainedResources()
    {
        var parameters = new Parameters();
        parameters.Add("aBool", new FhirBoolean(true));
        parameters.Add("aResource", new OperationOutcome());

        var paramList = parameters["parameter"].Should().BeOfType<List<Parameters.ParameterComponent>>().Subject;
        paramList.Count.Should().Be(2);
        paramList[0].Name.Should().Be("aBool");
        paramList[1].Name.Should().Be("aResource");

        var ps = paramList[1];
        ps.TryGetValue("value", out _).Should().BeFalse();
        ps.TryGetValue("resource", out var r).Should().BeTrue();

        var resource = ps["resource"].Should().BeAssignableTo<Resource>().Subject;
        Assert.Throws<KeyNotFoundException>(() => resource["resourceType"]);
    }
}

file static class TestDictionaryHelpers
{
    public static List<KeyValuePair<string, object>> GetElementList(this Base b) => b.EnumerateElements().ToList();
}