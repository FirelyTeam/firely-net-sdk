/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
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

namespace Hl7.FhirPath.R4.Tests
{
    [TestClass]
    public class FhirPathTests
    {
        public TestContext TestContext { get; set; }

        private void testPredicate(ITypedElement resource, String expression, IEnumerable<XElement> expected)
        {
            var expectedTypedValue = boolean.Parse(expected.First().Value);

            var result = resource.Predicate(expression);
            Assert.AreEqual(expectedTypedValue, result);
        }

        private void test(ITypedElement resource, String expression, IEnumerable<XElement> expected)
        {
            IEnumerable<ITypedElement> actual = resource.Select(expression);
            Assert.AreEqual(expected.Count(), actual.Count());

            expected.Zip(actual, compare).Count();
        }

        private static bool compare(XElement expected, ITypedElement actual)
        {
            var type = expected.Attribute("type").Value;
            var tp = (ITypedElement)actual;
            Assert.AreEqual(type, tp.InstanceType);

            if (expected.IsEmpty) return true;      // we are not checking the value

            Assert.AreEqual(expected.Value, actual.ToStringRepresentation());

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
            True,
            Syntax,
            Semantics
        }

        private void testInvalid(ITypedElement resource, ErrorType type, String expression)
        {
            try
            {
                resource.Select(expression);
                Assert.IsTrue(false, "Should have been invalid");
            }
            catch (FormatException)
            {
                if (!correctCategory(ErrorType.Syntax)) Assert.Fail("Invalid should have been of type syntax");
            }
            catch (InvalidCastException)
            {
                if (!correctCategory(ErrorType.Semantics)) Assert.Fail("Invalid should have been of type semantics");
            }
            catch (InvalidOperationException)
            {
                if (!correctCategory(ErrorType.Semantics)) Assert.Fail("Invalid should have been of type semantics2");
            }

            bool correctCategory(ErrorType t) =>
                type == t || type == ErrorType.True;
        }

        Dictionary<string, Model.DomainResource> _cache = new Dictionary<string, Model.DomainResource>();

        int numFailed = 0;
        int totalTests = 0;

        [TestMethod]
        public void TestPublishedTests()
        {
            var path = Path.Combine(TestData.GetTestDataBasePath(), "fhirpath");
            var files = Directory.EnumerateFiles(path, "*.xml", SearchOption.TopDirectoryOnly);

            foreach (var file in files)
            {
                TestContext.WriteLine($"==== Running tests from file '{file}' ====");
                runTests(file);
                TestContext.WriteLine(Environment.NewLine);
            }

            TestContext.WriteLine($"Ran {totalTests} tests in total, {totalTests - numFailed} succeeded, {numFailed} failed.");

            // TODO 20190709: we know that 175 tests are still failing. In the next release we make sure that these test will succeed again.
            // EK 20190722: Improving the support for normative FP, we've now gone down to 95
            Assert.AreEqual(175,numFailed, $"There were {numFailed} unsuccessful tests (out of a total of {totalTests})");
        }

        private void runTests(string pathToTest)
        {
            // Read the test file, then execute each of them
            var doc = XDocument.Load(pathToTest);

            foreach (var item in doc.Descendants("test"))
            {
                string groupName = item.Parent.Attribute("name").Value;
                string name = item.Attribute("name")?.Value ?? "(no name)";
                string inputfile = item.Attribute("inputfile").Value;
                var mode = item.Attribute("mode");
                string expression = item.Element("expression").Value;

                if (mode != null && mode.Value == "strict") continue; // don't do 'strict' tests yet

                // Now perform this unit test
                Model.DomainResource resource = null;
                string basepath = Path.Combine(TestData.GetTestDataBasePath(), "fhirpath", "input");

                if (!_cache.ContainsKey(inputfile))
                {
                    _cache.Add(inputfile, (Model.DomainResource)(new FhirXmlParser().Parse<Model.DomainResource>(
                        File.ReadAllText(Path.Combine(basepath, inputfile)))));
                }
                resource = _cache[inputfile];

                try
                {
                    totalTests += 1;
                    runTestItem(item, resource.ToTypedElement());
                }


                catch (Exception e)
                {
                    TestContext.WriteLine($"FAIL: {groupName} - {name}: {expression}");
                    TestContext.WriteLine($"   ({e.GetType().Name}) {e.Message}");
                    numFailed += 1;
                }
            }
        }

        private void runTestItem(XElement testLine, ITypedElement resource)
        {
            var expression = testLine.Element("expression");
            var output = testLine.Elements("output");

            string invalid = expression.TryGetAttribute("invalid", out bool hasInvalid);

            if (hasInvalid)
            {
                ErrorType errorType;

                if (invalid == "syntax")
                    errorType = ErrorType.Syntax;
                else if (invalid == "true")
                    errorType = ErrorType.True;
                else if (invalid == "semantic")
                    errorType = ErrorType.Semantics;
                else
                    throw new ArgumentException("unknown error type");

                testInvalid(resource, errorType, expression.Value);
            }
            else
            {
                // Still need to check the types (and values)

                var predicate = testLine.TryGetAttribute("predicate", out bool hasPredicate);

                if (hasPredicate && Boolean.Parse(predicate))
                {
                    testPredicate(resource, expression.Value, output);
                }
                else
                {
                    test(resource, expression.Value, output);
                }
            }
        }

        [TestMethod]
        public void testTyping()
        {
            Model.ElementDefinition ed = new Model.ElementDefinition();
            ed.Binding = new Model.ElementDefinition.ElementDefinitionBindingComponent();
            ed.Binding.setValueSet("http://test.org");
            testBoolean(null, ed.Binding.getValueSet(), "ElementDefinition.binding.valueSet", "startsWith('http:') or startsWith('https') or startsWith('urn:')", true);
        }

        [TestMethod]
        public void testDecimalRA()
        {
            Model.RiskAssessment r = new Model.RiskAssessment();
            Model.SimpleQuantity sq = new Model.SimpleQuantity();
            sq.setValue(0.2);
            sq.setUnit("%");
            sq.setCode("%");
            sq.setSystem("http://unitsofmeasure.org");
            Model.SimpleQuantity sq1 = new Model.SimpleQuantity();
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

        /*  [Fact, Trait("Area", "FhirPathFromSpec")]
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

        [TestMethod]
        public void testExtensionDefinitions()
        {
            // obsolete:
            // Bundle b = (Bundle)FhirParser.ParseResourceFromXml(File.ReadAllText(Path.Combine(TestData", "extension-definitions.xml")));
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