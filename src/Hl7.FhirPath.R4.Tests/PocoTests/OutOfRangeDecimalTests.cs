/*
 * Copyright (c) 2026, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Hl7.FhirPath.R4.Tests
{
    /// <summary>
    /// FHIRPath on decimals and quantities outside the range of a .NET decimal (e.g. 1e999999999). The conformance tests
    /// (group testLargeExponents) cover the values themselves; these cover them as the focus of other expressions.
    /// </summary>
    [TestClass]
    public class OutOfRangeDecimalTests
    {
        private static Parameters createParameters() => FhirXmlDeserializer.OSTRICH.Deserialize<Parameters>("""
            <Parameters xmlns="http://hl7.org/fhir">
              <parameter><name value="huge"/><valueDecimal value="1e999999999"/></parameter>
              <parameter><name value="tiny"/><valueDecimal value="1e-999999999"/></parameter>
              <parameter><name value="hugeQty"/><valueQuantity><value value="1e999999999"/><code value="kg"/></valueQuantity></parameter>
            </Parameters>
            """);

        [TestMethod]
        [DataRow("parameter[0].value.select(1 + 1)", 2L)]
        [DataRow("parameter[0].value.select(4 / 2)", 2.0)]
        [DataRow("parameter[0].value.select(-1)", -1L)]
        [DataRow("parameter[0].value.select('a' + 'x')", "ax")]
        [DataRow("parameter[0].value.exists($this > 1 + 1)", true)]
        [DataRow("parameter[0].value.all(1 + 1 = 2)", true)]
        [DataRow("parameter[1].value.select(2.0.round())", 2.0)]
        [DataRow("parameter[2].value.select(1 + 1)", 2L)]
        [DataRow("parameter[0].value.toDecimal().toString()", "1E+999999999")]
        [DataRow("parameter[0].value.toDecimal() > 1", true)]
        [DataRow("parameter[0].value.convertsToDecimal()", true)]
        [DataRow("parameter[0].value = parameter[0].value", true)]
        [DataRow("parameter[1].value = 0", true)]
        public void ArithmeticOnOtherValuesIsUnaffected(string expression, object expected)
        {
            var result = createParameters().Scalar(expression);
            if (expected is double d)
                result.Should().Be((decimal)d);
            else
                result.Should().Be(expected);
        }

        [TestMethod]
        [DataRow("parameter[0].value.select($this + 1)")]
        [DataRow("parameter[0].value.select(1 + $this)")]
        [DataRow("parameter[0].value.select(-$this)")]
        [DataRow("parameter[0].value.abs()")]
        [DataRow("parameter[0].value.sqrt()")]
        [DataRow("parameter[1].value.round()")]
        [DataRow("parameter[2].value.select(-$this)")]
        [DataRow("@2020-01-01T10:00:00Z + parameter[2].value")]
        public void ArithmeticOnOutOfRangeValuesIsEmpty(string expression) =>
            createParameters().Select(expression).Should().BeEmpty();

        [TestMethod]
        public void TypedElementValueIsTheLiteral()
        {
            var huge = createParameters().ToTypedElement(ModelInfo.ModelInspector)
                .Children("parameter").First().Children("value").Single();

            huge.Value.Should().Be("1e999999999");
        }
    }
}
