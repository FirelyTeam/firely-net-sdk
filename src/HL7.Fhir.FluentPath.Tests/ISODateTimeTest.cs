/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Sprache;
using Hl7.Fhir.FluentPath;

namespace Hl7.Fhir.Tests.FhirPath
{
    [TestClass]
#if PORTABLE45
	public class PortableISODateTimeTest
#else
    public class ISODateTimeTest
#endif
    {
        [TestMethod]
        public void DateTimeConstructor()
        {
            PartialDateTime.Parse("2012-03");
            PartialDateTime.Parse("2012-03-04");
            PartialDateTime.Parse("2012-03-04T12:34:34+02:00");
            PartialDateTime.Parse("2012-03-04T12:34:34Z");

            PartialDateTime pd;
            Assert.IsTrue(PartialDateTime.TryParse("2012-03", out pd));
            Assert.AreEqual(pd, PartialDateTime.Parse("2012-03"));
            Assert.AreEqual("2012-03", pd.ToString());

            Assert.IsFalse(PartialDateTime.TryParse("2012-03T12:34", out pd));
            Assert.IsFalse(PartialDateTime.TryParse("20120304", out pd));
            Assert.IsTrue(PartialDateTime.TryParse("2012-03-04T12:04:45", out pd));     //FHIR does not allow this, ISO8601 does.     
            Assert.IsTrue(PartialDateTime.TryParse("2012-03-04T12:04:45Z", out pd));
            Assert.IsFalse(PartialDateTime.TryParse("T12:04:45Z", out pd));
            Assert.IsFalse(PartialDateTime.TryParse("12:04:45Z", out pd));

            Assert.IsTrue(PartialDateTime.Parse("2012-03-04") > PartialDateTime.Parse("2012-03-01"));
            Assert.IsTrue(PartialDateTime.Parse("2012-03-04T13:00:00Z") > PartialDateTime.Parse("2012-03-04T12:00:00Z"));
            Assert.IsTrue(PartialDateTime.Parse("2012-03-04T13:00:00Z") < PartialDateTime.Parse("2012-03-04T18:00:00+02:00"));

            Assert.AreEqual(PartialDateTime.Today().ToString(), PartialDateTime.FromDateTime(DateTime.Today).ToString().Substring(0,10));
            Assert.AreEqual(PartialDateTime.Now().ToString().Substring(0, 19), PartialDateTime.FromDateTime(DateTimeOffset.Now).ToString().Substring(0, 19));
        }

        [TestMethod]
        public void TimeConstructor()
        {
            Time.Parse("T12:34:44+02:00");
            Time.Parse("T12:34:44");
            Time.Parse("T12:34:44Z");

            Time pd;
            Assert.IsTrue(Time.TryParse("T12:34:44Z", out pd));
            Assert.AreEqual(pd, Time.Parse("T12:34:44Z"));
            Assert.AreEqual("T12:34:44Z", pd.ToString());

            Assert.IsFalse(Time.TryParse("12:34:44Z", out pd));
            Assert.IsFalse(Time.TryParse("T12:34", out pd));
            
            Assert.IsTrue(Time.Parse("T12:34:00+00:00") > Time.Parse("T12:33:55+00:00"));
            Assert.IsTrue(Time.Parse("T13:00:00+00:00") < Time.Parse("T15:01:00+02:00"));
            Assert.IsTrue(Time.Parse("T13:00:00+00:00") > Time.Parse("T14:59:00+02:00"));
        }
    }
}