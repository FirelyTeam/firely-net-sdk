/*
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using P = Hl7.Fhir.ElementModel.Types;
using FocusCollection = System.Collections.Generic.IEnumerable<Hl7.Fhir.Model.PocoNode>;

namespace Hl7.FhirPath.Expressions
{
    internal class DynaDispatcher
    {
        public DynaDispatcher(string name, SymbolTable scope)
        {
            _scope = scope;
            _name = name;
            _firstOperand = FirstArithmeticOperand(name);
        }

        private readonly string _name;

        // The index of the first argument that is checked for numbers outside the range of a .NET decimal, or -1.
        private readonly int _firstOperand;
        private readonly SymbolTable _scope;

        public FocusCollection Dispatcher(Closure context, IEnumerable<Invokee> args)
        {
            var actualArgs = new List<FocusCollection>();

            var focus = args.First()(context, InvokeeFactory.EmptyArgs);
            context.focus = focus;
            if (!focus.Any()) return [];

            actualArgs.Add(focus);
            var newCtx = context.Nest(focus);

            actualArgs.AddRange(args.Skip(1).Select(a => a(newCtx, InvokeeFactory.EmptyArgs)));
            if (actualArgs.Any(aa => !aa.Any())) return [];

            // Arithmetic on a decimal or quantity outside the range of a .NET decimal (e.g. 1e999999999) gives empty.
            if (_firstOperand >= 0 && actualArgs.Skip(_firstOperand).Any(IsOutOfRangeNumber)) return [];

            var entry = _scope.DynamicGet(_name, actualArgs);

            if (entry != null)
            {
                try
                {
                    // The Get() here should never fail, since we already know there's a (dynamic) matching candidate
                    // Need to clean up this duplicate logic later
                    var argFuncs = actualArgs.Select(InvokeeFactory.Return);
                    var result = entry(context, argFuncs);

                    // Dynamically dispatched function arguments aren't wrapped
                    // for the debug/trace, so need to manually put the focus back
                    context.focus = focus;
                    return result;
                }
                catch (TargetInvocationException tie)
                {
                    // Unwrap the very non-informative T.I.E, and throw the nested exception instead
                    throw tie.InnerException;
                }
            }
            else
            {
                //TODO: Make error reporting better
                throw Error.Argument(noMatchError(actualArgs));
            }
        }

        /// <summary>
        /// The operators and functions that do arithmetic. Given a decimal or quantity outside the range of a .NET decimal
        /// (e.g. 1e999999999), they return empty.
        /// </summary>
        internal static readonly HashSet<string> ARITHMETIC_FUNCTIONS =
        [
            "unary.-", "unary.+", "binary.*", "binary./", "binary.+", "binary.-", "binary.div", "binary.mod",
            "abs", "ceiling", "exp", "floor", "ln", "log", "power", "round", "sqrt", "truncate",
            "lowBoundary", "highBoundary"
        ];

        /// <summary>
        /// For an arithmetic operator or function, the index of its first operand: operators get the focus as their first
        /// argument, which is not an operand, while functions operate on the focus. Returns -1 for other functions.
        /// </summary>
        internal static int FirstArithmeticOperand(string name) =>
            !ARITHMETIC_FUNCTIONS.Contains(name) ? -1
            : name.StartsWith(BinaryExpression.BIN_PREFIX) || name.StartsWith(UnaryExpression.URY_PREFIX) ? 1
            : 0;

        internal static bool IsOutOfRangeNumber(FocusCollection arg)
        {
            using var items = arg.GetEnumerator();
            if (!items.MoveNext()) return false;
            var item = items.Current;
            if (items.MoveNext()) return false;  // a collection, which the function will reject in its own way

            // Out-of-range values are kept as a string literal in FHIR decimals, and as a System value in System types.
            return item switch
            {
                PrimitiveNode { Primitive: FhirDecimal { JsonValue: string s } } => isOutOfRangeLiteral(s),
                PrimitiveNode { Primitive.JsonValue: P.Decimal { IsOutOfRange: true } or P.Quantity { IsOutOfRange: true } } => true,
                { Poco: Quantity { ValueElement.JsonValue: string s } } => isOutOfRangeLiteral(s),
                _ => false
            };

            static bool isOutOfRangeLiteral(string s) => P.Decimal.TryParseExtended(s, out var d) && d.IsOutOfRange;
        }

        private string noMatchError(IEnumerable<object> arguments)
        {
            string result;

            if (!arguments.Any())
                return "(no signature)";

            result = "on focus of type '{0}'".FormatWith(Typecasts.ReadableFhirPathName(arguments.First()));

            if (arguments.Skip(1).Any())
            {
                result = "with parameters of type " +
                        String.Join(" and ",
                        arguments.Skip(1).Select(a => Typecasts.ReadableFhirPathName(a)));
            }

            return "cannot be called " + result;
        }
    }
}
