using Hl7.Fhir.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Xml.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Support.Tests.Serialization
{
    [TestClass]
    public class PrimitiveTypeConverterTest
    {
        private void AssertConvertToType<T>(string input, T expected, bool expectException)
        {
            try
            {
                var result = PrimitiveTypeConverter.ConvertTo<T>(input);
                Assert.IsNotNull(result);
                Assert.AreEqual(expected, result);
            }
            catch (Exception ex)
            {
                if (!expectException) Assert.IsTrue(false, ex.Message);
            }
        }

        private IEnumerable<(string input, DateTimeOffset expected, bool expectException)> GetDateTimeOffsetTestdata()
        {
            yield return ("0001", new DateTimeOffset(1, 1, 1, 0, 0, 0, new TimeSpan()), false);
            yield return ("nodate", new DateTimeOffset(), true);
            yield return ("2018", new DateTimeOffset(2018, 1, 1, 0, 0, 0, new TimeSpan()), false);
            yield return ("0001-01", new DateTimeOffset(1, 1, 1, 0, 0, 0, new TimeSpan()), false);
            yield return ("0001-01-01", new DateTimeOffset(1, 1, 1, 0, 0, 0, new TimeSpan()), false);
            yield return ("0001-01-32", new DateTimeOffset(), true);
            yield return ("0001-01-02", new DateTimeOffset(1, 1, 2, 0, 0, 0, new TimeSpan()), false);
            yield return ("0001-01-02T00:00:00", new DateTimeOffset(new DateTime(1, 1, 2, 0, 0, 0)), false); // use localTime 
            yield return ("0001-01-02T00:00:00Z", new DateTimeOffset(1, 1, 2, 0, 0, 0, new TimeSpan()), true);
            yield return ("0001-01-01T00:00:00+01:00", new DateTimeOffset(), true);
        }

        private IEnumerable<(string input, DateTime expected, bool expectException)> GetDateTimeTestdata()
        {
            yield return ("0001-01-02+03:00", new DateTime(1, 1, 1, 21, 0, 0), false);
            yield return ("0001-01-02T00:00:00+00:00", new DateTime(1, 1, 2, 0, 0, 0), false); 
            yield return ("2018-04-12T13:22:12Z", new DateTime(2018, 4, 12, 13, 22, 12), false);
            yield return ("2018-04-12T13:22:12+02:00", new DateTime(2018, 4, 12, 11, 22, 12), false);
            yield return ("2018-04-12+02:00", new DateTime(2018, 4, 11, 22, 0, 0), false);
        }

        [TestMethod]
        public void ConvertToDateTimeOffsetTest()
        {
            foreach (var (input, expected, expectException) in GetDateTimeOffsetTestdata())
            {
                AssertConvertToType(input, expected, expectException);
            }
        }

        [TestMethod]
        public void ConvertToDateTimeTest()
        {
            foreach (var (input, expected, expectException) in GetDateTimeTestdata())
            {
                AssertConvertToType(input, expected, expectException);
            }
        }
    }
}
