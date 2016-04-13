/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.FluentPath
{
    public enum Operator
    {
        Invoke,

        Index,

        Plus,
        Minus,

        Mul,
        Div,
        TruncDiv,
        Modulo,

        Add,
        Sub,

        Union,

        [Obsolete]
        Concat,

        LessOrEqual,
        LessThan,
        GreaterThan,
        GreaterOrEqual,

        Is,
        As,

        Equal,
        Equivalent,
        NotEqual,
        NotEquivalent,
                   
        In,
        Contains,

        And,

        Or,
        Xor,

        Implies
    }

}
