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
        IFluentPathElement tree;

        [TestInitialize]
        public void Setup()
        {
            var tpXml = System.IO.File.ReadAllText("TestData\\FhirPathTestResource.xml");
            tree = Fhir.FluentPath.InstanceTree.TreeConstructor.FromXml(tpXml);
        }

        [TestMethod, TestCategory("FhirPath")]
        public void ConvertToInteger()
        {
            Assert.AreEqual(1, new ConstantValue(1).ToInteger().Value);
            Assert.AreEqual(2, new ConstantValue("2").ToInteger().Value);
            Assert.IsNull(new ConstantValue("2.4").ToInteger());
            Assert.AreEqual(1, new ConstantValue(true).ToInteger().Value);
            Assert.AreEqual(0, new ConstantValue(false).ToInteger().Value);
            Assert.IsNull(new ConstantValue(2.4m).ToInteger());
            Assert.IsNull(new ConstantValue(DateTimeOffset.Now).ToInteger());
        }

        [TestMethod, TestCategory("FhirPath")]
        public void ConvertToString()
        {
            throw new NotImplementedException();
        }

        [TestMethod, TestCategory("FhirPath")]
        public void ConvertToDecimal()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
            //var a = new ConstantValue(4);
            //var b = new ConstantValue(5);
            //var c = new ConstantValue(5);

            //Assert.AreEqual(9L, a.Add(b).Value);
            //Assert.AreEqual(-1L, a.Sub(b).Value);
            //Assert.IsTrue(a.LessThan(b).Value);
            //Assert.IsTrue(a.LessOrEqual(b).Value);
            //Assert.IsFalse(a.GreaterThan(b).Value);
            //Assert.IsFalse(a.GreaterOrEqual(b).Value);
            //Assert.IsTrue(b.IsEqualTo(c));
            //Assert.IsTrue(b.LessOrEqual(c).AsBoolean());
            //Assert.IsTrue(b.GreaterOrEqual(c).AsBoolean());
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
            var values = tree;

            var result = values.Children("Patient").Children("identifier").Children("use");
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("usual", result.First().Value);
        }

        [TestMethod, TestCategory("FhirPath")]
        public void TestExpression()
        {
            throw new NotImplementedException();

            //var values = tree;

            //var result = values.Children("Patient").Children("identifier")
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