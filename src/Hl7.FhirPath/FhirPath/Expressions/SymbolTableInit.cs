/* 
 * Copyright (c) 2015, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Linq;
using System.Collections.Generic;
using Hl7.FhirPath.Functions;
using System.Text.RegularExpressions;
using Hl7.Fhir.ElementModel;
using Hl7.Fhir.Model.Primitives;

namespace Hl7.FhirPath.Expressions
{
    public static class SymbolTableInit
    {
        public static SymbolTable AddStandardFP(this SymbolTable t)
        {
            // Functions that operate on the focus, without null propagation
            t.Add("empty", (IEnumerable<object> f) => !f.Any());
            t.Add("exists", (IEnumerable<object> f) => f.Any());
            t.Add("count", (IEnumerable<object> f) => f.Count());
            t.Add("trace", (IEnumerable<IElementNavigator> f, string name) => f.Trace(name));

            //   t.Add("binary.|", (object f, IEnumerable<IValueProvider> l, IEnumerable<IValueProvider> r) => l.ConcatUnion(r));
            t.Add("binary.|", (object f, IEnumerable<IElementNavigator> l, IEnumerable<IElementNavigator> r) => l.DistinctUnion(r));
            t.Add("binary.contains", (object f, IEnumerable<IElementNavigator> a, IElementNavigator b) => a.Contains(b));
            t.Add("binary.in", (object f, IElementNavigator a, IEnumerable<IElementNavigator> b) => b.Contains(a));
            t.Add("distinct", (IEnumerable<IElementNavigator> f) => f.Distinct());
            t.Add("isDistinct", (IEnumerable<IElementNavigator> f) => f.IsDistinct());
            t.Add("subsetOf", (IEnumerable<IElementNavigator> f, IEnumerable<IElementNavigator> a) => f.SubsetOf(a));
            t.Add("supersetOf", (IEnumerable<IElementNavigator> f, IEnumerable<IElementNavigator> a) => a.SubsetOf(f));

            t.Add("today", (object f) => PartialDateTime.Today());
            t.Add("now", (object f) => PartialDateTime.Now());

            t.Add("binary.&", (object f, string a, string b) => (a ?? "") + (b ?? ""));

            t.Add("iif", (IEnumerable<IElementNavigator> f, bool? condition, IEnumerable<IElementNavigator> result) => f.IIf(condition, result));
            t.Add("iif", (IEnumerable<IElementNavigator> f, bool? condition, IEnumerable<IElementNavigator> result, IEnumerable<IElementNavigator> otherwise) => f.IIf(condition, result, otherwise));

            // Functions that use normal null propagation and work with the focus (buy may ignore it)
            t.Add("not", (IEnumerable<IElementNavigator> f) => f.Not(), doNullProp:true);
            t.Add("builtin.children", (IEnumerable<IElementNavigator> f, string a) => f.Navigate(a), doNullProp: true);

            t.Add("children", (IEnumerable<IElementNavigator> f) => f.Children(), doNullProp: true);
            t.Add("descendants", (IEnumerable<IElementNavigator> f) => f.Descendants(), doNullProp: true);

            t.Add("binary.=", (object f, IEnumerable<IElementNavigator>  a, IEnumerable<IElementNavigator> b) => a.IsEqualTo(b), doNullProp: true);
            t.Add("binary.!=", (object f, IEnumerable<IElementNavigator> a, IEnumerable<IElementNavigator> b) => !a.IsEqualTo(b), doNullProp: true);
            t.Add("binary.~", (object f, IEnumerable<IElementNavigator> a, IEnumerable<IElementNavigator> b) => a.IsEquivalentTo(b), doNullProp: true);
            t.Add("binary.!~", (object f, IEnumerable<IElementNavigator> a, IEnumerable<IElementNavigator> b) => !a.IsEquivalentTo(b), doNullProp: true);

            t.Add("unary.-", (object f, long a) => -a, doNullProp: true);
            t.Add("unary.-", (object f, decimal a) => -a, doNullProp: true);
            t.Add("unary.+", (object f, long a) => a, doNullProp: true);
            t.Add("unary.+", (object f, decimal a) => a, doNullProp: true);

            t.Add("binary.*", (object f, long a, long b) => a * b, doNullProp: true);
            t.Add("binary.*", (object f, decimal a, decimal b) => a * b, doNullProp: true);

            t.Add("binary./", (object f, decimal a, decimal b) => a / b, doNullProp: true);
            //.Add((object f, decimal a, decimal b) => a / b, doNullProp: true);

            t.Add("binary.+", (object f, long a, long b) => a + b, doNullProp: true);
            t.Add("binary.+", (object f, decimal a, decimal b) => a + b, doNullProp: true);
            t.Add("binary.+", (object f, string a, string b) => a + b, doNullProp: true);

            t.Add("binary.-", (object f, long a, long b) => a - b, doNullProp: true);
            t.Add("binary.-", (object f, decimal a, decimal b) => a - b, doNullProp: true);

            t.Add("binary.div", (object f, long a, long b) => a / b, doNullProp: true);
            t.Add("binary.div", (object f, decimal a, decimal b) => (long)Math.Truncate(a / b), doNullProp: true);

            t.Add("binary.mod", (object f, long a, long b) => a % b, doNullProp: true);
            t.Add("binary.mod", (object f, decimal a, decimal b) => a % b, doNullProp: true);

            t.Add("binary.>", (object f, long a, long b) => a > b, doNullProp: true);
            t.Add("binary.>", (object f, decimal a, decimal b) => a > b, doNullProp: true);
            t.Add("binary.>", (object f, string a, string b) => String.CompareOrdinal(a, b) > 0, doNullProp: true);
            t.Add("binary.>", (object f, PartialDateTime a, PartialDateTime b) => a > b, doNullProp: true);
            t.Add("binary.>", (object f, PartialTime a, PartialTime b) => a > b, doNullProp: true);

            t.Add("binary.<", (object f, long a, long b) => a < b, doNullProp: true);
            t.Add("binary.<", (object f, decimal a, decimal b) => a < b, doNullProp: true);
            t.Add("binary.<", (object f, string a, string b) => String.CompareOrdinal(a, b) < 0, doNullProp: true);
            t.Add("binary.<", (object f, PartialDateTime a, PartialDateTime b) => a < b, doNullProp: true);
            t.Add("binary.<", (object f, PartialTime a, PartialTime b) => a < b, doNullProp: true);

            t.Add("binary.<=", (object f, long a, long b) => a <= b, doNullProp: true);
            t.Add("binary.<=", (object f, decimal a, decimal b) => a <= b, doNullProp: true);
            t.Add("binary.<=", (object f, string a, string b) => String.CompareOrdinal(a, b) <= 0, doNullProp: true);
            t.Add("binary.<=", (object f, PartialDateTime a, PartialDateTime b) => a <= b, doNullProp: true);
            t.Add("binary.<=", (object f, PartialTime a, PartialTime b) => a <= b, doNullProp: true);

            t.Add("binary.>=", (object f, long a, long b) => a >= b, doNullProp: true);
            t.Add("binary.>=", (object f, decimal a, decimal b) => a >= b, doNullProp: true);
            t.Add("binary.>=", (object f, string a, string b) => String.CompareOrdinal(a, b) >= 0, doNullProp: true);
            t.Add("binary.>=", (object f, PartialDateTime a, PartialDateTime b) => a >= b, doNullProp: true);
            t.Add("binary.>=", (object f, PartialTime a, PartialTime b) => a >= b, doNullProp: true);

            t.Add("single", (IEnumerable<IElementNavigator> f) => f.Single(), doNullProp: true);
            t.Add("skip", (IEnumerable<IElementNavigator> f, long a) =>  f.Skip((int)a), doNullProp: true);
            t.Add("first", (IEnumerable<IElementNavigator> f) => f.First(), doNullProp: true);
            t.Add("last", (IEnumerable<IElementNavigator> f) => f.Last(), doNullProp: true);
            t.Add("tail", (IEnumerable<IElementNavigator> f) => f.Tail(), doNullProp: true);
            t.Add("take", (IEnumerable<IElementNavigator> f, long a) => f.Take((int)a), doNullProp: true);
            t.Add("builtin.item", (IEnumerable<IElementNavigator> f, long a) => f.Item((int)a), doNullProp: true);

            t.Add("toInteger", (IElementNavigator f) => f.ToInteger(), doNullProp: true);
            t.Add("toDecimal", (IElementNavigator f) => f.ToDecimal(), doNullProp: true);
            t.Add("toString", (IElementNavigator f) => f.ToStringRepresentation(), doNullProp: true);

            t.Add("substring", (string f, long a) => f.FpSubstring((int)a), doNullProp: true);
            t.Add("substring", (string f, long a, long b) => f.FpSubstring((int)a, (int)b), doNullProp: true);
            t.Add("startsWith", (string f, string fragment) => f.StartsWith(fragment), doNullProp: true);
            t.Add("endsWith", (string f, string fragment) => f.EndsWith(fragment), doNullProp: true);
            t.Add("matches", (string f, string regex) => Regex.IsMatch(f, regex), doNullProp: true);
            t.Add("indexOf", (string f, string fragment) => f.FpIndexOf(fragment), doNullProp: true);
            t.Add("contains", (string f, string fragment) => f.Contains(fragment), doNullProp: true);
            t.Add("replaceMatches", (string f, string regex, string subst) => Regex.Replace(f, regex, subst), doNullProp: true);
            t.Add("replace", (string f, string regex, string subst) => f.FpReplace(regex, subst), doNullProp: true);
            t.Add("length", (string f) => f.Length, doNullProp: true);

            t.Add("is", (IElementNavigator f, string name) => f.Is(name), doNullProp: true);
            t.Add("as", (IEnumerable<IElementNavigator> f, string name) => f.FilterType(name), doNullProp: true);
            t.Add("binary.is", (object f, IElementNavigator left, string name) => left.Is(name), doNullProp: true);
            t.Add("binary.as", (object f, IElementNavigator left, string name) => left.CastAs(name), doNullProp: true);

            t.Add("extension", (IEnumerable<IElementNavigator> f, string url) => f.Extension(url), doNullProp: true);

            // Logic operators do not use null propagation and may do short-cut eval
            t.AddLogic("binary.and", (a, b) => a.And(b));
            t.AddLogic("binary.or", (a, b) => a.Or(b));
            t.AddLogic("binary.xor", (a, b) => a.XOr(b));
            t.AddLogic("binary.implies", (a, b) => a.Implies(b));

            // Special late-bound functions
            t.Add(new CallSignature("where", typeof(IEnumerable<IElementNavigator>), typeof(object), typeof(Invokee)), runWhere);
            t.Add(new CallSignature("select", typeof(IEnumerable<IElementNavigator>), typeof(object), typeof(Invokee)), runSelect);
            t.Add(new CallSignature("all", typeof(bool), typeof(object), typeof(Invokee)), runAll);
            t.Add(new CallSignature("any", typeof(bool), typeof(object), typeof(Invokee)), runAny);
            t.Add(new CallSignature("repeat", typeof(IEnumerable<IElementNavigator>), typeof(object), typeof(Invokee)), runRepeat);
            

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


        private static IEnumerable<IElementNavigator> runWhere(Closure ctx, IEnumerable<Invokee> arguments)
        {
            var focus = arguments.First()(ctx, InvokeeFactory.EmptyArgs);
            var lambda = arguments.Skip(1).First();

            foreach (IElementNavigator element in focus)
            {
                var newFocus = FhirValueList.Create(element);
                var newContext = ctx.Nest(newFocus);
                newContext.SetThis(newFocus);

                if (lambda(newContext, InvokeeFactory.EmptyArgs).BooleanEval() == true)
                    yield return element;
            }
        }

        private static IEnumerable<IElementNavigator> runSelect(Closure ctx, IEnumerable<Invokee> arguments)
        {
            var focus = arguments.First()(ctx, InvokeeFactory.EmptyArgs);
            var lambda = arguments.Skip(1).First();

            foreach (IElementNavigator element in focus)
            {
                var newFocus = FhirValueList.Create(element);
                var newContext = ctx.Nest(newFocus);
                newContext.SetThis(newFocus);

                var result = lambda(newContext, InvokeeFactory.EmptyArgs);
                foreach (var resultElement in result)       // implement SelectMany()
                    yield return resultElement;
            }
        }

        private static IEnumerable<IElementNavigator> runRepeat(Closure ctx, IEnumerable<Invokee> arguments)
        {
            var focus = arguments.First()(ctx, InvokeeFactory.EmptyArgs);
            var lambda = arguments.Skip(1).First();

            var fullResult = new List<IElementNavigator>();
            List<IElementNavigator> newNodes = new List<IElementNavigator>(focus);

            while (newNodes.Any())
            {
                var current = newNodes;
                newNodes = new List<IElementNavigator>();

                foreach (IElementNavigator element in current)
                {
                    var newFocus = FhirValueList.Create(element);
                    var newContext = ctx.Nest(newFocus);
                    newContext.SetThis(newFocus);


                    newNodes.AddRange(lambda(newContext, InvokeeFactory.EmptyArgs));
                }

                fullResult.AddRange(newNodes);
            }

            return fullResult;
        }

        private static IEnumerable<IElementNavigator> runAll(Closure ctx, IEnumerable<Invokee> arguments)
        {
            var focus = arguments.First()(ctx, InvokeeFactory.EmptyArgs);
            var lambda = arguments.Skip(1).First();

            foreach (IElementNavigator element in focus)
            {
                var newFocus = FhirValueList.Create(element);
                var newContext = ctx.Nest(newFocus);
                newContext.SetThis(newFocus);

                var result = lambda(newContext, InvokeeFactory.EmptyArgs).BooleanEval();
                if (result == null) return FhirValueList.Empty;
                if (result == false) return FhirValueList.Create(false);
            }

            return FhirValueList.Create(true);
        }

        private static IEnumerable<IElementNavigator> runAny(Closure ctx, IEnumerable<Invokee> arguments)
        {
            var focus = arguments.First()(ctx, InvokeeFactory.EmptyArgs);
            var lambda = arguments.Skip(1).First();

            foreach (IElementNavigator element in focus)
            {
                var newFocus = FhirValueList.Create(element);
                var newContext = ctx.Nest(newFocus);
                newContext.SetThis(newFocus);


                var result = lambda(newContext, InvokeeFactory.EmptyArgs).BooleanEval();

                //if (result == null) return FhirValueList.Empty; -> otherwise this would not be where().exists()
                //Patient.identifier.any(use = 'official') would return {} if ANY identifier has no 'use' element. Unexpected behaviour, I think
                if (result == true) return FhirValueList.Create(true);
            }

            return FhirValueList.Create(false);
        }
    }
}
