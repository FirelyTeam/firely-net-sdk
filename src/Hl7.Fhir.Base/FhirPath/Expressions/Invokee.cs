/*
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.ElementModel;
using Hl7.FhirPath.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using FocusCollection = System.Collections.Generic.IEnumerable<Hl7.Fhir.ElementModel.PocoNode>;
// ReSharper disable InconsistentNaming

namespace Hl7.FhirPath.Expressions;

internal delegate FocusCollection Invokee(Closure context, IEnumerable<Invokee> arguments);

internal static class InvokeeFactory
{
    public static readonly IEnumerable<Invokee> EmptyArgs = [];

    public static FocusCollection GetThis(Closure context, IEnumerable<Invokee> _) => context.GetThis();

    public static FocusCollection GetTotal(Closure context, IEnumerable<Invokee> _) => context.GetTotal();

    public static FocusCollection GetContext(Closure context, IEnumerable<Invokee> _) =>
        context.GetOriginalContext();

    public static FocusCollection GetResource(Closure context, IEnumerable<Invokee> _) =>
        context.GetResource();

    public static FocusCollection GetRootResource(Closure context, IEnumerable<Invokee> arguments) =>
        context.GetRootResource();

    public static FocusCollection GetThat(Closure context, IEnumerable<Invokee> _) =>
        context.GetThat();

    public static FocusCollection GetIndex(Closure context, IEnumerable<Invokee> args) =>
        context.GetIndex();
    
    private static readonly Predicate<FocusCollection> PROPAGATE_NEVER = _ => false;

    private static readonly Predicate<FocusCollection> PROPAGATE_EMPTY = focus =>
    {
        var first = focus.FirstOrDefault();
        return first is null or PrimitiveNode { Value: null };
    };

    private static Predicate<FocusCollection> getPropagator(bool doNullProp) =>
        doNullProp ? PROPAGATE_EMPTY : PROPAGATE_NEVER;

    public static Invokee Wrap<R>(Func<R> func)
    {
        return (_, _) => Typecasts.CastTo<FocusCollection>(func());
    }

    public static Invokee Wrap<A, R>(Func<A, R> func, bool propNull)
    {
        return (ctx, args) =>
        {
            if (typeof(A) != typeof(EvaluationContext))
            {
                var focus = args.First()(ctx, EmptyArgs);
                if (getPropagator(propNull)(focus)) return [];
                return Typecasts.CastTo<FocusCollection>(func(Typecasts.CastTo<A>(focus)));
            }

            A lastPar = (A)(object)ctx.EvaluationContext;
            return Typecasts.CastTo<FocusCollection>(func(lastPar));
        };
    }

    /// <summary>
    /// Wraps a function that is only supposed to propagate null in the focus, not in the other arguments.
    /// </summary>
    internal static Invokee WrapWithPropNullForFocus<A, B, C, R>(Func<A, B, C, R> func)
    {
        return (ctx, args) =>
        {
            // propagate only null for focus
            var focus = args.First()(ctx, EmptyArgs);
            if (getPropagator(true)(focus)) return [];

            return Wrap(func, false)(ctx, args);
        };
    }

    public static Invokee Wrap<A, B, R>(Func<A, B, R> func, bool propNull)
    {
        return (ctx, args) =>
        {
            var focus = args.First()(ctx, EmptyArgs);
            if (getPropagator(propNull)(focus)) return [];

            if (typeof(B) != typeof(EvaluationContext))
            {
                var argA = args.Skip(1).First()(ctx, EmptyArgs);
                if (getPropagator(propNull)(argA)) return [];

                return Typecasts.CastTo<FocusCollection>(func(Typecasts.CastTo<A>(focus), Typecasts.CastTo<B>(argA)));
            }
            else
            {
                B lastPar = (B)(object)ctx.EvaluationContext;
                return Typecasts.CastTo<FocusCollection>(func(Typecasts.CastTo<A>(focus), lastPar));
            }
        };
    }

    public static Invokee Wrap<A, B, C, R>(Func<A, B, C, R> func, bool propNull)
    {
        return (ctx, args) =>
        {
            var focus = args.First()(ctx, EmptyArgs);
            if (getPropagator(propNull)(focus)) return [];

            var argA = args.Skip(1).First()(ctx, EmptyArgs);
            if (getPropagator(propNull)(argA)) return [];

            if (typeof(C) != typeof(EvaluationContext))
            {
                var argB = args.Skip(2).First()(ctx, EmptyArgs);
                if (getPropagator(propNull)(argB)) return [];

                return Typecasts.CastTo<FocusCollection>(func(Typecasts.CastTo<A>(focus), Typecasts.CastTo<B>(argA),
                    Typecasts.CastTo<C>(argB)));
            }
            else
            {
                C lastPar = (C)(object)ctx.EvaluationContext;
                return Typecasts.CastTo<FocusCollection>(func(Typecasts.CastTo<A>(focus),
                    Typecasts.CastTo<B>(argA), lastPar));
            }
        };
    }

    public static Invokee Wrap<A, B, C, D, R>(Func<A, B, C, D, R> func, bool propNull)
    {
        return (ctx, args) =>
        {
            var focus = args.First()(ctx, EmptyArgs);
            if (getPropagator(propNull)(focus)) return [];

            var argA = args.Skip(1).First()(ctx, EmptyArgs);
            if (getPropagator(propNull)(argA)) return [];
            var argB = args.Skip(2).First()(ctx, EmptyArgs);
            if (getPropagator(propNull)(argB)) return [];

            if (typeof(D) != typeof(EvaluationContext))
            {
                var argC = args.Skip(3).First()(ctx, EmptyArgs);
                if (getPropagator(propNull)(argC)) return [];

                return Typecasts.CastTo<FocusCollection>(func(Typecasts.CastTo<A>(focus),
                    Typecasts.CastTo<B>(argA), Typecasts.CastTo<C>(argB), Typecasts.CastTo<D>(argC)));
            }
            else
            {
                D lastPar = (D)(object)ctx.EvaluationContext;

                return Typecasts.CastTo<FocusCollection>(func(Typecasts.CastTo<A>(focus),
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
            return Typecasts.CastTo<FocusCollection>(
                func(() => left(ctx, EmptyArgs).BooleanEval(), () => right(ctx, EmptyArgs).BooleanEval()));
        };
    }

    public static Invokee Return(FocusCollection value) => (_, _) => value;

    public static Invokee Invoke(string functionName, IEnumerable<Invokee> arguments, Invokee invokee)
    {
        return (ctx, _) =>
        {
            try
            {
                var wrappedArguments = arguments.Skip(1).Select(wrapWithNextContext);
                return invokee(ctx, [arguments.First(),.. wrappedArguments]);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(
                    $"Invocation of {formatFunctionName(functionName)} failed: {e.Message}");
            }
        };

        static Invokee wrapWithNextContext(Invokee unwrappedArgument)
        {
            return (ctx, args) => unwrappedArgument(ctx.Nest(ctx.GetThis()), args);
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