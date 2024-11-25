using FluentAssertions;
using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Tests.Model;

[TestClass]
public class PocoDictionaryTests
{
    [TestMethod]
    public void DynamicResourceAcceptsEverything()
    {
        var dr = new DynamicResource()
            {
                ["name"] = new FhirString("John"),
#pragma warning disable CA2244
                ["weight"] = new FhirDecimal(75.5m),
#pragma warning restore CA2244
                ["weight"] = new FhirDecimal(80.0m),
                ["someArray"] = new List<FhirString> { new("element") }
            };

        dr["name"].Should().BeOfType<FhirString>().Which.Value.Should().Be("John");
        dr["weight"].Should().BeOfType<FhirDecimal>().Which.Value.Should().Be(80.0m);
        dr["someArray"].Should().BeAssignableTo<IReadOnlyList<FhirString>>()
            .Which.Count.Should().Be(1);

        dr["name"] = null!;
        dr.TryGetValue("name", out _).Should().BeFalse();
    }

    [TestMethod]
    public void ResourceAcceptsOverflow()
    {
        var pat = new Patient();

        // setting an existing property to an incorrect type should fail.
        Assert.ThrowsException<InvalidCastException>(() => pat["name"] = "John");

        // Setting it correctly should work
        pat["name"] = new List<HumanName> { new HumanName().WithGiven("John") };

        // Adding a non-existing property should work
        Assert.ThrowsException<InvalidCastException>(() => pat["weight"] = 80.0m);
        pat["weight"] = new FhirDecimal(80.0m);

        pat["name"].Should().BeOfType<List<HumanName>>();
        pat["weight"].Should().BeOfType<FhirDecimal>().Which.Value.Should().Be(80.0m);

        pat["name"] = null!;
        pat["weight"] = null!;
        pat.GetElementPairs().Should().BeEmpty();
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

        patient["active"].Should().BeOfType<FhirBoolean>().
            Which.ObjectValue.Should().Be(true);
        patient["text"].Should().BeOfType<Narrative>()
            .Which["div"].Should().BeOfType<XHtml>()
            .Which.ObjectValue.Should().Be("<div>hello</div>");
        patient["meta"].Should().BeOfType<Meta>()
            .Which["id"].Should().BeOfType<FhirString>()
            .Which.ObjectValue.Should().Be("4");
        var extension = patient["extension"].Should().BeOfType<List<Extension>>().Which.Should().ContainSingle().Subject;
        extension.Should().BeAssignableTo<Base>()
            .Which["url"].Should().BeOfType<FhirUri>()
            .Which.ObjectValue.Should().Be("http://nu.nl");
    }
}