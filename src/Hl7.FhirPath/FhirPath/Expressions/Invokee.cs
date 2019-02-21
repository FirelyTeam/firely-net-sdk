/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.FhirPath.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using System.Reflection;

namespace Hl7.FhirPath.Expressions
{
    internal delegate IEnumerable<ITypedElement> Invokee(EvaluationContext ctx, IList<Invokee> arguments);

    internal static class InvokeeFactory
    {
        public static readonly IList<Invokee> EmptyArgs = new List<Invokee>();

        public static Invokee Wrap<R>(Func<R> func)
        {
            return (ctx, args) =>
            {
                return Typecasts.CastTo<IEnumerable<ITypedElement>>(func());
            };
        }

        public static Invokee Wrap<A, R>(Func<A, R> func, bool propNull)
        {
            return (ctx, args) =>
            {
                if (typeof(A) != typeof(EvaluationContext))
                {
                    var focus = args[0](ctx, InvokeeFactory.EmptyArgs);
                    if (propNull && !focus.Any()) return FhirValueList.Empty;

                    return Typecasts.CastTo<IEnumerable<ITypedElement>>(func(Typecasts.CastTo<A>(focus)));
                }
                else
                {
                    A lastPar = (A)(object)ctx;
                    return Typecasts.CastTo<IEnumerable<ITypedElement>>(func(lastPar));
                }
            };
        }

        public static Invokee Wrap<A, B, R>(Func<A, B, R> func, bool propNull)
        {
            return (ctx, args) =>
            {
                var focus = args[0](ctx, InvokeeFactory.EmptyArgs);
                if (propNull && !focus.Any()) return FhirValueList.Empty;

                if (typeof(B) != typeof(EvaluationContext))
                {
                    var argA = args[1](ctx, InvokeeFactory.EmptyArgs);
                    if (propNull && !argA.Any()) return FhirValueList.Empty;

                    return Typecasts.CastTo<IEnumerable<ITypedElement>>(func(Typecasts.CastTo<A>(focus), Typecasts.CastTo<B>(argA)));
                }
                else
                {
                    B lastPar = (B)(object)ctx;
                    return Typecasts.CastTo<IEnumerable<ITypedElement>>(func(Typecasts.CastTo<A>(focus), lastPar));
                }
            };
        }

        public static Invokee Wrap<A, B, C, R>(Func<A, B, C, R> func, bool propNull)
        {
            return (ctx, args) =>
            {
                var focus = args[0](ctx, InvokeeFactory.EmptyArgs);
                if (propNull && !focus.Any()) return FhirValueList.Empty;
                var argA = args[1](ctx, InvokeeFactory.EmptyArgs);
                if (propNull && !argA.Any()) return FhirValueList.Empty;

                if (typeof(C) != typeof(EvaluationContext))
                {
                    var argB = args[2](ctx, InvokeeFactory.EmptyArgs);
                    if (propNull && !argB.Any()) return FhirValueList.Empty;

                    return Typecasts.CastTo<IEnumerable<ITypedElement>>(func(Typecasts.CastTo<A>(focus), Typecasts.CastTo<B>(argA),
                        Typecasts.CastTo<C>(argB)));
                }
                else
                {
                    C lastPar = (C)(object)ctx;
                    return Typecasts.CastTo<IEnumerable<ITypedElement>>(func(Typecasts.CastTo<A>(focus),
                        Typecasts.CastTo<B>(argA), lastPar));
                }
            };
        }

        public static Invokee Wrap<A, B, C, D, R>(Func<A, B, C, D, R> func, bool propNull)
        {
            return (ctx, args) =>
            {
                var focus = args[0](ctx, InvokeeFactory.EmptyArgs);
                if (propNull && !focus.Any()) return FhirValueList.Empty;
                var argA = args[1](ctx, InvokeeFactory.EmptyArgs);
                if (propNull && !argA.Any()) return FhirValueList.Empty;
                var argB = args[2](ctx, InvokeeFactory.EmptyArgs);
                if (propNull && !argB.Any()) return FhirValueList.Empty;

                if (typeof(D) != typeof(EvaluationContext))
                {
                    var argC = args[3](ctx, InvokeeFactory.EmptyArgs);
                    if (propNull && !argC.Any()) return FhirValueList.Empty;

                    return Typecasts.CastTo<IEnumerable<ITypedElement>>(func(Typecasts.CastTo<A>(focus),
                                 Typecasts.CastTo<B>(argA), Typecasts.CastTo<C>(argB), Typecasts.CastTo<D>(argC)));
                }
                else
                {
                    D lastPar = (D)(object)ctx;

                    return Typecasts.CastTo<IEnumerable<ITypedElement>>(func(Typecasts.CastTo<A>(focus),
                                Typecasts.CastTo<B>(argA), Typecasts.CastTo<C>(argB), lastPar));

                }
            };
        }

        public static Invokee WrapLogic(Func<Func<bool?>, Func<bool?>, bool?> func)
        {
            return (ctx, args) =>
            {
                var left = args[1];
                var right = args[2];

                // Return function that actually executes the Invokee at the last moment
                return Typecasts.CastTo<IEnumerable<ITypedElement>>(
                    func(() => left(ctx, InvokeeFactory.EmptyArgs).BooleanEval(), () => right(ctx, InvokeeFactory.EmptyArgs).BooleanEval()));
            };
        }

        public static Invokee Return(ITypedElement value)
        {
            return (_, __) => (new[] { (ITypedElement)value });
        }

        public static Invokee Return(IEnumerable<ITypedElement> value)
        {
            return (_, __) => value;
        }

        public static Invokee Invoke(string functionName, IList<Invokee> arguments, Invokee invokee)
        {
            return (ctx, _) =>
            {
                try
                {
                    return invokee(ctx, arguments);
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException(
                        $"Invocation of {formatFunctionName(functionName)} failed: {e.Message}");
                }
            };

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