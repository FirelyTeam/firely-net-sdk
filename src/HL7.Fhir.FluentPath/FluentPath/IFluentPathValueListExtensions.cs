/* 
 * Copyright (c) 2015, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */


using System;
using System.Collections.Generic;
using System.Linq;

namespace Hl7.Fhir.FluentPath
{
    public static class IFluentPathValueListExtensions
    {

        public static IEnumerable<IFluentPathElement> JustElements(this IEnumerable<IFluentPathValue> focus)
        {
            return focus.OfType<IFluentPathElement>();
        }

        public static IEnumerable<IFluentPathValue> JustValues(this IEnumerable<IFluentPathValue> focus)
        {
            return focus.Where(f => f.Value != null);
        }
   
        public static object SingleValue(this IEnumerable<IFluentPathValue> focus)
        {
            return focus.JustValues().Single().Value;
        }

        public static long AsInteger(this IEnumerable<IFluentPathValue> focus)
        {
            return focus.Single().AsInteger();
        }

        public static decimal AsDecimal(this IEnumerable<IFluentPathValue> focus)
        {
            return focus.Single().AsDecimal();
        }

        public static bool AsBoolean(this IEnumerable<IFluentPathValue> focus)
        {
            return focus.Single().AsBoolean();
        }

        public static string AsString(this IEnumerable<IFluentPathValue> focus)
        {
            return focus.Single().AsString();
        }

        public static PartialDateTime AsDateTime(this IEnumerable<IFluentPathValue> focus)
        {
            return focus.Single().AsDateTime();
        }

        private static bool booleanEval(this IEnumerable<IFluentPathValue> focus)
        {
            var result = false;

            // An empty result is considered "false"
            if (!focus.Any())
                result = false;

            // A single result that's a boolean should be interpreted as a boolean
            else if (focus.JustValues().Count() == 1 && focus.JustValues().Single().Value is Boolean)
            {
                return focus.Single().AsBoolean();
            }

            // Otherwise, we have "some" content, which we'll consider "true"
            else
                result = true;

            return result;
        }

        public static IEnumerable<IFluentPathValue> BooleanEval(this IEnumerable<IFluentPathValue> focus)
        {
            return FhirValueList.Create(focus.booleanEval());
        }
        public static IEnumerable<IFluentPathValue> IsEmpty(this IEnumerable<IFluentPathValue> focus)
        {
            return FhirValueList.Create(!focus.Any());
        }

        public static IEnumerable<IFluentPathValue> Not(this IEnumerable<IFluentPathValue> focus)
        {
            return FhirValueList.Create(!focus.booleanEval());
        }

        public static IEnumerable<IFluentPathValue> Or(this IEnumerable<IFluentPathValue> left, IEnumerable<IFluentPathValue> right)
        {
            return FhirValueList.Create(left.booleanEval() || right.booleanEval());
        }

        public static IEnumerable<IFluentPathValue> And(this IEnumerable<IFluentPathValue> left, IEnumerable<IFluentPathValue> right)
        {
            return FhirValueList.Create(left.booleanEval() && right.booleanEval());
        }

        public static IEnumerable<IFluentPathValue> Xor(this IEnumerable<IFluentPathValue> left, IEnumerable<IFluentPathValue> right)
        {
            return FhirValueList.Create(left.booleanEval() ^ right.booleanEval());
        }

        public static IEnumerable<IFluentPathValue> Implies(this IEnumerable<IFluentPathValue> left, IEnumerable<IFluentPathValue> right)
        {
            return FhirValueList.Create(!left.booleanEval() || right.booleanEval());
        }

        public static IEnumerable<IFluentPathValue> Item(this IEnumerable<IFluentPathValue> focus, int index)
        {
            return focus.Skip(index).Take(1);
        }

        public static IEnumerable<IFluentPathValue> Where(this IEnumerable<IFluentPathValue> focus, 
                        Func<IEnumerable<IFluentPathValue>, IEnumerable<IFluentPathValue>> condition)
        {
            return focus.Where(v => condition(FhirValueList.Create(v)).booleanEval());
        }

        public static IEnumerable<IFluentPathValue> Any(this IEnumerable<IFluentPathValue> focus,
        Func<IEnumerable<IFluentPathValue>, IEnumerable<IFluentPathValue>> condition)
        {
            return FhirValueList.Create(focus.Any(v => condition(FhirValueList.Create(v)).booleanEval()));
        }

