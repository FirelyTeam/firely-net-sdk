/*
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.ElementModel;
using Hl7.FhirPath.Expressions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Hl7.FhirPath
{

    public class DiagnosticsDebugTracer : IDebugTracer
    {

        public void TraceCall(
            Expression expr,
            IEnumerable<ITypedElement>? focus,
            IEnumerable<ITypedElement>? thisValue,
            ITypedElement? index,
            IEnumerable<ITypedElement> totalValue,
            IEnumerable<ITypedElement> result,
            IEnumerable<KeyValuePair<string, IEnumerable<ITypedElement>>> variables)
        {
            string exprName;
            
            switch (expr)
            {
                case IdentifierExpression _:
                    return;
                
                case ConstantExpression ce:
                    Trace.WriteLine($"{expr.Location.LineNumber},{expr.Location.LinePosition},constant");
                    exprName = "constant";
                    break;
                
                case ChildExpression child:
                    Trace.WriteLine($"{expr.Location.LineNumber},{expr.Location.LinePosition},{child.ChildName}");
                    exprName = child.ChildName;
                    break;
                
                case IndexerExpression _:
                    Trace.WriteLine($"{expr.Location.LineNumber},{expr.Location.LinePosition},[]");
                    exprName = "[]";
                    break;
                
                case UnaryExpression ue:
                    Trace.WriteLine($"{expr.Location.LineNumber},{expr.Location.LinePosition},{ue.Op}");
                    exprName = ue.Op;
                    break;
                
                case BinaryExpression be:
                    Trace.WriteLine($"{expr.Location.LineNumber},{expr.Location.LinePosition},{be.Op}");
                    exprName = be.Op;
                    break;
                
                case FunctionCallExpression fe:
                    Trace.WriteLine($"{expr.Location.LineNumber},{expr.Location.LinePosition},{fe.FunctionName}");
                    exprName = fe.FunctionName;
                    break;
                
                case NewNodeListInitExpression _:
                    Trace.WriteLine($"{expr.Location.LineNumber},{expr.Location.LinePosition},{{}} (empty)");
                    exprName = "{}";
                    break;
                
                case AxisExpression ae:
                    if (ae.AxisName == "that")
                        return;
                    Trace.WriteLine($"Evaluated: {ae.AxisName} results: {result.Count()}");
                    exprName = "$" + ae.AxisName;
                    break;
                
                case VariableRefExpression ve:
                    Trace.WriteLine($"{expr.Location.LineNumber},{expr.Location.LinePosition},%{ve.Name}");
                    exprName = "%" + ve.Name;
                    break;
                
                default:
                    exprName = expr.GetType().Name;
#if DEBUG
                    Debugger.Break();
#endif
                    throw new Exception($"Unknown expression type: {expr.GetType().Name}");
                    // Trace.WriteLine($"Evaluated: {expr} results: {result.Count()}");
            }

            if (focus != null)
            {
                foreach (var item in focus)
                {
                    DebugTraceValue($"$focus", item);
                }
            }

            if (thisValue != null)
            {
                foreach (var item in thisValue)
                {
                    DebugTraceValue("$this", item);
                }
            }

            if (index != null)
            {
                DebugTraceValue("$index", index);
            }

            if (totalValue != null)
            {
                foreach (var item in totalValue)
                {
                    DebugTraceValue($"{exprName} »", item);
                }
            }

            if (result != null)
            {
                foreach (var item in result)
                {
                    DebugTraceValue($"{exprName} »", item);
                }
            }
        }

        private static void DebugTraceValue(string exprName, ITypedElement? item)
        {
            if (item == null)
                return; // possible with a null focus to kick things off
            if (item.Location == "@primitivevalue@" || item.Location == "@QuantityAsPrimitiveValue@")
                Trace.WriteLine($"  {exprName}:\t{item.Value}\t({item.InstanceType})");
            else
                Trace.WriteLine($"  {exprName}:\t{item.Value}\t({item.InstanceType})\t{item.Location}");
        }
    }
}
