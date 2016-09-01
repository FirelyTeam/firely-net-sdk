using System;
using System.Linq;
using System.Collections.Generic;
using Hl7.FluentPath.Parser;
using Hl7.FluentPath;
using Hl7.FluentPath.Expressions;
using Sprache;
using System.Text;
using System.Threading.Tasks;
using Hl7.ElementModel;
using Hl7.FluentPath.Functions;

namespace Hl7.FluentPath
{
    public delegate IEnumerable<IValueProvider> CompiledExpression(IValueProvider root, IValueProvider containerResource);

    public static class CompiledExpressionExtensions
    {
        public static object Scalar(this CompiledExpression evaluator, IValueProvider input, IValueProvider resource)
        {
            var result = evaluator(input, resource);
            if (result.Any())
                return evaluator(input, resource).Single().Value;
            else
                return null;
        }

        // For predicates, Empty is considered true
        public static bool Predicate(this CompiledExpression evaluator, IValueProvider input, IValueProvider resource)
        {
            var result = evaluator(input, resource).BooleanEval();

            if (result == null)
                return true;
            else
                return result.Value;
        }

        public static bool IsBoolean(this CompiledExpression evaluator, bool value, IValueProvider input, IValueProvider resource)
        {
            var result = evaluator(input, resource).BooleanEval();

            if (result == null)
                return false;
            else
                return result.Value == value;
        }
    }


}