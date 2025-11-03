/* 
 * Copyright (c) 2019, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

// To introduce the DSTU2 FHIR specification
// extern alias dstu2;

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.ElementModel.Types;
using Hl7.FhirPath.Functions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.FhirPath.Tests
{
    [TestClass]
    public class ConversionsTests
    {
        private List<Any> create(params object[] data) =>
            data.Select(o => Any.Convert(o)).ToList();
        //data.Select(o => Any.Convert(o)).Cast<ICqlConvertible>().ToList();

        [TestMethod]
        public void ConvertToBoolean()
        {
            var areTrue = create(true, "TruE", "Yes", "y", "t", "1", "1.0", 1, 1L, 1m, 1.0m);
            areTrue.ForEach(o => Assert.IsTrue(o.ToBoolean().Value));
            areTrue.ForEach(o => Assert.IsTrue(o.ConvertsToBoolean()));

            var areFalse = create(false, "fAlse", "nO", "N", "f", "0", "0.0", 0, 0L, 0m, 0.0m);
            areFalse.ForEach(o => Assert.IsFalse(o.ToBoolean().Value));
            areFalse.ForEach(o => Assert.IsTrue(o.ConvertsToBoolean()));

            var wrong = create("truex", "falsx", "not", "tr", -8, -1L, 2L, 2.0m, 1.1m);
            wrong.ForEach(o => Assert.IsNull(o.ToBoolean()));
            wrong.ForEach(o => Assert.IsFalse(o.ConvertsToBoolean()));

            Assert.AreEqual(true, ElementNode.ForPrimitive(true).Scalar("'TruE'.toBoolean()"));
            Assert.IsNull(ElementNode.ForPrimitive(true).Scalar("{}.toBoolean()"));
        }

        [TestMethod]
        public void ConvertToInteger()
        {
            var inputs = create(1, "2", -4, "-5", "+4", 1234L, true, false);
            var vals = new[] { 1, 2, -4, -5, 4, 1234, 1, 0 };

            inputs.Zip(vals, (i, v) => (i, v))
                .ToList()
                .ForEach(c => Assert.AreEqual(c.i.ToInteger(), c.v));
            inputs.ToList().ForEach(c => Assert.IsTrue(c.ConvertsToInteger()));

            var wrong = create("2.4", "++6", "2,6", "no", "false", int.MaxValue + 100L, DateTimeOffset.Now);
            wrong.ForEach(c => Assert.IsNull(c.ToInteger()));
            wrong.ForEach(c => Assert.IsFalse(c.ConvertsToInteger()));

            Assert.AreEqual(2, ElementNode.ForPrimitive(true).Scalar("'2'.toInteger()"));
            Assert.IsNull(ElementNode.ForPrimitive(true).Scalar("{}.toInteger()"));
        }

        [TestMethod]
        public void ConvertToLong()
        {
            var inputs = create(1, "2", -4, "-5", "+4", 1234L, true, false);
            var vals = new[] { 1L, 2L, -4L, -5L, 4L, 1234L, 1L, 0L };

            inputs.Zip(vals, (i, v) => (i, v))
                .ToList()
                .ForEach(c => Assert.AreEqual(c.i.ToLong(), c.v));
            inputs.ToList().ForEach(c => Assert.IsTrue(c.ConvertsToLong()));

            var wrong = create("2.4", "++6", "2,6", "no", "false", DateTimeOffset.Now);
            wrong.ForEach(c => Assert.IsNull(c.ToLong()));
            wrong.ForEach(c => Assert.IsFalse(c.ConvertsToLong()));

            Assert.AreEqual(2L, ElementNode.ForPrimitive(true).Scalar("'2'.toLong()"));
            Assert.IsNull(ElementNode.ForPrimitive(true).Scalar("{}.toLong()"));
        }


        [TestMethod]
        public void ConvertToDecimal()
        {
            var inputs = create(1, 1L, 2m, "2", "3.14", -4.4m, true, false);
            var vals = new[] { 1m, 1m, 2m, 2m, 3.14m, -4.4m, 1m, 0m };

            inputs.Zip(vals, (i, v) => (i, v))
                .ToList()
                .ForEach(c => Assert.AreEqual(c.i.ToDecimal(), c.v));
            inputs.ToList().ForEach(c => Assert.IsTrue(c.ConvertsToDecimal()));

            var wrong = create("hi", "++6", "2,6", "no", "false", DateTimeOffset.Now);
            wrong.ForEach(c => Assert.IsNull(c.ToDecimal()));
            wrong.ForEach(c => Assert.IsFalse(c.ConvertsToDecimal()));

            Assert.AreEqual(3.14m, ElementNode.ForPrimitive(true).Scalar("'3.14'.toDecimal()"));
            Assert.IsNull(ElementNode.ForPrimitive(true).Scalar("{}.toDecimal()"));
        }

        [TestMethod]
        public void ConvertToQuantity()
        {
            var inputs = create(5, 75L, 75.6m, "30 'wk'", false, true,
                            new P.Quantity(80.0m, "kg"));
            var vals = new[] {new P.Quantity(5m), new P.Quantity(75m), new P.Quantity(75.6m, P.Quantity.UCUM_UNIT),
                    new P.Quantity(30m,"wk"), new P.Quantity(0.0m),
                        new P.Quantity(1.0m), new P.Quantity(80m, "kg") };

            inputs.Zip(vals, (i, v) => (i, v))
                .ToList()
                .ForEach(c => Assert.AreEqual(c.v, c.i.ToQuantity()));
            inputs.ToList().ForEach(c => Assert.IsTrue(c.ConvertsToQuantity()));

            var wrong = create("hi", "++6", "2,6", "no", "false",DateTimeOffset.Now);
            wrong.ForEach(c => Assert.IsNull(c.ToQuantity()));
            wrong.ForEach(c => Assert.IsFalse(c.ConvertsToQuantity()));

            Assert.AreEqual(new P.Quantity(3m,"wk"), ElementNode.ForPrimitive(true).Scalar("'3 \\'wk\\''.toQuantity()"));
            Assert.IsNull(ElementNode.ForPrimitive(true).Scalar("{}.toQuantity()"));
        }

        [TestMethod]
        public void ConvertToString()
        {
            var inputs = create("hoi", 3, 4L, 3.4m, true, false, P.Time.Parse("15:47:00+01:00"),
                P.DateTime.Parse("2019-01-11T15:47:00+01:00"), P.Date.Parse("1972-11-30"), new P.Quantity(4m, "kg"));
            var vals = new[] { "hoi", "3", "4", "3.4", "true", "false", "15:47:00+01:00", "2019-01-11T15:47:00+01:00",
                    "1972-11-30", "4 'kg'"};

            inputs.Zip(vals, (i, v) => (i, v))
                .ToList()
                .ForEach(c => Assert.AreEqual(c.v, c.i.ToStringRepresentation()));
            inputs.ToList().ForEach(c => Assert.IsTrue(c.ConvertsToString()));

            Assert.AreEqual("true", ElementNode.ForPrimitive(true).Scalar("true.toString()"));
            Assert.IsNull(ElementNode.ForPrimitive(true).Scalar("{}.toString()"));
        }


        [TestMethod]
        public void ConvertToDate()
        {
            var then = P.DateTime.Parse("2019-01-11T15:47:00+01:00");
            var then2 = P.DateTime.Parse("2019-01-11T15:47:00");
            var bd = P.Date.Parse("1972-11-30");
            var inputs = create("1972-11", bd, then, then2);
            var vals = new[] { P.Date.Parse("1972-11"), bd, P.Date.Parse("2019-01-11+01:00"), P.Date.Parse("2019-01-11")  };

            inputs.Zip(vals, (i, v) => (i, v))
                .ToList()
                .ForEach(c => Assert.AreEqual(c.i.ToDate(), c.v));
            inputs.ToList().ForEach(c => Assert.IsTrue(c.ConvertsToDate()));

            var wrong = create("hi", 2.6m, false, P.Time.Parse("16:05:49"));
            wrong.ForEach(c => Assert.IsNull(c.ToDate()));
            wrong.ForEach(c => Assert.IsFalse(c.ConvertsToDate()));

            Assert.AreEqual(bd, ElementNode.ForPrimitive(true).Scalar("'1972-11-30'.toDate()"));
            Assert.IsNull(ElementNode.ForPrimitive(true).Scalar("{}.toDate()"));
        }


        [TestMethod]
        public void ConvertToDateTime()
        {
            var then = P.DateTime.Parse("2019-01-11T15:47:00+01:00");
            var then2 = P.DateTime.Parse("2019-01-11T15:47:00");
            var bd = P.Date.Parse("1972-11-30");
            var inputs = create("1972-11", bd, then, then2);
            var vals = new[] { P.DateTime.Parse("1972-11"), P.DateTime.Parse("1972-11-30"),
                then, then2 };

            inputs.Zip(vals, (i, v) => (i, v))
                .ToList()
                .ForEach(c => Assert.AreEqual(c.i.ToDateTime(), c.v));
            inputs.ToList().ForEach(c => Assert.IsTrue(c.ConvertsToDateTime()));

            var wrong = create("hi", 2.6m, false, P.Time.Parse("16:05:49"));
            wrong.ForEach(c => Assert.IsNull(c.ToDateTime()));
            wrong.ForEach(c => Assert.IsFalse(c.ConvertsToDateTime()));

            Assert.AreEqual(P.DateTime.Parse("1972-11-30T15:00:01"), ElementNode.ForPrimitive(true).Scalar("'1972-11-30T15:00:01'.toDateTime()"));
            Assert.IsNull(ElementNode.ForPrimitive(true).Scalar("{}.toDateTime()"));
        }


        [TestMethod]
        public void ConvertToTime()
        {
            var now = P.Time.Parse("15:47:00+01:00");
            var inputs = create(now, "12:05:45");
            var vals = new[] { now, P.Time.Parse("12:05:45") };

            inputs.Zip(vals, (i, v) => (i, v))
                .ToList()
                .ForEach(c => Assert.AreEqual(c.i.ToTime(), c.v));
            inputs.ToList().ForEach(c => Assert.IsTrue(c.ConvertsToTime()));

            var wrong = create(new DateTimeOffset(2019, 1, 11, 15, 47, 00, new TimeSpan(1, 0, 0)),
                "hi", 2.6m, false);
            wrong.ForEach(c => Assert.IsNull(c.ToTime()));
            wrong.ForEach(c => Assert.IsFalse(c.ConvertsToTime()));

            Assert.AreEqual(P.Time.Parse("15:00:01"), ElementNode.ForPrimitive(true).Scalar("'15:00:01'.toTime()"));
            Assert.IsNull(ElementNode.ForPrimitive(true).Scalar("{}.toTime()"));
        }

        [TestMethod]
        public void CheckTypeDetermination()
        {
            var values = ElementNode.CreateList(1, 1L, true, "hi", 4.0m, 4.0f, P.DateTime.Now());

            Test.IsInstanceOfType(values.Item(0).Single().Value, typeof(int));
            Test.IsInstanceOfType(values.Item(0).Single().Value, typeof(long));
            Test.IsInstanceOfType(values.Item(1).Single().Value, typeof(bool));
            Test.IsInstanceOfType(values.Item(2).Single().Value, typeof(string));
            Test.IsInstanceOfType(values.Item(3).Single().Value, typeof(decimal));
            Test.IsInstanceOfType(values.Item(4).Single().Value, typeof(decimal));
            Test.IsInstanceOfType(values.Item(5).Single().Value, typeof(P.DateTime));
        }


        [TestMethod]
        public void TestItemSelection()
        {
            var values = ElementNode.CreateList(1L, 2, 3L, 4, 5, 6, 7);

            Assert.AreEqual(1L, values.Item(0).Single().Value);
            Assert.AreEqual(2, values.Item(1).Single().Value);
            Assert.AreEqual(3L, values.Item(2).Single().Value);
            Assert.AreEqual(1L, values.First().Value);
            Assert.IsFalse(values.Item(100).Any());
        }

    }
}