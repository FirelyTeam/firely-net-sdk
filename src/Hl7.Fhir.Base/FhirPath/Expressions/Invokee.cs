/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.FhirPath.Functions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.FhirPath.Expressions
{
    internal delegate IEnumerable<IScopedNode> Invokee(Closure context, IEnumerable<Invokee> arguments);

    internal static class InvokeeFactory
    {
        public static readonly IEnumerable<Invokee> EmptyArgs = Enumerable.Empty<Invokee>();

        public static IEnumerable<IScopedNode> GetThis(Closure context, IEnumerable<Invokee> _)
        {
            return context.GetThis();
        }

        public static IEnumerable<IScopedNode> GetTotal(Closure context, IEnumerable<Invokee> _)
        {
            return context.GetTotal();
        }

        public static IEnumerable<IScopedNode> GetContext(Closure context, IEnumerable<Invokee> _)
        {
            return context.GetOriginalContext();
        }

        public static IEnumerable<IScopedNode> GetResource(Closure context, IEnumerable<Invokee> _)
        {
            return context.GetResource();
        }
        public static IEnumerable<IScopedNode> GetRootResource(Closure context, IEnumerable<Invokee> arguments)
        {
            return context.GetRootResource();
        }

        public static IEnumerable<IScopedNode> GetThat(Closure context, IEnumerable<Invokee> _)
        {
            return context.GetThat();
        }

        public static IEnumerable<IScopedNode> GetIndex(Closure context, IEnumerable<Invokee> args)
        {

            return context.GetIndex();
        }


        public static Invokee Wrap<R>(Func<R> func)
        {
            return (ctx, args) =>
            {
                return Typecasts.CastTo<IEnumerable<IScopedNode>>(func());
            };
        }

        public static Invokee Wrap<A, R>(Func<A, R> func, bool propNull)
        {
            return (ctx, args) =>
            {
                if (typeof(A) != typeof(EvaluationContext))
                {
                    var focus = args.First()(ctx, InvokeeFactory.EmptyArgs);
                    if (propNull && !focus.Any()) return [];

                    return Typecasts.CastTo<IEnumerable<IScopedNode>>(func(Typecasts.CastTo<A>(focus)));
                }
                else
                {
                    A lastPar = (A)(object)ctx.EvaluationContext;
                    return Typecasts.CastTo<IEnumerable<IScopedNode>>(func(lastPar));
                }
            };
        }

        internal static Invokee WrapWithPropNullForFocus<A, B, C, R>(Func<A, B, C, R> func)
        {
            return (ctx, args) =>
            {
                // propagate only null for focus
                var focus = args.First()(ctx, InvokeeFactory.EmptyArgs);
                if (!focus.Any()) return[];

                return Wrap(func, false)(ctx, args);
            };
        }

        public static Invokee Wrap<A, B, R>(Func<A, B, R> func, bool propNull)
        {
            return (ctx, args) =>
            {
                var focus = args.First()(ctx, InvokeeFactory.EmptyArgs);
                if (propNull && !focus.Any()) return [];

                if (typeof(B) != typeof(EvaluationContext))
                {
                    var argA = args.Skip(1).First()(ctx, InvokeeFactory.EmptyArgs);
                    if (propNull && !argA.Any()) return [];

                    return Typecasts.CastTo<IEnumerable<IScopedNode>>(func(Typecasts.CastTo<A>(focus), Typecasts.CastTo<B>(argA)));
                }
                else
                {
                    B lastPar = (B)(object)ctx.EvaluationContext;
                    return Typecasts.CastTo<IEnumerable<IScopedNode>>(func(Typecasts.CastTo<A>(focus), lastPar));
                }
            };
        }

        public static Invokee Wrap<A, B, C, R>(Func<A, B, C, R> func, bool propNull)
        {
            return (ctx, args) =>
            {
                var focus = args.First()((Closure)ctx, InvokeeFactory.EmptyArgs);
                if (propNull && !focus.Any()) return [];

                var argA = args.Skip(1).First()(ctx, InvokeeFactory.EmptyArgs);
                if (propNull && !argA.Any()) return [];

                if (typeof(C) != typeof(EvaluationContext))
                {
                    var argB = args.Skip(2).First()(ctx, InvokeeFactory.EmptyArgs);
                    if (propNull && !argB.Any()) return [];

                    return Typecasts.CastTo<IEnumerable<IScopedNode>>(func(Typecasts.CastTo<A>(focus), Typecasts.CastTo<B>(argA),
                        Typecasts.CastTo<C>(argB)));
                }
                else
                {
                    C lastPar = (C)(object)ctx.EvaluationContext;
                    return Typecasts.CastTo<IEnumerable<IScopedNode>>(func(Typecasts.CastTo<A>(focus),
                        Typecasts.CastTo<B>(argA), lastPar));
                }
            };
        }

        public static Invokee Wrap<A, B, C, D, R>(Func<A, B, C, D, R> func, bool propNull)
        {
            return (ctx, args) =>
            {
                var focus = args.First()((Closure)ctx, InvokeeFactory.EmptyArgs);
                if (propNull && !focus.Any()) return [];

                var argA = args.Skip(1).First()(ctx, InvokeeFactory.EmptyArgs);
                if (propNull && !argA.Any()) return [];
                var argB = args.Skip(2).First()(ctx, InvokeeFactory.EmptyArgs);
                if (propNull && !argB.Any()) return [];

                if (typeof(D) != typeof(EvaluationContext))
                {
                    var argC = args.Skip(3).First()(ctx, InvokeeFactory.EmptyArgs);
                    if (propNull && !argC.Any()) return [];

                    return Typecasts.CastTo<IEnumerable<IScopedNode>>(func(Typecasts.CastTo<A>(focus),
                                 Typecasts.CastTo<B>(argA), Typecasts.CastTo<C>(argB), Typecasts.CastTo<D>(argC)));
                }
                else
                {
                    D lastPar = (D)(object)ctx.EvaluationContext;

                    return Typecasts.CastTo<IEnumerable<IScopedNode>>(func(Typecasts.CastTo<A>(focus),
                                Typecasts.CastTo<B>(argA), Typecasts.CastTo<C>(argB), lastPar));

                }
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
                return Typecasts.CastTo<IEnumerable<IScopedNode>>(func(() => left(ctx, InvokeeFactory.EmptyArgs).BooleanEval(), () => right(ctx, InvokeeFactory.EmptyArgs).BooleanEval()));
            };
        }

        public static Invokee Return(IScopedNode value)
        {
            return (_, _) => [value];
        }

        public static Invokee Return(IEnumerable<IScopedNode> value)
        {
            return (_, __) => value;
        }

        public static Invokee Invoke(string functionName, IEnumerable<Invokee> arguments, Invokee invokee)
        {
            return (ctx, _) =>
            {
                try
                {
                    var wrappedArguments = arguments.Skip(1).Select(wrapWithNextContext);
                    return invokee(ctx, [arguments.First(), ..wrappedArguments]);
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException(
                        $"Invocation of {formatFunctionName(functionName)} failed: {e.Message}");
                }
            };
            
            Invokee wrapWithNextContext(Invokee unwrappedArgument)
            {
                return (ctx, args) =>
                {
                    return unwrappedArgument(ctx.Nest(ctx.GetThis()), args);
                };
            }

            string formatFunctionName(string name)
            {
                if (name.StartsWith(BinaryExpression.BIN_PREFIX))
                    return $"operator '{name.Substring(BinaryExpression.BIN_PREFIX_LEN)}'";
                else if (name.StartsWith(UnaryExpression.URY_PREFIX))
                    return $"operator '{name.Substring(UnaryExpression.URY_PREFIX_LEN)}'";
                else
                    return $"function '{name}'";
            }
        }

    }
}