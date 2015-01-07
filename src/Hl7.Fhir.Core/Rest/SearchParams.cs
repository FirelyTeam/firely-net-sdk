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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Hl7.Fhir.Support;
using Hl7.Fhir.Model;

namespace Hl7.Fhir.Rest
{
    /// <summary>
    /// This is the Parameters partial class that adds all the specific functionality of a Parameters to the model
    /// </summary>
    public class SearchParams
    {
        public SearchParams()
        {
            Include = new List<string>();
            Sort = new List<Tuple<string, SortOrder>>();
            _parameters = new List<Tuple<string, string>>();
        }

        /// <summary>
        /// List of all the search parameter that have some special meaning.
        /// Primarily used to filter to the non-special parameters.
        /// Notice that _id, _text, _content, _tag, _profile and _security are predefined in the standard,
        /// but not can still be parsed as regular criteria. So they are not in the RESERVED_PARAMETERS.
        /// </summary>
        public static readonly string[] RESERVED_PARAMETERS = new string[] {
            SEARCH_PARAM_QUERY,
            SEARCH_PARAM_TEXT,
            SEARCH_PARAM_CONTENT,
            SEARCH_PARAM_COUNT,
            SEARCH_PARAM_INCLUDE,
            SEARCH_PARAM_SORT,
            SEARCH_PARAM_SUMMARY,
            SEARCH_PARAM_FILTER
            };
     
     
        public const string SEARCH_MODIF_ASCENDING = "asc";
        public const string SEARCH_MODIF_DESCENDING = "desc";

        public const char SEARCH_CHAINSEPARATOR = '.';
        public const char SEARCH_MODIFIERSEPARATOR = ':';


        /// <summary>
        /// Add a parameter with a given name and value.
        /// </summary>
        /// <param name="name">The name of the parameter</param>
        /// <param name="value">The value of the parameter as a FHIR datatype or Resource</param>
        /// <returns>this (Parameters), so you can chain AddParameter calls</returns>
        public SearchParams Add(string name, string value)
        {
            if (name == null) throw Error.ArgumentNull("name");
            if (value == null) throw Error.ArgumentNull("value");

            if (name == SEARCH_PARAM_QUERY) Query = value;
            else if (name == SEARCH_PARAM_TEXT) Text = value;
            else if (name == SEARCH_PARAM_CONTENT) Content = value;
            else if (name == SEARCH_PARAM_COUNT) Count = Int32.Parse(value);
            else if (name == SEARCH_PARAM_INCLUDE) Include.Add(value);
            else if (name.StartsWith(SEARCH_PARAM_SORT + SEARCH_MODIFIERSEPARATOR))
            {
                var order = name.Substring(SEARCH_PARAM_SORT.Length + 1).ToLower();

                if (order.StartsWith("asc")) Sort.Add(Tuple.Create(name, SortOrder.Ascending));
                else if (order.StartsWith("desc")) Sort.Add(Tuple.Create(name, SortOrder.Descending));
                else throw Error.Format("Cannot parse sort order '{0}'", null, value);
            }
            else if (name == SEARCH_PARAM_SUMMARY)
            {
                if (value == "true") Summary = true;
                else if (value == "false") Summary = false;
                else throw Error.Format("Cannot parse summary value '{0}'", null, value);
            }
            else if (name == SEARCH_PARAM_FILTER) Filter = value;
            else
                _parameters.Add(Tuple.Create(name, value));

            return this;
        }


        public IList<Tuple<string,string>> _parameters { get; private set; }


        public const string SEARCH_PARAM_QUERY = "_query";

        /// <summary>
        /// Gets or sets the special _query search parameter which asks the server to run a 
        /// specific named query instead of the standard FHIR search.
        /// </summary>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string Query  { get; set; }


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
        public bool? Summary { get; set; }


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
            if (Sort.Any()) result.AddRange(Sort.Select(s => Tuple.Create(createSortParamName(s.Item2), s.Item1)));
            if (Summary != null) result.Add(Tuple.Create(SEARCH_PARAM_SUMMARY, Summary.Value ? "true" : "false"));
            if (!String.IsNullOrEmpty(Filter)) result.Add(Tuple.Create(SEARCH_PARAM_FILTER, Filter));

            result.AddRange(_parameters);
            return result;
        }
    }

    public enum SortOrder
    {
        Ascending,
        Descending
    }


    public class UriParamList : List<Tuple<string,string>>
    {
        public UriParamList WithName(string key)
        {
            throw new NotImplementedException();
            //var match = MatchParam(key);
            //return pars.Where(par => match(par));
        }

        public Tuple<string,string> SingleWithName(string key)
        {
            throw new NotImplementedException();
            //var match = MatchParam(key);
            //return pars.SingleOrDefault(par => match(par));
        }

        internal static Predicate<Parameters.ParametersParameterComponent> MatchParam(string key)
        {
            // PCL does not have an overload on this routine that takes a char, only string
            if (key.Contains(SearchParams.SEARCH_MODIFIERSEPARATOR.ToString()))
            {
                return (Parameters.ParametersParameterComponent ext) => ext.Name == key;
            }
            else
            {
                // Add a modifier separator to the end if there's no modifier,
                // this way we can assure we don't match just a prefix 
                // (e.g. a param _querySpecial when looking for_query)
                var paramWithSep = key + SearchParams.SEARCH_MODIFIERSEPARATOR;
                return (Parameters.ParametersParameterComponent ext) => ext.Name.StartsWith(paramWithSep) ||
                                ext.Name == key;
            }
        }
    }
}
