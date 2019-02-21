using Hl7.FhirPath;
using Hl7.FhirPath.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HL7.FhirPath.Tests.Tests
{
    [TestClass]
    public class FhirPathExpressionTest
    {
        [TestMethod]
        public void CompileConstant()
        {
            var constantExpr = new ConstantExpression(1);
            var eval = constantExpr.ToEvaluator(new SymbolTable());
            var result = eval(EvaluationContext.CreateDefault(), new List<Invokee>());
            Assert.AreEqual(1L, result.Single().Value);
        }

        [TestMethod]
        public void VariableRef()
        {
            var constantExpr = new ConstantExpression(1).ToEvaluator(new SymbolTable());
            var nested = new SymbolTable();
            nested.AddValue("val1", constantExpr);
            var variableRefExpr = new VariableRefExpression("val1");
            var eval = variableRefExpr.ToEvaluator(nested);
            var result = eval(EvaluationContext.CreateDefault(), InvokeeFactory.EmptyArgs);
            Assert.AreEqual(1L, result.Single().Value);
        }

        private static Invokee compile(Expression e)
        {
            var symbols = new SymbolTable().AddStandardFP();
            symbols.AddValue("builtin.focus", InvokeeFactory.Return(new ConstantValue(0)));
            return e.ToEvaluator(symbols);
        }

        [TestMethod]
        public void NestedExpressions()
        {
            var b1 = new BinaryExpression("and", new ConstantExpression(true), new ConstantExpression(false));
            var b2 = new BinaryExpression('=', b1, new ConstantExpression(false));
            var eval = compile(b2);
            var result = eval(EvaluationContext.CreateDefault(), InvokeeFactory.EmptyArgs);
            Assert.AreEqual(false, result.Single().Value);
        }

        [TestMethod]
        public void CreateLambda()
        {
            var lambdaExpr = new LambdaExpression(new[] { "a", "b" },
                        new BinaryExpression('+',
                            new VariableRefExpression("a"), new VariableRefExpression("b")));
            var eval = compile(lambdaExpr);
            var args = new List<Invokee> { new ConstantExpression(4).ToEvaluator(), new ConstantExpression(5).ToEvaluator()  };
            var result = eval(EvaluationContext.CreateDefault(), args);
            Assert.AreEqual(9L, result.Single().Value);
        }
    }
}
