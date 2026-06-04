/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

#nullable enable

namespace Hl7.FhirPath.Expressions
{
    /// <summary>
    /// A simple visitor that can produce a canonical string representation of an expression
    /// It will normalize any whitespace into single spaces and remove any comments
    /// </summary>
    /// <remarks>
    /// Delimiters are removed from delimited identifiers that don't require them<br/>
    /// Brackets are however retained where they are included in the original expression
    /// </remarks>
    public class CanonicalVisitor : ExpressionVisitor<StringBuilder>
    {
        private readonly StringBuilder _result = new StringBuilder();

        private void OutputIdentifierName(string identifier)
        {
            // verify that the identifier only contains valid chars in A-Za-Z0-9
            if (Regex.IsMatch(identifier, "^[A-Za-z]+[A-Za-z0-9]*$", RegexOptions.Singleline))
            {
                _result.Append($"{identifier}");
                return;
            }
            // delimit the output string
            _result.Append($"`{identifier.Replace("`","\\`")}`");
        }

        public override StringBuilder VisitConstant(ConstantExpression expression)
        {
            if (expression is IdentifierExpression identifier)
            {
                OutputIdentifierName(identifier.Value);
                return _result;
            }
            var t = Fhir.Serialization.PrimitiveTypeConverter.ConvertTo<string>(expression.Value);

            switch (expression.ExpressionType.Name)
            {
                case "Any":
                    _result.Append($"{t}");
                    break;
                case "Boolean":
                    _result.Append($"{t}");
                    break;
                case "Code":
                    _result.Append($"{t}");
                    break;
                case "Concept":
                    _result.Append($"{t}");
                    break;
                case "Date":
                    _result.Append($"@{t}");
                    break;
                case "DateTime":
                    if (t.Contains('T'))
                        _result.Append($"@{t}");
                    else
                        _result.Append($"@{t}T");
                    break;
                case "Decimal":
                    _result.Append($"{t}");
                    break;
                case "Integer":
                    _result.Append($"{t}");
                    break;
                case "Long":
                    _result.Append($"{t}");
                    break;
                case "Quantity":
                    _result.Append($"{t}");
                    break;
                case "String":
                    _result.Append($"'{t}'");
                    break;
                case "Ratio":
                    _result.Append($"{t}");
                    break;
                case "Time":
                    _result.Append($"@T{t}");
                    break;
                case "Void":
                    break;
            }

            return _result;
        }

        public override StringBuilder VisitFunctionCall(FunctionCallExpression expression)
        {
            if (expression.FunctionName == "builtin.coreexturl")
            {
                expression.Focus.Accept(this);
                _result.Append("%`ext-");
                if (expression.Arguments.FirstOrDefault() is ConstantExpression ceVar)
                    _result.Append($"{ceVar.Value}");
                _result.Append("`");
                return _result;
            }
            if (expression.FunctionName == "builtin.corevsurl")
            {
                expression.Focus.Accept(this);
                _result.Append("%`vs-");
                if (expression.Arguments.FirstOrDefault() is ConstantExpression ceVar)
                    _result.Append($"{ceVar.Value}");
                _result.Append("`");
                return _result;
            }
            if (expression is UnaryExpression ue)
            {
                ue.Focus.Accept(this);
                _result.Append($"{ue.Op}");
                ue.Arguments.FirstOrDefault()?.Accept(this);
                return _result;
            }
            if (expression is BinaryExpression be)
            {
                be.Focus.Accept(this);
                be.Arguments.FirstOrDefault()?.Accept(this);
                _result.Append($" {be.Op} ");
                be.Arguments.Skip(1).FirstOrDefault()?.Accept(this);
                return _result;
            }
            if (expression is IndexerExpression ie)
            {
                ie.Focus.Accept(this);
                _result.Append($"[");
                ie.Arguments.FirstOrDefault()?.Accept(this);
                _result.Append($"]");
                return _result;
            }

            expression.Focus.Accept(this);
            if (!(expression.Focus is VariableRefExpression vrf && vrf.Name == "builtin.that" || expression.Focus is AxisExpression aeFocus && aeFocus.AxisName == "that"))
                _result.Append($".");
            if (expression is ChildExpression ce)
            {
                OutputIdentifierName(ce.ChildName);
                return _result;
            }
            OutputIdentifierName(expression.FunctionName);
            _result.Append("(");

            expression.Arguments.FirstOrDefault()?.Accept(this);
            foreach (var arg in expression.Arguments.Skip(1))
            {
                _result.Append(", ");
                arg.Accept(this);
            }
            _result.Append(")");

            return _result;
        }

        public override StringBuilder VisitNewNodeListInit(NewNodeListInitExpression expression)
        {
            _result.Append("{");
            foreach (var element in expression.Contents)
                element.Accept(this);
            _result.Append("}");
            return _result;
        }

        public override StringBuilder VisitVariableRef(VariableRefExpression expression)
        {
            if (expression is AxisExpression ae)
            {
                // No need to output the `that` type
                if (ae.AxisName == "that")
                    return _result;

                _result.Append($"${ae.AxisName}");
                return _result;
            }
            if (expression.Name != "builtin.that")
            {
                _result.Append("%");
                OutputIdentifierName(expression.Name);
            }
            return _result;
        }

        public override StringBuilder VisitCustomExpression(CustomExpression expression)
        {
            if (expression is BracketExpression be)
            {
                _result.Append("(");
                be.Operand.Accept(this);
                _result.Append(")");
                return _result;
            }
            base.VisitCustomExpression(expression);
            return _result;
        }
    }

    public static class CanonicalVisitorExtensions
    {
        /// <summary>
        /// Create a canonical string representation from an expression tree,
        /// Normalizing any whitespace into single spaces and remove any comments
        /// </summary>
        /// <remarks>
        /// Delimiters are removed from delimited identifiers that don't require them
        /// Brackets are however retained where they are included in the original expression<br/>
        /// If you want to see the expression that was parsed, use <see cref="EchoVisitorExtensions.EchoExpression(Expression)">EchoExpression</see>
        /// </remarks>
        /// <param name="expr">The source Expression</param>
        /// <returns>A canonical string representation of the expression</returns>
        public static string ToCanonicalExpression(this Expression expr)
        {
            var dumper = new CanonicalVisitor();
            return expr.Accept(dumper).ToString();
        }
    }

}
