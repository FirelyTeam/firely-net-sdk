/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

namespace Hl7.FhirPath.Expressions
{
    public abstract class ExpressionVisitor<T>
    {
        public abstract T VisitConstant(ConstantExpression expression);

        public abstract T VisitFunctionCall(FunctionCallExpression expression);

        //public abstract T VisitLambda(LambdaExpression expression);

        public abstract T VisitNewNodeListInit(NewNodeListInitExpression expression);

        public abstract T VisitVariableRef(VariableRefExpression expression);
    }
}
