/*
 * Copyright (c) 2026, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

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
        [TestMethod]
        [DataRow("2026-03-15T10:00:00+01:00", "2026-03-15T10:00:00+01:00", "2026-03-15T10:00:01+01:00")]
        [DataRow("2026-01-01T00:00:00Z", "2026-01-01T00:00:00+00:00", "2026-01-01T00:00:01+00:00")]
        [DataRow("2026-01-01T00:00:00.000+00:00", "2026-01-01T00:00:00+00:00", "2026-01-01T00:00:00.001+00:00")]
        [DataRow("2026-01-01T00:00:00.0+00:00", "2026-01-01T00:00:00+00:00", "2026-01-01T00:00:00.1+00:00")]
        [DataRow("2026-01-01T10:00+02:00", "2026-01-01T10:00:00+02:00", "2026-01-01T10:01:00+02:00")]
        public void DateTimeWithOffsetCoversItsPrecision(string value, string start, string end)
        {
            P.DateTime.Parse(value).TryGetRange(out var range).Should().BeTrue();
            range!.Start.Should().Be(DateTimeOffset.Parse(start));
            range.End.Should().Be(DateTimeOffset.Parse(end));
            range.OriginalValue.Should().Be(value);
        }

        [TestMethod]
        public void MissingOffsetIsRejectedOrAssumed()
        {
            var dt = P.DateTime.Parse("2026-03-15T10:00:00");
            dt.TryGetRange(out _).Should().BeFalse();

            var range = dt.GetRange(TimeSpan.FromHours(1));
            range.Start.Should().Be(DateTimeOffset.Parse("2026-03-15T10:00:00+01:00"));
            range.End.Should().Be(DateTimeOffset.Parse("2026-03-15T10:00:01+01:00"));
        }

        [TestMethod]
        public void DateCoversItsPrecision()
        {
            var date = P.Date.Parse("2026");
            date.TryGetRange(out _).Should().BeFalse();

            var range = date.GetRange(TimeZoneInfo.Utc);
            range.Start.Should().Be(new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero));
            range.End.Should().Be(new DateTimeOffset(2027, 1, 1, 0, 0, 0, TimeSpan.Zero));
            range.Precision.Should().Be(P.DateTimePrecision.Year);
        }

        [TestMethod]
        public void BetweenTakesOuterBoundsAndCoarserPrecision()
        {
            var range = P.DateTimeRange.Between(
                P.DateTime.Parse("2026-01-01T00:00:00Z").GetRange(TimeZoneInfo.Utc),
                P.Date.Parse("2026-12").GetRange(TimeZoneInfo.Utc));

            range.Start.Should().Be(new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero));
            range.End.Should().Be(new DateTimeOffset(2027, 1, 1, 0, 0, 0, TimeSpan.Zero));
            range.Precision.Should().Be(P.DateTimePrecision.Month);
        }

        [TestMethod]
        public void PeriodWithMissingEndIsOpen()
        {
            var period = new Period(new FhirDateTime("2026-03-15"), null);

            period.TryGetRange(TimeZoneInfo.Utc, out var range).Should().BeTrue();
            range!.Start.Should().Be(new DateTimeOffset(2026, 3, 15, 0, 0, 0, TimeSpan.Zero));
            range.End.Should().BeNull();

            new Period().TryGetRange(TimeZoneInfo.Utc, out _).Should().BeFalse();
        }
    }
}
