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
	public class PortableFhirPathEvaluatorTest
#else
    public class FhirPathEvaluatorTest
#endif
    {
        IFhirPathElement tree;

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

            //var result = Expression.Expr.End().TryParse(
            //    @"(Patient.identifier.where ( use = ( 'offic' + 'ial')) = 
            //           Patient.identifier.skip(8/2 - 3*2 + 3)) and (Patient.identifier.where(use='usual') = Patient.identifier.first())");

            var result = Expression.Expr.End().TryParse(
                @"Patient.contact.relationship.where(coding.system = %vs-patient-contact-relationship and coding.code = 'owner').count() = 1");

            if (result.WasSuccessful)
            {
                var evaluator = result.Value;
                var resultNodes = evaluator.Evaluate(tree);
                Assert.AreEqual(1,resultNodes.Count());
                Assert.AreEqual(true, resultNodes.First().AsBool());
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
            var result = Expression.Expr.TryParse("Patient.deceased[x]");

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
