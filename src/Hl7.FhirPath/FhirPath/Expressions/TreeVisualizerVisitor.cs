/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/fhir-net-api/master/LICENSE
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
            return _result;
        }

        public override StringBuilder VisitFunctionCall(FunctionCallExpression expression, SymbolTable scope)
        {
            append("func {0}".FormatWith(expression.FunctionName));
            appendType(expression);

            incr();
            expression.Focus.Accept(this, scope);

            foreach (var arg in expression.Arguments)
                arg.Accept(this, scope);
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

        public override StringBuilder VisitNewNodeListInit(NewNodeListInitExpression expression, SymbolTable scope)
        {
            append("new NodeSet");
            appendType(expression);

            incr();
            foreach (var element in expression.Contents)
                element.Accept(this, scope);
            decr();

            return _result;
        }

        public override StringBuilder VisitVariableRef(VariableRefExpression expression, SymbolTable scope)
        {
            append("var {0}".FormatWith(expression.Name));
            appendType(expression);

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
            return expr.Accept(dumper, new SymbolTable()).ToString();
        }
    }

}
