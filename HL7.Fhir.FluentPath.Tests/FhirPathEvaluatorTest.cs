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
using Hl7.Fhir.FluentPath;
using System.Diagnostics;
using Hl7.Fhir.FluentPath.InstanceTree;
using Hl7.Fhir.Rest;
using HL7.Fhir.FluentPath.FluentPath;

namespace Hl7.Fhir.Tests.FhirPath
{
    [TestClass]
#if PORTABLE45
	public class PortableFhirPathEvaluatorTest
#else
    public class FhirPathEvaluatorTest
#endif
    {
        IFluentPathElement tree;

        [TestInitialize]
        public void Setup()
        {
            var tpXml = System.IO.File.ReadAllText("TestData\\FhirPathTestResource.xml");
            tree = TreeConstructor.FromXml(tpXml);
        }


        [TestMethod, TestCategory("FhirPath")]
        public void TestTreeVisualizerVisitor()
        {
            var expr = PathExpression.Parse("doSomething('ha!', 4, {}, $this, somethingElse(true))");
            var result = expr.Dump();
            Debug.WriteLine(result);
        }

        [TestMethod, TestCategory("FhirPath")]
        public void TestForMe()
        {
            Assert.IsTrue(PathExpression.IsTrue(@"{}.empty()", tree));
            Assert.IsTrue(PathExpression.IsTrue(@"1.empty().not()", tree));

            Assert.AreEqual(2L,PathExpression.Scalar(@"Patient.identifier.count()", tree));
            Assert.IsTrue(PathExpression.IsTrue(@"Patient.identifier.count() = 2", tree));
        }

        [TestMethod, TestCategory("FhirPath")]
        public void TestExpression()
        {

            Assert.IsTrue(PathExpression.IsTrue(@"(Patient.identifier.where ( use = ( 'offic' + 'ial')) = 
                       Patient.identifier.skip(8/2 - 3*2 + 3)) and (Patient.identifier.where(use='usual') = 
                        Patient.identifier.first())", tree));

            //// xpath gebruikt $current for $focus....waarom dat niet gebruiken?
            Assert.IsTrue(PathExpression.IsTrue(
                  @"Patient.contact.relationship.coding.where($focus.system = %vs-patient-contact-relationship and 
                        $focus.code = 'owner').log('after owner').$parent.$parent.organization.log('org')
                        .where(display.startsWith('Walt')).resolve().identifier.first().value = 'Gastro'", tree,
                                new FhirEvaluationContext(new FhirClient("http://spark.furore.com/fhir"))));

            //// why is in an operator and not a function?
            Assert.IsTrue(PathExpression.IsTrue(
                 @"(Patient.identifier.where(use='official') in Patient.identifier) and
                       (Patient.identifier.first() in Patient.identifier.tail()).not()", tree));

            Assert.IsTrue(PathExpression.IsTrue(
                @"(1|2|2|3|Patient.identifier.first()|Patient.identifier).distinct().count() = 
                        3 + Patient.identifier.count()", tree));

            Assert.IsTrue(PathExpression.IsTrue(
                        @"(1|2|3|4|5).where($focus > 2 and $focus <= 4) = (3|4)", tree));

            Assert.IsTrue(PathExpression.IsTrue(
                        @"Patient.name.select(given|family).count() = 2", tree));

            Assert.IsTrue(PathExpression.IsTrue(
                    @"Patient.**.contains('wne') = contact.relationship.coding.code and
                    Patient.**.matches('i.*/gif') in Patient.photo.*", tree));

            Assert.IsTrue(PathExpression.IsTrue(
                @"'m' + gender.extension('http://example.org/StructureDefinition/real-gender').valueCode
                    .substring(1,4) + 
                    gender.extension('http://example.org/StructureDefinition/real-gender').valueCode
                    .substring(5) = 'metrosexual'", tree));

            Assert.IsTrue(PathExpression.IsTrue(
                    @"Patient.identifier.any(use='official') and identifier.where(use='usual').any()", tree));

            Assert.IsTrue(PathExpression.IsTrue(
                    @"gender.extension('http://example.org/StructureDefinition/real-gender').valueCode
                    .select('m' + $focus.substring(1,4) + $focus.substring(5)) = 'metrosexual'", tree));

            Assert.IsTrue(PathExpression.IsTrue(
                    @"Patient.**.where($focus.contains('222')).item(1) = $context.contained.address.line", tree));
        }

        //[TestMethod, TestCategory("FhirPath")]
        //public void TestExpressionTodayFunction()
        //{
        //    // Check that date comes in
        //    Assert.AreEqual(PartialDateTime.Parse(DateTime.Today.ToFhirDate()).ToString(), PathExpression.Evaluate("today()", tree).AsDateTime().ToString());

        //    // Check greater than
        //    Assert.IsTrue(PathExpression.Predicate("today() < " + DateTime.Today.AddDays(1).ToFhirDate(), tree));

        //    // Check less than
        //    Assert.IsTrue(PathExpression.Predicate("today() > " + DateTime.Today.AddDays(-1).ToFhirDate(), tree));

        //    // Check ==
        //    Assert.IsTrue(PathExpression.Predicate("today() = " + DateTime.Today.ToFhirDate(), tree));
        //}

        [TestMethod, TestCategory("FhirPath")]
        public void TestExpressionSubstringFunction()
        {
            // Check that date comes in
            Assert.IsTrue(PathExpression.IsTrue("QuestionnaireResponse.group.group.where(linkId=\"Section - C\").question.where(linkId=\"C1\").answer.group.where(linkId = \"C1fields\").question.where(linkId = \"DateReturnToNormalDuties\").answer.valueDate.empty()", tree));

            Assert.IsFalse(PathExpression.IsTrue("QuestionnaireResponse.group.group.where(linkId=\"Section - C\").question.where(linkId=\"C1\").answer.group.where(linkId = \"C1fields\").question.where(linkId = \"DateReturnToNormalDuties\").answer.valueDate.empty().not()", tree));

            // Assert.AreEqual("1973-05-31", PathExpression.Evaluate("Patient.contained.Patient.birthDate.substring(0,10)", tree).ToString());

            // Assert.AreEqual(null, PathExpression.Evaluate("Patient.birthDate2", tree).ToString());

            // Assert.AreEqual(null, PathExpression.Evaluate("Patient.birthDate2.substring(0,10)", tree).ToString());
        }

        [TestMethod, TestCategory("FhirPath")]
        public void TestExpressionRegexFunction()
        {
            // Check that date comes in
            Assert.IsTrue(PathExpression.IsTrue("Patient.identifier.where(system=\"urn:oid:0.1.2.3.4.5.6.7\").value.matches(\"^[1-6]+$\")", tree));

            Assert.IsFalse(PathExpression.IsTrue("Patient.identifier.where(system=\"urn:oid:0.1.2.3.4.5.6.7\").value.matches(\"^[1-3]+$\")", tree));

            // Assert.AreEqual("1973-05-31", PathExpression.Evaluate("Patient.contained.Patient.birthDate.substring(0,10)", tree).ToString());

            // Assert.AreEqual(null, PathExpression.Evaluate("Patient.birthDate2", tree).ToString());

            // Assert.AreEqual(null, PathExpression.Evaluate("Patient.birthDate2.substring(0,10)", tree).ToString());
        }

        [TestMethod, TestCategory("FhirPath")]
        public void TestExpression2()
        {
            //var result = Grammar.Expr.TryParse("Patient.deceased[x]");

            //if (result.WasSuccessful)
            //{
            //    var evaluator = result.Value;
            //    var resultNodes = evaluator.Select(tree);
            //    Assert.AreEqual(1, resultNodes.Count());
            //}
            //else
            //{
            //    Debug.WriteLine("Expectations: " + String.Join(",", result.Expectations));
            //    Assert.Fail(result.ToString());
            //}
        }

    }
}