        public static IEnumerable<IFluentPathValue> All(this IEnumerable<IFluentPathValue> focus,
                Func<IEnumerable<IFluentPathValue>, IEnumerable<IFluentPathValue>> condition)
        {
            return FhirValueList.Create(focus.All(v => condition(FhirValueList.Create(v)).booleanEval()));
        }


        public static IEnumerable<IFluentPathValue> Select(this IEnumerable<IFluentPathValue> focus,
                Func<IEnumerable<IFluentPathValue>, IEnumerable<IFluentPathValue>> mapper)
        {
            return focus.SelectMany(v => mapper(FhirValueList.Create(v)));
        }


        public static IEnumerable<IFluentPathValue> Distinct(this IEnumerable<IFluentPathValue> focus)
        {
            return focus.Distinct(new FhirPathValueEqualityComparer());
        }

        public static IEnumerable<IFluentPathValue> CountItems(this IEnumerable<IFluentPathValue> focus)
        {
            return FhirValueList.Create(focus.Count());
        }

        public static IEnumerable<IFluentPathValue> IntegerEval(this IEnumerable<IFluentPathValue> focus)
        {
            if (focus.JustValues().Count() == 1)
            {
                var val = focus.Single().Value;
                if(val != null)
                {
                    if (val is long) return FhirValueList.Create((long)val);
                    //if (val is decimal) return (Int64)Math.Round((decimal)val);
                    if (val is string)
                    {
                        long result;
                        if (Int64.TryParse((string)val, out result))
                            return FhirValueList.Create(result);
                    }

                }
            }

            return FhirValueList.Empty();
        }

        public static IEnumerable<IFluentPathValue> Substring(this IEnumerable<IFluentPathValue> focus, long start, long? length)
        {
            if(focus.Count() == 1)
            {
                if (focus.First().Value != null)
                {
                    var str = focus.First().AsStringRepresentation();

                    if (length.HasValue)
                        return FhirValueList.Create(str.Substring((int)start, (int)length.Value));
                    else
                        return FhirValueList.Create(str.Substring((int)start));
                }
            }

            return FhirValueList.Empty();
        }

        public static IEnumerable<IFluentPathValue> StartingWith(this IEnumerable<IFluentPathValue> focus, string prefix)
        {
            return focus.JustValues().Where(value => value.AsStringRepresentation().StartsWith(prefix));
        }

        //public static IEnumerable<IFluentPathElement> Resolve(this IEnumerable<IFluentPathValue> focus, FhirClient client)
        //{
        //    return focus.Resolve(new BaseEvaluationContext(client));
        //}

        //public static IEnumerable<IFluentPathElement> Resolve(this IEnumerable<IFluentPathValue> focus, IEvaluationContext context)
        //{
        //    foreach (var item in focus)
        //    {
        //        string url = null;

        //        // Something that looks like a Reference
        //        if (item is IFluentPathElement)
        //        {
        //            var maybeReference = ((IFluentPathElement)item).Children("reference").SingleOrDefault();

        //            if (maybeReference != null && maybeReference.Value != null && maybeReference.Value is string)
        //                url = maybeReference.AsString();
        //        }

        //        // A string as a direct url
        //        if (item.Value != null && item.Value is string)
        //            url = item.AsString();

        //        if(url != null)
        //            yield return context.ResolveResource(url);
        //    }
        //}

        public static IEnumerable<IFluentPathValue> MaxLength(this IEnumerable<IFluentPathValue> focus)
        {
            return FhirValueList.Create( focus.JustValues()
                .Aggregate(0, (val, item) => Math.Max(item.AsStringRepresentation().Length, val)) );
        }

        public static IEnumerable<IFluentPathValue> SubsetOf(this IEnumerable<IFluentPathValue> left, IEnumerable<IFluentPathValue> right)
        {
            return left.All(l => right.Any(r => l.IsEqualTo(r)));
        }


        public static IEnumerable<IFluentPathValue> Extension(this IEnumerable<IFluentPathValue> focus, string url)
        {            
            return focus.Children("extension").Where(es => es.Children("url").IsEqualTo(url));
        }

