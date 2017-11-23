using Hl7.Fhir.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Hl7.Fhir.Support.Tests.Serialization
{
    public class PrimitiveTypeConverterTest
    {
        [Theory]
        [MemberData(nameof(GetTestdata))]
        public void ConvertToDateTimeOffsetTest(string input, DateTimeOffset expected, bool expectException)
        {
            try
            {
                var result = PrimitiveTypeConverter.ConvertTo<DateTimeOffset>(input);
                Assert.NotNull(result);
                Assert.Equal(expected, result);

            }
            catch (Exception)
            {
                if (!expectException) Assert.True(false);
            }
        }

        public static IEnumerable<object[]> GetTestdata()
        {
            return new List<object[]>()
            {
                new object[]{ "0001", new DateTimeOffset(1,1,1,0,0,0,new TimeSpan()), false },
                new object[]{ "nodate", null, true },
                new object[]{ "2018", new DateTimeOffset(2018,1,1,0,0,0,new TimeSpan()), false },
                new object[]{ "0001-01", new DateTimeOffset(1,1,1,0,0,0,new TimeSpan()), false },
                new object[]{ "0001-01-01", new DateTimeOffset(1,1,1,0,0,0,new TimeSpan()), false },
                new object[]{ "0001-01-32", null, true},
                new object[]{ "0001-01-02", new DateTimeOffset(1,1,2,0,0,0,new TimeSpan()), false },
                new object[]{ "0001-01-02T00:00:00", new DateTimeOffset(new DateTime(1,1,2,0,0,0)), false }, // use localTime 
                new object[]{ "0001-01-02T00:00:00Z", new DateTimeOffset(1,1,2,0,0,0,new TimeSpan()), true },
                new object[]{ "0001-01-01T00:00:00+01:00", null, true },
            };
        }

    }
}
