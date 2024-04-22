/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */
using Hl7.Fhir.Language;
using Hl7.FhirPath;
using Hl7.FhirPath.Expressions;
using Hl7.FhirPath.Sprache;
using P = Hl7.Fhir.ElementModel.Types;
using System;
using System.Linq;

namespace Hl7.FhirPath.Parser
{
    internal class Grammar
    {
        public static readonly Parser<P.Quantity> Quantity = quantityParser;
        private static IResult<P.Quantity> quantityParser(IInput i)
        {
            var current = i;
            var result = Lexer.Quantity.TokenIgnoreComments()(i);

            if (result.WasSuccessful)
            {
                var success = P.Quantity.TryParse(result.Value, out var quantity);
                if (success)
                    return Result.Success(quantity, result.Remainder);
            }

            return Result.Failure<P.Quantity>(i, $"Quantity is invalid",
                new[] { "a quantity" });

        }


        // literal
        //  : ('true' | 'false')                                    #booleanLiteral
        //  | STRING                                                #stringLiteral
        //  | NUMBER                                                #numberLiteral
        //  | DATETIME                                              #dateTimeLiteral
        //  | DATE                                                  #dateLiteral - additional to the spec
        //  | TIME                                                  #timeLiteral
        //  | quantity
        //  ;
        public static readonly Parser<ConstantExpression> Literal =
            Lexer.String.Select(v => new ConstantExpression(v, TypeSpecifier.String)).Positioned()
                .Or(Lexer.DateTime.Select(v => new ConstantExpression(v, TypeSpecifier.DateTime)).Positioned())
                .Or(Lexer.Date.Select(v => new ConstantExpression(v, TypeSpecifier.Date)).Positioned())
                .Or(Lexer.Time.Select(v => new ConstantExpression(v, TypeSpecifier.Time)).Positioned())
                .XOr(Lexer.Bool.Select(v => new ConstantExpression(v, TypeSpecifier.Boolean)).Positioned())
                .Or(Quantity.Select(v => new ConstantExpression(v, TypeSpecifier.Quantity)).Positioned())
                .Or(Lexer.DecimalNumber.Select(v => new ConstantExpression(v, TypeSpecifier.Decimal)).Positioned())
                .Or(Lexer.IntegerNumber.Select(v => new ConstantExpression(v, TypeSpecifier.Integer)).Positioned());


        //term
        //  : invocation                                            #invocationTerm
        //  | literal                                               #literalTerm
        //  | externalConstant                                      #externalConstantTerm
        //  | '(' expression ')'                                    #parenthesizedTerm
        //  | '{' '}'                                               #nullLiteral
        //  ;
        public static readonly Parser<Expression> BracketExpr =
            (from lparen in Parse.Char('(')
             from expr in Parse.Ref(() => Expression) //.Positioned()
             from rparen in Parse.Char(')')
             select new BracketExpression(expr))
            .Positioned()
            .Named("BracketExpr");

        public static readonly Parser<Expression> EmptyList =
            Parse.Char('{').TokenIgnoreComments().Then(c => Parse.Char('}').TokenIgnoreComments())
                    .Select(v => NewNodeListInitExpression.Empty)
                    .Positioned();

        public static Parser<Expression> Function(Expression context)
        {
            return
                (
                from n in Lexer.Identifier.Select(name => name)
                from lparen in Parse.Char('(').TokenIgnoreComments()
                from paramList in Parse.Ref(() => FunctionParameter(n).Named("parameter")).DelimitedBy(Parse.Char(',').TokenIgnoreComments()).Optional()
                from rparen in Parse.Char(')').TokenIgnoreComments()
                select new FunctionCallExpression(context, n, TypeSpecifier.Any, paramList.GetOrElse(Enumerable.Empty<Expression>())))
                .Positioned();
        }

        public static Parser<Expression> FunctionParameter(string name) =>
            // Make exception for is() and as() FUNCTIONS (operators are handled elsewhere), since they don't
            // take a normal parameter, but an identifier (which is not normally a FhirPath type)
            (name != "is" && name != "as" && name != "ofType" ? Grammar.Expression : TypeSpec.Select(s => new IdentifierExpression(s))).Positioned();


        public static Parser<Expression> FunctionInvocation(Expression focus)
        {
            return Function(focus)
                .Or(Lexer.Identifier.Select(i => new ChildExpression(focus, i)).Positioned())
                //.XOr(Lexer.Axis.Select(a => new AxisExpression(a)))
                .TokenIgnoreComments();
        }

