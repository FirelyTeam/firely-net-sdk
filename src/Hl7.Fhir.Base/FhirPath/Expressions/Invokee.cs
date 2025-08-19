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
using FocusCollection = System.Collections.Generic.IEnumerable<Hl7.Fhir.ElementModel.ITypedElement>;
// ReSharper disable InconsistentNaming

namespace Hl7.FhirPath.Expressions;

internal delegate FocusCollection Invokee(Closure context, IEnumerable<Invokee> arguments);

internal static class InvokeeFactory
{
    public static readonly IEnumerable<Invokee> EmptyArgs = [];

    public static FocusCollection GetThis(Closure context, IEnumerable<Invokee> _)
    {
        var result = context.GetThis();
        context.focus = result;
        return result;
    }

    public static FocusCollection GetTotal(Closure context, IEnumerable<Invokee> _)
    {
        context.focus = context.GetThis();
        return context.GetTotal();
    }

    public static FocusCollection GetContext(Closure context, IEnumerable<Invokee> _)
    {
        context.focus = context.GetThis();
        return context.GetOriginalContext();
    }

    public static FocusCollection GetResource(Closure context, IEnumerable<Invokee> _)
    {
        context.focus = context.GetThis();
        return context.GetResource();
    }

    public static FocusCollection GetRootResource(Closure context, IEnumerable<Invokee> arguments)
    {
        context.focus = context.GetThis();
        return context.GetRootResource();
    }

    public static FocusCollection GetThat(Closure context, IEnumerable<Invokee> _)
    {
        context.focus = context.GetThis();
        return context.GetThat();
    }

    public static FocusCollection GetIndex(Closure context, IEnumerable<Invokee> args)
    {
        context.focus = context.GetThis();
        return context.GetIndex();
    }

    private static readonly Predicate<FocusCollection> PROPAGATE_WHEN_EMPTY = focus => !focus.Any();
    private static readonly Predicate<FocusCollection> PROPAGATE_NEVER = _ => false;

    private static readonly Predicate<FocusCollection> PROPAGATE_EMPTY_PRIMITIVE = focus =>
    {
        var first = focus.FirstOrDefault();
        if (first is null) return true;

        // If this is not a primitive, then it is not empty.
        if (first.InstanceType is null || !char.IsLower(first.InstanceType[0])) return false;

        return first.Value is null;
    };

    private static Predicate<FocusCollection> getPropagator(bool doNullProp, Type argType) =>
        doNullProp switch
        {
            true when isPrimitiveDotNetType(argType) => PROPAGATE_EMPTY_PRIMITIVE,
            true => PROPAGATE_WHEN_EMPTY,
            _ => PROPAGATE_NEVER
        };

    private static bool isPrimitiveDotNetType(Type t) => t.IsPrimitive || t == typeof(string) || t == typeof(Decimal);


    public static Invokee Wrap<R>(Func<R> func)
    {
        return (Closure ctx, IEnumerable<Invokee> _) =>
        {
            ctx.focus = ctx.GetThis();
            return Typecasts.CastTo<FocusCollection>(func());
        };
    }

