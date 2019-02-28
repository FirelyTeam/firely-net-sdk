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
                Assert.AreEqual(expected, result, $"Input [{input}] was not expected [{expected.ToString()}]");
            }
            catch (AssertFailedException ae)
            {
                throw ae;
            }
            catch (Exception ex)
            {
                if (!expectException) Assert.IsTrue(false, $"[{input}] gave an exception: {ex.Message} ");
                return;
            }
            if (expectException) Assert.Fail($"[{input}] should give an exception");
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
            yield return ("0001-01-02T00:00:00Z", new DateTimeOffset(1, 1, 2, 0, 0, 0, new TimeSpan()), false);
            yield return ("0001-01-01T00:00:00Z", new DateTimeOffset(1, 1, 1, 0, 0, 0, new TimeSpan()), false);
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

        private IEnumerable<(string input, decimal expected, bool expectException)> GetDecimalTestdata()
        {
            // The following regex should be accepted for a decimal (comes from R4)
            // -?(0|[1-9][0-9]*)(\.[0-9]+)?([eE][+-]?[0-9]+)?

            yield return ("0", 0m, false);
            yield return ("-0", 0m, false);
            yield return ("-0.00", 0m, false);
            yield return ("1.2", 1.2m, false);
            yield return ("1.2000", 1.2m, false);
            yield return ("1.2e0", 1.2m, false);
            yield return ("1.2E0", 1.2m, false);
            yield return ("1.2e+10", 12000000000m, false);
            yield return ("1.2e10", 12000000000m, false);
            yield return ("1.2e-10", 0.00000000012m, false);
            yield return ("1.2000e0", 1.2m, false);
            yield return ("1e2", 100m, false);

            yield return ("1.2ee-00", 0m, true);
            yield return ("1.2eE0", 1.2m, true);
            yield return ("(8)", 8m, true);
            yield return ("1.6-", 1.6m, true);
            yield return ("NotANumber", 0m, true);
            yield return ("    8", 8m, true);
            yield return ("1,200.00", 0m, true);

            yield return ("+1.2", 1.2m, true);
            yield return (".2", 0.2m, true);
            yield return ("-0.", 0m, true);
            yield return ("0000078", 78m, true);
            yield return ("000000.5", 0.5m, true);
            //yield return ("08", 8m, true);  // Unfortunately this will be accepted by decimal.Parse(). 
        }

        [TestMethod]
        public void ConvertToDecimalTest()
        {
            foreach (var (input, expected, expectException) in GetDecimalTestdata())
            {
                AssertConvertToType(input, expected, expectException);
            }
        }
    }
}
