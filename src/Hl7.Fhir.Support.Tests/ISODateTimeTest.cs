/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model.Primitives;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.FhirPath.Tests
{
    [TestClass]
    public class ISODateTimeTest
    {
        [TestMethod]
        public void DateTimeConstructor()
        {
            PartialDateTime.Parse("2012-03");
            PartialDateTime.Parse("2012-03-04");
            PartialDateTime.Parse("2012-03-04T12:34:34+02:00");
            PartialDateTime.Parse("2012-03-04T12:34:34Z");

            Assert.IsTrue(PartialDateTime.TryParse("2012-03", out PartialDateTime pd));
            Assert.AreEqual(pd, PartialDateTime.Parse("2012-03"));
            Assert.AreEqual("2012-03", pd.ToString());

            Assert.IsFalse(PartialDateTime.TryParse("2012-03T12:34", out pd));
            Assert.IsFalse(PartialDateTime.TryParse("20120304", out pd));
            Assert.IsTrue(PartialDateTime.TryParse("2012-03-04T12:04:45", out pd));     //FHIR does not allow this, ISO8601 does.     
            Assert.IsTrue(PartialDateTime.TryParse("2012-03-04T12:04:45Z", out pd));
            Assert.IsFalse(PartialDateTime.TryParse("T12:04:45Z", out pd));
            Assert.IsFalse(PartialDateTime.TryParse("12:04:45Z", out pd));

            Assert.IsTrue(PartialDateTime.Parse("2012-03-04") > PartialDateTime.Parse("2012-03-01"));

            Assert.AreEqual(PartialDateTime.Today().ToString(), PartialDateTime.FromDateTime(DateTimeOffset.Now).ToString().Substring(0,10));
            Assert.AreEqual(PartialDateTime.Now().ToString().Substring(0, 19), PartialDateTime.FromDateTime(DateTimeOffset.Now).ToString().Substring(0, 19));
        }

        [TestMethod]
        public void TimeConstructor()
        {
            PartialTime.Parse("12:34:44+02:00");
            PartialTime.Parse("12:34:44");
            PartialTime.Parse("12:34:44Z");

            Assert.IsTrue(PartialTime.TryParse("12:34:44Z", out PartialTime pd));
            Assert.AreEqual(pd, PartialTime.Parse("12:34:44Z"));
            Assert.AreEqual("12:34:44Z", pd.ToString());

            Assert.IsFalse(PartialTime.TryParse("92:34:44Z", out pd));
        }

        [TestMethod]
        public void TimeComparison()
        {
            Assert.IsTrue(PartialDateTime.Parse("2012-03-04T13:00:00Z") > PartialDateTime.Parse("2012-03-04T12:00:00Z"));
            Assert.IsTrue(PartialDateTime.Parse("2012-03-04T13:00:00Z") < PartialDateTime.Parse("2012-03-04T18:00:00+02:00"));

            Assert.IsTrue(PartialTime.Parse("12:34:00+00:00") > PartialTime.Parse("12:33:55+00:00"));
            Assert.IsTrue(PartialTime.Parse("13:00:00+00:00") < PartialTime.Parse("15:01:00+02:00"));
            Assert.IsTrue(PartialTime.Parse("13:00:00+00:00") > PartialTime.Parse("14:59:00+02:00"));
        }

        [TestMethod]
        public void TimeEquality()
        {
            Assert.IsTrue(PartialDateTime.Parse("2015-01-01") == PartialDateTime.Parse("2015-01-01"));
            Assert.IsTrue(PartialDateTime.Parse("2015-01-01") != PartialDateTime.Parse("2015-01"));
            Assert.IsTrue(PartialDateTime.Parse("2015-01-01T13:40:50+02:00") == PartialDateTime.Parse("2015-01-01T13:40:50+02:00"));
            Assert.IsTrue(PartialDateTime.Parse("2015-01-01T13:40:50+00:00") == PartialDateTime.Parse("2015-01-01T13:40:50Z"));
            Assert.IsTrue(PartialDateTime.Parse("2015-01-01T13:40:50+00:10") != PartialDateTime.Parse("2015-01-01T13:40:50Z"));
            Assert.IsTrue(PartialDateTime.Parse("2015-01-01T13:40:50+00:10") != PartialDateTime.Parse("2015-01-01"));

            Assert.IsTrue(PartialTime.Parse("13:45:02Z") == PartialTime.Parse("13:45:02+00:00"));
            Assert.IsTrue(PartialTime.Parse("13:45:02+01:00") == PartialTime.Parse("13:45:02+01:00"));
            Assert.IsTrue(PartialTime.Parse("13:45:02+00:00") != PartialTime.Parse("13:45:02+01:00"));
        }

        [TestMethod]
        public void CheckOrdering()
        {
            Assert.AreEqual(1, PartialDateTime.Parse("2012-03-04T13:00:00Z").CompareTo(PartialDateTime.Parse("2012-03-04T12:00:00Z")));
            Assert.AreEqual(-1, PartialDateTime.Parse("2012-03-04T13:00:00Z").CompareTo(PartialDateTime.Parse("2012-03-04T18:00:00+02:00")));
            Assert.AreEqual(0,  PartialDateTime.Parse("2015-01-01").CompareTo(PartialDateTime.Parse("2015-01-01")));

            Assert.AreEqual(1, PartialTime.Parse("12:34:00+00:00").CompareTo(PartialTime.Parse("12:33:55+00:00")));
            Assert.AreEqual(-1, PartialTime.Parse("13:00:00+00:00").CompareTo(PartialTime.Parse("15:01:00+02:00")));
            Assert.AreEqual(0, PartialTime.Parse("13:45:02+01:00").CompareTo(PartialTime.Parse("13:45:02+01:00")));
        }
    }
}