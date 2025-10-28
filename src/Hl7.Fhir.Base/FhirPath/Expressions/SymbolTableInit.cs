/*
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model;
using Hl7.Fhir.Utility;
using Hl7.FhirPath.FhirPath.Functions;
using Hl7.FhirPath.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using P = Hl7.Fhir.ElementModel.Types;
using FocusCollection = System.Collections.Generic.IEnumerable<Hl7.Fhir.ElementModel.ITypedElement>;

namespace Hl7.FhirPath.Expressions;

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
        t.Add("trace", (IEnumerable<PocoNode> f, string name, EvaluationContext ctx)
            => f.Trace(name, ctx));

        t.Add("allTrue", (IEnumerable<PocoNode> f) => f.All(e => e.GetValue() is true));
        t.Add("anyTrue", (IEnumerable<PocoNode> f) => f.Any(e => e.GetValue() is true));
        t.Add("allFalse", (IEnumerable<PocoNode> f) => f.All(e => e.GetValue() is false));
        t.Add("anyFalse", (IEnumerable<PocoNode> f) => f.Any(e => e.GetValue() is false));
        t.Add("combine", (IEnumerable<PocoNode> l, IEnumerable<PocoNode> r) => l.Concat(r));
        t.Add("binary.|", (object _, IEnumerable<PocoNode> l, IEnumerable<PocoNode> r) => l.DistinctUnion(r));
        t.Add("union", (IEnumerable<PocoNode> l, IEnumerable<PocoNode> r) => l.DistinctUnion(r));
        t.Add("binary.contains", (object _, IEnumerable<PocoNode> a, PocoNode b) => a.Contains(b));
        t.Add("binary.in", (object _, PocoNode a, IEnumerable<PocoNode> b) => b.Contains(a));
        t.Add("distinct", (IEnumerable<PocoNode> f) => f.Distinct());
        t.Add("isDistinct", (IEnumerable<PocoNode> f) => f.IsDistinct());
        t.Add("subsetOf", (IEnumerable<PocoNode> f, IEnumerable<PocoNode> a) => f.SubsetOf(a));
        t.Add("supersetOf", (IEnumerable<PocoNode> f, IEnumerable<PocoNode> a) => a.SubsetOf(f));
        t.Add("intersect", (IEnumerable<PocoNode> f, IEnumerable<PocoNode> a) => f.Intersect(a));
        t.Add("exclude", (IEnumerable<PocoNode> f, IEnumerable<PocoNode> a) => f.Exclude(a));

        t.Add("today", (object _) => P.Date.Today());
        t.Add("now", (object _) => P.DateTime.Now());
        t.Add("timeOfDay", (object _) => P.Time.Now());

        t.Add("binary.&", (object _, string a, string b) => (a ?? "") + (b ?? ""));

        t.Add(new CallSignature("iif", typeof(IEnumerable<PocoNode>), typeof(object), typeof(bool?), typeof(Invokee), typeof(Invokee)), runIif);
        t.Add(new CallSignature("iif", typeof(IEnumerable<PocoNode>), typeof(object), typeof(bool?), typeof(Invokee)), runIif);

        // Functions that use normal null propagation and work with the focus (buy may ignore it)
        t.Add("not", (IEnumerable<PocoNode> f) => f.Not(), doNullProp: true);
        // t.Add("builtin.children", (IEnumerable<PocoNode> f, string a) => f.Navigate(a), doNullProp: true);
        t.AddBuiltinChildren();

        t.Add("children", (IEnumerable<PocoNode> f) => f.SelectMany(node => node.Children().SelectMany(n => n)), doNullProp: true);
        t.Add("descendants", (IEnumerable<PocoNode> f) => f.Descendants(), doNullProp: true);

        t.Add("binary.=", (object f, IEnumerable<PocoNode> a, IEnumerable<PocoNode> b) => a.IsEqualTo(b), doNullProp: true);
        t.Add("binary.!=", (object f, IEnumerable<PocoNode> a, IEnumerable<PocoNode> b) => !a.IsEqualTo(b), doNullProp: true);
        t.Add("binary.~", (object f, IEnumerable<PocoNode> a, IEnumerable<PocoNode> b) => a.IsEquivalentTo(b), doNullProp: false);
        t.Add("binary.!~", (object f, IEnumerable<PocoNode> a, IEnumerable<PocoNode> b) => !a.IsEquivalentTo(b), doNullProp: false);

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
        // t.Add("binary.*", (object f, P.Quantity a, P.Quantity b) => a * b, doNullProp: true);

        t.Add("binary./", (object f, decimal a, decimal b) => b != 0 ? a / b : (decimal?)null, doNullProp: true);
        // t.Add("binary./", (object f, P.Quantity a, P.Quantity b) => a / b, doNullProp: true);

        t.Add("binary.+", (object f, int a, int b) => a + b, doNullProp: true);
        t.Add("binary.+", (object f, long a, long b) => a + b, doNullProp: true);
        t.Add("binary.+", (object f, decimal a, decimal b) => a + b, doNullProp: true);
        t.Add("binary.+", (object f, string a, string b) => a + b, doNullProp: true);
        t.Add("binary.+", (object f, P.DateTime a, P.Quantity b) => a + b, doNullProp: true);
        t.Add("binary.+", (object f, P.Date a, P.Quantity b) => a + b, doNullProp: true);
        // t.Add("binary.+", (object f, P.Quantity a, P.Quantity b) => a + b, doNullProp: true);

        t.Add("binary.-", (object f, int a, int b) => a - b, doNullProp: true);
        t.Add("binary.-", (object f, long a, long b) => a - b, doNullProp: true);
        t.Add("binary.-", (object f, decimal a, decimal b) => a - b, doNullProp: true);
        t.Add("binary.-", (object f, P.DateTime a, P.Quantity b) => a - b, doNullProp: true);
        t.Add("binary.-", (object f, P.Date a, P.Quantity b) => a - b, doNullProp: true);
        // t.Add("binary.-", (object f, P.Quantity a, P.Quantity b) => a - b, doNullProp: true);

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

        t.Add("single", (IEnumerable<PocoNode> f) => f.Single(), doNullProp: true);
        t.Add("skip", (IEnumerable<PocoNode> f, long a) => f.Skip((int)a), doNullProp: true);
        t.Add("first", (IEnumerable<PocoNode> f) => f.First(), doNullProp: true);
        t.Add("last", (IEnumerable<PocoNode> f) => f.Last(), doNullProp: true);
        t.Add("tail", (IEnumerable<PocoNode> f) => f.Tail(), doNullProp: true);
        t.Add("take", (IEnumerable<PocoNode> f, long a) => f.Take((int)a), doNullProp: true);
        t.Add("builtin.item", (IEnumerable<PocoNode> f, long a) => f.Item((int)a), doNullProp: true);

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
        t.Add("join", (IEnumerable<PocoNode> f, string separator) => f.FpJoin(separator), doNullProp: true);
        t.Add("join", (IEnumerable<PocoNode> f) => f.FpJoin(), doNullProp: true);
        t.Add("indexOf", (IEnumerable<PocoNode> f, PocoNode elem, int start) => f.IndexOf(elem, start), doNullProp: true);
        t.Add("indexOf", (IEnumerable<PocoNode> f, PocoNode elem) => f.IndexOf(elem), doNullProp: true);
        t.Add("lastIndexOf", (IEnumerable<PocoNode> f, PocoNode elem, int start) => f.LastIndexOf(elem, start), doNullProp: true);
        t.Add("lastIndexOf", (IEnumerable<PocoNode> f, PocoNode elem) => f.LastIndexOf(elem), doNullProp: true);

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
        t.Add("is", (PocoNode f, string name) => f.Is(name), doNullProp: true);
        t.Add("as", (IEnumerable<PocoNode> f, string name) => f.FilterType(name), doNullProp: true);

        t.Add("ofType", (IEnumerable<PocoNode> f, string name) => f.FilterType(name), doNullProp: true);
        t.Add("binary.is", (object f, PocoNode left, string name) => left.Is(name), doNullProp: true);
        t.Add("binary.as", (object f, IEnumerable<PocoNode> left, string name) => left.FilterType(name), doNullProp: true);

        // Kept for backwards compatibility, but no longer part of the spec
        t.Add("binary.as", (object f, IEnumerable<PocoNode> left, string name) => left.FilterType(name), doNullProp: true);

        t.Add("extension", (IEnumerable<PocoNode> f, string url) => f.Extension(url), doNullProp: true);

        // Logic operators do not use null propagation and may do short-cut eval
        t.AddLogic("binary.and", (a, b) => a.And(b));
        t.AddLogic("binary.or", (a, b) => a.Or(b));
        t.AddLogic("binary.xor", (a, b) => a.XOr(b));
        t.AddLogic("binary.implies", (a, b) => a.Implies(b));

        // Special late-bound functions
        t.Add(new CallSignature("where", typeof(IEnumerable<PocoNode>), typeof(object), typeof(Invokee)), runWhere);
        t.Add(new CallSignature("select", typeof(IEnumerable<PocoNode>), typeof(object), typeof(Invokee)), runSelect);
        t.Add(new CallSignature("all", typeof(bool), typeof(object), typeof(Invokee)), runAll);
        t.Add(new CallSignature("any", typeof(bool), typeof(object), typeof(Invokee)), runAny);
        t.Add(new CallSignature("exists", typeof(bool), typeof(object), typeof(Invokee)), runAny);
        t.Add(new CallSignature("repeat", typeof(IEnumerable<PocoNode>), typeof(object), typeof(Invokee)), runRepeat);
        t.Add(new CallSignature("trace", typeof(IEnumerable<PocoNode>), typeof(string), typeof(object), typeof(Invokee)), Trace);
        t.Add(new CallSignature("defineVariable", typeof(IEnumerable<PocoNode>), typeof(object), typeof(string)), DefineVariable);
        t.Add(new CallSignature("defineVariable", typeof(IEnumerable<PocoNode>), typeof(object), typeof(string), typeof(Invokee)), DefineVariable);

        // Co-alesce and sort have variable number of arguments.
        t.Add(new UnknownArgCountCallSignature("coalesce", typeof(IEnumerable<PocoNode>)), runCoalesce);
        t.Add(new UnknownArgCountCallSignature("sort", typeof(IEnumerable<PocoNode>)), runSort);
        // these unary operators just inject an ordering node that includes which direction the sort if processing
        t.Add("unary.asc", (object f, PocoNode a) => OrderedNode.FromPrimitiveNode(a), doNullProp: true);
        t.Add("unary.desc", (object f, PocoNode a) => OrderedNode.FromPrimitiveNode(a, true), doNullProp: true);

        t.Add(new CallSignature("aggregate", typeof(IEnumerable<PocoNode>), typeof(Invokee), typeof(Invokee)), runAggregate);
        t.Add(new CallSignature("aggregate", typeof(IEnumerable<PocoNode>), typeof(Invokee), typeof(Invokee), typeof(Invokee)), runAggregate);

        t.AddVar("sct", "http://snomed.info/sct");
        t.AddVar("loinc", "http://loinc.org");
        t.AddVar("ucum", "http://unitsofmeasure.org");

        t.Add("builtin.coreexturl", (object f, string id) => getCoreExtensionUrl(id));
        t.Add("builtin.corevsurl", (object f, string id) => getCoreValueSetUrl(id));

        return t;
    }

    /// <summary>
    /// With the regular Add extension methods, a Wrap is added to each argument to turn it into IEnumerable&lt;PocoNode&gt;.
    /// For 'builtin.children' we know that the focus and the result are already of the correct type,
    /// so we created an optimized implementation avoiding the Wrap.
    /// </summary>
    /// <param name="table"></param>
    internal static void AddBuiltinChildren(this SymbolTable table)
    {
        table.Add(new CallSignature("builtin.children",
            typeof(IEnumerable<PocoNode>),
            typeof(IEnumerable<PocoNode>),
            typeof(string)), (
            ctx, invokees) =>
        {
            var iks = invokees.ToArray();
            var focus = iks[0].Invoke(ctx, InvokeeFactory.EmptyArgs);
            ctx.focus = focus;
            var name = (string)iks[1].Invoke(ctx, InvokeeFactory.EmptyArgs).First().GetValue();
            var result= focus.Navigate(name);

            return result;
        });
    }

    private static string getCoreExtensionUrl(string id)
    {
        return "http://hl7.org/fhir/StructureDefinition/" + id;
    }

    private static string getCoreValueSetUrl(string id)
    {
        return "http://hl7.org/fhir/ValueSet/" + id;
    }

    private static IEnumerable<PocoNode> runSort(Closure ctx, IEnumerable<Invokee> arguments)
    {
        var focus = arguments.First()(ctx, InvokeeFactory.EmptyArgs);
        var lambda = arguments.Skip(1);
        if (!lambda.Any())
        {
            // Just sort using the native element comparer
            // System.Linq.Enumerable.Order;
            return CachedEnumerable.Create(focus.OrderBy(item => item, EqualityOperators.TypedElementComparer));
        }

        var keySelector = lambda.First();
        IOrderedEnumerable<PocoNode> orderedResult = focus.OrderBy(item => readElement(ctx, item, keySelector).FirstOrDefault(), EqualityOperators.TypedElementComparer);
        lambda = lambda.Skip(1);
        while (lambda.Any())
        {
            keySelector = lambda.First();
            orderedResult = orderedResult.ThenBy(item => readElement(ctx, item, keySelector).FirstOrDefault(), EqualityOperators.TypedElementComparer);

            // move onto the next item
            lambda = lambda.Skip(1);
        }

        return orderedResult.ToList();
    }

    private static IEnumerable<PocoNode> readElement(Closure ctx, PocoNode element, Invokee selectProp)
    {
        var newContext = ctx.Nest(element);
        newContext.SetThis(element);
        var result = selectProp(newContext, InvokeeFactory.EmptyArgs);
        foreach (var resultElement in result)       // implement SelectMany()
            yield return resultElement;
    }

    private static IEnumerable<PocoNode> runCoalesce(Closure ctx, IEnumerable<Invokee> arguments)
    {
        var focus = arguments.First()(ctx, InvokeeFactory.EmptyArgs);
        var lambda = arguments.Skip(1);

        while (lambda.Any())
        {
            var keySelector = lambda.First();
            var results = keySelector(ctx, InvokeeFactory.EmptyArgs);
            if (results.Any())
                return results;

            // move onto the next item
            lambda = lambda.Skip(1);
        }

        return [];
    }
    private static IEnumerable<PocoNode> runAggregate(Closure ctx, IEnumerable<Invokee> arguments)
    {
        var focus = arguments.First()(ctx, InvokeeFactory.EmptyArgs);
        ctx.focus = focus;
        var incrExpre = arguments.Skip(1).First();
        IEnumerable<PocoNode> initialValue = [];
        if (arguments.Count() > 2)
        {
            var initialValueExpr = arguments.Skip(2).First();
            initialValue = initialValueExpr(ctx, InvokeeFactory.EmptyArgs);
        }

        var totalContext = ctx.Nest();
        totalContext.SetTotal(initialValue);

        foreach (PocoNode element in focus)
        {
            IEnumerable<PocoNode> newFocus = element;
            var newContext = totalContext.Nest(newFocus);
            newContext.focus = newFocus;
            newContext.SetThis(newFocus);
            newContext.SetTotal(totalContext.GetTotal());
            var newTotalResult = incrExpre(newContext, InvokeeFactory.EmptyArgs);
            totalContext.SetTotal(newTotalResult);
        }

        return totalContext.GetTotal();
    }

    private static IEnumerable<PocoNode> Trace(Closure ctx, IEnumerable<Invokee> arguments)
    {
        var focus = arguments.First()(ctx, InvokeeFactory.EmptyArgs);
        ctx.focus = focus;
        var name = arguments.Skip(1).First()(ctx, InvokeeFactory.EmptyArgs).FirstOrDefault()?.GetValue() as string;

        List<Invokee> selectArgs = [arguments.First(), .. arguments.Skip(2)];
        var selectResults = runSelect(ctx, selectArgs);
        ctx.EvaluationContext?.Tracer?.Invoke(name, selectResults);

        return focus;
    }
        
    private static IEnumerable<PocoNode> DefineVariable(Closure ctx, IEnumerable<Invokee> arguments)
    {
        Invokee[] enumerable = arguments as Invokee[] ?? arguments.ToArray();
        var focus = enumerable[0](ctx, InvokeeFactory.EmptyArgs);
        ctx.focus = focus;
        var name = enumerable[1](ctx, InvokeeFactory.EmptyArgs).FirstOrDefault()?.GetValue() as string;

        if(ctx.ResolveValue(name) is not null) throw new InvalidOperationException($"Variable {name} is already defined in this scope");
            
        if (enumerable.Length == 2)
        {
            ctx.SetValue(name, focus);
        }
        else
        {
            var newContext = ctx.Nest(focus);
            newContext.focus = focus;
            newContext.SetThis(focus);
            var result = enumerable[2](newContext, InvokeeFactory.EmptyArgs);
            ctx.SetValue(name, result);
        }

        return focus;
    }

    private static IEnumerable<PocoNode> runIif(Closure ctx, IEnumerable<Invokee> arguments)
    {
        // iif(criterion: expression, true-result: collection [, otherwise-result: collection]) : collection
        // note: short-circuit behavior is expected in this function
        var focus = arguments.First()(ctx, InvokeeFactory.EmptyArgs);
        ctx.focus = focus;

        var newContext = ctx.Nest(focus);
        newContext.focus = focus;
        newContext.SetThis(focus);
            
        var expression = arguments.Skip(1).First()(newContext, InvokeeFactory.EmptyArgs);
        var trueResult = arguments.Skip(2).First();
        var otherResult = arguments.Skip(3).FirstOrDefault();

        if (expression.Count() > 1)
            throw Error.InvalidOperation($"Result of {nameof(expression)} is not of type boolean");

        return (expression.BooleanEval() ?? false)
            ? trueResult(newContext, InvokeeFactory.EmptyArgs) // share focus with this function
            : otherResult == null ? [] : otherResult(newContext, InvokeeFactory.EmptyArgs);
    }

    private static IEnumerable<PocoNode> runWhere(Closure ctx, IEnumerable<Invokee> arguments)
    {
        var focus = arguments.First()(ctx, InvokeeFactory.EmptyArgs);
        ctx.focus = focus;
        var lambda = arguments.Skip(1).First();

        return CachedEnumerable.Create(runForeach());

        IEnumerable<PocoNode> runForeach()
        {
            var index = 0;

            foreach (PocoNode element in focus)
            {
                PocoNode[] newFocus = [element];
                var newContext = ctx.Nest(newFocus);
                newContext.focus = newFocus;
                newContext.SetThis(newFocus);
                newContext.SetIndex(PocoNode.ForPrimitive<Integer>(index));
                index++;

                if (lambda(newContext, InvokeeFactory.EmptyArgs).BooleanEval() == true)
                    yield return element;
            }
        }
    }

    private static IEnumerable<PocoNode> runSelect(Closure ctx, IEnumerable<Invokee> arguments)
    {
        var focus = arguments.First()(ctx, InvokeeFactory.EmptyArgs);
        ctx.focus = focus;
        var lambda = arguments.Skip(1).First();

        return CachedEnumerable.Create(runForeach());

        IEnumerable<PocoNode> runForeach()
        {
            var index = 0;

            foreach (PocoNode element in focus)
            {
                IEnumerable<PocoNode> newFocus = [element];
                var newContext = ctx.Nest(newFocus);
                newContext.focus = newFocus;
                newContext.SetThis(newFocus);
                newContext.SetIndex(PocoNode.ForPrimitive<Integer>(index));
                index++;

                var result = lambda(newContext, InvokeeFactory.EmptyArgs);
                foreach (var resultElement in result)       // implement SelectMany()
                    yield return resultElement;
            }
        }
    }
        
    private static IEnumerable<PocoNode> runRepeat(Closure ctx, IEnumerable<Invokee> arguments)
    {
        var newNodes = arguments.First()(ctx, InvokeeFactory.EmptyArgs).ToList();
        ctx.focus = newNodes;
        var lambda = arguments.Skip(1).First();

        var fullResult = new List<PocoNode>();

        while (newNodes.Any())
        {
            var index = 0;
            var current = newNodes;
            newNodes = [];

            foreach (PocoNode element in current)
            {
                IEnumerable<PocoNode> newFocus = [element];
                var newContext = ctx.Nest(newFocus);
                newContext.focus = newFocus;
                newContext.SetThis(newFocus);
                newContext.SetIndex(PocoNode.ForPrimitive<Integer>(index));
                index++;

                var candidates = lambda(newContext, InvokeeFactory.EmptyArgs);
                var uniqueNewNodes = candidates.Except<PocoNode>(fullResult, EqualityOperators.TypedElementEqualityComparer);

                newNodes.AddRange(uniqueNewNodes);
            }

            fullResult.AddRange(newNodes);
        }

        return fullResult;
    }

    private static IEnumerable<PocoNode> runAll(Closure ctx, IEnumerable<Invokee> arguments)
    {
        var focus = arguments.First()(ctx, InvokeeFactory.EmptyArgs);
        ctx.focus = focus;
        var lambda = arguments.Skip(1).First();
        var index = 0;

        foreach (PocoNode element in focus)
        {
            IEnumerable<PocoNode> newFocus = [element];
            var newContext = ctx.Nest(newFocus);
            newContext.focus = newFocus;
            newContext.SetThis(newFocus);
            newContext.SetIndex(PocoNode.ForPrimitive<Integer>(index));
            index++;

            var result = lambda(newContext, InvokeeFactory.EmptyArgs).BooleanEval();
            if (result == null) return [];
            if (result == false) return PocoNode.ForPrimitive<FhirBoolean>(false);
        }

        return PocoNode.ForPrimitive<FhirBoolean>(true);
    }

    private static IEnumerable<PocoNode> runAny(Closure ctx, IEnumerable<Invokee> arguments)
    {
        var focus = arguments.First()(ctx, InvokeeFactory.EmptyArgs);
        ctx.focus = focus;
        var lambda = arguments.Skip(1).First();
        var index = 0;

        foreach (PocoNode element in focus)
        {
            IEnumerable<PocoNode> newFocus = [element];
            var newContext = ctx.Nest(newFocus);
            newContext.focus = newFocus;
            newContext.SetThis(newFocus);
            newContext.SetIndex(PocoNode.ForPrimitive<Integer>(index));
            index++;

            var result = lambda(newContext, InvokeeFactory.EmptyArgs).BooleanEval();
            if (result == true) return PocoNode.ForPrimitive<FhirBoolean>(true);
        }
            
        return PocoNode.Root(new FhirBoolean(false));
    }
}