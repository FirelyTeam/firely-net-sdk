using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Hl7.Fhir.Support
{
    public static class StringExtensions
    {
        public static string[] SplitNotInQuotes(this string value, char separator)
        {
            var parts = Regex.Split(value, separator + "(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)")
                                .Select(s => s.Trim());
                               
            return parts.ToArray<string>();
        }

        public static string[] SplitNotEscaped(this string value, char separator)
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
    }
}
