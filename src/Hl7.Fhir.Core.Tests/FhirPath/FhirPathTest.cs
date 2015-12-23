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
        public void TestExpression()
        {
            //var result = Expression.Expr.TryParse("(4>$parent.bla*.blie.(jee+4).bloe.where(parent>5,false != true))and(%bla>=6)");
            //var result = Expression.FpConst.TryParse("4.5");

            var result = Path.Predicate.TryParse("Patient.identifier.use.bla");

            if (result.WasSuccessful)
            {
                var evaluator = result.Value;
                var resultNodes = evaluator.Evaluate(tree);
                Assert.AreEqual(2,resultNodes.Count());
            }
            else
            {
                Debug.WriteLine("Expectations: " + String.Join(",",result.Expectations));
                Assert.Fail(result.ToString());
            }            
        }


        [TestMethod]
        public void TestExpression2()
        {
            var result = Path.Predicate.TryParse("Patient.deceased[x]");

            if (result.WasSuccessful)
            {
                var evaluator = result.Value;
                var resultNodes = evaluator.Evaluate(tree);
                Assert.AreEqual(1, resultNodes.Count());
            }
            else
            {
                Debug.WriteLine("Expectations: " + String.Join(",", result.Expectations));
                Assert.Fail(result.ToString());
            }
        }

    }
}
