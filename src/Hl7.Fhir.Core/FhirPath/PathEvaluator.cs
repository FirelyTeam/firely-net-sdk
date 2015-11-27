using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.FhirPath
{
    public delegate Result<T> Evaluator<T>(EvaluationContext c);

    public static class Eval
    {
        public static Evaluator<T> Return<T>(T value)
        {
            return c => Result.ForValue(value);
        }

        public static Evaluator<decimal> Add(this Evaluator<decimal> left, Evaluator<decimal> right)
        {
            return c => Result.ForValue(left(c).Value + right(c).Value);
        }

        public static Evaluator<decimal> Sub(this Evaluator<decimal> left, Evaluator<decimal> right)
        {
            return c => Result.ForValue(left(c).Value - right(c).Value);
        }

        public static Evaluator<decimal> Mul(this Evaluator<decimal> left, Evaluator<decimal> right)
        {
            return c => Result.ForValue(left(c).Value * right(c).Value);
        }

        public static Evaluator<decimal> Div(this Evaluator<decimal> left, Evaluator<decimal> right)
        {
            return c => Result.ForValue(left(c).Value / right(c).Value);
        }

        public static Evaluator<string> Add(this Evaluator<string> left, Evaluator<string> right)
        {
            return c => Result.ForValue(left(c).Value + right(c).Value);
        }
    }
}
