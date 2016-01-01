/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.FhirPath;
using Sprache;
using System.Diagnostics;
using Hl7.Fhir.Navigation;
using Hl7.Fhir.FhirPath.Grammar;

namespace Hl7.Fhir.Tests.FhirPath
{
    [TestClass]
#if PORTABLE45
	public class PortableFhirPathTest
#else
    public class FhirPathTest
#endif
    {
        FhirNavigationTree tree;

        [TestInitialize]
        public void Setup()
        {
            var tpXml = System.IO.File.ReadAllText("TestData\\FhirPathTestResource.xml");
            tree = TreeConstructor.FromXml(tpXml);
        }

        [TestMethod]
        public void TestSimpleBooleanExpressions()
        {
            var values = new EvaluationContext(1, 3, 5, 7);
            Assert.IsTrue(values.Empty().Not().ToBoolean());
        }

        [TestMethod]
        public void CheckTypeDetermination()
        {
            var values = new EvaluationContext(1, true, "hi", 4.0m, PartialDateTime.Now(), 4.0f);

            Assert.AreEqual(Fhir.FhirPath.ValueType.Integer, values.ItemAt(0).Single().Type);
            Assert.AreEqual(Fhir.FhirPath.ValueType.Boolean, values.ItemAt(1).Single().Type);
            Assert.AreEqual(Fhir.FhirPath.ValueType.String, values.ItemAt(2).Single().Type);
            Assert.AreEqual(Fhir.FhirPath.ValueType.Decimal, values.ItemAt(3).Single().Type);
            Assert.AreEqual(Fhir.FhirPath.ValueType.DateTime, values.ItemAt(4).Single().Type);
            Assert.AreEqual(Fhir.FhirPath.ValueType.Unknown, values.ItemAt(5).Single().Type);
        }


        [TestMethod]
        public void TestItemSelection()
        {
            var values = new EvaluationContext(1, 2, 3, 4, 5, 6, 7);

            Assert.AreEqual(1, values.ItemAt(0));
            Assert.AreEqual(3, values.ItemAt(2));
            Assert.AreEqual(1, values.First());
            Assert.IsTrue(values.ItemAt(100).Empty());
        }

        [TestMethod]
        public void TestNavigation()
        {
            var values = new EvaluationContext(tree);

            var result = values["Patient"]["identifier"]["use"];
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("usual", result.First());
        }

        [TestMethod]
        public void TestExpression()
        {
            var values = new EvaluationContext(tree);
            var result = values["Patient"]["identifier"]
                .Where(ctx => ctx["use"].IsEqualTo("official")).Empty().Not();
            Assert.AreEqual(true, result);
        }
    }
}