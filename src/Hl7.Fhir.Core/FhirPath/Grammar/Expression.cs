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

namespace Hl7.Fhir.FhirPath.Grammar
{
    internal class Expression
    {
        // fpconst: STRING |
        //   '-'? NUMBER |
        //   BOOL |
        //   CONST |
        //   DATETIME;
        public static readonly Parser<Evaluator> FpConst =
            Lexer.String.Select(s => Eval.Constant(s))
            .XOr(Lexer.DateTime.Select(dt => Eval.Constant(dt)))
            .XOr(Lexer.Number.Select(n => Eval.Constant(n)))
            .XOr(Lexer.Bool.Select(b => Eval.Constant(b)))
            .XOr(Lexer.Const.Select(s => Eval.Constant(s)));

        // term:
        //   '(' expr ')' |
        //   predicate |
        //   fpconst;
        public static readonly Parser<Evaluator> BracketExpr =
            (from lparen in Parse.Char('(')
             from expr in Parse.Ref(() => Expr)
             from rparen in Parse.Char(')')
             select expr)
            .Named("BracketExpr");

        public static readonly Parser<Evaluator> Term =
            FpConst
            .XOr(BracketExpr)
            .Or(Path.Predicate)
            .Named("Term");

        //expr:
        //  term |
        //  expr('*' | '/') expr |
        //  expr('+' | '-') expr |
        //  expr('|' | '&') expr |
        //  expr COMP expr |
        //  expr LOGIC expr;

        //public static readonly Parser<string> MulExpr =
        //    Parse.ChainOperator(Parse.Chars("*/"), Term, (op, left, right) => left + " " + op + " " + right);

        //public static readonly Parser<string> AddExpr =
        //    Parse.ChainOperator(Parse.Chars("+-"), MulExpr, (op, left, right) => left + " " + op + " " + right);

        //public static readonly Parser<string> JoinExpr =
        //    Parse.ChainOperator(Parse.Chars("|&"), AddExpr, (op, left, right) => left + " " + op + " " + right);

        public static readonly Parser<Evaluator> CompExpr =
            Parse.ChainOperator(Lexer.Comp, Term, (op, left, right) => Eval.Compare(op, left, right));

        //public static readonly Parser<string> CompExpr =
        //    Parse.ChainOperator(Lexer.Comp, JoinExpr, (op, left, right) => left + " " + op + " " + right);

        //public static readonly Parser<string> LogicExpr =
        //    Parse.ChainOperator(Lexer.Logic, CompExpr, (op, left, right) => left + " " + op + " " + right);

        public static readonly Parser<Evaluator> Expr = CompExpr;    
    }
}