    public static Invokee Wrap<A, R>(Func<A, R> func, bool propNull)
    {
        return (Closure ctx, IEnumerable<Invokee> args) =>
        {
            if (typeof(A) != typeof(EvaluationContext))
            {
                var focus = args.First()(ctx, EmptyArgs);
                ctx.focus = focus;
                if (getPropagator(propNull, typeof(A))(focus))
                    return ElementNode.EmptyList;
                return Typecasts.CastTo<FocusCollection>(func(Typecasts.CastTo<A>(focus)));
            }
            else
            {
                ctx.focus = ctx.GetThis();
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
        return (Closure ctx, IEnumerable<Invokee> args) =>
        {
            // Get the original focus first before any processing
            var focus = args.First()(ctx, EmptyArgs);
            ctx.focus = focus;

            // Check for null propagation condition
            if (getPropagator(true, typeof(A))(focus)) return ElementNode.EmptyList;

            // For the actual function execution, we need a new Invokee that handles the arguments
            // but doesn't modify the focus for debug tracing
            // re-wrapping (as the old code did) will fully re-evaluate the focus, again. Which can be VERY expensive in some expressions.
            if (typeof(B) != typeof(EvaluationContext))
            {
                var argA = args.Skip(1).First()(ctx, EmptyArgs);
                if (getPropagator(false, typeof(B))(argA)) return ElementNode.EmptyList;

                if (typeof(C) != typeof(EvaluationContext))
                {
                    var argB = args.Skip(2).First()(ctx, EmptyArgs);
                    if (getPropagator(false, typeof(C))(argB)) return ElementNode.EmptyList;

                    return Typecasts.CastTo<FocusCollection>(func(Typecasts.CastTo<A>(focus),
                        Typecasts.CastTo<B>(argA),
                        Typecasts.CastTo<C>(argB)));
                }
                else
                {
                    C lastPar = (C)(object)ctx.EvaluationContext;
                    return Typecasts.CastTo<FocusCollection>(func(Typecasts.CastTo<A>(focus),
                        Typecasts.CastTo<B>(argA), lastPar));
                }
            }
            else
            {
                B argA = (B)(object)ctx.EvaluationContext;

                if (typeof(C) != typeof(EvaluationContext))
                {
                    var argB = args.Skip(2).First()(ctx, EmptyArgs);
                    if (getPropagator(false, typeof(C))(argB)) return ElementNode.EmptyList;

                    return Typecasts.CastTo<FocusCollection>(func(Typecasts.CastTo<A>(focus),
                        argA,
                        Typecasts.CastTo<C>(argB)));
                }
                else
                {
                    C lastPar = (C)(object)ctx.EvaluationContext;
                    return Typecasts.CastTo<FocusCollection>(func(Typecasts.CastTo<A>(focus),
                        argA, lastPar));
                }
            }
        };
    }

    public static Invokee Wrap<A, B, R>(Func<A, B, R> func, bool propNull)
    {
        return (Closure ctx, IEnumerable<Invokee> args) =>
        {
            var focus = args.First()(ctx, EmptyArgs);
            ctx.focus = focus;
            if (getPropagator(propNull, typeof(A))(focus)) return ElementNode.EmptyList;

            if (typeof(B) != typeof(EvaluationContext))
            {
                var argA = args.Skip(1).First()(ctx, EmptyArgs);
                if (getPropagator(propNull, typeof(B))(argA)) return ElementNode.EmptyList;

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
        return (Closure ctx, IEnumerable<Invokee> args) =>
        {
            var focus = args.First()(ctx, EmptyArgs);
            ctx.focus = focus;
            if (getPropagator(propNull,typeof(A))(focus)) return ElementNode.EmptyList;

            var argA = args.Skip(1).First()(ctx, EmptyArgs);
            if (getPropagator(propNull, typeof(B))(argA)) return ElementNode.EmptyList;

            if (typeof(C) != typeof(EvaluationContext))
            {
                var argB = args.Skip(2).First()(ctx, EmptyArgs);
                if (getPropagator(propNull, typeof(C))(argB)) return ElementNode.EmptyList;

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
        return (Closure ctx, IEnumerable<Invokee> args) =>
        {
            var focus = args.First()(ctx, EmptyArgs);
            ctx.focus = focus;
            if (getPropagator(propNull, typeof(A))(focus)) return ElementNode.EmptyList;

            var argA = args.Skip(1).First()(ctx, EmptyArgs);
            if (getPropagator(propNull, typeof(B))(argA)) return ElementNode.EmptyList;
            var argB = args.Skip(2).First()(ctx, EmptyArgs);
            if (getPropagator(propNull, typeof(C))(argB)) return ElementNode.EmptyList;

            if (typeof(D) != typeof(EvaluationContext))
            {
                var argC = args.Skip(3).First()(ctx, EmptyArgs);
                if (getPropagator(propNull, typeof(D))(argC)) return ElementNode.EmptyList;

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
        return (Closure ctx, IEnumerable<Invokee> args) =>
        {
            // Ignore focus
            // Arguments to functions (except iterative functions like `where` and `select` that update the value of $this) are not processed on the focus, they are processed on $this.
            ctx.focus = ctx.GetThis();
            var left = args.Skip(1).First();
            var right = args.Skip(2).First();

            // Return function that actually executes the Invokee at the last moment
            var result = Typecasts.CastTo<FocusCollection>(
                func(() => left(ctx, EmptyArgs).BooleanEval(),
                     () => right(ctx, EmptyArgs).BooleanEval()));
            return result;
        };
    }

    public static Invokee Return(ITypedElement value) => (Closure ctx, IEnumerable<Invokee> _) =>
    {
        ctx.focus = ctx.GetThis();
        return [value];
    };

    public static Invokee Return(FocusCollection value) => (Closure ctx, IEnumerable<Invokee> _) =>
    {
        ctx.focus = ctx.GetThis();
        return value;
    };

    public static Invokee Invoke(string functionName, IEnumerable<Invokee> arguments, Invokee invokee)
    {
        return (Closure ctx, IEnumerable<Invokee> _) =>
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
            return (Closure ctx, IEnumerable<Invokee> args) =>
            {
                // Bring the context outside the call so that it is created before calling the invokee
                // so that the debug tracer which will be injected gets the correct context object in it.
                var newContext = ctx.Nest(ctx.GetThis());
                var result = unwrappedArgument(newContext, args);
                return result;
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