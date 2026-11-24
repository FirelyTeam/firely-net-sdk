/* 
 * Copyright (c) 2021, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.FhirPath;
using Hl7.Fhir.Introspection;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.FhirPath;
using Hl7.FhirPath.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.Support.Tests
{
    [TestClass]
    public class FhirPathTests
    {
        private static FhirPathCompiler _compiler;

        [ClassInitialize]
        public static void ClassSetup(TestContext context)
        {
            var symbolTable = new SymbolTable();
            symbolTable.AddStandardFP();
            symbolTable.AddFhirExtensions();
            _compiler = new FhirPathCompiler(symbolTable);
        }

        [TestMethod]
        public void ResolveOnEmptyTest()
        {
            // resolve should handle an empty collection as input
            var evaluator = _compiler.Compile("{}.resolve()");
            var result = evaluator(null, new FhirEvaluationContext());

            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        [DataRow("<div>Not empty</div>", false, "no XHTML namespace")]
        [DataRow("<div xmlns=\"http://www.w3.org/1999/xhtml\"> </div>", false, "containing only whitespace")]
        [DataRow("<div xmlns=\"http://www.w3.org/1999/xhtml\">\t\n</div>", false, "containing only whitespace")]
        [DataRow("<div xmlns=\"http://www.w3.org/1999/xhtml\"></div>", false, "empty div element")]
        [DataRow("<div xmlns=\"http://www.w3.org/1999/xhtml\">Not empty</div>", true, "non empty div element with XHTML namespace")]
        [DataRow("<div xmlns=\"http://www.w3.org/1999/xhtml\"><img src=\"fhir.gif\" alt=\"Fhir gif\"></img></div>", true, "containing an image element")]
        [DataRow("<div xmlns=\"http://www.w3.org/1999/xhtml\"><p><b><i> </i></b></p></div>", false, "no text")]
        [DataRow("<div xmlns=\"http://www.w3.org/1999/xhtml\"><p><b><i> </i></b><img src=\"fhir.gif\" alt=\"Fhir gif\"></img></p></div>", true, "containing an image element")]
        public void HtmlChecks(string xml, bool expected, string because)
        {
            var evaluator = _compiler.Compile("htmlChecks()");
            evaluator.Predicate(PocoNode.ForPrimitive<XHtml>(xml), new FhirEvaluationContext()).Should().Be(expected, because);
        }

        [TestMethod]
        [DataRow("Not empty", true, "plain text is valid content of a div")]
        [DataRow("<p>Not empty</p>", true, "valid html content of a div")]
        [DataRow(" ", false, "containing only whitespace")]
        [DataRow("", false, "empty string")]
        [DataRow("<p>unclosed", false, "not well-formed xml")]
        [DataRow("<script>alert('x')</script>", false, "scripts are not allowed")]
        public void HtmlChecksOnString(string html, bool expected, string because)
        {
            var evaluator = _compiler.Compile("htmlChecks()");
            evaluator.Predicate(PocoNode.ForPrimitive<FhirString>(html), new FhirEvaluationContext()).Should().Be(expected, because);
        }

        [TestMethod]
        public void HtmlChecksOnStringLiteral()
        {
            _compiler.Compile("'<p>Not empty</p>'.htmlChecks()")
                .Predicate(PocoNode.ForPrimitive<FhirBoolean>(true), new FhirEvaluationContext()).Should().BeTrue();
            _compiler.Compile("'<script>alert(1)</script>'.htmlChecks()")
                .Predicate(PocoNode.ForPrimitive<FhirBoolean>(true), new FhirEvaluationContext()).Should().BeFalse();
        }

        [TestMethod]
        public void HtmlChecksOnNonStringReturnsEmpty()
        {
            var evaluator = _compiler.Compile("htmlChecks()");
            var result = evaluator(PocoNode.ForPrimitive<FhirBoolean>(true), new FhirEvaluationContext());
            result.Should().BeEmpty("htmlChecks() returns empty on any other kind of element");
        }

        [TestMethod]
        public void HtmlChecksOnMultipleItemsReturnsEmpty()
        {
            var evaluator = _compiler.Compile("htmlChecks()");
            var result = evaluator(
                [PocoNode.ForPrimitive<FhirString>("<p>first</p>"), PocoNode.ForPrimitive<FhirString>("<p>second</p>")],
                new FhirEvaluationContext());
            result.Should().BeEmpty("htmlChecks() requires a singleton focus");
        }

        [TestMethod]
        [DynamicData(nameof(GetTypedElements))]
        public void NavigateWithChoiceTypes(ITypedElement typedElement, string method)
        {
            // expression with TypedElement
            typedElement.Predicate("parameter[0].value.exists()").Should().BeTrue();
            typedElement.Predicate("parameter[0].valueString.exists()").Should().BeFalse();
            typedElement.Scalar("parameter[0].value").Should().Be("test");
            typedElement.Scalar("parameter[0].valueString").Should().BeNull();
        }

        public static IEnumerable<object[]> GetTypedElements()
        {
            var xml = "<Parameters xmlns=\"http://hl7.org/fhir\"><parameter><name value=\"item\" /><valueString value=\"test\"/></parameter></Parameters>";
            var sourceNode = FhirXmlNode.Parse(xml);

            yield return [sourceNode.ToTypedElement(ModelInspector.Base), "sourceNode to TypedElement"];

            var poco = sourceNode.ToPoco<Parameters>(ModelInspector.Base);
            yield return [poco.ToTypedElement(ModelInspector.Base), "poco to TypedElement"];
        }

        public static IEnumerable<object[]> LowBoundaryTestCases() =>
            new (string expression, bool expected)[]
                {
                    ("1.587.lowBoundary(8) = 1.58650000", true),
                    ("1.587.lowBoundary()  = 1.5865", true),
                    ("0.0.lowBoundary()    = -0.05", true),
                    ("1.1.lowBoundary(8)   = 1.05000000", true),
                    ("1.25.lowBoundary(8)  = 1.24500000", true),
                    ("1.0.lowBoundary(8)   = 0.95000000", true),
                    ("2400.lowBoundary(8)  = 2399.50000", true),
                    ("2.25.lowBoundary(8)  = 2.24500000", true),
                    ("2.2500.lowBoundary(8)= 2.24995000", true),
                    ("12.lowBoundary() = 11.5", true),
                    ("'string'.lowBoundary() = 'string'", false),
                    ("1.587 'cm'.lowBoundary(8) = 1.58650000 'cm'", true),
                    ("@2014.lowBoundary(6) = @2014-01", true),
                    ("@2014.lowBoundary() = @2014-01-01T00:00:00.000", true),
                    ("@2014.lowBoundary(8) = @2014-01-01", true),
                    ("@2014-02.lowBoundary(8) = @2014-02-01", true),
                    ("@2014-01-01T08.lowBoundary(17) = @2014-01-01T08:00:00.000", true),
                    ("@T10:30.lowBoundary(9) = @T10:30:00.000", true),
                    ("@T10:30:00.000.lowBoundary() = @T10:30:00.000", true),
                    ("@T10:30.lowBoundary() = @T10:30:00", true),
                }.Select(t => new object[] { t.expression, t.expected });

        public static IEnumerable<object[]> HighBoundaryTestCases() =>
            new (string expression, bool expected)[]
                {
                    ("1.587.highBoundary(8) = 1.58750000", true),
                    ("1.587.highBoundary()  = 1.5875", true),
                    ("0.0.highBoundary()    = 0.05", true),
                    ("1.1.highBoundary(8)   = 1.15000000", true),
                    ("1.25.highBoundary(8)  = 1.25500000", true),
                    ("1.0.highBoundary(8)   = 1.05000000", true),
                    ("2400.highBoundary(8)  = 2400.50000", true),
                    ("2.25.highBoundary(8)  = 2.2550000", true),
                    ("2.2500.highBoundary(8)= 2.25005000", true),
                    ("12.highBoundary() = 12.5", true),
                    ("'string'.highBoundary() = 'string'", false),
                    ("1.587 'cm'.highBoundary(8) = 1.58750000 'cm'", true),
                    ("@2014.highBoundary(6) = @2014-12", true),
                    ("@2014.highBoundary(8) = @2014-12-31", true),
                    ("@2014-03.highBoundary(8) = @2014-03-31", true),
                    ("@2014-02.highBoundary(8) = @2014-02-28", true),
                    ("@2020-02.highBoundary(8) = @2020-02-29", true), // leap year
                    ("@T10:30.highBoundary(9) = @T10:30:59.999", true),
                    ("@T10:30.highBoundary() = @T10:30:59.999", true),
                }.Select(t => new object[] { t.expression, t.expected });

        public static IEnumerable<object[]> ComparableTestCases() =>
           new (string expression, bool expected)[]
               {
                    ("1 'cm'.comparable(1 '[in_i]')", false),
                    ("1 week.comparable(1 'wk')", true),
                    ("1 'cm'.comparable(1 's')", false),
                }.Select(t => new object[] { t.expression, t.expected });

        public static IEnumerable<object[]> AllTestCases() =>
            LowBoundaryTestCases()
            .Concat(HighBoundaryTestCases())
            .Concat(ComparableTestCases());


        [TestMethod]
        [DynamicData(nameof(AllTestCases))]
        public void AssertFhirPathTestcases(string expression, bool expected)
        {
            var evaluator = _compiler.Compile(expression);
            var result = evaluator(PocoNode.ForPrimitive<FhirBoolean>(true), new FhirEvaluationContext());

            if (result.Any())
            {
                result.Should().ContainSingle().Which.GetValue().Should().Be(expected);
            }
            else
            {
                expected.Should().BeFalse();
            }
        }

        [TestMethod]
        // default precision is 8
        [DataRow("1.587.lowBoundary()", "1.58650000")]
        [DataRow("1.587.highBoundary()", "1.58750000")]
        [DataRow("1.lowBoundary()", "0.50000000")]
        [DataRow("1.highBoundary()", "1.50000000")]
        [DataRow("0.0.lowBoundary()", "-0.05000000")]
        [DataRow("0.0.highBoundary()", "0.05000000")]
        // zero lies half a unit from both its boundaries, so both are rounded half up when digits are dropped
        [DataRow("0.lowBoundary(0)", "-1")]
        [DataRow("0.highBoundary(0)", "1")]
        [DataRow("0.0.lowBoundary(0)", "0")]
        [DataRow("0.0.highBoundary(1)", "0.1")]
        [DataRow("(-0.0034).lowBoundary(1)", "0.0")]
        [DataRow("1.587 'cm'.lowBoundary()", "1.58650000 'cm'")]
        // fewer decimal places than the boundary: the low boundary of a positive number is truncated,
        // the high boundary is rounded half up (with carry)
        [DataRow("1.587.lowBoundary(2)", "1.58")]
        [DataRow("1.587.highBoundary(2)", "1.59")]
        [DataRow("1.587.lowBoundary(0)", "1")]
        [DataRow("1.lowBoundary(0)", "0")]
        [DataRow("1.highBoundary(0)", "2")]
        [DataRow("10.lowBoundary(0)", "9")]
        [DataRow("100.lowBoundary(2)", "99.50")]
        [DataRow("120.highBoundary(2)", "120.50")]
        [DataRow("12.500.lowBoundary(4)", "12.4995")]
        [DataRow("12.500.highBoundary(4)", "12.5005")]
        [DataRow("0.0034.lowBoundary(1)", "0.0")]
        [DataRow("0.0034.highBoundary(1)", "0.0")]
        [DataRow("9.95.highBoundary(1)", "10.0")]
        [DataRow("0.996.highBoundary(1)", "1.0")]
        [DataRow("99.96.highBoundary(1)", "100.0")]
        [DataRow("9.highBoundary(0)", "10")]
        [DataRow("1.587 'cm'.highBoundary(2)", "1.59 'cm'")]
        // negative numbers mirror the boundaries of their magnitude
        [DataRow("(-1.587).lowBoundary()", "-1.58750000")]
        [DataRow("(-1.587).highBoundary()", "-1.58650000")]
        [DataRow("(-1.587).lowBoundary(2)", "-1.59")]
        [DataRow("(-1.587).highBoundary(2)", "-1.58")]
        [DataRow("(-1.587).lowBoundary(0)", "-2")]
        [DataRow("(-1.587).lowBoundary(6)", "-1.587500")]
        [DataRow("(-120).lowBoundary(2)", "-120.50")]
        [DataRow("(-9.96).lowBoundary(1)", "-10.0")]
        [DataRow("(-99.96).lowBoundary(1)", "-100.0")]
        [DataRow("(-9).lowBoundary(0)", "-10")]
        [DataRow("(-10).highBoundary(0)", "-9")]
        // more decimal places than the boundary
        [DataRow("1.587.lowBoundary(6)", "1.586500")]
        [DataRow("1.587.highBoundary(28)", "1.5875000000000000000000000000")]
        public void DecimalBoundaryHasRequestedPrecision(string expression, string expected)
        {
            var result = _compiler.Compile(expression)(PocoNode.ForPrimitive<FhirBoolean>(true), new FhirEvaluationContext()).ToList();

            result.Should().ContainSingle();
            ElementModel.Types.Any.Convert(((ITypedElement)result[0]).Value).ToString().Should().Be(expected);
        }



    }
}