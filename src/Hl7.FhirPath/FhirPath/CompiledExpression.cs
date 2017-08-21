using System;
using System.Linq;
using System.Collections.Generic;
using Hl7.FhirPath.Parser;
using Hl7.FhirPath;
using Hl7.FhirPath.Expressions;
using Sprache;
using System.Text;
using System.Threading.Tasks;
using Hl7.FhirPath.Functions;
using Hl7.Fhir.ElementModel;

namespace Hl7.FhirPath
{
    public class EvaluationContext
    {
        public static readonly EvaluationContext Default = new EvaluationContext();

        public EvaluationContext()
        {
            // no defaults yet
        }

        public EvaluationContext(IElementNavigator container)
        {
            Container = container;
        }

        public IElementNavigator Container { get; set; }
    }



    public delegate IEnumerable<IElementNavigator> CompiledExpression(IElementNavigator root, EvaluationContext ctx);

    public static class CompiledExpressionExtensions
    {
        public static object Scalar(this CompiledExpression evaluator, IElementNavigator input, EvaluationContext ctx)
        {
            var result = evaluator(input, ctx);
            if (result.Any())
                return evaluator(input, ctx).Single().Value;
            else
                return null;
        }

        // For predicates, Empty is considered true
        public static bool Predicate(this CompiledExpression evaluator, IElementNavigator input, EvaluationContext ctx)
        {
            var result = evaluator(input, ctx).BooleanEval();

            if (result == null)
                return true;
            else
                return result.Value;
        }

        public static bool IsBoolean(this CompiledExpression evaluator, bool value, IElementNavigator input, EvaluationContext ctx)
        {
            var result = evaluator(input, ctx).BooleanEval();

            if (result == null)
                return false;
            else
                return result.Value == value;
        }
    }


}