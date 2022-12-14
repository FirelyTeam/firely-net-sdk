/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Utility;
using Hl7.FhirPath.FhirPath.Functions;
using Hl7.FhirPath.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using P = Hl7.Fhir.ElementModel.Types;

namespace Hl7.FhirPath.Expressions
{
    public static class SymbolTableInit
    {
        /// <summary>
        /// Add the function library for the standard FhirPath Normative dialect to the <see cref="SymbolTable"/>.
        /// </summary>
        public static SymbolTable AddStandardFP(this SymbolTable t)
        {
            // Functions that operate on the focus, without null propagation
            t.Add("empty", (IEnumerable<object> f) => !f.Any());
            t.Add("exists", (IEnumerable<object> f) => f.Any());

            t.Add("count", (IEnumerable<object> f) => f.Count());
            t.Add("trace", (IEnumerable<ITypedElement> f, string name, EvaluationContext ctx)
                    => f.Trace(name, ctx));

            t.Add("allTrue", (IEnumerable<ITypedElement> f) => f.All(e => e.Value as bool? == true));
            t.Add("anyTrue", (IEnumerable<ITypedElement> f) => f.Any(e => e.Value as bool? == true));
            t.Add("allFalse", (IEnumerable<ITypedElement> f) => f.All(e => e.Value as bool? == false));
            t.Add("anyFalse", (IEnumerable<ITypedElement> f) => f.Any(e => e.Value as bool? == false));
            t.Add("combine", (IEnumerable<ITypedElement> l, IEnumerable<ITypedElement> r) => l.Concat(r));
            t.Add("binary.|", (object _, IEnumerable<ITypedElement> l, IEnumerable<ITypedElement> r) => l.DistinctUnion(r));
            t.Add("union", (IEnumerable<ITypedElement> l, IEnumerable<ITypedElement> r) => l.DistinctUnion(r));
            t.Add("binary.contains", (object _, IEnumerable<ITypedElement> a, ITypedElement b) => a.Contains(b));
            t.Add("binary.in", (object _, ITypedElement a, IEnumerable<ITypedElement> b) => b.Contains(a));
            t.Add("distinct", (IEnumerable<ITypedElement> f) => f.Distinct());
            t.Add("isDistinct", (IEnumerable<ITypedElement> f) => f.IsDistinct());
            t.Add("subsetOf", (IEnumerable<ITypedElement> f, IEnumerable<ITypedElement> a) => f.SubsetOf(a));
            t.Add("supersetOf", (IEnumerable<ITypedElement> f, IEnumerable<ITypedElement> a) => a.SubsetOf(f));
            t.Add("intersect", (IEnumerable<ITypedElement> f, IEnumerable<ITypedElement> a) => f.Intersect(a));
            t.Add("exclude", (IEnumerable<ITypedElement> f, IEnumerable<ITypedElement> a) => f.Exclude(a));

            t.Add("today", (object _) => P.Date.Today());
            t.Add("now", (object _) => P.DateTime.Now());
            t.Add("timeOfDay", (object _) => P.Time.Now());

            t.Add("binary.&", (object _, string a, string b) => (a ?? "") + (b ?? ""));

            t.Add(new CallSignature("iif", typeof(IEnumerable<ITypedElement>), typeof(object), typeof(bool?), typeof(Invokee), typeof(Invokee)), runIif);
            t.Add(new CallSignature("iif", typeof(IEnumerable<ITypedElement>), typeof(object), typeof(bool?), typeof(Invokee)), runIif);

            // Functions that use normal null propagation and work with the focus (buy may ignore it)
            t.Add("not", (IEnumerable<ITypedElement> f) => f.Not(), doNullProp: true);
            t.Add("builtin.children", (IEnumerable<ITypedElement> f, string a) => f.Navigate(a), doNullProp: true);

            t.Add("children", (IEnumerable<ITypedElement> f) => f.Children(), doNullProp: true);
            t.Add("descendants", (IEnumerable<ITypedElement> f) => f.Descendants(), doNullProp: true);

            t.Add("binary.=", (object f, IEnumerable<ITypedElement> a, IEnumerable<ITypedElement> b) => a.IsEqualTo(b), doNullProp: true);
            t.Add("binary.!=", (object f, IEnumerable<ITypedElement> a, IEnumerable<ITypedElement> b) => !a.IsEqualTo(b), doNullProp: true);
            t.Add("binary.~", (object f, IEnumerable<ITypedElement> a, IEnumerable<ITypedElement> b) => a.IsEquivalentTo(b), doNullProp: false);
            t.Add("binary.!~", (object f, IEnumerable<ITypedElement> a, IEnumerable<ITypedElement> b) => !a.IsEquivalentTo(b), doNullProp: false);

            t.Add("unary.-", (object f, int a) => -a, doNullProp: true);
            t.Add("unary.-", (object f, long a) => -a, doNullProp: true);
            t.Add("unary.-", (object f, decimal a) => -a, doNullProp: true);
            t.Add("unary.-", (object f, P.Quantity a) => new P.Quantity(-a.Value, a.Unit), doNullProp: true);
            t.Add("unary.+", (object f, int a) => a, doNullProp: true);
            t.Add("unary.+", (object f, long a) => a, doNullProp: true);
            t.Add("unary.+", (object f, decimal a) => a, doNullProp: true);
            t.Add("unary.+", (object f, P.Quantity a) => a, doNullProp: true);

            t.Add("binary.*", (object f, int a, int b) => a * b, doNullProp: true);
            t.Add("binary.*", (object f, long a, long b) => a * b, doNullProp: true);
            t.Add("binary.*", (object f, decimal a, decimal b) => a * b, doNullProp: true);
            t.Add("binary.*", (object f, P.Quantity a, P.Quantity b) => a * b, doNullProp: true);

            t.Add("binary./", (object f, decimal a, decimal b) => b != 0 ? a / b : (decimal?)null, doNullProp: true);
            t.Add("binary./", (object f, P.Quantity a, P.Quantity b) => a / b, doNullProp: true);

            t.Add("binary.+", (object f, int a, int b) => a + b, doNullProp: true);
            t.Add("binary.+", (object f, long a, long b) => a + b, doNullProp: true);
            t.Add("binary.+", (object f, decimal a, decimal b) => a + b, doNullProp: true);
            t.Add("binary.+", (object f, string a, string b) => a + b, doNullProp: true);
            t.Add("binary.+", (object f, P.DateTime a, P.Quantity b) => a + b, doNullProp: true);
            t.Add("binary.+", (object f, P.Date a, P.Quantity b) => a + b, doNullProp: true);
            t.Add("binary.+", (object f, P.Quantity a, P.Quantity b) => a + b, doNullProp: true);

            t.Add("binary.-", (object f, int a, int b) => a - b, doNullProp: true);
            t.Add("binary.-", (object f, long a, long b) => a - b, doNullProp: true);
            t.Add("binary.-", (object f, decimal a, decimal b) => a - b, doNullProp: true);
            t.Add("binary.-", (object f, P.Quantity a, P.Quantity b) => a - b, doNullProp: true);

            t.Add("binary.div", (object f, int a, int b) => b != 0 ? a / b : (int?)null, doNullProp: true);
            t.Add("binary.div", (object f, long a, long b) => b != 0 ? a / b : (long?)null, doNullProp: true);
            t.Add("binary.div", (object f, decimal a, decimal b) => b != 0 ? (long?)Math.Truncate(a / b) : null, doNullProp: true);

            t.Add("binary.mod", (object f, int a, int b) => b != 0 ? a % b : (int?)null, doNullProp: true);
            t.Add("binary.mod", (object f, long a, long b) => b != 0 ? a % b : (long?)null, doNullProp: true);
            t.Add("binary.mod", (object f, decimal a, decimal b) => b != 0 ? a % b : (decimal?)null, doNullProp: true);

            t.Add("binary.>", (object f, P.Any a, P.Any b) => EqualityOperators.Compare(a, b, ">"), doNullProp: true);
            t.Add("binary.<", (object f, P.Any a, P.Any b) => EqualityOperators.Compare(a, b, "<"), doNullProp: true);
            t.Add("binary.<=", (object f, P.Any a, P.Any b) => EqualityOperators.Compare(a, b, "<="), doNullProp: true);
            t.Add("binary.>=", (object f, P.Any a, P.Any b) => EqualityOperators.Compare(a, b, ">="), doNullProp: true);

            t.Add("single", (IEnumerable<ITypedElement> f) => f.Single(), doNullProp: true);
            t.Add("skip", (IEnumerable<ITypedElement> f, long a) => f.Skip((int)a), doNullProp: true);
            t.Add("first", (IEnumerable<ITypedElement> f) => f.First(), doNullProp: true);
            t.Add("last", (IEnumerable<ITypedElement> f) => f.Last(), doNullProp: true);
            t.Add("tail", (IEnumerable<ITypedElement> f) => f.Tail(), doNullProp: true);
            t.Add("take", (IEnumerable<ITypedElement> f, long a) => f.Take((int)a), doNullProp: true);
            t.Add("builtin.item", (IEnumerable<ITypedElement> f, long a) => f.Item((int)a), doNullProp: true);

            t.Add("toBoolean", (P.Any f) => f.ToBoolean(), doNullProp: true);
            t.Add("convertsToBoolean", (P.Any f) => f.ConvertsToBoolean(), doNullProp: true);
            t.Add("toInteger", (P.Any f) => f.ToInteger(), doNullProp: true);
            t.Add("convertsToInteger", (P.Any f) => f.ConvertsToInteger(), doNullProp: true);
            t.Add("toLong", (P.Any f) => f.ToLong(), doNullProp: true);
            t.Add("convertsToLong", (P.Any f) => f.ConvertsToLong(), doNullProp: true);
            t.Add("toDecimal", (P.Any f) => f.ToDecimal(), doNullProp: true);
            t.Add("convertsToDecimal", (P.Any f) => f.ConvertsToDecimal(), doNullProp: true);
            t.Add("toQuantity", (P.Any f) => f.ToQuantity(), doNullProp: true);
            t.Add("convertsToQuantity", (P.Any f) => f.ConvertsToQuantity(), doNullProp: true);
            t.Add("toString", (P.Any f) => f.ToStringRepresentation(), doNullProp: true);
            t.Add("convertsToString", (P.Any f) => f.ConvertsToString(), doNullProp: true);
            t.Add("toDate", (P.Any f) => f.ToDate(), doNullProp: true);
            t.Add("convertsToDate", (P.Any f) => f.ConvertsToDate(), doNullProp: true);
            t.Add("toDateTime", (P.Any f) => f.ToDateTime(), doNullProp: true);
            t.Add("convertsToDateTime", (P.Any f) => f.ConvertsToDateTime(), doNullProp: true);
            t.Add("toTime", (P.Any f) => f.ToTime(), doNullProp: true);
            t.Add("convertsToTime", (P.Any f) => f.ConvertsToTime(), doNullProp: true);

            t.Add("upper", (string f) => f.ToUpper(), doNullProp: true);
            t.Add("lower", (string f) => f.ToLower(), doNullProp: true);
            t.Add("toChars", (string f) => f.ToChars(), doNullProp: true);
            t.Add("substring", (string f, int a) => f.FpSubstring(a, null), doNullProp: true);
            t.Add("trim", (string f) => f.Trim(), doNullProp: true);
            t.Add("encode", (string f, string enc) => f.FpEncode(enc), doNullProp: true);
            t.Add("decode", (string f, string enc) => f.FpDecode(enc), doNullProp: true);
            t.Add("escape", (string f, string enc) => f.FpEscape(enc), doNullProp: true);
            t.Add("unescape", (string f, string enc) => f.FpUnescape(enc), doNullProp: true);

            //special case: only focus should be Null propagated:
            t.Add(new CallSignature("substring", typeof(string), typeof(string), typeof(int), typeof(int?)),
                InvokeeFactory.WrapWithPropNullForFocus((string f, int a, int? b) => f.FpSubstring(a, b)));
            t.Add("startsWith", (string f, string fragment) => f.StartsWith(fragment), doNullProp: true);
            t.Add("endsWith", (string f, string fragment) => f.EndsWith(fragment), doNullProp: true);
            t.Add("matches", (string f, string regex) => Regex.IsMatch(f, regex), doNullProp: true);
            t.Add("indexOf", (string f, string fragment) => f.FpIndexOf(fragment), doNullProp: true);
            t.Add("contains", (string f, string fragment) => f.Contains(fragment), doNullProp: true);
            t.Add("replaceMatches", (string f, string regex, string subst) => Regex.Replace(f, regex, subst), doNullProp: true);
            t.Add("replace", (string f, string regex, string subst) => f.FpReplace(regex, subst), doNullProp: true);
            t.Add("length", (string f) => f.Length, doNullProp: true);
            t.Add("split", (string f, string seperator) => f.FpSplit(seperator), doNullProp: true);
            t.Add("join", (IEnumerable<ITypedElement> f, string separator) => f.FpJoin(separator), doNullProp: true);

            // Math functions
            t.Add("abs", (decimal f) => Math.Abs(f), doNullProp: true);
            t.Add("abs", (P.Quantity f) => new P.Quantity(Math.Abs(f.Value), f.Unit), doNullProp: true);
            t.Add("ceiling", (decimal f) => Math.Ceiling(f), doNullProp: true);
            t.Add("exp", (decimal f) => Math.Exp((double)f), doNullProp: true);
            t.Add("floor", (decimal f) => Math.Floor(f), doNullProp: true);
            t.Add("ln", (decimal f) => Math.Log((double)f), doNullProp: true);
            t.Add("log", (decimal f, decimal @base) => Math.Log((double)f, (double)@base), doNullProp: true);
            t.Add("power", (decimal f, decimal exponent) => f.Power(exponent), doNullProp: true);
            t.Add("round", (decimal f, long precision) => Math.Round(f, (int)precision), doNullProp: true);
            t.Add("round", (decimal f) => Math.Round(f), doNullProp: true);
            t.Add("sqrt", (decimal f) => f.Sqrt(), doNullProp: true);
            t.Add("truncate", (decimal f) => Math.Truncate((double)f), doNullProp: true);

            // The next two functions existed pre-normative, so we have kept them.
            t.Add("is", (ITypedElement f, string name) => f.Is(name), doNullProp: true);
            t.Add("as", (IEnumerable<ITypedElement> f, string name) => f.FilterType(name), doNullProp: true);

            t.Add("ofType", (IEnumerable<ITypedElement> f, string name) => f.FilterType(name), doNullProp: true);
            t.Add("binary.is", (object f, ITypedElement left, string name) => left.Is(name), doNullProp: true);
            t.Add("binary.as", (object f, IEnumerable<ITypedElement> left, string name) => left.FilterType(name), doNullProp: true);

            // Kept for backwards compatibility, but no longer part of the spec
            t.Add("binary.as", (object f, IEnumerable<ITypedElement> left, string name) => left.FilterType(name), doNullProp: true);

            t.Add("extension", (IEnumerable<ITypedElement> f, string url) => f.Extension(url), doNullProp: true);

            // Logic operators do not use null propagation and may do short-cut eval
            t.AddLogic("binary.and", (a, b) => a.And(b));
            t.AddLogic("binary.or", (a, b) => a.Or(b));
            t.AddLogic("binary.xor", (a, b) => a.XOr(b));
            t.AddLogic("binary.implies", (a, b) => a.Implies(b));

            // Special late-bound functions
            t.Add(new CallSignature("where", typeof(IEnumerable<ITypedElement>), typeof(object), typeof(Invokee)), runWhere);
            t.Add(new CallSignature("select", typeof(IEnumerable<ITypedElement>), typeof(object), typeof(Invokee)), runSelect);
            t.Add(new CallSignature("all", typeof(bool), typeof(object), typeof(Invokee)), runAll);
            t.Add(new CallSignature("any", typeof(bool), typeof(object), typeof(Invokee)), runAny);
            t.Add(new CallSignature("exists", typeof(bool), typeof(object), typeof(Invokee)), runAny);
            t.Add(new CallSignature("repeat", typeof(IEnumerable<ITypedElement>), typeof(object), typeof(Invokee)), runRepeat);
            t.Add(new CallSignature("trace", typeof(IEnumerable<ITypedElement>), typeof(string), typeof(object), typeof(Invokee)), Trace);

            t.Add(new CallSignature("aggregate", typeof(IEnumerable<ITypedElement>), typeof(Invokee), typeof(Invokee)), runAggregate);
            t.Add(new CallSignature("aggregate", typeof(IEnumerable<ITypedElement>), typeof(Invokee), typeof(Invokee), typeof(Invokee)), runAggregate);

            t.AddVar("sct", "http://snomed.info/sct");
            t.AddVar("loinc", "http://loinc.org");
            t.AddVar("ucum", "http://unitsofmeasure.org");

            t.Add("builtin.coreexturl", (object f, string id) => getCoreExtensionUrl(id));
            t.Add("builtin.corevsurl", (object f, string id) => getCoreValueSetUrl(id));

            return t;
        }


