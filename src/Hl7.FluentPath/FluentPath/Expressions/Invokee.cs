/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.FluentPath.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.ElementModel;
using Furore.Support;

namespace Hl7.FluentPath.Expressions
{
    internal delegate IEnumerable<IValueProvider> Invokee(Closure context, IEnumerable<Invokee> arguments);

    internal static class InvokeeFactory
    {
        public static readonly IEnumerable<Invokee> EmptyArgs = Enumerable.Empty<Invokee>();
    

        public static IEnumerable<IValueProvider> GetThis(Closure context, IEnumerable<Invokee> args)
        {
            return context.GetThis();
        }

        public static IEnumerable<IValueProvider> GetContext(Closure context, IEnumerable<Invokee> arguments)
        {
            return context.GetOriginalContext();
        }

        public static IEnumerable<IValueProvider> GetResource(Closure context, IEnumerable<Invokee> arguments)
        {
            return context.GetResource();
        }

        public static IEnumerable<IValueProvider> GetThat(Closure context, IEnumerable<Invokee> args)
        {
            return context.GetThat();
        }


        public static Invokee Wrap<R>(Func<R> func)
        {
            return (ctx, args) =>
            {
                return Typecasts.CastTo<IEnumerable<IValueProvider>>(func());
            };
        }

        public static Invokee Wrap<A, R>(Func<A, R> func, bool propNull)
        {
            return (ctx, args) =>
            {
                var focus = args.First()(ctx, InvokeeFactory.EmptyArgs);
                if (propNull && !focus.Any()) return FhirValueList.Empty;
          
                return Typecasts.CastTo<IEnumerable<IValueProvider>>(func(Typecasts.CastTo<A>(focus)));
            };
        }

        public static Invokee Wrap<A, B, R>(Func<A, B, R> func, bool propNull)
        {
            return (ctx, args) =>
            {
                var focus = args.First()(ctx, InvokeeFactory.EmptyArgs);
                if (propNull && !focus.Any()) return FhirValueList.Empty;

                var newCtx = ctx.Nest(focus);             
                var argA = args.Skip(1).First()(newCtx, InvokeeFactory.EmptyArgs);
                if (propNull && !argA.Any()) return FhirValueList.Empty;

                return Typecasts.CastTo<IEnumerable<IValueProvider>>(func(Typecasts.CastTo<A>(focus), Typecasts.CastTo<B>(argA)));
            };
        }

        public static Invokee Wrap<A, B, C, R>(Func<A, B, C, R> func, bool propNull)
        {
            return (ctx, args) =>
            {
                var focus = args.First()(ctx, InvokeeFactory.EmptyArgs);
                if (propNull && !focus.Any()) return FhirValueList.Empty;

                var newCtx = ctx.Nest(focus);
                var argA = args.Skip(1).First()(newCtx, InvokeeFactory.EmptyArgs);
                if (propNull && !argA.Any()) return FhirValueList.Empty;
                var argB = args.Skip(2).First()(newCtx, InvokeeFactory.EmptyArgs);
                if (propNull && !argB.Any()) return FhirValueList.Empty;

                return Typecasts.CastTo<IEnumerable<IValueProvider>>(func(Typecasts.CastTo<A>(focus), Typecasts.CastTo<B>(argA), Typecasts.CastTo<C>(argB)));
            };
        }

        public static Invokee Wrap<A, B, C, D, R>(Func<A, B, C, D, R> func, bool propNull)
        {
            return (ctx, args) =>
            {
                var focus = args.First()(ctx, InvokeeFactory.EmptyArgs);
                if (propNull && !focus.Any()) return FhirValueList.Empty;

                var newCtx = ctx.Nest(focus);
                var argA = args.Skip(1).First()(newCtx, InvokeeFactory.EmptyArgs);
                if (propNull && !argA.Any()) return FhirValueList.Empty;
                var argB = args.Skip(2).First()(newCtx, InvokeeFactory.EmptyArgs);
                if (propNull && !argB.Any()) return FhirValueList.Empty;
                var argC = args.Skip(3).First()(newCtx, InvokeeFactory.EmptyArgs);
                if (propNull && !argC.Any()) return FhirValueList.Empty;

                return Typecasts.CastTo<IEnumerable<IValueProvider>>(func(Typecasts.CastTo<A>(focus), 
                            Typecasts.CastTo<B>(argA), Typecasts.CastTo<C>(argB), Typecasts.CastTo<D>(argC)));
            };
        }

        public static Invokee WrapLogic(Func<Func<bool?>, Func<bool?>, bool?> func)
        {
            return (ctx, args) =>
            {
                // Ignore focus
                // NOT GOOD, arguments need to be evaluated in the context of the focus to give "$that" meaning.
                var left = args.Skip(1).First();
                var right = args.Skip(2).First();

                // Return function that actually executes the Invokee at the last moment
                return Typecasts.CastTo<IEnumerable<IValueProvider>>(func(() => left(ctx, InvokeeFactory.EmptyArgs).BooleanEval(), () => right(ctx, InvokeeFactory.EmptyArgs).BooleanEval()));
            };
        }

        public static Invokee Return(IValueProvider value)
        {
            return (_, __) => (new[] { ( IValueProvider)value });
        }

        public static Invokee Return(IEnumerable<IValueProvider> value)
        {
            return (_, __) => value;
        }

        public static Invokee Invoke(string functionName, IEnumerable<Invokee> arguments, Invokee invokee)
        {
            Func<Closure, IEnumerable<IValueProvider>> boundFunc = (ctx) => invokee(ctx, arguments);
            IEnumerable<IValueProvider> lastResult = null;

            return (ctx, _) =>
            {
              //  if (lastResult != null) return lastResult;

                try
                {
                    lastResult = boundFunc(ctx);
                    return lastResult;
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException("Invocation of '{0}' failed: {1}".FormatWith(functionName, e.Message));
                }
            };
        }

    }
}