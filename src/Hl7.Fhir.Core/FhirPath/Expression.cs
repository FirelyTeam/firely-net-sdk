using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public static readonly Parser<string> Term =
            (from lparen in Parse.Char('(')
             from expr in Parse.Ref(() => LogicExpr)
             from rparen in Parse.Char(')')
             select expr)
             .XOr(FpConst);

        //expr:
        //  term |
        //  expr('*' | '/') expr |
        //  expr('+' | '-') expr |
        //  expr('|' | '&') expr |
        //  expr COMP expr |
        //  expr LOGIC expr;
        public static readonly Parser<string> MulExpr =
            Parse.ChainOperator(Parse.Char('*'), Term, (op, left, right) => left + " " + op + " " + right);

        public static readonly Parser<string> AddExpr =
            Parse.ChainOperator(Parse.Char('+'), MulExpr, (op, left, right) => left + " " + op + " " + right);

        public static readonly Parser<string> JoinExpr =
            Parse.ChainOperator(Parse.Char('|'), AddExpr, (op, left, right) => left + " " + op + " " + right);

        public static readonly Parser<string> CompExpr =
            Parse.ChainOperator(Parse.Char('>'), JoinExpr, (op, left, right) => left + " " + op + " " + right);

        public static readonly Parser<string> LogicExpr =
            Parse.ChainOperator(Parse.String("and"), CompExpr, (op, left, right) => left + " " + op + " " + right);

        public static readonly Parser<string> Expr =
            LogicExpr.End();
    }
}
