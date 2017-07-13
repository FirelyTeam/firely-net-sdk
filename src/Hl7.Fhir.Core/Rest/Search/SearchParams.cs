/*
  Copyright (c) 2011-2012, HL7, Inc
  All rights reserved.
  
  Redistribution and use in source and binary forms, with or without modification, 
  are permitted provided that the following conditions are met:
  
   * Redistributions of source code must retain the above copyright notice, this 
     list of conditions and the following disclaimer.
   * Redistributions in binary form must reproduce the above copyright notice, 
     this list of conditions and the following disclaimer in the documentation 
     and/or other materials provided with the distribution.
   * Neither the name of HL7 nor the names of its contributors may be used to 
     endorse or promote products derived from this software without specific 
     prior written permission.
  
  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
  IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
  POSSIBILITY OF SUCH DAMAGE.
  

*/



using Hl7.Fhir.Introspection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Utility;

namespace Hl7.Fhir.Rest.Search
{
    public static class All
    {
        public static Type Params => typeof(All);
        public static NumberParameter Count => new NumberParameter().Named("_count");
        public static TokenParameter Query => new TokenParameter().Named("_query");
        public static TokenParameter Kind => new TokenParameter().Named("kind");
        public static SummaryParam Summary => new SummaryParam().Named("_summary");
        public static CompositeParam Demo => new CompositeParam().Named("_demo");
        public static DateTimeParameter Birthdate => new DateTimeParameter().Named("birthdate");
        public static StringParameter Name => new StringParameter().Named("name");
    }

    public interface IBaseParam<P>
    {
        string Name { get; set; }
        string Value { get; set; }
        string Modifier { get; set; }

        P Named(string name);

        P Is(string value);

        P ModifiedBy(string modifier);
    }

    public interface IBaseParam<P,V> : IBaseParam<P>
    {
        P Is(V value);
    }
    public class SearchParam<P> : SearchParam, IBaseParam<P>
    {
        public P Named(string name)
        {
            Name = name;
            return (P)(object)this;
        }

        public P Is(string value)
        {
            Value = value;
            return (P)(object)this;
        }

        public P ModifiedBy(string modifier)
        {
            Modifier = modifier;
            return (P)(object)this;
        }
    }

    public class SearchParam<P,V> : SearchParam<P>, IBaseParam<P,V>
    {
        public P Is(V value)
        {
            Value = value?.Canonical();
            return (P)(object)this;
        }
    }

    public class SearchParam
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public string Modifier { get; set; }

        public string Key => $"{Name}{Modifier?.Prepend(":")}";

