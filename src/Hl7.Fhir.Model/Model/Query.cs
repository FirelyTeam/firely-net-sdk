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

namespace Hl7.Fhir.Model
{
    public partial class Query
    {
        public const string SEARCH_PARAM_ID = "_id";
        public const string SEARCH_PARAM_NARRATIVE = "_text";
        public const string SEARCH_PARAM_CONTENT = "_content";
        public const string SEARCH_PARAM_TAG = "_tag";
        public const string SEARCH_PARAM_PROFILE = "_profile";
        public const string SEARCH_PARAM_SECURITY = "_security";
        public const string SEARCH_PARAM_QUERY = "_query";
        public const string SEARCH_PARAM_TYPE = "_type";

        public const string SEARCH_PARAM_COUNT = "_count";
        public const string SEARCH_PARAM_INCLUDE = "_include";
        public const string SEARCH_PARAM_SORT = "_sort";
        public const string SEARCH_PARAM_SUMMARY = "_summary";

        /// <summary>
        /// List of all the search parameter that have some special meaning.
        /// Primarily used to filter to the non-special parameters.
        /// Notice that _id, _text, _content, _tag, _profile and _security are predefined in the standard,
        /// but not can still be parsed as regular criteria. So they are not in the RESERVED_PARAMETERS.
        /// </summary>
        public static readonly string[] RESERVED_PARAMETERS = new string[] {
            SEARCH_PARAM_QUERY,
            SEARCH_PARAM_TYPE,

            SEARCH_PARAM_COUNT,
            SEARCH_PARAM_INCLUDE,
            SEARCH_PARAM_SORT,
            SEARCH_PARAM_SUMMARY
            };

        /// <summary>
        /// List of additional core search criteria that are
        /// resource-independent
        /// </summary>
        public static readonly string[] CORE_SEARCH_CRITERIA = new string[]
            {
                  SEARCH_PARAM_ID, 
                  SEARCH_PARAM_NARRATIVE, SEARCH_PARAM_CONTENT,
                  SEARCH_PARAM_TAG, SEARCH_PARAM_PROFILE, SEARCH_PARAM_SECURITY,
                  SEARCH_PARAM_TYPE
            };

        public const string SEARCH_MODIF_ASCENDING = "asc";
        public const string SEARCH_MODIF_DESCENDING = "desc";

        public const char SEARCH_CHAINSEPARATOR = '.';
        public const char SEARCH_MODIFIERSEPARATOR = ':';

        public const string PARAMETERURL = "http://hl7.org/fhir/query";


        public Query()
        {
            Identifier = new Uri("urn:uuid:" + Guid.NewGuid());
            Parameter = new List<Extension>();
        }

        /// <summary>
        /// Gets or sets the special _query search parameter which asks the server to run a 
        /// specific named query instead of the standard FHIR search.
        /// </summary>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string QueryName
        {
            get
            {
                return GetSingleValue(Query.SEARCH_PARAM_QUERY);
            }
            set
            {
                RemoveParameter(Query.SEARCH_PARAM_QUERY);
                AddParameter(Query.SEARCH_PARAM_QUERY, value);
            }
        }


        /// <summary>
        /// Gets or sets the special _type parameter, which limits the search to resources
        /// of a specific type. 
        /// </summary>
        /// <remarks>If this parameter is null, the search will be a non-restricted search
        /// across all resources.</remarks>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public string ResourceType
        {
            get
            {
                return GetSingleValue(Query.SEARCH_PARAM_TYPE);
            }
            set
            {
                RemoveParameter(Query.SEARCH_PARAM_TYPE);
                AddParameter(Query.SEARCH_PARAM_TYPE, value);
            }
        }


        /// <summary>
        /// Gets or sets the special _count search parameter, which limits the number
        /// of mathes returned per page in the pages search result
        /// </summary>
        /// <remark>The number of resources returned from the search may exceed this
        /// parameter, since additional _included resources for the matches are returned
        /// as well</remark>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public int? Count
        {
            get
            {
                var count = GetSingleValue(Query.SEARCH_PARAM_COUNT);
                return count != null ? Int32.Parse(count) : (int?)null;
            }
            set
            {
                RemoveParameter(Query.SEARCH_PARAM_COUNT);
                if (value.HasValue)
                    AddParameter(Query.SEARCH_PARAM_COUNT, value.ToString());
            }
        }


        /// <summary>
        /// Gets or sets the special _summary search parameter. If set to true,
        /// the server will not return all elements in each matching resource, but just
        /// the most important ones.
        /// </summary>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public bool Summary
        {
            get
            {
                var val = GetSingleValue(Query.SEARCH_PARAM_SUMMARY);
                return val == "true";
            }
            set
            {
                RemoveParameter(Query.SEARCH_PARAM_SUMMARY);
                AddParameter(Query.SEARCH_PARAM_SUMMARY, value ? "true" : "false");
            }
        }

