/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.ElementModel;
using Hl7.FhirPath;
using Sprache;

namespace Hl7.FhirPath
{
    public static class IValueProviderFPExtensions
    {
        private static Dictionary<string, CompiledExpression> _cache = new Dictionary<string, CompiledExpression>();
        private static List<string> _mruList = new List<string>();      // sigh, no sortedlist in NETSTANDARD 1.0
        private static Object _cacheLock = new Object();
        public static int MAX_FP_EXPRESSION_CACHE_SIZE = 500;

        private static CompiledExpression getCompiledExpression(string expression)
        {
            lock (_cacheLock)
            {
                bool success = _cache.TryGetValue(expression, out CompiledExpression ce);

                if (!success)
                {
                    var compiler = new FhirPathCompiler();
                    ce = compiler.Compile(expression);

                    if (_cache.Count >= MAX_FP_EXPRESSION_CACHE_SIZE)
                    {
                        var lruExpression = _mruList.First();
                        _cache.Remove(lruExpression);
                        _mruList.Remove(lruExpression);
                    }

                    _cache.Add(expression, ce);
                }

                _mruList.Remove(expression);
                _mruList.Add(expression);

                return ce;
            }
        }


        public static IEnumerable<ITypedElement> Select(this ITypedElement input, string expression, EvaluationContext ctx = null)
        {
            var evaluator = getCompiledExpression(expression);
            return evaluator(input, ctx ?? EvaluationContext.CreateDefault());
        }

        [Obsolete("Use Select(this ITypedElement input) instead")]
        public static IEnumerable<IElementNavigator> Select(this IElementNavigator input, string expression, EvaluationContext ctx = null)
        {
            return Select(input.ToTypedElement(), expression, ctx).Select(t => t.ToElementNavigator());
        }

        public static object Scalar(this ITypedElement input, string expression, EvaluationContext ctx = null)
        {
            var evaluator = getCompiledExpression(expression);
            return evaluator.Scalar(input, ctx ?? EvaluationContext.CreateDefault());
        }

        [Obsolete("Use Scalar(this ITypedElement input) instead")]
        public static object Scalar(this IElementNavigator input, string expression, EvaluationContext ctx = null)
        {
            return Scalar(input.ToTypedElement(), expression, ctx);
        }

        public static bool Predicate(this ITypedElement input, string expression, EvaluationContext ctx = null)
        {
            var evaluator = getCompiledExpression(expression);
            return evaluator.Predicate(input, ctx ?? EvaluationContext.CreateDefault());
        }

        [Obsolete("Use Predicate(this ITypedElement input) instead")]
        public static bool Predicate(this IElementNavigator input, string expression, EvaluationContext ctx = null)
        {
            return Predicate(input.ToTypedElement(), expression, ctx);
        }


        public static bool IsBoolean(this ITypedElement input, string expression, bool value, EvaluationContext ctx = null)
        {
            var evaluator = getCompiledExpression(expression);
            return evaluator.IsBoolean(value, input, ctx ?? EvaluationContext.CreateDefault());
        }

        [Obsolete("Use IsBoolean(this ITypedElement input) instead")]
        public static bool IsBoolean(this IElementNavigator input, string expression, bool value, EvaluationContext ctx = null)
        {
            return IsBoolean(input.ToTypedElement(), expression, value, ctx);
        }
    }
}
