/* 
 * Copyright (c) 2016, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.FluentPath.Parser;
using Hl7.FluentPath;
using Hl7.FluentPath.Expressions;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.ElementModel;
using Hl7.FluentPath.Functions;

namespace Hl7.FluentPath
{
    public static class IValueProviderFPExtensions
    {
        private static Dictionary<string, CompiledExpression> _cache = new Dictionary<string, CompiledExpression>();
        private static List<string> _mruList = new List<string>();      // sigh, no sortedlist in NETSTANDARD 1.0
        private static Object _cacheLock = new Object();
        public static int MAX_FP_EXPRESSION_CACHE_SIZE = 500;

        private static CompiledExpression getCompiledExpression(string expression)
        {
            lock(_cacheLock)
            {
                CompiledExpression ce = null;
                bool success = _cache.TryGetValue(expression, out ce);

                if(!success)
                {
                    var compiler = new FluentPathCompiler();
                    ce = compiler.Compile(expression);

                    if(_cache.Count >= MAX_FP_EXPRESSION_CACHE_SIZE)
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


        public static IEnumerable<IValueProvider> Select(this IValueProvider input, string expression, IValueProvider resource=null)
        {
            var evaluator = getCompiledExpression(expression);
            return evaluator(input, resource);
        }

        public static object Scalar(this IValueProvider input, string expression, IValueProvider resource = null)
        {
            var evaluator = getCompiledExpression(expression);
            return evaluator.Scalar(input, resource);
        }

        public static bool Predicate(this IValueProvider input, string expression, IValueProvider resource = null)
        {
            var evaluator = getCompiledExpression(expression);
            return evaluator.Predicate(input, resource);
        }

        public static bool IsBoolean(this IValueProvider input, string expression, bool value, IValueProvider resource = null)
        {
            var evaluator = getCompiledExpression(expression);
            return evaluator.IsBoolean(value, input, resource);
        }
    }
}
