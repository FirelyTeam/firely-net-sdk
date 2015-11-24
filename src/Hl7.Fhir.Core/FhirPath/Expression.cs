/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */
using System;
using System.Collections.Generic;
using System.Linq;
using Sprache;

namespace Hl7.Fhir.FhirPath
{
    internal class Expression
    {
        // fpconst: STRING |
        //   '-'? NUMBER |
        //   BOOL |
        //   CONST;
        public static readonly Parser<string> FpConst =
            Lexer.String
            .XOr(Lexer.Number)
            .XOr(Lexer.Bool)
            .XOr(Lexer.Const);

        public static Parser<string> makeOperator(Parser<string> left, char op, Parser<string> right)
        {
            return
                from l in left
                from o in Parse.Char(op).Once().Text()
                from r in right
                select l + o + r;
        }

        // term:
        //   '(' expr ')' |
        //   predicate |
        //   fpconst;
        public static readonly Parser<string> BracketExpr =
            from lparen in Parse.Char('(')
            from expr in Parse.Ref(() => LogicExpr)
            from rparen in Parse.Char(')')
            select "(" + expr + ")";

        public static readonly Parser<string> Term =
            BracketExpr
             .Or(Path.Predicate)
             .XOr(FpConst);

        //expr:
        //  term |
        //  expr('*' | '/') expr |
        //  expr('+' | '-') expr |
        //  expr('|' | '&') expr |
        //  expr COMP expr |
        //  expr LOGIC expr;
        public static readonly Parser<string> MulExpr =
            Parse.ChainOperator(Parse.Chars("*/"), Term, (op, left, right) => left + " " + op + " " + right);

        public static readonly Parser<string> AddExpr =
            Parse.ChainOperator(Parse.Chars("+-"), MulExpr, (op, left, right) => left + " " + op + " " + right);

        public static readonly Parser<string> JoinExpr =
            Parse.ChainOperator(Parse.Chars("|&"), AddExpr, (op, left, right) => left + " " + op + " " + right);

        public static readonly Parser<string> CompExpr =
            Parse.ChainOperator(Lexer.Comp, JoinExpr, (op, left, right) => left + " " + op + " " + right);

        public static readonly Parser<string> LogicExpr =
            Parse.ChainOperator(Lexer.Logic, CompExpr, (op, left, right) => left + " " + op + " " + right);

        public static readonly Parser<string> Expr = LogicExpr.End();
     

    }
}
