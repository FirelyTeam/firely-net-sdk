/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
 */

using Hl7.FhirPath.Sprache;
using System.Linq;
using System.Collections.Generic;


namespace Hl7.FhirPath.Parser
{
    internal partial class Lexer
    {
        internal static Parser<string> Operator(params string[] ops )
        {
            var first = Parse.String(ops.First()).Token();
            return ops.Skip(1).Aggregate(first, (expr, s) => expr.Or(Parse.String(s).Token()), expr => expr.Text());
        }

        internal static readonly Parser<string> PolarityOperator = Lexer.Operator("+", "-");
        internal static readonly Parser<string> MulOperator = Lexer.Operator("*","/","div","mod");
        internal static readonly Parser<string> AddOperator = Lexer.Operator("+", "-", "&");
        internal static readonly Parser<string> UnionOperator = Lexer.Operator("|");

        // NOTE: ORDER MATTERS, since otherwise shorter ops will be recognized before longer ones!
        internal static readonly Parser<string> InEqOperator = Lexer.Operator("<=", "<", ">=", ">");
        internal static readonly Parser<string> TypeOperator = Lexer.Operator("is", "as");

        // NOTE: ORDER MATTERS, since otherwise shorter ops will be recognized before longer ones!
        internal static readonly Parser<string> EqOperator = Lexer.Operator("=", "~", "!=", "!~");
        internal static readonly Parser<string> MembershipOperator = Lexer.Operator("in", "contains");
        internal static readonly Parser<string> AndOperator = Lexer.Operator("and");
        internal static readonly Parser<string> OrOperator = Lexer.Operator("or", "xor");
        internal static readonly Parser<string> ImpliesOperator = Lexer.Operator("implies");
    }     
}
