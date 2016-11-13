using Hl7.Fhir.Support;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using System.Collections.Generic;

namespace Hl7.Fhir.Model
{
    [TestClass]
    public class NullExtensionsTest
    {
        [TestMethod]
        public void TestIsNullOrEmpty_Extension()
        {
            var e = new Extension();
            Assert.IsTrue(e.IsNullOrEmpty());

            var value = new FhirString("test");
            Assert.IsFalse(value.IsNullOrEmpty());

            e.Value = value;
            Assert.IsFalse(e.IsNullOrEmpty());

            e.Value = null;
            Assert.IsTrue(e.IsNullOrEmpty());
        }

        [TestMethod]
        public void TestIsNullOrEmpty_FhirBoolean() => testIsNullOrEmpty_Primitive<FhirBoolean, bool?>(true, false);

        [TestMethod]
        public void TestIsNullOrEmpty_Integer() => testIsNullOrEmpty_Primitive<Integer, int?>(42, 0);

        [TestMethod]
        public void TestIsNullOrEmpty_FhirDateTime() => testIsNullOrEmpty_StringPrimitive<FhirDateTime>(XmlConvert.ToString(DateTime.Now, XmlDateTimeSerializationMode.RoundtripKind));

        [TestMethod]
        public void TestIsNullOrEmpty_FhirInstant() => testIsNullOrEmpty_Primitive<Instant, DateTimeOffset?>(DateTimeOffset.Now, new DateTimeOffset());

        [TestMethod]
        public void TestIsNullOrEmpty_FhirString() => testIsNullOrEmpty_StringPrimitive<FhirString>();

        [TestMethod]
        public void TestIsNullOrEmpty_Code() => testIsNullOrEmpty_StringPrimitive<Code>();

        [TestMethod]
        public void TestIsNullOrEmpty_FhirUri() => testIsNullOrEmpty_StringPrimitive<FhirUri>("http://example.org");

        void testIsNullOrEmpty_Primitive<T, V>(V testValue, V emptyValue) where T : Primitive<V>, IValue<V>, new()
        {
            var elem = new T();
            Assert.IsTrue(elem.IsNullOrEmpty());

            elem.Value = emptyValue;
            Assert.IsFalse(elem.IsNullOrEmpty());
            Assert.IsFalse((elem as Base).IsNullOrEmpty());

            elem.Value = testValue;
            Assert.IsFalse(elem.IsNullOrEmpty());
            Assert.IsFalse((elem as Base).IsNullOrEmpty());

            elem.Value = default(V);
            Assert.IsTrue(elem.IsNullOrEmpty());
            Assert.IsTrue((elem as Base).IsNullOrEmpty());

            var extension = new Extension();
            Assert.IsTrue(extension.IsNullOrEmpty());
            elem.Extension.Add(extension);
            Assert.IsTrue(elem.IsNullOrEmpty());

            var extensionValue = new FhirString("test");
            Assert.IsFalse(extensionValue.IsNullOrEmpty());
            extension.Value = extensionValue;
            Assert.IsFalse(extension.IsNullOrEmpty());
            Assert.IsFalse(elem.IsNullOrEmpty());
            Assert.IsFalse((elem as Base).IsNullOrEmpty());

            extensionValue.Value = null;
            Assert.IsTrue(extensionValue.IsNullOrEmpty());
            Assert.IsTrue(extension.IsNullOrEmpty());
            Assert.IsTrue(elem.IsNullOrEmpty());

            elem.Extension.Clear();
            Assert.IsTrue(elem.IsNullOrEmpty());
        }

