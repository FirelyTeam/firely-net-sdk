/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;
using Hl7.Fhir.Validation;

namespace Hl7.Fhir.Tests.Model
{
    [TestClass]
    public class DateTimeOperatorTests
    {
        [TestMethod]
        public void DateEqualityOperators()
        {
            Assert.IsTrue(new Date("2016-05") == new Date("2016-05"));
            Assert.IsTrue(new Date("2016-05") != new Date("2016-05-03"));
            Assert.IsTrue(new Date("2016-05-02") != new Date("2016-05-03"));
            Assert.IsTrue(new Date("2016-05-02") != null);
            Assert.IsTrue(null != new FhirDateTime("2016-05-03"));
            Assert.IsTrue(new Date("2016") != new Date());
            Assert.IsTrue(new Date() != new Date("2017"));
            Assert.IsTrue(new Date() == new Date());
            Assert.IsTrue((null as Date) == null);
        }

        [TestMethod]
        public void DateComparisonOperators()
        {
            Assert.IsTrue(new Date("2016-05") > new Date("2016-04"));
            Assert.IsFalse(new Date("2016-05") > new Date());
            Assert.IsTrue(new Date("2016-02") < new Date("2016-04"));
            Assert.IsFalse(new Date("2016-05") < new Date());
        }


        [TestMethod]
        public void DateTimeEqualityOperators()
        {
            Assert.IsTrue(new FhirDateTime(2016, 5) == new FhirDateTime(2016, 5));
            Assert.IsTrue(new FhirDateTime(2016, 5) != new FhirDateTime(2016, 5, 3));
            Assert.IsTrue(new FhirDateTime(2016, 5, 2) != new FhirDateTime(2016, 5, 3));
            Assert.IsTrue(new FhirDateTime(2016, 5, 2) != null);
            Assert.IsTrue(null != new FhirDateTime(2016, 5, 3));
            Assert.IsTrue(new FhirDateTime("2016") != new FhirDateTime());
            Assert.IsTrue(new FhirDateTime() != new FhirDateTime("2017"));
            Assert.IsTrue(new FhirDateTime() == new FhirDateTime());
            Assert.IsTrue((null as FhirDateTime) == null);

            // Time-zone checks
            DateTimeOffset dtNow = DateTimeOffset.Now;
            DateTimeOffset dtNowUtc = dtNow.UtcDateTime;
            Assert.IsTrue(new FhirDateTime(dtNow) == new FhirDateTime(dtNowUtc));

            Assert.IsTrue(new FhirDateTime("2016-01-01T10:00:00+10:00") == new FhirDateTime("2016-01-01T00:00:00Z"));
            Assert.IsTrue(new FhirDateTime("2016-01-01T10:00:00+10:00") == new FhirDateTime("2016-01-01T06:00:00+06:00"));
            Assert.IsTrue(new FhirDateTime("2016-01-01T10:00:00+10:00") != new FhirDateTime("2016-01-01T10:00:00+06:00"));
        }

        [TestMethod]
        public void InstantEqualityOperators()
        {
            // Time-zone checks
            DateTimeOffset dtNow = DateTimeOffset.Now;
            DateTimeOffset dtNowUtc = dtNow.UtcDateTime;
            Assert.IsTrue(new Instant(dtNow) == new Instant(dtNow));
            Assert.IsTrue(new Instant(dtNow) == new Instant(dtNowUtc));
            Assert.IsTrue((null as Instant) == null);

            Assert.IsTrue(new Instant(dtNow) != new Instant(dtNow.AddDays(1)));

            Assert.IsTrue(new Instant(new FhirDateTime("2016-01-01T10:00:00+10:00").ToDateTimeOffset()) 
                != new Instant(new FhirDateTime("2016-01-01T10:00:00+06:00").ToDateTimeOffset()));

            // regular checks
            Assert.IsTrue(new Instant(dtNow) != null);
            Assert.IsTrue(null != new Instant(dtNow));

            Assert.IsFalse(new Instant(dtNow) == null);
            Assert.IsFalse(null == new Instant(dtNow));
        }

        [TestMethod]
        public void TimeEqualityOperators()
        {
            Assert.IsTrue(new Time("00:00:00") == new Time("00:00:00"));
            Assert.IsTrue(new Time("12:00:00") != new Time("00:00:00"));
            Assert.IsTrue(new Time("12:00:00") != null);
            Assert.IsTrue(null != new Time("12:00:00"));
            Assert.IsTrue(new Time("12:00:00") != new Time());
            Assert.IsTrue(new Time() != new Time("12:00:00"));
            Assert.IsTrue(new Time() == new Time());
            Assert.IsTrue((null as Time) == null);
        }
    }
}
