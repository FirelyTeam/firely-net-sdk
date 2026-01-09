/*
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

using FocusCollection = System.Collections.Generic.IEnumerable<Hl7.Fhir.Model.PocoNode>;
// ReSharper disable PossibleMultipleEnumeration

namespace Hl7.FhirPath.Expressions;

internal class EvaluatorVisitor(SymbolTable symbols, bool injectDebugHook) : ExpressionVisitor<Invokee>
{
    private Invokee wrapForDebugTracer(Invokee invokee, Expression expression)
    {
        if (injectDebugHook)
        {
            return (context, arguments) => {
                var oldFocus = context.Focus;
                var result = invokee(context, arguments);

                context.EvaluationContext.DebugTracer?.TraceCall(expression, context.Id, context.Focus,
                    context.GetThis(), context.GetIndex().FirstOrDefault(), context.GetTotal(),
                    result, context.Variables());

                // restore the original focus to the context
                context.Focus = oldFocus;
                return result;
            };
        }
        return invokee;
    }

    public SymbolTable Symbols { get; } = symbols;

    public EvaluatorVisitor(SymbolTable symbols) : this(symbols, true)
    {
    }

    public override Invokee VisitConstant(ConstantExpression expression)
    {
        return wrapForDebugTracer(InvokeeFactory.Return(PocoNode.ForAnyPrimitive(expression.Value)), expression);
    }

    public override Invokee VisitFunctionCall(FunctionCallExpression expression)
    {
        var focus = expression.Focus.ToEvaluator(Symbols, injectDebugHook);
        var arguments = new List<Invokee> { focus };
        arguments.AddRange(expression.Arguments.Select(arg => arg.ToEvaluator(Symbols, injectDebugHook)));

        // We have no real type information, so just pass object as the type
        var types = new List<Type>() { typeof(object) }; //   for the focus;
        types.AddRange(expression.Arguments.Select(_ => typeof(object)));   // for the arguments

        // Now locate the function based on the types and name
        Invokee boundFunction = resolve(Symbols, expression.FunctionName, types);

        return wrapForDebugTracer(InvokeeFactory.Invoke(expression.FunctionName, arguments, boundFunction), expression);
    }

    public override Invokee VisitNewNodeListInit(NewNodeListInitExpression expression)
    {
        return wrapForDebugTracer(InvokeeFactory.Return([]), expression);
    }

    public override Invokee VisitVariableRef(VariableRefExpression expression)
    {
        // HACK, for now, $this is special, and we handle in run-time, not compile time...
        if (expression.Name == "builtin.this")
            return wrapForDebugTracer(InvokeeFactory.GetThis, expression);

        // HACK, for now, $this is special, and we handle in run-time, not compile time...
        if (expression.Name == "builtin.that")
            return InvokeeFactory.GetThat;

        // HACK, for now, $total is special, and we handle in run-time, not compile time...
        if (expression.Name == "builtin.total")
            return wrapForDebugTracer(InvokeeFactory.GetTotal, expression);

        // HACK, for now, $index is special, and we handle in run-time, not compile time...
        if (expression.Name == "builtin.index")
            return wrapForDebugTracer(InvokeeFactory.GetIndex, expression);

        // HACK, for now, %context is special, and we handle in run-time, not compile time...
        if (expression.Name == "context")
            return wrapForDebugTracer(InvokeeFactory.GetContext, expression);

        // HACK, for now, %resource is special, and we handle in run-time, not compile time...
        if (expression.Name == "resource")
            return wrapForDebugTracer(InvokeeFactory.GetResource, expression);

        // HACK, for now, %rootResource is special, and we handle in run-time, not compile time...
        if (expression.Name == "rootResource")
            return wrapForDebugTracer(InvokeeFactory.GetRootResource, expression);

        return wrapForDebugTracer(chainResolves, expression);

        FocusCollection chainResolves(Closure context, IEnumerable<Invokee> invokees)
        {
            var value = context.ResolveValue(expression.Name);
            if (value != null)
            {
                // this was in the context, so the scope was $this (the context)
                context.Focus = context.GetThis();
                return value;
            }
            else
            {
                return resolve(Symbols, expression.Name, Enumerable.Empty<Type>())(context, []);
            }
        }
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

            var func = candidateTable.First();
            return func ?? throw Error.Argument("Function '{0}' is not called with the right number or type of parameters".FormatWith(name));
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
    extension(Expression expr)
    {
        public Invokee ToEvaluator(SymbolTable scope)
        {
            var compiler = new EvaluatorVisitor(scope);
            return expr.Accept(compiler);
        }

        public Invokee ToEvaluator(SymbolTable scope, bool injectDebugTraceHooks)
        {
            var compiler = new EvaluatorVisitor(scope, injectDebugTraceHooks);
            return expr.Accept(compiler);
        }
    }
}