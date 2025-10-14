/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
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
            // Need to ensure that operators don't accidentally match a part
            // of an input stream. E.g. 'as' should not match 'asc'.
            var parsers = ops.Select(op => 
            {
                var baseParser = Parse.String(op);
                
                // For operators that are alphabetic (keywords), ensure they're followed by word boundaries
                if (op.All(char.IsLetter))
                {
                    return from matched in baseParser
                           from boundary in Parse.Not(Parse.LetterOrDigit).Return("")
                           select matched;
                }
                else
                {
                    return baseParser;
                }
            });

            return parsers.Aggregate((p1, p2) => p1.Or(p2)).Text();
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
