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
using Hl7.Fhir.FluentPath.Expressions;
using System.Diagnostics;
using Hl7.Fhir.FluentPath.InstanceTree;
using Hl7.Fhir.Navigation;

namespace Hl7.Fhir.Tests.FhirPath
{
    [TestClass]
#if PORTABLE45
	public class PortableFhirPathEvaluatorTest
#else
    public class FhirPathEvaluatorTest
#endif
    {
        FhirInstanceTree tree;
        IEnumerable<IValueProvider> testInput;

        [TestInitialize]
        public void Setup()
        {
            var tpXml = System.IO.File.ReadAllText("TestData\\FhirPathTestResource.xml");
            tree = TreeConstructor.FromXml(tpXml);
            testInput = FhirValueList.Create(new TreeNavigator(tree));
        }


        [TestMethod, TestCategory("FhirPath")]
        public void TestTreeVisualizerVisitor()
        {
            var expr = PathExpression.Parse("doSomething('ha!', 4, {}, $this, somethingElse(true))");
            var result = expr.Dump();
            Debug.WriteLine(result);
        }

        [TestMethod, TestCategory("FhirPath")]
        public void TestExistence()
        {
            isTrue(@"{}.empty()");
            isTrue(@"1.empty().not()");
            isTrue(@"1.exists()");
            isTrue(@"Patient.identifier.exists()");
            isTrue(@"Patient.dientifeir.exists().not()");
            Assert.AreEqual(3L, PathExpression.Scalar(@"Patient.identifier.count()", testInput));
            Assert.AreEqual(3L, PathExpression.Scalar(@"Patient.identifier.value.count()", testInput));
        }

        [TestMethod, TestCategory("FhirPath")]
        public void TestNullPropagation()
        {
            isTrue(@"({}.substring(0)).empty()");
            isTrue(@"('hello'.substring({})).empty()");
        }

        [TestMethod, TestCategory("FhirPath")]
        public void TestDynaBinding()
        {
            var input = FhirValueList.Create(new ConstantValue("Hello world!"), new ConstantValue(4));
            
            //TODO: Improve error on this:
            // Assert.AreEqual("ello", PathExpression.Scalar(@"substring(1,%context[1])", input));
            
            Assert.AreEqual("ello", PathExpression.Scalar(@"$this[0].substring(1,%context[1])", input));        
        }


        [TestMethod, TestCategory("FhirPath")]
        public void TestSubsetting()
        {
            isTrue(@"Patient.identifier[0] = Patient.identifier.first()");
            isTrue(@"Patient.identifier[2] = Patient.identifier.last()");
            isTrue(@"Patient.identifier[0] | Patient.identifier[1]  = Patient.identifier.take(2)");
            isTrue(@"Patient.identifier.skip(1) = Patient.identifier.tail()");
            isTrue(@"Patient.identifier.skip(2) = Patient.identifier.last()");
            isTrue(@"Patient.identifier.first().single()");

            try
            {
                isTrue(@"Patient.identifier.single()");
                Assert.Fail();
            }
            catch(InvalidOperationException io)
            {
                Assert.IsTrue(io.Message.Contains("contains more than one element"));
            }
        }


        [TestMethod, TestCategory("FhirPath")]
        public void TestGreaterThan()
        {
            isTrue(@"4.5 > 0");
            isTrue(@"'ewout' > 'alfred'");
            isTrue(@"2016-04-01 > 2015-04-01");
            isTrue(@"5 > 6 = false");
            isTrue(@"(5 > {}).empty()");
        }

        private void isTrue(string expr)
        {
            Assert.IsTrue(PathExpression.IsTrue(expr, testInput));
        }

        [TestMethod, TestCategory("FhirPath")]
        public void TestMath()
        {
            isTrue(@"-4.5 + 4.5 = 0");
            isTrue(@"4/2 = 2");
            isTrue(@"2/4 = 0.5");
            isTrue(@"10/4 = 2.5");
            isTrue(@"10.0/4 = 2.5");
            isTrue(@"4.0/2.0 = 2");
            isTrue(@"2.0/4 = 0.5");
            isTrue(@"2.0 * 4 = 8");
            isTrue(@"2 * 4.1 = 8.2");
            isTrue(@"-0.5 * 0.5 = -0.25");
            isTrue(@"5 - 4.5 = 0.5");
            isTrue(@"9.5 - 4.5 = 5");
            isTrue(@"5 + 4.5 = 9.5");
            isTrue(@"9.5 + 0.5 = 10");

            isTrue(@"103 mod 5 = 3");
            isTrue(@"101.4 mod 5.2 = 2.6");
            isTrue(@"103 div 5 = 20");
            isTrue(@"20.0 div 5.5 = 3");

            isTrue(@"'offic'+'ial' = 'official'");

            isTrue(@"12/(2+2) - (3 div 2) = 2");
            isTrue(@"-4.5 + 4.5 * 2 * 4 / 4 - 1.5 = 3");
        }


        [TestMethod, TestCategory("FhirPath")]
        public void Test3VLBoolean()
        {
            isTrue(@"true and true");
            isTrue(@"(true and false) = false");
            isTrue(@"(true and {}).empty()");
            isTrue(@"(false and true) = false");
            isTrue(@"(false and false) = false");
            isTrue(@"(false and {}) = false");
            isTrue(@"({} and true).empty()");
            isTrue(@"({} and false) = false");
            isTrue(@"({} and {}).empty()");

            isTrue(@"true or true");
            isTrue(@"true or false");
            isTrue(@"true or {}");
            isTrue(@"false or true");
            isTrue(@"(false or false) = false");
            isTrue(@"(false or {}).empty()");
            isTrue(@"{} or true");
            isTrue(@"({} or false).empty()");
            isTrue(@"({} or {}).empty()");

            isTrue(@"(true xor true)=false");
            isTrue(@"true xor false");
            isTrue(@"(true xor {}).empty()");
            isTrue(@"false xor true");
            isTrue(@"(false xor false) = false");
            isTrue(@"(false xor {}).empty()");
            isTrue(@"({} xor true).empty()");
            isTrue(@"({} xor false).empty()");
            isTrue(@"({} xor {}).empty()");

            isTrue(@"true implies true");
            isTrue(@"(true implies false) = false");
            isTrue(@"(true implies {}).empty()");
            isTrue(@"false implies true");
            isTrue(@"false implies false");
            isTrue(@"false implies {}");
            isTrue(@"{} implies true");
            isTrue(@"({} implies false).empty()");
            isTrue(@"({} implies {}).empty()");
        }

        [TestMethod, TestCategory("FhirPath")]
        public void TestLogicalShortcut()
        {
            isTrue(@"true or (1/0 = 0)");
            isTrue(@"(false and (1/0 = 0)) = false");
        }


        [TestMethod, TestCategory("FhirPath")]
        public void TestConversions()
        {
            isTrue(@"(4.1).toString() = '4.1'");
            isTrue(@"true.toString() = 'true'");
            isTrue(@"true.toDecimal() = 1");
            isTrue(@"Patient.identifier.value.first().toDecimal() = 654321");
        }


        [TestMethod, TestCategory("FhirPath")]
        public void TestEquality()
        {
            isTrue(@"4 = 4");
            isTrue(@"4 = 4.0");
            isTrue(@"true = true");
            isTrue(@"true != false");

            isTrue(@"Patient.identifier = Patient.identifier");
            isTrue(@"Patient.identifier.first() != Patient.identifier.skip(1)");
            isTrue(@"(1|2|3) = (1|2|3)");
            isTrue(@"(1|2|3) = (1.0|2.0|3)");
            isTrue(@"(1|Patient.identifier|3) = (1|Patient.identifier|3)");
            isTrue(@"(3|Patient.identifier|1) != (1|Patient.identifier|3)");

            isTrue(@"Patient.gender = 'male'"); // gender has an extension
            isTrue(@"Patient.communication.first() = Patient.communication.skip(1)");       // different extensions, same values

            isTrue(@"@2015-01-01 = @2015-01-01");
            isTrue(@"@2015-01-01 != @2015-01");
            isTrue(@"@2015-01-01T13:40:50+00:00 = @2015-01-01T13:40:50Z");
            isTrue(@"@T13:45:02Z = @T13:45:02+00:00");
            isTrue(@"@T13:45:02+00:00 != @T13:45:02+01:00");
        }

        [TestMethod, TestCategory("FhirPath")]
        public void TestCollectionFunctions()
        {
            isTrue(@"Patient.identifier.use.distinct() = ('usual' | 'official')");
            isTrue(@"Patient.identifier.use.distinct().count() = 2");
            isTrue(@"Patient.identifier.use.isDistinct() = false");
            isTrue(@"Patient.identifier.system.isDistinct()");
            isTrue(@"(3|4).isDistinct()");
            isTrue(@"{}.isDistinct()");

            isTrue(@"Patient.identifier.skip(1).subsetOf(%context.Patient.identifier)");
            isTrue(@"Patient.identifier.supersetOf(%context.Patient.identifier)");
            isTrue(@"Patient.identifier.supersetOf({})");
            isTrue(@"{}.subsetOf(%context.Patient.identifier)");
        }

        [TestMethod, TestCategory("FhirPath")]
        public void TestCollectionOperators()
        {
            isTrue(@"Patient.identifier.last() in Patient.identifier");
            isTrue(@"4 in (3|4.0|5)");
            isTrue(@"(3|4.0|5|3).count() = 3");
            isTrue(@"Patient.identifier contains Patient.identifier.last()");
            isTrue(@"(3|4.0|5) contains 4");
            isTrue(@"({} contains 4) = false");
            isTrue(@"(4 in {}) = false");
            isTrue(@"Patient.identifier.count() = (Patient.identifier | Patient.identifier).count()");
            isTrue(@"(Patient.identifier | Patient.name).count() = Patient.identifier.count() + Patient.name.count()");
            isTrue(@"Patient.select(identifier | name).count() = Patient.select(identifier.count() + name.count())");
        }


        [TestMethod, TestCategory("FhirPath")]
        public void TestWhere()
        {
            isTrue("Patient.identifier.where(use = ('offic' + 'ial')).count() = 2");

            isTrue(@"(5 | 'hi' | 4).where($this = 'hi').count()=1");
            isTrue(@"{}.where($this = 'hi').count()=0");
        }

        [TestMethod, TestCategory("FhirPath")]
        public void TestAll()
        {
            isTrue(@"Patient.identifier.skip(1).all(use = 'official')");
            isTrue(@"Patient.identifier.skip(999).all(use = 'official')");   // {}.All() aways returns true
            isTrue(@"Patient.identifier.skip(1).all({}).empty()");   // empty results still count as "empty"
        }

        [TestMethod, TestCategory("FhirPath")]
        public void TestAny()
        {
            isTrue(@"Patient.identifier.any(use = 'official')");
            isTrue(@"Patient.identifier.skip(999).any(use = 'official') = false");   // {}.Any() aways returns true
            isTrue(@"Patient.contained.skip(1).group.group.any(concept.code = 'COMORBIDITY')");       // really need to filter on Questionnare (as('Questionnaire'))
        }

        [TestMethod, TestCategory("FhirPath")]
        public void TestRepeat()
        {
            isTrue(@"Patient.contained.skip(1).repeat(group).count() = 4");       // really need to filter on Questionnare (as('Questionnaire'))
            isTrue(@"Patient.contained.skip(1).repeat(group|question).count() = 11");       // really need to filter on Questionnare (as('Questionnaire'))
            Assert.AreEqual(11L, PathExpression.Scalar(@"Patient.contained.skip(1).repeat(group | question).count()", testInput));       // really need to filter on Questionnare (as('Questionnaire'))
            isTrue(@"Patient.contained.skip(1).repeat(group|question).count() = 11");       // really need to filter on Questionnare (as('Questionnaire'))
            isTrue(@"Patient.contained.skip(1).repeat(group).select(concept.code) contains 'COMORBIDITY'");       // really need to filter on Questionnare (as('Questionnaire'))
            isTrue(@"Patient.contained.skip(1).repeat(group).any(concept.code = 'COMORBIDITY')");       // really need to filter on Questionnare (as('Questionnaire'))
            isTrue(@"Patient.contained.skip(1).repeat(group).any(concept.code = 'CARDIAL') = false");       // really need to filter on Questionnare (as('Questionnaire'))
            isTrue(@"Patient.contained.skip(1).repeat(group|question).any(concept.code = 'CARDIAL')");       // really need to filter on Questionnare (as('Questionnaire'))
        }


        [TestMethod, TestCategory("FhirPath")]
        public void TestExpression()
        {
            isTrue(@"(Patient.identifier.where( use = ( 'offic' + 'ial')) = 
                       Patient.identifier.skip(8 div 2 - 3*2 + 3)) and (Patient.identifier.where(use='usual') = 
                        Patient.identifier.first())");

            isTrue(@"(1|2|3|4|5).where($this > 2 and $this <= 4) = (3|4)");

            isTrue(@"(1|2|2|3|Patient.identifier.first()|Patient.identifier).distinct().count() = 
                        3 + Patient.identifier.count()");

            isTrue(@"(Patient.identifier.where(use='official').last() in Patient.identifier) and
                       (Patient.identifier.first() in Patient.identifier.tail()).not()");

            // TODO: Better error reporting for this:
            //isTrue(@"(Patient.identifier.where(use='official') in Patient.identifier) and
            //           (Patient.identifier.first() in Patient.identifier.tail()).not()");


            //// xpath gebruikt $current for $focus....waarom dat niet gebruiken?
            //isTrue(
            //      @"Patient.contact.relationship.coding.where($focus.system = %vs-patient-contact-relationship and 
            //            $focus.code = 'owner').log('after owner').$parent.$parent.organization.log('org')
            //            .where(display.startsWith('Walt')).resolve().identifier.first().value = 'Gastro'", navigator,
            //                    new TestEvaluationContext()));

            isTrue(@"Patient.name.select(given|family).count() = 2");
            isTrue(@"Patient.identifier.where(use = 'official').select(value + 'yep') = ('7654321yep' | '11223344yep')");

            //isTrue(
            //        @"Patient.**.contains('wne') = contact.relationship.coding.code and
            //        Patient.**.matches('i.*/gif') in Patient.photo.*", navigator));

            //isTrue(
            //    @"'m' + gender.extension('http://example.org/StructureDefinition/real-gender').valueCode
            //        .substring(1,4) + 
            //        gender.extension('http://example.org/StructureDefinition/real-gender').valueCode
            //        .substring(5) = 'metrosexual'", navigator));

            //isTrue(
            //        @"Patient.identifier.any(use='official') and identifier.where(use='usual').any()", navigator));

            //isTrue(
            //        @"gender.extension('http://example.org/StructureDefinition/real-gender').valueCode
            //        .select('m' + $focus.substring(1,4) + $focus.substring(5)) = 'metrosexual'", navigator));

            //isTrue(
            //        @"Patient.**.where($focus.contains('222')).item(1) = $context.contained.address.line", navigator));
        }

        [TestMethod, TestCategory("FhirPath")]
        public void TestExpressionTodayFunction()
        {
            // Check that date comes in
            Assert.AreEqual(PartialDateTime.Today(), PathExpression.Scalar("today()", testInput));

            // Check greater than
            isTrue("today() < @" + PartialDateTime.FromDateTime(DateTime.Today.AddDays(1)));

            // Check less than
            isTrue("today() > @" + PartialDateTime.FromDateTime(DateTime.Today.AddDays(-1)));

            // Check ==
            isTrue("today() = @" + PartialDateTime.Today());

            isTrue("now() > @" + PartialDateTime.Today());
            isTrue("now() >= @" + PartialDateTime.Now());
        }

        [TestMethod, TestCategory("FhirPath")]
        public void TestExpressionSubstringFunction()
        {
            // Check that date comes in
            //isTrue("QuestionnaireResponse.group.group.where(linkId=\"Section - C\").question.where(linkId=\"C1\").answer.group.where(linkId = \"C1fields\").question.where(linkId = \"DateReturnToNormalDuties\").answer.valueDate.empty()", navigator));

            //Assert.IsFalse(PathExpression.IsTrue("QuestionnaireResponse.group.group.where(linkId=\"Section - C\").question.where(linkId=\"C1\").answer.group.where(linkId = \"C1fields\").question.where(linkId = \"DateReturnToNormalDuties\").answer.valueDate.empty().not()", navigator));

            // Assert.AreEqual("1973-05-31", PathExpression.Evaluate("Patient.contained.Patient.birthDate.substring(0,10)", tree).ToString());

            // Assert.AreEqual(null, PathExpression.Evaluate("Patient.birthDate2", tree).ToString());

            // Assert.AreEqual(null, PathExpression.Evaluate("Patient.birthDate2.substring(0,10)", tree).ToString());
        }

        [TestMethod, TestCategory("FhirPath")]
        public void TestExpressionRegexFunction()
        {
            // Check that date comes in
            //isTrue("Patient.identifier.where(system=\"urn:oid:0.1.2.3.4.5.6.7\").value.matches(\"^[1-6]+$\")", navigator));

            //Assert.IsFalse(PathExpression.IsTrue("Patient.identifier.where(system=\"urn:oid:0.1.2.3.4.5.6.7\").value.matches(\"^[1-3]+$\")", navigator));

            // Assert.AreEqual("1973-05-31", PathExpression.Evaluate("Patient.contained.Patient.birthDate.substring(0,10)", tree).ToString());

            // Assert.AreEqual(null, PathExpression.Evaluate("Patient.birthDate2", tree).ToString());

            // Assert.AreEqual(null, PathExpression.Evaluate("Patient.birthDate2.substring(0,10)", tree).ToString());
        }
    }
}
