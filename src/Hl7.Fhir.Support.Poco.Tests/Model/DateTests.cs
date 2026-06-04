/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Hl7.Fhir.Tests.Model
{
    [TestClass]
    public class DateTests
    {
        [TestMethod]
        public void DateHandling()
        {
            var dt = new Date(2010, 1, 1);
            Assert.AreEqual("2010-01-01", dt.Value);

            var dt2 = new Date(1972, 11, 30);
            dt2.Value.Should().Be("1972-11-30");

            var dtNoDay = new Date(2014, 12);
            dtNoDay.Value.Should().Be("2014-12");

            var stamp = new DateTimeOffset(1972, 11, 30, 15, 10, 0, TimeSpan.Zero);
            dt = Date.FromDateTimeOffset(stamp);
            dt.Value.Should().Be("1972-11-30");
        }

        [TestMethod]
        public void TryToDateTimeOffset()
        {
            var fdt = new Date(2021, 3, 18);
            fdt.TryToDateTimeOffset(out var dto1).Should().BeTrue();
            Assert.AreEqual("2021-03-18T00:00:00.0000000+00:00", dto1.ToString("o"));

            fdt = new Date(2021, 32, 18);
            fdt.TryToDateTimeOffset(out var _).Should().BeFalse();

            fdt = new Date("2021-03-18+01:00");
            fdt.TryToDateTimeOffset(out var _).Should().BeFalse();

            fdt = new Date("2021-32-18");
            fdt.TryToDateTimeOffset(out var _).Should().BeFalse();
        }

        [TestMethod]
        public void TodayTests()
        {
            var todayLocal = Date.Today();
            Assert.AreEqual(DateTimeOffset.Now.ToString("yyy-MM-dd"), todayLocal.Value);

            var todayUtc = Date.UtcToday();
            Assert.AreEqual(DateTimeOffset.UtcNow.ToString("yyy-MM-dd"), todayUtc.Value);
        }

        [TestMethod]
        public void UpdatesCachedValue()
        {
            var dft = new Date(2023, 07, 11);
            dft.TryToDateTimeOffset(out var dto).Should().BeTrue();
            dft.TryToDateTimeOffset(out var dto2).Should().BeTrue();
            dto.Equals(dto2).Should().BeTrue();

            dft.Value = "2023-07-11";
            dft.TryToDateTimeOffset(out dto).Should().BeTrue();
            dto.Day.Should().Be(11);
            dft.TryToDateTimeOffset(out dto2).Should().BeTrue();
            dto.Equals(dto2).Should().BeTrue();

            dft.JsonValue = "2023-07-11";
            dft.TryToDateTimeOffset(out dto).Should().BeTrue();
            dto.Month.Should().Be(7);
            dft.TryToDateTimeOffset(out dto2).Should().BeTrue();
            dto.Equals(dto2).Should().BeTrue();

            dft.Value = null;
            dft.TryToDateTimeOffset(out _).Should().BeFalse();
            Assert.Throws<InvalidOperationException>(() => dft.ToDateTimeOffset());
        }

        [TestMethod]
        public void ToDateTimeOffsetThrowsInvalidFormat()
        {
            var dft = new Date("T45:45:56");

            Assert.Throws<CodedValidationException>(() => dft.ToDateTimeOffset());

            dft.TryToDateTimeOffset(out _).Should().BeFalse();
        }

        [TestMethod]
        public void CanConvertToDateTime()
        {
            var dft = new Date(2023, 07, 11);
            dft.TryToSystemDate(out var dt).Should().BeTrue();
            dt.Days.Should().Be(11);
            dt.Precision.Should().Be(ElementModel.Types.DateTimePrecision.Day);

            dft = new Date(2023, 7);
            dft.TryToSystemDate(out dt).Should().BeTrue();
            dt.Days.Should().BeNull();
            dt.Precision.Should().Be(ElementModel.Types.DateTimePrecision.Month);

            dft = new Date(null);
            dft.TryToSystemDate(out dt).Should().BeFalse();
            dt.Should().BeNull();
        }
    }
}