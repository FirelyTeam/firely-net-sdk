/* 
 * Copyright (c) 2016, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Hl7.FhirPath;
using Hl7.FhirPath.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.Fhir.FhirPath
{
    public static class ElementNavFhirExtensions
    {
        internal static bool _fhirSymbolTableExtensionsAdded = false;
        public static void PrepareFhirSymbolTableFunctions()
        {
            if (!_fhirSymbolTableExtensionsAdded)
            {
                _fhirSymbolTableExtensionsAdded = true;
                Hl7.FhirPath.FhirPathCompiler.DefaultSymbolTable.AddFhirExtensions();
            }
        }

        public static Func<string, ITypedElement> ToFhirPathResolver(this Func<string, Resource> resolver)
        {
            return navResolver;

            ITypedElement navResolver(string url)
            {
                var resource = resolver(url);
                return resource?.ToTypedElement();
            }
        }

        public static SymbolTable AddFhirExtensions(this SymbolTable t)
        {
            t.Add("hasValue", (ITypedElement f) => f.HasValue(), doNullProp: false);
            t.Add("resolve", (ITypedElement f, EvaluationContext ctx) => resolver(f, ctx), doNullProp: false);

            // Pre-normative this function was called htmlchecks, normative is htmlChecks
            // lets keep both to keep everyone happy.
            t.Add("htmlchecks", (ITypedElement f) => f.HtmlChecks(), doNullProp: false);
            t.Add("htmlChecks", (ITypedElement f) => f.HtmlChecks(), doNullProp: false);

            return t;

            static ITypedElement resolver(ITypedElement f, EvaluationContext ctx)
            {
                return ctx is FhirEvaluationContext fctx ? f.Resolve(fctx.ElementResolver) : f.Resolve();
            }
        }

        /// <summary>
        /// Check if the node has a value, and not just extensions.
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static bool HasValue(this ITypedElement focus)
        {
            if (focus == null)
                return false;
            if (focus.Value == null)
                return false;
            return true;
        }

        /// <summary>
        /// Check if the node has a value, and not just extensions.
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static bool HtmlChecks(this ITypedElement focus)
        {
            if (focus == null)
                return false;
            if (focus.Value == null)
                return false;
            // Perform the checking of the content for valid html content
            _ = focus.Value.ToString();
            // TODO: Perform the checking
            return true;
        }


        public static IEnumerable<Base> ToFhirValues(this IEnumerable<ITypedElement> results)
        {
            return results.Select(r =>
            {
                if (r == null)
                    return null;

                var fhirValue = r.Annotation<IFhirValueProvider>();
                if (fhirValue != null)
                {
                    return fhirValue.FhirValue;
                }

                object result = r.Value;

                return result switch
                {
                    bool _ => new FhirBoolean((bool)result),
                    long _ => new Integer((int)(long)result),
                    decimal _ => new FhirDecimal((decimal)result),
                    string _ => new FhirString((string)result),
                    P.Date d => new Date(d.ToString()),
                    P.Time t => new Time(t.ToString()),
                    P.DateTime dt => new FhirDateTime(dt.ToDateTimeOffset(TimeSpan.Zero).ToUniversalTime()),
                    _ => (Base)result
                };
            });
        }

        public static IEnumerable<Base> Select(this Base input, string expression, FhirEvaluationContext ctx = null)
        {
            var inputNav = input.ToTypedElement();
            var result = inputNav.Select(expression, ctx ?? FhirEvaluationContext.CreateDefault());
            return result.ToFhirValues();
        }

        public static object Scalar(this Base input, string expression, FhirEvaluationContext ctx = null)
        {
            var inputNav = input.ToTypedElement();
            return inputNav.Scalar(expression, ctx ?? FhirEvaluationContext.CreateDefault());
        }

        public static bool Predicate(this Base input, string expression, FhirEvaluationContext ctx = null)
        {
            var inputNav = input.ToTypedElement();
            return inputNav.Predicate(expression, ctx ?? FhirEvaluationContext.CreateDefault());
        }

        public static bool IsBoolean(this Base input, string expression, bool value, FhirEvaluationContext ctx = null)
        {
            var inputNav = input.ToTypedElement();
            return inputNav.IsBoolean(expression, value, ctx ?? FhirEvaluationContext.CreateDefault());
        }
    }
}
