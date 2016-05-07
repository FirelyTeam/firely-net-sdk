/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.FluentPath.Parser;
using HL7.Fhir.FluentPath.FluentPath;
using HL7.Fhir.FluentPath.FluentPath.Expressions;
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
       // private static ConcurrentDictionary<string, Evaluator> _cache = new ConcurrentDictionary<string, Evaluator>();

        public static Expression Parse(string expression)
        {         
            var parse = Grammar.Expression.End().TryParse(expression);

            if (parse.WasSuccessful)
            {
            //    _cache.TryAdd(cacheName, compilation.Value);
                return parse.Value;
            }
            else
            {
               throw new FormatException("Compilation failed: " + parse.ToString());
            }
        }


        public static Evaluator Compile(string expression)
        {
            //var cacheName = expression.Replace(" ", "");

            //if (_cache.ContainsKey(cacheName))
            //    return _cache[cacheName];  
            
            return Compile(Parse(expression));
        }

        public static Evaluator Compile(this Expression expression)
        {
            return expression.ToEvaluator();
        }

    
        public static IEnumerable<IFluentPathValue> Select(string expression, IFluentPathValue instance)
        {
            var evaluator = Compile(expression);
            return evaluator.Select(instance);
        }

        public static IEnumerable<IFluentPathValue> Select(string expression, IFluentPathValue instance, IEvaluationContext context)
        {
            var evaluator = Compile(expression);
            return evaluator.Select(instance, context);
        }

        public static object Scalar(string expression, IFluentPathValue instance)
        {
            var evaluator = Compile(expression);
            return evaluator.Scalar(instance);
        }

        public static object Scalar(string expression, IFluentPathValue instance, IEvaluationContext context)
        {
            var evaluator = Compile(expression);
            return evaluator.Scalar(instance, context);
        }

        public static bool IsTrue(string expression, IFluentPathValue instance)
        {
            var evaluator = Compile(expression);
            return evaluator.Predicate(instance);
        }

        public static bool IsTrue(string expression, IFluentPathValue instance, IEvaluationContext context)
        {
            var evaluator = Compile(expression);
            return evaluator.Predicate(instance, context);
        }
    }

    public static class PathExpressionLinq
    {
        public static IEnumerable<IFluentPathValue> FluentPathSelect<T>(this IFluentPathValue instance, string expression) 
        {
            return PathExpression.Select(expression, instance);
        }

        public static IEnumerable<IFluentPathValue> FluentPathIsTrue<T>(this IFluentPathValue instance, string expression)
        {
            return PathExpression.Select(expression, instance);
        }
    }
}
