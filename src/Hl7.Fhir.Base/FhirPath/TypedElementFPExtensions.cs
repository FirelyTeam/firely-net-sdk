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
using System;
using System.Collections.Generic;

namespace Hl7.FhirPath
{

    public static class IValueProviderFPExtensions
    {
        public static int MAX_FP_EXPRESSION_CACHE_SIZE = FhirPathCompilerCache.DEFAULT_FP_EXPRESSION_CACHE_SIZE;

        private static Lazy<FhirPathCompilerCache> CACHE = new(() => new(compiler: null, cacheSize: MAX_FP_EXPRESSION_CACHE_SIZE));

        /// <inheritdoc cref="FhirPathCompilerCache.Select(PocoNode, string, EvaluationContext?)"/>
        public static IEnumerable<ITypedElement> Select(this ITypedElement input, string expression, EvaluationContext? ctx = null)
            => CACHE.Value.Select(input.ToPocoNode(rootName: input.Location), expression, ctx);

        /// <inheritdoc cref="FhirPathCompilerCache.Scalar(PocoNode, string, EvaluationContext?)"/>
        public static object? Scalar(this ITypedElement input, string expression, EvaluationContext? ctx = null)
            => CACHE.Value.Scalar(input.ToPocoNode(rootName: input.Location), expression, ctx);

        /// <inheritdoc cref="FhirPathCompilerCache.Predicate(PocoNode, string, EvaluationContext?)"/>
        public static bool Predicate(this ITypedElement input, string expression, EvaluationContext? ctx = null)
            => CACHE.Value.Predicate(input.ToPocoNode(rootName: input.Location), expression, ctx);

        /// <inheritdoc cref="FhirPathCompilerCache.IsTrue(PocoNode, string, EvaluationContext?)"/>
        public static bool IsTrue(this ITypedElement input, string expression, EvaluationContext? ctx = null)
            => CACHE.Value.IsTrue(input.ToPocoNode(rootName: input.Location), expression, ctx);

        /// <inheritdoc cref="FhirPathCompilerCache.IsBoolean(PocoNode, string, bool, EvaluationContext?)"/>
        public static bool IsBoolean(this ITypedElement input, string expression, bool value, EvaluationContext? ctx = null)
            => CACHE.Value.IsBoolean(input.ToPocoNode(rootName: input.Location), expression, value, ctx);

        /// <summary>
        /// Reinitialize the cache. This method is only meant for the unit tests, but can be made public later. We need some refactoring here, I (MV) think.
        /// </summary>
        /// <param name="compiler">A userdefined compiler</param>
        /// <param name="cacheSize">the new size for the cache</param>
        internal static void ReInitializeCache(FhirPathCompiler? compiler = null, int? cacheSize = null)
        {
            CACHE = new(() => new(compiler, cacheSize ?? MAX_FP_EXPRESSION_CACHE_SIZE));
        }
    }
}

#nullable restore