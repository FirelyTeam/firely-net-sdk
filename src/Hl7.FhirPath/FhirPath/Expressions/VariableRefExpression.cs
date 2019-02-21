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
    public class VariableRefExpression : Expression
    {
        public VariableRefExpression(string name) : base(TypeInfo.Any)
        {
            if (name == null) throw Error.ArgumentNull("name");

            Name = name;
        }

        public string Name { get; private set; }

        public override T Accept<T>(ExpressionVisitor<T> visitor, SymbolTable scope)
        {
            return visitor.VisitVariableRef(this, scope);
        }
        public override bool Equals(object obj)
        {
            if (base.Equals(obj) && obj is VariableRefExpression)
            {
                var f = (VariableRefExpression)obj;

                return f.Name == Name;
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ Name.GetHashCode();
        }
    }
}
