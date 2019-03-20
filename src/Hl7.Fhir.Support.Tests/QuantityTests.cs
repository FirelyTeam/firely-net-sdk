/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using System;
using Hl7.Fhir.Model.Primitives;
using Hl7.Fhir.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.FhirPath.Tests
{
    [TestClass]
    public class QuantityTests
    {
        [TestMethod]
        public void QuantityConstructor()
        {
            var newq = new Quantity(3.14m, "kg");
            Assert.AreEqual("kg", newq.Unit);
            Assert.AreEqual(3.14m, newq.Value);

            newq = new Quantity(3.14, "kg", "http://someothersystem.nl");
            Assert.AreEqual("kg", newq.Unit);
            Assert.AreEqual(3.14m, newq.Value);
            Assert.AreEqual("http://someothersystem.nl", newq.System);
        }

        [TestMethod]
        public void QuantityEquals()
        {
            var newq = new Quantity(3.14m, "kg");
            var newq2 = new Quantity(3.14, "kg", Quantity.UCUM);
            var newq3 = new Quantity(3.14, "kg", "http://nu.nl");
            var newq4 = new Quantity(3.15, "kg");

            Assert.AreEqual(newq, newq2);
            ExceptionAssert.Throws<NotSupportedException>( () => newq == newq3);
            Assert.AreNotEqual(newq, newq4);
        }

        [TestMethod]
        public void Comparison()
        {
            var smaller = new Quantity(3.14m, "kg");
            var smaller2 = new Quantity(3.14, "kg", Quantity.UCUM);
            var bigger = new Quantity(4.0, "kg");

            Assert.IsTrue(smaller < bigger);
            Assert.IsTrue(smaller <= smaller2);
            Assert.IsTrue(bigger >= smaller);

            Assert.AreEqual(-1, smaller.CompareTo(bigger));
            Assert.AreEqual(1, bigger.CompareTo(smaller));
            Assert.AreEqual(0, smaller.CompareTo(smaller));
        }


        [TestMethod]
        public void DifferentUnitsNotSupported()
        {
            var a = new Quantity(3.14m, "kg");
            var b = new Quantity(30.5, "g");

            ExceptionAssert.Throws<NotSupportedException>( () => a < b);
            ExceptionAssert.Throws<NotSupportedException>(() => a == b);
            ExceptionAssert.Throws<NotSupportedException>(() => a >= b);

            Assert.IsFalse(a.Equals(b));
        }
    }
}