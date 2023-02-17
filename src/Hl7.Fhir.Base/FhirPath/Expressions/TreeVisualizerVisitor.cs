/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */
using Hl7.Fhir.Language;
using Hl7.Fhir.Utility;
using System;
using System.Text;

namespace Hl7.FhirPath.Expressions
{
    public class TreeVisualizerVisitor : ExpressionVisitor<StringBuilder>
    {
        private readonly StringBuilder _result = new StringBuilder();
        private int _indent = 0;

        public override StringBuilder VisitConstant(ConstantExpression expression)
        {
            append("const {0}".FormatWith(expression.Value));
            appendType(expression);
            return _result;
        }

        public override StringBuilder VisitFunctionCall(FunctionCallExpression expression)
        {
            append("func {0}".FormatWith(expression.FunctionName));
            appendType(expression);

            incr();
            expression.Focus.Accept(this);

            foreach (var arg in expression.Arguments)
                arg.Accept(this);
            decr();

            return _result;
        }

        //public override StringBuilder VisitLambda(LambdaExpression expression)
        //{
        //    append("lambda $this -> ");
        //    appendType(expression);

        //    incr();
        //    expression.Body.Accept(this);
        //    decr();

        //    return _result;
        //}

        public override StringBuilder VisitNewNodeListInit(NewNodeListInitExpression expression)
        {
            append("new NodeSet");
            appendType(expression);

            incr();
            foreach (var element in expression.Contents)
                element.Accept(this);
            decr();

            return _result;
        }

        public override StringBuilder VisitVariableRef(VariableRefExpression expression)
        {
            append("var {0}".FormatWith(expression.Name));
            appendType(expression);

            return _result;
        }

        private void appendType(Expression expr)
        {
            if (expr.ExpressionType != TypeSpecifier.Any)
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
            return expr.Accept(dumper).ToString();
        }
    }

}
