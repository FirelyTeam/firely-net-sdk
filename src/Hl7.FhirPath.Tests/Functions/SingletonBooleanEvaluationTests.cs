using FluentAssertions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.FhirPath;
using Hl7.FhirPath.Functions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace HL7.FhirPath.Tests
{
    /// <summary>
    /// Where an operator or function expects a single boolean (a criteria expression, or an operand of
    /// a boolean operator), a collection with more than one item is an execution error, following the
    /// FHIRPath "singleton evaluation of collections" rules and the Java reference engine (asBool()).
    /// See https://github.com/FirelyTeam/firely-net-sdk/issues/3745.
    /// </summary>
    [TestClass]
    public class SingletonBooleanEvaluationTests
    {
        private static readonly FhirPathCompiler COMPILER = new();
        private static readonly PocoNode DUMMY = PocoNode.ForPrimitive<FhirBoolean>(true);

        private static PocoNode[] evaluate(string expression) =>
            COMPILER.Compile(expression)(DUMMY, new EvaluationContext()).ToArray();

        [TestMethod]
        [DataRow("(1 | 2).where((true | false))")]
        [DataRow("(1 | 2).where($this | 3)")]
        [DataRow("(1 | 2).exists((true | false))")]
        [DataRow("(1 | 2).all((1 | 2))")]
        [DataRow("(1 | 2).all(true | 1)")]
        [DataRow("iif((true | false), 1, 2)")]
        [DataRow("iif(1 | 2 | 3, true, false)")]
        [DataRow("(true | false).not()")]
        [DataRow("(true | false) and true")]
        [DataRow("true and (true | false)")]
        [DataRow("{} and (true | false)")]
        [DataRow("(true | false) or false")]
        [DataRow("false or (true | false)")]
        [DataRow("{} or (true | false)")]
        [DataRow("(true | false) xor true")]
        [DataRow("true xor (true | false)")]
        [DataRow("{} xor (true | false)")]
        [DataRow("(true | false) implies true")]
        [DataRow("true implies (true | false)")]
        [DataRow("{} implies (true | false)")]
        public void MultipleItemsWhereBooleanExpectedIsAnError(string expression)
        {
            Action act = () => evaluate(expression);
            act.Should().Throw<InvalidOperationException>().WithMessage("*more than one item*");
        }

        [TestMethod]
        // Short-cut evaluation: the right operand is never evaluated (as in the Java engine).
        [DataRow("false and (true | false)", false)]
        [DataRow("true or (true | false)", true)]
        [DataRow("false implies (true | false)", true)]
        // xor still gives empty when one side is empty.
        [DataRow("({} xor true).empty()", true)]
        [DataRow("(true xor {}).empty()", true)]
        // A single non-boolean item still counts as true.
        [DataRow("(1 | 2).where('a').count() = 2", true)]
        [DataRow("(1 | 2).all(1)", true)]
        [DataRow("(1 | 2).exists(1)", true)]
        [DataRow("iif('a', true, false)", true)]
        [DataRow("'a' and true", true)]
        [DataRow("1.not()", false)]
        // An empty criteria result is not an error.
        [DataRow("(1 | 2).where({}).empty()", true)]
        [DataRow("iif({}, true, false)", false)]
        // Multi-item collections are fine wherever no single boolean is expected.
        [DataRow("(1 | 2).select($this | 3).count() = 4", true)]
        [DataRow("(true | false).allTrue()", false)]
        [DataRow("(true | false).anyTrue()", true)]
        public void SingleItemOrNoItemsWhereBooleanExpectedIsFine(string expression, bool expected)
        {
            var result = evaluate(expression);
            result.Should().ContainSingle().Which.Should().BeOfType<PrimitiveNode>()
                .Which.Value.Should().Be(expected);
        }

        [TestMethod]
        public void WhereOnlyFailsWhenTheOffendingItemIsReached()
        {
            // where() is evaluated lazily, so the error only surfaces once the item is enumerated.
            evaluate("(1 | 2).where(iif($this = 1, true, (true | false))).first() = 1")
                .Should().ContainSingle().Which.Should().BeOfType<PrimitiveNode>().Which.Value.Should().Be(true);

            Action act = () => evaluate("(1 | 2).where(iif($this = 1, true, (true | false))).count()");
            act.Should().Throw<InvalidOperationException>().WithMessage("*more than one item*");
        }

        [TestMethod]
        [DataRow("(true | false)")]
        [DataRow("(false | 1)")]
        [DataRow("(1 | 2)")]
        public void TopLevelResultWithMultipleItemsIsStillTrueForPublicHelpers(string expression)
        {
            // The helpers that turn a final result into a .NET bool stay lenient, like the Java engine's
            // convertToBoolean(), so invariants whose result has more than one item keep working as before.
            var compiled = COMPILER.Compile(expression);
            var ctx = new EvaluationContext();

            compiled.IsTrue(DUMMY, ctx).Should().BeTrue();
            compiled.Predicate(DUMMY, ctx).Should().BeTrue();
            compiled.IsBoolean(true, DUMMY, ctx).Should().BeTrue();
            compiled.IsBoolean(false, DUMMY, ctx).Should().BeFalse();
        }

        [TestMethod]
        public void LenientHelpersStillEvaluateEveryItem()
        {
            // The third item's criteria is a collection with more than one item, which is an error. The lenient
            // helpers must still evaluate it, rather than stop after two items of the lazy where() result.
            var compiled = COMPILER.Compile("(1 | 2 | 3).where(iif($this = 3, (true | false), true))");
            var ctx = new EvaluationContext();

            Action predicate = () => compiled.Predicate(DUMMY, ctx);
            Action isTrue = () => compiled.IsTrue(DUMMY, ctx);
            Action isBoolean = () => compiled.IsBoolean(true, DUMMY, ctx);
            predicate.Should().Throw<InvalidOperationException>();
            isTrue.Should().Throw<InvalidOperationException>();
            isBoolean.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void BooleanEvalFollowsSingletonEvaluation()
        {
            Array.Empty<PocoNode>().BooleanEval().Should().BeNull();
            new[] { PocoNode.ForPrimitive<FhirBoolean>(false) }.BooleanEval().Should().BeFalse();
            new[] { PocoNode.ForPrimitive<FhirString>("a") }.BooleanEval().Should().BeTrue();

            var two = new[] { PocoNode.ForPrimitive<FhirBoolean>(true), PocoNode.ForPrimitive<FhirBoolean>(true) };
            Action act = () => two.BooleanEval();
            act.Should().Throw<InvalidOperationException>();
            two.LenientBooleanEval().Should().BeTrue();
            Array.Empty<PocoNode>().LenientBooleanEval().Should().BeNull();
        }
    }
}
