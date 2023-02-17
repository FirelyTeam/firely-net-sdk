using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
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

        public static SymbolTable AddFhirExtensions(this SymbolTable t)
        {
            t.Add("hasValue", (ITypedElement f) => f.HasValue(), doNullProp: false);
            t.Add("resolve", (ITypedElement f, EvaluationContext ctx) => resolver(f, ctx), doNullProp: false);

            t.Add("memberOf", (Func<object, string, bool>)memberOf, doNullProp: false);

            // Pre-normative this function was called htmlchecks, normative is htmlChecks
            // lets keep both to keep everyone happy.
            t.Add("htmlchecks", (ITypedElement f) => f.HtmlChecks(), doNullProp: false);
            t.Add("htmlChecks", (ITypedElement f) => f.HtmlChecks(), doNullProp: false);

            return t;

            static ITypedElement resolver(ITypedElement f, EvaluationContext ctx)
            {
                return ctx is FhirEvaluationContext fctx ? f.Resolve(fctx.ElementResolver) : f.Resolve();
            }

            static bool memberOf(object focus, string valueset) => throw new NotImplementedException("Terminology functions in FhirPath are unsupported in the .NET FhirPath engine.");
        }

        /// <summary>
        /// Check if the node has a value, and not just extensions.
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static bool HasValue(this ITypedElement focus) => focus?.Value is not null;

        /// <summary>
        /// Check if the node has a value, and not just extensions.
        /// </summary>
        /// <param name="focus"></param>
        /// <returns></returns>
        public static bool HtmlChecks(this ITypedElement focus)
        {
            if (focus?.Value is null) return false;

            // Perform the checking of the content for valid html content
            return XHtml.IsValidNarrativeXhtml(focus.Value.ToString());
        }

        public static IEnumerable<Base> ToFhirValues(this IEnumerable<ITypedElement> results)
        {
            return results.Select(r =>
            {
                if (r is null)
                    return null;

                var fhirValue = r.Annotation<IFhirValueProvider>();
                if (fhirValue != null)
                {
                    return fhirValue.FhirValue;
                }

                object result = r.Value;

                return result switch
                {
                    bool b => new FhirBoolean(b),
                    long l => new Integer64(l),
                    int i => new Integer(i),
                    decimal dec => new FhirDecimal(dec),
                    string s => new FhirString(s),
                    P.Date d => new Date(d.ToString()),
                    P.Time t => new Time(t.ToString()),
                    P.DateTime dt => new FhirDateTime(dt.ToDateTimeOffset(TimeSpan.Zero).ToUniversalTime()),
                    _ => (Base)result
                };
            });
        }
    }
}