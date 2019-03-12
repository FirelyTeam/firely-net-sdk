using Hl7.Fhir.ElementModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Hl7.FhirPath.Functions
{
    internal static class UtilityOperators
    {
        private static readonly Action<string> WriteLine = (string s) => Debug.WriteLine(s);

        public static IEnumerable<ITypedElement> Extension(this IEnumerable<ITypedElement> focus, string url)
        {
            return focus.Navigate("extension").Where(es => es.Navigate("url").Single().IsEqualTo(new ConstantValue(url)));
        }

        public static IEnumerable<ITypedElement> Trace(this IEnumerable<ITypedElement> focus, string name, EvaluationContext ctx)
        {
            ctx.Tracer?.Invoke(name, focus);
            return focus;
        }
    }
}
