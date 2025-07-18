/*
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using Hl7.FhirPath.Expressions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FP = Hl7.FhirPath.Expressions;

namespace Hl7.FhirPath
{
    public delegate void DebugTraceDelegate(Expression expr,
            IEnumerable<ITypedElement> focus,
            IEnumerable<ITypedElement> thisValue,
            ITypedElement index,
            IEnumerable<ITypedElement> totalValue,
            IEnumerable<ITypedElement> result,
            IEnumerable<KeyValuePair<string, IEnumerable<ITypedElement>>> variables);

    public class DebugTracer
    {

        public static void TraceCall(
            Expression expr,
            IEnumerable<ITypedElement> focus,
            IEnumerable<ITypedElement> thisValue,
            ITypedElement index,
            IEnumerable<ITypedElement> totalValue,
            IEnumerable<ITypedElement> result,
            IEnumerable<KeyValuePair<string, IEnumerable<ITypedElement>>> variables)
        {
            string exprName;
            if (expr is IdentifierExpression ie)
                return;

            if (expr is ConstantExpression ce)
            {
                System.Diagnostics.Trace.WriteLine($"{expr.Location.LineNumber},{expr.Location.LinePosition},constant");
                exprName = "constant";
            }
            else if (expr is ChildExpression child)
            {
                System.Diagnostics.Trace.WriteLine($"{expr.Location.LineNumber},{expr.Location.LinePosition},{child.ChildName}");
                exprName = child.ChildName;
            }
            else if (expr is IndexerExpression indexer)
            {
                System.Diagnostics.Trace.WriteLine($"{expr.Location.LineNumber},{expr.Location.LinePosition},[]");
                exprName = "[]";
            }
            else if (expr is UnaryExpression ue)
            {
                System.Diagnostics.Trace.WriteLine($"{expr.Location.LineNumber},{expr.Location.LinePosition},{ue.Op}");
                exprName = ue.Op;
            }
            else if (expr is BinaryExpression be)
            {
                System.Diagnostics.Trace.WriteLine($"{expr.Location.LineNumber},{expr.Location.LinePosition},{be.Op}");
                exprName = be.Op;
            }
            else if (expr is FunctionCallExpression fe)
            {
                System.Diagnostics.Trace.WriteLine($"{expr.Location.LineNumber},{expr.Location.LinePosition},{fe.FunctionName}");
                exprName = fe.FunctionName;
            }
            else if (expr is NewNodeListInitExpression)
            {
                System.Diagnostics.Trace.WriteLine($"{expr.Location.LineNumber},{expr.Location.LinePosition},{{}} (empty)");
                exprName = "{}";
            }
            else if (expr is AxisExpression ae)
            {
                if (ae.AxisName == "that")
                    return;
                System.Diagnostics.Trace.WriteLine($"Evaluated: {ae.AxisName} results: {result.Count()}");
                exprName = "$" + ae.AxisName;
            }
            else if (expr is VariableRefExpression ve)
            {
                System.Diagnostics.Trace.WriteLine($"{expr.Location.LineNumber},{expr.Location.LinePosition},%{ve.Name}");
                exprName = "%" + ve.Name;
            }
            else
            {
                exprName = expr.GetType().Name;
#if DEBUG
                Debugger.Break();
#endif
                throw new Exception($"Unknown expression type: {expr.GetType().Name}");
                // System.Diagnostics.Trace.WriteLine($"Evaluated: {expr} results: {result.Count()}");
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

        private static void DebugTraceValue(string exprName, ITypedElement item)
        {
            if (item == null)
                return; // possible with a null focus to kick things off
            if (item.Location == "@primitivevalue@" || item.Location == "@QuantityAsPrimitiveValue@")
                System.Diagnostics.Trace.WriteLine($"  {exprName}:\t{item.Value}\t({item.InstanceType})");
            else
                System.Diagnostics.Trace.WriteLine($"  {exprName}:\t{item.Value}\t({item.InstanceType})\t{item.Location}");
        }
    }
}
