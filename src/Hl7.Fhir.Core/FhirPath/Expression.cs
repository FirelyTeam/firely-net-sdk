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
            Lexer.String.XOr(Lexer.Bool).XOr(Lexer.Const);


        public static Parser<string> makeOperator(Parser<string> left, char op, Parser<string> right)
        {
            return
                from l in left
                from o in Parse.Char(op).Once().Text()
                from r in right
                select l + o + r;
        }

        public static readonly Parser<string> Term = FpConst;

        //expr:
        //  expr('*' | '/') expr |
        //  expr('+' | '-') expr |
        //  expr('|' | '&') expr |
        //  expr COMP expr |
        //  expr LOGIC expr |
        //  '(' expr ')' |
        //  predicate |
        //  fpconst;
        //public static readonly Parser<string> Expression =
        //    Parse.ChainOperator(Parse.Char('*'), FpConst, (op, )
        //    makeOperator(FpConst, '*', FpConst).Or(makeOperator(FpConst, '/', FpConst));
    }
}
