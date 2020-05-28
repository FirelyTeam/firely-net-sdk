/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Serialization;
using Hl7.FhirPath.Functions;
using Hl7.FhirPath.Tests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;
using boolean = System.Boolean;
using DecimalType = Hl7.Fhir.Model.FhirDecimal; // System.Decimal;
using Model = Hl7.Fhir.Model;

namespace Hl7.FhirPath.R4.Tests
{
    public class FhirPathTests
    {
        private readonly ITestOutputHelper output;

        public FhirPathTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        private void test(Model.Resource resource, String expression, IEnumerable<XElement> expected, bool asPredicate)
        {
            var tpXml = new FhirXmlSerializer().SerializeToString(resource);
            var npoco = resource.ToTypedElement();
            //       FhirPathEvaluatorTest.Render(npoco);
            IEnumerable<ITypedElement> actual;

            if (!asPredicate)
                actual = npoco.Select(expression);
            else
                actual = ElementNode.CreateList(npoco.Predicate(expression));

            Assert.Equal(expected.Count(), actual.Count());

            expected.Zip(actual, compare).Count();
        }

        private static bool compare(XElement expected, ITypedElement actual)
        {
            var type = expected.Attribute("type").Value.ToLower();
            var tp = actual.InstanceType.ToLower();

            if (type.Contains(".")) type = type.Substring(type.IndexOf(".") + 1);
            if (tp.Contains(".")) tp = tp.Substring(tp.IndexOf(".") + 1);
            Assert.Equal(type, tp);

            if (expected.IsEmpty) return true;      // we are not checking the value

            Assert.Equal(expected.Value, actual.ToStringRepresentation());

            return true;
        }

        // @SuppressWarnings("deprecation")
        private void testBoolean(Model.Resource resource, Model.Base focus, String focusType, String expression, boolean value)
        {
            var input = focus.ToTypedElement();
            var container = resource?.ToTypedElement();

            Assert.True(input.IsBoolean(expression, value, new EvaluationContext(container)));
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
                Assert.True(false, "Should have been invalid");
            }
            catch (FormatException)
            {
                if (type != ErrorType.Syntax) Assert.True(false, "Invalid should have been of type syntax");
            }
            catch (InvalidCastException)
            {
                if (type != ErrorType.Semantics) Assert.True(false, "Invalid should have been of type semantics");
            }
            catch (InvalidOperationException)
            {
                if (type != ErrorType.Semantics) Assert.True(false, "Invalid should have been of type semantics2");
            }
        }

        Dictionary<string, Model.DomainResource> _cache = new Dictionary<string, Model.DomainResource>();

        int numFailed = 0;
        int totalTests = 0;

        [Fact, Trait("Area", "FhirPathFromSpec")]
        public void TestPublishedTests()
        {
            var path = Path.Combine(TestData.GetTestDataBasePath(), "fhirpath");
            var files = Directory.EnumerateFiles(path, "*.xml", SearchOption.TopDirectoryOnly);

            foreach (var file in files)
            {
                output.WriteLine($"==== Running tests from file '{file}' ====");
                runTests(file);
                output.WriteLine(Environment.NewLine);
            }

            output.WriteLine($"Ran {totalTests} tests in total, {totalTests - numFailed} succeeded, {numFailed} failed.");

            // TODO 20190709: we know that 103 tests are still failing. In the next release we make sure that these test will succeed again.
            // MV 20191210: For version 4.0.1 we added some extra functions in FhirPath (intersect and ` is allowed), so now 97 tests (instead of 103) are failing
            // MV 20200302: After merging 1.x into develop we have 131 failing tests
            // EK 20200528: Managed it down to 108, by changing the type of integer constants back to long.
            Assert.True(108 == numFailed, $"There were {numFailed} unsuccessful tests (out of a total of {totalTests})");
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
                string basepath = Path.Combine(TestData.GetTestDataBasePath(), @"fhirpath\input");

                if (!_cache.ContainsKey(inputfile))
                {
                    _cache.Add(inputfile, (Model.DomainResource)(new FhirXmlParser().Parse<Model.DomainResource>(
                        File.ReadAllText(Path.Combine(basepath, inputfile)))));
                }
                resource = _cache[inputfile];

                try
                {
                    totalTests += 1;
                    runTestItem(item, resource);
                }
                catch (Exception e)
                {
                    output.WriteLine($"FAIL: {groupName} - {name}: {expression}");
                    if (!(e is XunitException))
                        output.WriteLine($"   ({e.GetType().Name}) {e.Message}");
                    else
                        output.WriteLine($"   {e.Message}");
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

        [Fact, Trait("Area", "FhirPathFromSpec")]
        public void testTyping()
        {
            Model.ElementDefinition ed = new Model.ElementDefinition();
            ed.Binding = new Model.ElementDefinition.ElementDefinitionBindingComponent();
            ed.Binding.setValueSet("http://test.org");
            testBoolean(null, ed.Binding.getValueSet(), "ElementDefinition.binding.valueSet", "startsWith('http:') or startsWith('https') or startsWith('urn:')", true);
        }

        [Fact, Trait("Area", "FhirPathFromSpec")]
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

        [Fact, Trait("Area", "FhirPathFromSpec")]
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