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
    public delegate IEnumerable<IValueProvider> Invokee(IEvaluationContext context, IEnumerable<Evaluator> arguments);

    public static class InvokeeFactory
    {        
        public static Invokee NullProp(this Invokee source)
        {
            return (ctx, args) =>
            {
                var focus = ctx.GetThis();

                if (!focus.Any()) return FhirValueList.Empty;
                
                foreach (var arg in args)
                {
                    var argValue = arg(ctx);
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
                var argA = Typecasts.CastTo<B>(args.First()(ctx));
                return Typecasts.CastTo<IEnumerable<IValueProvider>>(func(focus,argA));
            };
        }

        public static Invokee Wrap<A, B, C, R>(Func<A, B, C, R> func)
        {
            return (ctx, args) =>
            {
                var focus = Typecasts.CastTo<A>(ctx.GetThis());
                var argA = Typecasts.CastTo<B>(args.First()(ctx));
                var argB = Typecasts.CastTo<C>(args.Skip(1).First()(ctx));
                return Typecasts.CastTo<IEnumerable<IValueProvider>>(func(focus, argA, argB)); 
            };
        }


        public static Invokee WrapLogic(Func<Func<bool?>, Func<bool?>, bool?> func)
        {
            return (ctx, args) =>
            {
                var left = args.First();
                var right = args.Skip(1).First();
                return Typecasts.CastTo<IEnumerable<IValueProvider>>(func(() => left(ctx).BooleanEval(), () => right(ctx).BooleanEval()));
            };
        }

    }
}