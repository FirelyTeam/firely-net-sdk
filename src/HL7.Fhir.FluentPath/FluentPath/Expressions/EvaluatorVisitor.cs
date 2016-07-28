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
            var arguments = new List<Invokee>() { expression.Focus.ToEvaluator() };
            arguments.AddRange(expression.Arguments.Select(arg => arg.ToEvaluator()));

            // We have no real type information, so just pass object as the type
            var types = new List<Type>() { typeof(object) }; //   for the focus;
            types.AddRange(expression.Arguments.Select(a => typeof(object)));   // for the arguments

            // Now locate the function based on the types and name
            Invokee boundFunction = BindingTable.Resolve(expression.FunctionName, types);

            return InvokeeFactory.Invoke(expression.FunctionName, arguments, boundFunction);
        }

        public override Invokee VisitNewNodeListInit(FP.NewNodeListInitExpression expression)
        {
            return InvokeeFactory.Return(FhirValueList.Empty);
        }

        public override Invokee VisitVariableRef(FP.VariableRefExpression expression)
        {
            return InvokeeFactory.ResolveValue(expression.Name);
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
