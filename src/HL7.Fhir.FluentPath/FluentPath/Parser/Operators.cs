/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Sprache;
using System.Linq;
using System.Collections.Generic;


namespace Hl7.Fhir.FluentPath.Parser
{
    internal partial class Lexer
    {
        internal static Parser<string> Operator(params string[] ops )
        {
            var first = Parse.String(ops.First());
            return ops.Skip(1).Aggregate(first, (expr, s) => expr.Or(Parse.String(s)), expr => expr.Text());
        }

        internal static readonly Parser<string> PolarityOperator = Lexer.Operator("+", "-");
        internal static readonly Parser<string> MulOperator = Lexer.Operator("*","/","div","mod");
        internal static readonly Parser<string> AddOperator = Lexer.Operator("+", "-");
        internal static readonly Parser<string> UnionOperator = Lexer.Operator("|");
        internal static readonly Parser<string> InEqOperator = Lexer.Operator("<=", "<", ">=", ">");
        internal static readonly Parser<string> EqOperator = Lexer.Operator("=", "~", "!=", "!~", "<>");
        internal static readonly Parser<string> TypeOperator = Lexer.Operator("is", "as");

        //internal static readonly Parser<Operator> IsType = Operator("is", FluentPath.Operator.Is);
        //internal static readonly Parser<Operator> AsType = Operator("as", FluentPath.Operator.As);

        //internal static readonly Parser<Operator> In = Operator("in", FluentPath.Operator.In);
        //internal static readonly Parser<Operator> Contains = Operator("contains", FluentPath.Operator.Contains);

        //internal static readonly Parser<Operator> And = Operator("and", FluentPath.Operator.And);

        //internal static readonly Parser<Operator> Or = Operator("or", FluentPath.Operator.Or);
        //internal static readonly Parser<Operator> Xor = Operator("xor", FluentPath.Operator.Xor);

        //internal static readonly Parser<Operator> Implies = Operator("implies", FluentPath.Operator.Implies);

        // COMP: '=' | '~' | '!=' | '!~' | '>' | '<' | '<=' | '>=' | 'in';
        // NOTE: ORDER MATTERS, since otherwise shorter ops will be recognized before longer ones!
        // TODO: Watch out for new <> which can conflict with <
        //public static readonly Parser<Operator> Comp =
        //    Equal
        //    .Or(Equivalent)
        //    .Or(NotEqual)
        //    .Or(NotEquivalent)
        //    .Or(LessOrEqual)
        //    .Or(GreaterOrEqual)
        //    .Or(GreaterThan)
        //    .Or(LessThan)
        //    .Or(In)
        //    .Named("Comp");

        // LOGIC: 'and' | 'or' | 'xor' | 'implies';
        //public static readonly Parser<Operator> Logic =
        //    And
        //    .Or(Or)
        //    .Or(Xor)
        //    .Or(Implies)
        //    .Named("Logic");
    }     
}
