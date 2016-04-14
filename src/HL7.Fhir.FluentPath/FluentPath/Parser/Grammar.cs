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
using HL7.Fhir.FluentPath.FluentPath.Expressions;

namespace Hl7.Fhir.FluentPath.Parser
{
    internal class Grammar
    {
        // literal
        //  : ('true' | 'false')                                    #booleanLiteral
        //  | STRING                                                #stringLiteral
        //  | NUMBER                                                #numberLiteral
        //  | DATETIME                                              #dateTimeLiteral
        //  | TIME                                                  #timeLiteral
        //  ;
        public static readonly Parser<ConstantExpression> Literal =
            Lexer.String.Select(v => new ConstantExpression(v, FluentPathType.String))
                .XOr(Lexer.DateTime.Select(v => new ConstantExpression(v, FluentPathType.DateTime)))
                .XOr(Lexer.Time.Select(v => new ConstantExpression(v, FluentPathType.Time)))
                .XOr(Lexer.Bool.Select(v => new ConstantExpression(v, FluentPathType.Bool)))
                .Or(Lexer.DecimalNumber.Select(v => new ConstantExpression(v, FluentPathType.Decimal)))
                .Or(Lexer.IntegerNumber.Select(v => new ConstantExpression(v, FluentPathType.Integer)));

        // term
        //      : invocation                                            #invocationTerm
        //      | literal                                               #literalTerm
        //      | externalConstant                                      #externalConstantTerm
        //      | '(' expression ')'                                    #parenthesizedTerm
        //      ;
        public static readonly Parser<Evaluator> BracketExpr =
            (from lparen in Parse.Char('(')
             from expr in Parse.Ref(() => Expr)
             from rparen in Parse.Char(')')
             select expr)
            .Named("BracketExpr");

        public static readonly Parser<Evaluator> EmptyList =
            Parse.Char('{').Token().Then(c => Parse.Char('}').Token()).Select(v => Eval.Return(FhirValueList.Create()));

        public static readonly Parser<Evaluator> Term =
            Literal.Select(v => Eval.Return(v))
            .Or(Path.Invoc)
            .XOr(Lexer.ExternalConstant.Select(v => Eval.ExternalConstant(v)))
            .XOr(BracketExpr)
            .XOr(EmptyList)
            .Token()
            .Named("Term");

        //expr:
        //  term '.' invoc |
        //  expr ('*' | '/') expr |
        //  expr ('+' | '-') expr |
        //  expr ('|' | '&') expr |
        //  expr COMP expr |
        //  expr LOGIC expr;
        public static readonly Parser<Evaluator> InvocExpr =
            from term in Term
            from invoc in 
                (from firstOp in Parse.Char('.')
                 from invocs in Parse.ChainOperator(Parse.Char('.'), Path.Invoc, (op, left, right) => left.Then(right))
                 select invocs).Optional()
            select invoc.IsEmpty ? term : term.Then(invoc.Get());

        public static readonly Parser<Evaluator> MulExpr =
          Parse.ChainOperator(Lexer.Mul.Or(Lexer.Div), InvocExpr, (op, left, right) => left.Infix(op, right));

        public static readonly Parser<Evaluator> AddExpr =
            Parse.ChainOperator(Lexer.Add.Or(Lexer.Sub), MulExpr, (op, left, right) => left.Infix(op, right));

        public static readonly Parser<Evaluator> JoinExpr =
            Parse.ChainOperator(Lexer.Union, AddExpr, (op, left, right) => left.Infix(op, right));

        public static readonly Parser<Evaluator> CompExpr =
            Parse.ChainOperator(Lexer.Comp, JoinExpr, (op, left, right) => left.Infix(op, right));

        public static readonly Parser<Evaluator> LogicExpr =
            Parse.ChainOperator(Lexer.Logic, CompExpr, (op, left, right) => left.Infix(op, right));

        public static readonly Parser<Evaluator> Expr = LogicExpr;    
    }
}
