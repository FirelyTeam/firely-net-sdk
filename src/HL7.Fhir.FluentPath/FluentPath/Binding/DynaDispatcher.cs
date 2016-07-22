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

        public DynaDispatcher(string name, IEnumerable<CallBinding> bindings)
        {
            _name = name;
            _candidates.AddRange(bindings);
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

        public Invokee MakeDispatcher()
        {
            Invokee v = invokeNested;
            return v.NullProp();
        }

        private IEnumerable<IValueProvider> invokeNested(IEvaluationContext context, IEnumerable<Invokee> args)
        {
            List<object> actualArgs = new List<object>();
            actualArgs.Add(context.GetThis());
            actualArgs.AddRange(args.Select(a => a(context, InvokeeFactory.EmptyArgs)));

            foreach(var entry in _candidates)
            {
                if (entry.DynamicMatches(_name,actualArgs))
                {
                    try
                    {
                        return entry.Function(context, args);
                    }
                    catch (TargetInvocationException tie)
                    {
                        // Unwrap the very non-informative T.I.E, and throw the nested exception instead
                        throw tie.InnerException;
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

            result = "on focus of type '{0}'".FormatWith(Typecasts.ReadableFluentPathName(arguments.First().GetType()));
            
            if(arguments.Skip(1).Any())
            {
                result = "with parameters of type '{0}' "
                        .FormatWith(String.Join(",", arguments.Skip(1).Select(a => Typecasts.ReadableFluentPathName(a.GetType()))), result);
            }

            return "Function cannot be called " + result;
        }     
    }
}
