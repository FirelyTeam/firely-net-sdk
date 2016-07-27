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
using System.Text.RegularExpressions;

namespace Hl7.Fhir.FhirPath
{
    public delegate IEnumerable<IFhirPathValue> Evaluator(IEnumerable<IFhirPathValue> focus, IEvaluationContext ctx);
   // public delegate object ScalarEvaluator(IEnumerable<IFhirPathValue> focus, IEvaluationContext ctx);

    // There is a special case around the entry point, where the type of the entry point can be represented, but is optional.
    // To illustrate this point, take the path
    //      telecom.where(use = 'work').value
    // This can be evaluated as an expression on a Patient resource, or other kind of resources. 
    // However, for natural human use, expressions are often prefixed with the name of the context in which they are used:
    //	    Patient.telecom.where(use = 'work').value
    // These 2 expressions have the same outcome, but when evaluating the second, the evaluation will only produce results when used on a Patient resource.

    public static class Eval
    {
        public static IEnumerable<IFhirPathValue> Evaluate(this Evaluator evaluator, IFhirPathValue instance, IEvaluationContext context)
        {
            var original = FhirValueList.Create(instance);
            context.OriginalContext = original;
            return evaluator(original, context);
        }

        public static IEnumerable<IFhirPathValue> Evaluate(this Evaluator evaluator, IFhirPathValue instance)
        {
            var original = FhirValueList.Create(instance);
            return evaluator.Evaluate(instance, new EvaluationContext());
        }

        public static object Scalar(this Evaluator evaluator, IFhirPathValue instance, IEvaluationContext context)
        {
            return evaluator.Evaluate(instance, context).SingleValue();
        }

        public static object Scalar(this Evaluator evaluator, IFhirPathValue instance)
        {
            return evaluator.Evaluate(instance, new EvaluationContext()).SingleValue();
        }

        public static bool Predicate(this Evaluator evaluator, IFhirPathValue instance, IEvaluationContext context)
        {
            var focus = evaluator.Evaluate(instance, context);

            // An empty result is considered "true" for invariant processing
            if (!focus.Any())
                return true;

            // A single result that's a boolean should be interpreted as a boolean
            else if (focus.JustValues().Count() == 1 && focus.JustValues().Single().Value is Boolean)
            {
                return focus.Single().AsBoolean();
            }

            // Otherwise, we have "some" content, which we'll consider "true"
            return true;
        }

        public static bool IsTrue(this Evaluator evaluator, IFhirPathValue instance)
        {
            return evaluator.Evaluate(instance, new EvaluationContext()).BooleanEval().AsBoolean();
        }
        public static Evaluator Then(this Evaluator first, Evaluator then)
        {
            return (f,c) =>  then(first(f,c),c);
        }

        //public static Evaluator Chain(IEnumerable<Evaluator> evaluators)
        //{
        //    return c =>
        //        evaluators.Aggregate(c, (result, next) => next(result));
        //}


        public static Evaluator TypedValue(object value)
        {
            return (_, __) => new[] { new TypedValue(value) };
        }

        public static Evaluator Constant(string name)
        {
            return (_, ctx) => new[] { ctx.ResolveConstant(name) };
        }


        public static Evaluator Axis(Axis axis)
        {
            return (focus, ctx) =>
            {
                switch (axis)
                {
                    case FhirPath.Axis.Descendants:
                        return focus.JustElements().Descendants();
                    case FhirPath.Axis.Parent:
                        return focus.JustElements().Parents();
                    case FhirPath.Axis.Focus:
                        return focus;
                    case FhirPath.Axis.Children:
                        return focus.JustElements().Children();
                    case FhirPath.Axis.Context:
                        if (ctx.OriginalContext == null)
                            throw new InvalidOperationException("Cannot resolve the $context, the Evaluator did not provide it at runtime");
                        else
                            return ctx.OriginalContext;
                    case FhirPath.Axis.Resource:
                        if (ctx.OriginalResource == null)
                            throw new InvalidOperationException("Cannot resolve the $resource, the evaluation context has no information about it.");
                        else
                            return FhirValueList.Create(ctx.OriginalResource);
                    default:
                        throw new InvalidOperationException("Internal error: unknown axis '{0}'".FormatWith(axis));
                }
            };
        }

