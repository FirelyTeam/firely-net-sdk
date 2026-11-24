/*
 * Copyright (c) 2026, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.FhirPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Hl7.Fhir.FhirPath.R4.Tests
{
    /// <summary>
    /// Equality (=) and equivalence (~) of FHIR Quantities that have unit text but no code.
    /// Follows the reference (Java) engine: such a quantity only compares to another one with the same unit text.
    /// See https://github.com/FirelyTeam/firely-net-sdk/issues/3692
    /// </summary>
    [TestClass]
    public class QuantityUnitComparisonTests
    {
        private static Parameters input() => new()
        {
            Parameter =
            [
                new() { Name = "no-unit", Value = new Quantity { Value = 1 } },
                new() { Name = "tablets", Value = new Quantity { Value = 1, Unit = "tablets" } },
                new() { Name = "mg-text", Value = new Quantity { Value = 1, Unit = "mg" } },
                new() { Name = "mg-code", Value = new Quantity { Value = 1, Code = "mg", System = "http://unitsofmeasure.org" } },
                new() { Name = "milligram-text", Value = new Quantity { Value = 1, Unit = "milligram" } },
                new() { Name = "d-text", Value = new Quantity { Value = 1, Unit = "d" } },
                new() { Name = "empty-code", Value = new Quantity { Value = 1, Unit = "tablets", Code = "" } },
                new() { Name = "mg-code-and-text", Value = new Quantity { Value = 1, Unit = "milligram", Code = "mg", System = "http://unitsofmeasure.org" } },
                new() { Name = "mg-text-dl", Value = new Quantity { Value = 1, Unit = "mg/dL" } },
                new() { Name = "mg-snomed", Value = new Quantity { Value = 1, Unit = "mg", Code = "258684004", System = "http://snomed.info/sct" } },
                new() { Name = "g-snomed", Value = new Quantity { Value = 1, Unit = "g", Code = "258682000", System = "http://snomed.info/sct" } },
                new() { Name = "mg-local", Value = new Quantity { Value = 1, Code = "mg", System = "http://example.org/units" } },
            ]
        };

        private static string p(string name) => $"Parameters.parameter.where(name = '{name}').value";

        // null means the result is the empty collection
        private static bool? eval(string expression) => (bool?)input().Scalar(expression);

        [TestMethod]
        [DataRow("no-unit", "no-unit", true, true)]
        [DataRow("tablets", "tablets", true, true)]
        [DataRow("tablets", "no-unit", null, false)]
        [DataRow("no-unit", "tablets", null, false)]
        [DataRow("mg-text", "mg-code", null, false)]
        [DataRow("mg-text", "mg-code-and-text", null, false)]
        [DataRow("mg-code", "milligram-text", null, false)]
        [DataRow("mg-code-and-text", "mg-code", true, true)]
        [DataRow("mg-code-and-text", "milligram-text", null, false)]
        [DataRow("empty-code", "tablets", true, true)]
        [DataRow("empty-code", "no-unit", null, false)]
        public void ComparesFhirQuantities(string left, string right, bool? equal, bool equivalent)
        {
            eval($"{p(left)} = {p(right)}").Should().Be(equal);
            eval($"{p(left)} ~ {p(right)}").Should().Be(equivalent);
            eval($"{p(left)} != {p(right)}").Should().Be(!equal);
            eval($"{p(left)} !~ {p(right)}").Should().Be(!equivalent);
        }

        [TestMethod]
        [DataRow("tablets", "1 'tablets'", null, false)]
        [DataRow("tablets", "1", null, false)]
        [DataRow("tablets", "1 '1'", null, false)]
        [DataRow("no-unit", "1", true, true)]
        [DataRow("no-unit", "1 '1'", true, true)]
        [DataRow("mg-text", "1 'mg'", null, false)]
        [DataRow("d-text", "1 day", null, false)]
        [DataRow("d-text", "1 'd'", null, false)]
        [DataRow("mg-code", "1 'mg'", true, true)]
        public void ComparesFhirQuantityToSystemValue(string left, string right, bool? equal, bool equivalent)
        {
            eval($"{p(left)} = {right}").Should().Be(equal);
            eval($"{p(left)} ~ {right}").Should().Be(equivalent);
            eval($"{right} = {p(left)}").Should().Be(equal);
            eval($"{right} ~ {p(left)}").Should().Be(equivalent);
        }

        [TestMethod]
        [DataRow("mg-text < 2 'mg'", true)]
        [DataRow("mg-text < 2000 'ug'", true)]
        [DataRow("mg-text > 0.5 'mg'", true)]
        [DataRow("mg-text-dl <= 10 'mg/L'", true)]
        [DataRow("mg-text-dl < 10 'mg/L'", false)]
        [DataRow("tablets < 2", null)]
        [DataRow("tablets < 2 'tablets'", true)]
        [DataRow("mg-text.comparable(1 'g')", true)]
        [DataRow("tablets.comparable(1 'g')", false)]
        public void OrdersFhirQuantitiesByUnitText(string expression, bool? expected)
        {
            var name = expression.Split(' ', '.')[0];
            eval(p(name) + expression.Substring(name.Length)).Should().Be(expected);
        }

        [TestMethod]
        public void ConvertsFhirQuantityWithUnitTextToString()
        {
            input().Scalar($"{p("mg-text")}.toString()").Should().Be("1 'mg'");
            input().Scalar($"{p("no-unit")}.toString()").Should().Be("1 '1'");
        }

        [TestMethod]
        [DataRow("mg-snomed", "g-snomed", null, false)]
        [DataRow("mg-snomed", "mg-snomed", true, true)]
        [DataRow("mg-snomed", "mg-code", null, false)]
        [DataRow("mg-local", "mg-code", null, false)]
        [DataRow("mg-local", "mg-local", true, true)]
        public void DoesNotConvertUnitsFromOtherSystems(string left, string right, bool? equal, bool equivalent)
        {
            eval($"{p(left)} = {p(right)}").Should().Be(equal);
            eval($"{p(left)} ~ {p(right)}").Should().Be(equivalent);
            eval($"{p(left)} < {p(right)}").Should().Be(equal is null ? null : false);
            eval($"{p(left)} = 1 'mg'").Should().BeNull();
        }

        [TestMethod]
        public void MembershipDoesNotMatchUnitTextQuantityToUnitless()
        {
            eval($"{p("tablets")} in {p("no-unit")}").Should().BeFalse();
            eval($"{p("no-unit")} contains {p("tablets")}").Should().BeFalse();
            eval($"{p("tablets")} in {p("tablets")}").Should().BeTrue();
        }

        [TestMethod]
        public void SetFunctionsTreatEqualQuantitiesAsOne()
        {
            // structurally different (unit text present on one side only), but equal by code
            var q = "Parameters.parameter.where(name = 'mg-code-and-text' or name = 'mg-code').value";
            input().Select($"{q}.distinct()").Should().HaveCount(1);
            input().Select($"{p("mg-code-and-text")} | {p("mg-code")}").Should().HaveCount(1);
            input().Select($"{p("mg-code-and-text")}.intersect({p("mg-code")})").Should().HaveCount(1);
            input().Select($"{p("mg-code-and-text")}.exclude({p("mg-code")})").Should().BeEmpty();
            input().Select($"{p("mg-text")} | {p("mg-code")}").Should().HaveCount(2);
            input().Select($"{p("tablets")} | {p("no-unit")}").Should().HaveCount(2);
        }
    }
}
