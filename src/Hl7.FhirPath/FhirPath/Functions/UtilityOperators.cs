using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;

namespace Hl7.FhirPath.Functions
{
    internal static class UtilityOperators
    {
#if WAY_TOO_MUCH_OUTPUT
        static Action<string> WriteLine = (string s) => Debug.WriteLine(s);
#else
        static Action<string> WriteLine = (string s) => { };
#endif

        public static IEnumerable<ITypedElement> Extension(this IEnumerable<ITypedElement> focus, string url)
        {
            return focus.Navigate("extension").Where(es => es.Navigate("url").Single().IsEqualTo(new ConstantValue(url)));
        }

        public static IEnumerable<ITypedElement> Trace(this IEnumerable<ITypedElement> focus, string name, EvaluationContext ctx)
        {
            if (ctx.Tracer != null)
                ctx.Tracer(name, focus);
            return focus;
        }
    }
}
