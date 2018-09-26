/* 
 * Copyright (c) 2017+ brianpos, Firely and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/ewoutkramer/fhir-net-api/blob/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Hl7.Fhir.WebApi
{
    public static class HttpRequestExtensions
    {
        public static bool Exists(this HttpHeaders headers, string key)
        {
            IEnumerable<string> values;
            if (headers.TryGetValues(key, out values))
            {
                return values.Count() > 0;
            }
            else return false;

        }
        public static void Replace(this HttpHeaders headers, string header, string value)
        {
            //if (headers.Exists(header)) 
            headers.Remove(header);
            headers.Add(header, value);
        }
        public static string Value(this HttpHeaders headers, string key)
        {
            IEnumerable<string> values;
            if (headers.TryGetValues(key, out values))
            {
                return values.FirstOrDefault();
            }
            else return null;
        }
        public static void ReplaceHeader(this HttpRequestMessage request, string header, string value)
        {
            request.Headers.Replace(header, value);
        }
        public static string Header(this HttpRequestMessage request, string key)
        {
            IEnumerable<string> values;
            if (request.Content.Headers.TryGetValues(key, out values))
            {
                return values.FirstOrDefault();
            }
            else return null;
        }
        
        public static string GetParameter(this HttpRequestMessage request, string key)
        {
            foreach (var param in request.GetQueryNameValuePairs())
            {
                if (param.Key == key) return param.Value;
            }
            return null;
        }

        static List<String> reservedSearchParams = new List<string>() { "_id", "_language", "_lastUpdated", "_profile", "_security", "_tag" };

        /// <summary>
        /// Retrieve all the parameters from the Request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="filterReservedParameters">Do not include any parameters that start with "_" (such as _since)</param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<string, string>> TupledParameters(this HttpRequestMessage request, bool filterReservedParameters)
        {
            var list = new List<KeyValuePair<string, string>>();

            IEnumerable<KeyValuePair<string, string>> query = request.GetQueryNameValuePairs();
            
            foreach (var pair in query)
            {
                if (!filterReservedParameters || !pair.Key.StartsWith("_") || reservedSearchParams.Contains(pair.Key))
                    list.Add(new KeyValuePair<string, string>(pair.Key, pair.Value));
            }
            return list;
        }

        public static IEnumerable<KeyValuePair<string, string>> TupledParameters(this System.Collections.Specialized.NameValueCollection query, bool filterReservedParameters)
        {
            var list = new List<KeyValuePair<string, string>>();

            if (query.HasKeys())
            {
                foreach (string key in query.Keys)
                {
                    if (!filterReservedParameters || !key.StartsWith("_") || reservedSearchParams.Contains(key))
                        list.Add(new KeyValuePair<string, string>(key, query[key]));
                }
            }
            return list;
        }
    }
}