/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model.Primitives;
using System;
using Xunit;

namespace Hl7.FhirPath.Tests
{

    public class ISODateTimeTest
    {
        [Fact]
        public void DateTimeConstructor()
        {
            PartialDateTime.Parse("2012-03");
            PartialDateTime.Parse("2012-03-04");
            PartialDateTime.Parse("2012-03-04T12:34:34+02:00");
            PartialDateTime.Parse("2012-03-04T12:34:34Z");

            Assert.True(PartialDateTime.TryParse("2012-03", out PartialDateTime pd));
            Assert.Equal(pd, PartialDateTime.Parse("2012-03"));
            Assert.Equal("2012-03", pd.ToString());

            Assert.False(PartialDateTime.TryParse("2012-03T12:34", out pd));
            Assert.False(PartialDateTime.TryParse("20120304", out pd));
            Assert.True(PartialDateTime.TryParse("2012-03-04T12:04:45", out pd));     //FHIR does not allow this, ISO8601 does.     
            Assert.True(PartialDateTime.TryParse("2012-03-04T12:04:45Z", out pd));
            Assert.False(PartialDateTime.TryParse("T12:04:45Z", out pd));
            Assert.False(PartialDateTime.TryParse("12:04:45Z", out pd));

            Assert.True(PartialDateTime.Parse("2012-03-04") > PartialDateTime.Parse("2012-03-01"));

            Assert.Equal(PartialDateTime.Today().ToString(), PartialDateTime.FromDateTime(DateTimeOffset.Now).ToString().Substring(0,10));
            Assert.Equal(PartialDateTime.Now().ToString().Substring(0, 19), PartialDateTime.FromDateTime(DateTimeOffset.Now).ToString().Substring(0, 19));
        }

        [Fact]
        public void TimeConstructor()
        {
            PartialTime.Parse("12:34:44+02:00");
            PartialTime.Parse("12:34:44");
            PartialTime.Parse("12:34:44Z");

            Assert.True(PartialTime.TryParse("12:34:44Z", out PartialTime pd));
            Assert.Equal(pd, PartialTime.Parse("12:34:44Z"));
            Assert.Equal("12:34:44Z", pd.ToString());

            Assert.False(PartialTime.TryParse("92:34:44Z", out pd));
        }

        [Fact]
        public void TimeComparison()
        {
            Assert.True(PartialDateTime.Parse("2012-03-04T13:00:00Z") > PartialDateTime.Parse("2012-03-04T12:00:00Z"));
            Assert.True(PartialDateTime.Parse("2012-03-04T13:00:00Z") < PartialDateTime.Parse("2012-03-04T18:00:00+02:00"));

            Assert.True(PartialTime.Parse("12:34:00+00:00") > PartialTime.Parse("12:33:55+00:00"));
            Assert.True(PartialTime.Parse("13:00:00+00:00") < PartialTime.Parse("15:01:00+02:00"));
            Assert.True(PartialTime.Parse("13:00:00+00:00") > PartialTime.Parse("14:59:00+02:00"));
        }

        [Fact]
        public void TimeEquality()
        {
            Assert.True(PartialDateTime.Parse("2015-01-01") == PartialDateTime.Parse("2015-01-01"));
            Assert.True(PartialDateTime.Parse("2015-01-01") != PartialDateTime.Parse("2015-01"));
            Assert.True(PartialDateTime.Parse("2015-01-01T13:40:50+02:00") == PartialDateTime.Parse("2015-01-01T13:40:50+02:00"));
            Assert.True(PartialDateTime.Parse("2015-01-01T13:40:50+00:00") == PartialDateTime.Parse("2015-01-01T13:40:50Z"));
            Assert.True(PartialDateTime.Parse("2015-01-01T13:40:50+00:10") != PartialDateTime.Parse("2015-01-01T13:40:50Z"));
            Assert.True(PartialDateTime.Parse("2015-01-01T13:40:50+00:10") != PartialDateTime.Parse("2015-01-01"));

            Assert.True(PartialTime.Parse("13:45:02Z") == PartialTime.Parse("13:45:02+00:00"));
            Assert.True(PartialTime.Parse("13:45:02+01:00") == PartialTime.Parse("13:45:02+01:00"));
            Assert.True(PartialTime.Parse("13:45:02+00:00") != PartialTime.Parse("13:45:02+01:00"));
        }

        [Fact]
        public void CheckOrdering()
        {
            Assert.Equal(1, PartialDateTime.Parse("2012-03-04T13:00:00Z").CompareTo(PartialDateTime.Parse("2012-03-04T12:00:00Z")));
            Assert.Equal(-1, PartialDateTime.Parse("2012-03-04T13:00:00Z").CompareTo(PartialDateTime.Parse("2012-03-04T18:00:00+02:00")));
            Assert.Equal(0,  PartialDateTime.Parse("2015-01-01").CompareTo(PartialDateTime.Parse("2015-01-01")));

            Assert.Equal(1, PartialTime.Parse("12:34:00+00:00").CompareTo(PartialTime.Parse("12:33:55+00:00")));
            Assert.Equal(-1, PartialTime.Parse("13:00:00+00:00").CompareTo(PartialTime.Parse("15:01:00+02:00")));
            Assert.Equal(0, PartialTime.Parse("13:45:02+01:00").CompareTo(PartialTime.Parse("13:45:02+01:00")));
        }
    }
}