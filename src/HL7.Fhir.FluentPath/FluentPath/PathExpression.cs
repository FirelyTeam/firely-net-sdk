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

        public static IEnumerable<T> Select<T>(string expression, T instance, IEvaluationContext context) where T : IFluentPathValue
        {
            var evaluator = Compile(expression);
            return evaluator(FhirValueList.Create(instance), context).Select(v => (T)v);
        }

        public static IEnumerable<T> Select<T>(string expression, T instance) where T: IFluentPathValue
        {
            var evaluator = Compile(expression);
            return evaluator(FhirValueList.Create(instance), new BaseEvaluationContext()).Select(v => (T)v);
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

        public static bool IsTrue(string expression, IFluentPathValue instance, IEvaluationContext context)
        {
            var evaluator = Compile(expression);
            return evaluator.Predicate(instance, context);
        }

        public static bool IsTrue(string expression, IFluentPathValue instance)
        {
            var evaluator = Compile(expression);
            return evaluator.Predicate(instance, new BaseEvaluationContext());
        }

    }

    public static class PathExpressionLinq
    {
        public static IEnumerable<T> FluentPathSelect<T>(this T instance, string expression) where T : IFluentPathValue
        {
            return PathExpression.Select(expression, instance);
        }

        public static IEnumerable<T> FluentPathIsTrue<T>(this T instance, string expression) where T : IFluentPathValue
        {
            return PathExpression.Select(expression, instance);
        }

    }


}
