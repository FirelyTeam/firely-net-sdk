/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.FhirPath.Functions;
using Hl7.FhirPath.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Boolean = System.Boolean;
using PocoNode = Hl7.Fhir.Model.PocoNode;
using DecimalType = Hl7.Fhir.Model.FhirDecimal; // System.Decimal;
using Model = Hl7.Fhir.Model;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.FhirPath.R4.Tests
{
    [TestClass]
    public class FhirPathTests
    {
        private static readonly FhirPathCompiler _compiler = createCompiler();

        private static FhirPathCompiler createCompiler()
        {
            ElementNavFhirExtensions.PrepareFhirSymbolTableFunctions();
            return new FhirPathCompiler();
        }

        private static List<PocoNode> evaluate(PocoNode input, string expression)
        {
            var compiled = _compiler.Compile(expression);
            // Pass the node itself (not a list wrapping it), so the evaluator can infer %resource/%rootResource from it.
            return compiled(input is null ? [] : input, new FhirEvaluationContext()).ToList();
        }

        private static void test(IEnumerable<PocoNode> focus, string expression, IReadOnlyList<XElement> expected, bool asPredicate, bool ordered)
        {
            List<PocoNode> actual = focus.Any()
                ? focus.SelectMany(f => evaluate(f, expression)).ToList()
                : evaluate(null, expression);

            if (asPredicate)
            {
                // As in the reference implementation, an empty result counts as false.
                var b = actual.BooleanEval();
                actual = [ElementNode.ForPrimitive(b is not null && b.Value).ToPocoNode()];
            }

            if (!expected.Any() && actual.Any())
                Assert.Fail($"Expected an empty result, but got {actual.Count} item(s): {string.Join(", ", actual.Select(describe))}");
            if (expected.Any() && !actual.Any())
                Assert.Fail("Expected a non-empty result");

            Assert.AreEqual(expected.Count, actual.Count, $"The number of items in the result differ from the expected number. Actual: {string.Join(", ", actual.Select(describe))}");

            if (ordered)
            {
                for (int i = 0; i < expected.Count; i++)
                    compare(expected[i], actual[i]);
            }
            else
            {
                // Unordered: every expected item must match a distinct actual item.
                var remaining = actual.ToList();
                foreach (var exp in expected)
                {
                    var match = remaining.FirstOrDefault(a => matches(exp, a));
                    if (match is null) Assert.Fail($"No (unordered) match for expected {exp}. Actual: {string.Join(", ", actual.Select(describe))}");
                    remaining.Remove(match);
                }
            }
        }

        private static string describe(PocoNode n)
        {
            var te = (ITypedElement)n;
            return te.Value is null ? $"({te.InstanceType})" : $"{P.Any.Convert(te.Value)} ({te.InstanceType})";
        }

        private static bool matches(XElement expected, PocoNode actual)
        {
            try { compare(expected, actual); return true; }
            catch (AssertFailedException) { return false; }
        }

        private static string normalizeType(string type)
        {
            type = type.ToLowerInvariant();
            return type.Contains('.') ? type.Substring(type.IndexOf('.') + 1) : type;
        }

        private static void compare(XElement expected, PocoNode actualNode)
        {
            var actual = (ITypedElement)actualNode;
            var expectedType = expected.Attribute("type")?.Value;

            if (expectedType is not null)
                Assert.AreEqual(normalizeType(expectedType), normalizeType(actual.InstanceType), "Type of result differs");

            if (expected.IsEmpty) return;      // we are not checking the value

            Assert.AreEqual(normalizeExpectedValue(expected.Value, expectedType), actual.Value is null ? null : P.Any.Convert(actual.Value).ToString(), "Value of result differs");
        }

        /// <summary>
        /// The suite writes date, dateTime and time outputs as FHIRPath literals (e.g. <c>@2012-04-15</c>, <c>@T12:00:00</c>),
        /// whereas the engine renders them without the literal prefix (<c>@</c>, or <c>@T</c> for times).
        /// </summary>
        private static string normalizeExpectedValue(string value, string expectedType)
        {
            // Untyped outputs (e.g. from the boundary functions) use the same literal notation.
            if (expectedType is null or "date" or "dateTime" or "time" && value.StartsWith("@"))
                return value.StartsWith("@T") ? value.Substring(2) : value.Substring(1);

            return value;
        }

        private static void testInvalid(IEnumerable<PocoNode> focus, string kind, string expression)
        {
            try
            {
                if (focus.Any())
                    focus.SelectMany(f => evaluate(f, expression)).ToList();
                else
                    evaluate(null, expression);
            }
            catch (FormatException) when (kind is "syntax")
            {
                return;
            }
            catch (Exception e) when (kind is "syntax")
            {
                Assert.Fail($"Expected a syntax error, but got {e.GetType().Name}: {e.Message}");
            }
            catch (ArgumentException e) when (e.Message.StartsWith("Unknown symbol"))
            {
                Assert.Fail($"Expected a(n) {kind} error, but the engine does not implement a function used: {e.Message}");
            }
            catch (FormatException e)
            {
                Assert.Fail($"Expected a(n) {kind} error, but got a syntax error: {e.Message}");
            }
            catch (Exception)
            {
                // semantic, execution and (legacy) "true" errors: any non-syntax error is accepted, since
                // this engine has no separate static (semantic) checking phase.
                return;
            }

            Assert.Fail($"Expected a(n) {kind} error, but the expression evaluated successfully");
        }

        private readonly Dictionary<string, PocoNode> _cache = new();
        private int numFailed = 0;
        private int numSkipped = 0;
        private int numKnownFailures = 0;
        private int totalTests = 0;
        private readonly List<string> _failures = new();

        /// <summary>
        /// Modes (on a test, or inherited from its group) that this engine does not run. See the
        /// ModeType documentation in the test schema (testSchema.xsd in the fhir-test-cases repository).
        /// </summary>
        private static readonly string[] SKIPPED_MODES =
        [
            "custom",   // engine-specific extension functions (HL7 Java reference implementation only)
            "tx",       // requires a terminology server (%terminologies)
            "cda",      // CDA documents
        ];

        /// <summary>
        /// Tests in the published suite that this engine currently does not pass, grouped by root cause.
        /// They are still run: a known failure that passes fails the test run, so the fix for a root cause
        /// must remove its entries from this list.
        /// </summary>
        private static readonly string[] IGNORE_TESTCASES =
        [
            // Arithmetic, comparison, equality and unary minus operators return empty when the input focus is empty (tests
            // without an input file). Fixing this can change the outcome of no-input tests in other groups, so re-run the
            // suite
            "testPrecedence1", "testPrecedence2", "testMatchesEmpty", "testMatchesEmpty2", "testMatchesEmpty3",
            "HighBoundaryDecimal6", "HighBoundaryDecimal14", "LowBoundaryDecimal13", "LowBoundaryNegDecimal1",
            // These boundary tests also negate a literal without an input file; the boundary itself is right
            "LowBoundaryNegDecimalDefault", "LowBoundaryNegDecimal2", "LowBoundaryNegDecimal4", "LowBoundaryDecimalCarry1",
            "LowBoundaryDecimalCarry2", "LowBoundaryDecimalCarry3", "HighBoundaryDecimal4", "HighBoundaryDecimal5",
            "HighBoundaryDecimal16",

            // No static (semantic) type checking: invalid paths and type-guard checks are not reported as semantic errors
            "testSimpleFail", "testSimpleWithWrongContext", "testPolymorphismB", "testPolymorphismAsB",
            "testDollarOrderNotAllowed", "testIif6", "testPolymorphicsB", "isEffectOnLogic3Base", "isGuardFnImpliesBad",
            "isGuardImpliesBad", "isGuardOrTypesImpliesBad", "isGuardOrFnTypesImpliesBad", "isGuardOrOtherImpliesBad",
            "isGuardOrOtherImpliesNoParensBad", "isGuardOrInnerImpliesBad", "isGuardAndBad", "isGuardOrBad", "isGuardXorBad",
            "isGuardNotImpliesBad", "isGuardIifBad", "isGuardSupertypeBad", "isGuardOtherExpressionBad",

            // (-0.0034).lowBoundary(1) is expected to be -0.0, a negative zero, which System.Decimal does not print
            "LowBoundaryDecimal15",

            // Decimals with an exponent lose their significance when parsed: 9.5e2 becomes 950 rather than a value with
            // two significant digits, so its boundaries come out as 949.5/950.5 instead of 945/955
            "LowBoundaryDecimalExponent1", "LowBoundaryDecimalExponent3", "LowBoundaryDecimalExponent4",
            "HighBoundaryDecimalExponent1", "HighBoundaryDecimalExponent2", "HighBoundaryDecimalExponent4",
            "HighBoundaryDecimalExponent5",

            // round() with a precision out of range (negative, or beyond what the engine supports) should give an empty
            // result, not an error
            "testHugeRoundPrecision", "testNegativeRoundPrecision",

            // lowBoundary()/highBoundary() on dateTime/time: millisecond precision and timezone boundaries (+14:00/-12:00)
            "LowBoundaryDateTimeMillisecond1", "LowBoundaryDateTimeMillisecond2", "LowBoundaryTimeMillisecond",
            "HighBoundaryDateTimeMillisecond1", "HighBoundaryDateTimeMillisecond3", "testPeriodInvariantNew",

            // Date/time arithmetic drops the input's millisecond precision in the result
            "testPlusDate3", "testPlusDate4", "testPlusDate5", "testPlusDate6", "testPlusDate7", "testPlusDate8",
            "testPlusDate18", "testPlusDate19", "testPlusDate20", "testPlusDate21", "testPlusDate22",

            // Date/time arithmetic fails on partial-precision dateTimes with a timezone (e.g. @2015-02-04T10:00Z + 1 hour)
            "testPlusDate23", "testPlusDate24", "testMinus7",

            // type() reflection function is not implemented
            "testType1", "testType1a", "testType2", "testType2a", "testType3", "testType4", "testType9", "testType10",
            "testType15", "testType16", "testType20", "testType21", "testType23",

            // is() does not follow the FHIR primitive type hierarchy (code is a string, url is a uri)
            "testFHIRPathIsFunction2", "testTypeA4",

            // Missing execution errors: as() on multiple items, unknown type names, iif() on a collection, allTrue() on non-
            // booleans
            "testFHIRPathAsFunction21", "testFHIRPathAsFunction23", "testFHIRPathAsFunction24", "testIif10", "from-zulip-2",

            // Operator precedence of 'is' relative to '|' and comparison operators
            "testPrecedence3", "testPrecedence4",

            // precision() function is not implemented
            "PrecisionDecimal", "PrecisionYear", "PrecisionDateTimeMilliseconds", "PrecisionTimeMinutes",
            "PrecisionTimeMilliseconds",

            // matchesFull() function is not implemented
            "testMatchesFullWithinUrl1", "testMatchesFullWithinUrl1a", "testMatchesFullWithinUrl2", "testMatchesFullWithinUrl3",
            "testMatchesFullWithinUrl4",

            // Regex semantics: matches() should use single-line mode ('.' matches newlines); replaceMatches() with an empty
            // pattern should return the input unchanged
            "testMatchesSingleLineMode1", "testReplaceMatches2",

            // Quantity comparison/equality does not convert between UCUM units (e.g. 4 'g' = 4000 'mg')
            "testQuantity1", "testQuantity2", "testQuantity3", "testQuantity4", "testNEquality24", "testLessThan31",
            "testLessThan33", "testLessOrEqual29", "testLessOrEqual30", "testGreaterThan30", "Comparable1",

            // Calendar duration quantities vs UCUM: 7 days = 1 week, 1 year > 1 'a', and toString() of calendar quantities
            "testQuantity5", "testQuantity6", "testQuantity7", "testQuantity8", "testGreaterThan28",
            "testQuantityLiteralWeekToString",

            // Quantity multiplication and division (2.0 'cm' * 2.0 'm', 4.0 'g' / 2.0 'm')
            "testQuantity9", "testQuantity10", "testQuantity11",

            // Comparing a quantity with a unit to one without a unit should give empty (=) / false (~)
            "testQuantityUnitVsNoUnitEqual", "testQuantityNoUnitVsUnitEqual", "testQuantityUnitVsNoUnitEquivalent",
            "testQuantityNoUnitVsUnitEquivalent",

            // Decimals with exponents outside the System.Decimal range (e.g. 1e999999999) are not supported
            "testLargeExponentQtyEqualOtherUnit", "testLargeExponentQtyEqualSameUnit", "testLargeExponentQtyEquivalentOtherUnit",
            "testLargeExponentQtyEquivalentSameUnit", "testLargeExponentQtyLessOtherUnit", "testLargeExponentQtyGreaterSameUnit",
            "testLargeExponentQtyToString", "testLargeExponentQtyTimes", "testLargeExponentQtyDivide",
            "testLargeExponentQtyLowBoundary", "testLargeExponentQtyEqualOtherUnitElement", "testLargeExponentQtyToStringElement",
            "testSmallExponentQtyToString", "testLargeExponentDecPlus", "testLargeExponentDecMinus", "testLargeExponentDecTimes",
            "testLargeExponentDecDivide", "testLargeExponentDecDiv", "testLargeExponentDecMod", "testLargeExponentDecRound",
            "testLargeExponentDecGreater", "testSmallExponentDecPlus", "testLargeExponentDateAdd",

            // Time literals with a timezone (@T14:34:28Z) should be an execution error, not a syntax error
            "testLiteralTimeUTC", "testLiteralTimeTimezoneOffset",

            // sort() with descending (-) keys on strings
            "testSort8", "testSort10",

            // repeat() with a literal argument should not repeat the same item
            "testRepeat5",

            // Negative indexer (Patient.name[-1]) should give an empty result
            "testIndexer3",

            // all() returns empty instead of false when a criterion is empty (e.g. comparing dateTimes of different
            // precision)
            "testPeriodInvariantOld",

            // conformsTo() is not implemented (needs profile resolution / validation)
            "testConformsTo1", "testConformsTo2", "testConformsTo3",
        ];

        [TestMethod, TestCategory("FhirPathFromSpec")]
        public void TestPublishedTests()
        {
            var path = Path.Combine(TestData.GetTestDataBasePath(), "fhirpath");
            var files = Directory.EnumerateFiles(path, "*.xml", SearchOption.TopDirectoryOnly);

            foreach (var file in files)
            {
                Console.WriteLine($"==== Running tests from file '{file}' ====");
                runTests(file, IGNORE_TESTCASES);
                Console.WriteLine(Environment.NewLine);
            }

            Console.WriteLine($"Ran {totalTests} tests in total, {totalTests - numFailed - numKnownFailures} succeeded, {numKnownFailures} failed as expected (IGNORE_TESTCASES), {numFailed} failed unexpectedly, {numSkipped} skipped.");

            Assert.AreEqual(0, numFailed, $"There were {numFailed} unsuccessful tests (out of a total of {totalTests}):{Environment.NewLine}{string.Join(Environment.NewLine, _failures)}");
        }

        private PocoNode loadInput(string inputfile)
        {
            if (!_cache.TryGetValue(inputfile, out var node))
            {
                var fullPath = Path.Combine(TestData.GetTestDataBasePath(), "fhirpath", "input", inputfile);
                var content = File.ReadAllText(fullPath);
                Model.Resource resource = inputfile.EndsWith(".json", StringComparison.OrdinalIgnoreCase)
                    ? FhirJsonDeserializer.OSTRICH.Deserialize<Model.Resource>(content)
                    : FhirXmlDeserializer.OSTRICH.Deserialize<Model.Resource>(content);
                node = resource.ToPocoNode();
                _cache.Add(inputfile, node);
            }

            return node;
        }

        private void runTests(string pathToTest, IEnumerable<string> ignoreTestcases)
        {
            // Read the test file, then execute each of them
            var doc = XDocument.Load(pathToTest);

            foreach (var item in doc.Descendants("test"))
            {
                string groupName = item.Parent.Attribute("name")?.Value ?? "(no group name)";
                string name = item.Attribute("name")?.Value ?? "(no name)";
                string testMode = item.Attribute("mode")?.Value;
                string groupMode = item.Parent.Attribute("mode")?.Value;

                // A custom group holds engine-specific functions, whatever mode its tests give themselves.
                if (SKIPPED_MODES.Contains(testMode) || SKIPPED_MODES.Contains(groupMode))
                {
                    numSkipped += 1;
                    continue;
                }

                string expression = item.Element("expression").Value;
                bool knownFailure = ignoreTestcases.Contains(name);

                try
                {
                    totalTests += 1;
                    runTestItem(item);

                    if (knownFailure)
                    {
                        // Known failures are still run, so a fix shows which entries it can remove.
                        var message = $"UNEXPECTED PASS: {groupName} - {name}: {expression}{Environment.NewLine}   Remove it from IGNORE_TESTCASES.";
                        Console.WriteLine(message);
                        _failures.Add(message);
                        numFailed += 1;
                    }
                }
                catch (Exception e)
                {
                    if (knownFailure)
                    {
                        numKnownFailures += 1;
                        continue;
                    }

                    var message = $"FAIL: {groupName} - {name}: {expression}{Environment.NewLine}   ({e.GetType().Name}) {e.Message}";
                    Console.WriteLine(message);
                    _failures.Add(message);
                    numFailed += 1;
                }
            }
        }

        private void runTestItem(XElement testLine)
        {
            var expression = testLine.Element("expression");
            var output = testLine.Elements("output").ToList();
            var isPredicate = testLine.Attribute("predicate")?.Value == "true";
            var isOrdered = testLine.Attribute("ordered")?.Value != "false";
            var inputfile = testLine.Attribute("inputfile")?.Value;
            var context = testLine.Attribute("context")?.Value;

            var input = inputfile is null ? null : loadInput(inputfile);

            // The 'context' attribute gives an expression that selects the focus for the test expression from the input.
            IEnumerable<PocoNode> focus = input is null ? [] : context is null ? [input] : evaluate(input, context);
            if (context is not null && !focus.Any())
                throw new InvalidOperationException($"The context expression '{context}' did not select anything from '{inputfile}'");

            var invalid = expression.Attribute("invalid")?.Value;

            if (invalid is not null && invalid != "false")
            {
                if (invalid is not ("syntax" or "semantic" or "execution" or "true"))
                    throw new ArgumentException($"unknown error type '{invalid}'");

                testInvalid(focus, invalid, expression.Value);
            }
            else
            {
                // Still need to check the types (and values)
                test(focus, expression.Value, output, isPredicate, isOrdered);
            }
        }

        private void testBoolean(Model.Resource resource, Model.Base focus, String focusType, String expression, Boolean value)
        {
            var input = focus.ToTypedElement();
            Assert.IsTrue(input.IsBoolean(expression, value, new EvaluationContext()));
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
            var parser = new FhirXmlDeserializer();
            Model.Bundle b = parser.Deserialize<Model.Bundle>(TestData.ReadTextFile("extension-definitions.xml"));

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