/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

// To introduce the DSTU2 FHIR specification
// extern alias dstu2;

using System;
using System.Linq;
using Hl7.FhirPath.Expressions;
using System.Diagnostics;
using Hl7.Fhir.Model;
using System.Xml.Linq;
using Xunit;
using System.IO;
using Xunit.Abstractions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model.Primitives;
using Hl7.Fhir.Utility;
using Hl7.Fhir.FhirPath;

namespace Hl7.FhirPath.Tests
{
    public class PatientFixture : IDisposable
    {
        public IElementNavigator TestInput;
        public IElementNavigator Questionnaire;
        public IElementNavigator UuidProfile;
        public int Counter = 0;
        public XDocument Xdoc;

    
        public PatientFixture()
        {
            var parser = new Hl7.Fhir.Serialization.FhirXmlParser();
            var tpXml = TestData.ReadTextFile("fp-test-patient.xml");

            var patient = parser.Parse<Patient>(tpXml);
            TestInput = new PocoNavigator(patient);

            tpXml = TestData.ReadTextFile("questionnaire-example.xml");
            var quest = parser.Parse<Questionnaire>(tpXml);
            Questionnaire = new PocoNavigator(quest);

            tpXml = TestData.ReadTextFile("uuid.profile.xml");
            var uuid = parser.Parse<StructureDefinition>(tpXml);
            UuidProfile = new PocoNavigator(uuid);

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

        public void IsTrue(string expr)
        {
            Counter += 1;
            var testName = "CSharpTest" + Counter.ToString("D4");
            var fileName = "fp-test-patient.xml";

            var testXml = new XElement("test",
                        new XAttribute("name", testName), new XAttribute("inputfile", fileName),
                        new XElement("expression", new XText(expr)),
                        new XElement("output", new XAttribute("type", "boolean"), new XText("true")));
            Xdoc.Elements().First().Add(testXml);

            Assert.True(TestInput.IsBoolean(expr, true));
        }

        public void IsTrue(string expr, IElementNavigator input)
        {
            Assert.True(input.IsBoolean(expr, true));
        }
    }

    public class FhirPathEvaluatorTest : IClassFixture<PatientFixture>
    {
        PatientFixture fixture;

        private readonly ITestOutputHelper output;


        public FhirPathEvaluatorTest(PatientFixture fixture, ITestOutputHelper output)
        {
            ElementNavFhirExtensions.PrepareFhirSymbolTableFunctions();

            this.fixture = fixture;
            this.output = output;
        }

        [Fact]
        public void TestTreeVisualizerVisitor()
        {
            var compiler = new FhirPathCompiler();
            var expr = compiler.Parse("doSomething('ha!', 4, {}, $this, somethingElse(true))");
            var result = expr.Dump();
            Debug.WriteLine(result);
        }

        [Fact]
        public void TestExistence()
        {
            fixture.IsTrue(@"{}.empty()");
            fixture.IsTrue(@"1.empty().not()");
            fixture.IsTrue(@"1.exists()");
            fixture.IsTrue(@"Patient.identifier.exists()");
            fixture.IsTrue(@"Patient.dientifeir.exists().not()");
            fixture.IsTrue(@"Patient.telecom.rank.exists()");
            Assert.Equal(3L, fixture.TestInput.Scalar(@"identifier.count()"));
            Assert.Equal(3L, fixture.TestInput.Scalar(@"Patient.identifier.count()"));
            Assert.Equal(3L, fixture.TestInput.Scalar(@"Patient.identifier.value.count()"));
            Assert.Equal(1L, fixture.TestInput.Scalar(@"Patient.telecom.rank"));
            fixture.IsTrue(@"Patient.telecom.rank = 1");
        }

        [Fact]
        public void TestNullPropagation()
        {
            fixture.IsTrue(@"({}.substring(0)).empty()");
            fixture.IsTrue(@"('hello'.substring({})).empty()");
        }

