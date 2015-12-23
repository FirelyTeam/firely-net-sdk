/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using Hl7.Fhir.Navigation;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using Hl7.Fhir.FhirPath.Grammar;

namespace Hl7.Fhir.FhirPath
{
    public delegate EvaluationContext Evaluator(EvaluationContext c);

    // There is a special case around the entry point, where the type of the entry point can be represented, but is optional.
    // To illustrate this point, take the path
    //      telecom.where(use = 'work').value
    // This can be evaluated as an expression on a Patient resource, or other kind of resources. 
    // However, for natural human use, expressions are often prefixed with the name of the context in which they are used:
    //	    Patient.telecom.where(use = 'work').value
    // These 2 expressions have the same outcome, but when evaluating the second, the evaluation will only produce results when used on a Patient resource.

    public static class Eval
    {
        public static IEnumerable<IValueProvider> Evaluate(this Evaluator evaluator, FhirNavigationTree instance)
        {
            var context = EvaluationContext.NewContext(null, new List<FhirNavigationTree> { instance });
            var resultContxt = evaluator(context);

            return resultContxt.Focus;
        }


        public static Evaluator Then(this Evaluator first, Evaluator then)
        {
            return c => then(first(c));
        }


        public static Evaluator Chain(IEnumerable<Evaluator> evaluators)
        {
            return c =>
                evaluators.Aggregate(c, (result, next) => next(result));
        }

        public static Evaluator ChildrenMatchingName(string name)
        {
            return c =>
            {
                return EvaluationContext.NewContext(c, c.Focus.JustFhirPathElements().Navigate(name));
            };
        }

        public static Evaluator Constant(object value)
        {
            return c => EvaluationContext.NewContext(c, value);
        }

        public static Evaluator Compare(FPComparison op, Evaluator left, Evaluator right)
        {
            return c =>
            {
                var leftNodes = left(c).Focus;
                var rightNodes = right(c).Focus;

                if (op == FPComparison.Equals)
                {
                    return EvaluationContext.NewContext(c, leftNodes.IsEqualTo(rightNodes));
                }
                else
                    throw Error.NotImplemented("Operator '{0}' is not yet implemented".FormatWith(op));
            };
        }

        public static Evaluator Function(string name, IEnumerable<Evaluator> paramList)
        {
            if(name == "where")
                return Where(paramList);
            else
                throw Error.NotSupported("An unknown function '{0}' is invoked".FormatWith(name));
        }

        public static Evaluator Where(IEnumerable<Evaluator> parameters)
        {
            if (parameters.Count() != 1) throw Error.Argument("parameters", "'where' requires exactly one parameter");

            return c =>
            {
                var condition = parameters.Single();
                return EvaluationContext.NewContext(c, where(c, condition));
            };
        }

        private static IEnumerable<IFhirPathValue> where(EvaluationContext parentContext, Evaluator condition)
        {
            foreach (var element in parentContext.Focus)
            {
                var context = EvaluationContext.NewContext(parentContext, element);
                var result = condition(context).Focus.AsBooleanEvaluation();
                if (result) yield return element;
            }
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

    public enum FPComparison
    {
        Equals,
        Equivalent,
        NotEquals,
        NotEquivalent,
        GreaterThan,
        LessThan,
        GreaterOrEqual,
        LessOrEqual,
        In
    }

}
