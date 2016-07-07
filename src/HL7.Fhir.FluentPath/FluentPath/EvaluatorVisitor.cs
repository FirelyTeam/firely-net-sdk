using Hl7.Fhir.Support;
using FP = HL7.Fhir.FluentPath.FluentPath.Expressions;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.FluentPath;
using Hl7.Fhir.FluentPath.Binding;
using HL7.Fhir.FluentPath.FluentPath.Binding;

namespace HL7.Fhir.FluentPath.FluentPath
{
    internal class EvaluatorVisitor : FP.ExpressionVisitor<Evaluator>
    {
        public override Evaluator VisitConstant(FP.ConstantExpression expression)
        {
            return Return(new ConstantValue(expression.Value));
        }

        public override Evaluator VisitFunctionCall(FP.FunctionCallExpression expression)
        {
            var focusEval = expression.Focus.ToEvaluator();
            var argsEval = expression.Arguments.Select(arg => arg.ToEvaluator());

            var types = expression.Arguments.Select(a => a.ExpressionType);
            Invokee boundFunction = Functions.Resolve(expression.FunctionName, types);

            return buildBindingInvoke(focusEval, argsEval, boundFunction);
        }

        public override Evaluator VisitNewNodeListInit(FP.NewNodeListInitExpression expression)
        {
            return Return(FhirValueList.Empty());
        }

        public override Evaluator VisitVariableRef(FP.VariableRefExpression expression)
        {
            // Special case: $this
            if (expression is FP.AxisExpression)
            {
                var axis = (FP.AxisExpression)expression;
                if (axis.AxisName == "this")
                    return Focus();
                else
                    throw new NotSupportedException("Cannot resolve axis reference " + axis.AxisName);
            }
            else
                return ResolveValue(expression.Name);
        }

        public override Evaluator VisitTypeBinaryExpression(FP.TypeBinaryExpression expression)
        {
            throw new NotImplementedException();
        }

        public static Evaluator Return(Hl7.Fhir.FluentPath.IValueProvider value)
        {
            return (_,__) => (new[] { (Hl7.Fhir.FluentPath.IValueProvider)value });
        }

        public static Evaluator Return(IEnumerable<Hl7.Fhir.FluentPath.IValueProvider> value)
        {
            return (_,__) => value;
        }

        public static Evaluator ResolveValue(string name)
        {
            return (ctx,f) => ctx.ResolveValue(name);
        }


        public static Evaluator Focus()
        {
            return (ctx, f) => f;
        }

        private static Evaluator buildBindingInvoke(Evaluator focus, IEnumerable<Evaluator> arguments, Invokee invokee)
        {
            return (ctx,f) =>
            {
                var focusNodes = focus(ctx,f);
                return invokee(ctx, focusNodes, arguments);
            };
        }
    }

    public static class EvaluatorExpressionExtensions
    {
        public static Evaluator ToEvaluator(this FP.Expression expr)
        {
            var compiler = new EvaluatorVisitor();
            return expr.Accept<Evaluator>(compiler);
        }
    }

}
