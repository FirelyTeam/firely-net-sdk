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