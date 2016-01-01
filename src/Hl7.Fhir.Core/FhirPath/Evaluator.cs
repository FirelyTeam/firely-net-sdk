/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public static IEnumerable<IFhirPathValue> Evaluate(this Evaluator evaluator, IFhirPathValue instance)
        {
            var context = new EvaluationContext(instance);
            var resultContxt = evaluator(context);

            return resultContxt.Focus;
        }

        public static IEnumerable<IFhirPathValue> Evaluate(this Evaluator evaluator, EvaluationContext parentContext, IFhirPathValue instance)
        {
            var context = new EvaluationContext(parentContext, instance);
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
                return c.Navigate(name);
            };
        }

        public static Evaluator Constant(object value)
        {
            return c => new EvaluationContext(c, value);
        }

        public static Evaluator Compare(FPComparison op, Evaluator left, Evaluator right)
        {
            return c =>
            {
                var leftNodes = left(c).Focus;
                var rightNodes = right(c).Focus;

                if (op == FPComparison.Equals)
                {
                    return new EvaluationContext(c, leftNodes.IsEqualTo(rightNodes));
                }
                else
                    throw Error.NotImplemented("Operator '{0}' is not yet implemented".FormatWith(op));
            };
        }

        public static Evaluator Function(string name, IEnumerable<Evaluator> paramList)
        {
            return c =>
            {
                EvaluationContext result;

                if (name == "where") result = where(c, paramList);
                else if (name == "empty") result = empty(c, paramList);
                else if (name == "not") result = not(c, paramList);
                else
                    throw Error.NotSupported("An unknown function '{0}' is invoked".FormatWith(name));

                return result;
            };
        }

        private static EvaluationContext empty(EvaluationContext parentContext, IEnumerable<Evaluator> parameters)
        {            
            if (parameters.Any()) throw Error.Argument("parameters", "'empty' does not take parameters");

            return parentContext.Empty();
        }

        private static EvaluationContext not(EvaluationContext parentContext, IEnumerable<Evaluator> parameters)
        {
            if (parameters.Any()) throw Error.Argument("parameters", "'not' does not take parameters");

            return parentContext.Not();
        }

        private static EvaluationContext where(EvaluationContext parentContext, IEnumerable<Evaluator> parameters)
        {
            if (parameters.Count() != 1) throw Error.Argument("parameters", "'where' requires exactly one parameter");

            var condition = parameters.Single();

            return parentContext.Where(ctx => condition(ctx));
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
