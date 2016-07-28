/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

// To introduce the DSTU2 FHIR specification
extern alias dstu2;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.FluentPath;
using Hl7.Fhir.FluentPath.Expressions;
using System.Diagnostics;
using dstu2::Hl7.Fhir.Model;
using Hl7.Fhir.FluentPath.Binding;
using Hl7.Fhir.Support;
using System.Xml.Linq;

namespace Hl7.Fhir.Tests.FhirPath
{
    [TestClass]
#if PORTABLE45
	public class PortableFhirPathEvaluatorTest
#else
    public class FhirPathEvaluatorTest
#endif
    {
        static IEnumerable<IValueProvider> testInput;
        static IEnumerable<IValueProvider> questionnaire;
        static int counter = 0;
        static XDocument xdoc;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var parser = new dstu2::Hl7.Fhir.Serialization.FhirXmlParser();

            var tpXml = System.IO.File.ReadAllText("TestData\\fp-test-patient.xml");
            var patient = parser.Parse<Patient>(tpXml);
            testInput = FhirValueList.Create(new ModelNavigator(patient));
           
            tpXml = System.IO.File.ReadAllText("TestData\\questionnaire-example.xml");
            var quest = parser.Parse<Questionnaire>(tpXml);
            questionnaire = FhirValueList.Create(new ModelNavigator(quest));

            xdoc = new XDocument(new XElement("group", new XAttribute("name", "CSharpTests")));
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            xdoc.Save(@"c:\temp\csharp-tests.xml");
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
            Assert.AreEqual(3L, PathExpression.Scalar(@"identifier.count()", testInput));
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
            Assert.AreEqual("ello", PathExpression.Scalar(@"$this[0].substring(1,%context[1])", input));
        }


        [TestMethod, TestCategory("FhirPath")]
        public void TestSubsetting()
        {
            isTrue(@"Patient.identifier[1] != Patient.identifier.first()");

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
            catch (InvalidOperationException io)
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
            counter += 1;
            var testName = "CSharpTest" + counter.ToString("D4");
            var fileName = "fp-test-patient.xml";

            var testXml = new XElement("test",
                        new XAttribute("name", testName), new XAttribute("inputfile", fileName),
                        new XElement("expression", new XText(expr)),
                        new XElement("output", new XAttribute("type", "boolean"), new XText("true")));
            xdoc.Elements().First().Add(testXml);

            Assert.IsTrue(PathExpression.Compile(expr).IsBoolean(true,testInput));
        }

        private void isTrue(string expr, IEnumerable<IValueProvider> input)
        {
            Assert.IsTrue(PathExpression.Compile(expr).IsBoolean(true,input));
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
            isTrue(@"@2014-12-14.toString() = '2014-12-14'");
        }

        [TestMethod, TestCategory("FhirPath")]
        public void TestIIf()
        {
            isTrue(@"Patient.name.iif(exists(), 'named', 'unnamed') = 'named'");
            isTrue(@"Patient.name.iif(empty(), 'unnamed', 'named') = 'named'");

            isTrue(@"Patient.contained[0].name.iif(exists(), 'named', 'unnamed') = 'named'");
            isTrue(@"Patient.contained[0].name.iif(empty(), 'unnamed', 'named') = 'named'");

            isTrue(@"Patient.name.iif({}, 'named', 'unnamed') = 'unnamed'");
        }

