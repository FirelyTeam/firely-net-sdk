#nullable  enable

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
using Hl7.Fhir.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Hl7.Fhir.Rest
{
    /// <summary>
    /// Contains criteria that can be passed to a search operation or conditional update/delete/create
    /// </summary>
    public class SearchParams
    {
        /// <summary>
        /// Construct an empty instance.
        /// </summary>
        public SearchParams()
        {
            // Nothing
        }

        /// <summary>
        /// Construct a new instance initialized with a single name/value.
        /// </summary>
        public SearchParams(string name, string value) : this() => Add(name, value);

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

        /// <summary>
        /// Initializes the <see cref="Elements"/> parameter with the given list of elements.
        /// </summary>
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
                if (!Int32.TryParse(value, out int count) || count < 0) throw Error.Format("Invalid {0}: '{1}' is not a non-negative integer".FormatWith(name, value));
                Count = count;
            }
            else if (name.StartsWith(SEARCH_PARAM_INCLUDE + SEARCH_MODIFIERSEPARATOR))
            {
                if (Enum.TryParse<IncludeModifier>(name.Substring(SEARCH_PARAM_INCLUDE.Length + 1), ignoreCase: true, out var modifier))
                    addNonEmpty(name, Include, (value, modifier));
                else
                    throw Error.Format($"Invalid include modifier in {name}");
            }
            else if (name == SEARCH_PARAM_INCLUDE) addNonEmpty(name, Include, (value, IncludeModifier.None));
            else if (name.StartsWith(SEARCH_PARAM_REVINCLUDE + SEARCH_MODIFIERSEPARATOR))
            {
                if (Enum.TryParse<IncludeModifier>(name.Substring(SEARCH_PARAM_REVINCLUDE.Length + 1), ignoreCase: true, out var modifier))
                    addNonEmpty(name, RevInclude, (value, modifier));
                else
                    throw Error.Format($"Invalid revinclude modifier in {name}");
            }
            else if (name == SEARCH_PARAM_REVINCLUDE) addNonEmpty(name, RevInclude, (value, IncludeModifier.None));
            else if (name.StartsWith(SEARCH_PARAM_SORT + SEARCH_MODIFIERSEPARATOR))
                throw Error.Format($"Invalid {SEARCH_PARAM_SORT}: encountered DSTU2 (modifier) based sort, please change to newer format");
            else if (name == SEARCH_PARAM_SORT)
            {
                if (String.IsNullOrEmpty(value))
                    throw Error.Format($"Invalid {SEARCH_PARAM_SORT}: value cannot be empty");
                var elements = value.Split(',');
                if (elements.Any(String.IsNullOrEmpty))
                    throw Error.Format($"Invalid {SEARCH_PARAM_SORT}: must be a list of non-empty element names");
                if (elements.Any(f => f == "-"))
                    throw Error.Format($"Invalid {SEARCH_PARAM_SORT}: one of the values is just a single '-', an element name must be provided");
                if (!elements.All(f => Char.IsLetter(f[0]) || f[0] == '-' || f[0] == '_'))
                    throw Error.Format($"Invalid {SEARCH_PARAM_SORT}: must be a list of element names, optionally prefixed with '-'");

                addNonEmptySort(elements);
            }
            else if (name == SEARCH_PARAM_SUMMARY)
            {
                if (Enum.TryParse(value, ignoreCase: true, result: out SummaryType st))
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
            else if (name == SEARCH_PARAM_ELEMENTS)
            {
                if (String.IsNullOrEmpty(value)) throw Error.Format("Invalid {0} value: it cannot be empty".FormatWith(name));
                Elements.AddRange(value.Split(','));
            }
            else
                Parameters.Add(Tuple.Create(name, value));

            return this;
        }

        private static string nonEmptySingleValue(string paramName, string? currentValue, string newValue)
        {
            if (String.IsNullOrEmpty(newValue)) throw Error.Format("Invalid {0} value: it cannot be empty".FormatWith(paramName));
            if (!String.IsNullOrEmpty(currentValue)) throw Error.Format("{0} cannot be specified more than once".FormatWith(paramName));
            return newValue;
        }

        private static void addNonEmpty(string paramName, IList<(string path, IncludeModifier modifier)> values, (string path, IncludeModifier modifier) value)
        {
            if (String.IsNullOrEmpty(value.path)) throw Error.Format("Invalid {0} value: it cannot be empty".FormatWith(paramName));
            values.Add(value);
        }

        private void addNonEmptySort(string[] elements)
        {
            foreach (var e in elements)
            {
                var newTuple = e[0] == '-' ? (e.Substring(1), SortOrder.Descending) :
                           (e, SortOrder.Ascending);
                Sort.Add(newTuple);
            }
        }

        /// <summary>
        /// The 'regular' parameters. The parameters that have no special meaning.
        /// </summary>
        public IList<Tuple<string, string>> Parameters { get; } = [];

        public const string SEARCH_PARAM_QUERY = "_query";

        /// <summary>
        /// Gets or sets the special _query search parameter which asks the server to run a 
        /// specific named query instead of the standard FHIR search.
        /// </summary>
        [NotMapped]
        [IgnoreDataMember]
        public string? Query { get; set; }

        public const string SEARCH_PARAM_TEXT = "_text";

        /// <summary>
        /// Gets or sets the special _text search parameter which which search on the narrative of the resource. 
        /// </summary>
        [NotMapped]
        [IgnoreDataMember]
        public string? Text { get; set; }


        public const string SEARCH_PARAM_CONTENT = "_content";

        /// <summary>
        /// Gets or sets the special _text search parameter which which search on the entire content of the resource.
        /// </summary>
        [NotMapped]
        [IgnoreDataMember]
        public string? Content { get; set; }


        public const string SEARCH_PARAM_COUNT = "_count";

        /// <summary>
        /// Gets or sets the special _count search parameter, which limits the number
        /// of mathes returned per page in the pages search result
        /// </summary>
        /// <remark>The number of resources returned from the search may exceed this
        /// parameter, since additional _included resources for the matches are returned
        /// as well</remark>
        [NotMapped]
        [IgnoreDataMember]
        public int? Count { get; set; }

        public const string SEARCH_PARAM_SUMMARY = "_summary";

        /// <summary>
        /// Gets or sets the special _summary search parameter. If set to true,
        /// the server will not return all elements in each matching resource, but just
        /// the most important ones.
        /// </summary>
        [NotMapped]
        [IgnoreDataMember]
        public SummaryType? Summary { get; set; }

        public const string SEARCH_PARAM_FILTER = "_filter";

        /// <summary>
        /// Gets or sets the special _filter search parameter to supply an advanced query expression
        /// </summary>
        [NotMapped]
        [IgnoreDataMember]
        public string? Filter { get; set; }
        
        /// <summary>
        /// "_sort"
        /// </summary>
        public const string SEARCH_PARAM_SORT = "_sort";
        
        /// <summary>
        /// Gets or sets the _sort parameter, to modify the sort order of the search result.
        /// Uses a tuple (name, sortorder).
        /// </summary>
        [NotMapped]
        [IgnoreDataMember]
        public IList<(string, SortOrder)> Sort { get; } = [];


        public const string SEARCH_PARAM_INCLUDE = "_include";

        /// <summary>
        /// Returns a modifiable collection of _include parameters. These are used to include
        /// resources in the search result that the matched resources refer to.
        /// </summary>
        [NotMapped]
        [IgnoreDataMember]
        public IList<(string, IncludeModifier)> Include { get; } = [];

        public const string SEARCH_PARAM_REVINCLUDE = "_revinclude";

        [NotMapped]
        [IgnoreDataMember]
        public IList<(string, IncludeModifier)> RevInclude { get; } = [];


        public const string SEARCH_PARAM_CONTAINED = "_contained";

        [NotMapped]
        [IgnoreDataMember]
        public ContainedSearch? Contained { get; private set; }


        public const string SEARCH_PARAM_CONTAINEDTYPE = "_containedType";

        [NotMapped]
        [IgnoreDataMember]
        public ContainedResult? ContainedType { get; private set; }


        public const string SEARCH_PARAM_ELEMENTS = "_elements";

        public List<string> Elements { get; } = [];

        /// <summary>
        /// Take a list of key/value pairs and turn them into a new <see cref="SearchParams"/> instance.
        /// </summary>
        public static SearchParams FromUriParamList(IEnumerable<Tuple<string, string>> parameters)
        {
            var result = new SearchParams();

            foreach (var parameter in parameters)
                result.Add(parameter.Item1, parameter.Item2);

            return result;
        }

        /// <summary>
        /// Convert this instance into a <see cref="UriParamList"/>. 
        /// </summary>
        public UriParamList ToUriParamList()
        {
            var result = new UriParamList();

            if (!String.IsNullOrEmpty(Query)) result.Add(Tuple.Create(SEARCH_PARAM_QUERY, Query));
            if (!String.IsNullOrEmpty(Text)) result.Add(Tuple.Create(SEARCH_PARAM_TEXT, Text));
            if (!String.IsNullOrEmpty(Content)) result.Add(Tuple.Create(SEARCH_PARAM_CONTENT, Content));
            if (Count != null) result.Add(Tuple.Create(SEARCH_PARAM_COUNT, Count.Value.ToString()));
            if (Include.Any()) result.AddRange(createIncludeParams(SEARCH_PARAM_INCLUDE, Include));
            if (RevInclude.Any()) result.AddRange(createIncludeParams(SEARCH_PARAM_REVINCLUDE, RevInclude));
            if (Sort.Any()) result.Add(createSortParam(Sort));
            if (Summary != null) result.Add(Tuple.Create(SEARCH_PARAM_SUMMARY, Summary.Value.ToString().ToLower()));
            if (!String.IsNullOrEmpty(Filter)) result.Add(Tuple.Create(SEARCH_PARAM_FILTER, Filter));
            if (Contained != null) result.Add(Tuple.Create(SEARCH_PARAM_CONTAINED, Contained.Value.ToString().ToLower()));
            if (ContainedType != null) result.Add(Tuple.Create(SEARCH_PARAM_CONTAINEDTYPE, ContainedType.Value.ToString().ToLower()));
            if (Elements.Any()) result.Add(Tuple.Create(SEARCH_PARAM_ELEMENTS, String.Join(",", Elements)));

            result.AddRange(Parameters);
            return result;

            Tuple<string, string> createSortParam(IList<(string, SortOrder)> sorts)
            {
                var values =
                    from s in sorts
                    let orderPrefix = s.Item2 == SortOrder.Descending ? "-" : ""
                    select orderPrefix + s.Item1;
                return new Tuple<string, string>(SEARCH_PARAM_SORT, String.Join(",", values));
            }

            IEnumerable<Tuple<string, string>> createIncludeParams(string paramtype, IList<(string path, IncludeModifier modifier)> includes)
            {
                return from i in includes
                       let modifier = (i.modifier != IncludeModifier.None) ? SEARCH_MODIFIERSEPARATOR + i.modifier.GetLiteral() : ""
                       select new Tuple<string, string>(paramtype + modifier, i.path);

            }
        }
    }

    /// <summary>
    /// Possible values for the "_sort" parameter
    /// </summary>
    public enum SortOrder
    {
        Ascending,
        Descending
    }

    /// <summary>
    /// Possible values for the "_contained" parameter
    /// </summary>
    public enum ContainedSearch
    {
        True,
        False,
        Both
    }

    /// <summary>
    /// Possible "_include" modifiers
    /// </summary>
    public enum IncludeModifier
    {
        /// <summary>
        /// No modifier on the include/revinclude parameter
        /// </summary>
        None,
        /// <summary>
        /// R4 only: Allows the include/revinclude process to iterate
        /// </summary>
        [EnumLiteral("iterate")]
        Iterate,
        /// <summary>
        /// STU3 only: Allows the include/revinclude process to recurse
        /// </summary>
        [EnumLiteral("recurse")]
        Recurse
    }

    /// <summary>
    /// Possible "_summary" values
    /// </summary>
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


    /// <summary>
    /// Values for the "_containedType" parameter.
    /// </summary>
    public enum ContainedResult
    {
        Container,
        Contained
    }
}

#nullable restore

