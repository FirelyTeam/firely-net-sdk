/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Sprache;
using Hl7.Fhir.FluentPath;
using HL7.Fhir.FluentPath.FluentPath.Expressions;
using Hl7.Fhir.FluentPath.InstanceTree;
using Hl7.Fhir.Navigation;
using HL7.Fhir.FluentPath;

namespace Hl7.Fhir.Tests.FhirPath
{
    [TestClass]
#if PORTABLE45
	public class PortableFhirPathTest
#else
    public class FhirPathTest
#endif
    {
        FhirInstanceTree tree;

        public IElementNavigator getTestData()
        {
            var tpXml = System.IO.File.ReadAllText("TestData\\FhirPathTestResource.xml");
            tree = TreeConstructor.FromXml(tpXml);
            var navigator = new TreeNavigator(tree);

            return navigator;
        }

        [TestMethod, TestCategory("FhirPath")]
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

        [TestMethod, TestCategory("FhirPath")]
        public void ConvertToString()
        {
            Assert.AreEqual("hoi", new ConstantValue("hoi").ToString());
            Assert.AreEqual("3.4", new ConstantValue(3.4m).ToString());
            Assert.AreEqual("4", new ConstantValue(4L).ToString());
            Assert.AreEqual("true", new ConstantValue(true).ToString());
            Assert.AreEqual("false", new ConstantValue(false).ToString());
            Assert.IsNull(new ConstantValue(DateTimeOffset.Now).ToString());
        }

        [TestMethod, TestCategory("FhirPath")]
        public void ConvertToDecimal()
        {
            Assert.AreEqual(1m, new ConstantValue(1m).ToDecimal());
            Assert.AreEqual(2.01m, new ConstantValue("2.01").ToDecimal());
            Assert.AreEqual(1L, new ConstantValue(true).ToDecimal());
            Assert.AreEqual(0L, new ConstantValue(false).ToDecimal());
            Assert.IsNull(new ConstantValue(2).ToDecimal());
//            Assert.IsNull(new ConstantValue("2").ToDecimal());   Not clear according to spec
            Assert.IsNull(new ConstantValue(DateTimeOffset.Now).ToDecimal());
        }


        [TestMethod, TestCategory("FhirPath")]
        public void CheckTypeDetermination()
        {
            var values = FhirValueList.Create(1, true, "hi", 4.0m, 4.0f, PartialDateTime.Now());

            Assert.IsInstanceOfType(values.Item(0).SingleValue(), typeof(Int64));
            Assert.IsInstanceOfType(values.Item(1).SingleValue(), typeof(Boolean));
            Assert.IsInstanceOfType(values.Item(2).SingleValue(), typeof(String));
            Assert.IsInstanceOfType(values.Item(3).SingleValue(), typeof(Decimal));
            Assert.IsInstanceOfType(values.Item(4).SingleValue(), typeof(Decimal));
            Assert.IsInstanceOfType(values.Item(5).SingleValue(), typeof(PartialDateTime));
        }


        [TestMethod, TestCategory("FhirPath")]
        public void TestValueOps()
        {
            var a = new ConstantValue(4);
            var b = new ConstantValue(5);
            var c = new ConstantValue(5);

            Assert.AreEqual(9L, a.Add(b).Value);
            Assert.AreEqual(-1L, a.Sub(b).Value);
            Assert.IsTrue(a.LessThan(b));
            Assert.IsTrue(a.LessOrEqual(b));
            Assert.IsFalse(a.GreaterThan(b));
            Assert.IsFalse(a.GreaterOrEqual(b));
            Assert.IsTrue(b.IsEqualTo(c));
            Assert.IsTrue(b.LessOrEqual(c));
            Assert.IsTrue(b.GreaterOrEqual(c));
        }

        [TestMethod, TestCategory("FhirPath")]
        public void TestItemSelection()
        {
            var values = FhirValueList.Create(1, 2, 3, 4, 5, 6, 7);

            Assert.AreEqual((Int64)1, values.Item(0).Single().Value);
            Assert.AreEqual((Int64)3, values.Item(2).Single().Value);
            Assert.AreEqual((Int64)1, values.First().Value);
            Assert.IsFalse(values.Item(100).Any());
        }

        [TestMethod, TestCategory("FhirPath")]
        public void TestNavigation()
        {
            var values = getTestData();
            
            var result = values.EnumerateChildrenByName("Patient").EnumerateChildrenByName("identifier").EnumerateChildrenByName("use");
            Assert.AreEqual(2, result.Count()); 
            Assert.AreEqual("usual", result.First().Value);
        }

        [TestMethod, TestCategory("FhirPath"),Ignore]
        public void TestExpression()
        {
            throw new NotImplementedException();

            //var values = navigator;

            //var result = values.EnumerateChildrenByName("Patient").EnumerateChildrenByName("identifier")
            //    .Where(ctx => ctx.Children("use").IsEqualTo(FhirValueList.Create("official"))).IsEmpty().Not();

            //Assert.AreEqual(true, result.BooleanEval().Value);
        }

        [TestMethod, TestCategory("FhirPath")]
        public void TypeInfoEquality()
        {
            Assert.AreEqual(TypeInfo.Bool, TypeInfo.Bool);
            Assert.IsTrue(TypeInfo.Decimal == TypeInfo.ByName("decimal"));
            Assert.AreNotEqual(TypeInfo.Bool, TypeInfo.String);
            Assert.IsTrue(TypeInfo.Decimal == TypeInfo.ByName("decimal"));
            Assert.AreEqual(TypeInfo.ByName("something"), TypeInfo.ByName("something"));
            Assert.AreNotEqual(TypeInfo.ByName("something"), TypeInfo.ByName("somethingElse"));
            Assert.IsTrue(TypeInfo.ByName("something") == TypeInfo.ByName("something"));
            Assert.IsTrue(TypeInfo.ByName("something") != TypeInfo.ByName("somethingElse"));
        }
    }
}