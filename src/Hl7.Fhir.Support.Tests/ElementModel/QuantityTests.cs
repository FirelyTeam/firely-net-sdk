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

        [TestMethod]
        public void QuantityToString()
        {
            // UCUM units are quoted, calendar duration keywords are not (and are plural unless the value is 1)
            new Quantity(53m, "km").ToString().Should().Be("53 'km'");
            new Quantity(4m, "d").ToString().Should().Be("4 'd'");
            Quantity.Parse("1 week").ToString().Should().Be("1 week");
            Quantity.Parse("4 days").ToString().Should().Be("4 days");
            Quantity.Parse("1 day").ToString().Should().Be("1 day");
            Quantity.Parse("2.5 years").ToString().Should().Be("2.5 years");

            // only the calendar duration keywords lose their quotes
            new Quantity(3m, "wk", QuantityUnitSystem.CalendarDuration).ToString().Should().Be("3 'wk'");

            // and they round-trip through Parse
            Quantity.Parse(Quantity.Parse("4 days").ToString()).Should().Be(Quantity.Parse("4 days"));
        }

        [TestMethod]
        public void CompareDurations()
        {
            // calendar durations from week down are equal to their UCUM counterparts, and convert into each other
            Quantity.Parse("7 days").Should().Be(Quantity.Parse("1 week"));
            Quantity.Parse("7 days").Should().Be(Quantity.Parse("1 'wk'"));
            Quantity.Parse("1 week").Should().Be(Quantity.Parse("7 'd'"));
            Quantity.Parse("1 hour").Should().Be(Quantity.Parse("3600 's'"));
            Quantity.Parse("1500 'ms'").Should().Be(Quantity.Parse("1.5 seconds"));
            Quantity.Parse("1 'a'").Should().Be(Quantity.Parse("365.25 'd'"));
            Quantity.Parse("1 'a'").Should().Be(Quantity.Parse("12 'mo'"));

            ((ICqlOrderable)Quantity.Parse("6 days")).CompareTo(Quantity.Parse("1 week")).Should().Be(-1);
            ((ICqlOrderable)Quantity.Parse("8 days")).CompareTo(Quantity.Parse("1 'wk'")).Should().Be(1);

            // calendar years and months are not equal to, nor ordered with, the UCUM 'a' and 'mo'...
            Quantity.Parse("1 year").TryEquals(Quantity.Parse("1 'a'"), Quantity.CQL_EQUALS_COMPARISON, out _).Should().BeFalse();
            ((ICqlOrderable)Quantity.Parse("1 year")).CompareTo(Quantity.Parse("1 'a'")).Should().BeNull();
            ((ICqlOrderable)Quantity.Parse("1 year")).CompareTo(Quantity.Parse("2 months")).Should().BeNull();
            ((ICqlOrderable)Quantity.Parse("1 month")).CompareTo(Quantity.Parse("1 'd'")).Should().BeNull();

            // ... but they are equivalent
            ((ICqlEquatable)Quantity.Parse("1 year")).IsEquivalentTo(Quantity.Parse("1 'a'")).Should().BeTrue();
            ((ICqlEquatable)Quantity.Parse("1 year")).IsEquivalentTo(Quantity.Parse("12 months")).Should().BeTrue();

            // for equivalence, a calendar year or month converts like 'a' or 'mo' (as in the Java reference engine)
            ((ICqlEquatable)Quantity.Parse("1 year")).IsEquivalentTo(Quantity.Parse("365.25 days")).Should().BeTrue();
            ((ICqlEquatable)Quantity.Parse("1 month")).IsEquivalentTo(Quantity.Parse("30.4375 'd'")).Should().BeTrue();
            ((ICqlEquatable)Quantity.Parse("1 year")).IsEquivalentTo(Quantity.Parse("1 day")).Should().BeFalse();
            ((ICqlEquatable)Quantity.Parse("1 month")).IsEquivalentTo(Quantity.Parse("1 day")).Should().BeFalse();

            // converting does not hide differences below the rounding precision of the larger unit
            Quantity.Parse("1 'wk'").Should().NotBe(Quantity.Parse("604800001 'ms'"));
            ((ICqlOrderable)Quantity.Parse("604800001 'ms'")).CompareTo(Quantity.Parse("1 week")).Should().Be(1);
            ((ICqlOrderable)Quantity.Parse("1 'ms'")).CompareTo(Quantity.Parse("0 'a'")).Should().Be(1);
            Quantity.Parse("1 'd'").Should().NotBe(Quantity.Parse("0.14285714 'wk'"));
            ((ICqlOrderable)Quantity.Parse("-8 days")).CompareTo(Quantity.Parse("-1 week")).Should().Be(-1);

            // huge values do not overflow
            var huge = Quantity.Parse("79228162514264337593543950335 'd'");
            huge.Equals(Quantity.Parse("1 'wk'")).Should().BeFalse();
            ((ICqlOrderable)huge).CompareTo(Quantity.Parse("1 'wk'")).Should().Be(1);
            ((ICqlOrderable)Quantity.Parse("1 'wk'")).CompareTo(huge).Should().Be(-1);
            Quantity.Parse("3000000000000000000000 'a'").Should().Be(Quantity.Parse("1095750000000000000000000 'd'"));

            // equal durations have equal hash codes
            Quantity.Parse("7 days").GetHashCode().Should().Be(Quantity.Parse("1 week").GetHashCode());
            Quantity.Parse("7 'd'").GetHashCode().Should().Be(Quantity.Parse("1 'wk'").GetHashCode());
            Quantity.Parse("1 week").GetHashCode().Should().Be(Quantity.Parse("1 'wk'").GetHashCode());
            new HashSet<Quantity> { Quantity.Parse("7 'd'") }.Should().Contain(Quantity.Parse("1 'wk'"));
            Quantity.Parse("3000000000000000000000 'a'").GetHashCode().Should().Be(Quantity.Parse("1095750000000000000000000 'd'").GetHashCode());
            Quantity.Parse("1.000000001 'kg'").GetHashCode().Should().Be(Quantity.Parse("1 'kg'").GetHashCode());
            Quantity.Parse("-1 'wk'").GetHashCode().Should().Be(Quantity.Parse("-7 days").GetHashCode());
            Quantity.Parse("0 'wk'").GetHashCode().Should().Be(Quantity.Parse("0.00 'ms'").GetHashCode());
            Quantity.ForCalendarDuration(1m, "wk").GetHashCode().Should().Be(new Quantity(7m, "d").GetHashCode());

            // values whose product in milliseconds needs more than 28 digits are still compared exactly
            var precise = Quantity.Parse("1234567890123456789.12345678 'd'");
            precise.Should().Be(Quantity.Parse("1234567890123456789.12345678 day"));
            // (exactly 106666665706666666580266.665792 's', which has too many digits for a decimal)
            ((ICqlOrderable)precise).CompareTo(Quantity.Parse("106666665706666666580266.6658 's'")).Should().Be(-1);
            ((ICqlOrderable)precise).CompareTo(Quantity.Parse("106666665706666666580266.6657 's'")).Should().Be(1);
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
        public void DifferentUnitsNotSupported()
        {
            var a = new Quantity(3.14m, "kg");
            var b = new Quantity(30.5m, "g");
            
            Func<object> func = () => a < b;
            func.Should().Throw<InvalidOperationException>();
            func = () => a == b;
            func.Should().Throw<InvalidOperationException>();
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