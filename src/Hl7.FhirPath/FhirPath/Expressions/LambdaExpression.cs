/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Utility;
using System.Linq;

namespace Hl7.FhirPath.Expressions
{
    public class LambdaExpression : Expression
    {
        public LambdaExpression(string[] paramNames, Expression body): base(TypeInfo.Any)
        {
            ParamNames = paramNames ?? throw Error.ArgumentNull(nameof(paramNames));
            Body = body ?? throw Error.ArgumentNull(nameof(body));
        }

        public string[] ParamNames { get; private set; }

        public Expression Body { get; private set; }

        public override T Accept<T>(ExpressionVisitor<T> visitor, SymbolTable scope) 
            => visitor.VisitLambda(this, scope);

        public override bool Equals(object obj) =>
           (base.Equals(obj) && obj is LambdaExpression f) &&
                ParamNames.SequenceEqual(f.ParamNames) && Body == f.Body;        

        public override int GetHashCode() =>
            base.GetHashCode() ^ ParamNames.GetHashCode() ^ Body.GetHashCode();
    }
}
