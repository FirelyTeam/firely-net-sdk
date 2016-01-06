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
    public delegate object ScalarEvaluator(IEnumerable<IFhirPathValue> focus);

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

        //public static Evaluator Chain(IEnumerable<Evaluator> evaluators)
        //{
        //    return c =>
        //        evaluators.Aggregate(c, (result, next) => next(result));
        //}


        public static Evaluator CastToCollection(ScalarEvaluator scalar)
        {
            return c => Focus.Create(scalar(c));
        }

        public static Evaluator Constant(object value)
        {
            return c => Focus.Create(value);
        }

        public static Evaluator Invoke(Func<IEnumerable<IFhirPathValue>, IEnumerable<IFhirPathValue>> func)
        {
            return c => func(c);
        }

        public static Evaluator Invoke(Func<IEnumerable<IFhirPathValue>, IFhirPathValue> func)
        {
            return c => Focus.Create(func(c));
        }

        public static Evaluator Infix(this Evaluator left, InfixOperator op, Evaluator right)
        {
            return c =>
            {
                var leftNodes = left(c);
                var rightNodes = right(c);

                switch (op)
                {
                    case InfixOperator.Equal:
                        return Focus.Create(leftNodes.IsEqualTo(rightNodes));
                    case InfixOperator.Add:
                        return leftNodes.Add(rightNodes);
                    default:
                        throw Error.NotImplemented("Infix operator '{0}' is not yet implemented".FormatWith(op));
                }
            };
        }

        public static Evaluator Where(Evaluator condition)
        {
            return c=> c.Where(ctx => condition(Focus.Create(ctx)).AsBoolean());
        }

        public static ScalarEvaluator All(Evaluator condition)
        {
            return c => c.Empty() || c.All(ctx => condition(Focus.Create(ctx)).AsBoolean());
        }

        public static ScalarEvaluator Any(Evaluator condition)
        {
            return c => c.Any(ctx => condition(Focus.Create(ctx)).AsBoolean());
        }

        public static ScalarEvaluator Empty()
        {
            return c=> c.Empty();
        }

        public static ScalarEvaluator Not()
        {
            return c=> !c.AsBoolean();
        }


        public static Evaluator Children(Evaluator nameParam)
        {
            return c =>
            {
                var name = (string)nameParam(c).Single().Value;
                return c.Children(name);
            };
        }

        public static Evaluator Children(string name)
        {
            return Children(Eval.Constant(name));
        }


        public static Evaluator Function(string name, IEnumerable<Evaluator> paramList)
        {
            // Later: provide a hook for custom functions
            throw Error.NotSupported("An unknown function '{0}' is invoked".FormatWith(name));
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
        Invoke,

        Mul,
        Div,
        Add,
        Sub,
        Union,
        Concat,

        Equal,
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


