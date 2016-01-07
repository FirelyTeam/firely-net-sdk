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
    public delegate IEnumerable<IFhirPathValue> Evaluator(IEnumerable<IFhirPathValue> focus, IEvaluationContext ctx);
    public delegate object ScalarEvaluator(IEnumerable<IFhirPathValue> focus, IEvaluationContext ctx);

    // There is a special case around the entry point, where the type of the entry point can be represented, but is optional.
    // To illustrate this point, take the path
    //      telecom.where(use = 'work').value
    // This can be evaluated as an expression on a Patient resource, or other kind of resources. 
    // However, for natural human use, expressions are often prefixed with the name of the context in which they are used:
    //	    Patient.telecom.where(use = 'work').value
    // These 2 expressions have the same outcome, but when evaluating the second, the evaluation will only produce results when used on a Patient resource.

    public static class Eval
    {
        public static IEnumerable<IFhirPathValue> Evaluate(this Evaluator evaluator, IEvaluationContext context, IFhirPathValue instance)
        {
            return evaluator(Focus.Create(instance), context);
        }

        public static IEnumerable<IFhirPathValue> Evaluate(this Evaluator evaluator, IFhirPathValue instance)
        {
            return evaluator(Focus.Create(instance), new EvaluationContext());
        }

        public static Evaluator Then(this Evaluator first, Evaluator then)
        {
            return (f,c) => then(f,c);
        }

        //public static Evaluator Chain(IEnumerable<Evaluator> evaluators)
        //{
        //    return c =>
        //        evaluators.Aggregate(c, (result, next) => next(result));
        //}


        public static Evaluator CastToCollection(ScalarEvaluator scalar)
        {
            return (f,c) => scalar != null ? Focus.Create(scalar(f,c)) : Focus.Empty();
        }

        public static Evaluator TypedValue(object value)
        {
            return (_, __) => new[] { new TypedValue(value) };
        }

        public static Evaluator Constant(string name)
        {
            return (_, ctx) => new[] { ctx.ResolveConstant(name) };
        }

        public static Evaluator Infix(this Evaluator left, InfixOperator op, Evaluator right)
        {
            return (f,c) =>
            {
                var leftNodes = left(f,c);
                var rightNodes = right(f,c);

                switch (op)
                {
                    case InfixOperator.Equal:
                        return Focus.Create(leftNodes.IsEqualTo(rightNodes));
                    case InfixOperator.Add:
                    case InfixOperator.Sub:
                    case InfixOperator.Mul:
                    case InfixOperator.Div:
                        return leftNodes.Operator(op,rightNodes);
                    case InfixOperator.And:
                        return Focus.Create(leftNodes.AsBoolean() && rightNodes.AsBoolean());
                    case InfixOperator.Or:
                        return Focus.Create(leftNodes.AsBoolean() || rightNodes.AsBoolean());
                    case InfixOperator.Xor:
                        return Focus.Create(leftNodes.AsBoolean() ^ rightNodes.AsBoolean());
                    case InfixOperator.Implies:
                        return Focus.Create(!leftNodes.AsBoolean() || rightNodes.AsBoolean());
                    default:
                        throw Error.NotImplemented("Infix operator '{0}' is not yet implemented".FormatWith(op));
                }
            };
        }

        public static Evaluator Where(Evaluator condition)
        {
            return (f,c)=> f.Where(element => condition(Focus.Create(element),c).AsBoolean());
        }

        public static ScalarEvaluator All(Evaluator condition)
        {
            return (f,c) => f.Empty() || f.All(ctx => condition(Focus.Create(ctx),c).AsBoolean());
        }

        public static ScalarEvaluator Any(Evaluator condition)
        {
            return (f,c) => f.Any(ctx => condition(Focus.Create(ctx),c).AsBoolean());
        }

        public static ScalarEvaluator Empty()
        {
            return (f,_)=> f.Empty();
        }

        public static ScalarEvaluator Not()
        {
            return (f,_)=> !f.AsBoolean();
        }

        public static ScalarEvaluator Item(Evaluator index)
        {
            return (f,c) =>
            {
                var ix = index(f,c).Single().AsInteger();
                return f.Item((int)ix);
            };
        }

        public static ScalarEvaluator First()
        {
            return (f,_) => f.FirstOrDefault();
        }


        public static ScalarEvaluator Last()
        {
            return (f,_) => f.LastOrDefault();
        }

        public static Evaluator Tail()
        {
            return (f,_) => f.Skip(1);
        }

        public static Evaluator Skip(Evaluator num)
        {
            return (f,c) =>
            {
                var ix = num(f,c).Single().AsInteger();
                return f.Skip((int)ix);
            };
        }

        public static Evaluator Take(Evaluator num)
        {
            return (f,c) =>
            {
                var ix = num(f,c).Single().AsInteger();
                return f.Take((int)ix);
            };
        }

        public static ScalarEvaluator Count()
        {
            return (f,_) => f.Count();
        }

        public static ScalarEvaluator AsInteger()
        {
            return (f,_) => f.AsInteger();
        }
        public static Evaluator Children(Evaluator nameParam)
        {
            return (f,c) =>
            {
                var name = nameParam(f,c).Single().AsString();
                return f.Children(name);
            };
        }

        public static Evaluator Children(string name)
        {
            return Children(Eval.TypedValue(name));
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


