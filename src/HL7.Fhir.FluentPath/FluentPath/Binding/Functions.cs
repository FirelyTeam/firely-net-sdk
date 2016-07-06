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
        public delegate object Builtin<F>(F focus);
        public delegate object Builtin<F,A>(F focus, A argument1);
        public delegate object Builtin<F,A, B>(F focus, A argument1, B argument2);

        private static ParamBinding<T> par<T>(string name)
        {
            return new ParamBinding<T>(name);
        }

        private static FocusBinding<T> focus<T>()
        {
            return new FocusBinding<T>();
        }

        private class ParamBinding<T> : ParamBinding
        {
            public ParamBinding(string name) : base(name, typeof(T))
            {
            }
        }

        private class FocusBinding<T> : ParamBinding
        {
            public FocusBinding() : base("focus", typeof(T))
            {
            }
        }


        static Functions()
        {
            add("not", f => f.Not());
            add("empty", f => f.IsEmpty());
            add("exists", f => f.Exists());
            add("builtin.children", focus<IEnumerable<IValueProvider>>(), par<string>("name"), (f, a) => f.Children(a));
            add("substring", focus<string>(), par<int>("start"), (f, a) => f.Substring(a));
            add("substring", focus<string>(), par<int>("start"), par<int>("length"), (f, a, b) => f.Substring(a, b));

            //add("count", (f, _) => f.CountItems(), None());
            ////add("=", Exactly(1) && Args(TypeInfo.Decimal, TypeInfo.Decimal), (f, a) => Operators.IsEqualTo(a[0]), Exactly(1));
            //add("+", (f, a) => f.Add(a[0]), Exactly(1));
        }


        public static Invokee Resolve(FunctionCallExpression expression)
        {
            CallBinding binding = _functions.SingleOrDefault(f=>f.StaticMatches(expression));

            if (binding != null)
                return binding.Function;
            else
            {
                if (_functions.Any(f => f.Name == expression.FunctionName))
                {
                    // No function could be found, but there IS a function with the given name, 
                    // report an error about the fact that the function is known, but could not be bound
                    throw Error.Argument("Function '{0}' is not called with the right number or type of parameters".FormatWith(expression.FunctionName));
                }
                else
                {
                    // Not an internally known function, forward to context (so it can provide a hook to handle it)
                    return buildExternalCall(expression.FunctionName);
                }
            }
        }


        private static List<CallBinding> _functions = new List<CallBinding>();


        private static void add(string name, Func<IEnumerable<IValueProvider>, object> focusFunc)
        {
            _functions.Add(new CallBinding(name, buildFocusInputCall(focusFunc), new ParamBinding[] { }));
        }

        private static void add<F>(string name, FocusBinding<F> focus, Builtin<F> func)
        {
            _functions.Add(new CallBinding(name, buildNullPropCall(focus,func), new ParamBinding[] { }));
        }

        private static void add<F,A>(string name, FocusBinding<F> focus, ParamBinding<A> param1, Builtin<F,A> func)
        {
            _functions.Add(new CallBinding(name, buildNullPropCall(focus, func, param1), param1));
        }

        private static void add<F,A, B>(string name, FocusBinding<F> focus, ParamBinding<A> param1, ParamBinding<B> param2, Builtin<F,A, B> func)
        {
            _functions.Add(new CallBinding(name, buildNullPropCall(focus, func, param1, param2), param1, param2));
        }

        private static Invokee buildExternalCall(string name)
        {
            return (ctx, args) =>
            {
                var evaluatedArguments = args.Select(a => a(ctx));
                return ctx.InvokeExternalFunction(name, ctx.CurrentFocus, evaluatedArguments);
            };
        }


        private static Invokee buildNullPropCall<F>(FocusBinding<F> focus, Builtin<F> b)
        {
            return (ctx, args) =>
            {
                if (!ctx.CurrentFocus.IsEmpty())
                    return castResult(b(focus.Bind<F>(ctx.CurrentFocus)));
                else
                    return FhirValueList.Empty();
            };
        }

        private static Invokee buildNullPropCall<F,A>(FocusBinding<F> focus, Builtin<F,A> b, ParamBinding<A> binding1)
        {
            return (ctx, args) =>
            {
                if (!ctx.CurrentFocus.IsEmpty())
                {
                    var argValue = args.Single()(ctx);       // early bound argument, evaluate now. Not good for functions that have lambdas as parameters

                    if (!argValue.IsEmpty())            // Implement the usual logic of null propagation for functions
                        return castResult(b(focus.Bind<F>(ctx.CurrentFocus), binding1.Bind<A>(argValue)));
                }

                return FhirValueList.Empty();
            };
        }

        private static Invokee buildNullPropCall<F,A, B>(FocusBinding<F> focus, Builtin<F, A, B> b, ParamBinding<A> binding1, ParamBinding<B> binding2)
        {
            return (ctx, args) =>
            {
                if (!ctx.CurrentFocus.IsEmpty())
                {
                    var arg1Value = args.First()(ctx);       // early bound argument, evaluate now. Not good for functions that have lambdas as parameters

                    if (!arg1Value.IsEmpty())    // null propagation
                    {
                        var arg2Value = args.Skip(1).First()(ctx);       // early bound argument, evaluate now. Not good for functions that have lambdas as parameters

                        if (!arg2Value.IsEmpty())
                            return castResult(b(focus.Bind<F>(ctx.CurrentFocus), binding1.Bind<A>(arg1Value), binding2.Bind<B>(arg2Value)));
                    }
                }

                return FhirValueList.Empty();
            };
        }


        private static Invokee buildFocusInputCall(Func<IEnumerable<IValueProvider>,object> func)
        {
            return (ctx, args) =>
            {
                return FhirValueList.Create(func(ctx.CurrentFocus));
            };
        }
       
        private static IEnumerable<IValueProvider> castResult(object result)
        {
            // Builtins may return
            // * null -> this is turned into an empty list of IValueProvider
            // * A list of IValueProvider -> this is returned as-is
            // * A single IValueProvider -> returned as a list with that single IValueProvider
            // * Any other value -> a list with an IValueProvider with that value

            if (result == null) return FhirValueList.Empty();

            if (result is IEnumerable<IValueProvider>)
                return (IEnumerable<IValueProvider>)result;
            else
                return FhirValueList.Create(result);
        }
    }
}
