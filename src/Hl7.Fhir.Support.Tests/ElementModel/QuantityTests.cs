/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.ElementModel.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.ElementModel.Tests
{
    [TestClass]
    public class QuantityTests
    {
        [TestMethod]
        public void QuantityParsing()
        {
            Assert.AreEqual(new P.Quantity(75.5m, "kg"), P.Quantity.Parse("75.5 'kg'"));
            Assert.AreEqual(new P.Quantity(75.5m, "kg"), P.Quantity.Parse("75.5'kg'"));
            Assert.AreEqual(new P.Quantity(75m, "kg"), P.Quantity.Parse("75 'kg'"));
            Assert.AreEqual(new P.Quantity(40m, "wk"), P.Quantity.Parse("40 'wk'"));
            Assert.AreEqual(new P.Quantity(40m, "wk"), P.Quantity.Parse("40 weeks"));
            Assert.AreEqual(P.Quantity.ForCalendarDuration(2m, "year"), P.Quantity.Parse("2 year"));
            Assert.AreEqual(P.Quantity.ForCalendarDuration(6m, "month"), P.Quantity.Parse("6 months"));
            Assert.AreEqual(new P.Quantity(40.0m, P.Quantity.UCUM_UNIT), P.Quantity.Parse("40.0"));
            Assert.AreEqual(new P.Quantity(1m), P.Quantity.Parse("1 '1'"));
            Assert.AreEqual(new P.Quantity(1m, "m/s"), P.Quantity.Parse("1 'm/s'"));

            reject("40,5 weeks");
            reject("40 weks");
            reject("40 decennia");
            reject("ab kg");
            reject("75 'kg");
            reject("75 kg");
            reject("'kg'");
        }

        void reject(string testValue)
        {
            Assert.IsFalse(P.Quantity.TryParse(testValue, out _));
        }

        [TestMethod]
        public void QuantityFormatting()
        {
            Assert.AreEqual("75.6 'kg'", new P.Quantity(75.6m, "kg").ToString());
        }

        [TestMethod]
        public void QuantityConstructor()
        {
            var newq = new P.Quantity(3.14m, "kg");
            Assert.AreEqual("kg", newq.Unit);
            Assert.AreEqual(3.14m, newq.Value);
        }

        [TestMethod]
        public void QuantityEquals()
        {
            var newq = new P.Quantity(3.14m, "kg");

            Assert.AreEqual(newq, new P.Quantity(3.14m, "kg"));
            Assert.AreNotEqual(newq, new P.Quantity(3.15m, "kg"));
        }

        [TestMethod]
        public void QuantityComparison()
        {
            var smaller = new P.Quantity(3.14m, "kg");
            var bigger = new P.Quantity(4.0m, "kg");

            Assert.IsTrue(smaller < bigger);
#pragma warning disable CS1718 // Comparison made to same variable
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
            var a = new P.Quantity(3.14m, "kg");
            var b = new P.Quantity(30.5m, "g");
            /*
            Func<object> func = () => a < b;
            ExceptionAssert.Throws<NotSupportedException>);
            Assert.IsFalse(a == b);
            ExceptionAssert.Throws<NotSupportedException>(() => a >= b);
            Assert.IsFalse(a.Equals(b));*/
        }

        public enum Comparison
        {
            LessThan,
            Equals,
            GreaterThan
        }

        [DataTestMethod]
        [DataRow("1 'm'", "1 'm'", Comparison.Equals)]
        [DataRow("1 'm'", "2 'm'", Comparison.LessThan)]
        [DataRow("1 'cm'", "1 'm'", Comparison.LessThan)]
        [DataRow("30.0 'g'", "0.03 'kg'", Comparison.Equals)]
        [DataRow("1 '[in_i]'", "2 'cm'", Comparison.GreaterThan)] // 1 inch is greater than 2
        [DataRow("1 '[stone_av]'", "6350.29318 'g'", Comparison.Equals)]
        [DataRow("3 'hr'", "3 'h'", Comparison.Equals, true)]
        [DataRow("3 'a'", "3 year", Comparison.Equals)]
        [DataRow("3 'a'", "3 years", Comparison.Equals)]
        [DataRow("3 'mo'", "3 months", Comparison.Equals)]
        [DataRow("3 'd'", "3 days", Comparison.Equals)]
        [DataRow("1 'd'", "3 days", Comparison.LessThan)]
        public void QuantityCompareTests(string left, string right, Comparison expectedResult, bool shouldThrowException = false)
        {

            P.Quantity.TryParse(left, out var a);
            P.Quantity.TryParse(right, out var b);

            Func<int> func = () => a.CompareTo(b);

            if (shouldThrowException)
            {
                func.Should().Throw<Exception>();
                return;
            }
            var result = func();

            switch (expectedResult)
            {
                case Comparison.LessThan:
                    result.Should().BeNegative();
                    break;
                case Comparison.Equals:
                    result.Should().Be(0);
                    break;
                case Comparison.GreaterThan:
                    result.Should().BePositive();
                    break;
            }
        }

        /*     [DataTestMethod]
         [DataRow("25 'kg'", "5 'kg'", "30 'kg'",  (Quantity a, Quantity b) => a + b ]
        [DataRow("1 'm'", "2 'm'", Comparison.LessThan)]
            [DataRow("1 'cm'", "1 'm'", Comparison.LessThan)]
            [DataRow("30.0 'g'", "0.03 'kg'", Comparison.Equals)]
            [DataRow("1 '[in_i]'", "2 'cm'", Comparison.GreaterThan)] // 1 inch is greater than 2
            [DataRow("1 '[stone_av]'", "6350.29318 'g'", Comparison.Equals)]
            [DataRow("3 'hr'", "3 'h'", Comparison.Equals, true)]
            [DataRow("3 'a'", "3 year", Comparison.Equals)]
            [DataRow("3 'a'", "3 years", Comparison.Equals)]
            [DataRow("3 'mo'", "3 months", Comparison.Equals)]
            [DataRow("3 'd'", "3 days", Comparison.Equals)]
            [DataRow("1 'd'", "3 days", Comparison.LessThan)]*/

        public static IEnumerable<object[]> ArithmeticTestdata => new[]
                {
                    new object[] { "25 'kg'", "5 'kg'", "30 'kg'", (Quantity a, Quantity b) => a + b },
                    new object[] { "25 'kg'", "1000 'g'", "26000 'g'", (Quantity a, Quantity b) => a + b },
                    new object[] { "3 '[in_i]'", "2 '[in_i]'", "5 '[in_i]'", (Quantity a, Quantity b) => a + b },
                    new object[] { "4.0 'kg.m/s2'", "2000 'g.m.s-2'", "6000 'g.m.s-2'", (Quantity a, Quantity b) => a + b },

                    new object[] { "25 'kg'", "500 'g'", "24500 'g'", (Quantity a, Quantity b) => a - b },
                    new object[] { "25 'kg'", "25001 'g'", "-1 'g'", (Quantity a, Quantity b) => a - b },
                    new object[] { "1 '[in_i]'", "2 'cm'", "0.005400 'm'", (Quantity a, Quantity b) => a - b },

                    new object[] { "25 'km'", "20 'cm'", "5000 'm2'", (Quantity a, Quantity b) => a * b },
                    new object[] { "2.0 'cm'", "2.0 'm'", "0.040 'm2'", (Quantity a, Quantity b) => a * b },

                    new object[] { "14.4 'km'", "2.0 'h'", "2 'm.s-1'", (Quantity a, Quantity b) => a / b },
                    new object[] { "9 'm2'", "3 'm'", "3 'm'", (Quantity a, Quantity b) => a / b },
                };


        [TestMethod]
        [DynamicData(nameof(ArithmeticTestdata))]
        public void ArithmeticOperationsTests(string left, string right, string result, Func<Quantity, Quantity, Quantity> operation)
        {
            _ = Quantity.TryParse(left, out var q1);
            _ = Quantity.TryParse(right, out var q2);
            _ = Quantity.TryParse(result, out var r);

            var opResult = operation(q1, q2);

            opResult.Should().Be(r);
        }
    }
}