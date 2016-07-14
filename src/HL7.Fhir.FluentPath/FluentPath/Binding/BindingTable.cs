/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.FluentPath;
using System;
using System.Linq;
using System.Collections.Generic;
using Hl7.Fhir.Support;
using Hl7.Fhir.FluentPath.Functions;

namespace Hl7.Fhir.FluentPath.Binding
{

    public class BindingTable
    {
        private static ParamBinding<IEnumerable<IValueProvider>> parAny(string name)
        {
            return new ParamBinding<IEnumerable<IValueProvider>>(name);
        }

        private static FocusBinding<IEnumerable<IValueProvider>> anyFocus = new FocusBinding<IEnumerable<IValueProvider>>();

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


        static BindingTable()
        {
            // Functions that operate on the focus, without null propagation
            focus("empty", f => !f.Any());
            focus("exists", f => f.Any());
            focus("count", f => f.Count());

            // Functions that use normal null propagation and work with the focus (buy may ignore it)
            add("not", (IEnumerable<IValueProvider> f) => f.Not());
            add("builtin.children", (IEnumerable<IValueProvider> f, string a) => f.Children(a));

            add("binary.=", (object f, IEnumerable<IValueProvider>  a, IEnumerable<IValueProvider> b) => a.IsEqualTo(b));
            add("binary.!=", (object f, IEnumerable<IValueProvider> a, IEnumerable<IValueProvider> b) => !a.IsEqualTo(b));

            add("unary.-", (object f, long a) => -a)
                .Add((object f, decimal a) => -a);
            add("unary.+", (object f, long a) => a)
                .Add((object f, decimal a) => a);

            add("binary.*", (object f, long a, long b) => a * b)
                .Add( (object f, decimal a, decimal b) => a * b);
            add("binary./", (object f, decimal a, decimal b) => a / b);
                //.Add((object f, decimal a, decimal b) => a / b);
            add("binary.+", (object f, long a, long b) => a + b)
                .Add((object f, decimal a, decimal b) => a + b)
                .Add((object f, string a, string b) => a + b);
            add("binary.-", (object f, long a, long b) => a - b)
                .Add((object f, decimal a, decimal b) => a - b);
            add("binary.div", (object f, long a, long b) => a / b)
                .Add((object f, decimal a, decimal b) => (long)Math.Truncate(a / b));
            add("binary.mod", (object f, long a, long b) => a % b)
                .Add((object f, decimal a, decimal b) => a % b);

            add("binary.>", (object f, long a, long b) => a > b)
                .Add((object f, decimal a, decimal b) => a > b)
                .Add((object f, string a, string b) => String.Compare(a, b) > 0)
                .Add((object f, PartialDateTime a, PartialDateTime b) => a > b)
                .Add((object f, Time a, Time b) => a > b);
            add("binary.<", (object f, long a, long b) => a < b)
                .Add((object f, decimal a, decimal b) => a < b)
                .Add((object f, string a, string b) => String.Compare(a, b) < 0)
                .Add((object f, PartialDateTime a, PartialDateTime b) => a < b)
                .Add((object f, Time a, Time b) => a < b);
            add("binary.<=", (object f, long a, long b) => a <= b)
                .Add((object f, decimal a, decimal b) => a <= b)
                .Add((object f, string a, string b) => String.Compare(a, b) <= 0)
                .Add((object f, PartialDateTime a, PartialDateTime b) => a <= b)
                .Add((object f, Time a, Time b) => a <= b);
            add("binary.>=", (object f, long a, long b) => a >= b)
                .Add((object f, decimal a, decimal b) => a >= b)
                .Add((object f, string a, string b) => String.Compare(a, b) >= 0)
                .Add((object f, PartialDateTime a, PartialDateTime b) => a >= b)
                .Add((object f, Time a, Time b) => a >= b);

            add("binary.|", (object f, IEnumerable<IValueProvider> l, IEnumerable<IValueProvider> r) => l.Union(r) );
            add("binary.contains", (object f, IEnumerable<IValueProvider> a, IValueProvider b) => a.Contains(b) );
            add("binary.in", (object f, IValueProvider a, IEnumerable<IValueProvider> b) => b.Contains(a));
            add("distinct", (IEnumerable<IValueProvider> f) => f.Distinct());
            add("isDistinct", (IEnumerable<IValueProvider> f) => f.IsDistinct());
            add("subsetOf", (IEnumerable<IValueProvider> f, IEnumerable<IValueProvider> a) => f.SubsetOf(a));
            add("supersetOf", (IEnumerable<IValueProvider> f, IEnumerable<IValueProvider> a) => a.SubsetOf(f));

            add("single", (IEnumerable<IValueProvider> f) => f.Single());
            add("skip", (IEnumerable<IValueProvider> f, long a) =>  f.Skip((int)a));
            add("first", (IEnumerable<IValueProvider> f) => f.First());
            add("last", (IEnumerable<IValueProvider> f) => f.Last());
            add("tail", (IEnumerable<IValueProvider> f) => f.Tail());
            add("take", (IEnumerable<IValueProvider> f, long a) => f.Take((int)a));
            add("item", (IEnumerable<IValueProvider> f, long a) => f.Item((int)a));

            add("toInteger", (IValueProvider f) => f.ToInteger());
            add("toDecimal", (IValueProvider f) => f.ToDecimal());
            add("toString", (IValueProvider f) => f.ToStringRepresentation());

            add("substring", (string f, long a) => f.Substring((int)a));
            add("substring", (string f, long a, long b) => f.Substring((int)a, (int)b));

            add("today", (object f) => PartialDateTime.Today());
            add("now", (object f) => PartialDateTime.Now());

            // Logic operators do not use null propagation and may do short-cut eval
            logic("binary.and", (a, b) => a.And(b));
            logic("binary.or", (a, b) => a.Or(b));
            logic("binary.xor", (a, b) => a.XOr(b));
            logic("binary.implies", (a, b) => a.Implies(b));

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


        private static void focus(string name, Func<IEnumerable<IValueProvider>, object> focusFunc)
        {
            _functions.Add(new CallBinding(name, buildFocusInputCall(focusFunc), new ParamBinding[] { }));
        }

        private static DynaDispatcher add<F>(string name, Func<F,object> func)
        {
            var db = new DynaDispatcher();
            _functions.Add(new CallBinding(name, db.Invoke, new ParamBinding[] { } ));
            db.Add(func);

            return db;
        }

        private static DynaDispatcher add<F,A>(string name, Func<F,A,object> func)
        {
            var db = new DynaDispatcher();
            _functions.Add(new CallBinding(name, db.Invoke, parAny("param1")));
            db.Add(func);

            return db;
        }

        private static DynaDispatcher add<F,A, B>(string name, Func<F,A,B,object> func)
        {
            var db = new DynaDispatcher();
            _functions.Add(new CallBinding(name, db.Invoke, parAny("param1"), parAny("param2")));
            db.Add(func);

            return db;
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
                return Typecasts.CastTo<IEnumerable<IValueProvider>>(func(()=>left(ctx, f).BooleanEval(), ()=>right(ctx, f).BooleanEval()));
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

        private static Invokee buildFocusInputCall(Func<IEnumerable<IValueProvider>,object> func)
        {
            return (ctx, focus, args) =>
            {
                return Typecasts.WrapNative<IEnumerable<IValueProvider>>(anyFocus, func, focus);
            };
        }            
    }
}
