/* 
 * Copyright (c) 2017, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://github.com/FirelyTeam/fhir-net-api/blob/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Hl7.Fhir.Specification.Source
{
    internal class FilePatternFilter
    {
        private readonly bool _negate;

        private readonly string _regex;
        private readonly Regex _compiledRegex;

        public FilePatternFilter(string filter, bool negate = false) : this(new[] { filter }, negate)
        {
        }

        public FilePatternFilter(IEnumerable<string> filters, bool negate = false)
        {
            _regex = filterToRegex(filters);
            _compiledRegex = new Regex(_regex, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            _negate = negate;
        }

        private const string REGEX_ESCAPED_CHARS = @".^$+?()[{\|/";

        private static string filterToRegex(IEnumerable<string> filters)
        {
            var patterns = filters
                .Select(i => convertPathSuffix(convertPathPrefix(i)))
                .Select(e => escapeRegexChars(e))
                .Select(g => convertToRegEx(g));

            var pattern = String.Join("|", patterns);

            return pattern;

            string convertPathPrefix(string ignore) => ignore.StartsWith("/") || ignore.StartsWith("**/") ? ignore : "**/" + ignore;
            string convertPathSuffix(string ignore) => !ignore.EndsWith("/")  || ignore.EndsWith("/**") ? ignore : ignore + "**";
            string convertToRegEx(string glob) => "^" + glob.Replace("**", "__recglob__").Replace("*", "__glob__")
                            .Replace("__recglob__", @"(.*)").Replace("__glob__", @"([^\/]*)") + "$";
            string escapeRegexChars(string escape) =>
                            escape.Aggregate("",(s, c) => REGEX_ESCAPED_CHARS.Contains(c) ? s += $"\\{c}" : s += c);
         }


        public bool matchesPattern(string path) => _compiledRegex.IsMatch(path);

        public string[] Filter(string baseDirectory, IEnumerable<string> filePaths)
        {            
            if (baseDirectory.Last() != Path.DirectorySeparatorChar) baseDirectory += Path.DirectorySeparatorChar;
            var len = baseDirectory.Length;

            return filePaths
                .Where(input => input.StartsWith(baseDirectory))
                .Select(full => convertToUnix(removeBase(full)))
                .Where(relative => _negate ? !matchesPattern(relative) : matchesPattern(relative))
                .Select(candidate => addBase(convertToNative(candidate)) )
                .ToArray();

            string removeBase(string path) => path.Substring(len - 1);
            string convertToNative(string path) => path.Replace('/', Path.DirectorySeparatorChar);
            string addBase(string path) => Path.Combine(baseDirectory, path.Substring(1));
            string convertToUnix(string dos) => dos.Replace(Path.DirectorySeparatorChar, '/');
        }
    }
}
