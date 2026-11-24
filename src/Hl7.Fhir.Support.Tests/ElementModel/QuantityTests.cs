/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using FluentAssertions;
using Hl7.Fhir.ElementModel.Types;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.ElementModel.Tests
{
    [TestClass]
    public class QuantityTests
    {
        [TestMethod]
        public void QuantityParsing()
        {
            Assert.AreEqual(new Quantity(75.5m, "kg"), Quantity.Parse("75.5 'kg'"));
            Assert.AreEqual(new Quantity(75.5m, "kg"), Quantity.Parse("75.5'kg'"));
            Assert.AreEqual(new Quantity(75m, "kg"), Quantity.Parse("75 'kg'"));
            Assert.AreEqual(new Quantity(40m, "wk"), Quantity.Parse("40 'wk'"));
            Assert.AreEqual(new Quantity(40m, "wk"), Quantity.Parse("40 weeks"));
            Assert.AreEqual(Quantity.ForCalendarDuration(2m, "year"), Quantity.Parse("2 year"));
            Assert.AreEqual(Quantity.ForCalendarDuration(6m, "month"), Quantity.Parse("6 months"));
            Assert.AreEqual(new Quantity(40.0m), Quantity.Parse("40.0"));
            Assert.AreEqual(new Quantity(1m), Quantity.Parse("1 '1'"));
            Assert.AreEqual(new Quantity(1m, "m/s"), Quantity.Parse("1 'm/s'"));

            reject("40,5 weeks");
            reject("40 weks");
            reject("40 decennia");
            reject("ab kg");
            reject("75 'kg");
            reject("75 kg");
            reject("'kg'");

            // A trailing line break must not be accepted (see #3659)
            Assert.AreEqual(new Quantity(1m, "mg"), Quantity.Parse("1 'mg'"));
            reject("1 'mg'\n");
            reject("40 weeks\n");

            // ... also not when there is no unit, where the whitespace before the (absent) unit used to swallow it
            Assert.AreEqual(new Quantity(40.0m), Quantity.Parse("40.0"));
            reject("40.0\n");
            reject("40.0\r\n");
            reject("40.0 \n");
            reject("40.0\n ");
            reject("1 'mg'\r\n");
        }

        void reject(string testValue)
        {
            Assert.IsFalse(Quantity.TryParse(testValue, out _));
        }

        [TestMethod]
        public void QuantityFormatting()
        {
            Assert.AreEqual("75.6 'kg'", new Quantity(75.6m, "kg").ToString());
        }

        [TestMethod]
        public void QuantityConstructor()
        {
            var newq = new Quantity(3.14m, "kg");
            Assert.AreEqual("kg", newq.Unit);
            Assert.AreEqual(3.14m, newq.Value);
        }

        [TestMethod]
        public void QuantityEquals()
        {
            var newq = new Quantity(3.14m, "kg");

            Assert.AreEqual(newq, new Quantity(3.14m, "kg"));
            Assert.AreNotEqual(newq, new Quantity(3.15m, "kg"));
        }

        [TestMethod]
        public void QuantityComparison()
        {
            var smaller = new Quantity(3.14m, "kg");
            var bigger = new Quantity(4.0m, "kg");

            Assert.IsTrue(smaller < bigger);
#pragma warning disable CS1718 // Comparison made to same variable
            // ReSharper disable once EqualExpressionComparison
            Assert.IsTrue(smaller <= smaller);
#pragma warning restore CS1718 // Comparison made to same variable
            Assert.IsTrue(bigger >= smaller);

            Assert.AreEqual(-1, smaller.CompareTo(bigger));
            Assert.AreEqual(1, bigger.CompareTo(smaller));
            Assert.AreEqual(0, smaller.CompareTo(smaller));
        }


        [TestMethod]
        public void IncomparableUnitsNotSupported()
        {
            var a = new Quantity(3.14m, "kg");
            var b = new Quantity(30.5m, "m");
            
            Func<object> func = () => a < b;
            func.Should().Throw<InvalidOperationException>();
            func = () => a == b;
            func.Should().Throw<InvalidOperationException>();

            // Units that are not valid UCUM cannot be compared either
            a.TryCompareTo(new Quantity(3.14m, "tablets"), out _).Should().BeFalse();
            new Quantity(1m, "tablets").TryCompareTo(new Quantity(1m, "tablets"), out var same).Should().BeTrue();
            same.Should().Be(0);
        }

        [TestMethod]
        public void ComparesDifferentUcumUnits()
        {
            var kg = new Quantity(3.14m, "kg");

            kg.Should().Be(new Quantity(3140m, "g"));
            kg.Should().NotBe(new Quantity(3141m, "g"));
            (kg > new Quantity(30.5m, "g")).Should().BeTrue();
            (kg < new Quantity(3141m, "g")).Should().BeTrue();
            (new Quantity(1m, "[in_i]") == new Quantity(2.54m, "cm")).Should().BeTrue();
            (new Quantity(1m, "mg/dL") == new Quantity(10m, "mg/L")).Should().BeTrue();
            (new Quantity(50m, "%") == new Quantity(0.5m)).Should().BeTrue();
            (new Quantity(1m, "h") == new Quantity(60m, "min")).Should().BeTrue();

            // Small values must not be lost by rounding in the (larger) canonical unit
            new Quantity(1m, "ng").Should().NotBe(new Quantity(0.002m, "ug"));
            (new Quantity(1m, "ng") < new Quantity(0.002m, "ug")).Should().BeTrue();
            new Quantity(1m, "pg").Should().Be(new Quantity(0.001m, "ng"));

            // Very different scales: the conversion to the smaller unit overflows, so the other way around is used
            (new Quantity(1m, "Ym") > new Quantity(1m, "ym")).Should().BeTrue();

            // UCUM annotations are dimensionless
            (new Quantity(1m, "{score}") == new Quantity(1m)).Should().BeTrue();

            // Equal quantities must have the same hash code, also when their units differ
            kg.GetHashCode().Should().Be(new Quantity(3140m, "g").GetHashCode());
            new Quantity(1m, "d").GetHashCode().Should().Be(Quantity.ForCalendarDuration(1m, "day").GetHashCode());
            new Quantity(1000.000000004m, "kg").GetHashCode().Should().Be(new Quantity(1000.000000001m, "kg").GetHashCode());
            new Quantity(1m, "g", QuantityUnitSystem.CalendarDuration).Invoking(q => q.GetHashCode()).Should().NotThrow();
        }

        [TestMethod]
        public void IncomparableUcumUnits()
        {
            // Units that the UCUM library cannot convert linearly, or that have no canonical form, cannot be compared
            new Quantity(37m, "Cel").TryCompareTo(new Quantity(310m, "K"), out _).Should().BeFalse();
            new Quantity(1m, "g").TryCompareTo(new Quantity(1m, "[s]"), out _).Should().BeFalse();

            // Calendar durations are not converted to UCUM durations with a different unit
            Quantity.ForCalendarDuration(7m, "day").TryCompareTo(new Quantity(1m, "wk"), out _).Should().BeFalse();
            Quantity.ForCalendarDuration(1m, "day").TryCompareTo(new Quantity(1m, "d"), out var r).Should().BeTrue();
            r.Should().Be(0);
        }

        [TestMethod]
        public void EquivalenceRoundsToLeastPreciseValue()
        {
            ICqlEquatable fourGrams = new Quantity(4m, "g");

            fourGrams.IsEquivalentTo(new Quantity(4000m, "mg")).Should().BeTrue();
            fourGrams.IsEquivalentTo(new Quantity(4040m, "mg")).Should().BeTrue();
            fourGrams.IsEquivalentTo(new Quantity(4.4m, "g")).Should().BeTrue();
            fourGrams.IsEquivalentTo(new Quantity(4600m, "mg")).Should().BeFalse();
            ((ICqlEquatable)new Quantity(4.0m, "g")).IsEquivalentTo(new Quantity(4040m, "mg")).Should().BeTrue();
            ((ICqlEquatable)new Quantity(4.01m, "g")).IsEquivalentTo(new Quantity(4040m, "mg")).Should().BeFalse();
            ((ICqlEquatable)new Quantity(1m, "cm")).IsEquivalentTo(new Quantity(1m, "mm")).Should().BeFalse();
            fourGrams.IsEquivalentTo(new Quantity(4m, "m")).Should().BeFalse();

            // The precision is that of the values as written: 1 'kg' is precise to the kilogram, as is 1400 'g' after rounding
            ((ICqlEquatable)new Quantity(1m, "kg")).IsEquivalentTo(new Quantity(1400m, "g")).Should().BeTrue();
            ((ICqlEquatable)new Quantity(1400m, "g")).IsEquivalentTo(new Quantity(1m, "kg")).Should().BeTrue();
            ((ICqlEquatable)new Quantity(1m, "kg")).IsEquivalentTo(new Quantity(1.4m, "kg")).Should().BeTrue();
            ((ICqlEquatable)new Quantity(1.0m, "kg")).IsEquivalentTo(new Quantity(1000.4m, "g")).Should().BeTrue();
            ((ICqlEquatable)new Quantity(1.000m, "kg")).IsEquivalentTo(new Quantity(1000.4m, "g")).Should().BeTrue();
            ((ICqlEquatable)new Quantity(1.001m, "kg")).IsEquivalentTo(new Quantity(1000.4m, "g")).Should().BeFalse();

            // Values with the same unit are not rounded in the (larger) canonical unit
            ((ICqlEquatable)new Quantity(4000m, "mg")).IsEquivalentTo(new Quantity(4400m, "mg")).Should().BeFalse();
            ((ICqlEquatable)new Quantity(100m, "mg")).IsEquivalentTo(new Quantity(149m, "mg")).Should().BeFalse();
            ((ICqlEquatable)new Quantity(1m, "ng")).IsEquivalentTo(new Quantity(2m, "ng")).Should().BeFalse();
            ((ICqlEquatable)new Quantity(1m, "ng")).IsEquivalentTo(new Quantity(0.001m, "ug")).Should().BeTrue();
            ((ICqlEquatable)new Quantity(1m, "[in_i]")).IsEquivalentTo(new Quantity(3m, "cm")).Should().BeTrue();
            ((ICqlEquatable)new Quantity(1.01m, "[in_i]")).IsEquivalentTo(new Quantity(2.5m, "cm")).Should().BeFalse();
        }

        public enum Comparison
        {
            LessThan,
            Equals,
            GreaterThan
        }

        [TestMethod]
        [DataRow("2.0 'cm'", "2.0 'm'", "0.040 'm2'")]
        [DataRow("25 'm'", "20 'm'", "500 'm2'")]
        [DataRow("2.0 'cm'", "9 'kg'", "180 'g.m'")]
        [DataRow("2 'kg'", "3 '1'", "6 'kg'")]
        [DataRow("3 '1'", "2 'kg'", "6 'kg'")]
        [DataRow("5 'mg'", "2 '1'", "10 'mg'")]
        [DataRow("37 'Cel'", "2 '1'", "74 'Cel'")]
        [DataRow("2 'kg'", "3 'm'", "6000 'g.m'")]
        [DataRow("-2 'm'", "3 'm'", "-6 'm2'")]
        [DataRow("3 '[in_i]'", "2 '[in_i]'", "0.00387096 'm2'")]
        [DataRow("2 '{tbl}'", "3 '{tbl}'", "6 '1'")]
        [DataRow("2 '[IU]'", "3 'mL'", null)]
        [DataRow("79228162514264337593543950335 'km'", "79228162514264337593543950335 'km'", null)]
        [DataRow("3 '1'", "2 '1'", "6 '1'")]
        [DataRow("2 '{tbl}'", "3 'g'", "6 'g'")]
        [DataRow("2 'm'", "3 seconds", "6 'm.s'")]
        [DataRow("2 'm'", "0 'm'", "0 'm2'")]
        [DataRow("2 'Cel'", "3 'm'", null)]
        [DataRow("2 'tablets'", "3 'm'", null)]
        [DataRow("2 'm'", "1 year", null)]
        [DataRow("1 year", "2 '1'", "2 year")]
        public void QuantityMultiplication(string left, string right, string? expected) =>
            checkArithmetic(left, right, expected, Quantity.TryMultiply);

        [TestMethod]
        [DataRow("4.0 'g'", "2.0 'm'", "2 'g.m-1'")]
        [DataRow("1.0 'm'", "1.0 'm'", "1 '1'")]
        [DataRow("12 'cm2'", "3 'cm'", "0.04 'm'")]
        [DataRow("14.4 'km'", "2.0 'h'", "2 'm.s-1'")]
        [DataRow("2 'kg'", "4 'g'", "500 '1'")]
        [DataRow("6 'kg'", "3 '1'", "2 'kg'")]
        [DataRow("1 week", "1 day", "7 '1'")]
        [DataRow("-6 'm'", "3 's'", "-2 'm.s-1'")]
        [DataRow("2 '[IU]'", "1 'L'", null)]
        [DataRow("2 '[iU]/mL'", "1 'L'", null)]
        [DataRow("6 '1'", "3 'g'", "2 'g-1'")]
        [DataRow("6 'm'", "3 seconds", "2 'm.s-1'")]
        [DataRow("3 'm'", "0 'cm'", null)]
        [DataRow("3 'm'", "0 '1'", null)]
        [DataRow("3 'tablets'", "1 'm'", null)]
        [DataRow("3 'm'", "1 month", null)]
        public void QuantityDivision(string left, string right, string? expected) =>
            checkArithmetic(left, right, expected, Quantity.TryDivide);

        [TestMethod]
        [DataRow("m/")]
        [DataRow("kg.")]
        [DataRow("//")]
        [DataRow("[")]
        [DataRow("{")]
        [DataRow("m)")]
        [DataRow("(m")]
        [DataRow("m-")]
        [DataRow("m2.3")]
        [DataRow("")]
        public void QuantityArithmeticWithMalformedUnitIsNotPossible(string unit)
        {
            var malformed = new Quantity(2m, unit);
            var valid = new Quantity(3m, "m");

            Quantity.TryMultiply(malformed, valid, out var result).Should().BeFalse();
            result.Should().BeNull();
            Quantity.TryMultiply(valid, malformed, out result).Should().BeFalse();
            result.Should().BeNull();
            Quantity.TryDivide(malformed, valid, out result).Should().BeFalse();
            result.Should().BeNull();
            Quantity.TryDivide(valid, malformed, out result).Should().BeFalse();
            result.Should().BeNull();
        }

        [TestMethod]
        public void ArbitraryUnitsAreReadFromUcumEssence()
        {
            // The embedded ucum-essence.xml must be found, and the fallback list must match it.
            var fromEssence = Quantity.ReadArbitraryUnits();
            fromEssence.Should().NotBeNull();
            fromEssence.Should().BeEquivalentTo(Quantity.ARBITRARY_UNITS_FALLBACK);
            Quantity.ArbitraryUnits.Should().BeEquivalentTo(fromEssence);

            // Every arbitrary unit (also with a prefix) makes multiplication and division with another unit impossible
            foreach (var unit in fromEssence!.Concat(["k[IU]", "[IU]/mL", "[hp_X]"]))
            {
                Quantity.TryMultiply(new Quantity(2m, unit), new Quantity(3m, "s"), out _).Should().BeFalse(unit);
                Quantity.TryDivide(new Quantity(2m, "s"), new Quantity(3m, unit), out _).Should().BeFalse(unit);
            }
        }

        [TestMethod]
        public void QuantityArithmeticWithLeadingUnityFactor()
        {
            // '1/min' is common in FHIR vital signs (e.g. heart rate)
            Quantity.TryMultiply(new Quantity(60m, "1/min"), new Quantity(2m, "min"), out var result).Should().BeTrue();
            result!.Unit.Should().Be("1");
            Math.Round(result.Value, 8).Should().Be(120m);

            Quantity.TryDivide(new Quantity(60m, "1/min"), new Quantity(2m, "1/min"), out result).Should().BeTrue();
            result!.Unit.Should().Be("1");
            Math.Round(result.Value, 8).Should().Be(30m);
        }

        [TestMethod]
        public void QuantityArithmeticIsThreadSafe()
        {
            var results = new System.Collections.Concurrent.ConcurrentBag<string>();

            System.Threading.Tasks.Parallel.For(0, 2000, i =>
            {
                Quantity.TryMultiply(new Quantity(2.0m, "cm"), new Quantity(2.0m, "m"), out var product).Should().BeTrue();
                Quantity.TryDivide(new Quantity(4.0m, "g"), new Quantity(2.0m, "m"), out var quotient).Should().BeTrue();
                results.Add($"{product} {quotient}");
            });

            System.Linq.Enumerable.Distinct(results).Should().ContainSingle().Which.Should().Be("0.040000 'm2' 2 'g.m-1'");
        }

        private delegate bool QuantityOperation(Quantity left, Quantity right, out Quantity? result);

        private static void checkArithmetic(string left, string right, string? expected, QuantityOperation operation)
        {
            var success = operation(Quantity.Parse(left), Quantity.Parse(right), out var result);

            if (expected is null)
            {
                success.Should().BeFalse();
                result.Should().BeNull();
            }
            else
            {
                success.Should().BeTrue();
                var expectedQ = Quantity.Parse(expected);
                result!.Unit.Should().Be(expectedQ.Unit);
                result.System.Should().Be(expectedQ.System);
                result.Value.Should().Be(expectedQ.Value);
            }
        }

        [TestMethod]
        public void IsNullWhenInvalid()
        {
            Quantity.TryParse("hi", out var parsed).Should().BeFalse();
            parsed.Should().BeNull();
        }
    }
}