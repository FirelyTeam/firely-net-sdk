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
                new() { Name = "mg-text-dl", Value = new Quantity { Value = 1, Unit = "mg/dL" } },
                new() { Name = "mg-dl-code-and-text", Value = new Quantity { Value = 2, Unit = "mg/dL", Code = "mg/dL", System = "http://unitsofmeasure.org" } },
                new() { Name = "mg-snomed", Value = new Quantity { Value = 1, Unit = "mg", Code = "258684004", System = "http://snomed.info/sct" } },
                new() { Name = "g-snomed", Value = new Quantity { Value = 1, Unit = "g", Code = "258682000", System = "http://snomed.info/sct" } },
                new() { Name = "mg-local", Value = new Quantity { Value = 1, Code = "mg", System = "http://example.org/units" } },
                new() { Name = "mg-no-system", Value = new Quantity { Value = 1, Code = "mg" } },
                new() { Name = "days-text", Value = new Quantity { Value = 1, Unit = "days" } },
                new() { Name = "day-text", Value = new Quantity { Value = 1, Unit = "day" } },
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

        // Ordering follows the reference (Java) engine: a quantity without a code is only ordered against
        // a quantity with the same unit text, and its unit text is never converted as a UCUM unit.
        [TestMethod]
        [DataRow("mg-text-dl < mg-dl-code-and-text", true)]
        [DataRow("mg-dl-code-and-text > mg-text-dl", true)]
        [DataRow("mg-text-dl >= mg-dl-code-and-text", false)]
        [DataRow("tablets <= tablets", true)]
        [DataRow("tablets < tablets", false)]
        [DataRow("mg-text < mg-code", null)]
        [DataRow("mg-text < 2 'mg'", null)]
        [DataRow("mg-text < 2000 'ug'", null)]
        [DataRow("tablets < 2", null)]
        [DataRow("tablets < no-unit", null)]
        [DataRow("no-unit < 2", true)]
        [DataRow("mg-code < 2000 'ug'", true)]
        [DataRow("mg-code > 999 'ug'", true)]
        [DataRow("days-text < 2 days", true)]
        [DataRow("days-text <= 1 day", true)]
        [DataRow("days-text < 2 'd'", null)]
        [DataRow("no-unit < 200 '%'", null)]
        [DataRow("(-tablets) < 5", null)]
        [DataRow("tablets.abs() < 5", null)]
        [DataRow("tablets.lowBoundary() < 2", null)]
        [DataRow("mg-local.abs() < 2000 'ug'", null)]
        [DataRow("(-mg-code) < 2000 'ug'", true)]
        public void OrdersFhirQuantities(string expression, bool? expected)
        {
            eval(expand(expression)).Should().Be(expected);
        }

        // A quantity keeps its unit information through abs(), unary minus and the boundary functions, so equality
        // gives the same result as without them
        [TestMethod]
        [DataRow("mg-text-dl", "mg-dl-code-and-text", null, false)]
        [DataRow("mg-text-dl", "mg-text-dl", true, true)]
        [DataRow("tablets", "no-unit", null, false)]
        [DataRow("mg-code", "mg-code", true, true)]
        public void EqualityAfterAbs(string left, string right, bool? equal, bool equivalent)
        {
            eval($"{p(left)}.abs() = {p(right)}").Should().Be(equal);
            eval($"{p(left)}.abs() ~ {p(right)}").Should().Be(equivalent);
            eval($"{p(right)} = {p(left)}.abs()").Should().Be(equal);
            eval($"{p(right)} ~ {p(left)}.abs()").Should().Be(equivalent);
        }

        [TestMethod]
        public void ComparableNeedsCodes()
        {
            eval($"{p("mg-code")}.comparable(1 'g')").Should().BeTrue();
            eval($"{p("mg-code")}.comparable(1 'm')").Should().BeFalse();
            eval($"{p("mg-text")}.comparable(1 'g')").Should().BeFalse();
            eval($"{p("tablets")}.comparable({p("tablets")})").Should().BeFalse();
            eval($"{p("no-unit")}.comparable({p("no-unit")})").Should().BeFalse();
            eval($"{p("mg-local")}.comparable({p("mg-code")})").Should().BeFalse();
            eval($"{p("mg-local")}.comparable({p("mg-local")})").Should().BeTrue();
            eval($"{p("mg-no-system")}.comparable({p("mg-no-system")})").Should().BeFalse();
            eval($"(-{p("mg-local")}).comparable(1 'g')").Should().BeFalse();
            eval("1 hour.comparable(1 'min')").Should().BeTrue();
            eval("1 year.comparable(1 year)").Should().BeFalse();
            eval("1 'Ym'.comparable(1 'ym')").Should().BeTrue();

            // An empty argument or focus is not comparable
            eval($"{p("mg-code")}.comparable({p("does-not-exist")})").Should().BeFalse();
            eval($"{p("does-not-exist")}.comparable(1 'g')").Should().BeFalse();
        }

        // Units are only converted when the FHIR system is UCUM. Codes are compared regardless of the system.
        [TestMethod]
        [DataRow("mg-snomed", "g-snomed", null, false)]
        [DataRow("mg-snomed", "mg-snomed", true, true)]
        [DataRow("mg-snomed", "mg-code", null, false)]
        [DataRow("mg-local", "mg-code", true, true)]
        [DataRow("mg-local", "mg-local", true, true)]
        public void DoesNotConvertUnitsFromOtherSystems(string left, string right, bool? equal, bool equivalent)
        {
            eval($"{p(left)} = {p(right)}").Should().Be(equal);
            eval($"{p(left)} ~ {p(right)}").Should().Be(equivalent);
            eval($"{p(left)} < {p(right)}").Should().Be(equal is null ? null : false);
        }

        [TestMethod]
        public void ConvertsOnlyUcumCodes()
        {
            eval($"{p("mg-code")} = 1000 'ug'").Should().BeTrue();
            eval($"{p("mg-local")} = 1000 'ug'").Should().BeNull();
            eval($"{p("mg-local")} ~ 1000 'ug'").Should().BeFalse();
            eval($"{p("mg-local")} < 2000 'ug'").Should().BeNull();
        }

        // As in the reference engine, the plural and singular calendar words are the same unit text only when ordering
        [TestMethod]
        public void PluralCalendarUnitTextOnlyMatchesWhenOrdering()
        {
            eval($"{p("days-text")} <= {p("day-text")}").Should().BeTrue();
            eval($"{p("days-text")} < {p("day-text")}").Should().BeFalse();
            eval($"{p("days-text")} = {p("day-text")}").Should().BeNull();
            eval($"{p("days-text")} ~ {p("day-text")}").Should().BeFalse();
            eval($"{p("days-text")}.abs() = {p("day-text")}").Should().BeNull();
            input().Scalar($"({p("days-text")} | {p("day-text")}).count()").Should().Be(2);
        }

        [TestMethod]
        public void UnionAndDistinctUseFhirPathEquality()
        {
            input().Scalar($"({p("mg-code")} | 1000 'ug').count()").Should().Be(1);
            input().Scalar("(4 'g' | 4000 'mg' | 4 'kg').count()").Should().Be(2);
        }

        private static readonly string[] NAMES = input().Parameter.Select(pc => pc.Name).OrderByDescending(n => n.Length).ToArray();

        // Replaces the parameter names in an expression with the path to their value
        private static string expand(string expression) =>
            string.Join(" ", expression.Split(' ').Select(token =>
            {
                var prefix = new string(token.TakeWhile(c => c is '(' or '-').ToArray());
                var rest = token.Substring(prefix.Length);
                return NAMES.FirstOrDefault(n => rest == n || rest.StartsWith(n + ".") || rest.StartsWith(n + ")")) is { } name
                    ? prefix + p(name) + rest.Substring(name.Length)
                    : token;
            }));

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
