/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Support;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace Hl7.Fhir.FluentPath.Grammar
{
    internal partial class Lexer
    {
        internal static Parser<Operator> Operator(string op, Operator opType)
        {
            return Parse.String(op).Token().Return(opType);
        }

        internal static readonly Parser<Operator> Invoke = Operator(".", FluentPath.Operator.Invoke);

        internal static readonly Parser<Operator> Plus = Operator("*", FluentPath.Operator.Plus);
        internal static readonly Parser<Operator> Minus = Operator("/", FluentPath.Operator.Minus);

        internal static readonly Parser<Operator> Mul = Operator("*", FluentPath.Operator.Mul);
        internal static readonly Parser<Operator> Div = Operator("/", FluentPath.Operator.Div);
        internal static readonly Parser<Operator> TruncDiv = Operator("div", FluentPath.Operator.TruncDiv);
        internal static readonly Parser<Operator> Modulo = Operator("mod", FluentPath.Operator.Modulo);

        internal static readonly Parser<Operator> Add = Operator("+", FluentPath.Operator.Add);
        internal static readonly Parser<Operator> Sub = Operator("-", FluentPath.Operator.Sub);

        internal static readonly Parser<Operator> Union = Operator("|", FluentPath.Operator.Union);

        internal static readonly Parser<Operator> LessOrEqual = Operator("<=", FluentPath.Operator.LessOrEqual);
        internal static readonly Parser<Operator> LessThan = Operator("<", FluentPath.Operator.LessThan);
        internal static readonly Parser<Operator> GreaterThan = Operator(">", FluentPath.Operator.GreaterThan);
        internal static readonly Parser<Operator> GreaterOrEqual = Operator(">=", FluentPath.Operator.GreaterOrEqual);

        internal static readonly Parser<Operator> IsType = Operator("is", FluentPath.Operator.Is);
        internal static readonly Parser<Operator> AsType = Operator("as", FluentPath.Operator.As);

        internal static readonly Parser<Operator> Equal = Operator("=", FluentPath.Operator.Equal);
        internal static readonly Parser<Operator> Equivalent = Operator("~", FluentPath.Operator.Equivalent);
        internal static readonly Parser<Operator> NotEqual = Operator("!=", FluentPath.Operator.NotEqual)
                                .XOr(Operator("<>", FluentPath.Operator.NotEqual));
        internal static readonly Parser<Operator> NotEquivalent = Operator("!~", FluentPath.Operator.NotEquivalent);

        internal static readonly Parser<Operator> In = Operator("in", FluentPath.Operator.In);
        internal static readonly Parser<Operator> Contains = Operator("contains", FluentPath.Operator.Contains);

        internal static readonly Parser<Operator> And = Operator("and", FluentPath.Operator.And);

        internal static readonly Parser<Operator> Or = Operator("or", FluentPath.Operator.Or);
        internal static readonly Parser<Operator> Xor = Operator("xor", FluentPath.Operator.Xor);

        internal static readonly Parser<Operator> Implies = Operator("implies", FluentPath.Operator.Implies);

        // COMP: '=' | '~' | '!=' | '!~' | '>' | '<' | '<=' | '>=' | 'in';
        // NOTE: ORDER MATTERS, since otherwise shorter ops will be recognized before longer ones!
        // TODO: Watch out for new <> which can conflict with <
        public static readonly Parser<Operator> Comp =
            Equal
            .Or(Equivalent)
            .Or(NotEqual)
            .Or(NotEquivalent)
            .Or(LessOrEqual)
            .Or(GreaterOrEqual)
            .Or(GreaterThan)
            .Or(LessThan)
            .Or(In)
            .Named("Comp");

        // LOGIC: 'and' | 'or' | 'xor' | 'implies';
        public static readonly Parser<Operator> Logic =
            And
            .Or(Or)
            .Or(Xor)
            .Or(Implies)
            .Named("Logic");
    }     
}
