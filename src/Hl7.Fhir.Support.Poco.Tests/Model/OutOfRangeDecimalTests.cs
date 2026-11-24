/*
 * Copyright (c) 2026, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.Support.Poco.Tests.Model;

/// <summary>
/// Decimals outside the range of a .NET decimal, e.g. 1e999999999 and 1e-999999999.
/// </summary>
[TestClass]
public class OutOfRangeDecimalTests
{
    private static P.Decimal parse(string literal) =>
        P.Decimal.TryParseExtended(literal, out var result) ? result : throw new FormatException(literal);

    [TestMethod]
    [DataRow("1e999999999", "1E+999999999")]
    [DataRow("-1e999999999", "-1E+999999999")]
    [DataRow("1e-999999999", "1E-999999999")]
    [DataRow("12.50e999999999", "1.25E+1000000000")]
    [DataRow("0.00120E-999999999", "1.2E-1000000002")]
    [DataRow("100000000000000000000000000000", "1E+29")]
    [DataRow("1e-29", "1E-29")]
    [DataRow("9223372036854775807e9223372036854775807", null)] // the exponent overflows a long: not parsed
    public void ParsesOutOfRangeLiterals(string literal, string? expected)
    {
        if (expected is null)
        {
            P.Decimal.TryParseExtended(literal, out _).Should().BeFalse();
            return;
        }

        var d = parse(literal);
        d.IsOutOfRange.Should().BeTrue();
        d.ToString().Should().Be(expected);
    }

    [TestMethod]
    [DataRow("0e999999999", "0")]
    [DataRow("0.00", "0.00")]
    [DataRow("1.5", "1.5")]
    [DataRow("1.5e-28", "0.0000000000000000000000000002")]
    [DataRow("79228162514264337593543950335", "79228162514264337593543950335")]
    public void ParsesInRangeLiteralsAsBefore(string literal, string expected)
    {
        var d = parse(literal);
        d.IsOutOfRange.Should().BeFalse();
        d.ToString().Should().Be(expected);
    }

    [TestMethod]
    [DataRow("+1e999999999")]
    [DataRow(".5")]
    [DataRow("1.")]
    [DataRow("1e")]
    [DataRow("abc")]
    public void RejectsMalformedLiterals(string literal) =>
        P.Decimal.TryParseExtended(literal, out _).Should().BeFalse();

    [TestMethod]
    public void PublicParseIsUnchanged()
    {
        P.Decimal.TryParse("1e999999999", out _).Should().BeFalse();
        P.Decimal.TryParse("1e-999999999", out var tiny).Should().BeTrue();
        tiny!.IsOutOfRange.Should().BeFalse();
    }

    [TestMethod]
    public void ValueOfAnOutOfRangeDecimal()
    {
        var huge = parse("1e999999999");
        var tiny = parse("1e-999999999");

        huge.Invoking(h => h.Value).Should().Throw<OverflowException>();
        tiny.Value.Should().Be(0m);
    }

    [TestMethod]
    public void ComparesOutOfRangeDecimals()
    {
        var huge = parse("1e999999999");
        var bigger = parse("2e999999999");
        var negHuge = parse("-1e999999999");
        var tiny = parse("1e-999999999");
        var one = new P.Decimal(1m);
        var max = new P.Decimal(decimal.MaxValue);

        (huge > max).Should().BeTrue();
        (negHuge < new P.Decimal(decimal.MinValue)).Should().BeTrue();
        (bigger > huge).Should().BeTrue();
        (parse("-2e999999999") < negHuge).Should().BeTrue();
        (parse("1e1000000000") > bigger).Should().BeTrue();
        (huge > tiny).Should().BeTrue();

        huge.Equals(one).Should().BeFalse();
        huge.Equals(parse("1.0e999999999")).Should().BeTrue();
        huge.GetHashCode().Should().Be(parse("1.0e999999999").GetHashCode());
        huge.Equals(bigger).Should().BeFalse();

        // Below the step size of 10^-8, so it compares as 0, like other tiny values.
        tiny.Equals(new P.Decimal(0m)).Should().BeTrue();
        tiny.CompareTo(new P.Decimal(0m)).Should().Be(0);
        tiny.GetHashCode().Should().Be(new P.Decimal(0m).GetHashCode());
        tiny.Equals(new P.Decimal(0m), P.DecimalComparison.RoundToSmallestScale).Should().BeTrue();
        tiny.Equals(new P.Decimal(0m), P.DecimalComparison.Strict).Should().BeFalse();
        (tiny < one).Should().BeTrue();
    }

    [TestMethod]
    public void QuantityKeepsAnOutOfRangeValue()
    {
        var huge = parse("1e999999999");
        P.Quantity q = huge;

        q.IsOutOfRange.Should().BeTrue();
        q.ToString().Should().Be("1E+999999999 '1'");
        q.Invoking(x => x.Value).Should().Throw<OverflowException>();
        (q > new P.Quantity(decimal.MaxValue)).Should().BeTrue();
    }

    [TestMethod]
    public void XmlParserKeepsOutOfRangeLiterals()
    {
        const string xml = """
            <Observation xmlns="http://hl7.org/fhir">
              <status value="final"/>
              <code><text value="test"/></code>
              <valueQuantity><value value="1e-999999999"/><unit value="kg"/><code value="kg"/></valueQuantity>
            </Observation>
            """;

        var obs = FhirXmlDeserializer.OSTRICH.Deserialize<Observation>(xml);
        var value = ((Hl7.Fhir.Model.Quantity)obs.Value!).ValueElement!;

        value.JsonValue.Should().Be("1e-999999999");
        ((P.IToSystemPrimitive)obs.Value!).TryConvertToSystemType(out var system).Should().BeTrue();
        system!.ToString().Should().Be("1E-999999999 'kg'");
        value.ToSystemDecimal().IsOutOfRange.Should().BeTrue();
    }

    [TestMethod]
    [DataRow("1e-999999999", "1e-999999999")]
    [DataRow("1e999999999", "1e999999999")]
    [DataRow("0", 0.0)]
    [DataRow("0e-999999999", 0.0)]
    public void JsonParserKeepsOutOfRangeLiterals(string literal, object expected)
    {
        var json = $$$"""{"resourceType":"Observation","status":"final","code":{"text":"test"},"valueQuantity":{"value":{{{literal}}},"unit":"kg"}}""";

        var obs = FhirJsonDeserializer.OSTRICH.Deserialize<Observation>(json);
        var value = ((Hl7.Fhir.Model.Quantity)obs.Value!).ValueElement!.JsonValue;

        if (expected is double dbl)
            value.Should().Be((decimal)dbl);
        else
            value.Should().Be(expected);
    }
}