        /// <summary>
        /// Gets or sets the _sort parameter, to modify the sort order of the search result.
        /// Uses a tuple (name, sortorder).
        /// </summary>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public Tuple<string, SortOrder> Sort
        {
            get
            {
                var ext = Parameter.SingleWithName(Query.SEARCH_PARAM_SORT);
                if (ext == null) return null;

                var key = ExtractParamKey(ext);
                var sort = key.EndsWith(Query.SEARCH_MODIF_DESCENDING) ? SortOrder.Descending : SortOrder.Ascending;
                var name = ExtractParamValue(ext);

                return Tuple.Create(name, sort);
            }
            set
            {
                RemoveParameter(Query.SEARCH_PARAM_SORT);

                var modif = value.Item2 == SortOrder.Ascending ?
                    Query.SEARCH_MODIF_ASCENDING : Query.SEARCH_MODIF_DESCENDING;
                var name = value.Item1;

                AddParameter(Query.SEARCH_PARAM_SORT + Query.SEARCH_MODIFIERSEPARATOR + modif, name);
            }
        }


        /// <summary>
        /// Returns a modifiable collection of _include parameters. These are used to include
        /// resources in the search result that the matched resources refer to.
        /// </summary>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public ICollection<string> Includes
        {
            get
            {
                return new IncludeCollection(this.Parameter);
            }
        }

        /// <summary>
        /// Returns a modifiable collection of all the parameters that are not reserved parameters.
        /// These are the parameters that can be parsed as <see cref="Hl7.Fhir.Search.Criterium"/>.
        /// These include the resource-independent parameters _id, _text, _content, _tag, _profile and _security.
        /// </summary>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public ICollection<Extension> Criteria
        {
            get
            {
                return new List<Extension>(this.Parameter.Where(p => !p.IsReserved()));
            }
        }

        /// <summary>
        /// Add a parameter with a given key and value.
        /// </summary>
        /// <param name="key">The name of the parameter, possibly including the modifier</param>
        /// <param name="value">The string representation of the parameter value</param>
        /// <returns>this (Query), so you can chain AddParameter calls</returns>
        public Query AddParameter(string key, string value)
        {
            if (key == null) throw new ArgumentNullException("key");
            if (value == null) throw new ArgumentNullException("value");

            if (Parameter == null) Parameter = new List<Extension>();

            Parameter.Add(BuildParamExtension(key, value));

            return this;
        }


        /// <summary>
        /// Remove a parameter with a given name.
        /// </summary>
        /// <param name="key">The name of the parameter, possibly including the modifier</param>
        /// <remarks><para>If the key includes a modifier, only that parameter will be removed. If
        /// the key is just a parameter name, all parameters with that name will be removed, regardless
        /// of modifiers attached to it.</para><para>No exception is thrown when the parameters were not found and nothing was removed.</para></remarks>
        public void RemoveParameter(string key)
        {
            if (key == null) throw new ArgumentNullException("key");

            if (Parameter == null) return;
            Parameter.RemoveAll(ParamsExtensions.MatchParam(key));
        }

        /// <summary>
        /// Searches for a parameter with the given name, and returns the
        /// value of the parameter, if a single result was found.
        /// </summary>
        /// <param name="key">The name of the parameter, possibly including the modifier</param>
        /// <returns>The value of the parameter with the given name. Will throw an 
        /// exception if multiple parameters with the given name exist.</returns>
        /// <remarks>If the key includes a modifier, the search will be for a parameter with the
        /// given modifier, otherwise any parameter with the given name will be matched.</remarks>
        public string GetSingleValue(string key)
        {
            if (key == null) throw new ArgumentNullException("key");
            if (Parameter == null) return null;

            var extension = Parameter.SingleWithName(key);
            return ExtractParamValue(extension);
        }


        /// <summary>
        /// Searches for all parameter with the given name, and returns a list
        /// with the values of those parameters.
        /// </summary>
        /// <param name="key">The name of the parameter, possibly including the modifier</param>
        /// <returns>A list of values of the parameters with the given name.</returns>
        /// <remarks>If the key includes a modifier, the search will be for parameters with the
        /// given modifier, otherwise any parameter with the given name will be matched.</remarks>
        public IEnumerable<string> GetValues(string key)
        {
            if (key == null) throw new ArgumentNullException("key");
            if (Parameter == null) return null;

            var extension = Parameter.WithName(key);
            return extension.Select(ext => ExtractParamValue(ext));
        }