        private static string getCoreExtensionUrl(string id)
        {
            return "http://hl7.org/fhir/StructureDefinition/" + id;
        }

        private static string getCoreValueSetUrl(string id)
        {
            return "http://hl7.org/fhir/ValueSet/" + id;
        }

        private static IEnumerable<ITypedElement> runAggregate(Closure ctx, IEnumerable<Invokee> arguments)
        {
            var focus = arguments.First()(ctx, InvokeeFactory.EmptyArgs);
            var incrExpre = arguments.Skip(1).First();
            IEnumerable<ITypedElement> initialValue = ElementNode.EmptyList;
            if (arguments.Count() > 2)
            {
                var initialValueExpr = arguments.Skip(2).First();
                initialValue = initialValueExpr(ctx, InvokeeFactory.EmptyArgs);
            }
            var totalContext = ctx.Nest();
            totalContext.SetTotal(initialValue);
            foreach (ITypedElement element in focus)
            {
                var newFocus = ElementNode.CreateList(element);
                var newContext = totalContext.Nest(newFocus);
                newContext.SetThis(newFocus);
                newContext.SetTotal(totalContext.GetTotal());
                var newTotalResult = incrExpre(newContext, InvokeeFactory.EmptyArgs);
                totalContext.SetTotal(newTotalResult);
            }
            return ElementNode.CreateList(totalContext.GetTotal());
        }

