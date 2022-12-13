/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using FP = Hl7.FhirPath.Expressions;

namespace Hl7.FhirPath.Expressions
{
    internal class EvaluatorVisitor : FP.ExpressionVisitor<Invokee>
    {
        public SymbolTable Symbols { get; }

        public EvaluatorVisitor(SymbolTable symbols)
        {
            Symbols = symbols;
        }


        public override Invokee VisitConstant(FP.ConstantExpression expression)
        {
            return InvokeeFactory.Return(ElementNode.ForPrimitive(expression.Value));
        }

        public override Invokee VisitFunctionCall(FP.FunctionCallExpression expression)
        {
            var focus = expression.Focus.ToEvaluator(Symbols);
            var arguments = new List<Invokee>() { focus };
            arguments.AddRange(expression.Arguments.Select(arg => arg.ToEvaluator(Symbols)));

            // We have no real type information, so just pass object as the type
            var types = new List<Type>() { typeof(object) }; //   for the focus;
            types.AddRange(expression.Arguments.Select(a => typeof(object)));   // for the arguments

            // Now locate the function based on the types and name
            Invokee boundFunction = resolve(Symbols, expression.FunctionName, types);

            return InvokeeFactory.Invoke(expression.FunctionName, arguments, boundFunction);
        }

        public override Invokee VisitNewNodeListInit(FP.NewNodeListInitExpression expression)
        {
            return InvokeeFactory.Return(ElementNode.EmptyList);
        }

        public override Invokee VisitVariableRef(FP.VariableRefExpression expression)
        {
            // HACK, for now, $this is special, and we handle in run-time, not compile time...
            if (expression.Name == "builtin.this")
                return InvokeeFactory.GetThis;

            // HACK, for now, $this is special, and we handle in run-time, not compile time...
            if (expression.Name == "builtin.that")
                return InvokeeFactory.GetThat;

            // HACK, for now, $total is special, and we handle in run-time, not compile time...
            if (expression.Name == "builtin.total")
                return InvokeeFactory.GetTotal;

            // HACK, for now, $index is special, and we handle in run-time, not compile time...
            if (expression.Name == "builtin.index")
                return InvokeeFactory.GetIndex;

            // HACK, for now, %context is special, and we handle in run-time, not compile time...
            if (expression.Name == "context")
                return InvokeeFactory.GetContext;

            // HACK, for now, %resource is special, and we handle in run-time, not compile time...
            if (expression.Name == "resource")
                return InvokeeFactory.GetResource;

            // HACK, for now, %rootResource is special, and we handle in run-time, not compile time...
            if (expression.Name == "rootResource")
                return InvokeeFactory.GetRootResource;

            // Variables are still functions without arguments. For now variables are treated separately here,
            //Functions are handled elsewhere.
            return resolve(Symbols, expression.Name, Enumerable.Empty<Type>());
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
            else if (count == 1)
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
            var compiler = new EvaluatorVisitor(scope);
            return expr.Accept(compiler);
        }
    }

}
