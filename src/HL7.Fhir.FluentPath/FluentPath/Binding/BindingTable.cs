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
using System.Text.RegularExpressions;

namespace Hl7.Fhir.FluentPath.Binding
{

    public static class BindingTable
    {
        static BindingTable()
        {
            // Functions that operate on the focus, without null propagation
            add("empty", (IEnumerable<object> f) => !f.Any());
            add("exists", (IEnumerable<object> f) => f.Any());
            add("count", (IEnumerable<object> f) => f.Count());

            add("binary.|", (object f, IEnumerable<IValueProvider> l, IEnumerable<IValueProvider> r) => l.DistinctUnion(r));
            add("binary.contains", (object f, IEnumerable<IValueProvider> a, IValueProvider b) => a.Contains(b));
            add("binary.in", (object f, IValueProvider a, IEnumerable<IValueProvider> b) => b.Contains(a));
            add("distinct", (IEnumerable<IValueProvider> f) => f.Distinct());
            add("isDistinct", (IEnumerable<IValueProvider> f) => f.IsDistinct());
            add("subsetOf", (IEnumerable<IValueProvider> f, IEnumerable<IValueProvider> a) => f.SubsetOf(a));
            add("supersetOf", (IEnumerable<IValueProvider> f, IEnumerable<IValueProvider> a) => a.SubsetOf(f));



            // Functions that use normal null propagation and work with the focus (buy may ignore it)
            nullp("not", (IEnumerable<IValueProvider> f) => f.Not());
            nullp("builtin.children", (IEnumerable<IValueProvider> f, string a) => f.Children(a));

            nullp("binary.=", (object f, IEnumerable<IValueProvider>  a, IEnumerable<IValueProvider> b) => a.IsEqualTo(b));
            nullp("binary.!=", (object f, IEnumerable<IValueProvider> a, IEnumerable<IValueProvider> b) => !a.IsEqualTo(b));

            nullp("unary.-", (object f, long a) => -a);
            nullp("unary.-", (object f, decimal a) => -a);
            nullp("unary.+", (object f, long a) => a);
            nullp("unary.+", (object f, decimal a) => a);

            nullp("binary.*", (object f, long a, long b) => a * b);
            nullp("binary.*", (object f, decimal a, decimal b) => a * b);

            nullp("binary./", (object f, decimal a, decimal b) => a / b);
            //.Add((object f, decimal a, decimal b) => a / b);

            nullp("binary.+", (object f, long a, long b) => a + b);
            nullp("binary.+", (object f, decimal a, decimal b) => a + b);
            nullp("binary.+", (object f, string a, string b) => a + b);

            nullp("binary.-", (object f, long a, long b) => a - b);
            nullp("binary.-", (object f, decimal a, decimal b) => a - b);

            nullp("binary.div", (object f, long a, long b) => a / b);
            nullp("binary.div", (object f, decimal a, decimal b) => (long)Math.Truncate(a / b));

            nullp("binary.mod", (object f, long a, long b) => a % b);
            nullp("binary.mod", (object f, decimal a, decimal b) => a % b);

            nullp("binary.>", (object f, long a, long b) => a > b);
            nullp("binary.>", (object f, decimal a, decimal b) => a > b);
            nullp("binary.>", (object f, string a, string b) => String.Compare(a, b) > 0);
            nullp("binary.>", (object f, PartialDateTime a, PartialDateTime b) => a > b);
            nullp("binary.>", (object f, Time a, Time b) => a > b);

            nullp("binary.<", (object f, long a, long b) => a < b);
            nullp("binary.<", (object f, decimal a, decimal b) => a < b);
            nullp("binary.<", (object f, string a, string b) => String.Compare(a, b) < 0);
            nullp("binary.<", (object f, PartialDateTime a, PartialDateTime b) => a < b);
            nullp("binary.<", (object f, Time a, Time b) => a < b);

            nullp("binary.<=", (object f, long a, long b) => a <= b);
            nullp("binary.<=", (object f, decimal a, decimal b) => a <= b);
            nullp("binary.<=", (object f, string a, string b) => String.Compare(a, b) <= 0);
            nullp("binary.<=", (object f, PartialDateTime a, PartialDateTime b) => a <= b);
            nullp("binary.<=", (object f, Time a, Time b) => a <= b);

            nullp("binary.>=", (object f, long a, long b) => a >= b);
            nullp("binary.>=", (object f, decimal a, decimal b) => a >= b);
            nullp("binary.>=", (object f, string a, string b) => String.Compare(a, b) >= 0);
            nullp("binary.>=", (object f, PartialDateTime a, PartialDateTime b) => a >= b);
            nullp("binary.>=", (object f, Time a, Time b) => a >= b);

            nullp("single", (IEnumerable<IValueProvider> f) => f.Single());
            nullp("skip", (IEnumerable<IValueProvider> f, long a) =>  f.Skip((int)a));
            nullp("first", (IEnumerable<IValueProvider> f) => f.MyFirst());
            nullp("last", (IEnumerable<IValueProvider> f) => f.Last());
            nullp("tail", (IEnumerable<IValueProvider> f) => f.Tail());
            nullp("take", (IEnumerable<IValueProvider> f, long a) => f.Take((int)a));
            nullp("builtin.item", (IEnumerable<IValueProvider> f, long a) => f.Item((int)a));

            nullp("toInteger", (IValueProvider f) => f.ToInteger());
            nullp("toDecimal", (IValueProvider f) => f.ToDecimal());
            nullp("toString", (IValueProvider f) => f.ToStringRepresentation());

            nullp("substring", (string f, long a) => f.FpSubstring((int)a));
            nullp("substring", (string f, long a, long b) => f.FpSubstring((int)a, (int)b));
            nullp("startsWith", (string f, string fragment) => f.StartsWith(fragment));
            nullp("endsWith", (string f, string fragment) => f.EndsWith(fragment));
            nullp("matches", (string f, string regex) => Regex.IsMatch(f, regex));
            nullp("indexOf", (string f, string fragment) => f.FpIndexOf(fragment));
            nullp("contains", (string f, string fragment) => f.Contains(fragment));
            nullp("replaceMatches", (string f, string regex, string subst) => Regex.Replace(f, regex, subst));
            nullp("replace", (string f, string regex, string subst) => f.FpReplace(regex, subst));
            nullp("length", (string f) => f.Length);

            nullp("today", (object f) => PartialDateTime.Today());
            nullp("now", (object f) => PartialDateTime.Now());

            // Logic operators do not use null propagation and may do short-cut eval
            logic("binary.and", (a, b) => a.And(b));
            logic("binary.or", (a, b) => a.Or(b));
            logic("binary.xor", (a, b) => a.XOr(b));
            logic("binary.implies", (a, b) => a.Implies(b));

            // Special late-bound functions
            _functions.Add(new CallBinding("where", buildLambdaCall(runWhere), typeof(object), typeof(Invokee)));
            _functions.Add(new CallBinding("select", buildLambdaCall(runSelect), typeof(object), typeof(Invokee)));
            _functions.Add(new CallBinding("all", buildLambdaCall(runAll), typeof(object), typeof(Invokee)));
            _functions.Add(new CallBinding("any", buildLambdaCall(runAny), typeof(object), typeof(Invokee)));
            _functions.Add(new CallBinding("repeat", buildLambdaCall(runRepeat), typeof(object), typeof(Invokee)));
        }


