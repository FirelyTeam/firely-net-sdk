using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.FhirPath
{
    public enum InfixOperator
    {
        Mul,
        Div,
        Add,
        Sub,
        Union,
        Concat,

        Equals,
        Equivalent,
        NotEqual,
        NotEquivalent,
        GreaterThan,
        LessThan,
        LessOrEqual,
        GreaterOrEqual,
        In,

        And,
        Or,
        Xor,
        Implies
    }

}
