using System;
using System.Linq;
using System.Collections.Generic;
using Hl7.Fhir.Support;
using HL7.Fhir.FluentPath;
using HL7.Fhir.FluentPath.FluentPath;
using HL7.Fhir.FluentPath.FluentPath.Expressions;
using Sprache;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.FluentPath.Binding
{
    public delegate IEnumerable<IValueProvider> Invokee(IEvaluationContext context, IEnumerable<Evaluator> arguments);

    //TODO: Add compositional functions here

    public static class InvokeeExtensions
    {
        public static Evaluator CastToValueProviders(this Func<object> input)
        {
            return ctx =>
            {
                var result = input();

                if (result == null) return FhirValueList.Empty();

                // TODO: Object may be a constant native value....

                if (result is IEnumerable<IValueProvider>)
                    return (IEnumerable<IValueProvider>)result;
                else if (result is IValueProvider)
                    return FhirValueList.Create((IValueProvider)result);
                else
                    throw new InvalidOperationException("Bound functions should either return IValueProvider or IEnumerable<IValueProvider>");
            };
        }

    }
}