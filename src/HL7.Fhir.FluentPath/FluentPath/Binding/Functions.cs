/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.FluentPath;
using Hl7.Fhir.FluentPath.Binding;
using System;
using System.Linq;
using System.Collections.Generic;
using Hl7.Fhir.Support;

namespace HL7.Fhir.FluentPath.FluentPath.Binding
{

    public class Functions
    {
        private static ParamBinding<T> par<T>(string name)
        {
            return new ParamBinding<T>(name);
        }

        private static ParamBinding<IEnumerable<IValueProvider>> parAny(string name)
        {
            return new ParamBinding<IEnumerable<IValueProvider>>(name);
        }

        private static FocusBinding<IEnumerable<IValueProvider>> anyFocus = new FocusBinding<IEnumerable<IValueProvider>>();

        private static FocusBinding<T> focus<T>()
        {
            return new FocusBinding<T>();
        }

        private class ParamBinding<T> : ParamBinding
        {
            public ParamBinding(string name) : base(name, TypeInfo.ForNativeType(typeof(T)))
            {
            }
        }

        private class FocusBinding<T> : ParamBinding<T>
        {
            public FocusBinding() : base("focus")
            {
            }
        }


        static Functions()
        {
            // Functions that operate on the focus, without null propagation
            add("empty", f => !f.Any());
            add("exists", f => f.Any());
            add("count", f => f.CountItems());

            // Functions that use normal null propagation and work with the focus (buy may ignore it)
            add("not", anyFocus, f => f.Not());
            add("builtin.children", anyFocus, par<string>("name"), (f, a) => f.Children(a));
            add("builtin.=", anyFocus, parAny("left"), parAny("right"), (f, a, b) => a.IsEqualTo(b));

            add("builtin.*", anyFocus, parAny("left"), parAny("right"), (f, a, b) => a.DynaMul(b));
            add("builtin./", anyFocus, parAny("left"), parAny("right"), (f, a, b) => a.DynaDiv(b));
            add("builtin.+", anyFocus, parAny("left"), parAny("right"), (f, a, b) => a.DynaAdd(b));
            add("builtin.-", anyFocus, parAny("left"), parAny("right"), (f, a, b) => a.DynaSub(b));

            add("substring", focus<string>(), par<long>("start"), (f, a) => f.Substring((int)a));
            add("substring", focus<string>(), par<long>("start"), par<long>("length"), (f, a, b) => f.Substring((int)a, (int)b));
            add("skip", anyFocus, par<long>("amount"), (f, a) => f.Skip((int)a));
            add("first", anyFocus, f => f.First());

            // Logic operators do not use null propagation and may do short-cut eval
            logic("builtin.and", (a, b) => a.And(b));
            logic("builtin.or", (a, b) => a.Or(b));
            logic("builtin.xor", (a, b) => a.XOr(b));
            logic("builtin.implies", (a, b) => a.Implies(b));

            // Special late-bound functions
            _functions.Add(new CallBinding("where", buildWhereLambda(), new ParamBinding("condition", TypeInfo.Any)));
        }


        public static Invokee Resolve(string functionName, IEnumerable<TypeInfo> argumentTypes)
        {
            CallBinding binding = _functions.SingleOrDefault(f => f.StaticMatches(functionName, argumentTypes));                        

            if (binding != null)
                return binding.Function;
            else
            {
                if (_functions.Any(f => f.Name == functionName))
                {
                    // No function could be found, but there IS a function with the given name, 
                    // report an error about the fact that the function is known, but could not be bound
                    throw Error.Argument("Function '{0}' is not called with the right number or type of parameters".FormatWith(functionName));
                }
                else
                {
                    // Not an internally known function, forward to context (so it can provide a hook to handle it)
                    return buildExternalCall(functionName);
                }
            }
        }


        private static List<CallBinding> _functions = new List<CallBinding>();


        private static void add(string name, Func<IEnumerable<IValueProvider>, object> focusFunc)
        {
            _functions.Add(new CallBinding(name, buildFocusInputCall(focusFunc), new ParamBinding[] { }));
        }

