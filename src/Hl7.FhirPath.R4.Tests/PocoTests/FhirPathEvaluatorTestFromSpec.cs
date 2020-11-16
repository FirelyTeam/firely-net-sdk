/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Serialization;
using Hl7.FhirPath.Functions;
using Hl7.FhirPath.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using boolean = System.Boolean;
using DecimalType = Hl7.Fhir.Model.FhirDecimal; // System.Decimal;
using Model = Hl7.Fhir.Model;
using P = Hl7.Fhir.ElementModel.Types; 

namespace Hl7.FhirPath.R4.Tests
{
    [TestClass]
    public class FhirPathTests
    {
        private void test(Model.Resource resource, String expression, IEnumerable<XElement> expected, bool asPredicate)
        {
            //var tpXml = new FhirXmlSerializer().SerializeToString(resource);
            var npoco = resource.ToTypedElement();
            //       FhirPathEvaluatorTest.Render(npoco);
            IEnumerable<ITypedElement> actual;

            if (!asPredicate)
                actual = npoco.Select(expression);
            else
                actual = ElementNode.CreateList(npoco.Predicate(expression));

            if (!expected.Any() && actual.Any())
                Assert.Fail("Expected an empty result");
            if (expected.Any() && !actual.Any())
                Assert.Fail("Expected a non-empty result");

            Assert.AreEqual(expected.Count(), actual.Count(), "The number of items in the result differ from the expected number");
            expected.Zip(actual, compare).Count();
        }

        private static bool compare(XElement expected, ITypedElement actual)
        {
            var type = expected.Attribute("type").Value.ToLower();
            var tp = actual.InstanceType.ToLower();

            if (type.Contains(".")) type = type.Substring(type.IndexOf(".") + 1);
            if (tp.Contains(".")) tp = tp.Substring(tp.IndexOf(".") + 1);
            Assert.AreEqual(type, tp);

            if (expected.IsEmpty) return true;      // we are not checking the value

            Assert.AreEqual(expected.Value, P.Any.Convert(actual.Value).ToString());

            return true;
        }

        // @SuppressWarnings("deprecation")
        private void testBoolean(Model.Resource resource, Model.Base focus, String focusType, String expression, boolean value)
        {
            var input = focus.ToTypedElement();
            var container = resource?.ToTypedElement();

            Assert.IsTrue(input.IsBoolean(expression, value, new EvaluationContext(container)));
        }

        enum ErrorType
        {
            Syntax,
            Semantics
        }

        private void testInvalid(Model.Resource resource, ErrorType type, String expression)
        {
            try
            {
                var resourceNav = resource.ToTypedElement();
                resourceNav.Select(expression);
                Assert.IsTrue(false, "Should have been invalid");
            }
            catch (FormatException)
            {
                if (type != ErrorType.Syntax) Assert.IsTrue(false, "Invalid should have been of type syntax");
            }
            catch (InvalidCastException)
            {
                if (type != ErrorType.Semantics) Assert.IsTrue(false, "Invalid should have been of type semantics");
            }
            catch (InvalidOperationException)
            {
                if (type != ErrorType.Semantics) Assert.IsTrue(false, "Invalid should have been of type semantics2");
            }
        }

        Dictionary<string, Model.DomainResource> _cache = new Dictionary<string, Model.DomainResource>();

        int numFailed = 0;
        int totalTests = 0;

        [TestMethod, TestCategory("FhirPathFromSpec")]
        public void TestPublishedTests()
        {
            var path = Path.Combine(TestData.GetTestDataBasePath(), "fhirpath");
            var files = Directory.EnumerateFiles(path, "*.xml", SearchOption.TopDirectoryOnly);

            var ignoreTestcases = new string[]
            {
                // this one is incorrect - has been reported and agreed on.
                "testIntegerBooleanNotTrue",

                // this works with quantities with different UCUM units, which we don't support
                "testQuantity1", "testQuantity2", "testQuantity3", "testQuantity4", "testQuantity5", "testQuantity6",
                "testQuantity7", "testQuantity8", "testQuantity9", "testQuantity10", "testQuantity11",
                "testAggregate1", "testAggregate2", "testAggregate3", "testAggregate4",
                "testEquality7", "testNEquality24", "testNotEquivalent22",

                // this tests the reflection capabilities, that we do not have yet
                "testType1", "testType2", "testType3", "testType4", "testType9", "testType10", "testType15", "testType16",
                "testType20", "testType21", "testType23",
                "testConformsTo",

                // these date tests are incorrect - reported on Zulip
                "testDateNotEqualTimezoneOffsetBefore", "testDateNotEqualTimezoneOffsetAfter", "testDateNotEqualUTC",

                // we don't support these FhirPath extensions for validation yet
                 "testConformsTo1", "testConformsTo2",

                // rounding pi to 3 decimals will not become 2
                "testRound2",

                // Still under discussion on Zulip (but this will probably align with outs in the end)
                "testTrueAndFoo"
            };

            foreach (var file in files)
            {
                Console.WriteLine($"==== Running tests from file '{file}' ====");
                runTests(file, ignoreTestcases);
                Console.WriteLine(Environment.NewLine);
            }

            Console.WriteLine($"Ran {totalTests} tests in total, {totalTests - numFailed} succeeded, {numFailed} failed.");

            Assert.IsTrue(0 == numFailed, $"There were {numFailed} unsuccessful tests (out of a total of {totalTests})");
        }