        public static IEnumerable<IFluentPathElement> Children(this IEnumerable<IFluentPathValue> focus, string name)
        {
            return focus.JustElements().SelectMany(node => node.Children(name));
        }

        //public static IEnumerable<IFhirPathElement> Child(this IEnumerable<IFhirPathValue> focus, string name)
        //{
        //    return focus.JustFhirPathElements().Child(name);
        //}

        //public static IEnumerable<IFhirPathElement> Child(this IEnumerable<IFhirPathElement> focus, string name)
        //{
        //    return focus.SelectMany(node => node.Children().Where(child => child.IsMatch(name)));
        //}


        public static IEnumerable<IFluentPathElement> Children(this IFluentPathValue focus)
        {
            if (focus is IFluentPathElement)
            {
                return ((IFluentPathElement)focus).Children();
            }

            return Enumerable.Empty<IFluentPathElement>();
        }

        public static IEnumerable<IFluentPathElement> Children(this IEnumerable<IFluentPathValue> focus)
        {
            return focus.JustElements().SelectMany(node => node.Children());
        }

        public static IEnumerable<IFluentPathElement> Descendants(this IEnumerable<IFluentPathElement> focus)
        {
            return focus.SelectMany(node => node.Descendants());
        }

        public static IEnumerable<IFluentPathValue> IsEqualTo(this IEnumerable<IFluentPathValue> left, IEnumerable<IFluentPathValue> right)
        {
            if (!left.Any() && !right.Any()) return FhirValueList.Create(true);
            if (left.Count() != right.Count()) return FhirValueList.Create(false);

            return FhirValueList.Create(left.Zip(right, (l, r) => l.IsEqualTo(r)).All(x => x));
        }

        public static IEnumerable<IFluentPathValue> IsEqualTo(this IEnumerable<IFluentPathValue> left, object value)
        {
            var result = left.SingleOrDefault(v => Object.Equals(v.Value,value)) != null;
            return FhirValueList.Create(result);
        }

        public static IEnumerable<IFluentPathValue> IsEquivalentTo(this IEnumerable<IFluentPathValue> left, IEnumerable<IFluentPathValue> right)
        {
            if (!left.Any() && !right.Any()) return FhirValueList.Create(true);
            if (left.Count() != right.Count()) return FhirValueList.Create(false);

            return FhirValueList.Create(left.All((IFluentPathValue l) => right.Any(r => l.IsEquivalentTo(r))));
        }


        public static IEnumerable<IFluentPathValue> GreaterThan(this IEnumerable<IFluentPathValue> left, IEnumerable<IFluentPathValue> right)
        {
            if (left.Count() == 1 && right.Count() == 1)
                yield return left.Single().GreaterThan(right.Single());
        }

        public static IEnumerable<IFluentPathValue> GreaterOrEqual(this IEnumerable<IFluentPathValue> left, IEnumerable<IFluentPathValue> right)
        {
            if (left.Count() == 1 && right.Count() == 1)
                yield return left.Single().GreaterOrEqual(right.Single());
        }

        public static IEnumerable<IFluentPathValue> LessThan(this IEnumerable<IFluentPathValue> left, IEnumerable<IFluentPathValue> right)
        {
            if (left.Count() == 1 && right.Count() == 1)
                yield return left.Single().LessThan(right.Single());
        }

        public static IEnumerable<IFluentPathValue> LessOrEqual(this IEnumerable<IFluentPathValue> left, IEnumerable<IFluentPathValue> right)
        {
            if (left.Count() == 1 && right.Count() == 1)
                yield return left.Single().LessOrEqual(right.Single());
        }

        public static IEnumerable<IFluentPathValue> Add(this IEnumerable<IFluentPathValue> left, IEnumerable<IFluentPathValue> right)
        {
            if (left.Count() == 1 && right.Count() == 1)
                yield return left.Single().Add(right.Single());
        }

        public static IEnumerable<IFluentPathValue> Sub(this IEnumerable<IFluentPathValue> left, IEnumerable<IFluentPathValue> right)
        {
            if (left.Count() == 1 && right.Count() == 1)
                yield return left.Single().Sub(right.Single());
        }

        public static IEnumerable<IFluentPathValue> Mul(this IEnumerable<IFluentPathValue> left, IEnumerable<IFluentPathValue> right)
        {
            if (left.Count() == 1 && right.Count() == 1)
                yield return left.Single().Mul(right.Single());
        }

