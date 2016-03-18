using Hl7.Fhir.FluentPath.Grammar;
using Sprache;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.FluentPath
{
    public static class PathExpression
    {
        private static ConcurrentDictionary<string, Evaluator> _cache = new ConcurrentDictionary<string, Evaluator>();

        public static Evaluator Compile(string expression)
        {
            var cacheName = expression.Replace(" ", "");

            if (_cache.ContainsKey(cacheName))
                return _cache[cacheName]; 

            var compilation = Expression.Expr.End().TryParse(expression);

            if (compilation.WasSuccessful)
            {
                _cache.TryAdd(cacheName, compilation.Value);
//                if(_cache.Count)
                return compilation.Value;
            }
            else
            {
               throw new FormatException("Compilation failed: " + compilation.ToString());
            }
        }

        public static IEnumerable<IFluentPathValue> Evaluate(string expression, IFluentPathValue instance, IEvaluationContext context)
        {
            var evaluator = Compile(expression);
            return evaluator(FhirValueList.Create(instance), context);
        }

        public static IEnumerable<IFluentPathValue> Evaluate(string expression, IFluentPathValue instance)
        {
            var evaluator = Compile(expression);
            return evaluator(FhirValueList.Create(instance), new BaseEvaluationContext());
        }

        public static object Scalar(string expression, IFluentPathValue instance, IEvaluationContext context)
        {
            var evaluator = Compile(expression);
            return evaluator.Scalar(instance, context);
        }

        public static object Scalar(string expression, IFluentPathValue instance)
        {
            var evaluator = Compile(expression);
            return evaluator.Scalar(instance, new BaseEvaluationContext());
        }

        public static bool Predicate(string expression, IFluentPathValue instance, IEvaluationContext context)
        {
            var evaluator = Compile(expression);
            return evaluator.Predicate(instance, context);
        }

        public static bool Predicate(string expression, IFluentPathValue instance)
        {
            var evaluator = Compile(expression);
            return evaluator.Predicate(instance, new BaseEvaluationContext());
        }

    }


}
