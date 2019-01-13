/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using Xunit;
using Hl7.Fhir.Model.Primitives;

namespace Hl7.FhirPath.Tests
{

    public class QuantityTests
    {
        [Fact]
        public void QuantityParsing()
        {
            Assert.Equal(new Quantity(75.5m, "kg"), Quantity.Parse("75.5 'kg'"));
            Assert.Equal(new Quantity(75.5m, "kg"), Quantity.Parse("75.5'kg'"));
            Assert.Equal(new Quantity(75m, "kg"), Quantity.Parse("75 'kg'"));
            Assert.Equal(new Quantity(40, "wk"), Quantity.Parse("40 weeks"));
            Assert.Equal(new Quantity(40.0m, "1"), Quantity.Parse("40.0"));
            Assert.Equal(new Quantity(1, "1"), Quantity.Parse("1 '1'"));
            Assert.Equal(new Quantity(1, "m/s"), Quantity.Parse("1 'm/s'"));

            Assert.Throws<FormatException>(() => Quantity.Parse("40,5 weeks"));
            Assert.Throws<FormatException>(() => Quantity.Parse("40 decennia"));
            Assert.Throws<FormatException>(() => Quantity.Parse("ab kg"));
            Assert.Throws<FormatException>(() => Quantity.Parse("75 'kg"));
            Assert.Throws<FormatException>(() => Quantity.Parse("'kg'"));
        }

        [Fact]
        public void QuantityFormatting()
        {
            Assert.Equal("75.6 'kg'", new Quantity(75.6m, "kg").ToString());
        }

        [Fact]
        public void QuantityConstructor()
        {
            var newq = new Quantity(3.14m, "kg");
            Assert.Equal("kg", newq.Unit);
            Assert.Equal(3.14m, newq.Value);

            newq = new Quantity(3.14, "kg", "http://someothersystem.nl");
            Assert.Equal("kg", newq.Unit);
            Assert.Equal(3.14m, newq.Value);
            Assert.Equal("http://someothersystem.nl", newq.System);
        }

        [Fact]
        public void QuantityEquals()
        {
            var newq = new Quantity(3.14m, "kg");
            var newq2 = new Quantity(3.14, "kg", Quantity.UCUM);
            var newq3 = new Quantity(3.14, "kg", "http://nu.nl");
            var newq4 = new Quantity(3.15, "kg");

            Assert.Equal(newq, newq2);
            Assert.Throws<NotSupportedException>( () => newq == newq3);
            Assert.NotEqual(newq, newq4);
        }

        [Fact]
        public void Comparison()
        {
            var smaller = new Quantity(3.14m, "kg");
            var smaller2 = new Quantity(3.14, "kg", Quantity.UCUM);
            var bigger = new Quantity(4.0, "kg");

            Assert.True(smaller < bigger);
            Assert.True(smaller <= smaller2);
            Assert.True(bigger >= smaller);

            Assert.Equal(-1, smaller.CompareTo(bigger));
            Assert.Equal(1, bigger.CompareTo(smaller));
            Assert.Equal(0, smaller.CompareTo(smaller));
        }


        [Fact]
        public void DifferentUnitsNotSupported()
        {
            var a = new Quantity(3.14m, "kg");
            var b = new Quantity(30.5, "g");

            Assert.Throws<NotSupportedException>( () => a < b);
            Assert.Throws<NotSupportedException>(() => a == b);
            Assert.Throws<NotSupportedException>(() => a >= b);

            Assert.False(a.Equals(b));
        }
    }
}