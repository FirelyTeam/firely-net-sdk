using Hl7.Fhir.Model;
using Hl7.Fhir.Model.Primitives;
using Hl7.Fhir.Support.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.Support.Tests.Model
{
    [TestClass]
    public class PrimitiveTests
    {
        [DataRow(typeof(PartialDateTime), "dateTime")]
        [DataRow(typeof(UInt16), "integer")]
        [DataRow(typeof(Boolean), "boolean")]
        [DataTestMethod]
        public void GetPrimitiveName(Type native, string expected)
        {
            Assert.IsTrue(Primitives.TryGetPrimitiveTypeName(native, out var actual));
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CatchesUnknownPrimitiveName()
        {
            Assert.IsFalse(Primitives.TryGetPrimitiveTypeName(typeof(PrimitiveTests), out _));
        }

        [TestMethod]
        public void ConvertPrimitiveValue()
        {
            var tests = new (object,object)[] { ("a string", "a string"),
                            (new DateTimeOffset(2019, 6, 20, 13, 48, 0, TimeSpan.Zero), PartialDateTime.Parse("2019-06-20T13:48:00Z")),
                            (3u, 3L) };

            foreach (var test in tests)
            {
                Assert.IsTrue(Primitives.TryConvertToPrimitiveValue(test.Item1, out var actual));
                Assert.AreEqual(test.Item2, actual);
            }
        }


        /* 
         *    case "boolean":
                        return typeof(bool);
                    case "integer":
                    case "unsignedInt":
                    case "positiveInt":
                        return typeof(long);
                    case "time":
                        return typeof(PartialTime);
                    case "instant":
                    case "date":
                    case "dateTime":
                        return typeof(PartialDateTime);
                    case "decimal":
                        return typeof(decimal);
                    case "string":
                    case "code":
                    case "id":
                    case "uri":
                    case "oid":
                    case "uuid":
                    case "canonical":
                    case "url":
                    case "markdown":
                    case "base64Binary":
                    case "xhtml":
                        return typeof(string);
                    default:
                        return null;\
                        */

        [DataRow("decimal", typeof(decimal))]
        [DataRow("url", typeof(string))]
        [DataRow("string", typeof(string))]
        [DataRow("time", typeof(PartialTime))]
        [DataRow("positiveInt", typeof(long))]
        [DataTestMethod]
        public void GetNativeRepresentation(string fhirType, Type native)
        {
            Assert.IsTrue(Primitives.TryGetNativeRepresentation(fhirType, out var actual));
            Assert.AreEqual(native, actual);
        }

        [TestMethod]
        public void CatchesUnknownNativeRepresentation()
        {
            Assert.IsFalse(Primitives.TryGetNativeRepresentation("Patient", out _));
        }

    }
}
