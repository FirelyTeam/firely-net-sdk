#nullable enable

using Hl7.Fhir.ElementModel;
using Hl7.FhirPath.Functions;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.FhirPath
{
    public delegate IEnumerable<ITypedElement> CompiledExpression(ITypedElement root, EvaluationContext ctx);

    public static class CompiledExpressionExtensions
    {
        /// <summary>
        /// Evaluates an expression against a given context and returns a single result
        /// </summary>
        /// <param name="evaluator">Expression which is to be evaluated</param>
        /// <param name="input">Input at which the expression is evaluated</param>
        /// <param name="ctx">Context of the evaluation</param>
        /// <returns>The single result of an expression</returns>
        public static object? Scalar(this CompiledExpression evaluator, ITypedElement input, EvaluationContext ctx)
        {
            var result = evaluator(input, ctx).Take(2).ToArray();
            return result.Any() ? result.Single().Value : null;
        }

        /// <summary>
        /// Evaluates an expression and returns true for expression being evaluated as true or empty, otherwise false.
        /// </summary>
        /// <param name="evaluator">Expression which is to be evaluated</param>
        /// <param name="input">Input at which the expression is evaluated</param>
        /// <param name="ctx">Context of the evaluation</param>
        /// <returns>True if expression returns true of empty, otheriwse false</returns>
        public static bool Predicate(this CompiledExpression evaluator, ITypedElement input, EvaluationContext ctx)
        {
            var result = evaluator(input, ctx).BooleanEval();
            return result is null || result.Value;
        }

        /// <summary>
        /// Evaluates an expression and returns true for expression being evaluated as true, and false if the expression returns false or empty.
        /// </summary>
        /// <param name="evaluator">Expression which is to be evaluated</param>
        /// <param name="input">Input at which the expression is evaluated</param>
        /// <param name="ctx">Context of the evaluation</param>
        /// <returns>True if expression returns true , and false if expression returns empty of false.</returns>
        public static bool IsTrue(this CompiledExpression evaluator, ITypedElement input, EvaluationContext ctx)
        {
            var result = evaluator(input, ctx).BooleanEval();
            return result is not null && result.Value;
        }

        /// <summary>
        /// Evaluates if the result of an expression is equal to a given boolean.
        /// </summary>
        /// <param name = "evaluator"> Expression which is to be evaluated</param>
        /// <param name="value">boolean that is to be compared to the result of the expression</param>
        /// <param name="input">Input at which the expression is evaluated</param>
        /// <param name="ctx">Context of the evaluation</param>
        /// <returns>True if the result of an expression is equal to a given boolean, otherwise false</returns>
        public static bool IsBoolean(this CompiledExpression evaluator, bool value, ITypedElement input, EvaluationContext ctx)
        {
            var result = evaluator(input, ctx).BooleanEval();
            return result is not null && result.Value == value;
        }
    }
}

#nullable restore