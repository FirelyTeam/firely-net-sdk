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
            var result = evaluator(null, FhirEvaluationContext.CreateDefault());

            Assert.IsFalse(result.Any());
        }

        [DataTestMethod]
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
            evaluator.Predicate(ElementNode.ForPrimitive(xml), FhirEvaluationContext.CreateDefault()).Should().Be(expected, because);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetTypedElements), DynamicDataSourceType.Method)]
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

            yield return new object[] { sourceNode.ToTypedElement(ModelInspector.ForAssembly(typeof(Resource).Assembly)), "sourceNode to TypedElement" };

            var poco = sourceNode.ToPoco<Parameters>(ModelInspector.Base);
            yield return new object[] { poco.ToTypedElement(ModelInspector.Base), "poco to TypedElement" };

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
                    ("12.lowBoundary() = 12", true),
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
                    ("12.highBoundary() = 12", true),
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
                    ("1 'cm'.comparable(1 '[in_i]')", true),
                    ("1 week.comparable(1 'wk')", true),
                    ("1 'cm'.comparable(1 's')", false),
                }.Select(t => new object[] { t.expression, t.expected });

        public static IEnumerable<object[]> AllTestCases() =>
            LowBoundaryTestCases()
            .Concat(HighBoundaryTestCases())
            .Concat(ComparableTestCases());


        [DataTestMethod]
        [DynamicData(nameof(AllTestCases), DynamicDataSourceType.Method)]
        public void AssertFhirPathTestcases(string expression, bool expected)
        {
            var evaluator = _compiler.Compile(expression);
            var result = evaluator(null, FhirEvaluationContext.CreateDefault());

            if (result.Any())
            {
                result.Should().ContainSingle().Which.Value.Should().Be(expected);
            }
            else
            {
                expected.Should().BeFalse();
            }
        }



    }
}
