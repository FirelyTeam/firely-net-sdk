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


        //term
        //  : invocation                                            #invocationTerm
        //  | literal                                               #literalTerm
        //  | externalConstant                                      #externalConstantTerm
        //  | '(' expression ')'                                    #parenthesizedTerm
        //  | '{' '}'                                               #nullLiteral
        //  ;
        public static readonly Parser<Expression> BracketExpr =
            (from lparen in Parse.Char('(')
             from expr in Parse.Ref(() => Expr)
             from rparen in Parse.Char(')')
             select expr)
            .Named("BracketExpr");

        public static readonly Parser<Expression> EmptyList =
            Parse.Char('{').Token().Then(c => Parse.Char('}').Token())
                    .Select(v => NewNodeListInitExpression.Empty);

        public static Parser<Expression> Invocation(Expression focus)
        {
            return Functions.Function(focus)
                .Or(Lexer.Identifier.Select(i => new ChildExpression(focus, i)))
                .XOr(Lexer.Axis.Select(a => new AxisExpression(a)))
                .Token();
        }

        public static readonly Parser<Expression> Term =
            Literal
            .Or(Invocation(AxisExpression.This))
            .XOr(Lexer.ExternalConstant.Select(n => (Expression)new ExternalConstantExpression(n))) //Was .XOr(Lexer.ExternalConstant.Select(v => Eval.ExternalConstant(v)))
            .XOr(BracketExpr)
            .XOr(EmptyList)
            .Token()
            .Named("Term");

        //expression
        // : term                                                      #termExpression
        // | expression '.' invocation                                 #invocationExpression
        // | expression '[' expression ']'                             #indexerExpression
        // | ('+' | '-') expression                                    #polarityExpression
        // | expression('*' | '/' | 'div' | 'mod') expression         #multiplicativeExpression
        // | expression('+' | '-' ) expression                        #additiveExpression
        // | expression '|' expression                                 #unionExpression
        // | expression('<=' | '<' | '>' | '>=') expression           #inequalityExpression
        // | expression('is' | 'as') typeSpecifier                    #typeExpression
        // | expression('=' | '~' | '!=' | '!~' | '<>') expression    #equalityExpression
        // | expression('in' | 'contains') expression                 #membershipExpression
        // | expression 'and' expression                               #andExpression
        // | expression('or' | 'xor') expression                      #orExpression
        // | expression 'implies' expression                           #impliesExpression
        // ;
        public static readonly Parser<Expression> InvocationExpression =
            Term.Then(expr => chainInvocOperatorRest(expr));

        private static Parser<Expression> chainInvocOperatorRest(Expression first)
        {
            if (first == null) throw new ArgumentNullException("first");

            return Parse.Or(Parse.Char('.').Then(opvalue =>
                            Invocation(first).Then(second => chainInvocOperatorRest(second))),
                                Parse.Return(first));
        }

        public static readonly Parser<Expression> IndexerExpression =
            from invoc in InvocationExpression
            from index in Parse.Contained(Expr, Parse.Char('['), Parse.Char(']'))
            select new BinaryExpression(Operator.Index, invoc, index);

            //public static readonly Parser<Evaluator> MulExpr =
            //  Parse.ChainOperator(Lexer.Mul.Or(Lexer.Div), InvocExpr, (op, left, right) => left.Infix(op, right));

        //public static readonly Parser<Evaluator> AddExpr =
        //    Parse.ChainOperator(Lexer.Add.Or(Lexer.Sub), MulExpr, (op, left, right) => left.Infix(op, right));

        //public static readonly Parser<Evaluator> JoinExpr =
        //    Parse.ChainOperator(Lexer.Union, AddExpr, (op, left, right) => left.Infix(op, right));

        //public static readonly Parser<Evaluator> CompExpr =
        //    Parse.ChainOperator(Lexer.Comp, JoinExpr, (op, left, right) => left.Infix(op, right));

        //public static readonly Parser<Evaluator> LogicExpr =
        //    Parse.ChainOperator(Lexer.Logic, CompExpr, (op, left, right) => left.Infix(op, right));

        //public static readonly Parser<Evaluator> Expr = LogicExpr;


        public static readonly Parser<Expression> Expr = InvocationExpression;

    }
}
