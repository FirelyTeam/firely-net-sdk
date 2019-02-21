/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;

namespace Hl7.FhirPath.Expressions
{
    public class UnaryExpression : FunctionCallExpression
    {
        internal const string URY_PREFIX = "unary.";
        internal static readonly int URY_PREFIX_LEN = URY_PREFIX.Length;

        public UnaryExpression(char op, Expression operand) : this(new String(op,1), operand)
        {
        }

        public UnaryExpression(string op, Expression operand) : base(AxisExpression.Focus, URY_PREFIX + op, TypeInfo.Any, operand)
        {
        }
        public string Op
        {
            get
            {
                return FunctionName.Substring(URY_PREFIX_LEN);
            }
        }

        public Expression Operand
        {
            get
            {
                return Focus;
            }
        }
    }
}
