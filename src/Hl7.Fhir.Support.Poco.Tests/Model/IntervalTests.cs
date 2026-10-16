/*
 * Copyright (c) 2026, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using FluentAssertions;
using Hl7.Fhir.ElementModel.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Hl7.Fhir.Tests.Model
{
    [TestClass]
    public class IntervalTests
    {
        [TestMethod]
        public void ContainsRespectsClosedness()
        {
            var closed = new Interval<int>(1, 5);
            var open = new Interval<int>(1, 5, lowClosed: false, highClosed: false);

            closed.Contains(1).Should().BeTrue();
            closed.Contains(5).Should().BeTrue();
            open.Contains(1).Should().BeFalse();
            open.Contains(5).Should().BeFalse();
            open.Contains(3).Should().BeTrue();
            Interval<int>.From(3).Contains(int.MaxValue).Should().BeTrue();
            Interval<int>.Until(3, closed: false).Contains(3).Should().BeFalse();
        }

        [TestMethod]
        public void OverlapsAtSharedBoundOnlyWhenBothClosed()
        {
            new Interval<int>(1, 5).Overlaps(new Interval<int>(5, 9)).Should().BeTrue();
            new Interval<int>(1, 5, highClosed: false).Overlaps(new Interval<int>(5, 9)).Should().BeFalse();
            Interval<int>.Until(0).Overlaps(Interval<int>.From(0)).Should().BeTrue();
            Interval<int>.Until(0).Overlaps(Interval<int>.From(1)).Should().BeFalse();
        }

        [TestMethod]
        public void CoversComparesClosedness()
        {
            new Interval<int>(1, 5).Covers(new Interval<int>(1, 5, false, false)).Should().BeTrue();
            new Interval<int>(1, 5, false, false).Covers(new Interval<int>(1, 5)).Should().BeFalse();
            Interval<int>.From(0).Covers(new Interval<int>(1, 5)).Should().BeTrue();
            new Interval<int>(1, 5).Covers(Interval<int>.From(1)).Should().BeFalse();
        }

        [TestMethod]
        public void EmptyIntervalsAreRejected()
        {
            FluentActions.Invoking(() => new Interval<int>(5, 1)).Should().Throw<ArgumentException>();
            FluentActions.Invoking(() => new Interval<int>(5, 5, highClosed: false)).Should().Throw<ArgumentException>();
            new Interval<int>(5, 5).Contains(5).Should().BeTrue();
        }

        [TestMethod]
        public void UnboundedSideHasNoBound()
        {
            var i = Interval<int>.From(3);
            i.HasHigh.Should().BeFalse();
            i.HighClosed.Should().BeFalse();
            FluentActions.Invoking(() => i.High).Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void DefaultIsUnboundedOnBothSides()
        {
            var all = default(Interval<int>);
            all.HasLow.Should().BeFalse();
            all.HasHigh.Should().BeFalse();
            all.Contains(int.MinValue).Should().BeTrue();
            all.Covers(new Interval<int>(1, 5)).Should().BeTrue();
            all.ToString().Should().Be("(-∞, +∞)");
        }

        [TestMethod]
        public void EqualityAndToString()
        {
            (new Interval<int>(1, 5) == new Interval<int>(1, 5)).Should().BeTrue();
            (new Interval<int>(1, 5) != new Interval<int>(1, 5, highClosed: false)).Should().BeTrue();
            new Interval<int>(1, 5).GetHashCode().Should().Be(new Interval<int>(1, 5).GetHashCode());
            new Interval<int>(1, 5, highClosed: false).ToString().Should().Be("[1, 5)");
            Interval<int>.Until(5).ToString().Should().Be("(-∞, 5]");
        }

        [TestMethod]
        public void WorksForReferenceTypes()
        {
            var i = new Interval<string>("b", "d");
            i.Contains("c").Should().BeTrue();
            i.Contains("e").Should().BeFalse();
            FluentActions.Invoking(() => i.Contains(null!)).Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void DateTimeRangeExposesItsInterval()
        {
            var range = Hl7.Fhir.ElementModel.Types.DateTime.Parse("2026-03-15").GetRange(TimeSpan.Zero);

            range.Interval.LowClosed.Should().BeTrue();
            range.Interval.HighClosed.Should().BeFalse();
            range.Interval.Low.Should().Be(range.Start!.Value);
            range.Interval.High.Should().Be(range.End!.Value);
        }
    }
}
