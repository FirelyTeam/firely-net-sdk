/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model.Primitives;
using Hl7.Fhir.Utility;
using System;

namespace Hl7.FhirPath.Expressions
{
    public class ConstantExpression : Expression
    {
        public ConstantExpression(object value, TypeInfo type) : base(type)
        {
            if (value == null) Error.ArgumentNull("value");

            Value = value;
        }

        public ConstantExpression(object value) : base(TypeInfo.Any)
        {
            if (value == null) Error.ArgumentNull("value");

            Value = ConstantValue.ToFhirPathValue(value);

            if (Value is bool)
                ExpressionType = TypeInfo.Boolean;
            else if (Value is string)
                ExpressionType = TypeInfo.String;
            else if (Value is Int64)
                ExpressionType = TypeInfo.Integer;
            else if (Value is Decimal)
                ExpressionType = TypeInfo.Decimal;
            else if (Value is PartialDateTime)
                ExpressionType = TypeInfo.DateTime;
            else if (Value is PartialTime)
                ExpressionType = TypeInfo.Time;
            else if (Value is Quantity)
                ExpressionType = TypeInfo.Quantity;
            else
                throw Error.InvalidOperation("Internal logic error: encountered unmappable Value of type " + Value.GetType().Name);
        }

        public object Value { get; private set; }

        public override T Accept<T>(ExpressionVisitor<T> visitor, SymbolTable scope)
        {
            return visitor.VisitConstant(this, scope);
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj) && obj is ConstantExpression)
            {
                var c = (ConstantExpression)obj;
                return Object.Equals(c.Value, Value);
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ Value.GetHashCode();
        }
    }
}
