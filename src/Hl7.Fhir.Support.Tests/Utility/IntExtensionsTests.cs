using FluentAssertions;
using Hl7.Fhir.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Hl7.Fhir.Support.Tests
{
    [TestClass]
    public class IntExtensionsTests
    {
        [TestMethod]
        [DataRow(1, 4, 25)]
        [DataRow(0, 7, 0)]
        [DataRow(4, 4, 100)]
        [DataRow(30_000_000, 60_000_000, 50)]
        [DataRow(int.MaxValue, int.MaxValue, 100)]
        public void PercentageOfComputesWholePercentage(int part, int total, int expected) =>
            part.PercentageOf(total).Should().Be(expected);

        [TestMethod]
        [DataRow(0)]
        [DataRow(5)]
        public void PercentageOfThrowsOnZeroTotal(int part)
        {
            Action act = () => part.PercentageOf(0);
            act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("total");
        }

        [TestMethod]
        public void PercentageOfThrowsWhenResultDoesNotFitInInt()
        {
            Action act = () => int.MaxValue.PercentageOf(1);
            act.Should().Throw<OverflowException>();
        }
    }
}