        void testIsNullOrEmpty_StringPrimitive<T>(string exampleValue = "test") where T : Primitive<string>, IStringValue, new()
        {
            var elem = new T();
            Assert.IsTrue(elem.IsNullOrEmpty());

            elem.Value = string.Empty;
            Assert.IsTrue(elem.IsNullOrEmpty());
            Assert.IsTrue((elem as Base).IsNullOrEmpty());

            Assert.IsFalse(string.IsNullOrEmpty(exampleValue));
            elem.Value = exampleValue;
            Assert.IsFalse(elem.IsNullOrEmpty());
            Assert.IsFalse((elem as Base).IsNullOrEmpty());

            elem.Value = null;
            Assert.IsTrue(elem.IsNullOrEmpty());
            Assert.IsTrue((elem as Base).IsNullOrEmpty());

            var extension = new Extension();
            Assert.IsTrue(extension.IsNullOrEmpty());
            elem.Extension.Add(extension);
            Assert.IsTrue(elem.IsNullOrEmpty());

            var extensionValue = new FhirString(exampleValue);
            Assert.IsFalse(extensionValue.IsNullOrEmpty());
            extension.Value = extensionValue;
            Assert.IsFalse(extension.IsNullOrEmpty());
            Assert.IsFalse(elem.IsNullOrEmpty());
            Assert.IsFalse((elem as Base).IsNullOrEmpty());

            extensionValue.Value = null;
            Assert.IsTrue(extensionValue.IsNullOrEmpty());
            Assert.IsTrue(extension.IsNullOrEmpty());
            Assert.IsTrue(elem.IsNullOrEmpty());

            elem.Extension.Clear();
            Assert.IsTrue(elem.IsNullOrEmpty());
        }

        [TestMethod]
        public void TestIsNullOrEmpty_Coding()
        {
            var coding = new Coding();
            Assert.IsTrue(coding.IsNullOrEmpty());

            var uri = new FhirUri();
            Assert.IsTrue(uri.IsNullOrEmpty());
            coding.SystemElement = uri;
            Assert.IsTrue(coding.IsNullOrEmpty());

            uri.Value = "http://example.org/";
            Assert.IsFalse(uri.IsNullOrEmpty());
            Assert.IsFalse(coding.IsNullOrEmpty());
            Assert.IsFalse((coding as Base).IsNullOrEmpty());

            uri.Value = null;
            Assert.IsTrue(uri.IsNullOrEmpty());
            Assert.IsTrue(coding.IsNullOrEmpty());

            var extension = new Extension();
            Assert.IsTrue(extension.IsNullOrEmpty());
            coding.Extension.Add(extension);
            Assert.IsTrue(coding.IsNullOrEmpty());

            var extensionValue = new Coding(null, "test");
            Assert.IsFalse(extensionValue.IsNullOrEmpty());
            extension.Value = extensionValue;
            Assert.IsFalse(extension.IsNullOrEmpty());
            Assert.IsFalse(coding.IsNullOrEmpty());
            Assert.IsFalse((coding as Base).IsNullOrEmpty());

            extensionValue.Code = null;
            Assert.IsTrue(extensionValue.IsNullOrEmpty());
            Assert.IsTrue(extension.IsNullOrEmpty());
            Assert.IsTrue(coding.IsNullOrEmpty());

            coding.Extension.Clear();
            Assert.IsTrue(coding.IsNullOrEmpty());
        }

        [TestMethod]
        public void TestIsNullOrEmpty_List()
        {
            var codings = new List<Coding>();
            Assert.IsTrue(codings.IsNullOrEmpty());

            var coding = new Coding();
            Assert.IsTrue(coding.IsNullOrEmpty());

            codings.Add(coding);
            Assert.IsFalse(codings.IsNullOrEmpty()); // Empty values are counted... (but shouldn't occur)
            // Assert.IsTrue(codings.IsNullOrEmpty());

            coding.Code = "test";
            Assert.IsFalse(coding.IsNullOrEmpty());
            Assert.IsFalse(codings.IsNullOrEmpty());

            coding.Code = string.Empty;
            Assert.IsTrue(coding.IsNullOrEmpty());
            Assert.IsFalse(codings.IsNullOrEmpty()); // Empty values are counted... (but shouldn't occur)
            // Assert.IsTrue(codings.IsNullOrEmpty());
        }
    }
}