        public static readonly Parser<Expression> Term =
            Literal
            .Or(FunctionInvocation(AxisExpression.That))
            .XOr(Lexer.ExternalConstant.Select(n => BuildVariableRefExpression(n)).Positioned()) //Was .XOr(Lexer.ExternalConstant.Select(v => Eval.ExternalConstant(v)))
            .XOr(BracketExpr)
            .XOr(EmptyList)
            .XOr(Lexer.Axis.Select(a => new AxisExpression(a)).Positioned())
            .TokenIgnoreComments()
            .Named("Term");


        public static Expression BuildVariableRefExpression(string name)
        {
            if (name.StartsWith("ext-"))
                return new FunctionCallExpression(AxisExpression.That, "builtin.coreexturl", TypeSpecifier.String, new ConstantExpression(name.Substring(4)));
#pragma warning disable IDE0046 // Convert to conditional expression
            else if (name.StartsWith("vs-"))
#pragma warning restore IDE0046 // Convert to conditional expression
                return new FunctionCallExpression(AxisExpression.That, "builtin.corevsurl", TypeSpecifier.String, new ConstantExpression(name.Substring(3)));
            else
                return new VariableRefExpression(name);
        }

        public static readonly Parser<string> TypeSpec =
            Lexer.QualifiedIdentifier.TokenIgnoreComments();

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
            Term.Then(expr => chainInvocation(expr));

        private static Parser<Expression> chainInvocation(Expression focus)
        {
            if (focus == null) throw new ArgumentNullException("focus");

            return Parse.Or(Invocation(focus).Then(second => chainInvocation(second)),
                                 Parse.Return(focus));
        }


        public static Parser<Expression> Invocation(Expression focus)
        {
            return Parse.Or(DotInvocation(focus), IndexerInvocation(focus));
        }

        // '.' invocation
        public static Parser<Expression> DotInvocation(Expression focus)
        {
            return Parse.Char('.').Then(op => FunctionInvocation(focus));
        }

        // '[' expression ']'                             #indexerExpression
        public static Parser<Expression> IndexerInvocation(Expression focus)
        {
            return Parse.Contained(Expression, Parse.Char('[').TokenIgnoreComments(), Parse.Char(']').TokenIgnoreComments())
                .Select(ix => new IndexerExpression(focus, ix)).Positioned();
        }

        // | ('+' | '-') expression                                    #polarityExpression
        public static readonly Parser<Expression> PolarityExpression =
            (from op in Lexer.PolarityOperator.Optional()
            from indexer in InvocationExpression
            select op.IsEmpty ? indexer : new UnaryExpression(op.Get(), indexer))
            .Positioned();

        public static Parser<Expression> BinaryExpression(Parser<string> oper, Parser<Expression> operands)
        {
            return Parse.ChainOperator(oper, operands, (op, left, right) => new BinaryExpression(op, left, right)).Positioned();
            //return
            //   from left in operands
            //   from right in (from op in oper
            //                  from right in operands
            //                  select Tuple.Create(op, right)).Optional()
            //   select right.IsEmpty ? left : new BinaryExpression(right.Get().Item1, left, right.Get().Item2);
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
                             from tp in TypeSpec.Select(v => new IdentifierExpression(v)).Positioned()
                             select new BinaryExpression(isas, ineq, tp))
                    .Or(Parse.Return(ineq).Positioned()))
                    // .Positioned()
                    ;

        // | expression('=' | '~' | '!=' | '!~' | '<>') expression    #equalityExpression
        public static readonly Parser<Expression> EqExpression = BinaryExpression(Lexer.EqOperator, TypeExpression);

        // | expression('in' | 'contains') expression                 #membershipExpression
        public static readonly Parser<Expression> MembershipExpression = BinaryExpression(Lexer.MembershipOperator, EqExpression);

        // | expression 'and' expression                               #andExpression
        public static readonly Parser<Expression> AndExpression = BinaryExpression(Lexer.AndOperator, MembershipExpression);

        // | expression('or' | 'xor') expression                      #orExpression
        public static readonly Parser<Expression> OrExpression = BinaryExpression(Lexer.OrOperator, AndExpression);

        // | expression 'implies' expression                           #impliesExpression
        public static readonly Parser<Expression> ImpliesExpression = BinaryExpression(Lexer.ImpliesOperator, OrExpression);

        public static readonly Parser<Expression> Expression = ImpliesExpression;
    }
}
