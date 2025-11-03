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
using System.Globalization;
using System.Linq;

namespace Hl7.FhirPath.Parser
{
    internal class Grammar
    {
        public static readonly Parser<ConstantExpression> Quantity = quantityParser;
        private static IResult<ConstantExpression> quantityParser(IInput i)
        {
            // No longer uses the lexer for quantities so that it can decompose the individual tokens and whitespace
            // Note that quantities are always parsed with a unit, otherwise they would be an integer or decimal
            // and the +/- are unary operators and not a part of the quantity itself.
            var result = (
                from val in Lexer.DecimalNumber.Select(n => n.ToString(CultureInfo.InvariantCulture))
                    .Or(Lexer.IntegerNumber.Select(n => n.ToString(CultureInfo.InvariantCulture))).Select(v => new SubToken(v)).Positioned()
                from ws in WhitespaceOrComments()
                from unit in Lexer.String.Select(u => $"'{u.Replace("'", "\\'")}'").Or(Lexer.Id).Select(v => new SubToken(v).WithLeadingWS(ws)).Positioned()
                select (valToken: val, unitToken: unit)
                          )(i);

            if (result.WasSuccessful)
            {
                var success = P.Quantity.TryParse($"{result.Value.valToken.Value} {result.Value.unitToken.Value}", out var quantity);
                if (success)
                {
                    var qv = new ConstantExpression(quantity, result.Value.unitToken);
                    return Result.Success(qv, result.Remainder);
                }
            }

            return Result.Failure<ConstantExpression>(i, $"Quantity is invalid",
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
            from wsLeading in WhitespaceOrComments()
            from l in
            Lexer.String.Select(v => new ConstantExpression(v, TypeSpecifier.String)).Positioned()
                .Or(Lexer.DateTime.Select(v => new ConstantExpression(v, TypeSpecifier.DateTime)).Positioned())
                .Or(Lexer.Date.Select(v => new ConstantExpression(v, TypeSpecifier.Date)).Positioned())
                .Or(Lexer.Time.Select(v => new ConstantExpression(v, TypeSpecifier.Time)).Positioned())
                .XOr(Lexer.Bool.Select(v => new ConstantExpression(v, TypeSpecifier.Boolean)).Positioned())
                .Or(Quantity.Positioned())
                .Or(Lexer.DecimalNumber.Select(v => new ConstantExpression(v, TypeSpecifier.Decimal)).Positioned())
                .Or(Lexer.IntegerNumber.Select(v => new ConstantExpression(v, TypeSpecifier.Integer)).Positioned())
            from wsTrailing in WhitespaceOrComments()
            select l.CaptureWhitespaceAndComments(wsLeading, wsTrailing);


        //term
        //  : invocation                                            #invocationTerm
        //  | literal                                               #literalTerm
        //  | externalConstant                                      #externalConstantTerm
        //  | '(' expression ')'                                    #parenthesizedTerm
        //  | '{' '}'                                               #nullLiteral
        //  ;
        public static readonly Parser<Expression> BracketExpr =
            (
                 from lparen in Parse.Char('(').Select(v => new SubToken(v)).Positioned()
                 from expr in Parse.Ref(() => Expression)
                 from rparen in Parse.Char(')').Select(v => new SubToken(v)).Positioned()
                 select new BracketExpression(lparen, rparen, expr)
            ).Positioned()
            .Named("BracketExpr");

        // Doesn't support exposing the comments if they are embedded in the new empty node list
        public static readonly Parser<Expression> EmptyList =
            (
                from lparen in Parse.Char('{').Select(v => new SubToken(v)).Positioned()
                from wsMiddle in WhitespaceOrComments()
                from rparen in Parse.Char('}').Select(v => new SubToken(v).WithLeadingWS(wsMiddle)).Positioned()
                select new NewNodeListInitExpression(lparen, rparen)
            ).Named("EmptyList")
            .Positioned();

        public static Parser<FunctionCallExpression> Function(Expression context)
        {
            return
                from ws1 in WhitespaceOrComments()
                from n in Lexer.Identifier.Select(name => new IdentifierExpression(name).WithLeadingWS(ws1)).Positioned()

                from ws2 in WhitespaceOrComments()
                from lparen in Parse.Char('(').Select(v => new SubToken(v).WithLeadingWS(ws2)).Positioned()

                from paramList in Parse.Ref(() => FunctionParameter(n.Value).Named("parameter")).DelimitedBy(Parse.Char(',')).Optional()

                from ws3 in WhitespaceOrComments()
                from rparen in Parse.Char(')').Select(v => new SubToken(v).WithLeadingWS(ws3)).Positioned()

                from ws4 in WhitespaceOrComments()
                select new FunctionCallExpression(context, n.Value, lparen, rparen, TypeSpecifier.Any, paramList.GetOrElse(Enumerable.Empty<Expression>())).WithTrailingWS(ws4)
                .UsePositionFrom(n.Location);
        }

        // Direction parser for sort function
        public static readonly Parser<string> Direction =
            Parse.String("asc").Text().Or(Parse.String("desc").Text()).Named("Direction");

        public static Parser<Expression> FunctionParameter(string name)
        {
            // Special handling for sort function: allows optional direction argument
            if (name == "sort")
            {
                return
                    from wsLeading in WhitespaceOrComments()
                    from expr in Grammar.Expression
                    from wsDir in WhitespaceOrComments()
                    from dir in Direction.Optional()
                    from wsTrailing in WhitespaceOrComments()
                    select dir.IsDefined
                        ? new SortDirectionExpression(dir.Get(), expr.WithLeadingWS(wsLeading)).WithLeadingWS(wsDir).WithTrailingWS(wsTrailing)
                        : expr.WithLeadingWS(wsLeading).WithTrailingWS(wsTrailing);
            }
            // Make exception for is() and as() FUNCTIONS (operators are handled elsewhere), since they don't
            // take a normal parameter, but an identifier (which is not normally a FhirPath type)
            if (name != "is" && name != "as" && name != "ofType")
                return Grammar.Expression;
            return WhitespaceOrComments()
                .Then(wsLeading => TypeSpec.Select(s => new IdentifierExpression(s).WithLeadingWS(wsLeading)).Positioned())
                .Then(i => WhitespaceOrComments().Select(wsTrailing => i.WithTrailingWS(wsTrailing)));
        }

        // no "capturing" the comments here
        public static Parser<Expression> FunctionInvocation(Expression focus)
        {
            return Function(focus)
                .Or(WhitespaceOrComments().Then(wsLeading => Lexer.Identifier.Select(i => new IdentifierExpression(i).WithLeadingWS(wsLeading)).Positioned()).Select(i => new ChildExpression(focus, i)).Positioned())
                //.XOr(Lexer.Axis.Select(a => new AxisExpression(a)))
                ;
        }

        public static readonly Parser<Expression> Term =
            (
                from wsLeading in WhitespaceOrComments()
                from l in Literal
                    .Or(FunctionInvocation(AxisExpression.That))
                    .XOr(Lexer.ExternalConstant.Select(n => new SubToken(n)).Positioned().Select(n => BuildVariableRefExpression(n))) //Was .XOr(Lexer.ExternalConstant.Select(v => Eval.ExternalConstant(v)))
                    .XOr(BracketExpr)
                    .XOr(EmptyList)
                    .XOr(Lexer.Axis.Select(a => new AxisExpression(a)).Positioned())
                from wsTrailing in WhitespaceOrComments()
                select l.CaptureWhitespaceAndComments(wsLeading, wsTrailing)
            ).Named("Term");

        public static Expression BuildVariableRefExpression(SubToken name)
        {
            if (name.Value.StartsWith("ext-"))
                return new FunctionCallExpression(AxisExpression.That, "builtin.coreexturl", null, null, TypeSpecifier.String, new ConstantExpression(name.Value.Substring(4)).UsePositionFrom(name.Location)).UsePositionFrom(name.Location);
#pragma warning disable IDE0046 // Convert to conditional expression
            else if (name.Value.StartsWith("vs-"))
#pragma warning restore IDE0046 // Convert to conditional expression
                return new FunctionCallExpression(AxisExpression.That, "builtin.corevsurl", null, null, TypeSpecifier.String, new ConstantExpression(name.Value.Substring(3)).UsePositionFrom(name.Location)).UsePositionFrom(name.Location);
            else
                return new VariableRefExpression(name.Value).UsePositionFrom(name.Location);
        }

        public static readonly Parser<string> TypeSpec =
            Lexer.QualifiedIdentifier;

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

            return Invocation(focus).Then(second => chainInvocation(second)).Or(Parse.Return(focus));
        }


        public static Parser<Expression> Invocation(Expression focus)
        {
            return DotInvocation(focus).Or(IndexerInvocation(focus));
        }

        // '.' invocation
        public static Parser<Expression> DotInvocation(Expression focus)
        {
            return WhitespaceOrComments().Then(wsBeforeDot => Parse.Char('.').Select(v => new SubToken(v).WithLeadingWS(wsBeforeDot)).Positioned())
                .Then(op => FunctionInvocation(focus).Select(t => { t.WithLeadingWS(op.LeadingWhitespace); return t; }));
        }

        // '[' expression ']'                             #indexerExpression
        public static Parser<Expression> IndexerInvocation(Expression focus)
        {
            return
                // The whitespace before the '[' is captured in the ws1 token
                // so that the position information for the indexer isn't confused with the comment
                from wsLeading in WhitespaceOrComments()
                from indexer in (
                    from lparen in Parse.Char('[').Select(v => new SubToken(v)).Positioned()

                    from ix in Expression

                    from rparen in Parse.Char(']').Select(v => new SubToken(v)).Positioned()

                    select new IndexerExpression(focus, ix, lparen, rparen)
                ).Positioned()
                .Select(r => { r.LeftBrace.WithLeadingWS(wsLeading); return r; })
                select indexer;
        }

        // | ('+' | '-') expression                                    #polarityExpression
        public static readonly Parser<Expression> PolarityExpression =
            from ws1 in WhitespaceOrComments()
            from op in Lexer.PolarityOperator.Select(v => new SubToken(v).WithLeadingWS(ws1)).Positioned().Optional()

            from ws2 in WhitespaceOrComments()
            from indexer in InvocationExpression
            select op.IsEmpty ? indexer.WithLeadingWS(ws1) : new UnaryExpression(op.Get().Value[0], indexer.WithLeadingWS(ws2)).UsePositionFrom(op.Get().Location);

        private static Parser<SubToken> WrapSubTokenParameter(Parser<SubToken> parser)
        {
            return
                from wsLeading in WhitespaceOrComments()
                from p in parser.SubTokenWithLeadingWS(wsLeading)
                select p;
        }

        public static Parser<Expression> BinaryExpression(Parser<SubToken> oper, Parser<Expression> operands)
        {
            return Parse.ChainOperator(WrapSubTokenParameter(oper), operands, (op, left, right) => new BinaryExpression(op, left, right).UsePositionFrom(op.Location));
        }

        // | expression('*' | '/' | 'div' | 'mod') expression         #multiplicativeExpression
        public static readonly Parser<Expression> MulExpression = BinaryExpression(
                                    WhitespaceOrComments().Then(ws => Lexer.MulOperator.Select(v => new SubToken(v)).Positioned()),
                                    PolarityExpression);

        // | expression('+' | '-' ) expression                        #additiveExpression
        public static readonly Parser<Expression> AddExpression = BinaryExpression(
                                    Lexer.AddOperator.Select(v => new SubToken(v)).Positioned(),
                                    MulExpression);

        // | expression '|' expression                                 #unionExpression
        public static readonly Parser<Expression> UnionExpression = BinaryExpression(
                                    Lexer.UnionOperator.Select(v => new SubToken(v)).Positioned(),
                                    AddExpression);

        // | expression('<=' | '<' | '>' | '>=') expression           #inequalityExpression
        public static readonly Parser<Expression> InEqExpression = BinaryExpression(
                                    Lexer.InEqOperator.Select(v => new SubToken(v)).Positioned(),
                                    UnionExpression);

        // | expression ('is' | 'as') typeSpecifier                    #typeExpression
        public static readonly Parser<Expression> TypeExpression =
            InEqExpression.Then(
                    ineq => (
                        from wsLeading in WhitespaceOrComments()
                        from isas in Lexer.TypeOperator.Select(v => new SubToken(v).WithLeadingWS(wsLeading)).Positioned()
                        from wsLeadingTypeSpec in WhitespaceOrComments()
                        from tp in TypeSpec.Select(v => new IdentifierExpression(v).WithLeadingWS(wsLeadingTypeSpec)).Positioned()
                        from wsTrailing in WhitespaceOrComments()
                        select new BinaryExpression(isas, ineq, tp).WithTrailingWS(wsTrailing)
                        .UsePositionFrom(isas.Location)
                    )
                    .Or(Parse.Return(ineq)));

        // | expression('=' | '~' | '!=' | '!~' | '<>') expression    #equalityExpression
        public static readonly Parser<Expression> EqExpression = BinaryExpression(
                                    Lexer.EqOperator.Select(v => new SubToken(v)).Positioned(),
                                    TypeExpression);

        // | expression('in' | 'contains') expression                 #membershipExpression
        public static readonly Parser<Expression> MembershipExpression = BinaryExpression(
                                    Lexer.MembershipOperator.Select(v => new SubToken(v)).Positioned(),
                                    EqExpression);

        // | expression 'and' expression                               #andExpression
        public static readonly Parser<Expression> AndExpression = BinaryExpression(
                                    Lexer.AndOperator.Select(v => new SubToken(v)).Positioned(),
                                    MembershipExpression);

        // | expression('or' | 'xor') expression                      #orExpression
        public static readonly Parser<Expression> OrExpression = BinaryExpression(
                                    Lexer.OrOperator.Select(v => new SubToken(v)).Positioned(),
                                    AndExpression);

        // | expression 'implies' expression                           #impliesExpression
        public static readonly Parser<Expression> ImpliesExpression = BinaryExpression(
                                    Lexer.ImpliesOperator.Select(v => new SubToken(v)).Positioned(),
                                    OrExpression);

        public static readonly Parser<Expression> Expression =
            from wsLeading in WhitespaceOrComments()
            from op in ImpliesExpression.WithLeadingWS(wsLeading)
            from wsTrailing in WhitespaceOrComments()
            select op.WithTrailingWS(wsTrailing);

        // Whitespace or comments
        private static Parser<System.Collections.Generic.IEnumerable<WhitespaceSubToken>> WhitespaceOrComments() =>
            Parse.WhiteSpace.Many().Select(w => new WhitespaceSubToken(new string(w.ToArray()))).Positioned()
            .XOr(Lexer.Comment.Select(v => new CommentSubToken(v, false)).Positioned())
            .XOr(Lexer.CommentBlock.Select(v => new CommentSubToken(v, true)).Positioned())
            .Many()
            .Named("Whitespace and/or comments");
    }
}