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

        // public static IEnumerable<object?[]> ArithmeticTestdata => new[]
        //         {
        //             ["25 'kg'", "5 'kg'", "30 'kg'" , (object)Quantity.Add],
        //             ["25000 'g'", "1000 'g'", "26000 'g'", (object)Quantity.Add],
        //             ["3 '[in_i]'", "2 '[in_i]'", "5 '[in_i]'", (object)Quantity.Add],
        //             ["4.0 'kg.m/s2'", "2.0 'kg.m/s2'", "6.0 'kg.m/s2'", (object)Quantity.Add],
        //             ["3 'm'", "0.03 'm'", "3.03 'm'", (object)Quantity.Add],
        //             ["3 'm'", "0 'm'","3 'm'", (object)Quantity.Add],
        //             ["3 'm'", "-0.8 'm'", "2.2 'm'", (object)Quantity.Add],
        //             ["3 'm'", "0 'kg'", null, (object)Quantity.Add],
        //             ["25000 'g'", "500 'g'", "24500 'g'", (object)Quantity.Substract],
        //             ["25000 'g'", "25001 'g'", "-1 'g'", (object)Quantity.Substract],
        //             ["1 '[in_i]'", "2 'kg'", null, (object)Quantity.Substract],
        //             ["25 'm'", "20 'm'", "500 '(m).(m)'", (object)Quantity.Multiply],
        //             ["2.0 'cm'", "9 'kg'", "18 '(cm).(kg)'", (object)Quantity.Multiply],
        //             ["14.4 'km'", "2.0 'h'", "7.2 '(km)/(h)'", (object)Quantity.Divide],
        //             ["9 'm2'", "3 'm'", "3 '(m2)/(m)'", (object)Quantity.Divide],
        //             ["6 'm'", "3 'm'", "2 '(m)/(m)'", (object)Quantity.Divide],
        //             new[] { "3 'm'", "0 'cm'", null, (object)Quantity.Divide }
        //         };


        // [TestMethod]
        // [DynamicData(nameof(ArithmeticTestdata))]
        // public void ArithmeticOperationsTests(string left, string right, object result, Func<Quantity, Quantity, Result<Quantity>> operation)
        // {
        //     Quantity.TryParse(left, out var q1).Should().BeTrue();
        //     Quantity.TryParse(right, out var q2).Should().BeTrue();
        //
        //     var opResult = operation(q1!, q2!);
        //
        //     if (result is string r && Quantity.TryParse(r, out var q3))
        //     {
        //         opResult.ValueOrDefault().Should().Be(q3);
        //     }
        //     else
        //     {
        //         opResult.Should().BeAssignableTo<IFailed>();
        //     }
        // }

        [TestMethod]
        public void IsNullWhenInvalid()
        {
            Quantity.TryParse("hi", out var parsed).Should().BeFalse();
            parsed.Should().BeNull();
        }
    }
}