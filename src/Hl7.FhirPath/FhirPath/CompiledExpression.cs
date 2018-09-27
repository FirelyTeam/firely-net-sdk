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
    }


}