        [Fact]
        public void TestDynaBinding()
        {
            var input = (ElementNode.Node("root", 
                    ElementNode.Valued("child", "Hello world!", "string"), ElementNode.Valued("child", 4L, "integer"))).ToNavigator();

            Assert.Equal("ello", input.Scalar(@"$this.child[0].substring(1,%context.child[1])"));
        }


        [Fact]
        public void TestSDF11Bug()
        {
            Assert.True(fixture.UuidProfile.IsBoolean("snapshot.element.first().path = constrainedType", true));
        }

        [Fact]
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
                Assert.True(io.Message.Contains("contains more than one element"));
            }
        }


        [Fact]
        public void TestGreaterThan()
        {
            fixture.IsTrue(@"4.5 > 0");
            fixture.IsTrue(@"'ewout' > 'alfred'");
            fixture.IsTrue(@"2016-04-01 > 2015-04-01");
            fixture.IsTrue(@"5 > 6 = false");
            fixture.IsTrue(@"(5 > {}).empty()");
        }



        

        [Fact]
        public void TestMath()
        {
            fixture.IsTrue(@"-4.5 + 4.5 = 0");
            fixture.IsTrue(@"4/2 = 2");
            fixture.IsTrue(@"2/4 = 0.5");
            fixture.IsTrue(@"10/4 = 2.5");
            fixture.IsTrue(@"10.0/4 = 2.5");
            fixture.IsTrue(@"4.0/2.0 = 2");
            fixture.IsTrue(@"2.0/4 = 0.5");
            fixture.IsTrue(@"2.0 * 4 = 8");
            fixture.IsTrue(@"2 * 4.1 = 8.2");
            fixture.IsTrue(@"-0.5 * 0.5 = -0.25");
            fixture.IsTrue(@"5 - 4.5 = 0.5");
            fixture.IsTrue(@"9.5 - 4.5 = 5");
            fixture.IsTrue(@"5 + 4.5 = 9.5");
            fixture.IsTrue(@"9.5 + 0.5 = 10");

            fixture.IsTrue(@"103 mod 5 = 3");
            fixture.IsTrue(@"101.4 mod 5.2 = 2.6");
            fixture.IsTrue(@"103 div 5 = 20");
            fixture.IsTrue(@"20.0 div 5.5 = 3");

            fixture.IsTrue(@"'offic'+'ial' = 'official'");

            fixture.IsTrue(@"12/(2+2) - (3 div 2) = 2");
            fixture.IsTrue(@"-4.5 + 4.5 * 2 * 4 / 4 - 1.5 = 3");
        }


        [Fact]
        public void Test3VLBoolean()
        {
            fixture.IsTrue(@"true and true");
            fixture.IsTrue(@"(true and false) = false");
            fixture.IsTrue(@"(true and {}).empty()");
            fixture.IsTrue(@"(false and true) = false");
            fixture.IsTrue(@"(false and false) = false");
            fixture.IsTrue(@"(false and {}) = false");
            fixture.IsTrue(@"({} and true).empty()");
            fixture.IsTrue(@"({} and false) = false");
            fixture.IsTrue(@"({} and {}).empty()");

            fixture.IsTrue(@"true or true");
            fixture.IsTrue(@"true or false");
            fixture.IsTrue(@"true or {}");
            fixture.IsTrue(@"false or true");
            fixture.IsTrue(@"(false or false) = false");
            fixture.IsTrue(@"(false or {}).empty()");
            fixture.IsTrue(@"{} or true");
            fixture.IsTrue(@"({} or false).empty()");
            fixture.IsTrue(@"({} or {}).empty()");

            fixture.IsTrue(@"(true xor true)=false");
            fixture.IsTrue(@"true xor false");
            fixture.IsTrue(@"(true xor {}).empty()");
            fixture.IsTrue(@"false xor true");
            fixture.IsTrue(@"(false xor false) = false");
            fixture.IsTrue(@"(false xor {}).empty()");
            fixture.IsTrue(@"({} xor true).empty()");
            fixture.IsTrue(@"({} xor false).empty()");
            fixture.IsTrue(@"({} xor {}).empty()");

            fixture.IsTrue(@"true implies true");
            fixture.IsTrue(@"(true implies false) = false");
            fixture.IsTrue(@"(true implies {}).empty()");
            fixture.IsTrue(@"false implies true");
            fixture.IsTrue(@"false implies false");
            fixture.IsTrue(@"false implies {}");
            fixture.IsTrue(@"{} implies true");
            fixture.IsTrue(@"({} implies false).empty()");
            fixture.IsTrue(@"({} implies {}).empty()");
        }

