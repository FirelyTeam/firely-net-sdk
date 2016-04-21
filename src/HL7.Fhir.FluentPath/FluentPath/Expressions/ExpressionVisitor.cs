using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL7.Fhir.FluentPath.FluentPath.Expressions
{
    public abstract class ExpressionVisitor
    {
        //public void Visit(Expression expression)
        //{
        //    if (expression is ConstantExpression)
        //        VisitConstant((ConstantExpression)expression);
        //    else if (expression is FunctionCallExpression)
        //        VisitFunctionCall((FunctionCallExpression)expression);
        //    else if (expression is LambdaExpression)
        //        VisitLambda((LambdaExpression)expression);
        //    else if (expression is NewNodeListInitExpression)
        //        VisitNewNodeListInit((NewNodeListInitExpression)expression);
        //    else if (expression is VariableRefExpression)
        //        VisitVariableRef((VariableRefExpression)expression);
        //}

        public virtual void VisitConstant(ConstantExpression expression) { }

        public virtual void VisitFunctionCall(FunctionCallExpression expression) { }

        public virtual void VisitLambda(LambdaExpression expression) { }

        public virtual void VisitNewNodeListInit(NewNodeListInitExpression expression) { }

        public virtual void VisitVariableRef(VariableRefExpression expression) { }

        public virtual void VisitTypeBinaryExpression(TypeBinaryExpression expression) { }
    }
}