        public static IValueProvider MyFirst(this IEnumerable<IValueProvider> focus)
        {
            return focus.First();
        }

        public static Invokee Resolve(string functionName, IEnumerable<Type> argumentTypes)
        {
            var bindings = _functions.Where(f => f.StaticMatches(functionName, argumentTypes));                        

            if(bindings.Any())
            {
                if (bindings.Count() > 1)
                {
                    return (new DynaDispatcher(functionName, bindings)).MakeDispatcher();
                }
                else
                    return bindings.Single().Function;
            }

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


        private static void add<A,R>(string name, Func<A, R> func)
        {
            _functions.Add(new CallBinding(name, InvokeeFactory.Wrap(func), typeof(A)));
        }

        private static void add<A,B,R>(string name, Func<A,B,R> func)
        {
            _functions.Add(new CallBinding(name, InvokeeFactory.Wrap(func), typeof(A), typeof(B)));
        }

        private static void add<A, B, C, R>(string name, Func<A, B, C, R> func)
        {
            _functions.Add(new CallBinding(name, InvokeeFactory.Wrap(func), typeof(A), typeof(B), typeof(C)));
        }

        private static void nullp<F>(string name, Func<F,object> func)
        {
            _functions.Add(new CallBinding(name, InvokeeFactory.Wrap(func).NullProp(), typeof(F)));
        }

        private static void nullp<F,A>(string name, Func<F,A,object> func)
        {
            _functions.Add(new CallBinding(name, InvokeeFactory.Wrap(func).NullProp(), typeof(F), typeof(A)));
        }

        private static void nullp<F,A, B>(string name, Func<F,A,B,object> func)
        {
            _functions.Add(new CallBinding(name, InvokeeFactory.Wrap(func).NullProp(), typeof(F), typeof(A), typeof(B)));
        }

        private static void logic(string name, Func<Func<bool?>,Func<bool?>,bool?> func)
        {
            _functions.Add(new CallBinding(name, InvokeeFactory.WrapLogic(func), typeof(object), typeof(Func<bool?>), typeof(Func<bool?>)));
        }

        private static Invokee buildExternalCall(string name)
        {
            return (ctx, args) =>
            {
                var focus = ctx.GetThis();
                var evaluatedArguments = args.Select(a => a(ctx,InvokeeFactory.EmptyArgs));
                return ctx.InvokeExternalFunction(name, focus, evaluatedArguments);
            };
        }

        private static Invokee buildLambdaCall(Func<IEvaluationContext,IEnumerable<IValueProvider>,Invokee,IEnumerable<IValueProvider>> evaluator)
        {
            return (ctx, args) =>
            {
                var focus = ctx.GetThis();
                Invokee lambda = args.First();

                return evaluator(ctx, focus, lambda);
            };
        }

        private static IEnumerable<IValueProvider> runWhere(IEvaluationContext ctx, IEnumerable<IValueProvider> focus, Invokee lambda)
        {
            foreach (IValueProvider element in focus)
            {
                var newContext = ctx.Nest(FhirValueList.Create(element));
                if (lambda(newContext, InvokeeFactory.EmptyArgs).BooleanEval() == true)
                    yield return element; 
            }
        }

        private static IEnumerable<IValueProvider> runSelect(IEvaluationContext ctx, IEnumerable<IValueProvider> focus, Invokee lambda)
        {
            foreach (IValueProvider element in focus)
            {
                var newContext = ctx.Nest(FhirValueList.Create(element));
                var result = lambda(newContext, InvokeeFactory.EmptyArgs);
                foreach (var resultElement in result)       // implement SelectMany()
                    yield return resultElement;
            }
        }

        private static IEnumerable<IValueProvider> runRepeat(IEvaluationContext ctx, IEnumerable<IValueProvider> focus, Invokee lambda)
        {
            var fullResult = new List<IValueProvider>();
            List<IValueProvider> newNodes = new List<IValueProvider>(focus);

            while (newNodes.Any())
            {
                var current = newNodes;
                newNodes = new List<IValueProvider>();

                foreach (IValueProvider element in current)
                {
                    var newContext = ctx.Nest(FhirValueList.Create(element));
                    newNodes.AddRange(lambda(newContext, InvokeeFactory.EmptyArgs));
                }

                fullResult.AddRange(newNodes);
            }

            return fullResult;
        }

        private static IEnumerable<IValueProvider> runAll(IEvaluationContext ctx, IEnumerable<IValueProvider> focus, Invokee lambda)
        {
            foreach (IValueProvider element in focus)
            {
                var newContext = ctx.Nest(FhirValueList.Create(element));

                var result = lambda(newContext, InvokeeFactory.EmptyArgs).BooleanEval();
                if (result == null) return FhirValueList.Empty;
                if (result == false) return FhirValueList.Create(false);
            }

            return FhirValueList.Create(true);
        }

        private static IEnumerable<IValueProvider> runAny(IEvaluationContext ctx, IEnumerable<IValueProvider> focus, Invokee lambda)
        {
            foreach (IValueProvider element in focus)
            {
                var newContext = ctx.Nest(FhirValueList.Create(element));
                var result = lambda(newContext, InvokeeFactory.EmptyArgs).BooleanEval();

                //if (result == null) return FhirValueList.Empty; -> otherwise this would not be where().exists()
                //Patient.identifier.any(use = 'official') would return {} if ANY identifier has no 'use' element. Unexpected behaviour, I think
                if (result == true) return FhirValueList.Create(true);
            }

            return FhirValueList.Create(false);
        }

    }
}
