using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.ElementModel.Tests
{
    [TestClass]
    public class DateTest
    {
        [TestMethod]
        public void DateConstructor()
        {
            accept("2010", 2010, null, null, P.DateTimePrecision.Year, null);
            accept("2010-12", 2010, 12, null, P.DateTimePrecision.Month, null);
            accept("2010-08-12", 2010, 08, 12, P.DateTimePrecision.Day, null);
            accept("2010-08-12+02:00", 2010, 08, 12, P.DateTimePrecision.Day, new TimeSpan(2, 0, 0));
            accept("2010-08-12+00:00", 2010, 08, 12, P.DateTimePrecision.Day, TimeSpan.Zero);

            reject("");
            reject("+02:00");
            reject("12-10");
            reject("12");
            reject("Test2010-08-12");
            reject("2010-08-12Test");
            reject("2010-2-2");
            reject("2010-02-4");
            reject("2010-2-04");
        }

        void accept(string testInput, int? y, int? m, int? d, P.DateTimePrecision? p, TimeSpan? o)
        {
            Assert.IsTrue(P.Date.TryParse(testInput, out P.Date parsed), "TryParse");
            Assert.AreEqual(y, parsed.Years, "years");
            Assert.AreEqual(m, parsed.Months, "months");
            Assert.AreEqual(d, parsed.Days, "days");
            Assert.AreEqual(o, parsed.Offset, "offset");
            Assert.AreEqual(p, parsed.Precision, "precision");
            Assert.AreEqual(testInput, parsed.ToString(), "ToString");
        }

        void reject(string testValue)
        {
            Assert.IsFalse(P.Date.TryParse(testValue, out _));
        }

        [TestMethod]
        public void GetToday()
        {
            var today = P.Date.Today();
            var today2 = DateTimeOffset.Now;   // just don't run this unit test a split second before midnight

            Assert.AreEqual(today2.Year, today.Years);
            Assert.AreEqual(today2.Month, today.Months);
            Assert.AreEqual(today2.Day, today.Days);
            Assert.AreEqual(P.DateTimePrecision.Day, today.Precision);
            Assert.IsFalse(today.HasOffset);

            today = P.Date.Today(includeOffset: true);
            Assert.IsTrue(today.HasOffset);
        }

        [TestMethod]
        public void ToDateTimeOffset()
        {
            var plusOne = new TimeSpan(1, 0, 0);
            var plusTwo = new TimeSpan(2, 0, 0);

            var partialDate = P.Date.Parse("2010-06-04");
            var dateTimeOffset = partialDate.ToDateTimeOffset(12, 3, 4, 5, plusOne);
            Assert.AreEqual(2010, dateTimeOffset.Year);
            Assert.AreEqual(06, dateTimeOffset.Month);
            Assert.AreEqual(04, dateTimeOffset.Day);
            Assert.AreEqual(12, dateTimeOffset.Hour);
            Assert.AreEqual(3, dateTimeOffset.Minute);
            Assert.AreEqual(4, dateTimeOffset.Second);
            Assert.AreEqual(5, dateTimeOffset.Millisecond);
            Assert.AreEqual(plusOne, dateTimeOffset.Offset);

            partialDate = P.Date.Parse("2010-06-04+02:00");
            dateTimeOffset = partialDate.ToDateTimeOffset(12, 3, 4, 5, plusOne);
            Assert.AreEqual(2010, dateTimeOffset.Year);
            Assert.AreEqual(06, dateTimeOffset.Month);
            Assert.AreEqual(04, dateTimeOffset.Day);
            Assert.AreEqual(12, dateTimeOffset.Hour);
            Assert.AreEqual(3, dateTimeOffset.Minute);
            Assert.AreEqual(4, dateTimeOffset.Second);
            Assert.AreEqual(5, dateTimeOffset.Millisecond);
            Assert.AreEqual(plusTwo, dateTimeOffset.Offset);

            partialDate = P.Date.Parse("2010-06");
            dateTimeOffset = partialDate.ToDateTimeOffset(12, 3, 4, 5, plusOne);
            Assert.AreEqual(2010, dateTimeOffset.Year);
            Assert.AreEqual(06, dateTimeOffset.Month);
            Assert.AreEqual(1, dateTimeOffset.Day);
            Assert.AreEqual(12, dateTimeOffset.Hour);
            Assert.AreEqual(3, dateTimeOffset.Minute);
            Assert.AreEqual(4, dateTimeOffset.Second);
            Assert.AreEqual(5, dateTimeOffset.Millisecond);
            Assert.AreEqual(plusOne, dateTimeOffset.Offset);

            partialDate = P.Date.Parse("2010");
            dateTimeOffset = partialDate.ToDateTimeOffset(12, 3, 4, 5, plusOne);
            Assert.AreEqual(2010, dateTimeOffset.Year);
            Assert.AreEqual(1, dateTimeOffset.Month);
            Assert.AreEqual(1, dateTimeOffset.Day);
            Assert.AreEqual(12, dateTimeOffset.Hour);
            Assert.AreEqual(3, dateTimeOffset.Minute);
            Assert.AreEqual(4, dateTimeOffset.Second);
            Assert.AreEqual(5, dateTimeOffset.Millisecond);
            Assert.AreEqual(plusOne, dateTimeOffset.Offset);
        }

        [TestMethod]
        public void FromDateTimeOffset()
        {
            var plusOne = new TimeSpan(1, 0, 0);

            var dateTimeOffset = new DateTimeOffset(2019, 7, 23, 13, 45, 56, 567, plusOne);
            var partialDate = P.Date.FromDateTimeOffset(dateTimeOffset);
            Assert.AreEqual(2019, partialDate.Years);
            Assert.AreEqual(7, partialDate.Months);
            Assert.AreEqual(23, partialDate.Days);
            Assert.IsNull(partialDate.Offset);

            partialDate = P.Date.FromDateTimeOffset(dateTimeOffset, includeOffset: true);
            Assert.AreEqual(2019, partialDate.Years);
            Assert.AreEqual(7, partialDate.Months);
            Assert.AreEqual(23, partialDate.Days);
            Assert.AreEqual(plusOne, partialDate.Offset);

            partialDate = P.Date.FromDateTimeOffset(dateTimeOffset, prec: P.DateTimePrecision.Year, includeOffset: true);
            Assert.AreEqual(2019, partialDate.Years);
            Assert.IsNull(partialDate.Months);
            Assert.AreEqual(P.DateTimePrecision.Year, partialDate.Precision);
            Assert.AreEqual(plusOne, partialDate.Offset);
        }

    }
}