        private static void add<F>(string name, FocusBinding<F> focus, Func<F,object> func)
        {
            _functions.Add(new CallBinding(name, buildNullPropCall(focus,func), new ParamBinding[] { }));
        }

        private static void add<F,A>(string name, FocusBinding<F> focus, ParamBinding<A> param1, Func<F,A,object> func)
        {
            _functions.Add(new CallBinding(name, buildNullPropCall(focus, func, param1), param1));
        }

        private static void add<F,A, B>(string name, FocusBinding<F> focus, ParamBinding<A> param1, ParamBinding<B> param2, Func<F,A,B,object> func)
        {
            _functions.Add(new CallBinding(name, buildNullPropCall(focus, func, param1, param2), param1, param2));
        }


        private static void logic(string name, Func<Func<bool?>,Func<bool?>,bool?> func)
        {
            _functions.Add(new CallBinding(name, buildLogicCall(func), new ParamBinding("left", TypeInfo.Boolean), new ParamBinding("right", TypeInfo.Boolean)));
        }


        private static Invokee buildLogicCall(Func<Func<bool?>, Func<bool?>, bool?> func)
        {
            return (ctx, f, args) =>
            {
                var left = args.First();
                var right = args.Skip(1).First();
                return castResult(func(() => ParamBinding.CastToSingleValue<bool?>(left(ctx, f)),
                            () => ParamBinding.CastToSingleValue<bool?>(right(ctx, f))));
            };
        }

        private static Invokee buildExternalCall(string name)
        {
            return (ctx, focus, args) =>
            {
                var evaluatedArguments = args.Select(a => a(ctx, focus));
                return ctx.InvokeExternalFunction(name, focus, evaluatedArguments);
            };
        }


        private static Invokee buildWhereLambda()
        {
            return (ctx, focus, args) =>
            {
                Evaluator lambda = args.First();

                return run(ctx, focus, lambda);
            };
        }

        

        private static IEnumerable<IValueProvider> run(IEvaluationContext ctx, IEnumerable<IValueProvider> focus, Evaluator lambda)
        {
            foreach (IValueProvider element in focus)
            {
                var newFocus = FhirValueList.Create(element);
                if (lambda(ctx, newFocus).BooleanEval() == true)
                    yield return element; 
            }
        }

        private static Invokee buildNullPropCall<F>(FocusBinding<F> focusBinding, Func<F,object> b)
        {
            return (ctx, focus, args) =>
            {
                return PropEmpty(focus, f => castResult(b(focusBinding.Bind<F>(f))));
            };
        }

        private static Invokee buildNullPropCall<F,A>(FocusBinding<F> focusBinding, Func<F,A,object> b, ParamBinding<A> binding1)
        {
            return (ctx, focus, args) =>
            {
                return
                    PropEmpty(focus, f =>
                        PropEmpty(args.First()(ctx, focus), a1 =>
                            castResult(b(focusBinding.Bind<F>(f), binding1.Bind<A>(a1)))));
            };
        }

        private static Invokee buildNullPropCall<F,A, B>(FocusBinding<F> focusBinding, Func<F,A,B,object> b, ParamBinding<A> binding1, ParamBinding<B> binding2)
        {
            return (ctx, focus, args) =>
            {              
                return
                    PropEmpty(focus, f =>
                        PropEmpty(args.First()(ctx, focus), a1 =>
                            PropEmpty(args.Skip(1).First()(ctx, focus), a2 =>
                                castResult(b(focusBinding.Bind<F>(f), binding1.Bind<A>(a1), binding2.Bind<B>(a2))))));
            };
        }


        private static IEnumerable<U> PropEmpty<T,U>(IEnumerable<T> source, Func<IEnumerable<T>, IEnumerable<U>> f)
        {
            if (source.Any())
                return f(source);
            else
                return Enumerable.Empty<U>();
        }

        private static Invokee buildFocusInputCall(Func<IEnumerable<IValueProvider>,object> func)
        {
            return (ctx, focus, args) =>
            {
                return castResult(func(focus));
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
