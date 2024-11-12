using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using IRO = System.Collections.Generic.IReadOnlyDictionary<string, object>;

namespace Hl7.Fhir.Tests.Model;

[TestClass]
public class PocoDictionaryTests
{
    [TestMethod]
    public void DynamicResourceAcceptsEverything()
    {
        var dr = new DynamicResource()
            {
                ["name"] = "John",
                ["age"] = 23,
                ["alive"] = true,
                ["dob"] = new Date(1972, 11, 30),
#pragma warning disable CA2244
                ["weight"] = 75.5m,
#pragma warning restore CA2244
                ["weight"] = 80.0m
            };

        dr["name"].Should().Be("John");
        dr["age"].Should().Be(23);
        dr["alive"].Should().Be(true);
        dr["dob"].Should().BeOfType<Date>().Which.Value.Should().Be("1972-11-30");
        dr["weight"].Should().Be(80.0m);

        dr["name"] = null!;
        dr.AsReadOnlyDictionary().ContainsKey("name").Should().BeFalse();
    }

    [TestMethod]
    public void ResourceAcceptsOverflow()
    {
        var pat = new Patient().AsDictionary();

        // setting an existing property to an incorrect type should fail.
        Assert.ThrowsException<InvalidCastException>(() => pat["name"] = "John");

        // Setting it correctly should work
        pat["name"] = new List<HumanName> { new HumanName().WithGiven("John") };

        // Adding a non-existing property should work
        pat["weight"] = 80.0m;

        pat["name"].Should().BeOfType<List<HumanName>>();
        pat["weight"].Should().Be(80.0m);

        pat["name"] = null!;
        pat["weight"] = null!;
        pat.Should().BeEmpty();
    }

    [TestMethod]
    public void CanReadSpecialProperties()
    {
        var patient = new Patient()
        {
            Text = new Narrative { Div = "<div>hello</div>" },
            Active = true,
            Meta = new Meta { ElementId = "4" },
        };

        patient.AddExtension("http://nu.nl", new FhirBoolean(true));
        var pat = patient.AsReadOnlyDictionary();

        pat["active"].Should().BeOfType<FhirBoolean>().And
            .BeAssignableTo<IRO>().Which["value"].Should().Be(true);
        pat["text"].Should().BeOfType<Narrative>().And
            .BeAssignableTo<IRO>().Which["div"].Should().BeOfType<XHtml>().And
            .BeAssignableTo<IRO>().Which["value"].Should().Be("<div>hello</div>");
        pat["meta"].Should().BeOfType<Meta>().And
            .BeAssignableTo<IRO>().Which["id"].Should().Be("4");
        var extension = pat["extension"].Should().BeOfType<List<Extension>>().Which.Should().ContainSingle().Subject;
        extension.Should().BeAssignableTo<IRO>().Which["url"].Should().Be("http://nu.nl");
    }

       [TestMethod]
        public void CanEnumerateFhirPrimitive()
        {
            IReadOnlyDictionary<string, object> b = new FhirBoolean(null);
            b.Count.Should().Be(0);
            b.Any().Should().Be(false);

            b = new FhirBoolean(true);
            b.Count.Should().Be(1);
            b.First().Should().BeEquivalentTo(KeyValuePair.Create("value", true));

            var nb = new FhirBoolean(true);
            nb.SetStringExtension("http://nu.nl", "then");
            nb.ElementId = "id1";
            b = nb;
            b.Count.Should().Be(3);
            b.Keys.Should().BeEquivalentTo("value", "id", "extension");
            b.Values.First().Should().BeOfType<bool>();
            b.Values.Skip(1).First().Should().BeOfType<string>();
            b.Values.Skip(2).First().Should().BeAssignableTo<IEnumerable<Extension>>();

            b.ToList()[2].Value.Should().BeAssignableTo<IEnumerable<Extension>>();

            b.TryGetValue("id", out var v).Should().BeTrue();
            v.Should().Be("id1");
            b.TryGetValue("idX", out _).Should().BeFalse();
        }

        [TestMethod]
        public void CanEnumerateCodedValue()
        {
            IReadOnlyDictionary<string, object> b = new Code<Narrative.NarrativeStatus>(Narrative.NarrativeStatus.Additional);
            b.Should().BeEquivalentTo(new[] { KeyValuePair.Create("value", Narrative.NarrativeStatus.Additional.GetLiteral()) });
        }

