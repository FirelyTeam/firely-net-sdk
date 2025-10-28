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