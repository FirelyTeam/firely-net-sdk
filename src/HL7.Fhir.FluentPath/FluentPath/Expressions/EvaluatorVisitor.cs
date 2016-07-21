using Hl7.Fhir.Support;
using FP = Hl7.Fhir.FluentPath.Expressions;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hl7.Fhir.FluentPath;
using Hl7.Fhir.FluentPath.Binding;

namespace Hl7.Fhir.FluentPath.Expressions
{
    internal class EvaluatorVisitor : FP.ExpressionVisitor<Invokee>
    {
        public override Invokee VisitConstant(FP.ConstantExpression expression)
        {
            return InvokeeFactory.Return(new ConstantValue(expression.Value));
        }

        public override Invokee VisitFunctionCall(FP.FunctionCallExpression expression)
        {
            var focusEval = expression.Focus.ToEvaluator();
            var argsEval = expression.Arguments.Select(arg => arg.ToEvaluator());

            // We have no real type information, so just pass object as the type
            var types = new List<Type>() { typeof(object) }; //   for the focus;
            types.AddRange(expression.Arguments.Select(a => typeof(object)));   // for the arguments
            Invokee boundFunction = BindingTable.Resolve(expression.FunctionName, types);

            return buildBindingInvoke(expression.FunctionName, focusEval, argsEval, boundFunction);
        }

        public override Invokee VisitNewNodeListInit(FP.NewNodeListInitExpression expression)
        {
            return InvokeeFactory.Return(FhirValueList.Empty);
        }

        public override Invokee VisitVariableRef(FP.VariableRefExpression expression)
        {
            // Special case: $this   -> can now GO AWAY -> this is a declared name in the context
            if (expression is FP.AxisExpression)
            {
                var axis = (FP.AxisExpression)expression;
                if (axis.AxisName == "this")
                    return InvokeeFactory.Focus();
                else
                    throw new NotSupportedException("Cannot resolve axis reference " + axis.AxisName);
            }
            else
                return InvokeeFactory.ResolveValue(expression.Name);
        }

        public override Invokee VisitTypeBinaryExpression(FP.TypeBinaryExpression expression)
        {
            throw new NotImplementedException();
        }

        private static Invokee buildBindingInvoke(string functionName, Invokee focus, IEnumerable<Invokee> arguments, Invokee invokee)
        {
            return (ctx,_) =>
            {
                var focusNodes = focus(ctx, InvokeeFactory.EmptyArgs);
                var newContext = ctx.Nest(focusNodes);

                try
                {
                    return invokee(newContext, arguments);
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException("Invocation of '{0}' failed: {1}".FormatWith(functionName, e.Message));
                }

            };
        }
    }

    public static class EvaluatorExpressionExtensions
    {
        public static Invokee ToEvaluator(this FP.Expression expr)
        {
            var compiler = new EvaluatorVisitor();
            return expr.Accept<Invokee>(compiler);
        }
    }

}