        public static IEnumerable<IFluentPathValue> Div(this IEnumerable<IFluentPathValue> left, IEnumerable<IFluentPathValue> right)
        {
            if (left.Count() == 1 && right.Count() == 1)
                yield return left.Single().Div(right.Single());
        }

        //public static IEnumerable<IFhirPathValue> Union(this IEnumerable<IFhirPathValue> left, IEnumerable<IFhirPathValue> right)
        //{
        //    return left.Union(right);
        //}

        ///// <summary>Enumerate the descendants of the specified nodes.</summary>
        ///// <typeparam name="T">The type of a tree node.</typeparam>
        ///// <param name="nodeSet">A set of tree nodes.</param>
        ///// <returns>An enumerator for nodes of type <typeparamref name="T"/>.</returns>
        //public static IEnumerable<T> Descendants<T>(this IEnumerable<T> nodeSet) where T : FhirInstanceTree
        //{
        //    return nodeSet.SelectMany(node => node.Descendants());
        //}
        ///// <summary>Enumerate the specified nodes and their descendants.</summary>
        ///// <typeparam name="T">The type of a tree node.</typeparam>
        ///// <param name="nodeSet">A set of tree nodes.</param>
        ///// <returns>An enumerator for nodes of type <typeparamref name="T"/>.</returns>
        //public static IEnumerable<T> DescendantsAndSelf<T>(this IEnumerable<T> nodeSet) where T : FhirInstanceTree
        //{
        //    return nodeSet.SelectMany(node => node.DescendantsAndSelf());
        //}

        ///// <summary>Enumerate the ancestors of the specified nodes.</summary>
        ///// <typeparam name="T">The type of a tree node.</typeparam>
        ///// <param name="nodeSet">A set of tree nodes.</param>
        ///// <returns>An enumerator for nodes of type <typeparamref name="T"/>.</returns>
        //public static IEnumerable<T> Ancestors<T>(this IEnumerable<T> nodeSet) where T : FhirInstanceTree
        //{
        //    return nodeSet.SelectMany(node => node.Ancestors());
        //}

        ///// <summary>Enumerate the specified nodes and their ancestors.</summary>
        ///// <typeparam name="T">The type of a tree node.</typeparam>
        ///// <param name="nodeSet">A set of tree nodes.</param>
        ///// <returns>An enumerator for nodes of type <typeparamref name="T"/>.</returns>
        //public static IEnumerable<T> AncestorsAndSelf<T>(this IEnumerable<T> nodeSet) where T : FhirInstanceTree
        //{
        //    return nodeSet.SelectMany(node => node.AncestorsAndSelf());
        //}

        ///// <summary>Enumerate the siblings preceding the specified nodes.</summary>
        ///// <typeparam name="T">The type of a tree node.</typeparam>
        ///// <param name="nodeSet">A set of tree nodes.</param>
        ///// <returns>An enumerator for nodes of type <typeparamref name="T"/>.</returns>
        //public static IEnumerable<T> PrecedingSiblings<T>(this IEnumerable<T> nodeSet) where T : FhirInstanceTree
        //{
        //    return nodeSet.SelectMany(node => node.PrecedingSiblings());
        //}

        ///// <summary>Enumerate the siblings following the specified nodes.</summary>
        ///// <typeparam name="T">The type of a tree node.</typeparam>
        ///// <param name="nodeSet">A set of tree nodes.</param>
        ///// <returns>An enumerator for nodes of type <typeparamref name="T"/>.</returns>
        //public static IEnumerable<T> FollowingSiblings<T>(this IEnumerable<T> nodeSet) where T : FhirInstanceTree
        //{
        //    return nodeSet.SelectMany(node => node.PrecedingSiblings());
        //}

        class FhirPathValueEqualityComparer : IEqualityComparer<IFluentPathValue>
        {
            public bool Equals(IFluentPathValue x, IFluentPathValue y)
            {
                return x.IsEqualTo(y);
            }

            public int GetHashCode(IFluentPathValue value)
            {
                var result = value.Value != null ? value.Value.GetHashCode() : 0;

                if (value is IFluentPathElement)
                {
                    result ^= (((IFluentPathElement)value).GetChildNames().SingleOrDefault() ?? "key").GetHashCode();
                }

                return result;
            }
        }
    }
}


