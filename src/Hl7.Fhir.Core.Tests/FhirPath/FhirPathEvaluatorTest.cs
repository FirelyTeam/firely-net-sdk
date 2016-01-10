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
using Hl7.Fhir.FhirPath.InstanceTree;
using Hl7.Fhir.Rest;

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
            Assert.IsTrue(PathExpression.Predicate(@"(Patient.identifier.where ( use = ( 'offic' + 'ial')) = 
                       Patient.identifier.skip(8/2 - 3*2 + 3)) and (Patient.identifier.where(use='usual') = 
                        Patient.identifier.first())", tree));

            //// xpath gebruikt $current for $focus....waarom dat niet gebruiken?
            Assert.IsTrue(PathExpression.Predicate(
                  @"Patient.contact.relationship.coding.where($focus.system = %vs-patient-contact-relationship and 
                        $focus.code = 'owner').log('after owner').$parent.$parent.organization.log('org')
                        .where(display.startsWith('Walt')).resolve().identifier.first().value = 'Gastro'", tree,
                                new EvaluationContext(new FhirClient("http://spark.furore.com/fhir"))));

            //// why is in an operator and not a function?
            Assert.IsTrue(PathExpression.Predicate(
                 @"(Patient.identifier.where(use='official') in Patient.identifier) and
                       (Patient.identifier.first() in Patient.identifier.tail()).not()", tree));

            Assert.IsTrue(PathExpression.Predicate(
                @"(1|2|2|3|Patient.identifier.first()|Patient.identifier).distinct().count() = 
                        3 + Patient.identifier.count()", tree));

            Assert.IsTrue(PathExpression.Predicate(
                        @"(1|2|3|4|5).where($focus > 2 and $focus <= 4) = (3|4)", tree));

            Assert.IsTrue(PathExpression.Predicate(
                    @"Patient.**.contains('wne') = contact.relationship.coding.system.code and
                    Patient.**.matches('i.*/gif') in Patient.photo.*", tree));

            Assert.IsTrue(PathExpression.Predicate(
                @"'m' + gender.extension('http://example.org/StructureDefinition/real-gender').valueCode
                    .substring(1,4) + 
                    gender.extension('http://example.org/StructureDefinition/real-gender').valueCode
                    .substring(5) = 'metrosexual'", tree));

            Assert.IsTrue(PathExpression.Predicate(
                    @"Patient.identifier.any(use='official') and identifier.where(use='usual').any()", tree));

            Assert.IsTrue(PathExpression.Predicate(
                    @"gender.extension('http://example.org/StructureDefinition/real-gender').valueCode
                    .select('m' + $focus.substring(1,4) + $focus.substring(5)) = 'metrosexual'", tree));

            Assert.IsTrue(PathExpression.Predicate(
                    @"Patient.**.where($focus.contains('222')).item(1) = $context.contained.address.line", tree));
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
