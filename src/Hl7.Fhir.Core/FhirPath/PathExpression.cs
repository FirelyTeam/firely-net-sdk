using Hl7.Fhir.FhirPath.Grammar;
using Sprache;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.FhirPath
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

        public static IEnumerable<IFhirPathValue> Evaluate(string expression, IFhirPathValue instance, IEvaluationContext context)
        {
            var evaluator = Compile(expression);
            return evaluator(FhirValueList.Create(instance), context);
        }

        public static IEnumerable<IFhirPathValue> Evaluate(string expression, IFhirPathValue instance)
        {
            var evaluator = Compile(expression);
            return evaluator(FhirValueList.Create(instance), new EvaluationContext());
        }

        public static object Scalar(string expression, IFhirPathValue instance, IEvaluationContext context)
        {
            var evaluator = Compile(expression);
            return evaluator.Scalar(instance, context);
        }

        public static object Scalar(string expression, IFhirPathValue instance)
        {
            var evaluator = Compile(expression);
            return evaluator.Scalar(instance, new EvaluationContext());
        }

        public static bool Predicate(string expression, IFhirPathValue instance, IEvaluationContext context)
        {
            var evaluator = Compile(expression);
            return evaluator.Predicate(instance, context);
        }

        public static bool Predicate(string expression, IFhirPathValue instance)
        {
            var evaluator = Compile(expression);
            return evaluator.Predicate(instance, new EvaluationContext());
        }

    }


}
