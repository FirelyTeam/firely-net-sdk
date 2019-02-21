/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */
using Hl7.Fhir.Utility;
using System;
using System.Text;

namespace Hl7.FhirPath.Expressions
{
    public class TreeVisualizerVisitor : ExpressionVisitor<StringBuilder>
    {
        private StringBuilder _result = new StringBuilder();
        private int _indent = 0;

        public override StringBuilder VisitConstant(ConstantExpression expression, SymbolTable scope)
        {
            append("const {0}".FormatWith(expression.Value));
            appendType(expression);
            nl();           

            return _result;
        }

        public override StringBuilder VisitFunctionCall(FunctionCallExpression expression, SymbolTable scope)
        {
            append("call {0}".FormatWith(expression.FunctionName));
            appendType(expression);
            nl();

            incr();
            expression.Focus.Accept(this, scope);

            for (int argpos = 0; argpos < expression.Arguments.Count; argpos++)
            {
                append($"arg {argpos}: ");
                expression.Arguments[argpos].Accept(this, scope);
            }
            decr();

            return _result;
        }

        public override StringBuilder VisitLambda(LambdaExpression expression, SymbolTable scope)
        {
            append($"lambda ({String.Join(",", expression.ParamNames)}) -> ");
            appendType(expression);
            nl();

            incr();
            expression.Body.Accept(this, scope);
            decr();

            return _result;
        }

        public override StringBuilder VisitNewNodeListInit(NewNodeListInitExpression expression, SymbolTable scope)
        {
            append("new NodeSet");
            appendType(expression);
            nl();

            incr();
            foreach (var element in expression.Contents)
                element.Accept(this, scope);
            decr();

            return _result;
        }

        public override StringBuilder VisitVariableRef(VariableRefExpression expression, SymbolTable scope)
        {
            append("varref {0}".FormatWith(expression.Name));
            appendType(expression);
            nl();

            return _result;
        }

        //public override StringBuilder VisitTypeBinaryExpression(TypeBinaryExpression expression)
        //{
        //    append("{0} {1}".FormatWith(expression.Op, expression.Type.Name));
        //    appendType(expression);

        //    return _result;
        //}

        private void appendType(Expression expr)
        {
            if (expr.ExpressionType != TypeInfo.Any)
                append(" : {0}".FormatWith(expr.ExpressionType));
        }

        private void append(string text)
        {
            _result.Append(new String(' ', _indent * 4));
            _result.Append(text);
        }

        private void nl() => _result.AppendLine();

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
            return expr.Accept(dumper, new SymbolTable()).ToString();
        }
    }

}