        /// <summary>
        /// Build an Extension instance with an Url indicating a
        /// FHIR search parameter and a ValueString set to the given value.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Extension BuildParamExtension(string key, string value)
        {
            if (key == null) throw new ArgumentNullException("key");
            if (value == null) throw new ArgumentNullException("value");

            return new Extension(BuildParamUri(key), new FhirString(value));
        }


        public static string ExtractParamValue(Extension extension)
        {
            var element = extension != null ? extension.Value as FhirString : null;
            var value = element != null ? element.Value : null;
            return value;
        }

        /// <summary>
        /// Constructs an Url indicating a FHIR search parameter
        /// </summary>
        /// <param name="paramKey"></param>
        /// <returns></returns>
        public static Uri BuildParamUri(string paramKey)
        {
            if (paramKey == null) throw new ArgumentNullException("paramName");

            return new Uri(PARAMETERURL + "#" + paramKey, UriKind.Absolute);
        }

        private const string PARAMETERURLANDFRAGMENT = PARAMETERURL + "#";

        /// <summary>
        /// Given a Extension containing a FHIR search parameter, returns the
        /// name (and possibly modifier) of the parameter
        /// </summary>
        /// <param name="paramExt">An Extension containing a FHIR search parameter</param>
        /// <returns>The name of the parameter, possibly including a modifier</returns>
        public static string ExtractParamKey(Extension paramExt)
        {
            if (paramExt == null) throw new ArgumentNullException("paramExt");
            if (paramExt.Url == null) throw new ArgumentException("Extension.url cannot be null", "paramExt");

            var uriString = paramExt.Url.ToString();

            if (uriString.StartsWith(PARAMETERURLANDFRAGMENT))
                return uriString.Remove(0, PARAMETERURLANDFRAGMENT.Length);
            else
                return null;
        }
    }

    public enum SortOrder
    {
        Ascending,
        Descending
    }


    internal class IncludeCollection : ICollection<string>
    {
        public IncludeCollection(List<Extension> wrapped)
        {
            Wrapped = wrapped;
            _matcher = ParamsExtensions.MatchParam(Query.SEARCH_PARAM_INCLUDE);
        }

        public List<Extension> Wrapped { get; private set; }
        private Predicate<Extension> _matcher;


        public void Add(string item)
        {
            Wrapped.Add(Query.BuildParamExtension(Query.SEARCH_PARAM_INCLUDE, item));
        }

        public void Clear()
        {
            Wrapped.RemoveAll(_matcher);
        }

        public bool Contains(string item)
        {
            return Wrapped.Any(ext => _matcher(ext) && Query.ExtractParamValue(ext) == item);
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            Wrapped.FindAll(_matcher).Select(ext => Query.ExtractParamValue(ext))
                .ToArray<string>().CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return Wrapped.FindAll(_matcher).Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(string item)
        {
            var found = Wrapped.FirstOrDefault(ext => _matcher(ext) && Query.ExtractParamValue(ext) == item);
            if (found == null) return false;

            return Wrapped.Remove(found);
        }

        public IEnumerator<string> GetEnumerator()
        {
            return Wrapped.FindAll(_matcher).Select(ext => Query.ExtractParamValue(ext)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }



    public static class ParamsExtensions
    {
        public static IEnumerable<Extension> WithName(this IEnumerable<Extension> pars, string key)
        {
            var match = MatchParam(key);
            return pars.Where(par => match(par));
        }

        public static Extension SingleWithName(this IEnumerable<Extension> pars, string key)
        {
            var match = MatchParam(key);
            return pars.SingleOrDefault(par => match(par));
        }

        internal static Predicate<Extension> MatchParam(string key)
        {
            var param = Query.BuildParamUri(key).ToString();

			// PCL does not have an overload on this routine that takes a char, only string
			if (key.Contains(Query.SEARCH_MODIFIERSEPARATOR.ToString()))
			{
                return (Extension ext) => ext.Url.ToString() == param;
            }
            else
            {
                // Add a modifier separator to the end if there's no modifier,
                // this way we can assure we don't match just a prefix 
                // (e.g. a param _querySpecial when looking for_query)
                var paramWithSep = Query.BuildParamUri(key + Query.SEARCH_MODIFIERSEPARATOR).ToString();
                return (Extension ext) => ext.Url.ToString().StartsWith(paramWithSep) ||
                                (ext.Url.ToString() == param);
            }
        }

        internal static bool IsReserved(this Extension parameter)
        {
            string key = Query.ExtractParamKey(parameter).Split(new char[] { Query.SEARCH_MODIFIERSEPARATOR }).First();
            return Query.RESERVED_PARAMETERS.Contains(key);
        }
    }
}
