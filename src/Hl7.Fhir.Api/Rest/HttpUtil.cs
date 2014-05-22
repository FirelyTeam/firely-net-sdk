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
using System.Xml;
using System.Xml.Linq;

namespace Hl7.Fhir.Rest
{
    public static class HttpUtil
    {
        public const string CONTENTLOCATION = "Content-Location";
        public const string LOCATION = "Location";
        public const string LASTMODIFIED = "Last-Modified";
        public const string CATEGORY = "Category";

        public const string RESTPARAM_FORMAT = "_format";

      
        public const string HISTORY_PARAM_SINCE = "_since";
        public const string HISTORY_PARAM_COUNT = Query.SEARCH_PARAM_COUNT;

        public static byte[] ReadAllFromStream(Stream s, int contentLength)
        {
            if (contentLength == 0) return null;

            //int bufferSize = contentLength < 4096 ? contentLength : 4096;
            int bufferSize = 4096;

            byte[] byteBuffer = new byte[bufferSize];
            MemoryStream buffer = new MemoryStream();

            int readLen = s.Read(byteBuffer, 0, byteBuffer.Length);

            while (readLen > 0)
            {
                buffer.Write(byteBuffer, 0, readLen);
                readLen = s.Read(byteBuffer, 0, byteBuffer.Length);
            }

            return buffer.ToArray();
        }
          

        public static ICollection<Tag> ParseCategoryHeader(string value)
        {
            if (String.IsNullOrEmpty(value)) return new List<Tag>();

            var result = new List<Tag>();

            var categories = value.SplitNotInQuotes(',').Where(s => !String.IsNullOrEmpty(s));

            foreach (var category in categories)
            {
                var values = category.SplitNotInQuotes(';').Where(s => !String.IsNullOrEmpty(s));

                if (values.Count() >= 1)
                {
                    var term = values.First();

                    var pars = values.Skip(1).Select( v =>
                        { 
                            var vsplit = v.Split('=');
                            var item1 = vsplit[0].Trim();
                            var item2 = vsplit.Length > 1 ? vsplit[1].Trim() : null;
                            return new Tuple<string,string>(item1,item2);
                        });

                    var scheme = new Uri(pars.Where(t => t.Item1 == "scheme").Select(t => t.Item2.Trim('\"')).FirstOrDefault(), UriKind.RelativeOrAbsolute);
                    var label = pars.Where(t => t.Item1 == "label").Select(t => t.Item2.Trim('\"')).FirstOrDefault();
                       
                    result.Add(new Tag(term,scheme,label));
                }
            }

            return result;
        }

        
       
        public static string BuildCategoryHeader(IEnumerable<Tag> tags)
        {
            var result = new List<string>();
            foreach(var tag in tags)
            {                
                StringBuilder sb = new StringBuilder();

                if (!String.IsNullOrEmpty(tag.Term))
                {
                    if (tag.Term.Contains(",") || tag.Term.Contains(";"))
                        throw new ArgumentException("Found tag containing ',' or ';' - this will produce an inparsable Category header");
                    sb.Append(tag.Term);
                }

                if (!String.IsNullOrEmpty(tag.Label))
                    sb.AppendFormat("; label=\"{0}\"", tag.Label);

                sb.AppendFormat("; scheme=\"{0}\"", tag.Scheme.ToString());
                result.Add(sb.ToString());
            }

            return String.Join(", ", result);
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
    }
}
