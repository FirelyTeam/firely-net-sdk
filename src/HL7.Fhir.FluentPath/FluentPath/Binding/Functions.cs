using Hl7.Fhir.FluentPath;
using Hl7.Fhir.FluentPath.Binding;
using HL7.Fhir.FluentPath.FluentPath.Expressions;
using System;
using System.Linq;
using System.Collections.Generic;
using Hl7.Fhir.Support;

namespace HL7.Fhir.FluentPath.FluentPath.Binding
{

    public class Functions
    {
        public delegate object Builtin(IEnumerable<IValueProvider> focus);
        public delegate object Builtin<A>(IEnumerable<IValueProvider> focus, A argument1);
        public delegate object Builtin<A, B>(IEnumerable<IValueProvider> focus, A argument1, B argument2);

        private static ParamBinding<T> par<T>(string name, bool optional = false)
        {
            return new ParamBinding<T>(name, optional);
        }

        private class ParamBinding<T> : ParamBinding
        {
            public ParamBinding(string name, bool optional = false) : base(name, optional, typeof(T))
            {
            }
        }


        static Functions()
        {
            add("not", f => f.Not());
            add("empty", f => f.IsEmpty());
            add("exists", f => f.Exists());
            add("builtin.children", par<string>("name"), (f, a) => f.Children(a));
            //add("count", (f, _) => f.CountItems(), None());
            ////add("=", Exactly(1) && Args(TypeInfo.Decimal, TypeInfo.Decimal), (f, a) => Operators.IsEqualTo(a[0]), Exactly(1));
            //add("+", (f, a) => f.Add(a[0]), Exactly(1));
        }


        public static Invokee Resolve(FunctionCallExpression expression)
        {
            CallBinding binding = null;
            var isKnown = _functions.TryGetValue(expression.FunctionName, out binding);

            if (isKnown)
            {
                binding.Verify(expression);
                return binding.Function;
            }
            else
                return buildExternalCall(expression.FunctionName);
        }


        private static IDictionary<string,CallBinding> _functions = new Dictionary<string,CallBinding>();

        private static void add(string name, Builtin func)
        {
            _functions.Add(name,new CallBinding(name, buildInternalCall(func), new ParamBinding[] { }));
        }

        private static void add<A>(string name, ParamBinding<A> param1, Builtin<A> func)
        {
            _functions.Add(name, new CallBinding(name, buildInternalCall(func, param1), param1));
        }

        private static void add<A, B>(string name, ParamBinding<A> param1, ParamBinding<B> param2, Builtin<A, B> func)
        {
            _functions.Add(name, new CallBinding(name, buildInternalCall(func, param1, param2), param1, param2));
        }

        private static Invokee buildExternalCall(string name)
        {
            return (ctx, args) =>
            {
                var evaluatedArguments = args.Select(a => a(ctx));
                return ctx.InvokeExternalFunction(name, ctx.CurrentFocus, evaluatedArguments);
            };            
        }

        private static Invokee buildInternalCall(Builtin b)
        {
            return (ctx, args) =>
            {
                return castResult(b(ctx.CurrentFocus));
            };
        }

        private static Invokee buildInternalCall<A>(Builtin<A> b, ParamBinding<A> binding1)
        {
            return (ctx, args) =>
            {
                var argValue = args.Single()(ctx);       // early bound argument, evaluate now. Not good for functions that have lambdas as parameters

                return castResult(b(ctx.CurrentFocus, binding1.Bind<A>(argValue)));
            };
        }

        private static Invokee buildInternalCall<A,B>(Builtin<A,B> b, ParamBinding<A> binding1, ParamBinding<B> binding2)
        {
            return (ctx, args) =>
            {
                var arg1Value = args.First()(ctx);       // early bound argument, evaluate now. Not good for functions that have lambdas as parameters
                var arg2Value = args.Skip(1).First()(ctx);       // early bound argument, evaluate now. Not good for functions that have lambdas as parameters

                return castResult(b(ctx.CurrentFocus, binding1.Bind<A>(arg1Value), binding2.Bind<B>(arg2Value)));
            };
        }

        private static IEnumerable<IValueProvider> castResult(object result)
        {
            // TODO: Object may be a constant native value....

            if (result is IEnumerable<IValueProvider>)
                return (IEnumerable<IValueProvider>)result;
            else if (result is IValueProvider)
            {
                if (result == null)
                    return FhirValueList.Empty();
                else
                    return FhirValueList.Create((IValueProvider)result);
            }
            else
                throw new InvalidOperationException("Bound functions should either return IValueProvider or IEnumerable<IValueProvider>");
        }
    }
}
