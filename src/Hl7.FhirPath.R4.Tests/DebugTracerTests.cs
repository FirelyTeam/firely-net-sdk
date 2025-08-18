/*
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

// To introduce the DSTU2 FHIR specification
//extern alias dstu2;

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.FhirPath;
using Hl7.FhirPath.Expressions;
using Hl7.FhirPath.R4.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Hl7.FhirPath.Tests
{

    [TestClass]
    public class DebugTracerTest
    {
        static PatientFixture fixture;
        static FhirPathCompiler compiler;

        [ClassInitialize]
        public static void Initialize(TestContext ctx)
        {
            fixture = new PatientFixture();
            compiler = new FhirPathCompiler();
        }

        private class TestDebugTracer: IDebugTracer
        {
            public List<string> traceOutput = new List<string>();
            public void TraceCall(
                Expression expr,
                int contextId,
                IEnumerable<ITypedElement> focus,
                IEnumerable<ITypedElement> thisValue,
                ITypedElement index,
                IEnumerable<ITypedElement> totalValue,
                IEnumerable<ITypedElement> result,
                IEnumerable<KeyValuePair<string, IEnumerable<ITypedElement>>> variables)
            {
                DiagnosticsDebugTracer.DebugTraceCall(expr, contextId, focus, thisValue, index, totalValue, result, variables);

                var exprName = TraceExpressionNodeName(expr);
                if (exprName == null)
                    return; // this is a node that we aren't interested in tracing (Identifier and $that)
                var pi = expr.Location as FhirPathExpressionLocationInfo;
                string output = $"{pi.RawPosition},{pi.Length},{exprName}:" +
                                $" focus={focus?.Count() ?? 0} result={result?.Count() ?? 0}";
                traceOutput.Add(output);
                if (TraceNode != null)
                {
                    TraceNode(traceOutput.Count-1, expr, contextId,
                        focus, thisValue, index, totalValue, result);
                }
            }

            public delegate void TraceNodeDelegate(int n, Expression expr, int contextId,
                IEnumerable<ITypedElement> focus,
                IEnumerable<ITypedElement> thisValue,
                ITypedElement index,
                IEnumerable<ITypedElement> totalValue,
                IEnumerable<ITypedElement> result);
            public TraceNodeDelegate TraceNode { get; set; } = null;

            public string TraceExpressionNodeName(Expression expr)
            {
                switch (expr)
                {
                    case IdentifierExpression _:
                        return null; // we don't trace IdentifierExpressions, they are just names
                    case ConstantExpression ce:
                        return "constant";
                    case ChildExpression child:
                        return child.ChildName;
                    case IndexerExpression indexer:
                        return "[]";
                    case UnaryExpression ue:
                        return ue.Op;
                    case BinaryExpression be:
                        return be.Op;
                    case FunctionCallExpression fe:
                        return fe.FunctionName;
                    case NewNodeListInitExpression:
                        return "{}";
                    case AxisExpression ae:
                    {
                        if (ae.AxisName == "that")
                            return null;
                        return "$" + ae.AxisName;
                    }
                    case VariableRefExpression ve:
                        return "%" + ve.Name;
                }
#if DEBUG
                Debugger.Break();
#endif
                throw new Exception($"Unknown expression type: {expr.GetType().Name}");
            }

            public void DumpDiagnostics()
            {
                foreach (var item in traceOutput)
                {
                    System.Diagnostics.Trace.WriteLine(item);
                }
            }

            public string DebugTraceValue(Expression expr, ITypedElement? item)
            {
                string exprName = TraceExpressionNodeName(expr);
                if (item == null)
                    return null; // possible with a null focus to kick things off

                if (item.Location == "@primitivevalue@" || item.Location == "@QuantityAsPrimitiveValue@")
                    return $"{exprName}:\t{item.Value}\t({item.InstanceType})";

                return $"{exprName}:\t{item.Value}\t({item.InstanceType})\t{item.Location}";
            }
        }

        [TestMethod]
        public void testDebugTrace_PropertyWalking()
        {
            var expression = "Patient.birthDate.toString().substring(0, 4)";
            var input = fixture.PatientExample.ToTypedElement().ToScopedNode();
            var tracer = new TestDebugTracer();
            var expr = compiler.Compile(expression, true);
            var results = expr(input, new FhirEvaluationContext() { DebugTracer = tracer }).ToFhirValues().ToList();
            System.Diagnostics.Trace.WriteLine("Expression: " + expression);
            tracer.DumpDiagnostics();

            Assert.AreEqual(1, results.Count());
            Assert.AreEqual("1974", results[0].ToString());

            Assert.AreEqual(6, tracer.traceOutput.Count());
            Assert.AreEqual("0,7,Patient: focus=1 result=1", tracer.traceOutput[0]);
            Assert.AreEqual("8,9,birthDate: focus=1 result=1", tracer.traceOutput[1]);
            Assert.AreEqual("18,8,toString: focus=1 result=1", tracer.traceOutput[2]);
            Assert.AreEqual("39,1,constant: focus=1 result=1", tracer.traceOutput[3]);
            Assert.AreEqual("42,1,constant: focus=1 result=1", tracer.traceOutput[4]);
            Assert.AreEqual("29,9,substring: focus=1 result=1", tracer.traceOutput[5]);
        }

        [TestMethod]
        public void testDebugTrace_WhereClause()
        {
            var expression = "name.where(use='official' or use='usual').given";

            var input = fixture.PatientExample.ToTypedElement().ToScopedNode();
            var tracer = new TestDebugTracer();
            var expr = compiler.Compile(expression, true);
            var results = expr(input, new FhirEvaluationContext() { DebugTracer = tracer }).ToList();
            System.Diagnostics.Trace.WriteLine("Expression: " + expression);
            tracer.DumpDiagnostics();

            Assert.AreEqual(3, results.Count());
            Assert.AreEqual("Peter", results[0].Value.ToString());
            Assert.AreEqual("James", results[1].Value.ToString());
            Assert.AreEqual("Jim", results[2].Value.ToString());

            Assert.AreEqual("Patient.name[0].given[0]", results[0].Location);
            Assert.AreEqual("Patient.name[0].given[1]", results[1].Location);
            Assert.AreEqual("Patient.name[1].given[0]", results[2].Location);

            Assert.AreEqual(14, tracer.traceOutput.Count());
            Assert.AreEqual("0,4,name: focus=1 result=2", tracer.traceOutput[0]);
            Assert.AreEqual("11,3,use: focus=1 result=1", tracer.traceOutput[1]);
            Assert.AreEqual("15,10,constant: focus=1 result=1", tracer.traceOutput[2]);
            Assert.AreEqual("14,1,=: focus=1 result=1", tracer.traceOutput[3]);
            Assert.AreEqual("26,2,or: focus=1 result=1", tracer.traceOutput[4]);
            Assert.AreEqual("11,3,use: focus=1 result=1", tracer.traceOutput[5]);
            Assert.AreEqual("15,10,constant: focus=1 result=1", tracer.traceOutput[6]);
            Assert.AreEqual("14,1,=: focus=1 result=1", tracer.traceOutput[7]);
            Assert.AreEqual("29,3,use: focus=1 result=1", tracer.traceOutput[8]);
            Assert.AreEqual("33,7,constant: focus=1 result=1", tracer.traceOutput[9]);
            Assert.AreEqual("32,1,=: focus=1 result=1", tracer.traceOutput[10]);
            Assert.AreEqual("26,2,or: focus=1 result=1", tracer.traceOutput[11]);
            Assert.AreEqual("5,5,where: focus=2 result=2", tracer.traceOutput[12]);
            Assert.AreEqual("42,5,given: focus=2 result=3", tracer.traceOutput[13]);
        }

        [TestMethod]
        public void testDebugTrace_ConstantValues()
        {
            var expression = "'42'";

            var input = fixture.PatientExample.ToTypedElement().ToScopedNode();
            var tracer = new TestDebugTracer();
            var expr = compiler.Compile(expression, true);
            var results = expr(input, new FhirEvaluationContext() { DebugTracer = tracer }).ToFhirValues().ToList();
            System.Diagnostics.Trace.WriteLine("Expression: " + expression);
            tracer.DumpDiagnostics();

            Assert.AreEqual(1, results.Count());
            Assert.AreEqual("42", results[0].ToString());

            Assert.AreEqual(1, tracer.traceOutput.Count());
            Assert.AreEqual("0,4,constant: focus=1 result=1", tracer.traceOutput[0]);
        }

        [TestMethod]
        public void testDebugTrace_GroupedOr()
        {
            var expression = "id='official' or id='example'";

            var input = fixture.PatientExample.ToTypedElement().ToScopedNode();
            var tracer = new TestDebugTracer();
            var expr = compiler.Compile(expression, true);
            var results = expr(input, new FhirEvaluationContext() { DebugTracer = tracer }).ToFhirValues().ToList();
            System.Diagnostics.Trace.WriteLine("Expression: " + expression);
            tracer.DumpDiagnostics();

            Assert.AreEqual(1, results.Count());
            Assert.AreEqual("true", results[0].ToString());

            Assert.AreEqual(7, tracer.traceOutput.Count());
            Assert.AreEqual("0,2,id: focus=1 result=1", tracer.traceOutput[0]);
            Assert.AreEqual("3,10,constant: focus=1 result=1", tracer.traceOutput[1]);
            Assert.AreEqual("2,1,=: focus=1 result=1", tracer.traceOutput[2]);
            Assert.AreEqual("17,2,id: focus=1 result=1", tracer.traceOutput[3]);
            Assert.AreEqual("20,9,constant: focus=1 result=1", tracer.traceOutput[4]);
            Assert.AreEqual("19,1,=: focus=1 result=1", tracer.traceOutput[5]);
            Assert.AreEqual("14,2,or: focus=1 result=1", tracer.traceOutput[6]);
        }
    }
}