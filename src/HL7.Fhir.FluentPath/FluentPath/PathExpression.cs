/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.FluentPath.Parser;
using Hl7.Fhir.FluentPath;
using Hl7.Fhir.FluentPath.Expressions;
using Sprache;
using System;
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

    
        public static IEnumerable<IValueProvider> Select(string expression, IEnumerable<IValueProvider> input)
        {
            var evaluator = Compile(expression);
            return evaluator.Select(input);
        }

        public static IEnumerable<IValueProvider> Select(string expression, IEnumerable<IValueProvider> input, IEvaluationContext context)
        {
            var evaluator = Compile(expression);
            return evaluator.Select(input, context);
        }

        public static object Scalar(string expression, IEnumerable<IValueProvider> input)
        {
            var evaluator = Compile(expression);
            return evaluator.Scalar(input);
        }

        public static object Scalar(string expression, IEnumerable<IValueProvider> input, IEvaluationContext context)
        {
            var evaluator = Compile(expression);
            return evaluator.Scalar(input, context);
        }

        public static bool IsTrue(string expression, IEnumerable<IValueProvider> input)
        {
            var evaluator = Compile(expression);
            return evaluator.Predicate(input);
        }

        public static bool IsTrue(string expression, IEnumerable<IValueProvider> input, IEvaluationContext context)
        {
            var evaluator = Compile(expression);
            return evaluator.Predicate(input, context);
        }
    }

    //public static class PathExpressionLinq
    //{
    //    public static IEnumerable<IValueProvider> FluentPathSelect<T>(this IValueProvider instance, string expression) 
    //    {
    //        return PathExpression.Select(expression, FhirValueList.Create(instance));
    //    }

    //    public static IEnumerable<IValueProvider> FluentPathSelect<T>(this IEnumerable<IValueProvider> input, string expression)
    //    {
    //        return PathExpression.Select(expression, input);
    //    }


    //    public static IEnumerable<IValueProvider> FluentPathIsTrue<T>(this IValueProvider instance, string expression)
    //    {
    //        return PathExpression.Select(expression, instance);
    //    }
    //}
}