        [TestMethod]
        public void CanEnumerateNarrative()
        {
            IReadOnlyDictionary<string, object> b = new Narrative("<p>bla</p>");
            b.Count.Should().Be(2);
            b.Should().BeEquivalentTo(new[] {
                KeyValuePair.Create<string, object>("div", new XHtml("<p>bla</p>")),
                KeyValuePair.Create<string, object>("status", new Code<Narrative.NarrativeStatus>(Narrative.NarrativeStatus.Generated)) });
        }

        [TestMethod]
        public void CanEnumerateExtension()
        {
            // Explicitly test hand-written IROD implementation.
            IReadOnlyDictionary<string, object> b = new Extension("http://nu.nl", new FhirBoolean(true));
            b.Count.Should().Be(2);
            b.Should().BeEquivalentTo(new[] {
                KeyValuePair.Create<string, object>("url", "http://nu.nl"),
                KeyValuePair.Create<string, object>("value", new FhirBoolean(true)) });

            b.TryGetValue("valueString", out _).Should().BeFalse();
            b.TryGetValue("valueBoolean", out _).Should().BeFalse();
            b.TryGetValue("valueXXXXBoolean", out _).Should().BeFalse();
            b.TryGetValue("value", out var fb).Should().BeTrue();

            fb.Should().BeOfType<FhirBoolean>().Which.Value.Should().BeTrue();

            b["value"].Should().BeOfType<FhirBoolean>().Which.Value.Should().BeTrue();
        }

        [TestMethod]
        public void HandlesChoiceElements()
        {
            IReadOnlyDictionary<string, object> b = new Parameters.ParameterComponent() { Name = "test1", Value = new FhirBoolean(true) };

            b.TryGetValue("valueString", out _).Should().BeFalse();
            b.TryGetValue("valueBoolean", out _).Should().BeFalse();
            b.TryGetValue("value", out var fb).Should().BeTrue();
            b.TryGetValue("valueXXXXBoolean", out _).Should().BeFalse();
            fb.Should().BeOfType<FhirBoolean>().Which.Value.Should().BeTrue();

            b["value"].Should().BeOfType<FhirBoolean>().Which.Value.Should().BeTrue();
        }

        private OperationOutcome setupOutcome()
        {
            OperationOutcome oo = new OperationOutcome()
            {
                Id = "1",
                Meta = new Meta { Profile = new[] { "http://simplifier.net/profiles/x" }, VersionId = "2" }
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
                    Expression = new[] { "Patient.x" },
                    Severity = OperationOutcome.IssueSeverity.Error
                });
            oo.Id = "1";

            return oo;
        }

        [TestMethod]
        public void CanEnumerateResource()
        {
            IReadOnlyDictionary<string, object> b = setupOutcome();
            b.Count.Should().Be(3);
            b.TryGetValue("resourceType", out _).Should().BeFalse();  // we do not generate "resourceType" anymore

            // Check a backbone
            IReadOnlyDictionary<string, object> bb = b["issue"].Should().BeOfType<List<OperationOutcome.IssueComponent>>().Subject.Single();
            bb.Keys.Should().BeEquivalentTo("code", "details", "diagnostics", "expression", "severity");
        }

        [TestMethod]
        public void CanEnumerateContainedResources()
        {
            IReadOnlyDictionary<string, object> ps = new Parameters
            {
                { "aBool", new FhirBoolean(true) },
                { "aResource", new OperationOutcome() }
            };

            var paramList = ps["parameter"].Should().BeOfType<List<Parameters.ParameterComponent>>().Subject;
            paramList.Count.Should().Be(2);
            paramList[0].Name.Should().Be("aBool");
            paramList[1].Name.Should().Be("aResource");

            ps = paramList[1];
            ps.TryGetValue("value", out _).Should().BeFalse();
            ps.TryGetValue("resource", out var r).Should().BeTrue();

            var resource = ps["resource"].Should().BeAssignableTo<IReadOnlyDictionary<string, object>>().Subject;
            Assert.ThrowsException<KeyNotFoundException>(() => resource["resourceType"]);
        }
}