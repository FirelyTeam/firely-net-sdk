using Hl7.Fhir.Navigation;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.FhirPath
{
    // [WMR]
    // 1. Add parameter for input node set?
    //    public delegate Result<T> Evaluator<I, T>(I input, EvaluationContext c);
    //    I = IEnumerable<Navigation.NavNodeSet>
    //    OR provide input as a member of EvaluationContext

    public delegate EvaluationContext Evaluator(EvaluationContext c);

    public static class Eval
    {
        public static Evaluator Chain(IEnumerable<Evaluator> evaluators)
        {
            return c =>
                evaluators.Aggregate(c, (result, next) => next(result));
        }

        public static Evaluator ChildrenWithName(string name)
        {
            return c =>
            {
                if (c.IsNodeSet)
                    return EvaluationContext.NewContext(c, c.Nodes.Children(name));
                else
                    throw Error.InvalidOperation("Cannot navigate to children {0} on an expression that is not a node-set");
            };
        }

        public static Evaluator Constant(string value)
        {
            return c => EvaluationContext.NewContext(c, value);
        }

        //public static Evaluator<decimal> Add(this Evaluator<decimal> left, Evaluator<decimal> right)
        //{
        //    return c => Result.ForValue(left(c).Value + right(c).Value);
        //}

        //public static Evaluator<decimal> Sub(this Evaluator<decimal> left, Evaluator<decimal> right)
        //{
        //    return c => Result.ForValue(left(c).Value - right(c).Value);
        //}

        //public static Evaluator<decimal> Mul(this Evaluator<decimal> left, Evaluator<decimal> right)
        //{
        //    return c => Result.ForValue(left(c).Value * right(c).Value);
        //}

        //public static Evaluator<decimal> Div(this Evaluator<decimal> left, Evaluator<decimal> right)
        //{
        //    return c => Result.ForValue(left(c).Value / right(c).Value);
        //}

        //public static Evaluator<string> Add(this Evaluator<string> left, Evaluator<string> right)
        //{
        //    return c => Result.ForValue(left(c).Value + right(c).Value);
        //}
    }
}