        [Fact]
        public void TestLogicalShortcut()
        {
            fixture.IsTrue(@"true or (1/0 = 0)");
            fixture.IsTrue(@"(false and (1/0 = 0)) = false");            
        }


        [Fact]
        public void TestConversions()
        {
            fixture.IsTrue(@"(4.1).toString() = '4.1'");
            fixture.IsTrue(@"true.toString() = 'true'");
            fixture.IsTrue(@"true.toDecimal() = 1");
            fixture.IsTrue(@"Patient.identifier.value.first().toDecimal() = 654321");
            fixture.IsTrue(@"@2014-12-14.toString() = '2014-12-14'");
        }

        [Fact]
        public void TestIIf()
        {
            fixture.IsTrue(@"Patient.name.iif(exists(), 'named', 'unnamed') = 'named'");
            fixture.IsTrue(@"Patient.name.iif(empty(), 'unnamed', 'named') = 'named'");

            fixture.IsTrue(@"Patient.contained[0].name.iif(exists(), 'named', 'unnamed') = 'named'");
            fixture.IsTrue(@"Patient.contained[0].name.iif(empty(), 'unnamed', 'named') = 'named'");

            fixture.IsTrue(@"Patient.name.iif({}, 'named', 'unnamed') = 'unnamed'");

         //   fixture.IsTrue(@"Patient.name[0].family.iif(length()-8 != 0, 5/(length()-8), 'no result') = 'no result'");
        }

        [Fact]
        public void TestExtension()
        {
            fixture.IsTrue(@"Patient.birthDate.extension('http://hl7.org/fhir/StructureDefinition/patient-birthTime').exists()");
            fixture.IsTrue(@"Patient.birthDate.extension(%""ext-patient-birthTime"").exists()");
            fixture.IsTrue(@"Patient.birthDate.extension('http://hl7.org/fhir/StructureDefinition/patient-birthTime1').empty()");
        }

        [Fact]
        public void TestEquality()
        {
            fixture.IsTrue(@"4 = 4");
            fixture.IsTrue(@"4 = 4.0");
            fixture.IsTrue(@"true = true");
            fixture.IsTrue(@"true != false");

            fixture.IsTrue(@"Patient.identifier = Patient.identifier");
            fixture.IsTrue(@"Patient.identifier.first() != Patient.identifier.skip(1)");
            fixture.IsTrue(@"(1|2|3) = (1|2|3)");
            fixture.IsTrue(@"(1|2|3) = (1.0|2.0|3)");
            fixture.IsTrue(@"(1|Patient.identifier|3) = (1|Patient.identifier|3)");
            fixture.IsTrue(@"(3|Patient.identifier|1) != (1|Patient.identifier|3)");

            fixture.IsTrue(@"Patient.gender = 'male'"); // gender has an extension
            fixture.IsTrue(@"Patient.communication = Patient.communication");       // different extensions, same values
            fixture.IsTrue(@"Patient.communication.first() = Patient.communication.skip(1)");       // different extensions, same values

            fixture.IsTrue(@"@2015-01-01 = @2015-01-01");
            fixture.IsTrue(@"@2015-01-01 != @2015-01");
            fixture.IsTrue(@"@2015-01-01T13:40:50+00:00 = @2015-01-01T13:40:50Z");
            fixture.IsTrue(@"@T13:45:02Z = @T13:45:02+00:00");
            fixture.IsTrue(@"@T13:45:02+00:00 != @T13:45:02+01:00");
        }

