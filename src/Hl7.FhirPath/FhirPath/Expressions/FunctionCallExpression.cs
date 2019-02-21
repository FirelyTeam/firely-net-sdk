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

namespace Hl7.FhirPath.Expressions
{
    public class FunctionCallExpression : Expression
    {
        public FunctionCallExpression(Expression focus, string name, TypeInfo type, params Expression[] arguments) : this(focus, name, type, (IEnumerable<Expression>) arguments)
        {
        }

        public FunctionCallExpression(Expression focus, string name, TypeInfo type, IEnumerable<Expression> arguments) : base(type)
        {
            if (String.IsNullOrEmpty(name)) throw Error.ArgumentNull("name");
            Focus = focus ?? throw Error.ArgumentNull("focus");
            FunctionName = name;
            Arguments = arguments?.ToList() ?? throw Error.ArgumentNull("arguments");

        //    throw new NotImplementedException("Should we introduce the implicit lambda's for where etc. here?");
        }

        public Expression Focus { get; private set; }
        public string FunctionName { get; private set; }

        public IList<Expression> Arguments { get; private set; }

        public override T Accept<T>(ExpressionVisitor<T> visitor, SymbolTable scope)
        {
            return visitor.VisitFunctionCall(this, scope);
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj) && obj is FunctionCallExpression)
            {
                var f = (FunctionCallExpression)obj;

                return f.FunctionName == FunctionName && Arguments.SequenceEqual(f.Arguments);
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ FunctionName.GetHashCode() ^ Arguments.GetHashCode();
        }
    }
}
