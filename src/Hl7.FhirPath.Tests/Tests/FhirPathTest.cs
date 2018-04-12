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
using System.Collections.Generic;
using System.Linq;
using Sprache;
using Hl7.FhirPath;
using Hl7.FhirPath.Functions;
using Xunit;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Model.Primitives;

namespace Hl7.FhirPath.Tests
{
    public class FhirPathTest
    {
        [Fact]
        public void ConvertToInteger()
        {
            Assert.Equal(1L, new ConstantValue(1).ToInteger());
            Assert.Equal(2L, new ConstantValue("2").ToInteger());
            Assert.Null(new ConstantValue("2.4").ToInteger());
            Assert.Equal(1L, new ConstantValue(true).ToInteger());
            Assert.Equal(0L, new ConstantValue(false).ToInteger());
            Assert.Null(new ConstantValue(2.4m).ToInteger());
            Assert.Null(new ConstantValue(DateTimeOffset.Now).ToInteger());
        }

        [Fact]
        public void ConvertToString()
        {
            Assert.Equal("hoi", new ConstantValue("hoi").ToString());
            Assert.Equal("3.4", new ConstantValue(3.4m).ToString());
            Assert.Equal("4", new ConstantValue(4L).ToString());
            Assert.Equal("true", new ConstantValue(true).ToString());
            Assert.Equal("false", new ConstantValue(false).ToString());
            Assert.NotNull(new ConstantValue(DateTimeOffset.Now).ToString());
        }

        [Fact]
        public void ConvertToDecimal()
        {
            Assert.Equal(1m, new ConstantValue(1m).ToDecimal());
            Assert.Equal(2.01m, new ConstantValue("2.01").ToDecimal());
            Assert.Equal(1L, new ConstantValue(true).ToDecimal());
            Assert.Equal(0L, new ConstantValue(false).ToDecimal());
            Assert.Null(new ConstantValue(2).ToDecimal());
//            Assert.Null(new ConstantValue("2").ToDecimal());   Not clear according to spec
            Assert.Null(new ConstantValue(DateTimeOffset.Now).ToDecimal());
        }


        

        [Fact]
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


        [Fact]
        public void TestItemSelection()
        {
            var values = FhirValueList.Create(1, 2, 3, 4, 5, 6, 7);

            Assert.Equal((Int64)1, values.Item(0).Single().Value);
            Assert.Equal((Int64)3, values.Item(2).Single().Value);
            Assert.Equal((Int64)1, values.First().Value);
            Assert.False(values.Item(100).Any());
        }

        [Fact]
        public void TypeInfoEquality()
        {
            Assert.Equal(TypeInfo.Boolean, TypeInfo.Boolean);
            Assert.True(TypeInfo.Decimal == TypeInfo.ByName("decimal"));
            Assert.NotEqual(TypeInfo.Boolean, TypeInfo.String);
            Assert.True(TypeInfo.Decimal == TypeInfo.ByName("decimal"));
            Assert.Equal(TypeInfo.ByName("something"), TypeInfo.ByName("something"));
            Assert.NotEqual(TypeInfo.ByName("something"), TypeInfo.ByName("somethingElse"));
            Assert.True(TypeInfo.ByName("something") == TypeInfo.ByName("something"));
            Assert.True(TypeInfo.ByName("something") != TypeInfo.ByName("somethingElse"));
        }

        //[Fact]
        //public void TypeInfoAndNativeMatching()
        //{
        //    Assert.True(TypeInfo.Decimal.MapsToNative(typeof(decimal)));
        //    Assert.False(TypeInfo.Decimal.MapsToNative(typeof(long)));
        //    Assert.False(TypeInfo.Any.CanBeCastTo(typeof(long)));
        //}

    }
}