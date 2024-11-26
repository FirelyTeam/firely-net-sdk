/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.FhirPath;
using Hl7.FhirPath.Expressions;
using System;
using System.Collections.Generic;

namespace Hl7.Fhir.FhirPath
{
    public static class FhirPathExtensions
    {
        private static readonly FhirPathCompiler COMPILER = new(new SymbolTable().AddStandardFP().AddFhirExtensions());
        private static readonly FhirPathCompilerCache CACHE = new(COMPILER);

        /// <summary>
        /// Converts results of a resolver from Resource to IScopedNode
        /// </summary>
        /// <param name="resolver">results of a resolver as Resource</param>
        /// <returns>Result of the convertion to IScopedNode</returns>
        public static Func<string, IScopedNode?> ToFhirPathResolver(this Func<string, SinglePocoElementNode> resolver)
        {
            return navResolver;

            IScopedNode? navResolver(string url)
            {
                return resolver(url);
            }
        }

        /// <summary>
        /// Expose the SymbolTable of the compiler, so we can add extra symbols to it.
        /// </summary>
        /// <returns>The SymbolTable of the internal FP compiler</returns>
        /// <remarks>This function is still internal and not public, because it is not sure the function will remain in the SDK. It is 
        /// now used by 1 unit test FhirPathScaleTest</remarks>
        internal static SymbolTable GetSymbols() => COMPILER.Symbols;

        /// <inheritdoc cref="FhirPathCompilerCache.Select(IScopedNode, string, EvaluationContext?)"/>
        public static IEnumerable<Base?> Select(this Base input, string expression, FhirEvaluationContext? ctx = null)
            => CACHE.Select(input.ToTypedElement().ToScopedNode(), expression, ctx ?? new FhirEvaluationContext()).ToFhirValues();

        /// <inheritdoc cref="FhirPathCompilerCache.Scalar(IScopedNode, string, EvaluationContext?)"/>
        public static object? Scalar(this Base input, string expression, FhirEvaluationContext? ctx = null)
            => CACHE.Scalar(input.ToTypedElement().ToScopedNode(), expression, ctx ?? new FhirEvaluationContext());

        /// <inheritdoc cref="FhirPathCompilerCache.Predicate(IScopedNode, string, EvaluationContext?)"/>
        public static bool Predicate(this Base input, string expression, FhirEvaluationContext? ctx = null)
            => CACHE.Predicate(input.ToTypedElement().ToScopedNode(), expression, ctx ?? new FhirEvaluationContext());

        /// <inheritdoc cref="FhirPathCompilerCache.IsTrue(IScopedNode, string, EvaluationContext?)"/>
        public static bool IsTrue(this Base input, string expression, FhirEvaluationContext? ctx = null)
            => CACHE.IsTrue(input.ToTypedElement().ToScopedNode(), expression, ctx ?? new FhirEvaluationContext());

        /// <inheritdoc cref="FhirPathCompilerCache.IsBoolean(IScopedNode, string, bool, EvaluationContext?) "/>
        public static bool IsBoolean(this Base input, string expression, bool value, FhirEvaluationContext? ctx = null)
            => CACHE.IsBoolean(input.ToTypedElement().ToScopedNode(), expression, value, ctx ?? new FhirEvaluationContext());
    }
}

#nullable restore
