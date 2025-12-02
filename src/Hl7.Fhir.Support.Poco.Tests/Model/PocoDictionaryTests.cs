/*
 * Copyright (c) 2025, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Hl7.Fhir.Support.Poco.Tests;

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

        // setting an existing property to a non-Base type should fail.
        Assert.Throws<ArgumentException>(() => pat["name"] = "John");

        // Setting it correctly should work
        pat["name"] = new List<HumanName> { new HumanName().WithGiven("John") };

        // Adding a non-existing property should work
        Assert.Throws<ArgumentException>(() => pat["weight"] = 80.0m);
        pat["weight"] = new FhirDecimal(80.0m);

        pat["name"].Should().BeOfType<List<HumanName>>();
        pat["weight"].Should().BeOfType<FhirDecimal>().Which.Value.Should().Be(80.0m);

        pat["name"] = null!;
        pat["weight"] = null!;
        pat.EnumerateElements().Should().BeEmpty();
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
            Which.JsonValue.Should().Be(true);
        patient["text"].Should().BeOfType<Narrative>()
            .Which["div"].Should().BeOfType<XHtml>()
            .Which.JsonValue.Should().Be("<div>hello</div>");
        patient["meta"].Should().BeOfType<Meta>()
            .Which["id"].Should().BeOfType<FhirString>()
            .Which.JsonValue.Should().Be("4");
        var extension = patient["extension"].Should().BeOfType<List<Extension>>().Which.Should().ContainSingle().Subject;
        extension.Should().BeAssignableTo<Base>()
            .Which["url"].Should().BeOfType<FhirUri>()
            .Which.JsonValue.Should().Be("http://nu.nl");
    }

    /// <summary>
    /// see <see cref="OverflowErrorTests"/> for more tests on getting invalid values and the associated errors
    /// </summary>
    [TestMethod]
    public void CanContainInvalidData()
    {
        var name = new HumanName();
        
        name["given"] = new FhirString("John");
        name["given"].Should().BeOfType<FhirString>().Which.JsonValue.Should().Be("John");
        name["given"] = new List<HumanName>([new HumanName()]);
        name["given"].Should().BeOfType<List<HumanName>>().Which.Should().ContainSingle().Which.Should().NotBeNull();
        name["given"] = new List<FhirString>([new FhirString("ji")]);
        name["given"].Should().BeOfType<List<FhirString>>().Which.Should().ContainSingle().Which.JsonValue.Should().Be("ji");
        name["given"] = null!;
        var act = () => name["given"];
        act.Should().Throw<KeyNotFoundException>();
    }
}