        private static IEnumerable<ITypedElement> Trace(Closure ctx, IEnumerable<Invokee> arguments)
        {
            var focus = arguments.First()(ctx, InvokeeFactory.EmptyArgs);
            string name = arguments.Skip(1).First()(ctx, InvokeeFactory.EmptyArgs).FirstOrDefault()?.Value as string;

            List<Invokee> selectArgs = new() { arguments.First() };
            selectArgs.AddRange(arguments.Skip(2));
            var selectResults = runSelect(ctx, selectArgs);
            ctx?.EvaluationContext?.Tracer?.Invoke(name, selectResults);

            return focus;
        }

        private static IEnumerable<ITypedElement> runIif(Closure ctx, IEnumerable<Invokee> arguments)
        {
            // iif(criterion: expression, true-result: collection [, otherwise-result: collection]) : collection
            // note: short-circuit behavior is expected in this function
            var focus = arguments.First()(ctx, InvokeeFactory.EmptyArgs);

            var expression = arguments.Skip(1).First()(ctx, InvokeeFactory.EmptyArgs);
            var trueResult = arguments.Skip(2).First();
            var otherResult = arguments.Skip(3).FirstOrDefault();

            if (expression.Count() > 1)
                throw Error.InvalidOperation($"Result of {nameof(expression)} is not of type boolean");

            return (expression.BooleanEval() ?? false)
                ? trueResult(ctx, InvokeeFactory.EmptyArgs)
                : otherResult == null ? ElementNode.EmptyList : otherResult(ctx, InvokeeFactory.EmptyArgs);
        }

