/*
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.FhirPath.Expressions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Hl7.FhirPath
{

    public class DiagnosticsDebugTracer : IDebugTracer
    {
        public void TraceCall(
            Expression expr,
            int contextId,
            IEnumerable<PocoNode>? focus,
            IEnumerable<PocoNode>? thisValue,
            PocoNode? index,
            IEnumerable<PocoNode> totalValue,
            IEnumerable<PocoNode> result,
            IEnumerable<KeyValuePair<string, IEnumerable<PocoNode>>> variables)
        {
            DiagnosticsDebugTracer.DebugTraceCall(expr, contextId, focus, thisValue, index, totalValue, result, variables);
        }

        public static void DebugTraceCall(
            Expression expr,
            int contextId,
            IEnumerable<PocoNode>? focus,
            IEnumerable<PocoNode>? thisValue,
            PocoNode? index,
            IEnumerable<PocoNode> totalValue,
            IEnumerable<PocoNode> result,
            IEnumerable<KeyValuePair<string, IEnumerable<PocoNode>>> variables)
        {
            string exprName;

            switch (expr)
            {
                case IdentifierExpression _:
                    return;

                case ConstantExpression ce:
                    Trace.WriteLine($"{expr.Location.LineNumber},{expr.Location.LinePosition},constant (ctx.id: {contextId})");
                    exprName = "constant";
                    break;

                case ChildExpression child:
                    Trace.WriteLine($"{expr.Location.LineNumber},{expr.Location.LinePosition},{child.ChildName} (ctx.id: {contextId})");
                    exprName = child.ChildName;
                    break;

                case IndexerExpression _:
                    Trace.WriteLine($"{expr.Location.LineNumber},{expr.Location.LinePosition},[] (ctx.id: {contextId})");
                    exprName = "[]";
                    break;

                case UnaryExpression ue:
                    Trace.WriteLine($"{expr.Location.LineNumber},{expr.Location.LinePosition},{ue.Op} (ctx.id: {contextId})");
                    exprName = ue.Op;
                    break;

                case BinaryExpression be:
                    Trace.WriteLine($"{expr.Location.LineNumber},{expr.Location.LinePosition},{be.Op} (ctx.id: {contextId})");
                    exprName = be.Op;
                    break;

                case FunctionCallExpression fe:
                    Trace.WriteLine($"{expr.Location.LineNumber},{expr.Location.LinePosition},{fe.FunctionName} (ctx.id: {contextId})");
                    exprName = fe.FunctionName;
                    break;

                case NewNodeListInitExpression _:
                    Trace.WriteLine($"{expr.Location.LineNumber},{expr.Location.LinePosition},{{}} (empty) (ctx.id: {contextId})");
                    exprName = "{}";
                    break;

                case AxisExpression ae:
                    if (ae.AxisName == "that")
                        return;
                    Trace.WriteLine($"{expr.Location.LineNumber},{expr.Location.LinePosition},${ae.AxisName} (ctx.id: {contextId})");
                    exprName = "$" + ae.AxisName;
                    break;

                case VariableRefExpression ve:
                    Trace.WriteLine($"{expr.Location.LineNumber},{expr.Location.LinePosition},%{ve.Name} (ctx.id: {contextId})");
                    exprName = "%" + ve.Name;
                    break;

                default:
                    exprName = expr.GetType().Name;
#if DEBUG
                    Debugger.Break();
#endif
                    throw new Exception($"Unknown expression type: {expr.GetType().Name} (ctx.id: {contextId})");
                    // Trace.WriteLine($"Evaluated: {expr} results: {result.Count()}");
            }

            if (result != null)
            {
                foreach (var item in result)
                {
                    DebugTraceValue($"{exprName} »", item);
                }
            }

            if (focus != null)
            {
                foreach (var item in focus)
                {
                    DebugTraceValue($"$focus", item);
                }
            }

            if (index != null)
            {
                DebugTraceValue("$index", index);
            }

            if (thisValue != null)
            {
                foreach (var item in thisValue)
                {
                    DebugTraceValue("$this", item);
                }
            }

            if (totalValue != null)
            {
                foreach (var item in totalValue)
                {
                    DebugTraceValue($"{exprName} »", item);
                }
            }
        }

        private static void DebugTraceValue(string exprName, PocoNode? item)
        {
            if (item == null)
                return; // possible with a null focus to kick things off
            if (item is PrimitiveNode)
                Trace.WriteLine($"  {exprName}:\t{item.GetValue()}\t({item.Poco.TypeName})");
            else
                Trace.WriteLine($"  {exprName}:\t{item.GetValue()}\t({item.Poco.TypeName})\t{item.GetLocation()}");
        }
    }
}
