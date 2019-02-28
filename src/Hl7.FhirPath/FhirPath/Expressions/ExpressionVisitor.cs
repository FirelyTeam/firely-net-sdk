/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.FhirPath.Expressions
{
    public abstract class ExpressionVisitor<T>
    {
        public abstract T VisitConstant(ConstantExpression expression, SymbolTable scope);

        public abstract T VisitFunctionCall(FunctionCallExpression expression, SymbolTable scope);

        public abstract T VisitLambda(LambdaExpression expression, SymbolTable scope);

        public abstract T VisitNewNodeListInit(NewNodeListInitExpression expression, SymbolTable scope);

        public abstract T VisitVariableRef(VariableRefExpression expression, SymbolTable scope);

        // Should have been abstract, but that would break existing code in 1.x. Maybe change to abstract in 2.x
        public virtual T VisitLet(LetExpression expression, SymbolTable scope) => throw new NotImplementedException("Visitor does not implement VisitLet");
    }
}
