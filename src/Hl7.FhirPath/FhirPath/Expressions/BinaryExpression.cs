/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Linq;

namespace Hl7.FhirPath.Expressions
{
    public class BinaryExpression : FunctionCallExpression
    {
        internal const string BIN_PREFIX = "binary.";
        internal static readonly int BIN_PREFIX_LEN = BIN_PREFIX.Length;


        public BinaryExpression(char op, Expression left, Expression right) : this(new String(op,1), left, right)
        {
        }

        public BinaryExpression(string op, Expression left, Expression right) : base(AxisExpression.Focus, BIN_PREFIX + op, TypeInfo.Any, left, right)
        {
        }
        public string Op => FunctionName.Substring(BIN_PREFIX_LEN);

        public Expression Left => Focus;

        public Expression Right => Arguments.First();
    }
}