        private void runTests(string pathToTest, IEnumerable<string> ignoreTestcases)
        {
            // Read the test file, then execute each of them
            var doc = XDocument.Load(pathToTest);

            foreach (var item in doc.Descendants("test"))
            {
                string groupName = item.Parent.Attribute("name")?.Value ?? "(no group name)";
                string name = item.Attribute("name")?.Value ?? "(no name)";

                if (ignoreTestcases.Contains(name)) continue; // skip the ignore testcases

                string inputfile = item.Attribute("inputfile").Value;
                var mode = item.Attribute("mode");
                var expressionNode = item.Element("expression");
                string expression = expressionNode.Value;
                bool invalid = expressionNode.Attribute("invalid")?.Value == "true";

                if (mode?.Value == "strict" || invalid) continue; // don't do 'strict' or invlaid tests yet
                string basepath = Path.Combine(TestData.GetTestDataBasePath(), @"fhirpath\input");

                if (!_cache.ContainsKey(inputfile))
                {
                    _cache.Add(inputfile, new FhirXmlParser().Parse<Model.DomainResource>(
                        File.ReadAllText(Path.Combine(basepath, inputfile))));
                }
                // Now perform this unit test
                Model.DomainResource resource = _cache[inputfile];

                try
                {
                    totalTests += 1;
                    runTestItem(item, resource);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"FAIL: {groupName} - {name}: {expression}");
                    Console.WriteLine($"   ({e.GetType().Name}) {e.Message}");
                    numFailed += 1;
                }
            }
        }

        private void runTestItem(XElement testLine, Model.DomainResource resource)
        {
            var expression = testLine.Element("expression");
            var output = testLine.Elements("output");
            var isPredicate = testLine.Attribute("predicate")?.Value == "true";

            string invalid = expression.TryGetAttribute("invalid", out bool hasInvalid);

            if (hasInvalid)
            {
                ErrorType errorType;

                if (invalid == "syntax" || invalid == "true")
                    errorType = ErrorType.Syntax;
                else if (invalid == "semantic")
                    errorType = ErrorType.Semantics;
                else
                    throw new ArgumentException("unknown error type");

                testInvalid(resource, errorType, expression.Value);
            }
            else
            {
                // Still need to check the types (and values)
                test(resource, expression.Value, output, isPredicate);
            }
        }

        [TestMethod, TestCategory("FhirPathFromSpec")]
        public void testTyping()
        {
            Model.ElementDefinition ed = new Model.ElementDefinition();
            ed.Binding = new Model.ElementDefinition.ElementDefinitionBindingComponent();
            ed.Binding.setValueSet("http://test.org");
            testBoolean(null, ed.Binding.getValueSet(), "ElementDefinition.binding.valueSet", "startsWith('http:') or startsWith('https') or startsWith('urn:')", true);
        }

        [TestMethod, TestCategory("FhirPathFromSpec")]
        public void testDecimalRA()
        {
            var r = new Model.RiskAssessment();
            var sq = new Model.Quantity();
            sq.setValue(0.2);
            sq.setUnit("%");
            sq.setCode("%");
            sq.setSystem("http://unitsofmeasure.org");
            var sq1 = new Model.Quantity();
            sq1.setValue(0.4);
            sq1.setUnit("%");
            sq1.setCode("%");
            sq1.setSystem("http://unitsofmeasure.org");
            r.addPrediction().setProbability(new Model.Range().setLow(sq).setHigh(sq1));
            testBoolean(r, r.getPrediction()[0].getProbability(), "RiskAssessment.prediction.probabilityRange",
                "(low.empty() or ((low.code = '%') and (low.system = %ucum))) and (high.empty() or ((high.code = '%') and (high.system = %ucum)))", true);
            testBoolean(r, r.getPrediction()[0], "RiskAssessment.prediction", "probability is decimal implies probability.as(decimal) <= 100", true);
            r.getPrediction()[0].setProbability(new DecimalType(80));
            testBoolean(r, r.getPrediction()[0], "RiskAssessment.prediction", "probability.as(decimal) <= 100", true);
        }

        /*  [TestMethod, TestCategory("Area", "FhirPathFromSpec")]
          public void testQuestionnaire()  {
            Questionnaire q = (Questionnaire)FhirParser.ParseResourceFromJson(File.ReadAllText("C:/work/org.hl7.fhir/build - DSTU2.0/publish/questionnaire-example-gcs.json"));
            for (QuestionnaireItemComponent qi : q.getItem()) {
              testQItem(qi);
            }
          }

          private void testQItem(QuestionnaireItemComponent qi)  {
            testBoolean(null, qi, "Questionnaire.item", "(type = 'choice' or type = 'open-choice') or (options.empty() and option.empty())", true);
          }
        */

        [TestMethod, TestCategory("FhirPathFromSpec")]
        public void testExtensionDefinitions()
        {
            // obsolete:
            // Bundle b = (Bundle)FhirParser.ParseResourceFromXml(File.ReadAllText("TestData\\extension-definitions.xml"));
            var parser = new FhirXmlParser();
            Model.Bundle b = parser.Parse<Model.Bundle>(TestData.ReadTextFile("extension-definitions.xml"));

            foreach (Model.Bundle.EntryComponent be in b.Entry)
            {
                testStructureDefinition((Model.StructureDefinition)be.Resource);
            }
        }

        private void testStructureDefinition(Model.StructureDefinition sd)
        {
            testBoolean(sd, sd, "StructureDefinition", "snapshot.element.tail().all(path.startsWith(%resource.snapshot.element.first().path&'.')) and differential.element.tail().all(path.startsWith(%resource.differential.element.first().path&'.'))", true);
        }

    }
}