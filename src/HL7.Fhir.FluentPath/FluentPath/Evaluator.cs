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
    public delegate IEnumerable<IValueProvider> Evaluator(IEvaluationContext ctx);
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
        public static IEnumerable<IValueProvider> Select(this Evaluator evaluator, IValueProvider instance, IEvaluationContext context)
        {
            var original = FhirValueList.Create(instance);
            context.OriginalContext = original;
            context.Push(original);
            return evaluator(context);
        }

        public static IEnumerable<IValueProvider> Select(this Evaluator evaluator, IValueProvider instance)
        {
            var original = FhirValueList.Create(instance);
            return evaluator.Select(instance, new BaseEvaluationContext());
        }

        public static object Scalar(this Evaluator evaluator, IValueProvider instance, IEvaluationContext context)
        {
            return evaluator.Select(instance, context).SingleValue();
        }

        public static object Scalar(this Evaluator evaluator, IValueProvider instance)
        {
            return evaluator.Scalar(instance, new BaseEvaluationContext());
        }

        // For predicates, Empty is considered false (?)
        public static bool Predicate(this Evaluator evaluator, IValueProvider instance, IEvaluationContext context)
        {
            return evaluator.Select(instance, context).BooleanEval() == true;
        }

        public static bool Predicate(this Evaluator evaluator, IValueProvider instance)
        {
            return evaluator.Predicate(instance, new BaseEvaluationContext());
        }


        //public static Evaluator Length()
        //{
        //    return (f, _) => f.MaxLength();
        //}

        //public static Evaluator Extension(Evaluator url)
        //{
        //    return (f, c) =>
        //    {
        //        var u = url(f, c).AsString();
        //        return f.Extension(u);
        //    };
        //}

        //public static Evaluator Substring(Evaluator start, Evaluator length)
        //{
        //    return (f, c) =>
        //    {
        //        long s = start(f, c).AsInteger();
        //        long? l = length != null ? length(f, c).AsInteger() : (long?)null;

        //        return f.Substring(s, l);
        //    };
        //}

     

        //        return result;
        //    };
        //}

        //public static Evaluator Where(Evaluator condition)
        //{
        //    return (f,c)=> f.Where(elements => condition(elements,c));
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

        //public static Evaluator First()
        //{
        //    return (f,_) => f.Take(1);
        //}


        //public static Evaluator Last()
        //{
        //    return (f, _) => f.Reverse().Take(1);
        //}

        //public static Evaluator Tail()
        //{
        //    return (f,_) => f.Skip(1);
        //}

        //public static Evaluator Skip(Evaluator num)
        //{
        //    return (f,c) =>
        //    {
        //        var ix = num(f,c).Single().AsInteger();
        //        return f.Skip((int)ix);
        //    };
        //}

        //public static Evaluator Take(Evaluator num)
        //{
        //    return (f,c) =>
        //    {
        //        var ix = num(f,c).Single().AsInteger();
        //        return f.Take((int)ix);
        //    };
        //}

        //public static Evaluator Count()
        //{
        //    return (f, _) => f.CountItems();
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
