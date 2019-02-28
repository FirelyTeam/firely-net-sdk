/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */
using FP = Hl7.FhirPath.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.Utility;

namespace Hl7.FhirPath.Expressions
{
    internal class RewritingVisitor : FP.ExpressionVisitor<Expression>
    {
        public override Expression VisitConstant(FP.ConstantExpression expression, SymbolTable scope)
            => expression;

        public override Expression VisitNewNodeListInit(FP.NewNodeListInitExpression expression, SymbolTable scope)
            => new NewNodeListInitExpression(expression.Contents.Select(c=>c.Accept(this, null)));

        public override Expression VisitVariableRef(FP.VariableRefExpression expression, SymbolTable scope)
            => expression;

        public override Expression VisitFunctionCall(FP.FunctionCallExpression expression, SymbolTable scope)
            => rewriteImplicitLambdas(expression);

        public override Expression VisitLambda(LambdaExpression expression, SymbolTable scope)
            => new LambdaExpression(expression.ParamNames, expression.Body.Accept(this, null));


        private static readonly string[] FOCUS_LAMBDA = new[] { "builtin.focus" };
        private static readonly string[] FOCUS_THIS_LAMBDA = new[] { "builtin.focus", "this" };
        private static readonly string[] FOCUS_THIS_INDEX_LAMBDA = new[] { "builtin.focus", "this", "index" };
        private static readonly string[] FOCUS_THIS_INDEX_TOTAL_LAMBDA = new[] { "builtin.focus", "this", "index", "total" };

        private static readonly Dictionary<string,Func<Expression[],Expression[]>> lambdaReplacementTable = new Dictionary<string,Func<Expression[], Expression[]>>
            {
                {"where", argtf(0, FOCUS_THIS_LAMBDA) },
                {"select", argtf(0, FOCUS_THIS_LAMBDA) },
                {"all", argtf(0, FOCUS_THIS_LAMBDA) },
                {"repeat", argtf(0, FOCUS_THIS_LAMBDA) },
                {"exists", argtf(0, FOCUS_THIS_LAMBDA) },
                {"trace", argtf(1, FOCUS_THIS_INDEX_LAMBDA) },
                {"aggregate", argtf(0, FOCUS_THIS_INDEX_TOTAL_LAMBDA) },
                {"iif", argtf(0, FOCUS_LAMBDA) },
            };

        private static Func<Expression[], Expression[]> argtf(int index, string[] lambdaArgs) =>
            (args) => args
                .Select((e, i) => i == index ? new LambdaExpression(lambdaArgs, e) : e)
                .ToArray();

        private FunctionCallExpression rewriteImplicitLambdas(FunctionCallExpression original)
        {
            // First, recursively rewrite the focus
            var rewrittenFocus = original.Focus.Accept(this, null);

            // Then, recursively rewrite the arguments to the function call, before (eventually) wrapping the lambdas around them
            var rewrittenArgs = original.Arguments.Select(a => a.Accept(this, null)).ToArray();

            // For backwards compatibility, we accept 'any' as an equivalent for 'exists'
            var rewrittenName = original.FunctionName == "any" ? "exists" : original.FunctionName;

            // Now, for some specific functions in the spec, we need to turn specific arguments into lambda's. 
            // E.g. while($this > 4) is actually while(\$this => $this > 4), since the while gets passed a function
            // that is invoked over all elements in its focus. The lambdaReplacementTable specifies exactly which 
            // functions and which args need to be replaced.
            var wrappedArgs = findTransformerForFunc(rewrittenName)(rewrittenArgs);

            return new FunctionCallExpression(rewrittenFocus, rewrittenName, TypeInfo.Any, (IEnumerable<Expression>)wrappedArgs);

            Func<Expression[], Expression[]> findTransformerForFunc(string name) =>
                lambdaReplacementTable.TryGetValue(name, out var transformer) ? transformer : identity;

            Expression[] identity(Expression[] args) => args;
        }
    }

    internal static class RewritingVisitorExtensions
    {
        public static Expression Rewrite(this FP.Expression expr)
        {
            var visitor = new RewritingVisitor();
            return expr.Accept<Expression>(visitor, null);
        }
    }

}
