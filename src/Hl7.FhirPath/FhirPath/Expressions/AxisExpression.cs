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
using System.Collections.Generic;
using System.Linq;

namespace Hl7.FhirPath.Expressions
{

    public class AxisExpression : VariableRefExpression
    {
        public AxisExpression(string axisName) : base(OP_PREFIX+axisName)
        {
            if (axisName == null) throw Error.ArgumentNull("axisName");
        }

        public string AxisName => Name.Substring(OP_PREFIX_LEN);

        public static readonly AxisExpression This = new AxisExpression("this");
        public static readonly AxisExpression Focus = new AxisExpression("focus");
    }
}