        [TestMethod, TestCategory("FhirPath")]
        public void TestExtension()
        {
            isTrue(@"Patient.birthDate.extension('http://hl7.org/fhir/StructureDefinition/patient-birthTime').exists()");
            isTrue(@"Patient.birthDate.extension(%""ext-patient-birthTime"").exists()");
            isTrue(@"Patient.birthDate.extension('http://hl7.org/fhir/StructureDefinition/patient-birthTime1').empty()");
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
            isTrue(@"Patient.communication = Patient.communication");       // different extensions, same values
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
        public void TestEquivalence()
        {
            isTrue("@2012-04-15 ~ @2012-04-15T10:00:00");
            isTrue("@T10:01:02Z !~ @T10:01:55+01:00");
        }

        public static string ToString(IValueProvider nav)
        {
            var result = "";

            if (nav is INameProvider)
            {
                result = ((INameProvider)nav).Name;
            }

            if (nav is ITypeNameProvider)
            {
                var tnp = (ITypeNameProvider)nav;
                result += ": " + tnp.TypeName;
            }

            if (nav.Value != null) result += " = " + nav.Value;

            return result;
        }

        public static void Render(IElementNavigator navigator, int nest = 0)
        {
            do
            {
                string indent = new string(' ', nest * 4);
                Debug.WriteLine($"{indent}" + ToString(navigator));

                var child = navigator.Clone();
                if (child.MoveToFirstChild())
                {
                    Render(child, nest + 1);
                }
            }
            while (navigator.MoveToNext());
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
            isTrue(@"Patient.contained.skip(1).repeat(group | question).count() = 11");       // really need to filter on Questionnare (as('Questionnaire'))
            isTrue(@"Patient.contained.skip(1).repeat(group|question).count() = 11");       // really need to filter on Questionnare (as('Questionnaire'))
            isTrue(@"Patient.contained.skip(1).repeat(group).select(concept.code) contains 'COMORBIDITY'");       // really need to filter on Questionnare (as('Questionnaire'))
            isTrue(@"Patient.contained.skip(1).repeat(group).any(concept.code = 'COMORBIDITY')");       // really need to filter on Questionnare (as('Questionnaire'))
            isTrue(@"Patient.contained.skip(1).repeat(group).any(concept.code = 'CARDIAL') = false");       // really need to filter on Questionnare (as('Questionnaire'))
            isTrue(@"Patient.contained.skip(1).repeat(group|question).any(concept.code = 'CARDIAL')");       // really need to filter on Questionnare (as('Questionnaire'))

            isTrue(@"Questionnaire.descendants().linkId.distinct()", questionnaire);
            isTrue(@"Questionnaire.repeat(group | question).concept.count()", questionnaire);
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

            isTrue(@"Patient.identifier.any(use='official') and identifier.where(use='usual').exists()");

            isTrue(@"Patient.descendants().where($this.as(string).contains('222'))[1] = %context.contained.address.line");

            isTrue(@"Patient.name.select(given|family).count() = 2");
            isTrue(@"Patient.identifier.where(use = 'official').select(value + 'yep') = ('7654321yep' | '11223344yep')");
            isTrue(@"Patient.descendants().where(($this is code) and ($this.contains('wne'))).trace('them') = contact.relationship.coding.code");
            isTrue(@"Patient.descendants().as(code).where(matches('i.*/gif')) in Patient.photo.children()");

            isTrue(
                @"'m' + gender.extension('http://example.org/StructureDefinition/real-gender').value.as(code)
                    .substring(1,4) + 
                    gender.extension('http://example.org/StructureDefinition/real-gender').value.as(code)
                    .substring(5) = 'metrosexual'");

            isTrue(
                    @"gender.extension('http://example.org/StructureDefinition/real-gender').value.as(code)
                    .select('m' + $this.substring(1,4) + $this.substring(5)) = 'metrosexual'");

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
        public void TestSubstring()
        {
            isTrue("Patient.name.family.substring(0,6) = 'Donald'");
            isTrue("Patient.name.family.substring(2,6) = 'nald'");
            isTrue("Patient.name.family.substring(2,4) = 'nald'");

            isTrue("Patient.name.family.substring(-1,8).empty()");
            isTrue("Patient.name.family.substring(999,1).empty()");
            isTrue("''.substring(0,1).empty()");
            isTrue("{}.substring(0,10).empty()");
            isTrue("{}.substring(0,10).empty()");

            try
            {
                // TODO: Improve exception on this one
                isTrue("Patient.identifier.use.substring(0,10)");
                Assert.Fail();
            }
            catch (InvalidOperationException)
            {
            }
        }

        [TestMethod, TestCategory("FhirPath")]
        public void TestStringOps()
        {
            isTrue("Patient.name.family.startsWith('')");
            isTrue("Patient.name.family.startsWith('Don')");
            isTrue("Patient.name.family.startsWith('Dox')=false");

            isTrue("Patient.name.family.endsWith('')");
            isTrue("Patient.name.family.endsWith('ald')");
            isTrue("Patient.name.family.endsWith('old')=false");

            isTrue("Patient.identifier.where(system='urn:oid:0.1.2.3.4.5.6.7').value.matches('^[1-6]+$')");
            isTrue("Patient.identifier.where(system='urn:oid:0.1.2.3.4.5.6.7').value.matches('^[1-3]+$') = false");

            isTrue("Patient.contained.name[0].family.indexOf('ywo') = 4");
            isTrue("Patient.contained.name[0].family.indexOf('') = 0");
            isTrue("Patient.contained.name[0].family.indexOf('qq').empty()");

            isTrue("Patient.contained.name[0].family.contains('ywo')");
            isTrue("Patient.contained.name[0].family.contains('ywox')=false");
            isTrue("Patient.contained.name[0].family.contains('')");

            isTrue(@"'11/30/1972'.replaceMatches('\\b(?<month>\\d{1,2})/(?<day>\\d{1,2})/(?<year>\\d{2,4})\\b','${day}-${month}-${year}') = '30-11-1972'");

            isTrue(@"'abc'.replace('a', 'q') = 'qbc'");
            isTrue(@"'abc'.replace('a', 'qq') = 'qqbc'");
            isTrue(@"'abc'.replace('q', 'x') = 'abc'");
            isTrue(@"'abc'.replace('ab', '') = 'c'");
            isTrue(@"'abc'.replace('', 'xh') = 'xhaxhbxhc'");

            isTrue("Patient.contained.name[0].family.length() = " + "Everywoman".Length);
            isTrue("''.length() = 0");
            isTrue("{}.length().empty()");
        }
    }
}
