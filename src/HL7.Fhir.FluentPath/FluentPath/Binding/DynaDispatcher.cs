using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.FluentPath.Binding
{
    internal class DynaDispatcher
    {
        class DynaEntry
        {
            public IEnumerable<Type> ArgTypes;
            public Delegate Invokee;
        }

        private List<DynaEntry> _candidates = new List<DynaEntry>();

        public DynaDispatcher Add<F, R>(Func<F, R> f)
        {
            return Add((Delegate)f);
        }

        public DynaDispatcher Add<F, A, R>(Func<F, A, R> f)
        {
            return Add((Delegate)f);
        }

        public DynaDispatcher Add<F, A, B, R>(Func<F, A, B, R> f)
        {
            return Add((Delegate)f);
        }

        private DynaDispatcher Add(Delegate f)
        {
            var entry = new DynaEntry();

            entry.ArgTypes = f.Method.GetParameters().Select(p => p.ParameterType);
            entry.Invokee = f;
            _candidates.Add(entry);

            return this;
        }

        private DynaDispatcher Add(IEnumerable<Delegate> f)
        {
            foreach (var candidate in f)
                Add(candidate);

            return this;
        }

        public IEnumerable<IValueProvider> Invoke(IEvaluationContext context, IEnumerable<IValueProvider> focus, IEnumerable<Evaluator> args)
        {
            List<object> actualArgs = new List<object>();

            if (!focus.Any()) return FhirValueList.Empty;
            actualArgs.Add(Typecasts.Unbox(focus));

            foreach (var arg in args)
            {
                var argValue = arg(context);
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
                        try
                        {
                            return Typecasts.CastTo<IEnumerable<IValueProvider>>(entry.Invokee.Method.Invoke(entry.Invokee.Target, actualArgs.Zip(casts, (a, c) => c(a)).ToArray()));
                        }
                        catch(TargetInvocationException tie)
                        {
                            // Unrwarp the very non-informative T.I.E, and throw the nested exception instead
                            throw tie.InnerException;
                        }
                    }
                }
            }

            //TODO: Make error reporting better
            throw Error.Argument(noMatchError(actualArgs));
        }


        private string noMatchError(IEnumerable<object> arguments)
        {
            string result;

            if (!arguments.Any())
                return "(no signature)";

            result = "on focus of type '{0}'".FormatWith(readableName(arguments.First().GetType()));
            
            if(arguments.Skip(1).Any())
            {
                result = "with parameters of type '{0}' on {1}"
                        .FormatWith(String.Join(",", arguments.Skip(1).Select(a => readableName(a.GetType()))), result);
            }

            return "Function cannot be called " + result;
        }

        private string readableName(Type t)
        {
            if (typeof(IEnumerable<IValueProvider>).IsAssignableFrom(t))
                return "collection";
            else if (typeof(IValueProvider).IsAssignableFrom(t))
                return "object";
            else
                return t.Name;
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
