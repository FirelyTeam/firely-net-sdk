/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


namespace Hl7.FhirPath.Expressions
{
    public abstract class Expression
    {
        internal const string OP_PREFIX = "builtin.";
        internal static readonly int OP_PREFIX_LEN = OP_PREFIX.Length;

        protected Expression(TypeInfo type)
        {
            ExpressionType = type;
        }
        public TypeInfo ExpressionType { get; protected set; }

        public abstract T Accept<T>(ExpressionVisitor<T> visitor, SymbolTable scope);

        public override bool Equals(object obj)
        {
            if (obj is Expression && obj != null)
            {
                return ((Expression)obj).ExpressionType == ExpressionType;
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return ExpressionType.GetHashCode();
        }
    }
}
