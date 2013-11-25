using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;

namespace Hl7.Fhir.Test.Serialization
{
    [TestClass]
    public class ConversionTest
    {
        [TestMethod]
        public void TestStringToBinary()
        {
            var result = PrimitiveTypeConverter.Convert<byte[]>("AAECAw==");
            CollectionAssert.AreEqual(new byte[] { 0x00, 0x01, 0x02, 0x03 },result);
            Assert.AreEqual("AAECAw==", PrimitiveTypeConverter.Convert<string>(result));
        }

        [TestMethod]
        public void TestStringToBool()
        {
            var result = PrimitiveTypeConverter.Convert<bool>("false");
            Assert.IsFalse(result);
            Assert.AreEqual("false", PrimitiveTypeConverter.Convert<string>(result));

            result = PrimitiveTypeConverter.Convert<bool>("true");
            Assert.IsTrue(result);
            Assert.AreEqual("true", PrimitiveTypeConverter.Convert<string>(result));
        }

        [TestMethod]
        public void TestStringToInteger()
        {
            var result = PrimitiveTypeConverter.Convert<int>("-314159");
            Assert.AreEqual(-314159,result);
            Assert.AreEqual("-314159", PrimitiveTypeConverter.Convert<string>(result));
        }

        [TestMethod]
        public void TestStringToDecimal()
        {
            var result = PrimitiveTypeConverter.Convert<decimal>("3.141592653589");
            Assert.AreEqual( 3.141592653589M, result);
            Assert.AreEqual("3.141592653589", PrimitiveTypeConverter.Convert<string>(result));
        }


        [TestMethod]
        public void TestStringToInstant()
        {
            var result = PrimitiveTypeConverter.Convert<DateTimeOffset>("2011-03-04T14:45:33Z");
            Assert.AreEqual("2011-03-04T14:45:33+00:00", PrimitiveTypeConverter.Convert<string>(result));

            result = PrimitiveTypeConverter.Convert<DateTimeOffset>("2011-03-04T14:45:33+02:00");
            Assert.AreEqual("2011-03-04T14:45:33+02:00", PrimitiveTypeConverter.Convert<string>(result));

            result = PrimitiveTypeConverter.Convert<DateTimeOffset>("2012-04-14T10:35:23+00:00");
            Assert.AreEqual("2012-04-14T10:35:23+00:00", PrimitiveTypeConverter.Convert<string>(result));

            try
            {
                result = PrimitiveTypeConverter.Convert<DateTimeOffset>("2011-03-04T11:45:33");
                Assert.Fail();
            }
            catch (Exception) { }

            Instant ins5 = Instant.FromDateTimeUtc(2011, 3, 4, 16, 45, 33);
            Assert.AreEqual("2011-03-04T16:45:33+00:00", ins5.ToString());
        }

        [TestMethod]
        public void TestStringToUri()
        {
            var result = PrimitiveTypeConverter.Convert<Uri>("http://www.nu.nl/test");
            Assert.IsTrue(result.IsAbsoluteUri);
            Assert.AreEqual("www.nu.nl", result.Host);
            Assert.AreEqual("http://www.nu.nl/test", PrimitiveTypeConverter.Convert<string>(result));

            result = PrimitiveTypeConverter.Convert<Uri>("service/patient/3");
            Assert.IsFalse(result.IsAbsoluteUri);
            Assert.AreEqual("service/patient/3", PrimitiveTypeConverter.Convert<string>(result));
        }
    }
}
