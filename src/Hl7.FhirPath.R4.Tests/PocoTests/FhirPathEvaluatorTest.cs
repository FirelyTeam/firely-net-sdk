/*
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

// To introduce the DSTU2 FHIR specification
// extern alias dstu2;

using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;
using Hl7.FhirPath;
using Hl7.FhirPath.Expressions;
using Hl7.FhirPath.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Hl7.Fhir.FhirPath.R4.Tests
{
    public class PatientFixture : IDisposable
    {
        public Patient TestInput;
        public Patient PatientExample;
        public Questionnaire Questionnaire;
        public StructureDefinition UuidProfile;
        public int Counter = 0;
        public XDocument Xdoc;

        public PatientFixture()
        {
            var parser = FhirXmlDeserializer.OSTRICH; // There are tons of errors in de demo files
            var tpXml = TestData.ReadTextFile("fp-test-patient.xml");

            TestInput = parser.Deserialize<Patient>(tpXml);

            var epXml = TestData.ReadTextFile("patient-example.xml");
            PatientExample = parser.Deserialize<Patient>(epXml);

            tpXml = TestData.ReadTextFile("questionnaire-example.xml");
            Questionnaire = parser.Deserialize<Questionnaire>(tpXml);

            tpXml = TestData.ReadTextFile("uuid.profile.xml");
            UuidProfile = parser.Deserialize<StructureDefinition>(tpXml);

            Xdoc = new XDocument(new XElement("group", new XAttribute("name", "CSharpTests")));
        }

        public void Save(XDocument document, string filename)
        {
            using (var file = new FileStream(filename, FileMode.Create))
            using (var writer = new StreamWriter(file))
            {
                Xdoc.Save(writer);
            }
        }

        public void Dispose()
        {
            Directory.CreateDirectory(@"c:\temp");
            Save(Xdoc, @"c:\temp\csharp-tests.xml");
        }

        public void IsTrue(string expr) => IsBoolean(expr, true);

        public void IsFalse(string expr) => IsBoolean(expr, false);

        public void IsBoolean(string expr, bool result)
        {
            Counter += 1;
            var testName = "CSharpTest" + Counter.ToString("D4");
            var fileName = "fp-test-patient.xml";

            var testXml = new XElement("test",
                        new XAttribute("name", testName), new XAttribute("inputfile", fileName),
                        new XElement("expression", new XText(expr)),
                        new XElement("output", new XAttribute("type", "boolean"), new XText(result ? "true" : "false")));
            Xdoc.Elements().First().Add(testXml);

            Assert.IsTrue(IsBoolean(TestInput, expr, result));
        }

        public bool IsBoolean(Base baseInput, string expression, bool value, EvaluationContext ctx = null)
        {
            var input = baseInput.ToPocoNode();

            // Don't use the expression cache as we need to inject the debug tracer
            var compiler = new FhirPathCompiler();
            var evaluator = compiler.Compile(expression, true);

            System.Diagnostics.Trace.WriteLine("");
            System.Diagnostics.Trace.WriteLine("------------------------------------");
            System.Diagnostics.Trace.WriteLine(expression);
            System.Diagnostics.Trace.WriteLine("------------------------------------");

            return evaluator.IsBoolean(value, input, ctx ?? new EvaluationContext() { DebugTracer = new DiagnosticsDebugTracer() });
        }


        public void IsTrue(string expr, Base input)
        {
            Assert.IsTrue(IsBoolean(input, expr, true));
        }
    }

    [TestClass]
    public class FhirPathEvaluatorTest
    {
        static PatientFixture fixture;

        [ClassInitialize]
        public static void Initialize(TestContext ctx)
        {
            fixture = new PatientFixture();
        }

        [TestMethod]
        public void TestTreeVisualizerVisitor()
        {
            var compiler = new FhirPathCompiler();
            var expr = compiler.Parse("doSomething('ha!', 4, {}, $this, somethingElse(true))");
            var result = expr.Dump();
            Debug.WriteLine(result);
        }

        [TestMethod]
        public void TestExistence()
        {
            fixture.IsTrue(@"Patient.identifier.exists()");
            fixture.IsTrue(@"Patient.dientifeir.exists().not()");
            fixture.IsTrue(@"Patient.telecom.rank.exists()");
            Assert.AreEqual(3, fixture.TestInput.Scalar(@"identifier.count()"));
            Assert.AreEqual(3, fixture.TestInput.Scalar(@"Patient.identifier.count()"));
            Assert.AreEqual(3, fixture.TestInput.Scalar(@"Patient.identifier.value.count()"));
            Assert.AreEqual(1, fixture.TestInput.Scalar(@"Patient.telecom.rank"));
            fixture.IsTrue(@"Patient.telecom.rank = 1");
        }

        [TestMethod]
        public void TestSDF11Bug()
        {
            Assert.IsTrue(fixture.UuidProfile.IsBoolean("snapshot.element.first().path = type", true));
        }

        [TestMethod]
        public void TestSubsetting()
        {
            fixture.IsTrue(@"Patient.identifier[1] != Patient.identifier.first()");

            fixture.IsTrue(@"Patient.identifier[0] = Patient.identifier.first()");
            fixture.IsTrue(@"Patient.identifier[2] = Patient.identifier.last()");
            fixture.IsTrue(@"Patient.identifier[0] | Patient.identifier[1]  = Patient.identifier.take(2)");
            fixture.IsTrue(@"Patient.identifier.skip(1) = Patient.identifier.tail()");
            fixture.IsTrue(@"Patient.identifier.skip(2) = Patient.identifier.last()");
            fixture.IsTrue(@"Patient.identifier.first().single()");

            try
            {
                fixture.IsTrue(@"Patient.identifier.single()");
                // todo: mh
                // Assert.Fail();
                throw new Exception();
            }
            catch (InvalidOperationException io)
            {
                Assert.Contains("contains more than one element", io.Message);
            }
        }




        [TestMethod]
        public void TestIIfContext()
        {
            var res = fixture.TestInput.Scalar("Patient.contained[0].name[0].given[0].iif(true, single())");
            res.Should().BeOfType<string>().Subject.Should().Be("Eve");
            res = fixture.TestInput.Scalar("Patient.contained[0].name.iif(use = 'official', 'given', given.join(''))"); // comparison between collection and string
            res.Should().BeOfType<string>().Subject.Should().Be("EveEveline");
        }

        [TestMethod]
        public void TestExtension()
        {
            fixture.IsTrue(@"Patient.birthDate.extension('http://hl7.org/fhir/StructureDefinition/patient-birthTime').exists()");
            fixture.IsTrue(@"Patient.birthDate.extension(%""ext-patient-birthTime"").exists()");
            fixture.IsTrue(@"Patient.birthDate.extension('http://hl7.org/fhir/StructureDefinition/patient-birthTime1').empty()");
        }

        [TestMethod]
        public void TestEquality()
        {

            fixture.IsTrue(@"Patient.identifier = Patient.identifier");
            fixture.IsTrue(@"Patient.identifier.first() != Patient.identifier.skip(1)");
            fixture.IsTrue(@"4 = 4");
            fixture.IsTrue(@"4 = 4.0");
            fixture.IsTrue(@"true = true");
            fixture.IsTrue(@"true != false");

            fixture.IsTrue(@"(1|2|3) = (1|2|3)");
            fixture.IsTrue(@"(1|2|3) = (1.0|2.0|3)");
            fixture.IsTrue(@"(1|Patient.identifier|3) = (1|Patient.identifier|3)");
            fixture.IsTrue(@"(3|Patient.identifier|1) != (1|Patient.identifier|3)");

            fixture.IsTrue(@"Patient.gender = 'male'"); // gender has an extension
            fixture.IsTrue(@"Patient.communication = Patient.communication");       // different extensions, same values
            fixture.IsTrue(@"Patient.communication.first() = Patient.communication.skip(1)");       // different extensions, same values
        }


        [TestMethod]
        public void TestCollectionFunctions()
        {
            fixture.IsTrue(@"Patient.identifier.use.distinct() = ('usual' | 'official')");
            fixture.IsTrue(@"Patient.identifier.use.distinct().count() = 2");
            fixture.IsTrue(@"Patient.identifier.use.isDistinct() = false");
            fixture.IsTrue(@"Patient.identifier.system.isDistinct()");
            fixture.IsTrue(@"(3|4).isDistinct()");
            fixture.IsTrue(@"{}.isDistinct()");

            fixture.IsTrue(@"Patient.identifier.skip(1).subsetOf(%context.Patient.identifier)");
            fixture.IsTrue(@"Patient.identifier.supersetOf(%context.Patient.identifier)");
            fixture.IsTrue(@"Patient.identifier.supersetOf({})");
            fixture.IsTrue(@"{}.subsetOf(%context.Patient.identifier)");
        }

        [TestMethod]
        public void TestCollectionOperators()
        {
            fixture.IsTrue(@"Patient.identifier.last() in Patient.identifier");
            fixture.IsTrue(@"4 in (3|4.0|5)");
            fixture.IsTrue(@"(3|4.0|5|3).count() = 3");
            fixture.IsTrue(@"Patient.identifier contains Patient.identifier.last()");
            fixture.IsTrue(@"(3|4.0|5) contains 4");
            fixture.IsTrue(@"({} contains 4) = false");
            fixture.IsTrue(@"(4 in {}) = false");
            fixture.IsTrue(@"Patient.identifier.count() = (Patient.identifier | Patient.identifier).count()");
            fixture.IsTrue(@"(Patient.identifier | Patient.name).count() = Patient.identifier.count() + Patient.name.count()");
            fixture.IsTrue(@"Patient.select(identifier | name).count() = Patient.select(identifier.count() + name.count())");
        }


        [TestMethod]
        public void TestWhere()
        {
            fixture.IsTrue("Patient.identifier.where(use = ('offic' + 'ial')).count() = 2");

            fixture.IsTrue(@"(5 | 'hi' | 4).where($this = 'hi').count()=1");
            fixture.IsTrue(@"{}.where($this = 'hi').count()=0");
        }

        [TestMethod]
        public void TestAll()
        {
            fixture.IsTrue(@"Patient.identifier.skip(1).all(use = 'official')");
            fixture.IsTrue(@"Patient.identifier.skip(999).all(use = 'official')");   // {}.All() aways returns true
            fixture.IsTrue(@"Patient.identifier.skip(1).all({}).empty()");   // empty results still count as "empty"
        }

        [TestMethod]
        public void TestAny()
        {
            fixture.IsTrue(@"Patient.identifier.any(use = 'official')");
            fixture.IsTrue(@"Patient.identifier.skip(999).any(use = 'official') = false");   // {}.Any() always returns false
            fixture.IsTrue(@"Patient.contained.skip(1).item.any(code.code = 'COMORBIDITY')");       // really need to filter on Questionnare (as('Questionnaire'))
        }

        [TestMethod]
        public void TestRepeat()
        {
            fixture.IsTrue(@"Patient.contained.skip(1).repeat(item.where(type='group')).count() = 3");       // really need to filter on Questionnare (as('Questionnaire'))
            fixture.IsTrue(@"Patient.contained.skip(1).repeat(item).count() = 10");       // really need to filter on Questionnare (as('Questionnaire'))
            fixture.IsTrue(@"Patient.contained.skip(1).repeat(item | item.where(type='group')).count() = 10");       // really need to filter on Questionnare (as('Questionnaire'))
            fixture.IsTrue(@"Patient.contained.skip(1).repeat(item ).count() = 10");       // really need to filter on Questionnare (as('Questionnaire'))
            fixture.IsTrue(@"Patient.contained.skip(1).repeat(item).select(code.code) contains 'COMORBIDITY'");       // really need to filter on Questionnare (as('Questionnaire'))
            fixture.IsTrue(@"Patient.contained.skip(1).repeat(item).any(code.code = 'COMORBIDITY')");       // really need to filter on Questionnare (as('Questionnaire'))
            fixture.IsTrue(@"Patient.contained.skip(1).repeat(item.where(type='group')).any(code.code = 'CARDIAL') = false");       // really need to filter on Questionnare (as('Questionnaire'))
            fixture.IsTrue(@"Patient.contained.skip(1).repeat(item).any(code.code = 'CARDIAL')");       // really need to filter on Questionnare (as('Questionnaire'))

            fixture.IsTrue(@"Questionnaire.descendants().linkId.distinct()", fixture.Questionnaire);
            fixture.IsTrue(@"Questionnaire.repeat(item).code.count()", fixture.Questionnaire);
        }


        [TestMethod]
        public void TestExpression()
        {
            fixture.IsTrue(@"(Patient.identifier.where( use = ( 'offic' + 'ial')) = 
                       Patient.identifier.skip(8 div 2 - 3*2 + 3)) and (Patient.identifier.where(use='usual') = 
                        Patient.identifier.first())");
            
            fixture.IsTrue(@"(1|2|3|4|5).where($this > 2 and $this <= 4) = (3|4)");
            
            fixture.IsTrue(@"(1|2|2|3|Patient.identifier.first()|Patient.identifier).distinct().count() = 
                        3 + Patient.identifier.count()");
            
            fixture.IsTrue(@"(Patient.identifier.where(use='official').last() in Patient.identifier) and
                       (Patient.identifier.first() in Patient.identifier.tail()).not()");
            
            fixture.IsTrue(@"Patient.identifier.any(use='official') and identifier.where(use='usual').exists()");
            
            fixture.IsTrue(@"Patient.descendants().where($this.as(string).contains('222'))[1] = %context.contained.address.line");
            
            fixture.IsTrue(@"Patient.name.select(given|family).count() = 2");
            fixture.IsTrue(@"Patient.identifier.where(use = 'official').select(value + 'yep') = ('7654321yep' | '11223344yep')");
            fixture.IsTrue(@"Patient.descendants().where(($this is code) and ($this.contains('wne'))).trace('them') = contact.relationship.coding.code");
            fixture.IsTrue(@"Patient.descendants().as(code).where(matches('i.*/gif')) in Patient.photo.children()");

            fixture.IsTrue(
                @"'m' + gender.extension('http://example.org/StructureDefinition/real-gender').value.as(code)
                    .substring(1,4) + 
                    gender.extension('http://example.org/StructureDefinition/real-gender').value.as(code)
                    .substring(5) = 'metrosexual'");

            fixture.IsTrue(
                    @"gender.extension('http://example.org/StructureDefinition/real-gender').value.as(code)
                    .select('m' + $this.substring(1,4) + $this.substring(5)) = 'metrosexual'");

        }

        [TestMethod]
        public void TestStringOps()
        {
            fixture.IsTrue("Patient.name.family.startsWith('')");
            fixture.IsTrue("Patient.name.family.startsWith('Don')");
            fixture.IsTrue("Patient.name.family.startsWith('Dox')=false");

            fixture.IsTrue("Patient.name.family.endsWith('')");
            fixture.IsTrue("Patient.name.family.endsWith('ald')");
            fixture.IsTrue("Patient.name.family.endsWith('old')=false");

            fixture.IsTrue("Patient.identifier.where(system='urn:oid:0.1.2.3.4.5.6.7').value.matches('^[1-6]+$')");
            fixture.IsTrue("Patient.identifier.where(system='urn:oid:0.1.2.3.4.5.6.7').value.matches('^[1-3]+$') = false");

            fixture.IsTrue("Patient.contained.name[0].family.indexOf('ywo') = 4");
            fixture.IsTrue("Patient.contained.name[0].family.indexOf('') = 0");
            fixture.IsTrue("Patient.contained.name[0].family.indexOf('qq') = -1");

            fixture.IsTrue("Patient.contained.name[0].family.contains('ywo')");
            fixture.IsTrue("Patient.contained.name[0].family.contains('ywox')=false");
            fixture.IsTrue("Patient.contained.name[0].family.contains('')");

            fixture.IsTrue(@"'11/30/1972'.replaceMatches('\\b(?<month>\\d{1,2})/(?<day>\\d{1,2})/(?<year>\\d{2,4})\\b','${day}-${month}-${year}') = '30-11-1972'");

            fixture.IsTrue(@"'abc'.replace('a', 'q') = 'qbc'");
            fixture.IsTrue(@"'abc'.replace('a', 'qq') = 'qqbc'");
            fixture.IsTrue(@"'abc'.replace('q', 'x') = 'abc'");
            fixture.IsTrue(@"'abc'.replace('ab', '') = 'c'");
            fixture.IsTrue(@"'abc'.replace('', 'xh') = 'xhaxhbxhcxh'");

            fixture.IsTrue("Patient.contained.name[0].family.length() = " + "Everywoman".Length);
            fixture.IsTrue("''.length() = 0");
            fixture.IsTrue("{}.length().empty()");
        }


        //[TestMethod]
        //public void TestUnionNotDistinct()
        //{
        //    var patXml = @"<Patient xmlns='http://hl7.org/fhir'>
        //        <name>
        //            <given value='bobs' />
        //            <given value='bobs' />
        //            <given value='bob2' />
        //            <family value='f1' />
        //            <family value='f2' />
        //        </name>
        //        <birthDate value='1973' />
        //    </Patient>";

        //    var pat = (new FhirXmlParser()).Parse<Patient>(patXml);
        //    var patNav = new PocoNavigator(pat);

        //    var result = PathExpression.Select("name.given | name.family", new[] { patNav }
        //    Assert.Equal(5, result.Count());
        //}

        [TestMethod]
        public void CompilationIsCached()
        {
            // If the test failed, try again, we might have been
            // bugged by temporary slowness of the CI build.
            if (!test())
            {
                Assert.IsTrue(test());
            }

            static bool test()
            {
                var uncached = run(null, out var last);
                var cached = run(last, out var _);
                Console.WriteLine("Uncached: {0}, cached: {1}".FormatWith(uncached, cached));

                return cached < uncached / 2;
            }

            static long run(string fixd, out string lastExpression)
            {
                lastExpression = null;
                var sw = new Stopwatch();
                sw.Start();

                var random = new Random();

                // something that has not been compiled before
                for (int i = 0; i < 1000; i++)
                {
                    var next = random.Next(0, 10000);
                    lastExpression = fixd ?? $"Patient.name[{next}]";
                    fixture.TestInput.Select(lastExpression);
                }
                sw.Stop();

                return sw.ElapsedMilliseconds;
            }
        }

        // Verifies https://github.com/FirelyTeam/firely-net-sdk/issues/1140
        [TestMethod]
        public void TestELD13Bug()
        {
            var emptyPat = new Patient();

            // Test how the engine treats primitives with no values in operations that
            // do not propagate null values....
            Assert.AreEqual("", emptyPat.Scalar("name & gender"));
        }
    }

    [TestClass]
    public class FhirPathDefineVariableTests
    {
        static PatientFixture fixture;

        [ClassInitialize]
        public static void Initialize(TestContext ctx)
        {
            fixture = new PatientFixture();
        }

        [TestMethod]
        public void SimplestVariable()
        {
            var expr = "defineVariable('v1', 'value1').select(%v1)";
            var r = fixture.PatientExample.Select(expr).ToList();
            Assert.AreEqual(1, r.Count());
            Assert.AreEqual("value1", r.First().ToString());
        }

        [TestMethod]
        public void SimpleUseOfAVariable()
        {
            var expr = "defineVariable('n1', name.first()).select(%n1.given)";
            var r = fixture.PatientExample.Select(expr).ToList();
            Assert.AreEqual(2, r.Count());
            Assert.AreEqual("Peter", r.First().ToString());
            Assert.AreEqual("James", r.Skip(1).First().ToString());
            // .toStrictEqual(["Peter", "James"]);
        }

        [TestMethod]
        public void simple_use_of_a_variable_2_selects()
        {
            var expr = "defineVariable('n1', name.first()).select(%n1.given).first()";
            var r = fixture.PatientExample.Select(expr).ToList();
            Assert.AreEqual(1, r.Count());
            Assert.AreEqual("Peter", r.First().ToString());
        }

        [TestMethod]
        public void use_of_a_variable_in_separate_contexts()
        {
            // this example defines the same variable name in 2 different contexts
            // this shouldn't report an issue where the variable is being redefined (as it's not in the same context)
            var expr = "defineVariable('n1', name.first()).select(%n1.given) | defineVariable('n1', name.skip(1).first()).select(%n1.given)";
            var r = fixture.PatientExample.Select(expr).ToList();
            Assert.AreEqual(3, r.Count());
            Assert.AreEqual("Peter", r.First().ToString());
            Assert.AreEqual("James", r.Skip(1).First().ToString());
            Assert.AreEqual("Jim", r.Skip(2).First().ToString());
            // .toStrictEqual(["Peter", "James", "Jim"]);
        }

        [TestMethod]
        public void use_of_a_variable_in_separate_contexts_defined_in_2_but_used_in_1()
        {
            // this example defines the same variable name in 2 different contexts,
            // but only uses it in the second. This ensures that the first context doesn't remain when using it in another context
            var expr = "defineVariable('n1', name.first()).where(active.not()) | defineVariable('n1', name.skip(1).first()).select(%n1.given)";
            var r = fixture.PatientExample.Select(expr).ToList();
            Assert.AreEqual(1, r.Count());
            Assert.AreEqual("Jim", r.First().ToString());
            // .toStrictEqual(["Jim"]);
        }

        [TestMethod]
        public void use_of_different_variables_in_different_contexts()
        {
            var expr = "defineVariable('n1', name.first()).select(id & '-' & %n1.given.join('|')) | defineVariable('n2', name.skip(1).first()).select(%n2.given)";
            var r = fixture.PatientExample.Select(expr).ToList();
            Assert.AreEqual(2, r.Count());
            Assert.AreEqual("example-Peter|James", r.First().ToString());
            Assert.AreEqual("Jim", r.Skip(1).First().ToString());
            // .toStrictEqual(["example-Peter|James", "Jim"]);
        }

        [TestMethod]
        public void Two_vars_one_unused()
        {
            var expr = "defineVariable('n1', name.first()).active | defineVariable('n2', name.skip(1).first()).select(%n2.given)";
            var r = fixture.PatientExample.Select(expr).ToList();
            Assert.AreEqual(2, r.Count());
            Assert.IsTrue(((FhirBoolean)r.First()).Value);
            Assert.AreEqual("Jim", r.Skip(1).First().ToString());
            // .toStrictEqual([true, "Jim"]);
        }

        [TestMethod]
        public void composite_variable_use()
        {
            var expr = "defineVariable('v1', 'value1').select(%v1).trace('data').defineVariable('v2', 'value2').select($this & ':' & %v1 & '-' & %v2) | defineVariable('v3', 'value3').select(%v3)";
            var r = fixture.PatientExample.Select(expr).ToList();
            Assert.AreEqual(2, r.Count());
            Assert.AreEqual("value1:value1-value2", r.First().ToString());
            Assert.AreEqual("value3", r.Skip(1).First().ToString());
            //.toStrictEqual(["value1:value1-value2", "value3"]);
        }



        [TestMethod]
        public void use_of_a_variable_outside_context_throws_error()
        {
            // test with a variable that is not in the context that should throw an error
            var expr = "defineVariable('n1', name.first()).active | defineVariable('n2', name.skip(1).first()).select(%n1.given)";
            try
            {
                var r = fixture.PatientExample.Select(expr).ToList();
                Assert.Fail("Should have thrown an exception");
            }
            catch(InvalidOperationException ex)
            {
                ex.Message.Should().Contain("Unknown symbol 'n1'");
            }
        }

        [TestMethod]
        public void use_undefined_variable_throws_error()
        {
            // test with a variable that is not in the context that should throw an error
            var expr = "select(%fam.given)";
            try
            {
                var r = fixture.PatientExample.Select(expr).ToList();
                Assert.Fail("Should have thrown an exception");
            }
            catch(InvalidOperationException ex)
            {
                ex.Message.Should().Contain("Unknown symbol 'fam'");
            }
        }

        [TestMethod]
        public void redefining_variable_throws_error()
        {
            var expr = "defineVariable('v1').defineVariable('v1').select(%v1)";
            Assert.Throws<InvalidOperationException>(() => fixture.PatientExample.Select(expr).ToList());
        }


        [TestMethod]
        public void sequence_of_variable_definitions_tweak()
        {
            var expr = "Patient.name.defineVariable('n2', skip(1).first()).defineVariable('res', %n2.given+%n2.given).select(%res)";
            var r = fixture.PatientExample.Select(expr).ToList();
            Assert.AreEqual(2, r.Count());
            Assert.AreEqual("JimJim", r.First().ToString());
            Assert.AreEqual("JimJim", r.Skip(1).First().ToString());
            // .toStrictEqual(["JimJim", "JimJim", "JimJim"]);
        }

        [TestMethod]
        public void sequence_of_variable_definitions_original()
        {
            // A variable defined based on another variable
            var expr = "Patient.name.defineVariable('n1', first()).exists(%n1) | Patient.name.defineVariable('n2', skip(1).first()).defineVariable('res', %n2.given+%n2.given).select(%res)";
            var r = fixture.PatientExample.Select(expr).ToList();
            Assert.AreEqual(2, r.Count());
            Assert.IsTrue(((FhirBoolean)r.First()).Value);
            Assert.AreEqual("JimJim", r.Skip(1).First().ToString());
            // the duplicate JimJim values are removed due to the | operator
            // .toStrictEqual([true, "JimJim"]);
        }


        [TestMethod]
        public void multi_tree_vars_valid()
        {
            var expr = "defineVariable('root', 'r1-').select(defineVariable('v1', 'v1').defineVariable('v2', 'v2').select(%v1 | %v2)).select(%root & $this)";
            var r = fixture.PatientExample.Select(expr).ToList();
            Assert.AreEqual(2, r.Count());
            Assert.AreEqual("r1-v1", r.First().ToString());
            Assert.AreEqual("r1-v2", r.Skip(1).First().ToString());
            // .toStrictEqual(["r1-v1", "r1-v2"]);
        }

        [TestMethod]
        public void defineVariable_with_compile_success()
        {
            var expr = "defineVariable('root', 'r1-').select(defineVariable('v1', 'v1').defineVariable('v2', 'v2').select(%v1 | %v2)).select(%root & $this)";
            var compiler = new FhirPathCompiler();
            var exprCompiled = compiler.Compile(expr);
            var r = exprCompiled(fixture.PatientExample.ToPocoNode(), new FhirEvaluationContext());
            Assert.AreEqual(2, r.Count());
            Assert.AreEqual("r1-v1", r.First().GetValue());
            Assert.AreEqual("r1-v2", r.Skip(1).First().GetValue());
            // .toStrictEqual(["r1-v1", "r1-v2"]);
        }
        /*
        [TestMethod]
        public void defineVariable_with_compile_error()
        {
            var expr = "defineVariable('root', 'r1-').select(defineVariable('v1', 'v1').defineVariable('v2', 'v2').select(%v1 | %v2)).select(%root & $this & %v1)";
            var f = fhirpath.compile(expr, r4_model);
            expect(() => { f(input.patientExample); })
                      .toThrowError("Attempting to access an undefined environment variable: v1");
        }

        [TestMethod]
        public void defineVariable_cant_overwrite_an_environment_var()
        {
            var expr = "defineVariable('context', 'oops')";
            var f = fhirpath.compile(expr, r4_model);
            expect(() => { f(input.patientExample); })
              .toThrowError("Environment Variable %context already defined");
        }

        [TestMethod]
        public void realistic_example_with_conceptmap()
        {
            var expr = """
                group.select(
                  defineVariable('grp')
                  .element
                        .select(
                        defineVariable('ele')
                        .target
                    .select(% grp.source & '|' & % ele.code & ' ' & equivalence & ' ' & % grp.target & '|' & code)
                        )
                        )
                        .trace('all')
                 .isDistinct()
                """;
            expect(fhirpath.evaluate(input.conceptMapExample, expr, r4_model)
            ).toStrictEqual([
              false
            ]);
        }
        */

        [TestMethod]
        public void defineVariable_in_function_parameters1()
        {
            var expr = "defineVariable(defineVariable('param','ppp').select(%param), defineVariable('param','value').select(%param)).select(%ppp)";
            var r = fixture.PatientExample.Select(expr).ToList();
            Assert.AreEqual(1, r.Count());
            Assert.AreEqual("value", r.First().ToString());
            // .toStrictEqual(["value"]);
        }

        [TestMethod]
        public void defineVariable_in_function_parameters2()
        {
            var expr = "'aaa'.replace(defineVariable('param', 'aaa').select(%param), defineVariable('param','bbb').select(%param))";
            var r = fixture.PatientExample.Select(expr).ToList();
            Assert.AreEqual(1, r.Count());
            Assert.AreEqual("bbb", r.First().ToString());
            // .toStrictEqual(["bbb"]);
        }
    }
}