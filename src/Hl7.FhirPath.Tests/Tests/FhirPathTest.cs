/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

// To introduce the DSTU2 FHIR specification
// extern alias dstu2;

using System;
using System.Linq;
using Hl7.FhirPath.Functions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model.Primitives;
using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.FhirPath.Tests
{
    [TestClass]
    public class FhirPathTest
    {
        [TestMethod]
        public void ConvertToInteger()
        {
            Assert.AreEqual(1L, new ConstantValue(1).ToInteger());
            Assert.AreEqual(2L, new ConstantValue("2").ToInteger());
            Assert.IsNull(new ConstantValue("2.4").ToInteger());
            Assert.AreEqual(1L, new ConstantValue(true).ToInteger());
            Assert.AreEqual(0L, new ConstantValue(false).ToInteger());
            Assert.IsNull(new ConstantValue(2.4m).ToInteger());
            Assert.IsNull(new ConstantValue(DateTimeOffset.Now).ToInteger());
        }

        [TestMethod]
        public void ConvertToString()
        {
            Assert.AreEqual("hoi", new ConstantValue("hoi").ToString());
            Assert.AreEqual("3.4", new ConstantValue(3.4m).ToString());
            Assert.AreEqual("4", new ConstantValue(4L).ToString());
            Assert.AreEqual("true", new ConstantValue(true).ToString());
            Assert.AreEqual("false", new ConstantValue(false).ToString());
            Assert.IsNotNull(new ConstantValue(DateTimeOffset.Now).ToString());
        }

        [TestMethod]
        public void ConvertToDecimal()
        {
            Assert.AreEqual(1m, new ConstantValue(1m).ToDecimal());
            Assert.AreEqual(2.01m, new ConstantValue("2.01").ToDecimal());
            Assert.AreEqual(1L, new ConstantValue(true).ToDecimal());
            Assert.AreEqual(0L, new ConstantValue(false).ToDecimal());
            Assert.IsNull(new ConstantValue(2).ToDecimal());
//            Assert.Null(new ConstantValue("2").ToDecimal());   Not clear according to spec
            Assert.IsNull(new ConstantValue(DateTimeOffset.Now).ToDecimal());
        }

        [TestMethod]
        public void CheckTypeDetermination()
        {
            var values = FhirValueList.Create(1, true, "hi", 4.0m, 4.0f, PartialDateTime.Now());
            
            
            Test.IsInstanceOfType(values.Item(0).Single().Value, typeof(Int64));
            Test.IsInstanceOfType(values.Item(1).Single().Value, typeof(Boolean));
            Test.IsInstanceOfType(values.Item(2).Single().Value, typeof(String));
            Test.IsInstanceOfType(values.Item(3).Single().Value, typeof(Decimal));
            Test.IsInstanceOfType(values.Item(4).Single().Value, typeof(Decimal));
            Test.IsInstanceOfType(values.Item(5).Single().Value, typeof(PartialDateTime));
        }


        [TestMethod]
        public void TestItemSelection()
        {
            var values = FhirValueList.Create(1, 2, 3, 4, 5, 6, 7);

            Assert.AreEqual((Int64)1, values.Item(0).Single().Value);
            Assert.AreEqual((Int64)3, values.Item(2).Single().Value);
            Assert.AreEqual((Int64)1, values.First().Value);
            Assert.IsFalse(values.Item(100).Any());
        }

        [TestMethod]
        public void TypeInfoEquality()
        {
            Assert.AreEqual(TypeInfo.Boolean, TypeInfo.Boolean);
            Assert.IsTrue(TypeInfo.Decimal == TypeInfo.ByName("decimal"));
            Assert.AreNotEqual(TypeInfo.Boolean, TypeInfo.String);
            Assert.IsTrue(TypeInfo.Decimal == TypeInfo.ByName("decimal"));
            Assert.AreEqual(TypeInfo.ByName("something"), TypeInfo.ByName("something"));
            Assert.AreNotEqual(TypeInfo.ByName("something"), TypeInfo.ByName("somethingElse"));
            Assert.IsTrue(TypeInfo.ByName("something") == TypeInfo.ByName("something"));
            Assert.IsTrue(TypeInfo.ByName("something") != TypeInfo.ByName("somethingElse"));
        }

        [TestMethod]
        public void TestFhirPathPolymporphism()
        {
            var patient = new Hl7.Fhir.Model.Patient() { Active = false };
            patient.Meta = new Meta() { LastUpdated = new DateTimeOffset(2018, 5, 24, 14, 48, 0, TimeSpan.Zero) };
            var nav = new PocoNavigator(patient);

            var result = nav.Select("Resource.meta.lastUpdated");
            Assert.IsNotNull(result.FirstOrDefault());
            Assert.AreEqual(PartialDateTime.Parse("2018-05-24T14:48:00+00:00"), result.First().Value);
        }

        //[TestMethod]
        //public void TypeInfoAndNativeMatching()
        //{
        //    Assert.True(TypeInfo.Decimal.MapsToNative(typeof(decimal)));
        //    Assert.False(TypeInfo.Decimal.MapsToNative(typeof(long)));
        //    Assert.False(TypeInfo.Any.CanBeCastTo(typeof(long)));
        //}

    }
}