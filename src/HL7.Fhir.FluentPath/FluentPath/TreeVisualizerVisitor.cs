using Hl7.Fhir.Support;
using HL7.Fhir.FluentPath.FluentPath.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL7.Fhir.FluentPath.FluentPath
{
    internal class TreeVisualizerVisitor : ExpressionVisitor
    {
        private StringBuilder _result = new StringBuilder();
        private int _indent = 0;

        public string Result
        {
            get { return _result.ToString(); }
        }

        public override void VisitConstant(ConstantExpression expression)
        {
            append("const {0}".FormatWith(expression.Value));
            append(expression);
        }

        public override void VisitFunctionCall(FunctionCallExpression expression)
        {
            append("func {0}".FormatWith(expression.FunctionName));
            append(expression);

            incr();
            expression.Focus.Accept(this);

            foreach (var arg in expression.Arguments)
                arg.Accept(this);
            decr();
        }

        public override void VisitLambda(LambdaExpression expression)
        {
            append("lambda $this -> ");
            append(expression);

            incr();
            expression.Body.Accept(this);
            decr();
        }

        public override void VisitNewNodeListInit(NewNodeListInitExpression expression)
        {
            append("new NodeSet");
            append(expression);

            incr();
            foreach (var element in expression.Contents)
                element.Accept(this);
            decr();
        }

        public override void VisitVariableRef(VariableRefExpression expression)
        {
            append("var {0}".FormatWith(expression.Name));
            append(expression);
        }

        private void append(Expression expr)
        {
            if (expr.ExpressionType != FluentPathType.Any)
                append(" : {0}".FormatWith(expr.ExpressionType), newLine: false);
        }

        private void append(string text, bool newLine = true)
        {
            if (newLine)
            {
                _result.AppendLine();
                _result.Append(new String(' ', _indent * 4));
            }

            _result.Append(text);
        }

        private void incr()
        {           
            _indent += 1;        
        }

        private void decr()
        {
            _indent -= 1;
        }
    }

    public static class TreeVisualizerExpressionExtensions
    {
        public static string Dump(this Expression expr)
        {
            var dumper = new TreeVisualizerVisitor();
            expr.Accept(dumper);
            return dumper.Result;
        }
    }

}
