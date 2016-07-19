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
        public DynaDispatcher(string name)
        {
            _name = name;
        }

        private List<CallBinding> _candidates = new List<CallBinding>();
        private string _name;

        public DynaDispatcher Add<A, R>(Func<A, R> f)
        {
            _candidates.Add(CallBinding.Create(_name, f));
            return this;
        }

        public DynaDispatcher Add<F, A, R>(Func<F, A, R> f)
        {
            _candidates.Add(CallBinding.Create(_name, f));
            return this;
        }

        public DynaDispatcher Add<F, A, B, R>(Func<F, A, B, R> f)
        {
            _candidates.Add(CallBinding.Create(_name, f));
            return this;
        }

        public IEnumerable<IValueProvider> Invoke(IEvaluationContext context, IEnumerable<Evaluator> args)
        {
            List<object> actualArgs = new List<object>();
            var focus = context.GetThis();

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
                if (entry.ArgumentTypes.Count() == actualArgs.Count())
                {
                    var casts = actualArgs.Zip(entry.ArgumentTypes, (aa, ea) => Typecasts.GetImplicitCast(aa.GetType(), ea));
                    if(casts.All(c => c != null))
                    {
                        try
                        {
                            return entry.Function(context, args);
                        }
                        catch(TargetInvocationException tie)
                        {
                            // Unwrap the very non-informative T.I.E, and throw the nested exception instead
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
    }
}
