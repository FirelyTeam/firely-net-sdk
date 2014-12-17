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

namespace Hl7.Fhir.Model
{
    /// <summary>
    /// This is the Parameters partial class that adds all the specific functionality of a Query to the model
    /// </summary>
    public partial class Parameters
    {
        /// <summary>
        /// "_id"
        /// </summary>
        public const string SEARCH_PARAM_ID = "_id";
        /// <summary>
        /// "_text"
        /// </summary>
        public const string SEARCH_PARAM_NARRATIVE = "_text";
        /// <summary>
        /// "_content"
        /// </summary>
        public const string SEARCH_PARAM_CONTENT = "_content";
        /// <summary>
        /// "_tag"
        /// </summary>
        public const string SEARCH_PARAM_TAG = "_tag";
        /// <summary>
        /// "_profile"
        /// </summary>
        public const string SEARCH_PARAM_PROFILE = "_profile";
        /// <summary>
        /// "_security"
        /// </summary>
        public const string SEARCH_PARAM_SECURITY = "_security";
        /// <summary>
        /// "_query"
        /// </summary>
        public const string SEARCH_PARAM_QUERY = "_query";
        /// <summary>
        /// "_type"
        /// </summary>
        public const string SEARCH_PARAM_TYPE = "_type";

        /// <summary>
        /// "_count"
        /// </summary>
        public const string SEARCH_PARAM_COUNT = "_count";
        /// <summary>
        /// "_include"
        /// </summary>
        public const string SEARCH_PARAM_INCLUDE = "_include";
        /// <summary>
        /// "_sort"
        /// </summary>
        public const string SEARCH_PARAM_SORT = "_sort";
        /// <summary>
        /// "_summary"
        /// </summary>
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

        


