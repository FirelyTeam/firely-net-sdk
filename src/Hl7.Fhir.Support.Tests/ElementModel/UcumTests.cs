#nullable enable 

using FluentAssertions;
using Hl7.Fhir.ElementModel.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Hl7.Fhir.Support.Tests.ElementModel
{
    [TestClass]
    public class UcumTests
    {
        [DataTestMethod]
        [DataRow(1.0, "m", true, "m")]
        [DataRow(1.0, "cm", true, "m")]
        [DataRow(1.0, "mm", true, "m")]
        [DataRow(1.0, "[in_i]", true, "m")]
        [DataRow(1.0, "[stone_av]", true, "g")] //britisch stone
        [DataRow(1.0, "m[H2O]", true, "g.m-1.s-2")] // meter of water column
        [DataRow(1.0, "unknown", false, null)]
        public void TryCanonicalizeTests(double val, string unit, bool exptectedResult, string? expectedUnit)
        {
            var input = new Quantity(Convert.ToDecimal(val), unit);

            var result = input.TryCanonicalize(out var convertedQuantity);

            result.Should().Be(exptectedResult);
            var u = convertedQuantity?.Unit;
            u.Should().Be(expectedUnit);
        }
    }
}

#nullable restore