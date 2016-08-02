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
        public DynaDispatcher(string name, SymbolTable scope)
        {
            _scope = scope;
            _name = name;
        }


        private string _name;
        private SymbolTable _scope;

        public Invokee MakeDispatcher()
        {
            Invokee v = invokeNested;
            return v.NullProp();
        }

        private IEnumerable<IValueProvider> invokeNested(IEvaluationContext context, IEnumerable<Invokee> args)
        {
            List<object> actualArgs = new List<object>();
            actualArgs.AddRange(args.Select(a => a(context, InvokeeFactory.EmptyArgs)));

            var entry = _scope.DynamicGet(_name, actualArgs);

            if (entry != null)
            {
                try
                {
                    // The Get() here should never fail, since we already know there's a (dynamic) matching candidate
                    // Need to clean up this duplicate logic later
                    return entry(context, args);
                }
                catch (TargetInvocationException tie)
                {
                    // Unwrap the very non-informative T.I.E, and throw the nested exception instead
                    throw tie.InnerException;
                }
            }
            else
            {
                //TODO: Make error reporting better
                throw Error.Argument(noMatchError(actualArgs));
            }
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
