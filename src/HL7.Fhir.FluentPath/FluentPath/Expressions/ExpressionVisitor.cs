using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.FluentPath.Expressions
{
    public abstract class ExpressionVisitor<T>
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

        public abstract T VisitConstant(ConstantExpression expression, SymbolTable scope);

        public abstract T VisitFunctionCall(FunctionCallExpression expression, SymbolTable scope);

        //public abstract T VisitLambda(LambdaExpression expression);

        public abstract T VisitNewNodeListInit(NewNodeListInitExpression expression, SymbolTable scope);

        public abstract T VisitVariableRef(VariableRefExpression expression, SymbolTable scope);

        //public abstract T VisitTypeBinaryExpression(TypeBinaryExpression expression);
    }
}