        public override string ToString() => Value != null ? $"{Key}={Value}" : Key;
    }



    // Where(SearchParam) + Where(SearchParams) +  SearchParams SearchParam.And(this SearchParam, SearchParam that)
    // Or the same?

    public interface IMultiValued<P> : IBaseParam<P> {  }

    public interface IOrderedValue<P,V> : IBaseParam<P,V> { }

    public interface IOrderedRangeValue<P, V> : IOrderedValue<P, V> { }


    public class StringParameter : SearchParam<StringParameter,string>, IMultiValued<StringParameter>
    {
        public StringParameter Exactly() => this.ModifiedBy("exact");
        public StringParameter Exactly(string value) => this.ModifiedBy("exact").Is(value);
        public StringParameter Contains(string value) => this.ModifiedBy("contains").Is(value);
        public StringParameter AsText() => this.ModifiedBy("text");
    }

    public class UriParameter : SearchParam<UriParameter,string>, IMultiValued<UriParameter>
    {
        public UriParameter Is(Uri value) => this.Is(value?.OriginalString);

        public UriParameter Below(string value) => this.Is(value).ModifiedBy("below");
        public UriParameter Below(Uri value) => this.Is(value).ModifiedBy("below");

    }

    public class DateTimeParameter : SearchParam<DateTimeParameter,string>, IOrderedRangeValue<DateTimeParameter, string>, IMultiValued<DateTimeParameter>
    {
        public DateTimeParameter Is(DateTimeOffset value) => this.Is(value.Canonical());
    }

    public class NumberParameter : SearchParam<NumberParameter,decimal>, IOrderedValue<NumberParameter,decimal>, IMultiValued<NumberParameter> { }

    public class ModifierParameter : SearchParam<ModifierParameter, bool> { }

    public class TokenParameter : SearchParam<TokenParameter,string>, IMultiValued<TokenParameter>
    {
        public string System { get; set; }

        public TokenParameter Is(string system, string value)
        {
            System = system;
            Is($"{System}|{value}");

            return this;
        }

        public TokenParameter Is(bool value) => Is(value.Canonical());

        public TokenParameter Is(Coding value) => Is(value?.System, value?.Code);

        public TokenParameter Is(CodeableConcept value) => Is(value?.Coding?.FirstOrDefault() ?? new Coding());

        public TokenParameter Is(Identifier value) => Is(value?.System, value?.Value);

        public TokenParameter Is(ContactPoint value) => Is(value?.Use?.Canonical(), value?.Value);

        public TokenParameter ByText(CodeableConcept value) => Is(value?.Text).ModifiedBy("text");
        public TokenParameter ByText(Coding value) => Is(value?.Display).ModifiedBy("text");
        public TokenParameter ByText(Identifier value) => Is(value?.Type?.Text).ModifiedBy("text");

        public TokenParameter Below(string system, string value) => Is(system, value).ModifiedBy("below");
        public TokenParameter Below(Coding value) => Is(value).ModifiedBy("below");
        public TokenParameter Below(CodeableConcept value) => Is(value).ModifiedBy("below");

        public TokenParameter Above(string system, string value) => Is(system, value).ModifiedBy("above");
        public TokenParameter Above(Coding value) => Is(value).ModifiedBy("above");
        public TokenParameter Above(CodeableConcept value) => Is(value).ModifiedBy("above");

        public TokenParameter Not() => this.ModifiedBy("not");

        public TokenParameter In(string canonical) => Is(canonical).ModifiedBy("in");
        public TokenParameter NotIn(string canonical) => Is(canonical).ModifiedBy("not-in");
    }

    public class CompositeParam : SearchParam<CompositeParam>
    {
        public CompositeParam Values(params SearchParam[] pars)
        {
            if (pars.Any(p => p.Modifier != null))
                throw new InvalidOperationException("Cannot create a composite parameter with parameters having modifiers");

            // type of parameter should not itself be composite - might solve that using inheritance hierarchy
            // ..or just check whether the Value has $ or , in it, making the result unparseable
            Value = String.Join(",", pars.Select(p => $"{p.Name}${p.Value}"));

            return this;
        }
    }

    public class QuantityParam : SearchParam<QuantityParam>, IMultiValued<QuantityParam>
    {

    }

    public class SummaryParam : SearchParam<SummaryParam,SummaryType> { }

    

    public static class ParamConstructors
    {
        internal static string Canonical(this object value) => PrimitiveTypeConverter.ConvertTo<string>(value);

        public static ModifierParameter Missing<P>(this IBaseParam<P> par, bool value = true) 
            => new ModifierParameter().Named(par.Name).ModifiedBy("missing").Is(value);

        public static P EqualTo<P, V>(this IOrderedValue<P, V> par, V value) where P : SearchParam<P,V>
            => par.Is($"eq{par.Is(value).Value}");
        public static P NotEqualTo<P, V>(this IOrderedValue<P, V> par, V value) where P : SearchParam<P,V>
            => par.Is($"ne{par.Is(value).Value}");
        public static P GreaterThan<P,V>(this IOrderedValue<P,V> par, V value) where P:SearchParam<P,V>
           => par.Is($"gt{par.Is(value).Value}");
        public static P LessThan<P, V>(this IOrderedValue<P, V> par, V value) where P : SearchParam<P,V>
           => par.Is($"lt{par.Is(value).Value}");
        public static P GreaterThanOrEqual<P, V>(this IOrderedValue<P, V> par, V value) where P : SearchParam<P,V>
           => par.Is($"ge{par.Is(value).Value}");
        public static P LessThanOrEqual<P, V>(this IOrderedValue<P, V> par, V value) where P : SearchParam<P,V>
           => par.Is($"le{par.Is(value).Value}");
        public static P IsApproximately<P, V>(this IOrderedValue<P, V> par, V value) where P : SearchParam<P,V>
           => par.Is($"ap{par.Is(value).Value}");
        public static P StartsAfter<P, V>(this IOrderedRangeValue<P, V> par, V value) where P : SearchParam<P,V>
           => par.Is($"sa{par.Is(value).Value}");
        public static P EndsBefore<P, V>(this IOrderedRangeValue<P, V> par, V value) where P : SearchParam<P,V>
           => par.Is($"eb{par.Is(value).Value}");


        public static P Or<P>(this IMultiValued<P> par, IMultiValued<P> other) where P:IBaseParam<P>, new()
        {
            var o = (P)other;
            var p = (P)par;

            // only supported when name of other == my name, translates into name=x,y,z
            if (o.Name == p.Name)
                return new P().Named(o.Name).Is(p.Value + "," + o.Value);
            else
                throw new InvalidOperationException($"{nameof(Or)} can only combine parameters with the same name ({o.Name} != {p.Name})");
        }
    }


    /// <summary>
    /// Contains criteria that can be passed to a search operation or conditional update/delete/create
    /// </summary>
    public class SearchParams
    {
        public SearchParams()
        {
            Include = new List<string>();
            RevInclude = new List<string>();
            Sort = new List<Tuple<string, SortOrder>>();
            Parameters = new List<Tuple<string, string>>();
            Elements = new List<string>();
        }

        /// <summary>
        /// List of all the search parameter that have some special meaning.
        /// Primarily used to filter to the non-special parameters.
        /// Notice that _query, _text, _filter and _content are predefined searches in the standard,
        /// but cannot be parsed as regular criteria. So they are included in the RESERVED_PARAMETERS 
        /// and thus not included in the Parameters property
        /// </summary>
        public static readonly string[] RESERVED_PARAMETERS = new string[] {
            SEARCH_PARAM_QUERY,
            SEARCH_PARAM_TEXT,
            SEARCH_PARAM_CONTENT,
            SEARCH_PARAM_COUNT,
            SEARCH_PARAM_SORT,
            SEARCH_PARAM_FILTER,
            SEARCH_PARAM_INCLUDE,
            SEARCH_PARAM_REVINCLUDE,
            SEARCH_PARAM_SUMMARY,
            SEARCH_PARAM_CONTAINED,
            SEARCH_PARAM_CONTAINEDTYPE,
            SEARCH_PARAM_ELEMENTS
            // lastUpdated, _tag, _profile, _security, _list
            };
     
     
        public const string SEARCH_MODIF_ASCENDING = "asc";
        public const string SEARCH_MODIF_DESCENDING = "desc";

        public const char SEARCH_CHAINSEPARATOR = '.';
        public const char SEARCH_MODIFIERSEPARATOR = ':';

        public const string SEARCH_CONTAINED_TRUE = "true";
        public const string SEARCH_CONTAINED_FALSE = "false";
        public const string SEARCH_CONTAINED_BOTH = "both";

        public const string SEARCH_CONTAINED_TYPE_CONTAINER = "container";
        public const string SEARCH_CONTAINED_TYPE_CONTAINED = "contained";

        public SearchParams Select(params string[] elements)
        {
            Elements.AddRange(elements);
            return this;
        }

        /// <summary>
        /// Add a parameter with a given name and value.
        /// </summary>
        /// <param name="name">The name of the parameter</param>
        /// <param name="value">The value of the parameter as a FHIR datatype or Resource</param>
        /// <returns>this (Parameters), so you can chain AddParameter calls</returns>
        public SearchParams Add(string name, string value)
        {
            if (name == null) throw Error.ArgumentNull(nameof(name));
            if (value == null) throw Error.ArgumentNull(nameof(value));

            if (name == SEARCH_PARAM_QUERY) Query = nonEmptySingleValue(name, Query, value);
            else if (name == SEARCH_PARAM_TEXT) Text = nonEmptySingleValue(name, Text, value);
            else if (name == SEARCH_PARAM_CONTENT) Content = nonEmptySingleValue(name, Content, value);
            else if (name == SEARCH_PARAM_COUNT)
            {
                int count;
                if ( !Int32.TryParse(value, out count) || count <= 0) throw Error.Format("Invalid {0}: '{1}' is not a positive integer".FormatWith(name, value));
                Count = count;
            }
            else if (name == SEARCH_PARAM_INCLUDE) addNonEmpty(name, Include, value);
            else if (name == SEARCH_PARAM_REVINCLUDE) addNonEmpty(name, RevInclude, value);
            else if (name.StartsWith(SEARCH_PARAM_SORT + SEARCH_MODIFIERSEPARATOR))
            {
                var order = name.Substring(SEARCH_PARAM_SORT.Length + 1).ToLower();

                if ( "ascending".StartsWith(order) && order.Length >= 3) addNonEmptySort(value, SortOrder.Ascending);
                else if ( "descending".StartsWith(order) && order.Length >= 4) addNonEmptySort(value, SortOrder.Descending);
                else throw Error.Format("Invalid {0}: '{1}' is not a recognized sort order".FormatWith(SEARCH_PARAM_SORT, order));
            }
            else if (name == SEARCH_PARAM_SORT)
            {
                addNonEmptySort(value, SortOrder.Ascending);
            }
            else if (name == SEARCH_PARAM_SUMMARY)
            {
                SummaryType st = SummaryType.False;
                if (Enum.TryParse(value, ignoreCase: true, result: out st))
                    Summary = st;
                else
                    throw Error.Format("Invalid {0}: '{1}' is not a recognized summary value".FormatWith(name, value));
            }
            else if (name == SEARCH_PARAM_FILTER) Filter = nonEmptySingleValue(name, Filter, value);
            else if (name == SEARCH_PARAM_CONTAINED)
            {
                if (SEARCH_CONTAINED_TRUE.Equals(value)) Contained = ContainedSearch.True;
                else if (SEARCH_CONTAINED_FALSE.Equals(value)) Contained = ContainedSearch.False;
                else if (SEARCH_CONTAINED_BOTH.Equals(value)) Contained = ContainedSearch.Both;
                else throw Error.Format("Invalid {0}: '{1}' is not a recognized contained value".FormatWith(name, value));
            }
            else if (name == SEARCH_PARAM_CONTAINEDTYPE)
            {
                if (SEARCH_CONTAINED_TYPE_CONTAINED.Equals(value)) ContainedType = ContainedResult.Contained;
                else if (SEARCH_CONTAINED_TYPE_CONTAINER.Equals(value)) ContainedType = ContainedResult.Container;
                else throw Error.Format("Invalid {0}: '{1}' is not a recognized containedType value".FormatWith(name, value));
            }
            else if (name== SEARCH_PARAM_ELEMENTS)
            {
                if (String.IsNullOrEmpty(value)) throw Error.Format("Invalid {0} value: it cannot be empty".FormatWith(name));
                Elements.AddRange(value.Split(','));
            }
            else
                Parameters.Add(Tuple.Create(name, value));

            return this;
        }

        private static string nonEmptySingleValue(string paramName, string currentValue, string newValue)
        {
            if (String.IsNullOrEmpty(newValue)) throw Error.Format("Invalid {0} value: it cannot be empty".FormatWith(paramName));
            if (!String.IsNullOrEmpty(currentValue)) throw Error.Format("{0} cannot be specified more than once".FormatWith(paramName));
            return newValue;
        }

        private static void addNonEmpty(string paramName, IList<string> values, string value)
        {
            if (String.IsNullOrEmpty(value)) throw Error.Format("Invalid {0} value: it cannot be empty".FormatWith(paramName));
            values.Add(value);
        }

        private void addNonEmptySort(string value, SortOrder sortOrder)
        {
            if (String.IsNullOrEmpty(value)) throw Error.Format("Invalid {0} value: it cannot be empty".FormatWith(SEARCH_PARAM_SORT));
            Sort.Add(Tuple.Create(value, sortOrder));
        }

        /// <summary>
        /// The 'regular' parameters. The parameters that have no special meaning.
        /// </summary>
        public IList<Tuple<string,string>> Parameters { get; private set; }


        public const string SEARCH_PARAM_QUERY = "_query";

        /// <summary>
        /// Gets or sets the special _query search parameter which asks the server to run a 
        /// specific named query instead of the standard FHIR search.
        /// </summary>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Query { get; set; }

        public const string SEARCH_PARAM_TEXT = "_text";

        /// <summary>
        /// Gets or sets the special _text search parameter which which search on the narrative of the resource. 
        /// </summary>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Text{ get; set; }


        public const string SEARCH_PARAM_CONTENT = "_content";

        /// <summary>
        /// Gets or sets the special _text search parameter which which search on the entire content of the resource.
        /// </summary>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Content{ get; set; }


        public const string SEARCH_PARAM_COUNT = "_count";

        /// <summary>
        /// Gets or sets the special _count search parameter, which limits the number
        /// of mathes returned per page in the pages search result
        /// </summary>
        /// <remark>The number of resources returned from the search may exceed this
        /// parameter, since additional _included resources for the matches are returned
        /// as well</remark>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? Count { get; set; }

        public const string SEARCH_PARAM_SUMMARY = "_summary";

        /// <summary>
        /// Gets or sets the special _summary search parameter. If set to true,
        /// the server will not return all elements in each matching resource, but just
        /// the most important ones.
        /// </summary>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public SummaryType? Summary { get; set; }



        public const string SEARCH_PARAM_FILTER = "_filter";

        /// <summary>
        /// Gets or sets the special _filter search parameter to supply an advanced query expression
        /// </summary>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Filter { get; set; }


        /// <summary>
        /// "_sort"
        /// </summary>
        public const string SEARCH_PARAM_SORT = "_sort";


        /// <summary>
        /// Gets or sets the _sort parameter, to modify the sort order of the search result.
        /// Uses a tuple (name, sortorder).
        /// </summary>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IList<Tuple<string, SortOrder>> Sort { get; private set; }


        public const string SEARCH_PARAM_INCLUDE = "_include";

        /// <summary>
        /// Returns a modifiable collection of _include parameters. These are used to include
        /// resources in the search result that the matched resources refer to.
        /// </summary>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IList<string> Include { get; private set; }


        public const string SEARCH_PARAM_REVINCLUDE = "_revinclude";

        [NotMapped]
        [IgnoreDataMemberAttribute]
        public IList<string> RevInclude { get; private set; }


        public const string SEARCH_PARAM_CONTAINED = "_contained";

        [NotMapped]
        [IgnoreDataMemberAttribute]
        public ContainedSearch? Contained { get; private set; }


        public const string SEARCH_PARAM_CONTAINEDTYPE = "_containedType";

        [NotMapped]
        [IgnoreDataMemberAttribute]
        public ContainedResult? ContainedType { get; private set; }


        public const string SEARCH_PARAM_ELEMENTS = "_elements";

        public List<string> Elements { get; private set;  }



        public static SearchParams FromUriParamList(IEnumerable<Tuple<string,string>> parameters)
        {
            var result = new SearchParams();

            foreach (var parameter in parameters)
                result.Add(parameter.Item1, parameter.Item2);

            return result;
        }



        private string createSortParamName(SortOrder order)
        {
            return SEARCH_PARAM_SORT + SEARCH_MODIFIERSEPARATOR +
                         (order == SortOrder.Ascending ? SEARCH_MODIF_ASCENDING : SEARCH_MODIF_DESCENDING);
        }

        public UriParamList ToUriParamList()
        {
            var result = new UriParamList();

            if (!String.IsNullOrEmpty(Query)) result.Add(Tuple.Create(SEARCH_PARAM_QUERY, Query));
            if (!String.IsNullOrEmpty(Text)) result.Add(Tuple.Create(SEARCH_PARAM_TEXT, Text));
            if (!String.IsNullOrEmpty(Content)) result.Add(Tuple.Create(SEARCH_PARAM_CONTENT, Content));
            if (Count != null) result.Add(Tuple.Create(SEARCH_PARAM_COUNT, Count.Value.ToString()));
            if (Include.Any()) result.AddRange(Include.Select(i => Tuple.Create(SEARCH_PARAM_INCLUDE, i)));
            if (RevInclude.Any()) result.AddRange(RevInclude.Select(i => Tuple.Create(SEARCH_PARAM_REVINCLUDE, i)));
            if (Sort.Any()) result.AddRange(Sort.Select(s => Tuple.Create(createSortParamName(s.Item2), s.Item1)));
            if (Summary != null) result.Add(Tuple.Create(SEARCH_PARAM_SUMMARY, Summary.Value.ToString().ToLower()));
            if (!String.IsNullOrEmpty(Filter)) result.Add(Tuple.Create(SEARCH_PARAM_FILTER, Filter));
            if (Contained != null) result.Add(Tuple.Create(SEARCH_PARAM_CONTAINED, Contained.Value.ToString().ToLower()));
            if (ContainedType != null) result.Add(Tuple.Create(SEARCH_PARAM_CONTAINEDTYPE, ContainedType.Value.ToString().ToLower()));
            if (Elements.Any()) result.Add(Tuple.Create(SEARCH_PARAM_ELEMENTS, String.Join(",",Elements)));

            result.AddRange(Parameters);
            return result;
        }


        public static SearchParams FromParameters(Parameters parameters)
        {
            var result = new SearchParams();

            foreach (var parameter in parameters.Parameter)
            {
                var name = parameter.Name;
                var value = parameter.Value;
                
                if(value != null && value is Primitive)
                {
                    result.Add(parameter.Name, PrimitiveTypeConverter.ConvertTo<string>(value));
                }
                else
                    if (value == null) throw Error.NotSupported("Can only convert primitive parameters to Uri parameters");
            }

            return result;
        }

        public Parameters ToParameters()
        {
            var result = new Parameters();

            foreach (var parameter in ToUriParamList())
            {
                result.Add(parameter.Item1, new FhirString(parameter.Item2));
            }

            return result;
        }
    }

    public enum SortOrder
    {
        Ascending,
        Descending
    }

    public enum ContainedSearch
    {
        True,
        False,
        Both
    }


    public enum SummaryType
    {
        /// <summary>
        /// Return only those elements marked as "summary" in the base definition of the resource(s)
        /// </summary>
        [EnumLiteral("true")]
        True,

        /// <summary>
        /// Return only the "text" element, and any mandatory elements
        /// </summary>
        [EnumLiteral("text")]
        Text,

        /// <summary>
        /// Remove the text element
        /// </summary>
        [EnumLiteral("data")]
        Data,

        /// <summary>
        /// Search only: just return a count of the matching resources, without returning the actual matches
        /// </summary>
        [EnumLiteral("count")]
        Count,

        /// <summary>
        /// Return all parts of the resource(s)
        /// </summary>
        [EnumLiteral("false")]
        False
    }


    public enum ContainedResult
    {
        Container,
        Contained
    }
}
