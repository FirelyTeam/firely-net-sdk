using Hl7.Fhir.Support;
using FP = HL7.Fhir.FluentPath.FluentPath.Expressions;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.FluentPath;

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

            return Bind(expression.FunctionName, focusEval, argsEval);   
        }

        public override Evaluator VisitLambda(FP.LambdaExpression expression)
        {
            return expression.Accept(this);
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

        public static Evaluator Return(IFluentPathValue value)
        {
            return _ => new[] { (IFluentPathValue)value };
        }

        public static Evaluator Return(IEnumerable<IFluentPathValue> value)
        {
            return _ => value;
        }

        public static Evaluator ResolveValue(string name)
        {
            return ctx => ctx.ResolveValue(name);
        }


        public static Evaluator Focus()
        {
            return ctx => ctx.FocusStack.Peek();
        }
     
        public static Evaluator Bind(string name, Evaluator focus, IEnumerable<Evaluator> arguments)
        {
            return Binding.Bind(name, focus, arguments);
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
