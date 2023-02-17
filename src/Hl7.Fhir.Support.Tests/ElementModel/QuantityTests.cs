/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
        public void Comparison()
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

            ExceptionAssert.Throws<NotSupportedException>(() => a < b);
            Assert.IsFalse(a == b);
            ExceptionAssert.Throws<NotSupportedException>(() => a >= b);
            Assert.IsFalse(a.Equals(b));
        }
    }
}