        private static IEnumerable<ITypedElement> runWhere(Closure ctx, IEnumerable<Invokee> arguments)
        {
            var focus = arguments.First()(ctx, InvokeeFactory.EmptyArgs);
            var lambda = arguments.Skip(1).First();
            var index = 0;

            foreach (ITypedElement element in focus)
            {
                var newFocus = ElementNode.CreateList(element);
                var newContext = ctx.Nest(newFocus);
                newContext.SetThis(newFocus);
                newContext.SetIndex(ElementNode.CreateList(index));
                index++;

                if (lambda(newContext, InvokeeFactory.EmptyArgs).BooleanEval() == true)
                    yield return element;
            }
        }

        private static IEnumerable<ITypedElement> runSelect(Closure ctx, IEnumerable<Invokee> arguments)
        {
            var focus = arguments.First()(ctx, InvokeeFactory.EmptyArgs);
            var lambda = arguments.Skip(1).First();
            var index = 0;

            foreach (ITypedElement element in focus)
            {
                var newFocus = ElementNode.CreateList(element);
                var newContext = ctx.Nest(newFocus);
                newContext.SetThis(newFocus);
                newContext.SetIndex(ElementNode.CreateList(index));
                index++;

                var result = lambda(newContext, InvokeeFactory.EmptyArgs);
                foreach (var resultElement in result)       // implement SelectMany()
                    yield return resultElement;
            }
        }

