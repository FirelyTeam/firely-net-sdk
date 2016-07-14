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

namespace Hl7.Fhir.FluentPath
{
    public delegate IEnumerable<IValueProvider> Evaluator(IEvaluationContext ctx, IEnumerable<IValueProvider> focus);
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
        public static IEnumerable<IValueProvider> Select(this Evaluator evaluator, IEnumerable<IValueProvider> input, IEvaluationContext context)
        {
            context.OriginalContext = input;
            return evaluator(context, input);
        }

        public static IEnumerable<IValueProvider> Select(this Evaluator evaluator, IEnumerable<IValueProvider> input)
        {
            return evaluator.Select(input, new BaseEvaluationContext());
        }

        public static object Scalar(this Evaluator evaluator, IEnumerable<IValueProvider> input, IEvaluationContext context)
        {
            return evaluator.Select(input, context).Single().Value;
        }

        public static object Scalar(this Evaluator evaluator, IEnumerable<IValueProvider> input)
        {
            return evaluator.Scalar(input, new BaseEvaluationContext());
        }

        // For predicates, Empty is considered false (?)
        public static bool Predicate(this Evaluator evaluator, IEnumerable<IValueProvider> input, IEvaluationContext context)
        {
            var result = evaluator.Select(input, context).BooleanEval();

            if (!result == null)
                return false;
            else
                return result.Value;
        }

        public static bool Predicate(this Evaluator evaluator, IEnumerable<IValueProvider> input)
        {
            return evaluator.Predicate(input, new BaseEvaluationContext());
        }


       //public static Evaluator Extension(Evaluator url)
        //{
        //    return (f, c) =>
        //    {
        //        var u = url(f, c).AsString();
        //        return f.Extension(u);
        //    };
        //}


        //public static Evaluator All(Evaluator condition)
        //{
        //    return (f,c) => f.All(elements => condition(elements,c));
        //}

        //public static Evaluator Any(Evaluator condition)
        //{
        //    return (f, c) =>
        //    {
        //        if (condition != null)
        //            return f.Any(elements => condition(elements, c));
        //        else
        //            return FhirValueList.Create(f.Any());
        //    };
        //}

        //public static Evaluator Select(Evaluator mapper)
        //{
        //    return (f, c) => f.Select(elements => mapper(elements, c));
        //}

      
        //public static Evaluator Item(Evaluator index)
        //{
        //    return (f,c) =>
        //    {
        //        var ix = index(f,c).Single().AsInteger();
        //        return f.Item((int)ix);
        //    };
        //}


        //public static Evaluator AsInteger()
        //{
        //    return (f,_) => f.IntegerEval();
        //}

        //public static Evaluator StartsWith(Evaluator prefix)
        //{
        //    return (f, c) =>
        //    {
        //        var p = prefix(f, c).Single().AsString();
        //        return f.StartingWith(p);
        //    };
        //}

        //public static Evaluator Log(Evaluator argument)
        //{
        //    return (f, c) =>
        //    {
        //        var arg = argument(f, c).Single().AsString();
        //        c.Log(arg, f);
        //        return f;
        //    };
        //}

        //public static Evaluator Today()
        //{
        //    return Eval.Return(PartialDateTime.Today());
        //}

        //public static Evaluator Distinct()
        //{
        //    return (f, c) => f.Distinct();
        //}

        //public static Evaluator Contains(Evaluator substring)
        //{
        //    return (f, c) =>
        //    {
        //        var subs = substring(f, c).AsString();
        //        return f.JustValues().Where(v => v.AsStringRepresentation().Contains(subs));
        //    };
        //}

        //public static Evaluator Matches(Evaluator regexp)
        //{
        //    return (f, c) =>
        //    {
        //        var r = regexp(f, c).AsString();
        //        return f.JustValues().Where(v => Regex.IsMatch(v.AsStringRepresentation(), r));
        //    };
        //}

        
    }
  }