        public static Evaluator Length()
        {
            return (f, _) => f.MaxLength();
        }

        public static Evaluator Extension(Evaluator url)
        {
            return (f, c) =>
            {
                var u = url(f, c).AsString();
                return f.Extension(u);
            };
        }

        public static Evaluator Substring(Evaluator start, Evaluator length)
        {
            return (f, c) =>
            {
                long s = start(f, c).AsInteger();
                long? l = length != null ? length(f, c).AsInteger() : (long?)null;

                return f.Substring(s, l);
            };
        }

        public static Evaluator Infix(this Evaluator left, InfixOperator op, Evaluator right)
        {
            return (f,c) =>
            {
                var leftNodes = left(f,c);
                var rightNodes = right(f,c);

                IEnumerable<IFhirPathValue> result = null;

                switch (op)
                {
                    case InfixOperator.Equals:
                        result = leftNodes.IsEqualTo(rightNodes); break;
                    case InfixOperator.NotEqual:
                        result = leftNodes.IsNotEqualTo(rightNodes); break;
                    case InfixOperator.Equivalent:
                        result = leftNodes.IsEquivalentTo(rightNodes); break;
                    case InfixOperator.GreaterThan:
                        result = leftNodes.GreaterThan(rightNodes); break;
                    case InfixOperator.GreaterOrEqual:
                        result = leftNodes.GreaterOrEqual(rightNodes); break;
                    case InfixOperator.LessThan:
                        result = leftNodes.LessThan(rightNodes); break;
                    case InfixOperator.LessOrEqual:
                        result = leftNodes.LessOrEqual(rightNodes); break;
                    case InfixOperator.Add:
                        result = leftNodes.Add(rightNodes); break;
                    case InfixOperator.Sub:
                        result = leftNodes.Sub(rightNodes); break;
                    case InfixOperator.Mul:
                        result = leftNodes.Mul(rightNodes); break;
                    case InfixOperator.Div:
                        result = leftNodes.Div(rightNodes); break;
                    case InfixOperator.And:
                        result = leftNodes.And(rightNodes); break;
                    case InfixOperator.Or:
                        result = leftNodes.Or(rightNodes); break;
                    case InfixOperator.Xor:
                        result = leftNodes.Xor(rightNodes); break;
                    case InfixOperator.Implies:
                        result = leftNodes.Implies(rightNodes); break;
                    case InfixOperator.Union:
                        result = leftNodes.Union(rightNodes, new IFhirPathValueListExtensions.FhirPathValueEqualityComparer()); break;
                    case InfixOperator.Concat:
                        result = leftNodes.Add(rightNodes); break;  // should only work for strings ;-)                        
                    case InfixOperator.In:
                        result = leftNodes.SubsetOf(rightNodes); break;
                    default:
                        throw Error.NotImplemented("Infix operator '{0}' is not yet implemented".FormatWith(op));
                }

                return result;
            };
        }

        public static Evaluator Where(Evaluator condition)
        {
            return (f,c)=> f.Where(elements => condition(elements,c));
        }

        public static Evaluator All(Evaluator condition)
        {
            return (f,c) => f.All(elements => condition(elements,c));
        }

        public static Evaluator Any(Evaluator condition)
        {
            return (f, c) =>
            {
                if (condition != null)
                    return f.Any(elements => condition(elements, c));
                else
                    return FhirValueList.Create(f.Any());
            };
        }

        public static Evaluator Select(Evaluator mapper)
        {
            return (f, c) => f.Select(elements => mapper(elements, c));
        }

        public static Evaluator Exists()
        {
            return (f,_)=> f.IsEmpty().Not();
        }
        public static Evaluator Single()
        {
            return (f, _) =>
            {
                var a = f.Single();
                return FhirValueList.Create(a);
            };
        }