        private static IEnumerable<ITypedElement> runRepeat(Closure ctx, IEnumerable<Invokee> arguments)
        {
            var focus = arguments.First()(ctx, InvokeeFactory.EmptyArgs);
            var lambda = arguments.Skip(1).First();

            var fullResult = new List<ITypedElement>();
            List<ITypedElement> newNodes = new(focus);

            while (newNodes.Any())
            {
                var index = 0;
                var current = newNodes;
                newNodes = new List<ITypedElement>();

                foreach (ITypedElement element in current)
                {
                    var newFocus = ElementNode.CreateList(element);
                    var newContext = ctx.Nest(newFocus);
                    newContext.SetThis(newFocus);
                    newContext.SetIndex(ElementNode.CreateList(index));
                    index++;

                    var candidates = lambda(newContext, InvokeeFactory.EmptyArgs);
                    var uniqeNewNodes = candidates.Except(fullResult, EqualityOperators.TypedElementEqualityComparer);

                    newNodes.AddRange(uniqeNewNodes);
                }

                fullResult.AddRange(newNodes);
            }
            return fullResult;
        }

        private static IEnumerable<ITypedElement> runAll(Closure ctx, IEnumerable<Invokee> arguments)
        {
            var focus = arguments.First()(ctx, InvokeeFactory.EmptyArgs);
            var lambda = arguments.Skip(1).First();
            var index = 0;

            foreach (ITypedElement element in focus)
            {
                var newFocus = ElementNode.CreateList(element);
                var newContext = ctx.Nest(newFocus);
                newContext.SetThis(newFocus);
                newContext.SetIndex(ElementNode.CreateList(index));
                index++;

                var result = lambda(newContext, InvokeeFactory.EmptyArgs).BooleanEval();
                if (result == null) return ElementNode.EmptyList;
                if (result == false) return ElementNode.CreateList(false);
            }

            return ElementNode.CreateList(true);
        }

        private static IEnumerable<ITypedElement> runAny(Closure ctx, IEnumerable<Invokee> arguments)
        {
            var focus = arguments.First()(ctx, InvokeeFactory.EmptyArgs);
            var lambda = arguments.Skip(1).First();
            var index = 0;

            foreach (ITypedElement element in focus)
            {
                var newFocus = ElementNode.CreateList(element);
                var newContext = ctx.Nest(newFocus);
                newContext.SetThis(newFocus);
                newContext.SetIndex(ElementNode.CreateList(index));
                index++;

                var result = lambda(newContext, InvokeeFactory.EmptyArgs).BooleanEval();

                //if (result == null) return ElementNode.EmptyList; -> otherwise this would not be where().exists()
                //Patient.identifier.any(use = 'official') would return {} if ANY identifier has no 'use' element. Unexpected behaviour, I think
                if (result == true) return ElementNode.CreateList(true);
            }

            return ElementNode.CreateList(false);
        }
    }
}
