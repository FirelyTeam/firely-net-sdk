/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
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
    public class DateTimeTest
    {
        [TestMethod]
        public void DateTimeConstructor()
        {
            var plusOne = new TimeSpan(1, 0, 0);

            accept("2012", 2012);
            accept("2012Z", 2012, o: TimeSpan.Zero);
            accept("2012-03", 2012, 3);
            accept("2012-03+01:00", 2012, 3, o: plusOne);
            accept("2012-03-04", 2012, 3, 4);
            accept("2012-03-04-01:00", 2012, 3, 4, o: new TimeSpan(-1, 0, 0));
            accept("2012-03-04T12Z", 2012, 3, 4, 12, o: TimeSpan.Zero);
            accept("2012-03-04T12Z", 2012, 3, 4, 12, o: TimeSpan.Zero);
            accept("2012-03-04T12:34Z", 2012, 3, 4, 12, 34, o: TimeSpan.Zero);
            accept("2012-03-04T12:34:35", 2012, 3, 4, 12, 34, 35);
            accept("2012-03-04T12:34:35.2323+01:00", 2012, 3, 4, 12, 34, 35, 232, plusOne);

            reject("2012-03-04T");
            reject("2012-03-04TZ");
            reject("2012-3-4");
            reject("2012-03T12:34");
            reject("2012-03TT");
            reject("20120304");
            reject("2012-45-01");
            reject("2012-03-04T12:34:35+99:00");
            reject("2019-02-29");
            reject("T12:04:45Z");
            reject("12:04:45Z");

            static void accept(string testValue, int? y = default, int? mo = default, int? d = default,
                int? h = default, int? m = default, int? s = default, int? ms = default, TimeSpan? o = default)
            {
                Assert.IsTrue(P.DateTime.TryParse(testValue, out P.DateTime parsed), "TryParse");
                Assert.AreEqual(y, parsed.Years, "years");
                Assert.AreEqual(mo, parsed.Months, "months");
                Assert.AreEqual(d, parsed.Days, "days");
                Assert.AreEqual(h, parsed.Hours, "hours");
                Assert.AreEqual(m, parsed.Minutes, "minutes");
                Assert.AreEqual(s, parsed.Seconds, "seconds");
                Assert.AreEqual(ms, parsed.Millis, "millis");
                Assert.AreEqual(o, parsed.Offset, "offset");
                Assert.AreEqual(testValue, parsed.ToString(), "ToString");
            }

            static void reject(string testValue)
            {
                Assert.IsFalse(P.DateTime.TryParse(testValue, out _));
            }
        }


        [TestMethod]
        public void ToDateTimeOffset()
        {
            var plusOne = new TimeSpan(1, 0, 0);
            var plusTwo = new TimeSpan(2, 0, 0);

            var pt = P.DateTime.Parse("2019-07-23T13:45:56");
            var dto = pt.ToDateTimeOffset(plusOne);
            Assert.AreEqual(2019, dto.Year);
            Assert.AreEqual(7, dto.Month);
            Assert.AreEqual(23, dto.Day);
            Assert.AreEqual(13, dto.Hour);
            Assert.AreEqual(45, dto.Minute);
            Assert.AreEqual(56, dto.Second);
            Assert.AreEqual(plusOne, dto.Offset);

            pt = P.DateTime.Parse("2019-07-23T13:45:56.456+02:00");
            dto = pt.ToDateTimeOffset(plusOne);
            Assert.AreEqual(13, dto.Hour);
            Assert.AreEqual(45, dto.Minute);
            Assert.AreEqual(56, dto.Second);
            Assert.AreEqual(456, dto.Millisecond);
            Assert.AreEqual(plusTwo, dto.Offset);

            pt = P.DateTime.Parse("2019-07-23T13+02:00");
            dto = pt.ToDateTimeOffset(plusOne);
            Assert.AreEqual(13, dto.Hour);
            Assert.AreEqual(0, dto.Minute);
            Assert.AreEqual(0, dto.Second);
            Assert.AreEqual(plusTwo, dto.Offset);
        }

        [TestMethod]
        public void GetToday()
        {
            var today = P.DateTime.Today();
            var today2 = DateTimeOffset.Now;   // just don't run this unit test a split second before midnight

            Assert.AreEqual(today2.Year, today.Years);
            Assert.AreEqual(today2.Month, today.Months);
            Assert.AreEqual(today2.Day, today.Days);
            Assert.AreEqual(P.DateTimePrecision.Day, today.Precision);
        }

        [TestMethod]
        public void FromDateTimeOffset()
        {
            var plusOne = new TimeSpan(1, 0, 0);

            var dto = new DateTimeOffset(2019, 7, 23, 13, 45, 56, 567, plusOne);
            var pt = P.DateTime.FromDateTimeOffset(dto);

            Assert.AreEqual(2019, pt.Years);
            Assert.AreEqual(7, pt.Months);
            Assert.AreEqual(23, pt.Days);
            Assert.AreEqual(13, pt.Hours);
            Assert.AreEqual(45, pt.Minutes);
            Assert.AreEqual(56, pt.Seconds);
            Assert.AreEqual(567, pt.Millis);
            Assert.AreEqual(plusOne, pt.Offset);
        }

        [TestMethod]
        public void CanCastDateToDateTime()
        {
            var pdt = P.Date.Parse("2018-04").ToDateTime();
            Assert.AreEqual(P.DateTimePrecision.Month, pdt.Precision);
            Assert.AreEqual(2018, pdt.Years);
            Assert.AreEqual(4, pdt.Months);
            Assert.AreEqual("2018-04", pdt.ToString());
        }
    }
}