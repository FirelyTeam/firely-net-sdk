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

namespace Hl7.Fhir.Rest
{
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
                if (!Int32.TryParse(value, out count) || count <= 0) throw Error.Format("Invalid {0}: '{1}' is not a positive integer".FormatWith(name, value));
                Count = count;
            }
            else if (name == SEARCH_PARAM_INCLUDE) addNonEmpty(name, Include, value);
            else if (name == SEARCH_PARAM_REVINCLUDE) addNonEmpty(name, RevInclude, value);
            else if (name.StartsWith(SEARCH_PARAM_SORT + SEARCH_MODIFIERSEPARATOR))
                throw Error.Format($"Invalid {SEARCH_PARAM_SORT}: encountered DSTU2 (modifier) based sort, please change to STU3 format");
            else if (name == SEARCH_PARAM_SORT)
            {
                if(String.IsNullOrEmpty(value))
                    throw Error.Format($"Invalid {SEARCH_PARAM_SORT}: value cannot be empty");
                var elements = value.Split(',');
                if (elements.Any(f => String.IsNullOrEmpty(f)))
                    throw Error.Format($"Invalid {SEARCH_PARAM_SORT}: must be a list of non-empty element names");
                if (!elements.All(f => Char.IsLetter(f[0]) || f[0] == '-' ))
                    throw Error.Format($"Invalid {SEARCH_PARAM_SORT}: must be a list of element names, optionally prefixed with '-'");

                addNonEmptySort(elements);
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
            else if (name == SEARCH_PARAM_ELEMENTS)
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

        private void addNonEmptySort(string[] elements)
        {
            foreach (var e in elements)
            {
                var newTuple = e[0] == '-' ? Tuple.Create(e.Substring(1), SortOrder.Descending) :
                            Tuple.Create(e, SortOrder.Ascending);
                Sort.Add(newTuple);                    
            }
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


        public UriParamList ToUriParamList()
        {
            var result = new UriParamList();

            if (!String.IsNullOrEmpty(Query)) result.Add(Tuple.Create(SEARCH_PARAM_QUERY, Query));
            if (!String.IsNullOrEmpty(Text)) result.Add(Tuple.Create(SEARCH_PARAM_TEXT, Text));
            if (!String.IsNullOrEmpty(Content)) result.Add(Tuple.Create(SEARCH_PARAM_CONTENT, Content));
            if (Count != null) result.Add(Tuple.Create(SEARCH_PARAM_COUNT, Count.Value.ToString()));
            if (Include.Any()) result.AddRange(Include.Select(i => Tuple.Create(SEARCH_PARAM_INCLUDE, i)));
            if (RevInclude.Any()) result.AddRange(RevInclude.Select(i => Tuple.Create(SEARCH_PARAM_REVINCLUDE, i)));
            if (Sort.Any()) result.Add(createSortParam(Sort));
            if (Summary != null) result.Add(Tuple.Create(SEARCH_PARAM_SUMMARY, Summary.Value.ToString().ToLower()));
            if (!String.IsNullOrEmpty(Filter)) result.Add(Tuple.Create(SEARCH_PARAM_FILTER, Filter));
            if (Contained != null) result.Add(Tuple.Create(SEARCH_PARAM_CONTAINED, Contained.Value.ToString().ToLower()));
            if (ContainedType != null) result.Add(Tuple.Create(SEARCH_PARAM_CONTAINEDTYPE, ContainedType.Value.ToString().ToLower()));
            if (Elements.Any()) result.Add(Tuple.Create(SEARCH_PARAM_ELEMENTS, String.Join(",",Elements)));

            result.AddRange(Parameters);
            return result;

            Tuple<string,string> createSortParam(IList<Tuple<string,SortOrder>> sorts)
            {
                var values = 
                    from s in sorts
                    let orderPrefix = s.Item2 == SortOrder.Descending ? "-" : ""
                    select orderPrefix + s.Item1;
                return new Tuple<string,string>(SEARCH_PARAM_SORT, String.Join(",", values));
            }
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
