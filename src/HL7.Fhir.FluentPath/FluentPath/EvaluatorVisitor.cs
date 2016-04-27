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
            return Eval.Return(expression.Value);
        }

        public override Evaluator VisitFunctionCall(FP.FunctionCallExpression expression)
        {
            throw new NotImplementedException();
        }

        public override Evaluator VisitLambda(FP.LambdaExpression expression)
        {
            return expression.Accept(this);
        }

        public override Evaluator VisitNewNodeListInit(FP.NewNodeListInitExpression expression)
        {
            return Eval.Return(FhirValueList.Empty());
        }

        public override Evaluator VisitVariableRef(FP.VariableRefExpression expression)
        {
            if (expression is FP.AxisExpression)
                return Eval.Axis(expression.Name);
            return Eval.ResolveValue(expression.Name);
        }

        public override Evaluator VisitTypeBinaryExpression(FP.TypeBinaryExpression expression)
        {
            throw new NotImplementedException();
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
