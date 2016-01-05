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
    public delegate IEnumerable<IFhirPathValue> Evaluator(IEnumerable<IFhirPathValue> focus);

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
            return evaluator(Focus.Create(instance));
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

        public static Evaluator Constant(object value)
        {
            return c => Focus.Create(value);
        }

        public static Evaluator Invoke(Func<IEnumerable<IFhirPathValue>, IEnumerable<IFhirPathValue>> func)
        {
            return c => func(c);
        }

        public static Evaluator Infix(InfixOperator op, Evaluator left, Evaluator right)
        {
            return c =>
            {
                var leftNodes = left(c);
                var rightNodes = right(c);

                switch (op)
                {
                    case InfixOperator.Equals:
                        return Focus.Create(leftNodes.IsEqualTo(rightNodes));
                    case InfixOperator.Add:
                        return leftNodes.Add(rightNodes);
                    default:
                        throw Error.NotImplemented("Operator '{0}' is not yet implemented".FormatWith(op));
                }
            };
        }

        public static Evaluator Function(string name, IEnumerable<Evaluator> paramList)
        {
            return c =>
            {
                IEnumerable<IFhirPathValue> result;

                if (name == "where") result = where(c, paramList);
                else if (name == "empty") result = empty(c, paramList);
                else if (name == "not") result = not(c, paramList);
                else
                    throw Error.NotSupported("An unknown function '{0}' is invoked".FormatWith(name));

                return result;
            };
        }

        private static IEnumerable<IFhirPathValue> empty(IEnumerable<IFhirPathValue> focus, IEnumerable<Evaluator> parameters)
        {            
            if (parameters.Any()) throw Error.Argument("parameters", "'empty' does not take parameters");

            return Focus.Create(focus.Empty());
        }

        private static IEnumerable<IFhirPathValue> not(IEnumerable<IFhirPathValue> focus, IEnumerable<Evaluator> parameters)
        {
            if (parameters.Any()) throw Error.Argument("parameters", "'not' does not take parameters");

            return Focus.Create(!focus.AsBoolean());
        }

        private static IEnumerable<IFhirPathValue> where(IEnumerable<IFhirPathValue> focus, IEnumerable<Evaluator> parameters)
        {
            if (parameters.Count() != 1) throw Error.Argument("parameters", "'where' requires exactly one parameter");

            var condition = parameters.Single();

            return focus.Where(ctx => condition(Focus.Create(ctx)).AsBoolean());
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

    public enum InfixOperator
    {
        Equals,
        Equivalent,
        NotEquals,
        NotEquivalent,
        GreaterThan,
        LessThan,
        GreaterOrEqual,
        LessOrEqual,
        In,
        Add,
        Sub        
    }

}
