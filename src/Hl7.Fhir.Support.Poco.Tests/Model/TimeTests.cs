/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Hl7.Fhir.Tests.Model
{
    [TestClass]
    public class TimeTests
    {
        [TestMethod]
        public void TimeHandling()
        {
            var dt = new Time(23, 1, 2);
            Assert.AreEqual("23:01:02", dt.Value);

            var stamp = new DateTimeOffset(1972, 11, 30, 15, 10, 0, TimeSpan.Zero);
            dt = Time.FromDateTimeOffset(stamp);
            dt.Value.Should().Be("15:10:00");
        }

        [TestMethod]
        public void FromDateTimeOffsetWithMilliseconds()
        {
            var stamp = new DateTimeOffset(1972, 11, 30, 15, 10, 0, 123, TimeSpan.Zero);

            var dt = Time.FromDateTimeOffset(stamp);
            dt.Value.Should().Be("15:10:00");

            dt = Time.FromDateTimeOffset(stamp, includeMillis: true);
            dt.Value.Should().Be("15:10:00.123");
        }

        [TestMethod]
        public void TryToTimeSpan()
        {
            var fdt = new Time(13, 4, 18);
            fdt.TryToTimeSpan(out var dto1).Should().BeTrue();
            dto1.Should().Be(new TimeSpan(13, 4, 18));

            fdt = new Time("1999-01-01");
            fdt.TryToTimeSpan(out dto1).Should().BeFalse();

            fdt = new Time("24:02:03");
            fdt.TryToTimeSpan(out dto1).Should().BeFalse();

            fdt = new Time("01:01:01+01:00");
            fdt.TryToTimeSpan(out dto1).Should().BeFalse();
        }

        [TestMethod]
        public void NowTests()
        {
            var nowLocal = Time.Now();
            Assert.AreEqual(DateTimeOffset.Now.ToString("HH:mm:ss"), nowLocal.Value);

            var now = Time.UtcNow();
            Assert.AreEqual(DateTimeOffset.UtcNow.ToString("HH:mm:ss"), now.Value);
        }

        [TestMethod]
        public void UpdatesCachedValue()
        {
            var dft = new Time(12, 07, 11);
            dft.TryToTimeSpan(out var dto).Should().BeTrue();
            dft.TryToTimeSpan(out var dto2).Should().BeTrue();
            dto.Equals(dto2).Should().BeTrue();

            dft.Value = "01:02";
            dft.TryToTimeSpan(out dto).Should().BeTrue();
            dto.Hours.Should().Be(1);
            dft.TryToTimeSpan(out dto2).Should().BeTrue();
            dto.Equals(dto2).Should().BeTrue();

            dft.JsonValue = "18:23:34";
            dft.TryToTimeSpan(out dto).Should().BeTrue();
            dto.Minutes.Should().Be(23);
            dft.TryToTimeSpan(out dto2).Should().BeTrue();
            dto.Equals(dto2).Should().BeTrue();

            dft.Value = null;
            dft.TryToTimeSpan(out _).Should().BeFalse();
            Assert.Throws<FormatException>(() => dft.ToTimeSpan());
        }

        [TestMethod]
        public void ToTimeSpanThrowsInvalidFormat()
        {
            var dft = new Time("45:45:56");

            Assert.Throws<FormatException>(() => dft.ToTimeSpan());

            dft.TryToTimeSpan(out var _).Should().BeFalse();
        }

        [TestMethod]
        public void CanConvertToTime()
        {
            var dft = new Time(11, 12, 13);
            dft.TryToSystemTime(out var dt).Should().BeTrue();
            dt.Minutes.Should().Be(12);
            dt.Precision.Should().Be(ElementModel.Types.DateTimePrecision.Second);

            dft = new Time("11:12");
            dft.TryToSystemTime(out dt).Should().BeTrue();
            dt.Seconds.Should().BeNull();
            dt.Precision.Should().Be(ElementModel.Types.DateTimePrecision.Minute);

            dft = new Time("11:12:34.123");
            dft.TryToSystemTime(out dt).Should().BeTrue();
            dt.Millis.Should().Be(123);
            dt.Precision.Should().Be(ElementModel.Types.DateTimePrecision.Fraction);

            dft = new Time(11,12,34,123);
            dft.TryToSystemTime(out dt).Should().BeTrue();
            dt.Millis.Should().Be(123);
            dt.Precision.Should().Be(ElementModel.Types.DateTimePrecision.Fraction);

            dft = new Time(null);
            dft.TryToSystemTime(out dt).Should().BeFalse();
            dt.Should().BeNull();

            // Time cannot have an offset, so this should fail
            dft = new Time("01:01:01.001Z");
            dft.TryToSystemTime(out dt).Should().BeFalse();
            dt.Should().BeNull();
        }
    }
}