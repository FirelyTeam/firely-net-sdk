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

namespace Hl7.Fhir.Support.Tests.Serialization
{
    [TestClass]
    public class TestDictionaryImplementation
    {
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
}