/*
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using P = Hl7.Fhir.ElementModel.Types;

#nullable enable

namespace Hl7.FhirPath.Expressions
{
    /// <summary>
    ///
    /// </summary>
    public class EchoVisitor : ExpressionVisitor<StringBuilder>
    {
        private readonly StringBuilder _result = new StringBuilder();

        #region << output utilties>>
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

        private void OutputPrecedingTokens(Expression? expr)
        {
            if (expr?.LeadingWhitespace?.Any() == true)
                _result.Append(System.String.Join("", expr.LeadingWhitespace.Select(ws => ws.ToString())));
        }
        private void OutputTrailingTokens(Expression expr)
        {
            if (expr.TrailingWhitespace?.Any() == true)
                _result.Append(System.String.Join("", expr.TrailingWhitespace.Select(ws => ws.ToString())));
        }

        private void OutputSubToken(SubToken subtoken)
        {
            if (subtoken == null) return;
            OutputPrecedingTokens(subtoken);
            _result.Append($"{subtoken.Value}");
            OutputTrailingTokens(subtoken);
        }

        private void OutputPrecedingTokens(SubToken subtoken)
        {
            if (subtoken == null) return;
            if (subtoken.LeadingWhitespace?.Any() == true)
                _result.Append(System.String.Join("", subtoken.LeadingWhitespace.Select(ws => ws.ToString())));
        }
        private void OutputTrailingTokens(SubToken subtoken)
        {
            if (subtoken == null) return;
            if (subtoken.TrailingWhitespace?.Any() == true)
                _result.Append(System.String.Join("", subtoken.TrailingWhitespace.Select(ws => ws.ToString())));
        }
        #endregion

        public override StringBuilder VisitConstant(ConstantExpression expression)
        {
            OutputPrecedingTokens(expression);
            if (expression is IdentifierExpression identifier)
            {
                OutputIdentifierName(identifier.Value);
                OutputTrailingTokens(expression);
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
                    if (expression.Value is P.Quantity q)
                    {
                        _result.Append(q.Value.ToString(CultureInfo.InvariantCulture));
                        OutputSubToken(expression.Unit);
                    }
                    else
                    {
                        _result.Append($"{t}");
                    }
                    break;
                case "String":
                    _result.Append("'" + Functions.StringOperators.EscapeJson(t) + "'");
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

            OutputTrailingTokens(expression);
            return _result;
        }

        public override StringBuilder VisitFunctionCall(FunctionCallExpression expression)
        {
            if (expression.FunctionName == "builtin.coreexturl")
            {
                OutputPrecedingTokens(expression);
                expression.Focus.Accept(this);
                _result.Append("%`ext-");
                if (expression.Arguments.FirstOrDefault() is ConstantExpression ceVar)
                    _result.Append($"{ceVar.Value}");
                _result.Append("`");
                OutputTrailingTokens(expression);
                return _result;
            }
            if (expression.FunctionName == "builtin.corevsurl")
            {
                OutputPrecedingTokens(expression);
                expression.Focus.Accept(this);
                _result.Append("%`vs-");
                if (expression.Arguments.FirstOrDefault() is ConstantExpression ceVar)
                    _result.Append($"{ceVar.Value}");
                _result.Append("`");
                OutputTrailingTokens(expression);
                return _result;
            }
            if (expression is SortDirectionExpression sd)
            {
                sd.Focus.Accept(this);
                sd.Arguments.FirstOrDefault()?.Accept(this);
                OutputPrecedingTokens(sd);
                _result.Append($"{sd.Op}");
                OutputTrailingTokens(expression);
                return _result;
            }
            if (expression is UnaryExpression ue)
            {
                OutputPrecedingTokens(expression);
                ue.Focus.Accept(this);
                _result.Append($"{ue.Op}");
                ue.Arguments.FirstOrDefault()?.Accept(this);
                OutputTrailingTokens(expression);
                return _result;
            }
            if (expression is BinaryExpression be)
            {
                OutputPrecedingTokens(expression);
                be.Focus.Accept(this);
                be.Arguments.FirstOrDefault()?.Accept(this);
                if (be.OpToken != null)
                    OutputSubToken(be.OpToken);
                else
                    _result.Append($"{be.Op}");
                be.Arguments.Skip(1).FirstOrDefault()?.Accept(this);
                OutputTrailingTokens(expression);
                return _result;
            }
            if (expression is IndexerExpression ie)
            {
                OutputPrecedingTokens(expression);
                ie.Focus.Accept(this);
                OutputSubToken(expression.LeftBrace);
                ie.Arguments.FirstOrDefault()?.Accept(this);
                OutputSubToken(expression.RightBrace);
                OutputTrailingTokens(expression);
                return _result;
            }

            OutputPrecedingTokens(expression);
            expression.Focus.Accept(this);
            if (!(expression.Focus is VariableRefExpression vrf && vrf.Name == "builtin.that" || expression.Focus is AxisExpression aeFocus && aeFocus.AxisName == "that"))
            {
                _result.Append($".");
            }
            if (expression is ChildExpression ce)
            {
                var ca = ce.Arguments.FirstOrDefault();
                OutputPrecedingTokens(ca);
                OutputIdentifierName(ce.ChildName);
                OutputTrailingTokens(expression);
                return _result;
            }
            OutputIdentifierName(expression.FunctionName);
            OutputSubToken(expression.LeftBrace);

            expression.Arguments.FirstOrDefault()?.Accept(this);
            foreach (var arg in expression.Arguments.Skip(1))
            {
                _result.Append(",");
                arg.Accept(this);
            }
            OutputSubToken(expression.RightBrace);
            OutputTrailingTokens(expression);

            return _result;
        }

        public override StringBuilder VisitNewNodeListInit(NewNodeListInitExpression expression)
        {
            OutputPrecedingTokens(expression);
            OutputSubToken(expression.LeftBrace);
            foreach (var element in expression.Contents)
                element.Accept(this);
            OutputSubToken(expression.RightBrace);
            OutputTrailingTokens(expression);
            return _result;
        }

        public override StringBuilder VisitVariableRef(VariableRefExpression expression)
        {
            if (expression is AxisExpression ae)
            {
                // No need to output the `that` type
                if (ae.AxisName == "that" || ae.AxisName == "this" && ae.Location == null)
                    return _result;

                OutputPrecedingTokens(expression);
                _result.Append($"${ae.AxisName}");
                OutputTrailingTokens(expression);
                return _result;
            }
            if (expression.Name != "builtin.that")
            {
                OutputPrecedingTokens(expression);
                _result.Append("%");
                OutputIdentifierName(expression.Name);
                OutputTrailingTokens(expression);
            }
            return _result;
        }

        public override StringBuilder VisitCustomExpression(CustomExpression expression)
        {
            if (expression is BracketExpression be)
            {
                OutputPrecedingTokens(expression);
                OutputPrecedingTokens(be.LeftBrace);
                _result.Append("(");
                OutputTrailingTokens(be.LeftBrace);
                be.Operand.Accept(this);
                OutputPrecedingTokens(be.RightBrace);
                _result.Append(")");
                OutputTrailingTokens(be.RightBrace);
                OutputTrailingTokens(expression);
                return _result;
            }
            base.VisitCustomExpression(expression);
            return _result;
        }
    }

    public static class EchoVisitorExtensions
    {
        /// <summary>
        /// Create a canonical string representation from an expression tree,
        /// Normalizing any whitespace into single spaces and remove any comments
        /// </summary>
        /// <remarks>
        /// Delimiters are removed from delimited identifiers that don't require them
        /// Brackets are however retained where they are included in the original expression<br/>
        /// If you need a canonical representation of the expression, use <see cref="CanonicalVisitorExtensions.ToCanonicalExpression(Expression)">ToCanonicalExpression</see>
        /// </remarks>
        /// <param name="expr">The source Expression</param>
        /// <returns>A string representation of the expression (including parsed whitespace/comments)</returns>
        public static string EchoExpression(this Expression expr)
        {
            var dumper = new EchoVisitor();
            return expr.Accept(dumper).ToString();
        }
    }

}