        public Parameters()
        {
            // As this is an actual resource, we can't just allocate an
            // arbitrary identifier to it
            // base.Id = "urn:uuid:" + Guid.NewGuid();
            Parameter = new List<ParametersParameterComponent>();
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
                return GetSingleValue(Parameters.SEARCH_PARAM_QUERY);
            }
            set
            {
                RemoveParameter(Parameters.SEARCH_PARAM_QUERY);
                AddParameter(Parameters.SEARCH_PARAM_QUERY, value);
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
        public string ResourceSearchType
        {
            get
            {
                return GetSingleValue(Parameters.SEARCH_PARAM_TYPE);
            }
            set
            {
                RemoveParameter(Parameters.SEARCH_PARAM_TYPE);
                AddParameter(Parameters.SEARCH_PARAM_TYPE, value);
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
                var count = GetSingleValue(Parameters.SEARCH_PARAM_COUNT);
                return count != null ? Int32.Parse(count) : (int?)null;
            }
            set
            {
                RemoveParameter(Parameters.SEARCH_PARAM_COUNT);
                if (value.HasValue)
                    AddParameter(Parameters.SEARCH_PARAM_COUNT, value.ToString());
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
                var val = GetSingleValue(Parameters.SEARCH_PARAM_SUMMARY);
                return val == "true";
            }
            set
            {
                RemoveParameter(Parameters.SEARCH_PARAM_SUMMARY);
                AddParameter(Parameters.SEARCH_PARAM_SUMMARY, value ? "true" : "false");
            }
        }

        /// <summary>
        /// Gets or sets the _sort parameter, to modify the sort order of the search result.
        /// Uses a tuple (name, sortorder).
        /// </summary>
        [NotMapped]
        [IgnoreDataMemberAttribute]
        public List<Tuple<string, SortOrder>> Sort
        {
            get
            {
                var sortParams = Parameter.WithName(Parameters.SEARCH_PARAM_SORT);
                if (sortParams == null) return null;

                var result = new List<Tuple<string,SortOrder>>();

                foreach (var sp in sortParams)
                {
                    var sort = sp.Name.EndsWith(Parameters.SEARCH_MODIFIERSEPARATOR + Parameters.SEARCH_MODIF_DESCENDING) ? SortOrder.Descending : SortOrder.Ascending;
                    var name = ExtractParamValue(sp);

                    result.Add(Tuple.Create(name, sort));
                }

                return result;
            }
            set
            {
                RemoveParameter(Parameters.SEARCH_PARAM_SORT);

                foreach (var sort in value)
                {
                    var modif = sort.Item2 == SortOrder.Ascending ?
                        Parameters.SEARCH_MODIF_ASCENDING : Parameters.SEARCH_MODIF_DESCENDING;
                    var name = sort.Item1;

                    AddParameter(Parameters.SEARCH_PARAM_SORT + Parameters.SEARCH_MODIFIERSEPARATOR + modif, name);
                }                
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
        public ICollection<ParametersParameterComponent> Criteria
        {
            get
            {
                return new List<ParametersParameterComponent>(this.Parameter.Where(p => !p.IsReserved()));
            }
        }

        /// <summary>
        /// Add a parameter with a given key and value.
        /// </summary>
        /// <param name="key">The name of the parameter, possibly including the modifier</param>
        /// <param name="value">The string representation of the parameter value</param>
        /// <returns>this (Query), so you can chain AddParameter calls</returns>
        public Parameters AddParameter(string key, string value)
        {
            if (key == null) throw new ArgumentNullException("key");
            if (value == null) throw new ArgumentNullException("value");

            if (Parameter == null) Parameter = new List<ParametersParameterComponent>();

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
        public static Parameters.ParametersParameterComponent BuildParamExtension(string key, string value)
        {
            if (key == null) throw new ArgumentNullException("key");
            if (value == null) throw new ArgumentNullException("value");

            return new Parameters.ParametersParameterComponent() { Name = key, Value = new FhirString(value)};
        }


        public static string ExtractParamValue(Parameters.ParametersParameterComponent paramComponent)
        {
            var element = paramComponent != null ? paramComponent.Value as FhirString : null;
            var value = element != null ? element.Value : null;
            return value;
        }

    }

    public enum SortOrder
    {
        Ascending,
        Descending
    }


    internal class IncludeCollection : ICollection<string>
    {
        public IncludeCollection(List<Parameters.ParametersParameterComponent> wrapped)
        {
            Wrapped = wrapped;
            _matcher = ParamsExtensions.MatchParam(Parameters.SEARCH_PARAM_INCLUDE);
        }

        public List<Parameters.ParametersParameterComponent> Wrapped { get; private set; }
        private Predicate<Parameters.ParametersParameterComponent> _matcher;


        public void Add(string item)
        {
            Wrapped.Add(Parameters.BuildParamExtension(Parameters.SEARCH_PARAM_INCLUDE, item));
        }

        public void Clear()
        {
            Wrapped.RemoveAll(_matcher);
        }

        public bool Contains(string item)
        {
            return Wrapped.Any(ext => _matcher(ext) && Parameters.ExtractParamValue(ext) == item);
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            Wrapped.FindAll(_matcher).Select(ext => Parameters.ExtractParamValue(ext))
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
            var found = Wrapped.FirstOrDefault(ext => _matcher(ext) && Parameters.ExtractParamValue(ext) == item);
            if (found == null) return false;

            return Wrapped.Remove(found);
        }

        public IEnumerator<string> GetEnumerator()
        {
            return Wrapped.FindAll(_matcher).Select(ext => Parameters.ExtractParamValue(ext)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }



    public static class ParamsExtensions
    {
        public static IEnumerable<Parameters.ParametersParameterComponent> WithName(this IEnumerable<Parameters.ParametersParameterComponent> pars, string key)
        {
            var match = MatchParam(key);
            return pars.Where(par => match(par));
        }

        public static Parameters.ParametersParameterComponent SingleWithName(this IEnumerable<Parameters.ParametersParameterComponent> pars, string key)
        {
            var match = MatchParam(key);
            return pars.SingleOrDefault(par => match(par));
        }

        internal static Predicate<Parameters.ParametersParameterComponent> MatchParam(string key)
        {
			// PCL does not have an overload on this routine that takes a char, only string
			if (key.Contains(Parameters.SEARCH_MODIFIERSEPARATOR.ToString()))
			{
                return (Parameters.ParametersParameterComponent ext) => ext.Name == key;
            }
            else
            {
                // Add a modifier separator to the end if there's no modifier,
                // this way we can assure we don't match just a prefix 
                // (e.g. a param _querySpecial when looking for_query)
                var paramWithSep = key + Parameters.SEARCH_MODIFIERSEPARATOR;
                return (Parameters.ParametersParameterComponent ext) => ext.Name.StartsWith(paramWithSep) ||
                                ext.Name == key;
            }
        }

        internal static bool IsReserved(this Parameters.ParametersParameterComponent parameter)
        {
            string key = parameter.Name.Split(new char[] { Parameters.SEARCH_MODIFIERSEPARATOR }).First();
            return Parameters.RESERVED_PARAMETERS.Contains(key);
        }
    }
}
