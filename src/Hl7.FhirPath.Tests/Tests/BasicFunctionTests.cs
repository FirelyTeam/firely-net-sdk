/*
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

// To introduce the DSTU2 FHIR specification
//extern alias dstu2;

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.FhirPath.Functions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.FhirPath.Tests
{
    [TestClass]
    public class BasicFunctionsTest
    {
        private static void isB(string expr, object value = null)
        {
            var dummy = PocoNode.ForAnyPrimitive(value ?? true);
            var compiler = new FhirPathCompiler();
            var evaluator = compiler.Compile(expr, true);
            Assert.IsTrue(evaluator.IsBoolean(true, dummy, new EvaluationContext() { DebugTracer = new DiagnosticsDebugTracer() }));
        }

        private static object scalar(string expr)
        {
            PocoNode dummy = PocoNode.ForAnyPrimitive(true);
            var compiler = new FhirPathCompiler();
            var evaluator = compiler.Compile(expr, true);
            return evaluator.Scalar(dummy, new EvaluationContext() { DebugTracer = new DiagnosticsDebugTracer() });
        }

        private static object scalar(PocoNode dummy, string expr)
        {
            var compiler = new FhirPathCompiler();
            var evaluator = compiler.Compile(expr, true);
            return evaluator.Scalar(dummy, new EvaluationContext() { DebugTracer = new DiagnosticsDebugTracer() });
        }

        [TestMethod]
        public void TestDynaBinding()
        {
#pragma warning disable CS0618 // Type or member is internal
            var input = SourceNode.Node("root",
                    SourceNode.Valued("child", "Hello world!"),
                    SourceNode.Valued("child", "4")).ToTypedElementLegacy();
#pragma warning restore CS0618 // Type or member is internal
            Assert.AreEqual("ello", input.Scalar(@"$this.child[0].substring(1,%context.child[1].toInteger())"));
        }

        [TestMethod]
        public void TestGreaterThan()
        {
            isB(@"4.5 > 0");
            isB(@"'ewout' > 'alfred'");
            isB(@"2016-04-01 > 2015-04-01");
            isB(@"5 > 6 = false");
            isB(@"(5 > {}).empty()");
        }

        [TestMethod]
        public void TestMath()
        {
            isB(@"-4.5 + 4.5 = 0");
            isB(@"4/2 = 2");
            isB(@"2/4 = 0.5");
            isB(@"10/4 = 2.5");
            isB(@"10.0/4 = 2.5");
            isB(@"4.0/2.0 = 2");
            isB(@"2.0/4 = 0.5");
            isB(@"2.0 * 4 = 8");
            isB(@"2 * 4.1 = 8.2");
            isB(@"-0.5 * 0.5 = -0.25");
            isB(@"5 - 4.5 = 0.5");
            isB(@"9.5 - 4.5 = 5");
            isB(@"5 + 4.5 = 9.5");
            isB(@"9.5 + 0.5 = 10");

            isB(@"103 mod 5 = 3");
            isB(@"101.4 mod 5.2 = 2.6");
            isB(@"103 div 5 = 20");
            isB(@"20.0 div 5.5 = 3");

            isB(@"'offic'+'ial' = 'official'");

            isB(@"12/(2+2) - (3 div 2) = 2");
            isB(@"-4.5 + 4.5 * 2 * 4 / 4 - 1.5 = 3");
        }


        [TestMethod]
        public void Test3VLBoolean()
        {
            isB(@"true and true");
            isB(@"(true and false) = false");
            isB(@"(true and {}).empty()");
            isB(@"(false and true) = false");
            isB(@"(false and false) = false");
            isB(@"(false and {}) = false");
            isB(@"({} and true).empty()");
            isB(@"({} and false) = false");
            isB(@"({} and {}).empty()");

            isB(@"true or true");
            isB(@"true or false");
            isB(@"true or {}");
            isB(@"false or true");
            isB(@"(false or false) = false");
            isB(@"(false or {}).empty()");
            isB(@"{} or true");
            isB(@"({} or false).empty()");
            isB(@"({} or {}).empty()");

            isB(@"(true xor true)=false");
            isB(@"true xor false");
            isB(@"(true xor {}).empty()");
            isB(@"false xor true");
            isB(@"(false xor false) = false");
            isB(@"(false xor {}).empty()");
            isB(@"({} xor true).empty()");
            isB(@"({} xor false).empty()");
            isB(@"({} xor {}).empty()");

            isB(@"true implies true");
            isB(@"(true implies false) = false");
            isB(@"(true implies {}).empty()");
            isB(@"false implies true");
            isB(@"false implies false");
            isB(@"false implies {}");
            isB(@"{} implies true");
            isB(@"({} implies false).empty()");
            isB(@"({} implies {}).empty()");
        }

        [TestMethod]
        public void TestConversions()
        {
            isB(@"'654321'.toDecimal() = 654321");

            isB(@"(4.1).toString() = '4.1'");
            isB(@"true.toString() = 'true'");
            isB(@"true.toDecimal() = 1");
            isB(@"@2014-12-14T.toString() = '2014-12-14'");
            isB(@"@2014-12-14.toString() = '2014-12-14'");

            isB(@"1.convertsToInteger()");
            isB(@"'14:34:28'.convertsToTime()");
            isB(@"1.convertsToQuantity()");
        }

        [TestMethod]
        public void TestDateTimeEquality()
        {
            isB(@"@2015-01-01 = @2015-01-01");
            isB(@"@2015-01-01T = @2015-01-01T");
            isB(@"(@2015-01-01 != @2015-01).empty()");
            isB(@"(@2015-01-01T != @2015-01T).empty()");

            isB(@"@2015-01-01T13:40:50+00:00 = @2015-01-01T13:40:50Z");

            isB(@"@T13:45:02 = @T13:45:02");
            isB(@"@T13:45:02 != @T14:45:02");
        }

        [TestMethod]
        public void TestDateTimeEquivalence()
        {
            isB("@2012-04-15T !~ @2012-04-15T10:00:00");
            isB("@T10:01:02 !~ @T10:01:55");
        }

        [TestMethod]
        public void TestSubstring()
        {
            isB("substring(0,6) = 'Donald'", "Donald");
            isB("substring(2,6) = 'nald'", "Donald");
            isB("substring(2,4) = 'nald'", "Donald");

            isB("substring(2,length()-3) = 'nal'", "Donald");

            isB("substring(-1,8).empty()", "Donald");
            isB("substring(999,1).empty()", "Donald");
            isB("''.substring(0,1).empty()");
            isB("{}.substring(0,10).empty()");
            isB("{}.substring(0,10).empty()");
        }

        [TestMethod]
        public void TestExpressionTodayFunction()
        {
            // Check that date comes in
            Assert.AreEqual(P.Date.Today(), scalar("today()"));

            // Check greater than
            isB("today() < @" + P.Date.FromDateTimeOffset(DateTimeOffset.UtcNow.AddDays(2), includeOffset: false));

            // Check less than
            isB("today() > @" + P.Date.FromDateTimeOffset(DateTimeOffset.UtcNow.AddDays(-1), includeOffset: false));

            // Check ==
            isB("today() = @" + P.Date.Today());

            // This unit-test will fail if you are working between midnight
            // and start-of-day in GMT:
            // e.g. 2018-08-10T01:00T+02:00 > 2018-08-10 will fail, which is then
            // test on the next line
            //isB("now() > @" + DateTime.Today());
            isB("now() >= @" + P.DateTime.Now());
        }

        [TestMethod]
        public void TestLogicalShortcut()
        {
            isB(@"true or (1/0 = 0)");
            isB(@"(false and (1/0 = 0)) = false");
        }


        [TestMethod]
        public void StringConcatenationAndEmpty()
        {
            PocoNode dummy = PocoNode.ForAnyPrimitive(true);

            Assert.AreEqual("ABCDEF", scalar(dummy, "'ABC' + '' + 'DEF'"));
            Assert.AreEqual("DEF", scalar(dummy, "'' + 'DEF'"));
            Assert.AreEqual("DEF", scalar(dummy, "'DEF' + ''"));

            Assert.IsNull(scalar(dummy, "{} + 'DEF'"));
            Assert.IsNull(scalar(dummy, "'ABC' + {} + 'DEF'"));
            Assert.IsNull(scalar(dummy, "'ABC' + {}"));

            Assert.AreEqual("ABCDEF", scalar(dummy, "'ABC' & '' & 'DEF'"));
            Assert.AreEqual("DEF", scalar(dummy, "'' & 'DEF'"));
            Assert.AreEqual("DEF", scalar(dummy, "'DEF' & ''"));

            Assert.AreEqual("DEF", scalar(dummy, "{} & 'DEF'"));
            Assert.AreEqual("ABCDEF", scalar(dummy, "'ABC' & {} & 'DEF'"));
            Assert.AreEqual("ABC", scalar(dummy, "'ABC' & {}"));

            Assert.IsNull(scalar(dummy, "'ABC' & {} & 'DEF' + {}"));
        }

        [TestMethod]
        public void TestStringSplit()
        {
            ITypedElement dummy = ElementNode.ForPrimitive("a,b,c,d");
            var result = dummy.Select("split(',')");
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(new[] { "a", "b", "c", "d" }, result.Select(r => r.Value.ToString()).ToArray());

            dummy = ElementNode.ForPrimitive("a,,b,c,d"); // Empty element should be removed
            result = dummy.Select("split(',')");
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(new[] { "a", "", "b", "c", "d" }, result.Select(r => r.Value.ToString()).ToArray());

            dummy = ElementNode.ForPrimitive("");
            result = dummy.Select("split(',')");
            Assert.IsNotNull(result);

            dummy = ElementNode.ForPrimitive("[stop]ONE[stop][stop]TWO[stop][stop][stop]THREE[stop][stop]");
            result = dummy.Select("split('[stop]')");
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(new[] { "", "ONE", "", "TWO", "", "", "THREE", "", "" }, result.Select(r => r.Value.ToString()).ToArray());
        }

        [TestMethod]
        [DataRow("(1 | 2 | 3).indexOf(3)", 2)]
        [DataRow("((1 | 2 | 3).combine(2)).indexOf(2, 2)", 3)]
        [DataRow("((1 | 2 | 3).combine(2)).lastIndexOf(2)", 3)]
        [DataRow("(1 | 2).combine(2 | 1).lastIndexOf(1, 2)", 0)]
        public void TestStringIndexOf(string expr, int expected)
        {
            Assert.AreEqual(expected, scalar(expr));
        }

        [TestMethod]
        public void TestDivZero()
        {
            Assert.IsNull(scalar("1 / 0"));
            Assert.IsNull(scalar("1.0 / 0"));
            Assert.IsNull(scalar("1 mod 0"));
            Assert.IsNull(scalar("1 mod 0.0"));
            Assert.IsNull(scalar("1 div 0"));
            Assert.IsNull(scalar("1.0 div 0"));
        }

        [TestMethod]
        public void TestStringJoin()
        {
            var dummy = PocoNode.FromList<FhirString>(["This ", "is ", "one ", "sentence", "."]);
            var result = dummy.FpJoin(string.Empty);
            Assert.IsNotNull(result);
            Assert.AreEqual("This is one sentence.", result);

            dummy = PocoNode.FromList<FhirString>(["a", "b", "c"]);
            result = dummy.FpJoin();
            Assert.IsNotNull(result);
            Assert.AreEqual("abc", result);

            dummy = PocoNode.FromList<FhirString>([]);
            result = dummy.FpJoin(string.Empty);
            Assert.AreEqual(string.Empty, result);

            dummy = PocoNode.FromList<FhirString>(["This", "is", "a", "separated", "sentence."]);
            result = dummy.FpJoin(";");
            Assert.IsNotNull(result);
            Assert.AreEqual("This;is;a;separated;sentence.", result);
        }

        [TestMethod]
        public void TestStringJoinError()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                var dummy = PocoNode.FromList<FhirString>(["This", "is", "sentence", "with", 1, "number."]);
                dummy.FpJoin(string.Empty);
            });
        }
    }

}