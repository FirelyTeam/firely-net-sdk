using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Hl7.FhirPath.Functions
{
    internal static class UtilityOperators
    {
        public static IEnumerable<ITypedElement> Extension(this IEnumerable<PocoNode> focus, string url)
        {
            return focus.Navigate("extension")
                .Where(es => es.Child<PrimitiveNode>("url")?.Value as string == url);
        }

        public static IEnumerable<ITypedElement> Trace(this IEnumerable<PocoNode> focus, string name, EvaluationContext ctx)
        {
            ctx.Tracer?.Invoke(name, focus);
            return focus;
        }
    }
}