        [Fact]
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

        [Fact]
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


        [Fact]
        public void TestEquivalence()
        {
            fixture.IsTrue("@2012-04-15 ~ @2012-04-15T10:00:00");
            fixture.IsTrue("@T10:01:02Z !~ @T10:01:55+01:00");
        }

        public static string ToString(IElementNavigator nav)
        {
            var result = nav.Name;

            if (nav.Type != null)
            {
                result += ": " + nav.Type;
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


        [Fact]
        public void TestWhere()
        {
            fixture.IsTrue("Patient.identifier.where(use = ('offic' + 'ial')).count() = 2");

            fixture.IsTrue(@"(5 | 'hi' | 4).where($this = 'hi').count()=1");
            fixture.IsTrue(@"{}.where($this = 'hi').count()=0");
        }

        [Fact]
        public void TestAll()
        {
            fixture.IsTrue(@"Patient.identifier.skip(1).all(use = 'official')");
            fixture.IsTrue(@"Patient.identifier.skip(999).all(use = 'official')");   // {}.All() aways returns true
            fixture.IsTrue(@"Patient.identifier.skip(1).all({}).empty()");   // empty results still count as "empty"
        }

        [Fact]
        public void TestAny()
        {
            fixture.IsTrue(@"Patient.identifier.any(use = 'official')");
            fixture.IsTrue(@"Patient.identifier.skip(999).any(use = 'official') = false");   // {}.Any() aways returns true
            fixture.IsTrue(@"Patient.contained.skip(1).group.group.any(concept.code = 'COMORBIDITY')");       // really need to filter on Questionnare (as('Questionnaire'))
        }

        [Fact]
        public void TestRepeat()
        {
            fixture.IsTrue(@"Patient.contained.skip(1).repeat(group).count() = 4");       // really need to filter on Questionnare (as('Questionnaire'))
            fixture.IsTrue(@"Patient.contained.skip(1).repeat(group|question).count() = 11");       // really need to filter on Questionnare (as('Questionnaire'))
            fixture.IsTrue(@"Patient.contained.skip(1).repeat(group | question).count() = 11");       // really need to filter on Questionnare (as('Questionnaire'))
            fixture.IsTrue(@"Patient.contained.skip(1).repeat(group|question).count() = 11");       // really need to filter on Questionnare (as('Questionnaire'))
            fixture.IsTrue(@"Patient.contained.skip(1).repeat(group).select(concept.code) contains 'COMORBIDITY'");       // really need to filter on Questionnare (as('Questionnaire'))
            fixture.IsTrue(@"Patient.contained.skip(1).repeat(group).any(concept.code = 'COMORBIDITY')");       // really need to filter on Questionnare (as('Questionnaire'))
            fixture.IsTrue(@"Patient.contained.skip(1).repeat(group).any(concept.code = 'CARDIAL') = false");       // really need to filter on Questionnare (as('Questionnaire'))
            fixture.IsTrue(@"Patient.contained.skip(1).repeat(group|question).any(concept.code = 'CARDIAL')");       // really need to filter on Questionnare (as('Questionnaire'))

            fixture.IsTrue(@"Questionnaire.descendants().linkId.distinct()", fixture.Questionnaire);
            fixture.IsTrue(@"Questionnaire.repeat(group | question).concept.count()", fixture.Questionnaire);
        }


        [Fact]
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

        [Fact]
        public void TestExpressionTodayFunction()
        {
            // Check that date comes in
            Assert.Equal(PartialDateTime.Today(), fixture.TestInput.Scalar("today()"));

            // Check greater than
            fixture.IsTrue("today() < @" + PartialDateTime.FromDateTime(DateTime.Today.AddDays(1)));

            // Check less than
            fixture.IsTrue("today() > @" + PartialDateTime.FromDateTime(DateTime.Today.AddDays(-1)));

            // Check ==
            fixture.IsTrue("today() = @" + PartialDateTime.Today());

            fixture.IsTrue("now() > @" + PartialDateTime.Today());
            fixture.IsTrue("now() >= @" + PartialDateTime.Now());
        }

        [Fact]
        public void TestSubstring()
        {
            fixture.IsTrue("Patient.name.family");

            fixture.IsTrue("Patient.name.family.substring(0,6) = 'Donald'");
            fixture.IsTrue("Patient.name.family.substring(2,6) = 'nald'");
            fixture.IsTrue("Patient.name.family.substring(2,4) = 'nald'");

            fixture.IsTrue("Patient.name.family.substring(2,length()-3) = 'nal'");

            fixture.IsTrue("Patient.name.family.substring(-1,8).empty()");
            fixture.IsTrue("Patient.name.family.substring(999,1).empty()");
            fixture.IsTrue("''.substring(0,1).empty()");
            fixture.IsTrue("{}.substring(0,10).empty()");
            fixture.IsTrue("{}.substring(0,10).empty()");

            try
            {
                // TODO: Improve exception on this one
                fixture.IsTrue("Patient.identifier.use.substring(0,10)");
                // todo: mh: Assert.Fail();
                throw new Exception();
            }
            catch (InvalidOperationException)
            {
            }
        }

        [Fact]
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
            fixture.IsTrue("Patient.contained.name[0].family.indexOf('qq').empty()");

            fixture.IsTrue("Patient.contained.name[0].family.contains('ywo')");
            fixture.IsTrue("Patient.contained.name[0].family.contains('ywox')=false");
            fixture.IsTrue("Patient.contained.name[0].family.contains('')");

            fixture.IsTrue(@"'11/30/1972'.replaceMatches('\\b(?<month>\\d{1,2})/(?<day>\\d{1,2})/(?<year>\\d{2,4})\\b','${day}-${month}-${year}') = '30-11-1972'");

            fixture.IsTrue(@"'abc'.replace('a', 'q') = 'qbc'");
            fixture.IsTrue(@"'abc'.replace('a', 'qq') = 'qqbc'");
            fixture.IsTrue(@"'abc'.replace('q', 'x') = 'abc'");
            fixture.IsTrue(@"'abc'.replace('ab', '') = 'c'");
            fixture.IsTrue(@"'abc'.replace('', 'xh') = 'xhaxhbxhc'");

            fixture.IsTrue("Patient.contained.name[0].family.length() = " + "Everywoman".Length);
            fixture.IsTrue("''.length() = 0");
            fixture.IsTrue("{}.length().empty()");
        }


        //[Fact]
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

        //    var result = PathExpression.Select("name.given | name.family", new[] { patNav });
        //    Assert.Equal(5, result.Count());
        //}

        [Fact]
        public void CompilationIsCached()
        {
            Stopwatch sw = new Stopwatch();
            string expression = "";

            sw.Start();

            var random = new Random();

            // something that has not been compiled before
            for (int i = 0; i < 1000; i++)
            {
                var next = random.Next(0, 10000);
                expression = $"Patient.name[{next}]";
                fixture.TestInput.Select(expression);
            }
            sw.Stop();

            var uncached = sw.ElapsedMilliseconds;

            sw.Restart();

            for (int i = 0; i < 1000; i++)
            {
                fixture.TestInput.Select(expression);
            }

            sw.Stop();

            var cached = sw.ElapsedMilliseconds;
            output.WriteLine("Uncached: {0}, cached: {1}".FormatWith(uncached, cached));

            Assert.True(cached < uncached / 2);

        }

    }
}
