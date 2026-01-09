/*
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable


using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using P = Hl7.Fhir.ElementModel.Types;

#nullable enable

namespace Hl7.FhirPath.Expressions;

/// <summary>
///
/// </summary>
public class EchoVisitor : ExpressionVisitor<StringBuilder>
{
    private readonly StringBuilder _result = new();

    #region << output utilties>>
    private void outputIdentifierName(string identifier)
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

    private void outputPrecedingTokens(Expression? expr)
    {
        if (expr?.LeadingWhitespace?.Any() == true)
            _result.Append(System.String.Join("", expr.LeadingWhitespace.Select(ws => ws.ToString())));
    }
    private void outputTrailingTokens(Expression expr)
    {
        if (expr.TrailingWhitespace?.Any() == true)
            _result.Append(System.String.Join("", expr.TrailingWhitespace.Select(ws => ws.ToString())));
    }

    private void outputSubToken(SubToken? subtoken)
    {
        if (subtoken == null) return;
        outputPrecedingTokens(subtoken);
        _result.Append($"{subtoken.Value}");
        outputTrailingTokens(subtoken);
    }

    private void outputPrecedingTokens(SubToken? subtoken)
    {
        if (subtoken == null) return;
        if (subtoken.LeadingWhitespace?.Any() == true)
            _result.Append(System.String.Join("", subtoken.LeadingWhitespace.Select(ws => ws.ToString())));
    }

    private void outputTrailingTokens(SubToken? subtoken)
    {
        if (subtoken == null) return;
        if (subtoken.TrailingWhitespace?.Any() == true)
            _result.Append(System.String.Join("", subtoken.TrailingWhitespace.Select(ws => ws.ToString())));
    }
    #endregion

    public override StringBuilder VisitConstant(ConstantExpression expression)
    {
        outputPrecedingTokens(expression);
        if (expression is IdentifierExpression identifier)
        {
            outputIdentifierName(identifier.Value);
            outputTrailingTokens(expression);
            return _result;
        }
        var t = Fhir.Serialization.PrimitiveTypeConverter.ConvertTo<string>(expression.Value);

        switch (expression.ExpressionType.Name)
        {
            case "Date":
            case "DateTime" when t.Contains('T'):
                _result.Append($"@{t}");
                break;
            case "DateTime":
                _result.Append($"@{t}T");
                break;
            case "Quantity" when expression.Value is P.Quantity q:
                _result.Append(q.Value.ToString(CultureInfo.InvariantCulture));
                outputSubToken(expression.Unit);
                break;
            case "Quantity":
            case "Decimal":
            case "Integer":
            case "Ratio":
            case "Long":
            case "Any":
            case "Boolean":
            case "Code":
            case "Concept":
                _result.Append($"{t}");
                break;
            case "String":
                _result.Append("'" + Functions.StringOperators.EscapeJson(t) + "'");
                break;
            case "Time":
                _result.Append($"@T{t}");
                break;
            case "Void":
                break;
        }

        outputTrailingTokens(expression);
        return _result;
    }

    public override StringBuilder VisitFunctionCall(FunctionCallExpression expression)
    {
        if (expression.FunctionName == "builtin.coreexturl")
        {
            outputPrecedingTokens(expression);
            expression.Focus.Accept(this);
            _result.Append("%`ext-");
            if (expression.Arguments.FirstOrDefault() is ConstantExpression ceVar)
                _result.Append($"{ceVar.Value}");
            _result.Append("`");
            outputTrailingTokens(expression);
            return _result;
        }
        if (expression.FunctionName == "builtin.corevsurl")
        {
            outputPrecedingTokens(expression);
            expression.Focus.Accept(this);
            _result.Append("%`vs-");
            if (expression.Arguments.FirstOrDefault() is ConstantExpression ceVar)
                _result.Append($"{ceVar.Value}");
            _result.Append("`");
            outputTrailingTokens(expression);
            return _result;
        }
        if (expression is SortDirectionExpression sd)
        {
            sd.Focus.Accept(this);
            sd.Arguments.FirstOrDefault()?.Accept(this);
            outputPrecedingTokens(sd);
            _result.Append($"{sd.Op}");
            outputTrailingTokens(expression);
            return _result;
        }
        if (expression is UnaryExpression ue)
        {
            outputPrecedingTokens(expression);
            ue.Focus.Accept(this);
            _result.Append($"{ue.Op}");
            ue.Arguments.FirstOrDefault()?.Accept(this);
            outputTrailingTokens(expression);
            return _result;
        }
        if (expression is BinaryExpression be)
        {
            outputPrecedingTokens(expression);
            be.Focus.Accept(this);
            be.Arguments.FirstOrDefault()?.Accept(this);
            if (be.OpToken != null)
                outputSubToken(be.OpToken);
            else
                _result.Append($"{be.Op}");
            be.Arguments.Skip(1).FirstOrDefault()?.Accept(this);
            outputTrailingTokens(expression);
            return _result;
        }
        if (expression is IndexerExpression ie)
        {
            outputPrecedingTokens(expression);
            ie.Focus.Accept(this);
            outputSubToken(expression.LeftBrace);
            ie.Arguments.FirstOrDefault()?.Accept(this);
            outputSubToken(expression.RightBrace);
            outputTrailingTokens(expression);
            return _result;
        }

        outputPrecedingTokens(expression);
        expression.Focus.Accept(this);
        if (!(expression.Focus is VariableRefExpression { Name: "builtin.that" } ||
              expression.Focus is AxisExpression { AxisName: "that" }))
        {
            _result.Append('.');
        }
        if (expression is ChildExpression ce)
        {
            var ca = ce.Arguments.FirstOrDefault();
            outputPrecedingTokens(ca);
            outputIdentifierName(ce.ChildName);
            outputTrailingTokens(expression);
            return _result;
        }
        outputIdentifierName(expression.FunctionName);
        outputSubToken(expression.LeftBrace);

        expression.Arguments.FirstOrDefault()?.Accept(this);
        foreach (var arg in expression.Arguments.Skip(1))
        {
            _result.Append(',');
            arg.Accept(this);
        }
        outputSubToken(expression.RightBrace);
        outputTrailingTokens(expression);

        return _result;
    }

    public override StringBuilder VisitNewNodeListInit(NewNodeListInitExpression expression)
    {
        outputPrecedingTokens(expression);
        outputSubToken(expression.LeftBrace);
        foreach (var element in expression.Contents)
            element.Accept(this);
        outputSubToken(expression.RightBrace);
        outputTrailingTokens(expression);
        return _result;
    }

    public override StringBuilder VisitVariableRef(VariableRefExpression expression)
    {
        if (expression is AxisExpression ae)
        {
            // No need to output the `that` type
            if (ae.AxisName == "that" || ae is { AxisName: "this", Location: null })
                return _result;

            outputPrecedingTokens(expression);
            _result.Append($"${ae.AxisName}");
            outputTrailingTokens(expression);
            return _result;
        }
        if (expression.Name != "builtin.that")
        {
            outputPrecedingTokens(expression);
            _result.Append('%');
            outputIdentifierName(expression.Name);
            outputTrailingTokens(expression);
        }
        return _result;
    }

    public override StringBuilder VisitCustomExpression(CustomExpression expression)
    {
        if (expression is BracketExpression be)
        {
            outputPrecedingTokens(expression);
            outputPrecedingTokens(be.LeftBrace);
            _result.Append("(");
            outputTrailingTokens(be.LeftBrace);
            be.Operand.Accept(this);
            outputPrecedingTokens(be.RightBrace);
            _result.Append(")");
            outputTrailingTokens(be.RightBrace);
            outputTrailingTokens(expression);
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