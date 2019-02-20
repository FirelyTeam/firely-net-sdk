using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.ElementModel;
using Hl7.FhirPath.Functions;

namespace Hl7.FhirPath
{
    public delegate IEnumerable<ITypedElement> CompiledExpression(ITypedElement root, EvaluationContext ctx);

    public static class CompiledExpressionExtensions
    {
        public static object Scalar(this CompiledExpression evaluator, ITypedElement input, EvaluationContext ctx)
        {
            var result = evaluator(input, ctx);
            if (result.Any())
                return evaluator(input, ctx).Single().Value;
            else
                return null;
        }

        // For predicates, Empty is considered true
        public static bool Predicate(this CompiledExpression evaluator, ITypedElement input, EvaluationContext ctx)
        {
            var result = evaluator(input, ctx).BooleanEval();

            if (result == null)
                return true;
            else
                return result.Value;
        }

        public static bool IsBoolean(this CompiledExpression evaluator, bool value, ITypedElement input, EvaluationContext ctx)
        {
            var result = evaluator(input, ctx).BooleanEval();

            if (result == null)
                return false;
            else
                return result.Value == value;
        }

        #region Obsolete members
        [Obsolete("Use Scalar(this CompiledExpression evaluator, ITypedElement input, EvaluationContext ctx) instead. Obsolete since 2018-10-17")]
        public static object Scalar(this CompiledExpression evaluator, IElementNavigator input, EvaluationContext ctx)
        {
            return evaluator.Scalar(input.ToTypedElement(), ctx);
        }

        [Obsolete("Use Predicate(this CompiledExpression evaluator, IElementNavigator input, EvaluationContext ctx) instead. Obsolete since 2018-10-17")]
        public static bool Predicate(this CompiledExpression evaluator, IElementNavigator input, EvaluationContext ctx)
        {
            return evaluator.Predicate(input.ToTypedElement(), ctx);
        }

        [Obsolete("Use IsBoolean(this CompiledExpression evaluator, bool value, ITypedElement input, EvaluationContext ctx) instead. Obsolete since 2018-10-17")]
        public static bool IsBoolean(this CompiledExpression evaluator, bool value, IElementNavigator input, EvaluationContext ctx)
        {
            return evaluator.IsBoolean(value, input.ToTypedElement(), ctx);
        }
        #endregion
    }
}