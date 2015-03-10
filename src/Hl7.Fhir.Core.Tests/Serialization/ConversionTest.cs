/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;

namespace Hl7.Fhir.Tests.Serialization
{
    [TestClass]
#if PORTABLE45
	public class PortableConversionTest
#else
	public class ConversionTest
#endif
    {
        [TestMethod]
        public void TestStringToBinary()
        {
            var result = PrimitiveTypeConverter.ConvertTo<byte[]>("AAECAw==");
            CollectionAssert.AreEqual(new byte[] { 0x00, 0x01, 0x02, 0x03 },result);
            Assert.AreEqual("AAECAw==", PrimitiveTypeConverter.ConvertTo<string>(result));
        }

        [TestMethod]
        public void TestStringToBool()
        {
            var result = PrimitiveTypeConverter.ConvertTo<bool>("false");
            Assert.IsFalse(result);
            Assert.AreEqual("false", PrimitiveTypeConverter.ConvertTo<string>(result));

            result = PrimitiveTypeConverter.ConvertTo<bool>("true");
            Assert.IsTrue(result);
            Assert.AreEqual("true", PrimitiveTypeConverter.ConvertTo<string>(result));
        }

        [TestMethod]
        public void TestGetValueAsString()
        {
            var x = new FhirBoolean(true);

            Assert.AreEqual("true", x.GetValueAsString());
        }

        [TestMethod]
        public void TestStringToInteger()
        {
            var result = PrimitiveTypeConverter.ConvertTo<int>("-314159");
            Assert.AreEqual(-314159,result);
            Assert.AreEqual("-314159", PrimitiveTypeConverter.ConvertTo<string>(result));
        }

        [TestMethod]
        public void TestStringToDecimal()
        {
            var result = PrimitiveTypeConverter.ConvertTo<decimal>("3.141592653589");
            Assert.AreEqual( 3.141592653589M, result);
            Assert.AreEqual("3.141592653589", PrimitiveTypeConverter.ConvertTo<string>(result));
        }

        [TestMethod]
        public void TestEnumToString()
        {
            var x = Quantity.QuantityComparator.LessOrEqual;

            Assert.AreEqual("<=", PrimitiveTypeConverter.ConvertTo<string>(x));
        }

        [TestMethod]
        public void TestStringToInstant()
        {
            var result = PrimitiveTypeConverter.ConvertTo<DateTimeOffset>("2011-03-04T14:45:33Z");
            Assert.AreEqual("2011-03-04T14:45:33+00:00", PrimitiveTypeConverter.ConvertTo<string>(result));

            result = PrimitiveTypeConverter.ConvertTo<DateTimeOffset>("2011-03-04T14:45:33+02:00");
            Assert.AreEqual("2011-03-04T14:45:33+02:00", PrimitiveTypeConverter.ConvertTo<string>(result));

            result = PrimitiveTypeConverter.ConvertTo<DateTimeOffset>("2012-04-14T10:35:23+00:00");
            Assert.AreEqual("2012-04-14T10:35:23+00:00", PrimitiveTypeConverter.ConvertTo<string>(result));

            try
            {
                result = PrimitiveTypeConverter.ConvertTo<DateTimeOffset>("2011-03-04T11:45:33");
                Assert.Fail();
            }
            catch (Exception) { }

            Instant ins5 = Instant.FromDateTimeUtc(2011, 3, 4, 16, 45, 33);
            Assert.AreEqual(new DateTimeOffset(2011,3,4,16,45,33, TimeSpan.Zero),ins5.Value);
        }

        [TestMethod]
        public void TestStringToUri()
        {
            var result = PrimitiveTypeConverter.ConvertTo<Uri>("http://www.nu.nl/test");
            Assert.IsTrue(result.IsAbsoluteUri);
            Assert.AreEqual("www.nu.nl", result.Host);
            Assert.AreEqual("http://www.nu.nl/test", PrimitiveTypeConverter.ConvertTo<string>(result));

            result = PrimitiveTypeConverter.ConvertTo<Uri>("service/patient/3");
            Assert.IsFalse(result.IsAbsoluteUri);
            Assert.AreEqual("service/patient/3", PrimitiveTypeConverter.ConvertTo<string>(result));
        }
    }
}
