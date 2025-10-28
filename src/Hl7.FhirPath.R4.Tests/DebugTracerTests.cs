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
using Hl7.Fhir.FhirPath.R4.Tests;
using Hl7.FhirPath.Expressions;
using Hl7.FhirPath.R4.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
using BaseExtensions = Hl7.Fhir.Model.BaseExtensions;
using PocoNode = Hl7.Fhir.Model.PocoNode;
using PocoNodeExtensions = Hl7.Fhir.Model.PocoNodeExtensions;
using PrimitiveNode = Hl7.Fhir.Model.PrimitiveNode;

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
            private List<ExceptionDispatchInfo> exceptions = new List<ExceptionDispatchInfo>();

            public void Assert()
            {
                if (exceptions.Count == 0)
                    return; // no exceptions to throw
                System.Diagnostics.Trace.WriteLine($"Tracer exceptions: {exceptions.Count}");
                foreach (var item in exceptions)
                {
                    item.Throw();
                }
            }


            public void TraceCall(
                Expression expr,
                int contextId,
                IEnumerable<PocoNode> focus,
                IEnumerable<PocoNode> thisValue,
                PocoNode index,
                IEnumerable<PocoNode> totalValue,
                IEnumerable<PocoNode> result,
                IEnumerable<KeyValuePair<string, IEnumerable<PocoNode>>> variables)
            {
                // DiagnosticsDebugTracer.DebugTraceCall(expr, contextId, focus, thisValue, index, totalValue, result, variables);

                var exprName = TraceExpressionNodeName(expr);
                if (exprName == null)
                    return; // this is a node that we aren't interested in tracing (Identifier and $that)
                var pi = expr.Location as FhirPathExpressionLocationInfo;
                string output = $"{pi.RawPosition},{pi.Length},{exprName}:" +
                                $" focus={focus?.Count() ?? 0} result={result?.Count() ?? 0}";
                traceOutput.Add(output);
                if (TraceNode != null)
                {
                    try
                    {
                        TraceNode(traceOutput.Count - 1, expr, contextId,
                            focus, thisValue, index, totalValue, result);
                    }
                    catch(Exception e)
                    {
                        // swallow the exception while tracing during testing, then after evaluation
                        // is complete, we can throw them.
                        exceptions.Add(ExceptionDispatchInfo.Capture(e));
                    }
                }
            }

            public delegate void TraceNodeDelegate(int n, Expression expr, int contextId,
                IEnumerable<PocoNode> focus,
                IEnumerable<PocoNode> thisValue,
                PocoNode index,
                IEnumerable<PocoNode> totalValue,
                IEnumerable<PocoNode> result);
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
                System.Diagnostics.Trace.WriteLine("---");
                foreach (var item in traceOutput)
                {
                    System.Diagnostics.Trace.WriteLine(item);
                }
            }

            public string DebugTraceValue(PocoNode item)
            {
                if (item == null)
                    return null; // possible with a null focus to kick things off

                return $"{PocoNodeExtensions.GetValue(item)}\t({item.Poco.TypeName})\t{PocoNodeExtensions.GetLocation(item)}";
            }
        }

        [TestMethod]
        public void testDebugTrace_PropertyWalking()
        {
            var expression = "Patient.birthDate.toString().substring(0, 4)";
            var input = BaseExtensions.ToPocoNode(fixture.PatientExample);
            var tracer = new TestDebugTracer();
            tracer.TraceNode = (n, expr, contextId, focus, thisValue, index, totalValue, result) =>
            {
                DiagnosticsDebugTracer.DebugTraceCall(expr, contextId, focus, thisValue, index, totalValue, result, null);
                var vThis = tracer.DebugTraceValue(thisValue?.FirstOrDefault());
                Assert.AreEqual("\t(Patient)\tPatient", vThis);
                var vFocus = tracer.DebugTraceValue(focus?.FirstOrDefault());
                var vResult = tracer.DebugTraceValue(result?.FirstOrDefault());

                if (n == 2)
                {
                    // toString
                    Assert.AreEqual("1974-12-25\t(date)\tPatient.birthDate[0]", vFocus);
                    Assert.AreEqual("1974-12-25\t(System.String)\tSystem.String", vResult);
                }
                if (n == 3)
                {
                    // constant 0
                    Assert.AreEqual("\t(Patient)\tPatient", vFocus);
                    Assert.AreEqual("0\t(System.Integer)\tSystem.Integer", vResult);
                }
                if (n == 4)
                {
                    // constant 4
                    Assert.AreEqual("\t(Patient)\tPatient", vFocus);
                    Assert.AreEqual("4\t(System.Integer)\tSystem.Integer", vResult);
                }
                if (n == 5)
                {
                    // substring
                    Assert.AreEqual("1974-12-25\t(System.String)\tSystem.String", vFocus);
                    Assert.AreEqual("1974\t(System.String)\tSystem.String", vResult);
                }
            };
            var expr = compiler.Compile(expression, true);
            Trace.WriteLine("Expression: " + expression + "\r\n");
            var results = expr(input, new FhirEvaluationContext() { DebugTracer = tracer }).ToFhirValues().ToList();
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

            // Now check the tracer assertions
            tracer.Assert();
        }

        [TestMethod]
        public void testDebugTrace_PropertyAndFunctionCalls()
        {
            var expression = "Patient.id.indexOf('am')";
            var input = BaseExtensions.ToPocoNode(fixture.PatientExample);
            var tracer = new TestDebugTracer();
            tracer.TraceNode = (n, expr, contextId, focus, thisValue, index, totalValue, result) =>
            {
                DiagnosticsDebugTracer.DebugTraceCall(expr, contextId, focus, thisValue, index, totalValue, result, null);
                var vThis = tracer.DebugTraceValue(thisValue?.FirstOrDefault());
                var vFocus = tracer.DebugTraceValue(focus?.FirstOrDefault());
                var vResult = tracer.DebugTraceValue(result?.FirstOrDefault());
                Assert.AreEqual("\t(Patient)\tPatient", vThis); // in this specific expression, this is always the patient
                if (n == 2)
                {
                    // the context and results of the constant 'am' call
                    Assert.AreEqual("\t(Patient)\tPatient", vFocus);
                    Assert.AreEqual("am\t(System.String)\tSystem.String", vResult);
                }
                if (n == 3)
                {
                    // the context and results of indexOf call
                    Assert.AreEqual("example\t(id)\tPatient.id[0]", vFocus);
                    Assert.AreEqual("2\t(integer)\tinteger", vResult);
                }
            };
            var expr = compiler.Compile(expression, true);
            Trace.WriteLine("Expression: " + expression + "\r\n");
            var results = expr(input, new FhirEvaluationContext() { DebugTracer = tracer }).ToFhirValues().ToList();
            tracer.DumpDiagnostics();

            Assert.AreEqual(1, results.Count());
            Assert.AreEqual("2", results[0].ToString());

            Assert.AreEqual(4, tracer.traceOutput.Count());
            Assert.AreEqual("0,7,Patient: focus=1 result=1", tracer.traceOutput[0]);
            Assert.AreEqual("8,2,id: focus=1 result=1", tracer.traceOutput[1]);
            Assert.AreEqual("19,4,constant: focus=1 result=1", tracer.traceOutput[2]);
            Assert.AreEqual("11,7,indexOf: focus=1 result=1", tracer.traceOutput[3]);

            // Now check the tracer assertions
            tracer.Assert();
        }

        [TestMethod]
        public void testDebugTrace_Aggregate()
        {
            var expression = "(1|2).aggregate($total+$this, 0)";
            var input = BaseExtensions.ToPocoNode(fixture.PatientExample);
            var tracer = new TestDebugTracer();
            tracer.TraceNode = (n, expr, contextId, focus, thisValue, index, totalValue, result) =>
            {
                // TODO: Check the focus values.
                if (n == 2)
                {
                    // the results of the | operator
                    DiagnosticsDebugTracer.DebugTraceCall(expr, contextId, focus, thisValue, index, totalValue, result, null);
                    var vThis = tracer.DebugTraceValue(thisValue?.FirstOrDefault());
                    var vFocus = tracer.DebugTraceValue(focus?.FirstOrDefault());
                    var vResult1 = tracer.DebugTraceValue(result?.FirstOrDefault());
                    var vResult2 = tracer.DebugTraceValue(result?.Skip(1)?.FirstOrDefault());
                    Assert.AreEqual(0, contextId);
                }
                if (n == 3) {
                    // the results of the constant "0" for the init expression
                    DiagnosticsDebugTracer.DebugTraceCall(expr, contextId, focus, thisValue, index, totalValue, result, null);
                    var v1 = tracer.DebugTraceValue(focus?.FirstOrDefault());
                    var v2 = tracer.DebugTraceValue(focus?.Skip(1)?.FirstOrDefault());
                    // Assert.AreEqual(3, contextId);
                }
            };

            var expr = compiler.Compile(expression, true);
            Trace.WriteLine("Expression: " + expression + "\r\n");
            var results = expr(input, new FhirEvaluationContext() { DebugTracer = tracer }).ToFhirValues().ToList();
            tracer.DumpDiagnostics();

            Assert.AreEqual(1, results.Count());
            Assert.AreEqual("3", results[0].ToString());

            // Now check the tracer outputs
            Assert.AreEqual(11, tracer.traceOutput.Count());
            int n = 0;
            Assert.AreEqual("1,1,constant: focus=1 result=1", tracer.traceOutput[n++]);
            Assert.AreEqual("3,1,constant: focus=1 result=1", tracer.traceOutput[n++]);
            Assert.AreEqual("2,1,|: focus=1 result=2", tracer.traceOutput[n++]);
            Assert.AreEqual("30,1,constant: focus=1 result=1", tracer.traceOutput[n++]);
            Assert.AreEqual("16,6,$total: focus=1 result=1", tracer.traceOutput[n++]);
            Assert.AreEqual("23,5,$this: focus=1 result=1", tracer.traceOutput[n++]);
            Assert.AreEqual("22,1,+: focus=1 result=1", tracer.traceOutput[n++]);
            Assert.AreEqual("16,6,$total: focus=1 result=1", tracer.traceOutput[n++]);
            Assert.AreEqual("23,5,$this: focus=1 result=1", tracer.traceOutput[n++]);
            Assert.AreEqual("22,1,+: focus=1 result=1", tracer.traceOutput[n++]);
            Assert.AreEqual("6,9,aggregate: focus=2 result=1", tracer.traceOutput[n++]);

            // Now check the tracer assertions
            tracer.Assert();
        }

        [TestMethod]
        public void testDebugTrace_Operator()
        {
            var expression = "Patient.id.toString() = Patient.id";
            var input = BaseExtensions.ToPocoNode(fixture.PatientExample);
            var tracer = new TestDebugTracer();
            tracer.TraceNode = (n, expr, contextId, focus, thisValue, index, totalValue, result) =>
            {
                DiagnosticsDebugTracer.DebugTraceCall(expr, contextId, focus, thisValue, index, totalValue, result, null);
                var vThis = tracer.DebugTraceValue(thisValue?.FirstOrDefault());
                Assert.AreEqual("\t(Patient)\tPatient", vThis);
                var vFocus = tracer.DebugTraceValue(focus?.FirstOrDefault());
                if (n == 2)
                {
                    // the context and results of toString call
                    var vResult = tracer.DebugTraceValue(result?.FirstOrDefault());
                    Assert.AreEqual("example\t(id)\tPatient.id[0]", vFocus);
                    Assert.AreEqual("example\t(System.String)\tSystem.String", vResult);
                }
            };

            var expr = compiler.Compile(expression, true);
            Trace.WriteLine("Expression: " + expression + "\r\n");
            var results = expr(input, new FhirEvaluationContext() { DebugTracer = tracer }).ToFhirValues().ToList();
            tracer.DumpDiagnostics();

            Assert.AreEqual(1, results.Count());
            Assert.AreEqual("true", results[0].ToString());

            // Now check the tracer outputs
            Assert.AreEqual(6, tracer.traceOutput.Count());
            int n = 0;
            Assert.AreEqual("0,7,Patient: focus=1 result=1", tracer.traceOutput[n++]);
            Assert.AreEqual("8,2,id: focus=1 result=1", tracer.traceOutput[n++]);
            Assert.AreEqual("11,8,toString: focus=1 result=1", tracer.traceOutput[n++]);
            Assert.AreEqual("24,7,Patient: focus=1 result=1", tracer.traceOutput[n++]);
            Assert.AreEqual("32,2,id: focus=1 result=1", tracer.traceOutput[n++]);
            Assert.AreEqual("22,1,=: focus=1 result=1", tracer.traceOutput[n++]);

            // Now check the tracer assertions
            tracer.Assert();
        }

        [TestMethod]
        public void testDebugTrace_WhereClause()
        {
            var expression = "name.where(use='official' or use='usual').given";

            var input = BaseExtensions.ToPocoNode(fixture.PatientExample);
            var tracer = new TestDebugTracer();
            tracer.TraceNode = (n, expr, contextId, focus, thisValue, index, totalValue, result) =>
            {
                DiagnosticsDebugTracer.DebugTraceCall(expr, contextId, focus, thisValue, index, totalValue, result, null);
                var vThis = tracer.DebugTraceValue(thisValue?.FirstOrDefault());
                var vFocus = tracer.DebugTraceValue(focus?.FirstOrDefault());
                var vResult = tracer.DebugTraceValue(result?.FirstOrDefault());
                var vIndex= PocoNodeExtensions.GetValue(index);
                if (n == 0)
                {
                    // name
                    Assert.AreEqual("\t(Patient)\tPatient", vThis);
                    Assert.AreEqual("\t(Patient)\tPatient", vFocus);
                    Assert.AreEqual(2, result.Count());
                }

                if (n == 1 || n == 2 || n == 3 || n == 4)
                {
                    Assert.AreEqual("\t(HumanName)\tPatient.name[0]", vThis);
                    Assert.AreEqual("\t(HumanName)\tPatient.name[0]", vFocus);
                    Assert.AreEqual(0, vIndex);
                }
                if (n >= 5 && n <= 11)
                {
                    Assert.AreEqual("\t(HumanName)\tPatient.name[1]", vThis);
                    Assert.AreEqual("\t(HumanName)\tPatient.name[1]", vFocus);
                    Assert.AreEqual(1, vIndex);
                }

                if (n == 12)
                {
                    // Where clause
                    Assert.AreEqual("\t(Patient)\tPatient", vThis);
                    Assert.AreEqual("\t(HumanName)\tPatient.name[0]", vFocus);
                    Assert.AreEqual(2, focus.Count());
                    Assert.AreEqual(2, result.Count());
                    Assert.AreEqual("\t(HumanName)\tPatient.name[0]", vResult);
                }
                if (n == 13)
                {
                    // The final given prop navigator
                    Assert.AreEqual("\t(Patient)\tPatient", vThis);
                    Assert.AreEqual("\t(HumanName)\tPatient.name[0]", vFocus);
                    Assert.AreEqual(2, focus.Count());
                    Assert.AreEqual(3, result.Count());
                }
            };
            var expr = compiler.Compile(expression, true);
            Trace.WriteLine("Expression: " + expression + "\r\n");
            var results = expr(input, new FhirEvaluationContext() { DebugTracer = tracer }).ToList();
            tracer.DumpDiagnostics();

            Assert.AreEqual(3, results.Count());
            Assert.AreEqual("Peter", PocoNodeExtensions.GetValue(results[0])?.ToString());
            Assert.AreEqual("James", PocoNodeExtensions.GetValue(results[1])?.ToString());
            Assert.AreEqual("Jim", PocoNodeExtensions.GetValue(results[2])?.ToString());

            Assert.AreEqual("Patient.name[0].given[0]", PocoNodeExtensions.GetLocation(results[0]));
            Assert.AreEqual("Patient.name[0].given[1]", PocoNodeExtensions.GetLocation(results[1]));
            Assert.AreEqual("Patient.name[1].given[0]", PocoNodeExtensions.GetLocation(results[2]));

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

            // Now check the tracer assertions
            tracer.Assert();
        }

        [TestMethod]
        public void testDebugTrace_ConstantValues()
        {
            var expression = "'42'";

            var input = BaseExtensions.ToPocoNode(fixture.PatientExample);
            var tracer = new TestDebugTracer();
            tracer.TraceNode = (n, expr, contextId, focus, thisValue, index, totalValue, result) =>
            {
                DiagnosticsDebugTracer.DebugTraceCall(expr, contextId, focus, thisValue, index, totalValue, result, null);
            };
            var expr = compiler.Compile(expression, true);
            Trace.WriteLine("Expression: " + expression + "\r\n");
            var results = expr(input, new FhirEvaluationContext() { DebugTracer = tracer }).ToFhirValues().ToList();
            tracer.DumpDiagnostics();

            Assert.AreEqual(1, results.Count());
            Assert.AreEqual("42", results[0].ToString());

            Assert.AreEqual(1, tracer.traceOutput.Count());
            Assert.AreEqual("0,4,constant: focus=1 result=1", tracer.traceOutput[0]);

            // Now check the tracer assertions
            tracer.Assert();
        }

        [TestMethod]
        public void testDebugTrace_GroupedOr()
        {
            var expression = "id='official' or id='example'";

            var input = BaseExtensions.ToPocoNode(fixture.PatientExample);
            var tracer = new TestDebugTracer();
            tracer.TraceNode = (n, expr, contextId, focus, thisValue, index, totalValue, result) =>
            {
                DiagnosticsDebugTracer.DebugTraceCall(expr, contextId, focus, thisValue, index, totalValue, result, null);

                // interestingly all the nodes in this expression have the same focus and $this value
                var vThis = tracer.DebugTraceValue(thisValue?.FirstOrDefault());
                var vFocus = tracer.DebugTraceValue(focus?.FirstOrDefault());
                Assert.AreEqual("\t(Patient)\tPatient", vThis);
                Assert.AreEqual("\t(Patient)\tPatient", vFocus);


            };
            var expr = compiler.Compile(expression, true);
            Trace.WriteLine("Expression: " + expression + "\r\n");
            var results = expr(input, new FhirEvaluationContext() { DebugTracer = tracer }).ToFhirValues().ToList();
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

            // Now check the tracer assertions
            tracer.Assert();
        }
    }
}