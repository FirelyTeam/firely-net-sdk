/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using System.Collections.Generic;

namespace Hl7.FhirPath
{
    /// <summary>
    /// A combination of a configured <see cref="FhirPathCompiler" /> and a set of cached <see cref="CompiledExpression"/> 
    /// produced by that compiler.
    /// </summary>
    public class FhirPathCompilerCache
    {
        public const int DEFAULT_FP_EXPRESSION_CACHE_SIZE = 500;

        private Cache<string, CompiledExpression> _cache = new();
        private readonly FhirPathCompiler _compiler;
        private readonly int _cacheSize = DEFAULT_FP_EXPRESSION_CACHE_SIZE;

        public FhirPathCompilerCache(FhirPathCompiler? compiler = null, int cacheSize = DEFAULT_FP_EXPRESSION_CACHE_SIZE)
        {
            _cacheSize = cacheSize;
            _compiler = compiler ?? new FhirPathCompiler(FhirPathCompiler.DefaultSymbolTable);
            Clear();
        }

        /// <summary>
        /// Clears the cache.
        /// </summary>
        public void Clear()
        {
            _cache = new(expr => compile(expr),
                new CacheSettings() { MaxCacheSize = _cacheSize });

            CompiledExpression compile(string expression) => _compiler.Compile(expression);
        }

        /// <summary>
        /// Returns a <see cref="CompiledExpression"/> for the given FhirPath expression.
        /// </summary>
        /// <remarks>The expression will be retrieved from the cache if available, otherwise
        /// it will be added to the cache.</remarks>
        public CompiledExpression GetCompiledExpression(string expression) => _cache!.GetValue(expression);

        /// <summary>
        /// Evaluates an expression against a given context and returns the result(s)
        /// </summary>
        /// <param name="input">Input on which the expression is being evaluated</param>
        /// <param name="expression">Expression which is to be evaluated</param>
        /// <param name="ctx">Context of the evaluation</param>
        /// <returns>The result(s) of the expression</returns>
        public IEnumerable<ITypedElement> Select(ITypedElement input, string expression, EvaluationContext? ctx = null)
        {
            input = input.ToScopedNode();
            var evaluator = GetCompiledExpression(expression);
            return evaluator(input, ctx ?? EvaluationContext.CreateDefault());
        }

        /// <summary>
        /// Evaluates an expression against a given context and returns a single result
        /// </summary>
        /// <param name="input">Input on which the expression is being evaluated</param>
        /// <param name="expression">Expression which is to be evaluated</param>
        /// <param name="ctx">Context of the evaluation</param>
        /// <returns>The single result of the expression, and null if the expression returns multiple results</returns>
        public object? Scalar(ITypedElement input, string expression, EvaluationContext? ctx = null)
        {
            input = input.ToScopedNode();
            var evaluator = GetCompiledExpression(expression);
            return evaluator.Scalar(input, ctx ?? EvaluationContext.CreateDefault());
        }

        /// <summary>
        /// Evaluates an expression and returns true for expression being evaluated as true or empty, otherwise false.
        /// </summary>
        /// <param name="input">Input on which the expression is being evaluated</param>
        /// <param name="expression">Expression which is to be evaluated</param>
        /// <param name="ctx">Context of the evaluation</param>
        /// <returns>True if expression returns true of empty, otheriwse false</returns>
        public bool Predicate(ITypedElement input, string expression, EvaluationContext? ctx = null)
        {
            input = input.ToScopedNode();
            var evaluator = GetCompiledExpression(expression);
            return evaluator.Predicate(input, ctx ?? EvaluationContext.CreateDefault());
        }

        /// <summary>
        /// Evaluates an expression and returns true for expression being evaluated as true, and false if the expression returns false or empty.
        /// </summary>
        /// <param name="input">Input on which the expression is being evaluated</param>
        /// <param name="expression">Expression which is to be evaluated</param>
        /// <param name="ctx">Context of the evaluation</param>
        /// <returns>True if expression returns true , and false if expression returns empty of false.</returns>
        public bool IsTrue(ITypedElement input, string expression, EvaluationContext? ctx = null)
        {
            input = input.ToScopedNode();
            var evaluator = GetCompiledExpression(expression);
            return evaluator.IsTrue(input, ctx ?? EvaluationContext.CreateDefault());
        }


        /// <summary>
        ///Evaluates if the result of an expression is equal to a given boolean.
        /// </summary>
        /// <param name="input">Input on which the expression is being evaluated</param>
        /// <param name="value">Boolean that is to be compared to the result of the expression</param>
        /// <param name="expression">Expression which is to be evaluated</param>
        /// <param name="ctx">Context of the evaluation</param>
        /// <returns>True if the result of an expression is equal to a given boolean, otherwise false</returns>
        public bool IsBoolean(ITypedElement input, string expression, bool value, EvaluationContext? ctx = null)
        {
            input = input.ToScopedNode();

            var evaluator = GetCompiledExpression(expression);
            return evaluator.IsBoolean(value, input, ctx ?? EvaluationContext.CreateDefault());
        }

    }
}

#nullable restore