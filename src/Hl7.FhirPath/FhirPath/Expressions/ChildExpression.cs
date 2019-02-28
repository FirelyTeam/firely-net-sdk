/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System.Linq;

namespace Hl7.FhirPath.Expressions
{
    public class ChildExpression : FunctionCallExpression
    {
        public ChildExpression(Expression focus, string name) : base(focus, OP_PREFIX+"children", TypeInfo.Any, new ConstantExpression(name, TypeInfo.String))
        {
        }

        public string ChildName => (string)((ConstantExpression)Arguments.First()).Value;
    }
}
