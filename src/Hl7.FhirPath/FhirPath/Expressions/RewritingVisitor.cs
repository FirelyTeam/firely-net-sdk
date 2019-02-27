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
            => expression;

        public override Expression VisitVariableRef(FP.VariableRefExpression expression, SymbolTable scope)
            => expression;

        public override Expression VisitFunctionCall(FP.FunctionCallExpression expression, SymbolTable scope)
            => constructFhirFunc(expression.Focus, expression.FunctionName, expression.Arguments);

        public override Expression VisitLambda(LambdaExpression expression, SymbolTable scope)
            => expression;

        private static FunctionCallExpression constructFhirFunc(Expression focus, string name, IList<Expression> paramList)
        {
            // for backwards compatibility, accept 'any' as an equivalent for 'exists'
            if (name == "any") name = "exists";

            // Rewriting the args is only required for some functions defined in the spec, that
            // are using lambdas implicitly. What this code does is turn these arguments into
            // explicit lambdas.
            var numArgs = paramList.Count;

            switch (name)
            {
                case "where" when numArgs == 1:
                    // where(criteria : expression) : collection
                    return singleArgLambda();
                case "select" when numArgs == 1:
                    //   select(projection: expression)
                    return singleArgLambda();
                case "all" when numArgs == 1:
                    // all(criteria: expression) : Boolean
                    return singleArgLambda();
                case "repeat" when numArgs == 1:
                    // repeat(projection: expression) : collection
                    return singleArgLambda();
                case "trace" when numArgs == 2:
                    //trace(name: string; selector: expression) : collection
                    return new FunctionCallExpression(focus, name, TypeInfo.Any,
                            new[] { paramList[0], new LambdaExpression(new[] { "this", "index" }, paramList[1]) });
                case "exists" when numArgs == 1:
                    // exists(criteria: expression) : Boolean  (without optional criteria this falls through to default)
                    return singleArgLambda();
                case "aggregate" when numArgs == 1:
                    // aggregate(aggregator: expression) : value
                    return new FunctionCallExpression(focus, name, TypeInfo.Any,
                        new[] { new LambdaExpression(new[] { "this", "total", "index" }, paramList[0]) });
                case "aggregate" when numArgs == 2:
                    // aggregate(aggregator: expression, init: value) : value
                    return new FunctionCallExpression(focus, name, TypeInfo.Any,
                        new[] { new LambdaExpression(new[] { "this", "total", "index" }, paramList[0]), paramList[1] });
                case "iif" when numArgs == 2:
                    // iif(criterion: expression, true-result: collection) : collection                    
                    return new FunctionCallExpression(focus, name, TypeInfo.Any,
                        new[] { new LambdaExpression(new[] { "this", "index" }, paramList[0]), paramList[1] });
                case "iif" when numArgs == 3:
                    // iif(criterion: expression, true - result: collection, otherwise-result: collection) : collection                    
                    return new FunctionCallExpression(focus, name, TypeInfo.Any,
                        new[] { new LambdaExpression(new[] { "this", "index" }, paramList[0]), paramList[1], paramList[2] });
                default:
                    return unchanged();
            }

            FunctionCallExpression singleArgLambda() =>
                new FunctionCallExpression(focus, name, TypeInfo.Any,
                        new[] { new LambdaExpression(new[] { "this", "index" }, paramList[0]) });

            FunctionCallExpression unchanged() =>
                new FunctionCallExpression(focus, name, TypeInfo.Any, paramList);
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
