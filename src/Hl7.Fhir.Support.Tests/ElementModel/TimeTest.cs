/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.ElementModel.Tests
{
    [TestClass]
    public class TimeTest
    {
        [TestMethod]
        public void TimeConstructor()
        {
            accept("12:34:44.123456+02:00", 12, 34, 44, 123, new TimeSpan(2, 0, 0));
            accept("12:34:44.1+02:00", 12, 34, 44, 100, new TimeSpan(2, 0, 0));
            accept("12:34:44+02:00", 12, 34, 44, null, new TimeSpan(2, 0, 0));
            accept("12:34:44Z", 12, 34, 44, null, TimeSpan.Zero);
            accept("12:34:44+00:00", 12, 34, 44, null, TimeSpan.Zero);
            accept("12:34:44", 12, 34, 44, null, null);

            accept("12:34Z", 12, 34, null, null, TimeSpan.Zero);
            accept("12:34", 12, 34, null, null, null);
            accept("12", 12, null, null, null, null);
            accept("12Z", 12, null, null, null, TimeSpan.Zero);
            accept("12-04:30", 12, null, null, null, new TimeSpan(-4, -30, 0));

            reject("");
            reject("+05:00");
            reject("Z");
            reject("12:34.1234");
            reject("Hi12:34:44");
            reject("12:34:44there");
            reject("12:34:44+A");
            reject("12:34:44+345:432");
            reject("92:34:44");
            reject("12:34:AM");

            static void accept(string testValue, int? h, int? m, int? s, int? ms, TimeSpan? o)
            {
                Assert.IsTrue(P.Time.TryParse(testValue, out P.Time parsed), "TryParse");
                Assert.AreEqual(h, parsed.Hours, "hours");
                Assert.AreEqual(m, parsed.Minutes, "minutes");
                Assert.AreEqual(s, parsed.Seconds, "seconds");
                Assert.AreEqual(ms, parsed.Millis, "millis");
                Assert.AreEqual(o, parsed.Offset, "offset");
                Assert.AreEqual(testValue, parsed.ToString(), "ToString");
            }

            static void reject(string testValue)
            {
                Assert.IsFalse(P.Time.TryParse(testValue, out _));
            }
        }

        [TestMethod]
        public void ToDateTimeOffset()
        {
            var plusOne = new TimeSpan(1, 0, 0);
            var plusTwo = new TimeSpan(2, 0, 0);

            var pt = P.Time.Parse("13:45:56");
            var dto = pt.ToDateTimeOffset(2019, 7, 23, plusOne);
            Assert.AreEqual(2019, dto.Year);
            Assert.AreEqual(7, dto.Month);
            Assert.AreEqual(23, dto.Day);
            Assert.AreEqual(13, dto.Hour);
            Assert.AreEqual(45, dto.Minute);
            Assert.AreEqual(56, dto.Second);
            Assert.AreEqual(plusOne, dto.Offset);

            pt = P.Time.Parse("13:45:56.456+02:00");
            dto = pt.ToDateTimeOffset(2019, 7, 23, plusOne);
            Assert.AreEqual(13, dto.Hour);
            Assert.AreEqual(45, dto.Minute);
            Assert.AreEqual(56, dto.Second);
            Assert.AreEqual(456, dto.Millisecond);
            Assert.AreEqual(plusTwo, dto.Offset);

            pt = P.Time.Parse("13+02:00");
            dto = pt.ToDateTimeOffset(2019, 7, 23, plusOne);
            Assert.AreEqual(13, dto.Hour);
            Assert.AreEqual(0, dto.Minute);
            Assert.AreEqual(0, dto.Second);
            Assert.AreEqual(plusTwo, dto.Offset);
        }


        [TestMethod]
        public void GetNow()
        {
            var now = P.Time.Now();
            var now2 = DateTimeOffset.Now;

            Assert.AreEqual(now2.Hour, now.Hours);
            Assert.AreEqual(now2.Minute, now.Minutes);
            // Assert.AreEqual(now2.Second, now.Seconds);  // well, maybe not to avoid random CI build/test failures
            Assert.AreEqual(P.DateTimePrecision.Fraction, now.Precision);
            Assert.IsFalse(now.HasOffset);

            now = P.Time.Now(includeOffset: true);
            Assert.IsTrue(now.HasOffset);
        }

        [TestMethod]
        public void FromDateTimeOffset()
        {
            var plusOne = new TimeSpan(1, 0, 0);

            var dto = new DateTimeOffset(2019, 7, 23, 13, 45, 56, 567, plusOne);
            var pt = P.Time.FromDateTimeOffset(dto);
            Assert.AreEqual(13, pt.Hours);
            Assert.AreEqual(45, pt.Minutes);
            Assert.AreEqual(56, pt.Seconds);
            Assert.AreEqual(567, pt.Millis);
            Assert.IsNull(pt.Offset);

            pt = P.Time.FromDateTimeOffset(dto, includeOffset: true);
            Assert.AreEqual(13, pt.Hours);
            Assert.AreEqual(45, pt.Minutes);
            Assert.AreEqual(56, pt.Seconds);
            Assert.AreEqual(567, pt.Millis);
            Assert.AreEqual(plusOne, pt.Offset);

            pt = P.Time.FromDateTimeOffset(dto, prec: P.DateTimePrecision.Hour, includeOffset: true);
            Assert.AreEqual(13, pt.Hours);
            Assert.IsNull(pt.Minutes);
            Assert.AreEqual(P.DateTimePrecision.Hour, pt.Precision);
        }
    }
}