/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.FhirPath.Expressions
{
    internal class FormattedUnit
    {
        public FormattedUnit(IEnumerable<string> content)
        {
            Lines = content.ToArray();
            MaxLength = Lines.Aggregate(0, (max, l) => Math.Max(l.Length, max));
        }

        public FormattedUnit(string content) : this(new[] { content })
        {
            //
        }

        public readonly string[] Lines;
        public readonly int MaxLength;

        bool IsMultiline => Lines.Length > 1;

        public string SingleLine => IsMultiline ?
            throw new InvalidOperationException("Must not be multiline") : Lines[0];

        public FormattedUnit Indent() => new FormattedUnit(Lines.Select(l => "   " + l));

        public static FormattedUnit Compose(FormattedUnit unit, string sep = "") =>
            Compose(new[] { unit }, sep);

        public static FormattedUnit Enclose(FormattedUnit open, FormattedUnit inner, FormattedUnit close) => 
            inner.IsMultiline
                ? new FormattedUnit(open.Lines.Union(inner.Indent().Lines).Union(close.Lines))
                : new FormattedUnit(open.ToString() + inner.ToString() + close.ToString());

        public static FormattedUnit Enclose(FormattedUnit open, FormattedUnit inner) =>
            inner.IsMultiline
                ? new FormattedUnit(open.Lines.Union(inner.Indent().Lines))
                : new FormattedUnit(open.ToString() + inner.ToString());

        public FormattedUnit Append(FormattedUnit appendee) => 
            new FormattedUnit(
                Lines.Take(Lines.Length - 1).Union(new[] { Lines.Last() + appendee.ToString() }));

        public static FormattedUnit Compose(IEnumerable<FormattedUnit> units, string sep = "")
        {
            var unitsList = units.ToArray();
            
            bool multiLine = unitsList.Any(u => u.IsMultiline) ||
                    unitsList.Sum(u => u.MaxLength) > 60;

            if (!multiLine)
                return new FormattedUnit(String.Join(sep, unitsList.Select(u => u.SingleLine)));

            var enclosed = unitsList.SelectMany((unit,ix) => 
                ix == unitsList.Length - 1 ?
                    unit.Lines :
                    unit.Append(new FormattedUnit(sep)).Lines);

            return new FormattedUnit(enclosed);
        }

        public override string ToString() => String.Join(Environment.NewLine, Lines);
    }

    internal class TreeVisualizerVisitor : ExpressionVisitor<FormattedUnit>
    {

        public override FormattedUnit VisitConstant(ConstantExpression expression, SymbolTable scope) =>
             new FormattedUnit($"'{expression.Value}'" + formatType(expression));

        public override FormattedUnit VisitFunctionCall(FunctionCallExpression expression, SymbolTable scope)
        {
            var funcf = new FormattedUnit($"{expression.FunctionName}(");

            var args = new[] { expression.Focus.Accept(this, scope) }
                .Union(expression.Arguments.Select(a => a.Accept(this, scope)));

            var argsf = FormattedUnit.Compose(args, ", ");
            var typef = new FormattedUnit(")" + formatType(expression));

            return FormattedUnit.Enclose(funcf, argsf.Append(typef));
        }

        public override FormattedUnit VisitLambda(LambdaExpression expression, SymbolTable scope)
        {
            var lambda = new FormattedUnit($"({String.Join(",", expression.ParamNames)}) => ");
            var body = expression.Body.Accept(this, scope);
            var typef = new FormattedUnit(formatType(expression));

            return FormattedUnit.Enclose(lambda, body.Append(typef));
        }

        public override FormattedUnit VisitNewNodeListInit(NewNodeListInitExpression expression, SymbolTable scope)
        {
            var contents = FormattedUnit.Compose(expression.Contents.Select(a => a.Accept(this, scope)), ", ");

            return FormattedUnit.Enclose(new FormattedUnit("["), contents.Append(new FormattedUnit("]")));
        }

        public override FormattedUnit VisitVariableRef(VariableRefExpression expression, SymbolTable scope) =>
            new FormattedUnit($"{expression.Name}");


        private string formatType(Expression expr) 
            => expr.ExpressionType != TypeInfo.Any ? $"::{expr.ExpressionType.Name}" : "";
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
