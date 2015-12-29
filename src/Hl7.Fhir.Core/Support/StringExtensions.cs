/* 
 * Copyright (c) 2014, Furore (info@furore.com) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/ewoutkramer/fhir-net-api/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Hl7.Fhir.Support
{
    internal static class StringExtensions
    {
        public static string RemovePrefix(this string instance, string prefix)
        {
            if (instance == null) return null;
            if (prefix == null) return instance;

            if (instance.StartsWith(prefix))
                return instance.Remove(0, prefix.Length);
            else
                return instance;
        }

        public static string FormatWith(this string format, params object[] args)
        {
            if (format == null)
                throw new ArgumentNullException("format");

            return string.Format(format, args);
        }

        public static string FormatWith(this string format, IFormatProvider provider, params object[] args)
        {
            if (format == null)
                throw new ArgumentNullException("format");

            return string.Format(provider, format, args);
        }


        public static string Capitalize(this string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;

            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        internal static string[] SplitNotInQuotes(this string value, char separator)
        {
            var parts = Regex.Split(value, separator + "(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)")
                                .Select(s => s.Trim());
                               
            return parts.ToArray<string>();
        }

        internal static string[] SplitNotEscaped(this string value, char separator)
        {
            String word = String.Empty;
            List<String> result = new List<string>();
            bool seenEscape = false;

            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] == '\\')
                {
                    seenEscape = true;
                    continue;
                }
               
                if (value[i] == separator && !seenEscape)
                {
                    result.Add(word);
                    word = String.Empty;
                    continue;
                }

                if (seenEscape)
                {
                    word += '\\';
                    seenEscape = false;
                }

                word += value[i];
            }

            result.Add(word);

            return result.ToArray<string>();
        }

        internal static Tuple<string,string> SplitLeft(this string text, char separator)
        {
            var pos = text.IndexOf(separator);

            if (pos == -1)
                return Tuple.Create(text, (string)null);     // Nothing to split
            else
            {
                var key = text.Substring(0, pos);
                var value = text.Substring(pos + 1);

                return Tuple.Create(key, value);
            }
        }
    }
}
