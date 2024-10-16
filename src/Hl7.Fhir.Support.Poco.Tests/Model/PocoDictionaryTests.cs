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
        var pat = new Patient();

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
        pat.AsReadOnlyDictionary().Should().BeEmpty();
    }

    [TestMethod]
    public void CanReadSpecialProperties()
    {
        var pat = new Patient()
        {
            Text = new Narrative { Div = "<div>hello</div>" },
            Active = true,
            Meta = new Meta { ElementId = "4" },
        };

        pat.AddExtension("http://nu.nl", new FhirBoolean(true));

        pat["active"].Should().BeOfType<FhirBoolean>().Which["value"].Should().Be(true);
        pat["text"].Should().BeOfType<Narrative>().Which["div"].Should().BeOfType<XHtml>().Which["value"].Should().Be("<div>hello</div>");
        pat["meta"].Should().BeOfType<Meta>().Which["id"].Should().Be("4");
        var extension = pat["extension"].Should().BeOfType<List<Extension>>().Subject;
        extension.Should().ContainSingle().Which["url"].Should().Be("http://nu.nl");
    }
}