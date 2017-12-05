using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Support.Tests.Serialization
{
    [TestClass]
    public class PrimitiveTypeConverterTest
    {
        private void AssertConvertToDateTimeOffset(string input, DateTimeOffset? expected, bool expectException)
        {
            try
            {
                var result = PrimitiveTypeConverter.ConvertTo<DateTimeOffset>(input);
                Assert.IsNotNull(result);
                Assert.AreEqual(expected, result);
            }
            catch (Exception)
            {
                if (!expectException) Assert.IsTrue(false);
            }
        }

        private IEnumerable<(string input, DateTimeOffset? expected, bool expectException)> GetTestdata()
        {
            yield return ("0001", new DateTimeOffset(1, 1, 1, 0, 0, 0, new TimeSpan()), false);
            yield return ("nodate", null, true);
            yield return ("2018", new DateTimeOffset(2018, 1, 1, 0, 0, 0, new TimeSpan()), false);
            yield return ("0001-01", new DateTimeOffset(1, 1, 1, 0, 0, 0, new TimeSpan()), false);
            yield return ("0001-01-01", new DateTimeOffset(1, 1, 1, 0, 0, 0, new TimeSpan()), false);
            yield return ("0001-01-32", null, true);
            yield return ("0001-01-02", new DateTimeOffset(1, 1, 2, 0, 0, 0, new TimeSpan()), false);
            yield return ("0001-01-02T00:00:00", new DateTimeOffset(new DateTime(1, 1, 2, 0, 0, 0)), false); // use localTime 
            yield return ("0001-01-02T00:00:00Z", new DateTimeOffset(1, 1, 2, 0, 0, 0, new TimeSpan()), true);
            yield return ("0001-01-01T00:00:00+01:00", null, true);
        }

        [TestMethod]
        public void ConvertToDateTimeOffsetTest()
        {
            foreach (var item in GetTestdata())
            {
                AssertConvertToDateTimeOffset(item.input, item.expected, item.expectException);
            }
        }
    }
}
