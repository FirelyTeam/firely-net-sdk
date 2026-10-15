/*
 * Copyright (c) 2026, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using FluentAssertions;
using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.Tests.Model
{
    [TestClass]
    public class DateTimeRangeTests
    {
        private static TimeZoneInfo amsterdam() =>
            TimeZoneInfo.TryFindSystemTimeZoneById("Europe/Amsterdam", out var tz) ? tz
            : TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");

        private static P.DateTimeRange range(DateTimeOffset? start, DateTimeOffset? end) =>
            P.DateTimeRange.Between(
                start is null ? null : P.DateTime.Parse(start.Value.ToString("o")).GetRange(TimeZoneInfo.Utc),
                end is null ? null : P.DateTime.Parse(end.Value.AddTicks(-1).ToString("o")).GetRange(TimeZoneInfo.Utc));

        private static DateTimeOffset utc(int y, int m, int d, int h = 0) => new(y, m, d, h, 0, 0, TimeSpan.Zero);

        [TestMethod]
        [DataRow("2026-03-15T10:00:00+01:00", "2026-03-15T10:00:00+01:00", "2026-03-15T10:00:01+01:00")]
        [DataRow("2026-01-01T00:00:00Z", "2026-01-01T00:00:00+00:00", "2026-01-01T00:00:01+00:00")]
        [DataRow("2026-01-01T00:00:00.000+00:00", "2026-01-01T00:00:00+00:00", "2026-01-01T00:00:00.001+00:00")]
        [DataRow("2026-01-01T00:00:00.0+00:00", "2026-01-01T00:00:00+00:00", "2026-01-01T00:00:00.1+00:00")]
        [DataRow("2026-01-01T00:00:00.0000000+00:00", "2026-01-01T00:00:00+00:00", "2026-01-01T00:00:00.0000001+00:00")]
        [DataRow("2026-01-01T10:00+02:00", "2026-01-01T10:00:00+02:00", "2026-01-01T10:01:00+02:00")]
        [DataRow("2026-01-01T10-05:00", "2026-01-01T10:00:00-05:00", "2026-01-01T11:00:00-05:00")]
        public void DateTimeWithOffsetCoversItsPrecision(string value, string start, string end)
        {
            P.DateTime.Parse(value).TryGetRange(out var range).Should().BeTrue();
            range!.Start.Should().Be(DateTimeOffset.Parse(start));
            range.End.Should().Be(DateTimeOffset.Parse(end));
            range.Start!.Value.Offset.Should().Be(DateTimeOffset.Parse(start).Offset);
            range.OriginalValue.Should().Be(value);
        }

        [TestMethod]
        public void OverlongFractionSpansOneTick()
        {
            if (!P.DateTime.TryParse("2026-01-01T00:00:00.123456789Z", out var dt)) return; // not parseable by DateTimeOffset on every runtime

            dt.TryGetRange(out var range).Should().BeTrue();
            (range!.End - range.Start).Should().Be(TimeSpan.FromTicks(1));
        }

        [TestMethod]
        public void MissingOffsetIsRejectedOrAssumed()
        {
            var dt = P.DateTime.Parse("2026-03-15T10:00:00");
            dt.TryGetRange(out _).Should().BeFalse();

            var range = dt.GetRange(TimeSpan.FromHours(-5));
            range.Start.Should().Be(DateTimeOffset.Parse("2026-03-15T10:00:00-05:00"));
            range.End.Should().Be(DateTimeOffset.Parse("2026-03-15T10:00:01-05:00"));

            dt.GetRange(TimeZoneInfo.CreateCustomTimeZone("minus5", TimeSpan.FromHours(-5), "minus5", "minus5"))
                .Should().Be(range);
        }

        [TestMethod]
        public void OffsetOfValueWinsOverAssumedOffset()
        {
            var dt = P.DateTime.Parse("2026-03-15T10:00:00+01:00");
            dt.GetRange(TimeSpan.FromHours(5)).Start.Should().Be(DateTimeOffset.Parse("2026-03-15T10:00:00+01:00"));
            dt.GetRange(TimeZoneInfo.Utc).Start.Should().Be(DateTimeOffset.Parse("2026-03-15T10:00:00+01:00"));
        }

        [TestMethod]
        [DataRow("2026", "2026-01-01", "2027-01-01", P.DateTimePrecision.Year)]
        [DataRow("2026-02", "2026-02-01", "2026-03-01", P.DateTimePrecision.Month)]
        [DataRow("2024-02", "2024-02-01", "2024-03-01", P.DateTimePrecision.Month)]
        [DataRow("2024-02-29", "2024-02-29", "2024-03-01", P.DateTimePrecision.Day)]
        public void DateCoversItsPrecision(string value, string start, string end, P.DateTimePrecision precision)
        {
            var date = P.Date.Parse(value);
            date.TryGetRange(out _).Should().BeFalse();

            var range = date.GetRange(TimeZoneInfo.Utc);
            range.Start.Should().Be(DateTimeOffset.Parse(start + "T00:00:00Z"));
            range.End.Should().Be(DateTimeOffset.Parse(end + "T00:00:00Z"));
            range.Precision.Should().Be(precision);
        }

        [TestMethod]
        public void DateWithOffsetHasRangeWithoutAssumption()
        {
            var date = P.Date.FromDateTimeOffset(new DateTimeOffset(2026, 3, 15, 0, 0, 0, TimeSpan.FromHours(2)), includeOffset: true);

            date.TryGetRange(out var range).Should().BeTrue();
            range!.Start.Should().Be(new DateTimeOffset(2026, 3, 15, 0, 0, 0, TimeSpan.FromHours(2)));
            range.End.Should().Be(new DateTimeOffset(2026, 3, 16, 0, 0, 0, TimeSpan.FromHours(2)));
        }

        [TestMethod]
        [DataRow("2026-03-29", 23)]
        [DataRow("2026-10-25", 25)]
        [DataRow("2026-06-01", 24)]
        public void DayCrossingDaylightSavingChangeHasItsRealLength(string day, int hours)
        {
            var range = P.Date.Parse(day).GetRange(amsterdam());
            (range.End - range.Start).Should().Be(TimeSpan.FromHours(hours));
        }

        [TestMethod]
        public void SkippedWallClockTimeWidensRange()
        {
            // 02:30 does not exist in Amsterdam on 2026-03-29: clocks go from 02:00 +01:00 to 03:00 +02:00.
            var range = P.DateTime.Parse("2026-03-29T02:30").GetRange(amsterdam());

            range.Start.Should().Be(utc(2026, 3, 29, 0).AddMinutes(30));  // 02:30 at +02:00
            range.End.Should().Be(utc(2026, 3, 29, 1).AddMinutes(31));    // 02:31 at +01:00
            range.Covers(P.DateTime.Parse("2026-03-29T02:30:00+01:00").GetRange(TimeZoneInfo.Utc)).Should().BeTrue();
            range.Covers(P.DateTime.Parse("2026-03-29T02:30:00+02:00").GetRange(TimeZoneInfo.Utc)).Should().BeTrue();
        }

        [TestMethod]
        public void RepeatedWallClockTimeWidensRange()
        {
            // 02:30 happens twice in Amsterdam on 2026-10-25: first at +02:00, then at +01:00.
            var range = P.DateTime.Parse("2026-10-25T02:30").GetRange(amsterdam());

            range.Start.Should().Be(utc(2026, 10, 25, 0).AddMinutes(30)); // first 02:30, at +02:00
            range.End.Should().Be(utc(2026, 10, 25, 1).AddMinutes(31));   // second 02:31, at +01:00
        }

        [TestMethod]
        public void BetweenTakesOuterBoundsAndCoarserPrecision()
        {
            var range = P.DateTimeRange.Between(
                P.DateTime.Parse("2026-01-01T00:00:00Z").GetRange(TimeZoneInfo.Utc),
                P.Date.Parse("2026-12").GetRange(TimeZoneInfo.Utc));

            range.Start.Should().Be(utc(2026, 1, 1));
            range.End.Should().Be(utc(2027, 1, 1));
            range.Precision.Should().Be(P.DateTimePrecision.Month);
        }

        [TestMethod]
        public void BetweenHandlesOpenSides()
        {
            var day = P.Date.Parse("2026-03-15").GetRange(TimeZoneInfo.Utc);

            var openStart = P.DateTimeRange.Between(null, day);
            openStart.Start.Should().BeNull();
            openStart.End.Should().Be(utc(2026, 3, 16));

            var reopened = P.DateTimeRange.Between(openStart, day);
            reopened.Start.Should().BeNull();
            reopened.End.Should().Be(utc(2026, 3, 16));

            var openEnd = P.DateTimeRange.Between(day, null);
            openEnd.Start.Should().Be(utc(2026, 3, 15));
            openEnd.End.Should().BeNull();

            FluentActions.Invoking(() => P.DateTimeRange.Between(openStart, null)).Should().Throw<ArgumentException>();
            FluentActions.Invoking(() => P.DateTimeRange.Between(null, null)).Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void ContainsIsHalfOpen()
        {
            var r = range(utc(2026, 1, 1), utc(2026, 1, 2));
            r.Contains(utc(2026, 1, 1)).Should().BeTrue();
            r.Contains(utc(2026, 1, 2).AddTicks(-1)).Should().BeTrue();
            r.Contains(utc(2026, 1, 2)).Should().BeFalse();
            r.Contains(utc(2025, 12, 31)).Should().BeFalse();

            range(null, utc(2026, 1, 2)).Contains(DateTimeOffset.MinValue).Should().BeTrue();
            range(utc(2026, 1, 1), null).Contains(DateTimeOffset.MaxValue).Should().BeTrue();
        }

        [TestMethod]
        public void OverlapsAndCovers()
        {
            var jan = range(utc(2026, 1, 1), utc(2026, 2, 1));
            var feb = range(utc(2026, 2, 1), utc(2026, 3, 1));
            var midJanToMidFeb = range(utc(2026, 1, 15), utc(2026, 2, 15));
            var untilFeb = range(null, utc(2026, 2, 1));
            var fromJan = range(utc(2026, 1, 1), null);

            jan.Overlaps(feb).Should().BeFalse(); // adjacent half-open ranges share no instant
            jan.Overlaps(midJanToMidFeb).Should().BeTrue();
            untilFeb.Overlaps(feb).Should().BeFalse();
            fromJan.Overlaps(untilFeb).Should().BeTrue();

            fromJan.Covers(feb).Should().BeTrue();
            jan.Covers(midJanToMidFeb).Should().BeFalse();
            jan.Covers(untilFeb).Should().BeFalse(); // an unbounded side is never covered by a bounded one
            untilFeb.Covers(jan).Should().BeTrue();
            jan.Covers(jan).Should().BeTrue();
        }

        [TestMethod]
        public void EqualityIgnoresOriginalValue()
        {
            var a = P.DateTime.Parse("2026-01-01T00:00:00Z").GetRange(TimeZoneInfo.Utc);
            var b = P.DateTime.Parse("2026-01-01T01:00:00+01:00").GetRange(TimeZoneInfo.Utc);

            a.Should().Be(b);
            a.GetHashCode().Should().Be(b.GetHashCode());
            a.Should().NotBe(P.DateTime.Parse("2026-01-01T00:00Z").GetRange(TimeZoneInfo.Utc));
        }

        [TestMethod]
        public void ToStringMarksOpenSides()
        {
            var day = P.Date.Parse("2026-03-15").GetRange(TimeZoneInfo.Utc);

            day.ToString().Should().Be("[2026-03-15T00:00:00.0000000+00:00, 2026-03-16T00:00:00.0000000+00:00)");
            P.DateTimeRange.Between(null, day).ToString().Should().Be("(-∞, 2026-03-16T00:00:00.0000000+00:00)");
            P.DateTimeRange.Between(day, null).ToString().Should().Be("[2026-03-15T00:00:00.0000000+00:00, +∞)");
        }

        [TestMethod]
        public void PeriodWithMissingEndIsOpen()
        {
            var period = new Period(new FhirDateTime("2026-03-15"), null);

            period.TryGetRange(TimeZoneInfo.Utc, out var range).Should().BeTrue();
            range!.Start.Should().Be(utc(2026, 3, 15));
            range.End.Should().BeNull();

            period.TryGetRange(out _).Should().BeFalse(); // a date has no offset
            new Period().TryGetRange(TimeZoneInfo.Utc, out _).Should().BeFalse();
        }

        [TestMethod]
        public void PeriodWithOnlyEndIsOpenAtStart()
        {
            var period = new Period(null, new FhirDateTime("2026-03-15T10:00:00Z"));

            period.TryGetRange(out var range).Should().BeTrue();
            range!.Start.Should().BeNull();
            range.End.Should().Be(utc(2026, 3, 15, 10).AddSeconds(1));
        }
    }
}
