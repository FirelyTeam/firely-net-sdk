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
using Hl7.Fhir.FhirPath;
using Sprache;
using Hl7.Fhir.Model;
using Hl7.Fhir.FhirPath.InstanceTree;

namespace Hl7.Fhir.Tests.FhirPath
{
    [TestClass]
#if PORTABLE45
	public class PortableFhirPathTest
#else
    public class FhirPathTest
#endif
    {
        IFhirPathElement tree;

        [TestInitialize]
        public void Setup()
        {
            var tpXml = System.IO.File.ReadAllText("TestData\\FhirPathTestResource.xml");
            tree = Fhir.FhirPath.InstanceTree.TreeConstructor.FromXml(tpXml);
        }

        [TestMethod, TestCategory("FhirPath")]
        public void AsIntegerOnList()
        {
            Assert.IsFalse(FhirValueList.Create(1, 2).IntegerEval().Any());
            Assert.IsFalse(FhirValueList.Empty().IntegerEval().Any());

            Assert.AreEqual(1L, FhirValueList.Create(1).IntegerEval().AsInteger());
            Assert.AreEqual(45L, FhirValueList.Create("45").IntegerEval().AsInteger());
            Assert.IsFalse(FhirValueList.Create(4.5m).IntegerEval().Any());
            Assert.IsFalse(FhirValueList.Create("4.5").IntegerEval().Any());
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
            var a = new TypedValue(4);
            var b = new TypedValue(5);
            var c = new TypedValue(5);

            Assert.AreEqual(9L, a.Add(b).AsInteger());
            Assert.AreEqual(-1L, a.Sub(b).AsInteger());
            Assert.IsTrue(a.LessThan(b).AsBoolean());
            Assert.IsTrue(a.LessOrEqual(b).AsBoolean());
            Assert.IsFalse(a.GreaterThan(b).AsBoolean());
            Assert.IsFalse(a.GreaterOrEqual(b).AsBoolean());
            Assert.IsTrue(b.IsEqualTo(c));
            Assert.IsTrue(b.LessOrEqual(c).AsBoolean());
            Assert.IsTrue(b.GreaterOrEqual(c).AsBoolean());
        }

        [TestMethod, TestCategory("FhirPath")]
        public void TestItemSelection()
        {
            var values = FhirValueList.Create(1, 2, 3, 4, 5, 6, 7);

            Assert.AreEqual((Int64)1, values.Item(0).IntegerEval().AsInteger());
            Assert.AreEqual((Int64)3, values.Item(2).IntegerEval().AsInteger());
            Assert.AreEqual((Int64)1, values.First().AsInteger());
            Assert.IsFalse(values.Item(100).Any());
        }

        [TestMethod, TestCategory("FhirPath")]
        public void TestNavigation()
        {
            var values = tree;

            var result = values.Children("Patient").Children("identifier").Children("use");
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("usual", result.First().AsStringRepresentation());

            var tpXml = System.IO.File.ReadAllText("TestData\\FhirPathTestResource.xml");
            Patient p = Fhir.Serialization.FhirParser.ParseResourceFromXml(tpXml) as Patient;

            IFhirPathElement model = p;
            result = model.Children("Patient").Children("identifier").Children("use");
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("usual", result.First().AsStringRepresentation());
        }

        [TestMethod, TestCategory("FhirPath")]
        public void TestExpression()
        {
            var values = tree;

            var result = values.Children("Patient").Children("identifier")
                .Where(ctx => ctx.Children("use").IsEqualTo(FhirValueList.Create("official"))).IsEmpty().Not();

            Assert.AreEqual(true, result.AsBoolean());
        }

        [TestMethod, TestCategory("FhirPath")]
        public void FhirPathDateValidationWithTodayAndTimezones()
        {
            Eval.FixedTodayValue = new DateTime(2016, 4, 27);
            string expression = "answer.empty() or answer.valueDate < today() or answer.valueDate = today()";

            var tree = TreeConstructor.FromXml("<QuestionnaireResponse xmlns=\"http://hl7.org/fhir\">"
                + "<answer><valueDate value=\"2016-04-28\" /></answer>"
                + "</QuestionnaireResponse>");
            Assert.IsTrue(PathExpression.Predicate(expression, tree));

            Eval.FixedTodayValue = new DateTime(2016, 4, 29);
            Assert.IsTrue(PathExpression.Predicate(expression, tree));

            tree = TreeConstructor.FromXml("<QuestionnaireResponse xmlns=\"http://hl7.org/fhir\">"
                + "<answer><valueDate value=\"2016-04-29\" /></answer>"
                + "</QuestionnaireResponse>");
            Assert.IsTrue(PathExpression.Predicate(expression, tree));

            tree = TreeConstructor.FromXml("<QuestionnaireResponse xmlns=\"http://hl7.org/fhir\">"
                + "<answer><valueDate value=\"2016-04-30\" /></answer>"
                + "</QuestionnaireResponse>");
            Assert.IsFalse(PathExpression.Predicate(expression, tree));
        }
    }
}