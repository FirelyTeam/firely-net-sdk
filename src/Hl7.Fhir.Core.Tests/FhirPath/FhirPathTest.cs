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
            tree = Fhir.Navigation.TreeConstructor.FromXml(tpXml);
        }

        [TestMethod]
        public void CheckTypeDetermination()
        {
            var values = Focus.Create(1, true, "hi", 4.0m, 4.0f, PartialDateTime.Now(), 
                        new UntypedValue("1"), new UntypedValue("true"), new UntypedValue("hi"), new UntypedValue("4.0"),
                        new UntypedValue(PartialDateTime.Now().ToString()));

            Assert.IsInstanceOfType(values.ItemAt(0).Value, typeof(Int64));
            Assert.IsInstanceOfType(values.ItemAt(1).Value, typeof(Boolean));
            Assert.IsInstanceOfType(values.ItemAt(2).Value, typeof(String));
            Assert.IsInstanceOfType(values.ItemAt(3).Value, typeof(Decimal));
            Assert.IsInstanceOfType(values.ItemAt(4).Value, typeof(Decimal));
            Assert.IsInstanceOfType(values.ItemAt(5).Value, typeof(PartialDateTime));

            Assert.IsInstanceOfType(values.ItemAt(6).Value, typeof(Int64));
            Assert.IsInstanceOfType(values.ItemAt(7).Value, typeof(Boolean));
            Assert.IsInstanceOfType(values.ItemAt(8).Value, typeof(String));
            Assert.IsInstanceOfType(values.ItemAt(9).Value, typeof(Decimal));
            Assert.IsInstanceOfType(values.ItemAt(10).Value, typeof(PartialDateTime));
        }


        [TestMethod]
        public void TestAdd()
        {
            var a = new TypedValue(4);
            var b = new UntypedValue("5");

            var result = a.Add(b);

            Assert.AreEqual((Int64)9, result.Value);
        }

        [TestMethod]
        public void TestItemSelection()
        {
            var values = Focus.Create(1, 2, 3, 4, 5, 6, 7);

            Assert.AreEqual(1, values.ItemAt(0));
            Assert.AreEqual(3, values.ItemAt(2));
            Assert.AreEqual(1, values.First());
            Assert.IsNotNull(values.ItemAt(100));
        }

        [TestMethod]
        public void TestNavigation()
        {
            var values = (IEnumerable<IFhirPathValue>)tree;

            var result = values.Children("Patient").Children("identifier").Children("use");
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("usual", result.First());
        }

        [TestMethod]
        public void TestExpression()
        {
            var values = (IEnumerable<IFhirPathElement>)tree.Children();

            var result = !values.Children("Patient").Children("identifier")
                .Where(ctx => ctx.Children("use").IsEqualTo(Focus.Create("official"))).Empty();

            Assert.AreEqual(true, result);
        }
    }
}