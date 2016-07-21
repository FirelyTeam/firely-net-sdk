/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.FluentPath.Binding
{
    public delegate IEnumerable<IValueProvider> Invokee(IEvaluationContext context, IEnumerable<Invokee> arguments);

    public static class InvokeeFactory
    {
        public static IEnumerable<IValueProvider> Select(this Invokee evaluator, IEvaluationContext context)
        {
            return evaluator(context, InvokeeFactory.EmptyArgs);
        }

        public static IEnumerable<IValueProvider> Select(this Invokee evaluator, IEnumerable<IValueProvider> input)
        {
            return evaluator.Select(BaseEvaluationContext.Root(input));
        }

        public static object Scalar(this Invokee evaluator, IEvaluationContext context)
        {
            return evaluator.Select(context).Single().Value;
        }

        public static object Scalar(this Invokee evaluator, IEnumerable<IValueProvider> input)
        {
            return evaluator.Scalar(BaseEvaluationContext.Root(input));
        }

        // For predicates, Empty is considered false (?)
        public static bool Predicate(this Invokee evaluator, IEvaluationContext context)
        {
            var result = evaluator.Select(context).BooleanEval();

            if (!result == null)
                return false;
            else
                return result.Value;
        }

        public static bool Predicate(this Invokee evaluator, IEnumerable<IValueProvider> input)
        {
            return evaluator.Predicate(BaseEvaluationContext.Root(input));
        }

        public static readonly IEnumerable<Invokee> EmptyArgs = Enumerable.Empty<Invokee>();

        public static Invokee NullProp(this Invokee source)
        {
            return (ctx, args) =>
            {
                var focus = ctx.GetThis();

                if (!focus.Any()) return FhirValueList.Empty;
                
                foreach (var arg in args)
                {
                    var argValue = arg(ctx, InvokeeFactory.EmptyArgs);
                    if (!argValue.Any()) return FhirValueList.Empty;
                }

                return source(ctx, args);
            };
        }


        public static Invokee Wrap<R>(Func<R> func)
        {
            return (ctx, args) =>
            {
                return Typecasts.CastTo<IEnumerable<IValueProvider>>(func());
            };
        }

        public static Invokee Wrap<A,R>(Func<A,R> func)
        {
            return (ctx, args) =>
            {
                var focus = Typecasts.CastTo<A>(ctx.GetThis());
                return Typecasts.CastTo<IEnumerable<IValueProvider>>(func(focus));
            };
        }

        public static Invokee Wrap<A,B,R>(Func<A,B,R> func)
        {
            return (ctx, args) =>
            {
                var focus = Typecasts.CastTo<A>(ctx.GetThis());
                var argA = Typecasts.CastTo<B>(args.First()(ctx, InvokeeFactory.EmptyArgs));
                return Typecasts.CastTo<IEnumerable<IValueProvider>>(func(focus,argA));
            };
        }

        public static Invokee Wrap<A, B, C, R>(Func<A, B, C, R> func)
        {
            return (ctx, args) =>
            {
                var focus = Typecasts.CastTo<A>(ctx.GetThis());
                var argA = Typecasts.CastTo<B>(args.First()(ctx, InvokeeFactory.EmptyArgs));
                var argB = Typecasts.CastTo<C>(args.Skip(1).First()(ctx, InvokeeFactory.EmptyArgs));
                return Typecasts.CastTo<IEnumerable<IValueProvider>>(func(focus, argA, argB)); 
            };
        }


        public static Invokee WrapLogic(Func<Func<bool?>, Func<bool?>, bool?> func)
        {
            return (ctx, args) =>
            {
                var left = args.First();
                var right = args.Skip(1).First();
                return Typecasts.CastTo<IEnumerable<IValueProvider>>(func(() => left(ctx, InvokeeFactory.EmptyArgs).BooleanEval(), () => right(ctx, InvokeeFactory.EmptyArgs).BooleanEval()));
            };
        }

        public static Invokee Return(Hl7.Fhir.FluentPath.IValueProvider value)
        {
            return (_, __) => (new[] { (Hl7.Fhir.FluentPath.IValueProvider)value });
        }

        public static Invokee Return(IEnumerable<Hl7.Fhir.FluentPath.IValueProvider> value)
        {
            return (_, __) => value;
        }

        public static Invokee ResolveValue(string name)
        {
            return (ctx, __) => ctx.ResolveValue(name);
        }


        public static Invokee Focus()
        {
            return (ctx, _) => ctx.GetThis();
        }

    }
}