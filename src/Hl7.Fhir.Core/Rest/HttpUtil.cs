/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Support;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Rest
{
	/*
	 * Brian 16 Dec 2014:
	 *		Removed the Category in the HTML Header as we don't do this anymore for DSTU2
	 *		Implement everything using the native .net async patterns
	 */
    public static class HttpUtil
    {
        #region << HTTP Headers >>
        /// <summary>
        /// "Content-Location" found in the HTTP Headers
        /// </summary>
        public const string CONTENTLOCATION = "Content-Location";
        /// <summary>
        /// "Location" found in the HTTP Headers
        /// </summary>
        public const string LOCATION = "Location";
        /// <summary>
        /// "Last-Modified" found in the HTTP Headers
        /// </summary>
        public const string LASTMODIFIED = "Last-Modified";
        /// <summary>
        /// "ETag" found in the HTTP Headers
        /// </summary>
        public const string ETAG = "ETag";
        #endregion

        /// <summary>
        /// "_format" found as a parameter on the REST URL
        /// </summary>
        public const string RESTPARAM_FORMAT = "_format";

        /// <summary>
        /// "_since" found as a parameter on the REST History operation URL
        /// </summary>
        public const string HISTORY_PARAM_SINCE = "_since";
        /// <summary>
        /// "_count" found as a parameter on the REST History operation URL
        /// </summary>
        public const string HISTORY_PARAM_COUNT = Parameters.SEARCH_PARAM_COUNT;

        public static async Task<byte[]> ReadAllFromStream(Stream s, int contentLength)
        {
            if (contentLength == 0)
                return null;

            //int bufferSize = contentLength < 4096 ? contentLength : 4096;
            int bufferSize = 4096;

            byte[] byteBuffer = new byte[bufferSize];
            MemoryStream buffer = new MemoryStream();

            int readLen = await s.ReadAsync(byteBuffer, 0, byteBuffer.Length);

            while (readLen > 0)
            {
                await buffer.WriteAsync(byteBuffer, 0, readLen);
                readLen = await s.ReadAsync (byteBuffer, 0, byteBuffer.Length);
            }

            return buffer.ToArray();
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
            var value = pair.Length >= 2 ? String.Join("?", pair.Skip(1)) : null;
            if (value != null) value = Uri.UnescapeDataString(value);

            return new Tuple<string, string>(key, value);
        }

        public static IEnumerable<Tuple<string, string>> SplitParams(string query)
        {
            if (query == null) throw new ArgumentNullException("query");

            var result = new List<Tuple<string, string>>();

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
        /// <param name="pars"></param>
        /// <returns></returns>
        public static string JoinParams(IEnumerable<Tuple<string, string>> pars)
        {
            StringBuilder result = new StringBuilder();

            foreach (var kv in pars)
            {
                result.Append(JoinParam(kv));
                result.Append("&");
            }

            return result.ToString().TrimEnd('&');
        }


        public static bool IsWithin(this Uri me, Uri other)
        {
            if (!other.IsAbsoluteUri) return false;     // can never be within a relative path

            if (me.IsAbsoluteUri)
            {
                if (other.Authority.ToLower() != me.Authority.ToLower()) return false;
            }

            var meSegments = me.OriginalString.ToLower().Split('/');
            var otherSegments = other.OriginalString.ToLower().Split('/');

            var otherLength = otherSegments.Length;
            var meLength = meSegments.Length;

            if (meSegments.Length > otherSegments.Length) return false;
            for (int index = 0; index < meLength; index++)
            {
                if (otherSegments[otherLength-index-1].TrimEnd('/') != meSegments[meLength-index-1].TrimEnd('/')) return false;
            }

            return true;
        }

    }
}