        public static Evaluator Empty()
        {
            return (f, _) => f.IsEmpty();
        }

        public static Evaluator Not()
        {
            return (f, _) => f.Not();
        }

        public static Evaluator Item(Evaluator index)
        {
            return (f,c) =>
            {
                var ix = index(f,c).Single().AsInteger();
                return f.Item((int)ix);
            };
        }

        public static Evaluator First()
        {
            return (f,_) => f.Take(1);
        }


        public static Evaluator Last()
        {
            return (f, _) => f.Reverse().Take(1);
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

        public static Evaluator Count()
        {
            return (f, _) => f.CountItems();
        }

        public static Evaluator ToInteger()
        {
            return (f,_) => f.IntegerEval();
        }

        public static Evaluator ToDecimal()
        {
            return (f, _) => f.DecimalEval();
        }

        public static Evaluator ToString()
        {
            return (f, _) => f.StringEval();
        }

        public static Evaluator StartsWith(Evaluator prefix)
        {
            return (f, c) =>
            {
                var p = prefix(f, c).Single().AsString();
                return f.StartingWith(p);
            };
        }

        public static Evaluator Log(Evaluator argument)
        {
            return (f, c) =>
            {
                var arg = argument(f, c).Single().AsString();
                c.Log(arg, f);
                return f;
            };
        }

        /// <summary>
        /// This value is here primarily for unit testing to make the value predictable
        /// </summary>
        public static DateTime? FixedNowValue;
        public static Evaluator Now()
        {
            return (f, c) =>
            {
                System.Diagnostics.Trace.WriteLine("Evaluating the now() expression");
                if (FixedNowValue.HasValue)
                {
                    return new[] { new TypedValue(PartialDateTime.Parse(FixedNowValue.Value.ToFhirDateTime())) };
                }
                return new[] { new TypedValue(PartialDateTime.Parse(DateTime.Now.ToFhirDateTime())) };
            };
        }

        public static Evaluator Today()
        {
            return (f, c) =>
            {
            //    System.Diagnostics.Trace.WriteLine("Evaluating the today() expression");
                if (FixedNowValue.HasValue)
                {
                    return new[] { new TypedValue(new PartialDateTime(FixedNowValue.Value.Date, PartialDateTime.Precision.Day)) };
                }
                return new[] { new TypedValue(new PartialDateTime(DateTimeOffset.Now.Date, PartialDateTime.Precision.Day)) };
            };
        }

        /// <summary>
        /// Add values to a component of a date
        /// </summary>
        /// <param name="datepart">the part of the date to modify, yy | mm | dd | hh | mi | ss</param>
        /// <param name="value">The value to increment in the date</param>
        /// <returns></returns>
        public static Evaluator DateAdd(Evaluator datepart, Evaluator value)
        {
            return (f, c) =>
            {
                string part = datepart != null ? datepart(f, c).Single().ToString() : null;
                int valueToAdd = (int)value(f, c).AsInteger();

                return f.DateAdd(part, valueToAdd);
                // return new[] { new TypedValue(PartialDateTime.Parse(DateTime.Today.AddDays(2).ToFhirDate())) };
            };
        }

        public static Evaluator Resolve()
        {
            return (f, c) => f.Resolve(c);
        }

        public static Evaluator Distinct()
        {
            return (f, c) => f.Distinct();
        }

        public static Evaluator Contains(Evaluator substring)
        {
            return (f, c) =>
            {
                var subs = substring(f, c).AsString();
                return f.JustValues().Where(v => v.AsStringRepresentation().Contains(subs));
            };
        }

        public static Evaluator Matches(Evaluator regexp)
        {
            return (f, c) =>
            {
                var r = regexp(f, c).AsString();
                return f.JustValues().Where(v => Regex.IsMatch(v.AsStringRepresentation(), r));
            };
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


    public enum Axis
    {
        Children,
        Descendants,
        Context,
        Resource,
        Parent,
        Focus
    }

  }
