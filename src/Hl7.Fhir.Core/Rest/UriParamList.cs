/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hl7.Fhir.Rest
{
    /// <summary>
    /// Represents a list of FHIR search parameters, which is based on a list of (key,value) tuples,
    /// where the key may have a modifier appended. 
    /// </summary>
    public class UriParamList : List<Tuple<string, string>>
    {
        public UriParamList() : base() { }

        public UriParamList(IEnumerable<Tuple<string, string>> contents) : base(contents) { }

        public static UriParamList FromUri(string uri)       
        {
            return null;
        }

        public void Add(string key, string value)
        {
            Add(Tuple.Create(key, value));
        }


        public UriParamList WithKey(string key)
        {
            var match = MatchParam(key);

            var result = new UriParamList();
            result.AddRange(this.Where(par => match(par)));
            return result;
        }

        public Tuple<string, string> Single(string key)
        {
            var match = MatchParam(key);
            return this.SingleOrDefault(par => match(par));
        }

        public string SingleValue(string key)
        {
            var t = Single(key);

            return t != null ? t.Item2 : null;
        }

        public IEnumerable<string> Values(string key)
        {
            return WithKey(key).Select(t => t.Item2);
        }

        public void Remove(string key)
        {
            var hits = MatchParam(key);

            this.RemoveAll(hits);
        }

        internal static Predicate<Tuple<string, string>> MatchParam(string key)
        {
            // PCL does not have an overload on this routine that takes a char, only string
            if (key.Contains(SearchParams.SEARCH_MODIFIERSEPARATOR.ToString()))
            {
                return (ext) => ext.Item1 == key;
            }
            else
            {
                // Add a modifier separator to the end if there's no modifier,
                // this way we can assure we don't match just a prefix 
                // (e.g. a param _querySpecial when looking for_query)
                var paramWithSep = key + SearchParams.SEARCH_MODIFIERSEPARATOR;
                return (ext) => ext.Item1.StartsWith(paramWithSep) || ext.Item1 == key;
            }
        }


        /// <summary>
        /// Parses the possibly escaped key=value query parameter into a (key,value) Tuple
        /// </summary>
        /// <param name="param"></param>
        /// <returns>A Tuple&lt;string,string&gt; containing the key and value. Value maybe null if
        /// only the key was specified as a query parameter.</returns>
        internal static Tuple<string, string> SplitParam(string param)
        {
            if (param == null) throw new ArgumentNullException("param");

            string[] pair = param.Split('=');

            var key = Uri.UnescapeDataString(pair[0]);
            var value = pair.Length >= 2 ? String.Join("=", pair.Skip(1)) : null;
            if (value != null) value = Uri.UnescapeDataString(value);

            return new Tuple<string, string>(key, value);
        }

        public static UriParamList FromQueryString(string query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var result = new UriParamList();

            if (query == String.Empty) return result;

            var q = query.TrimStart('?');

            var querySegments = q.Split(new string[] { "&" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var segment in querySegments)
            {
                var kv = SplitParam(segment);
                result.Add(kv);
            }

            return result;
        }


        /// <summary>
        /// Converts a key,value pair into a query parameters, escaping the key and value
        /// of necessary.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static string MakeParam(string key, string value = null)
        {
            if (key == null) throw new ArgumentNullException("key");

            var result = Uri.EscapeDataString(key);

            if (value != null)
                result += "=" + Uri.EscapeDataString(value);

            return result;
        }


        internal static string JoinParam(Tuple<string, string> kv)
        {
            if (kv == null) throw new ArgumentNullException("kv");
            if (kv.Item1 == null) throw new ArgumentException("Key in tuple may not be null", "kv");

            return MakeParam(kv.Item1, kv.Item2);
        }

        /// <summary>
        /// Builds a query string based on a set of key,value pairs
        /// </summary>
        public string ToQueryString()
        {
            StringBuilder result = new StringBuilder();

            foreach (var kv in this)
            {
                result.Append(JoinParam(kv));
                result.Append("&");
            }

            return result.ToString().TrimEnd('&');
        }
    }
}
