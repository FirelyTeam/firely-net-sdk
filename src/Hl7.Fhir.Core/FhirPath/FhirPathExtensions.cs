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
using Hl7.Fhir.Utility;
using Hl7.FhirPath;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.FhirPath
{
    public static class FhirPathExtensions
    {
        /// <summary>
        /// Converts results of a resolver from Resource to ITypedElement
        /// </summary>
        /// <param name="resolver">results of a resolver as Resource</param>
        /// <returns>Result of the convertion to ITypedElement</returns>
        public static Func<string, ITypedElement?> ToFhirPathResolver(this Func<string, Resource> resolver)
        {
            return navResolver;

            ITypedElement? navResolver(string url)
            {
                var resource = resolver(url);
                return resource?.ToTypedElement();
            }
        }
        /// <summary>
        /// Evaluates an expression against a given context and returns the result(s)
        /// </summary>
        /// <param name="input">Input on which the expression is being evaluated</param>
        /// <param name="expression">Expression which is to be evaluated</param>
        /// <param name="ctx">Context of the evaluation</param>
        /// <returns>The result(s) of the expression</returns>
        public static IEnumerable<Base> Select(this Base input, string expression, FhirEvaluationContext? ctx = null)
        {
            var inputNav = input.ToTypedElement();
            var result = inputNav.Select(expression, ctx ?? FhirEvaluationContext.CreateDefault());
            return result.ToFhirValues();
        }

        /// <summary>
        /// Evaluates an expression against a given context and returns a single result
        /// </summary>
        /// <param name="input">Input on which the expression is being evaluated</param>
        /// <param name="expression">Expression which is to be evaluated</param>
        /// <param name="ctx">Context of the evaluation</param>
        /// <returns>The single result of the expression, and null if the expression returns multiple results</returns>
        public static object? Scalar(this Base input, string expression, FhirEvaluationContext? ctx = null)
        {
            var inputNav = input.ToTypedElement();
            return inputNav.Scalar(expression, ctx ?? FhirEvaluationContext.CreateDefault());
        }

        /// <summary>
        /// Evaluates an expression and returns true for expression being evaluated as true or empty, otherwise false.
        /// </summary>
        /// <param name="input">Input on which the expression is being evaluated</param>
        /// <param name="expression">Expression which is to be evaluated</param>
        /// <param name="ctx">Context of the evaluation</param>
        /// <returns>True if expression returns true of empty, otheriwse false</returns>
        public static bool Predicate(this Base input, string expression, FhirEvaluationContext? ctx = null)
        {
            var inputNav = input.ToTypedElement();
            return inputNav.Predicate(expression, ctx ?? FhirEvaluationContext.CreateDefault());
        }

        /// <summary>
        /// Evaluates an expression and returns true for expression being evaluated as true, and false if the expression returns false or empty.
        /// </summary>
        /// <param name="input">Input on which the expression is being evaluated</param>
        /// <param name="expression">Expression which is to be evaluated</param>
        /// <param name="ctx">Context of the evaluation</param>
        /// <returns>True if expression returns true , and false if expression returns empty of false.</returns>
        public static bool IsTrue(this Base input, string expression, FhirEvaluationContext? ctx = null)
        {
            var inputNav = input.ToTypedElement();
            return inputNav.IsTrue(expression, ctx ?? FhirEvaluationContext.CreateDefault());
        }

        /// <summary>
        ///Evaluates if the result of an expression is equal to a given boolean.
        /// </summary>
        /// <param name="input">Input on which the expression is being evaluated</param>
        /// <param name="value">Boolean that is to be compared to the result of the expression</param>
        /// <param name="expression">Expression which is to be evaluated</param>
        /// <param name="ctx">Context of the evaluation</param>
        /// <returns>True if the result of an expression is equal to a given boolean, otherwise false</returns>
        public static bool IsBoolean(this Base input, string expression, bool value, FhirEvaluationContext? ctx = null)
        {
            var inputNav = input.ToTypedElement();
            return inputNav.IsBoolean(expression, value, ctx ?? FhirEvaluationContext.CreateDefault());
        }
    }
}

#nullable restore
