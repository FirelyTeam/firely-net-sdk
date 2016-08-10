/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
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
    public static class PathExpression
    {
       // private static ConcurrentDictionary<string, Evaluator> _cache = new ConcurrentDictionary<string, Evaluator>();

        public static Expression Parse(string expression)
        {         
            var parse = Grammar.Expression.End().TryParse(expression);

            if (parse.WasSuccessful)
            {
                return parse.Value;
            }
            else
            {
               throw new FormatException("Compilation failed: " + parse.ToString());
            }
        }

        public delegate IEnumerable<IValueProvider> CompiledExpression(IEnumerable<IValueProvider> focus, IEnumerable<IValueProvider> containerResource);

        public static CompiledExpression Compile(this Expression expression, SymbolTable scope)
        {
            Invokee inv = expression.ToEvaluator(scope);

            return (IEnumerable<IValueProvider> focus, IEnumerable<IValueProvider> containerResource) =>
                {
                    var closure = Closure.Root(focus, containerResource);
                    return inv(closure, InvokeeFactory.EmptyArgs);
                };
        }


        public static CompiledExpression Compile(string expression, SymbolTable scope)
        {
            return Parse(expression).Compile(scope);
        }


        public static CompiledExpression Compile(string expression)
        {
            var scope = new SymbolTable().AddStandardFP();

            return Compile(expression, scope);
        }

        public static CompiledExpression Compile(this Expression expression)
        {
            var scope = new SymbolTable().AddStandardFP();

            return Compile(expression,scope);
        }


        public static object Scalar(this CompiledExpression evaluator, IEnumerable<IValueProvider> input, IEnumerable<IValueProvider> resource)
        {
            var result = evaluator(input, resource);
            if (result.Any())
                return evaluator(input, resource).Single().Value;
            else
                return null;
        }

        // For predicates, Empty is considered true
        public static bool Predicate(this CompiledExpression evaluator, IEnumerable<IValueProvider> input, IEnumerable<IValueProvider> resource)
        {
            var result = evaluator(input, resource).BooleanEval();

            if (result == null)
                return true;
            else
                return result.Value;
        }

        public static bool IsBoolean(this CompiledExpression evaluator, bool value, IEnumerable<IValueProvider> input, IEnumerable<IValueProvider> resource)
        {
            var result = evaluator(input, resource).BooleanEval();

            if (result == null)
                return false;
            else
                return result.Value == value;
        }

        public static IEnumerable<IValueProvider> Select(string expression, IEnumerable<IValueProvider> input, IEnumerable<IValueProvider> resource=null)
        {
            var evaluator = Compile(expression);
            return evaluator(input, resource);
        }

        public static object Scalar(string expression, IEnumerable<IValueProvider> input, IEnumerable<IValueProvider> resource = null)
        {
            var evaluator = Compile(expression);
            return evaluator.Scalar(input, resource);
        }

        public static bool Predicate(string expression, IEnumerable<IValueProvider> input, IEnumerable<IValueProvider> resource = null)
        {
            var evaluator = Compile(expression);
            return evaluator.Predicate(input, resource);
        }

        public static bool IsBoolean(string expression, bool value, IEnumerable<IValueProvider> input, IEnumerable<IValueProvider> resource = null)
        {
            var evaluator = Compile(expression);
            return evaluator.IsBoolean(value, input, resource);
        }
    }
}
