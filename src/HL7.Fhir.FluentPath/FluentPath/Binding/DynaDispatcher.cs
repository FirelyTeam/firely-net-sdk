using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.FluentPath.Binding
{
    internal class DynaBinder
    {
        class DynaEntry
        {
            public IEnumerable<Type> ArgTypes;
            public Delegate Invokee;
        }

        private List<DynaEntry> _candidates = new List<DynaEntry>();

        public DynaBinder AddCandidate(Delegate f)
        {
            var entry = new DynaEntry();

            entry.ArgTypes = f.Method.GetParameters().Select(p => p.ParameterType);
            entry.Invokee = f;
            _candidates.Add(entry);

            return this;
        }

        public DynaBinder AddCandidate<F,A,B,R>(Func<F,A,B,R> f)
        {
            return AddCandidate((Delegate)f);
        }


        public IEnumerable<IValueProvider> Invoke(IEvaluationContext context, IEnumerable<IValueProvider> focus, IEnumerable<Evaluator> args)
        {
            List<object> actualArgs = new List<object>();

            if (!focus.Any()) return FhirValueList.Empty;
            actualArgs.Add(Typecasts.Unbox(focus));

            foreach (var arg in args)
            {
                var argValue = arg(context, focus);
                if (!argValue.Any()) return FhirValueList.Empty;
                actualArgs.Add(Typecasts.Unbox(argValue));
            }

            foreach(var entry in _candidates)
            {
                if (entry.ArgTypes.Count() == actualArgs.Count())
                {
                    var casts = actualArgs.Zip(entry.ArgTypes, (aa, ea) => Typecasts.GetImplicitCast(aa.GetType(), ea));
                    if(casts.All(c => c != null))
                    {
                        return Typecasts.CastTo<IEnumerable<IValueProvider>>(entry.Invokee.Method.Invoke(entry.Invokee.Target, actualArgs.Zip(casts, (a, c) => c(a)).ToArray()));
                    }
                }
            }

            //TODO: Make error reporting better
            throw Error.Argument("Cannot find overload for call");
        }

        private static IEnumerable<IValueProvider> PropEmpty(IEnumerable<IValueProvider> source, Func<IEnumerable<IValueProvider>, IEnumerable<IValueProvider>> f)
        {
            if (source.Any())
                return f(source);
            else
                return FhirValueList.Empty;
        }
    }
}
