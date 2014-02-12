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



using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
            Id = "urn:uuid" + Guid.NewGuid();
            Parameter = new List<Extension>();
        }

        /// <summary>
        /// Gets or sets the special _query search parameter
        /// </summary>
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
        /// Gets or sets the special _count search parameter
        /// </summary>
        public int Count
        {
            get
            {
                return Int32.Parse(GetSingleValue(Query.SEARCH_PARAM_COUNT));
            }
            set
            {
                RemoveParameter(Query.SEARCH_PARAM_COUNT);
                AddParameter(Query.SEARCH_PARAM_COUNT, value.ToString());
            }
        }

        /// <summary>
        /// Gets or sets the special _summary search parameter
        /// </summary>
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
        /// Gets or sets the ordering modifier on _sort parameter (name,order)
        /// </summary>
        /// <value>A tuple with two items (name,sort order).</value>
        public Tuple<string,SortOrder> Sort
        {
            get
            {
                var ext = Parameter.SingleOrDefault(Query.SEARCH_PARAM_SORT, ignoreModifier: true);
                if (ext == null) return null;

                var key = ExtractParamKey(ext.Url);
                var sort = key.EndsWith(Query.SEARCH_MODIF_DESCENDING) ? SortOrder.Descending : SortOrder.Ascending;
                var name = GetParamValue(ext);

                return Tuple.Create(name, sort);
            }
            set
            {
                RemoveParameter(Query.SEARCH_PARAM_SORT, ignoreModifier:true);

                var modif = value.Item2 == SortOrder.Ascending? 
                    Query.SEARCH_MODIF_ASCENDING : Query.SEARCH_MODIF_DESCENDING;
                var name = value.Item1;

                AddParameter(Query.SEARCH_PARAM_SORT + Query.SEARCH_MODIFIERSEPARATOR + modif, name);
            }
        }


        public ICollection<string> Include
        {
            get
            {
                return new IncludeCollection(this.Parameter);
            }
        }

        public void AddParameter(string key, string value)
        {
            if (key == null) throw new ArgumentNullException("key");
            if (value == null) throw new ArgumentNullException("value");

            if (Parameter == null) Parameter = new List<Extension>();

            Parameter.Add(BuildParamExtension(key,value));
        }

        public void RemoveParameter(string key, bool ignoreModifier = false)
        {
            if (key == null) throw new ArgumentNullException("key");

            if (Parameter == null) return;
            Parameter.RemoveAll( ParamsExtensions.MatchParam(key,ignoreModifier) );
        }

        public string GetSingleValue(string key)
        {
            if (key == null) throw new ArgumentNullException("key");

            if (Parameter == null) return null;

            var extension = Parameter.SingleOrDefault(key, ignoreModifier: false);
            return GetParamValue(extension);
        }


        public IEnumerable<string> GetValues(string key)
        {
            if (key == null) throw new ArgumentNullException("key");

            if (Parameter == null) return null;

            var extension = Parameter.Where(key, ignoreModifier: false);

            return extension.Select(ext => GetParamValue(ext));
        }


        public static Extension BuildParamExtension(string key, string value)
        {
            if (key == null) throw new ArgumentNullException("key");
            if (value == null) throw new ArgumentNullException("value");

            return new Extension(BuildParamUri(key), new FhirString(value));
        }


        internal static string GetParamValue(Extension extension)
        {
            var element = extension != null ? extension.Value as FhirString : null;
            var value = element != null ? element.Value : null;
            return value;
        }

        public static Uri BuildParamUri(string paramKey)
        {
            if (paramKey == null) throw new ArgumentNullException("paramName");

            return new Uri(PARAMETERURL + "#" + paramKey, UriKind.Absolute);
        }

        private const string PARAMETERURLANDFRAGMENT = PARAMETERURL + "#";

        public static string ExtractParamKey(Uri paramUri)
        {
            if (paramUri == null) return null;

            var uriString = paramUri.ToString();

            if (uriString.StartsWith(PARAMETERURLANDFRAGMENT))
                return uriString.Remove(0,PARAMETERURLANDFRAGMENT.Length);
            else
                return null;
        }
    }

    public enum SortOrder
    {
        Ascending,
        Descending
    }


    // Tuple<modifier,value>
    internal class IncludeCollection : ICollection<string>
    {
        public IncludeCollection(List<Extension> wrapped)
        {
            Wrapped = wrapped;
            _matcher = ParamsExtensions.MatchParam(Query.SEARCH_PARAM_INCLUDE,ignoreModifier:false);
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
            return Wrapped.Any(ext => _matcher(ext) && Query.GetParamValue(ext) == item);
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            Wrapped.FindAll(_matcher).Select(ext => Query.GetParamValue(ext))
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
            var found = Wrapped.FirstOrDefault( ext => _matcher(ext) && Query.GetParamValue(ext) == item);
            if(found == null) return false;

            return Wrapped.Remove(found);
        }

        public IEnumerator<string> GetEnumerator()
        {
            return Wrapped.FindAll(_matcher).Select(ext => Query.GetParamValue(ext)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }



    public static class ParamsExtensions
    {
        public static IEnumerable<Extension> Where(this IEnumerable<Extension> pars, string key, bool ignoreModifier)
        {
            var match = MatchParam(key, ignoreModifier);
            return pars.Where( par => match(par) );
        }

        public static Extension SingleOrDefault(this IEnumerable<Extension> pars, string key, bool ignoreModifier)
        {
            var match = MatchParam(key, ignoreModifier);
            return pars.SingleOrDefault(par => match(par));
        }

        public static Extension Single(this IEnumerable<Extension> pars, string key, bool ignoreModifier)
        {
            var match = MatchParam(key, ignoreModifier);
            return pars.Single(par => match(par));
        }

        internal static Predicate<Extension> MatchParam(string key, bool ignoreModifier)
        {
            var param = Query.BuildParamUri(key).ToString();
            var paramWithModifier = Query.BuildParamUri(key).ToString() + Query.SEARCH_MODIFIERSEPARATOR;

            return (Extension ext) => ext.Url.ToString() == param ||
                (ignoreModifier && ext.Url.ToString().StartsWith(paramWithModifier));
        }
    }
}
