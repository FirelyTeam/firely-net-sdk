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
using Hl7.FhirPath.Sprache;

namespace Hl7.FhirPath
{
    public static class IValueProviderFPExtensions
    {
        private static Dictionary<string, CompiledExpression> _cache = new Dictionary<string, CompiledExpression>();
        private static List<string> _mruList = new List<string>();      // sigh, no sortedlist in NETSTANDARD 1.0
        private static readonly Object _cacheLock = new Object();
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
        
        public static object Scalar(this ITypedElement input, string expression, EvaluationContext ctx = null)
        {
            var evaluator = getCompiledExpression(expression);
            return evaluator.Scalar(input, ctx ?? EvaluationContext.CreateDefault());
        }
        
        public static bool Predicate(this ITypedElement input, string expression, EvaluationContext ctx = null)
        {
            var evaluator = getCompiledExpression(expression);
            return evaluator.Predicate(input, ctx ?? EvaluationContext.CreateDefault());
        }
        
        public static bool IsBoolean(this ITypedElement input, string expression, bool value, EvaluationContext ctx = null)
        {
            var evaluator = getCompiledExpression(expression);
            return evaluator.IsBoolean(value, input, ctx ?? EvaluationContext.CreateDefault());
        }

        #region Obsolete members
        [Obsolete("Use Select(this ITypedElement input) instead. Obsolete since 2018-10-17")]
        public static IEnumerable<IElementNavigator> Select(this IElementNavigator input, string expression, EvaluationContext ctx = null)
        {
            return Select(input.ToTypedElement(), expression, ctx).Select(t => t.ToElementNavigator());
        }

        [Obsolete("Replace with the overload taking an EvaluationContext, initialized with the resource paramete. Obsolete since 2018-10-17r")]
        public static IEnumerable<IElementNavigator> Select(this IElementNavigator input, string expression, IElementNavigator resource)
        {
            return Select(input.ToTypedElement(), expression, new EvaluationContext(resource)).Select(t => t.ToElementNavigator());
        }

        [Obsolete("Use Scalar(this ITypedElement input) instead. Obsolete since 2018-10-17")]
        public static object Scalar(this IElementNavigator input, string expression, EvaluationContext ctx = null)
        {
            return Scalar(input.ToTypedElement(), expression, ctx);
        }

        [Obsolete("Replace with the overload taking an EvaluationContext, initialized with the resource parameter. Obsolete since 2018-10-17")]
        public static object Scalar(this IElementNavigator input, string expression, IElementNavigator resource)
        {
            return Scalar(input.ToTypedElement(), expression, new EvaluationContext(resource));
        }

        [Obsolete("Use Predicate(this ITypedElement input) instead. Obsolete since 2018-10-17")]
        public static bool Predicate(this IElementNavigator input, string expression, EvaluationContext ctx = null)
        {
            return Predicate(input.ToTypedElement(), expression, ctx);
        }

        [Obsolete("Replace with the overload taking an EvaluationContext, initialized with the resource parameter. Obsolete since 2018-10-17")]
        public static bool Predicate(this IElementNavigator input, string expression, IElementNavigator resource)
        {
            return Predicate(input.ToTypedElement(), expression, new EvaluationContext(resource));
        }

        [Obsolete("Use IsBoolean(this ITypedElement input) instead. Obsolete since 2018-10-17")]
        public static bool IsBoolean(this IElementNavigator input, string expression, bool value, EvaluationContext ctx = null)
        {
            return IsBoolean(input.ToTypedElement(), expression, value, ctx);
        }

        [Obsolete("Replace with the overload taking an EvaluationContext, initialized with the resource parameter. Obsolete since 2018-10-17")]
        public static bool IsBoolean(this IElementNavigator input, string expression, bool value, IElementNavigator resource)
        {
            return IsBoolean(input.ToTypedElement(), expression, value, new EvaluationContext(resource));
        }
        #endregion
    }
}