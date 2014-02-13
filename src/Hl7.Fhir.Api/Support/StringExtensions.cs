using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Hl7.Fhir.Support
{
    public static class StringExtensions
    {
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
                if (value[i] == '\\' && !seenEscape)
                {
                    seenEscape = true;
                    continue;
                }
                else if (value[i] == separator && !seenEscape)
                {
                    result.Add(word);
                    word = String.Empty;
                }
                else
                {
                    word += value[i];
                    seenEscape = false;
                }
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
