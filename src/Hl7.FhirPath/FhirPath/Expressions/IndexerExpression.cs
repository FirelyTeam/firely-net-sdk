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
    public class IndexerExpression : FunctionCallExpression
    {
        public IndexerExpression(Expression collection, Expression index) : base(collection, OP_PREFIX+"item", TypeInfo.Any, index)
        {
        }

        public Expression Index
        {
            get
            {
                return Arguments.First();
            }
        }
    }
}
