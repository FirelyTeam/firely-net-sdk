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
using HL7.Fhir.FluentPath;
using HL7.Fhir.FluentPath.Expressions;

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
            Lexer.String.Select(v => new ConstantExpression(v, TypeInfo.String))
                .XOr(Lexer.DateTime.Select(v => new ConstantExpression(v, TypeInfo.DateTime)))
                .XOr(Lexer.Time.Select(v => new ConstantExpression(v, TypeInfo.Time)))
                .XOr(Lexer.Bool.Select(v => new ConstantExpression(v, TypeInfo.Boolean)))
                .Or(Lexer.DecimalNumber.Select(v => new ConstantExpression(v, TypeInfo.Decimal)))
                .Or(Lexer.IntegerNumber.Select(v => new ConstantExpression(v, TypeInfo.Integer)));


        //term
        //  : invocation                                            #invocationTerm
        //  | literal                                               #literalTerm
        //  | externalConstant                                      #externalConstantTerm
        //  | '(' expression ')'                                    #parenthesizedTerm
        //  | '{' '}'                                               #nullLiteral
        //  ;
        public static readonly Parser<Expression> BracketExpr =
            (from lparen in Parse.Char('(')
             from expr in Parse.Ref(() => Expression)
             from rparen in Parse.Char(')')
             select expr)
            .Named("BracketExpr");

        public static readonly Parser<Expression> EmptyList =
            Parse.Char('{').Token().Then(c => Parse.Char('}').Token())
                    .Select(v => NewNodeListInitExpression.Empty);

        public static Parser<Expression> Function(Expression context)
        {
            return
                from n in Lexer.Identifier.Select(name => name)
                from lparen in Parse.Char('(').Token()
                from paramList in Parse.Ref(() => Grammar.Expression.Named("parameter")).DelimitedBy(Parse.Char(',').Token()).Optional()
                from rparen in Parse.Char(')').Token()
                select new FunctionCallExpression(context, n, TypeInfo.Any, paramList.GetOrElse(Enumerable.Empty<Expression>()));
        }


        public static Parser<Expression> Invocation(Expression focus)
        {
            return Function(focus)
                .Or(Lexer.Identifier.Select(i => new ChildExpression(focus, i)))
                .XOr(Lexer.Axis.Select(a => new AxisExpression(a)))
                .Token();
        }

        public static readonly Parser<Expression> Term =
            Literal
            .Or(Invocation(AxisExpression.This))
            .XOr(Lexer.ExternalConstant.Select(n => (Expression)new VariableRefExpression(n))) //Was .XOr(Lexer.ExternalConstant.Select(v => Eval.ExternalConstant(v)))
            .XOr(BracketExpr)
            .XOr(EmptyList)
            .Token()
            .Named("Term");


        public static readonly Parser<TypeInfo> TypeSpecifier =
            Lexer.QualifiedIdentifier.Select(qi => TypeInfo.ByName(qi)).Token();

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

        // | expression '.' invocation                                 #invocationExpression
        public static readonly Parser<Expression> InvocationExpression =
            Term.Then(expr => chainInvocOperatorRest(expr));

        private static Parser<Expression> chainInvocOperatorRest(Expression first)
        {
            if (first == null) throw new ArgumentNullException("first");

            return Parse.Or(Parse.Char('.').Then(opvalue =>
                            Invocation(first).Then(second => chainInvocOperatorRest(second))),
                                Parse.Return(first));
        }

        // | expression '[' expression ']'                             #indexerExpression
        public static readonly Parser<Expression> IndexerExpression =
            InvocationExpression.Then(
                invoc => Parse.Contained(Expression, Parse.Char('['), Parse.Char(']')).Select(ix => new IndexerExpression(invoc, ix))
                .Or(Parse.Return(invoc)));

        // | ('+' | '-') expression                                    #polarityExpression
        public static readonly Parser<Expression> PolarityExpression =
            from op in Lexer.PolarityOperator.Optional()
            from indexer in IndexerExpression
            select op.IsEmpty ? indexer : new UnaryExpression(op.Get(), indexer);

        public static Parser<Expression> BinaryExpression(Parser<string> oper, Parser<Expression> operands)
        {
            return Parse.ChainOperator(oper, operands, (op, left, right) => new BinaryExpression(op, left, right));
        }

        // | expression('*' | '/' | 'div' | 'mod') expression         #multiplicativeExpression
        public static readonly Parser<Expression> MulExpression = BinaryExpression(Lexer.MulOperator, PolarityExpression);

        // | expression('+' | '-' ) expression                        #additiveExpression
        public static readonly Parser<Expression> AddExpression = BinaryExpression(Lexer.AddOperator, MulExpression);

        // | expression '|' expression                                 #unionExpression
        public static readonly Parser<Expression> UnionExpression = BinaryExpression(Lexer.UnionOperator, AddExpression);

        // | expression('<=' | '<' | '>' | '>=') expression           #inequalityExpression
        public static readonly Parser<Expression> InEqExpression = BinaryExpression(Lexer.InEqOperator, UnionExpression);

        // | expression('is' | 'as') typeSpecifier                    #typeExpression
        public static readonly Parser<Expression> TypeExpression =
            InEqExpression.Then(
                    ineq => (from isas in Lexer.TypeOperator
                             from tp in TypeSpecifier
                             select new TypeBinaryExpression(isas, ineq, tp))
                    .Or(Parse.Return(ineq)));

        // | expression('=' | '~' | '!=' | '!~' | '<>') expression    #equalityExpression
        public static readonly Parser<Expression> EqExpression = BinaryExpression(Lexer.EqOperator, TypeExpression);

        // | expression 'and' expression                               #andExpression
        public static readonly Parser<Expression> AndExpression = BinaryExpression(Lexer.AndOperator, EqExpression);

        // | expression('or' | 'xor') expression                      #orExpression
        public static readonly Parser<Expression> OrExpression = BinaryExpression(Lexer.OrOperator, AndExpression);

        // | expression 'implies' expression                           #impliesExpression
        public static readonly Parser<Expression> ImpliesExpression = BinaryExpression(Lexer.ImpliesOperator, OrExpression);

        public static readonly Parser<Expression> Expression = ImpliesExpression;
    }
}
