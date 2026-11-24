/*
 * Copyright (c) 2026, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.FhirPath;
using Hl7.FhirPath.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.Support.Poco.Tests.Model;

/// <summary>
/// Decimals written with an exponent, like 9.5e2, are precise to the digits of their mantissa (two significant digits
/// here), which a .NET decimal (950) cannot express. The FHIRPath boundary functions need that precision.
/// </summary>
[TestClass]
public class ExponentDecimalPrecisionTests
{
    private static readonly FhirPathCompiler COMPILER = new(new SymbolTable().AddStandardFP().AddFhirExtensions());

    [TestMethod]
    [DataRow("9.5e2", 950, -1)]
    [DataRow("9.5E2", 950, -1)]
    [DataRow("-9.5e+2", -950, -1)]
    [DataRow("1e3", 1000, -3)]
    [DataRow("95e1", 950, -1)]
    [DataRow("0.95e3", 950, -1)]
    // zero has no precision to keep, whatever its exponent: its boundaries are those of 0, as in the reference implementation
    [DataRow("0e2", 0, 0)]
    [DataRow("-0.0e2147483647", 0, 0)]
    [DataRow("9.50e2", 950, 0)]
    [DataRow("9.500e2", 950, 1)]
    [DataRow("1.5e-3", 0.0015, 4)]
    [DataRow("950", 950, 0)]
    [DataRow("1.50", 1.5, 2)]
    public void ParsingKeepsThePrecisionOfTheMantissa(string literal, double value, int precisionScale)
    {
        var parsed = P.Decimal.Parse(literal);

        parsed.Value.Should().Be((decimal)value);
        parsed.PrecisionScale.Should().Be(precisionScale);
        parsed.Should().Be(new P.Decimal((decimal)value), "the precision does not change the value");
        parsed.ToString().Should().Be(new P.Decimal(parsed.Value).ToString(), "the precision does not change the string form");

        P.Decimal.TryParseExtended(literal, out var extended).Should().BeTrue();
        extended!.PrecisionScale.Should().Be(precisionScale);
    }

    [TestMethod]
    public void XmlParserKeepsTheLiteral()
    {
        var obs = FhirXmlDeserializer.OSTRICH.Deserialize<Observation>(observationXml("9.5e2"));
        var value = ((Quantity)obs.Value!).ValueElement!;

        value.Value.Should().Be(950m);
        value.ExponentLiteral.Should().Be("9.5e2");
        value.ToSystemDecimal().PrecisionScale.Should().Be(-1);
        ((Quantity)obs.Value!).ToSystemQuantity().DecimalValue.PrecisionScale.Should().Be(-1);
    }

    [TestMethod]
    public void JsonParserKeepsTheLiteral()
    {
        var obs = FhirJsonDeserializer.OSTRICH.Deserialize<Observation>(observationJson("-9.5E+2"));
        var value = ((Quantity)obs.Value!).ValueElement!;

        value.Value.Should().Be(-950m);
        value.ExponentLiteral.Should().Be("-9.5E+2");
        value.ToSystemDecimal().PrecisionScale.Should().Be(-1);
    }

    [TestMethod]
    [DataRow("950")]
    [DataRow("1.5e-3")] // a .NET decimal keeps this precision (0.0015)
    [DataRow("9.50e2")] // as precise as 950
    [DataRow("0e1000000")] // zero
    public void ParsersOnlyKeepALiteralThatAddsPrecision(string literal)
    {
        var fromXml = FhirXmlDeserializer.OSTRICH.Deserialize<Observation>(observationXml(literal));
        var fromJson = FhirJsonDeserializer.OSTRICH.Deserialize<Observation>(observationJson(literal));

        ((Quantity)fromXml.Value!).ValueElement!.ExponentLiteral.Should().BeNull();
        ((Quantity)fromJson.Value!).ValueElement!.ExponentLiteral.Should().BeNull();
    }

    [TestMethod]
    public void TheLiteralFollowsTheValue()
    {
        var obs = FhirXmlDeserializer.OSTRICH.Deserialize<Observation>(observationXml("9.5e2"));
        var value = ((Quantity)obs.Value!).ValueElement!;

        var copy = (FhirDecimal)value.DeepCopy();
        copy.ExponentLiteral.Should().Be("9.5e2");
        ((Quantity)((Observation)obs.DeepCopy()).Value!).ValueElement!.ExponentLiteral.Should().Be("9.5e2");

        value.Value = 950m;
        value.ExponentLiteral.Should().BeNull("a new value has the precision it shows");
        copy.ExponentLiteral.Should().Be("9.5e2");

        copy.JsonValue = 951m;
        copy.ExponentLiteral.Should().BeNull();
        copy.ToSystemDecimal().PrecisionScale.Should().Be(0);
    }

    [TestMethod]
    public void ALiteralThatDoesNotMatchTheValueIsNotKept()
    {
        var value = new FhirDecimal(950m);
        value.KeepParsedLiteral("9.6e2");
        value.ExponentLiteral.Should().BeNull();

        value = new FhirDecimal();
        value.KeepParsedLiteral("9.5e2");
        value.ExponentLiteral.Should().BeNull();
    }

    [TestMethod]
    [DataRow("value.lowBoundary()", "945.00000000 'kg'")]
    [DataRow("value.highBoundary(0)", "955 'kg'")]
    [DataRow("value.value.lowBoundary(0)", "945")]
    [DataRow("value.value.highBoundary()", "955.00000000")]
    [DataRow("value.value.highBoundary(1)", "955.0")]
    // other functions see the value, 950
    [DataRow("value.value + 1", "951")]
    [DataRow("value.value.toString()", "950")]
    [DataRow("value.value = 950", "true")]
    // functions that return the same node keep the precision
    [DataRow("value.value.first().lowBoundary(0)", "945")]
    [DataRow("iif(true, value.value).highBoundary(0)", "955")]
    public void BoundariesUseThePrecisionOfTheLiteral(string expression, string expected)
    {
        foreach (var obs in parseAllWays("9.5e2"))
        {
            var result = COMPILER.Compile(expression)(obs.ToPocoNode(), new FhirEvaluationContext()).ToList();

            result.Should().ContainSingle();
            show(result[0]).Should().Be(expected);
        }
    }

    [TestMethod]
    [DataRow("-9.5E+2", "-955", "-945")]
    [DataRow("1.5E2", "145", "155")]
    [DataRow("95e1", "945", "955")]
    [DataRow("0.95e3", "945", "955")]
    [DataRow("1e3", "500", "1500")]
    [DataRow("1.5e-3", "0", "0")]
    [DataRow("0e2", "0", "1")]
    [DataRow("0e2147483647", "0", "1")]
    [DataRow("-0.0e5", "0", "1")]
    public void BoundariesOfLiterals(string literal, string low, string high)
    {
        foreach (var obs in parseAllWays(literal))
        {
            show(COMPILER.Compile("value.value.lowBoundary(0)")(obs.ToPocoNode(), new FhirEvaluationContext()).Single()).Should().Be(low);
            show(COMPILER.Compile("value.value.highBoundary(0)")(obs.ToPocoNode(), new FhirEvaluationContext()).Single()).Should().Be(high);
        }
    }

    [TestMethod]
    public void BoundariesOfAValueSetInCodeUseItsDigits()
    {
        var obs = new Observation { Value = new Quantity(950m, "kg") };

        var result = COMPILER.Compile("value.value.lowBoundary(1)")(obs.ToPocoNode(), new FhirEvaluationContext()).Single();

        show(result).Should().Be("949.5");
    }

    /// <summary>
    /// The observation parsed by the XML and JSON parsers, and through the XML ISourceNode. (The JSON ISourceNode,
    /// FhirJsonNode, reads numbers as .NET decimals, so it loses the literal.)
    /// </summary>
    private static Observation[] parseAllWays(string value) =>
    [
        FhirXmlDeserializer.OSTRICH.Deserialize<Observation>(observationXml(value)),
        FhirJsonDeserializer.OSTRICH.Deserialize<Observation>(observationJson(value)),
        FhirXmlNode.Parse(observationXml(value)).ToPoco<Observation>(ModelInfo.ModelInspector)
    ];

    private static string? show(PocoNode node) => P.Any.Convert(((Hl7.Fhir.ElementModel.ITypedElement)node).Value)?.ToString();

    private static string observationXml(string value) => $"""
        <Observation xmlns="http://hl7.org/fhir">
          <status value="final"/>
          <code><text value="test"/></code>
          <valueQuantity><value value="{value}"/><unit value="kg"/><system value="http://unitsofmeasure.org"/><code value="kg"/></valueQuantity>
        </Observation>
        """;

    private static string observationJson(string value) =>
        $$$"""{"resourceType":"Observation","status":"final","code":{"text":"test"},"valueQuantity":{"value":{{{value}}},"unit":"kg","system":"http://unitsofmeasure.org","code":"kg"}}""";
}
