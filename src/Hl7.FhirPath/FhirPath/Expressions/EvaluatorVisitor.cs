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
    internal class EvaluatorVisitor : FP.ExpressionVisitor<Invokee>
    {
        public override Invokee VisitConstant(FP.ConstantExpression expression, SymbolTable scope) 
            => InvokeeFactory.Return(new ConstantValue(expression.Value));

        public override Invokee VisitNewNodeListInit(FP.NewNodeListInitExpression expression, SymbolTable scope) 
            => InvokeeFactory.Return(FhirValueList.Empty);

        public override Invokee VisitVariableRef(FP.VariableRefExpression expression, SymbolTable scope)
            => resolve(scope, expression.Name, Enumerable.Empty<Type>());

        // every expression is rooted in the focus - the parse tree should have
        // $focus.A 
        private static readonly string[] IMPLICIT_CALL_PARAMS = new[] { "focus" };

        public override Invokee VisitFunctionCall(FP.FunctionCallExpression expression, SymbolTable scope)
        {
            var focus = expression.Focus.ToEvaluator(scope);

            var arguments = new List<Invokee>() { focus };
            var argScope = new SymbolTable(scope);
            argScope.AddValue("focus", focus);

            //// Create a lambda for each argument introducing the $focus as the first
            //// positional parameter. This will be picked up by the first expression in
            //// the argument, which is the variable ref to $focus.  Also, the arguments
            //// are evaluated in the context of the lambda expression, which includes
            //// a new symbol scope including the aforementioned $focus.
            //arguments.AddRange(expression.Arguments.Select(arg =>
            //      new LambdaExpression(IMPLICIT_CALL_PARAMS, arg).ToEvaluator(scope)));
            arguments.AddRange(expression.Arguments.Select(arg =>
                    arg.ToEvaluator(argScope)));

            // We have no real type information, so just pass object as the type
            var types = new List<Type>() { typeof(object) }; // for the focus
            types.AddRange(expression.Arguments.Select(a => typeof(object)));   // for the arguments

            // Now locate the function based on the types and name
            Invokee boundFunction = resolve(scope, expression.FunctionName, types);

            // The Invokee returned will execute the boundFunction, just catching
            // exceptions when caught.
            return InvokeeFactory.Invoke(expression.FunctionName, arguments, boundFunction);
        }

        private class ParameterPlaceHolder
        {
            public Invokee Parameter;
        }


        public override Invokee VisitLambda(LambdaExpression expression, SymbolTable scope)
        {
            var lambdaScope = new SymbolTable(scope);
            var stack = new ParameterPlaceHolder[expression.ParamNames.Length];

            for (var position = 0; position < expression.ParamNames.Length; position++)
            {
                var paramName = expression.ParamNames[position];
                stack[position] = new ParameterPlaceHolder();
                var paramRef = stack[position];
                lambdaScope.AddFunction(new CallSignature(paramName, typeof(object)),
                    (ctx, args) => paramRef.Parameter(ctx, args));
            }

            var body = expression.Body.ToEvaluator(lambdaScope);

            return (ctx, args) =>
            {
                var argsList = args.ToList();
                if (args.Count != expression.ParamNames.Length)
                    throw Error.InvalidOperation("Internal error: lambda invocation has mismatching number of args.");

                for (var position = 0; position < args.Count; position++)
                    stack[position].Parameter = args[position];

                return body(ctx, new List<Invokee>());
            };           
        }



        private static Invokee resolve(SymbolTable scope, string name, IEnumerable<Type> argumentTypes)
        {
            // For now, we don't have the types or the parameters statically, so we just match on name
            var candidateTable = scope.Filter(name, argumentTypes.Count());
            var count = candidateTable.Count();

            if (count > 1)
            {
                // If we have multiple candidates, delay resolution to runtime
                return (new DynaDispatcher(name, candidateTable).Dispatcher);
            }
            else if(count == 1)
            {
                // There's only one candidate, again we don't have the right parameter types at
                // to match yet.
                //Invokee func = scope.Get(name, argumentTypes);

                Invokee func = candidateTable.First();
                if (func == null)
                    throw Error.Argument("Function '{0}' is not called with the right number or type of parameters".FormatWith(name));

                return func;
            }
            else
            {
                // No function could be found, but there IS a function with the given name, 
                // report an error about the fact that the function is known, but could not be bound
                throw Error.Argument("Unknown symbol '{0}'".FormatWith(name));
            }
        }

    }

    internal static class EvaluatorExpressionExtensions
    {
        public static Invokee ToEvaluator(this FP.Expression expr, SymbolTable scope)
        {
            var compiler = new EvaluatorVisitor();
            return expr.Accept<Invokee>(compiler, scope);
        }

        public static Invokee ToEvaluator(this FP.Expression expr) => expr.ToEvaluator(new SymbolTable());
    }

}
