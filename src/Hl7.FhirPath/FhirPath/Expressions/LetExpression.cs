/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Utility;

namespace Hl7.FhirPath.Expressions
{
    public class LetExpression : Expression
    {
        // let <name> = <expression> in <body>
        public LetExpression(string name, Expression expression, Expression body) : base(TypeInfo.Any)
        {
            Name = name ?? throw Error.ArgumentNull("name");
            Expression = expression ?? throw Error.ArgumentNull("expression");
            Body = body ?? throw Error.ArgumentNull("body");
        }

        public string Name { get; private set; }

        public Expression Expression { get; private set; }
        public Expression Body { get; private set; }

        public override T Accept<T>(ExpressionVisitor<T> visitor, SymbolTable scope) => visitor.VisitLet(this, scope);
        public override bool Equals(object obj) => base.Equals(obj) 
            && obj is LetExpression f ? (f.Name == Name && f.Body == Body && f.Expression == Expression) : false;

        public override int GetHashCode() => base.GetHashCode() ^ Name.GetHashCode() ^ Body.GetHashCode() & Expression.GetHashCode();
    }
}
