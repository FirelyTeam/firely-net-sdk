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
                new() { Name = "tablets-2", Value = new Quantity { Value = 2, Unit = "tablets" } },
                new() { Name = "capsules", Value = new Quantity { Value = 1, Unit = "capsules" } },
                new() { Name = "year-text", Value = new Quantity { Value = 1, Unit = "year" } },
                new() { Name = "years-text", Value = new Quantity { Value = 1, Unit = "years" } },
                new() { Name = "a-code", Value = new Quantity { Value = 1, Code = "a", System = "http://unitsofmeasure.org" } },
                new() { Name = "with-comparator", Value = new Quantity { Value = 1, Code = "mg", Comparator = Quantity.QuantityComparator.LessThan } },
                new() { Name = "no-value", Value = new Quantity { Unit = "mg" } },
                new() { Name = "absent-value", Value = new Quantity
                {
                    ValueElement = new FhirDecimal { Extension = [new Extension("http://hl7.org/fhir/StructureDefinition/data-absent-reason", new Code("unknown"))] },
                    Unit = "mg"
                } },
                new() { Name = "empty-code", Value = new Quantity { Value = 1, Unit = "tablets", Code = "" } },
                new() { Name = "mg-code-and-text", Value = new Quantity { Value = 1, Unit = "milligram", Code = "mg", System = "http://unitsofmeasure.org" } },
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
        [DataRow("tablets", "tablets-2", false, false)]
        [DataRow("tablets", "capsules", null, false)]
        [DataRow("year-text", "years-text", true, true)]
        [DataRow("year-text", "a-code", null, true)]
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
        [DataRow("tablets", "1", null, false)] // Java: false (complex vs primitive); here the integer converts to 1 '1'
        [DataRow("tablets", "1 '1'", null, false)]
        [DataRow("no-unit", "1", true, true)]
        [DataRow("no-unit", "1 '1'", true, true)]
        [DataRow("mg-text", "1 'mg'", null, false)]
        [DataRow("d-text", "1 day", null, false)]
        [DataRow("d-text", "1 'd'", null, false)]
        [DataRow("year-text", "1 year", true, true)]
        [DataRow("year-text", "1 'a'", null, true)]
        [DataRow("year-text", "1 month", null, false)]
        [DataRow("mg-code", "1 'mg'", true, true)]
        public void ComparesFhirQuantityToSystemValue(string left, string right, bool? equal, bool equivalent)
        {
            eval($"{p(left)} = {right}").Should().Be(equal);
            eval($"{p(left)} ~ {right}").Should().Be(equivalent);
            eval($"{right} = {p(left)}").Should().Be(equal);
            eval($"{right} ~ {p(left)}").Should().Be(equivalent);
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

        [TestMethod]
        public void SetFunctionsDoNotThrowOnUnconvertibleQuantities()
        {
            input().Select($"{p("with-comparator")} | {p("mg-code")}").Should().HaveCount(2);
            input().Select($"{p("no-value")} | {p("mg-code")}").Should().HaveCount(2);
            input().Select($"{p("absent-value")} | {p("mg-code")}").Should().HaveCount(2);
            input().Select($"({p("absent-value")} | {p("tablets")}).distinct()").Should().HaveCount(2);
            input().Select($"({p("with-comparator")} | {p("no-value")} | {p("tablets")}).distinct()").Should().HaveCount(3);
        }

        [TestMethod]
        public void SetFunctionsMatchFhirQuantityToLiteral()
        {
            input().Select($"{p("mg-code")}.intersect(1 'mg')").Should().HaveCount(1);
            input().Select($"{p("mg-code")} | 1 'mg'").Should().HaveCount(1);
            input().Scalar($"({p("mg-code")} | 1 'mg' | 1 'mg').count()").Should().Be(1);
            input().Select($"{p("tablets")} | 1 'tablets'").Should().HaveCount(2);
        }
    }
}
